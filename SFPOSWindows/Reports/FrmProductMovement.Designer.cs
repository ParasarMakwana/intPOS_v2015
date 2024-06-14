namespace SFPOSWindows.MasterForm
{
    partial class FrmProductMovement
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.cmbSearch = new MetroFramework.Controls.MetroComboBox();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.previousYearBox = new System.Windows.Forms.CheckBox();
            this.previousWeekBox = new System.Windows.Forms.CheckBox();
            this.previousMonthBox = new System.Windows.Forms.CheckBox();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.PreviousDaysBox = new System.Windows.Forms.CheckBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.lblVendor = new MetroFramework.Controls.MetroLabel();
            this.lblItem = new MetroFramework.Controls.MetroLabel();
            this.lblTotalCost = new MetroFramework.Controls.MetroLabel();
            this.lblTotalRevenue = new MetroFramework.Controls.MetroLabel();
            this.lblGrossProfit = new MetroFramework.Controls.MetroLabel();
            this.lblSellPrice = new MetroFramework.Controls.MetroLabel();
            this.lblUnitCost = new MetroFramework.Controls.MetroLabel();
            this.lblUpcCode = new MetroFramework.Controls.MetroLabel();
            this.lblProductName = new MetroFramework.Controls.MetroLabel();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.endDate = new MetroFramework.Controls.MetroDateTime();
            this.startDate = new MetroFramework.Controls.MetroDateTime();
            this.txtSearch = new MetroFramework.Controls.MetroTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(10, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 47);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel2.Location = new System.Drawing.Point(15, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(969, 1);
            this.panel2.TabIndex = 1;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(8, 7);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(208, 32);
            this.lblSubHeading.TabIndex = 0;
            this.lblSubHeading.Text = "Product Movement";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbSearch
            // 
            this.cmbSearch.FormattingEnabled = true;
            this.cmbSearch.ItemHeight = 23;
            this.cmbSearch.Location = new System.Drawing.Point(827, 13);
            this.cmbSearch.MaxDropDownItems = 50;
            this.cmbSearch.Name = "cmbSearch";
            this.cmbSearch.Size = new System.Drawing.Size(155, 29);
            this.cmbSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbSearch.TabIndex = 1;
            this.cmbSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbSearch.UseSelectable = true;
            this.cmbSearch.Visible = false;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel1.Controls.Add(this.previousYearBox);
            this.metroPanel1.Controls.Add(this.previousWeekBox);
            this.metroPanel1.Controls.Add(this.previousMonthBox);
            this.metroPanel1.Controls.Add(this.metroLabel12);
            this.metroPanel1.Controls.Add(this.PreviousDaysBox);
            this.metroPanel1.Controls.Add(this.cmbSearch);
            this.metroPanel1.Controls.Add(this.groupBox11);
            this.metroPanel1.Controls.Add(this.metroLabel7);
            this.metroPanel1.Controls.Add(this.metroLabel6);
            this.metroPanel1.Controls.Add(this.btnSearch);
            this.metroPanel1.Controls.Add(this.endDate);
            this.metroPanel1.Controls.Add(this.startDate);
            this.metroPanel1.Controls.Add(this.txtSearch);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(1, 46);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1019, 205);
            this.metroPanel1.TabIndex = 15;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // previousYearBox
            // 
            this.previousYearBox.AutoSize = true;
            this.previousYearBox.Location = new System.Drawing.Point(688, 54);
            this.previousYearBox.Name = "previousYearBox";
            this.previousYearBox.Size = new System.Drawing.Size(128, 17);
            this.previousYearBox.TabIndex = 34;
            this.previousYearBox.Text = "PREVIOUS 12 YEAR";
            this.previousYearBox.UseVisualStyleBackColor = true;
            this.previousYearBox.CheckedChanged += new System.EventHandler(this.previousYearBox_CheckedChanged);
            // 
            // previousWeekBox
            // 
            this.previousWeekBox.AutoSize = true;
            this.previousWeekBox.Location = new System.Drawing.Point(406, 54);
            this.previousWeekBox.Name = "previousWeekBox";
            this.previousWeekBox.Size = new System.Drawing.Size(131, 17);
            this.previousWeekBox.TabIndex = 32;
            this.previousWeekBox.Text = "PREVIOUS 12 WEEK";
            this.previousWeekBox.UseVisualStyleBackColor = true;
            this.previousWeekBox.CheckedChanged += new System.EventHandler(this.previousWeekBox_CheckedChanged);
            // 
            // previousMonthBox
            // 
            this.previousMonthBox.AutoSize = true;
            this.previousMonthBox.Location = new System.Drawing.Point(543, 54);
            this.previousMonthBox.Name = "previousMonthBox";
            this.previousMonthBox.Size = new System.Drawing.Size(139, 17);
            this.previousMonthBox.TabIndex = 33;
            this.previousMonthBox.Text = "PREVIOUS 12 MONTH";
            this.previousMonthBox.UseVisualStyleBackColor = true;
            this.previousMonthBox.CheckedChanged += new System.EventHandler(this.previousMonthBox_CheckedChanged);
            // 
            // metroLabel12
            // 
            this.metroLabel12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel12.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel12.Location = new System.Drawing.Point(214, 13);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(76, 19);
            this.metroLabel12.TabIndex = 21;
            this.metroLabel12.Text = "UPCCODE:";
            this.metroLabel12.UseCustomBackColor = true;
            this.metroLabel12.UseCustomForeColor = true;
            // 
            // PreviousDaysBox
            // 
            this.PreviousDaysBox.AutoSize = true;
            this.PreviousDaysBox.Location = new System.Drawing.Point(272, 54);
            this.PreviousDaysBox.Name = "PreviousDaysBox";
            this.PreviousDaysBox.Size = new System.Drawing.Size(128, 17);
            this.PreviousDaysBox.TabIndex = 31;
            this.PreviousDaysBox.Text = "PREVIOUS 12 DAYS";
            this.PreviousDaysBox.UseVisualStyleBackColor = true;
            this.PreviousDaysBox.CheckedChanged += new System.EventHandler(this.PreviousDaysBox_CheckedChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.lblVendor);
            this.groupBox11.Controls.Add(this.lblItem);
            this.groupBox11.Controls.Add(this.lblTotalCost);
            this.groupBox11.Controls.Add(this.lblTotalRevenue);
            this.groupBox11.Controls.Add(this.lblGrossProfit);
            this.groupBox11.Controls.Add(this.lblSellPrice);
            this.groupBox11.Controls.Add(this.lblUnitCost);
            this.groupBox11.Controls.Add(this.lblUpcCode);
            this.groupBox11.Controls.Add(this.lblProductName);
            this.groupBox11.Controls.Add(this.metroLabel11);
            this.groupBox11.Controls.Add(this.metroLabel10);
            this.groupBox11.Controls.Add(this.metroLabel9);
            this.groupBox11.Controls.Add(this.metroLabel8);
            this.groupBox11.Controls.Add(this.metroLabel5);
            this.groupBox11.Controls.Add(this.metroLabel4);
            this.groupBox11.Controls.Add(this.metroLabel3);
            this.groupBox11.Controls.Add(this.metroLabel2);
            this.groupBox11.Controls.Add(this.metroLabel1);
            this.groupBox11.Font = new System.Drawing.Font("Segoe UI Semibold", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox11.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox11.Location = new System.Drawing.Point(10, 88);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(998, 91);
            this.groupBox11.TabIndex = 20;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "PRODUCT DETAILS";
            // 
            // lblVendor
            // 
            this.lblVendor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVendor.AutoSize = true;
            this.lblVendor.Location = new System.Drawing.Point(130, 47);
            this.lblVendor.MaximumSize = new System.Drawing.Size(261, 19);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(0, 0);
            this.lblVendor.TabIndex = 30;
            // 
            // lblItem
            // 
            this.lblItem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(516, 47);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(0, 0);
            this.lblItem.TabIndex = 29;
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Location = new System.Drawing.Point(130, 79);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(76, 19);
            this.lblTotalCost.TabIndex = 28;
            this.lblTotalCost.Text = "lblTotalCost";
            this.lblTotalCost.Visible = false;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Location = new System.Drawing.Point(516, 79);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(99, 19);
            this.lblTotalRevenue.TabIndex = 27;
            this.lblTotalRevenue.Text = "lblTotalRevenue";
            this.lblTotalRevenue.Visible = false;
            // 
            // lblGrossProfit
            // 
            this.lblGrossProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGrossProfit.AutoSize = true;
            this.lblGrossProfit.Location = new System.Drawing.Point(883, 83);
            this.lblGrossProfit.Name = "lblGrossProfit";
            this.lblGrossProfit.Size = new System.Drawing.Size(87, 19);
            this.lblGrossProfit.TabIndex = 26;
            this.lblGrossProfit.Text = "lblGrossProfit";
            this.lblGrossProfit.Visible = false;
            // 
            // lblSellPrice
            // 
            this.lblSellPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSellPrice.AutoSize = true;
            this.lblSellPrice.Location = new System.Drawing.Point(883, 51);
            this.lblSellPrice.Name = "lblSellPrice";
            this.lblSellPrice.Size = new System.Drawing.Size(0, 0);
            this.lblSellPrice.TabIndex = 25;
            // 
            // lblUnitCost
            // 
            this.lblUnitCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnitCost.AutoSize = true;
            this.lblUnitCost.Location = new System.Drawing.Point(883, 15);
            this.lblUnitCost.Name = "lblUnitCost";
            this.lblUnitCost.Size = new System.Drawing.Size(0, 0);
            this.lblUnitCost.TabIndex = 24;
            // 
            // lblUpcCode
            // 
            this.lblUpcCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUpcCode.AutoSize = true;
            this.lblUpcCode.Location = new System.Drawing.Point(516, 18);
            this.lblUpcCode.Name = "lblUpcCode";
            this.lblUpcCode.Size = new System.Drawing.Size(0, 0);
            this.lblUpcCode.TabIndex = 23;
            // 
            // lblProductName
            // 
            this.lblProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(130, 18);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(0, 0);
            this.lblProductName.TabIndex = 22;
            // 
            // metroLabel11
            // 
            this.metroLabel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.BackColor = System.Drawing.Color.White;
            this.metroLabel11.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel11.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel11.ForeColor = System.Drawing.Color.Black;
            this.metroLabel11.Location = new System.Drawing.Point(777, 83);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(108, 15);
            this.metroLabel11.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel11.TabIndex = 21;
            this.metroLabel11.Text = "GROSS PROFIT(%): ";
            this.metroLabel11.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel11.UseCustomBackColor = true;
            this.metroLabel11.UseCustomForeColor = true;
            this.metroLabel11.Visible = false;
            // 
            // metroLabel10
            // 
            this.metroLabel10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.BackColor = System.Drawing.Color.White;
            this.metroLabel10.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel10.ForeColor = System.Drawing.Color.Black;
            this.metroLabel10.Location = new System.Drawing.Point(413, 83);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(97, 15);
            this.metroLabel10.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel10.TabIndex = 20;
            this.metroLabel10.Text = "TOTAL REVENUE: ";
            this.metroLabel10.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel10.UseCustomBackColor = true;
            this.metroLabel10.UseCustomForeColor = true;
            this.metroLabel10.Visible = false;
            // 
            // metroLabel9
            // 
            this.metroLabel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.BackColor = System.Drawing.Color.White;
            this.metroLabel9.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel9.ForeColor = System.Drawing.Color.Black;
            this.metroLabel9.Location = new System.Drawing.Point(21, 83);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(77, 15);
            this.metroLabel9.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel9.TabIndex = 19;
            this.metroLabel9.Text = "TOTAL COST: ";
            this.metroLabel9.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel9.UseCustomBackColor = true;
            this.metroLabel9.UseCustomForeColor = true;
            this.metroLabel9.Visible = false;
            // 
            // metroLabel8
            // 
            this.metroLabel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.BackColor = System.Drawing.Color.White;
            this.metroLabel8.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel8.ForeColor = System.Drawing.Color.Black;
            this.metroLabel8.Location = new System.Drawing.Point(777, 51);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(60, 15);
            this.metroLabel8.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel8.TabIndex = 18;
            this.metroLabel8.Text = "Sell Price: ";
            this.metroLabel8.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel8.UseCustomBackColor = true;
            this.metroLabel8.UseCustomForeColor = true;
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.BackColor = System.Drawing.Color.White;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.ForeColor = System.Drawing.Color.Black;
            this.metroLabel5.Location = new System.Drawing.Point(777, 18);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(62, 15);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel5.TabIndex = 17;
            this.metroLabel5.Text = "Unit Cost: ";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel5.UseCustomBackColor = true;
            this.metroLabel5.UseCustomForeColor = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.White;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.ForeColor = System.Drawing.Color.Black;
            this.metroLabel4.Location = new System.Drawing.Point(413, 51);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(73, 15);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel4.TabIndex = 16;
            this.metroLabel4.Text = "ITEM CODE: ";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel4.UseCustomBackColor = true;
            this.metroLabel4.UseCustomForeColor = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.White;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.Color.Black;
            this.metroLabel3.Location = new System.Drawing.Point(413, 18);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(70, 15);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel3.TabIndex = 15;
            this.metroLabel3.Text = "UPC CODE: ";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseCustomForeColor = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.White;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(21, 51);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(96, 15);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel2.TabIndex = 14;
            this.metroLabel2.Text = "VENDOR NAME: ";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel2.UseCustomBackColor = true;
            this.metroLabel2.UseCustomForeColor = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.White;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(21, 18);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(103, 15);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.TabIndex = 13;
            this.metroLabel1.Text = "PRODUCT NAME: ";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel1.UseCustomBackColor = true;
            this.metroLabel1.UseCustomForeColor = true;
            // 
            // metroLabel7
            // 
            this.metroLabel7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel7.Location = new System.Drawing.Point(475, 54);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(75, 19);
            this.metroLabel7.TabIndex = 19;
            this.metroLabel7.Text = "END DATE:";
            this.metroLabel7.UseCustomBackColor = true;
            this.metroLabel7.UseCustomForeColor = true;
            this.metroLabel7.Visible = false;
            // 
            // metroLabel6
            // 
            this.metroLabel6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel6.Location = new System.Drawing.Point(178, 54);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(84, 19);
            this.metroLabel6.TabIndex = 18;
            this.metroLabel6.Text = "START DATE:";
            this.metroLabel6.UseCustomBackColor = true;
            this.metroLabel6.UseCustomForeColor = true;
            this.metroLabel6.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.Location = new System.Drawing.Point(579, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 29);
            this.btnSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // endDate
            // 
            this.endDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.endDate.Location = new System.Drawing.Point(561, 54);
            this.endDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(198, 29);
            this.endDate.TabIndex = 7;
            this.endDate.Visible = false;
            // 
            // startDate
            // 
            this.startDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startDate.Location = new System.Drawing.Point(272, 54);
            this.startDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(197, 29);
            this.startDate.TabIndex = 6;
            this.startDate.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.txtSearch.CustomButton.Image = null;
            this.txtSearch.CustomButton.Location = new System.Drawing.Point(249, 1);
            this.txtSearch.CustomButton.Name = "";
            this.txtSearch.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearch.CustomButton.TabIndex = 1;
            this.txtSearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearch.CustomButton.UseSelectable = true;
            this.txtSearch.CustomButton.Visible = false;
            this.txtSearch.DisplayIcon = true;
            this.txtSearch.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSearch.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearch.IconRight = true;
            this.txtSearch.Lines = new string[0];
            this.txtSearch.Location = new System.Drawing.Point(296, 13);
            this.txtSearch.MaxLength = 20;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PromptText = "UPC Code";
            this.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.ShortcutsEnabled = true;
            this.txtSearch.Size = new System.Drawing.Size(277, 29);
            this.txtSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearch.UseSelectable = true;
            this.txtSearch.WaterMark = "UPC Code";
            this.txtSearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Location = new System.Drawing.Point(0, 243);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1020, 533);
            this.panel3.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(11, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(999, 111);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PREVIOUS 12 DAYS MOVEMENTS";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox4.Location = new System.Drawing.Point(10, 366);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(999, 111);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PREVIOUS 12 YEAR MOVEMENTS";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox3.Location = new System.Drawing.Point(10, 249);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(999, 111);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PREVIOUS 12 MONTH MOVEMENTS";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox2.Location = new System.Drawing.Point(11, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(999, 111);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PREVIOUS 12 WEEK MOVEMENTS";
            // 
            // FrmProductMovement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 788);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmProductMovement";
            this.Text = "Product Movement";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton btnSearch;
        private MetroFramework.Controls.MetroTextBox txtSearch;
        private MetroFramework.Controls.MetroComboBox cmbSearch;
        private System.Windows.Forms.Panel panel3;
        private MetroFramework.Controls.MetroDateTime endDate;
        private MetroFramework.Controls.MetroDateTime startDate;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel lblProductName;
        private MetroFramework.Controls.MetroLabel lblUpcCode;
        private MetroFramework.Controls.MetroLabel lblTotalCost;
        private MetroFramework.Controls.MetroLabel lblTotalRevenue;
        private MetroFramework.Controls.MetroLabel lblGrossProfit;
        private MetroFramework.Controls.MetroLabel lblSellPrice;
        private MetroFramework.Controls.MetroLabel lblVendor;
        private MetroFramework.Controls.MetroLabel lblItem;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        private MetroFramework.Controls.MetroLabel lblUnitCost;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox previousYearBox;
        private System.Windows.Forms.CheckBox previousWeekBox;
        private System.Windows.Forms.CheckBox previousMonthBox;
        private System.Windows.Forms.CheckBox PreviousDaysBox;
    }
}