namespace SFPOSWindows.MenuForm
{
    partial class MenuDashBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuDashBoard));
            this.GrpSaleCategory = new System.Windows.Forms.GroupBox();
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbDropdown = new System.Windows.Forms.ComboBox();
            this.cartesianChartDay = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChartYear = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChartMonth = new LiveCharts.WinForms.CartesianChart();
            this.PanelGrid = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cartesianChartGross = new LiveCharts.WinForms.CartesianChart();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.angularGaugeToday = new LiveCharts.WinForms.AngularGauge();
            this.angularGaugeMonth = new LiveCharts.WinForms.AngularGauge();
            this.label1 = new System.Windows.Forms.Label();
            this.angularGaugeYear = new LiveCharts.WinForms.AngularGauge();
            this.GrpSaleCategory.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.PanelGrid.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpSaleCategory
            // 
            this.GrpSaleCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpSaleCategory.BackColor = System.Drawing.Color.White;
            this.GrpSaleCategory.Controls.Add(this.pieChart1);
            this.GrpSaleCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpSaleCategory.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.GrpSaleCategory.Location = new System.Drawing.Point(8, 377);
            this.GrpSaleCategory.Name = "GrpSaleCategory";
            this.GrpSaleCategory.Size = new System.Drawing.Size(769, 476);
            this.GrpSaleCategory.TabIndex = 17;
            this.GrpSaleCategory.TabStop = false;
            this.GrpSaleCategory.Text = "Department wise sale";
            // 
            // pieChart1
            // 
            this.pieChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pieChart1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pieChart1.Location = new System.Drawing.Point(13, 24);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(654, 437);
            this.pieChart1.TabIndex = 9;
            this.pieChart1.Text = "pieChart1";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.cmbDropdown);
            this.groupBox4.Controls.Add(this.cartesianChartDay);
            this.groupBox4.Controls.Add(this.cartesianChartYear);
            this.groupBox4.Controls.Add(this.cartesianChartMonth);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox4.Location = new System.Drawing.Point(7, 859);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(787, 597);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Day/Month/Year Sales";
            // 
            // cmbDropdown
            // 
            this.cmbDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDropdown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbDropdown.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDropdown.FormattingEnabled = true;
            this.cmbDropdown.Location = new System.Drawing.Point(560, 15);
            this.cmbDropdown.Name = "cmbDropdown";
            this.cmbDropdown.Size = new System.Drawing.Size(210, 25);
            this.cmbDropdown.TabIndex = 20;
            this.cmbDropdown.SelectedIndexChanged += new System.EventHandler(this.cmbDropdown_SelectedIndexChanged);
            // 
            // cartesianChartDay
            // 
            this.cartesianChartDay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChartDay.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cartesianChartDay.Location = new System.Drawing.Point(12, 46);
            this.cartesianChartDay.Name = "cartesianChartDay";
            this.cartesianChartDay.Size = new System.Drawing.Size(758, 544);
            this.cartesianChartDay.TabIndex = 21;
            this.cartesianChartDay.Text = "cartesianChart1";
            // 
            // cartesianChartYear
            // 
            this.cartesianChartYear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChartYear.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cartesianChartYear.Location = new System.Drawing.Point(12, 46);
            this.cartesianChartYear.Name = "cartesianChartYear";
            this.cartesianChartYear.Size = new System.Drawing.Size(683, 544);
            this.cartesianChartYear.TabIndex = 23;
            this.cartesianChartYear.Text = "cartesianChart1";
            // 
            // cartesianChartMonth
            // 
            this.cartesianChartMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChartMonth.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cartesianChartMonth.Location = new System.Drawing.Point(12, 46);
            this.cartesianChartMonth.Name = "cartesianChartMonth";
            this.cartesianChartMonth.Size = new System.Drawing.Size(683, 544);
            this.cartesianChartMonth.TabIndex = 22;
            this.cartesianChartMonth.Text = "cartesianChart1";
            // 
            // PanelGrid
            // 
            this.PanelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelGrid.Controls.Add(this.groupBox2);
            this.PanelGrid.Controls.Add(this.label4);
            this.PanelGrid.Controls.Add(this.label3);
            this.PanelGrid.Controls.Add(this.label2);
            this.PanelGrid.Controls.Add(this.angularGaugeToday);
            this.PanelGrid.Controls.Add(this.angularGaugeMonth);
            this.PanelGrid.Controls.Add(this.label1);
            this.PanelGrid.Controls.Add(this.groupBox4);
            this.PanelGrid.Controls.Add(this.GrpSaleCategory);
            this.PanelGrid.Controls.Add(this.angularGaugeYear);
            this.PanelGrid.Location = new System.Drawing.Point(-3, 12);
            this.PanelGrid.Name = "PanelGrid";
            this.PanelGrid.Size = new System.Drawing.Size(805, 2028);
            this.PanelGrid.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.cartesianChartGross);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox2.Location = new System.Drawing.Point(8, 1466);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(787, 327);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gross  Amount";
            // 
            // cartesianChartGross
            // 
            this.cartesianChartGross.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChartGross.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cartesianChartGross.Location = new System.Drawing.Point(7, 12);
            this.cartesianChartGross.Name = "cartesianChartGross";
            this.cartesianChartGross.Size = new System.Drawing.Size(762, 308);
            this.cartesianChartGross.TabIndex = 24;
            this.cartesianChartGross.Text = "cartesianChart1";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(600, 361);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "Yearly Sales";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(360, 359);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "Monthly Sales";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(120, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "Daily Sales";
            // 
            // angularGaugeToday
            // 
            this.angularGaugeToday.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.angularGaugeToday.Location = new System.Drawing.Point(9, 66);
            this.angularGaugeToday.Name = "angularGaugeToday";
            this.angularGaugeToday.Size = new System.Drawing.Size(294, 276);
            this.angularGaugeToday.TabIndex = 3;
            this.angularGaugeToday.Text = "angularGaugeToday";
            // 
            // angularGaugeMonth
            // 
            this.angularGaugeMonth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.angularGaugeMonth.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.angularGaugeMonth.Location = new System.Drawing.Point(250, 64);
            this.angularGaugeMonth.Name = "angularGaugeMonth";
            this.angularGaugeMonth.Size = new System.Drawing.Size(294, 276);
            this.angularGaugeMonth.TabIndex = 1;
            this.angularGaugeMonth.Text = "angularGaugeMonth";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 25);
            this.label1.TabIndex = 21;
            this.label1.Text = "DashBoard";
            // 
            // angularGaugeYear
            // 
            this.angularGaugeYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.angularGaugeYear.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.angularGaugeYear.Location = new System.Drawing.Point(481, 66);
            this.angularGaugeYear.Name = "angularGaugeYear";
            this.angularGaugeYear.Size = new System.Drawing.Size(294, 276);
            this.angularGaugeYear.TabIndex = 0;
            this.angularGaugeYear.Text = "angularGaugeYear";
            // 
            // MenuDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(906, 749);
            this.Controls.Add(this.PanelGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuDashBoard";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Dashboard";
            this.TransparencyKey = System.Drawing.Color.White;
            this.GrpSaleCategory.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.PanelGrid.ResumeLayout(false);
            this.PanelGrid.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpSaleCategory;
        private LiveCharts.WinForms.PieChart pieChart1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbDropdown;
        private LiveCharts.WinForms.CartesianChart cartesianChartDay;
        private LiveCharts.WinForms.CartesianChart cartesianChartYear;
        private LiveCharts.WinForms.CartesianChart cartesianChartMonth;
        private System.Windows.Forms.Panel PanelGrid;
        private System.Windows.Forms.Label label1;
        private LiveCharts.WinForms.AngularGauge angularGaugeToday;
        private LiveCharts.WinForms.AngularGauge angularGaugeYear;
        private LiveCharts.WinForms.AngularGauge angularGaugeMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private LiveCharts.WinForms.CartesianChart cartesianChartGross;
    }
}