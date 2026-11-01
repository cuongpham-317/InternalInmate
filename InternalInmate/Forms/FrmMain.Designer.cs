namespace InternalInmate.Forms
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnNavAbout = new System.Windows.Forms.Button();
            this.btnNavBackup = new System.Windows.Forms.Button();
            this.btnNavNewProfile = new System.Windows.Forms.Button();
            this.btnNavProfiles = new System.Windows.Forms.Button();
            this.btnNavDashboard = new System.Windows.Forms.Button();
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.lblBrandSub = new System.Windows.Forms.Label();
            this.lblBrandTitle = new System.Windows.Forms.Label();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDbStatus = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlDashboardContent = new System.Windows.Forms.Panel();
            this.grpQuickActions = new System.Windows.Forms.GroupBox();
            this.btnQuickBackup = new System.Windows.Forms.Button();
            this.btnQuickList = new System.Windows.Forms.Button();
            this.btnQuickAdd = new System.Windows.Forms.Button();
            this.tblKpi = new System.Windows.Forms.TableLayoutPanel();
            this.cardClosed = new System.Windows.Forms.Panel();
            this.lblStatClosed = new System.Windows.Forms.Label();
            this.lblTitleClosed = new System.Windows.Forms.Label();
            this.cardDraft = new System.Windows.Forms.Panel();
            this.lblStatDraft = new System.Windows.Forms.Label();
            this.lblTitleDraft = new System.Windows.Forms.Label();
            this.cardActive = new System.Windows.Forms.Panel();
            this.lblStatActive = new System.Windows.Forms.Label();
            this.lblTitleActive = new System.Windows.Forms.Label();
            this.cardTotal = new System.Windows.Forms.Panel();
            this.lblStatTotal = new System.Windows.Forms.Label();
            this.lblTitleTotal = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.pnlSidebar.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            this.pnlTopBar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlDashboardContent.SuspendLayout();
            this.grpQuickActions.SuspendLayout();
            this.tblKpi.SuspendLayout();
            this.cardClosed.SuspendLayout();
            this.cardDraft.SuspendLayout();
            this.cardActive.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSidebar.Controls.Add(this.lblVersion);
            this.pnlSidebar.Controls.Add(this.btnNavAbout);
            this.pnlSidebar.Controls.Add(this.btnNavBackup);
            this.pnlSidebar.Controls.Add(this.btnNavNewProfile);
            this.pnlSidebar.Controls.Add(this.btnNavProfiles);
            this.pnlSidebar.Controls.Add(this.btnNavDashboard);
            this.pnlSidebar.Controls.Add(this.pnlBrand);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(220, 681);
            this.pnlSidebar.TabIndex = 0;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblVersion.Location = new System.Drawing.Point(12, 659);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(73, 13);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "Phiên bản 1.0";
            // 
            // btnNavAbout
            // 
            this.btnNavAbout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavAbout.FlatAppearance.BorderSize = 0;
            this.btnNavAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavAbout.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavAbout.Location = new System.Drawing.Point(0, 276);
            this.btnNavAbout.Name = "btnNavAbout";
            this.btnNavAbout.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnNavAbout.Size = new System.Drawing.Size(220, 48);
            this.btnNavAbout.TabIndex = 5;
            this.btnNavAbout.Text = "ℹ️  Giới thiệu & Trợ giúp";
            this.btnNavAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavAbout.UseVisualStyleBackColor = true;
            this.btnNavAbout.Click += new System.EventHandler(this.BtnNavAbout_Click);
            // 
            // btnNavBackup
            // 
            this.btnNavBackup.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavBackup.FlatAppearance.BorderSize = 0;
            this.btnNavBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavBackup.Location = new System.Drawing.Point(0, 228);
            this.btnNavBackup.Name = "btnNavBackup";
            this.btnNavBackup.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnNavBackup.Size = new System.Drawing.Size(220, 48);
            this.btnNavBackup.TabIndex = 4;
            this.btnNavBackup.Text = "💾  Sao lưu & Phục hồi";
            this.btnNavBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavBackup.UseVisualStyleBackColor = true;
            this.btnNavBackup.Click += new System.EventHandler(this.BtnNavBackup_Click);
            // 
            // btnNavNewProfile
            // 
            this.btnNavNewProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavNewProfile.FlatAppearance.BorderSize = 0;
            this.btnNavNewProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavNewProfile.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavNewProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavNewProfile.Location = new System.Drawing.Point(0, 180);
            this.btnNavNewProfile.Name = "btnNavNewProfile";
            this.btnNavNewProfile.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnNavNewProfile.Size = new System.Drawing.Size(220, 48);
            this.btnNavNewProfile.TabIndex = 3;
            this.btnNavNewProfile.Text = "➕  Thêm hồ sơ mới";
            this.btnNavNewProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNewProfile.UseVisualStyleBackColor = true;
            this.btnNavNewProfile.Click += new System.EventHandler(this.BtnNavNewProfile_Click);
            // 
            // btnNavProfiles
            // 
            this.btnNavProfiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavProfiles.FlatAppearance.BorderSize = 0;
            this.btnNavProfiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavProfiles.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavProfiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavProfiles.Location = new System.Drawing.Point(0, 132);
            this.btnNavProfiles.Name = "btnNavProfiles";
            this.btnNavProfiles.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnNavProfiles.Size = new System.Drawing.Size(220, 48);
            this.btnNavProfiles.TabIndex = 2;
            this.btnNavProfiles.Text = "📁  Quản lý hồ sơ";
            this.btnNavProfiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavProfiles.UseVisualStyleBackColor = true;
            this.btnNavProfiles.Click += new System.EventHandler(this.BtnNavProfiles_Click);
            // 
            // btnNavDashboard
            // 
            this.btnNavDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnNavDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavDashboard.FlatAppearance.BorderSize = 0;
            this.btnNavDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNavDashboard.Location = new System.Drawing.Point(0, 84);
            this.btnNavDashboard.Name = "btnNavDashboard";
            this.btnNavDashboard.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnNavDashboard.Size = new System.Drawing.Size(220, 48);
            this.btnNavDashboard.TabIndex = 1;
            this.btnNavDashboard.Text = "📊  Tổng quan";
            this.btnNavDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDashboard.UseVisualStyleBackColor = false;
            this.btnNavDashboard.Click += new System.EventHandler(this.BtnNavDashboard_Click);
            // 
            // pnlBrand
            // 
            this.pnlBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlBrand.Controls.Add(this.lblBrandSub);
            this.pnlBrand.Controls.Add(this.lblBrandTitle);
            this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBrand.Location = new System.Drawing.Point(0, 0);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Padding = new System.Windows.Forms.Padding(16, 18, 16, 12);
            this.pnlBrand.Size = new System.Drawing.Size(220, 84);
            this.pnlBrand.TabIndex = 0;
            // 
            // lblBrandSub
            // 
            this.lblBrandSub.AutoSize = true;
            this.lblBrandSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBrandSub.Location = new System.Drawing.Point(14, 46);
            this.lblBrandSub.Name = "lblBrandSub";
            this.lblBrandSub.Size = new System.Drawing.Size(124, 15);
            this.lblBrandSub.TabIndex = 1;
            this.lblBrandSub.Text = "Profile Management";
            // 
            // lblBrandTitle
            // 
            this.lblBrandTitle.AutoSize = true;
            this.lblBrandTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBrandTitle.ForeColor = System.Drawing.Color.White;
            this.lblBrandTitle.Location = new System.Drawing.Point(12, 18);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new System.Drawing.Size(155, 21);
            this.lblBrandTitle.TabIndex = 0;
            this.lblBrandTitle.Text = "QUẢN LÝ HỒ SƠ";
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.White;
            this.pnlTopBar.Controls.Add(this.lblTime);
            this.pnlTopBar.Controls.Add(this.lblDbStatus);
            this.pnlTopBar.Controls.Add(this.lblPageTitle);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(220, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlTopBar.Size = new System.Drawing.Size(964, 56);
            this.pnlTopBar.TabIndex = 1;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTime.Location = new System.Drawing.Point(834, 21);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(107, 15);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "24/10/2026 14:00:00";
            // 
            // lblDbStatus
            // 
            this.lblDbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDbStatus.AutoSize = true;
            this.lblDbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.lblDbStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblDbStatus.Location = new System.Drawing.Point(620, 20);
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.lblDbStatus.Size = new System.Drawing.Size(185, 19);
            this.lblDbStatus.TabIndex = 1;
            this.lblDbStatus.Text = "CSDL: ProfileManagementDb";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPageTitle.Location = new System.Drawing.Point(16, 17);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(193, 21);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "TỔNG QUAN HỆ THỐNG";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlContent.Controls.Add(this.pnlDashboardContent);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(220, 56);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(24);
            this.pnlContent.Size = new System.Drawing.Size(964, 625);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlDashboardContent
            // 
            this.pnlDashboardContent.Controls.Add(this.grpQuickActions);
            this.pnlDashboardContent.Controls.Add(this.tblKpi);
            this.pnlDashboardContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboardContent.Location = new System.Drawing.Point(24, 24);
            this.pnlDashboardContent.Name = "pnlDashboardContent";
            this.pnlDashboardContent.Size = new System.Drawing.Size(916, 577);
            this.pnlDashboardContent.TabIndex = 0;
            // 
            // grpQuickActions
            // 
            this.grpQuickActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpQuickActions.BackColor = System.Drawing.Color.White;
            this.grpQuickActions.Controls.Add(this.btnQuickBackup);
            this.grpQuickActions.Controls.Add(this.btnQuickList);
            this.grpQuickActions.Controls.Add(this.btnQuickAdd);
            this.grpQuickActions.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpQuickActions.Location = new System.Drawing.Point(4, 155);
            this.grpQuickActions.Name = "grpQuickActions";
            this.grpQuickActions.Padding = new System.Windows.Forms.Padding(16);
            this.grpQuickActions.Size = new System.Drawing.Size(908, 110);
            this.grpQuickActions.TabIndex = 1;
            this.grpQuickActions.TabStop = false;
            this.grpQuickActions.Text = "Thao tác nhanh";
            // 
            // btnQuickBackup
            // 
            this.btnQuickBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnQuickBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuickBackup.Location = new System.Drawing.Point(412, 38);
            this.btnQuickBackup.Name = "btnQuickBackup";
            this.btnQuickBackup.Size = new System.Drawing.Size(180, 42);
            this.btnQuickBackup.TabIndex = 2;
            this.btnQuickBackup.Text = "💾 Sao lưu dữ liệu";
            this.btnQuickBackup.UseVisualStyleBackColor = false;
            this.btnQuickBackup.Click += new System.EventHandler(this.BtnQuickBackup_Click);
            // 
            // btnQuickList
            // 
            this.btnQuickList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnQuickList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickList.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuickList.Location = new System.Drawing.Point(216, 38);
            this.btnQuickList.Name = "btnQuickList";
            this.btnQuickList.Size = new System.Drawing.Size(180, 42);
            this.btnQuickList.TabIndex = 1;
            this.btnQuickList.Text = "📁 Tìm kiếm hồ sơ";
            this.btnQuickList.UseVisualStyleBackColor = false;
            this.btnQuickList.Click += new System.EventHandler(this.BtnQuickList_Click);
            // 
            // btnQuickAdd
            // 
            this.btnQuickAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnQuickAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuickAdd.ForeColor = System.Drawing.Color.White;
            this.btnQuickAdd.Location = new System.Drawing.Point(20, 38);
            this.btnQuickAdd.Name = "btnQuickAdd";
            this.btnQuickAdd.Size = new System.Drawing.Size(180, 42);
            this.btnQuickAdd.TabIndex = 0;
            this.btnQuickAdd.Text = "➕ Tạo hồ sơ mới";
            this.btnQuickAdd.UseVisualStyleBackColor = false;
            this.btnQuickAdd.Click += new System.EventHandler(this.BtnQuickAdd_Click);
            // 
            // tblKpi
            // 
            this.tblKpi.ColumnCount = 4;
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKpi.Controls.Add(this.cardClosed, 3, 0);
            this.tblKpi.Controls.Add(this.cardDraft, 2, 0);
            this.tblKpi.Controls.Add(this.cardActive, 1, 0);
            this.tblKpi.Controls.Add(this.cardTotal, 0, 0);
            this.tblKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblKpi.Location = new System.Drawing.Point(0, 0);
            this.tblKpi.Name = "tblKpi";
            this.tblKpi.RowCount = 1;
            this.tblKpi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblKpi.Size = new System.Drawing.Size(916, 130);
            this.tblKpi.TabIndex = 0;
            // 
            // cardClosed
            // 
            this.cardClosed.BackColor = System.Drawing.Color.White;
            this.cardClosed.Controls.Add(this.lblStatClosed);
            this.cardClosed.Controls.Add(this.lblTitleClosed);
            this.cardClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardClosed.Location = new System.Drawing.Point(695, 8);
            this.cardClosed.Margin = new System.Windows.Forms.Padding(8);
            this.cardClosed.Name = "cardClosed";
            this.cardClosed.Padding = new System.Windows.Forms.Padding(16);
            this.cardClosed.Size = new System.Drawing.Size(213, 114);
            this.cardClosed.TabIndex = 3;
            // 
            // lblStatClosed
            // 
            this.lblStatClosed.AutoSize = true;
            this.lblStatClosed.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStatClosed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatClosed.Location = new System.Drawing.Point(12, 42);
            this.lblStatClosed.Name = "lblStatClosed";
            this.lblStatClosed.Size = new System.Drawing.Size(38, 45);
            this.lblStatClosed.TabIndex = 1;
            this.lblStatClosed.Text = "0";
            // 
            // lblTitleClosed
            // 
            this.lblTitleClosed.AutoSize = true;
            this.lblTitleClosed.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleClosed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTitleClosed.Location = new System.Drawing.Point(14, 16);
            this.lblTitleClosed.Name = "lblTitleClosed";
            this.lblTitleClosed.Size = new System.Drawing.Size(109, 15);
            this.lblTitleClosed.TabIndex = 0;
            this.lblTitleClosed.Text = "ĐÃ ĐÓNG / NGỪNG";
            // 
            // cardDraft
            // 
            this.cardDraft.BackColor = System.Drawing.Color.White;
            this.cardDraft.Controls.Add(this.lblStatDraft);
            this.cardDraft.Controls.Add(this.lblTitleDraft);
            this.cardDraft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDraft.Location = new System.Drawing.Point(466, 8);
            this.cardDraft.Margin = new System.Windows.Forms.Padding(8);
            this.cardDraft.Name = "cardDraft";
            this.cardDraft.Padding = new System.Windows.Forms.Padding(16);
            this.cardDraft.Size = new System.Drawing.Size(213, 114);
            this.cardDraft.TabIndex = 2;
            // 
            // lblStatDraft
            // 
            this.lblStatDraft.AutoSize = true;
            this.lblStatDraft.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStatDraft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblStatDraft.Location = new System.Drawing.Point(12, 42);
            this.lblStatDraft.Name = "lblStatDraft";
            this.lblStatDraft.Size = new System.Drawing.Size(38, 45);
            this.lblStatDraft.TabIndex = 1;
            this.lblStatDraft.Text = "0";
            // 
            // lblTitleDraft
            // 
            this.lblTitleDraft.AutoSize = true;
            this.lblTitleDraft.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleDraft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTitleDraft.Location = new System.Drawing.Point(14, 16);
            this.lblTitleDraft.Name = "lblTitleDraft";
            this.lblTitleDraft.Size = new System.Drawing.Size(65, 15);
            this.lblTitleDraft.TabIndex = 0;
            this.lblTitleDraft.Text = "BẢN NHÁP";
            // 
            // cardActive
            // 
            this.cardActive.BackColor = System.Drawing.Color.White;
            this.cardActive.Controls.Add(this.lblStatActive);
            this.cardActive.Controls.Add(this.lblTitleActive);
            this.cardActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardActive.Location = new System.Drawing.Point(237, 8);
            this.cardActive.Margin = new System.Windows.Forms.Padding(8);
            this.cardActive.Name = "cardActive";
            this.cardActive.Padding = new System.Windows.Forms.Padding(16);
            this.cardActive.Size = new System.Drawing.Size(213, 114);
            this.cardActive.TabIndex = 1;
            // 
            // lblStatActive
            // 
            this.lblStatActive.AutoSize = true;
            this.lblStatActive.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStatActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblStatActive.Location = new System.Drawing.Point(12, 42);
            this.lblStatActive.Name = "lblStatActive";
            this.lblStatActive.Size = new System.Drawing.Size(38, 45);
            this.lblStatActive.TabIndex = 1;
            this.lblStatActive.Text = "0";
            // 
            // lblTitleActive
            // 
            this.lblTitleActive.AutoSize = true;
            this.lblTitleActive.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTitleActive.Location = new System.Drawing.Point(14, 16);
            this.lblTitleActive.Name = "lblTitleActive";
            this.lblTitleActive.Size = new System.Drawing.Size(109, 15);
            this.lblTitleActive.TabIndex = 0;
            this.lblTitleActive.Text = "ĐANG HOẠT ĐỘNG";
            // 
            // cardTotal
            // 
            this.cardTotal.BackColor = System.Drawing.Color.White;
            this.cardTotal.Controls.Add(this.lblStatTotal);
            this.cardTotal.Controls.Add(this.lblTitleTotal);
            this.cardTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotal.Location = new System.Drawing.Point(8, 8);
            this.cardTotal.Margin = new System.Windows.Forms.Padding(8);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Padding = new System.Windows.Forms.Padding(16);
            this.cardTotal.Size = new System.Drawing.Size(213, 114);
            this.cardTotal.TabIndex = 0;
            // 
            // lblStatTotal
            // 
            this.lblStatTotal.AutoSize = true;
            this.lblStatTotal.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStatTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblStatTotal.Location = new System.Drawing.Point(12, 42);
            this.lblStatTotal.Name = "lblStatTotal";
            this.lblStatTotal.Size = new System.Drawing.Size(38, 45);
            this.lblStatTotal.TabIndex = 1;
            this.lblStatTotal.Text = "0";
            // 
            // lblTitleTotal
            // 
            this.lblTitleTotal.AutoSize = true;
            this.lblTitleTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTitleTotal.Location = new System.Drawing.Point(14, 16);
            this.lblTitleTotal.Name = "lblTitleTotal";
            this.lblTitleTotal.Size = new System.Drawing.Size(92, 15);
            this.lblTitleTotal.TabIndex = 0;
            this.lblTitleTotal.Text = "TỔNG SỐ HỒ SƠ";
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.TimerClock_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopBar);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Hồ sơ - InternalInmate";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlBrand.ResumeLayout(false);
            this.pnlBrand.PerformLayout();
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlDashboardContent.ResumeLayout(false);
            this.grpQuickActions.ResumeLayout(false);
            this.tblKpi.ResumeLayout(false);
            this.cardClosed.ResumeLayout(false);
            this.cardClosed.PerformLayout();
            this.cardDraft.ResumeLayout(false);
            this.cardDraft.PerformLayout();
            this.cardActive.ResumeLayout(false);
            this.cardActive.PerformLayout();
            this.cardTotal.ResumeLayout(false);
            this.cardTotal.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblBrandSub;
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Button btnNavProfiles;
        private System.Windows.Forms.Button btnNavNewProfile;
        private System.Windows.Forms.Button btnNavBackup;
        private System.Windows.Forms.Button btnNavAbout;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblDbStatus;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlDashboardContent;
        private System.Windows.Forms.TableLayoutPanel tblKpi;
        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Label lblTitleTotal;
        private System.Windows.Forms.Label lblStatTotal;
        private System.Windows.Forms.Panel cardActive;
        private System.Windows.Forms.Label lblStatActive;
        private System.Windows.Forms.Label lblTitleActive;
        private System.Windows.Forms.Panel cardDraft;
        private System.Windows.Forms.Label lblStatDraft;
        private System.Windows.Forms.Label lblTitleDraft;
        private System.Windows.Forms.Panel cardClosed;
        private System.Windows.Forms.Label lblStatClosed;
        private System.Windows.Forms.Label lblTitleClosed;
        private System.Windows.Forms.GroupBox grpQuickActions;
        private System.Windows.Forms.Button btnQuickAdd;
        private System.Windows.Forms.Button btnQuickList;
        private System.Windows.Forms.Button btnQuickBackup;
        private System.Windows.Forms.Timer timerClock;
    }
}
