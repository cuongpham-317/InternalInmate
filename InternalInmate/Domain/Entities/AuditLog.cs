using InternalInmate.Domain.Enums;
using System;

namespace InternalInmate.Domain.Entities
{

    public class AuditLog
    {
        public int Id { get; set; }
        public AuditAction Action { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public string Detail { get; set; }
        public string UserName { get; set; } = "local";
        public DateTime CreatedAt { get; set; }
    }

}
