namespace SFPOSWindows.MasterForm
{
    partial class FrmScaleSync
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSync = new MetroFramework.Controls.MetroButton();
            this.chkBoxList = new System.Windows.Forms.CheckedListBox();
            this.picLoader = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.picLoader);
            this.groupBox1.Controls.Add(this.btnSync);
            this.groupBox1.Controls.Add(this.chkBoxList);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(15, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 333);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // btnSync
            // 
            this.btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSync.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSync.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnSync.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSync.Location = new System.Drawing.Point(225, 267);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(81, 27);
            this.btnSync.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSync.TabIndex = 17;
            this.btnSync.Text = "SYNC";
            this.btnSync.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSync.UseCustomForeColor = true;
            this.btnSync.UseSelectable = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // chkBoxList
            // 
            this.chkBoxList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBoxList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBoxList.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkBoxList.FormattingEnabled = true;
            this.chkBoxList.Items.AddRange(new object[] {
            "Products",
            "Unit Of Measures",
            "Department"});
            this.chkBoxList.Location = new System.Drawing.Point(19, 24);
            this.chkBoxList.Name = "chkBoxList";
            this.chkBoxList.Size = new System.Drawing.Size(287, 224);
            this.chkBoxList.TabIndex = 15;
            this.chkBoxList.ThreeDCheckBoxes = true;
            // 
            // picLoader
            // 
            this.picLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.picLoader.BackColor = System.Drawing.Color.Transparent;
            this.picLoader.Image = global::SFPOSWindows.Properties.Resources.Loader;
            this.picLoader.Location = new System.Drawing.Point(71, 51);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(190, 174);
            this.picLoader.TabIndex = 26;
            this.picLoader.TabStop = false;
            this.picLoader.Visible = false;
            // 
            // FrmScaleSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 410);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmScaleSync";
            this.Text = "Scale Sync";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton btnSync;
        private System.Windows.Forms.CheckedListBox chkBoxList;
        private System.Windows.Forms.PictureBox picLoader;
    }
}