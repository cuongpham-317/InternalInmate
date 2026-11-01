using InternalInmate.Application.DTOs;
using InternalInmate.Application.Interfaces;
using InternalInmate.Domain.Entities;
using InternalInmate.Domain.Enums;
using InternalInmate.Domain.Exceptions;
using InternalInmate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Application.Services
{
    public class DocumentService
    {
        private readonly IProfileRepository _profiles;
        private readonly IProfileDocumentRepository _documents;
        private readonly IFileStorageService _files;
        private readonly IAuditLogRepository _audit;
        private readonly IClock _clock;

        public DocumentService(
            IProfileRepository profiles,
            IProfileDocumentRepository documents,
            IFileStorageService files,
            IAuditLogRepository audit,
            IClock clock)
        {
            _profiles = profiles;
            _documents = documents;
            _files = files;
            _audit = audit;
            _clock = clock;
        }

        public async Task<IReadOnlyList<ProfileDocumentDto>> ListAsync(int profileId, CancellationToken cancellationToken = default)
        {
            var items = await _documents.GetByProfileIdAsync(profileId, cancellationToken);
            return items.Select(ToDto).ToList();
        }

        public async Task<ProfileDocumentDto> AttachAsync(AttachDocumentRequest request, CancellationToken cancellationToken = default)
        {
            var profile = await _profiles.GetByIdAsync(request.ProfileId, false, cancellationToken);
            if (profile == null)
            {
                throw new DomainException("Không tìm thấy hồ sơ.");
            }

            StoredFileInfo stored;
            try
            {
                stored = _files.Save(request.SourceFilePath, profile.Id);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Không lưu được tệp đính kèm: {ex.Message}");
            }

            var document = new ProfileDocument
            {
                ProfileId = profile.Id,
                OriginalFileName = Path.GetFileName(request.SourceFilePath),
                StoredFileName = stored.StoredFileName,
                RelativePath = stored.RelativePath,
                ContentType = stored.ContentType,
                SizeBytes = stored.SizeBytes,
                Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim(),
                UploadedAt = _clock.UtcNow
            };

            try
            {
                await _documents.AddAsync(document, cancellationToken);
                await _documents.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _files.Delete(stored.RelativePath);
                throw new DomainException("Đã sao chép tệp nhưng không ghi được database. Tệp tạm đã được gỡ.");
            }

            await _audit.AddAsync(new AuditLog
            {
                Action = AuditAction.DocumentAttached,
                EntityType = nameof(ProfileDocument),
                EntityId = document.Id,
                Detail = $"{profile.Code}: {document.OriginalFileName}",
                UserName = Environment.UserName,
                CreatedAt = _clock.UtcNow
            }, cancellationToken);
            await _audit.SaveChangesAsync(cancellationToken);

            return ToDto(document);
        }

        public async Task OpenAsync(int documentId, Action<string> openFile, CancellationToken cancellationToken = default)
        {
            var document = await RequireAsync(documentId, cancellationToken);
            var path = _files.GetAbsolutePath(document.RelativePath);
            if (!_files.Exists(document.RelativePath))
            {
                throw new DomainException("Tệp đính kèm không còn trên đĩa. Hãy khôi phục từ bản backup.");
            }

            openFile(path);
        }

        public async Task ExportCopyAsync(int documentId, string destinationFilePath, CancellationToken cancellationToken = default)
        {
            var document = await RequireAsync(documentId, cancellationToken);
            if (!_files.Exists(document.RelativePath))
            {
                throw new DomainException("Tệp đính kèm không còn trên đĩa.");
            }

            _files.CopyTo(document.RelativePath, destinationFilePath);
        }

        public async Task DeleteAsync(int documentId, CancellationToken cancellationToken = default)
        {
            var document = await RequireAsync(documentId, cancellationToken);
            var relativePath = document.RelativePath;
            var name = document.OriginalFileName;
            var profileId = document.ProfileId;

            _documents.Remove(document);
            await _documents.SaveChangesAsync(cancellationToken);

            try
            {
                _files.Delete(relativePath);
            }
            catch
            {
                // Metadata đã xóa; file mồ côi sẽ được dọn khi backup/restore hoặc lần xóa sau.
            }

            await _audit.AddAsync(new AuditLog
            {
                Action = AuditAction.DocumentDeleted,
                EntityType = nameof(ProfileDocument),
                EntityId = documentId,
                Detail = $"Profile {profileId}: {name}",
                UserName = Environment.UserName,
                CreatedAt = _clock.UtcNow
            }, cancellationToken);
            await _audit.SaveChangesAsync(cancellationToken);
        }

        private async Task<ProfileDocument> RequireAsync(int id, CancellationToken cancellationToken)
        {
            var doc = await _documents.GetByIdAsync(id, cancellationToken);
            if (doc == null)
            {
                throw new DomainException("Không tìm thấy tài liệu.");
            }
            return doc;
        }

        private ProfileDocumentDto ToDto(ProfileDocument document)
        {
            return new ProfileDocumentDto
            {
                Id = document.Id,
                OriginalFileName = document.OriginalFileName,
                SizeBytes = document.SizeBytes,
                Description = document.Description,
                UploadedAt = document.UploadedAt,
                FileExists = _files.Exists(document.RelativePath)
            };
        }
    }
}
