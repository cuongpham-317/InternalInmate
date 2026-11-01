using InternalInmate.Application.Interfaces;
using InternalInmate.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternalInmate.Forms
{
    public partial class FrmBackupRestore : Form
    {
        private readonly IBackupService _backupService;
        private string _backupRoot;

        public FrmBackupRestore()
        {
            InitializeComponent();
            _backupService = AppContainer.Current.BackupService;
            _backupRoot = AppContainer.Current.Settings.BackupDirectory;
            txtBackupFolder.Text = _backupRoot;

            // Kích hoạt tối ưu Double Buffering
            this.EnableDoubleBuffering();
            pnlHeader.EnableDoubleBuffering();
            pnlTopSettings.EnableDoubleBuffering();
            grpList.EnableDoubleBuffering();
            pnlBottom.EnableDoubleBuffering();
            lstBackups.EnableDoubleBuffering();
        }

        private void FrmBackupRestore_Load(object sender, EventArgs e)
        {
            RefreshBackupList();
        }

        private void RefreshBackupList()
        {
            lstBackups.BeginUpdate();
            try
            {
                lstBackups.Items.Clear();
                btnRestore.Enabled = false;

                if (!Directory.Exists(_backupRoot))
                {
                    Directory.CreateDirectory(_backupRoot);
                }

                var dirs = Directory.GetDirectories(_backupRoot, "Backup_*");
                Array.Sort(dirs);
                Array.Reverse(dirs);

                foreach (var dir in dirs)
                {
                    var folderName = Path.GetFileName(dir);
                    var bakFile = Path.Combine(dir, "database.bak");
                    var docsFolder = Path.Combine(dir, "documents");
                    var manifest = Path.Combine(dir, "manifest.txt");

                    var valid = File.Exists(bakFile);
                    var info = $"{folderName}  {(valid ? "[Hợp lệ]" : "[Thiếu database.bak]")}";
                    if (Directory.Exists(docsFolder))
                    {
                        var fileCount = Directory.GetFiles(docsFolder, "*", SearchOption.AllDirectories).Length;
                        info += $" - {fileCount} tài liệu";
                    }

                    lstBackups.Items.Add(new BackupItem(folderName, dir, valid, info));
                }

                lblStatus.Text = $"Tìm thấy {lstBackups.Items.Count} bản sao lưu trong thư mục.";
            }
            finally
            {
                lstBackups.EndUpdate();
            }
        }

        private void LstBackups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBackups.SelectedItem is BackupItem item)
            {
                btnRestore.Enabled = item.IsValid;
                lblSelectedBackup.Text = "Đang chọn: " + item.FullPath;
            }
            else
            {
                btnRestore.Enabled = false;
                lblSelectedBackup.Text = "Chưa chọn bản sao lưu.";
            }
        }

        private void BtnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Chọn thư mục lưu trữ các bản sao lưu";
                fbd.SelectedPath = _backupRoot;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    _backupRoot = fbd.SelectedPath;
                    txtBackupFolder.Text = _backupRoot;
                    RefreshBackupList();
                }
            }
        }

        private async void BtnBackupNow_Click(object sender, EventArgs e)
        {
            btnBackupNow.Enabled = false;
            lblStatus.Text = "Đang tiến hành sao lưu CSDL và tài liệu đính kèm...";

            try
            {
                var targetDir = txtBackupFolder.Text.Trim();
                if (string.IsNullOrWhiteSpace(targetDir)) targetDir = _backupRoot;

                var resultFolder = await _backupService.BackupAsync(targetDir);
                lblStatus.Text = "Sao lưu hoàn tất thành công!";
                MessageBox.Show(
                    "Đã sao lưu thành công CSDL và thư mục tài liệu!\nThư mục sao lưu:\n" + resultFolder,
                    "Sao lưu thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshBackupList();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi sao lưu: " + ex.Message;
                MessageBox.Show("Sao lưu thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBackupNow.Enabled = true;
            }
        }

        private async void BtnRestore_Click(object sender, EventArgs e)
        {
            if (!(lstBackups.SelectedItem is BackupItem item) || !item.IsValid)
            {
                MessageBox.Show("Vui lòng chọn một bản sao lưu hợp lệ trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"CẢNH BÁO: Phục hồi sẽ GHI ĐÈ toàn bộ dữ liệu CSDL hiện tại và thư mục tài liệu bằng bản sao lưu:\n'{item.FolderName}'\n\nBạn có chắc chắn muốn thực hiện phục hồi?",
                "Xác nhận phục hồi CSDL",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            btnRestore.Enabled = false;
            btnBackupNow.Enabled = false;
            lblStatus.Text = "Đang tiến hành phục hồi CSDL và tài liệu...";

            try
            {
                await _backupService.RestoreAsync(item.FullPath);
                lblStatus.Text = "Phục hồi thành công!";
                MessageBox.Show(
                    "Đã phục hồi CSDL và tài liệu thành công từ bản sao lưu:\n" + item.FolderName,
                    "Phục hồi thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi phục hồi: " + ex.Message;
                MessageBox.Show("Phục hồi thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBackupNow.Enabled = true;
                btnRestore.Enabled = true;
            }
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_backupRoot))
            {
                Process.Start("explorer.exe", _backupRoot);
            }
        }

        private sealed class BackupItem
        {
            public string FolderName { get; }
            public string FullPath { get; }
            public bool IsValid { get; }
            private readonly string _display;

            public BackupItem(string name, string fullPath, bool isValid, string display)
            {
                FolderName = name;
                FullPath = fullPath;
                IsValid = isValid;
                _display = display;
            }

            public override string ToString() => _display;
        }
    }
}
