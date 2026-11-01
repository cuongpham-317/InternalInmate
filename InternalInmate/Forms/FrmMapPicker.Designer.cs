namespace InternalInmate.Forms
{
    partial class FrmMapPicker
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnApplyCoordinates = new System.Windows.Forms.Button();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.lblLng = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.lblLat = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Controls.Add(this.lstResults);
            this.pnlTop.Controls.Add(this.btnSearch);
            this.pnlTop.Controls.Add(this.txtSearch);
            this.pnlTop.Controls.Add(this.lblSearch);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(12);
            this.pnlTop.Size = new System.Drawing.Size(884, 90);
            this.pnlTop.TabIndex = 0;
            // 
            // lstResults
            // 
            this.lstResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResults.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstResults.FormattingEnabled = true;
            this.lstResults.ItemHeight = 15;
            this.lstResults.Location = new System.Drawing.Point(120, 38);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(642, 49);
            this.lstResults.TabIndex = 3;
            this.lstResults.Visible = false;
            this.lstResults.SelectedIndexChanged += new System.EventHandler(this.LstResults_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(768, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(104, 27);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm địa chỉ";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(120, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(642, 24);
            this.txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Location = new System.Drawing.Point(12, 14);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(102, 15);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm bản đồ:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.lblStatus);
            this.pnlBottom.Controls.Add(this.btnApplyCoordinates);
            this.pnlBottom.Controls.Add(this.txtLongitude);
            this.pnlBottom.Controls.Add(this.lblLng);
            this.pnlBottom.Controls.Add(this.txtLatitude);
            this.pnlBottom.Controls.Add(this.lblLat);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnSelect);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 501);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(12);
            this.pnlBottom.Size = new System.Drawing.Size(884, 60);
            this.pnlBottom.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatus.Location = new System.Drawing.Point(12, 38);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(262, 15);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Nhấp chuột trên bản đồ để chọn tọa độ chính xác.";
            // 
            // btnApplyCoordinates
            // 
            this.btnApplyCoordinates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnApplyCoordinates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyCoordinates.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnApplyCoordinates.Location = new System.Drawing.Point(400, 10);
            this.btnApplyCoordinates.Name = "btnApplyCoordinates";
            this.btnApplyCoordinates.Size = new System.Drawing.Size(65, 25);
            this.btnApplyCoordinates.TabIndex = 6;
            this.btnApplyCoordinates.Text = "Định vị";
            this.btnApplyCoordinates.UseVisualStyleBackColor = false;
            this.btnApplyCoordinates.Click += new System.EventHandler(this.BtnApplyCoordinates_Click);
            // 
            // txtLongitude
            // 
            this.txtLongitude.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLongitude.Location = new System.Drawing.Point(265, 11);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(125, 23);
            this.txtLongitude.TabIndex = 5;
            // 
            // lblLng
            // 
            this.lblLng.AutoSize = true;
            this.lblLng.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblLng.Location = new System.Drawing.Point(205, 15);
            this.lblLng.Name = "lblLng";
            this.lblLng.Size = new System.Drawing.Size(54, 15);
            this.lblLng.TabIndex = 4;
            this.lblLng.Text = "Kinh độ:";
            // 
            // txtLatitude
            // 
            this.txtLatitude.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLatitude.Location = new System.Drawing.Point(65, 11);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(125, 23);
            this.txtLatitude.TabIndex = 3;
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblLat.Location = new System.Drawing.Point(12, 15);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(43, 15);
            this.lblLat.TabIndex = 2;
            this.lblLat.Text = "Vĩ độ:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(784, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelect.ForeColor = System.Drawing.Color.White;
            this.btnSelect.Location = new System.Drawing.Point(645, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(133, 32);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Chọn vị trí này";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 90);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(884, 411);
            this.webBrowser.TabIndex = 2;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
            // 
            // FrmMapPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmMapPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn vị trí trên bản đồ";
            this.Load += new System.EventHandler(this.FrmMapPicker_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.Label lblLng;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.Button btnApplyCoordinates;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStatus;
    }
}
