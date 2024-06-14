namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddProducSaleDisc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddProducSaleDisc));
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.datePickerEndDate = new MetroFramework.Controls.MetroDateTime();
            this.datePickerStartDate = new MetroFramework.Controls.MetroDateTime();
            this.cmbProductName = new MetroFramework.Controls.MetroComboBox();
            this.txtSalesDiscount = new MetroFramework.Controls.MetroTextBox();
            this.txtProductName = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(204, 289);
            this.metroBtnClear.Name = "metroBtnClear";
            this.metroBtnClear.Size = new System.Drawing.Size(92, 31);
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
            this.MetrobtnSave.Location = new System.Drawing.Point(96, 289);
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
            // datePickerEndDate
            // 
            this.datePickerEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerEndDate.Location = new System.Drawing.Point(29, 240);
            this.datePickerEndDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.Size = new System.Drawing.Size(267, 29);
            this.datePickerEndDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerEndDate.TabIndex = 3;
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerStartDate.Location = new System.Drawing.Point(29, 185);
            this.datePickerStartDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.Size = new System.Drawing.Size(267, 29);
            this.datePickerStartDate.TabIndex = 2;
            // 
            // cmbProductName
            // 
            this.cmbProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProductName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProductName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.ItemHeight = 23;
            this.cmbProductName.Location = new System.Drawing.Point(29, 79);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(267, 29);
            this.cmbProductName.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbProductName.TabIndex = 11;
            this.cmbProductName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbProductName.UseSelectable = true;
            this.cmbProductName.Visible = false;
            // 
            // txtSalesDiscount
            // 
            this.txtSalesDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSalesDiscount.CustomButton.Image = null;
            this.txtSalesDiscount.CustomButton.Location = new System.Drawing.Point(239, 1);
            this.txtSalesDiscount.CustomButton.Name = "";
            this.txtSalesDiscount.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSalesDiscount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSalesDiscount.CustomButton.TabIndex = 1;
            this.txtSalesDiscount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSalesDiscount.CustomButton.UseSelectable = true;
            this.txtSalesDiscount.CustomButton.Visible = false;
            this.txtSalesDiscount.DisplayIcon = true;
            this.txtSalesDiscount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSalesDiscount.Icon = global::SFPOSWindows.Properties.Resources.percentage1;
            this.txtSalesDiscount.IconRight = true;
            this.txtSalesDiscount.Lines = new string[0];
            this.txtSalesDiscount.Location = new System.Drawing.Point(29, 133);
            this.txtSalesDiscount.MaxLength = 20;
            this.txtSalesDiscount.Name = "txtSalesDiscount";
            this.txtSalesDiscount.PasswordChar = '\0';
            this.txtSalesDiscount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSalesDiscount.SelectedText = "";
            this.txtSalesDiscount.SelectionLength = 0;
            this.txtSalesDiscount.SelectionStart = 0;
            this.txtSalesDiscount.ShortcutsEnabled = true;
            this.txtSalesDiscount.Size = new System.Drawing.Size(267, 29);
            this.txtSalesDiscount.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSalesDiscount.TabIndex = 1;
            this.txtSalesDiscount.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSalesDiscount.UseSelectable = true;
            this.txtSalesDiscount.WaterMark = "Sale Discount";
            this.txtSalesDiscount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSalesDiscount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSalesDiscount.TextChanged += new System.EventHandler(this.txtSalesDiscount_TextChanged);
            // 
            // txtProductName
            // 
            this.txtProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtProductName.CustomButton.Image = null;
            this.txtProductName.CustomButton.Location = new System.Drawing.Point(239, 1);
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
            this.txtProductName.Location = new System.Drawing.Point(29, 79);
            this.txtProductName.MaxLength = 20;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.PasswordChar = '\0';
            this.txtProductName.ReadOnly = true;
            this.txtProductName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProductName.SelectedText = "";
            this.txtProductName.SelectionLength = 0;
            this.txtProductName.SelectionStart = 0;
            this.txtProductName.ShortcutsEnabled = true;
            this.txtProductName.Size = new System.Drawing.Size(267, 29);
            this.txtProductName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProductName.TabIndex = 12;
            this.txtProductName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProductName.UseSelectable = true;
            this.txtProductName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProductName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // FrmMetro_AddProducSaleDisc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 359);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.datePickerEndDate);
            this.Controls.Add(this.datePickerStartDate);
            this.Controls.Add(this.cmbProductName);
            this.Controls.Add(this.txtSalesDiscount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMetro_AddProducSaleDisc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sales Discount";
            this.Load += new System.EventHandler(this.FrmMetro_AddProducSaleDisc_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        public MetroFramework.Controls.MetroDateTime datePickerEndDate;
        public MetroFramework.Controls.MetroDateTime datePickerStartDate;
        public MetroFramework.Controls.MetroComboBox cmbProductName;
        public MetroFramework.Controls.MetroTextBox txtSalesDiscount;
        public MetroFramework.Controls.MetroTextBox txtProductName;
    }
}