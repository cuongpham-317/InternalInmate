using InternalInmate.Application.DTOs;
using InternalInmate.Application.Services;
using InternalInmate.Domain.Enums;
using InternalInmate.Infrastructure;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternalInmate.Forms
{
    public partial class FrmMain : Form
    {
        private FrmProfileList _profileListForm;
        private FrmBackupRestore _backupRestoreForm;
        private readonly ProfileService _profileService;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (Double buffering mượt mà khi chạy app)
                }
                return cp;
            }
        }

        public FrmMain()
        {
            InitializeComponent();
            _profileService = AppContainer.Current.ProfileService;

            // Kích hoạt Double Buffering cho các container chính để loại bỏ nhấp nháy
            this.EnableDoubleBuffering();
            pnlSidebar.EnableDoubleBuffering();
            pnlTopBar.EnableDoubleBuffering();
            pnlContent.EnableDoubleBuffering();
            pnlDashboardContent.EnableDoubleBuffering();
            tblKpi.EnableDoubleBuffering();
            cardTotal.EnableDoubleBuffering();
            cardActive.EnableDoubleBuffering();
            cardDraft.EnableDoubleBuffering();
            cardClosed.EnableDoubleBuffering();
        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            lblDbStatus.Text = "CSDL: " + AppContainer.Current.Settings.DatabaseName;
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            timerClock.Start();

            await ShowDashboardAsync();
        }

        private void TimerClock_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void SetActiveButton(Button btn)
        {
            btnNavDashboard.BackColor = Color.FromArgb(30, 41, 59);
            btnNavProfiles.BackColor = Color.FromArgb(30, 41, 59);
            btnNavNewProfile.BackColor = Color.FromArgb(30, 41, 59);
            btnNavBackup.BackColor = Color.FromArgb(30, 41, 59);
            btnNavAbout.BackColor = Color.FromArgb(30, 41, 59);

            if (btn != null)
            {
                btn.BackColor = Color.FromArgb(51, 65, 85);
            }
        }

        private async Task ShowDashboardAsync()
        {
            SetActiveButton(btnNavDashboard);
            lblPageTitle.Text = "TỔNG QUAN HỆ THỐNG";

            pnlContent.SuspendLayout();
            try
            {
                if (_profileListForm != null && _profileListForm.Visible)
                {
                    _profileListForm.Visible = false;
                }
                if (_backupRestoreForm != null && _backupRestoreForm.Visible)
                {
                    _backupRestoreForm.Visible = false;
                }

                pnlDashboardContent.Visible = true;
                pnlDashboardContent.BringToFront();
            }
            finally
            {
                pnlContent.ResumeLayout(true);
            }

            await RefreshDashboardStatsAsync();
        }

        private async Task RefreshDashboardStatsAsync()
        {
            try
            {
                var all = await _profileService.SearchAsync(new ProfileSearchRequest());
                var total = all.Count;
                var active = all.Count(p => p.Status == ProfileStatus.Active);
                var draft = all.Count(p => p.Status == ProfileStatus.Draft);
                var closed = all.Count(p => p.Status == ProfileStatus.Closed || p.Status == ProfileStatus.Suspended);

                lblStatTotal.Text = total.ToString();
                lblStatActive.Text = active.ToString();
                lblStatDraft.Text = draft.ToString();
                lblStatClosed.Text = closed.ToString();
            }
            catch
            {
                // Bỏ qua lỗi thống kê nếu DB chưa sẵn sàng
            }
        }

        private async void BtnNavDashboard_Click(object sender, EventArgs e)
        {
            await ShowDashboardAsync();
        }

        private void BtnNavProfiles_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnNavProfiles);
            lblPageTitle.Text = "DANH SÁCH & QUẢN LÝ HỒ SƠ";

            pnlContent.SuspendLayout();
            try
            {
                pnlDashboardContent.Visible = false;
                if (_backupRestoreForm != null) _backupRestoreForm.Visible = false;

                if (_profileListForm == null || _profileListForm.IsDisposed)
                {
                    _profileListForm = new FrmProfileList
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill
                    };
                    pnlContent.Controls.Add(_profileListForm);
                }

                _profileListForm.Visible = true;
                _profileListForm.BringToFront();
                _profileListForm.Show();
            }
            finally
            {
                pnlContent.ResumeLayout(true);
            }
        }

        private async void BtnNavNewProfile_Click(object sender, EventArgs e)
        {
            using (var form = new FrmProfileDetail(null))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (_profileListForm != null && !_profileListForm.IsDisposed)
                    {
                        await _profileListForm.SearchAsync();
                    }
                    await RefreshDashboardStatsAsync();
                }
            }
        }

        private void BtnNavBackup_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnNavBackup);
            lblPageTitle.Text = "SAO LƯU & PHỤC HỒI HỆ THỐNG";

            pnlContent.SuspendLayout();
            try
            {
                pnlDashboardContent.Visible = false;
                if (_profileListForm != null) _profileListForm.Visible = false;

                if (_backupRestoreForm == null || _backupRestoreForm.IsDisposed)
                {
                    _backupRestoreForm = new FrmBackupRestore
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill
                    };
                    pnlContent.Controls.Add(_backupRestoreForm);
                }

                _backupRestoreForm.Visible = true;
                _backupRestoreForm.BringToFront();
                _backupRestoreForm.Show();
            }
            finally
            {
                pnlContent.ResumeLayout(true);
            }
        }

        private void BtnNavAbout_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnNavAbout);
            MessageBox.Show(
                "PHẦN MỀM QUẢN LÝ HỒ SƠ (WinForms C#)\n\n" +
                "• Phiên bản: 1.0.0 (MVP)\n" +
                "• Kiến trúc: Domain - Application - Infrastructure - WinForms\n" +
                "• CSDL: SQL Server Local / LocalDB\n" +
                "• Contact: cuong.2p\n" +
                "• Tích hợp bản đồ: Leaflet + OpenStreetMap & Nominatim Geocoding\n" +
                "• Quản lý tài liệu: Phân loại, mở xem, xuất bản sao, xóa tệp\n" +
                "• Sao lưu/Phục hồi: Đồng bộ database .bak và thư mục tài liệu",
                "Giới thiệu phần mềm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnQuickAdd_Click(object sender, EventArgs e)
        {
            BtnNavNewProfile_Click(sender, e);
        }

        private void BtnQuickList_Click(object sender, EventArgs e)
        {
            BtnNavProfiles_Click(sender, e);
        }

        private void BtnQuickBackup_Click(object sender, EventArgs e)
        {
            BtnNavBackup_Click(sender, e);
        }
    }
}
