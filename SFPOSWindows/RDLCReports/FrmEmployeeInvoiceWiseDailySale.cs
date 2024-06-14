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
    public partial class FrmEmployeeInvoiceWiseDailySale : MetroForm
    {
        EmloyeeSaleService _EmloyeeSaleService = new EmloyeeSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmEmployeeInvoiceWiseDailySale()
        {
            InitializeComponent();
        }

        private void FrmEmployeeInvoiceWiseDailySale_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = EmployeeInvoiceWiseDailySale();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("EmployeeInvoiceWiseDailySale", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"EmployeeInvoiceWiseDailySale.rdlc");

            ReportParameter rp1 = new ReportParameter("StoreName", LoginInfo.StoreName);
            ReportParameter rp2 = new ReportParameter("UserName", LoginInfo.UserName);
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            reportViewer1.RefreshReport();
        }

        public DataSet EmployeeInvoiceWiseDailySale()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<EmployeeInvoiceWiseDailySaleModel> lstEmployeeInvoiceWiseDailySale = _EmloyeeSaleService.EmployeeInvoiceWiseDailySale();

                dt.Columns.Add("EmployeeID", typeof(string));
                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("OrdNo", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                //dt.Columns.Add("CancelAmount", typeof(decimal));
                foreach (var item in lstEmployeeInvoiceWiseDailySale)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmployeeID"] = item.EmployeeID;
                    dr["EmpName"] = item.EmpName;
                    dr["OrdNo"] = item.OrdNo;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    //dr["CancelAmount"] = item.CancelAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployeeInvoiceWiseDailySale + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
