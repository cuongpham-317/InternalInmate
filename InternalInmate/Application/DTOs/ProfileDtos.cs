using InternalInmate.Domain.Enums;
using System;
using System.Collections.Generic;

namespace InternalInmate.Application.DTOs
{
    public class ProfileListItemDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public ProfileStatus Status { get; set; }
        public string PrimaryAddress { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string AvatarPath { get; set; }

        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case ProfileStatus.Draft: return "Bản nháp";
                    case ProfileStatus.Active: return "Hoạt động";
                    case ProfileStatus.Suspended: return "Tạm dừng";
                    case ProfileStatus.Closed: return "Đã đóng";
                    default: return Status.ToString();
                }
            }
        }
    }

    public class ProfileDetailDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string IdentityNumber { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public ProfileStatus Status { get; set; }
        public string Notes { get; set; }
        public string AvatarPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProfileAddressDto> Addresses { get; set; } = new List<ProfileAddressDto>();
        public List<ProfileDocumentDto> Documents { get; set; } = new List<ProfileDocumentDto>();
    }

    public class SaveProfileRequest
    {
        public int? Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string IdentityNumber { get; set; }
        public int CategoryId { get; set; }
        public ProfileStatus Status { get; set; }
        public string Notes { get; set; }
        public string AvatarPath { get; set; }
        public List<ProfileAddressDto> Addresses { get; set; } = new List<ProfileAddressDto>();
    }

    public class ProfileAddressDto
    {
        public int Id { get; set; }
        public string AddressLine { get; set; } = string.Empty;
        public string Ward { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Country { get; set; } = "Việt Nam";
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsPrimary { get; set; }
        public string Note { get; set; }
        public string DisplayText { get; set; } = string.Empty;
    }

    public class ProfileDocumentDto
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;
        public long SizeBytes { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool FileExists { get; set; }

        public string SizeFormatted
        {
            get
            {
                if (SizeBytes < 1024) return $"{SizeBytes} B";
                if (SizeBytes < 1024 * 1024) return $"{SizeBytes / 1024.0:F1} KB";
                return $"{SizeBytes / (1024.0 * 1024.0):F2} MB";
            }
        }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class ProfileSearchRequest
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
        public ProfileStatus? Status { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }

    public class AttachDocumentRequest
    {
        public int ProfileId { get; set; }
        public string SourceFilePath { get; set; } = string.Empty;
        public string Description { get; set; }
    }

    public class GeocodeResult
    {
        public string DisplayName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
