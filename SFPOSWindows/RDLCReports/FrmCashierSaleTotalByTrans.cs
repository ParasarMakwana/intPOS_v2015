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
    public partial class FrmCashierSaleTotalByTrans : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        CashierSaleService _CashierSaleService = new CashierSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmCashierSaleTotalByTrans()
        {
            InitializeComponent();
        }

        private void FrmEmployeeInvoiceWiseSaleHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CashierSaleTotalByTrans(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CashierSaleTotalByTrans", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CashierSaleTotalByTrans.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet CashierSaleTotalByTrans(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<CashierSaleTotalByTransModel> lstCashierSaleTotalByTrans = new List<CashierSaleTotalByTransModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstCashierSaleTotalByTrans = _CashierSaleService.CashierSaleTotalByTrans(DateTime.Now.Date, DateTime.Now.Date);
                        break;

                    case "DAILY":
                        lstCashierSaleTotalByTrans = _CashierSaleService.CashierSaleTotalByTrans(FromDate, ToDate);
                        break;

                    case "YESTERDAY":
                        lstCashierSaleTotalByTrans = _CashierSaleService.CashierSaleTotalByTrans(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstCashierSaleTotalByTrans = _CashierSaleService.CashierSaleTotalByTrans(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstCashierSaleTotalByTrans = _CashierSaleService.CashierSaleTotalByTrans(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("EmployeeID", typeof(string));
                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("OrdNo", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("OverridePriceTotal", typeof(decimal));
                dt.Columns.Add("TotalSalesAmount", typeof(decimal));
                dt.Columns.Add("CreatedDate", typeof(string));
                foreach (var item in lstCashierSaleTotalByTrans)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmployeeID"] = item.EmployeeID;
                    dr["EmpName"] = item.EmpName;
                    dr["OrdNo"] = item.OrdNo;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["OverridePriceTotal"] = item.OverridePriceTotal;
                    dr["TotalSalesAmount"] = item.TotalSalesAmount;
                    dr["CreatedDate"] = item.CreatedDate;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmEmployeeInvoiceWiseSaleHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
