namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddProduct));
            this.cmbSection = new MetroFramework.Controls.MetroComboBox();
            this.cmbDepartment = new MetroFramework.Controls.MetroComboBox();
            this.txtUPCCode = new MetroFramework.Controls.MetroTextBox();
            this.txtProductName = new MetroFramework.Controls.MetroTextBox();
            this.txtCertificateCode = new MetroFramework.Controls.MetroTextBox();
            this.cmbUoM = new MetroFramework.Controls.MetroComboBox();
            this.cmbTaxGroup = new MetroFramework.Controls.MetroComboBox();
            this.txtPrice = new MetroFramework.Controls.MetroTextBox();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.btnBrowse = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.ImageProduct = new System.Windows.Forms.PictureBox();
            this.toggleFdStamp = new MetroFramework.Controls.MetroToggle();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.toggleActive = new MetroFramework.Controls.MetroToggle();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.ToggleAgeVerify = new MetroFramework.Controls.MetroToggle();
            this.txtGroupQty = new MetroFramework.Controls.MetroTextBox();
            this.txtGroupPrice = new MetroFramework.Controls.MetroTextBox();
            this.txtLinkUPCCode = new MetroFramework.Controls.MetroTextBox();
            this.txtTareWeight = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.ToggleScaled = new MetroFramework.Controls.MetroToggle();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel13 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel14 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel15 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel16 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel17 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel18 = new MetroFramework.Controls.MetroLabel();
            this.ToggleLabeled = new MetroFramework.Controls.MetroToggle();
            this.metroLabel19 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel20 = new MetroFramework.Controls.MetroLabel();
            this.txtCasePrice = new MetroFramework.Controls.MetroTextBox();
            this.txtCaseQty = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel21 = new MetroFramework.Controls.MetroLabel();
            this.txtUnitCost = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel22 = new MetroFramework.Controls.MetroLabel();
            this.DtUpdateddate = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel23 = new MetroFramework.Controls.MetroLabel();
            this.cmbVendors = new MetroFramework.Controls.MetroComboBox();
            this.metroBtnProductvendor = new MetroFramework.Controls.MetroButton();
            this.metroBtnSalePrice = new MetroFramework.Controls.MetroButton();
            this.ToggleGroupPrice = new MetroFramework.Controls.MetroToggle();
            this.metroLabel24 = new MetroFramework.Controls.MetroLabel();
            this.lblPack = new MetroFramework.Controls.MetroLabel();
            this.metroLabel26 = new MetroFramework.Controls.MetroLabel();
            this.txtPack = new MetroFramework.Controls.MetroTextBox();
            this.txtSize = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel25 = new MetroFramework.Controls.MetroLabel();
            this.txtSecondaryPLU = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel27 = new MetroFramework.Controls.MetroLabel();
            this.txtPalletQTY = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel28 = new MetroFramework.Controls.MetroLabel();
            this.txtCountryofOrig = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel29 = new MetroFramework.Controls.MetroLabel();
            this.txtFSEligibleAmount = new MetroFramework.Controls.MetroTextBox();
            this.btnChangeFSAmount = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSection
            // 
            this.cmbSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.ItemHeight = 23;
            this.cmbSection.Location = new System.Drawing.Point(414, 133);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(166, 29);
            this.cmbSection.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbSection.TabIndex = 5;
            this.cmbSection.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbSection.UseSelectable = true;
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.ItemHeight = 23;
            this.cmbDepartment.Location = new System.Drawing.Point(149, 133);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(166, 29);
            this.cmbDepartment.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbDepartment.TabIndex = 4;
            this.cmbDepartment.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbDepartment.UseSelectable = true;
            this.cmbDepartment.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // txtUPCCode
            // 
            this.txtUPCCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtUPCCode.CustomButton.Image = null;
            this.txtUPCCode.CustomButton.Location = new System.Drawing.Point(139, 1);
            this.txtUPCCode.CustomButton.Name = "";
            this.txtUPCCode.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtUPCCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUPCCode.CustomButton.TabIndex = 1;
            this.txtUPCCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUPCCode.CustomButton.UseSelectable = true;
            this.txtUPCCode.CustomButton.Visible = false;
            this.txtUPCCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUPCCode.Lines = new string[0];
            this.txtUPCCode.Location = new System.Drawing.Point(148, 98);
            this.txtUPCCode.MaxLength = 13;
            this.txtUPCCode.Name = "txtUPCCode";
            this.txtUPCCode.PasswordChar = '\0';
            this.txtUPCCode.PromptText = "UPC Code";
            this.txtUPCCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUPCCode.SelectedText = "";
            this.txtUPCCode.SelectionLength = 0;
            this.txtUPCCode.SelectionStart = 0;
            this.txtUPCCode.ShortcutsEnabled = true;
            this.txtUPCCode.Size = new System.Drawing.Size(167, 29);
            this.txtUPCCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUPCCode.TabIndex = 2;
            this.txtUPCCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUPCCode.UseSelectable = true;
            this.txtUPCCode.WaterMark = "UPC Code";
            this.txtUPCCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUPCCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtUPCCode.TextChanged += new System.EventHandler(this.txtUPCCode_TextChanged);
            this.txtUPCCode.Leave += new System.EventHandler(this.txtUPCCode_Leave);
            // 
            // txtProductName
            // 
            this.txtProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtProductName.CustomButton.Image = null;
            this.txtProductName.CustomButton.Location = new System.Drawing.Point(405, 1);
            this.txtProductName.CustomButton.Name = "";
            this.txtProductName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtProductName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProductName.CustomButton.TabIndex = 1;
            this.txtProductName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProductName.CustomButton.UseSelectable = true;
            this.txtProductName.CustomButton.Visible = false;
            this.txtProductName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtProductName.Lines = new string[0];
            this.txtProductName.Location = new System.Drawing.Point(148, 63);
            this.txtProductName.MaxLength = 100;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.PasswordChar = '\0';
            this.txtProductName.PromptText = "Name";
            this.txtProductName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProductName.SelectedText = "";
            this.txtProductName.SelectionLength = 0;
            this.txtProductName.SelectionStart = 0;
            this.txtProductName.ShortcutsEnabled = true;
            this.txtProductName.Size = new System.Drawing.Size(433, 29);
            this.txtProductName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProductName.TabIndex = 1;
            this.txtProductName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProductName.UseSelectable = true;
            this.txtProductName.WaterMark = "Name";
            this.txtProductName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProductName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtProductName.TextChanged += new System.EventHandler(this.txtProductName_TextChanged);
            // 
            // txtCertificateCode
            // 
            // 
            // 
            // 
            this.txtCertificateCode.CustomButton.Image = null;
            this.txtCertificateCode.CustomButton.Location = new System.Drawing.Point(139, 2);
            this.txtCertificateCode.CustomButton.Name = "";
            this.txtCertificateCode.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtCertificateCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCertificateCode.CustomButton.TabIndex = 1;
            this.txtCertificateCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCertificateCode.CustomButton.UseSelectable = true;
            this.txtCertificateCode.CustomButton.Visible = false;
            this.txtCertificateCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCertificateCode.Lines = new string[0];
            this.txtCertificateCode.Location = new System.Drawing.Point(414, 240);
            this.txtCertificateCode.MaxLength = 20;
            this.txtCertificateCode.Name = "txtCertificateCode";
            this.txtCertificateCode.PasswordChar = '\0';
            this.txtCertificateCode.PromptText = "Item Code";
            this.txtCertificateCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCertificateCode.SelectedText = "";
            this.txtCertificateCode.SelectionLength = 0;
            this.txtCertificateCode.SelectionStart = 0;
            this.txtCertificateCode.ShortcutsEnabled = true;
            this.txtCertificateCode.Size = new System.Drawing.Size(167, 30);
            this.txtCertificateCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCertificateCode.TabIndex = 11;
            this.txtCertificateCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCertificateCode.UseSelectable = true;
            this.txtCertificateCode.WaterMark = "Item Code";
            this.txtCertificateCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCertificateCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtCertificateCode.TextChanged += new System.EventHandler(this.txtCertificateCode_TextChanged);
            // 
            // cmbUoM
            // 
            this.cmbUoM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUoM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbUoM.FormattingEnabled = true;
            this.cmbUoM.ItemHeight = 23;
            this.cmbUoM.Location = new System.Drawing.Point(149, 168);
            this.cmbUoM.Name = "cmbUoM";
            this.cmbUoM.Size = new System.Drawing.Size(166, 29);
            this.cmbUoM.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbUoM.TabIndex = 6;
            this.cmbUoM.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbUoM.UseSelectable = true;
            // 
            // cmbTaxGroup
            // 
            this.cmbTaxGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTaxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbTaxGroup.FormattingEnabled = true;
            this.cmbTaxGroup.ItemHeight = 23;
            this.cmbTaxGroup.Location = new System.Drawing.Point(414, 168);
            this.cmbTaxGroup.Name = "cmbTaxGroup";
            this.cmbTaxGroup.Size = new System.Drawing.Size(166, 29);
            this.cmbTaxGroup.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbTaxGroup.TabIndex = 7;
            this.cmbTaxGroup.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbTaxGroup.UseSelectable = true;
            // 
            // txtPrice
            // 
            this.txtPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtPrice.CustomButton.Image = null;
            this.txtPrice.CustomButton.Location = new System.Drawing.Point(139, 2);
            this.txtPrice.CustomButton.Name = "";
            this.txtPrice.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtPrice.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPrice.CustomButton.TabIndex = 1;
            this.txtPrice.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPrice.CustomButton.UseSelectable = true;
            this.txtPrice.CustomButton.Visible = false;
            this.txtPrice.DisplayIcon = true;
            this.txtPrice.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPrice.Icon = global::SFPOSWindows.Properties.Resources.dollar_coin_money1;
            this.txtPrice.IconRight = true;
            this.txtPrice.Lines = new string[0];
            this.txtPrice.Location = new System.Drawing.Point(414, 203);
            this.txtPrice.MaxLength = 20;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.PasswordChar = '\0';
            this.txtPrice.PromptText = "Price";
            this.txtPrice.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPrice.SelectedText = "";
            this.txtPrice.SelectionLength = 0;
            this.txtPrice.SelectionStart = 0;
            this.txtPrice.ShortcutsEnabled = true;
            this.txtPrice.Size = new System.Drawing.Size(167, 30);
            this.txtPrice.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPrice.TabIndex = 9;
            this.txtPrice.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPrice.UseSelectable = true;
            this.txtPrice.WaterMark = "Price";
            this.txtPrice.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPrice.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            this.txtPrice.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPrice_KeyUp);
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(488, 660);
            this.metroBtnClear.Name = "metroBtnClear";
            this.metroBtnClear.Size = new System.Drawing.Size(92, 29);
            this.metroBtnClear.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnClear.TabIndex = 26;
            this.metroBtnClear.Text = "CLEAR";
            this.metroBtnClear.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnClear.UseCustomForeColor = true;
            this.metroBtnClear.UseSelectable = true;
            this.metroBtnClear.Click += new System.EventHandler(this.metroBtnClear_Click);
            // 
            // MetrobtnSave
            // 
            this.MetrobtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MetrobtnSave.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.MetrobtnSave.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.MetrobtnSave.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MetrobtnSave.Location = new System.Drawing.Point(390, 660);
            this.MetrobtnSave.Name = "MetrobtnSave";
            this.MetrobtnSave.Size = new System.Drawing.Size(92, 29);
            this.MetrobtnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnSave.TabIndex = 25;
            this.MetrobtnSave.Text = "SAVE";
            this.MetrobtnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnSave.UseCustomForeColor = true;
            this.MetrobtnSave.UseSelectable = true;
            this.MetrobtnSave.Click += new System.EventHandler(this.MetrobtnSave_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnBrowse.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnBrowse.Location = new System.Drawing.Point(326, 589);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(62, 23);
            this.btnBrowse.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnBrowse.TabIndex = 24;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnBrowse.UseCustomForeColor = true;
            this.btnBrowse.UseSelectable = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(25, 589);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(106, 19);
            this.metroLabel1.TabIndex = 24;
            this.metroLabel1.Text = "Product Image: ";
            // 
            // ImageProduct
            // 
            this.ImageProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageProduct.BackColor = System.Drawing.Color.Transparent;
            this.ImageProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ImageProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImageProduct.Location = new System.Drawing.Point(150, 589);
            this.ImageProduct.Name = "ImageProduct";
            this.ImageProduct.Size = new System.Drawing.Size(166, 104);
            this.ImageProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageProduct.TabIndex = 20;
            this.ImageProduct.TabStop = false;
            // 
            // toggleFdStamp
            // 
            this.toggleFdStamp.AutoSize = true;
            this.toggleFdStamp.DisplayStatus = false;
            this.toggleFdStamp.Location = new System.Drawing.Point(150, 527);
            this.toggleFdStamp.Name = "toggleFdStamp";
            this.toggleFdStamp.Size = new System.Drawing.Size(50, 17);
            this.toggleFdStamp.TabIndex = 18;
            this.toggleFdStamp.Text = "Off";
            this.toggleFdStamp.UseSelectable = true;
            this.toggleFdStamp.CheckedChanged += new System.EventHandler(this.toggleFdStamp_CheckedChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(27, 527);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(83, 19);
            this.metroLabel2.TabIndex = 27;
            this.metroLabel2.Text = "Food Stamp";
            // 
            // toggleActive
            // 
            this.toggleActive.DisplayStatus = false;
            this.toggleActive.Location = new System.Drawing.Point(326, 527);
            this.toggleActive.Name = "toggleActive";
            this.toggleActive.Size = new System.Drawing.Size(50, 17);
            this.toggleActive.TabIndex = 19;
            this.toggleActive.Text = "Off";
            this.toggleActive.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(260, 527);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(46, 19);
            this.metroLabel3.TabIndex = 29;
            this.metroLabel3.Text = "Active";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(416, 527);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(71, 19);
            this.metroLabel4.TabIndex = 31;
            this.metroLabel4.Text = "Age Verify";
            // 
            // ToggleAgeVerify
            // 
            this.ToggleAgeVerify.AutoSize = true;
            this.ToggleAgeVerify.DisplayStatus = false;
            this.ToggleAgeVerify.Location = new System.Drawing.Point(527, 529);
            this.ToggleAgeVerify.Name = "ToggleAgeVerify";
            this.ToggleAgeVerify.Size = new System.Drawing.Size(50, 17);
            this.ToggleAgeVerify.TabIndex = 20;
            this.ToggleAgeVerify.Text = "Off";
            this.ToggleAgeVerify.UseSelectable = true;
            // 
            // txtGroupQty
            // 
            this.txtGroupQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtGroupQty.CustomButton.Image = null;
            this.txtGroupQty.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtGroupQty.CustomButton.Name = "";
            this.txtGroupQty.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtGroupQty.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtGroupQty.CustomButton.TabIndex = 1;
            this.txtGroupQty.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtGroupQty.CustomButton.UseSelectable = true;
            this.txtGroupQty.CustomButton.Visible = false;
            this.txtGroupQty.DisplayIcon = true;
            this.txtGroupQty.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtGroupQty.IconRight = true;
            this.txtGroupQty.Lines = new string[0];
            this.txtGroupQty.Location = new System.Drawing.Point(149, 275);
            this.txtGroupQty.MaxLength = 20;
            this.txtGroupQty.Name = "txtGroupQty";
            this.txtGroupQty.PasswordChar = '\0';
            this.txtGroupQty.PromptText = "Qty";
            this.txtGroupQty.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtGroupQty.SelectedText = "";
            this.txtGroupQty.SelectionLength = 0;
            this.txtGroupQty.SelectionStart = 0;
            this.txtGroupQty.ShortcutsEnabled = true;
            this.txtGroupQty.Size = new System.Drawing.Size(166, 30);
            this.txtGroupQty.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtGroupQty.TabIndex = 12;
            this.txtGroupQty.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtGroupQty.UseSelectable = true;
            this.txtGroupQty.WaterMark = "Qty";
            this.txtGroupQty.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtGroupQty.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtGroupPrice
            // 
            this.txtGroupPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtGroupPrice.CustomButton.Image = null;
            this.txtGroupPrice.CustomButton.Location = new System.Drawing.Point(139, 2);
            this.txtGroupPrice.CustomButton.Name = "";
            this.txtGroupPrice.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtGroupPrice.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtGroupPrice.CustomButton.TabIndex = 1;
            this.txtGroupPrice.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtGroupPrice.CustomButton.UseSelectable = true;
            this.txtGroupPrice.CustomButton.Visible = false;
            this.txtGroupPrice.DisplayIcon = true;
            this.txtGroupPrice.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtGroupPrice.Icon = global::SFPOSWindows.Properties.Resources.dollar_coin_money1;
            this.txtGroupPrice.IconRight = true;
            this.txtGroupPrice.Lines = new string[0];
            this.txtGroupPrice.Location = new System.Drawing.Point(414, 275);
            this.txtGroupPrice.MaxLength = 20;
            this.txtGroupPrice.Name = "txtGroupPrice";
            this.txtGroupPrice.PasswordChar = '\0';
            this.txtGroupPrice.PromptText = "Group/Price";
            this.txtGroupPrice.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtGroupPrice.SelectedText = "";
            this.txtGroupPrice.SelectionLength = 0;
            this.txtGroupPrice.SelectionStart = 0;
            this.txtGroupPrice.ShortcutsEnabled = true;
            this.txtGroupPrice.Size = new System.Drawing.Size(167, 30);
            this.txtGroupPrice.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtGroupPrice.TabIndex = 13;
            this.txtGroupPrice.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtGroupPrice.UseSelectable = true;
            this.txtGroupPrice.WaterMark = "Group/Price";
            this.txtGroupPrice.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtGroupPrice.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtGroupPrice.TextChanged += new System.EventHandler(this.txtGroupPrice_TextChanged);
            // 
            // txtLinkUPCCode
            // 
            this.txtLinkUPCCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtLinkUPCCode.CustomButton.Image = null;
            this.txtLinkUPCCode.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtLinkUPCCode.CustomButton.Name = "";
            this.txtLinkUPCCode.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtLinkUPCCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtLinkUPCCode.CustomButton.TabIndex = 1;
            this.txtLinkUPCCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtLinkUPCCode.CustomButton.UseSelectable = true;
            this.txtLinkUPCCode.CustomButton.Visible = false;
            this.txtLinkUPCCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtLinkUPCCode.Lines = new string[0];
            this.txtLinkUPCCode.Location = new System.Drawing.Point(149, 240);
            this.txtLinkUPCCode.MaxLength = 20;
            this.txtLinkUPCCode.Name = "txtLinkUPCCode";
            this.txtLinkUPCCode.PasswordChar = '\0';
            this.txtLinkUPCCode.PromptText = "Link UPC Code";
            this.txtLinkUPCCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLinkUPCCode.SelectedText = "";
            this.txtLinkUPCCode.SelectionLength = 0;
            this.txtLinkUPCCode.SelectionStart = 0;
            this.txtLinkUPCCode.ShortcutsEnabled = true;
            this.txtLinkUPCCode.Size = new System.Drawing.Size(166, 30);
            this.txtLinkUPCCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtLinkUPCCode.TabIndex = 10;
            this.txtLinkUPCCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtLinkUPCCode.UseSelectable = true;
            this.txtLinkUPCCode.WaterMark = "Link UPC Code";
            this.txtLinkUPCCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtLinkUPCCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtTareWeight
            // 
            this.txtTareWeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtTareWeight.CustomButton.Image = null;
            this.txtTareWeight.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtTareWeight.CustomButton.Name = "";
            this.txtTareWeight.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtTareWeight.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTareWeight.CustomButton.TabIndex = 1;
            this.txtTareWeight.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTareWeight.CustomButton.UseSelectable = true;
            this.txtTareWeight.CustomButton.Visible = false;
            this.txtTareWeight.DisplayIcon = true;
            this.txtTareWeight.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtTareWeight.IconRight = true;
            this.txtTareWeight.Lines = new string[0];
            this.txtTareWeight.Location = new System.Drawing.Point(149, 203);
            this.txtTareWeight.MaxLength = 20;
            this.txtTareWeight.Name = "txtTareWeight";
            this.txtTareWeight.PasswordChar = '\0';
            this.txtTareWeight.PromptText = "Tare Weight";
            this.txtTareWeight.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTareWeight.SelectedText = "";
            this.txtTareWeight.SelectionLength = 0;
            this.txtTareWeight.SelectionStart = 0;
            this.txtTareWeight.ShortcutsEnabled = true;
            this.txtTareWeight.Size = new System.Drawing.Size(166, 30);
            this.txtTareWeight.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTareWeight.TabIndex = 8;
            this.txtTareWeight.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTareWeight.UseSelectable = true;
            this.txtTareWeight.WaterMark = "Tare Weight";
            this.txtTareWeight.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtTareWeight.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtTareWeight.TextChanged += new System.EventHandler(this.txtTareWeight_TextChanged);
            this.txtTareWeight.Leave += new System.EventHandler(this.txtTareWeight_TabIndexChanged);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(26, 557);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(47, 19);
            this.metroLabel5.TabIndex = 37;
            this.metroLabel5.Text = "Scaled";
            // 
            // ToggleScaled
            // 
            this.ToggleScaled.AutoSize = true;
            this.ToggleScaled.DisplayStatus = false;
            this.ToggleScaled.Location = new System.Drawing.Point(150, 557);
            this.ToggleScaled.Name = "ToggleScaled";
            this.ToggleScaled.Size = new System.Drawing.Size(50, 17);
            this.ToggleScaled.TabIndex = 21;
            this.ToggleScaled.Text = "Off";
            this.ToggleScaled.UseSelectable = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.Location = new System.Drawing.Point(26, 63);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(104, 19);
            this.metroLabel6.TabIndex = 38;
            this.metroLabel6.Text = "Product Name: ";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.Location = new System.Drawing.Point(26, 98);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(79, 19);
            this.metroLabel7.TabIndex = 39;
            this.metroLabel7.Text = "UPC Code: ";
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel8.Location = new System.Drawing.Point(319, 244);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(76, 19);
            this.metroLabel8.TabIndex = 40;
            this.metroLabel8.Text = "Item Code:";
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel9.Location = new System.Drawing.Point(26, 240);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(123, 19);
            this.metroLabel9.TabIndex = 41;
            this.metroLabel9.Text = "Linked UPC Code: ";
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel10.Location = new System.Drawing.Point(26, 133);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(90, 19);
            this.metroLabel10.TabIndex = 42;
            this.metroLabel10.Text = "Department: ";
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel11.Location = new System.Drawing.Point(26, 203);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(87, 19);
            this.metroLabel11.TabIndex = 43;
            this.metroLabel11.Text = "Tare Weight: ";
            // 
            // metroLabel12
            // 
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel12.Location = new System.Drawing.Point(319, 133);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(66, 19);
            this.metroLabel12.TabIndex = 44;
            this.metroLabel12.Text = "Sections: ";
            // 
            // metroLabel13
            // 
            this.metroLabel13.AutoSize = true;
            this.metroLabel13.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel13.Location = new System.Drawing.Point(318, 168);
            this.metroLabel13.Name = "metroLabel13";
            this.metroLabel13.Size = new System.Drawing.Size(34, 19);
            this.metroLabel13.TabIndex = 45;
            this.metroLabel13.Text = "Tax: ";
            // 
            // metroLabel14
            // 
            this.metroLabel14.AutoSize = true;
            this.metroLabel14.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel14.Location = new System.Drawing.Point(26, 168);
            this.metroLabel14.Name = "metroLabel14";
            this.metroLabel14.Size = new System.Drawing.Size(115, 19);
            this.metroLabel14.TabIndex = 46;
            this.metroLabel14.Text = "Unit of Measure: ";
            // 
            // metroLabel15
            // 
            this.metroLabel15.AutoSize = true;
            this.metroLabel15.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel15.Location = new System.Drawing.Point(318, 203);
            this.metroLabel15.Name = "metroLabel15";
            this.metroLabel15.Size = new System.Drawing.Size(73, 19);
            this.metroLabel15.TabIndex = 47;
            this.metroLabel15.Text = "Sale Price: ";
            // 
            // metroLabel16
            // 
            this.metroLabel16.AutoSize = true;
            this.metroLabel16.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel16.Location = new System.Drawing.Point(26, 275);
            this.metroLabel16.Name = "metroLabel16";
            this.metroLabel16.Size = new System.Drawing.Size(70, 19);
            this.metroLabel16.TabIndex = 48;
            this.metroLabel16.Text = "Quantity: ";
            // 
            // metroLabel17
            // 
            this.metroLabel17.AutoSize = true;
            this.metroLabel17.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel17.Location = new System.Drawing.Point(317, 275);
            this.metroLabel17.Name = "metroLabel17";
            this.metroLabel17.Size = new System.Drawing.Size(89, 19);
            this.metroLabel17.TabIndex = 49;
            this.metroLabel17.Text = "Group/Price: ";
            // 
            // metroLabel18
            // 
            this.metroLabel18.AutoSize = true;
            this.metroLabel18.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel18.Location = new System.Drawing.Point(260, 557);
            this.metroLabel18.Name = "metroLabel18";
            this.metroLabel18.Size = new System.Drawing.Size(56, 19);
            this.metroLabel18.TabIndex = 50;
            this.metroLabel18.Text = "Labeled";
            // 
            // ToggleLabeled
            // 
            this.ToggleLabeled.AutoSize = true;
            this.ToggleLabeled.DisplayStatus = false;
            this.ToggleLabeled.Location = new System.Drawing.Point(326, 557);
            this.ToggleLabeled.Name = "ToggleLabeled";
            this.ToggleLabeled.Size = new System.Drawing.Size(50, 17);
            this.ToggleLabeled.TabIndex = 22;
            this.ToggleLabeled.Text = "Off";
            this.ToggleLabeled.UseSelectable = true;
            // 
            // metroLabel19
            // 
            this.metroLabel19.AutoSize = true;
            this.metroLabel19.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel19.Location = new System.Drawing.Point(318, 313);
            this.metroLabel19.Name = "metroLabel19";
            this.metroLabel19.Size = new System.Drawing.Size(78, 19);
            this.metroLabel19.TabIndex = 55;
            this.metroLabel19.Text = "Case Price: ";
            // 
            // metroLabel20
            // 
            this.metroLabel20.AutoSize = true;
            this.metroLabel20.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel20.Location = new System.Drawing.Point(27, 313);
            this.metroLabel20.Name = "metroLabel20";
            this.metroLabel20.Size = new System.Drawing.Size(103, 19);
            this.metroLabel20.TabIndex = 54;
            this.metroLabel20.Text = "Case Quantity: ";
            // 
            // txtCasePrice
            // 
            this.txtCasePrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCasePrice.CustomButton.Image = null;
            this.txtCasePrice.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtCasePrice.CustomButton.Name = "";
            this.txtCasePrice.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtCasePrice.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCasePrice.CustomButton.TabIndex = 1;
            this.txtCasePrice.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCasePrice.CustomButton.UseSelectable = true;
            this.txtCasePrice.CustomButton.Visible = false;
            this.txtCasePrice.DisplayIcon = true;
            this.txtCasePrice.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCasePrice.Icon = global::SFPOSWindows.Properties.Resources.dollar_coin_money1;
            this.txtCasePrice.IconRight = true;
            this.txtCasePrice.Lines = new string[0];
            this.txtCasePrice.Location = new System.Drawing.Point(415, 313);
            this.txtCasePrice.MaxLength = 20;
            this.txtCasePrice.Name = "txtCasePrice";
            this.txtCasePrice.PasswordChar = '\0';
            this.txtCasePrice.PromptText = "Case Price";
            this.txtCasePrice.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCasePrice.SelectedText = "";
            this.txtCasePrice.SelectionLength = 0;
            this.txtCasePrice.SelectionStart = 0;
            this.txtCasePrice.ShortcutsEnabled = true;
            this.txtCasePrice.Size = new System.Drawing.Size(166, 30);
            this.txtCasePrice.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCasePrice.TabIndex = 15;
            this.txtCasePrice.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCasePrice.UseSelectable = true;
            this.txtCasePrice.WaterMark = "Case Price";
            this.txtCasePrice.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCasePrice.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtCaseQty
            // 
            this.txtCaseQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCaseQty.CustomButton.Image = null;
            this.txtCaseQty.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtCaseQty.CustomButton.Name = "";
            this.txtCaseQty.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtCaseQty.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCaseQty.CustomButton.TabIndex = 1;
            this.txtCaseQty.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCaseQty.CustomButton.UseSelectable = true;
            this.txtCaseQty.CustomButton.Visible = false;
            this.txtCaseQty.DisplayIcon = true;
            this.txtCaseQty.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCaseQty.IconRight = true;
            this.txtCaseQty.Lines = new string[0];
            this.txtCaseQty.Location = new System.Drawing.Point(149, 313);
            this.txtCaseQty.MaxLength = 20;
            this.txtCaseQty.Name = "txtCaseQty";
            this.txtCaseQty.PasswordChar = '\0';
            this.txtCaseQty.PromptText = "Case Qty";
            this.txtCaseQty.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCaseQty.SelectedText = "";
            this.txtCaseQty.SelectionLength = 0;
            this.txtCaseQty.SelectionStart = 0;
            this.txtCaseQty.ShortcutsEnabled = true;
            this.txtCaseQty.Size = new System.Drawing.Size(166, 30);
            this.txtCaseQty.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCaseQty.TabIndex = 14;
            this.txtCaseQty.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCaseQty.UseSelectable = true;
            this.txtCaseQty.WaterMark = "Case Qty";
            this.txtCaseQty.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCaseQty.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel21
            // 
            this.metroLabel21.AutoSize = true;
            this.metroLabel21.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel21.Location = new System.Drawing.Point(26, 349);
            this.metroLabel21.Name = "metroLabel21";
            this.metroLabel21.Size = new System.Drawing.Size(74, 19);
            this.metroLabel21.TabIndex = 57;
            this.metroLabel21.Text = "Unit Cost: ";
            // 
            // txtUnitCost
            // 
            this.txtUnitCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtUnitCost.CustomButton.Image = null;
            this.txtUnitCost.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtUnitCost.CustomButton.Name = "";
            this.txtUnitCost.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtUnitCost.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUnitCost.CustomButton.TabIndex = 1;
            this.txtUnitCost.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUnitCost.CustomButton.UseSelectable = true;
            this.txtUnitCost.CustomButton.Visible = false;
            this.txtUnitCost.DisplayIcon = true;
            this.txtUnitCost.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUnitCost.IconRight = true;
            this.txtUnitCost.Lines = new string[0];
            this.txtUnitCost.Location = new System.Drawing.Point(149, 349);
            this.txtUnitCost.MaxLength = 20;
            this.txtUnitCost.Name = "txtUnitCost";
            this.txtUnitCost.PasswordChar = '\0';
            this.txtUnitCost.PromptText = "Unit Cost";
            this.txtUnitCost.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUnitCost.SelectedText = "";
            this.txtUnitCost.SelectionLength = 0;
            this.txtUnitCost.SelectionStart = 0;
            this.txtUnitCost.ShortcutsEnabled = true;
            this.txtUnitCost.Size = new System.Drawing.Size(166, 30);
            this.txtUnitCost.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUnitCost.TabIndex = 16;
            this.txtUnitCost.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUnitCost.UseSelectable = true;
            this.txtUnitCost.WaterMark = "Unit Cost";
            this.txtUnitCost.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUnitCost.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtUnitCost.TextChanged += new System.EventHandler(this.txtUnitCost_TextChanged);
            // 
            // metroLabel22
            // 
            this.metroLabel22.AutoSize = true;
            this.metroLabel22.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel22.Location = new System.Drawing.Point(319, 349);
            this.metroLabel22.Name = "metroLabel22";
            this.metroLabel22.Size = new System.Drawing.Size(96, 19);
            this.metroLabel22.TabIndex = 58;
            this.metroLabel22.Text = "Updated date:";
            // 
            // DtUpdateddate
            // 
            this.DtUpdateddate.Enabled = false;
            this.DtUpdateddate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtUpdateddate.Location = new System.Drawing.Point(415, 350);
            this.DtUpdateddate.MinimumSize = new System.Drawing.Size(0, 29);
            this.DtUpdateddate.Name = "DtUpdateddate";
            this.DtUpdateddate.Size = new System.Drawing.Size(165, 29);
            this.DtUpdateddate.TabIndex = 17;
            // 
            // metroLabel23
            // 
            this.metroLabel23.AutoSize = true;
            this.metroLabel23.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel23.Location = new System.Drawing.Point(319, 98);
            this.metroLabel23.Name = "metroLabel23";
            this.metroLabel23.Size = new System.Drawing.Size(60, 19);
            this.metroLabel23.TabIndex = 60;
            this.metroLabel23.Text = "Vendor: ";
            // 
            // cmbVendors
            // 
            this.cmbVendors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVendors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbVendors.FormattingEnabled = true;
            this.cmbVendors.ItemHeight = 23;
            this.cmbVendors.Location = new System.Drawing.Point(415, 98);
            this.cmbVendors.Name = "cmbVendors";
            this.cmbVendors.Size = new System.Drawing.Size(166, 29);
            this.cmbVendors.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbVendors.TabIndex = 3;
            this.cmbVendors.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbVendors.UseSelectable = true;
            this.cmbVendors.SelectedIndexChanged += new System.EventHandler(this.cmbVendors_SelectedIndexChanged);
            // 
            // metroBtnProductvendor
            // 
            this.metroBtnProductvendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnProductvendor.Enabled = false;
            this.metroBtnProductvendor.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnProductvendor.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnProductvendor.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnProductvendor.Location = new System.Drawing.Point(473, 31);
            this.metroBtnProductvendor.Name = "metroBtnProductvendor";
            this.metroBtnProductvendor.Size = new System.Drawing.Size(107, 26);
            this.metroBtnProductvendor.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnProductvendor.TabIndex = 62;
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
            this.metroBtnSalePrice.Location = new System.Drawing.Point(364, 31);
            this.metroBtnSalePrice.Name = "metroBtnSalePrice";
            this.metroBtnSalePrice.Size = new System.Drawing.Size(103, 26);
            this.metroBtnSalePrice.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnSalePrice.TabIndex = 61;
            this.metroBtnSalePrice.Text = "SALE PRICE";
            this.metroBtnSalePrice.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnSalePrice.UseCustomForeColor = true;
            this.metroBtnSalePrice.UseSelectable = true;
            this.metroBtnSalePrice.Click += new System.EventHandler(this.metroBtnSalePrice_Click);
            // 
            // ToggleGroupPrice
            // 
            this.ToggleGroupPrice.AutoSize = true;
            this.ToggleGroupPrice.DisplayStatus = false;
            this.ToggleGroupPrice.Location = new System.Drawing.Point(527, 559);
            this.ToggleGroupPrice.Name = "ToggleGroupPrice";
            this.ToggleGroupPrice.Size = new System.Drawing.Size(50, 17);
            this.ToggleGroupPrice.TabIndex = 23;
            this.ToggleGroupPrice.Text = "Off";
            this.ToggleGroupPrice.UseSelectable = true;
            this.ToggleGroupPrice.CheckedChanged += new System.EventHandler(this.ToggleGroupPrice_CheckedChanged);
            // 
            // metroLabel24
            // 
            this.metroLabel24.AutoSize = true;
            this.metroLabel24.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel24.Location = new System.Drawing.Point(416, 557);
            this.metroLabel24.Name = "metroLabel24";
            this.metroLabel24.Size = new System.Drawing.Size(81, 19);
            this.metroLabel24.TabIndex = 64;
            this.metroLabel24.Text = "Group Price";
            // 
            // lblPack
            // 
            this.lblPack.AutoSize = true;
            this.lblPack.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPack.Location = new System.Drawing.Point(26, 386);
            this.lblPack.Name = "lblPack";
            this.lblPack.Size = new System.Drawing.Size(40, 19);
            this.lblPack.TabIndex = 72;
            this.lblPack.Text = "Pack:";
            // 
            // metroLabel26
            // 
            this.metroLabel26.AutoSize = true;
            this.metroLabel26.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel26.Location = new System.Drawing.Point(324, 385);
            this.metroLabel26.Name = "metroLabel26";
            this.metroLabel26.Size = new System.Drawing.Size(39, 19);
            this.metroLabel26.TabIndex = 71;
            this.metroLabel26.Text = "Size: ";
            // 
            // txtPack
            // 
            this.txtPack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtPack.CustomButton.Image = null;
            this.txtPack.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtPack.CustomButton.Name = "";
            this.txtPack.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtPack.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPack.CustomButton.TabIndex = 1;
            this.txtPack.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPack.CustomButton.UseSelectable = true;
            this.txtPack.CustomButton.Visible = false;
            this.txtPack.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPack.Lines = new string[0];
            this.txtPack.Location = new System.Drawing.Point(149, 385);
            this.txtPack.MaxLength = 20;
            this.txtPack.Name = "txtPack";
            this.txtPack.PasswordChar = '\0';
            this.txtPack.PromptText = "Pack";
            this.txtPack.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPack.SelectedText = "";
            this.txtPack.SelectionLength = 0;
            this.txtPack.SelectionStart = 0;
            this.txtPack.ShortcutsEnabled = true;
            this.txtPack.Size = new System.Drawing.Size(166, 30);
            this.txtPack.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPack.TabIndex = 69;
            this.txtPack.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPack.UseSelectable = true;
            this.txtPack.WaterMark = "Pack";
            this.txtPack.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPack.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtSize
            // 
            // 
            // 
            // 
            this.txtSize.CustomButton.Image = null;
            this.txtSize.CustomButton.Location = new System.Drawing.Point(139, 2);
            this.txtSize.CustomButton.Name = "";
            this.txtSize.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtSize.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSize.CustomButton.TabIndex = 1;
            this.txtSize.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSize.CustomButton.UseSelectable = true;
            this.txtSize.CustomButton.Visible = false;
            this.txtSize.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSize.Lines = new string[0];
            this.txtSize.Location = new System.Drawing.Point(415, 385);
            this.txtSize.MaxLength = 20;
            this.txtSize.Name = "txtSize";
            this.txtSize.PasswordChar = '\0';
            this.txtSize.PromptText = "Size";
            this.txtSize.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSize.SelectedText = "";
            this.txtSize.SelectionLength = 0;
            this.txtSize.SelectionStart = 0;
            this.txtSize.ShortcutsEnabled = true;
            this.txtSize.Size = new System.Drawing.Size(167, 30);
            this.txtSize.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSize.TabIndex = 70;
            this.txtSize.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSize.UseSelectable = true;
            this.txtSize.WaterMark = "Size";
            this.txtSize.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSize.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel25
            // 
            this.metroLabel25.AutoSize = true;
            this.metroLabel25.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel25.Location = new System.Drawing.Point(26, 423);
            this.metroLabel25.Name = "metroLabel25";
            this.metroLabel25.Size = new System.Drawing.Size(104, 19);
            this.metroLabel25.TabIndex = 74;
            this.metroLabel25.Text = "Secondary PLU:";
            // 
            // txtSecondaryPLU
            // 
            this.txtSecondaryPLU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSecondaryPLU.CustomButton.Image = null;
            this.txtSecondaryPLU.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtSecondaryPLU.CustomButton.Name = "";
            this.txtSecondaryPLU.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtSecondaryPLU.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSecondaryPLU.CustomButton.TabIndex = 1;
            this.txtSecondaryPLU.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSecondaryPLU.CustomButton.UseSelectable = true;
            this.txtSecondaryPLU.CustomButton.Visible = false;
            this.txtSecondaryPLU.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSecondaryPLU.Lines = new string[0];
            this.txtSecondaryPLU.Location = new System.Drawing.Point(149, 421);
            this.txtSecondaryPLU.MaxLength = 20;
            this.txtSecondaryPLU.Name = "txtSecondaryPLU";
            this.txtSecondaryPLU.PasswordChar = '\0';
            this.txtSecondaryPLU.PromptText = "Secondary PLU";
            this.txtSecondaryPLU.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSecondaryPLU.SelectedText = "";
            this.txtSecondaryPLU.SelectionLength = 0;
            this.txtSecondaryPLU.SelectionStart = 0;
            this.txtSecondaryPLU.ShortcutsEnabled = true;
            this.txtSecondaryPLU.Size = new System.Drawing.Size(166, 30);
            this.txtSecondaryPLU.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSecondaryPLU.TabIndex = 73;
            this.txtSecondaryPLU.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSecondaryPLU.UseSelectable = true;
            this.txtSecondaryPLU.WaterMark = "Secondary PLU";
            this.txtSecondaryPLU.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSecondaryPLU.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel27
            // 
            this.metroLabel27.AutoSize = true;
            this.metroLabel27.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel27.Location = new System.Drawing.Point(319, 423);
            this.metroLabel27.Name = "metroLabel27";
            this.metroLabel27.Size = new System.Drawing.Size(74, 19);
            this.metroLabel27.TabIndex = 76;
            this.metroLabel27.Text = "Pallet QTY:";
            // 
            // txtPalletQTY
            // 
            this.txtPalletQTY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtPalletQTY.CustomButton.Image = null;
            this.txtPalletQTY.CustomButton.Location = new System.Drawing.Point(138, 2);
            this.txtPalletQTY.CustomButton.Name = "";
            this.txtPalletQTY.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtPalletQTY.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPalletQTY.CustomButton.TabIndex = 1;
            this.txtPalletQTY.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPalletQTY.CustomButton.UseSelectable = true;
            this.txtPalletQTY.CustomButton.Visible = false;
            this.txtPalletQTY.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPalletQTY.Lines = new string[0];
            this.txtPalletQTY.Location = new System.Drawing.Point(415, 421);
            this.txtPalletQTY.MaxLength = 20;
            this.txtPalletQTY.Name = "txtPalletQTY";
            this.txtPalletQTY.PasswordChar = '\0';
            this.txtPalletQTY.PromptText = "Pallet QTY";
            this.txtPalletQTY.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPalletQTY.SelectedText = "";
            this.txtPalletQTY.SelectionLength = 0;
            this.txtPalletQTY.SelectionStart = 0;
            this.txtPalletQTY.ShortcutsEnabled = true;
            this.txtPalletQTY.Size = new System.Drawing.Size(166, 30);
            this.txtPalletQTY.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPalletQTY.TabIndex = 75;
            this.txtPalletQTY.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPalletQTY.UseSelectable = true;
            this.txtPalletQTY.WaterMark = "Pallet QTY";
            this.txtPalletQTY.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPalletQTY.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel28
            // 
            this.metroLabel28.AutoSize = true;
            this.metroLabel28.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel28.Location = new System.Drawing.Point(23, 457);
            this.metroLabel28.Name = "metroLabel28";
            this.metroLabel28.Size = new System.Drawing.Size(124, 19);
            this.metroLabel28.TabIndex = 78;
            this.metroLabel28.Text = "Country of Origin: ";
            // 
            // txtCountryofOrig
            // 
            this.txtCountryofOrig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCountryofOrig.CustomButton.Image = null;
            this.txtCountryofOrig.CustomButton.Location = new System.Drawing.Point(139, 1);
            this.txtCountryofOrig.CustomButton.Name = "";
            this.txtCountryofOrig.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCountryofOrig.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCountryofOrig.CustomButton.TabIndex = 1;
            this.txtCountryofOrig.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCountryofOrig.CustomButton.UseSelectable = true;
            this.txtCountryofOrig.CustomButton.Visible = false;
            this.txtCountryofOrig.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCountryofOrig.Lines = new string[0];
            this.txtCountryofOrig.Location = new System.Drawing.Point(149, 457);
            this.txtCountryofOrig.MaxLength = 25;
            this.txtCountryofOrig.Name = "txtCountryofOrig";
            this.txtCountryofOrig.PasswordChar = '\0';
            this.txtCountryofOrig.PromptText = "Country of Origin";
            this.txtCountryofOrig.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCountryofOrig.SelectedText = "";
            this.txtCountryofOrig.SelectionLength = 0;
            this.txtCountryofOrig.SelectionStart = 0;
            this.txtCountryofOrig.ShortcutsEnabled = true;
            this.txtCountryofOrig.Size = new System.Drawing.Size(167, 29);
            this.txtCountryofOrig.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCountryofOrig.TabIndex = 77;
            this.txtCountryofOrig.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCountryofOrig.UseSelectable = true;
            this.txtCountryofOrig.WaterMark = "Country of Origin";
            this.txtCountryofOrig.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCountryofOrig.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel29
            // 
            this.metroLabel29.AutoSize = true;
            this.metroLabel29.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel29.Location = new System.Drawing.Point(319, 457);
            this.metroLabel29.Name = "metroLabel29";
            this.metroLabel29.Size = new System.Drawing.Size(126, 19);
            this.metroLabel29.TabIndex = 80;
            this.metroLabel29.Text = "FS Eligible Amount:";
            // 
            // txtFSEligibleAmount
            // 
            this.txtFSEligibleAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtFSEligibleAmount.CustomButton.Image = null;
            this.txtFSEligibleAmount.CustomButton.Location = new System.Drawing.Point(101, 2);
            this.txtFSEligibleAmount.CustomButton.Name = "";
            this.txtFSEligibleAmount.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtFSEligibleAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtFSEligibleAmount.CustomButton.TabIndex = 1;
            this.txtFSEligibleAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtFSEligibleAmount.CustomButton.UseSelectable = true;
            this.txtFSEligibleAmount.CustomButton.Visible = false;
            this.txtFSEligibleAmount.Enabled = false;
            this.txtFSEligibleAmount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtFSEligibleAmount.Lines = new string[0];
            this.txtFSEligibleAmount.Location = new System.Drawing.Point(451, 457);
            this.txtFSEligibleAmount.MaxLength = 20;
            this.txtFSEligibleAmount.Name = "txtFSEligibleAmount";
            this.txtFSEligibleAmount.PasswordChar = '\0';
            this.txtFSEligibleAmount.PromptText = "FS Eligible Amount";
            this.txtFSEligibleAmount.ReadOnly = true;
            this.txtFSEligibleAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFSEligibleAmount.SelectedText = "";
            this.txtFSEligibleAmount.SelectionLength = 0;
            this.txtFSEligibleAmount.SelectionStart = 0;
            this.txtFSEligibleAmount.ShortcutsEnabled = true;
            this.txtFSEligibleAmount.Size = new System.Drawing.Size(129, 30);
            this.txtFSEligibleAmount.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtFSEligibleAmount.TabIndex = 79;
            this.txtFSEligibleAmount.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtFSEligibleAmount.UseSelectable = true;
            this.txtFSEligibleAmount.WaterMark = "FS Eligible Amount";
            this.txtFSEligibleAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtFSEligibleAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtFSEligibleAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFSEligibleAmount_KeyUp);
            // 
            // btnChangeFSAmount
            // 
            this.btnChangeFSAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeFSAmount.Enabled = false;
            this.btnChangeFSAmount.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnChangeFSAmount.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnChangeFSAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnChangeFSAmount.Location = new System.Drawing.Point(416, 493);
            this.btnChangeFSAmount.Name = "btnChangeFSAmount";
            this.btnChangeFSAmount.Size = new System.Drawing.Size(164, 26);
            this.btnChangeFSAmount.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnChangeFSAmount.TabIndex = 81;
            this.btnChangeFSAmount.Text = "CHANGE FS AMOUNT";
            this.btnChangeFSAmount.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnChangeFSAmount.UseCustomForeColor = true;
            this.btnChangeFSAmount.UseSelectable = true;
            this.btnChangeFSAmount.Visible = false;
            this.btnChangeFSAmount.Click += new System.EventHandler(this.btnChangeFSAmount_Click);
            // 
            // FrmMetro_AddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 700);
            this.Controls.Add(this.btnChangeFSAmount);
            this.Controls.Add(this.metroLabel29);
            this.Controls.Add(this.txtFSEligibleAmount);
            this.Controls.Add(this.metroLabel28);
            this.Controls.Add(this.txtCountryofOrig);
            this.Controls.Add(this.metroLabel27);
            this.Controls.Add(this.txtPalletQTY);
            this.Controls.Add(this.metroLabel25);
            this.Controls.Add(this.txtSecondaryPLU);
            this.Controls.Add(this.lblPack);
            this.Controls.Add(this.metroLabel26);
            this.Controls.Add(this.txtPack);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.ToggleGroupPrice);
            this.Controls.Add(this.metroLabel24);
            this.Controls.Add(this.metroBtnProductvendor);
            this.Controls.Add(this.metroBtnSalePrice);
            this.Controls.Add(this.metroLabel23);
            this.Controls.Add(this.cmbVendors);
            this.Controls.Add(this.DtUpdateddate);
            this.Controls.Add(this.metroLabel22);
            this.Controls.Add(this.metroLabel21);
            this.Controls.Add(this.txtUnitCost);
            this.Controls.Add(this.metroLabel19);
            this.Controls.Add(this.metroLabel20);
            this.Controls.Add(this.txtCasePrice);
            this.Controls.Add(this.txtCaseQty);
            this.Controls.Add(this.ToggleLabeled);
            this.Controls.Add(this.metroLabel18);
            this.Controls.Add(this.metroLabel17);
            this.Controls.Add(this.metroLabel16);
            this.Controls.Add(this.metroLabel15);
            this.Controls.Add(this.metroLabel14);
            this.Controls.Add(this.metroLabel13);
            this.Controls.Add(this.metroLabel12);
            this.Controls.Add(this.metroLabel11);
            this.Controls.Add(this.metroLabel10);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.metroLabel8);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.ToggleScaled);
            this.Controls.Add(this.txtTareWeight);
            this.Controls.Add(this.txtLinkUPCCode);
            this.Controls.Add(this.txtGroupPrice);
            this.Controls.Add(this.txtGroupQty);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.ToggleAgeVerify);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.toggleActive);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.toggleFdStamp);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.ImageProduct);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.cmbTaxGroup);
            this.Controls.Add(this.cmbUoM);
            this.Controls.Add(this.txtCertificateCode);
            this.Controls.Add(this.cmbSection);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.txtUPCCode);
            this.Controls.Add(this.txtProductName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(611, 700);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(611, 700);
            this.Name = "FrmMetro_AddProduct";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMetro_AddProduct_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ImageProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public MetroFramework.Controls.MetroComboBox cmbSection;
        public MetroFramework.Controls.MetroComboBox cmbDepartment;
        public MetroFramework.Controls.MetroTextBox txtUPCCode;
        public MetroFramework.Controls.MetroTextBox txtProductName;
        public MetroFramework.Controls.MetroTextBox txtCertificateCode;
        public MetroFramework.Controls.MetroComboBox cmbUoM;
        public MetroFramework.Controls.MetroComboBox cmbTaxGroup;
        public MetroFramework.Controls.MetroTextBox txtPrice;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        private MetroFramework.Controls.MetroButton btnBrowse;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        public System.Windows.Forms.PictureBox ImageProduct;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        public MetroFramework.Controls.MetroToggle toggleFdStamp;
        public MetroFramework.Controls.MetroToggle toggleActive;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        public MetroFramework.Controls.MetroToggle ToggleAgeVerify;
        public MetroFramework.Controls.MetroTextBox txtGroupQty;
        public MetroFramework.Controls.MetroTextBox txtGroupPrice;
        public MetroFramework.Controls.MetroTextBox txtLinkUPCCode;
        public MetroFramework.Controls.MetroTextBox txtTareWeight;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        public MetroFramework.Controls.MetroToggle ToggleScaled;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        private MetroFramework.Controls.MetroLabel metroLabel13;
        private MetroFramework.Controls.MetroLabel metroLabel14;
        private MetroFramework.Controls.MetroLabel metroLabel15;
        private MetroFramework.Controls.MetroLabel metroLabel16;
        private MetroFramework.Controls.MetroLabel metroLabel17;
        private MetroFramework.Controls.MetroLabel metroLabel18;
        public MetroFramework.Controls.MetroToggle ToggleLabeled;
        private MetroFramework.Controls.MetroLabel metroLabel19;
        private MetroFramework.Controls.MetroLabel metroLabel20;
        public MetroFramework.Controls.MetroTextBox txtCasePrice;
        public MetroFramework.Controls.MetroTextBox txtCaseQty;
        private MetroFramework.Controls.MetroLabel metroLabel21;
        public MetroFramework.Controls.MetroTextBox txtUnitCost;
        private MetroFramework.Controls.MetroLabel metroLabel22;
        private MetroFramework.Controls.MetroLabel metroLabel23;
        public MetroFramework.Controls.MetroComboBox cmbVendors;
        public MetroFramework.Controls.MetroButton metroBtnProductvendor;
        public MetroFramework.Controls.MetroButton metroBtnSalePrice;
        public MetroFramework.Controls.MetroToggle ToggleGroupPrice;
        private MetroFramework.Controls.MetroLabel metroLabel24;
        private MetroFramework.Controls.MetroLabel lblPack;
        private MetroFramework.Controls.MetroLabel metroLabel26;
        public MetroFramework.Controls.MetroTextBox txtPack;
        public MetroFramework.Controls.MetroTextBox txtSize;
        private MetroFramework.Controls.MetroLabel metroLabel25;
        public MetroFramework.Controls.MetroTextBox txtSecondaryPLU;
        private MetroFramework.Controls.MetroLabel metroLabel27;
        public MetroFramework.Controls.MetroTextBox txtPalletQTY;
        public MetroFramework.Controls.MetroDateTime DtUpdateddate;
        private MetroFramework.Controls.MetroLabel metroLabel28;
        public MetroFramework.Controls.MetroTextBox txtCountryofOrig;
        private MetroFramework.Controls.MetroLabel metroLabel29;
        public MetroFramework.Controls.MetroTextBox txtFSEligibleAmount;
        public MetroFramework.Controls.MetroButton btnChangeFSAmount;
    }
}