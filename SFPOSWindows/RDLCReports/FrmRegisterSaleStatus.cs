using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
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
using SFPOS.Common;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmRegisterSaleStatus : MetroForm
    {
        RegisterSaleService _RegisterSaleService = new RegisterSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public DateTime FromDate;
        public DateTime ToDate;
        public string FilterVal; public int monthNumber;
        public FrmRegisterSaleStatus()
        {
            InitializeComponent();
        }

        private void FrmCounterWisePaymentHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = RegisterSaleStatus(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("RegisterSaleStatus", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"RegisterSaleStatus.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());
            reportViewer1.LocalReport.ReportPath = reportPath;

            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet RegisterSaleStatus(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<RegisterSaleStatusModel> lstRegisterSaleStatus = new List<RegisterSaleStatusModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstRegisterSaleStatus = _RegisterSaleService.RegisterSaleStatus(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstRegisterSaleStatus = _RegisterSaleService.RegisterSaleStatus(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstRegisterSaleStatus = _RegisterSaleService.RegisterSaleStatus(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstRegisterSaleStatus = _RegisterSaleService.RegisterSaleStatus(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstRegisterSaleStatus = _RegisterSaleService.RegisterSaleStatus(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns.Add("CashAmount", typeof(decimal));
                dt.Columns.Add("CheckAmount", typeof(decimal));
                dt.Columns.Add("CreditCardAmount", typeof(decimal));
                dt.Columns.Add("FoodStampAmount", typeof(decimal));
                dt.Columns.Add("CouponDiscountAmount", typeof(decimal));
                dt.Columns.Add("CounterIP", typeof(string));
                dt.Columns.Add("CheckRefund", typeof(decimal));
                dt.Columns.Add("CancelAmount", typeof(decimal));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("OverwritePriceTotal", typeof(decimal));
                dt.Columns.Add("TotalAmt", typeof(decimal));

                foreach (var item in lstRegisterSaleStatus)
                {
                    DataRow dr = dt.NewRow();
                    dr["TotalAmount"] = item.TotalAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["RefundAmount"] = item.RefundAmount;
                    dr["CashAmount"] = item.CashAmount;
                    dr["CheckAmount"] = item.CheckAmount;
                    dr["CreditCardAmount"] = item.CreditCardAmount;
                    dr["FoodStampAmount"] = item.FoodStampAmount;
                    dr["CouponDiscountAmount"] = item.CouponDiscountAmount;
                    dr["CounterIP"] = item.CounterIP;
                    dr["CheckRefund"] = item.CheckRefund;
                    dr["CancelAmount"] = item.CancelAmount;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["OverwritePriceTotal"] = item.OverwritePriceTotal;
                    dr["TotalAmt"] = item.TotalAmt;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCounterWisePaymentHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
