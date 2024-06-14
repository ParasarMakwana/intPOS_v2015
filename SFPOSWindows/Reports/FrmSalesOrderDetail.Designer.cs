namespace SFPOSWindows.Reports
{
    partial class FrmSalesOrderDetail
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
            this.OrderDetailGrdView = new MetroFramework.Controls.MetroGrid();
            this.btnPrint = new MetroFramework.Controls.MetroButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.lblDateTime = new MetroFramework.Controls.MetroLabel();
            this.lblCashier = new MetroFramework.Controls.MetroLabel();
            this.BtnPrintFullSize = new MetroFramework.Controls.MetroButton();
            this.lblTransNo = new MetroFramework.Controls.MetroLabel();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDetailGrdView)).BeginInit();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OrderDetailGrdView
            // 
            this.OrderDetailGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.OrderDetailGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.OrderDetailGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderDetailGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.OrderDetailGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.OrderDetailGrdView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.OrderDetailGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OrderDetailGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.OrderDetailGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderDetailGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.OrderDetailGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.OrderDetailGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.OrderDetailGrdView.EnableHeadersVisualStyles = false;
            this.OrderDetailGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.OrderDetailGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.OrderDetailGrdView.Location = new System.Drawing.Point(5, 146);
            this.OrderDetailGrdView.Name = "OrderDetailGrdView";
            this.OrderDetailGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderDetailGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.OrderDetailGrdView.RowHeadersVisible = false;
            this.OrderDetailGrdView.RowHeadersWidth = 50;
            this.OrderDetailGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.OrderDetailGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrderDetailGrdView.Size = new System.Drawing.Size(788, 334);
            this.OrderDetailGrdView.TabIndex = 18;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnPrint.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnPrint.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPrint.Location = new System.Drawing.Point(683, 40);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(87, 27);
            this.btnPrint.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "REPRINT";
            this.btnPrint.UseCustomForeColor = true;
            this.btnPrint.UseSelectable = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.White;
            this.metroPanel1.Controls.Add(this.lblDateTime);
            this.metroPanel1.Controls.Add(this.lblCashier);
            this.metroPanel1.Controls.Add(this.BtnPrintFullSize);
            this.metroPanel1.Controls.Add(this.btnPrint);
            this.metroPanel1.Controls.Add(this.lblTransNo);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(5, 63);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(788, 77);
            this.metroPanel1.TabIndex = 20;
            this.metroPanel1.UseCustomBackColor = true;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblDateTime.Location = new System.Drawing.Point(303, 12);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(89, 19);
            this.lblDateTime.TabIndex = 20;
            this.lblDateTime.Text = "DATE-TIME: ";
            // 
            // lblCashier
            // 
            this.lblCashier.AutoSize = true;
            this.lblCashier.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblCashier.Location = new System.Drawing.Point(18, 48);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(75, 19);
            this.lblCashier.TabIndex = 3;
            this.lblCashier.Text = "CASHIER: ";
            // 
            // BtnPrintFullSize
            // 
            this.BtnPrintFullSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrintFullSize.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BtnPrintFullSize.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.BtnPrintFullSize.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BtnPrintFullSize.Location = new System.Drawing.Point(580, 40);
            this.BtnPrintFullSize.Name = "BtnPrintFullSize";
            this.BtnPrintFullSize.Size = new System.Drawing.Size(87, 27);
            this.BtnPrintFullSize.Style = MetroFramework.MetroColorStyle.Blue;
            this.BtnPrintFullSize.TabIndex = 19;
            this.BtnPrintFullSize.Text = "PRINT";
            this.BtnPrintFullSize.UseCustomForeColor = true;
            this.BtnPrintFullSize.UseSelectable = true;
            this.BtnPrintFullSize.Click += new System.EventHandler(this.BtnPrintFullSize_Click);
            // 
            // lblTransNo
            // 
            this.lblTransNo.AutoSize = true;
            this.lblTransNo.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblTransNo.Location = new System.Drawing.Point(18, 12);
            this.lblTransNo.Name = "lblTransNo";
            this.lblTransNo.Size = new System.Drawing.Size(142, 19);
            this.lblTransNo.TabIndex = 2;
            this.lblTransNo.Text = "TRANSACTION NO: ";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(23, 234);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 21;
            // 
            // FrmSalesOrderDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 485);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.OrderDetailGrdView);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmSalesOrderDetail";
            this.Text = "Sales Order Detail";
            this.Load += new System.EventHandler(this.FrmSalesOrderDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OrderDetailGrdView)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MetroFramework.Controls.MetroGrid OrderDetailGrdView;
        private MetroFramework.Controls.MetroButton btnPrint;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        public MetroFramework.Controls.MetroLabel lblTransNo;
        public MetroFramework.Controls.MetroLabel lblCashier;
        public MetroFramework.Controls.MetroLabel lblDateTime;
        private MetroFramework.Controls.MetroButton BtnPrintFullSize;
        private System.Windows.Forms.PrintDialog printDialog1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}