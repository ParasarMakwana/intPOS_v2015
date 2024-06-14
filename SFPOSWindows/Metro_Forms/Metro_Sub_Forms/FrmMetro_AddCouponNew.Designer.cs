namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddCouponNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddCouponNew));
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.toggleIsMulUser = new MetroFramework.Controls.MetroToggle();
            this.metroCustRest = new MetroFramework.Controls.MetroToggle();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroAllDep = new MetroFramework.Controls.MetroToggle();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroSelectedDep = new MetroFramework.Controls.MetroToggle();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.txtDiscount = new MetroFramework.Controls.MetroTextBox();
            this.datePickerStartDate = new MetroFramework.Controls.MetroDateTime();
            this.txtCouponName = new MetroFramework.Controls.MetroTextBox();
            this.datePickerEndDate = new MetroFramework.Controls.MetroDateTime();
            this.txtMinPurAmt = new MetroFramework.Controls.MetroTextBox();
            this.txtCouponCode = new MetroFramework.Controls.MetroTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAllowDepartment = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(23, 89);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(97, 19);
            this.metroLabel3.TabIndex = 35;
            this.metroLabel3.Text = "Coupon Code:";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(23, 194);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(68, 19);
            this.metroLabel4.TabIndex = 41;
            this.metroLabel4.Text = "End Date:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(23, 159);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(74, 19);
            this.metroLabel2.TabIndex = 40;
            this.metroLabel2.Text = "Start Date:";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 229);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(125, 19);
            this.metroLabel1.TabIndex = 43;
            this.metroLabel1.Text = "Min Purchase Amt:";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(23, 264);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(85, 19);
            this.metroLabel5.TabIndex = 45;
            this.metroLabel5.Text = "Discount(%):";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.Location = new System.Drawing.Point(23, 124);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(101, 19);
            this.metroLabel6.TabIndex = 47;
            this.metroLabel6.Text = "Coupon Name:";
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(336, 395);
            this.metroBtnClear.Name = "metroBtnClear";
            this.metroBtnClear.Size = new System.Drawing.Size(92, 31);
            this.metroBtnClear.TabIndex = 11;
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
            this.MetrobtnSave.Location = new System.Drawing.Point(225, 395);
            this.MetrobtnSave.Name = "MetrobtnSave";
            this.MetrobtnSave.Size = new System.Drawing.Size(92, 31);
            this.MetrobtnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnSave.TabIndex = 10;
            this.MetrobtnSave.Text = "SAVE";
            this.MetrobtnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnSave.UseCustomForeColor = true;
            this.MetrobtnSave.UseSelectable = true;
            this.MetrobtnSave.Click += new System.EventHandler(this.MetrobtnSave_Click);
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.Location = new System.Drawing.Point(23, 307);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(126, 19);
            this.metroLabel7.TabIndex = 48;
            this.metroLabel7.Text = "Allow Multiple Use:";
            // 
            // toggleIsMulUser
            // 
            this.toggleIsMulUser.AutoSize = true;
            this.toggleIsMulUser.DisplayStatus = false;
            this.toggleIsMulUser.Location = new System.Drawing.Point(163, 310);
            this.toggleIsMulUser.Name = "toggleIsMulUser";
            this.toggleIsMulUser.Size = new System.Drawing.Size(50, 17);
            this.toggleIsMulUser.TabIndex = 49;
            this.toggleIsMulUser.Text = "Off";
            this.toggleIsMulUser.UseSelectable = true;
            this.toggleIsMulUser.CheckedChanged += new System.EventHandler(this.toggleIsMulUser_CheckedChanged);
            // 
            // metroCustRest
            // 
            this.metroCustRest.AutoSize = true;
            this.metroCustRest.DisplayStatus = false;
            this.metroCustRest.Location = new System.Drawing.Point(163, 344);
            this.metroCustRest.Name = "metroCustRest";
            this.metroCustRest.Size = new System.Drawing.Size(50, 17);
            this.metroCustRest.TabIndex = 51;
            this.metroCustRest.Text = "Off";
            this.metroCustRest.UseSelectable = true;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel8.Location = new System.Drawing.Point(23, 341);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(140, 19);
            this.metroLabel8.TabIndex = 50;
            this.metroLabel8.Text = "Customer Restricted :";
            // 
            // metroAllDep
            // 
            this.metroAllDep.AutoSize = true;
            this.metroAllDep.DisplayStatus = false;
            this.metroAllDep.Location = new System.Drawing.Point(378, 309);
            this.metroAllDep.Name = "metroAllDep";
            this.metroAllDep.Size = new System.Drawing.Size(50, 17);
            this.metroAllDep.TabIndex = 53;
            this.metroAllDep.Text = "Off";
            this.metroAllDep.UseSelectable = true;
            this.metroAllDep.CheckedChanged += new System.EventHandler(this.metroAllDep_CheckedChanged);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel9.Location = new System.Drawing.Point(233, 308);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(109, 19);
            this.metroLabel9.TabIndex = 52;
            this.metroLabel9.Text = "All Department :";
            // 
            // metroSelectedDep
            // 
            this.metroSelectedDep.AutoSize = true;
            this.metroSelectedDep.DisplayStatus = false;
            this.metroSelectedDep.Location = new System.Drawing.Point(378, 343);
            this.metroSelectedDep.Name = "metroSelectedDep";
            this.metroSelectedDep.Size = new System.Drawing.Size(50, 17);
            this.metroSelectedDep.TabIndex = 55;
            this.metroSelectedDep.Text = "Off";
            this.metroSelectedDep.UseSelectable = true;
            this.metroSelectedDep.CheckedChanged += new System.EventHandler(this.metroSelectedDep_CheckedChanged);
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel10.Location = new System.Drawing.Point(233, 342);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(144, 19);
            this.metroLabel10.TabIndex = 54;
            this.metroLabel10.Text = "Selected Department :";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDiscount.CustomButton.Image = null;
            this.txtDiscount.CustomButton.Location = new System.Drawing.Point(237, 1);
            this.txtDiscount.CustomButton.Name = "";
            this.txtDiscount.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtDiscount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDiscount.CustomButton.TabIndex = 1;
            this.txtDiscount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDiscount.CustomButton.UseSelectable = true;
            this.txtDiscount.CustomButton.Visible = false;
            this.txtDiscount.DisplayIcon = true;
            this.txtDiscount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDiscount.Icon = global::SFPOSWindows.Properties.Resources.percentage1;
            this.txtDiscount.IconRight = true;
            this.txtDiscount.Lines = new string[0];
            this.txtDiscount.Location = new System.Drawing.Point(163, 264);
            this.txtDiscount.MaxLength = 20;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.PasswordChar = '\0';
            this.txtDiscount.PromptText = "Discount";
            this.txtDiscount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDiscount.SelectedText = "";
            this.txtDiscount.SelectionLength = 0;
            this.txtDiscount.SelectionStart = 0;
            this.txtDiscount.ShortcutsEnabled = true;
            this.txtDiscount.Size = new System.Drawing.Size(265, 29);
            this.txtDiscount.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDiscount.TabIndex = 8;
            this.txtDiscount.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDiscount.UseSelectable = true;
            this.txtDiscount.WaterMark = "Discount";
            this.txtDiscount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDiscount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerStartDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerStartDate.Location = new System.Drawing.Point(163, 159);
            this.datePickerStartDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.Size = new System.Drawing.Size(265, 29);
            this.datePickerStartDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerStartDate.TabIndex = 5;
            this.datePickerStartDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtCouponName
            // 
            this.txtCouponName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCouponName.CustomButton.Image = null;
            this.txtCouponName.CustomButton.Location = new System.Drawing.Point(237, 1);
            this.txtCouponName.CustomButton.Name = "";
            this.txtCouponName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCouponName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponName.CustomButton.TabIndex = 1;
            this.txtCouponName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponName.CustomButton.UseSelectable = true;
            this.txtCouponName.CustomButton.Visible = false;
            this.txtCouponName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCouponName.Lines = new string[0];
            this.txtCouponName.Location = new System.Drawing.Point(163, 124);
            this.txtCouponName.MaxLength = 20;
            this.txtCouponName.Name = "txtCouponName";
            this.txtCouponName.PasswordChar = '\0';
            this.txtCouponName.PromptText = "Coupon Name";
            this.txtCouponName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCouponName.SelectedText = "";
            this.txtCouponName.SelectionLength = 0;
            this.txtCouponName.SelectionStart = 0;
            this.txtCouponName.ShortcutsEnabled = true;
            this.txtCouponName.Size = new System.Drawing.Size(265, 29);
            this.txtCouponName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponName.TabIndex = 4;
            this.txtCouponName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponName.UseSelectable = true;
            this.txtCouponName.WaterMark = "Coupon Name";
            this.txtCouponName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCouponName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerEndDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEndDate.Location = new System.Drawing.Point(163, 194);
            this.datePickerEndDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.Size = new System.Drawing.Size(265, 29);
            this.datePickerEndDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerEndDate.TabIndex = 6;
            this.datePickerEndDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtMinPurAmt
            // 
            this.txtMinPurAmt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtMinPurAmt.CustomButton.Image = null;
            this.txtMinPurAmt.CustomButton.Location = new System.Drawing.Point(237, 1);
            this.txtMinPurAmt.CustomButton.Name = "";
            this.txtMinPurAmt.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtMinPurAmt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMinPurAmt.CustomButton.TabIndex = 1;
            this.txtMinPurAmt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtMinPurAmt.CustomButton.UseSelectable = true;
            this.txtMinPurAmt.CustomButton.Visible = false;
            this.txtMinPurAmt.DisplayIcon = true;
            this.txtMinPurAmt.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtMinPurAmt.Icon = global::SFPOSWindows.Properties.Resources.dollar_coin_money1;
            this.txtMinPurAmt.IconRight = true;
            this.txtMinPurAmt.Lines = new string[0];
            this.txtMinPurAmt.Location = new System.Drawing.Point(163, 229);
            this.txtMinPurAmt.MaxLength = 10;
            this.txtMinPurAmt.Name = "txtMinPurAmt";
            this.txtMinPurAmt.PasswordChar = '\0';
            this.txtMinPurAmt.PromptText = "Minimum Purchase Amount";
            this.txtMinPurAmt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMinPurAmt.SelectedText = "";
            this.txtMinPurAmt.SelectionLength = 0;
            this.txtMinPurAmt.SelectionStart = 0;
            this.txtMinPurAmt.ShortcutsEnabled = true;
            this.txtMinPurAmt.Size = new System.Drawing.Size(265, 29);
            this.txtMinPurAmt.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMinPurAmt.TabIndex = 7;
            this.txtMinPurAmt.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtMinPurAmt.UseSelectable = true;
            this.txtMinPurAmt.WaterMark = "Minimum Purchase Amount";
            this.txtMinPurAmt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtMinPurAmt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtCouponCode
            // 
            this.txtCouponCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCouponCode.CustomButton.Image = null;
            this.txtCouponCode.CustomButton.Location = new System.Drawing.Point(237, 1);
            this.txtCouponCode.CustomButton.Name = "";
            this.txtCouponCode.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCouponCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponCode.CustomButton.TabIndex = 1;
            this.txtCouponCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponCode.CustomButton.UseSelectable = true;
            this.txtCouponCode.CustomButton.Visible = false;
            this.txtCouponCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCouponCode.Lines = new string[0];
            this.txtCouponCode.Location = new System.Drawing.Point(163, 89);
            this.txtCouponCode.MaxLength = 20;
            this.txtCouponCode.Name = "txtCouponCode";
            this.txtCouponCode.PasswordChar = '\0';
            this.txtCouponCode.PromptText = "Coupon Code";
            this.txtCouponCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCouponCode.SelectedText = "";
            this.txtCouponCode.SelectionLength = 0;
            this.txtCouponCode.SelectionStart = 0;
            this.txtCouponCode.ShortcutsEnabled = true;
            this.txtCouponCode.Size = new System.Drawing.Size(265, 29);
            this.txtCouponCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponCode.TabIndex = 3;
            this.txtCouponCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponCode.UseSelectable = true;
            this.txtCouponCode.WaterMark = "Coupon Code";
            this.txtCouponCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCouponCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkAllowDepartment);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(441, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 381);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Allow Department";
            this.groupBox1.Visible = false;
            // 
            // chkAllowDepartment
            // 
            this.chkAllowDepartment.CheckOnClick = true;
            this.chkAllowDepartment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAllowDepartment.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkAllowDepartment.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllowDepartment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkAllowDepartment.FormattingEnabled = true;
            this.chkAllowDepartment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkAllowDepartment.Location = new System.Drawing.Point(3, 21);
            this.chkAllowDepartment.Name = "chkAllowDepartment";
            this.chkAllowDepartment.Size = new System.Drawing.Size(223, 356);
            this.chkAllowDepartment.TabIndex = 17;
            this.chkAllowDepartment.ThreeDCheckBoxes = true;
            // 
            // FrmMetro_AddCouponNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 480);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.metroSelectedDep);
            this.Controls.Add(this.metroLabel10);
            this.Controls.Add(this.metroAllDep);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.metroCustRest);
            this.Controls.Add(this.metroLabel8);
            this.Controls.Add(this.toggleIsMulUser);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.txtCouponName);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txtMinPurAmt);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.datePickerEndDate);
            this.Controls.Add(this.datePickerStartDate);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.txtCouponCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(980, 900);
            this.MinimumSize = new System.Drawing.Size(419, 385);
            this.Name = "FrmMetro_AddCouponNew";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate New Coupon";
            this.Load += new System.EventHandler(this.FrmMetro_AddCouponNew_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        public MetroFramework.Controls.MetroToggle toggleIsMulUser;
        public MetroFramework.Controls.MetroToggle metroCustRest;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        public MetroFramework.Controls.MetroToggle metroAllDep;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        public MetroFramework.Controls.MetroToggle metroSelectedDep;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        public MetroFramework.Controls.MetroTextBox txtDiscount;
        public MetroFramework.Controls.MetroDateTime datePickerStartDate;
        public MetroFramework.Controls.MetroTextBox txtCouponName;
        public MetroFramework.Controls.MetroDateTime datePickerEndDate;
        public MetroFramework.Controls.MetroTextBox txtMinPurAmt;
        public MetroFramework.Controls.MetroTextBox txtCouponCode;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckedListBox chkAllowDepartment;
    }
}