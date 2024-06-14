namespace SFPOSWindows.Reports
{
    partial class FrmSalesPerson
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
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.endDate = new MetroFramework.Controls.MetroDateTime();
            this.startDate = new MetroFramework.Controls.MetroDateTime();
            this.EmployeeSaleGrdView = new MetroFramework.Controls.MetroGrid();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.btnOK = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeSaleGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel7
            // 
            this.metroLabel7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroLabel7.Location = new System.Drawing.Point(253, 77);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(75, 19);
            this.metroLabel7.TabIndex = 30;
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
            this.metroLabel6.Location = new System.Drawing.Point(4, 77);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(84, 19);
            this.metroLabel6.TabIndex = 29;
            this.metroLabel6.Text = "START DATE:";
            this.metroLabel6.UseCustomBackColor = true;
            this.metroLabel6.UseCustomForeColor = true;
            // 
            // endDate
            // 
            this.endDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDate.Location = new System.Drawing.Point(334, 77);
            this.endDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(134, 29);
            this.endDate.TabIndex = 28;
            // 
            // startDate
            // 
            this.startDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDate.Location = new System.Drawing.Point(94, 77);
            this.startDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(133, 29);
            this.startDate.TabIndex = 27;
            // 
            // EmployeeSaleGrdView
            // 
            this.EmployeeSaleGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.EmployeeSaleGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.EmployeeSaleGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeSaleGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EmployeeSaleGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.EmployeeSaleGrdView.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.EmployeeSaleGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EmployeeSaleGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.EmployeeSaleGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EmployeeSaleGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.EmployeeSaleGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EmployeeSaleGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.EmployeeSaleGrdView.EnableHeadersVisualStyles = false;
            this.EmployeeSaleGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.EmployeeSaleGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.EmployeeSaleGrdView.Location = new System.Drawing.Point(4, 117);
            this.EmployeeSaleGrdView.Name = "EmployeeSaleGrdView";
            this.EmployeeSaleGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EmployeeSaleGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.EmployeeSaleGrdView.RowHeadersVisible = false;
            this.EmployeeSaleGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.EmployeeSaleGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EmployeeSaleGrdView.Size = new System.Drawing.Size(466, 195);
            this.EmployeeSaleGrdView.TabIndex = 26;
            this.EmployeeSaleGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmployeeSaleGrdView_CellClick);
            // 
            // metroButton2
            // 
            this.metroButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton2.Location = new System.Drawing.Point(386, 330);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(82, 34);
            this.metroButton2.TabIndex = 25;
            this.metroButton2.Text = "CANCEL";
            this.metroButton2.UseSelectable = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(298, 330);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 34);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "OK";
            this.btnOK.UseSelectable = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmSalesPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 381);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.EmployeeSaleGrdView);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.btnOK);
            this.Name = "FrmSalesPerson";
            this.Text = "Sales Person productivity";
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeSaleGrdView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroDateTime endDate;
        private MetroFramework.Controls.MetroDateTime startDate;
        public MetroFramework.Controls.MetroGrid EmployeeSaleGrdView;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton btnOK;
    }
}