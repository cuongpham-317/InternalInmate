using InternalInmate.Application.DTOs;
using InternalInmate.Application.Services;
using InternalInmate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternalInmate.Forms
{
    public partial class FrmDocumentManager : Form
    {
        private readonly int _profileId;
        private readonly string _profileCode;
        private readonly string _profileName;
        private readonly DocumentService _documentService;
        private IReadOnlyList<ProfileDocumentDto> _documents;

        public FrmDocumentManager(int profileId, string profileCode, string profileName)
        {
            InitializeComponent();
            _profileId = profileId;
            _profileCode = profileCode;
            _profileName = profileName;
            _documentService = AppContainer.Current.DocumentService;

            // Kích hoạt tối ưu Double Buffering cho Form và DataGridView
            gridDocuments.OptimizeGrid();
            this.EnableDoubleBuffering();
            pnlHeader.EnableDoubleBuffering();
            pnlToolbar.EnableDoubleBuffering();

            lblHeader.Text = $"Tài liệu đính kèm: [{_profileCode}] {_profileName}";
        }

        private async void FrmDocumentManager_Load(object sender, EventArgs e)
        {
            await LoadDocumentsAsync();
        }

        private async Task LoadDocumentsAsync()
        {
            btnAttach.Enabled = false;
            btnOpen.Enabled = false;
            btnExport.Enabled = false;
            btnDelete.Enabled = false;

            try
            {
                _documents = await _documentService.ListAsync(_profileId);

                gridDocuments.SuspendLayout();
                try
                {
                    gridDocuments.Rows.Clear();

                    if (_documents != null && _documents.Count > 0)
                    {
                        foreach (var doc in _documents)
                        {
                            var rowIndex = gridDocuments.Rows.Add(
                                doc.Id,
                                doc.OriginalFileName,
                                doc.SizeFormatted,
                                doc.UploadedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                                doc.Description ?? string.Empty,
                                doc.FileExists ? "Sẵn sàng" : "Thiếu tệp"
                            );

                            var row = gridDocuments.Rows[rowIndex];
                            if (!doc.FileExists)
                            {
                                row.DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                    }
                }
                finally
                {
                    gridDocuments.ResumeLayout();
                }

                lblCount.Text = $"Tổng số tài liệu: {_documents?.Count ?? 0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách tài liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAttach.Enabled = true;
                UpdateActionButtonsState();
            }
        }

        private void GridDocuments_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActionButtonsState();
        }

        private void UpdateActionButtonsState()
        {
            var hasSelection = gridDocuments.SelectedRows.Count > 0;
            btnOpen.Enabled = hasSelection;
            btnExport.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }

        private ProfileDocumentDto GetSelectedDocument()
        {
            if (gridDocuments.SelectedRows.Count == 0) return null;
            var docId = (int)gridDocuments.SelectedRows[0].Cells["colId"].Value;
            foreach (var doc in _documents)
            {
                if (doc.Id == docId) return doc;
            }
            return null;
        }

        private async void BtnAttach_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn tệp tài liệu đính kèm";
                ofd.Filter = "Tài liệu phổ biến (*.pdf;*.docx;*.xlsx;*.jpg;*.png;*.txt)|*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.jpg;*.jpeg;*.png;*.gif;*.txt|Tất cả tệp (*.*)|*.*";
                ofd.Multiselect = false;

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    var desc = PromptInput("Mô tả tài liệu (tùy chọn):", "Ghi chú tài liệu", Path.GetFileName(ofd.FileName));
                    var req = new AttachDocumentRequest
                    {
                        ProfileId = _profileId,
                        SourceFilePath = ofd.FileName,
                        Description = desc
                    };

                    try
                    {
                        await _documentService.AttachAsync(req);
                        MessageBox.Show("Đính kèm tài liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDocumentsAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể đính kèm tài liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void BtnOpen_Click(object sender, EventArgs e)
        {
            var doc = GetSelectedDocument();
            if (doc == null) return;

            try
            {
                await _documentService.OpenAsync(doc.Id, path =>
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            var doc = GetSelectedDocument();
            if (doc == null) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất bản sao tài liệu";
                sfd.FileName = doc.OriginalFileName;
                sfd.Filter = "Tệp cùng loại (*" + Path.GetExtension(doc.OriginalFileName) + ")|*" + Path.GetExtension(doc.OriginalFileName) + "|Tất cả tệp (*.*)|*.*";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        await _documentService.ExportCopyAsync(doc.Id, sfd.FileName);
                        MessageBox.Show("Đã xuất bản sao tài liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể xuất bản sao: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            var doc = GetSelectedDocument();
            if (doc == null) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài liệu '{doc.OriginalFileName}' khỏi hồ sơ?",
                "Xác nhận xóa tài liệu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await _documentService.DeleteAsync(doc.Id);
                    MessageBox.Show("Đã xóa tài liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDocumentsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa tài liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string PromptInput(string text, string caption, string defaultValue = "")
        {
            using (var prompt = new Form())
            {
                prompt.Width = 460;
                prompt.Height = 160;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.Text = caption;
                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.Font = new Font("Segoe UI", 9F);

                var textLabel = new Label() { Left = 20, Top = 15, Text = text, AutoSize = true };
                var textBox = new TextBox() { Left = 20, Top = 40, Width = 400, Text = defaultValue };
                var confirmation = new Button() { Text = "Đồng ý", Left = 240, Width = 85, Top = 80, DialogResult = DialogResult.OK };
                var cancel = new Button() { Text = "Bỏ qua", Left = 335, Width = 85, Top = 80, DialogResult = DialogResult.Cancel };

                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancel;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text.Trim() : defaultValue;
            }
        }
    }
}
