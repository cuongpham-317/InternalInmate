using System;

namespace InternalInmate.Domain.Entities
{
    public class ProfileDocument
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string RelativePath { get; set; } = string.Empty;
        public string ContentType { get; set; }
        public long SizeBytes { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; }

        public Profile Profile { get; set; }
    }

}

