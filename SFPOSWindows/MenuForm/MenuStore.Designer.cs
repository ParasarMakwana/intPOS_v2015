namespace SFPOSWindows.MenuForm
{
    partial class frmMenuStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuStore));
            this.label1 = new System.Windows.Forms.Label();
            this.btnStore = new System.Windows.Forms.Button();
            this.btnEmployee = new System.Windows.Forms.Button();
            this.PanelGrid = new System.Windows.Forms.Panel();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.PictureWatermark = new System.Windows.Forms.PictureBox();
            this.PanelGrid.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(183, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "Store Management";
            // 
            // btnStore
            // 
            this.btnStore.BackColor = System.Drawing.Color.White;
            this.btnStore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStore.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnStore.FlatAppearance.BorderSize = 0;
            this.btnStore.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnStore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStore.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStore.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnStore.Location = new System.Drawing.Point(12, 82);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(112, 39);
            this.btnStore.TabIndex = 17;
            this.btnStore.Text = "Stores";
            this.btnStore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStore.UseVisualStyleBackColor = false;
            this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
            // 
            // btnEmployee
            // 
            this.btnEmployee.BackColor = System.Drawing.Color.White;
            this.btnEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmployee.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEmployee.FlatAppearance.BorderSize = 0;
            this.btnEmployee.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnEmployee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmployee.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmployee.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnEmployee.Location = new System.Drawing.Point(12, 127);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(149, 39);
            this.btnEmployee.TabIndex = 18;
            this.btnEmployee.Text = "Employees";
            this.btnEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmployee.UseVisualStyleBackColor = false;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // PanelGrid
            // 
            this.PanelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelGrid.BackColor = System.Drawing.Color.White;
            this.PanelGrid.Controls.Add(this.btnCustomer);
            this.PanelGrid.Controls.Add(this.btnEmployee);
            this.PanelGrid.Controls.Add(this.btnStore);
            this.PanelGrid.Controls.Add(this.label1);
            this.PanelGrid.Controls.Add(this.PictureWatermark);
            this.PanelGrid.Location = new System.Drawing.Point(0, 0);
            this.PanelGrid.Name = "PanelGrid";
            this.PanelGrid.Size = new System.Drawing.Size(884, 733);
            this.PanelGrid.TabIndex = 10;
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.Color.White;
            this.btnCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCustomer.FlatAppearance.BorderSize = 0;
            this.btnCustomer.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnCustomer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomer.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomer.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCustomer.Location = new System.Drawing.Point(12, 173);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(149, 39);
            this.btnCustomer.TabIndex = 21;
            this.btnCustomer.Text = "Customer";
            this.btnCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomer.UseVisualStyleBackColor = false;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // PictureWatermark
            // 
            this.PictureWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureWatermark.BackColor = System.Drawing.Color.White;
            this.PictureWatermark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PictureWatermark.Image = global::SFPOSWindows.Properties.Resources.intPOSDemo_lightX;
            this.PictureWatermark.Location = new System.Drawing.Point(-9, -3);
            this.PictureWatermark.Name = "PictureWatermark";
            this.PictureWatermark.Size = new System.Drawing.Size(881, 733);
            this.PictureWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureWatermark.TabIndex = 20;
            this.PictureWatermark.TabStop = false;
            // 
            // frmMenuStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.PanelGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuStore";
            this.Text = "Store Management";
            this.PanelGrid.ResumeLayout(false);
            this.PanelGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureWatermark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.Panel PanelGrid;
        private System.Windows.Forms.PictureBox PictureWatermark;
        private System.Windows.Forms.Button btnCustomer;
    }
}