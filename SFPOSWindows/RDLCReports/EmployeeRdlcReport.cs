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

namespace SFPOSWindows.CrystalReports
{
    public partial class EmployeeRdlcReport : Form
    {
        EmloyeeSaleService _EmloyeeSaleService = new EmloyeeSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public EmployeeRdlcReport()
        {
            InitializeComponent();
        }

        private void CategoryRdlcReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = InvoiceDetailSP();
            reportViewer1.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder, @"EmployeeReport.rdlc");

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.RefreshReport();
       }

        public DataSet InvoiceDetailSP()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<EmployeeSale_ResultModel> Employeedata = _EmloyeeSaleService.GetAllEmployeeSales();

                dt.Columns.Add(EmployeeSale_ResultModelCont.EmployeeID, typeof(string));              //EmployeeID
                dt.Columns.Add(EmployeeSale_ResultModelCont.UserID, typeof(string));                //UPCCode
                dt.Columns.Add(EmployeeSale_ResultModelCont.FirstName, typeof(string));            //ProductName
                dt.Columns.Add(EmployeeSale_ResultModelCont.LastName, typeof(string));                  //Price
                dt.Columns.Add(EmployeeSale_ResultModelCont.PhoneNo, typeof(string));               //Discount
                dt.Columns.Add(EmployeeSale_ResultModelCont.CreatedDate, typeof(string));           //NORMAL_PRICE
                dt.Columns.Add(EmployeeSale_ResultModelCont.IsActive, typeof(string));            //NORMAL_COST
                dt.Columns.Add(EmployeeSale_ResultModelCont.RoleType, typeof(string));           //GROSS_PROFIT
                foreach (var item in Employeedata)
                {
                    DataRow dr = dt.NewRow();
                    dr[EmployeeSale_ResultModelCont.EmployeeID] = item.EmployeeID;
                    dr[EmployeeSale_ResultModelCont.UserID] = item.UserID;
                    dr[EmployeeSale_ResultModelCont.FirstName] = item.FirstName;
                    dr[EmployeeSale_ResultModelCont.LastName] = item.LastName;
                    dr[EmployeeSale_ResultModelCont.PhoneNo] = item.PhoneNo;
                    dr[EmployeeSale_ResultModelCont.CreatedDate] = item.CreatedDate;
                    dr[EmployeeSale_ResultModelCont.IsActive] =  item.IsActive;
                    dr[EmployeeSale_ResultModelCont.RoleType] = item.RoleType;
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
