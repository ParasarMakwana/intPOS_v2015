using SFPOS.Common;

namespace SFPOSWindows.MasterForm
{
    partial class FrmProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProduct));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEditList = new MetroFramework.Controls.MetroButton();
            this.btnColumnFilter = new MetroFramework.Controls.MetroButton();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.metroBtnSaleDisc = new MetroFramework.Controls.MetroButton();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.btnImport = new MetroFramework.Controls.MetroButton();
            this.metroBtnProductvendor = new MetroFramework.Controls.MetroButton();
            this.metroBtnSalePrice = new MetroFramework.Controls.MetroButton();
            this.txtSearchProductName = new MetroFramework.Controls.MetroTextBox();
            this.metroBtnShowAll = new MetroFramework.Controls.MetroButton();
            this.MetrobtnAdd = new MetroFramework.Controls.MetroButton();
            this.backgroundWorkerLoadData = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ProductGrdView = new MetroFramework.Controls.MetroGrid();
            this.ColumnFilterpanel = new System.Windows.Forms.Panel();
            this.BtnSearch = new MetroFramework.Controls.MetroButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbSearchFilter = new MetroFramework.Controls.MetroComboBox();
            this.txtSearchFilter = new MetroFramework.Controls.MetroTextBox();
            this.chkBoxList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnclear = new MetroFramework.Controls.MetroButton();
            this.btnSaveFilter = new MetroFramework.Controls.MetroButton();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.btnApplyFilter = new MetroFramework.Controls.MetroButton();
            this.picLoader = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductGrdView)).BeginInit();
            this.ColumnFilterpanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnEditList);
            this.panel1.Controls.Add(this.btnColumnFilter);
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Controls.Add(this.metroBtnSaleDisc);
            this.panel1.Location = new System.Drawing.Point(5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 44);
            this.panel1.TabIndex = 23;
            // 
            // btnEditList
            // 
            this.btnEditList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditList.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnEditList.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnEditList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnEditList.Location = new System.Drawing.Point(724, 11);
            this.btnEditList.Name = "btnEditList";
            this.btnEditList.Size = new System.Drawing.Size(107, 26);
            this.btnEditList.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnEditList.TabIndex = 28;
            this.btnEditList.Text = "EDIT LIST";
            this.btnEditList.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnEditList.UseCustomForeColor = true;
            this.btnEditList.UseSelectable = true;
            this.btnEditList.Visible = false;
            this.btnEditList.Click += new System.EventHandler(this.btnEditList_Click);
            // 
            // btnColumnFilter
            // 
            this.btnColumnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColumnFilter.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnColumnFilter.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnColumnFilter.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnColumnFilter.Location = new System.Drawing.Point(851, 11);
            this.btnColumnFilter.Name = "btnColumnFilter";
            this.btnColumnFilter.Size = new System.Drawing.Size(107, 26);
            this.btnColumnFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnColumnFilter.TabIndex = 27;
            this.btnColumnFilter.Text = "VIEW";
            this.btnColumnFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnColumnFilter.UseCustomForeColor = true;
            this.btnColumnFilter.UseSelectable = true;
            this.btnColumnFilter.Click += new System.EventHandler(this.btnColumnFilter_Click);
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(102, 32);
            this.lblSubHeading.TabIndex = 0;
            this.lblSubHeading.Text = "Products";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroBtnSaleDisc
            // 
            this.metroBtnSaleDisc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnSaleDisc.Enabled = false;
            this.metroBtnSaleDisc.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnSaleDisc.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnSaleDisc.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnSaleDisc.Location = new System.Drawing.Point(813, 11);
            this.metroBtnSaleDisc.Name = "metroBtnSaleDisc";
            this.metroBtnSaleDisc.Size = new System.Drawing.Size(146, 26);
            this.metroBtnSaleDisc.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnSaleDisc.TabIndex = 11;
            this.metroBtnSaleDisc.Text = "SALE DISCOUNT";
            this.metroBtnSaleDisc.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnSaleDisc.UseCustomForeColor = true;
            this.metroBtnSaleDisc.UseSelectable = true;
            this.metroBtnSaleDisc.Visible = false;
            this.metroBtnSaleDisc.Click += new System.EventHandler(this.metroBtnSaleDisc_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.btnExport);
            this.pnlSearch.Controls.Add(this.btnImport);
            this.pnlSearch.Controls.Add(this.metroBtnProductvendor);
            this.pnlSearch.Controls.Add(this.metroBtnSalePrice);
            this.pnlSearch.Controls.Add(this.txtSearchProductName);
            this.pnlSearch.Controls.Add(this.metroBtnShowAll);
            this.pnlSearch.Controls.Add(this.MetrobtnAdd);
            this.pnlSearch.Location = new System.Drawing.Point(5, 51);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(962, 46);
            this.pnlSearch.TabIndex = 24;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExport.Location = new System.Drawing.Point(545, 9);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 26);
            this.btnExport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnExport.TabIndex = 26;
            this.btnExport.Text = "EXPORT";
            this.btnExport.UseCustomForeColor = true;
            this.btnExport.UseSelectable = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnImport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnImport.Location = new System.Drawing.Point(446, 9);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 26);
            this.btnImport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnImport.TabIndex = 25;
            this.btnImport.Text = "IMPORT";
            this.btnImport.UseCustomForeColor = true;
            this.btnImport.UseSelectable = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // metroBtnProductvendor
            // 
            this.metroBtnProductvendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnProductvendor.Enabled = false;
            this.metroBtnProductvendor.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnProductvendor.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnProductvendor.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnProductvendor.Location = new System.Drawing.Point(852, 9);
            this.metroBtnProductvendor.Name = "metroBtnProductvendor";
            this.metroBtnProductvendor.Size = new System.Drawing.Size(107, 26);
            this.metroBtnProductvendor.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnProductvendor.TabIndex = 10;
            this.metroBtnProductvendor.Text = "VENDOR";
            this.metroBtnProductvendor.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnProductvendor.UseCustomForeColor = true;
            this.metroBtnProductvendor.UseSelectable = true;
            this.metroBtnProductvendor.Click += new System.EventHandler(this.metroBtnProductvendor_Click);
            // 
            // metroBtnSalePrice
            // 
            this.metroBtnSalePrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnSalePrice.Enabled = false;
            this.metroBtnSalePrice.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnSalePrice.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnSalePrice.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnSalePrice.Location = new System.Drawing.Point(743, 9);
            this.metroBtnSalePrice.Name = "metroBtnSalePrice";
            this.metroBtnSalePrice.Size = new System.Drawing.Size(103, 26);
            this.metroBtnSalePrice.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnSalePrice.TabIndex = 9;
            this.metroBtnSalePrice.Text = "SALE PRICE";
            this.metroBtnSalePrice.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnSalePrice.UseCustomForeColor = true;
            this.metroBtnSalePrice.UseSelectable = true;
            this.metroBtnSalePrice.Click += new System.EventHandler(this.metroBtnSalePrice_Click);
            // 
            // txtSearchProductName
            // 
            // 
            // 
            // 
            this.txtSearchProductName.CustomButton.Image = null;
            this.txtSearchProductName.CustomButton.Location = new System.Drawing.Point(204, 2);
            this.txtSearchProductName.CustomButton.Name = "";
            this.txtSearchProductName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchProductName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchProductName.CustomButton.TabIndex = 1;
            this.txtSearchProductName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchProductName.CustomButton.UseSelectable = true;
            this.txtSearchProductName.CustomButton.Visible = false;
            this.txtSearchProductName.DisplayIcon = true;
            this.txtSearchProductName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearchProductName.IconRight = true;
            this.txtSearchProductName.Lines = new string[0];
            this.txtSearchProductName.Location = new System.Drawing.Point(12, 9);
            this.txtSearchProductName.MaxLength = 20;
            this.txtSearchProductName.Name = "txtSearchProductName";
            this.txtSearchProductName.PasswordChar = '\0';
            this.txtSearchProductName.PromptText = "Name/UPC Code";
            this.txtSearchProductName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchProductName.SelectedText = "";
            this.txtSearchProductName.SelectionLength = 0;
            this.txtSearchProductName.SelectionStart = 0;
            this.txtSearchProductName.ShortcutsEnabled = true;
            this.txtSearchProductName.Size = new System.Drawing.Size(228, 26);
            this.txtSearchProductName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchProductName.TabIndex = 1;
            this.txtSearchProductName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchProductName.UseSelectable = true;
            this.txtSearchProductName.WaterMark = "Name/UPC Code";
            this.txtSearchProductName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchProductName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchProductName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchProductName_KeyPress);
            // 
            // metroBtnShowAll
            // 
            this.metroBtnShowAll.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnShowAll.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnShowAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnShowAll.Location = new System.Drawing.Point(246, 9);
            this.metroBtnShowAll.Name = "metroBtnShowAll";
            this.metroBtnShowAll.Size = new System.Drawing.Size(95, 26);
            this.metroBtnShowAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnShowAll.TabIndex = 7;
            this.metroBtnShowAll.Text = "SHOW ALL";
            this.metroBtnShowAll.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnShowAll.UseCustomForeColor = true;
            this.metroBtnShowAll.UseSelectable = true;
            this.metroBtnShowAll.Click += new System.EventHandler(this.metroBtnShowAll_Click);
            // 
            // MetrobtnAdd
            // 
            this.MetrobtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MetrobtnAdd.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.MetrobtnAdd.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.MetrobtnAdd.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MetrobtnAdd.Location = new System.Drawing.Point(645, 9);
            this.MetrobtnAdd.Name = "MetrobtnAdd";
            this.MetrobtnAdd.Size = new System.Drawing.Size(95, 26);
            this.MetrobtnAdd.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnAdd.TabIndex = 6;
            this.MetrobtnAdd.Text = "ADD";
            this.MetrobtnAdd.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnAdd.UseCustomForeColor = true;
            this.MetrobtnAdd.UseSelectable = true;
            this.MetrobtnAdd.Click += new System.EventHandler(this.MetrobtnAdd_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // ProductGrdView
            // 
            this.ProductGrdView.AllowUserToAddRows = false;
            this.ProductGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.ProductGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ProductGrdView.BackgroundColor = System.Drawing.Color.White;
            this.ProductGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProductGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProductGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductGrdView.EnableHeadersVisualStyles = false;
            this.ProductGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ProductGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ProductGrdView.Location = new System.Drawing.Point(17, 103);
            this.ProductGrdView.MultiSelect = false;
            this.ProductGrdView.Name = "ProductGrdView";
            this.ProductGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ProductGrdView.RowHeadersVisible = false;
            this.ProductGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ProductGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductGrdView.Size = new System.Drawing.Size(947, 422);
            this.ProductGrdView.TabIndex = 26;
            this.ProductGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductGrdView_CellClick);
            this.ProductGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductGrdView_CellContentClick);
            this.ProductGrdView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductGrdView_CellDoubleClick);
            this.ProductGrdView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductGrdView_CellValueChanged);
            this.ProductGrdView.CurrentCellDirtyStateChanged += new System.EventHandler(this.ProductGrdView_CurrentCellDirtyStateChanged);
            this.ProductGrdView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.ProductGrdView_DataError);
            this.ProductGrdView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ProductGrdView_Scroll);
            // 
            // ColumnFilterpanel
            // 
            this.ColumnFilterpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColumnFilterpanel.Controls.Add(this.BtnSearch);
            this.ColumnFilterpanel.Controls.Add(this.metroButton1);
            this.ColumnFilterpanel.Controls.Add(this.label1);
            this.ColumnFilterpanel.Controls.Add(this.panel2);
            this.ColumnFilterpanel.Controls.Add(this.groupBox2);
            this.ColumnFilterpanel.Controls.Add(this.chkBoxList);
            this.ColumnFilterpanel.Controls.Add(this.groupBox1);
            this.ColumnFilterpanel.Location = new System.Drawing.Point(328, 156);
            this.ColumnFilterpanel.Name = "ColumnFilterpanel";
            this.ColumnFilterpanel.Size = new System.Drawing.Size(360, 341);
            this.ColumnFilterpanel.TabIndex = 27;
            this.ColumnFilterpanel.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSearch.BackgroundImage = global::SFPOSWindows.Properties.Resources.magnifying_glass2;
            this.BtnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnSearch.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BtnSearch.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.BtnSearch.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BtnSearch.Location = new System.Drawing.Point(323, 301);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(26, 29);
            this.BtnSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.BtnSearch.TabIndex = 35;
            this.BtnSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.BtnSearch.UseCustomForeColor = true;
            this.BtnSearch.UseSelectable = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButton1.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroButton1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroButton1.Location = new System.Drawing.Point(267, 246);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(80, 27);
            this.metroButton1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroButton1.TabIndex = 29;
            this.metroButton1.Text = "Cancel";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroButton1.UseCustomForeColor = true;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Columns";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::SFPOSWindows.Properties.Resources._41;
            this.panel2.Location = new System.Drawing.Point(-6, -5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(365, 10);
            this.panel2.TabIndex = 30;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbSearchFilter);
            this.groupBox2.Controls.Add(this.txtSearchFilter);
            this.groupBox2.Location = new System.Drawing.Point(3, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 51);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Filter";
            // 
            // cmbSearchFilter
            // 
            this.cmbSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSearchFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbSearchFilter.FormattingEnabled = true;
            this.cmbSearchFilter.IntegralHeight = false;
            this.cmbSearchFilter.ItemHeight = 23;
            this.cmbSearchFilter.Location = new System.Drawing.Point(155, 16);
            this.cmbSearchFilter.Name = "cmbSearchFilter";
            this.cmbSearchFilter.Size = new System.Drawing.Size(164, 29);
            this.cmbSearchFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbSearchFilter.TabIndex = 37;
            this.cmbSearchFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbSearchFilter.UseSelectable = true;
            // 
            // txtSearchFilter
            // 
            this.txtSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSearchFilter.CustomButton.Image = null;
            this.txtSearchFilter.CustomButton.Location = new System.Drawing.Point(120, 1);
            this.txtSearchFilter.CustomButton.Name = "";
            this.txtSearchFilter.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSearchFilter.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchFilter.CustomButton.TabIndex = 1;
            this.txtSearchFilter.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchFilter.CustomButton.UseSelectable = true;
            this.txtSearchFilter.CustomButton.Visible = false;
            this.txtSearchFilter.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSearchFilter.Lines = new string[0];
            this.txtSearchFilter.Location = new System.Drawing.Point(6, 16);
            this.txtSearchFilter.MaxLength = 100;
            this.txtSearchFilter.Name = "txtSearchFilter";
            this.txtSearchFilter.PasswordChar = '\0';
            this.txtSearchFilter.PromptText = "Search Here";
            this.txtSearchFilter.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchFilter.SelectedText = "";
            this.txtSearchFilter.SelectionLength = 0;
            this.txtSearchFilter.SelectionStart = 0;
            this.txtSearchFilter.ShortcutsEnabled = true;
            this.txtSearchFilter.Size = new System.Drawing.Size(148, 29);
            this.txtSearchFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchFilter.TabIndex = 37;
            this.txtSearchFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchFilter.UseSelectable = true;
            this.txtSearchFilter.WaterMark = "Search Here";
            this.txtSearchFilter.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchFilter.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // chkBoxList
            // 
            this.chkBoxList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBoxList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkBoxList.CheckOnClick = true;
            this.chkBoxList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBoxList.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkBoxList.FormattingEnabled = true;
            this.chkBoxList.Location = new System.Drawing.Point(10, 41);
            this.chkBoxList.Name = "chkBoxList";
            this.chkBoxList.Size = new System.Drawing.Size(333, 198);
            this.chkBoxList.TabIndex = 16;
            this.chkBoxList.ThreeDCheckBoxes = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnclear);
            this.groupBox1.Controls.Add(this.btnSaveFilter);
            this.groupBox1.Controls.Add(this.txtOut);
            this.groupBox1.Controls.Add(this.btnApplyFilter);
            this.groupBox1.Location = new System.Drawing.Point(3, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 253);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // btnclear
            // 
            this.btnclear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnclear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnclear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnclear.Location = new System.Drawing.Point(178, 215);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(80, 27);
            this.btnclear.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnclear.TabIndex = 38;
            this.btnclear.Text = "Clear Filter";
            this.btnclear.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnclear.UseCustomForeColor = true;
            this.btnclear.UseSelectable = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnSaveFilter
            // 
            this.btnSaveFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFilter.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSaveFilter.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnSaveFilter.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSaveFilter.Location = new System.Drawing.Point(6, 215);
            this.btnSaveFilter.Name = "btnSaveFilter";
            this.btnSaveFilter.Size = new System.Drawing.Size(80, 27);
            this.btnSaveFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSaveFilter.TabIndex = 37;
            this.btnSaveFilter.Text = "Save";
            this.btnSaveFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSaveFilter.UseCustomForeColor = true;
            this.btnSaveFilter.UseSelectable = true;
            this.btnSaveFilter.Click += new System.EventHandler(this.btnSaveFilter_Click);
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(308, 215);
            this.txtOut.Multiline = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.Size = new System.Drawing.Size(11, 15);
            this.txtOut.TabIndex = 1;
            this.txtOut.Visible = false;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyFilter.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnApplyFilter.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnApplyFilter.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnApplyFilter.Location = new System.Drawing.Point(92, 215);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(80, 27);
            this.btnApplyFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnApplyFilter.TabIndex = 28;
            this.btnApplyFilter.Text = "Apply";
            this.btnApplyFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnApplyFilter.UseCustomForeColor = true;
            this.btnApplyFilter.UseSelectable = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // picLoader
            // 
            this.picLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.picLoader.BackColor = System.Drawing.Color.White;
            this.picLoader.Image = global::SFPOSWindows.Properties.Resources.Loader;
            this.picLoader.Location = new System.Drawing.Point(372, 295);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(190, 174);
            this.picLoader.TabIndex = 25;
            this.picLoader.TabStop = false;
            this.picLoader.Visible = false;
            // 
            // FrmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(967, 537);
            this.Controls.Add(this.ColumnFilterpanel);
            this.Controls.Add(this.picLoader);
            this.Controls.Add(this.ProductGrdView);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProduct";
            this.Text = "Product Management";
            this.Load += new System.EventHandler(this.FrmProduct_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmProduct_VisibleChanged);
            this.Leave += new System.EventHandler(this.FrmProduct_Leave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductGrdView)).EndInit();
            this.ColumnFilterpanel.ResumeLayout(false);
            this.ColumnFilterpanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel pnlSearch;
        private MetroFramework.Controls.MetroButton metroBtnShowAll;
        private MetroFramework.Controls.MetroButton MetrobtnAdd;
        private MetroFramework.Controls.MetroButton metroBtnSaleDisc;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadData;
        private MetroFramework.Controls.MetroButton btnExport;
        private MetroFramework.Controls.MetroButton btnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox picLoader;
        public MetroFramework.Controls.MetroGrid ProductGrdView;
        private MetroFramework.Controls.MetroButton metroBtnProductvendor;
        private MetroFramework.Controls.MetroButton metroBtnSalePrice;
        private System.Windows.Forms.Panel ColumnFilterpanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkBoxList;
        private MetroFramework.Controls.MetroButton btnApplyFilter;
        private MetroFramework.Controls.MetroButton btnColumnFilter;
        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtOut;
        private MetroFramework.Controls.MetroButton BtnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public MetroFramework.Controls.MetroTextBox txtSearchFilter;
        private MetroFramework.Controls.MetroButton btnSaveFilter;
        private MetroFramework.Controls.MetroButton btnEditList;
        private MetroFramework.Controls.MetroButton btnclear;
        public MetroFramework.Controls.MetroComboBox cmbSearchFilter;
        public MetroFramework.Controls.MetroTextBox txtSearchProductName;
    }
}