namespace InternalInmate.Forms
{
    partial class FrmBackupRestore
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTopSettings = new System.Windows.Forms.Panel();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.txtBackupFolder = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.lstBackups = new System.Windows.Forms.ListBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSelectedBackup = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnBackupNow = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlTopSettings.SuspendLayout();
            this.grpList.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlHeader.Size = new System.Drawing.Size(784, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSub.Location = new System.Drawing.Point(14, 34);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(434, 15);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Sao lưu và phục hồi thống nhất: CSDL SQL Server và toàn bộ thư mục tài liệu đính " +
    "kèm";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SAO LƯU VÀ PHỤC HỒI DỮ LIỆU";
            // 
            // pnlTopSettings
            // 
            this.pnlTopSettings.BackColor = System.Drawing.Color.White;
            this.pnlTopSettings.Controls.Add(this.btnOpenFolder);
            this.pnlTopSettings.Controls.Add(this.btnBrowseFolder);
            this.pnlTopSettings.Controls.Add(this.txtBackupFolder);
            this.pnlTopSettings.Controls.Add(this.lblFolder);
            this.pnlTopSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopSettings.Location = new System.Drawing.Point(0, 60);
            this.pnlTopSettings.Name = "pnlTopSettings";
            this.pnlTopSettings.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlTopSettings.Size = new System.Drawing.Size(784, 52);
            this.pnlTopSettings.TabIndex = 1;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnOpenFolder.Location = new System.Drawing.Point(684, 11);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(85, 27);
            this.btnOpenFolder.TabIndex = 3;
            this.btnOpenFolder.Text = "Mở thư mục";
            this.btnOpenFolder.UseVisualStyleBackColor = false;
            this.btnOpenFolder.Click += new System.EventHandler(this.BtnOpenFolder_Click);
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnBrowseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFolder.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnBrowseFolder.Location = new System.Drawing.Point(595, 11);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(83, 27);
            this.btnBrowseFolder.TabIndex = 2;
            this.btnBrowseFolder.Text = "Chọn...";
            this.btnBrowseFolder.UseVisualStyleBackColor = false;
            this.btnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // txtBackupFolder
            // 
            this.txtBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackupFolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBackupFolder.Location = new System.Drawing.Point(140, 13);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.ReadOnly = true;
            this.txtBackupFolder.Size = new System.Drawing.Size(449, 23);
            this.txtBackupFolder.TabIndex = 1;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFolder.Location = new System.Drawing.Point(14, 16);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(111, 15);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "Thư mục sao lưu:";
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.lstBackups);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.grpList.Location = new System.Drawing.Point(0, 112);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(12);
            this.grpList.Size = new System.Drawing.Size(784, 274);
            this.grpList.TabIndex = 2;
            this.grpList.TabStop = false;
            this.grpList.Text = "Danh sách bản sao lưu đã tạo";
            // 
            // lstBackups
            // 
            this.lstBackups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBackups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBackups.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lstBackups.FormattingEnabled = true;
            this.lstBackups.ItemHeight = 17;
            this.lstBackups.Location = new System.Drawing.Point(12, 28);
            this.lstBackups.Name = "lstBackups";
            this.lstBackups.Size = new System.Drawing.Size(760, 234);
            this.lstBackups.TabIndex = 0;
            this.lstBackups.SelectedIndexChanged += new System.EventHandler(this.LstBackups_SelectedIndexChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.lblStatus);
            this.pnlBottom.Controls.Add(this.lblSelectedBackup);
            this.pnlBottom.Controls.Add(this.btnRestore);
            this.pnlBottom.Controls.Add(this.btnBackupNow);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 386);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlBottom.Size = new System.Drawing.Size(784, 75);
            this.pnlBottom.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatus.Location = new System.Drawing.Point(14, 45);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(54, 15);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Sẵn sàng.";
            // 
            // lblSelectedBackup
            // 
            this.lblSelectedBackup.AutoSize = true;
            this.lblSelectedBackup.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSelectedBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSelectedBackup.Location = new System.Drawing.Point(14, 15);
            this.lblSelectedBackup.Name = "lblSelectedBackup";
            this.lblSelectedBackup.Size = new System.Drawing.Size(127, 15);
            this.lblSelectedBackup.TabIndex = 2;
            this.lblSelectedBackup.Text = "Chưa chọn bản sao lưu.";
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnRestore.Enabled = false;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(584, 18);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(185, 36);
            this.btnRestore.TabIndex = 1;
            this.btnRestore.Text = "Phục hồi bản sao lưu này";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // btnBackupNow
            // 
            this.btnBackupNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupNow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnBackupNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupNow.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnBackupNow.ForeColor = System.Drawing.Color.White;
            this.btnBackupNow.Location = new System.Drawing.Point(404, 18);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(170, 36);
            this.btnBackupNow.TabIndex = 0;
            this.btnBackupNow.Text = "+ Tạo bản sao lưu mới";
            this.btnBackupNow.UseVisualStyleBackColor = false;
            this.btnBackupNow.Click += new System.EventHandler(this.BtnBackupNow_Click);
            // 
            // FrmBackupRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTopSettings);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(650, 400);
            this.Name = "FrmBackupRestore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sao lưu & Phục hồi dữ liệu";
            this.Load += new System.EventHandler(this.FrmBackupRestore_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTopSettings.ResumeLayout(false);
            this.pnlTopSettings.PerformLayout();
            this.grpList.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Panel pnlTopSettings;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtBackupFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.ListBox lstBackups;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnBackupNow;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label lblSelectedBackup;
        private System.Windows.Forms.Label lblStatus;
    }
}
