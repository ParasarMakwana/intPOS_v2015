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
    public partial class VendorRdlcReport : Form
    {
        public long VendorID = 0;
        public DateTime startdate;
        public DateTime enddate;
        VendorSaleService _VendorSaleService = new VendorSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public VendorRdlcReport()
        {
            InitializeComponent();
        }

        public DataSet InvoiceDetailSP(long VendorID, DateTime startdate,DateTime enddate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<VendorSale_ResultModel> VendorSaledata = _VendorSaleService.GetAllVendorSales(LoginInfo.StoreID, VendorID, startdate, enddate);

                dt.Columns.Add(VendorSale_ResultModelCont.ProductID, typeof(string));              //ProductID
                dt.Columns.Add(VendorSale_ResultModelCont.UPCCode, typeof(string));                //UPCCode
                dt.Columns.Add(VendorSale_ResultModelCont.ProductName, typeof(string));            //ProductName
                dt.Columns.Add(VendorSale_ResultModelCont.Price, typeof(string));                  //Price
                dt.Columns.Add(VendorSale_ResultModelCont.Discount, typeof(string));               //Discount
                dt.Columns.Add(VendorSale_ResultModelCont.NORMAL_PRICE, typeof(string));           //NORMAL_PRICE
                dt.Columns.Add(VendorSale_ResultModelCont.NORMAL_COST, typeof(string));            //NORMAL_COST
                dt.Columns.Add(VendorSale_ResultModelCont.GROSS_PROFIT, typeof(string));           //GROSS_PROFIT
                foreach (var item in VendorSaledata)
                {
                    DataRow dr = dt.NewRow();
                    dr[VendorSale_ResultModelCont.ProductID] = item.ProductID;
                    dr[VendorSale_ResultModelCont.UPCCode] = item.UPCCode;
                    dr[VendorSale_ResultModelCont.ProductName] = item.ProductName;
                    dr[VendorSale_ResultModelCont.Price] = CommonModelCont.AddDollorSign + item.Price;
                    dr[VendorSale_ResultModelCont.Discount] = item.Discount + "%";
                    dr[VendorSale_ResultModelCont.NORMAL_PRICE] = CommonModelCont.AddDollorSign + item.NORMAL_PRICE;
                    dr[VendorSale_ResultModelCont.NORMAL_COST] = CommonModelCont.AddDollorSign + item.NORMAL_COST;
                    dr[VendorSale_ResultModelCont.GROSS_PROFIT] = item.GROSS_PROFIT + "%";
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


        private void VendorRdlcReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = InvoiceDetailSP(VendorID, startdate, enddate);
            reportViewer1.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder, @"VendorReport.rdlc");

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.RefreshReport();
        }
    }
}
