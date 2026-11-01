using InternalInmate.Application.DTOs;
using InternalInmate.Application.Validators;
using InternalInmate.Domain.Entities;
using InternalInmate.Domain.Enums;
using InternalInmate.Domain.Exceptions;
using InternalInmate.Domain.Interfaces;
using InternalInmate.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Application.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository _profiles;
        private readonly IProfileCategoryRepository _categories;
        private readonly IAuditLogRepository _audit;
        private readonly IClock _clock;

        public ProfileService(
            IProfileRepository profiles,
            IProfileCategoryRepository categories,
            IAuditLogRepository audit,
            IClock clock)
        {
            _profiles = profiles;
            _categories = categories;
            _audit = audit;
            _clock = clock;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var items = await _categories.GetActiveAsync(cancellationToken);
            return items.Select(c => new CategoryDto { Id = c.Id, Code = c.Code, Name = c.Name }).ToList();
        }

        public async Task<IReadOnlyList<ProfileListItemDto>> SearchAsync(ProfileSearchRequest request, CancellationToken cancellationToken = default)
        {
            var criteria = new ProfileSearchCriteria
            {
                Keyword = request?.Keyword,
                CategoryId = request?.CategoryId,
                Status = request?.Status,
                CreatedFrom = request?.CreatedFrom,
                CreatedTo = request?.CreatedTo
            };

            var profiles = await _profiles.SearchAsync(criteria, cancellationToken);
            return profiles.Select(ToListItem).ToList();
        }

        public async Task<ProfileDetailDto> GetDetailAsync(int id, CancellationToken cancellationToken = default)
        {
            var profile = await _profiles.GetByIdAsync(id, true, cancellationToken);
            if (profile == null)
            {
                throw new DomainException("Không tìm thấy hồ sơ.");
            }

            return ToDetail(profile);
        }

        public async Task<int> SaveAsync(SaveProfileRequest request, CancellationToken cancellationToken = default)
        {
            var validation = SaveProfileValidator.Validate(request);
            if (!validation.IsValid)
            {
                throw new DomainException(string.Join(Environment.NewLine, validation.Errors));
            }

            var category = await _categories.GetByIdAsync(request.CategoryId, cancellationToken);
            if (category == null)
            {
                throw new DomainException("Loại hồ sơ không tồn tại hoặc đã ngừng sử dụng.");
            }

            var code = request.Code.Trim();
            var existingCode = await _profiles.GetByCodeAsync(code, cancellationToken);
            if (existingCode != null && existingCode.Id != (request.Id ?? 0))
            {
                throw new DomainException($"Mã hồ sơ '{code}' đã tồn tại.");
            }

            var now = _clock.UtcNow;
            Profile profile;
            AuditAction action;

            if (!request.Id.HasValue || request.Id.Value == 0)
            {
                profile = new Profile
                {
                    CreatedAt = now,
                    UpdatedAt = now
                };
                action = AuditAction.Created;
            }
            else
            {
                profile = await _profiles.GetByIdAsync(request.Id.Value, true, cancellationToken);
                if (profile == null)
                {
                    throw new DomainException("Không tìm thấy hồ sơ.");
                }
                action = AuditAction.Updated;
            }

            profile.Code = code;
            profile.FullName = request.FullName.Trim();
            profile.DateOfBirth = request.DateOfBirth.HasValue ? request.DateOfBirth.Value.Date : (DateTime?)null;
            profile.Gender = request.Gender;
            profile.IdentityNumber = string.IsNullOrWhiteSpace(request.IdentityNumber) ? null : request.IdentityNumber.Trim();
            profile.CategoryId = category.Id;
            profile.Status = request.Status;
            profile.Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim();
            profile.AvatarPath = string.IsNullOrWhiteSpace(request.AvatarPath) ? null : request.AvatarPath.Trim();
            profile.UpdatedAt = now;

            SyncAddresses(profile, request.Addresses ?? new List<ProfileAddressDto>());
            profile.EnsureSinglePrimaryAddress();

            if (action == AuditAction.Created)
            {
                await _profiles.AddAsync(profile, cancellationToken);
            }
            else
            {
                _profiles.Update(profile);
            }

            await _profiles.SaveChangesAsync(cancellationToken);
            await WriteAuditAsync(action, profile.Id, $"{profile.Code} - {profile.FullName}", cancellationToken);
            return profile.Id;
        }

        public async Task SoftDeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var profile = await _profiles.GetByIdAsync(id, false, cancellationToken);
            if (profile == null)
            {
                throw new DomainException("Không tìm thấy hồ sơ.");
            }

            profile.SoftDelete(_clock.UtcNow);
            _profiles.Update(profile);
            await _profiles.SaveChangesAsync(cancellationToken);
            await WriteAuditAsync(AuditAction.SoftDeleted, profile.Id, profile.Code, cancellationToken);
        }

        private static void SyncAddresses(Profile profile, List<ProfileAddressDto> incoming)
        {
            if (profile.Addresses == null)
            {
                profile.Addresses = new List<ProfileAddress>();
            }

            var keepIds = new HashSet<int>(incoming.Where(a => a.Id > 0).Select(a => a.Id));
            var toRemove = profile.Addresses.Where(a => !keepIds.Contains(a.Id)).ToList();
            foreach (var address in toRemove)
            {
                profile.Addresses.Remove(address);
            }

            foreach (var dto in incoming)
            {
                ProfileAddress entity;
                if (dto.Id > 0)
                {
                    entity = profile.Addresses.FirstOrDefault(a => a.Id == dto.Id);
                    if (entity == null)
                    {
                        throw new DomainException("Địa chỉ không thuộc hồ sơ này.");
                    }
                }
                else
                {
                    entity = new ProfileAddress();
                    profile.Addresses.Add(entity);
                }

                entity.AddressLine = dto.AddressLine != null ? dto.AddressLine.Trim() : string.Empty;
                entity.Ward = EmptyToNull(dto.Ward);
                entity.District = EmptyToNull(dto.District);
                entity.Province = EmptyToNull(dto.Province);
                entity.Country = string.IsNullOrWhiteSpace(dto.Country) ? "Việt Nam" : dto.Country.Trim();
                entity.IsPrimary = dto.IsPrimary;
                entity.Note = EmptyToNull(dto.Note);

                if (dto.Latitude.HasValue && dto.Longitude.HasValue)
                {
                    entity.SetCoordinate(GeoCoordinate.Create(dto.Latitude.Value, dto.Longitude.Value));
                }
                else
                {
                    entity.Latitude = null;
                    entity.Longitude = null;
                }
            }
        }

        private async Task WriteAuditAsync(AuditAction action, int entityId, string detail, CancellationToken cancellationToken)
        {
            await _audit.AddAsync(new AuditLog
            {
                Action = action,
                EntityType = nameof(Profile),
                EntityId = entityId,
                Detail = detail,
                UserName = Environment.UserName,
                CreatedAt = _clock.UtcNow
            }, cancellationToken);
            await _audit.SaveChangesAsync(cancellationToken);
        }

        private static string EmptyToNull(string value)
            => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        private static ProfileListItemDto ToListItem(Profile profile)
        {
            var primary = profile.GetPrimaryAddress();
            return new ProfileListItemDto
            {
                Id = profile.Id,
                Code = profile.Code,
                FullName = profile.FullName,
                CategoryName = profile.Category != null ? profile.Category.Name : string.Empty,
                Status = profile.Status,
                PrimaryAddress = primary != null ? primary.DisplayText : string.Empty,
                CreatedAt = profile.CreatedAt,
                AvatarPath = profile.AvatarPath
            };
        }

        private static ProfileDetailDto ToDetail(Profile profile)
        {
            return new ProfileDetailDto
            {
                Id = profile.Id,
                Code = profile.Code,
                FullName = profile.FullName,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                IdentityNumber = profile.IdentityNumber,
                CategoryId = profile.CategoryId,
                CategoryName = profile.Category != null ? profile.Category.Name : string.Empty,
                Status = profile.Status,
                Notes = profile.Notes,
                AvatarPath = profile.AvatarPath,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt,
                Addresses = profile.Addresses.Select(a => new ProfileAddressDto
                {
                    Id = a.Id,
                    AddressLine = a.AddressLine,
                    Ward = a.Ward,
                    District = a.District,
                    Province = a.Province,
                    Country = a.Country,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    IsPrimary = a.IsPrimary,
                    Note = a.Note,
                    DisplayText = a.DisplayText
                }).ToList(),
                Documents = profile.Documents.Select(d => new ProfileDocumentDto
                {
                    Id = d.Id,
                    OriginalFileName = d.OriginalFileName,
                    SizeBytes = d.SizeBytes,
                    Description = d.Description,
                    UploadedAt = d.UploadedAt
                }).ToList()
            };
        }
    }
}
