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
    public partial class FrmEmployeeWiseDailySale : MetroForm
    {
        EmloyeeSaleService _EmloyeeSaleService = new EmloyeeSaleService();

        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public FrmEmployeeWiseDailySale()
        {
            InitializeComponent();
        }

        private void FrmEmployeeWiseDailySale_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = EmployeeInvoiceWiseDailySale();
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("EmployeeWiseDailySale", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"EmployeeWiseDailySale.rdlc");

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
                List<EmployeeWiseDailySaleModel> lstEmployeeWiseDailySale = _EmloyeeSaleService.EmployeeWiseDailySale();

                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("SalesAmount", typeof(decimal));
                dt.Columns.Add("TaxAmount", typeof(decimal));
                //dt.Columns.Add("CancelAmount", typeof(decimal));
                foreach (var item in lstEmployeeWiseDailySale)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmpName"] = item.EmpName;
                    dr["SalesAmount"] = item.SalesAmount;
                    dr["TaxAmount"] = item.TaxAmount;
                    //dr["CancelAmount"] = item.CancelAmount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmEmployeeWiseDailySale + ex.StackTrace, ex.LineNumber());
            }
            return ds;
        }
    }
}
