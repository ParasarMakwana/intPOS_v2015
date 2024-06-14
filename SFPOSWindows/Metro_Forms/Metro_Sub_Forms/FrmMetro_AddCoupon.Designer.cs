namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddCoupon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddCoupon));
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.txtCouponCode = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.datePickerEndDate = new MetroFramework.Controls.MetroDateTime();
            this.datePickerStartDate = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtMinPurAmt = new MetroFramework.Controls.MetroTextBox();
            this.txtDiscount = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.txtCouponName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.txtCouponFrequency = new MetroFramework.Controls.MetroTextBox();
            this.lblMultipleUse = new MetroFramework.Controls.MetroLabel();
            this.toggleIsMulUser = new MetroFramework.Controls.MetroToggle();
            this.lblActive = new MetroFramework.Controls.MetroLabel();
            this.ToggleIsStaticCoupon = new MetroFramework.Controls.MetroToggle();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(23, 112);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(97, 19);
            this.metroLabel3.TabIndex = 35;
            this.metroLabel3.Text = "Coupon Code:";
            // 
            // txtCouponCode
            // 
            this.txtCouponCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCouponCode.CustomButton.Image = null;
            this.txtCouponCode.CustomButton.Location = new System.Drawing.Point(196, 1);
            this.txtCouponCode.CustomButton.Name = "";
            this.txtCouponCode.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCouponCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponCode.CustomButton.TabIndex = 1;
            this.txtCouponCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponCode.CustomButton.UseSelectable = true;
            this.txtCouponCode.CustomButton.Visible = false;
            this.txtCouponCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCouponCode.Lines = new string[0];
            this.txtCouponCode.Location = new System.Drawing.Point(163, 112);
            this.txtCouponCode.MaxLength = 20;
            this.txtCouponCode.Name = "txtCouponCode";
            this.txtCouponCode.PasswordChar = '\0';
            this.txtCouponCode.PromptText = "Coupon Code";
            this.txtCouponCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCouponCode.SelectedText = "";
            this.txtCouponCode.SelectionLength = 0;
            this.txtCouponCode.SelectionStart = 0;
            this.txtCouponCode.ShortcutsEnabled = true;
            this.txtCouponCode.Size = new System.Drawing.Size(224, 29);
            this.txtCouponCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponCode.TabIndex = 3;
            this.txtCouponCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponCode.UseSelectable = true;
            this.txtCouponCode.WaterMark = "Coupon Code";
            this.txtCouponCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCouponCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(23, 217);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(68, 19);
            this.metroLabel4.TabIndex = 41;
            this.metroLabel4.Text = "End Date:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(23, 182);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(74, 19);
            this.metroLabel2.TabIndex = 40;
            this.metroLabel2.Text = "Start Date:";
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerEndDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEndDate.Location = new System.Drawing.Point(163, 217);
            this.datePickerEndDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.Size = new System.Drawing.Size(224, 29);
            this.datePickerEndDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerEndDate.TabIndex = 6;
            this.datePickerEndDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerStartDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerStartDate.Location = new System.Drawing.Point(163, 182);
            this.datePickerStartDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.Size = new System.Drawing.Size(224, 29);
            this.datePickerStartDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerStartDate.TabIndex = 5;
            this.datePickerStartDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 252);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(125, 19);
            this.metroLabel1.TabIndex = 43;
            this.metroLabel1.Text = "Min Purchase Amt:";
            // 
            // txtMinPurAmt
            // 
            this.txtMinPurAmt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtMinPurAmt.CustomButton.Image = null;
            this.txtMinPurAmt.CustomButton.Location = new System.Drawing.Point(196, 1);
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
            this.txtMinPurAmt.Location = new System.Drawing.Point(163, 252);
            this.txtMinPurAmt.MaxLength = 10;
            this.txtMinPurAmt.Name = "txtMinPurAmt";
            this.txtMinPurAmt.PasswordChar = '\0';
            this.txtMinPurAmt.PromptText = "Minimum Purchase Amount";
            this.txtMinPurAmt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMinPurAmt.SelectedText = "";
            this.txtMinPurAmt.SelectionLength = 0;
            this.txtMinPurAmt.SelectionStart = 0;
            this.txtMinPurAmt.ShortcutsEnabled = true;
            this.txtMinPurAmt.Size = new System.Drawing.Size(224, 29);
            this.txtMinPurAmt.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMinPurAmt.TabIndex = 7;
            this.txtMinPurAmt.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtMinPurAmt.UseSelectable = true;
            this.txtMinPurAmt.WaterMark = "Minimum Purchase Amount";
            this.txtMinPurAmt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtMinPurAmt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDiscount.CustomButton.Image = null;
            this.txtDiscount.CustomButton.Location = new System.Drawing.Point(196, 1);
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
            this.txtDiscount.Location = new System.Drawing.Point(163, 287);
            this.txtDiscount.MaxLength = 20;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.PasswordChar = '\0';
            this.txtDiscount.PromptText = "Discount";
            this.txtDiscount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDiscount.SelectedText = "";
            this.txtDiscount.SelectionLength = 0;
            this.txtDiscount.SelectionStart = 0;
            this.txtDiscount.ShortcutsEnabled = true;
            this.txtDiscount.Size = new System.Drawing.Size(224, 29);
            this.txtDiscount.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDiscount.TabIndex = 8;
            this.txtDiscount.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDiscount.UseSelectable = true;
            this.txtDiscount.WaterMark = "Discount";
            this.txtDiscount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDiscount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(23, 287);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(85, 19);
            this.metroLabel5.TabIndex = 45;
            this.metroLabel5.Text = "Discount(%):";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.Location = new System.Drawing.Point(23, 147);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(101, 19);
            this.metroLabel6.TabIndex = 47;
            this.metroLabel6.Text = "Coupon Name:";
            // 
            // txtCouponName
            // 
            this.txtCouponName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCouponName.CustomButton.Image = null;
            this.txtCouponName.CustomButton.Location = new System.Drawing.Point(196, 1);
            this.txtCouponName.CustomButton.Name = "";
            this.txtCouponName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCouponName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponName.CustomButton.TabIndex = 1;
            this.txtCouponName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponName.CustomButton.UseSelectable = true;
            this.txtCouponName.CustomButton.Visible = false;
            this.txtCouponName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCouponName.Lines = new string[0];
            this.txtCouponName.Location = new System.Drawing.Point(163, 147);
            this.txtCouponName.MaxLength = 20;
            this.txtCouponName.Name = "txtCouponName";
            this.txtCouponName.PasswordChar = '\0';
            this.txtCouponName.PromptText = "Coupon Name";
            this.txtCouponName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCouponName.SelectedText = "";
            this.txtCouponName.SelectionLength = 0;
            this.txtCouponName.SelectionStart = 0;
            this.txtCouponName.ShortcutsEnabled = true;
            this.txtCouponName.Size = new System.Drawing.Size(224, 29);
            this.txtCouponName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponName.TabIndex = 4;
            this.txtCouponName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponName.UseSelectable = true;
            this.txtCouponName.WaterMark = "Coupon Name";
            this.txtCouponName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCouponName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.Location = new System.Drawing.Point(23, 322);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(121, 19);
            this.metroLabel7.TabIndex = 49;
            this.metroLabel7.Text = "Coupon Frquency:";
            // 
            // txtCouponFrequency
            // 
            this.txtCouponFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCouponFrequency.CustomButton.Image = null;
            this.txtCouponFrequency.CustomButton.Location = new System.Drawing.Point(196, 1);
            this.txtCouponFrequency.CustomButton.Name = "";
            this.txtCouponFrequency.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCouponFrequency.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponFrequency.CustomButton.TabIndex = 1;
            this.txtCouponFrequency.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponFrequency.CustomButton.UseSelectable = true;
            this.txtCouponFrequency.CustomButton.Visible = false;
            this.txtCouponFrequency.DisplayIcon = true;
            this.txtCouponFrequency.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCouponFrequency.IconRight = true;
            this.txtCouponFrequency.Lines = new string[0];
            this.txtCouponFrequency.Location = new System.Drawing.Point(163, 322);
            this.txtCouponFrequency.MaxLength = 20;
            this.txtCouponFrequency.Name = "txtCouponFrequency";
            this.txtCouponFrequency.PasswordChar = '\0';
            this.txtCouponFrequency.PromptText = "Coupon Frquency";
            this.txtCouponFrequency.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCouponFrequency.SelectedText = "";
            this.txtCouponFrequency.SelectionLength = 0;
            this.txtCouponFrequency.SelectionStart = 0;
            this.txtCouponFrequency.ShortcutsEnabled = true;
            this.txtCouponFrequency.Size = new System.Drawing.Size(224, 29);
            this.txtCouponFrequency.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponFrequency.TabIndex = 9;
            this.txtCouponFrequency.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponFrequency.UseSelectable = true;
            this.txtCouponFrequency.WaterMark = "Coupon Frquency";
            this.txtCouponFrequency.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCouponFrequency.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblMultipleUse
            // 
            this.lblMultipleUse.AutoSize = true;
            this.lblMultipleUse.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblMultipleUse.Location = new System.Drawing.Point(217, 81);
            this.lblMultipleUse.Name = "lblMultipleUse";
            this.lblMultipleUse.Size = new System.Drawing.Size(126, 19);
            this.lblMultipleUse.TabIndex = 53;
            this.lblMultipleUse.Text = "Allow Multiple Use:";
            // 
            // toggleIsMulUser
            // 
            this.toggleIsMulUser.AutoSize = true;
            this.toggleIsMulUser.DisplayStatus = false;
            this.toggleIsMulUser.Location = new System.Drawing.Point(357, 83);
            this.toggleIsMulUser.Name = "toggleIsMulUser";
            this.toggleIsMulUser.Size = new System.Drawing.Size(50, 17);
            this.toggleIsMulUser.TabIndex = 2;
            this.toggleIsMulUser.Text = "Off";
            this.toggleIsMulUser.UseSelectable = true;
            this.toggleIsMulUser.CheckedChanged += new System.EventHandler(this.toggleIsMulUser_CheckedChanged);
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblActive.Location = new System.Drawing.Point(13, 81);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(134, 19);
            this.lblActive.TabIndex = 50;
            this.lblActive.Text = "Static Coupon Code:";
            // 
            // ToggleIsStaticCoupon
            // 
            this.ToggleIsStaticCoupon.AutoSize = true;
            this.ToggleIsStaticCoupon.DisplayStatus = false;
            this.ToggleIsStaticCoupon.Location = new System.Drawing.Point(153, 83);
            this.ToggleIsStaticCoupon.Name = "ToggleIsStaticCoupon";
            this.ToggleIsStaticCoupon.Size = new System.Drawing.Size(50, 17);
            this.ToggleIsStaticCoupon.TabIndex = 1;
            this.ToggleIsStaticCoupon.Text = "Off";
            this.ToggleIsStaticCoupon.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ToggleIsStaticCoupon.UseSelectable = true;
            this.ToggleIsStaticCoupon.CheckedChanged += new System.EventHandler(this.ToggleIsStaticCoupon_CheckedChanged);
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(295, 378);
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
            this.MetrobtnSave.Location = new System.Drawing.Point(184, 378);
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
            // FrmMetro_AddCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 442);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.lblMultipleUse);
            this.Controls.Add(this.toggleIsMulUser);
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.ToggleIsStaticCoupon);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.txtCouponFrequency);
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
            this.MaximumSize = new System.Drawing.Size(419, 442);
            this.MinimumSize = new System.Drawing.Size(419, 442);
            this.Name = "FrmMetro_AddCoupon";
            this.Resizable = false;
            this.Text = "Generate New Coupon";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel3;
        public MetroFramework.Controls.MetroTextBox txtCouponCode;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        public MetroFramework.Controls.MetroDateTime datePickerEndDate;
        public MetroFramework.Controls.MetroDateTime datePickerStartDate;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        public MetroFramework.Controls.MetroTextBox txtMinPurAmt;
        public MetroFramework.Controls.MetroTextBox txtDiscount;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        public MetroFramework.Controls.MetroTextBox txtCouponName;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        public MetroFramework.Controls.MetroTextBox txtCouponFrequency;
        private MetroFramework.Controls.MetroLabel lblMultipleUse;
        public MetroFramework.Controls.MetroToggle toggleIsMulUser;
        private MetroFramework.Controls.MetroLabel lblActive;
        public MetroFramework.Controls.MetroToggle ToggleIsStaticCoupon;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
    }
}