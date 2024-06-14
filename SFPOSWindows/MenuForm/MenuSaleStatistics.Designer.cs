namespace SFPOSWindows.MenuForm
{
    partial class MenuSaleStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuSaleStatistics));
            this.btnProductSales = new System.Windows.Forms.Button();
            this.btnStore = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProductMovement = new System.Windows.Forms.Button();
            this.PanelGrid = new System.Windows.Forms.Panel();
            this.btnTillStatus = new System.Windows.Forms.Button();
            this.PictureWatermark = new System.Windows.Forms.PictureBox();
            this.PanelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProductSales
            // 
            this.btnProductSales.BackColor = System.Drawing.Color.White;
            this.btnProductSales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductSales.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProductSales.FlatAppearance.BorderSize = 0;
            this.btnProductSales.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnProductSales.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnProductSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductSales.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductSales.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnProductSales.Location = new System.Drawing.Point(15, 127);
            this.btnProductSales.Name = "btnProductSales";
            this.btnProductSales.Size = new System.Drawing.Size(149, 39);
            this.btnProductSales.TabIndex = 23;
            this.btnProductSales.Text = "Product Sales";
            this.btnProductSales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductSales.UseVisualStyleBackColor = false;
            this.btnProductSales.Click += new System.EventHandler(this.btnProductSales_Click);
            // 
            // btnStore
            // 
            this.btnStore.BackColor = System.Drawing.Color.White;
            this.btnStore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStore.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnStore.FlatAppearance.BorderSize = 0;
            this.btnStore.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnStore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStore.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStore.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnStore.Location = new System.Drawing.Point(15, 82);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(112, 39);
            this.btnStore.TabIndex = 22;
            this.btnStore.Text = "Store sales";
            this.btnStore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStore.UseVisualStyleBackColor = false;
            this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 25);
            this.label1.TabIndex = 24;
            this.label1.Text = "Sales Statistics";
            // 
            // btnProductMovement
            // 
            this.btnProductMovement.BackColor = System.Drawing.Color.White;
            this.btnProductMovement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductMovement.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProductMovement.FlatAppearance.BorderSize = 0;
            this.btnProductMovement.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnProductMovement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnProductMovement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductMovement.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductMovement.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnProductMovement.Location = new System.Drawing.Point(15, 172);
            this.btnProductMovement.Name = "btnProductMovement";
            this.btnProductMovement.Size = new System.Drawing.Size(193, 39);
            this.btnProductMovement.TabIndex = 25;
            this.btnProductMovement.Text = "Product Movement";
            this.btnProductMovement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductMovement.UseVisualStyleBackColor = false;
            this.btnProductMovement.Click += new System.EventHandler(this.btnProductMovement_Click);
            // 
            // PanelGrid
            // 
            this.PanelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelGrid.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PanelGrid.Controls.Add(this.btnTillStatus);
            this.PanelGrid.Controls.Add(this.btnStore);
            this.PanelGrid.Controls.Add(this.btnProductSales);
            this.PanelGrid.Controls.Add(this.btnProductMovement);
            this.PanelGrid.Controls.Add(this.label1);
            this.PanelGrid.Controls.Add(this.PictureWatermark);
            this.PanelGrid.Location = new System.Drawing.Point(-3, 0);
            this.PanelGrid.Name = "PanelGrid";
            this.PanelGrid.Size = new System.Drawing.Size(886, 733);
            this.PanelGrid.TabIndex = 26;
            // 
            // btnTillStatus
            // 
            this.btnTillStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnTillStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTillStatus.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTillStatus.FlatAppearance.BorderSize = 0;
            this.btnTillStatus.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnTillStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnTillStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTillStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTillStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnTillStatus.Location = new System.Drawing.Point(15, 217);
            this.btnTillStatus.Name = "btnTillStatus";
            this.btnTillStatus.Size = new System.Drawing.Size(215, 39);
            this.btnTillStatus.TabIndex = 27;
            this.btnTillStatus.Text = "Till Statistics and Balancing";
            this.btnTillStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTillStatus.UseVisualStyleBackColor = false;
            this.btnTillStatus.Click += new System.EventHandler(this.btnTillStatus_Click);
            // 
            // PictureWatermark
            // 
            this.PictureWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureWatermark.BackColor = System.Drawing.Color.White;
            this.PictureWatermark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PictureWatermark.Image = global::SFPOSWindows.Properties.Resources.intPOSDemo_lightX;
            this.PictureWatermark.Location = new System.Drawing.Point(3, 0);
            this.PictureWatermark.Name = "PictureWatermark";
            this.PictureWatermark.Size = new System.Drawing.Size(883, 731);
            this.PictureWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureWatermark.TabIndex = 26;
            this.PictureWatermark.TabStop = false;
            // 
            // MenuSaleStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.PanelGrid);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuSaleStatistics";
            this.Text = "MenuSaleStatistics";
            this.PanelGrid.ResumeLayout(false);
            this.PanelGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnProductSales;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProductMovement;
        private System.Windows.Forms.Panel PanelGrid;
        private System.Windows.Forms.PictureBox PictureWatermark;
        private System.Windows.Forms.Button btnTillStatus;
    }
}