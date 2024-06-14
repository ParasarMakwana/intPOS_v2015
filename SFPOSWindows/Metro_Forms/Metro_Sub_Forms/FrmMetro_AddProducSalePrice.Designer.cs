namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddProducSalePrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddProducSalePrice));
            this.datePickerStartDate = new MetroFramework.Controls.MetroDateTime();
            this.datePickerEndDate = new MetroFramework.Controls.MetroDateTime();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.txtSalesPrice = new MetroFramework.Controls.MetroTextBox();
            this.txtProductName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerStartDate.Location = new System.Drawing.Point(106, 161);
            this.datePickerStartDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.Size = new System.Drawing.Size(230, 29);
            this.datePickerStartDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerStartDate.TabIndex = 1;
            this.datePickerStartDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerEndDate.Location = new System.Drawing.Point(106, 205);
            this.datePickerEndDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.Size = new System.Drawing.Size(230, 29);
            this.datePickerEndDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerEndDate.TabIndex = 2;
            this.datePickerEndDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(244, 258);
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
            this.MetrobtnSave.Location = new System.Drawing.Point(135, 258);
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
            // txtSalesPrice
            // 
            this.txtSalesPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSalesPrice.CustomButton.Image = null;
            this.txtSalesPrice.CustomButton.Location = new System.Drawing.Point(202, 1);
            this.txtSalesPrice.CustomButton.Name = "";
            this.txtSalesPrice.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSalesPrice.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSalesPrice.CustomButton.TabIndex = 1;
            this.txtSalesPrice.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSalesPrice.CustomButton.UseSelectable = true;
            this.txtSalesPrice.CustomButton.Visible = false;
            this.txtSalesPrice.DisplayIcon = true;
            this.txtSalesPrice.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSalesPrice.Icon = global::SFPOSWindows.Properties.Resources.dollar_coin_money1;
            this.txtSalesPrice.IconRight = true;
            this.txtSalesPrice.Lines = new string[0];
            this.txtSalesPrice.Location = new System.Drawing.Point(106, 119);
            this.txtSalesPrice.MaxLength = 10;
            this.txtSalesPrice.Name = "txtSalesPrice";
            this.txtSalesPrice.PasswordChar = '\0';
            this.txtSalesPrice.PromptText = "Sale Price";
            this.txtSalesPrice.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSalesPrice.SelectedText = "";
            this.txtSalesPrice.SelectionLength = 0;
            this.txtSalesPrice.SelectionStart = 0;
            this.txtSalesPrice.ShortcutsEnabled = true;
            this.txtSalesPrice.Size = new System.Drawing.Size(230, 29);
            this.txtSalesPrice.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSalesPrice.TabIndex = 0;
            this.txtSalesPrice.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSalesPrice.UseSelectable = true;
            this.txtSalesPrice.WaterMark = "Sale Price";
            this.txtSalesPrice.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSalesPrice.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSalesPrice.TextChanged += new System.EventHandler(this.txtSalesPrice_TextChanged);
            // 
            // txtProductName
            // 
            this.txtProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtProductName.CustomButton.Image = null;
            this.txtProductName.CustomButton.Location = new System.Drawing.Point(202, 1);
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
            this.txtProductName.Location = new System.Drawing.Point(106, 75);
            this.txtProductName.MaxLength = 20;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.PasswordChar = '\0';
            this.txtProductName.ReadOnly = true;
            this.txtProductName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProductName.SelectedText = "";
            this.txtProductName.SelectionLength = 0;
            this.txtProductName.SelectionStart = 0;
            this.txtProductName.ShortcutsEnabled = true;
            this.txtProductName.Size = new System.Drawing.Size(230, 29);
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
            this.metroLabel3.Location = new System.Drawing.Point(23, 75);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(60, 19);
            this.metroLabel3.TabIndex = 34;
            this.metroLabel3.Text = "Product:";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 119);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(69, 19);
            this.metroLabel1.TabIndex = 35;
            this.metroLabel1.Text = "Sale Price:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(23, 161);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(74, 19);
            this.metroLabel2.TabIndex = 36;
            this.metroLabel2.Text = "Start Date:";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(24, 205);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(68, 19);
            this.metroLabel4.TabIndex = 37;
            this.metroLabel4.Text = "End Date:";
            // 
            // FrmMetro_AddProducSalePrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 317);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.datePickerEndDate);
            this.Controls.Add(this.datePickerStartDate);
            this.Controls.Add(this.txtSalesPrice);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 317);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 317);
            this.Name = "FrmMetro_AddProducSalePrice";
            this.Resizable = false;
            this.Text = "Sale Price ";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmMetro_AddProducSalePrice_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroTextBox txtSalesPrice;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        public MetroFramework.Controls.MetroDateTime datePickerStartDate;
        public MetroFramework.Controls.MetroDateTime datePickerEndDate;
        public MetroFramework.Controls.MetroTextBox txtProductName;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel4;
    }
}