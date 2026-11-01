using InternalInmate.Domain.Entities;
using InternalInmate.Domain.Interfaces;
using InternalInmate.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Infrastructure.Repositories
{
    public class ProfileDocumentRepository : IProfileDocumentRepository
    {
        private readonly string _connectionString;

        public ProfileDocumentRepository(AppSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<IReadOnlyList<ProfileDocument>> GetByProfileIdAsync(int profileId, CancellationToken cancellationToken = default)
        {
            var list = new List<ProfileDocument>();

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var query = @"
SELECT Id, ProfileId, OriginalFileName, StoredFileName, RelativePath, ContentType, SizeBytes, Description, UploadedAt
FROM ProfileDocument
WHERE ProfileId = @ProfileId
ORDER BY UploadedAt DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProfileId", profileId);
                    using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            list.Add(new ProfileDocument
                            {
                                Id = reader.GetInt32(0),
                                ProfileId = reader.GetInt32(1),
                                OriginalFileName = reader.GetString(2),
                                StoredFileName = reader.GetString(3),
                                RelativePath = reader.GetString(4),
                                ContentType = reader.IsDBNull(5) ? null : reader.GetString(5),
                                SizeBytes = reader.GetInt64(6),
                                Description = reader.IsDBNull(7) ? null : reader.GetString(7),
                                UploadedAt = reader.GetDateTime(8)
                            });
                        }
                    }
                }
            }

            return list;
        }

        public async Task<ProfileDocument> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var query = @"
SELECT Id, ProfileId, OriginalFileName, StoredFileName, RelativePath, ContentType, SizeBytes, Description, UploadedAt
FROM ProfileDocument
WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        if (await reader.ReadAsync(cancellationToken))
                        {
                            return new ProfileDocument
                            {
                                Id = reader.GetInt32(0),
                                ProfileId = reader.GetInt32(1),
                                OriginalFileName = reader.GetString(2),
                                StoredFileName = reader.GetString(3),
                                RelativePath = reader.GetString(4),
                                ContentType = reader.IsDBNull(5) ? null : reader.GetString(5),
                                SizeBytes = reader.GetInt64(6),
                                Description = reader.IsDBNull(7) ? null : reader.GetString(7),
                                UploadedAt = reader.GetDateTime(8)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task AddAsync(ProfileDocument document, CancellationToken cancellationToken = default)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var sql = @"
INSERT INTO ProfileDocument (ProfileId, OriginalFileName, StoredFileName, RelativePath, ContentType, SizeBytes, Description, UploadedAt)
VALUES (@ProfileId, @OriginalFileName, @StoredFileName, @RelativePath, @ContentType, @SizeBytes, @Description, @UploadedAt);
SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProfileId", document.ProfileId);
                    cmd.Parameters.AddWithValue("@OriginalFileName", document.OriginalFileName ?? string.Empty);
                    cmd.Parameters.AddWithValue("@StoredFileName", document.StoredFileName ?? string.Empty);
                    cmd.Parameters.AddWithValue("@RelativePath", document.RelativePath ?? string.Empty);
                    cmd.Parameters.AddWithValue("@ContentType", (object)document.ContentType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SizeBytes", document.SizeBytes);
                    cmd.Parameters.AddWithValue("@Description", (object)document.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UploadedAt", document.UploadedAt);

                    document.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync(cancellationToken));
                }
            }
        }

        public void Remove(ProfileDocument document)
        {
            if (document == null) return;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "DELETE FROM ProfileDocument WHERE Id = @Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", document.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
