using MetroFramework.Forms;
using System;

namespace TillStatusReport.RDLCReports
{
    public partial class ReportDateTimeFilter : MetroForm
    {
        public int FormNo = 0;
        public DateTime StartDate;
        public DateTime EndDate;
        public string FilterVal = "";
        public int FilterMonth = 0;
        public ReportDateTimeFilter()
        {
            InitializeComponent();
            dtStartDate.Value = DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //FormNo = 1;
                StartDate = dtStartDate.Value;
                formOpen(FormNo);
                //MessageBox.Show("btnOK_Click");
            }
            catch (Exception ex)
            {

            }
        }

        public void formOpen(int FormNo)
        {
            try
            {
                FrmSummaryReport objFrmTaxReport = new FrmSummaryReport();
                objFrmTaxReport.FormNo = FormNo;
                this.Close();
                objFrmTaxReport.FromDate = StartDate;
                objFrmTaxReport.ToDate = EndDate;
                objFrmTaxReport.FilterVal = FilterVal;
                objFrmTaxReport.monthNumber = FilterMonth;
                objFrmTaxReport.TopMost = true;
                objFrmTaxReport.Show();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in formOpen   :=" + ex.Message.ToString());
            }
        }

        private void dtStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
