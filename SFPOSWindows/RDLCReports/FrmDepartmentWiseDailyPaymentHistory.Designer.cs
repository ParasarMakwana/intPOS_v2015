namespace SFPOSWindows.RDLCReports
{
    partial class FrmDepartmentWiseDailyPaymentHistory
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(1, 28);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(757, 434);
            this.reportViewer1.TabIndex = 0;
            // 
            // FrmDepartmentWiseDailyPaymentHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 461);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmDepartmentWiseDailyPaymentHistory";
            this.Text = "FrmDepartmentWiseDailyPaymentHistory";
            this.Load += new System.EventHandler(this.FrmDepartmentWiseDailyPaymentHistory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}