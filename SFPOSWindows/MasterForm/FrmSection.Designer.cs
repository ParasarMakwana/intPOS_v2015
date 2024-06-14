namespace SFPOSWindows.MasterForm
{
    partial class FrmSection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSection));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ColumnFilterpanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnSearch = new MetroFramework.Controls.MetroButton();
            this.txtSearchFilter = new MetroFramework.Controls.MetroTextBox();
            this.btnSaveFilter = new MetroFramework.Controls.MetroButton();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.chkBoxList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnclear = new MetroFramework.Controls.MetroButton();
            this.btnApplyFilter = new MetroFramework.Controls.MetroButton();
            this.SectionGrdView = new MetroFramework.Controls.MetroGrid();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.btnImport = new MetroFramework.Controls.MetroButton();
            this.txtSearchSectionName = new MetroFramework.Controls.MetroTextBox();
            this.MetrobtnAdd = new MetroFramework.Controls.MetroButton();
            this.btnShowAll = new MetroFramework.Controls.MetroButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnColumnFilter = new MetroFramework.Controls.MetroButton();
            this.btnEditList = new MetroFramework.Controls.MetroButton();
            this.cmbSearchFilter = new MetroFramework.Controls.MetroComboBox();
            this.panel1.SuspendLayout();
            this.ColumnFilterpanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionGrdView)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ColumnFilterpanel);
            this.panel1.Controls.Add(this.SectionGrdView);
            this.panel1.Location = new System.Drawing.Point(3, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 494);
            this.panel1.TabIndex = 22;
            // 
            // ColumnFilterpanel
            // 
            this.ColumnFilterpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColumnFilterpanel.Controls.Add(this.groupBox2);
            this.ColumnFilterpanel.Controls.Add(this.btnSaveFilter);
            this.ColumnFilterpanel.Controls.Add(this.btnCancel);
            this.ColumnFilterpanel.Controls.Add(this.label1);
            this.ColumnFilterpanel.Controls.Add(this.panel2);
            this.ColumnFilterpanel.Controls.Add(this.txtOut);
            this.ColumnFilterpanel.Controls.Add(this.chkBoxList);
            this.ColumnFilterpanel.Controls.Add(this.groupBox1);
            this.ColumnFilterpanel.Location = new System.Drawing.Point(163, 59);
            this.ColumnFilterpanel.Name = "ColumnFilterpanel";
            this.ColumnFilterpanel.Size = new System.Drawing.Size(379, 402);
            this.ColumnFilterpanel.TabIndex = 28;
            this.ColumnFilterpanel.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbSearchFilter);
            this.groupBox2.Controls.Add(this.BtnSearch);
            this.groupBox2.Controls.Add(this.txtSearchFilter);
            this.groupBox2.Location = new System.Drawing.Point(3, 341);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 52);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Filter";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSearch.BackgroundImage = global::SFPOSWindows.Properties.Resources.magnifying_glass2;
            this.BtnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnSearch.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BtnSearch.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.BtnSearch.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BtnSearch.Location = new System.Drawing.Point(327, 17);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(28, 29);
            this.BtnSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.BtnSearch.TabIndex = 39;
            this.BtnSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.BtnSearch.UseCustomForeColor = true;
            this.BtnSearch.UseSelectable = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtSearchFilter
            // 
            this.txtSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSearchFilter.CustomButton.Image = null;
            this.txtSearchFilter.CustomButton.Location = new System.Drawing.Point(120, 1);
            this.txtSearchFilter.CustomButton.Name = "";
            this.txtSearchFilter.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSearchFilter.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchFilter.CustomButton.TabIndex = 1;
            this.txtSearchFilter.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchFilter.CustomButton.UseSelectable = true;
            this.txtSearchFilter.CustomButton.Visible = false;
            this.txtSearchFilter.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSearchFilter.Lines = new string[0];
            this.txtSearchFilter.Location = new System.Drawing.Point(6, 17);
            this.txtSearchFilter.MaxLength = 100;
            this.txtSearchFilter.Name = "txtSearchFilter";
            this.txtSearchFilter.PasswordChar = '\0';
            this.txtSearchFilter.PromptText = "Search Here";
            this.txtSearchFilter.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchFilter.SelectedText = "";
            this.txtSearchFilter.SelectionLength = 0;
            this.txtSearchFilter.SelectionStart = 0;
            this.txtSearchFilter.ShortcutsEnabled = true;
            this.txtSearchFilter.Size = new System.Drawing.Size(148, 29);
            this.txtSearchFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchFilter.TabIndex = 37;
            this.txtSearchFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchFilter.UseSelectable = true;
            this.txtSearchFilter.WaterMark = "Search Here";
            this.txtSearchFilter.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchFilter.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnSaveFilter
            // 
            this.btnSaveFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFilter.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSaveFilter.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnSaveFilter.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSaveFilter.Location = new System.Drawing.Point(10, 301);
            this.btnSaveFilter.Name = "btnSaveFilter";
            this.btnSaveFilter.Size = new System.Drawing.Size(85, 27);
            this.btnSaveFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSaveFilter.TabIndex = 32;
            this.btnSaveFilter.Text = "Save";
            this.btnSaveFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSaveFilter.UseCustomForeColor = true;
            this.btnSaveFilter.UseSelectable = true;
            this.btnSaveFilter.Click += new System.EventHandler(this.btnSaveFilter_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnCancel.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCancel.Location = new System.Drawing.Point(282, 301);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 27);
            this.btnCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnCancel.UseCustomForeColor = true;
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Columns";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::SFPOSWindows.Properties.Resources._41;
            this.panel2.Location = new System.Drawing.Point(-6, -5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 10);
            this.panel2.TabIndex = 30;
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(346, 308);
            this.txtOut.Name = "txtOut";
            this.txtOut.Size = new System.Drawing.Size(10, 20);
            this.txtOut.TabIndex = 29;
            this.txtOut.Visible = false;
            // 
            // chkBoxList
            // 
            this.chkBoxList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBoxList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkBoxList.CheckOnClick = true;
            this.chkBoxList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBoxList.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkBoxList.FormattingEnabled = true;
            this.chkBoxList.Location = new System.Drawing.Point(15, 50);
            this.chkBoxList.Name = "chkBoxList";
            this.chkBoxList.Size = new System.Drawing.Size(346, 220);
            this.chkBoxList.TabIndex = 16;
            this.chkBoxList.ThreeDCheckBoxes = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnclear);
            this.groupBox1.Controls.Add(this.btnApplyFilter);
            this.groupBox1.Location = new System.Drawing.Point(3, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 300);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // btnclear
            // 
            this.btnclear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnclear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnclear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnclear.Location = new System.Drawing.Point(189, 266);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(85, 27);
            this.btnclear.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnclear.TabIndex = 39;
            this.btnclear.Text = "Clear Filter";
            this.btnclear.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnclear.UseCustomForeColor = true;
            this.btnclear.UseSelectable = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyFilter.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnApplyFilter.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnApplyFilter.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnApplyFilter.Location = new System.Drawing.Point(98, 266);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(85, 27);
            this.btnApplyFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnApplyFilter.TabIndex = 28;
            this.btnApplyFilter.Text = "Apply";
            this.btnApplyFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnApplyFilter.UseCustomForeColor = true;
            this.btnApplyFilter.UseSelectable = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // SectionGrdView
            // 
            this.SectionGrdView.AllowUserToAddRows = false;
            this.SectionGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.SectionGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SectionGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SectionGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SectionGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.SectionGrdView.BackgroundColor = System.Drawing.Color.White;
            this.SectionGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SectionGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.SectionGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SectionGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SectionGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SectionGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.SectionGrdView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.SectionGrdView.EnableHeadersVisualStyles = false;
            this.SectionGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SectionGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.SectionGrdView.Location = new System.Drawing.Point(3, 17);
            this.SectionGrdView.Name = "SectionGrdView";
            this.SectionGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SectionGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.SectionGrdView.RowHeadersVisible = false;
            this.SectionGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SectionGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SectionGrdView.Size = new System.Drawing.Size(745, 457);
            this.SectionGrdView.TabIndex = 18;
            this.SectionGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SectionGrdView_CellClick);
            this.SectionGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SectionGrdView_CellContentClick);
            this.SectionGrdView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SectionGrdView_CellDoubleClick);
            this.SectionGrdView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.SectionGrdView_CellValueChanged);
            this.SectionGrdView.CurrentCellDirtyStateChanged += new System.EventHandler(this.SectionGrdView_CurrentCellDirtyStateChanged);
            this.SectionGrdView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.SectionGrdView_DataError);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.btnExport);
            this.pnlSearch.Controls.Add(this.btnImport);
            this.pnlSearch.Controls.Add(this.txtSearchSectionName);
            this.pnlSearch.Controls.Add(this.MetrobtnAdd);
            this.pnlSearch.Controls.Add(this.btnShowAll);
            this.pnlSearch.Location = new System.Drawing.Point(3, 57);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(748, 58);
            this.pnlSearch.TabIndex = 23;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExport.Location = new System.Drawing.Point(548, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 27);
            this.btnExport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnExport.TabIndex = 25;
            this.btnExport.Text = "EXPORT";
            this.btnExport.UseCustomForeColor = true;
            this.btnExport.UseSelectable = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnImport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnImport.Location = new System.Drawing.Point(448, 10);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 27);
            this.btnImport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnImport.TabIndex = 24;
            this.btnImport.Text = "IMPORT";
            this.btnImport.UseCustomForeColor = true;
            this.btnImport.UseSelectable = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtSearchSectionName
            // 
            // 
            // 
            // 
            this.txtSearchSectionName.CustomButton.Image = null;
            this.txtSearchSectionName.CustomButton.Location = new System.Drawing.Point(257, 2);
            this.txtSearchSectionName.CustomButton.Name = "";
            this.txtSearchSectionName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchSectionName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchSectionName.CustomButton.TabIndex = 1;
            this.txtSearchSectionName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchSectionName.CustomButton.UseSelectable = true;
            this.txtSearchSectionName.CustomButton.Visible = false;
            this.txtSearchSectionName.DisplayIcon = true;
            this.txtSearchSectionName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass1;
            this.txtSearchSectionName.IconRight = true;
            this.txtSearchSectionName.Lines = new string[0];
            this.txtSearchSectionName.Location = new System.Drawing.Point(12, 9);
            this.txtSearchSectionName.MaxLength = 20;
            this.txtSearchSectionName.Name = "txtSearchSectionName";
            this.txtSearchSectionName.PasswordChar = '\0';
            this.txtSearchSectionName.PromptText = "Name";
            this.txtSearchSectionName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchSectionName.SelectedText = "";
            this.txtSearchSectionName.SelectionLength = 0;
            this.txtSearchSectionName.SelectionStart = 0;
            this.txtSearchSectionName.ShortcutsEnabled = true;
            this.txtSearchSectionName.Size = new System.Drawing.Size(281, 26);
            this.txtSearchSectionName.TabIndex = 8;
            this.txtSearchSectionName.UseSelectable = true;
            this.txtSearchSectionName.WaterMark = "Name";
            this.txtSearchSectionName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchSectionName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchSectionName.TextChanged += new System.EventHandler(this.txtSearchSectionName_TextChanged);
            // 
            // MetrobtnAdd
            // 
            this.MetrobtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MetrobtnAdd.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.MetrobtnAdd.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.MetrobtnAdd.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MetrobtnAdd.Location = new System.Drawing.Point(648, 10);
            this.MetrobtnAdd.Name = "MetrobtnAdd";
            this.MetrobtnAdd.Size = new System.Drawing.Size(95, 26);
            this.MetrobtnAdd.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnAdd.TabIndex = 21;
            this.MetrobtnAdd.Text = "ADD";
            this.MetrobtnAdd.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnAdd.UseCustomForeColor = true;
            this.MetrobtnAdd.UseSelectable = true;
            this.MetrobtnAdd.Click += new System.EventHandler(this.MetrobtnAdd_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnShowAll.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnShowAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowAll.Location = new System.Drawing.Point(301, 9);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(94, 27);
            this.btnShowAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnShowAll.TabIndex = 7;
            this.btnShowAll.Text = "SHOW ALL";
            this.btnShowAll.UseCustomForeColor = true;
            this.btnShowAll.UseSelectable = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnColumnFilter
            // 
            this.btnColumnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColumnFilter.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnColumnFilter.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnColumnFilter.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnColumnFilter.Location = new System.Drawing.Point(652, 26);
            this.btnColumnFilter.Name = "btnColumnFilter";
            this.btnColumnFilter.Size = new System.Drawing.Size(95, 26);
            this.btnColumnFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnColumnFilter.TabIndex = 26;
            this.btnColumnFilter.Text = "VIEW";
            this.btnColumnFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnColumnFilter.UseCustomForeColor = true;
            this.btnColumnFilter.UseSelectable = true;
            this.btnColumnFilter.Click += new System.EventHandler(this.btnColumnFilter_Click);
            // 
            // btnEditList
            // 
            this.btnEditList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditList.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnEditList.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnEditList.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnEditList.Location = new System.Drawing.Point(538, 25);
            this.btnEditList.Name = "btnEditList";
            this.btnEditList.Size = new System.Drawing.Size(107, 26);
            this.btnEditList.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnEditList.TabIndex = 29;
            this.btnEditList.Text = "EDIT LIST";
            this.btnEditList.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnEditList.UseCustomForeColor = true;
            this.btnEditList.UseSelectable = true;
            this.btnEditList.Visible = false;
            this.btnEditList.Click += new System.EventHandler(this.btnEditList_Click);
            // 
            // cmbSearchFilter
            // 
            this.cmbSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSearchFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbSearchFilter.FormattingEnabled = true;
            this.cmbSearchFilter.IntegralHeight = false;
            this.cmbSearchFilter.ItemHeight = 23;
            this.cmbSearchFilter.Location = new System.Drawing.Point(160, 17);
            this.cmbSearchFilter.Name = "cmbSearchFilter";
            this.cmbSearchFilter.Size = new System.Drawing.Size(164, 29);
            this.cmbSearchFilter.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmbSearchFilter.TabIndex = 29;
            this.cmbSearchFilter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cmbSearchFilter.UseSelectable = true;
            // 
            // FrmSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 605);
            this.Controls.Add(this.btnEditList);
            this.Controls.Add(this.btnColumnFilter);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSection";
            this.Resizable = false;
            this.Text = "Section - ";
            this.panel1.ResumeLayout(false);
            this.ColumnFilterpanel.ResumeLayout(false);
            this.ColumnFilterpanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SectionGrdView)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlSearch;
        private MetroFramework.Controls.MetroTextBox txtSearchSectionName;
        private MetroFramework.Controls.MetroButton btnShowAll;
        private MetroFramework.Controls.MetroButton MetrobtnAdd;
        public MetroFramework.Controls.MetroGrid SectionGrdView;
        private MetroFramework.Controls.MetroButton btnExport;
        private MetroFramework.Controls.MetroButton btnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private MetroFramework.Controls.MetroButton btnColumnFilter;
        private System.Windows.Forms.Panel ColumnFilterpanel;
        private MetroFramework.Controls.MetroButton btnCancel;
        private MetroFramework.Controls.MetroButton btnApplyFilter;
        private System.Windows.Forms.CheckedListBox chkBoxList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        public MetroFramework.Controls.MetroTextBox txtSearchFilter;
        private MetroFramework.Controls.MetroButton BtnSearch;
        private System.Windows.Forms.TextBox txtOut;
        private MetroFramework.Controls.MetroButton btnSaveFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton btnEditList;
        private MetroFramework.Controls.MetroButton btnclear;
        public MetroFramework.Controls.MetroComboBox cmbSearchFilter;
    }
}