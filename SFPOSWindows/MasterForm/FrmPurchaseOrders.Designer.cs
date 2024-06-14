namespace SFPOSWindows.MasterForm
{
    partial class FrmPurchaseOrders
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPurchaseOrders));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnShowAll = new MetroFramework.Controls.MetroButton();
            this.btnADD = new MetroFramework.Controls.MetroButton();
            this.txtSearchVendorName = new MetroFramework.Controls.MetroTextBox();
            this.PurchaseOrderGrdView = new MetroFramework.Controls.MetroGrid();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseOrderGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 44);
            this.panel1.TabIndex = 15;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(172, 32);
            this.lblSubHeading.TabIndex = 1;
            this.lblSubHeading.Text = "Purchase Order";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.btnShowAll);
            this.pnlSearch.Controls.Add(this.btnADD);
            this.pnlSearch.Controls.Add(this.txtSearchVendorName);
            this.pnlSearch.Location = new System.Drawing.Point(5, 51);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(808, 47);
            this.pnlSearch.TabIndex = 16;
            // 
            // btnShowAll
            // 
            this.btnShowAll.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnShowAll.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnShowAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowAll.Location = new System.Drawing.Point(301, 9);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(94, 27);
            this.btnShowAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnShowAll.TabIndex = 16;
            this.btnShowAll.Text = "SHOW ALL";
            this.btnShowAll.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnShowAll.UseCustomForeColor = true;
            this.btnShowAll.UseSelectable = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnADD
            // 
            this.btnADD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnADD.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnADD.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnADD.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnADD.Location = new System.Drawing.Point(715, 8);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(83, 27);
            this.btnADD.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnADD.TabIndex = 15;
            this.btnADD.Text = "ADD";
            this.btnADD.UseCustomForeColor = true;
            this.btnADD.UseSelectable = true;
            this.btnADD.Click += new System.EventHandler(this.btnADD_Click);
            // 
            // txtSearchVendorName
            // 
            // 
            // 
            // 
            this.txtSearchVendorName.CustomButton.Image = null;
            this.txtSearchVendorName.CustomButton.Location = new System.Drawing.Point(258, 2);
            this.txtSearchVendorName.CustomButton.Name = "";
            this.txtSearchVendorName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchVendorName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchVendorName.CustomButton.TabIndex = 1;
            this.txtSearchVendorName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchVendorName.CustomButton.UseSelectable = true;
            this.txtSearchVendorName.CustomButton.Visible = false;
            this.txtSearchVendorName.DisplayIcon = true;
            this.txtSearchVendorName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearchVendorName.IconRight = true;
            this.txtSearchVendorName.Lines = new string[0];
            this.txtSearchVendorName.Location = new System.Drawing.Point(12, 9);
            this.txtSearchVendorName.MaxLength = 20;
            this.txtSearchVendorName.Name = "txtSearchVendorName";
            this.txtSearchVendorName.PasswordChar = '\0';
            //this.txtSearchVendorName.PromptText = "Vendor Name/PO Number";
            this.txtSearchVendorName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchVendorName.SelectedText = "";
            this.txtSearchVendorName.SelectionLength = 0;
            this.txtSearchVendorName.SelectionStart = 0;
            this.txtSearchVendorName.ShortcutsEnabled = true;
            this.txtSearchVendorName.Size = new System.Drawing.Size(282, 26);
            this.txtSearchVendorName.TabIndex = 14;
            this.txtSearchVendorName.UseSelectable = true;
            this.txtSearchVendorName.WaterMark = "Vendor Name/PO Number";
            this.txtSearchVendorName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchVendorName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchVendorName.TextChanged += new System.EventHandler(this.txtSearchVendorName_TextChanged);
            // 
            // PurchaseOrderGrdView
            // 
            this.PurchaseOrderGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.PurchaseOrderGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PurchaseOrderGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PurchaseOrderGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PurchaseOrderGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.PurchaseOrderGrdView.BackgroundColor = System.Drawing.Color.White;
            this.PurchaseOrderGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PurchaseOrderGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.PurchaseOrderGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PurchaseOrderGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PurchaseOrderGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PurchaseOrderGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.PurchaseOrderGrdView.EnableHeadersVisualStyles = false;
            this.PurchaseOrderGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PurchaseOrderGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.PurchaseOrderGrdView.Location = new System.Drawing.Point(17, 104);
            this.PurchaseOrderGrdView.Name = "PurchaseOrderGrdView";
            this.PurchaseOrderGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PurchaseOrderGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.PurchaseOrderGrdView.RowHeadersVisible = false;
            this.PurchaseOrderGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PurchaseOrderGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PurchaseOrderGrdView.Size = new System.Drawing.Size(786, 276);
            this.PurchaseOrderGrdView.TabIndex = 14;
            this.PurchaseOrderGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PurchaseOrderGrdView_CellClick);
            this.PurchaseOrderGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PurchaseOrderGrdView_CellContentClick);
            // 
            // FrmPurchaseOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(820, 392);
            this.Controls.Add(this.PurchaseOrderGrdView);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPurchaseOrders";
            this.Text = "Purchase Order";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseOrderGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel pnlSearch;
        private MetroFramework.Controls.MetroButton btnShowAll;
        private MetroFramework.Controls.MetroButton btnADD;
        private MetroFramework.Controls.MetroTextBox txtSearchVendorName;
        public MetroFramework.Controls.MetroGrid PurchaseOrderGrdView;
    }
}