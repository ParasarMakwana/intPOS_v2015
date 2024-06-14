namespace SFPOSWindows.MasterForm
{
    partial class FrmPurchaseHeaderLine
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPurchaseHeaderLine));
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtOrderDate = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.txtVendorInvoice = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtVendorName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPost = new MetroFramework.Controls.MetroButton();
            this.txtTotalAmt = new MetroFramework.Controls.MetroTextBox();
            this.txtInvoiceNo = new MetroFramework.Controls.MetroTextBox();
            this.txtReceivedBy = new MetroFramework.Controls.MetroTextBox();
            this.txtAdjustment = new MetroFramework.Controls.MetroTextBox();
            this.txtShippedBy = new MetroFramework.Controls.MetroTextBox();
            this.datePickerDate = new MetroFramework.Controls.MetroDateTime();
            this.lblAdjustment = new System.Windows.Forms.Label();
            this.lblTotalAmt = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.lblShippedBy = new System.Windows.Forms.Label();
            this.lblUpcItem = new System.Windows.Forms.Label();
            this.txtSearchUPCCode = new System.Windows.Forms.TextBox();
            this.PurchaseLineGrdView = new MetroFramework.Controls.MetroGrid();
            this.PurchaseLineID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseHeaderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitMeasureID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxGroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPCCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineAmtExclTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineAmtInclTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseLineGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.txtOrderDate);
            this.pnlSearch.Controls.Add(this.metroLabel3);
            this.pnlSearch.Controls.Add(this.txtVendorInvoice);
            this.pnlSearch.Controls.Add(this.metroLabel2);
            this.pnlSearch.Controls.Add(this.txtVendorName);
            this.pnlSearch.Controls.Add(this.metroLabel1);
            this.pnlSearch.Location = new System.Drawing.Point(8, 73);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(836, 38);
            this.pnlSearch.TabIndex = 20;
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.txtOrderDate.CustomButton.Image = null;
            this.txtOrderDate.CustomButton.Location = new System.Drawing.Point(93, 1);
            this.txtOrderDate.CustomButton.Name = "";
            this.txtOrderDate.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtOrderDate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtOrderDate.CustomButton.TabIndex = 1;
            this.txtOrderDate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOrderDate.CustomButton.UseSelectable = true;
            this.txtOrderDate.CustomButton.Visible = false;
            this.txtOrderDate.Enabled = false;
            this.txtOrderDate.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtOrderDate.Lines = new string[0];
            this.txtOrderDate.Location = new System.Drawing.Point(569, 5);
            this.txtOrderDate.MaxLength = 32767;
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.PasswordChar = '\0';
            this.txtOrderDate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOrderDate.SelectedText = "";
            this.txtOrderDate.SelectionLength = 0;
            this.txtOrderDate.SelectionStart = 0;
            this.txtOrderDate.ShortcutsEnabled = true;
            this.txtOrderDate.Size = new System.Drawing.Size(121, 29);
            this.txtOrderDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtOrderDate.TabIndex = 14;
            this.txtOrderDate.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOrderDate.UseSelectable = true;
            this.txtOrderDate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtOrderDate.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(488, 8);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(76, 19);
            this.metroLabel3.TabIndex = 13;
            this.metroLabel3.Text = "Order Date";
            // 
            // txtVendorInvoice
            // 
            this.txtVendorInvoice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.txtVendorInvoice.CustomButton.Image = null;
            this.txtVendorInvoice.CustomButton.Location = new System.Drawing.Point(93, 1);
            this.txtVendorInvoice.CustomButton.Name = "";
            this.txtVendorInvoice.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtVendorInvoice.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVendorInvoice.CustomButton.TabIndex = 1;
            this.txtVendorInvoice.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVendorInvoice.CustomButton.UseSelectable = true;
            this.txtVendorInvoice.CustomButton.Visible = false;
            this.txtVendorInvoice.Enabled = false;
            this.txtVendorInvoice.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtVendorInvoice.Lines = new string[0];
            this.txtVendorInvoice.Location = new System.Drawing.Point(331, 5);
            this.txtVendorInvoice.MaxLength = 32767;
            this.txtVendorInvoice.Name = "txtVendorInvoice";
            this.txtVendorInvoice.PasswordChar = '\0';
            this.txtVendorInvoice.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtVendorInvoice.SelectedText = "";
            this.txtVendorInvoice.SelectionLength = 0;
            this.txtVendorInvoice.SelectionStart = 0;
            this.txtVendorInvoice.ShortcutsEnabled = true;
            this.txtVendorInvoice.Size = new System.Drawing.Size(121, 29);
            this.txtVendorInvoice.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVendorInvoice.TabIndex = 12;
            this.txtVendorInvoice.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVendorInvoice.UseSelectable = true;
            this.txtVendorInvoice.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtVendorInvoice.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(248, 8);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(79, 19);
            this.metroLabel2.TabIndex = 11;
            this.metroLabel2.Text = "P.O.Number";
            // 
            // txtVendorName
            // 
            this.txtVendorName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.txtVendorName.CustomButton.Image = null;
            this.txtVendorName.CustomButton.Location = new System.Drawing.Point(93, 1);
            this.txtVendorName.CustomButton.Name = "";
            this.txtVendorName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtVendorName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVendorName.CustomButton.TabIndex = 1;
            this.txtVendorName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVendorName.CustomButton.UseSelectable = true;
            this.txtVendorName.CustomButton.Visible = false;
            this.txtVendorName.Enabled = false;
            this.txtVendorName.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtVendorName.Lines = new string[0];
            this.txtVendorName.Location = new System.Drawing.Point(110, 5);
            this.txtVendorName.MaxLength = 32767;
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.PasswordChar = '\0';
            this.txtVendorName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtVendorName.SelectedText = "";
            this.txtVendorName.SelectionLength = 0;
            this.txtVendorName.SelectionStart = 0;
            this.txtVendorName.ShortcutsEnabled = true;
            this.txtVendorName.Size = new System.Drawing.Size(121, 29);
            this.txtVendorName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVendorName.TabIndex = 10;
            this.txtVendorName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVendorName.UseSelectable = true;
            this.txtVendorName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtVendorName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(3, 8);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(91, 19);
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "Vendor Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnPost);
            this.groupBox1.Controls.Add(this.txtTotalAmt);
            this.groupBox1.Controls.Add(this.txtInvoiceNo);
            this.groupBox1.Controls.Add(this.txtReceivedBy);
            this.groupBox1.Controls.Add(this.txtAdjustment);
            this.groupBox1.Controls.Add(this.txtShippedBy);
            this.groupBox1.Controls.Add(this.datePickerDate);
            this.groupBox1.Controls.Add(this.lblAdjustment);
            this.groupBox1.Controls.Add(this.lblTotalAmt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblInvoiceNo);
            this.groupBox1.Controls.Add(this.lblShippedBy);
            this.groupBox1.Controls.Add(this.lblUpcItem);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 524);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(842, 127);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Invoice Information";
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(641, 90);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(197, 31);
            this.btnPost.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnPost.TabIndex = 36;
            this.btnPost.Text = "GENERATE INVOICE";
            this.btnPost.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnPost.UseSelectable = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // txtTotalAmt
            // 
            // 
            // 
            // 
            this.txtTotalAmt.CustomButton.Image = null;
            this.txtTotalAmt.CustomButton.Location = new System.Drawing.Point(132, 1);
            this.txtTotalAmt.CustomButton.Name = "";
            this.txtTotalAmt.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtTotalAmt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTotalAmt.CustomButton.TabIndex = 1;
            this.txtTotalAmt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTotalAmt.CustomButton.UseSelectable = true;
            this.txtTotalAmt.CustomButton.Visible = false;
            this.txtTotalAmt.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtTotalAmt.Lines = new string[0];
            this.txtTotalAmt.Location = new System.Drawing.Point(679, 52);
            this.txtTotalAmt.MaxLength = 7;
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.PasswordChar = '\0';
            this.txtTotalAmt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTotalAmt.SelectedText = "";
            this.txtTotalAmt.SelectionLength = 0;
            this.txtTotalAmt.SelectionStart = 0;
            this.txtTotalAmt.ShortcutsEnabled = true;
            this.txtTotalAmt.Size = new System.Drawing.Size(160, 29);
            this.txtTotalAmt.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTotalAmt.TabIndex = 7;
            this.txtTotalAmt.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTotalAmt.UseSelectable = true;
            this.txtTotalAmt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtTotalAmt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtTotalAmt.TextChanged += new System.EventHandler(this.txtTotalAmt_TextChanged);
            // 
            // txtInvoiceNo
            // 
            // 
            // 
            // 
            this.txtInvoiceNo.CustomButton.Image = null;
            this.txtInvoiceNo.CustomButton.Location = new System.Drawing.Point(134, 1);
            this.txtInvoiceNo.CustomButton.Name = "";
            this.txtInvoiceNo.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtInvoiceNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtInvoiceNo.CustomButton.TabIndex = 1;
            this.txtInvoiceNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtInvoiceNo.CustomButton.UseSelectable = true;
            this.txtInvoiceNo.CustomButton.Visible = false;
            this.txtInvoiceNo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtInvoiceNo.Lines = new string[0];
            this.txtInvoiceNo.Location = new System.Drawing.Point(110, 53);
            this.txtInvoiceNo.MaxLength = 6;
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.PasswordChar = '\0';
            this.txtInvoiceNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtInvoiceNo.SelectedText = "";
            this.txtInvoiceNo.SelectionLength = 0;
            this.txtInvoiceNo.SelectionStart = 0;
            this.txtInvoiceNo.ShortcutsEnabled = true;
            this.txtInvoiceNo.Size = new System.Drawing.Size(162, 29);
            this.txtInvoiceNo.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtInvoiceNo.TabIndex = 5;
            this.txtInvoiceNo.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtInvoiceNo.UseSelectable = true;
            this.txtInvoiceNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtInvoiceNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtInvoiceNo.TextChanged += new System.EventHandler(this.txtInvoiceNo_TextChanged);
            // 
            // txtReceivedBy
            // 
            // 
            // 
            // 
            this.txtReceivedBy.CustomButton.Image = null;
            this.txtReceivedBy.CustomButton.Location = new System.Drawing.Point(132, 1);
            this.txtReceivedBy.CustomButton.Name = "";
            this.txtReceivedBy.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtReceivedBy.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtReceivedBy.CustomButton.TabIndex = 1;
            this.txtReceivedBy.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtReceivedBy.CustomButton.UseSelectable = true;
            this.txtReceivedBy.CustomButton.Visible = false;
            this.txtReceivedBy.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtReceivedBy.Lines = new string[0];
            this.txtReceivedBy.Location = new System.Drawing.Point(388, 55);
            this.txtReceivedBy.MaxLength = 20;
            this.txtReceivedBy.Name = "txtReceivedBy";
            this.txtReceivedBy.PasswordChar = '\0';
            this.txtReceivedBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtReceivedBy.SelectedText = "";
            this.txtReceivedBy.SelectionLength = 0;
            this.txtReceivedBy.SelectionStart = 0;
            this.txtReceivedBy.ShortcutsEnabled = true;
            this.txtReceivedBy.Size = new System.Drawing.Size(160, 29);
            this.txtReceivedBy.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtReceivedBy.TabIndex = 6;
            this.txtReceivedBy.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtReceivedBy.UseSelectable = true;
            this.txtReceivedBy.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtReceivedBy.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtReceivedBy.TextChanged += new System.EventHandler(this.txtReceivedBy_TextChanged);
            // 
            // txtAdjustment
            // 
            // 
            // 
            // 
            this.txtAdjustment.CustomButton.Image = null;
            this.txtAdjustment.CustomButton.Location = new System.Drawing.Point(132, 1);
            this.txtAdjustment.CustomButton.Name = "";
            this.txtAdjustment.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtAdjustment.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtAdjustment.CustomButton.TabIndex = 1;
            this.txtAdjustment.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtAdjustment.CustomButton.UseSelectable = true;
            this.txtAdjustment.CustomButton.Visible = false;
            this.txtAdjustment.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtAdjustment.Lines = new string[0];
            this.txtAdjustment.Location = new System.Drawing.Point(678, 18);
            this.txtAdjustment.MaxLength = 7;
            this.txtAdjustment.Name = "txtAdjustment";
            this.txtAdjustment.PasswordChar = '\0';
            this.txtAdjustment.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAdjustment.SelectedText = "";
            this.txtAdjustment.SelectionLength = 0;
            this.txtAdjustment.SelectionStart = 0;
            this.txtAdjustment.ShortcutsEnabled = true;
            this.txtAdjustment.Size = new System.Drawing.Size(160, 29);
            this.txtAdjustment.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtAdjustment.TabIndex = 4;
            this.txtAdjustment.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtAdjustment.UseSelectable = true;
            this.txtAdjustment.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtAdjustment.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtAdjustment.TextChanged += new System.EventHandler(this.txtAdjustment_TextChanged);
            // 
            // txtShippedBy
            // 
            // 
            // 
            // 
            this.txtShippedBy.CustomButton.Image = null;
            this.txtShippedBy.CustomButton.Location = new System.Drawing.Point(132, 1);
            this.txtShippedBy.CustomButton.Name = "";
            this.txtShippedBy.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtShippedBy.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtShippedBy.CustomButton.TabIndex = 1;
            this.txtShippedBy.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtShippedBy.CustomButton.UseSelectable = true;
            this.txtShippedBy.CustomButton.Visible = false;
            this.txtShippedBy.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtShippedBy.Lines = new string[0];
            this.txtShippedBy.Location = new System.Drawing.Point(388, 18);
            this.txtShippedBy.MaxLength = 20;
            this.txtShippedBy.Name = "txtShippedBy";
            this.txtShippedBy.PasswordChar = '\0';
            this.txtShippedBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtShippedBy.SelectedText = "";
            this.txtShippedBy.SelectionLength = 0;
            this.txtShippedBy.SelectionStart = 0;
            this.txtShippedBy.ShortcutsEnabled = true;
            this.txtShippedBy.Size = new System.Drawing.Size(160, 29);
            this.txtShippedBy.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtShippedBy.TabIndex = 3;
            this.txtShippedBy.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtShippedBy.UseSelectable = true;
            this.txtShippedBy.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtShippedBy.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtShippedBy.TextChanged += new System.EventHandler(this.txtShippedBy_TextChanged);
            // 
            // datePickerDate
            // 
            this.datePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerDate.Location = new System.Drawing.Point(110, 18);
            this.datePickerDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.datePickerDate.Name = "datePickerDate";
            this.datePickerDate.Size = new System.Drawing.Size(162, 29);
            this.datePickerDate.Style = MetroFramework.MetroColorStyle.Blue;
            this.datePickerDate.TabIndex = 1;
            this.datePickerDate.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblAdjustment
            // 
            this.lblAdjustment.AutoSize = true;
            this.lblAdjustment.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustment.ForeColor = System.Drawing.Color.Black;
            this.lblAdjustment.Location = new System.Drawing.Point(566, 15);
            this.lblAdjustment.Name = "lblAdjustment";
            this.lblAdjustment.Size = new System.Drawing.Size(80, 17);
            this.lblAdjustment.TabIndex = 35;
            this.lblAdjustment.Text = "Adjustment: ";
            // 
            // lblTotalAmt
            // 
            this.lblTotalAmt.AutoSize = true;
            this.lblTotalAmt.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmt.ForeColor = System.Drawing.Color.Black;
            this.lblTotalAmt.Location = new System.Drawing.Point(567, 52);
            this.lblTotalAmt.Name = "lblTotalAmt";
            this.lblTotalAmt.Size = new System.Drawing.Size(93, 17);
            this.lblTotalAmt.TabIndex = 30;
            this.lblTotalAmt.Text = "Total Amount: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(278, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 29;
            this.label1.Text = "Received By: ";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceNo.ForeColor = System.Drawing.Color.Black;
            this.lblInvoiceNo.Location = new System.Drawing.Point(6, 50);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(77, 17);
            this.lblInvoiceNo.TabIndex = 28;
            this.lblInvoiceNo.Text = "Invoice No: ";
            // 
            // lblShippedBy
            // 
            this.lblShippedBy.AutoSize = true;
            this.lblShippedBy.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShippedBy.ForeColor = System.Drawing.Color.Black;
            this.lblShippedBy.Location = new System.Drawing.Point(278, 18);
            this.lblShippedBy.Name = "lblShippedBy";
            this.lblShippedBy.Size = new System.Drawing.Size(80, 17);
            this.lblShippedBy.TabIndex = 27;
            this.lblShippedBy.Text = "Shipped By: ";
            // 
            // lblUpcItem
            // 
            this.lblUpcItem.AutoSize = true;
            this.lblUpcItem.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpcItem.ForeColor = System.Drawing.Color.Black;
            this.lblUpcItem.Location = new System.Drawing.Point(10, 18);
            this.lblUpcItem.Name = "lblUpcItem";
            this.lblUpcItem.Size = new System.Drawing.Size(42, 17);
            this.lblUpcItem.TabIndex = 26;
            this.lblUpcItem.Text = "Date: ";
            // 
            // txtSearchUPCCode
            // 
            this.txtSearchUPCCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchUPCCode.Font = new System.Drawing.Font("Segoe UI Symbol", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchUPCCode.Location = new System.Drawing.Point(8, 117);
            this.txtSearchUPCCode.Name = "txtSearchUPCCode";
            this.txtSearchUPCCode.Size = new System.Drawing.Size(835, 30);
            this.txtSearchUPCCode.TabIndex = 0;
            this.txtSearchUPCCode.TextChanged += new System.EventHandler(this.txtSearchUPCCode_TextChanged);
            // 
            // PurchaseLineGrdView
            // 
            this.PurchaseLineGrdView.AllowUserToAddRows = false;
            this.PurchaseLineGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.PurchaseLineGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PurchaseLineGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PurchaseLineGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PurchaseLineGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.PurchaseLineGrdView.BackgroundColor = System.Drawing.Color.White;
            this.PurchaseLineGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PurchaseLineGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.PurchaseLineGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PurchaseLineGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PurchaseLineGrdView.ColumnHeadersHeight = 30;
            this.PurchaseLineGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.PurchaseLineGrdView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PurchaseLineID,
            this.PurchaseHeaderID,
            this.ProductID,
            this.UnitMeasureID,
            this.DepartmentID,
            this.SectionID,
            this.TaxGroupID,
            this.UPCCode,
            this.ItemCode,
            this.ProductName,
            this.Quantity,
            this.UnitCost,
            this.Tax,
            this.TaxAmount,
            this.LineAmtExclTax,
            this.LineAmtInclTax});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PurchaseLineGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.PurchaseLineGrdView.EnableHeadersVisualStyles = false;
            this.PurchaseLineGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PurchaseLineGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.PurchaseLineGrdView.Location = new System.Drawing.Point(8, 153);
            this.PurchaseLineGrdView.Name = "PurchaseLineGrdView";
            this.PurchaseLineGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PurchaseLineGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.PurchaseLineGrdView.RowHeadersVisible = false;
            this.PurchaseLineGrdView.RowHeadersWidth = 30;
            this.PurchaseLineGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PurchaseLineGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PurchaseLineGrdView.Size = new System.Drawing.Size(835, 365);
            this.PurchaseLineGrdView.TabIndex = 28;
            this.PurchaseLineGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PurchaseLineGrdView_CellClick);
            this.PurchaseLineGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PurchaseLineGrdView_CellContentClick);
            this.PurchaseLineGrdView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.PurchaseLineGrdView_CellValueChanged);
            this.PurchaseLineGrdView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.PurchaseLineGrdView_DataError);
            // 
            // PurchaseLineID
            // 
            this.PurchaseLineID.DataPropertyName = "PurchaseLineID";
            this.PurchaseLineID.HeaderText = "PurchaseLineID";
            this.PurchaseLineID.Name = "PurchaseLineID";
            this.PurchaseLineID.Visible = false;
            // 
            // PurchaseHeaderID
            // 
            this.PurchaseHeaderID.DataPropertyName = "PurchaseHeaderID";
            this.PurchaseHeaderID.HeaderText = "PurchaseHeaderID";
            this.PurchaseHeaderID.Name = "PurchaseHeaderID";
            this.PurchaseHeaderID.ReadOnly = true;
            this.PurchaseHeaderID.Visible = false;
            // 
            // ProductID
            // 
            this.ProductID.DataPropertyName = "ProductID";
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.Name = "ProductID";
            this.ProductID.ReadOnly = true;
            this.ProductID.Visible = false;
            // 
            // UnitMeasureID
            // 
            this.UnitMeasureID.DataPropertyName = "UnitMeasureID";
            this.UnitMeasureID.HeaderText = "UnitMeasureID";
            this.UnitMeasureID.Name = "UnitMeasureID";
            this.UnitMeasureID.ReadOnly = true;
            this.UnitMeasureID.Visible = false;
            // 
            // DepartmentID
            // 
            this.DepartmentID.DataPropertyName = "DepartmentID";
            this.DepartmentID.HeaderText = "DepartmentID";
            this.DepartmentID.Name = "DepartmentID";
            this.DepartmentID.ReadOnly = true;
            this.DepartmentID.Visible = false;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            this.SectionID.HeaderText = "SectionID";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            this.SectionID.Visible = false;
            // 
            // TaxGroupID
            // 
            this.TaxGroupID.DataPropertyName = "TaxGroupID";
            this.TaxGroupID.HeaderText = "TaxGroupID";
            this.TaxGroupID.Name = "TaxGroupID";
            this.TaxGroupID.ReadOnly = true;
            this.TaxGroupID.Visible = false;
            // 
            // UPCCode
            // 
            this.UPCCode.DataPropertyName = "UPCCode";
            this.UPCCode.HeaderText = "UPCCode";
            this.UPCCode.Name = "UPCCode";
            this.UPCCode.ReadOnly = true;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "ProductName";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MaxInputLength = 5;
            this.Quantity.Name = "Quantity";
            // 
            // UnitCost
            // 
            this.UnitCost.DataPropertyName = "UnitCost";
            this.UnitCost.HeaderText = "UnitCost";
            this.UnitCost.MaxInputLength = 10;
            this.UnitCost.Name = "UnitCost";
            // 
            // Tax
            // 
            this.Tax.DataPropertyName = "Tax";
            this.Tax.HeaderText = "Tax";
            this.Tax.MaxInputLength = 3;
            this.Tax.Name = "Tax";
            // 
            // TaxAmount
            // 
            this.TaxAmount.DataPropertyName = "TaxAmount";
            this.TaxAmount.HeaderText = "TaxAmount";
            this.TaxAmount.Name = "TaxAmount";
            this.TaxAmount.ReadOnly = true;
            // 
            // LineAmtExclTax
            // 
            this.LineAmtExclTax.DataPropertyName = "LineAmtExclTax";
            this.LineAmtExclTax.HeaderText = "LineAmtExclTax";
            this.LineAmtExclTax.Name = "LineAmtExclTax";
            this.LineAmtExclTax.ReadOnly = true;
            // 
            // LineAmtInclTax
            // 
            this.LineAmtInclTax.DataPropertyName = "LineAmtInclTax";
            this.LineAmtInclTax.HeaderText = "LineAmtInclTax";
            this.LineAmtInclTax.Name = "LineAmtInclTax";
            this.LineAmtInclTax.ReadOnly = true;
            // 
            // FrmPurchaseHeaderLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 650);
            this.Controls.Add(this.txtSearchUPCCode);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.PurchaseLineGrdView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(850, 650);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(850, 650);
            this.Name = "FrmPurchaseHeaderLine";
            this.Text = "Product Information";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPurchaseHeaderLine_FormClosing);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseLineGrdView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblUpcItem;
        private System.Windows.Forms.Label lblShippedBy;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalAmt;
        private System.Windows.Forms.Label lblAdjustment;
        public System.Windows.Forms.TextBox txtSearchUPCCode;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        public MetroFramework.Controls.MetroTextBox txtVendorName;
        public MetroFramework.Controls.MetroTextBox txtVendorInvoice;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        public MetroFramework.Controls.MetroTextBox txtOrderDate;
        private MetroFramework.Controls.MetroDateTime datePickerDate;
        private MetroFramework.Controls.MetroTextBox txtShippedBy;
        private MetroFramework.Controls.MetroTextBox txtAdjustment;
        private MetroFramework.Controls.MetroTextBox txtTotalAmt;
        private MetroFramework.Controls.MetroTextBox txtInvoiceNo;
        private MetroFramework.Controls.MetroTextBox txtReceivedBy;
        public MetroFramework.Controls.MetroButton btnPost;
        public MetroFramework.Controls.MetroGrid PurchaseLineGrdView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseLineID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseHeaderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitMeasureID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxGroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPCCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tax;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineAmtExclTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineAmtInclTax;
    }
}