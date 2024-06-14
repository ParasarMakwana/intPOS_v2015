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
    public partial class FrmCashierSaleStatusByTrans : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        CashierSaleService _CashierSaleService = new CashierSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmCashierSaleStatusByTrans()
        {
            InitializeComponent();
        }

        private void FrmEmployeeInvoiceWisePaymentHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CashierSaleStatusByTrans(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CashierSaleStatusByTrans", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CashierSaleStatusByTrans.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet CashierSaleStatusByTrans(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<CashierSaleStatusByTransModel> lstCashierSaleStatusByTrans = new List<CashierSaleStatusByTransModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstCashierSaleStatusByTrans = _CashierSaleService.CashierSaleStatusByTrans(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstCashierSaleStatusByTrans = _CashierSaleService.CashierSaleStatusByTrans(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstCashierSaleStatusByTrans = _CashierSaleService.CashierSaleStatusByTrans(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstCashierSaleStatusByTrans = _CashierSaleService.CashierSaleStatusByTrans(firstDayOfMonth.Date, lastDayOfMonth.Date);
                        break;
                    case "YEAR":
                        lstCashierSaleStatusByTrans = _CashierSaleService.CashierSaleStatusByTrans(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("OrdNo", typeof(string));
                dt.Columns.Add("EmployeeID", typeof(string));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("TotalAmt", typeof(decimal));
                dt.Columns.Add("CouponDiscAmt", typeof(decimal));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns.Add("CashAmount", typeof(decimal));
                dt.Columns.Add("CheckAmount", typeof(decimal));
                dt.Columns.Add("CreditCardAmount", typeof(decimal));
                dt.Columns.Add("FoodStampAmount", typeof(decimal));
                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("CancelAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("CheckRefund", typeof(decimal));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("CreatedDate", typeof(string));
                dt.Columns.Add("OverwritePriceTotal", typeof(decimal));
                foreach (var item in lstCashierSaleStatusByTrans)
                {
                    DataRow dr = dt.NewRow();
                    dr["OrdNo"] = item.OrdNo;
                    dr["TotalAmount"] = item.TotalAmount;
                    dr["TotalAmt"] = item.TotalAmt;
                    dr["CouponDiscAmt"] = item.CouponDiscAmt;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["RefundAmount"] = item.RefundAmount;
                    dr["CashAmount"] = item.CashAmount == null ? 0 : item.CashAmount;
                    dr["CheckAmount"] = item.CheckAmount == null ? 0 : item.CheckAmount;
                    dr["CreditCardAmount"] = item.CreditCardAmount == null ? 0 : item.CreditCardAmount;
                    dr["FoodStampAmount"] = item.FoodStampAmount == null ? 0 : item.FoodStampAmount;
                    dr["EmpName"] = item.EmpName;
                    dr["EmployeeID"] = item.EmployeeID;
                    dr["CancelAmount"] = item.CancelAmount;
                    dr["CheckRefund"] = item.CheckRefund;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["CreatedDate"] = item.CreatedDate;
                    dr["OverwritePriceTotal"] = item.OverwritePriceTotal;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmEmployeeInvoiceWisePaymentHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
