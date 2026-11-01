using System;
using System.Data.SqlClient;
using System.IO;

namespace InternalInmate.Infrastructure.Configurations
{
    public class AppSettings
    {
        private string _connectionString = @"Server=.;Database=ProfileManagementDb;Integrated Security=True;TrustServerCertificate=True;";
        private string _databaseName = "ProfileManagementDb";

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                UpdateDatabaseNameFromConnectionString(value);
            }
        }

        public string DataDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InternalInmate", "Data");
        public string DocumentsDirectory => Path.Combine(DataDirectory, "Documents");
        public string BackupDirectory => Path.Combine(DataDirectory, "Backups");
        public string LogsDirectory => Path.Combine(DataDirectory, "Logs");

        public string DatabaseName
        {
            get => _databaseName;
            set => _databaseName = value;
        }

        public void EnsureDirectories()
        {
            Directory.CreateDirectory(DataDirectory);
            Directory.CreateDirectory(DocumentsDirectory);
            Directory.CreateDirectory(BackupDirectory);
            Directory.CreateDirectory(LogsDirectory);
        }

        private void UpdateDatabaseNameFromConnectionString(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr)) return;
            try
            {
                var builder = new SqlConnectionStringBuilder(connStr);
                if (!string.IsNullOrWhiteSpace(builder.InitialCatalog))
                {
                    _databaseName = builder.InitialCatalog;
                }
            }
            catch
            {
                // Bỏ qua lỗi cú pháp nếu chuỗi kết nối chưa hoàn chỉnh
            }
        }
    }
}
