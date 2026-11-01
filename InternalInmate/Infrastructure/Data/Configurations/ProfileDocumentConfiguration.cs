using InternalInmate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternalInmate.Infrastructure.Data.Configurations
{
    public class ProfileDocumentConfiguration : IEntityTypeConfiguration<ProfileDocument>
    {
        public void Configure(EntityTypeBuilder<ProfileDocument> builder)
        {
            builder.ToTable("ProfileDocument");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OriginalFileName).HasMaxLength(260).IsRequired();
            builder.Property(x => x.StoredFileName).HasMaxLength(260).IsRequired();
            builder.Property(x => x.RelativePath).HasMaxLength(500).IsRequired();
            builder.Property(x => x.ContentType).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.HasIndex(x => x.ProfileId);

            builder.HasOne(x => x.Profile)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

