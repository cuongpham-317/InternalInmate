using InternalInmate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternalInmate.Infrastructure.Data.Configurations
{
    public class ProfileCategoryConfiguration : IEntityTypeConfiguration<ProfileCategory>
    {
        public void Configure(EntityTypeBuilder<ProfileCategory> builder)
        {
            builder.ToTable("ProfileCategory");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
        }
    }

}

