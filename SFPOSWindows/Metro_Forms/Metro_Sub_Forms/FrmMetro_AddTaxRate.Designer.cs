namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    partial class FrmMetro_AddTaxRate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetro_AddTaxRate));
            this.datePickerEndDate = new MetroFramework.Controls.MetroDateTime();
            this.datePickerStartDate = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroBtnClear = new MetroFramework.Controls.MetroButton();
            this.MetrobtnSave = new MetroFramework.Controls.MetroButton();
            this.cmbTaxGroupCode = new MetroFramework.Controls.MetroComboBox();
            this.txtTax = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerEndDate.CalendarFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEndDate.CalendarTrailingForeColor = System.Drawing.Color.Gray;
            this.datePickerEndDate.Location = new System.Drawing.Point(102, 226);
            this.datePickerEndDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.Size = new System.Drawing.Size(227, 29);
            this.datePickerEndDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerEndDate.TabIndex = 3;
            this.datePickerEndDate.Theme = MetroFramework.MetroThemeStyle.Light;
            this.datePickerEndDate.ValueChanged += new System.EventHandler(this.datePickerEndDate_ValueChanged);
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePickerStartDate.Location = new System.Drawing.Point(102, 175);
            this.datePickerStartDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.Size = new System.Drawing.Size(227, 29);
            this.datePickerStartDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerStartDate.TabIndex = 2;
            this.datePickerStartDate.Theme = MetroFramework.MetroThemeStyle.Light;
            this.datePickerStartDate.ValueChanged += new System.EventHandler(this.datePickerStartDate_ValueChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 175);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(74, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "Start Date:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(23, 226);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(68, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel2.TabIndex = 8;
            this.metroLabel2.Text = "End Date:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroBtnClear
            // 
            this.metroBtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroBtnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnClear.Location = new System.Drawing.Point(237, 283);
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
            this.MetrobtnSave.Location = new System.Drawing.Point(122, 283);
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
            // cmbTaxGroupCode
            // 
            this.cmbTaxGroupCode.FormattingEnabled = true;
            this.cmbTaxGroupCode.ItemHeight = 23;
            this.cmbTaxGroupCode.Location = new System.Drawing.Point(102, 74);
            this.cmbTaxGroupCode.Name = "cmbTaxGroupCode";
            this.cmbTaxGroupCode.PromptText = "--- Tax ---";
            this.cmbTaxGroupCode.Size = new System.Drawing.Size(226, 29);
            this.cmbTaxGroupCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbTaxGroupCode.TabIndex = 0;
            this.cmbTaxGroupCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbTaxGroupCode.UseSelectable = true;
            // 
            // txtTax
            // 
            this.txtTax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtTax.CustomButton.Image = null;
            this.txtTax.CustomButton.Location = new System.Drawing.Point(199, 1);
            this.txtTax.CustomButton.Name = "";
            this.txtTax.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtTax.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTax.CustomButton.TabIndex = 1;
            this.txtTax.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTax.CustomButton.UseSelectable = true;
            this.txtTax.CustomButton.Visible = false;
            this.txtTax.DisplayIcon = true;
            this.txtTax.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtTax.Icon = global::SFPOSWindows.Properties.Resources.percentage1;
            this.txtTax.IconRight = true;
            this.txtTax.Lines = new string[0];
            this.txtTax.Location = new System.Drawing.Point(102, 123);
            this.txtTax.MaxLength = 10;
            this.txtTax.Name = "txtTax";
            this.txtTax.PasswordChar = '\0';
            this.txtTax.PromptText = "Tax Rate";
            this.txtTax.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTax.SelectedText = "";
            this.txtTax.SelectionLength = 0;
            this.txtTax.SelectionStart = 0;
            this.txtTax.ShortcutsEnabled = true;
            this.txtTax.Size = new System.Drawing.Size(227, 29);
            this.txtTax.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTax.TabIndex = 1;
            this.txtTax.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTax.UseSelectable = true;
            this.txtTax.WaterMark = "Tax Rate";
            this.txtTax.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtTax.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtTax.TextChanged += new System.EventHandler(this.txtTax_TextChanged);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel9.Location = new System.Drawing.Point(23, 74);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(34, 19);
            this.metroLabel9.TabIndex = 53;
            this.metroLabel9.Text = "Tax: ";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(23, 123);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(65, 19);
            this.metroLabel3.TabIndex = 54;
            this.metroLabel3.Text = "Tax Rate: ";
            // 
            // FrmMetro_AddTaxRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 376);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.cmbTaxGroupCode);
            this.Controls.Add(this.metroBtnClear);
            this.Controls.Add(this.MetrobtnSave);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.datePickerEndDate);
            this.Controls.Add(this.datePickerStartDate);
            this.Controls.Add(this.txtTax);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(363, 376);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(363, 376);
            this.Name = "FrmMetro_AddTaxRate";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tax Rate";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroDateTime datePickerEndDate;
        public MetroFramework.Controls.MetroDateTime datePickerStartDate;
        public MetroFramework.Controls.MetroTextBox txtTax;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton metroBtnClear;
        private MetroFramework.Controls.MetroButton MetrobtnSave;
        public MetroFramework.Controls.MetroComboBox cmbTaxGroupCode;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}