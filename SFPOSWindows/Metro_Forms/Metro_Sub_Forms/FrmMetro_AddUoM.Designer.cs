namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddUoM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddUoM));
            this.txtUoMName = new MetroFramework.Controls.MetroTextBox();
            this.txtDescription = new MetroFramework.Controls.MetroTextBox();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // txtUoMName
            // 
            this.txtUoMName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtUoMName.CustomButton.Image = null;
            this.txtUoMName.CustomButton.Location = new System.Drawing.Point(195, 1);
            this.txtUoMName.CustomButton.Name = "";
            this.txtUoMName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtUoMName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUoMName.CustomButton.TabIndex = 1;
            this.txtUoMName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUoMName.CustomButton.UseSelectable = true;
            this.txtUoMName.CustomButton.Visible = false;
            this.txtUoMName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUoMName.Lines = new string[0];
            this.txtUoMName.Location = new System.Drawing.Point(147, 78);
            this.txtUoMName.MaxLength = 20;
            this.txtUoMName.Name = "txtUoMName";
            this.txtUoMName.PasswordChar = '\0';
            this.txtUoMName.PromptText = "Unit Of Measure";
            this.txtUoMName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUoMName.SelectedText = "";
            this.txtUoMName.SelectionLength = 0;
            this.txtUoMName.SelectionStart = 0;
            this.txtUoMName.ShortcutsEnabled = true;
            this.txtUoMName.Size = new System.Drawing.Size(223, 29);
            this.txtUoMName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUoMName.TabIndex = 1;
            this.txtUoMName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUoMName.UseSelectable = true;
            this.txtUoMName.WaterMark = "Unit Of Measure";
            this.txtUoMName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUoMName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtUoMName.TextChanged += new System.EventHandler(this.txtUoMName_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDescription.CustomButton.Image = null;
            this.txtDescription.CustomButton.Location = new System.Drawing.Point(195, 1);
            this.txtDescription.CustomButton.Name = "";
            this.txtDescription.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtDescription.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDescription.CustomButton.TabIndex = 1;
            this.txtDescription.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDescription.CustomButton.UseSelectable = true;
            this.txtDescription.CustomButton.Visible = false;
            this.txtDescription.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDescription.Lines = new string[0];
            this.txtDescription.Location = new System.Drawing.Point(147, 128);
            this.txtDescription.MaxLength = 500;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PasswordChar = '\0';
            this.txtDescription.PromptText = "Description";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescription.SelectedText = "";
            this.txtDescription.SelectionLength = 0;
            this.txtDescription.SelectionStart = 0;
            this.txtDescription.ShortcutsEnabled = true;
            this.txtDescription.Size = new System.Drawing.Size(223, 29);
            this.txtDescription.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDescription.TabIndex = 2;
            this.txtDescription.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDescription.UseSelectable = true;
            this.txtDescription.WaterMark = "Description";
            this.txtDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(278, 197);
            this.metroBtnClear.Name = "metroBtnClear";
            this.metroBtnClear.Size = new System.Drawing.Size(92, 31);
            this.metroBtnClear.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnClear.TabIndex = 4;
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
            this.MetrobtnSave.Location = new System.Drawing.Point(171, 197);
            this.MetrobtnSave.Name = "MetrobtnSave";
            this.MetrobtnSave.Size = new System.Drawing.Size(92, 31);
            this.MetrobtnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnSave.TabIndex = 3;
            this.MetrobtnSave.Text = "SAVE";
            this.MetrobtnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnSave.UseCustomForeColor = true;
            this.MetrobtnSave.UseSelectable = true;
            this.MetrobtnSave.Click += new System.EventHandler(this.MetrobtnSave_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(23, 78);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(118, 19);
            this.metroLabel3.TabIndex = 55;
            this.metroLabel3.Text = "Unit Of Measure: ";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 128);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(85, 19);
            this.metroLabel1.TabIndex = 56;
            this.metroLabel1.Text = "Description: ";
            // 
            // FrmMetro_AddUoM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 264);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtUoMName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(401, 264);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(401, 264);
            this.Name = "FrmMetro_AddUoM";
            this.Resizable = false;
            this.Text = "Unit Of Measure";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroTextBox txtUoMName;
        public MetroFramework.Controls.MetroTextBox txtDescription;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}