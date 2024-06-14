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
    public partial class DepartmentRdlcReport : Form
    {
        public long DepartmentID = 0;
        public DateTime startdate;
        public DateTime enddate;
        DepartmentWiseSaleService _DepartmentWiseSaleService = new DepartmentWiseSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public DepartmentRdlcReport()
        {
            InitializeComponent();
        }

        private void CategoryRdlcReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = InvoiceDetailSP(DepartmentID, startdate, enddate);
            reportViewer1.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder, @"DepartmentReport.rdlc");

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.RefreshReport();
       }

        public DataSet InvoiceDetailSP(long DepartmentID, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<DepartmentSale_ResultModel> Invoicedata = _DepartmentWiseSaleService.GetAllDepartmentSales(LoginInfo.StoreID, DepartmentID,startDate,endDate);

                dt.Columns.Add(DepartmentSale_ResultModelCont.ProductID, typeof(string));
                dt.Columns.Add(DepartmentSale_ResultModelCont.UPCCode, typeof(string));               //UPCCode
                dt.Columns.Add(DepartmentSale_ResultModelCont.ProductName, typeof(string));           //ProductName
                dt.Columns.Add(DepartmentSale_ResultModelCont.Price, typeof(string));                 //Price
                dt.Columns.Add(DepartmentSale_ResultModelCont.Discount, typeof(string));              //Discount
                dt.Columns.Add(DepartmentSale_ResultModelCont.NORMAL_PRICE, typeof(string));          //NORMAL_PRICE
                dt.Columns.Add(DepartmentSale_ResultModelCont.NORMAL_COST, typeof(string));           //NORMAL_COST
                dt.Columns.Add(DepartmentSale_ResultModelCont.GROSS_PROFIT, typeof(string));          //GROSS_PROFIT
                dt.Columns.Add(DepartmentSale_ResultModelCont.DepartmentName, typeof(string));
                foreach (var item in Invoicedata)
                {
                    DataRow dr = dt.NewRow();
                    dr[DepartmentSale_ResultModelCont.ProductID] = item.ProductID;
                    dr[DepartmentSale_ResultModelCont.UPCCode] = item.UPCCode;
                    dr[DepartmentSale_ResultModelCont.ProductName] = item.ProductName;
                    dr[DepartmentSale_ResultModelCont.Price] = CommonModelCont.AddDollorSign + item.Price;
                    dr[DepartmentSale_ResultModelCont.Discount] = item.Discount + "%";
                    dr[DepartmentSale_ResultModelCont.NORMAL_PRICE] = CommonModelCont.AddDollorSign + item.NORMAL_PRICE;
                    dr[DepartmentSale_ResultModelCont.NORMAL_COST] = CommonModelCont.AddDollorSign + item.NORMAL_COST;
                    dr[DepartmentSale_ResultModelCont.GROSS_PROFIT] = item.GROSS_PROFIT + "%";
                    dr[DepartmentSale_ResultModelCont.DepartmentName] = item.DepartmentName;
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
