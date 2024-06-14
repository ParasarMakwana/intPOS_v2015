namespace SFPOSWindows.MenuForm
{
    partial class frmMenuTax
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuTax));
            this.btnTaxGroup = new System.Windows.Forms.Button();
            this.PanelGrid = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PictureWatermark = new System.Windows.Forms.PictureBox();
            this.PanelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTaxGroup
            // 
            this.btnTaxGroup.BackColor = System.Drawing.Color.White;
            this.btnTaxGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaxGroup.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTaxGroup.FlatAppearance.BorderSize = 0;
            this.btnTaxGroup.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnTaxGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnTaxGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaxGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaxGroup.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnTaxGroup.Location = new System.Drawing.Point(17, 82);
            this.btnTaxGroup.Name = "btnTaxGroup";
            this.btnTaxGroup.Size = new System.Drawing.Size(116, 39);
            this.btnTaxGroup.TabIndex = 22;
            this.btnTaxGroup.Text = "Tax Group";
            this.btnTaxGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaxGroup.UseVisualStyleBackColor = false;
            this.btnTaxGroup.Click += new System.EventHandler(this.btnTaxGroup_Click);
            // 
            // PanelGrid
            // 
            this.PanelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelGrid.BackColor = System.Drawing.Color.White;
            this.PanelGrid.Controls.Add(this.button2);
            this.PanelGrid.Controls.Add(this.label1);
            this.PanelGrid.Controls.Add(this.btnTaxGroup);
            this.PanelGrid.Controls.Add(this.PictureWatermark);
            this.PanelGrid.Location = new System.Drawing.Point(0, 0);
            this.PanelGrid.Name = "PanelGrid";
            this.PanelGrid.Size = new System.Drawing.Size(880, 731);
            this.PanelGrid.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.Location = new System.Drawing.Point(17, 128);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 39);
            this.button2.TabIndex = 23;
            this.button2.Text = "Tax Detail";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnTaxDetail_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 25);
            this.label1.TabIndex = 21;
            this.label1.Text = "Tax Management";
            // 
            // PictureWatermark
            // 
            this.PictureWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureWatermark.BackColor = System.Drawing.Color.White;
            this.PictureWatermark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PictureWatermark.Image = global::SFPOSWindows.Properties.Resources.intPOSDemo_lightX;
            this.PictureWatermark.Location = new System.Drawing.Point(0, 0);
            this.PictureWatermark.Name = "PictureWatermark";
            this.PictureWatermark.Size = new System.Drawing.Size(880, 728);
            this.PictureWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureWatermark.TabIndex = 22;
            this.PictureWatermark.TabStop = false;
            // 
            // frmMenuTax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.PanelGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuTax";
            this.Text = "MenuTax";
            this.PanelGrid.ResumeLayout(false);
            this.PanelGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTaxGroup;
        private System.Windows.Forms.PictureBox PictureWatermark;
        private System.Windows.Forms.Button button2;
    }
}