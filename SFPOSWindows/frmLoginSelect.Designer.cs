namespace SFPOSWindows
{
    partial class frmLoginSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoginSelect));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBackOfc = new System.Windows.Forms.Button();
            this.btnOrderScan = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(450, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "BACK OFFICE";
            this.label1.Click += new System.EventHandler(this.btnBackOfc_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(119, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "ORDER SCANNING";
            this.label2.Click += new System.EventHandler(this.btnOrderScan_Click);
            // 
            // btnBackOfc
            // 
            this.btnBackOfc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBackOfc.BackgroundImage = global::SFPOSWindows.Properties.Resources.ADMIN1;
            this.btnBackOfc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBackOfc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackOfc.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBackOfc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBackOfc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBackOfc.Location = new System.Drawing.Point(367, 103);
            this.btnBackOfc.Name = "btnBackOfc";
            this.btnBackOfc.Size = new System.Drawing.Size(260, 183);
            this.btnBackOfc.TabIndex = 3;
            this.btnBackOfc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBackOfc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBackOfc.UseVisualStyleBackColor = false;
            this.btnBackOfc.Click += new System.EventHandler(this.btnBackOfc_Click);
            // 
            // btnOrderScan
            // 
            this.btnOrderScan.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOrderScan.BackgroundImage = global::SFPOSWindows.Properties.Resources.operator1;
            this.btnOrderScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOrderScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrderScan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOrderScan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnOrderScan.Location = new System.Drawing.Point(53, 103);
            this.btnOrderScan.Name = "btnOrderScan";
            this.btnOrderScan.Size = new System.Drawing.Size(260, 183);
            this.btnOrderScan.TabIndex = 2;
            this.btnOrderScan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOrderScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOrderScan.UseVisualStyleBackColor = false;
            this.btnOrderScan.Click += new System.EventHandler(this.btnOrderScan_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBackup.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBackup.Location = new System.Drawing.Point(468, 27);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(103, 39);
            this.btnBackup.TabIndex = 4;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBrowse.Location = new System.Drawing.Point(343, 27);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(103, 39);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(234, 22);
            this.textBox1.TabIndex = 6;
            // 
            // frmLoginSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 361);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBackOfc);
            this.Controls.Add(this.btnOrderScan);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoginSelect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBackOfc;
        private System.Windows.Forms.Button btnOrderScan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textBox1;
    }
}