using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
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
namespace SFPOSWindows.RDLCReports
{
    public partial class FrmCashierSaleTotal : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        CashierSaleService _CashierSaleService = new CashierSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmCashierSaleTotal()
        {
            InitializeComponent();
        }

        private void FrmEmployeeWiseSaleHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CashierSaleTotal(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CashierSaleTotal", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CashierSaleTotal.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet CashierSaleTotal(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<CashierSaleTotalModel> lstCashierSaleTotal = new List<CashierSaleTotalModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstCashierSaleTotal = _CashierSaleService.CashierSaleTotal(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstCashierSaleTotal = _CashierSaleService.CashierSaleTotal(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstCashierSaleTotal = _CashierSaleService.CashierSaleTotal(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstCashierSaleTotal = _CashierSaleService.CashierSaleTotal(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstCashierSaleTotal = _CashierSaleService.CashierSaleTotal(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("TotalSalesAmount", typeof(decimal));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("OverridePriceTotal", typeof(decimal));


                foreach (var item in lstCashierSaleTotal)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmpName"] = item.EmpName;
                    dr["TotalSalesAmount"] = item.TotalSalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["OverridePriceTotal"] = item.OverridePriceTotal;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmEmployeeWiseSaleHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
