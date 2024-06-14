namespace SFPOSWindows.MasterForm
{
    partial class FrmSyncData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSyncData));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSync = new MetroFramework.Controls.MetroButton();
            this.btnLastSyncStatus = new MetroFramework.Controls.MetroButton();
            this.btnScaleSync = new MetroFramework.Controls.MetroButton();
            this.btnUpdateStatus = new MetroFramework.Controls.MetroButton();
            this.btnCheckUpdate = new MetroFramework.Controls.MetroButton();
            this.label2 = new System.Windows.Forms.Label();
            this.PictureWatermark = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 25);
            this.label1.TabIndex = 23;
            this.label1.Text = "Syncronization";
            // 
            // btnSync
            // 
            this.btnSync.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSync.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnSync.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnSync.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSync.Highlight = true;
            this.btnSync.Location = new System.Drawing.Point(22, 87);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(152, 48);
            this.btnSync.TabIndex = 24;
            this.btnSync.Text = "Sync Nodes";
            this.btnSync.UseCustomBackColor = true;
            this.btnSync.UseSelectable = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnLastSyncStatus
            // 
            this.btnLastSyncStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLastSyncStatus.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnLastSyncStatus.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnLastSyncStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnLastSyncStatus.Highlight = true;
            this.btnLastSyncStatus.Location = new System.Drawing.Point(22, 155);
            this.btnLastSyncStatus.Name = "btnLastSyncStatus";
            this.btnLastSyncStatus.Size = new System.Drawing.Size(152, 48);
            this.btnLastSyncStatus.TabIndex = 26;
            this.btnLastSyncStatus.Text = "Last Sync Status";
            this.btnLastSyncStatus.UseCustomBackColor = true;
            this.btnLastSyncStatus.UseSelectable = true;
            this.btnLastSyncStatus.Click += new System.EventHandler(this.btnLastSyncStatus_Click);
            // 
            // btnScaleSync
            // 
            this.btnScaleSync.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnScaleSync.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnScaleSync.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnScaleSync.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnScaleSync.Highlight = true;
            this.btnScaleSync.Location = new System.Drawing.Point(22, 228);
            this.btnScaleSync.Name = "btnScaleSync";
            this.btnScaleSync.Size = new System.Drawing.Size(152, 48);
            this.btnScaleSync.TabIndex = 27;
            this.btnScaleSync.Text = "Scale Sync";
            this.btnScaleSync.UseCustomBackColor = true;
            this.btnScaleSync.UseSelectable = true;
            this.btnScaleSync.Click += new System.EventHandler(this.btnScaleSync_Click);
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnUpdateStatus.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnUpdateStatus.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnUpdateStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnUpdateStatus.Highlight = true;
            this.btnUpdateStatus.Location = new System.Drawing.Point(320, 155);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(152, 48);
            this.btnUpdateStatus.TabIndex = 30;
            this.btnUpdateStatus.Text = "Last Update Status";
            this.btnUpdateStatus.UseCustomBackColor = true;
            this.btnUpdateStatus.UseSelectable = true;
            this.btnUpdateStatus.Visible = false;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCheckUpdate.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnCheckUpdate.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnCheckUpdate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCheckUpdate.Highlight = true;
            this.btnCheckUpdate.Location = new System.Drawing.Point(320, 87);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(152, 48);
            this.btnCheckUpdate.TabIndex = 29;
            this.btnCheckUpdate.Text = "Check Update";
            this.btnCheckUpdate.UseCustomBackColor = true;
            this.btnCheckUpdate.UseSelectable = true;
            this.btnCheckUpdate.Visible = false;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(326, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "System Update";
            this.label2.Visible = false;
            // 
            // PictureWatermark
            // 
            this.PictureWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureWatermark.BackColor = System.Drawing.Color.White;
            this.PictureWatermark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureWatermark.Image = global::SFPOSWindows.Properties.Resources.intPOSDemo_lightX;
            this.PictureWatermark.Location = new System.Drawing.Point(0, 0);
            this.PictureWatermark.Name = "PictureWatermark";
            this.PictureWatermark.Size = new System.Drawing.Size(880, 732);
            this.PictureWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureWatermark.TabIndex = 21;
            this.PictureWatermark.TabStop = false;
            // 
            // FrmSyncData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(881, 733);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.btnCheckUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnScaleSync);
            this.Controls.Add(this.btnLastSyncStatus);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PictureWatermark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSyncData";
            this.Text = "FrmSyncData";
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureWatermark;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroButton btnSync;
        private MetroFramework.Controls.MetroButton btnLastSyncStatus;
        private MetroFramework.Controls.MetroButton btnScaleSync;
        private MetroFramework.Controls.MetroButton btnUpdateStatus;
        private MetroFramework.Controls.MetroButton btnCheckUpdate;
        private System.Windows.Forms.Label label2;
    }
}