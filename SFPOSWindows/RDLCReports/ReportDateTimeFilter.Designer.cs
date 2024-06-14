namespace SFPOSWindows.RDLCReports
{
    partial class ReportDateTimeFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportDateTimeFilter));
            this.dtStartDate = new MetroFramework.Controls.MetroDateTime();
            this.dtEndDate = new MetroFramework.Controls.MetroDateTime();
            this.lblStartDate = new MetroFramework.Controls.MetroLabel();
            this.lblEndDate = new MetroFramework.Controls.MetroLabel();
            this.btnOK = new MetroFramework.Controls.MetroButton();
            this.radioTodayfilter = new MetroFramework.Controls.MetroRadioButton();
            this.radioMonthfilter = new MetroFramework.Controls.MetroRadioButton();
            this.radioYearfilter = new MetroFramework.Controls.MetroRadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioCustomfilter = new MetroFramework.Controls.MetroRadioButton();
            this.cmbMonth = new MetroFramework.Controls.MetroComboBox();
            this.lblMonth = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtStartDate
            // 
            this.dtStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtStartDate.CalendarFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtStartDate.Enabled = false;
            this.dtStartDate.FontWeight = MetroFramework.MetroDateTimeWeight.Light;
            this.dtStartDate.Location = new System.Drawing.Point(126, 159);
            this.dtStartDate.MinDate = new System.DateTime(1809, 12, 26, 23, 59, 59, 0);
            this.dtStartDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(239, 29);
            this.dtStartDate.TabIndex = 0;
            this.dtStartDate.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtStartDate.Value = new System.DateTime(2020, 2, 13, 0, 0, 0, 0);
            this.dtStartDate.ValueChanged += new System.EventHandler(this.dtStartDate_ValueChanged);
            // 
            // dtEndDate
            // 
            this.dtEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtEndDate.CalendarFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtEndDate.Enabled = false;
            this.dtEndDate.FontWeight = MetroFramework.MetroDateTimeWeight.Light;
            this.dtEndDate.Location = new System.Drawing.Point(126, 204);
            this.dtEndDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(239, 29);
            this.dtEndDate.TabIndex = 1;
            this.dtEndDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblStartDate.Location = new System.Drawing.Point(20, 159);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(74, 19);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblEndDate.Location = new System.Drawing.Point(20, 204);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(68, 19);
            this.lblEndDate.TabIndex = 3;
            this.lblEndDate.Text = "End Date:";
            // 
            // btnOK
            // 
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Highlight = true;
            this.btnOK.Location = new System.Drawing.Point(270, 262);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 29);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseSelectable = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // radioTodayfilter
            // 
            this.radioTodayfilter.AutoSize = true;
            this.radioTodayfilter.Checked = true;
            this.radioTodayfilter.Location = new System.Drawing.Point(28, 17);
            this.radioTodayfilter.Name = "radioTodayfilter";
            this.radioTodayfilter.Size = new System.Drawing.Size(49, 15);
            this.radioTodayfilter.TabIndex = 5;
            this.radioTodayfilter.TabStop = true;
            this.radioTodayfilter.Text = "Daily";
            this.radioTodayfilter.UseSelectable = true;
            this.radioTodayfilter.CheckedChanged += new System.EventHandler(this.radioTodayfilter_CheckedChanged);
            // 
            // radioMonthfilter
            // 
            this.radioMonthfilter.AutoSize = true;
            this.radioMonthfilter.Location = new System.Drawing.Point(100, 17);
            this.radioMonthfilter.Name = "radioMonthfilter";
            this.radioMonthfilter.Size = new System.Drawing.Size(68, 15);
            this.radioMonthfilter.TabIndex = 7;
            this.radioMonthfilter.Text = "Monthly";
            this.radioMonthfilter.UseSelectable = true;
            this.radioMonthfilter.CheckedChanged += new System.EventHandler(this.radioMonthfilter_CheckedChanged);
            // 
            // radioYearfilter
            // 
            this.radioYearfilter.AutoSize = true;
            this.radioYearfilter.Location = new System.Drawing.Point(189, 17);
            this.radioYearfilter.Name = "radioYearfilter";
            this.radioYearfilter.Size = new System.Drawing.Size(45, 15);
            this.radioYearfilter.TabIndex = 8;
            this.radioYearfilter.Text = "Year";
            this.radioYearfilter.UseSelectable = true;
            this.radioYearfilter.CheckedChanged += new System.EventHandler(this.radioYearfilter_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioCustomfilter);
            this.groupBox1.Controls.Add(this.radioYearfilter);
            this.groupBox1.Controls.Add(this.radioMonthfilter);
            this.groupBox1.Controls.Add(this.radioTodayfilter);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(19, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 45);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // radioCustomfilter
            // 
            this.radioCustomfilter.AutoSize = true;
            this.radioCustomfilter.Location = new System.Drawing.Point(256, 17);
            this.radioCustomfilter.Name = "radioCustomfilter";
            this.radioCustomfilter.Size = new System.Drawing.Size(65, 15);
            this.radioCustomfilter.TabIndex = 9;
            this.radioCustomfilter.Text = "Custom";
            this.radioCustomfilter.UseSelectable = true;
            this.radioCustomfilter.CheckedChanged += new System.EventHandler(this.radioCustomfilter_CheckedChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.ItemHeight = 23;
            this.cmbMonth.Items.AddRange(new object[] {
            "JANUARY",
            "FEBRUARY",
            "MARCH",
            "APRIL",
            "MAY",
            "JUNE",
            "JULY",
            "AUGUST",
            "SEPTEMBER",
            "OCTOBER",
            "NOVEMBER",
            "DECEMBER"});
            this.cmbMonth.Location = new System.Drawing.Point(126, 116);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.PromptText = "---SELECT MONTH---";
            this.cmbMonth.Size = new System.Drawing.Size(239, 29);
            this.cmbMonth.TabIndex = 10;
            this.cmbMonth.Text = "---SELECT MONTH---";
            this.cmbMonth.UseSelectable = true;
            this.cmbMonth.Visible = false;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblMonth.Location = new System.Drawing.Point(20, 116);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(93, 19);
            this.lblMonth.TabIndex = 11;
            this.lblMonth.Text = "Select Month:";
            this.lblMonth.Visible = false;
            // 
            // ReportDateTimeFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 314);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.dtStartDate);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportDateTimeFilter";
            this.Text = "Date Time";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel lblStartDate;
        private MetroFramework.Controls.MetroLabel lblEndDate;
        private MetroFramework.Controls.MetroButton btnOK;
        public MetroFramework.Controls.MetroDateTime dtStartDate;
        public MetroFramework.Controls.MetroDateTime dtEndDate;
        private MetroFramework.Controls.MetroRadioButton radioTodayfilter;
        private MetroFramework.Controls.MetroRadioButton radioMonthfilter;
        private MetroFramework.Controls.MetroRadioButton radioYearfilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroComboBox cmbMonth;
        private MetroFramework.Controls.MetroLabel lblMonth;
        private MetroFramework.Controls.MetroRadioButton radioCustomfilter;
    }
}