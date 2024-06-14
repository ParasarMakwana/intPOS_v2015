namespace SFPOSWindows.MasterForm
{
    partial class FrmCustomer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.btnImport = new MetroFramework.Controls.MetroButton();
            this.btnShowAll = new MetroFramework.Controls.MetroButton();
            this.btnADD = new MetroFramework.Controls.MetroButton();
            this.txtSearchEmployeeName = new MetroFramework.Controls.MetroTextBox();
            this.CustomerGrdView = new MetroFramework.Controls.MetroGrid();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 44);
            this.panel1.TabIndex = 12;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(112, 32);
            this.lblSubHeading.TabIndex = 1;
            this.lblSubHeading.Text = "Customer";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.btnExport);
            this.pnlSearch.Controls.Add(this.btnImport);
            this.pnlSearch.Controls.Add(this.btnShowAll);
            this.pnlSearch.Controls.Add(this.btnADD);
            this.pnlSearch.Controls.Add(this.txtSearchEmployeeName);
            this.pnlSearch.Location = new System.Drawing.Point(5, 51);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(727, 47);
            this.pnlSearch.TabIndex = 10;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExport.Location = new System.Drawing.Point(524, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 27);
            this.btnExport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnExport.TabIndex = 20;
            this.btnExport.Text = "EXPORT";
            this.btnExport.UseCustomForeColor = true;
            this.btnExport.UseSelectable = true;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnImport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnImport.Location = new System.Drawing.Point(424, 8);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 27);
            this.btnImport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnImport.TabIndex = 19;
            this.btnImport.Text = "IMPORT";
            this.btnImport.UseCustomForeColor = true;
            this.btnImport.UseSelectable = true;
            this.btnImport.Visible = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnShowAll.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnShowAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowAll.Location = new System.Drawing.Point(300, 8);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(94, 27);
            this.btnShowAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnShowAll.TabIndex = 16;
            this.btnShowAll.Text = "SHOW ALL";
            this.btnShowAll.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnShowAll.UseCustomForeColor = true;
            this.btnShowAll.UseSelectable = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnADD
            // 
            this.btnADD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnADD.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnADD.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnADD.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnADD.Location = new System.Drawing.Point(623, 8);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(94, 27);
            this.btnADD.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnADD.TabIndex = 15;
            this.btnADD.Text = "ADD";
            this.btnADD.UseCustomForeColor = true;
            this.btnADD.UseSelectable = true;
            this.btnADD.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtSearchEmployeeName
            // 
            // 
            // 
            // 
            this.txtSearchEmployeeName.CustomButton.Image = null;
            this.txtSearchEmployeeName.CustomButton.Location = new System.Drawing.Point(258, 2);
            this.txtSearchEmployeeName.CustomButton.Name = "";
            this.txtSearchEmployeeName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchEmployeeName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchEmployeeName.CustomButton.TabIndex = 1;
            this.txtSearchEmployeeName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchEmployeeName.CustomButton.UseSelectable = true;
            this.txtSearchEmployeeName.CustomButton.Visible = false;
            this.txtSearchEmployeeName.DisplayIcon = true;
            this.txtSearchEmployeeName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearchEmployeeName.IconRight = true;
            this.txtSearchEmployeeName.Lines = new string[0];
            this.txtSearchEmployeeName.Location = new System.Drawing.Point(12, 9);
            this.txtSearchEmployeeName.MaxLength = 20;
            this.txtSearchEmployeeName.Name = "txtSearchEmployeeName";
            this.txtSearchEmployeeName.PasswordChar = '\0';
            this.txtSearchEmployeeName.PromptText = "First name/Last name/Customer ID/Phone";
            this.txtSearchEmployeeName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchEmployeeName.SelectedText = "";
            this.txtSearchEmployeeName.SelectionLength = 0;
            this.txtSearchEmployeeName.SelectionStart = 0;
            this.txtSearchEmployeeName.ShortcutsEnabled = true;
            this.txtSearchEmployeeName.Size = new System.Drawing.Size(282, 26);
            this.txtSearchEmployeeName.TabIndex = 14;
            this.txtSearchEmployeeName.UseSelectable = true;
            this.txtSearchEmployeeName.WaterMark = "First name/Last name/Customer ID/Phone";
            this.txtSearchEmployeeName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchEmployeeName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchEmployeeName.TextChanged += new System.EventHandler(this.txtSearchEmployeeName_TextChanged);
            // 
            // CustomerGrdView
            // 
            this.CustomerGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.CustomerGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CustomerGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomerGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CustomerGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.CustomerGrdView.BackgroundColor = System.Drawing.Color.White;
            this.CustomerGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CustomerGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.CustomerGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomerGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CustomerGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.CustomerGrdView.EnableHeadersVisualStyles = false;
            this.CustomerGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.CustomerGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.CustomerGrdView.Location = new System.Drawing.Point(17, 104);
            this.CustomerGrdView.Name = "CustomerGrdView";
            this.CustomerGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomerGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.CustomerGrdView.RowHeadersVisible = false;
            this.CustomerGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CustomerGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CustomerGrdView.Size = new System.Drawing.Size(705, 232);
            this.CustomerGrdView.TabIndex = 13;
            this.CustomerGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmployeeGrdView_CellClick);
            this.CustomerGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmployeeGrdView_CellContentClick);
            this.CustomerGrdView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmployeeGrdView_CellDoubleClick);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle5.NullValue")));
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle6.NullValue")));
            this.dataGridViewImageColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // FrmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 348);
            this.Controls.Add(this.CustomerGrdView);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCustomer";
            this.Text = "Employee Management";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSubHeading;
        private MetroFramework.Controls.MetroTextBox txtSearchEmployeeName;
        private MetroFramework.Controls.MetroButton btnADD;
        private MetroFramework.Controls.MetroButton btnShowAll;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        public MetroFramework.Controls.MetroGrid CustomerGrdView;
        private MetroFramework.Controls.MetroButton btnExport;
        private MetroFramework.Controls.MetroButton btnImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}