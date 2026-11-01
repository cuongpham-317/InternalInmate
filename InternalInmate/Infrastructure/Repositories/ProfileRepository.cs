using InternalInmate.Domain.Entities;
using InternalInmate.Domain.Enums;
using InternalInmate.Domain.Interfaces;
using InternalInmate.Domain.ValueObjects;
using InternalInmate.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly string _connectionString;

        public ProfileRepository(AppSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<Profile> GetByIdAsync(int id, bool includeRelated = true, CancellationToken cancellationToken = default)
        {
            Profile profile = null;

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);

                var query = @"
SELECT p.Id, p.Code, p.FullName, p.DateOfBirth, p.Gender, p.IdentityNumber,
       p.CategoryId, p.Status, p.Notes, p.CreatedAt, p.UpdatedAt, p.IsDeleted, p.DeletedAt, p.AvatarPath,
       c.Code AS CategoryCode, c.Name AS CategoryName
FROM Profile p
LEFT JOIN ProfileCategory c ON p.CategoryId = c.Id
WHERE p.Id = @Id AND p.IsDeleted = 0";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        if (await reader.ReadAsync(cancellationToken))
                        {
                            profile = MapProfile(reader);
                        }
                    }
                }

                if (profile != null && includeRelated)
                {
                    // Load addresses
                    var addrQuery = @"
SELECT Id, ProfileId, AddressLine, Ward, District, Province, Country, Latitude, Longitude, IsPrimary, Note
FROM ProfileAddress
WHERE ProfileId = @ProfileId
ORDER BY IsPrimary DESC, Id ASC";

                    using (var cmd = new SqlCommand(addrQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProfileId", id);
                        using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                        {
                            while (await reader.ReadAsync(cancellationToken))
                            {
                                profile.Addresses.Add(MapAddress(reader));
                            }
                        }
                    }

                    // Load documents
                    var docQuery = @"
SELECT Id, ProfileId, OriginalFileName, StoredFileName, RelativePath, ContentType, SizeBytes, Description, UploadedAt
FROM ProfileDocument
WHERE ProfileId = @ProfileId
ORDER BY UploadedAt DESC";

                    using (var cmd = new SqlCommand(docQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProfileId", id);
                        using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                        {
                            while (await reader.ReadAsync(cancellationToken))
                            {
                                profile.Documents.Add(MapDocument(reader));
                            }
                        }
                    }
                }
            }

            return profile;
        }

        public async Task<Profile> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var query = @"
SELECT p.Id, p.Code, p.FullName, p.DateOfBirth, p.Gender, p.IdentityNumber,
       p.CategoryId, p.Status, p.Notes, p.CreatedAt, p.UpdatedAt, p.IsDeleted, p.DeletedAt, p.AvatarPath,
       c.Code AS CategoryCode, c.Name AS CategoryName
FROM Profile p
LEFT JOIN ProfileCategory c ON p.CategoryId = c.Id
WHERE p.Code = @Code AND p.IsDeleted = 0";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Code", code.Trim());
                    using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        if (await reader.ReadAsync(cancellationToken))
                        {
                            return MapProfile(reader);
                        }
                    }
                }
            }
            return null;
        }

        public async Task<IReadOnlyList<Profile>> SearchAsync(ProfileSearchCriteria criteria, CancellationToken cancellationToken = default)
        {
            var list = new List<Profile>();

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);

                var sb = new StringBuilder(@"
SELECT DISTINCT TOP 500
       p.Id, p.Code, p.FullName, p.DateOfBirth, p.Gender, p.IdentityNumber,
       p.CategoryId, p.Status, p.Notes, p.CreatedAt, p.UpdatedAt, p.IsDeleted, p.DeletedAt, p.AvatarPath,
       c.Code AS CategoryCode, c.Name AS CategoryName,
       pa.AddressLine, pa.Ward, pa.District, pa.Province, pa.Country, pa.Latitude, pa.Longitude, pa.IsPrimary
FROM Profile p
LEFT JOIN ProfileCategory c ON p.CategoryId = c.Id
LEFT JOIN ProfileAddress pa ON p.Id = pa.ProfileId AND pa.IsPrimary = 1
WHERE p.IsDeleted = 0");

                var cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrWhiteSpace(criteria?.Keyword))
                {
                    var kw = "%" + criteria.Keyword.Trim() + "%";
                    sb.Append(@" AND (
                        p.Code LIKE @Keyword 
                        OR p.FullName LIKE @Keyword 
                        OR p.IdentityNumber LIKE @Keyword 
                        OR c.Name LIKE @Keyword
                        OR EXISTS (
                            SELECT 1 FROM ProfileAddress a 
                            WHERE a.ProfileId = p.Id AND (
                                a.AddressLine LIKE @Keyword OR 
                                a.Province LIKE @Keyword OR 
                                a.District LIKE @Keyword OR 
                                a.Ward LIKE @Keyword
                            )
                        )
                    )");
                    cmd.Parameters.AddWithValue("@Keyword", kw);
                }

                if (criteria?.CategoryId.HasValue == true && criteria.CategoryId.Value > 0)
                {
                    sb.Append(" AND p.CategoryId = @CategoryId");
                    cmd.Parameters.AddWithValue("@CategoryId", criteria.CategoryId.Value);
                }

                if (criteria?.Status.HasValue == true)
                {
                    sb.Append(" AND p.Status = @Status");
                    cmd.Parameters.AddWithValue("@Status", (int)criteria.Status.Value);
                }

                if (criteria?.CreatedFrom.HasValue == true)
                {
                    sb.Append(" AND p.CreatedAt >= @CreatedFrom");
                    cmd.Parameters.AddWithValue("@CreatedFrom", criteria.CreatedFrom.Value.Date);
                }

                if (criteria?.CreatedTo.HasValue == true)
                {
                    sb.Append(" AND p.CreatedAt < @CreatedTo");
                    cmd.Parameters.AddWithValue("@CreatedTo", criteria.CreatedTo.Value.Date.AddDays(1));
                }

                sb.Append(" ORDER BY p.UpdatedAt DESC");
                cmd.CommandText = sb.ToString();

                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        var profile = MapProfile(reader);

                        // Attach primary address if returned
                        if (!reader.IsDBNull(reader.GetOrdinal("AddressLine")))
                        {
                            var primaryAddress = new ProfileAddress
                            {
                                ProfileId = profile.Id,
                                AddressLine = reader.GetString(reader.GetOrdinal("AddressLine")),
                                Ward = reader.IsDBNull(reader.GetOrdinal("Ward")) ? null : reader.GetString(reader.GetOrdinal("Ward")),
                                District = reader.IsDBNull(reader.GetOrdinal("District")) ? null : reader.GetString(reader.GetOrdinal("District")),
                                Province = reader.IsDBNull(reader.GetOrdinal("Province")) ? null : reader.GetString(reader.GetOrdinal("Province")),
                                Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? "Việt Nam" : reader.GetString(reader.GetOrdinal("Country")),
                                Latitude = reader.IsDBNull(reader.GetOrdinal("Latitude")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("Latitude")),
                                Longitude = reader.IsDBNull(reader.GetOrdinal("Longitude")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("Longitude")),
                                IsPrimary = true
                            };
                            profile.Addresses.Add(primaryAddress);
                        }

                        list.Add(profile);
                    }
                }
            }

            return list;
        }

        public async Task AddAsync(Profile profile, CancellationToken cancellationToken = default)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                using (var trans = conn.BeginTransaction())
                {
                    var sql = @"
INSERT INTO Profile (Code, FullName, DateOfBirth, Gender, IdentityNumber, CategoryId, Status, Notes, CreatedAt, UpdatedAt, IsDeleted, AvatarPath)
VALUES (@Code, @FullName, @DateOfBirth, @Gender, @IdentityNumber, @CategoryId, @Status, @Notes, @CreatedAt, @UpdatedAt, @IsDeleted, @AvatarPath);
SELECT SCOPE_IDENTITY();";

                    using (var cmd = new SqlCommand(sql, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@Code", profile.Code ?? string.Empty);
                        cmd.Parameters.AddWithValue("@FullName", profile.FullName ?? string.Empty);
                        cmd.Parameters.AddWithValue("@DateOfBirth", (object)profile.DateOfBirth ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", (int)profile.Gender);
                        cmd.Parameters.AddWithValue("@IdentityNumber", (object)profile.IdentityNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CategoryId", profile.CategoryId);
                        cmd.Parameters.AddWithValue("@Status", (int)profile.Status);
                        cmd.Parameters.AddWithValue("@Notes", (object)profile.Notes ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedAt", profile.CreatedAt > new DateTime(1753, 1, 1) ? profile.CreatedAt : DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@UpdatedAt", profile.UpdatedAt > new DateTime(1753, 1, 1) ? profile.UpdatedAt : DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@IsDeleted", profile.IsDeleted);
                        cmd.Parameters.AddWithValue("@AvatarPath", (object)profile.AvatarPath ?? DBNull.Value);

                        var newId = Convert.ToInt32(await cmd.ExecuteScalarAsync(cancellationToken));
                        profile.Id = newId;
                    }

                    // Save addresses
                    if (profile.Addresses != null)
                    {
                        foreach (var addr in profile.Addresses)
                        {
                            addr.ProfileId = profile.Id;
                            var addrSql = @"
INSERT INTO ProfileAddress (ProfileId, AddressLine, Ward, District, Province, Country, Latitude, Longitude, IsPrimary, Note)
VALUES (@ProfileId, @AddressLine, @Ward, @District, @Province, @Country, @Latitude, @Longitude, @IsPrimary, @Note);
SELECT SCOPE_IDENTITY();";
                            using (var cmd = new SqlCommand(addrSql, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ProfileId", addr.ProfileId);
                                cmd.Parameters.AddWithValue("@AddressLine", addr.AddressLine ?? string.Empty);
                                cmd.Parameters.AddWithValue("@Ward", (object)addr.Ward ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@District", (object)addr.District ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Province", (object)addr.Province ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Country", (object)addr.Country ?? "Việt Nam");
                                cmd.Parameters.AddWithValue("@Latitude", (object)addr.Latitude ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Longitude", (object)addr.Longitude ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IsPrimary", addr.IsPrimary);
                                cmd.Parameters.AddWithValue("@Note", (object)addr.Note ?? DBNull.Value);

                                addr.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync(cancellationToken));
                            }
                        }
                    }

                    trans.Commit();
                }
            }
        }

        public void Update(Profile profile)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    var sql = @"
UPDATE Profile 
SET Code = @Code,
    FullName = @FullName,
    DateOfBirth = @DateOfBirth,
    Gender = @Gender,
    IdentityNumber = @IdentityNumber,
    CategoryId = @CategoryId,
    Status = @Status,
    Notes = @Notes,
    UpdatedAt = @UpdatedAt,
    IsDeleted = @IsDeleted,
    DeletedAt = @DeletedAt,
    AvatarPath = @AvatarPath
WHERE Id = @Id";

                    using (var cmd = new SqlCommand(sql, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@Id", profile.Id);
                        cmd.Parameters.AddWithValue("@Code", profile.Code ?? string.Empty);
                        cmd.Parameters.AddWithValue("@FullName", profile.FullName ?? string.Empty);
                        cmd.Parameters.AddWithValue("@DateOfBirth", (object)profile.DateOfBirth ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", (int)profile.Gender);
                        cmd.Parameters.AddWithValue("@IdentityNumber", (object)profile.IdentityNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CategoryId", profile.CategoryId);
                        cmd.Parameters.AddWithValue("@Status", (int)profile.Status);
                        cmd.Parameters.AddWithValue("@Notes", (object)profile.Notes ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedAt", profile.UpdatedAt > new DateTime(1753, 1, 1) ? profile.UpdatedAt : DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@IsDeleted", profile.IsDeleted);
                        cmd.Parameters.AddWithValue("@DeletedAt", (object)profile.DeletedAt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AvatarPath", (object)profile.AvatarPath ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    // Delete obsolete addresses and update/insert
                    var currentIds = profile.Addresses.Where(a => a.Id > 0).Select(a => a.Id).ToList();
                    var deleteSql = "DELETE FROM ProfileAddress WHERE ProfileId = @ProfileId";
                    if (currentIds.Count > 0)
                    {
                        deleteSql += $" AND Id NOT IN ({string.Join(",", currentIds)})";
                    }

                    using (var cmd = new SqlCommand(deleteSql, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@ProfileId", profile.Id);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (var addr in profile.Addresses)
                    {
                        if (addr.Id > 0)
                        {
                            var updAddrSql = @"
UPDATE ProfileAddress 
SET AddressLine = @AddressLine,
    Ward = @Ward,
    District = @District,
    Province = @Province,
    Country = @Country,
    Latitude = @Latitude,
    Longitude = @Longitude,
    IsPrimary = @IsPrimary,
    Note = @Note
WHERE Id = @Id";
                            using (var cmd = new SqlCommand(updAddrSql, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@Id", addr.Id);
                                cmd.Parameters.AddWithValue("@AddressLine", addr.AddressLine ?? string.Empty);
                                cmd.Parameters.AddWithValue("@Ward", (object)addr.Ward ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@District", (object)addr.District ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Province", (object)addr.Province ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Country", (object)addr.Country ?? "Việt Nam");
                                cmd.Parameters.AddWithValue("@Latitude", (object)addr.Latitude ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Longitude", (object)addr.Longitude ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IsPrimary", addr.IsPrimary);
                                cmd.Parameters.AddWithValue("@Note", (object)addr.Note ?? DBNull.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            var insAddrSql = @"
INSERT INTO ProfileAddress (ProfileId, AddressLine, Ward, District, Province, Country, Latitude, Longitude, IsPrimary, Note)
VALUES (@ProfileId, @AddressLine, @Ward, @District, @Province, @Country, @Latitude, @Longitude, @IsPrimary, @Note);
SELECT SCOPE_IDENTITY();";
                            using (var cmd = new SqlCommand(insAddrSql, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ProfileId", profile.Id);
                                cmd.Parameters.AddWithValue("@AddressLine", addr.AddressLine ?? string.Empty);
                                cmd.Parameters.AddWithValue("@Ward", (object)addr.Ward ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@District", (object)addr.District ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Province", (object)addr.Province ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Country", (object)addr.Country ?? "Việt Nam");
                                cmd.Parameters.AddWithValue("@Latitude", (object)addr.Latitude ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Longitude", (object)addr.Longitude ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IsPrimary", addr.IsPrimary);
                                cmd.Parameters.AddWithValue("@Note", (object)addr.Note ?? DBNull.Value);
                                addr.Id = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                        }
                    }

                    trans.Commit();
                }
            }
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Changes are committed in AddAsync and Update
            return Task.CompletedTask;
        }

        private static Profile MapProfile(SqlDataReader reader)
        {
            var p = new Profile
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Code = reader.GetString(reader.GetOrdinal("Code")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                Gender = (Gender)reader.GetInt32(reader.GetOrdinal("Gender")),
                IdentityNumber = reader.IsDBNull(reader.GetOrdinal("IdentityNumber")) ? null : reader.GetString(reader.GetOrdinal("IdentityNumber")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                Status = (ProfileStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
                DeletedAt = reader.IsDBNull(reader.GetOrdinal("DeletedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DeletedAt")),
                AvatarPath = reader.IsDBNull(reader.GetOrdinal("AvatarPath")) ? null : reader.GetString(reader.GetOrdinal("AvatarPath"))
            };

            var catNameOrd = reader.GetOrdinal("CategoryName");
            if (!reader.IsDBNull(catNameOrd))
            {
                p.Category = new ProfileCategory
                {
                    Id = p.CategoryId,
                    Name = reader.GetString(catNameOrd),
                    Code = reader.IsDBNull(reader.GetOrdinal("CategoryCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("CategoryCode"))
                };
            }

            return p;
        }

        private static ProfileAddress MapAddress(SqlDataReader reader)
        {
            return new ProfileAddress
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                ProfileId = reader.GetInt32(reader.GetOrdinal("ProfileId")),
                AddressLine = reader.GetString(reader.GetOrdinal("AddressLine")),
                Ward = reader.IsDBNull(reader.GetOrdinal("Ward")) ? null : reader.GetString(reader.GetOrdinal("Ward")),
                District = reader.IsDBNull(reader.GetOrdinal("District")) ? null : reader.GetString(reader.GetOrdinal("District")),
                Province = reader.IsDBNull(reader.GetOrdinal("Province")) ? null : reader.GetString(reader.GetOrdinal("Province")),
                Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? "Việt Nam" : reader.GetString(reader.GetOrdinal("Country")),
                Latitude = reader.IsDBNull(reader.GetOrdinal("Latitude")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("Latitude")),
                Longitude = reader.IsDBNull(reader.GetOrdinal("Longitude")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("Longitude")),
                IsPrimary = reader.GetBoolean(reader.GetOrdinal("IsPrimary")),
                Note = reader.IsDBNull(reader.GetOrdinal("Note")) ? null : reader.GetString(reader.GetOrdinal("Note"))
            };
        }

        private static ProfileDocument MapDocument(SqlDataReader reader)
        {
            return new ProfileDocument
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                ProfileId = reader.GetInt32(reader.GetOrdinal("ProfileId")),
                OriginalFileName = reader.GetString(reader.GetOrdinal("OriginalFileName")),
                StoredFileName = reader.GetString(reader.GetOrdinal("StoredFileName")),
                RelativePath = reader.GetString(reader.GetOrdinal("RelativePath")),
                ContentType = reader.IsDBNull(reader.GetOrdinal("ContentType")) ? null : reader.GetString(reader.GetOrdinal("ContentType")),
                SizeBytes = reader.GetInt64(reader.GetOrdinal("SizeBytes")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                UploadedAt = reader.GetDateTime(reader.GetOrdinal("UploadedAt"))
            };
        }
    }
}
