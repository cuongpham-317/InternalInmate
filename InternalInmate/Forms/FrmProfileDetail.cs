using InternalInmate.Application.DTOs;
using InternalInmate.Application.Services;
using InternalInmate.Domain.Enums;
using InternalInmate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternalInmate.Forms
{
    public partial class FrmProfileDetail : Form
    {
        private readonly int? _profileId;
        private readonly ProfileService _profileService;
        private ProfileDetailDto _currentProfile;
        private List<ProfileAddressDto> _addresses = new List<ProfileAddressDto>();
        private IReadOnlyList<CategoryDto> _categories;
        private string _pendingAvatarSourcePath;
        private bool _avatarRemoved;

        public int SavedProfileId { get; private set; }

        // Constructor không tham số bắt buộc cho Visual Studio Form Designer
        public FrmProfileDetail() : this(null)
        {
        }

        public FrmProfileDetail(int? profileId = null)
        {
            InitializeComponent();
            _profileId = profileId;
            _profileService = AppContainer.Current.ProfileService;

            // Kích hoạt tối ưu Double Buffering cho DataGridView và container
            gridAddresses.OptimizeGrid();
            this.EnableDoubleBuffering();
            pnlTop.EnableDoubleBuffering();
            pnlBottom.EnableDoubleBuffering();
            tabControl.EnableDoubleBuffering();

            Text = _profileId.HasValue ? "Chỉnh sửa hồ sơ" : "Thêm mới hồ sơ";
            lblTitle.Text = _profileId.HasValue ? "CẬP NHẬT THÔNG TIN HỒ SƠ" : "TẠO HỒ SƠ MỚI";
            btnManageDocs.Visible = _profileId.HasValue;

            gridAddresses.DataError += (s, e) => { e.ThrowException = false; };
            gridAddresses.CurrentCellDirtyStateChanged += GridAddresses_CurrentCellDirtyStateChanged;
            gridAddresses.CellValueChanged += GridAddresses_CellValueChanged;
        }

        private async void FrmProfileDetail_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
            PopulateEnums();

            if (_profileId.HasValue && _profileId.Value > 0)
            {
                await LoadProfileAsync(_profileId.Value);
            }
            else
            {
                // New profile default code
                txtCode.Text = "HS-" + DateTime.Now.ToString("yyMMdd-HHmm");
                cboStatus.SelectedItem = ProfileStatus.Draft;
                cboGender.SelectedItem = Gender.Unknown;
                chkHasDob.Checked = false;
                dtpDob.Enabled = false;
                btnRemoveAvatar.Enabled = false;

                // Add a default blank primary address row
                _addresses.Add(new ProfileAddressDto
                {
                    AddressLine = "",
                    Country = "Việt Nam",
                    IsPrimary = true
                });
                RefreshAddressGrid();
            }
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                _categories = await _profileService.GetCategoriesAsync();
                cboCategory.DisplayMember = "Name";
                cboCategory.ValueMember = "Id";
                cboCategory.DataSource = _categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateEnums()
        {
            cboGender.DataSource = new[] { Gender.Unknown, Gender.Male, Gender.Female };
            cboGender.Format += (s, e) =>
            {
                if (e.ListItem is Gender g)
                {
                    switch (g)
                    {
                        case Gender.Male: e.Value = "Nam"; break;
                        case Gender.Female: e.Value = "Nữ"; break;
                        default: e.Value = "Chưa xác định"; break;
                    }
                }
            };

            cboStatus.DataSource = new[] { ProfileStatus.Draft, ProfileStatus.Active, ProfileStatus.Suspended, ProfileStatus.Closed };
            cboStatus.Format += (s, e) =>
            {
                if (e.ListItem is ProfileStatus st)
                {
                    switch (st)
                    {
                        case ProfileStatus.Draft: e.Value = "Bản nháp"; break;
                        case ProfileStatus.Active: e.Value = "Hoạt động"; break;
                        case ProfileStatus.Suspended: e.Value = "Tạm dừng"; break;
                        case ProfileStatus.Closed: e.Value = "Đã đóng"; break;
                    }
                }
            };
        }

        private async Task LoadProfileAsync(int id)
        {
            try
            {
                _currentProfile = await _profileService.GetDetailAsync(id);
                txtCode.Text = _currentProfile.Code;
                txtFullName.Text = _currentProfile.FullName;
                txtIdentityNumber.Text = _currentProfile.IdentityNumber ?? string.Empty;
                txtNotes.Text = _currentProfile.Notes ?? string.Empty;

                if (_currentProfile.DateOfBirth.HasValue)
                {
                    chkHasDob.Checked = true;
                    dtpDob.Enabled = true;
                    dtpDob.Value = _currentProfile.DateOfBirth.Value;
                }
                else
                {
                    chkHasDob.Checked = false;
                    dtpDob.Enabled = false;
                }

                cboGender.SelectedItem = _currentProfile.Gender;
                cboStatus.SelectedItem = _currentProfile.Status;
                if (_currentProfile.CategoryId > 0)
                {
                    cboCategory.SelectedValue = _currentProfile.CategoryId;
                }

                _addresses = _currentProfile.Addresses != null ? _currentProfile.Addresses.ToList() : new List<ProfileAddressDto>();
                RefreshAddressGrid();

                var docCount = _currentProfile.Documents != null ? _currentProfile.Documents.Count : 0;
                btnManageDocs.Text = $"Tài liệu đính kèm ({docCount})";

                // Load avatar
                if (!string.IsNullOrWhiteSpace(_currentProfile.AvatarPath))
                {
                    var fullPath = AppContainer.Current.FileStorageService.GetAbsolutePath(_currentProfile.AvatarPath);
                    var img = LoadImageCopy(fullPath);
                    if (img != null)
                    {
                        picAvatar.Image?.Dispose();
                        picAvatar.Image = img;
                        btnRemoveAvatar.Enabled = true;
                    }
                    else
                    {
                        picAvatar.Image = null;
                        btnRemoveAvatar.Enabled = false;
                    }
                }
                else
                {
                    picAvatar.Image = null;
                    btnRemoveAvatar.Enabled = false;
                }
                picAvatar.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin hồ sơ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void ChkHasDob_CheckedChanged(object sender, EventArgs e)
        {
            dtpDob.Enabled = chkHasDob.Checked;
        }

        private void RefreshAddressGrid()
        {
            gridAddresses.SuspendLayout();
            try
            {
                gridAddresses.Rows.Clear();
                if (_addresses != null && _addresses.Count > 0)
                {
                    for (int i = 0; i < _addresses.Count; i++)
                    {
                        var addr = _addresses[i];
                        var coordStr = (addr.Latitude.HasValue && addr.Longitude.HasValue)
                            ? string.Format(CultureInfo.InvariantCulture, "{0:F4}, {1:F4}", addr.Latitude.Value, addr.Longitude.Value)
                            : "Chưa có";

                        gridAddresses.Rows.Add(
                            addr.Id,
                            addr.IsPrimary,
                            addr.AddressLine,
                            addr.Ward ?? "",
                            addr.District ?? "",
                            addr.Province ?? "",
                            coordStr,
                            addr.Note ?? ""
                        );
                    }
                }
            }
            finally
            {
                gridAddresses.ResumeLayout();
            }
        }

        private void BtnAddAddress_Click(object sender, EventArgs e)
        {
            gridAddresses.EndEdit();
            var isFirst = _addresses.Count == 0;
            _addresses.Add(new ProfileAddressDto
            {
                AddressLine = "Địa chỉ mới",
                Country = "Việt Nam",
                IsPrimary = isFirst
            });
            RefreshAddressGrid();
            gridAddresses.ClearSelection();
            if (gridAddresses.Rows.Count > 0)
            {
                gridAddresses.Rows[gridAddresses.Rows.Count - 1].Selected = true;
            }
        }

        private void BtnDeleteAddress_Click(object sender, EventArgs e)
        {
            if (gridAddresses.SelectedRows.Count == 0) return;
            var index = gridAddresses.SelectedRows[0].Index;
            if (index >= 0 && index < _addresses.Count)
            {
                var wasPrimary = _addresses[index].IsPrimary;
                _addresses.RemoveAt(index);
                if (wasPrimary && _addresses.Count > 0)
                {
                    _addresses[0].IsPrimary = true;
                }
                RefreshAddressGrid();
                if (_addresses.Count > 0)
                {
                    var newIndex = Math.Min(index, _addresses.Count - 1);
                    gridAddresses.Rows[newIndex].Selected = true;
                }
            }
        }

        private void BtnSetPrimaryAddress_Click(object sender, EventArgs e)
        {
            if (gridAddresses.SelectedRows.Count == 0) return;
            var index = gridAddresses.SelectedRows[0].Index;
            if (index >= 0 && index < _addresses.Count && index < gridAddresses.Rows.Count)
            {
                for (int i = 0; i < _addresses.Count; i++)
                {
                    var isPrimary = (i == index);
                    _addresses[i].IsPrimary = isPrimary;
                    if (i < gridAddresses.Rows.Count)
                    {
                        gridAddresses.Rows[i].Cells["colIsPrimary"].Value = isPrimary;
                    }
                }
            }
        }

        private void BtnPickMap_Click(object sender, EventArgs e)
        {
            gridAddresses.EndEdit();

            if (gridAddresses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng địa chỉ trong bảng để gắn vị trí bản đồ.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var index = gridAddresses.SelectedRows[0].Index;
            if (index < 0 || index >= _addresses.Count) return;
            var addr = _addresses[index];

            var fullAddress = string.Join(", ", new[] { addr.AddressLine, addr.Ward, addr.District, addr.Province }
                .Where(s => !string.IsNullOrWhiteSpace(s)));

            try
            {
                using (var mapForm = new FrmMapPicker(addr.Latitude, addr.Longitude, fullAddress))
                {
                    if (mapForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (mapForm.SelectedLatitude.HasValue && mapForm.SelectedLongitude.HasValue)
                        {
                            addr.Latitude = mapForm.SelectedLatitude.Value;
                            addr.Longitude = mapForm.SelectedLongitude.Value;
                            if (!string.IsNullOrWhiteSpace(mapForm.SelectedAddressName) && string.IsNullOrWhiteSpace(addr.AddressLine))
                            {
                                addr.AddressLine = mapForm.SelectedAddressName;
                            }
                            RefreshAddressGrid();
                            if (index >= 0 && index < gridAddresses.Rows.Count)
                            {
                                gridAddresses.Rows[index].Selected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị bản đồ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GridAddresses_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridAddresses.IsCurrentCellDirty && gridAddresses.CurrentCell is DataGridViewCheckBoxCell)
            {
                gridAddresses.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void GridAddresses_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _addresses.Count || e.RowIndex >= gridAddresses.Rows.Count) return;

            if (e.ColumnIndex == colIsPrimary.Index)
            {
                var isPrimary = Convert.ToBoolean(gridAddresses.Rows[e.RowIndex].Cells["colIsPrimary"].Value);
                _addresses[e.RowIndex].IsPrimary = isPrimary;
                if (isPrimary)
                {
                    for (int i = 0; i < _addresses.Count; i++)
                    {
                        if (i != e.RowIndex)
                        {
                            _addresses[i].IsPrimary = false;
                            if (i < gridAddresses.Rows.Count && Convert.ToBoolean(gridAddresses.Rows[i].Cells["colIsPrimary"].Value))
                            {
                                gridAddresses.Rows[i].Cells["colIsPrimary"].Value = false;
                            }
                        }
                    }
                }
            }
        }

        private void GridAddresses_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _addresses.Count || e.RowIndex >= gridAddresses.Rows.Count) return;
            var addr = _addresses[e.RowIndex];
            var row = gridAddresses.Rows[e.RowIndex];

            addr.IsPrimary = Convert.ToBoolean(row.Cells["colIsPrimary"].Value);
            addr.AddressLine = Convert.ToString(row.Cells["colAddressLine"].Value) ?? "";
            addr.Ward = Convert.ToString(row.Cells["colWard"].Value);
            addr.District = Convert.ToString(row.Cells["colDistrict"].Value);
            addr.Province = Convert.ToString(row.Cells["colProvince"].Value);
            addr.Note = Convert.ToString(row.Cells["colNote"].Value);

            // If user checked this row as primary, uncheck others in model and UI without clearing rows
            if (addr.IsPrimary)
            {
                for (int i = 0; i < _addresses.Count; i++)
                {
                    if (i != e.RowIndex)
                    {
                        _addresses[i].IsPrimary = false;
                        if (i < gridAddresses.Rows.Count && Convert.ToBoolean(gridAddresses.Rows[i].Cells["colIsPrimary"].Value))
                        {
                            gridAddresses.Rows[i].Cells["colIsPrimary"].Value = false;
                        }
                    }
                }
            }
        }

        private void BtnManageDocs_Click(object sender, EventArgs e)
        {
            if (!_profileId.HasValue) return;

            using (var docForm = new FrmDocumentManager(_profileId.Value, txtCode.Text, txtFullName.Text))
            {
                docForm.ShowDialog(this);
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            // Sync any current edit in address grid
            gridAddresses.EndEdit();

            // Process avatar save if changed
            string avatarPathToSave = _currentProfile?.AvatarPath;
            if (!string.IsNullOrEmpty(_pendingAvatarSourcePath))
            {
                try
                {
                    avatarPathToSave = AppContainer.Current.FileStorageService.SaveAvatar(_pendingAvatarSourcePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu ảnh hồ sơ:\n" + ex.Message, "Cảnh báo ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (_avatarRemoved)
            {
                avatarPathToSave = null;
            }

            var req = new SaveProfileRequest
            {
                Id = _profileId,
                Code = txtCode.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                IdentityNumber = txtIdentityNumber.Text.Trim(),
                DateOfBirth = chkHasDob.Checked ? dtpDob.Value.Date : (DateTime?)null,
                Gender = cboGender.SelectedItem is Gender g ? g : Gender.Unknown,
                CategoryId = cboCategory.SelectedValue is int catId ? catId : 0,
                Status = cboStatus.SelectedItem is ProfileStatus st ? st : ProfileStatus.Draft,
                Notes = txtNotes.Text.Trim(),
                AvatarPath = avatarPathToSave,
                Addresses = _addresses
            };

            btnSave.Enabled = false;

            try
            {
                var savedId = await _profileService.SaveAsync(req);
                SavedProfileId = savedId;
                MessageBox.Show("Lưu thông tin hồ sơ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu hồ sơ:\n" + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private void PicAvatar_Click(object sender, EventArgs e)
        {
            BtnChooseAvatar_Click(sender, e);
        }

        private void BtnChooseAvatar_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh đại diện hồ sơ";
                ofd.Filter = "Tệp hình ảnh (*.jpg;*.jpeg;*.png;*.bmp;*.webp)|*.jpg;*.jpeg;*.png;*.bmp;*.webp;*.gif|Tất cả tệp (*.*)|*.*";
                ofd.Multiselect = false;

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    var img = LoadImageCopy(ofd.FileName);
                    if (img != null)
                    {
                        picAvatar.Image?.Dispose();
                        picAvatar.Image = img;
                        _pendingAvatarSourcePath = ofd.FileName;
                        _avatarRemoved = false;
                        btnRemoveAvatar.Enabled = true;
                        picAvatar.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Không thể đọc tệp ảnh đã chọn hoặc định dạng không hợp lệ.", "Lỗi tải ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void BtnRemoveAvatar_Click(object sender, EventArgs e)
        {
            picAvatar.Image?.Dispose();
            picAvatar.Image = null;
            _pendingAvatarSourcePath = null;
            _avatarRemoved = true;
            btnRemoveAvatar.Enabled = false;
            picAvatar.Invalidate();
        }

        private void PicAvatar_Paint(object sender, PaintEventArgs e)
        {
            if (picAvatar.Image == null)
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                var w = picAvatar.ClientSize.Width;
                var h = picAvatar.ClientSize.Height;

                // Subtle silhouette
                using (var brush = new SolidBrush(Color.FromArgb(203, 213, 225)))
                {
                    int headRadius = 24;
                    int headX = (w / 2) - headRadius;
                    int headY = (h / 2) - 45;
                    g.FillEllipse(brush, headX, headY, headRadius * 2, headRadius * 2);

                    int shoulderW = 68;
                    int shoulderH = 40;
                    int shoulderX = (w / 2) - (shoulderW / 2);
                    int shoulderY = headY + (headRadius * 2) + 6;
                    g.FillPie(brush, shoulderX, shoulderY, shoulderW, shoulderH * 2, 180, 180);
                }

                using (var font = new Font("Segoe UI", 8F))
                using (var textBrush = new SolidBrush(Color.FromArgb(100, 116, 139)))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    var textRect = new RectangleF(4, h - 38, w - 8, 32);
                    g.DrawString("Nhấp để chọn ảnh", font, textBrush, textRect, sf);
                }
            }
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
