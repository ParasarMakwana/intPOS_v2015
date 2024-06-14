namespace SFPOSWindows.Frontend
{
    partial class frmOrderScanner
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderScanner));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel9 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.lblFinalAmount = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblTaxAmount = new System.Windows.Forms.Label();
            this.backgroundWorker_DataSync = new System.ComponentModel.BackgroundWorker();
            this.SyncTimer = new System.Windows.Forms.Timer(this.components);
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.PicNoInternet = new System.Windows.Forms.PictureBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.btnShortCut = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.PicNoInternetLine = new System.Windows.Forms.PictureBox();
            this.dataGridProducts = new MetroFramework.Controls.MetroGrid();
            this.UPCCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkedUPCCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParentUPCCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsForceTax = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsRefund = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsVoid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxGroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitMeasureID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitMeasureCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FoodStampTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsScale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsFoodStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountApplyed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Abb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.CasePriceApplied = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GroupQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaseQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CasePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearchUPCCode = new MetroFramework.Controls.MetroTextBox();
            this.lblFSTotal = new System.Windows.Forms.Label();
            this.lblFoodStamp = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.SyncTimer_LocalToLive = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker_LocalToLive = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicNoInternet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicNoInternetLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProducts)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(43, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Weight";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(462, 388);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Tax Amount";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(15, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 33);
            this.label8.TabIndex = 11;
            this.label8.Text = "Total";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Location = new System.Drawing.Point(-1, 361);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1045, 5);
            this.panel9.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(156, 388);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 18);
            this.label11.TabIndex = 18;
            this.label11.Text = "Item Count";
            // 
            // lblFinalAmount
            // 
            this.lblFinalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinalAmount.AutoSize = true;
            this.lblFinalAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblFinalAmount.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalAmount.ForeColor = System.Drawing.Color.White;
            this.lblFinalAmount.Location = new System.Drawing.Point(122, 13);
            this.lblFinalAmount.MaximumSize = new System.Drawing.Size(200, 41);
            this.lblFinalAmount.MinimumSize = new System.Drawing.Size(200, 41);
            this.lblFinalAmount.Name = "lblFinalAmount";
            this.lblFinalAmount.Size = new System.Drawing.Size(200, 41);
            this.lblFinalAmount.TabIndex = 16;
            this.lblFinalAmount.Text = "$0.00";
            this.lblFinalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.Black;
            this.lblTotalCount.Location = new System.Drawing.Point(193, 420);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(26, 27);
            this.lblTotalCount.TabIndex = 15;
            this.lblTotalCount.Text = "0";
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(331, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Sub Total";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.ForeColor = System.Drawing.Color.Black;
            this.lblSubTotal.Location = new System.Drawing.Point(313, 418);
            this.lblSubTotal.MaximumSize = new System.Drawing.Size(170, 30);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(139, 30);
            this.lblSubTotal.TabIndex = 16;
            this.lblSubTotal.Text = "$0.00";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTaxAmount
            // 
            this.lblTaxAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaxAmount.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxAmount.ForeColor = System.Drawing.Color.Black;
            this.lblTaxAmount.Location = new System.Drawing.Point(460, 418);
            this.lblTaxAmount.MaximumSize = new System.Drawing.Size(170, 30);
            this.lblTaxAmount.MinimumSize = new System.Drawing.Size(0, 30);
            this.lblTaxAmount.Name = "lblTaxAmount";
            this.lblTaxAmount.Size = new System.Drawing.Size(121, 30);
            this.lblTaxAmount.TabIndex = 18;
            this.lblTaxAmount.Text = "$0.00";
            this.lblTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backgroundWorker_DataSync
            // 
            this.backgroundWorker_DataSync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DataSync_DoWork);
            this.backgroundWorker_DataSync.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_DataSync_RunWorkerCompleted);
            // 
            // SyncTimer
            // 
            this.SyncTimer.Tick += new System.EventHandler(this.SyncTimer_Tick);
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.lblDate);
            this.panel8.Controls.Add(this.lblTime);
            this.panel8.Controls.Add(this.btnLogOut);
            this.panel8.Controls.Add(this.PicNoInternet);
            this.panel8.Controls.Add(this.btnSync);
            this.panel8.Controls.Add(this.pictureBox5);
            this.panel8.Controls.Add(this.lblStoreName);
            this.panel8.Controls.Add(this.btnShortCut);
            this.panel8.Controls.Add(this.pictureBox1);
            this.panel8.Controls.Add(this.button2);
            this.panel8.Controls.Add(this.lblVersion);
            this.panel8.Controls.Add(this.pictureBox4);
            this.panel8.Controls.Add(this.lblLoginName);
            this.panel8.Controls.Add(this.pictureBox3);
            this.panel8.Controls.Add(this.pictureBox2);
            this.panel8.Controls.Add(this.PicNoInternetLine);
            this.panel8.Location = new System.Drawing.Point(-1, -1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1045, 47);
            this.panel8.TabIndex = 21;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(666, 15);
            this.lblDate.MaximumSize = new System.Drawing.Size(300, 30);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(108, 18);
            this.lblDate.TabIndex = 15;
            this.lblDate.Text = "11/27/2018";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(780, 14);
            this.lblTime.MaximumSize = new System.Drawing.Size(300, 30);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(118, 18);
            this.lblTime.TabIndex = 12;
            this.lblTime.Text = "00:00:00 PM";
            // 
            // btnLogOut
            // 
            this.btnLogOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogOut.BackgroundImage = global::SFPOSWindows.Properties.Resources.exit2;
            this.btnLogOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnLogOut.FlatAppearance.BorderSize = 0;
            this.btnLogOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Location = new System.Drawing.Point(954, 3);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(40, 39);
            this.btnLogOut.TabIndex = 12;
            this.toolTip.SetToolTip(this.btnLogOut, "Sign Out");
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // PicNoInternet
            // 
            this.PicNoInternet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PicNoInternet.BackColor = System.Drawing.Color.Transparent;
            this.PicNoInternet.BackgroundImage = global::SFPOSWindows.Properties.Resources.offline;
            this.PicNoInternet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PicNoInternet.Location = new System.Drawing.Point(208, 7);
            this.PicNoInternet.Name = "PicNoInternet";
            this.PicNoInternet.Size = new System.Drawing.Size(33, 33);
            this.PicNoInternet.TabIndex = 25;
            this.PicNoInternet.TabStop = false;
            this.toolTip.SetToolTip(this.PicNoInternet, "Offline Mode");
            // 
            // btnSync
            // 
            this.btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSync.BackgroundImage = global::SFPOSWindows.Properties.Resources.sync;
            this.btnSync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSync.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSync.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnSync.FlatAppearance.BorderSize = 0;
            this.btnSync.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSync.Location = new System.Drawing.Point(904, 0);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(44, 46);
            this.btnSync.TabIndex = 23;
            this.toolTip.SetToolTip(this.btnSync, "Sync");
            this.btnSync.UseVisualStyleBackColor = false;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.BackgroundImage = global::SFPOSWindows.Properties.Resources.line;
            this.pictureBox5.Location = new System.Drawing.Point(933, 7);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(33, 33);
            this.pictureBox5.TabIndex = 22;
            this.pictureBox5.TabStop = false;
            // 
            // lblStoreName
            // 
            this.lblStoreName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStoreName.AutoSize = true;
            this.lblStoreName.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreName.ForeColor = System.Drawing.Color.White;
            this.lblStoreName.Location = new System.Drawing.Point(57, 13);
            this.lblStoreName.MaximumSize = new System.Drawing.Size(300, 30);
            this.lblStoreName.MinimumSize = new System.Drawing.Size(150, 5);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(150, 18);
            this.lblStoreName.TabIndex = 21;
            this.lblStoreName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnShortCut
            // 
            this.btnShortCut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShortCut.BackgroundImage = global::SFPOSWindows.Properties.Resources.information;
            this.btnShortCut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShortCut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShortCut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnShortCut.FlatAppearance.BorderSize = 0;
            this.btnShortCut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnShortCut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnShortCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShortCut.Location = new System.Drawing.Point(1000, 2);
            this.btnShortCut.Name = "btnShortCut";
            this.btnShortCut.Size = new System.Drawing.Size(44, 41);
            this.btnShortCut.TabIndex = 20;
            this.toolTip.SetToolTip(this.btnShortCut, "Short cut");
            this.btnShortCut.UseVisualStyleBackColor = false;
            this.btnShortCut.Click += new System.EventHandler(this.btnShortCut_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::SFPOSWindows.Properties.Resources.line;
            this.pictureBox1.Location = new System.Drawing.Point(980, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 33);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackgroundImage = global::SFPOSWindows.Properties.Resources.White_Icon___Copy;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(7, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 43);
            this.button2.TabIndex = 18;
            this.toolTip.SetToolTip(this.button2, "ezPOSPro");
            this.button2.UseVisualStyleBackColor = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(256, 15);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(58, 18);
            this.lblVersion.TabIndex = 17;
            this.lblVersion.Text = "1.5.1";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::SFPOSWindows.Properties.Resources.line;
            this.pictureBox4.Location = new System.Drawing.Point(301, 7);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(33, 33);
            this.pictureBox4.TabIndex = 16;
            this.pictureBox4.TabStop = false;
            // 
            // lblLoginName
            // 
            this.lblLoginName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoginName.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginName.ForeColor = System.Drawing.Color.White;
            this.lblLoginName.Location = new System.Drawing.Point(331, 14);
            this.lblLoginName.MaximumSize = new System.Drawing.Size(500, 30);
            this.lblLoginName.MinimumSize = new System.Drawing.Size(200, 17);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(318, 21);
            this.lblLoginName.TabIndex = 0;
            this.lblLoginName.Text = "TECHCRONUS";
            this.lblLoginName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::SFPOSWindows.Properties.Resources.line;
            this.pictureBox3.Location = new System.Drawing.Point(645, 7);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(33, 33);
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::SFPOSWindows.Properties.Resources.line;
            this.pictureBox2.Location = new System.Drawing.Point(885, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 33);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // PicNoInternetLine
            // 
            this.PicNoInternetLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PicNoInternetLine.BackColor = System.Drawing.Color.Transparent;
            this.PicNoInternetLine.BackgroundImage = global::SFPOSWindows.Properties.Resources.line;
            this.PicNoInternetLine.Location = new System.Drawing.Point(230, 7);
            this.PicNoInternetLine.Name = "PicNoInternetLine";
            this.PicNoInternetLine.Size = new System.Drawing.Size(33, 33);
            this.PicNoInternetLine.TabIndex = 24;
            this.PicNoInternetLine.TabStop = false;
            // 
            // dataGridProducts
            // 
            this.dataGridProducts.AllowUserToAddRows = false;
            this.dataGridProducts.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.dataGridProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridProducts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridProducts.BackgroundColor = System.Drawing.Color.White;
            this.dataGridProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridProducts.ColumnHeadersHeight = 45;
            this.dataGridProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UPCCode,
            this.LinkedUPCCode,
            this.ParentUPCCode,
            this.IsForceTax,
            this.IsRefund,
            this.IsVoid,
            this.ProductID,
            this.DepartmentID,
            this.SectionID,
            this.TaxGroupID,
            this.UnitMeasureID,
            this.ProductName,
            this.Qty,
            this.UnitMeasureCode,
            this.SellPrice,
            this.SalePrice,
            this.Tax,
            this.TaxAmount,
            this.Discount,
            this.DiscountAmount,
            this.FoodStampTotal,
            this.FinalPrice,
            this.IsScale,
            this.IsFoodStamp,
            this.IsTax,
            this.DiscountApplyed,
            this.Abb,
            this.Image,
            this.CasePriceApplied,
            this.GroupQty,
            this.GroupPrice,
            this.CaseQty,
            this.CasePrice});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridProducts.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridProducts.EnableHeadersVisualStyles = false;
            this.dataGridProducts.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataGridProducts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.dataGridProducts.Location = new System.Drawing.Point(12, 95);
            this.dataGridProducts.MultiSelect = false;
            this.dataGridProducts.Name = "dataGridProducts";
            this.dataGridProducts.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridProducts.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridProducts.RowHeadersVisible = false;
            this.dataGridProducts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridProducts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridProducts.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridProducts.Size = new System.Drawing.Size(1019, 260);
            this.dataGridProducts.TabIndex = 22;
            this.dataGridProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridProducts_CellClick);
            this.dataGridProducts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridProducts_KeyDown);
            this.dataGridProducts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridProducts_KeyPress);
            // 
            // UPCCode
            // 
            this.UPCCode.DataPropertyName = "UPCCode";
            this.UPCCode.HeaderText = "UPC Code ";
            this.UPCCode.Name = "UPCCode";
            this.UPCCode.Visible = false;
            // 
            // LinkedUPCCode
            // 
            this.LinkedUPCCode.DataPropertyName = "LinkedUPCCode";
            this.LinkedUPCCode.HeaderText = "LinkedUPCCode";
            this.LinkedUPCCode.Name = "LinkedUPCCode";
            this.LinkedUPCCode.ReadOnly = true;
            this.LinkedUPCCode.Visible = false;
            // 
            // ParentUPCCode
            // 
            this.ParentUPCCode.DataPropertyName = "ParentUPCCode";
            this.ParentUPCCode.HeaderText = "ParentUPCCode";
            this.ParentUPCCode.Name = "ParentUPCCode";
            this.ParentUPCCode.ReadOnly = true;
            this.ParentUPCCode.Visible = false;
            // 
            // IsForceTax
            // 
            this.IsForceTax.DataPropertyName = "IsForceTax";
            this.IsForceTax.HeaderText = "IsForceTax";
            this.IsForceTax.Name = "IsForceTax";
            this.IsForceTax.ReadOnly = true;
            this.IsForceTax.Visible = false;
            // 
            // IsRefund
            // 
            this.IsRefund.DataPropertyName = "IsRefund";
            this.IsRefund.HeaderText = "IsRefund";
            this.IsRefund.Name = "IsRefund";
            this.IsRefund.ReadOnly = true;
            this.IsRefund.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsRefund.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsRefund.Visible = false;
            // 
            // IsVoid
            // 
            this.IsVoid.DataPropertyName = "IsVoid";
            this.IsVoid.HeaderText = "IsVoid";
            this.IsVoid.Name = "IsVoid";
            this.IsVoid.ReadOnly = true;
            this.IsVoid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsVoid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsVoid.Visible = false;
            // 
            // ProductID
            // 
            this.ProductID.DataPropertyName = "ProductID";
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.Name = "ProductID";
            this.ProductID.Visible = false;
            // 
            // DepartmentID
            // 
            this.DepartmentID.DataPropertyName = "DepartmentID";
            this.DepartmentID.HeaderText = "DepartmentID";
            this.DepartmentID.Name = "DepartmentID";
            this.DepartmentID.Visible = false;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            this.SectionID.HeaderText = "SectionID";
            this.SectionID.Name = "SectionID";
            this.SectionID.Visible = false;
            // 
            // TaxGroupID
            // 
            this.TaxGroupID.DataPropertyName = "TaxGroupID";
            this.TaxGroupID.HeaderText = "TaxGroupID";
            this.TaxGroupID.Name = "TaxGroupID";
            this.TaxGroupID.Visible = false;
            // 
            // UnitMeasureID
            // 
            this.UnitMeasureID.DataPropertyName = "UnitMeasureID";
            this.UnitMeasureID.HeaderText = "UnitMeasureID";
            this.UnitMeasureID.Name = "UnitMeasureID";
            this.UnitMeasureID.Visible = false;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductName.HeaderText = "Product";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Qty";
            this.Qty.MaxInputLength = 10;
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // UnitMeasureCode
            // 
            this.UnitMeasureCode.DataPropertyName = "UnitMeasureCode";
            this.UnitMeasureCode.HeaderText = "Unit of Measure";
            this.UnitMeasureCode.Name = "UnitMeasureCode";
            this.UnitMeasureCode.Visible = false;
            // 
            // SellPrice
            // 
            this.SellPrice.DataPropertyName = "SellPrice";
            this.SellPrice.HeaderText = "Price";
            this.SellPrice.Name = "SellPrice";
            this.SellPrice.ReadOnly = true;
            this.SellPrice.Visible = false;
            // 
            // SalePrice
            // 
            this.SalePrice.DataPropertyName = "SalePrice";
            this.SalePrice.HeaderText = "Price";
            this.SalePrice.Name = "SalePrice";
            this.SalePrice.ReadOnly = true;
            // 
            // Tax
            // 
            this.Tax.DataPropertyName = "Tax";
            this.Tax.HeaderText = "Tax";
            this.Tax.Name = "Tax";
            this.Tax.Visible = false;
            // 
            // TaxAmount
            // 
            this.TaxAmount.DataPropertyName = "TaxAmount";
            this.TaxAmount.HeaderText = "Tax Amount";
            this.TaxAmount.Name = "TaxAmount";
            this.TaxAmount.Visible = false;
            // 
            // Discount
            // 
            this.Discount.DataPropertyName = "Discount";
            this.Discount.HeaderText = "Discount(%)";
            this.Discount.Name = "Discount";
            this.Discount.Visible = false;
            // 
            // DiscountAmount
            // 
            this.DiscountAmount.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.DiscountAmount.HeaderText = "Disc.";
            this.DiscountAmount.Name = "DiscountAmount";
            this.DiscountAmount.ReadOnly = true;
            this.DiscountAmount.Visible = false;
            // 
            // FoodStampTotal
            // 
            this.FoodStampTotal.DataPropertyName = "FoodStampTotal";
            this.FoodStampTotal.HeaderText = "FoodStampTotal";
            this.FoodStampTotal.Name = "FoodStampTotal";
            this.FoodStampTotal.ReadOnly = true;
            this.FoodStampTotal.Visible = false;
            // 
            // FinalPrice
            // 
            this.FinalPrice.DataPropertyName = "FinalPrice";
            this.FinalPrice.HeaderText = "Total";
            this.FinalPrice.Name = "FinalPrice";
            this.FinalPrice.ReadOnly = true;
            // 
            // IsScale
            // 
            this.IsScale.DataPropertyName = "IsScale";
            this.IsScale.HeaderText = "IsScale";
            this.IsScale.Name = "IsScale";
            this.IsScale.Visible = false;
            // 
            // IsFoodStamp
            // 
            this.IsFoodStamp.DataPropertyName = "IsFoodStamp";
            this.IsFoodStamp.HeaderText = "IsFoodStamp";
            this.IsFoodStamp.Name = "IsFoodStamp";
            this.IsFoodStamp.ReadOnly = true;
            this.IsFoodStamp.Visible = false;
            // 
            // IsTax
            // 
            this.IsTax.DataPropertyName = "IsTax";
            this.IsTax.HeaderText = "IsTax";
            this.IsTax.Name = "IsTax";
            this.IsTax.ReadOnly = true;
            this.IsTax.Visible = false;
            // 
            // DiscountApplyed
            // 
            this.DiscountApplyed.DataPropertyName = "DiscountApplyed";
            this.DiscountApplyed.HeaderText = "DiscountApplyed";
            this.DiscountApplyed.Name = "DiscountApplyed";
            this.DiscountApplyed.ReadOnly = true;
            this.DiscountApplyed.Visible = false;
            // 
            // Abb
            // 
            this.Abb.DataPropertyName = "Abb";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Abb.DefaultCellStyle = dataGridViewCellStyle5;
            this.Abb.HeaderText = "";
            this.Abb.Name = "Abb";
            this.Abb.ReadOnly = true;
            // 
            // Image
            // 
            this.Image.DataPropertyName = "Image";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle6.NullValue")));
            this.Image.DefaultCellStyle = dataGridViewCellStyle6;
            this.Image.HeaderText = "";
            this.Image.Image = global::SFPOSWindows.Properties.Resources.transparent;
            this.Image.Name = "Image";
            this.Image.ReadOnly = true;
            this.Image.ToolTipText = "Tax enabled";
            // 
            // CasePriceApplied
            // 
            this.CasePriceApplied.DataPropertyName = "CasePriceApplied";
            this.CasePriceApplied.HeaderText = "CasePriceApplied";
            this.CasePriceApplied.Name = "CasePriceApplied";
            this.CasePriceApplied.Visible = false;
            // 
            // GroupQty
            // 
            this.GroupQty.DataPropertyName = "GroupQty";
            this.GroupQty.HeaderText = "GroupQty";
            this.GroupQty.Name = "GroupQty";
            this.GroupQty.ReadOnly = true;
            this.GroupQty.Visible = false;
            // 
            // GroupPrice
            // 
            this.GroupPrice.DataPropertyName = "GroupPrice";
            this.GroupPrice.HeaderText = "GroupPrice";
            this.GroupPrice.Name = "GroupPrice";
            this.GroupPrice.ReadOnly = true;
            this.GroupPrice.Visible = false;
            // 
            // CaseQty
            // 
            this.CaseQty.DataPropertyName = "CaseQty";
            this.CaseQty.HeaderText = "CaseQty";
            this.CaseQty.Name = "CaseQty";
            this.CaseQty.ReadOnly = true;
            this.CaseQty.Visible = false;
            // 
            // CasePrice
            // 
            this.CasePrice.DataPropertyName = "CasePrice";
            this.CasePrice.HeaderText = "CasePrice";
            this.CasePrice.Name = "CasePrice";
            this.CasePrice.ReadOnly = true;
            this.CasePrice.Visible = false;
            // 
            // txtSearchUPCCode
            // 
            this.txtSearchUPCCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSearchUPCCode.CustomButton.Image = null;
            this.txtSearchUPCCode.CustomButton.Location = new System.Drawing.Point(988, 2);
            this.txtSearchUPCCode.CustomButton.Name = "";
            this.txtSearchUPCCode.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.txtSearchUPCCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchUPCCode.CustomButton.TabIndex = 1;
            this.txtSearchUPCCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchUPCCode.CustomButton.UseSelectable = true;
            this.txtSearchUPCCode.CustomButton.Visible = false;
            this.txtSearchUPCCode.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtSearchUPCCode.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtSearchUPCCode.Lines = new string[0];
            this.txtSearchUPCCode.Location = new System.Drawing.Point(12, 55);
            this.txtSearchUPCCode.MaxLength = 30;
            this.txtSearchUPCCode.Name = "txtSearchUPCCode";
            this.txtSearchUPCCode.PasswordChar = '\0';
            this.txtSearchUPCCode.PromptText = "UPC Code";
            this.txtSearchUPCCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchUPCCode.SelectedText = "";
            this.txtSearchUPCCode.SelectionLength = 0;
            this.txtSearchUPCCode.SelectionStart = 0;
            this.txtSearchUPCCode.ShortcutsEnabled = true;
            this.txtSearchUPCCode.Size = new System.Drawing.Size(1020, 34);
            this.txtSearchUPCCode.TabIndex = 1;
            this.txtSearchUPCCode.UseSelectable = true;
            this.txtSearchUPCCode.WaterMark = "UPC Code";
            this.txtSearchUPCCode.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtSearchUPCCode.WaterMarkFont = new System.Drawing.Font("Courier New", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchUPCCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchUPCCode_KeyDown);
            this.txtSearchUPCCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchUPCCode_KeyPress);
            // 
            // lblFSTotal
            // 
            this.lblFSTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFSTotal.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFSTotal.ForeColor = System.Drawing.Color.Black;
            this.lblFSTotal.Location = new System.Drawing.Point(585, 418);
            this.lblFSTotal.MaximumSize = new System.Drawing.Size(170, 30);
            this.lblFSTotal.MinimumSize = new System.Drawing.Size(0, 30);
            this.lblFSTotal.Name = "lblFSTotal";
            this.lblFSTotal.Size = new System.Drawing.Size(110, 30);
            this.lblFSTotal.TabIndex = 24;
            this.lblFSTotal.Text = "$0.00";
            this.lblFSTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFoodStamp
            // 
            this.lblFoodStamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFoodStamp.AutoSize = true;
            this.lblFoodStamp.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFoodStamp.ForeColor = System.Drawing.Color.Black;
            this.lblFoodStamp.Location = new System.Drawing.Point(587, 388);
            this.lblFoodStamp.Name = "lblFoodStamp";
            this.lblFoodStamp.Size = new System.Drawing.Size(108, 18);
            this.lblFoodStamp.TabIndex = 23;
            this.lblFoodStamp.Text = "Food Stamp";
            // 
            // lblWeight
            // 
            this.lblWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWeight.AutoSize = true;
            this.lblWeight.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.ForeColor = System.Drawing.Color.Black;
            this.lblWeight.Location = new System.Drawing.Point(5, 415);
            this.lblWeight.MinimumSize = new System.Drawing.Size(150, 37);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(150, 37);
            this.lblWeight.TabIndex = 19;
            this.lblWeight.Text = "0.00 lb";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.lblFinalAmount);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(704, 381);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 66);
            this.panel1.TabIndex = 14;
            // 
            // SyncTimer_LocalToLive
            // 
            this.SyncTimer_LocalToLive.Tick += new System.EventHandler(this.SyncTimer_LocalToLive_Tick);
            // 
            // backgroundWorker_LocalToLive
            // 
            this.backgroundWorker_LocalToLive.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_LocalToLive_DoWork);
            this.backgroundWorker_LocalToLive.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_LocalToLive_RunWorkerCompleted);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "Image";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle9.NullValue")));
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::SFPOSWindows.Properties.Resources.flag;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.ToolTipText = "Tax enabled";
            this.dataGridViewImageColumn1.Width = 195;
            // 
            // frmOrderScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1044, 463);
            this.Controls.Add(this.lblFSTotal);
            this.Controls.Add(this.lblFoodStamp);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblSubTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.txtSearchUPCCode);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.lblTaxAmount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.dataGridProducts);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOrderScanner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order Scanner";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOrderScanner_FormClosed);
            this.Load += new System.EventHandler(this.frmOrderScanner_Load);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicNoInternet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicNoInternetLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProducts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lblFinalAmount;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.ComponentModel.BackgroundWorker backgroundWorker_DataSync;
        private System.Windows.Forms.Timer SyncTimer;
        private System.Windows.Forms.Panel panel8;
        public System.Windows.Forms.Label lblStoreName;
        private System.Windows.Forms.Button btnShortCut;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblDate;
        public System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label11;
        public MetroFramework.Controls.MetroGrid dataGridProducts;
        private MetroFramework.Controls.MetroTextBox txtSearchUPCCode;
        private System.Windows.Forms.Label lblFSTotal;
        private System.Windows.Forms.Label lblFoodStamp;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.PictureBox PicNoInternet;
        private System.Windows.Forms.PictureBox PicNoInternetLine;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer SyncTimer_LocalToLive;
        private System.ComponentModel.BackgroundWorker backgroundWorker_LocalToLive;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPCCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkedUPCCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParentUPCCode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsForceTax;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsRefund;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsVoid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxGroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitMeasureID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitMeasureCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tax;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoodStampTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinalPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsScale;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsFoodStamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountApplyed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Abb;
        private System.Windows.Forms.DataGridViewImageColumn Image;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CasePriceApplied;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaseQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CasePrice;
        public System.Windows.Forms.Label lblVersion;
    }
}