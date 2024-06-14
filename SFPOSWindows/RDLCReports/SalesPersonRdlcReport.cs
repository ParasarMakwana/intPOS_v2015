using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.RDLCReports
{
    public partial class SalesPersonRdlcReport : Form
    {
        public long EmployeeID = 0;
        public DateTime startdate;
        public DateTime enddate;
        SalePersonService _SalePersonService = new SalePersonService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public SalesPersonRdlcReport()
        {
            InitializeComponent();
        }

        private void SalesPersonRdlcReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = InvoiceDetailSP(EmployeeID, startdate, enddate);
            reportViewer1.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder, @"SalesPersonReport.rdlc");

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.RefreshReport();
        }
        public DataSet InvoiceDetailSP(long VendorID, DateTime startdate, DateTime enddate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<SalesPerson_ResultModel> SalePersondata = _SalePersonService.GetAllSalesPerson(LoginInfo.StoreID, VendorID, startdate, enddate);

                dt.Columns.Add(SalesPerson_ResultModelCont.UPCCode, typeof(string));                //UPCCode
                dt.Columns.Add(SalesPerson_ResultModelCont.ProductName, typeof(string));            //ProductName
                dt.Columns.Add(SalesPerson_ResultModelCont.FirstName, typeof(string));              //FirstName
                dt.Columns.Add(SalesPerson_ResultModelCont.Quantity, typeof(string));               //Quantity
                dt.Columns.Add(SalesPerson_ResultModelCont.Total_Amount, typeof(string));           //Total_Amount
                foreach (var item in SalePersondata)
                {
                    DataRow dr = dt.NewRow();
                    dr[SalesPerson_ResultModelCont.UPCCode] = item.UPCCode;
                    dr[SalesPerson_ResultModelCont.ProductName] = item.ProductName;
                    dr[SalesPerson_ResultModelCont.FirstName] = item.FirstName;
                    dr[SalesPerson_ResultModelCont.Quantity] = item.Quantity;
                    dr[SalesPerson_ResultModelCont.Total_Amount] = item.Total_Amount;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPOCrystalReport + ex.StackTrace, ex.LineNumber());
            }
            return ds;

        }
    }
}
