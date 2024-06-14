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
    public partial class FrmRegisterSaleTotalByTrans : MetroForm
    {
        RegisterSaleService _RegisterSaleStatusService = new RegisterSaleService();
        public DateTime FromDate;
        public DateTime ToDate;
        public string FilterVal; public int monthNumber;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmRegisterSaleTotalByTrans()
        {
            InitializeComponent();
        }

        private void FrmCounterInvoiceWiseSaleHistory_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = RegisterSaleTotalByTrans(FromDate, ToDate);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("RegisterSaleTotalByTrans", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"RegisterSaleTotalByTrans.rdlc");
            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
            ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            reportViewer1.RefreshReport();
        }

        public DataSet RegisterSaleTotalByTrans(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<RegisterSaleTotalByTransModel> lstRegisterSaleTotalByTransModel = new List<RegisterSaleTotalByTransModel>();
                switch (FilterVal)
                {
                    case "TODAY":
                        lstRegisterSaleTotalByTransModel = _RegisterSaleStatusService.RegisterSaleTotalByTrans(DateTime.Now.Date, DateTime.Now.Date);
                        break;
                    case "DAILY":
                        lstRegisterSaleTotalByTransModel = _RegisterSaleStatusService.RegisterSaleTotalByTrans(FromDate, ToDate);
                        break;
                    case "YESTERDAY":
                        lstRegisterSaleTotalByTransModel = _RegisterSaleStatusService.RegisterSaleTotalByTrans(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                        break;
                    case "MONTH":
                        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        lstRegisterSaleTotalByTransModel = _RegisterSaleStatusService.RegisterSaleTotalByTrans(firstDayOfMonth, lastDayOfMonth);
                        break;
                    case "YEAR":
                        lstRegisterSaleTotalByTransModel = _RegisterSaleStatusService.RegisterSaleTotalByTrans(FromDate, ToDate);
                        break;
                }
                dt.Columns.Add("CounterIP", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("OrdNo", typeof(string));
                dt.Columns.Add("CancelAmount", typeof(decimal));
                dt.Columns.Add("OverridePriceTotal", typeof(decimal));
                dt.Columns.Add("CreatedDate", typeof(string));
                foreach (var item in lstRegisterSaleTotalByTransModel)
                {
                    DataRow dr = dt.NewRow();
                    dr["CounterIP"] = item.CounterIP;
                    dr["SalesAmount"] = item.SalesAmount == null ? 0 : item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount == null ? 0 : item.TaxAmount;
                    dr["TotalAmount"] = item.TotalAmount == null ? 0 : item.TotalAmount;
                    dr["OrdNo"] = item.OrdNo;
                    dr["CancelAmount"] = item.CancelAmount;
                    dr["OverridePriceTotal"] = item.OverridePriceTotal;
                    dr["CreatedDate"] = item.CreatedDate;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCounterInvoiceWiseSaleHistory + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
