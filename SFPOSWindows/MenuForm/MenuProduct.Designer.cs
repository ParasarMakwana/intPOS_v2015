namespace SFPOSWindows.MenuForm
{
    partial class frmMenuProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuProduct));
            this.PanelGrid = new System.Windows.Forms.Panel();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnUOM = new System.Windows.Forms.Button();
            this.btnCategory = new System.Windows.Forms.Button();
            this.titleProduct = new System.Windows.Forms.Label();
            this.PictureWatermark = new System.Windows.Forms.PictureBox();
            this.PanelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelGrid
            // 
            this.PanelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelGrid.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PanelGrid.Controls.Add(this.btnProduct);
            this.PanelGrid.Controls.Add(this.btnUOM);
            this.PanelGrid.Controls.Add(this.btnCategory);
            this.PanelGrid.Controls.Add(this.titleProduct);
            this.PanelGrid.Controls.Add(this.PictureWatermark);
            this.PanelGrid.Location = new System.Drawing.Point(0, 0);
            this.PanelGrid.Name = "PanelGrid";
            this.PanelGrid.Size = new System.Drawing.Size(883, 731);
            this.PanelGrid.TabIndex = 11;
            // 
            // btnProduct
            // 
            this.btnProduct.BackColor = System.Drawing.Color.White;
            this.btnProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProduct.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProduct.FlatAppearance.BorderSize = 0;
            this.btnProduct.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduct.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnProduct.Location = new System.Drawing.Point(12, 175);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(147, 39);
            this.btnProduct.TabIndex = 16;
            this.btnProduct.Text = "Products";
            this.btnProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduct.UseVisualStyleBackColor = false;
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // btnUOM
            // 
            this.btnUOM.BackColor = System.Drawing.Color.White;
            this.btnUOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUOM.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUOM.FlatAppearance.BorderSize = 0;
            this.btnUOM.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnUOM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnUOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUOM.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUOM.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnUOM.Location = new System.Drawing.Point(12, 129);
            this.btnUOM.Name = "btnUOM";
            this.btnUOM.Size = new System.Drawing.Size(117, 39);
            this.btnUOM.TabIndex = 18;
            this.btnUOM.Text = "UoM";
            this.btnUOM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUOM.UseVisualStyleBackColor = false;
            this.btnUOM.Click += new System.EventHandler(this.btnUOM_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.BackColor = System.Drawing.Color.White;
            this.btnCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCategory.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCategory.FlatAppearance.BorderSize = 0;
            this.btnCategory.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategory.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCategory.Location = new System.Drawing.Point(12, 82);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(156, 39);
            this.btnCategory.TabIndex = 17;
            this.btnCategory.Text = "Department";
            this.btnCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCategory.UseVisualStyleBackColor = false;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // titleProduct
            // 
            this.titleProduct.AutoSize = true;
            this.titleProduct.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.titleProduct.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.titleProduct.Location = new System.Drawing.Point(17, 25);
            this.titleProduct.Name = "titleProduct";
            this.titleProduct.Size = new System.Drawing.Size(207, 25);
            this.titleProduct.TabIndex = 12;
            this.titleProduct.Text = "Product Management";
            // 
            // PictureWatermark
            // 
            this.PictureWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureWatermark.BackColor = System.Drawing.Color.White;
            this.PictureWatermark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PictureWatermark.Image = global::SFPOSWindows.Properties.Resources.intPOSDemo_lightX;
            this.PictureWatermark.Location = new System.Drawing.Point(0, 0);
            this.PictureWatermark.Name = "PictureWatermark";
            this.PictureWatermark.Size = new System.Drawing.Size(883, 731);
            this.PictureWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureWatermark.TabIndex = 21;
            this.PictureWatermark.TabStop = false;
            // 
            // frmMenuProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.PanelGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuProduct";
            this.Text = "MenuProduct";
            this.PanelGrid.ResumeLayout(false);
            this.PanelGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelGrid;
        private System.Windows.Forms.Label titleProduct;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnUOM;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.PictureBox PictureWatermark;
    }
}