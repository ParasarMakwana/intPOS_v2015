namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddProducVendor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddProducVendor));
            this.txtVendorUPCCode = new MetroFramework.Controls.MetroTextBox();
            this.cmbVendorName = new MetroFramework.Controls.MetroComboBox();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.txtUnitCost = new MetroFramework.Controls.MetroTextBox();
            this.txtProductName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.toggleDefault = new MetroFramework.Controls.MetroToggle();
            this.SuspendLayout();
            // 
            // txtVendorUPCCode
            // 
            this.txtVendorUPCCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtVendorUPCCode.CustomButton.Image = null;
            this.txtVendorUPCCode.CustomButton.Location = new System.Drawing.Point(201, 1);
            this.txtVendorUPCCode.CustomButton.Name = "";
            this.txtVendorUPCCode.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtVendorUPCCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVendorUPCCode.CustomButton.TabIndex = 1;
            this.txtVendorUPCCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVendorUPCCode.CustomButton.UseSelectable = true;
            this.txtVendorUPCCode.CustomButton.Visible = false;
            this.txtVendorUPCCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtVendorUPCCode.Lines = new string[0];
            this.txtVendorUPCCode.Location = new System.Drawing.Point(99, 174);
            this.txtVendorUPCCode.MaxLength = 20;
            this.txtVendorUPCCode.Name = "txtVendorUPCCode";
            this.txtVendorUPCCode.PasswordChar = '\0';
            this.txtVendorUPCCode.PromptText = "Item Code";
            this.txtVendorUPCCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtVendorUPCCode.SelectedText = "";
            this.txtVendorUPCCode.SelectionLength = 0;
            this.txtVendorUPCCode.SelectionStart = 0;
            this.txtVendorUPCCode.ShortcutsEnabled = true;
            this.txtVendorUPCCode.Size = new System.Drawing.Size(229, 29);
            this.txtVendorUPCCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVendorUPCCode.TabIndex = 2;
            this.txtVendorUPCCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVendorUPCCode.UseSelectable = true;
            this.txtVendorUPCCode.WaterMark = "Item Code";
            this.txtVendorUPCCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtVendorUPCCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtVendorUPCCode.TextChanged += new System.EventHandler(this.txtVendorUPCCode_TextChanged);
            // 
            // cmbVendorName
            // 
            this.cmbVendorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVendorName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbVendorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbVendorName.FormattingEnabled = true;
            this.cmbVendorName.ItemHeight = 23;
            this.cmbVendorName.Location = new System.Drawing.Point(99, 124);
            this.cmbVendorName.Name = "cmbVendorName";
            this.cmbVendorName.Size = new System.Drawing.Size(229, 29);
            this.cmbVendorName.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbVendorName.TabIndex = 1;
            this.cmbVendorName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbVendorName.UseSelectable = true;
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(236, 312);
            this.metroBtnClear.Name = "metroBtnClear";
            this.metroBtnClear.Size = new System.Drawing.Size(92, 31);
            this.metroBtnClear.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnClear.TabIndex = 5;
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
            this.MetrobtnSave.Location = new System.Drawing.Point(128, 312);
            this.MetrobtnSave.Name = "MetrobtnSave";
            this.MetrobtnSave.Size = new System.Drawing.Size(92, 31);
            this.MetrobtnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnSave.TabIndex = 4;
            this.MetrobtnSave.Text = "SAVE";
            this.MetrobtnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnSave.UseCustomForeColor = true;
            this.MetrobtnSave.UseSelectable = true;
            this.MetrobtnSave.Click += new System.EventHandler(this.MetrobtnSave_Click);
            // 
            // txtUnitCost
            // 
            this.txtUnitCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtUnitCost.CustomButton.Image = null;
            this.txtUnitCost.CustomButton.Location = new System.Drawing.Point(201, 1);
            this.txtUnitCost.CustomButton.Name = "";
            this.txtUnitCost.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtUnitCost.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUnitCost.CustomButton.TabIndex = 1;
            this.txtUnitCost.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUnitCost.CustomButton.UseSelectable = true;
            this.txtUnitCost.CustomButton.Visible = false;
            this.txtUnitCost.DisplayIcon = true;
            this.txtUnitCost.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUnitCost.Icon = global::SFPOSWindows.Properties.Resources.dollar_coin_money1;
            this.txtUnitCost.IconRight = true;
            this.txtUnitCost.Lines = new string[0];
            this.txtUnitCost.Location = new System.Drawing.Point(99, 227);
            this.txtUnitCost.MaxLength = 20;
            this.txtUnitCost.Name = "txtUnitCost";
            this.txtUnitCost.PasswordChar = '\0';
            this.txtUnitCost.PromptText = "Price";
            this.txtUnitCost.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUnitCost.SelectedText = "";
            this.txtUnitCost.SelectionLength = 0;
            this.txtUnitCost.SelectionStart = 0;
            this.txtUnitCost.ShortcutsEnabled = true;
            this.txtUnitCost.Size = new System.Drawing.Size(229, 29);
            this.txtUnitCost.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUnitCost.TabIndex = 3;
            this.txtUnitCost.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUnitCost.UseSelectable = true;
            this.txtUnitCost.WaterMark = "Price";
            this.txtUnitCost.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUnitCost.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtUnitCost.TextChanged += new System.EventHandler(this.txtUnitCost_TextChanged);
            // 
            // txtProductName
            // 
            this.txtProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtProductName.CustomButton.Image = null;
            this.txtProductName.CustomButton.Location = new System.Drawing.Point(201, 1);
            this.txtProductName.CustomButton.Name = "";
            this.txtProductName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtProductName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProductName.CustomButton.TabIndex = 1;
            this.txtProductName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProductName.CustomButton.UseSelectable = true;
            this.txtProductName.CustomButton.Visible = false;
            this.txtProductName.DisplayIcon = true;
            this.txtProductName.Enabled = false;
            this.txtProductName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtProductName.IconRight = true;
            this.txtProductName.Lines = new string[0];
            this.txtProductName.Location = new System.Drawing.Point(99, 73);
            this.txtProductName.MaxLength = 20;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.PasswordChar = '\0';
            this.txtProductName.ReadOnly = true;
            this.txtProductName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProductName.SelectedText = "";
            this.txtProductName.SelectionLength = 0;
            this.txtProductName.SelectionStart = 0;
            this.txtProductName.ShortcutsEnabled = true;
            this.txtProductName.Size = new System.Drawing.Size(229, 29);
            this.txtProductName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProductName.TabIndex = 13;
            this.txtProductName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProductName.UseSelectable = true;
            this.txtProductName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProductName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(23, 73);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(60, 19);
            this.metroLabel3.TabIndex = 35;
            this.metroLabel3.Text = "Product:";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 124);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(56, 19);
            this.metroLabel1.TabIndex = 36;
            this.metroLabel1.Text = "Vendor:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(23, 174);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(76, 19);
            this.metroLabel2.TabIndex = 37;
            this.metroLabel2.Text = "Item Code:";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(23, 227);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(41, 19);
            this.metroLabel4.TabIndex = 38;
            this.metroLabel4.Text = "Price:";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(23, 263);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(53, 19);
            this.metroLabel5.TabIndex = 40;
            this.metroLabel5.Text = "Default";
            this.metroLabel5.Visible = false;
            // 
            // toggleDefault
            // 
            this.toggleDefault.DisplayStatus = false;
            this.toggleDefault.Location = new System.Drawing.Point(99, 265);
            this.toggleDefault.Name = "toggleDefault";
            this.toggleDefault.Size = new System.Drawing.Size(50, 17);
            this.toggleDefault.TabIndex = 39;
            this.toggleDefault.Text = "Off";
            this.toggleDefault.UseSelectable = true;
            this.toggleDefault.Visible = false;
            // 
            // FrmMetro_AddProducVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 380);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.toggleDefault);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.txtUnitCost);
            this.Controls.Add(this.cmbVendorName);
            this.Controls.Add(this.txtVendorUPCCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(369, 380);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(369, 380);
            this.Name = "FrmMetro_AddProducVendor";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product - Vendor";
            this.Load += new System.EventHandler(this.FrmMetro_AddProducVendor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public MetroFramework.Controls.MetroTextBox txtVendorUPCCode;
        public MetroFramework.Controls.MetroComboBox cmbVendorName;
        public MetroFramework.Controls.MetroTextBox txtUnitCost;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        public MetroFramework.Controls.MetroTextBox txtProductName;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        public MetroFramework.Controls.MetroToggle toggleDefault;
    }
}