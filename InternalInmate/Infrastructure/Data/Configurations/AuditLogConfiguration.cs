using InternalInmate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternalInmate.Infrastructure.Data.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLog");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EntityType).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Detail).HasMaxLength(1000);
            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.CreatedAt);
        }
    }

}
