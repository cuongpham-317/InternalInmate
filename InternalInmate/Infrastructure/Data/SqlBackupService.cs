using InternalInmate.Application.Interfaces;
using InternalInmate.Domain.Entities;
using InternalInmate.Domain.Enums;
using InternalInmate.Domain.Exceptions;
using InternalInmate.Domain.Interfaces;
using InternalInmate.Infrastructure.Configurations;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Infrastructure.Data
{
    public class SqlBackupService : IBackupService
    {
        private readonly AppSettings _settings;
        private readonly IFileStorageService _files;
        private readonly IAuditLogRepository _audit;
        private readonly IClock _clock;

        public SqlBackupService(
            AppSettings settings,
            IFileStorageService files,
            IAuditLogRepository audit,
            IClock clock)
        {
            _settings = settings;
            _files = files;
            _audit = audit;
            _clock = clock;
        }

        public async Task<string> BackupAsync(string destinationFolder, CancellationToken cancellationToken = default)
        {
            Directory.CreateDirectory(destinationFolder);
            var stamp = _clock.UtcNow.ToLocalTime().ToString("yyyyMMdd_HHmmss");
            var folder = Path.Combine(destinationFolder, $"Backup_{stamp}");
            Directory.CreateDirectory(folder);

            var bakPath = Path.Combine(folder, "database.bak");
            var docsPath = Path.Combine(folder, "documents");
            var escaped = bakPath.Replace("'", "''");

            try
            {
                using (var conn = new SqlConnection(_settings.ConnectionString))
                {
                    await conn.OpenAsync(cancellationToken);
                    var sql = $"BACKUP DATABASE [{_settings.DatabaseName}] TO DISK = '{escaped}' WITH INIT, COPY_ONLY";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DomainException("Sao lưu database thất bại. Kiểm tra kết nối SQL Server và quyền ghi thư mục.\n" + ex.Message);
            }

            try
            {
                _files.CopyAllTo(docsPath);
            }
            catch (Exception ex)
            {
                throw new DomainException("Database đã sao lưu nhưng sao chép tài liệu thất bại.\n" + ex.Message);
            }

            var manifestPath = Path.Combine(folder, "manifest.txt");
            var manifestContent = string.Format(
                "CreatedUtc={0:O}{1}Database={2}{1}Documents={3}{1}",
                _clock.UtcNow, Environment.NewLine, _settings.DatabaseName, docsPath);
            File.WriteAllText(manifestPath, manifestContent);

            await _audit.AddAsync(new AuditLog
            {
                Action = AuditAction.BackupCreated,
                EntityType = "Database",
                Detail = folder,
                UserName = Environment.UserName,
                CreatedAt = _clock.UtcNow
            }, cancellationToken);
            await _audit.SaveChangesAsync(cancellationToken);

            return folder;
        }

        public async Task RestoreAsync(string backupFolder, CancellationToken cancellationToken = default)
        {
            var bakPath = Path.Combine(backupFolder, "database.bak");
            var docsPath = Path.Combine(backupFolder, "documents");
            if (!File.Exists(bakPath))
            {
                throw new DomainException("Không tìm thấy tệp database.bak trong thư mục sao lưu.");
            }

            var escaped = bakPath.Replace("'", "''");
            var masterConnStr = new SqlConnectionStringBuilder(_settings.ConnectionString)
            {
                InitialCatalog = "master"
            }.ConnectionString;

            try
            {
                using (var conn = new SqlConnection(masterConnStr))
                {
                    await conn.OpenAsync(cancellationToken);
                    var restoreSql = $@"
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{_settings.DatabaseName}')
BEGIN
    ALTER DATABASE [{_settings.DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    RESTORE DATABASE [{_settings.DatabaseName}] FROM DISK = '{escaped}' WITH REPLACE;
    ALTER DATABASE [{_settings.DatabaseName}] SET MULTI_USER;
END
ELSE
BEGIN
    RESTORE DATABASE [{_settings.DatabaseName}] FROM DISK = '{escaped}';
END";
                    using (var cmd = new SqlCommand(restoreSql, conn))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DomainException("Phục hồi database thất bại. Hãy đóng các ứng dụng hoặc kết nối khác đang mở tới database rồi thử lại.\n" + ex.Message);
            }

            try
            {
                if (Directory.Exists(docsPath))
                {
                    _files.ReplaceAllFrom(docsPath);
                }
            }
            catch (Exception ex)
            {
                throw new DomainException("Database đã phục hồi nhưng phục hồi thư mục tài liệu thất bại.\n" + ex.Message);
            }

            await _audit.AddAsync(new AuditLog
            {
                Action = AuditAction.DatabaseRestored,
                EntityType = "Database",
                Detail = backupFolder,
                UserName = Environment.UserName,
                CreatedAt = _clock.UtcNow
            }, cancellationToken);
            await _audit.SaveChangesAsync(cancellationToken);
        }
    }
}
