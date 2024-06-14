namespace SFPOSWindows.MasterForm
{
    partial class FrmCoupon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCoupon));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.txtSearchCouponName = new MetroFramework.Controls.MetroTextBox();
            this.metroBtnShowAll = new MetroFramework.Controls.MetroButton();
            this.MetrobtnAdd = new MetroFramework.Controls.MetroButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CouponGrdView = new MetroFramework.Controls.MetroGrid();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CouponGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(-2, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 44);
            this.panel1.TabIndex = 14;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(104, 32);
            this.lblSubHeading.TabIndex = 0;
            this.lblSubHeading.Text = "Coupons";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.btnExport);
            this.pnlSearch.Controls.Add(this.txtSearchCouponName);
            this.pnlSearch.Controls.Add(this.metroBtnShowAll);
            this.pnlSearch.Controls.Add(this.MetrobtnAdd);
            this.pnlSearch.Location = new System.Drawing.Point(-2, 62);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(720, 47);
            this.pnlSearch.TabIndex = 15;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExport.Location = new System.Drawing.Point(516, 9);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 27);
            this.btnExport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "EXPORT";
            this.btnExport.UseCustomForeColor = true;
            this.btnExport.UseSelectable = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtSearchCouponName
            // 
            // 
            // 
            // 
            this.txtSearchCouponName.CustomButton.Image = null;
            this.txtSearchCouponName.CustomButton.Location = new System.Drawing.Point(257, 2);
            this.txtSearchCouponName.CustomButton.Name = "";
            this.txtSearchCouponName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchCouponName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchCouponName.CustomButton.TabIndex = 1;
            this.txtSearchCouponName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchCouponName.CustomButton.UseSelectable = true;
            this.txtSearchCouponName.CustomButton.Visible = false;
            this.txtSearchCouponName.DisplayIcon = true;
            this.txtSearchCouponName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearchCouponName.IconRight = true;
            this.txtSearchCouponName.Lines = new string[0];
            this.txtSearchCouponName.Location = new System.Drawing.Point(12, 9);
            this.txtSearchCouponName.MaxLength = 20;
            this.txtSearchCouponName.Name = "txtSearchCouponName";
            this.txtSearchCouponName.PasswordChar = '\0';
            //this.txtSearchCouponName.PromptText = "Name";
            this.txtSearchCouponName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchCouponName.SelectedText = "";
            this.txtSearchCouponName.SelectionLength = 0;
            this.txtSearchCouponName.SelectionStart = 0;
            this.txtSearchCouponName.ShortcutsEnabled = true;
            this.txtSearchCouponName.Size = new System.Drawing.Size(281, 26);
            this.txtSearchCouponName.TabIndex = 8;
            this.txtSearchCouponName.UseSelectable = true;
            this.txtSearchCouponName.WaterMark = "Name";
            this.txtSearchCouponName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchCouponName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchCouponName.TextChanged += new System.EventHandler(this.txtSearchCouponName_TextChanged);
            // 
            // metroBtnShowAll
            // 
            this.metroBtnShowAll.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnShowAll.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnShowAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnShowAll.Location = new System.Drawing.Point(301, 9);
            this.metroBtnShowAll.Name = "metroBtnShowAll";
            this.metroBtnShowAll.Size = new System.Drawing.Size(95, 26);
            this.metroBtnShowAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnShowAll.TabIndex = 7;
            this.metroBtnShowAll.Text = "SHOW ALL";
            this.metroBtnShowAll.UseCustomForeColor = true;
            this.metroBtnShowAll.UseSelectable = true;
            this.metroBtnShowAll.Click += new System.EventHandler(this.metroBtnShowAll_Click);
            // 
            // MetrobtnAdd
            // 
            this.MetrobtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MetrobtnAdd.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.MetrobtnAdd.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.MetrobtnAdd.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MetrobtnAdd.Location = new System.Drawing.Point(616, 9);
            this.MetrobtnAdd.Name = "MetrobtnAdd";
            this.MetrobtnAdd.Size = new System.Drawing.Size(94, 27);
            this.MetrobtnAdd.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnAdd.TabIndex = 6;
            this.MetrobtnAdd.Text = "ADD";
            this.MetrobtnAdd.UseCustomForeColor = true;
            this.MetrobtnAdd.UseSelectable = true;
            this.MetrobtnAdd.Click += new System.EventHandler(this.MetrobtnAdd_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.CouponGrdView);
            this.panel2.Location = new System.Drawing.Point(10, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(698, 259);
            this.panel2.TabIndex = 16;
            // 
            // CouponGrdView
            // 
            this.CouponGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.CouponGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CouponGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CouponGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CouponGrdView.BackgroundColor = System.Drawing.Color.White;
            this.CouponGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CouponGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.CouponGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CouponGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CouponGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CouponGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.CouponGrdView.EnableHeadersVisualStyles = false;
            this.CouponGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.CouponGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.CouponGrdView.Location = new System.Drawing.Point(3, 3);
            this.CouponGrdView.Name = "CouponGrdView";
            this.CouponGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CouponGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.CouponGrdView.RowHeadersVisible = false;
            this.CouponGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CouponGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CouponGrdView.Size = new System.Drawing.Size(687, 253);
            this.CouponGrdView.TabIndex = 12;
            this.CouponGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CouponGrdView_CellClick);
            this.CouponGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CouponGrdView_CellContentClick);
            this.CouponGrdView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CouponGrdView_CellDoubleClick);
            // 
            // FrmCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(717, 377);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCoupon";
            this.Text = "FemCoupon";
            this.Load += new System.EventHandler(this.FrmCoupon_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CouponGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel pnlSearch;
        private MetroFramework.Controls.MetroButton btnExport;
        private MetroFramework.Controls.MetroTextBox txtSearchCouponName;
        private MetroFramework.Controls.MetroButton metroBtnShowAll;
        private MetroFramework.Controls.MetroButton MetrobtnAdd;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel2;
        public MetroFramework.Controls.MetroGrid CouponGrdView;
    }
}