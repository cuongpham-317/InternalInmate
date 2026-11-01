using InternalInmate.Application.DTOs;
using InternalInmate.Application.Services;
using InternalInmate.Domain.Enums;
using InternalInmate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternalInmate.Forms
{
    public partial class FrmProfileList : Form
    {
        private readonly ProfileService _profileService;
        private IReadOnlyList<ProfileListItemDto> _profiles = new List<ProfileListItemDto>();
        private readonly Dictionary<string, Image> _avatarThumbnailCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        private static readonly Font _boldStatusFont = new Font("Segoe UI", 9F, FontStyle.Bold);

        public FrmProfileList()
        {
            InitializeComponent();
            _profileService = AppContainer.Current.ProfileService;

            // Kích hoạt tối ưu Double Buffering và Grid
            this.EnableDoubleBuffering();
            gridProfiles.OptimizeGrid();
            pnlFilter.EnableDoubleBuffering();
            pnlToolbar.EnableDoubleBuffering();
        }

        private async void FrmProfileList_Load(object sender, EventArgs e)
        {
            await LoadFilterCategoriesAsync();
            PopulateStatusFilter();
            await SearchAsync();
        }

        private async Task LoadFilterCategoriesAsync()
        {
            try
            {
                var categories = await _profileService.GetCategoriesAsync();
                var list = new List<CategoryFilterItem>
                {
                    new CategoryFilterItem { Id = null, Name = "-- Tất cả loại hồ sơ --" }
                };
                list.AddRange(categories.Select(c => new CategoryFilterItem { Id = c.Id, Name = c.Name }));

                cboCategoryFilter.DisplayMember = "Name";
                cboCategoryFilter.ValueMember = "Id";
                cboCategoryFilter.DataSource = list;
                cboCategoryFilter.SelectedIndex = 0;
            }
            catch
            {
                // Ignore initial filter load error
            }
        }

        private void PopulateStatusFilter()
        {
            cboStatusFilter.Items.Add(new StatusFilterItem { Status = null, Name = "-- Tất cả trạng thái --" });
            cboStatusFilter.Items.Add(new StatusFilterItem { Status = ProfileStatus.Draft, Name = "Bản nháp" });
            cboStatusFilter.Items.Add(new StatusFilterItem { Status = ProfileStatus.Active, Name = "Hoạt động" });
            cboStatusFilter.Items.Add(new StatusFilterItem { Status = ProfileStatus.Suspended, Name = "Tạm dừng" });
            cboStatusFilter.Items.Add(new StatusFilterItem { Status = ProfileStatus.Closed, Name = "Đã đóng" });
            cboStatusFilter.SelectedIndex = 0;
        }

        public async Task SearchAsync()
        {
            btnSearch.Enabled = false;
            lblStatusInfo.Text = "Đang tìm kiếm hồ sơ...";

            try
            {
                int? categoryId = null;
                if (cboCategoryFilter.SelectedItem is CategoryFilterItem catItem && catItem.Id.HasValue)
                {
                    categoryId = catItem.Id.Value;
                }

                ProfileStatus? status = null;
                if (cboStatusFilter.SelectedItem is StatusFilterItem stItem && stItem.Status.HasValue)
                {
                    status = stItem.Status.Value;
                }

                DateTime? from = chkFilterDate.Checked ? dtpFrom.Value.Date : (DateTime?)null;
                DateTime? to = chkFilterDate.Checked ? dtpTo.Value.Date : (DateTime?)null;

                var req = new ProfileSearchRequest
                {
                    Keyword = txtSearch.Text.Trim(),
                    CategoryId = categoryId,
                    Status = status,
                    CreatedFrom = from,
                    CreatedTo = to
                };

                _profiles = await _profileService.SearchAsync(req);

                gridProfiles.SuspendLayout();
                try
                {
                    gridProfiles.Rows.Clear();

                    if (_profiles != null && _profiles.Count > 0)
                    {
                        foreach (var p in _profiles)
                        {
                            var avatarImg = GetAvatarThumbnail(p.AvatarPath);

                            var rowIndex = gridProfiles.Rows.Add(
                                p.Id,
                                avatarImg,
                                p.Code,
                                p.FullName,
                                p.CategoryName,
                                p.StatusText,
                                p.PrimaryAddress,
                                p.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")
                            );

                            var row = gridProfiles.Rows[rowIndex];
                            switch (p.Status)
                            {
                                case ProfileStatus.Active:
                                    row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(22, 163, 74);
                                    row.Cells["colStatus"].Style.Font = _boldStatusFont;
                                    break;
                                case ProfileStatus.Draft:
                                    row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(217, 119, 6);
                                    break;
                                case ProfileStatus.Suspended:
                                    row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(220, 38, 38);
                                    break;
                                case ProfileStatus.Closed:
                                    row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(100, 116, 139);
                                    break;
                            }
                        }
                    }
                }
                finally
                {
                    gridProfiles.ResumeLayout();
                }

                lblStatusInfo.Text = $"Tìm thấy {_profiles.Count} hồ sơ.";
                UpdateSelectionState();
            }
            catch (Exception ex)
            {
                lblStatusInfo.Text = "Lỗi tìm kiếm: " + ex.Message;
                MessageBox.Show("Lỗi tìm kiếm hồ sơ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }

        private void GridProfiles_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectionState();
        }

        private void UpdateSelectionState()
        {
            var hasSelection = gridProfiles.SelectedRows.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDocuments.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;

            if (hasSelection)
            {
                var p = GetSelectedProfile();
                var code = Convert.ToString(gridProfiles.SelectedRows[0].Cells["colCode"].Value);
                var name = Convert.ToString(gridProfiles.SelectedRows[0].Cells["colFullName"].Value);
                lblSelectedInfo.Text = $"Đang chọn: [{code}] {name}";

                if (p != null && !string.IsNullOrWhiteSpace(p.AvatarPath))
                {
                    var fullPath = AppContainer.Current.FileStorageService.GetAbsolutePath(p.AvatarPath);
                    picSelectedAvatar.Image?.Dispose();
                    picSelectedAvatar.Image = LoadImageCopy(fullPath);
                }
                else
                {
                    picSelectedAvatar.Image?.Dispose();
                    picSelectedAvatar.Image = null;
                }
                picSelectedAvatar.Visible = true;
                picSelectedAvatar.Invalidate();
            }
            else
            {
                lblSelectedInfo.Text = "Chưa chọn hồ sơ nào.";
                picSelectedAvatar.Image?.Dispose();
                picSelectedAvatar.Image = null;
                picSelectedAvatar.Visible = false;
            }
        }

        private void PicSelectedAvatar_Paint(object sender, PaintEventArgs e)
        {
            if (picSelectedAvatar.Image == null)
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var w = picSelectedAvatar.ClientSize.Width;
                var h = picSelectedAvatar.ClientSize.Height;

                using (var brush = new SolidBrush(Color.FromArgb(203, 213, 225)))
                {
                    g.FillEllipse(brush, (w / 2) - 6, 6, 12, 12);
                    g.FillPie(brush, (w / 2) - 11, 17, 22, 22, 180, 180);
                }
            }
        }

        private static Image _defaultAvatarThumbnail;
        private static Image GetDefaultAvatarThumbnail()
        {
            if (_defaultAvatarThumbnail == null)
            {
                var bmp = new Bitmap(42, 42);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.FromArgb(241, 245, 249));
                    using (var brush = new SolidBrush(Color.FromArgb(203, 213, 225)))
                    {
                        g.FillEllipse(brush, 14, 8, 14, 14);
                        g.FillPie(brush, 8, 22, 26, 26, 180, 180);
                    }
                }
                _defaultAvatarThumbnail = bmp;
            }
            return _defaultAvatarThumbnail;
        }

        private Image GetAvatarThumbnail(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return GetDefaultAvatarThumbnail();
            }

            if (_avatarThumbnailCache.TryGetValue(relativePath, out var cachedImg))
            {
                return cachedImg;
            }

            try
            {
                var fullPath = AppContainer.Current.FileStorageService.GetAbsolutePath(relativePath);
                if (File.Exists(fullPath))
                {
                    using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var original = Image.FromStream(fs))
                    {
                        var thumb = new Bitmap(original, 40, 40);
                        _avatarThumbnailCache[relativePath] = thumb;
                        return thumb;
                    }
                }
            }
            catch
            {
                // Fallback to default
            }

            return GetDefaultAvatarThumbnail();
        }

        private static Image LoadImageCopy(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                return null;
            }

            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var temp = Image.FromStream(fs))
                {
                    return new Bitmap(temp);
                }
            }
            catch
            {
                return null;
            }
        }

        private ProfileListItemDto GetSelectedProfile()
        {
            if (gridProfiles.SelectedRows.Count == 0) return null;
            var id = (int)gridProfiles.SelectedRows[0].Cells["colId"].Value;
            return _profiles.FirstOrDefault(p => p.Id == id);
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            await SearchAsync();
        }

        private async void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                await SearchAsync();
            }
        }

        private async void BtnResetFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            cboCategoryFilter.SelectedIndex = 0;
            cboStatusFilter.SelectedIndex = 0;
            chkFilterDate.Checked = false;
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
            await SearchAsync();
        }

        private void ChkFilterDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = chkFilterDate.Checked;
            dtpTo.Enabled = chkFilterDate.Checked;
        }

        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new FrmProfileDetail(null))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await SearchAsync();
                }
            }
        }

        private async void BtnEdit_Click(object sender, EventArgs e)
        {
            var p = GetSelectedProfile();
            if (p == null) return;

            using (var form = new FrmProfileDetail(p.Id))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    await SearchAsync();
                }
            }
        }

        private void GridProfiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEdit_Click(sender, e);
            }
        }

        private void BtnDocuments_Click(object sender, EventArgs e)
        {
            var p = GetSelectedProfile();
            if (p == null) return;

            using (var form = new FrmDocumentManager(p.Id, p.Code, p.FullName))
            {
                form.ShowDialog(this);
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            var p = GetSelectedProfile();
            if (p == null) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa mềm (ngừng sử dụng) hồ sơ '{p.Code} - {p.FullName}'?\nHồ sơ sẽ được chuyển sang trạng thái Đã đóng.",
                "Xác nhận xóa mềm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await _profileService.SoftDeleteAsync(p.Id);
                    MessageBox.Show("Đã xóa mềm hồ sơ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await SearchAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hồ sơ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            await SearchAsync();
        }

        private sealed class CategoryFilterItem
        {
            public int? Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        private sealed class StatusFilterItem
        {
            public ProfileStatus? Status { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }
    }
}
