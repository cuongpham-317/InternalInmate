using InternalInmate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternalInmate.Infrastructure.Data.Configurations
{
    public class ProfileAddressConfiguration : IEntityTypeConfiguration<ProfileAddress>
    {
        public void Configure(EntityTypeBuilder<ProfileAddress> builder)
        {
            builder.ToTable("ProfileAddress");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AddressLine).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Ward).HasMaxLength(100);
            builder.Property(x => x.District).HasMaxLength(100);
            builder.Property(x => x.Province).HasMaxLength(100);
            builder.Property(x => x.Country).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);
            builder.HasIndex(x => x.ProfileId);
            builder.HasIndex(x => x.AddressLine);

            builder.HasOne(x => x.Profile)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

