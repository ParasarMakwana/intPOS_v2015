namespace SFPOSWindows.Metro_Forms
{
    partial class FrmMetroMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMetroMaster));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.procurementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taxSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roleSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveCountersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SyncDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.couponToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tillStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Pen6 = new MetroFramework.Controls.MetroPanel();
            this.Pen5 = new MetroFramework.Controls.MetroPanel();
            this.Pen8 = new MetroFramework.Controls.MetroPanel();
            this.Pen4 = new MetroFramework.Controls.MetroPanel();
            this.Pen3 = new MetroFramework.Controls.MetroPanel();
            this.Pen2 = new MetroFramework.Controls.MetroPanel();
            this.Pen1 = new MetroFramework.Controls.MetroPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Pen12 = new MetroFramework.Controls.MetroPanel();
            this.Pen13 = new MetroFramework.Controls.MetroPanel();
            this.Pen11 = new MetroFramework.Controls.MetroPanel();
            this.Pen10 = new MetroFramework.Controls.MetroPanel();
            this.Pen9 = new MetroFramework.Controls.MetroPanel();
            this.Pen7 = new MetroFramework.Controls.MetroPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel8 = new MetroFramework.Controls.MetroPanel();
            this.lblStoreName = new MetroFramework.Controls.MetroLabel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Pen12.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardToolStripMenuItem,
            this.storeToolStripMenuItem,
            this.productToolStripMenuItem,
            this.procurementToolStripMenuItem,
            this.salesStatisticsToolStripMenuItem,
            this.taxSetupToolStripMenuItem,
            this.roleSetupToolStripMenuItem,
            this.liveCountersToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.SyncDataToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.couponToolStripMenuItem,
            this.tillStatusToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(195, 508);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // dashboardToolStripMenuItem
            // 
            this.dashboardToolStripMenuItem.AutoSize = false;
            this.dashboardToolStripMenuItem.Checked = true;
            this.dashboardToolStripMenuItem.CheckOnClick = true;
            this.dashboardToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dashboardToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dashboardToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            this.dashboardToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.dashboardToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.dashboardToolStripMenuItem.Text = "Dashboard";
            this.dashboardToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dashboardToolStripMenuItem.Click += new System.EventHandler(this.dashboardToolStripMenuItem_Click);
            // 
            // storeToolStripMenuItem
            // 
            this.storeToolStripMenuItem.AutoSize = false;
            this.storeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.storeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeToolStripMenuItem.Name = "storeToolStripMenuItem";
            this.storeToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 10, 10, 10);
            this.storeToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.storeToolStripMenuItem.Text = "Store";
            this.storeToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.storeToolStripMenuItem.Click += new System.EventHandler(this.storeToolStripMenuItem_Click);
            // 
            // productToolStripMenuItem
            // 
            this.productToolStripMenuItem.AutoSize = false;
            this.productToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.productToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.productToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.productToolStripMenuItem.Text = "Product";
            this.productToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.productToolStripMenuItem.Click += new System.EventHandler(this.productToolStripMenuItem_Click);
            // 
            // procurementToolStripMenuItem
            // 
            this.procurementToolStripMenuItem.AutoSize = false;
            this.procurementToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.procurementToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procurementToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.procurementToolStripMenuItem.Name = "procurementToolStripMenuItem";
            this.procurementToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.procurementToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.procurementToolStripMenuItem.Text = "Procurement";
            this.procurementToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.procurementToolStripMenuItem.Click += new System.EventHandler(this.procurementToolStripMenuItem_Click);
            // 
            // salesStatisticsToolStripMenuItem
            // 
            this.salesStatisticsToolStripMenuItem.AutoSize = false;
            this.salesStatisticsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salesStatisticsToolStripMenuItem.Margin = new System.Windows.Forms.Padding(-1, 0, 0, 0);
            this.salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
            this.salesStatisticsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.salesStatisticsToolStripMenuItem.Text = "Sales";
            this.salesStatisticsToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.salesStatisticsToolStripMenuItem.Click += new System.EventHandler(this.salesStatisticsToolStripMenuItem_Click);
            // 
            // taxSetupToolStripMenuItem
            // 
            this.taxSetupToolStripMenuItem.AutoSize = false;
            this.taxSetupToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.taxSetupToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.taxSetupToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taxSetupToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.taxSetupToolStripMenuItem.Name = "taxSetupToolStripMenuItem";
            this.taxSetupToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.taxSetupToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.taxSetupToolStripMenuItem.Text = "Tax Setup";
            this.taxSetupToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.taxSetupToolStripMenuItem.Click += new System.EventHandler(this.taxSetupToolStripMenuItem_Click);
            // 
            // roleSetupToolStripMenuItem
            // 
            this.roleSetupToolStripMenuItem.AutoSize = false;
            this.roleSetupToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.roleSetupToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roleSetupToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.roleSetupToolStripMenuItem.Name = "roleSetupToolStripMenuItem";
            this.roleSetupToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.roleSetupToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.roleSetupToolStripMenuItem.Text = "Role Setup";
            this.roleSetupToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.roleSetupToolStripMenuItem.Click += new System.EventHandler(this.roleSetupToolStripMenuItem_Click);
            // 
            // liveCountersToolStripMenuItem
            // 
            this.liveCountersToolStripMenuItem.AutoSize = false;
            this.liveCountersToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.liveCountersToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.liveCountersToolStripMenuItem.Name = "liveCountersToolStripMenuItem";
            this.liveCountersToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.liveCountersToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.liveCountersToolStripMenuItem.Text = "Live Counters";
            this.liveCountersToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.liveCountersToolStripMenuItem.Click += new System.EventHandler(this.liveCountersToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.AutoSize = false;
            this.reportsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsToolStripMenuItem.Margin = new System.Windows.Forms.Padding(-1, 0, 0, 0);
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.reportsToolStripMenuItem.Text = "Reports";
            this.reportsToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // SyncDataToolStripMenuItem
            // 
            this.SyncDataToolStripMenuItem.AutoSize = false;
            this.SyncDataToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SyncDataToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.SyncDataToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.SyncDataToolStripMenuItem.Name = "SyncDataToolStripMenuItem";
            this.SyncDataToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.SyncDataToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.SyncDataToolStripMenuItem.Text = "Sync Data";
            this.SyncDataToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SyncDataToolStripMenuItem.Click += new System.EventHandler(this.SyncDataToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.AutoSize = false;
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsToolStripMenuItem.Margin = new System.Windows.Forms.Padding(-1, 0, 0, 0);
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // couponToolStripMenuItem
            // 
            this.couponToolStripMenuItem.AutoSize = false;
            this.couponToolStripMenuItem.Checked = true;
            this.couponToolStripMenuItem.CheckOnClick = true;
            this.couponToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.couponToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.couponToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.couponToolStripMenuItem.Name = "couponToolStripMenuItem";
            this.couponToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.couponToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.couponToolStripMenuItem.Text = "Coupon";
            this.couponToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.couponToolStripMenuItem.Visible = false;
            this.couponToolStripMenuItem.Click += new System.EventHandler(this.couponToolStripMenuItem_Click);
            // 
            // tillStatusToolStripMenuItem
            // 
            this.tillStatusToolStripMenuItem.AutoSize = false;
            this.tillStatusToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tillStatusToolStripMenuItem.Margin = new System.Windows.Forms.Padding(-1, 0, 0, 0);
            this.tillStatusToolStripMenuItem.Name = "tillStatusToolStripMenuItem";
            this.tillStatusToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.tillStatusToolStripMenuItem.Size = new System.Drawing.Size(180, 40);
            this.tillStatusToolStripMenuItem.Text = "Till Status";
            this.tillStatusToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tillStatusToolStripMenuItem.Click += new System.EventHandler(this.tillStatusToolStripMenuItem_Click);
            // 
            // Pen6
            // 
            this.Pen6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen6.HorizontalScrollbarBarColor = true;
            this.Pen6.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen6.HorizontalScrollbarSize = 10;
            this.Pen6.Location = new System.Drawing.Point(-3, 240);
            this.Pen6.Name = "Pen6";
            this.Pen6.Size = new System.Drawing.Size(13, 40);
            this.Pen6.TabIndex = 26;
            this.Pen6.UseCustomBackColor = true;
            this.Pen6.UseCustomForeColor = true;
            this.Pen6.VerticalScrollbarBarColor = true;
            this.Pen6.VerticalScrollbarHighlightOnWheel = false;
            this.Pen6.VerticalScrollbarSize = 10;
            // 
            // Pen5
            // 
            this.Pen5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen5.HorizontalScrollbarBarColor = true;
            this.Pen5.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen5.HorizontalScrollbarSize = 10;
            this.Pen5.Location = new System.Drawing.Point(-3, 200);
            this.Pen5.Name = "Pen5";
            this.Pen5.Size = new System.Drawing.Size(13, 40);
            this.Pen5.TabIndex = 27;
            this.Pen5.UseCustomBackColor = true;
            this.Pen5.UseCustomForeColor = true;
            this.Pen5.VerticalScrollbarBarColor = true;
            this.Pen5.VerticalScrollbarHighlightOnWheel = false;
            this.Pen5.VerticalScrollbarSize = 10;
            // 
            // Pen8
            // 
            this.Pen8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen8.HorizontalScrollbarBarColor = true;
            this.Pen8.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen8.HorizontalScrollbarSize = 10;
            this.Pen8.Location = new System.Drawing.Point(-3, 160);
            this.Pen8.Name = "Pen8";
            this.Pen8.Size = new System.Drawing.Size(13, 40);
            this.Pen8.TabIndex = 27;
            this.Pen8.UseCustomBackColor = true;
            this.Pen8.UseCustomForeColor = true;
            this.Pen8.VerticalScrollbarBarColor = true;
            this.Pen8.VerticalScrollbarHighlightOnWheel = false;
            this.Pen8.VerticalScrollbarSize = 10;
            // 
            // Pen4
            // 
            this.Pen4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen4.HorizontalScrollbarBarColor = true;
            this.Pen4.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen4.HorizontalScrollbarSize = 10;
            this.Pen4.Location = new System.Drawing.Point(-3, 120);
            this.Pen4.Name = "Pen4";
            this.Pen4.Size = new System.Drawing.Size(13, 40);
            this.Pen4.TabIndex = 27;
            this.Pen4.UseCustomBackColor = true;
            this.Pen4.UseCustomForeColor = true;
            this.Pen4.VerticalScrollbarBarColor = true;
            this.Pen4.VerticalScrollbarHighlightOnWheel = false;
            this.Pen4.VerticalScrollbarSize = 10;
            // 
            // Pen3
            // 
            this.Pen3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen3.HorizontalScrollbarBarColor = true;
            this.Pen3.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen3.HorizontalScrollbarSize = 10;
            this.Pen3.Location = new System.Drawing.Point(-3, 80);
            this.Pen3.Name = "Pen3";
            this.Pen3.Size = new System.Drawing.Size(13, 40);
            this.Pen3.TabIndex = 28;
            this.Pen3.UseCustomBackColor = true;
            this.Pen3.UseCustomForeColor = true;
            this.Pen3.VerticalScrollbarBarColor = true;
            this.Pen3.VerticalScrollbarHighlightOnWheel = false;
            this.Pen3.VerticalScrollbarSize = 10;
            // 
            // Pen2
            // 
            this.Pen2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen2.HorizontalScrollbarBarColor = true;
            this.Pen2.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen2.HorizontalScrollbarSize = 10;
            this.Pen2.Location = new System.Drawing.Point(-4, 40);
            this.Pen2.Name = "Pen2";
            this.Pen2.Size = new System.Drawing.Size(14, 40);
            this.Pen2.TabIndex = 29;
            this.Pen2.UseCustomBackColor = true;
            this.Pen2.UseCustomForeColor = true;
            this.Pen2.VerticalScrollbarBarColor = true;
            this.Pen2.VerticalScrollbarHighlightOnWheel = false;
            this.Pen2.VerticalScrollbarSize = 10;
            // 
            // Pen1
            // 
            this.Pen1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen1.HorizontalScrollbarBarColor = true;
            this.Pen1.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen1.HorizontalScrollbarSize = 10;
            this.Pen1.Location = new System.Drawing.Point(0, 56);
            this.Pen1.Name = "Pen1";
            this.Pen1.Size = new System.Drawing.Size(12, 40);
            this.Pen1.TabIndex = 30;
            this.Pen1.UseCustomBackColor = true;
            this.Pen1.UseCustomForeColor = true;
            this.Pen1.VerticalScrollbarBarColor = true;
            this.Pen1.VerticalScrollbarHighlightOnWheel = false;
            this.Pen1.VerticalScrollbarSize = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(2, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.Pen12);
            this.splitContainer1.Panel1.Controls.Add(this.Pen11);
            this.splitContainer1.Panel1.Controls.Add(this.Pen10);
            this.splitContainer1.Panel1.Controls.Add(this.Pen9);
            this.splitContainer1.Panel1.Controls.Add(this.Pen7);
            this.splitContainer1.Panel1.Controls.Add(this.Pen5);
            this.splitContainer1.Panel1.Controls.Add(this.Pen4);
            this.splitContainer1.Panel1.Controls.Add(this.Pen3);
            this.splitContainer1.Panel1.Controls.Add(this.Pen6);
            this.splitContainer1.Panel1.Controls.Add(this.Pen8);
            this.splitContainer1.Panel1.Controls.Add(this.Pen2);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1MinSize = 10;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackgroundImage = global::SFPOSWindows.Properties.Resources.intPOSlight;
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(827, 497);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.TabIndex = 32;
            // 
            // Pen12
            // 
            this.Pen12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen12.Controls.Add(this.Pen13);
            this.Pen12.HorizontalScrollbarBarColor = true;
            this.Pen12.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen12.HorizontalScrollbarSize = 10;
            this.Pen12.Location = new System.Drawing.Point(-3, 440);
            this.Pen12.Name = "Pen12";
            this.Pen12.Size = new System.Drawing.Size(13, 40);
            this.Pen12.TabIndex = 32;
            this.Pen12.UseCustomBackColor = true;
            this.Pen12.UseCustomForeColor = true;
            this.Pen12.VerticalScrollbarBarColor = true;
            this.Pen12.VerticalScrollbarHighlightOnWheel = false;
            this.Pen12.VerticalScrollbarSize = 10;
            this.Pen12.Visible = false;
            // 
            // Pen13
            // 
            this.Pen13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen13.HorizontalScrollbarBarColor = true;
            this.Pen13.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen13.HorizontalScrollbarSize = 10;
            this.Pen13.Location = new System.Drawing.Point(3, 28);
            this.Pen13.Name = "Pen13";
            this.Pen13.Size = new System.Drawing.Size(13, 40);
            this.Pen13.TabIndex = 33;
            this.Pen13.UseCustomBackColor = true;
            this.Pen13.UseCustomForeColor = true;
            this.Pen13.VerticalScrollbarBarColor = true;
            this.Pen13.VerticalScrollbarHighlightOnWheel = false;
            this.Pen13.VerticalScrollbarSize = 10;
            this.Pen13.Visible = false;
            // 
            // Pen11
            // 
            this.Pen11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen11.HorizontalScrollbarBarColor = true;
            this.Pen11.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen11.HorizontalScrollbarSize = 10;
            this.Pen11.Location = new System.Drawing.Point(-3, 400);
            this.Pen11.Name = "Pen11";
            this.Pen11.Size = new System.Drawing.Size(13, 40);
            this.Pen11.TabIndex = 31;
            this.Pen11.UseCustomBackColor = true;
            this.Pen11.UseCustomForeColor = true;
            this.Pen11.VerticalScrollbarBarColor = true;
            this.Pen11.VerticalScrollbarHighlightOnWheel = false;
            this.Pen11.VerticalScrollbarSize = 10;
            this.Pen11.Visible = false;
            // 
            // Pen10
            // 
            this.Pen10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen10.HorizontalScrollbarBarColor = true;
            this.Pen10.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen10.HorizontalScrollbarSize = 10;
            this.Pen10.Location = new System.Drawing.Point(-3, 360);
            this.Pen10.Name = "Pen10";
            this.Pen10.Size = new System.Drawing.Size(13, 40);
            this.Pen10.TabIndex = 29;
            this.Pen10.UseCustomBackColor = true;
            this.Pen10.UseCustomForeColor = true;
            this.Pen10.VerticalScrollbarBarColor = true;
            this.Pen10.VerticalScrollbarHighlightOnWheel = false;
            this.Pen10.VerticalScrollbarSize = 10;
            this.Pen10.Visible = false;
            // 
            // Pen9
            // 
            this.Pen9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen9.HorizontalScrollbarBarColor = true;
            this.Pen9.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen9.HorizontalScrollbarSize = 10;
            this.Pen9.Location = new System.Drawing.Point(-3, 320);
            this.Pen9.Name = "Pen9";
            this.Pen9.Size = new System.Drawing.Size(13, 40);
            this.Pen9.TabIndex = 28;
            this.Pen9.UseCustomBackColor = true;
            this.Pen9.UseCustomForeColor = true;
            this.Pen9.VerticalScrollbarBarColor = true;
            this.Pen9.VerticalScrollbarHighlightOnWheel = false;
            this.Pen9.VerticalScrollbarSize = 10;
            // 
            // Pen7
            // 
            this.Pen7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Pen7.HorizontalScrollbarBarColor = true;
            this.Pen7.HorizontalScrollbarHighlightOnWheel = false;
            this.Pen7.HorizontalScrollbarSize = 10;
            this.Pen7.Location = new System.Drawing.Point(-3, 280);
            this.Pen7.Name = "Pen7";
            this.Pen7.Size = new System.Drawing.Size(13, 40);
            this.Pen7.TabIndex = 27;
            this.Pen7.UseCustomBackColor = true;
            this.Pen7.UseCustomForeColor = true;
            this.Pen7.VerticalScrollbarBarColor = true;
            this.Pen7.VerticalScrollbarHighlightOnWheel = false;
            this.Pen7.VerticalScrollbarSize = 10;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 3);
            this.panel2.TabIndex = 35;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.metroPanel1.BackColor = System.Drawing.Color.LightGray;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(169, 53);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(3, 541);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.UseCustomBackColor = true;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel8
            // 
            this.metroPanel8.BackColor = System.Drawing.Color.White;
            this.metroPanel8.BackgroundImage = global::SFPOSWindows.Properties.Resources.intPOS_logo;
            this.metroPanel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.metroPanel8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.metroPanel8.HorizontalScrollbarBarColor = true;
            this.metroPanel8.HorizontalScrollbarHighlightOnWheel = true;
            this.metroPanel8.HorizontalScrollbarSize = 10;
            this.metroPanel8.Location = new System.Drawing.Point(18, 8);
            this.metroPanel8.Name = "metroPanel8";
            this.metroPanel8.Size = new System.Drawing.Size(45, 45);
            this.metroPanel8.TabIndex = 31;
            this.metroPanel8.UseCustomBackColor = true;
            this.metroPanel8.VerticalScrollbar = true;
            this.metroPanel8.VerticalScrollbarBarColor = true;
            this.metroPanel8.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel8.VerticalScrollbarSize = 10;
            // 
            // lblStoreName
            // 
            this.lblStoreName.AutoSize = true;
            this.lblStoreName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblStoreName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblStoreName.Location = new System.Drawing.Point(69, 15);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(107, 25);
            this.lblStoreName.TabIndex = 36;
            this.lblStoreName.Text = "StoreName";
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblDate.Location = new System.Drawing.Point(602, 22);
            this.lblDate.MaximumSize = new System.Drawing.Size(300, 30);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(108, 18);
            this.lblDate.TabIndex = 39;
            this.lblDate.Text = "30/01/2020";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblTime.Location = new System.Drawing.Point(711, 22);
            this.lblTime.MaximumSize = new System.Drawing.Size(300, 30);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(118, 18);
            this.lblTime.TabIndex = 38;
            this.lblTime.Text = "00:00:00 PM";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblVersion.Location = new System.Drawing.Point(217, 22);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(58, 18);
            this.lblVersion.TabIndex = 40;
            this.lblVersion.Text = "0.0.0";
            // 
            // lblLoginName
            // 
            this.lblLoginName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblLoginName.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblLoginName.Location = new System.Drawing.Point(284, 22);
            this.lblLoginName.MaximumSize = new System.Drawing.Size(500, 30);
            this.lblLoginName.MinimumSize = new System.Drawing.Size(200, 17);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(295, 20);
            this.lblLoginName.TabIndex = 37;
            this.lblLoginName.Text = "TECHCRONUS";
            this.lblLoginName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(573, 22);
            this.label1.MaximumSize = new System.Drawing.Size(300, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 18);
            this.label1.TabIndex = 41;
            this.label1.Text = "|";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label2.Location = new System.Drawing.Point(273, 22);
            this.label2.MaximumSize = new System.Drawing.Size(300, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 18);
            this.label2.TabIndex = 42;
            this.label2.Text = "|";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMetroMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(831, 552);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.Pen1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblLoginName);
            this.Controls.Add(this.lblStoreName);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.metroPanel8);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMetroMaster";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMetroMaster_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.Pen12.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dashboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procurementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taxSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roleSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liveCountersToolStripMenuItem;
        private MetroFramework.Controls.MetroPanel Pen6;
        private MetroFramework.Controls.MetroPanel Pen5;
        private MetroFramework.Controls.MetroPanel Pen8;
        private MetroFramework.Controls.MetroPanel Pen4;
        private MetroFramework.Controls.MetroPanel Pen3;
        private MetroFramework.Controls.MetroPanel Pen2;
        private MetroFramework.Controls.MetroPanel Pen1;
        private MetroFramework.Controls.MetroPanel metroPanel8;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.ToolStripMenuItem salesStatisticsToolStripMenuItem;
        private MetroFramework.Controls.MetroPanel Pen7;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private MetroFramework.Controls.MetroPanel Pen9;
        private System.Windows.Forms.ToolStripMenuItem SyncDataToolStripMenuItem;
        private MetroFramework.Controls.MetroPanel Pen10;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private MetroFramework.Controls.MetroPanel Pen11;
        private MetroFramework.Controls.MetroLabel lblStoreName;
        private System.Windows.Forms.ToolStripMenuItem couponToolStripMenuItem;
        private MetroFramework.Controls.MetroPanel Pen12;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        public System.Windows.Forms.Label lblVersion;
        public System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem tillStatusToolStripMenuItem;
        private MetroFramework.Controls.MetroPanel Pen13;
    }
}