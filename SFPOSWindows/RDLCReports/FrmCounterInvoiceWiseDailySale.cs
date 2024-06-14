using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Entities.MasterDataClasses;
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
using SFPOS.Entities.Reports;
using SFPOS.Common;
using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
namespace SFPOSWindows.RDLCReports
{
    public partial class FrmCounterInvoiceWiseDailySale : MetroForm
    {
        CounterWiseSaleService _CounterWiseSaleService = new CounterWiseSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();

        public FrmCounterInvoiceWiseDailySale()
        {
            InitializeComponent();
        }

        private void FrmCounterInvoiceWiseDailySale_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CounterWiseDailySale();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CounterInvoiceWiseDailySale", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CounterInvoiceWisedailySale.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();
        }
        public DataSet CounterWiseDailySale()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<CounterInvoiceWiseDailySaleModel> lstCounterWiseSaleService = _CounterWiseSaleService.CounterInvoiceWiseDailySale();

                dt.Columns.Add("CounterIP", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                dt.Columns.Add("TotalAmount", typeof(decimal));
                dt.Columns.Add("OrdNo", typeof(string));
                dt.Columns.Add("CancelAmount", typeof(decimal));
                foreach (var item in lstCounterWiseSaleService)
                {
                    DataRow dr = dt.NewRow();
                    dr["CounterIP"] = item.CounterIP;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["TotalAmount"] = item.TotalAmount;
                    dr["OrdNo"] = item.OrdNo;
                    dr["CancelAmount"] = item.CancelAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCounterInvoiceWiseDailySale + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
