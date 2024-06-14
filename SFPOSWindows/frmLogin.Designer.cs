namespace SFPOSWindows
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtUserId = new MetroFramework.Controls.MetroTextBox();
            this.txtPassword = new MetroFramework.Controls.MetroTextBox();
            this.btnLogin = new MetroFramework.Controls.MetroButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUserId
            // 
            this.txtUserId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtUserId.CustomButton.Image = null;
            this.txtUserId.CustomButton.Location = new System.Drawing.Point(190, 1);
            this.txtUserId.CustomButton.Name = "";
            this.txtUserId.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtUserId.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUserId.CustomButton.TabIndex = 1;
            this.txtUserId.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUserId.CustomButton.UseSelectable = true;
            this.txtUserId.CustomButton.Visible = false;
            this.txtUserId.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtUserId.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtUserId.Lines = new string[0];
            this.txtUserId.Location = new System.Drawing.Point(27, 120);
            this.txtUserId.MaxLength = 20;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.PasswordChar = '\0';
            this.txtUserId.PromptText = "User Id";
            this.txtUserId.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserId.SelectedText = "";
            this.txtUserId.SelectionLength = 0;
            this.txtUserId.SelectionStart = 0;
            this.txtUserId.ShortcutsEnabled = true;
            this.txtUserId.Size = new System.Drawing.Size(218, 29);
            this.txtUserId.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUserId.TabIndex = 0;
            this.txtUserId.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUserId.UseSelectable = true;
            this.txtUserId.WaterMark = "User Id";
            this.txtUserId.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUserId.WaterMarkFont = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmailId_KeyDown);
            this.txtUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserId_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtPassword.CustomButton.Image = null;
            this.txtPassword.CustomButton.Location = new System.Drawing.Point(190, 1);
            this.txtPassword.CustomButton.Name = "";
            this.txtPassword.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPassword.CustomButton.TabIndex = 1;
            this.txtPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPassword.CustomButton.UseSelectable = true;
            this.txtPassword.CustomButton.Visible = false;
            this.txtPassword.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtPassword.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtPassword.Lines = new string[0];
            this.txtPassword.Location = new System.Drawing.Point(27, 162);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PromptText = "Password";
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.ShortcutsEnabled = true;
            this.txtPassword.Size = new System.Drawing.Size(218, 29);
            this.txtPassword.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPassword.UseSelectable = true;
            this.txtPassword.WaterMark = "Password";
            this.txtPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmailId_KeyDown);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Location = new System.Drawing.Point(189, 214);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(56, 34);
            this.btnLogin.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnLogin.UseSelectable = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmailId_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SFPOSWindows.Properties.Resources.intPOS_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(69, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 100);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 281);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(279, 281);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(279, 281);
            this.Name = "frmLogin";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        public MetroFramework.Controls.MetroTextBox txtUserId;
        public MetroFramework.Controls.MetroTextBox txtPassword;
        private MetroFramework.Controls.MetroButton btnLogin;
    }
}