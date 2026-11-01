using InternalInmate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternalInmate.Infrastructure.Data.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profile");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.IdentityNumber).HasMaxLength(50);
            builder.Property(x => x.Notes).HasMaxLength(2000);
            builder.HasIndex(x => x.FullName);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.CreatedAt);
            builder.HasIndex(x => new { x.Code, x.IsDeleted })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Profiles)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }

}

