namespace SFPOSWindows.MasterForm
{
    partial class FrmProductSalesDiscount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductSalesDiscount));
            this.MetrobtnAdd = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ProductSalesDiscountGrdView = new MetroFramework.Controls.MetroGrid();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.btnImport = new MetroFramework.Controls.MetroButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductSalesDiscountGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // MetrobtnAdd
            // 
            this.MetrobtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MetrobtnAdd.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.MetrobtnAdd.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.MetrobtnAdd.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.MetrobtnAdd.Location = new System.Drawing.Point(672, 31);
            this.MetrobtnAdd.Name = "MetrobtnAdd";
            this.MetrobtnAdd.Size = new System.Drawing.Size(95, 26);
            this.MetrobtnAdd.Style = MetroFramework.MetroColorStyle.Blue;
            this.MetrobtnAdd.TabIndex = 20;
            this.MetrobtnAdd.Text = "ADD";
            this.MetrobtnAdd.Theme = MetroFramework.MetroThemeStyle.Light;
            this.MetrobtnAdd.UseCustomForeColor = true;
            this.MetrobtnAdd.UseSelectable = true;
            this.MetrobtnAdd.Click += new System.EventHandler(this.MetrobtnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ProductSalesDiscountGrdView);
            this.panel1.Location = new System.Drawing.Point(7, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 394);
            this.panel1.TabIndex = 21;
            // 
            // ProductSalesDiscountGrdView
            // 
            this.ProductSalesDiscountGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.ProductSalesDiscountGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductSalesDiscountGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductSalesDiscountGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductSalesDiscountGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ProductSalesDiscountGrdView.BackgroundColor = System.Drawing.Color.White;
            this.ProductSalesDiscountGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProductSalesDiscountGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProductSalesDiscountGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductSalesDiscountGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductSalesDiscountGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductSalesDiscountGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductSalesDiscountGrdView.EnableHeadersVisualStyles = false;
            this.ProductSalesDiscountGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ProductSalesDiscountGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ProductSalesDiscountGrdView.Location = new System.Drawing.Point(4, 3);
            this.ProductSalesDiscountGrdView.Name = "ProductSalesDiscountGrdView";
            this.ProductSalesDiscountGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductSalesDiscountGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ProductSalesDiscountGrdView.RowHeadersVisible = false;
            this.ProductSalesDiscountGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ProductSalesDiscountGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductSalesDiscountGrdView.Size = new System.Drawing.Size(757, 388);
            this.ProductSalesDiscountGrdView.TabIndex = 19;
            this.ProductSalesDiscountGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductSalesDiscountGrdView_CellClick);
            this.ProductSalesDiscountGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductSalesDiscountGrdView_CellContentClick);
            this.ProductSalesDiscountGrdView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductSalesDiscountGrdView_CellDoubleClick);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExport.Location = new System.Drawing.Point(572, 31);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 26);
            this.btnExport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnExport.TabIndex = 28;
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
            this.btnImport.Location = new System.Drawing.Point(472, 31);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 26);
            this.btnImport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnImport.TabIndex = 27;
            this.btnImport.Text = "IMPORT";
            this.btnImport.UseCustomForeColor = true;
            this.btnImport.UseSelectable = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // FrmProductSalesDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 463);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MetrobtnAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProductSalesDiscount";
            this.Text = "Sales Discount";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductSalesDiscountGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton MetrobtnAdd;
        private System.Windows.Forms.Panel panel1;
        public MetroFramework.Controls.MetroGrid ProductSalesDiscountGrdView;
        private MetroFramework.Controls.MetroButton btnExport;
        private MetroFramework.Controls.MetroButton btnImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}