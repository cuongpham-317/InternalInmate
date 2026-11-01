using System.Collections.Generic;

namespace InternalInmate.Domain.Entities
{
    public class ProfileCategory
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; }

        public ICollection<Profile> Profiles { get; set; } = new List<Profile>();
    }

}

