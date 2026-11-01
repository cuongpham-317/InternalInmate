using InternalInmate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternalInmate.Infrastructure.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<ProfileAddress> ProfileAddresses => Set<ProfileAddress>();
        public DbSet<ProfileDocument> ProfileDocuments => Set<ProfileDocument>();
        public DbSet<ProfileCategory> ProfileCategories => Set<ProfileCategory>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }

}
