namespace SFPOSWindows.Frontend
{
    partial class FrmAgeVerification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgeVerification));
            this.dtBirthdate = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtBirthdate
            // 
            this.dtBirthdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dtBirthdate.CustomButton.Image = null;
            this.dtBirthdate.CustomButton.Location = new System.Drawing.Point(217, 2);
            this.dtBirthdate.CustomButton.Name = "";
            this.dtBirthdate.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.dtBirthdate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.dtBirthdate.CustomButton.TabIndex = 1;
            this.dtBirthdate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtBirthdate.CustomButton.UseSelectable = true;
            this.dtBirthdate.CustomButton.Visible = false;
            this.dtBirthdate.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.dtBirthdate.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.dtBirthdate.IconRight = true;
            this.dtBirthdate.Lines = new string[0];
            this.dtBirthdate.Location = new System.Drawing.Point(23, 112);
            this.dtBirthdate.MaxLength = 5000;
            this.dtBirthdate.Name = "dtBirthdate";
            this.dtBirthdate.PasswordChar = '\0';
            this.dtBirthdate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dtBirthdate.SelectedText = "";
            this.dtBirthdate.SelectionLength = 0;
            this.dtBirthdate.SelectionStart = 0;
            this.dtBirthdate.ShortcutsEnabled = true;
            this.dtBirthdate.Size = new System.Drawing.Size(245, 30);
            this.dtBirthdate.TabIndex = 0;
            this.dtBirthdate.UseSelectable = true;
            this.dtBirthdate.WaterMark = "mm/dd/yyyy";
            this.dtBirthdate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.dtBirthdate.WaterMarkFont = new System.Drawing.Font("Courier New", 12.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtBirthdate.TextChanged += new System.EventHandler(this.dtBirthdate_TextChanged);
            this.dtBirthdate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtBirthdate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "Enter Customer\'s Birthdate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 27);
            this.label2.TabIndex = 32;
            this.label2.Text = "Age Verification";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(192, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnOK.FlatAppearance.BorderSize = 2;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(110, 178);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 34);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmAgeVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 262);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtBirthdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAgeVerification";
            this.Load += new System.EventHandler(this.FrmAgeVerification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public MetroFramework.Controls.MetroTextBox dtBirthdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}