namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddPurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddPurchaseOrder));
            this.cmbVendorName = new MetroFramework.Controls.MetroComboBox();
            this.txtVendorInvoiceNo = new MetroFramework.Controls.MetroTextBox();
            this.datePickerOrderDate = new MetroFramework.Controls.MetroDateTime();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // cmbVendorName
            // 
            this.cmbVendorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVendorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbVendorName.FormattingEnabled = true;
            this.cmbVendorName.ItemHeight = 23;
            this.cmbVendorName.Location = new System.Drawing.Point(120, 81);
            this.cmbVendorName.Name = "cmbVendorName";
            this.cmbVendorName.Size = new System.Drawing.Size(209, 29);
            this.cmbVendorName.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbVendorName.TabIndex = 0;
            this.cmbVendorName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbVendorName.UseSelectable = true;
            // 
            // txtVendorInvoiceNo
            // 
            this.txtVendorInvoiceNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtVendorInvoiceNo.CustomButton.Image = null;
            this.txtVendorInvoiceNo.CustomButton.Location = new System.Drawing.Point(181, 1);
            this.txtVendorInvoiceNo.CustomButton.Name = "";
            this.txtVendorInvoiceNo.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtVendorInvoiceNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVendorInvoiceNo.CustomButton.TabIndex = 1;
            this.txtVendorInvoiceNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVendorInvoiceNo.CustomButton.UseSelectable = true;
            this.txtVendorInvoiceNo.CustomButton.Visible = false;
            this.txtVendorInvoiceNo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtVendorInvoiceNo.Lines = new string[0];
            this.txtVendorInvoiceNo.Location = new System.Drawing.Point(120, 132);
            this.txtVendorInvoiceNo.MaxLength = 20;
            this.txtVendorInvoiceNo.Name = "txtVendorInvoiceNo";
            this.txtVendorInvoiceNo.PasswordChar = '\0';
            this.txtVendorInvoiceNo.PromptText = "P.O.Number";
            this.txtVendorInvoiceNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtVendorInvoiceNo.SelectedText = "";
            this.txtVendorInvoiceNo.SelectionLength = 0;
            this.txtVendorInvoiceNo.SelectionStart = 0;
            this.txtVendorInvoiceNo.ShortcutsEnabled = true;
            this.txtVendorInvoiceNo.Size = new System.Drawing.Size(209, 29);
            this.txtVendorInvoiceNo.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVendorInvoiceNo.TabIndex = 1;
            this.txtVendorInvoiceNo.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVendorInvoiceNo.UseSelectable = true;
            this.txtVendorInvoiceNo.WaterMark = "P.O.Number";
            this.txtVendorInvoiceNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtVendorInvoiceNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtVendorInvoiceNo.TextChanged += new System.EventHandler(this.txtVendorInvoiceNo_TextChanged);
            // 
            // datePickerOrderDate
            // 
            this.datePickerOrderDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerOrderDate.Location = new System.Drawing.Point(120, 185);
            this.datePickerOrderDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerOrderDate.Name = "datePickerOrderDate";
            this.datePickerOrderDate.Size = new System.Drawing.Size(209, 29);
            this.datePickerOrderDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerOrderDate.TabIndex = 2;
            this.datePickerOrderDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(237, 239);
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
            this.MetrobtnSave.Location = new System.Drawing.Point(124, 239);
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
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(27, 185);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(79, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Order Date:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(27, 132);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(87, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "P.O. Number:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(27, 81);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(55, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel3.TabIndex = 7;
            this.metroLabel3.Text = "Vendor:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // FrmMetro_AddPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 312);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.datePickerOrderDate);
            this.Controls.Add(this.cmbVendorName);
            this.Controls.Add(this.txtVendorInvoiceNo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(362, 312);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(362, 312);
            this.Name = "FrmMetro_AddPurchaseOrder";
            this.Resizable = false;
            this.Text = "Purchase Order";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroComboBox cmbVendorName;
        public MetroFramework.Controls.MetroTextBox txtVendorInvoiceNo;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        public MetroFramework.Controls.MetroDateTime datePickerOrderDate;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}