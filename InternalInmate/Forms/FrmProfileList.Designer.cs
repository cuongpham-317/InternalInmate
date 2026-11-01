namespace InternalInmate.Forms
{
    partial class FrmProfileList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.chkFilterDate = new System.Windows.Forms.CheckBox();
            this.cboStatusFilter = new System.Windows.Forms.ComboBox();
            this.cboCategoryFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDocuments = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.gridProfiles = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAvatar = new System.Windows.Forms.DataGridViewImageColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrimaryAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSelectedInfo = new System.Windows.Forms.Label();
            this.picSelectedAvatar = new System.Windows.Forms.PictureBox();
            this.pnlFilter.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFilter.Controls.Add(this.btnResetFilter);
            this.pnlFilter.Controls.Add(this.btnSearch);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.lblTo);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.chkFilterDate);
            this.pnlFilter.Controls.Add(this.cboStatusFilter);
            this.pnlFilter.Controls.Add(this.cboCategoryFilter);
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(12);
            this.pnlFilter.Size = new System.Drawing.Size(984, 85);
            this.pnlFilter.TabIndex = 0;
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetFilter.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnResetFilter.Location = new System.Drawing.Point(892, 46);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(80, 27);
            this.btnResetFilter.TabIndex = 9;
            this.btnResetFilter.Text = "Xóa lọc";
            this.btnResetFilter.UseVisualStyleBackColor = false;
            this.btnResetFilter.Click += new System.EventHandler(this.BtnResetFilter_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(892, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(580, 48);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(105, 23);
            this.dtpTo.TabIndex = 7;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(546, 52);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 15);
            this.lblTo.TabIndex = 6;
            this.lblTo.Text = "đến";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(435, 48);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(105, 23);
            this.dtpFrom.TabIndex = 5;
            // 
            // chkFilterDate
            // 
            this.chkFilterDate.AutoSize = true;
            this.chkFilterDate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.chkFilterDate.Location = new System.Drawing.Point(365, 50);
            this.chkFilterDate.Name = "chkFilterDate";
            this.chkFilterDate.Size = new System.Drawing.Size(68, 19);
            this.chkFilterDate.TabIndex = 4;
            this.chkFilterDate.Text = "Từ ngày";
            this.chkFilterDate.UseVisualStyleBackColor = true;
            this.chkFilterDate.CheckedChanged += new System.EventHandler(this.ChkFilterDate_CheckedChanged);
            // 
            // cboStatusFilter
            // 
            this.cboStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusFilter.FormattingEnabled = true;
            this.cboStatusFilter.Location = new System.Drawing.Point(190, 47);
            this.cboStatusFilter.Name = "cboStatusFilter";
            this.cboStatusFilter.Size = new System.Drawing.Size(160, 23);
            this.cboStatusFilter.TabIndex = 3;
            // 
            // cboCategoryFilter
            // 
            this.cboCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoryFilter.FormattingEnabled = true;
            this.cboCategoryFilter.Location = new System.Drawing.Point(12, 47);
            this.cboCategoryFilter.Name = "cboCategoryFilter";
            this.cboCategoryFilter.Size = new System.Drawing.Size(170, 23);
            this.cboCategoryFilter.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(85, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(795, 24);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Location = new System.Drawing.Point(12, 16);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(60, 15);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.picSelectedAvatar);
            this.pnlToolbar.Controls.Add(this.lblSelectedInfo);
            this.pnlToolbar.Controls.Add(this.btnRefresh);
            this.pnlToolbar.Controls.Add(this.btnDelete);
            this.pnlToolbar.Controls.Add(this.btnDocuments);
            this.pnlToolbar.Controls.Add(this.btnEdit);
            this.pnlToolbar.Controls.Add(this.btnAdd);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 85);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlToolbar.Size = new System.Drawing.Size(984, 48);
            this.pnlToolbar.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefresh.Location = new System.Drawing.Point(448, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(85, 32);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.Location = new System.Drawing.Point(347, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(95, 32);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnDocuments
            // 
            this.btnDocuments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnDocuments.Enabled = false;
            this.btnDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDocuments.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnDocuments.Location = new System.Drawing.Point(216, 8);
            this.btnDocuments.Name = "btnDocuments";
            this.btnDocuments.Size = new System.Drawing.Size(125, 32);
            this.btnDocuments.TabIndex = 2;
            this.btnDocuments.Text = "Tài liệu đính kèm";
            this.btnDocuments.UseVisualStyleBackColor = false;
            this.btnDocuments.Click += new System.EventHandler(this.BtnDocuments_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnEdit.Enabled = false;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnEdit.Location = new System.Drawing.Point(115, 8);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(95, 32);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Xem / Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(12, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(97, 32);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+ Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(984, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // lblStatusInfo
            // 
            this.lblStatusInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatusInfo.Name = "lblStatusInfo";
            this.lblStatusInfo.Size = new System.Drawing.Size(57, 17);
            this.lblStatusInfo.Text = "Sẵn sàng.";
            // 
            // gridProfiles
            // 
            this.gridProfiles.AllowUserToAddRows = false;
            this.gridProfiles.AllowUserToDeleteRows = false;
            this.gridProfiles.BackgroundColor = System.Drawing.Color.White;
            this.gridProfiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProfiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridProfiles.ColumnHeadersHeight = 34;
            this.gridProfiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colAvatar,
            this.colCode,
            this.colFullName,
            this.colCategory,
            this.colStatus,
            this.colPrimaryAddress,
            this.colCreatedAt});
            this.gridProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProfiles.EnableHeadersVisualStyles = false;
            this.gridProfiles.Location = new System.Drawing.Point(0, 133);
            this.gridProfiles.MultiSelect = false;
            this.gridProfiles.Name = "gridProfiles";
            this.gridProfiles.ReadOnly = true;
            this.gridProfiles.RowHeadersVisible = false;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.gridProfiles.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.gridProfiles.RowTemplate.Height = 42;
            this.gridProfiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProfiles.Size = new System.Drawing.Size(984, 406);
            this.gridProfiles.TabIndex = 3;
            this.gridProfiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridProfiles_CellDoubleClick);
            this.gridProfiles.SelectionChanged += new System.EventHandler(this.GridProfiles_SelectionChanged);
            // 
            // colId
            // 
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colAvatar
            // 
            this.colAvatar.HeaderText = "Ảnh";
            this.colAvatar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colAvatar.Name = "colAvatar";
            this.colAvatar.ReadOnly = true;
            this.colAvatar.Width = 52;
            // 
            // colCode
            // 
            this.colCode.HeaderText = "Mã hồ sơ";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            this.colCode.Width = 120;
            // 
            // colFullName
            // 
            this.colFullName.HeaderText = "Họ và tên";
            this.colFullName.Name = "colFullName";
            this.colFullName.ReadOnly = true;
            this.colFullName.Width = 200;
            // 
            // colCategory
            // 
            this.colCategory.HeaderText = "Loại hồ sơ";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            this.colCategory.Width = 140;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 110;
            // 
            // colPrimaryAddress
            // 
            this.colPrimaryAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPrimaryAddress.HeaderText = "Địa chỉ chính";
            this.colPrimaryAddress.Name = "colPrimaryAddress";
            this.colPrimaryAddress.ReadOnly = true;
            // 
            // colCreatedAt
            // 
            this.colCreatedAt.HeaderText = "Ngày tạo";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.ReadOnly = true;
            this.colCreatedAt.Width = 130;
            // 
            // lblSelectedInfo
            // 
            this.lblSelectedInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblSelectedInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSelectedInfo.Location = new System.Drawing.Point(590, 15);
            this.lblSelectedInfo.Name = "lblSelectedInfo";
            this.lblSelectedInfo.Size = new System.Drawing.Size(382, 20);
            this.lblSelectedInfo.TabIndex = 5;
            this.lblSelectedInfo.Text = "Chưa chọn hồ sơ nào.";
            this.lblSelectedInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picSelectedAvatar
            // 
            this.picSelectedAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSelectedAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.picSelectedAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSelectedAvatar.Location = new System.Drawing.Point(549, 7);
            this.picSelectedAvatar.Name = "picSelectedAvatar";
            this.picSelectedAvatar.Size = new System.Drawing.Size(34, 34);
            this.picSelectedAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSelectedAvatar.TabIndex = 6;
            this.picSelectedAvatar.TabStop = false;
            this.picSelectedAvatar.Visible = false;
            this.picSelectedAvatar.Paint += new System.Windows.Forms.PaintEventHandler(this.PicSelectedAvatar_Paint);
            // 
            // FrmProfileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.gridProfiles);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlFilter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmProfileList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý danh sách hồ sơ";
            this.Load += new System.EventHandler(this.FrmProfileList_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectedAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboCategoryFilter;
        private System.Windows.Forms.ComboBox cboStatusFilter;
        private System.Windows.Forms.CheckBox chkFilterDate;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDocuments;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusInfo;
        private System.Windows.Forms.DataGridView gridProfiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewImageColumn colAvatar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrimaryAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedAt;
        private System.Windows.Forms.PictureBox picSelectedAvatar;
        private System.Windows.Forms.Label lblSelectedInfo;
    }
}
