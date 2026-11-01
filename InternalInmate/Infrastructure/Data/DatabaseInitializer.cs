using InternalInmate.Infrastructure.Configurations;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InternalInmate.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(AppSettings settings)
        {
            // 1. Ensure database exists (thử kết nối master để tạo DB nếu chưa có)
            try
            {
                var masterConnStr = GetMasterConnectionString(settings.ConnectionString);
                using (var conn = new SqlConnection(masterConnStr))
                {
                    await conn.OpenAsync();
                    var checkDbSql = $@"
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{settings.DatabaseName}')
BEGIN
    CREATE DATABASE [{settings.DatabaseName}];
END";
                    using (var cmd = new SqlCommand(checkDbSql, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch
            {
                // Bỏ qua nếu user không có quyền vào master hoặc CSDL đã tồn tại trên hosting/cloud
            }

            // 2. Ensure schema & tables exist
            using (var conn = new SqlConnection(settings.ConnectionString))
            {
                await conn.OpenAsync();

                var createTablesSql = @"
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProfileCategory')
BEGIN
    CREATE TABLE ProfileCategory (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Code NVARCHAR(50) NOT NULL UNIQUE,
        Name NVARCHAR(200) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        SortOrder INT NOT NULL DEFAULT 0
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Profile')
BEGIN
    CREATE TABLE Profile (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Code NVARCHAR(50) NOT NULL,
        FullName NVARCHAR(200) NOT NULL,
        DateOfBirth DATETIME NULL,
        Gender INT NOT NULL DEFAULT 0,
        IdentityNumber NVARCHAR(50) NULL,
        CategoryId INT NOT NULL,
        Status INT NOT NULL DEFAULT 0,
        Notes NVARCHAR(MAX) NULL,
        CreatedAt DATETIME NOT NULL,
        UpdatedAt DATETIME NOT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0,
        DeletedAt DATETIME NULL,
        AvatarPath NVARCHAR(500) NULL,
        CONSTRAINT FK_Profile_Category FOREIGN KEY (CategoryId) REFERENCES ProfileCategory(Id)
    );

    CREATE UNIQUE INDEX IX_Profile_Code ON Profile(Code) WHERE IsDeleted = 0;
    CREATE INDEX IX_Profile_FullName ON Profile(FullName);
    CREATE INDEX IX_Profile_CategoryId ON Profile(CategoryId);
    CREATE INDEX IX_Profile_Status ON Profile(Status);
    CREATE INDEX IX_Profile_CreatedAt ON Profile(CreatedAt);
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProfileAddress')
BEGIN
    CREATE TABLE ProfileAddress (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProfileId INT NOT NULL,
        AddressLine NVARCHAR(500) NOT NULL,
        Ward NVARCHAR(100) NULL,
        District NVARCHAR(100) NULL,
        Province NVARCHAR(100) NULL,
        Country NVARCHAR(100) NOT NULL DEFAULT N'Việt Nam',
        Latitude FLOAT NULL,
        Longitude FLOAT NULL,
        IsPrimary BIT NOT NULL DEFAULT 0,
        Note NVARCHAR(500) NULL,
        CONSTRAINT FK_ProfileAddress_Profile FOREIGN KEY (ProfileId) REFERENCES Profile(Id) ON DELETE CASCADE
    );

    CREATE INDEX IX_ProfileAddress_ProfileId ON ProfileAddress(ProfileId);
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProfileDocument')
BEGIN
    CREATE TABLE ProfileDocument (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProfileId INT NOT NULL,
        OriginalFileName NVARCHAR(260) NOT NULL,
        StoredFileName NVARCHAR(260) NOT NULL,
        RelativePath NVARCHAR(500) NOT NULL,
        ContentType NVARCHAR(100) NULL,
        SizeBytes BIGINT NOT NULL DEFAULT 0,
        Description NVARCHAR(500) NULL,
        UploadedAt DATETIME NOT NULL,
        CONSTRAINT FK_ProfileDocument_Profile FOREIGN KEY (ProfileId) REFERENCES Profile(Id) ON DELETE CASCADE
    );

    CREATE INDEX IX_ProfileDocument_ProfileId ON ProfileDocument(ProfileId);
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AuditLog')
BEGIN
    CREATE TABLE AuditLog (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Action INT NOT NULL,
        EntityType NVARCHAR(100) NOT NULL,
        EntityId INT NULL,
        Detail NVARCHAR(MAX) NULL,
        UserName NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME NOT NULL
    );

    CREATE INDEX IX_AuditLog_CreatedAt ON AuditLog(CreatedAt);
END";

                using (var cmd = new SqlCommand(createTablesSql, conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }

                // Ensure AvatarPath column exists in existing database
                var migrationSql = @"
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Profile') AND name = 'AvatarPath')
BEGIN
    ALTER TABLE Profile ADD AvatarPath NVARCHAR(500) NULL;
END";
                using (var cmd = new SqlCommand(migrationSql, conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }

                // 3. Seed default categories if empty
                var seedSql = @"
IF NOT EXISTS (SELECT 1 FROM ProfileCategory)
BEGIN
    INSERT INTO ProfileCategory (Code, Name, SortOrder, IsActive) VALUES
    ('PERSON', N'Hồ sơ cá nhân', 1, 1),
    ('LOCATION', N'Hồ sơ địa điểm', 2, 1),
    ('ADMIN', N'Hồ sơ hành chính', 3, 1),
    ('OTHER', N'Khác', 4, 1);
END";
                using (var cmd = new SqlCommand(seedSql, conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private static string GetMasterConnectionString(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };
            return builder.ConnectionString;
        }
    }
}
