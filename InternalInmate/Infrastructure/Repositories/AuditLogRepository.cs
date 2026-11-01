using InternalInmate.Domain.Entities;
using InternalInmate.Domain.Interfaces;
using InternalInmate.Infrastructure.Configurations;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly string _connectionString;

        public AuditLogRepository(AppSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task AddAsync(AuditLog log, CancellationToken cancellationToken = default)
        {
            if (log == null) return;

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var sql = @"
INSERT INTO AuditLog (Action, EntityType, EntityId, Detail, UserName, CreatedAt)
VALUES (@Action, @EntityType, @EntityId, @Detail, @UserName, @CreatedAt);
SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Action", (int)log.Action);
                    cmd.Parameters.AddWithValue("@EntityType", log.EntityType ?? string.Empty);
                    cmd.Parameters.AddWithValue("@EntityId", (object)log.EntityId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Detail", (object)log.Detail ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserName", log.UserName ?? Environment.UserName);
                    cmd.Parameters.AddWithValue("@CreatedAt", log.CreatedAt);

                    log.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync(cancellationToken));
                }
            }
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
