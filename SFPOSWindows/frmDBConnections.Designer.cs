namespace SFPOSWindows
{
    partial class frmDBConnections
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBConnections));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDatabaseServerName = new MetroFramework.Controls.MetroTextBox();
            this.btnTestConnections = new MetroFramework.Controls.MetroButton();
            this.txtDatabaseName = new MetroFramework.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new MetroFramework.Controls.MetroTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new MetroFramework.Controls.MetroTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnect = new MetroFramework.Controls.MetroButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(100, 324);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 133);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 64);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(111, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database Server Detail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database Server Name:";
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
            this.txtDatabaseServerName.Location = new System.Drawing.Point(116, 102);
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
            // btnTestConnections
            // 
            this.btnTestConnections.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestConnections.Highlight = true;
            this.btnTestConnections.Location = new System.Drawing.Point(486, 128);
            this.btnTestConnections.Name = "btnTestConnections";
            this.btnTestConnections.Size = new System.Drawing.Size(137, 31);
            this.btnTestConnections.TabIndex = 5;
            this.btnTestConnections.Text = "TEST CONNECTIONS";
            this.btnTestConnections.UseSelectable = true;
            this.btnTestConnections.Click += new System.EventHandler(this.btnTestConnections_Click);
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
            this.txtDatabaseName.Location = new System.Drawing.Point(116, 147);
            this.txtDatabaseName.MaxLength = 19;
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.PasswordChar = '\0';
            this.txtDatabaseName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDatabaseName.SelectedText = "";
            this.txtDatabaseName.SelectionLength = 0;
            this.txtDatabaseName.SelectionStart = 0;
            this.txtDatabaseName.ShortcutsEnabled = true;
            this.txtDatabaseName.Size = new System.Drawing.Size(331, 25);
            this.txtDatabaseName.TabIndex = 6;
            this.txtDatabaseName.UseSelectable = true;
            this.txtDatabaseName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDatabaseName.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(113, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Database Name:";
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
            this.txtUserName.Location = new System.Drawing.Point(116, 192);
            this.txtUserName.MaxLength = 19;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserName.SelectedText = "";
            this.txtUserName.SelectionLength = 0;
            this.txtUserName.SelectionStart = 0;
            this.txtUserName.ShortcutsEnabled = true;
            this.txtUserName.Size = new System.Drawing.Size(331, 25);
            this.txtUserName.TabIndex = 8;
            this.txtUserName.UseSelectable = true;
            this.txtUserName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUserName.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(113, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "User Name:";
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
            this.txtPassword.Location = new System.Drawing.Point(116, 237);
            this.txtPassword.MaxLength = 19;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.ShortcutsEnabled = true;
            this.txtPassword.Size = new System.Drawing.Size(331, 25);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.UseSelectable = true;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPassword.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(113, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Password:";
            // 
            // btnConnect
            // 
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.Highlight = true;
            this.btnConnect.Location = new System.Drawing.Point(486, 203);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(137, 31);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseSelectable = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // frmDBConnections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 324);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnTestConnections);
            this.Controls.Add(this.txtDatabaseServerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDBConnections";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroTextBox txtDatabaseServerName;
        private MetroFramework.Controls.MetroButton btnTestConnections;
        private MetroFramework.Controls.MetroTextBox txtDatabaseName;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroTextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroTextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroButton btnConnect;
    }
}