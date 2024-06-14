using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities.MasterDataClasses;
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
using SFPOS.Entities.Reports;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
namespace SFPOSWindows.RDLCReports
{
    public partial class FrmDepartmentWiseSale : MetroForm
    {
        DepartmentWiseSaleService _DepartmentWiseSaleService = new DepartmentWiseSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public FrmDepartmentWiseSale()
        {
            InitializeComponent();
        }

        private void FrmDepartmentWiseSale_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = DepartmentWiseDailySale();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DepartmentWiseDailySale", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"DepartmentWiseDailySale.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();

        }

        public DataSet DepartmentWiseDailySale()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<DepartmentWiseDailySaleModel> lstDepartmentWiseDailySale = _DepartmentWiseSaleService.DepartmentWiseDailySale();
                //switch (FilterVal)
                //{
                //    case "TODAY":
                //        lstCounterWiseSaleService = _CounterWiseSaleService.CounterWiseSaleHistory(Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")), Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")));
                //        break;
                //    case "YESTERDAY":
                //        lstCounterWiseSaleService = _CounterWiseSaleService.CounterWiseSaleHistory(Convert.ToDateTime(DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy")), Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")));
                //        break;
                //    case "MONTH":
                //        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                //        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                //        lstCounterWiseSaleService = _CounterWiseSaleService.CounterWiseSaleHistory(firstDayOfMonth, lastDayOfMonth);
                //        break;
                //    case "YEAR":
                //        lstCounterWiseSaleService = _CounterWiseSaleService.CounterWiseSaleHistory(FromDate, ToDate);
                //        break;
                //}
                dt.Columns.Add("DepartmentID", typeof(string));
                dt.Columns.Add("DepartmentName", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                
                foreach (var item in lstDepartmentWiseDailySale)
                {
                    DataRow dr = dt.NewRow();
                    dr["DepartmentID"] = item.DepartmentID;
                    dr["DepartmentName"] = item.DepartmentName;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmDepartmentWiseSale + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
