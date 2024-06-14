namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddTaxGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddTaxGroup));
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.txtTaxGroupCode = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(198, 140);
            this.metroBtnClear.Name = "metroBtnClear";
            this.metroBtnClear.Size = new System.Drawing.Size(92, 31);
            this.metroBtnClear.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnClear.TabIndex = 3;
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
            this.MetrobtnSave.Location = new System.Drawing.Point(91, 140);
            this.MetrobtnSave.Name = "MetrobtnSave";
            this.MetrobtnSave.Size = new System.Drawing.Size(92, 31);
            this.MetrobtnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnSave.TabIndex = 2;
            this.MetrobtnSave.Text = "SAVE";
            this.MetrobtnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnSave.UseCustomForeColor = true;
            this.MetrobtnSave.UseSelectable = true;
            this.MetrobtnSave.Click += new System.EventHandler(this.MetrobtnSave_Click);
            // 
            // txtTaxGroupCode
            // 
            this.txtTaxGroupCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtTaxGroupCode.CustomButton.Image = null;
            this.txtTaxGroupCode.CustomButton.Location = new System.Drawing.Point(171, 1);
            this.txtTaxGroupCode.CustomButton.Name = "";
            this.txtTaxGroupCode.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtTaxGroupCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTaxGroupCode.CustomButton.TabIndex = 1;
            this.txtTaxGroupCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTaxGroupCode.CustomButton.UseSelectable = true;
            this.txtTaxGroupCode.CustomButton.Visible = false;
            this.txtTaxGroupCode.DisplayIcon = true;
            this.txtTaxGroupCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtTaxGroupCode.Icon = global::SFPOSWindows.Properties.Resources.percentage1;
            this.txtTaxGroupCode.IconRight = true;
            this.txtTaxGroupCode.Lines = new string[0];
            this.txtTaxGroupCode.Location = new System.Drawing.Point(91, 80);
            this.txtTaxGroupCode.MaxLength = 20;
            this.txtTaxGroupCode.Name = "txtTaxGroupCode";
            this.txtTaxGroupCode.PasswordChar = '\0';
            this.txtTaxGroupCode.PromptText = "Tax";
            this.txtTaxGroupCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTaxGroupCode.SelectedText = "";
            this.txtTaxGroupCode.SelectionLength = 0;
            this.txtTaxGroupCode.SelectionStart = 0;
            this.txtTaxGroupCode.ShortcutsEnabled = true;
            this.txtTaxGroupCode.Size = new System.Drawing.Size(199, 29);
            this.txtTaxGroupCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTaxGroupCode.TabIndex = 1;
            this.txtTaxGroupCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTaxGroupCode.UseSelectable = true;
            this.txtTaxGroupCode.WaterMark = "Tax";
            this.txtTaxGroupCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtTaxGroupCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtTaxGroupCode.TextChanged += new System.EventHandler(this.txtTaxGroupCode_TextChanged);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel9.Location = new System.Drawing.Point(23, 80);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(34, 19);
            this.metroLabel9.TabIndex = 53;
            this.metroLabel9.Text = "Tax: ";
            // 
            // FrmMetro_AddTaxGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 216);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.txtTaxGroupCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(325, 216);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(325, 216);
            this.Name = "FrmMetro_AddTaxGroup";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tax Group ";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroTextBox txtTaxGroupCode;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        private MetroFramework.Controls.MetroLabel metroLabel9;
    }
}