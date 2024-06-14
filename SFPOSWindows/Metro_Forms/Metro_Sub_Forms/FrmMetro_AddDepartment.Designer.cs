namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddDepartment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddDepartment));
            this.txtDepartmentName = new MetroFramework.Controls.MetroTextBox();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.ToggleIsActive = new MetroFramework.Controls.MetroToggle();
            this.txtAgeVerificationAge = new MetroFramework.Controls.MetroTextBox();
            this.toggleFdStamp = new MetroFramework.Controls.MetroToggle();
            this.cmbTaxGroup = new MetroFramework.Controls.MetroComboBox();
            this.cmbUoM = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.txtDepartmentNo = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.txtSubNo = new MetroFramework.Controls.MetroTextBox();
            this.toggleBtnActive = new MetroFramework.Controls.MetroToggle();
            this.txtDeptBtnCode = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.txtDeptBtnCodeIndex = new MetroFramework.Controls.MetroTextBox();
            this.toggleIsForceTax = new MetroFramework.Controls.MetroToggle();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.txtForcedTaxSuffix = new MetroFramework.Controls.MetroTextBox();
            this.chkIsForceTax = new System.Windows.Forms.CheckBox();
            this.lblActive = new MetroFramework.Controls.MetroLabel();
            this.metroLabel13 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel14 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.cmbSection = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // txtDepartmentName
            // 
            this.txtDepartmentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDepartmentName.CustomButton.Image = null;
            this.txtDepartmentName.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.txtDepartmentName.CustomButton.Name = "";
            this.txtDepartmentName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtDepartmentName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDepartmentName.CustomButton.TabIndex = 1;
            this.txtDepartmentName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDepartmentName.CustomButton.UseSelectable = true;
            this.txtDepartmentName.CustomButton.Visible = false;
            this.txtDepartmentName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDepartmentName.Lines = new string[0];
            this.txtDepartmentName.Location = new System.Drawing.Point(167, 81);
            this.txtDepartmentName.MaxLength = 20;
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.PasswordChar = '\0';
            this.txtDepartmentName.PromptText = "Name";
            this.txtDepartmentName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDepartmentName.SelectedText = "";
            this.txtDepartmentName.SelectionLength = 0;
            this.txtDepartmentName.SelectionStart = 0;
            this.txtDepartmentName.ShortcutsEnabled = true;
            this.txtDepartmentName.Size = new System.Drawing.Size(197, 29);
            this.txtDepartmentName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDepartmentName.TabIndex = 0;
            this.txtDepartmentName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDepartmentName.UseSelectable = true;
            this.txtDepartmentName.WaterMark = "Name";
            this.txtDepartmentName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDepartmentName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtDepartmentName.TextChanged += new System.EventHandler(this.txtDepartmentName_TextChanged);
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(199, 553);
            this.metroBtnClear.Name = "metroBtnClear";
            this.metroBtnClear.Size = new System.Drawing.Size(92, 31);
            this.metroBtnClear.TabIndex = 7;
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
            this.MetrobtnSave.Location = new System.Drawing.Point(86, 553);
            this.MetrobtnSave.Name = "MetrobtnSave";
            this.MetrobtnSave.Size = new System.Drawing.Size(92, 31);
            this.MetrobtnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnSave.TabIndex = 6;
            this.MetrobtnSave.Text = "SAVE";
            this.MetrobtnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnSave.UseCustomForeColor = true;
            this.MetrobtnSave.UseSelectable = true;
            this.MetrobtnSave.Click += new System.EventHandler(this.MetrobtnSave_Click);
            // 
            // ToggleIsActive
            // 
            this.ToggleIsActive.AutoSize = true;
            this.ToggleIsActive.Checked = true;
            this.ToggleIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToggleIsActive.DisplayStatus = false;
            this.ToggleIsActive.Location = new System.Drawing.Point(55, 517);
            this.ToggleIsActive.Name = "ToggleIsActive";
            this.ToggleIsActive.Size = new System.Drawing.Size(50, 17);
            this.ToggleIsActive.TabIndex = 4;
            this.ToggleIsActive.Text = "On";
            this.ToggleIsActive.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ToggleIsActive.UseSelectable = true;
            this.ToggleIsActive.CheckedChanged += new System.EventHandler(this.ToggleIsActive_CheckedChanged);
            // 
            // txtAgeVerificationAge
            // 
            this.txtAgeVerificationAge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtAgeVerificationAge.CustomButton.Image = null;
            this.txtAgeVerificationAge.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.txtAgeVerificationAge.CustomButton.Name = "";
            this.txtAgeVerificationAge.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtAgeVerificationAge.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtAgeVerificationAge.CustomButton.TabIndex = 1;
            this.txtAgeVerificationAge.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtAgeVerificationAge.CustomButton.UseSelectable = true;
            this.txtAgeVerificationAge.CustomButton.Visible = false;
            this.txtAgeVerificationAge.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtAgeVerificationAge.Lines = new string[0];
            this.txtAgeVerificationAge.Location = new System.Drawing.Point(167, 127);
            this.txtAgeVerificationAge.MaxLength = 7;
            this.txtAgeVerificationAge.Name = "txtAgeVerificationAge";
            this.txtAgeVerificationAge.PasswordChar = '\0';
            this.txtAgeVerificationAge.PromptText = "Age Verification Age";
            this.txtAgeVerificationAge.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAgeVerificationAge.SelectedText = "";
            this.txtAgeVerificationAge.SelectionLength = 0;
            this.txtAgeVerificationAge.SelectionStart = 0;
            this.txtAgeVerificationAge.ShortcutsEnabled = true;
            this.txtAgeVerificationAge.Size = new System.Drawing.Size(197, 29);
            this.txtAgeVerificationAge.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtAgeVerificationAge.TabIndex = 1;
            this.txtAgeVerificationAge.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtAgeVerificationAge.UseSelectable = true;
            this.txtAgeVerificationAge.WaterMark = "Age Verification Age";
            this.txtAgeVerificationAge.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtAgeVerificationAge.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // toggleFdStamp
            // 
            this.toggleFdStamp.AutoSize = true;
            this.toggleFdStamp.DisplayStatus = false;
            this.toggleFdStamp.Location = new System.Drawing.Point(333, 517);
            this.toggleFdStamp.Name = "toggleFdStamp";
            this.toggleFdStamp.Size = new System.Drawing.Size(50, 17);
            this.toggleFdStamp.TabIndex = 5;
            this.toggleFdStamp.Text = "Off";
            this.toggleFdStamp.UseSelectable = true;
            // 
            // cmbTaxGroup
            // 
            this.cmbTaxGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTaxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbTaxGroup.FormattingEnabled = true;
            this.cmbTaxGroup.ItemHeight = 23;
            this.cmbTaxGroup.Location = new System.Drawing.Point(167, 217);
            this.cmbTaxGroup.Name = "cmbTaxGroup";
            this.cmbTaxGroup.Size = new System.Drawing.Size(197, 29);
            this.cmbTaxGroup.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbTaxGroup.TabIndex = 3;
            this.cmbTaxGroup.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbTaxGroup.UseSelectable = true;
            // 
            // cmbUoM
            // 
            this.cmbUoM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUoM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbUoM.FormattingEnabled = true;
            this.cmbUoM.ItemHeight = 23;
            this.cmbUoM.Location = new System.Drawing.Point(167, 173);
            this.cmbUoM.Name = "cmbUoM";
            this.cmbUoM.Size = new System.Drawing.Size(197, 29);
            this.cmbUoM.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbUoM.TabIndex = 2;
            this.cmbUoM.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbUoM.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(25, 127);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(134, 19);
            this.metroLabel1.TabIndex = 32;
            this.metroLabel1.Text = "Age verification Age:";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(23, 81);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(126, 19);
            this.metroLabel3.TabIndex = 33;
            this.metroLabel3.Text = "Department Name:";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(25, 173);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(114, 19);
            this.metroLabel4.TabIndex = 34;
            this.metroLabel4.Text = "Unit Of Measure:";
            this.metroLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(25, 217);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(30, 19);
            this.metroLabel5.TabIndex = 35;
            this.metroLabel5.Text = "Tax:";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.Location = new System.Drawing.Point(25, 263);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(111, 19);
            this.metroLabel6.TabIndex = 37;
            this.metroLabel6.Text = "Department No.:";
            // 
            // txtDepartmentNo
            // 
            this.txtDepartmentNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDepartmentNo.CustomButton.Image = null;
            this.txtDepartmentNo.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.txtDepartmentNo.CustomButton.Name = "";
            this.txtDepartmentNo.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtDepartmentNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDepartmentNo.CustomButton.TabIndex = 1;
            this.txtDepartmentNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDepartmentNo.CustomButton.UseSelectable = true;
            this.txtDepartmentNo.CustomButton.Visible = false;
            this.txtDepartmentNo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDepartmentNo.Lines = new string[0];
            this.txtDepartmentNo.Location = new System.Drawing.Point(167, 263);
            this.txtDepartmentNo.MaxLength = 7;
            this.txtDepartmentNo.Name = "txtDepartmentNo";
            this.txtDepartmentNo.PasswordChar = '\0';
            this.txtDepartmentNo.PromptText = "Department No";
            this.txtDepartmentNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDepartmentNo.SelectedText = "";
            this.txtDepartmentNo.SelectionLength = 0;
            this.txtDepartmentNo.SelectionStart = 0;
            this.txtDepartmentNo.ShortcutsEnabled = true;
            this.txtDepartmentNo.Size = new System.Drawing.Size(197, 29);
            this.txtDepartmentNo.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDepartmentNo.TabIndex = 36;
            this.txtDepartmentNo.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDepartmentNo.UseSelectable = true;
            this.txtDepartmentNo.WaterMark = "Department No";
            this.txtDepartmentNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDepartmentNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.Location = new System.Drawing.Point(24, 306);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(60, 19);
            this.metroLabel7.TabIndex = 39;
            this.metroLabel7.Text = "Sub No.:";
            // 
            // txtSubNo
            // 
            this.txtSubNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSubNo.CustomButton.Image = null;
            this.txtSubNo.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.txtSubNo.CustomButton.Name = "";
            this.txtSubNo.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSubNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSubNo.CustomButton.TabIndex = 1;
            this.txtSubNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSubNo.CustomButton.UseSelectable = true;
            this.txtSubNo.CustomButton.Visible = false;
            this.txtSubNo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSubNo.Lines = new string[0];
            this.txtSubNo.Location = new System.Drawing.Point(166, 306);
            this.txtSubNo.MaxLength = 7;
            this.txtSubNo.Name = "txtSubNo";
            this.txtSubNo.PasswordChar = '\0';
            this.txtSubNo.PromptText = "Sub No";
            this.txtSubNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSubNo.SelectedText = "";
            this.txtSubNo.SelectionLength = 0;
            this.txtSubNo.SelectionStart = 0;
            this.txtSubNo.ShortcutsEnabled = true;
            this.txtSubNo.Size = new System.Drawing.Size(197, 29);
            this.txtSubNo.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSubNo.TabIndex = 38;
            this.txtSubNo.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSubNo.UseSelectable = true;
            this.txtSubNo.WaterMark = "Sub No";
            this.txtSubNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSubNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // toggleBtnActive
            // 
            this.toggleBtnActive.AutoSize = true;
            this.toggleBtnActive.DisplayStatus = false;
            this.toggleBtnActive.Location = new System.Drawing.Point(192, 517);
            this.toggleBtnActive.Name = "toggleBtnActive";
            this.toggleBtnActive.Size = new System.Drawing.Size(50, 17);
            this.toggleBtnActive.TabIndex = 5;
            this.toggleBtnActive.Text = "Off";
            this.toggleBtnActive.UseSelectable = true;
            this.toggleBtnActive.CheckedChanged += new System.EventHandler(this.toggleBtnActive_CheckedChanged);
            // 
            // txtDeptBtnCode
            // 
            this.txtDeptBtnCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDeptBtnCode.CustomButton.Image = null;
            this.txtDeptBtnCode.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.txtDeptBtnCode.CustomButton.Name = "";
            this.txtDeptBtnCode.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtDeptBtnCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDeptBtnCode.CustomButton.TabIndex = 1;
            this.txtDeptBtnCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDeptBtnCode.CustomButton.UseSelectable = true;
            this.txtDeptBtnCode.CustomButton.Visible = false;
            this.txtDeptBtnCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDeptBtnCode.Lines = new string[0];
            this.txtDeptBtnCode.Location = new System.Drawing.Point(167, 351);
            this.txtDeptBtnCode.MaxLength = 7;
            this.txtDeptBtnCode.Name = "txtDeptBtnCode";
            this.txtDeptBtnCode.PasswordChar = '\0';
            this.txtDeptBtnCode.PromptText = "Department Button Code";
            this.txtDeptBtnCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDeptBtnCode.SelectedText = "";
            this.txtDeptBtnCode.SelectionLength = 0;
            this.txtDeptBtnCode.SelectionStart = 0;
            this.txtDeptBtnCode.ShortcutsEnabled = true;
            this.txtDeptBtnCode.Size = new System.Drawing.Size(197, 29);
            this.txtDeptBtnCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDeptBtnCode.TabIndex = 38;
            this.txtDeptBtnCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDeptBtnCode.UseSelectable = true;
            this.txtDeptBtnCode.WaterMark = "Department Button Code";
            this.txtDeptBtnCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDeptBtnCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel9.Location = new System.Drawing.Point(25, 351);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(136, 19);
            this.metroLabel9.TabIndex = 39;
            this.metroLabel9.Text = "DepartmentBtnCode";
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel10.Location = new System.Drawing.Point(24, 390);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(137, 19);
            this.metroLabel10.TabIndex = 41;
            this.metroLabel10.Text = "DepartmentBtnIndex";
            // 
            // txtDeptBtnCodeIndex
            // 
            this.txtDeptBtnCodeIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDeptBtnCodeIndex.CustomButton.Image = null;
            this.txtDeptBtnCodeIndex.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.txtDeptBtnCodeIndex.CustomButton.Name = "";
            this.txtDeptBtnCodeIndex.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtDeptBtnCodeIndex.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDeptBtnCodeIndex.CustomButton.TabIndex = 1;
            this.txtDeptBtnCodeIndex.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDeptBtnCodeIndex.CustomButton.UseSelectable = true;
            this.txtDeptBtnCodeIndex.CustomButton.Visible = false;
            this.txtDeptBtnCodeIndex.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDeptBtnCodeIndex.Lines = new string[0];
            this.txtDeptBtnCodeIndex.Location = new System.Drawing.Point(166, 390);
            this.txtDeptBtnCodeIndex.MaxLength = 7;
            this.txtDeptBtnCodeIndex.Name = "txtDeptBtnCodeIndex";
            this.txtDeptBtnCodeIndex.PasswordChar = '\0';
            this.txtDeptBtnCodeIndex.PromptText = "Department Button Index";
            this.txtDeptBtnCodeIndex.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDeptBtnCodeIndex.SelectedText = "";
            this.txtDeptBtnCodeIndex.SelectionLength = 0;
            this.txtDeptBtnCodeIndex.SelectionStart = 0;
            this.txtDeptBtnCodeIndex.ShortcutsEnabled = true;
            this.txtDeptBtnCodeIndex.Size = new System.Drawing.Size(197, 29);
            this.txtDeptBtnCodeIndex.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDeptBtnCodeIndex.TabIndex = 40;
            this.txtDeptBtnCodeIndex.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDeptBtnCodeIndex.UseSelectable = true;
            this.txtDeptBtnCodeIndex.WaterMark = "Department Button Index";
            this.txtDeptBtnCodeIndex.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDeptBtnCodeIndex.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // toggleIsForceTax
            // 
            this.toggleIsForceTax.AutoSize = true;
            this.toggleIsForceTax.DisplayStatus = false;
            this.toggleIsForceTax.Location = new System.Drawing.Point(215, 553);
            this.toggleIsForceTax.Name = "toggleIsForceTax";
            this.toggleIsForceTax.Size = new System.Drawing.Size(50, 17);
            this.toggleIsForceTax.TabIndex = 42;
            this.toggleIsForceTax.Text = "Off";
            this.toggleIsForceTax.UseSelectable = true;
            this.toggleIsForceTax.Visible = false;
            // 
            // metroLabel12
            // 
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel12.Location = new System.Drawing.Point(23, 431);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(120, 19);
            this.metroLabel12.TabIndex = 45;
            this.metroLabel12.Text = "Forced Tax Section";
            // 
            // txtForcedTaxSuffix
            // 
            this.txtForcedTaxSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtForcedTaxSuffix.CustomButton.Image = null;
            this.txtForcedTaxSuffix.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.txtForcedTaxSuffix.CustomButton.Name = "";
            this.txtForcedTaxSuffix.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtForcedTaxSuffix.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtForcedTaxSuffix.CustomButton.TabIndex = 1;
            this.txtForcedTaxSuffix.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtForcedTaxSuffix.CustomButton.UseSelectable = true;
            this.txtForcedTaxSuffix.CustomButton.Visible = false;
            this.txtForcedTaxSuffix.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtForcedTaxSuffix.Lines = new string[0];
            this.txtForcedTaxSuffix.Location = new System.Drawing.Point(166, 466);
            this.txtForcedTaxSuffix.MaxLength = 100;
            this.txtForcedTaxSuffix.Name = "txtForcedTaxSuffix";
            this.txtForcedTaxSuffix.PasswordChar = '\0';
            this.txtForcedTaxSuffix.PromptText = "Forced Tax Suffix";
            this.txtForcedTaxSuffix.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtForcedTaxSuffix.SelectedText = "";
            this.txtForcedTaxSuffix.SelectionLength = 0;
            this.txtForcedTaxSuffix.SelectionStart = 0;
            this.txtForcedTaxSuffix.ShortcutsEnabled = true;
            this.txtForcedTaxSuffix.Size = new System.Drawing.Size(197, 29);
            this.txtForcedTaxSuffix.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtForcedTaxSuffix.TabIndex = 44;
            this.txtForcedTaxSuffix.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtForcedTaxSuffix.UseSelectable = true;
            this.txtForcedTaxSuffix.WaterMark = "Forced Tax Suffix";
            this.txtForcedTaxSuffix.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtForcedTaxSuffix.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // chkIsForceTax
            // 
            this.chkIsForceTax.AutoSize = true;
            this.chkIsForceTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsForceTax.Location = new System.Drawing.Point(215, 563);
            this.chkIsForceTax.Name = "chkIsForceTax";
            this.chkIsForceTax.Size = new System.Drawing.Size(71, 21);
            this.chkIsForceTax.TabIndex = 47;
            this.chkIsForceTax.Text = "Forced";
            this.chkIsForceTax.UseVisualStyleBackColor = true;
            this.chkIsForceTax.Visible = false;
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblActive.Location = new System.Drawing.Point(3, 515);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(46, 19);
            this.lblActive.TabIndex = 48;
            this.lblActive.Text = "Active";
            // 
            // metroLabel13
            // 
            this.metroLabel13.AutoSize = true;
            this.metroLabel13.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel13.Location = new System.Drawing.Point(111, 515);
            this.metroLabel13.Name = "metroLabel13";
            this.metroLabel13.Size = new System.Drawing.Size(67, 19);
            this.metroLabel13.TabIndex = 49;
            this.metroLabel13.Text = "BtnActive";
            // 
            // metroLabel14
            // 
            this.metroLabel14.AutoSize = true;
            this.metroLabel14.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel14.Location = new System.Drawing.Point(248, 515);
            this.metroLabel14.Name = "metroLabel14";
            this.metroLabel14.Size = new System.Drawing.Size(79, 19);
            this.metroLabel14.TabIndex = 50;
            this.metroLabel14.Text = "FoodStamp";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(22, 473);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(108, 19);
            this.metroLabel2.TabIndex = 51;
            this.metroLabel2.Text = "Forced Tax Suffix";
            // 
            // cmbSection
            // 
            this.cmbSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.ItemHeight = 23;
            this.cmbSection.Location = new System.Drawing.Point(166, 427);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(197, 29);
            this.cmbSection.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbSection.TabIndex = 52;
            this.cmbSection.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbSection.UseSelectable = true;
            // 
            // FrmMetro_AddDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 600);
            this.Controls.Add(this.cmbSection);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.metroLabel14);
            this.Controls.Add(this.metroLabel13);
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.chkIsForceTax);
            this.Controls.Add(this.metroLabel12);
            this.Controls.Add(this.txtForcedTaxSuffix);
            this.Controls.Add(this.metroLabel10);
            this.Controls.Add(this.txtDeptBtnCodeIndex);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.txtDeptBtnCode);
            this.Controls.Add(this.txtSubNo);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.txtDepartmentNo);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.cmbTaxGroup);
            this.Controls.Add(this.cmbUoM);
            this.Controls.Add(this.toggleBtnActive);
            this.Controls.Add(this.toggleFdStamp);
            this.Controls.Add(this.txtAgeVerificationAge);
            this.Controls.Add(this.ToggleIsActive);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.txtDepartmentName);
            this.Controls.Add(this.toggleIsForceTax);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(396, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(396, 600);
            this.Name = "FrmMetro_AddDepartment";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Department";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroTextBox txtDepartmentName;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        public MetroFramework.Controls.MetroToggle ToggleIsActive;
        public MetroFramework.Controls.MetroTextBox txtAgeVerificationAge;
        public MetroFramework.Controls.MetroToggle toggleFdStamp;
        public MetroFramework.Controls.MetroComboBox cmbTaxGroup;
        public MetroFramework.Controls.MetroComboBox cmbUoM;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        public MetroFramework.Controls.MetroTextBox txtDepartmentNo;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        public MetroFramework.Controls.MetroTextBox txtSubNo;
        public MetroFramework.Controls.MetroToggle toggleBtnActive;
        public MetroFramework.Controls.MetroTextBox txtDeptBtnCode;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        public MetroFramework.Controls.MetroTextBox txtDeptBtnCodeIndex;
        public MetroFramework.Controls.MetroToggle toggleIsForceTax;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        public MetroFramework.Controls.MetroTextBox txtForcedTaxSuffix;
        private MetroFramework.Controls.MetroLabel lblActive;
        private MetroFramework.Controls.MetroLabel metroLabel13;
        private MetroFramework.Controls.MetroLabel metroLabel14;
        public System.Windows.Forms.CheckBox chkIsForceTax;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        public MetroFramework.Controls.MetroComboBox cmbSection;
    }
}