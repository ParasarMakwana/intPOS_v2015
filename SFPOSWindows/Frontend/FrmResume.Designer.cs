namespace SFPOSWindows.Frontend
{
    partial class FrmResume
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResume));
            this.txtUPCCode = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUPCCode
            // 
            // 
            // 
            // 
            this.txtUPCCode.CustomButton.Image = null;
            this.txtUPCCode.CustomButton.Location = new System.Drawing.Point(236, 2);
            this.txtUPCCode.CustomButton.Name = "";
            this.txtUPCCode.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.txtUPCCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUPCCode.CustomButton.TabIndex = 1;
            this.txtUPCCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUPCCode.CustomButton.UseSelectable = true;
            this.txtUPCCode.CustomButton.Visible = false;
            this.txtUPCCode.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtUPCCode.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtUPCCode.Lines = new string[0];
            this.txtUPCCode.Location = new System.Drawing.Point(32, 90);
            this.txtUPCCode.MaxLength = 15;
            this.txtUPCCode.Name = "txtUPCCode";
            this.txtUPCCode.PasswordChar = '\0';
            this.txtUPCCode.PromptText = "Barcode";
            this.txtUPCCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUPCCode.SelectedText = "";
            this.txtUPCCode.SelectionLength = 0;
            this.txtUPCCode.SelectionStart = 0;
            this.txtUPCCode.ShortcutsEnabled = true;
            this.txtUPCCode.Size = new System.Drawing.Size(268, 34);
            this.txtUPCCode.TabIndex = 33;
            this.txtUPCCode.UseSelectable = true;
            this.txtUPCCode.WaterMark = "Barcode";
            this.txtUPCCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUPCCode.WaterMarkFont = new System.Drawing.Font("Courier New", 17.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUPCCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUPCCode_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 27);
            this.label2.TabIndex = 43;
            this.label2.Text = "Resume";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(225, 164);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 34);
            this.btnClose.TabIndex = 44;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmResume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 224);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUPCCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmResume";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmResume_FormClosing);
            this.Load += new System.EventHandler(this.FrmResume_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        public MetroFramework.Controls.MetroTextBox txtUPCCode;
    }
}