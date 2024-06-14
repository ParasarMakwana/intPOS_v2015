namespace SFPOSWindows.Frontend
{
    partial class FrmVendorSales
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
            this.btnOK = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.DepartmentSaleGrdView = new MetroFramework.Controls.MetroGrid();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.endDate = new MetroFramework.Controls.MetroDateTime();
            this.startDate = new MetroFramework.Controls.MetroDateTime();
            ((System.ComponentModel.ISupportInitialize)(this.DepartmentSaleGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(298, 326);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 34);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseSelectable = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton2.Location = new System.Drawing.Point(386, 326);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(82, 34);
            this.metroButton2.TabIndex = 7;
            this.metroButton2.Text = "CANCEL";
            this.metroButton2.UseSelectable = true;
            // 
            // DepartmentSaleGrdView
            // 
            this.DepartmentSaleGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.DepartmentSaleGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DepartmentSaleGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DepartmentSaleGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DepartmentSaleGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DepartmentSaleGrdView.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DepartmentSaleGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DepartmentSaleGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DepartmentSaleGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DepartmentSaleGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DepartmentSaleGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DepartmentSaleGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.DepartmentSaleGrdView.EnableHeadersVisualStyles = false;
            this.DepartmentSaleGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DepartmentSaleGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.DepartmentSaleGrdView.Location = new System.Drawing.Point(4, 113);
            this.DepartmentSaleGrdView.Name = "DepartmentSaleGrdView";
            this.DepartmentSaleGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DepartmentSaleGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DepartmentSaleGrdView.RowHeadersVisible = false;
            this.DepartmentSaleGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DepartmentSaleGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DepartmentSaleGrdView.Size = new System.Drawing.Size(466, 195);
            this.DepartmentSaleGrdView.TabIndex = 16;
            this.DepartmentSaleGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DepartmentSaleGrdView_CellClick);
            // 
            // metroLabel7
            // 
            this.metroLabel7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel7.Location = new System.Drawing.Point(241, 73);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(75, 19);
            this.metroLabel7.TabIndex = 23;
            this.metroLabel7.Text = "END DATE:";
            this.metroLabel7.UseCustomBackColor = true;
            this.metroLabel7.UseCustomForeColor = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel6.Location = new System.Drawing.Point(12, 73);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(84, 19);
            this.metroLabel6.TabIndex = 22;
            this.metroLabel6.Text = "START DATE:";
            this.metroLabel6.UseCustomBackColor = true;
            this.metroLabel6.UseCustomForeColor = true;
            // 
            // endDate
            // 
            this.endDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDate.Location = new System.Drawing.Point(317, 73);
            this.endDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(134, 29);
            this.endDate.TabIndex = 21;
            // 
            // startDate
            // 
            this.startDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDate.Location = new System.Drawing.Point(102, 73);
            this.startDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(133, 29);
            this.startDate.TabIndex = 20;
            // 
            // FrmVendorSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 381);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.DepartmentSaleGrdView);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.btnOK);
            this.Name = "FrmVendorSales";
            this.Text = "Vendor Sale Report";
            ((System.ComponentModel.ISupportInitialize)(this.DepartmentSaleGrdView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton btnOK;
        private MetroFramework.Controls.MetroButton metroButton2;
        public MetroFramework.Controls.MetroGrid DepartmentSaleGrdView;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroDateTime endDate;
        private MetroFramework.Controls.MetroDateTime startDate;
    }
}