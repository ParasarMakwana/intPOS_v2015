using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmTaxReport : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        TaxReportService _TaxReportService = new TaxReportService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmTaxReport()
        {
            InitializeComponent();
        }

        private void FrmTaxReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CashierSaleStatus(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("TaxReport", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"TaxReport.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }
        public DataSet CashierSaleStatus(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<TaxReportModel> lstTaxReportModel = new List<TaxReportModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstTaxReportModel = _TaxReportService.TaxReport(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstTaxReportModel = _TaxReportService.TaxReport(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstTaxReportModel = _TaxReportService.TaxReport(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstTaxReportModel = _TaxReportService.TaxReport(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstTaxReportModel = _TaxReportService.TaxReport(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("DepartmentName", typeof(string));
                dt.Columns.Add("TOTALSALES", typeof(decimal));
                dt.Columns.Add("FOODSTAMPEXEMPTEDSALES", typeof(decimal));
                //dt.Columns.Add("FSEXEMPTEDTAX", typeof(decimal));
                dt.Columns.Add("SALESTAXCOLLECTED", typeof(decimal));
                dt.Columns.Add("TAXABLESALES", typeof(decimal));
                dt.Columns.Add("TAXEXEMPTSALES", typeof(decimal));
                dt.Columns.Add("SUBTOTAL", typeof(decimal));

                foreach (var item in lstTaxReportModel)
                {
                    DataRow dr = dt.NewRow();
                    dr["DepartmentName"] = item.DepartmentName;
                    dr["TOTALSALES"] = item.TOTALSALES;
                    dr["FOODSTAMPEXEMPTEDSALES"] = item.FOODSTAMPEXEMPTEDSALES;
                    //dr["FSEXEMPTEDTAX"] = item.FSEXEMPTEDTAX;
                    dr["SALESTAXCOLLECTED"] = item.SALESTAXCOLLECTED;
                    dr["TAXABLESALES"] = item.TAXABLESALES;
                    dr["TAXEXEMPTSALES"] = item.TAXEXEMPTSALES;
                    dr["SUBTOTAL"] = item.SUBTOTAL;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmTaxReport + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
