using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
//using TillStatusReport.Models;

namespace TillStatusReport.RDLCReports
{
    public partial class FrmSummaryReport : MetroForm
    {
        public DateTime FromDate = DateTime.UtcNow;
        public DateTime ToDate = DateTime.UtcNow;
        TxtReportServices _TxtReportServices = new TxtReportServices();
        public string FilterVal; public int monthNumber;
        public int FormNo = 0;

        public FrmSummaryReport()
        {
            InitializeComponent();
        }

        private void FrmTaxReport_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.RefreshReport();
                DataSet dt = new DataSet();
                dt = TillStatusEmployeeReport(FromDate, ToDate);
                //MessageBox.Show("dt   :=" + dt.Tables[0].Rows.Count);
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("SummaryReport", dt.Tables[0]);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"TillReport.rdlc");
                switch (FormNo)
                {
                    case 2:
                        reportPath = Path.Combine(exeFolder + "\\Reports\\", @"TillDailyManagementReport.rdlc");
                        break;
                    case 3:
                        reportPath = Path.Combine(exeFolder + "\\Reports\\", @"TillDepositVerifierReport.rdlc");
                        break;
                }

                ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
                ReportParameter rp3 = new ReportParameter("ToDate", ToDate.ToShortDateString());
                ReportParameter rp4 = new ReportParameter("FromDate", FromDate.ToShortDateString());

                reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2, rp3, rp4 });
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in FrmTaxReport_Load   :=" + ex.Message.ToString());

            }

        }
        public DataSet TillStatusEmployeeReport(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {

                TillReportModel _TillReportModel = new TillReportModel();
                ReportsReq _ReportsReq = new ReportsReq();
                _ReportsReq.Date = FromDate;
                //List<TillReportModel> lst = ClsCommon.getdata(_TillReportModel, "SP_TillStatusEmployeeReports", _ReportsReq);
                List<TillReportModel> lst = _TxtReportServices.TillStatusEmployeeReport(FromDate);

                //List<TaxReportModel> lstTaxReportModel = new List<TaxReportModel>();
                //switch (FilterVal)
                //{
                //    case "TODAY":
                //        lstTaxReportModel = _TaxReportService.TaxReport(DateTime.Now.Date, DateTime.Now.Date);
                //        break;
                //    case "DAILY":
                //        lstTaxReportModel = _TaxReportService.TaxReport(FromDate, ToDate);
                //        break;
                //    case "YESTERDAY":
                //        lstTaxReportModel = _TaxReportService.TaxReport(DateTime.Today.AddDays(-1).Date, DateTime.Today.AddDays(-1).Date);
                //        break;
                //    case "MONTH":
                //        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, monthNumber, 1);
                //        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                //        lstTaxReportModel = _TaxReportService.TaxReport(firstDayOfMonth, lastDayOfMonth);
                //        break;
                //    case "YEAR":
                //        lstTaxReportModel = _TaxReportService.TaxReport(FromDate, ToDate);
                //        break;
                //}
                dt.Columns.Add("CashierName", typeof(string));
                dt.Columns.Add("Coin", typeof(decimal));
                dt.Columns.Add("Cash", typeof(decimal));
                dt.Columns.Add("CreditCard", typeof(decimal));
                dt.Columns.Add("Checks", typeof(decimal));
                dt.Columns.Add("CashPayout", typeof(decimal));
                dt.Columns.Add("SortBy", typeof(decimal));
                dt.Columns.Add("OverBy", typeof(decimal));
                dt.Columns.Add("TotalSortOver", typeof(decimal));
                dt.Columns.Add("Verifier", typeof(string));
                dt.Columns.Add("Lotto", typeof(decimal));
                dt.Columns.Add("SelfService", typeof(decimal));
                dt.Columns.Add("Scrathers", typeof(decimal));
                if (lst != null && lst.Count > 0)
                {
                    foreach (var item in lst)
                    {
                        DataRow dr = dt.NewRow();
                        dr["CashierName"] = item.CashierName;
                        dr["Coin"] = item.Coin;
                        dr["Cash"] = item.Cash;
                        dr["CreditCard"] = item.CreditCard;
                        dr["Checks"] = item.Checks;
                        dr["CashPayout"] = item.CashPayout;
                        dr["SortBy"] = item.SortBy;
                        dr["OverBy"] = item.OverBy;
                        dr["TotalSortOver"] = item.TotalSortOver;
                        dr["Verifier"] = item.Verifier;
                        dr["Lotto"] = item.Lotto;
                        dr["SelfService"] = item.SelfService;
                        dr["Scrathers"] = item.Scrathers;
                        dt.Rows.Add(dr);
                    }
                }

                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
