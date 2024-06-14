namespace SFPOSWindows.Reports
{
    partial class FrmDepositVerification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDepositVerification));
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel14 = new MetroFramework.Controls.MetroLabel();
            this.txtDate = new MetroFramework.Controls.MetroDateTime();
            this.label1 = new System.Windows.Forms.Label();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.txtdifftotal = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel4 = new MetroFramework.Controls.MetroPanel();
            this.txtBalanceTotal = new MetroFramework.Controls.MetroLabel();
            this.txtSystemTotal = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.lblDate = new MetroFramework.Controls.MetroLabel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.txtDCashPayout = new MetroFramework.Controls.MetroTextBox();
            this.txtACashPayout = new MetroFramework.Controls.MetroTextBox();
            this.txtDTCashPayout = new MetroFramework.Controls.MetroTextBox();
            this.lblCashPayout = new MetroFramework.Controls.MetroLabel();
            this.txtD_Creditcard = new MetroFramework.Controls.MetroTextBox();
            this.txtD_Check = new MetroFramework.Controls.MetroTextBox();
            this.txtD_Cash = new MetroFramework.Controls.MetroTextBox();
            this.txtB_CreaditCard = new MetroFramework.Controls.MetroTextBox();
            this.txtS_CreaditCard = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.txtB_Check = new MetroFramework.Controls.MetroTextBox();
            this.txtS_Check = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.txtB_Cash = new MetroFramework.Controls.MetroTextBox();
            this.txtS_Cash = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel2.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel2
            // 
            this.metroPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroPanel2.Controls.Add(this.metroLabel14);
            this.metroPanel2.Controls.Add(this.txtDate);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(23, 63);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(811, 55);
            this.metroPanel2.TabIndex = 2;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroLabel14
            // 
            this.metroLabel14.AutoSize = true;
            this.metroLabel14.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel14.Location = new System.Drawing.Point(29, 21);
            this.metroLabel14.MaximumSize = new System.Drawing.Size(100, 0);
            this.metroLabel14.MinimumSize = new System.Drawing.Size(50, 0);
            this.metroLabel14.Name = "metroLabel14";
            this.metroLabel14.Size = new System.Drawing.Size(48, 19);
            this.metroLabel14.TabIndex = 29;
            this.metroLabel14.Text = "Date: ";
            this.metroLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDate
            // 
            this.txtDate.FontWeight = MetroFramework.MetroDateTimeWeight.Bold;
            this.txtDate.Location = new System.Drawing.Point(91, 18);
            this.txtDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(229, 29);
            this.txtDate.TabIndex = 27;
            this.txtDate.ValueChanged += new System.EventHandler(this.txtDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 25);
            this.label1.TabIndex = 25;
            this.label1.Text = "Deposit Verification";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.metroPanel1.Controls.Add(this.txtdifftotal);
            this.metroPanel1.Controls.Add(this.metroLabel2);
            this.metroPanel1.Controls.Add(this.metroPanel4);
            this.metroPanel1.Controls.Add(this.txtBalanceTotal);
            this.metroPanel1.Controls.Add(this.txtSystemTotal);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.Controls.Add(this.metroLabel5);
            this.metroPanel1.Controls.Add(this.lblDate);
            this.metroPanel1.Controls.Add(this.metroPanel3);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 137);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(811, 249);
            this.metroPanel1.TabIndex = 28;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // txtdifftotal
            // 
            this.txtdifftotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdifftotal.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.txtdifftotal.Location = new System.Drawing.Point(456, 225);
            this.txtdifftotal.MaximumSize = new System.Drawing.Size(100, 19);
            this.txtdifftotal.MinimumSize = new System.Drawing.Size(100, 19);
            this.txtdifftotal.Name = "txtdifftotal";
            this.txtdifftotal.Size = new System.Drawing.Size(100, 19);
            this.txtdifftotal.TabIndex = 14;
            this.txtdifftotal.Text = "$0.00";
            this.txtdifftotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AllowDrop = true;
            this.metroLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(414, 6);
            this.metroLabel2.MaximumSize = new System.Drawing.Size(100, 19);
            this.metroLabel2.MinimumSize = new System.Drawing.Size(100, 19);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(100, 19);
            this.metroLabel2.TabIndex = 13;
            this.metroLabel2.Text = "Difference";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // metroPanel4
            // 
            this.metroPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel4.HorizontalScrollbarBarColor = true;
            this.metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel4.HorizontalScrollbarSize = 10;
            this.metroPanel4.Location = new System.Drawing.Point(19, 220);
            this.metroPanel4.Name = "metroPanel4";
            this.metroPanel4.Size = new System.Drawing.Size(764, 2);
            this.metroPanel4.TabIndex = 2;
            this.metroPanel4.VerticalScrollbarBarColor = true;
            this.metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel4.VerticalScrollbarSize = 10;
            // 
            // txtBalanceTotal
            // 
            this.txtBalanceTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBalanceTotal.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.txtBalanceTotal.Location = new System.Drawing.Point(649, 225);
            this.txtBalanceTotal.MaximumSize = new System.Drawing.Size(100, 19);
            this.txtBalanceTotal.MinimumSize = new System.Drawing.Size(100, 19);
            this.txtBalanceTotal.Name = "txtBalanceTotal";
            this.txtBalanceTotal.Size = new System.Drawing.Size(100, 19);
            this.txtBalanceTotal.TabIndex = 12;
            this.txtBalanceTotal.Text = "$0.00";
            this.txtBalanceTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSystemTotal
            // 
            this.txtSystemTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSystemTotal.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.txtSystemTotal.Location = new System.Drawing.Point(256, 225);
            this.txtSystemTotal.MaximumSize = new System.Drawing.Size(100, 19);
            this.txtSystemTotal.MinimumSize = new System.Drawing.Size(100, 19);
            this.txtSystemTotal.Name = "txtSystemTotal";
            this.txtSystemTotal.Size = new System.Drawing.Size(100, 19);
            this.txtSystemTotal.TabIndex = 11;
            this.txtSystemTotal.Text = "$0.00";
            this.txtSystemTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AllowDrop = true;
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(616, 6);
            this.metroLabel1.MaximumSize = new System.Drawing.Size(100, 19);
            this.metroLabel1.MinimumSize = new System.Drawing.Size(100, 19);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(100, 19);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Adjustment ";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AllowDrop = true;
            this.metroLabel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel5.Location = new System.Drawing.Point(220, 6);
            this.metroLabel5.MaximumSize = new System.Drawing.Size(100, 19);
            this.metroLabel5.MinimumSize = new System.Drawing.Size(100, 19);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(100, 19);
            this.metroLabel5.TabIndex = 7;
            this.metroLabel5.Text = "Daily Total";
            this.metroLabel5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblDate.Location = new System.Drawing.Point(46, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(55, 19);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Tender";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroPanel3
            // 
            this.metroPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel3.Controls.Add(this.txtDCashPayout);
            this.metroPanel3.Controls.Add(this.txtACashPayout);
            this.metroPanel3.Controls.Add(this.txtDTCashPayout);
            this.metroPanel3.Controls.Add(this.lblCashPayout);
            this.metroPanel3.Controls.Add(this.txtD_Creditcard);
            this.metroPanel3.Controls.Add(this.txtD_Check);
            this.metroPanel3.Controls.Add(this.txtD_Cash);
            this.metroPanel3.Controls.Add(this.txtB_CreaditCard);
            this.metroPanel3.Controls.Add(this.txtS_CreaditCard);
            this.metroPanel3.Controls.Add(this.metroLabel11);
            this.metroPanel3.Controls.Add(this.txtB_Check);
            this.metroPanel3.Controls.Add(this.txtS_Check);
            this.metroPanel3.Controls.Add(this.metroLabel10);
            this.metroPanel3.Controls.Add(this.txtB_Cash);
            this.metroPanel3.Controls.Add(this.txtS_Cash);
            this.metroPanel3.Controls.Add(this.metroLabel9);
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(20, 28);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(763, 186);
            this.metroPanel3.TabIndex = 2;
            this.metroPanel3.UseCustomBackColor = true;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // txtDCashPayout
            // 
            // 
            // 
            // 
            this.txtDCashPayout.CustomButton.Image = null;
            this.txtDCashPayout.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtDCashPayout.CustomButton.Name = "";
            this.txtDCashPayout.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtDCashPayout.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDCashPayout.CustomButton.TabIndex = 1;
            this.txtDCashPayout.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDCashPayout.CustomButton.UseSelectable = true;
            this.txtDCashPayout.CustomButton.Visible = false;
            this.txtDCashPayout.Enabled = false;
            this.txtDCashPayout.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDCashPayout.Lines = new string[0];
            this.txtDCashPayout.Location = new System.Drawing.Point(385, 146);
            this.txtDCashPayout.MaxLength = 32767;
            this.txtDCashPayout.Name = "txtDCashPayout";
            this.txtDCashPayout.PasswordChar = '\0';
            this.txtDCashPayout.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDCashPayout.SelectedText = "";
            this.txtDCashPayout.SelectionLength = 0;
            this.txtDCashPayout.SelectionStart = 0;
            this.txtDCashPayout.ShortcutsEnabled = true;
            this.txtDCashPayout.Size = new System.Drawing.Size(150, 29);
            this.txtDCashPayout.TabIndex = 31;
            this.txtDCashPayout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDCashPayout.UseSelectable = true;
            this.txtDCashPayout.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDCashPayout.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtACashPayout
            // 
            // 
            // 
            // 
            this.txtACashPayout.CustomButton.Image = null;
            this.txtACashPayout.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtACashPayout.CustomButton.Name = "";
            this.txtACashPayout.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtACashPayout.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtACashPayout.CustomButton.TabIndex = 1;
            this.txtACashPayout.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtACashPayout.CustomButton.UseSelectable = true;
            this.txtACashPayout.CustomButton.Visible = false;
            this.txtACashPayout.Enabled = false;
            this.txtACashPayout.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtACashPayout.Lines = new string[0];
            this.txtACashPayout.Location = new System.Drawing.Point(578, 145);
            this.txtACashPayout.MaxLength = 32767;
            this.txtACashPayout.Name = "txtACashPayout";
            this.txtACashPayout.PasswordChar = '\0';
            this.txtACashPayout.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtACashPayout.SelectedText = "";
            this.txtACashPayout.SelectionLength = 0;
            this.txtACashPayout.SelectionStart = 0;
            this.txtACashPayout.ShortcutsEnabled = true;
            this.txtACashPayout.Size = new System.Drawing.Size(150, 29);
            this.txtACashPayout.TabIndex = 30;
            this.txtACashPayout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtACashPayout.UseSelectable = true;
            this.txtACashPayout.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtACashPayout.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtDTCashPayout
            // 
            // 
            // 
            // 
            this.txtDTCashPayout.CustomButton.Image = null;
            this.txtDTCashPayout.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtDTCashPayout.CustomButton.Name = "";
            this.txtDTCashPayout.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtDTCashPayout.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDTCashPayout.CustomButton.TabIndex = 1;
            this.txtDTCashPayout.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDTCashPayout.CustomButton.UseSelectable = true;
            this.txtDTCashPayout.CustomButton.Visible = false;
            this.txtDTCashPayout.Enabled = false;
            this.txtDTCashPayout.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDTCashPayout.Lines = new string[0];
            this.txtDTCashPayout.Location = new System.Drawing.Point(189, 145);
            this.txtDTCashPayout.MaxLength = 32767;
            this.txtDTCashPayout.Name = "txtDTCashPayout";
            this.txtDTCashPayout.PasswordChar = '\0';
            this.txtDTCashPayout.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDTCashPayout.SelectedText = "";
            this.txtDTCashPayout.SelectionLength = 0;
            this.txtDTCashPayout.SelectionStart = 0;
            this.txtDTCashPayout.ShortcutsEnabled = true;
            this.txtDTCashPayout.Size = new System.Drawing.Size(150, 29);
            this.txtDTCashPayout.TabIndex = 29;
            this.txtDTCashPayout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDTCashPayout.UseSelectable = true;
            this.txtDTCashPayout.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDTCashPayout.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblCashPayout
            // 
            this.lblCashPayout.AutoSize = true;
            this.lblCashPayout.BackColor = System.Drawing.Color.Transparent;
            this.lblCashPayout.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblCashPayout.Location = new System.Drawing.Point(25, 145);
            this.lblCashPayout.Name = "lblCashPayout";
            this.lblCashPayout.Size = new System.Drawing.Size(91, 19);
            this.lblCashPayout.TabIndex = 28;
            this.lblCashPayout.Text = "Cash Payout";
            this.lblCashPayout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCashPayout.UseCustomBackColor = true;
            // 
            // txtD_Creditcard
            // 
            // 
            // 
            // 
            this.txtD_Creditcard.CustomButton.Image = null;
            this.txtD_Creditcard.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtD_Creditcard.CustomButton.Name = "";
            this.txtD_Creditcard.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtD_Creditcard.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtD_Creditcard.CustomButton.TabIndex = 1;
            this.txtD_Creditcard.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtD_Creditcard.CustomButton.UseSelectable = true;
            this.txtD_Creditcard.CustomButton.Visible = false;
            this.txtD_Creditcard.Enabled = false;
            this.txtD_Creditcard.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtD_Creditcard.Lines = new string[0];
            this.txtD_Creditcard.Location = new System.Drawing.Point(385, 103);
            this.txtD_Creditcard.MaxLength = 32767;
            this.txtD_Creditcard.Name = "txtD_Creditcard";
            this.txtD_Creditcard.PasswordChar = '\0';
            this.txtD_Creditcard.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtD_Creditcard.SelectedText = "";
            this.txtD_Creditcard.SelectionLength = 0;
            this.txtD_Creditcard.SelectionStart = 0;
            this.txtD_Creditcard.ShortcutsEnabled = true;
            this.txtD_Creditcard.Size = new System.Drawing.Size(150, 29);
            this.txtD_Creditcard.TabIndex = 27;
            this.txtD_Creditcard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtD_Creditcard.UseSelectable = true;
            this.txtD_Creditcard.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtD_Creditcard.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtD_Check
            // 
            // 
            // 
            // 
            this.txtD_Check.CustomButton.Image = null;
            this.txtD_Check.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtD_Check.CustomButton.Name = "";
            this.txtD_Check.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtD_Check.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtD_Check.CustomButton.TabIndex = 1;
            this.txtD_Check.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtD_Check.CustomButton.UseSelectable = true;
            this.txtD_Check.CustomButton.Visible = false;
            this.txtD_Check.Enabled = false;
            this.txtD_Check.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtD_Check.Lines = new string[0];
            this.txtD_Check.Location = new System.Drawing.Point(385, 62);
            this.txtD_Check.MaxLength = 32767;
            this.txtD_Check.Name = "txtD_Check";
            this.txtD_Check.PasswordChar = '\0';
            this.txtD_Check.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtD_Check.SelectedText = "";
            this.txtD_Check.SelectionLength = 0;
            this.txtD_Check.SelectionStart = 0;
            this.txtD_Check.ShortcutsEnabled = true;
            this.txtD_Check.Size = new System.Drawing.Size(150, 29);
            this.txtD_Check.TabIndex = 26;
            this.txtD_Check.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtD_Check.UseSelectable = true;
            this.txtD_Check.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtD_Check.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtD_Cash
            // 
            // 
            // 
            // 
            this.txtD_Cash.CustomButton.Image = null;
            this.txtD_Cash.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtD_Cash.CustomButton.Name = "";
            this.txtD_Cash.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtD_Cash.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtD_Cash.CustomButton.TabIndex = 1;
            this.txtD_Cash.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtD_Cash.CustomButton.UseSelectable = true;
            this.txtD_Cash.CustomButton.Visible = false;
            this.txtD_Cash.Enabled = false;
            this.txtD_Cash.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtD_Cash.Lines = new string[0];
            this.txtD_Cash.Location = new System.Drawing.Point(385, 21);
            this.txtD_Cash.MaxLength = 32767;
            this.txtD_Cash.Name = "txtD_Cash";
            this.txtD_Cash.PasswordChar = '\0';
            this.txtD_Cash.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtD_Cash.SelectedText = "";
            this.txtD_Cash.SelectionLength = 0;
            this.txtD_Cash.SelectionStart = 0;
            this.txtD_Cash.ShortcutsEnabled = true;
            this.txtD_Cash.Size = new System.Drawing.Size(150, 29);
            this.txtD_Cash.TabIndex = 25;
            this.txtD_Cash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtD_Cash.UseSelectable = true;
            this.txtD_Cash.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtD_Cash.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtB_CreaditCard
            // 
            // 
            // 
            // 
            this.txtB_CreaditCard.CustomButton.Image = null;
            this.txtB_CreaditCard.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtB_CreaditCard.CustomButton.Name = "";
            this.txtB_CreaditCard.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtB_CreaditCard.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtB_CreaditCard.CustomButton.TabIndex = 1;
            this.txtB_CreaditCard.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtB_CreaditCard.CustomButton.UseSelectable = true;
            this.txtB_CreaditCard.CustomButton.Visible = false;
            this.txtB_CreaditCard.Enabled = false;
            this.txtB_CreaditCard.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtB_CreaditCard.Lines = new string[0];
            this.txtB_CreaditCard.Location = new System.Drawing.Point(578, 102);
            this.txtB_CreaditCard.MaxLength = 32767;
            this.txtB_CreaditCard.Name = "txtB_CreaditCard";
            this.txtB_CreaditCard.PasswordChar = '\0';
            this.txtB_CreaditCard.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtB_CreaditCard.SelectedText = "";
            this.txtB_CreaditCard.SelectionLength = 0;
            this.txtB_CreaditCard.SelectionStart = 0;
            this.txtB_CreaditCard.ShortcutsEnabled = true;
            this.txtB_CreaditCard.Size = new System.Drawing.Size(150, 29);
            this.txtB_CreaditCard.TabIndex = 24;
            this.txtB_CreaditCard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtB_CreaditCard.UseSelectable = true;
            this.txtB_CreaditCard.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtB_CreaditCard.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtS_CreaditCard
            // 
            // 
            // 
            // 
            this.txtS_CreaditCard.CustomButton.Image = null;
            this.txtS_CreaditCard.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtS_CreaditCard.CustomButton.Name = "";
            this.txtS_CreaditCard.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtS_CreaditCard.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtS_CreaditCard.CustomButton.TabIndex = 1;
            this.txtS_CreaditCard.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtS_CreaditCard.CustomButton.UseSelectable = true;
            this.txtS_CreaditCard.CustomButton.Visible = false;
            this.txtS_CreaditCard.Enabled = false;
            this.txtS_CreaditCard.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtS_CreaditCard.Lines = new string[0];
            this.txtS_CreaditCard.Location = new System.Drawing.Point(189, 102);
            this.txtS_CreaditCard.MaxLength = 32767;
            this.txtS_CreaditCard.Name = "txtS_CreaditCard";
            this.txtS_CreaditCard.PasswordChar = '\0';
            this.txtS_CreaditCard.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtS_CreaditCard.SelectedText = "";
            this.txtS_CreaditCard.SelectionLength = 0;
            this.txtS_CreaditCard.SelectionStart = 0;
            this.txtS_CreaditCard.ShortcutsEnabled = true;
            this.txtS_CreaditCard.Size = new System.Drawing.Size(150, 29);
            this.txtS_CreaditCard.TabIndex = 23;
            this.txtS_CreaditCard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtS_CreaditCard.UseSelectable = true;
            this.txtS_CreaditCard.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtS_CreaditCard.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel11.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel11.Location = new System.Drawing.Point(25, 102);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(107, 19);
            this.metroLabel11.TabIndex = 21;
            this.metroLabel11.Text = "Credit Card/FS";
            this.metroLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroLabel11.UseCustomBackColor = true;
            // 
            // txtB_Check
            // 
            // 
            // 
            // 
            this.txtB_Check.CustomButton.Image = null;
            this.txtB_Check.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtB_Check.CustomButton.Name = "";
            this.txtB_Check.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtB_Check.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtB_Check.CustomButton.TabIndex = 1;
            this.txtB_Check.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtB_Check.CustomButton.UseSelectable = true;
            this.txtB_Check.CustomButton.Visible = false;
            this.txtB_Check.Enabled = false;
            this.txtB_Check.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtB_Check.Lines = new string[0];
            this.txtB_Check.Location = new System.Drawing.Point(578, 61);
            this.txtB_Check.MaxLength = 32767;
            this.txtB_Check.Name = "txtB_Check";
            this.txtB_Check.PasswordChar = '\0';
            this.txtB_Check.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtB_Check.SelectedText = "";
            this.txtB_Check.SelectionLength = 0;
            this.txtB_Check.SelectionStart = 0;
            this.txtB_Check.ShortcutsEnabled = true;
            this.txtB_Check.Size = new System.Drawing.Size(150, 29);
            this.txtB_Check.TabIndex = 20;
            this.txtB_Check.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtB_Check.UseSelectable = true;
            this.txtB_Check.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtB_Check.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtS_Check
            // 
            // 
            // 
            // 
            this.txtS_Check.CustomButton.Image = null;
            this.txtS_Check.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtS_Check.CustomButton.Name = "";
            this.txtS_Check.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtS_Check.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtS_Check.CustomButton.TabIndex = 1;
            this.txtS_Check.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtS_Check.CustomButton.UseSelectable = true;
            this.txtS_Check.CustomButton.Visible = false;
            this.txtS_Check.Enabled = false;
            this.txtS_Check.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtS_Check.Lines = new string[0];
            this.txtS_Check.Location = new System.Drawing.Point(189, 61);
            this.txtS_Check.MaxLength = 32767;
            this.txtS_Check.Name = "txtS_Check";
            this.txtS_Check.PasswordChar = '\0';
            this.txtS_Check.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtS_Check.SelectedText = "";
            this.txtS_Check.SelectionLength = 0;
            this.txtS_Check.SelectionStart = 0;
            this.txtS_Check.ShortcutsEnabled = true;
            this.txtS_Check.Size = new System.Drawing.Size(150, 29);
            this.txtS_Check.TabIndex = 19;
            this.txtS_Check.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtS_Check.UseSelectable = true;
            this.txtS_Check.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtS_Check.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel10.Location = new System.Drawing.Point(25, 61);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(49, 19);
            this.metroLabel10.TabIndex = 17;
            this.metroLabel10.Text = "Check";
            this.metroLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroLabel10.UseCustomBackColor = true;
            // 
            // txtB_Cash
            // 
            // 
            // 
            // 
            this.txtB_Cash.CustomButton.Image = null;
            this.txtB_Cash.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtB_Cash.CustomButton.Name = "";
            this.txtB_Cash.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtB_Cash.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtB_Cash.CustomButton.TabIndex = 1;
            this.txtB_Cash.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtB_Cash.CustomButton.UseSelectable = true;
            this.txtB_Cash.CustomButton.Visible = false;
            this.txtB_Cash.Enabled = false;
            this.txtB_Cash.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtB_Cash.Lines = new string[0];
            this.txtB_Cash.Location = new System.Drawing.Point(578, 20);
            this.txtB_Cash.MaxLength = 32767;
            this.txtB_Cash.Name = "txtB_Cash";
            this.txtB_Cash.PasswordChar = '\0';
            this.txtB_Cash.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtB_Cash.SelectedText = "";
            this.txtB_Cash.SelectionLength = 0;
            this.txtB_Cash.SelectionStart = 0;
            this.txtB_Cash.ShortcutsEnabled = true;
            this.txtB_Cash.Size = new System.Drawing.Size(150, 29);
            this.txtB_Cash.TabIndex = 16;
            this.txtB_Cash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtB_Cash.UseSelectable = true;
            this.txtB_Cash.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtB_Cash.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtS_Cash
            // 
            // 
            // 
            // 
            this.txtS_Cash.CustomButton.Image = null;
            this.txtS_Cash.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txtS_Cash.CustomButton.Name = "";
            this.txtS_Cash.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtS_Cash.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtS_Cash.CustomButton.TabIndex = 1;
            this.txtS_Cash.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtS_Cash.CustomButton.UseSelectable = true;
            this.txtS_Cash.CustomButton.Visible = false;
            this.txtS_Cash.Enabled = false;
            this.txtS_Cash.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtS_Cash.Lines = new string[0];
            this.txtS_Cash.Location = new System.Drawing.Point(189, 20);
            this.txtS_Cash.MaxLength = 32767;
            this.txtS_Cash.Name = "txtS_Cash";
            this.txtS_Cash.PasswordChar = '\0';
            this.txtS_Cash.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtS_Cash.SelectedText = "";
            this.txtS_Cash.SelectionLength = 0;
            this.txtS_Cash.SelectionStart = 0;
            this.txtS_Cash.ShortcutsEnabled = true;
            this.txtS_Cash.Size = new System.Drawing.Size(150, 29);
            this.txtS_Cash.TabIndex = 15;
            this.txtS_Cash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtS_Cash.UseSelectable = true;
            this.txtS_Cash.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtS_Cash.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel9.Location = new System.Drawing.Point(25, 20);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(40, 19);
            this.metroLabel9.TabIndex = 13;
            this.metroLabel9.Text = "Cash";
            this.metroLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroLabel9.UseCustomBackColor = true;
            // 
            // FrmDepositVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(857, 578);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDepositVerification";
            this.Text = "Till Status";
            this.Load += new System.EventHandler(this.FrmAddReport_Load);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroDateTime txtDate;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel4;
        private MetroFramework.Controls.MetroLabel txtBalanceTotal;
        private MetroFramework.Controls.MetroLabel txtSystemTotal;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel lblDate;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroTextBox txtB_CreaditCard;
        private MetroFramework.Controls.MetroTextBox txtS_CreaditCard;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroTextBox txtB_Check;
        private MetroFramework.Controls.MetroTextBox txtS_Check;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroTextBox txtB_Cash;
        private MetroFramework.Controls.MetroTextBox txtS_Cash;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel metroLabel14;
        private MetroFramework.Controls.MetroLabel txtdifftotal;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox txtD_Creditcard;
        private MetroFramework.Controls.MetroTextBox txtD_Check;
        private MetroFramework.Controls.MetroTextBox txtD_Cash;
        private MetroFramework.Controls.MetroTextBox txtDCashPayout;
        private MetroFramework.Controls.MetroTextBox txtACashPayout;
        private MetroFramework.Controls.MetroTextBox txtDTCashPayout;
        private MetroFramework.Controls.MetroLabel lblCashPayout;
    }
}