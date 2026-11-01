using InternalInmate.Domain.Entities;
using InternalInmate.Domain.Interfaces;
using InternalInmate.Infrastructure.Configurations;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Infrastructure.Repositories
{
    public class ProfileCategoryRepository : IProfileCategoryRepository
    {
        private readonly string _connectionString;

        public ProfileCategoryRepository(AppSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<IReadOnlyList<ProfileCategory>> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            var list = new List<ProfileCategory>();

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var query = "SELECT Id, Code, Name, IsActive, SortOrder FROM ProfileCategory WHERE IsActive = 1 ORDER BY SortOrder, Name";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        list.Add(new ProfileCategory
                        {
                            Id = reader.GetInt32(0),
                            Code = reader.GetString(1),
                            Name = reader.GetString(2),
                            IsActive = reader.GetBoolean(3),
                            SortOrder = reader.GetInt32(4)
                        });
                    }
                }
            }

            return list;
        }

        public async Task<ProfileCategory> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var query = "SELECT Id, Code, Name, IsActive, SortOrder FROM ProfileCategory WHERE Id = @Id AND IsActive = 1";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        if (await reader.ReadAsync(cancellationToken))
                        {
                            return new ProfileCategory
                            {
                                Id = reader.GetInt32(0),
                                Code = reader.GetString(1),
                                Name = reader.GetString(2),
                                IsActive = reader.GetBoolean(3),
                                SortOrder = reader.GetInt32(4)
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
