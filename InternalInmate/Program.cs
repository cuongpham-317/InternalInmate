using InternalInmate.Forms;
using InternalInmate.Infrastructure;
using InternalInmate.Infrastructure.Configurations;
using InternalInmate.Infrastructure.Data;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InternalInmate
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var settings = new AppSettings();

            // 1. Đọc chuỗi kết nối từ App.config (ưu tiên name="DefaultConnection")
            string configConnectionString = null;
            if (ConfigurationManager.ConnectionStrings["DefaultConnection"] != null)
            {
                configConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
            else if (ConfigurationManager.ConnectionStrings.Count > 0)
            {
                // Dự phòng trường hợp user cấu hình tên khác với "DefaultConnection"
                foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
                {
                    if (!string.IsNullOrWhiteSpace(cs.ConnectionString) &&
                        !cs.Name.Equals("LocalSqlServer", StringComparison.OrdinalIgnoreCase))
                    {
                        configConnectionString = cs.ConnectionString;
                        break;
                    }
                }
            }

            // 2. Xác định chuỗi kết nối sử dụng
            if (!string.IsNullOrWhiteSpace(configConnectionString))
            {
                if (CanConnectToSql(configConnectionString))
                {
                    settings.ConnectionString = configConnectionString;
                }
                else
                {
                    // Nếu là chuỗi kết nối cục bộ mặc định (Server=. hoặc MSSQLLocalDB), thử tự động chuyển đổi
                    var defaultInstanceConn = @"Server=.;Database=ProfileManagementDb;Integrated Security=True;TrustServerCertificate=True;";
                    var localDbConn = @"Server=(localdb)\MSSQLLocalDB;Database=ProfileManagementDb;Integrated Security=True;TrustServerCertificate=True;";

                    bool isLocalTemplate = IsLocalTemplateConnection(configConnectionString);

                    if (isLocalTemplate && CanConnectToSql(defaultInstanceConn))
                    {
                        settings.ConnectionString = defaultInstanceConn;
                    }
                    else if (isLocalTemplate && CanConnectToSql(localDbConn))
                    {
                        settings.ConnectionString = localDbConn;
                    }
                    else
                    {
                        // Giữ nguyên chuỗi kết nối người dùng đã cấu hình trong App.config để hiển thị lỗi chi tiết
                        settings.ConnectionString = configConnectionString;
                    }
                }
            }
            else
            {
                // Nếu App.config hoàn toàn không có chuỗi kết nối, tự dò tìm instance khả dụng trên máy
                var defaultInstanceConn = @"Server=.;Database=ProfileManagementDb;Integrated Security=True;TrustServerCertificate=True;";
                var localDbConn = @"Server=(localdb)\MSSQLLocalDB;Database=ProfileManagementDb;Integrated Security=True;TrustServerCertificate=True;";

                if (CanConnectToSql(defaultInstanceConn))
                {
                    settings.ConnectionString = defaultInstanceConn;
                }
                else if (CanConnectToSql(localDbConn))
                {
                    settings.ConnectionString = localDbConn;
                }
                else
                {
                    settings.ConnectionString = defaultInstanceConn;
                }
            }

            // 3. Đọc ghi đè DatabaseName từ <appSettings> nếu có
            var appSettingDb = ConfigurationManager.AppSettings["DatabaseName"];
            if (!string.IsNullOrWhiteSpace(appSettingDb))
            {
                settings.DatabaseName = appSettingDb;
            }

            // Initialize app container & services
            AppContainer.Current.Initialize(settings);

            // Initialize database schema and seed data
            try
            {
                DatabaseInitializer.InitializeAsync(settings).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể kết nối hoặc khởi tạo CSDL SQL Server:\n" + ex.Message +
                    "\n\nBạn có thể kiểm tra và cập nhật chuỗi kết nối trong file App.config (hoặc file InternalInmate.exe.config sau khi public/build).",
                    "Cảnh báo kết nối CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            System.Windows.Forms.Application.Run(new FrmMain());
        }

        private static bool IsLocalTemplateConnection(string connectionString)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                var ds = builder.DataSource?.Trim().ToLowerInvariant();
                return ds == "." || ds == "localhost" || ds == "(local)" || ds == @"(localdb)\mssqllocaldb";
            }
            catch
            {
                return false;
            }
        }

        private static bool CanConnectToSql(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) return false;

            // Thử kết nối trực tiếp đến database đích
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString)
                {
                    ConnectTimeout = 3
                };
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                // Nếu database chưa tồn tại, thử kết nối tới master để kiểm tra SQL Server instance
                try
                {
                    var builder = new SqlConnectionStringBuilder(connectionString)
                    {
                        InitialCatalog = "master",
                        ConnectTimeout = 3
                    };
                    using (var conn = new SqlConnection(builder.ConnectionString))
                    {
                        conn.Open();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
