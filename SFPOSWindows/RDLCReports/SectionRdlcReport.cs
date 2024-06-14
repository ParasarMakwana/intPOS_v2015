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
    public partial class SectionRdlcReport : Form
    {
        public long SectionID = 0;
        public DateTime startdate;
        public DateTime enddate;
        SectionSaleService _SectionSaleService = new SectionSaleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public SectionRdlcReport()
        {
            InitializeComponent();
        }

        private void CategoryRdlcReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = InvoiceDetailSP(SectionID, startdate, enddate);
            reportViewer1.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder, @"SectionReport.rdlc");

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.RefreshReport();
       }

        public DataSet InvoiceDetailSP(long SectionID,DateTime startdate,DateTime enddate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<Section_ResultModel> Sectiondata = _SectionSaleService.GetAllSectionSales(LoginInfo.StoreID, SectionID, startdate, enddate);

                dt.Columns.Add(Section_ResultModelCont.ProductID, typeof(string));              //ProductID
                dt.Columns.Add(Section_ResultModelCont.UPCCode, typeof(string));                //UPCCode
                dt.Columns.Add(Section_ResultModelCont.ProductName, typeof(string));            //ProductName
                dt.Columns.Add(Section_ResultModelCont.Price, typeof(string));                  //Price
                dt.Columns.Add(Section_ResultModelCont.Discount, typeof(string));               //Discount
                dt.Columns.Add(Section_ResultModelCont.NORMAL_PRICE, typeof(string));           //NORMAL_PRICE
                dt.Columns.Add(Section_ResultModelCont.NORMAL_COST, typeof(string));            //NORMAL_COST
                dt.Columns.Add(Section_ResultModelCont.GROSS_PROFIT, typeof(string));           //GROSS_PROFIT
                dt.Columns.Add(Section_ResultModelCont.SectionName, typeof(string));        //SectionName
                foreach (var item in Sectiondata)
                {
                    DataRow dr = dt.NewRow();
                    dr[Section_ResultModelCont.ProductID] = item.ProductID;
                    dr[Section_ResultModelCont.UPCCode] = item.UPCCode;
                    dr[Section_ResultModelCont.ProductName] = item.ProductName;
                    dr[Section_ResultModelCont.Price] = CommonModelCont.AddDollorSign + item.Price;
                    dr[Section_ResultModelCont.Discount] = item.Discount + "%";
                    dr[Section_ResultModelCont.NORMAL_PRICE] = CommonModelCont.AddDollorSign + item.NORMAL_PRICE;
                    dr[Section_ResultModelCont.NORMAL_COST] = CommonModelCont.AddDollorSign + item.NORMAL_COST;
                    dr[Section_ResultModelCont.GROSS_PROFIT] = item.GROSS_PROFIT + "%";
                    dr[Section_ResultModelCont.SectionName] = item.SectionName;
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
