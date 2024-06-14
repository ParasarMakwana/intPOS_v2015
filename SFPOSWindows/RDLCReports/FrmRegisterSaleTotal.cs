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
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmRegisterSaleTotal : MetroForm
    {
        RegisterSaleService _RegisterSaleService = new RegisterSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        public string FilterVal; public int monthNumber;
        public FrmRegisterSaleTotal()
        {
            InitializeComponent();
        }

        private void FrmCounterWiseSaleHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = RegisterSaleTotal(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("RegisterSaleTotal", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"RegisterSaleTotal.rdlc");
            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString()); 

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet RegisterSaleTotal(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<RegisterSaleTotalModel> lstRegisterSaleTotal = new List<RegisterSaleTotalModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstRegisterSaleTotal = _RegisterSaleService.RegisterSaleTotal(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":                        
                        lstRegisterSaleTotal = _RegisterSaleService.RegisterSaleTotal(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstRegisterSaleTotal = _RegisterSaleService.RegisterSaleTotal(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstRegisterSaleTotal = _RegisterSaleService.RegisterSaleTotal(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstRegisterSaleTotal = _RegisterSaleService.RegisterSaleTotal(FromDate, ToDate);
                        break;
                }

                dt.Columns.Add("CounterIP", typeof(string));
                dt.Columns.Add("CounterNo", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("OverridePriceTotal", typeof(decimal));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                
                foreach (var item in lstRegisterSaleTotal)
                {
                    DataRow dr = dt.NewRow();
                    dr["CounterIP"] = item.CounterIP;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["OverridePriceTotal"] = item.OverridePriceTotal;
                    dr["TotalAmount"] = item.TotalAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCounterWiseSaleHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }

       }
}
