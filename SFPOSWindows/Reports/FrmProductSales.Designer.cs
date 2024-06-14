namespace SFPOSWindows.MasterForm
{
    partial class FrmProductSales
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.endDate = new MetroFramework.Controls.MetroDateTime();
            this.startDate = new MetroFramework.Controls.MetroDateTime();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.txtSearch = new MetroFramework.Controls.MetroTextBox();
            this.cmbVendors = new MetroFramework.Controls.MetroComboBox();
            this.cmbSection = new MetroFramework.Controls.MetroComboBox();
            this.cmbDepartment = new MetroFramework.Controls.MetroComboBox();
            this.ProductSaleGrdView = new MetroFramework.Controls.MetroGrid();
            this.picLoader = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductSaleGrdView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(-2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 44);
            this.panel1.TabIndex = 3;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(8, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(150, 32);
            this.lblSubHeading.TabIndex = 0;
            this.lblSubHeading.Text = "Product Sales";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel1.Controls.Add(this.endDate);
            this.metroPanel1.Controls.Add(this.startDate);
            this.metroPanel1.Controls.Add(this.btnExport);
            this.metroPanel1.Controls.Add(this.btnSearch);
            this.metroPanel1.Controls.Add(this.metroLabel4);
            this.metroPanel1.Controls.Add(this.metroLabel3);
            this.metroPanel1.Controls.Add(this.txtSearch);
            this.metroPanel1.Controls.Add(this.cmbVendors);
            this.metroPanel1.Controls.Add(this.cmbSection);
            this.metroPanel1.Controls.Add(this.cmbDepartment);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(1, 51);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(787, 82);
            this.metroPanel1.TabIndex = 14;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // endDate
            // 
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDate.Location = new System.Drawing.Point(311, 48);
            this.endDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(132, 29);
            this.endDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.endDate.TabIndex = 29;
            this.endDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // startDate
            // 
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDate.Location = new System.Drawing.Point(93, 48);
            this.startDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(141, 29);
            this.startDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.startDate.TabIndex = 28;
            this.startDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(574, 50);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 29);
            this.btnExport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnExport.UseSelectable = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(478, 49);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 29);
            this.btnSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(240, 50);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(69, 19);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel4.TabIndex = 25;
            this.metroLabel4.Text = "End Date: ";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(14, 49);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(75, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel3.TabIndex = 23;
            this.metroLabel3.Text = "Start Date: ";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtSearch
            // 
            // 
            // 
            // 
            this.txtSearch.CustomButton.Image = null;
            this.txtSearch.CustomButton.Location = new System.Drawing.Point(163, 1);
            this.txtSearch.CustomButton.Name = "";
            this.txtSearch.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearch.CustomButton.TabIndex = 1;
            this.txtSearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearch.CustomButton.UseSelectable = true;
            this.txtSearch.CustomButton.Visible = false;
            this.txtSearch.DisplayIcon = true;
            this.txtSearch.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearch.IconRight = true;
            this.txtSearch.Lines = new string[0];
            this.txtSearch.Location = new System.Drawing.Point(11, 8);
            this.txtSearch.MaxLength = 20;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PromptText = "UPC Code/Name";
            this.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.ShortcutsEnabled = true;
            this.txtSearch.Size = new System.Drawing.Size(191, 29);
            this.txtSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearch.UseSelectable = true;
            this.txtSearch.WaterMark = "UPC Code/Name";
            this.txtSearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // cmbVendors
            // 
            this.cmbVendors.FormattingEnabled = true;
            this.cmbVendors.ItemHeight = 23;
            this.cmbVendors.Location = new System.Drawing.Point(574, 8);
            this.cmbVendors.MaxDropDownItems = 50;
            this.cmbVendors.Name = "cmbVendors";
            this.cmbVendors.Size = new System.Drawing.Size(172, 29);
            this.cmbVendors.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbVendors.TabIndex = 2;
            this.cmbVendors.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbVendors.UseSelectable = true;
            // 
            // cmbSection
            // 
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.ItemHeight = 23;
            this.cmbSection.Location = new System.Drawing.Point(386, 8);
            this.cmbSection.MaxDropDownItems = 50;
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(172, 29);
            this.cmbSection.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbSection.TabIndex = 2;
            this.cmbSection.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbSection.UseSelectable = true;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.ItemHeight = 23;
            this.cmbDepartment.Location = new System.Drawing.Point(208, 8);
            this.cmbDepartment.MaxDropDownItems = 50;
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(172, 29);
            this.cmbDepartment.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbDepartment.TabIndex = 1;
            this.cmbDepartment.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbDepartment.UseSelectable = true;
            this.cmbDepartment.SelectedIndexChanged += new System.EventHandler(this.cmbDepartment_SelectedIndexChanged);
            // 
            // ProductSaleGrdView
            // 
            this.ProductSaleGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.ProductSaleGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductSaleGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductSaleGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductSaleGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ProductSaleGrdView.BackgroundColor = System.Drawing.Color.White;
            this.ProductSaleGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProductSaleGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProductSaleGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductSaleGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductSaleGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductSaleGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductSaleGrdView.EnableHeadersVisualStyles = false;
            this.ProductSaleGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ProductSaleGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ProductSaleGrdView.Location = new System.Drawing.Point(12, 139);
            this.ProductSaleGrdView.Name = "ProductSaleGrdView";
            this.ProductSaleGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductSaleGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ProductSaleGrdView.RowHeadersVisible = false;
            this.ProductSaleGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ProductSaleGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductSaleGrdView.Size = new System.Drawing.Size(776, 800);
            this.ProductSaleGrdView.TabIndex = 15;
            this.ProductSaleGrdView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ProductSaleGrdView_ColumnHeaderMouseClick);
            // 
            // picLoader
            // 
            this.picLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.picLoader.BackColor = System.Drawing.Color.White;
            this.picLoader.Image = global::SFPOSWindows.Properties.Resources.Loader;
            this.picLoader.Location = new System.Drawing.Point(288, 221);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(201, 206);
            this.picLoader.TabIndex = 26;
            this.picLoader.TabStop = false;
            this.picLoader.Visible = false;
            // 
            // FrmProductSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.picLoader);
            this.Controls.Add(this.ProductSaleGrdView);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmProductSales";
            this.Text = "Product Sales";
            this.Load += new System.EventHandler(this.FrmProductSales_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmProductSales_VisibleChanged);
            this.Leave += new System.EventHandler(this.FrmProductSales_Leave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductSaleGrdView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroComboBox cmbSection;
        private MetroFramework.Controls.MetroComboBox cmbDepartment;
        private MetroFramework.Controls.MetroTextBox txtSearch;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroButton btnSearch;
        private MetroFramework.Controls.MetroDateTime endDate;
        private MetroFramework.Controls.MetroDateTime startDate;
        public MetroFramework.Controls.MetroGrid ProductSaleGrdView;
        private MetroFramework.Controls.MetroComboBox cmbVendors;
        private MetroFramework.Controls.MetroButton btnExport;
        private System.Windows.Forms.PictureBox picLoader;
    }
}