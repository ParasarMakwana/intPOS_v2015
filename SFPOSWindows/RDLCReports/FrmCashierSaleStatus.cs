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
    public partial class FrmCashierSaleStatus : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        CashierSaleService _CashierSaleService = new CashierSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmCashierSaleStatus()
        {
            InitializeComponent();
        }

        private void FrmEmployeeWisePaymentHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CashierSaleStatus(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CashierWiseSaleStatus", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CashierWiseSaleStatus.rdlc");

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
                List<CashierSaleStatusModel> lstCashierSaleStatus = new List<CashierSaleStatusModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstCashierSaleStatus = _CashierSaleService.CashierSaleStatus(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstCashierSaleStatus = _CashierSaleService.CashierSaleStatus(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstCashierSaleStatus = _CashierSaleService.CashierSaleStatus(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstCashierSaleStatus = _CashierSaleService.CashierSaleStatus(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstCashierSaleStatus = _CashierSaleService.CashierSaleStatus(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns.Add("CashAmount", typeof(decimal));
                dt.Columns.Add("CheckAmount", typeof(decimal));
                dt.Columns.Add("CreditCardAmount", typeof(decimal));
                dt.Columns.Add("FoodStampAmount", typeof(decimal));
                dt.Columns.Add("CancelledAmount", typeof(decimal));
                dt.Columns.Add("CheckRefund", typeof(decimal));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("CouponDiscAmt", typeof(decimal));
                dt.Columns.Add("TotalAmt", typeof(decimal));
                dt.Columns.Add("OverwritePriceTotal", typeof(decimal));
                foreach (var item in lstCashierSaleStatus)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmpName"] = item.EmpName;
                    dr["TotalAmount"] = item.TotalAmount;
                    dr["RefundAmount"] = item.RefundAmount;
                    dr["CashAmount"] = item.CashAmount;
                    dr["CheckAmount"] = item.CheckAmount;
                    dr["CreditCardAmount"] = item.CreditCardAmount;
                    dr["FoodStampAmount"] = item.FoodStampAmount;
                    dr["CancelledAmount"] = item.CancelledAmount;
                    dr["CheckRefund"] = item.CheckRefund;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["CouponDiscAmt"] = item.CouponDiscAmt;
                    dr["TotalAmt"] = item.TotalAmt;
                    dr["OverwritePriceTotal"] = item.OverwritePriceTotal;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmEmployeeWisePaymentHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
