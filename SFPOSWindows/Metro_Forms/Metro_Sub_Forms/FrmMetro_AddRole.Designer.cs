namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddRole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddRole));
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.txtRoleType = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtOverwriteAmount = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(195, 158);
            this.metroBtnClear.Name = "metroBtnClear";
            this.metroBtnClear.Size = new System.Drawing.Size(92, 31);
            this.metroBtnClear.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnClear.TabIndex = 2;
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
            this.MetrobtnSave.Location = new System.Drawing.Point(85, 158);
            this.MetrobtnSave.Name = "MetrobtnSave";
            this.MetrobtnSave.Size = new System.Drawing.Size(92, 31);
            this.MetrobtnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnSave.TabIndex = 1;
            this.MetrobtnSave.Text = "SAVE";
            this.MetrobtnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnSave.UseCustomForeColor = true;
            this.MetrobtnSave.UseSelectable = true;
            this.MetrobtnSave.Click += new System.EventHandler(this.MetrobtnSave_Click);
            // 
            // txtRoleType
            // 
            this.txtRoleType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtRoleType.CustomButton.Image = null;
            this.txtRoleType.CustomButton.Location = new System.Drawing.Point(158, 1);
            this.txtRoleType.CustomButton.Name = "";
            this.txtRoleType.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtRoleType.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtRoleType.CustomButton.TabIndex = 1;
            this.txtRoleType.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtRoleType.CustomButton.UseSelectable = true;
            this.txtRoleType.CustomButton.Visible = false;
            this.txtRoleType.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtRoleType.Lines = new string[0];
            this.txtRoleType.Location = new System.Drawing.Point(101, 77);
            this.txtRoleType.MaxLength = 20;
            this.txtRoleType.Name = "txtRoleType";
            this.txtRoleType.PasswordChar = '\0';
            this.txtRoleType.PromptText = "Role";
            this.txtRoleType.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtRoleType.SelectedText = "";
            this.txtRoleType.SelectionLength = 0;
            this.txtRoleType.SelectionStart = 0;
            this.txtRoleType.ShortcutsEnabled = true;
            this.txtRoleType.Size = new System.Drawing.Size(186, 29);
            this.txtRoleType.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtRoleType.TabIndex = 0;
            this.txtRoleType.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtRoleType.UseSelectable = true;
            this.txtRoleType.WaterMark = "Role";
            this.txtRoleType.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtRoleType.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtRoleType.TextChanged += new System.EventHandler(this.txtRoleType_TextChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 77);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(38, 19);
            this.metroLabel1.TabIndex = 36;
            this.metroLabel1.Text = "Role:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(23, 112);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(65, 19);
            this.metroLabel2.TabIndex = 38;
            this.metroLabel2.Text = "Override:";
            this.metroLabel2.WrapToLine = true;
            // 
            // txtOverwriteAmount
            // 
            this.txtOverwriteAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtOverwriteAmount.CustomButton.Image = null;
            this.txtOverwriteAmount.CustomButton.Location = new System.Drawing.Point(158, 1);
            this.txtOverwriteAmount.CustomButton.Name = "";
            this.txtOverwriteAmount.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtOverwriteAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtOverwriteAmount.CustomButton.TabIndex = 1;
            this.txtOverwriteAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOverwriteAmount.CustomButton.UseSelectable = true;
            this.txtOverwriteAmount.CustomButton.Visible = false;
            this.txtOverwriteAmount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtOverwriteAmount.Lines = new string[0];
            this.txtOverwriteAmount.Location = new System.Drawing.Point(101, 112);
            this.txtOverwriteAmount.MaxLength = 20;
            this.txtOverwriteAmount.Name = "txtOverwriteAmount";
            this.txtOverwriteAmount.PasswordChar = '\0';
            this.txtOverwriteAmount.PromptText = "Override Amount";
            this.txtOverwriteAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOverwriteAmount.SelectedText = "";
            this.txtOverwriteAmount.SelectionLength = 0;
            this.txtOverwriteAmount.SelectionStart = 0;
            this.txtOverwriteAmount.ShortcutsEnabled = true;
            this.txtOverwriteAmount.Size = new System.Drawing.Size(186, 29);
            this.txtOverwriteAmount.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtOverwriteAmount.TabIndex = 37;
            this.txtOverwriteAmount.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOverwriteAmount.UseSelectable = true;
            this.txtOverwriteAmount.WaterMark = "Override Amount";
            this.txtOverwriteAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtOverwriteAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // FrmMetro_AddRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 212);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.txtOverwriteAmount);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.txtRoleType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(321, 212);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(321, 212);
            this.Name = "FrmMetro_AddRole";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Role ";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        public MetroFramework.Controls.MetroTextBox txtRoleType;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        public MetroFramework.Controls.MetroTextBox txtOverwriteAmount;
    }
}