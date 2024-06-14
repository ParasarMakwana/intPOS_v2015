namespace SFPOSWindows.MasterForm
{
    partial class FrmSalesOrders
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.SaleOrderGrdView = new MetroFramework.Controls.MetroGrid();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.endDate = new MetroFramework.Controls.MetroDateTime();
            this.startDate = new MetroFramework.Controls.MetroDateTime();
            this.cmbCashier = new MetroFramework.Controls.MetroComboBox();
            this.txtSearchDepartmentName = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaleOrderGrdView)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 44);
            this.panel1.TabIndex = 2;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(8, 5);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(124, 32);
            this.lblSubHeading.TabIndex = 0;
            this.lblSubHeading.Text = "Store Sales";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaleOrderGrdView
            // 
            this.SaleOrderGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.SaleOrderGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SaleOrderGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaleOrderGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SaleOrderGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.SaleOrderGrdView.BackgroundColor = System.Drawing.Color.White;
            this.SaleOrderGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SaleOrderGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.SaleOrderGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SaleOrderGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SaleOrderGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SaleOrderGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.SaleOrderGrdView.EnableHeadersVisualStyles = false;
            this.SaleOrderGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SaleOrderGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.SaleOrderGrdView.Location = new System.Drawing.Point(15, 133);
            this.SaleOrderGrdView.Name = "SaleOrderGrdView";
            this.SaleOrderGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SaleOrderGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.SaleOrderGrdView.RowHeadersVisible = false;
            this.SaleOrderGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SaleOrderGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SaleOrderGrdView.Size = new System.Drawing.Size(842, 335);
            this.SaleOrderGrdView.TabIndex = 15;
            this.SaleOrderGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SaleOrderGrdView_CellClick);
            this.SaleOrderGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SaleOrderGrdView_CellContentClick);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel1.Controls.Add(this.btnSearch);
            this.metroPanel1.Controls.Add(this.metroLabel7);
            this.metroPanel1.Controls.Add(this.metroLabel6);
            this.metroPanel1.Controls.Add(this.endDate);
            this.metroPanel1.Controls.Add(this.startDate);
            this.metroPanel1.Controls.Add(this.cmbCashier);
            this.metroPanel1.Controls.Add(this.txtSearchDepartmentName);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(1, 50);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(865, 77);
            this.metroPanel1.TabIndex = 13;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(587, 42);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 29);
            this.btnSearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel7.Location = new System.Drawing.Point(310, 42);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(75, 19);
            this.metroLabel7.TabIndex = 24;
            this.metroLabel7.Text = "END DATE:";
            this.metroLabel7.UseCustomBackColor = true;
            this.metroLabel7.UseCustomForeColor = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel6.Location = new System.Drawing.Point(13, 42);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(84, 19);
            this.metroLabel6.TabIndex = 23;
            this.metroLabel6.Text = "START DATE:";
            this.metroLabel6.UseCustomBackColor = true;
            this.metroLabel6.UseCustomForeColor = true;
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(391, 42);
            this.endDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(190, 29);
            this.endDate.TabIndex = 22;
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(110, 42);
            this.startDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(193, 29);
            this.startDate.TabIndex = 21;
            // 
            // cmbCashier
            // 
            this.cmbCashier.FormattingEnabled = true;
            this.cmbCashier.ItemHeight = 23;
            this.cmbCashier.Location = new System.Drawing.Point(310, 7);
            this.cmbCashier.Name = "cmbCashier";
            this.cmbCashier.PromptText = "--- Cashier ---";
            this.cmbCashier.Size = new System.Drawing.Size(271, 29);
            this.cmbCashier.TabIndex = 20;
            this.cmbCashier.Text = "--- Cashier ---";
            this.cmbCashier.UseSelectable = true;
            this.cmbCashier.SelectedIndexChanged += new System.EventHandler(this.cmbCashier_SelectedIndexChanged);
            // 
            // txtSearchDepartmentName
            // 
            // 
            // 
            // 
            this.txtSearchDepartmentName.CustomButton.Image = null;
            this.txtSearchDepartmentName.CustomButton.Location = new System.Drawing.Point(261, 1);
            this.txtSearchDepartmentName.CustomButton.Name = "";
            this.txtSearchDepartmentName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSearchDepartmentName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchDepartmentName.CustomButton.TabIndex = 1;
            this.txtSearchDepartmentName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchDepartmentName.CustomButton.UseSelectable = true;
            this.txtSearchDepartmentName.CustomButton.Visible = false;
            this.txtSearchDepartmentName.DisplayIcon = true;
            this.txtSearchDepartmentName.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtSearchDepartmentName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearchDepartmentName.IconRight = true;
            this.txtSearchDepartmentName.Lines = new string[0];
            this.txtSearchDepartmentName.Location = new System.Drawing.Point(14, 7);
            this.txtSearchDepartmentName.MaxLength = 20;
            this.txtSearchDepartmentName.Name = "txtSearchDepartmentName";
            this.txtSearchDepartmentName.PasswordChar = '\0';
            this.txtSearchDepartmentName.PromptText = "Order Number";
            this.txtSearchDepartmentName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchDepartmentName.SelectedText = "";
            this.txtSearchDepartmentName.SelectionLength = 0;
            this.txtSearchDepartmentName.SelectionStart = 0;
            this.txtSearchDepartmentName.ShortcutsEnabled = true;
            this.txtSearchDepartmentName.Size = new System.Drawing.Size(289, 29);
            this.txtSearchDepartmentName.TabIndex = 18;
            this.txtSearchDepartmentName.UseSelectable = true;
            this.txtSearchDepartmentName.WaterMark = "Order Number";
            this.txtSearchDepartmentName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchDepartmentName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchDepartmentName.TextChanged += new System.EventHandler(this.txtSearchDepartmentName_TextChanged);
            this.txtSearchDepartmentName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchDepartmentName_KeyPress);
            // 
            // FrmSalesOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(866, 480);
            this.Controls.Add(this.SaleOrderGrdView);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSalesOrders";
            this.Text = "FrmSalesOrdercs";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaleOrderGrdView)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        public MetroFramework.Controls.MetroGrid SaleOrderGrdView;
        private MetroFramework.Controls.MetroTextBox txtSearchDepartmentName;
        private MetroFramework.Controls.MetroComboBox cmbCashier;
        private MetroFramework.Controls.MetroDateTime startDate;
        private MetroFramework.Controls.MetroDateTime endDate;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroButton btnSearch;
    }
}