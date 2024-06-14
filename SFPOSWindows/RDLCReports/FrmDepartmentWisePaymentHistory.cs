using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
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
using SFPOS.Entities.Reports;
using SFPOS.Entities.MasterDataClasses;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmDepartmentWisePaymentHistory : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber; DepartmentWiseSaleService _DepartmentWiseSaleService = new DepartmentWiseSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmDepartmentWisePaymentHistory()
        {
            InitializeComponent();
        }

        private void FrmDepartmentWisePaymentHistory_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CounterWiseDailySale(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CounterWisePaymentHistory", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CounterWisePaymentHistory.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();

        }
        public DataSet CounterWiseDailySale(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<DepartmentWisePaymentModel> lstDepartmentWisePayment = _DepartmentWiseSaleService.DepartmentWisePayment(FromDate, ToDate);

                switch (FilterVal)
                {
                    case "TODAY":
                        lstDepartmentWisePayment = _DepartmentWiseSaleService.DepartmentWisePayment(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "YESTERDAY":
                        lstDepartmentWisePayment = _DepartmentWiseSaleService.DepartmentWisePayment(DateTime.Today.AddDays(-1).Date, DateTime.Now.Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstDepartmentWisePayment = _DepartmentWiseSaleService.DepartmentWisePayment(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstDepartmentWisePayment = _DepartmentWiseSaleService.DepartmentWisePayment(FromDate, ToDate);
                        break;
                }

                dt.Columns.Add("CounterNo", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("RefundAmount", typeof(decimal));
                dt.Columns.Add("CashAmount", typeof(decimal));
                dt.Columns.Add("CheckAmount", typeof(decimal));
                dt.Columns.Add("CreditCardAmount", typeof(decimal));
                dt.Columns.Add("FoodStampAmount", typeof(decimal));
                dt.Columns.Add("CounterIP", typeof(string));

                foreach (var item in lstDepartmentWisePayment)
                {
                    DataRow dr = dt.NewRow();
                    //dr["CounterNo"] = item.CounterNo;
                    //dr["SalesAmount"] = item.SalesAmount;
                    //dr["RefundAmount"] = item.RefundAmount;
                    //dr["CashAmount"] = item.CashAmount;
                    //dr["CheckAmount"] = item.CheckAmount;
                    //dr["CreditCardAmount"] = item.CreditCardAmount;
                    //dr["FoodStampAmount"] = item.FoodStampAmount;
                    //dr["CounterIP"] = item.CounterIP;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmDepartmentWisePaymentHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
