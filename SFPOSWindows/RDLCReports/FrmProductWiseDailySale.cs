using SFPOS.BAL.MasterDataServices;
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
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
namespace SFPOSWindows.RDLCReports
{
    public partial class FrmProductWiseDailySale : MetroForm
    {
        ProductSalesService _ProductSaleService = new ProductSalesService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public FrmProductWiseDailySale()
        {
            InitializeComponent();
        }

        private void FrmProductWiseDailySale_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = ProductWiseDailySale();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("ProductWiseDailySale", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"ProductWiseDailySale.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();
        }

        public DataSet ProductWiseDailySale()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<ProductWiseDailySaleModel> lstProductWiseDailySale = _ProductSaleService.ProductWiseDailySale();
                dt.Columns.Add("ProductID", typeof(string));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                //dt.Columns.Add("CancelAmount", typeof(decimal));

                foreach (var item in lstProductWiseDailySale)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductID"] = item.ProductID;
                    dr["ProductName"] = item.ProductName;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    //dr["CancelAmount"] = item.CancelAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmProductWiseDailySale + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
