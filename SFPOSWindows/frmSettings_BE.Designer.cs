namespace SFPOSWindows
{
    partial class frmSettings_BE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings_BE));
            this.TabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabezPOSProInfo = new MetroFramework.Controls.MetroTabPage();
            this.txtLicense = new System.Windows.Forms.RichTextBox();
            this.lblInstationoDate = new System.Windows.Forms.Label();
            this.lblPOSType = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.tabLicenceKey = new MetroFramework.Controls.MetroTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnActive = new MetroFramework.Controls.MetroButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKey = new MetroFramework.Controls.MetroTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabDatabaseConnection = new MetroFramework.Controls.MetroTabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnConnect = new MetroFramework.Controls.MetroButton();
            this.txtPassword = new MetroFramework.Controls.MetroTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new MetroFramework.Controls.MetroTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new MetroFramework.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTestConnections = new MetroFramework.Controls.MetroButton();
            this.txtDatabaseServerName = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabReciptPrinter = new MetroFramework.Controls.MetroTabPage();
            this.txtLabelPrinter = new MetroFramework.Controls.MetroTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPrinterName = new MetroFramework.Controls.MetroTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSaveUpdate_ReciptPrinter = new MetroFramework.Controls.MetroButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tabSerialPort = new MetroFramework.Controls.MetroTabPage();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.toggleActive = new MetroFramework.Controls.MetroToggle();
            this.label20 = new System.Windows.Forms.Label();
            this.btnReadWeight = new MetroFramework.Controls.MetroButton();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.cmbScanner = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tabSystemUpdate = new MetroFramework.Controls.MetroTabPage();
            this.btnCheckUpdate = new MetroFramework.Controls.MetroButton();
            this.lblLastRelase = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblCurrentRelase = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblReleaseUpdatedDate = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.TabControl.SuspendLayout();
            this.tabezPOSProInfo.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.tabLicenceKey.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabDatabaseConnection.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabReciptPrinter.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tabSerialPort.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabSystemUpdate.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabezPOSProInfo);
            this.TabControl.Controls.Add(this.tabLicenceKey);
            this.TabControl.Controls.Add(this.tabDatabaseConnection);
            this.TabControl.Controls.Add(this.tabReciptPrinter);
            this.TabControl.Controls.Add(this.tabSerialPort);
            this.TabControl.Controls.Add(this.tabSystemUpdate);
            this.TabControl.Location = new System.Drawing.Point(1, 25);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(771, 413);
            this.TabControl.TabIndex = 0;
            this.TabControl.UseSelectable = true;
            // 
            // tabezPOSProInfo
            // 
            this.tabezPOSProInfo.Controls.Add(this.txtLicense);
            this.tabezPOSProInfo.Controls.Add(this.lblInstationoDate);
            this.tabezPOSProInfo.Controls.Add(this.lblPOSType);
            this.tabezPOSProInfo.Controls.Add(this.label10);
            this.tabezPOSProInfo.Controls.Add(this.panel5);
            this.tabezPOSProInfo.HorizontalScrollbarBarColor = true;
            this.tabezPOSProInfo.HorizontalScrollbarHighlightOnWheel = false;
            this.tabezPOSProInfo.HorizontalScrollbarSize = 10;
            this.tabezPOSProInfo.Location = new System.Drawing.Point(4, 38);
            this.tabezPOSProInfo.Name = "tabezPOSProInfo";
            this.tabezPOSProInfo.Size = new System.Drawing.Size(763, 371);
            this.tabezPOSProInfo.TabIndex = 0;
            this.tabezPOSProInfo.Text = "About";
            this.tabezPOSProInfo.VerticalScrollbarBarColor = true;
            this.tabezPOSProInfo.VerticalScrollbarHighlightOnWheel = false;
            this.tabezPOSProInfo.VerticalScrollbarSize = 10;
            // 
            // txtLicense
            // 
            this.txtLicense.Location = new System.Drawing.Point(116, 85);
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.Size = new System.Drawing.Size(611, 286);
            this.txtLicense.TabIndex = 22;
            this.txtLicense.Text = "";
            // 
            // lblInstationoDate
            // 
            this.lblInstationoDate.AutoSize = true;
            this.lblInstationoDate.BackColor = System.Drawing.Color.Transparent;
            this.lblInstationoDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstationoDate.Location = new System.Drawing.Point(514, 58);
            this.lblInstationoDate.Name = "lblInstationoDate";
            this.lblInstationoDate.Size = new System.Drawing.Size(213, 20);
            this.lblInstationoDate.TabIndex = 20;
            this.lblInstationoDate.Text = "Installation Date: 06/21/2019";
            // 
            // lblPOSType
            // 
            this.lblPOSType.AutoSize = true;
            this.lblPOSType.BackColor = System.Drawing.Color.Transparent;
            this.lblPOSType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOSType.Location = new System.Drawing.Point(112, 58);
            this.lblPOSType.Name = "lblPOSType";
            this.lblPOSType.Size = new System.Drawing.Size(206, 20);
            this.lblPOSType.TabIndex = 19;
            this.lblPOSType.Text = "POS Type: intPOS-Backend";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(111, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 25);
            this.label10.TabIndex = 17;
            this.label10.Text = "About";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel5.Controls.Add(this.pictureBox5);
            this.panel5.Cursor = System.Windows.Forms.Cursors.No;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(100, 371);
            this.panel5.TabIndex = 16;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox5.Image = global::SFPOSWindows.Properties.Resources.ezPOSPro;
            this.pictureBox5.Location = new System.Drawing.Point(18, 148);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(65, 64);
            this.pictureBox5.TabIndex = 1;
            this.pictureBox5.TabStop = false;
            // 
            // tabLicenceKey
            // 
            this.tabLicenceKey.Controls.Add(this.panel2);
            this.tabLicenceKey.Controls.Add(this.btnActive);
            this.tabLicenceKey.Controls.Add(this.label6);
            this.tabLicenceKey.Controls.Add(this.txtKey);
            this.tabLicenceKey.Controls.Add(this.label7);
            this.tabLicenceKey.Controls.Add(this.label8);
            this.tabLicenceKey.HorizontalScrollbarBarColor = true;
            this.tabLicenceKey.HorizontalScrollbarHighlightOnWheel = false;
            this.tabLicenceKey.HorizontalScrollbarSize = 10;
            this.tabLicenceKey.Location = new System.Drawing.Point(4, 38);
            this.tabLicenceKey.Name = "tabLicenceKey";
            this.tabLicenceKey.Size = new System.Drawing.Size(763, 371);
            this.tabLicenceKey.TabIndex = 4;
            this.tabLicenceKey.Text = "Licence Key";
            this.tabLicenceKey.VerticalScrollbarBarColor = true;
            this.tabLicenceKey.VerticalScrollbarHighlightOnWheel = false;
            this.tabLicenceKey.VerticalScrollbarSize = 10;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Cursor = System.Windows.Forms.Cursors.No;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 371);
            this.panel2.TabIndex = 6;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(18, 148);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(65, 63);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // btnActive
            // 
            this.btnActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActive.Location = new System.Drawing.Point(470, 164);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(137, 31);
            this.btnActive.TabIndex = 2;
            this.btnActive.Text = "ACTIVATE";
            this.btnActive.UseSelectable = true;
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(113, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(294, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "The Activation Key Look Like This XXXX-XXXX-XXXX-XXXX";
            // 
            // txtKey
            // 
            // 
            // 
            // 
            this.txtKey.CustomButton.Image = null;
            this.txtKey.CustomButton.Location = new System.Drawing.Point(301, 1);
            this.txtKey.CustomButton.Name = "";
            this.txtKey.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.txtKey.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtKey.CustomButton.TabIndex = 1;
            this.txtKey.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtKey.CustomButton.UseSelectable = true;
            this.txtKey.CustomButton.Visible = false;
            this.txtKey.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtKey.Lines = new string[0];
            this.txtKey.Location = new System.Drawing.Point(116, 164);
            this.txtKey.MaxLength = 19;
            this.txtKey.Name = "txtKey";
            this.txtKey.PasswordChar = '\0';
            this.txtKey.PromptText = "XXXX-XXXX-XXXX-XXXX";
            this.txtKey.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtKey.SelectedText = "";
            this.txtKey.SelectionLength = 0;
            this.txtKey.SelectionStart = 0;
            this.txtKey.ShortcutsEnabled = true;
            this.txtKey.Size = new System.Drawing.Size(331, 31);
            this.txtKey.TabIndex = 1;
            this.txtKey.UseSelectable = true;
            this.txtKey.WaterMark = "XXXX-XXXX-XXXX-XXXX";
            this.txtKey.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtKey.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.txtKey.TextChanged += new System.EventHandler(this.txtKey_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(111, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(336, 25);
            this.label7.TabIndex = 9;
            this.label7.Text = "Please Enter Your Activation Key:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(111, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(558, 25);
            this.label8.TabIndex = 8;
            this.label8.Text = "This Software failed to initialize because it\'s not activated";
            // 
            // tabDatabaseConnection
            // 
            this.tabDatabaseConnection.Controls.Add(this.panel1);
            this.tabDatabaseConnection.Controls.Add(this.btnConnect);
            this.tabDatabaseConnection.Controls.Add(this.txtPassword);
            this.tabDatabaseConnection.Controls.Add(this.label5);
            this.tabDatabaseConnection.Controls.Add(this.txtUserName);
            this.tabDatabaseConnection.Controls.Add(this.label4);
            this.tabDatabaseConnection.Controls.Add(this.txtDatabaseName);
            this.tabDatabaseConnection.Controls.Add(this.label3);
            this.tabDatabaseConnection.Controls.Add(this.btnTestConnections);
            this.tabDatabaseConnection.Controls.Add(this.txtDatabaseServerName);
            this.tabDatabaseConnection.Controls.Add(this.label2);
            this.tabDatabaseConnection.Controls.Add(this.label1);
            this.tabDatabaseConnection.HorizontalScrollbarBarColor = true;
            this.tabDatabaseConnection.HorizontalScrollbarHighlightOnWheel = false;
            this.tabDatabaseConnection.HorizontalScrollbarSize = 10;
            this.tabDatabaseConnection.Location = new System.Drawing.Point(4, 38);
            this.tabDatabaseConnection.Name = "tabDatabaseConnection";
            this.tabDatabaseConnection.Size = new System.Drawing.Size(763, 371);
            this.tabDatabaseConnection.TabIndex = 2;
            this.tabDatabaseConnection.Text = "Database Connection";
            this.tabDatabaseConnection.VerticalScrollbarBarColor = true;
            this.tabDatabaseConnection.VerticalScrollbarHighlightOnWheel = false;
            this.tabDatabaseConnection.VerticalScrollbarSize = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Cursor = System.Windows.Forms.Cursors.No;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 375);
            this.panel1.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 148);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 64);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.Location = new System.Drawing.Point(486, 228);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(137, 31);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "SAVE";
            this.btnConnect.UseSelectable = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPassword
            // 
            // 
            // 
            // 
            this.txtPassword.CustomButton.Image = null;
            this.txtPassword.CustomButton.Location = new System.Drawing.Point(307, 1);
            this.txtPassword.CustomButton.Name = "";
            this.txtPassword.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPassword.CustomButton.TabIndex = 1;
            this.txtPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPassword.CustomButton.UseSelectable = true;
            this.txtPassword.CustomButton.Visible = false;
            this.txtPassword.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPassword.Lines = new string[0];
            this.txtPassword.Location = new System.Drawing.Point(116, 262);
            this.txtPassword.MaxLength = 19;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.ShortcutsEnabled = true;
            this.txtPassword.Size = new System.Drawing.Size(331, 25);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSelectable = true;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPassword.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(113, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "Password:";
            // 
            // txtUserName
            // 
            // 
            // 
            // 
            this.txtUserName.CustomButton.Image = null;
            this.txtUserName.CustomButton.Location = new System.Drawing.Point(307, 1);
            this.txtUserName.CustomButton.Name = "";
            this.txtUserName.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtUserName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUserName.CustomButton.TabIndex = 1;
            this.txtUserName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUserName.CustomButton.UseSelectable = true;
            this.txtUserName.CustomButton.Visible = false;
            this.txtUserName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUserName.Lines = new string[0];
            this.txtUserName.Location = new System.Drawing.Point(116, 217);
            this.txtUserName.MaxLength = 19;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserName.SelectedText = "";
            this.txtUserName.SelectionLength = 0;
            this.txtUserName.SelectionStart = 0;
            this.txtUserName.ShortcutsEnabled = true;
            this.txtUserName.Size = new System.Drawing.Size(331, 25);
            this.txtUserName.TabIndex = 3;
            this.txtUserName.UseSelectable = true;
            this.txtUserName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUserName.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(113, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "User Name:";
            // 
            // txtDatabaseName
            // 
            // 
            // 
            // 
            this.txtDatabaseName.CustomButton.Image = null;
            this.txtDatabaseName.CustomButton.Location = new System.Drawing.Point(307, 1);
            this.txtDatabaseName.CustomButton.Name = "";
            this.txtDatabaseName.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtDatabaseName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDatabaseName.CustomButton.TabIndex = 1;
            this.txtDatabaseName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDatabaseName.CustomButton.UseSelectable = true;
            this.txtDatabaseName.CustomButton.Visible = false;
            this.txtDatabaseName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDatabaseName.Lines = new string[0];
            this.txtDatabaseName.Location = new System.Drawing.Point(116, 172);
            this.txtDatabaseName.MaxLength = 19;
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.PasswordChar = '\0';
            this.txtDatabaseName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDatabaseName.SelectedText = "";
            this.txtDatabaseName.SelectionLength = 0;
            this.txtDatabaseName.SelectionStart = 0;
            this.txtDatabaseName.ShortcutsEnabled = true;
            this.txtDatabaseName.Size = new System.Drawing.Size(331, 25);
            this.txtDatabaseName.TabIndex = 2;
            this.txtDatabaseName.UseSelectable = true;
            this.txtDatabaseName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDatabaseName.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(113, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Database Name:";
            // 
            // btnTestConnections
            // 
            this.btnTestConnections.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestConnections.Location = new System.Drawing.Point(486, 153);
            this.btnTestConnections.Name = "btnTestConnections";
            this.btnTestConnections.Size = new System.Drawing.Size(137, 31);
            this.btnTestConnections.TabIndex = 5;
            this.btnTestConnections.Text = "TEST CONNECTIONS";
            this.btnTestConnections.UseSelectable = true;
            this.btnTestConnections.Click += new System.EventHandler(this.btnTestConnections_Click);
            // 
            // txtDatabaseServerName
            // 
            // 
            // 
            // 
            this.txtDatabaseServerName.CustomButton.Image = null;
            this.txtDatabaseServerName.CustomButton.Location = new System.Drawing.Point(307, 1);
            this.txtDatabaseServerName.CustomButton.Name = "";
            this.txtDatabaseServerName.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtDatabaseServerName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDatabaseServerName.CustomButton.TabIndex = 1;
            this.txtDatabaseServerName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDatabaseServerName.CustomButton.UseSelectable = true;
            this.txtDatabaseServerName.CustomButton.Visible = false;
            this.txtDatabaseServerName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDatabaseServerName.Lines = new string[0];
            this.txtDatabaseServerName.Location = new System.Drawing.Point(116, 127);
            this.txtDatabaseServerName.MaxLength = 19;
            this.txtDatabaseServerName.Name = "txtDatabaseServerName";
            this.txtDatabaseServerName.PasswordChar = '\0';
            this.txtDatabaseServerName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDatabaseServerName.SelectedText = "";
            this.txtDatabaseServerName.SelectionLength = 0;
            this.txtDatabaseServerName.SelectionStart = 0;
            this.txtDatabaseServerName.ShortcutsEnabled = true;
            this.txtDatabaseServerName.Size = new System.Drawing.Size(331, 25);
            this.txtDatabaseServerName.TabIndex = 1;
            this.txtDatabaseServerName.UseSelectable = true;
            this.txtDatabaseServerName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDatabaseServerName.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Database Server Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(111, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Database Server Detail";
            // 
            // tabReciptPrinter
            // 
            this.tabReciptPrinter.Controls.Add(this.txtLabelPrinter);
            this.tabReciptPrinter.Controls.Add(this.label11);
            this.tabReciptPrinter.Controls.Add(this.label15);
            this.tabReciptPrinter.Controls.Add(this.txtPrinterName);
            this.tabReciptPrinter.Controls.Add(this.label14);
            this.tabReciptPrinter.Controls.Add(this.label12);
            this.tabReciptPrinter.Controls.Add(this.btnSaveUpdate_ReciptPrinter);
            this.tabReciptPrinter.Controls.Add(this.panel4);
            this.tabReciptPrinter.HorizontalScrollbarBarColor = true;
            this.tabReciptPrinter.HorizontalScrollbarHighlightOnWheel = false;
            this.tabReciptPrinter.HorizontalScrollbarSize = 10;
            this.tabReciptPrinter.Location = new System.Drawing.Point(4, 38);
            this.tabReciptPrinter.Name = "tabReciptPrinter";
            this.tabReciptPrinter.Size = new System.Drawing.Size(763, 371);
            this.tabReciptPrinter.TabIndex = 3;
            this.tabReciptPrinter.Text = "Printers Setting";
            this.tabReciptPrinter.VerticalScrollbarBarColor = true;
            this.tabReciptPrinter.VerticalScrollbarHighlightOnWheel = false;
            this.tabReciptPrinter.VerticalScrollbarSize = 10;
            // 
            // txtLabelPrinter
            // 
            // 
            // 
            // 
            this.txtLabelPrinter.CustomButton.Image = null;
            this.txtLabelPrinter.CustomButton.Location = new System.Drawing.Point(307, 1);
            this.txtLabelPrinter.CustomButton.Name = "";
            this.txtLabelPrinter.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtLabelPrinter.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtLabelPrinter.CustomButton.TabIndex = 1;
            this.txtLabelPrinter.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtLabelPrinter.CustomButton.UseSelectable = true;
            this.txtLabelPrinter.CustomButton.Visible = false;
            this.txtLabelPrinter.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtLabelPrinter.Lines = new string[0];
            this.txtLabelPrinter.Location = new System.Drawing.Point(118, 176);
            this.txtLabelPrinter.MaxLength = 9999;
            this.txtLabelPrinter.Name = "txtLabelPrinter";
            this.txtLabelPrinter.PasswordChar = '\0';
            this.txtLabelPrinter.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLabelPrinter.SelectedText = "";
            this.txtLabelPrinter.SelectionLength = 0;
            this.txtLabelPrinter.SelectionStart = 0;
            this.txtLabelPrinter.ShortcutsEnabled = true;
            this.txtLabelPrinter.Size = new System.Drawing.Size(331, 25);
            this.txtLabelPrinter.TabIndex = 61;
            this.txtLabelPrinter.UseSelectable = true;
            this.txtLabelPrinter.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtLabelPrinter.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(115, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(206, 16);
            this.label11.TabIndex = 62;
            this.label11.Text = "Please Enter Label Printer Name:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(113, 121);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(207, 25);
            this.label15.TabIndex = 60;
            this.label15.Text = "Label Printer Setting";
            // 
            // txtPrinterName
            // 
            // 
            // 
            // 
            this.txtPrinterName.CustomButton.Image = null;
            this.txtPrinterName.CustomButton.Location = new System.Drawing.Point(307, 1);
            this.txtPrinterName.CustomButton.Name = "";
            this.txtPrinterName.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtPrinterName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPrinterName.CustomButton.TabIndex = 1;
            this.txtPrinterName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPrinterName.CustomButton.UseSelectable = true;
            this.txtPrinterName.CustomButton.Visible = false;
            this.txtPrinterName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPrinterName.Lines = new string[0];
            this.txtPrinterName.Location = new System.Drawing.Point(118, 79);
            this.txtPrinterName.MaxLength = 9999;
            this.txtPrinterName.Name = "txtPrinterName";
            this.txtPrinterName.PasswordChar = '\0';
            this.txtPrinterName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPrinterName.SelectedText = "";
            this.txtPrinterName.SelectionLength = 0;
            this.txtPrinterName.SelectionStart = 0;
            this.txtPrinterName.ShortcutsEnabled = true;
            this.txtPrinterName.Size = new System.Drawing.Size(331, 25);
            this.txtPrinterName.TabIndex = 58;
            this.txtPrinterName.UseSelectable = true;
            this.txtPrinterName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPrinterName.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(115, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(219, 16);
            this.label14.TabIndex = 59;
            this.label14.Text = "Please Enter Receipt Printer Name:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(113, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(227, 25);
            this.label12.TabIndex = 57;
            this.label12.Text = "Receipt Printer Setting";
            // 
            // btnSaveUpdate_ReciptPrinter
            // 
            this.btnSaveUpdate_ReciptPrinter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveUpdate_ReciptPrinter.Location = new System.Drawing.Point(312, 244);
            this.btnSaveUpdate_ReciptPrinter.Name = "btnSaveUpdate_ReciptPrinter";
            this.btnSaveUpdate_ReciptPrinter.Size = new System.Drawing.Size(137, 31);
            this.btnSaveUpdate_ReciptPrinter.TabIndex = 18;
            this.btnSaveUpdate_ReciptPrinter.Text = "SAVE";
            this.btnSaveUpdate_ReciptPrinter.UseSelectable = true;
            this.btnSaveUpdate_ReciptPrinter.Click += new System.EventHandler(this.btnSaveUpdate_ReciptPrinter_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel4.Controls.Add(this.pictureBox4);
            this.panel4.Cursor = System.Windows.Forms.Cursors.No;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 375);
            this.panel4.TabIndex = 15;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox4.Image = global::SFPOSWindows.Properties.Resources.receipt_printer;
            this.pictureBox4.Location = new System.Drawing.Point(18, 148);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(65, 64);
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            // 
            // tabSerialPort
            // 
            this.tabSerialPort.Controls.Add(this.metroLabel3);
            this.tabSerialPort.Controls.Add(this.toggleActive);
            this.tabSerialPort.Controls.Add(this.label20);
            this.tabSerialPort.Controls.Add(this.btnReadWeight);
            this.tabSerialPort.Controls.Add(this.lstItems);
            this.tabSerialPort.Controls.Add(this.label9);
            this.tabSerialPort.Controls.Add(this.label13);
            this.tabSerialPort.Controls.Add(this.btnSave);
            this.tabSerialPort.Controls.Add(this.cmbScale);
            this.tabSerialPort.Controls.Add(this.cmbScanner);
            this.tabSerialPort.Controls.Add(this.panel3);
            this.tabSerialPort.HorizontalScrollbarBarColor = true;
            this.tabSerialPort.HorizontalScrollbarHighlightOnWheel = false;
            this.tabSerialPort.HorizontalScrollbarSize = 10;
            this.tabSerialPort.Location = new System.Drawing.Point(4, 38);
            this.tabSerialPort.Name = "tabSerialPort";
            this.tabSerialPort.Size = new System.Drawing.Size(763, 371);
            this.tabSerialPort.TabIndex = 1;
            this.tabSerialPort.Text = "Scanner/Scale";
            this.tabSerialPort.VerticalScrollbarBarColor = true;
            this.tabSerialPort.VerticalScrollbarHighlightOnWheel = false;
            this.tabSerialPort.VerticalScrollbarSize = 10;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(529, 173);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(143, 19);
            this.metroLabel3.TabIndex = 83;
            this.metroLabel3.Text = "Scanner/Scale In Used";
            // 
            // toggleActive
            // 
            this.toggleActive.DisplayStatus = false;
            this.toggleActive.Location = new System.Drawing.Point(529, 195);
            this.toggleActive.Name = "toggleActive";
            this.toggleActive.Size = new System.Drawing.Size(143, 29);
            this.toggleActive.TabIndex = 82;
            this.toggleActive.Text = "Off";
            this.toggleActive.UseSelectable = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(517, 227);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(190, 45);
            this.label20.TabIndex = 81;
            this.label20.Text = "* If scanner attached this system \r\nthen active above flag and scanner\r\nnot attac" +
    "hed then in-active this.";
            // 
            // btnReadWeight
            // 
            this.btnReadWeight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReadWeight.Location = new System.Drawing.Point(529, 73);
            this.btnReadWeight.Name = "btnReadWeight";
            this.btnReadWeight.Size = new System.Drawing.Size(137, 29);
            this.btnReadWeight.TabIndex = 71;
            this.btnReadWeight.Text = "READ WEIGHT";
            this.btnReadWeight.UseSelectable = true;
            this.btnReadWeight.Visible = false;
            this.btnReadWeight.Click += new System.EventHandler(this.btnReadWeight_Click);
            // 
            // lstItems
            // 
            this.lstItems.FormattingEnabled = true;
            this.lstItems.Location = new System.Drawing.Point(123, 137);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(385, 212);
            this.lstItems.TabIndex = 70;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(120, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 16);
            this.label9.TabIndex = 69;
            this.label9.Text = "Select Scale Type:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(120, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 16);
            this.label13.TabIndex = 68;
            this.label13.Text = "Select Scanner Type:";
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(529, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(137, 29);
            this.btnSave.TabIndex = 67;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseSelectable = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbScale
            // 
            this.cmbScale.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.Location = new System.Drawing.Point(268, 73);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(240, 29);
            this.cmbScale.TabIndex = 66;
            this.cmbScale.SelectedIndexChanged += new System.EventHandler(this.cmbScale_SelectedIndexChanged);
            // 
            // cmbScanner
            // 
            this.cmbScanner.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbScanner.FormattingEnabled = true;
            this.cmbScanner.Location = new System.Drawing.Point(268, 15);
            this.cmbScanner.Name = "cmbScanner";
            this.cmbScanner.Size = new System.Drawing.Size(240, 29);
            this.cmbScanner.TabIndex = 65;
            this.cmbScanner.SelectedIndexChanged += new System.EventHandler(this.cmbScanner_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Cursor = System.Windows.Forms.Cursors.No;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(100, 375);
            this.panel3.TabIndex = 14;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.Image = global::SFPOSWindows.Properties.Resources.serial_port;
            this.pictureBox3.Location = new System.Drawing.Point(18, 148);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(65, 64);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // tabSystemUpdate
            // 
            this.tabSystemUpdate.Controls.Add(this.btnCheckUpdate);
            this.tabSystemUpdate.Controls.Add(this.lblLastRelase);
            this.tabSystemUpdate.Controls.Add(this.label28);
            this.tabSystemUpdate.Controls.Add(this.lblCurrentRelase);
            this.tabSystemUpdate.Controls.Add(this.label26);
            this.tabSystemUpdate.Controls.Add(this.lblReleaseUpdatedDate);
            this.tabSystemUpdate.Controls.Add(this.lblCurrentVersion);
            this.tabSystemUpdate.Controls.Add(this.label16);
            this.tabSystemUpdate.Controls.Add(this.label21);
            this.tabSystemUpdate.Controls.Add(this.label22);
            this.tabSystemUpdate.Controls.Add(this.panel7);
            this.tabSystemUpdate.HorizontalScrollbarBarColor = true;
            this.tabSystemUpdate.HorizontalScrollbarHighlightOnWheel = false;
            this.tabSystemUpdate.HorizontalScrollbarSize = 10;
            this.tabSystemUpdate.Location = new System.Drawing.Point(4, 38);
            this.tabSystemUpdate.Name = "tabSystemUpdate";
            this.tabSystemUpdate.Size = new System.Drawing.Size(763, 371);
            this.tabSystemUpdate.TabIndex = 5;
            this.tabSystemUpdate.Text = "System Update";
            this.tabSystemUpdate.VerticalScrollbarBarColor = true;
            this.tabSystemUpdate.VerticalScrollbarHighlightOnWheel = false;
            this.tabSystemUpdate.VerticalScrollbarSize = 10;
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckUpdate.Highlight = true;
            this.btnCheckUpdate.Location = new System.Drawing.Point(331, 150);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(106, 31);
            this.btnCheckUpdate.TabIndex = 42;
            this.btnCheckUpdate.Text = "Check Update";
            this.btnCheckUpdate.UseSelectable = true;
            this.btnCheckUpdate.Visible = false;
            // 
            // lblLastRelase
            // 
            this.lblLastRelase.AutoSize = true;
            this.lblLastRelase.BackColor = System.Drawing.Color.Transparent;
            this.lblLastRelase.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastRelase.Location = new System.Drawing.Point(613, 86);
            this.lblLastRelase.Name = "lblLastRelase";
            this.lblLastRelase.Size = new System.Drawing.Size(19, 21);
            this.lblLastRelase.TabIndex = 41;
            this.lblLastRelase.Text = "0";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(458, 86);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(133, 21);
            this.label28.TabIndex = 40;
            this.label28.Text = "Last Release No.:";
            // 
            // lblCurrentRelase
            // 
            this.lblCurrentRelase.AutoSize = true;
            this.lblCurrentRelase.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentRelase.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentRelase.Location = new System.Drawing.Point(278, 86);
            this.lblCurrentRelase.Name = "lblCurrentRelase";
            this.lblCurrentRelase.Size = new System.Drawing.Size(19, 21);
            this.lblCurrentRelase.TabIndex = 39;
            this.lblCurrentRelase.Text = "0";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(114, 86);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(159, 21);
            this.label26.TabIndex = 38;
            this.label26.Text = "Current Release No.:";
            // 
            // lblReleaseUpdatedDate
            // 
            this.lblReleaseUpdatedDate.AutoSize = true;
            this.lblReleaseUpdatedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblReleaseUpdatedDate.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReleaseUpdatedDate.Location = new System.Drawing.Point(610, 56);
            this.lblReleaseUpdatedDate.Name = "lblReleaseUpdatedDate";
            this.lblReleaseUpdatedDate.Size = new System.Drawing.Size(94, 21);
            this.lblReleaseUpdatedDate.TabIndex = 37;
            this.lblReleaseUpdatedDate.Text = "2020-08-20";
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentVersion.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentVersion.Location = new System.Drawing.Point(246, 56);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(32, 21);
            this.lblCurrentVersion.TabIndex = 36;
            this.lblCurrentVersion.Text = "2.2";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(458, 56);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(149, 21);
            this.label16.TabIndex = 35;
            this.label16.Text = "Last Updated Date:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(113, 56);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(127, 21);
            this.label21.TabIndex = 34;
            this.label21.Text = "Current Version:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(113, 15);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(141, 25);
            this.label22.TabIndex = 33;
            this.label22.Text = "System Update";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel7.Controls.Add(this.pictureBox7);
            this.panel7.Cursor = System.Windows.Forms.Cursors.No;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(100, 375);
            this.panel7.TabIndex = 32;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox7.Image = global::SFPOSWindows.Properties.Resources.ezPOSPro;
            this.pictureBox7.Location = new System.Drawing.Point(18, 150);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(65, 64);
            this.pictureBox7.TabIndex = 1;
            this.pictureBox7.TabStop = false;
            // 
            // frmSettings_BE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(772, 441);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings_BE";
            this.Text = "Master Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.VisibleChanged += new System.EventHandler(this.frmSettings_BE_VisibleChanged);
            this.Leave += new System.EventHandler(this.frmSettings_BE_Leave);
            this.TabControl.ResumeLayout(false);
            this.tabezPOSProInfo.ResumeLayout(false);
            this.tabezPOSProInfo.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.tabLicenceKey.ResumeLayout(false);
            this.tabLicenceKey.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabDatabaseConnection.ResumeLayout(false);
            this.tabDatabaseConnection.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabReciptPrinter.ResumeLayout(false);
            this.tabReciptPrinter.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tabSerialPort.ResumeLayout(false);
            this.tabSerialPort.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabSystemUpdate.ResumeLayout(false);
            this.tabSystemUpdate.PerformLayout();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl TabControl;
        private MetroFramework.Controls.MetroTabPage tabezPOSProInfo;
        private MetroFramework.Controls.MetroTabPage tabSerialPort;
        private MetroFramework.Controls.MetroTabPage tabDatabaseConnection;
        private MetroFramework.Controls.MetroTabPage tabReciptPrinter;
        private MetroFramework.Controls.MetroTabPage tabLicenceKey;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroButton btnConnect;
        private MetroFramework.Controls.MetroTextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroTextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroTextBox txtDatabaseName;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroButton btnTestConnections;
        private MetroFramework.Controls.MetroTextBox txtDatabaseServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroButton btnActive;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroTextBox txtKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label10;
        private MetroFramework.Controls.MetroButton btnSaveUpdate_ReciptPrinter;
        private System.Windows.Forms.Label label12;
        private MetroFramework.Controls.MetroTextBox txtPrinterName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblInstationoDate;
        private System.Windows.Forms.Label lblPOSType;
        private System.Windows.Forms.RichTextBox txtLicense;
        private MetroFramework.Controls.MetroButton btnReadWeight;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private MetroFramework.Controls.MetroButton btnSave;
        private System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.ComboBox cmbScanner;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        public MetroFramework.Controls.MetroToggle toggleActive;
        private System.Windows.Forms.Label label20;
        private MetroFramework.Controls.MetroTextBox txtLabelPrinter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private MetroFramework.Controls.MetroTabPage tabSystemUpdate;
        private MetroFramework.Controls.MetroButton btnCheckUpdate;
        private System.Windows.Forms.Label lblLastRelase;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblCurrentRelase;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblReleaseUpdatedDate;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox pictureBox7;
    }
}