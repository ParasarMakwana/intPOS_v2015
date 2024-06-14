namespace SFPOSWindows.MasterForm
{
    partial class FrmLastSyncStatus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLastSyncStatus));
            this.LastSyncGrdView = new MetroFramework.Controls.MetroGrid();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblpre = new System.Windows.Forms.Label();
            this.btnRefresh = new MetroFramework.Controls.MetroButton();
            this.CounterIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TblName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSync = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SyncDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SyncStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.LastSyncGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // LastSyncGrdView
            // 
            this.LastSyncGrdView.AllowUserToAddRows = false;
            this.LastSyncGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.LastSyncGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.LastSyncGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LastSyncGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.LastSyncGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.LastSyncGrdView.BackgroundColor = System.Drawing.Color.White;
            this.LastSyncGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LastSyncGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.LastSyncGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.LastSyncGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.LastSyncGrdView.ColumnHeadersHeight = 30;
            this.LastSyncGrdView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CounterIP,
            this.TblName,
            this.UpdatedDate,
            this.IsSync,
            this.SyncDate,
            this.SyncStatus});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.LastSyncGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.LastSyncGrdView.EnableHeadersVisualStyles = false;
            this.LastSyncGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LastSyncGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.LastSyncGrdView.Location = new System.Drawing.Point(13, 163);
            this.LastSyncGrdView.Name = "LastSyncGrdView";
            this.LastSyncGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LastSyncGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.LastSyncGrdView.RowHeadersVisible = false;
            this.LastSyncGrdView.RowHeadersWidth = 30;
            this.LastSyncGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.LastSyncGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LastSyncGrdView.Size = new System.Drawing.Size(731, 285);
            this.LastSyncGrdView.TabIndex = 13;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar.Location = new System.Drawing.Point(13, 95);
            this.progressBar.MinimumSize = new System.Drawing.Size(50, 23);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(731, 23);
            this.progressBar.TabIndex = 14;
            this.progressBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressBar.Value = 100;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblpre
            // 
            this.lblpre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblpre.AutoSize = true;
            this.lblpre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpre.Location = new System.Drawing.Point(691, 70);
            this.lblpre.Name = "lblpre";
            this.lblpre.Size = new System.Drawing.Size(19, 20);
            this.lblpre.TabIndex = 15;
            this.lblpre.Text = "0";
            this.lblpre.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnRefresh.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRefresh.Highlight = true;
            this.btnRefresh.Location = new System.Drawing.Point(635, 126);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(109, 29);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseCustomBackColor = true;
            this.btnRefresh.UseSelectable = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // CounterIP
            // 
            this.CounterIP.DataPropertyName = "CounterIP";
            this.CounterIP.HeaderText = "Counter IP";
            this.CounterIP.Name = "CounterIP";
            this.CounterIP.ReadOnly = true;
            // 
            // TblName
            // 
            this.TblName.DataPropertyName = "TblName";
            this.TblName.HeaderText = "Tbl Name";
            this.TblName.Name = "TblName";
            this.TblName.ReadOnly = true;
            // 
            // UpdatedDate
            // 
            this.UpdatedDate.DataPropertyName = "UpdatedDate";
            this.UpdatedDate.HeaderText = "Updated Date";
            this.UpdatedDate.Name = "UpdatedDate";
            this.UpdatedDate.ReadOnly = true;
            // 
            // IsSync
            // 
            this.IsSync.DataPropertyName = "IsSync";
            this.IsSync.HeaderText = "IsSync";
            this.IsSync.Name = "IsSync";
            this.IsSync.ReadOnly = true;
            this.IsSync.Visible = false;
            // 
            // SyncDate
            // 
            this.SyncDate.DataPropertyName = "SyncDate";
            this.SyncDate.HeaderText = "Sync Date";
            this.SyncDate.Name = "SyncDate";
            this.SyncDate.ReadOnly = true;
            // 
            // SyncStatus
            // 
            this.SyncStatus.DataPropertyName = "SyncStatus";
            this.SyncStatus.HeaderText = "Sync Status";
            this.SyncStatus.Name = "SyncStatus";
            this.SyncStatus.ReadOnly = true;
            // 
            // FrmLastSyncStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 458);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblpre);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.LastSyncGrdView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLastSyncStatus";
            this.Text = "Last Sync Status";
            ((System.ComponentModel.ISupportInitialize)(this.LastSyncGrdView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroGrid LastSyncGrdView;
        private MetroFramework.Controls.MetroProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblpre;
        private MetroFramework.Controls.MetroButton btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TblName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdatedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSync;
        private System.Windows.Forms.DataGridViewTextBoxColumn SyncDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SyncStatus;
    }
}