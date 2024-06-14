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
    public partial class FrmProductWiseSaleHistory : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        ProductSalesService _ProductSaleService = new ProductSalesService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmProductWiseSaleHistory()
        {
            InitializeComponent();
        }

        private void FrmProductWiseSaleHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = ProductWiseSaleHistory(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("ProductWiseSaleHistory", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"ProductWiseSaleHistory.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet ProductWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<ProductWiseSaleHistoryModel> lstProductWiseSaleHistory = new List<ProductWiseSaleHistoryModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstProductWiseSaleHistory = _ProductSaleService.ProductWiseSaleHistory(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstProductWiseSaleHistory = _ProductSaleService.ProductWiseSaleHistory(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstProductWiseSaleHistory = _ProductSaleService.ProductWiseSaleHistory(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstProductWiseSaleHistory = _ProductSaleService.ProductWiseSaleHistory(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstProductWiseSaleHistory = _ProductSaleService.ProductWiseSaleHistory(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("ProductID", typeof(string));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                //dt.Columns.Add("CancelAmount", typeof(decimal));

                foreach (var item in lstProductWiseSaleHistory)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductID"] = item.ProductID;
                    dr["ProductName"] = item.ProductName;
                    dr["SalesAmount"] = item.SalesAmount;
                    //dr["CancelAmount"] = item.CancelAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmProductWiseSaleHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
