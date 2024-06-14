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
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Entities.Reports;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Common;
using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
namespace SFPOSWindows.RDLCReports
{
    public partial class FrmSectionWiseDailySale : MetroForm
    {
        SectionSaleService _SectionSaleService = new SectionSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        public FrmSectionWiseDailySale()
        {
            InitializeComponent();
        }

        private void FrmSectionWiseDailySale_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = SectionWiseDailySale();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("SectionWiseDailySale", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"SectionWiseDailySale.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();
        }

        public DataSet SectionWiseDailySale()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<SectionWiseDailySaleModel> lstSectionWiseDailySale = _SectionSaleService.SectionWiseDailySale();
                dt.Columns.Add("SectionID", typeof(string));
                dt.Columns.Add("DepartmentID", typeof(string));
                dt.Columns.Add("SectionName", typeof(string));
                dt.Columns.Add("DepartmentName", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));

                foreach (var item in lstSectionWiseDailySale)
                {
                    DataRow dr = dt.NewRow();
                    dr["SectionID"] = item.SectionID;
                    dr["DepartmentID"] = item.DepartmentID;
                    dr["SectionName"] = item.SectionName;
                    dr["DepartmentName"] = item.DepartmentName;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = Convert.ToDecimal(item.TaxAmount == null ? 0 : item.TaxAmount).ToString("0.00");
                    
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSectionWiseDailySale + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
