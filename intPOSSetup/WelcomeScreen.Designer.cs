namespace intPOSSetup
{
    partial class WelcomeScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeScreen));
            this.btnInstall = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkAgree = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rtbTerms = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblProgressmsg = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.Enabled = false;
            this.btnInstall.Location = new System.Drawing.Point(380, 29);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 71);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "intPOS";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(82, 51);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnOptions);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.chkAgree);
            this.panel2.Controls.Add(this.btnInstall);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 267);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(554, 64);
            this.panel2.TabIndex = 2;
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(299, 29);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(75, 23);
            this.btnOptions.TabIndex = 3;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Visible = false;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(461, 29);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkAgree
            // 
            this.chkAgree.AutoSize = true;
            this.chkAgree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAgree.Location = new System.Drawing.Point(296, 4);
            this.chkAgree.Name = "chkAgree";
            this.chkAgree.Size = new System.Drawing.Size(236, 19);
            this.chkAgree.TabIndex = 1;
            this.chkAgree.Text = "I agree to license terms and conditions";
            this.chkAgree.UseVisualStyleBackColor = true;
            this.chkAgree.CheckedChanged += new System.EventHandler(this.chkAgree_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.rtbTerms);
            this.panel3.Location = new System.Drawing.Point(0, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(551, 196);
            this.panel3.TabIndex = 2;
            // 
            // rtbTerms
            // 
            this.rtbTerms.BackColor = System.Drawing.Color.White;
            this.rtbTerms.Location = new System.Drawing.Point(12, 2);
            this.rtbTerms.Name = "rtbTerms";
            this.rtbTerms.ReadOnly = true;
            this.rtbTerms.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbTerms.Size = new System.Drawing.Size(524, 192);
            this.rtbTerms.TabIndex = 0;
            this.rtbTerms.Text = "";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.lblProgressmsg);
            this.panel4.Controls.Add(this.progressBar1);
            this.panel4.Location = new System.Drawing.Point(0, 71);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(553, 260);
            this.panel4.TabIndex = 3;
            // 
            // lblProgressmsg
            // 
            this.lblProgressmsg.AutoSize = true;
            this.lblProgressmsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressmsg.Location = new System.Drawing.Point(17, 49);
            this.lblProgressmsg.Name = "lblProgressmsg";
            this.lblProgressmsg.Size = new System.Drawing.Size(100, 31);
            this.lblProgressmsg.TabIndex = 2;
            this.lblProgressmsg.Text = "intPOS";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 93);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(527, 42);
            this.progressBar1.TabIndex = 0;
            // 
            // WelcomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 331);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WelcomeScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "intPOS - Setup";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbTerms;
        private System.Windows.Forms.CheckBox chkAgree;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblProgressmsg;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

