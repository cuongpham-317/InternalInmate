namespace InternalInmate.Forms
{
    partial class FrmProfileDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnManageDocs = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.grpAvatar = new System.Windows.Forms.GroupBox();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.btnChooseAvatar = new System.Windows.Forms.Button();
            this.btnRemoveAvatar = new System.Windows.Forms.Button();
            this.lblAvatarNote = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtIdentityNumber = new System.Windows.Forms.TextBox();
            this.lblIdNum = new System.Windows.Forms.Label();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.dtpDob = new System.Windows.Forms.DateTimePicker();
            this.chkHasDob = new System.Windows.Forms.CheckBox();
            this.lblDob = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.tabAddresses = new System.Windows.Forms.TabPage();
            this.gridAddresses = new System.Windows.Forms.DataGridView();
            this.colAddrId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsPrimary = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAddressLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistrict = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvince = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAddressTools = new System.Windows.Forms.Panel();
            this.btnPickMap = new System.Windows.Forms.Button();
            this.btnSetPrimaryAddress = new System.Windows.Forms.Button();
            this.btnDeleteAddress = new System.Windows.Forms.Button();
            this.btnAddAddress = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.grpAvatar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.tabAddresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAddresses)).BeginInit();
            this.pnlAddressTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlTop.Controls.Add(this.btnManageDocs);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlTop.Size = new System.Drawing.Size(800, 56);
            this.pnlTop.TabIndex = 0;
            // 
            // btnManageDocs
            // 
            this.btnManageDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnManageDocs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnManageDocs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageDocs.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnManageDocs.Location = new System.Drawing.Point(620, 12);
            this.btnManageDocs.Name = "btnManageDocs";
            this.btnManageDocs.Size = new System.Drawing.Size(168, 32);
            this.btnManageDocs.TabIndex = 1;
            this.btnManageDocs.Text = "Tài liệu đính kèm (0)";
            this.btnManageDocs.UseVisualStyleBackColor = false;
            this.btnManageDocs.Click += new System.EventHandler(this.BtnManageDocs_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(151, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÔNG TIN HỒ SƠ";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 525);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlBottom.Size = new System.Drawing.Size(800, 56);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(700, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(584, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu hồ sơ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabInfo);
            this.tabControl.Controls.Add(this.tabAddresses);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabControl.Location = new System.Drawing.Point(0, 56);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 469);
            this.tabControl.TabIndex = 2;
            // 
            // tabInfo
            // 
            this.tabInfo.BackColor = System.Drawing.Color.White;
            this.tabInfo.Controls.Add(this.grpAvatar);
            this.tabInfo.Controls.Add(this.txtNotes);
            this.tabInfo.Controls.Add(this.lblNotes);
            this.tabInfo.Controls.Add(this.cboStatus);
            this.tabInfo.Controls.Add(this.lblStatus);
            this.tabInfo.Controls.Add(this.cboCategory);
            this.tabInfo.Controls.Add(this.lblCategory);
            this.tabInfo.Controls.Add(this.txtIdentityNumber);
            this.tabInfo.Controls.Add(this.lblIdNum);
            this.tabInfo.Controls.Add(this.cboGender);
            this.tabInfo.Controls.Add(this.lblGender);
            this.tabInfo.Controls.Add(this.dtpDob);
            this.tabInfo.Controls.Add(this.chkHasDob);
            this.tabInfo.Controls.Add(this.lblDob);
            this.tabInfo.Controls.Add(this.txtFullName);
            this.tabInfo.Controls.Add(this.lblFullName);
            this.tabInfo.Controls.Add(this.txtCode);
            this.tabInfo.Controls.Add(this.lblCode);
            this.tabInfo.Location = new System.Drawing.Point(4, 26);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(20);
            this.tabInfo.Size = new System.Drawing.Size(792, 439);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "  Thông tin chung  ";
            // 
            // grpAvatar
            // 
            this.grpAvatar.Controls.Add(this.picAvatar);
            this.grpAvatar.Controls.Add(this.btnChooseAvatar);
            this.grpAvatar.Controls.Add(this.btnRemoveAvatar);
            this.grpAvatar.Controls.Add(this.lblAvatarNote);
            this.grpAvatar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.grpAvatar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.grpAvatar.Location = new System.Drawing.Point(20, 20);
            this.grpAvatar.Name = "grpAvatar";
            this.grpAvatar.Size = new System.Drawing.Size(160, 270);
            this.grpAvatar.TabIndex = 0;
            this.grpAvatar.TabStop = false;
            this.grpAvatar.Text = "Ảnh hồ sơ";
            // 
            // picAvatar
            // 
            this.picAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.picAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAvatar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAvatar.Location = new System.Drawing.Point(10, 22);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(140, 175);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            this.picAvatar.Click += new System.EventHandler(this.PicAvatar_Click);
            this.picAvatar.Paint += new System.Windows.Forms.PaintEventHandler(this.PicAvatar_Paint);
            // 
            // btnChooseAvatar
            // 
            this.btnChooseAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnChooseAvatar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseAvatar.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnChooseAvatar.Location = new System.Drawing.Point(10, 205);
            this.btnChooseAvatar.Name = "btnChooseAvatar";
            this.btnChooseAvatar.Size = new System.Drawing.Size(78, 28);
            this.btnChooseAvatar.TabIndex = 1;
            this.btnChooseAvatar.Text = "Chọn ảnh";
            this.btnChooseAvatar.UseVisualStyleBackColor = false;
            this.btnChooseAvatar.Click += new System.EventHandler(this.BtnChooseAvatar_Click);
            // 
            // btnRemoveAvatar
            // 
            this.btnRemoveAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnRemoveAvatar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAvatar.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRemoveAvatar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnRemoveAvatar.Location = new System.Drawing.Point(92, 205);
            this.btnRemoveAvatar.Name = "btnRemoveAvatar";
            this.btnRemoveAvatar.Size = new System.Drawing.Size(58, 28);
            this.btnRemoveAvatar.TabIndex = 2;
            this.btnRemoveAvatar.Text = "Xóa";
            this.btnRemoveAvatar.UseVisualStyleBackColor = false;
            this.btnRemoveAvatar.Click += new System.EventHandler(this.BtnRemoveAvatar_Click);
            // 
            // lblAvatarNote
            // 
            this.lblAvatarNote.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAvatarNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAvatarNote.Location = new System.Drawing.Point(8, 240);
            this.lblAvatarNote.Name = "lblAvatarNote";
            this.lblAvatarNote.Size = new System.Drawing.Size(144, 20);
            this.lblAvatarNote.TabIndex = 3;
            this.lblAvatarNote.Text = "Tối đa 10 MB (.jpg, .png)";
            this.lblAvatarNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.Location = new System.Drawing.Point(285, 196);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(491, 190);
            this.txtNotes.TabIndex = 17;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNotes.Location = new System.Drawing.Point(195, 201);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(51, 15);
            this.lblNotes.TabIndex = 16;
            this.lblNotes.Text = "Ghi chú:";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(570, 25);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(190, 25);
            this.cboStatus.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(485, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(63, 15);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Trạng thái:";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(285, 153);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(180, 25);
            this.cboCategory.TabIndex = 13;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.Location = new System.Drawing.Point(195, 158);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(72, 15);
            this.lblCategory.TabIndex = 12;
            this.lblCategory.Text = "Loại hồ sơ *:";
            // 
            // txtIdentityNumber
            // 
            this.txtIdentityNumber.Location = new System.Drawing.Point(570, 153);
            this.txtIdentityNumber.Name = "txtIdentityNumber";
            this.txtIdentityNumber.Size = new System.Drawing.Size(190, 24);
            this.txtIdentityNumber.TabIndex = 15;
            // 
            // lblIdNum
            // 
            this.lblIdNum.AutoSize = true;
            this.lblIdNum.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblIdNum.Location = new System.Drawing.Point(485, 158);
            this.lblIdNum.Name = "lblIdNum";
            this.lblIdNum.Size = new System.Drawing.Size(81, 15);
            this.lblIdNum.TabIndex = 14;
            this.lblIdNum.Text = "Số định danh:";
            // 
            // cboGender
            // 
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Location = new System.Drawing.Point(570, 110);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(190, 25);
            this.cboGender.TabIndex = 11;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblGender.Location = new System.Drawing.Point(485, 115);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(55, 15);
            this.lblGender.TabIndex = 10;
            this.lblGender.Text = "Giới tính:";
            // 
            // dtpDob
            // 
            this.dtpDob.CustomFormat = "dd/MM/yyyy";
            this.dtpDob.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDob.Location = new System.Drawing.Point(308, 112);
            this.dtpDob.Name = "dtpDob";
            this.dtpDob.Size = new System.Drawing.Size(157, 24);
            this.dtpDob.TabIndex = 9;
            // 
            // chkHasDob
            // 
            this.chkHasDob.AutoSize = true;
            this.chkHasDob.Location = new System.Drawing.Point(285, 118);
            this.chkHasDob.Name = "chkHasDob";
            this.chkHasDob.Size = new System.Drawing.Size(15, 14);
            this.chkHasDob.TabIndex = 8;
            this.chkHasDob.UseVisualStyleBackColor = true;
            this.chkHasDob.CheckedChanged += new System.EventHandler(this.ChkHasDob_CheckedChanged);
            // 
            // lblDob
            // 
            this.lblDob.AutoSize = true;
            this.lblDob.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDob.Location = new System.Drawing.Point(195, 115);
            this.lblDob.Name = "lblDob";
            this.lblDob.Size = new System.Drawing.Size(63, 15);
            this.lblDob.TabIndex = 7;
            this.lblDob.Text = "Ngày sinh:";
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.Location = new System.Drawing.Point(285, 68);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(491, 24);
            this.txtFullName.TabIndex = 6;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFullName.Location = new System.Drawing.Point(195, 73);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(69, 15);
            this.lblFullName.TabIndex = 5;
            this.lblFullName.Text = "Họ và tên *:";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCode.Location = new System.Drawing.Point(285, 25);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(180, 24);
            this.txtCode.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCode.Location = new System.Drawing.Point(195, 30);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(67, 15);
            this.lblCode.TabIndex = 1;
            this.lblCode.Text = "Mã hồ sơ *:";
            // 
            // tabAddresses
            // 
            this.tabAddresses.BackColor = System.Drawing.Color.White;
            this.tabAddresses.Controls.Add(this.gridAddresses);
            this.tabAddresses.Controls.Add(this.pnlAddressTools);
            this.tabAddresses.Location = new System.Drawing.Point(4, 26);
            this.tabAddresses.Name = "tabAddresses";
            this.tabAddresses.Padding = new System.Windows.Forms.Padding(12);
            this.tabAddresses.Size = new System.Drawing.Size(776, 439);
            this.tabAddresses.TabIndex = 1;
            this.tabAddresses.Text = "  Danh sách địa chỉ  ";
            // 
            // gridAddresses
            // 
            this.gridAddresses.AllowUserToAddRows = false;
            this.gridAddresses.AllowUserToDeleteRows = false;
            this.gridAddresses.BackgroundColor = System.Drawing.Color.White;
            this.gridAddresses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAddresses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridAddresses.ColumnHeadersHeight = 32;
            this.gridAddresses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAddrId,
            this.colIsPrimary,
            this.colAddressLine,
            this.colWard,
            this.colDistrict,
            this.colProvince,
            this.colCoordinates,
            this.colNote});
            this.gridAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAddresses.EnableHeadersVisualStyles = false;
            this.gridAddresses.Location = new System.Drawing.Point(12, 54);
            this.gridAddresses.MultiSelect = false;
            this.gridAddresses.Name = "gridAddresses";
            this.gridAddresses.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.gridAddresses.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridAddresses.RowTemplate.Height = 28;
            this.gridAddresses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAddresses.Size = new System.Drawing.Size(752, 373);
            this.gridAddresses.TabIndex = 1;
            this.gridAddresses.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridAddresses_CellEndEdit);
            // 
            // colAddrId
            // 
            this.colAddrId.HeaderText = "ID";
            this.colAddrId.Name = "colAddrId";
            this.colAddrId.ReadOnly = true;
            this.colAddrId.Visible = false;
            // 
            // colIsPrimary
            // 
            this.colIsPrimary.HeaderText = "Chính";
            this.colIsPrimary.Name = "colIsPrimary";
            this.colIsPrimary.Width = 55;
            // 
            // colAddressLine
            // 
            this.colAddressLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAddressLine.HeaderText = "Số nhà, tên đường";
            this.colAddressLine.Name = "colAddressLine";
            // 
            // colWard
            // 
            this.colWard.HeaderText = "Phường/Xã";
            this.colWard.Name = "colWard";
            this.colWard.Width = 110;
            // 
            // colDistrict
            // 
            this.colDistrict.HeaderText = "Quận/Huyện";
            this.colDistrict.Name = "colDistrict";
            this.colDistrict.Width = 110;
            // 
            // colProvince
            // 
            this.colProvince.HeaderText = "Tỉnh/TP";
            this.colProvince.Name = "colProvince";
            this.colProvince.Width = 110;
            // 
            // colCoordinates
            // 
            this.colCoordinates.HeaderText = "Tọa độ (Lat, Lng)";
            this.colCoordinates.Name = "colCoordinates";
            this.colCoordinates.ReadOnly = true;
            this.colCoordinates.Width = 130;
            // 
            // colNote
            // 
            this.colNote.HeaderText = "Ghi chú";
            this.colNote.Name = "colNote";
            this.colNote.Width = 110;
            // 
            // pnlAddressTools
            // 
            this.pnlAddressTools.Controls.Add(this.btnPickMap);
            this.pnlAddressTools.Controls.Add(this.btnSetPrimaryAddress);
            this.pnlAddressTools.Controls.Add(this.btnDeleteAddress);
            this.pnlAddressTools.Controls.Add(this.btnAddAddress);
            this.pnlAddressTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddressTools.Location = new System.Drawing.Point(12, 12);
            this.pnlAddressTools.Name = "pnlAddressTools";
            this.pnlAddressTools.Size = new System.Drawing.Size(752, 42);
            this.pnlAddressTools.TabIndex = 0;
            // 
            // btnPickMap
            // 
            this.btnPickMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnPickMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPickMap.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnPickMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.btnPickMap.Location = new System.Drawing.Point(380, 4);
            this.btnPickMap.Name = "btnPickMap";
            this.btnPickMap.Size = new System.Drawing.Size(160, 30);
            this.btnPickMap.TabIndex = 3;
            this.btnPickMap.Text = "Chọn trên bản đồ";
            this.btnPickMap.UseVisualStyleBackColor = false;
            this.btnPickMap.Click += new System.EventHandler(this.BtnPickMap_Click);
            // 
            // btnSetPrimaryAddress
            // 
            this.btnSetPrimaryAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnSetPrimaryAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPrimaryAddress.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnSetPrimaryAddress.Location = new System.Drawing.Point(230, 4);
            this.btnSetPrimaryAddress.Name = "btnSetPrimaryAddress";
            this.btnSetPrimaryAddress.Size = new System.Drawing.Size(140, 30);
            this.btnSetPrimaryAddress.TabIndex = 2;
            this.btnSetPrimaryAddress.Text = "Đặt làm địa chỉ chính";
            this.btnSetPrimaryAddress.UseVisualStyleBackColor = false;
            this.btnSetPrimaryAddress.Click += new System.EventHandler(this.BtnSetPrimaryAddress_Click);
            // 
            // btnDeleteAddress
            // 
            this.btnDeleteAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnDeleteAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAddress.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnDeleteAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDeleteAddress.Location = new System.Drawing.Point(120, 4);
            this.btnDeleteAddress.Name = "btnDeleteAddress";
            this.btnDeleteAddress.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteAddress.TabIndex = 1;
            this.btnDeleteAddress.Text = "Xóa địa chỉ";
            this.btnDeleteAddress.UseVisualStyleBackColor = false;
            this.btnDeleteAddress.Click += new System.EventHandler(this.BtnDeleteAddress_Click);
            // 
            // btnAddAddress
            // 
            this.btnAddAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnAddAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnAddAddress.Location = new System.Drawing.Point(4, 4);
            this.btnAddAddress.Name = "btnAddAddress";
            this.btnAddAddress.Size = new System.Drawing.Size(105, 30);
            this.btnAddAddress.TabIndex = 0;
            this.btnAddAddress.Text = "+ Thêm địa chỉ";
            this.btnAddAddress.UseVisualStyleBackColor = false;
            this.btnAddAddress.Click += new System.EventHandler(this.BtnAddAddress_Click);
            // 
            // FrmProfileDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 581);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(760, 520);
            this.Name = "FrmProfileDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin chi tiết hồ sơ";
            this.Load += new System.EventHandler(this.FrmProfileDetail_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.tabInfo.PerformLayout();
            this.grpAvatar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.tabAddresses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAddresses)).EndInit();
            this.pnlAddressTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnManageDocs;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.GroupBox grpAvatar;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Button btnChooseAvatar;
        private System.Windows.Forms.Button btnRemoveAvatar;
        private System.Windows.Forms.Label lblAvatarNote;
        private System.Windows.Forms.TabPage tabAddresses;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblDob;
        private System.Windows.Forms.CheckBox chkHasDob;
        private System.Windows.Forms.DateTimePicker dtpDob;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.Label lblIdNum;
        private System.Windows.Forms.TextBox txtIdentityNumber;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Panel pnlAddressTools;
        private System.Windows.Forms.Button btnAddAddress;
        private System.Windows.Forms.Button btnDeleteAddress;
        private System.Windows.Forms.Button btnSetPrimaryAddress;
        private System.Windows.Forms.Button btnPickMap;
        private System.Windows.Forms.DataGridView gridAddresses;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddrId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsPrimary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddressLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistrict;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvince;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCoordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
    }
}
