using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmDepartmentWiseSaleHistory : MetroForm
    {
        DepartmentWiseSaleService _DepartmentWiseSaleService = new DepartmentWiseSaleService();
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmDepartmentWiseSaleHistory()
        {
            InitializeComponent();
        }

        private void FrmDepartmentWiseSaleHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = DepartmentWiseSaleHistory(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DepartmentWiseSaleHistory", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"DepartmentWiseSaleHistory.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet DepartmentWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<DepartmentWiseSaleHistoryModel> lstDepartmentWiseSaleHistory = new List<DepartmentWiseSaleHistoryModel>();

                switch (FilterVal)
                {
                    case "TODAY":
                        lstDepartmentWiseSaleHistory = _DepartmentWiseSaleService.DepartmentWiseSaleHistory(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstDepartmentWiseSaleHistory = _DepartmentWiseSaleService.DepartmentWiseSaleHistory(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstDepartmentWiseSaleHistory = _DepartmentWiseSaleService.DepartmentWiseSaleHistory(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstDepartmentWiseSaleHistory = _DepartmentWiseSaleService.DepartmentWiseSaleHistory(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstDepartmentWiseSaleHistory = _DepartmentWiseSaleService.DepartmentWiseSaleHistory(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("DepartmentID", typeof(string));
                dt.Columns.Add("DepartmentName", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));

                foreach (var item in lstDepartmentWiseSaleHistory)
                {
                    DataRow dr = dt.NewRow();
                    dr["DepartmentID"] = item.DepartmentID;
                    dr["DepartmentName"] = item.DepartmentName;
                    dr["SalesAmount"] = item.SalesAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmDepartmentWiseSaleHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
