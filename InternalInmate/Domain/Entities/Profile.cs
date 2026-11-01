using InternalInmate.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternalInmate.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;
        public string IdentityNumber { get; set; }
        public int CategoryId { get; set; }
        public ProfileStatus Status { get; set; } = ProfileStatus.Draft;
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string AvatarPath { get; set; }

        public ProfileCategory Category { get; set; }
        public ICollection<ProfileAddress> Addresses { get; set; } = new List<ProfileAddress>();
        public ICollection<ProfileDocument> Documents { get; set; } = new List<ProfileDocument>();

        public void SoftDelete(DateTime utcNow)
        {
            if (IsDeleted)
            {
                return;
            }

            IsDeleted = true;
            DeletedAt = utcNow;
            UpdatedAt = utcNow;
            Status = ProfileStatus.Closed;
        }

        public ProfileAddress GetPrimaryAddress()
        {
            if (Addresses == null || Addresses.Count == 0)
                return null;

            return Addresses.FirstOrDefault(a => a.IsPrimary) ?? Addresses.FirstOrDefault();
        }

        public void EnsureSinglePrimaryAddress()
        {
            if (Addresses == null) return;

            var primaries = Addresses.Where(a => a.IsPrimary).ToList();
            if (primaries.Count <= 1)
            {
                return;
            }

            foreach (var extra in primaries.Skip(1))
            {
                extra.IsPrimary = false;
            }
        }
    }
}
