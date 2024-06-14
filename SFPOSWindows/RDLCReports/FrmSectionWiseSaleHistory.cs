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
    public partial class FrmSectionWiseSaleHistory : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        SectionSaleService _SectionSaleService = new SectionSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmSectionWiseSaleHistory()
        {
            InitializeComponent();
        }

        private void FrmSectionWiseSaleHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = SectionWiseSaleHistory(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("SectionWiseSaleHistory", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"SectionWiseSaleHistory.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet SectionWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<SectionWiseSaleHistoryModel> lstSectionWiseSaleHistory = new List<SectionWiseSaleHistoryModel>();

                switch (FilterVal)
                {
                    case "TODAY":
                        lstSectionWiseSaleHistory = _SectionSaleService.SectionWiseSaleHistory(DateTime.Now, DateTime.Now);
                        break;
                    case "DAILY":
                        lstSectionWiseSaleHistory = _SectionSaleService.SectionWiseSaleHistory(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstSectionWiseSaleHistory = _SectionSaleService.SectionWiseSaleHistory(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstSectionWiseSaleHistory = _SectionSaleService.SectionWiseSaleHistory(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstSectionWiseSaleHistory = _SectionSaleService.SectionWiseSaleHistory(FromDate, ToDate);
                        break;
                }

                dt.Columns.Add("SectionID", typeof(string));
                dt.Columns.Add("DepartmentID", typeof(string));
                dt.Columns.Add("SectionName", typeof(string));
                dt.Columns.Add("DepartmentName", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));

                foreach (var item in lstSectionWiseSaleHistory)
                {
                    DataRow dr = dt.NewRow();
                    dr["SectionID"] = item.SectionID;
                    dr["DepartmentID"] = item.DepartmentID;
                    dr["SectionName"] = item.SectionName;
                    dr["DepartmentName"] = item.DepartmentName;
                    dr["SalesAmount"] = item.SalesAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSectionWiseSaleHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
