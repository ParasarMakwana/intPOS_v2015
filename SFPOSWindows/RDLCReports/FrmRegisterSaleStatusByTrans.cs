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
    public partial class FrmRegisterSaleStatusByTrans : MetroForm
    {
        RegisterSaleService _RegisterSaleService = new RegisterSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public DateTime FromDate;
        public DateTime ToDate;
        public string FilterVal; public int monthNumber;
        public FrmRegisterSaleStatusByTrans()
        {
            InitializeComponent();
        }

        private void FrmCounterInvoiceWisePaymentHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = RegisterSaleStatusByTrans(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("RegisterSaleStatusByTrans", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"RegisterSaleStatusByTrans.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet RegisterSaleStatusByTrans(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<RegisterSaleStatusByTransModel> lstRegisterSaleStatusByTrans = new List<RegisterSaleStatusByTransModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstRegisterSaleStatusByTrans = _RegisterSaleService.RegisterSaleStatusByTrans(DateTime.Now.Date,DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstRegisterSaleStatusByTrans = _RegisterSaleService.RegisterSaleStatusByTrans(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstRegisterSaleStatusByTrans = _RegisterSaleService.RegisterSaleStatusByTrans(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstRegisterSaleStatusByTrans = _RegisterSaleService.RegisterSaleStatusByTrans(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstRegisterSaleStatusByTrans = _RegisterSaleService.RegisterSaleStatusByTrans(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("CounterIP", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns.Add("CashAmount", typeof(decimal));
                dt.Columns.Add("CheckAmount", typeof(decimal));
                dt.Columns.Add("CreditCardAmount", typeof(decimal));
                dt.Columns.Add("FoodStampAmount", typeof(decimal));
                dt.Columns.Add("OrdNo", typeof(string));
                dt.Columns.Add("CancelAmount", typeof(decimal));
                dt.Columns.Add("CheckRefund", typeof(decimal));
                dt.Columns.Add("CreatedDate", typeof(string));
                dt.Columns.Add("OverwritePriceTotal", typeof(decimal));
                dt.Columns.Add("TotalAmt", typeof(decimal));
                dt.Columns.Add("CouponDiscAmt", typeof(decimal));
                foreach (var item in lstRegisterSaleStatusByTrans)
                {
                    DataRow dr = dt.NewRow();
                    dr["CounterIP"] = item.CounterIP;
                    dr["TotalAmount"] = item.TotalAmount == null ? 0 : item.TotalAmount;
                    dr["SalesAmount"] = item.SalesAmount == null ? 0 : item.SalesAmount;
                    dr["TotalAmt"] = item.TotalAmt == null ? 0 : item.TotalAmt;
                    dr["CouponDiscAmt"] = item.CouponDiscAmt == null ? 0 : item.CouponDiscAmt;
                    dr["TaxAmount"] = item.TaxAmount == null ? 0 : item.TaxAmount;
                    dr["RefundAmount"] = item.RefundAmount == null ? 0 : item.RefundAmount;
                    dr["CashAmount"] = item.CashAmount == null ? 0 : item.CashAmount;
                    dr["CheckAmount"] = item.CheckAmount == null ? 0 : item.CheckAmount;
                    dr["CreditCardAmount"] = item.CreditCardAmount == null ? 0 : item.CreditCardAmount;
                    dr["FoodStampAmount"] = item.FoodStampAmount == null ? 0 : item.FoodStampAmount;
                    dr["OrdNo"] = item.OrdNo;
                    dr["CancelAmount"] = item.CancelAmount == null ? 0 : item.CancelAmount;
                    dr["CheckRefund"] = item.CheckRefund == null ? 0 : item.CheckRefund;
                    dr["CreatedDate"] = item.CreatedDate;
                    dr["OverwritePriceTotal"] = item.OverwritePriceTotal;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmRegisterSaleStatusByTrans + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
