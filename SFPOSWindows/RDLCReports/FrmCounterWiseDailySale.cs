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
using SFPOS.BAL.ReportServices;
using SFPOS.Entities.Reports;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Common;
using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;

namespace SFPOSWindows.RDLCReports
{
    public partial class FrmCounterWiseDailySale : MetroForm
    {
        CounterWiseSaleService _CounterWiseSaleService = new CounterWiseSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public FrmCounterWiseDailySale()
        {
            InitializeComponent();
        }

        private void FrmCounterWiseDailySale_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = CounterWiseDailySale();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("CounterWiseDailySale", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"CounterWiseDailySales.rdlc");
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
                List<CounterWiseDailySaleModel> lstCounterWiseSaleService = _CounterWiseSaleService.CounterWiseDailySale();

                dt.Columns.Add("CounterIP", typeof(string));
                //dt.Columns.Add("CounterNo", typeof(string));             
                dt.Columns.Add("SalesAmount", typeof(decimal));           
                dt.Columns.Add("TaxAmount", typeof(decimal));             
                dt.Columns.Add("TotalAmount", typeof(decimal));
                
                foreach (var item in lstCounterWiseSaleService)
                {
                    DataRow dr = dt.NewRow();
                    dr["CounterIP"] = item.CounterIP;
                    //dr["CounterNo"] = item.CounterNo;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["TotalAmount"] = item.TotalAmount;
                     
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCounterWiseDailySale + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
