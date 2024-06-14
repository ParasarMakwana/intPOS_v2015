using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.CrystalReports
{
    public partial class ReceiptCrystalReport : Form
    {
        public long OrderID = 0;

        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        ReceiptService _ReceiptService = new ReceiptService();
        public ReceiptCrystalReport()
        {
            InitializeComponent();
        }

        private void ReceiptCrystalReport_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ReceiptDetailSP(OrderID);
                ReportDocument crystalReport = new ReportDocument();

                //POS Paper size 72.0*72.0  //Small Receipt
                //crystalReport.Load(Application.StartupPath + "\\CrystalRptReceipt.rpt");

                //POS Paper size 76.2*76.2  //Big Receipt
                crystalReport.Load(Application.StartupPath + "\\CrystalRptReceiptSize7.rpt");
                crystalReport.SetDataSource(dt);

                //ReportView
                crystalReportViewer1.ReportSource = crystalReport;

                //ReportPrint
                crystalReport.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                crystalReport.PrintOptions.PrinterDuplex = PrinterDuplex.Simplex;
                crystalReportViewer1.ShowLastPage();
                int lastPage =  crystalReportViewer1.GetCurrentPageNumber();
                crystalReport.PrintToPrinter(1,false, 1, lastPage);
                crystalReport.Refresh();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPOCrystalReport + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable ReceiptDetailSP(long OrderID)
        {
           
            List<ReciptDetails_ResultModel> Receiptdata = _ReceiptService.GetReceiptDetails(OrderID);

            DataTable dt = new DataTable();

            dt.Columns.Add(ReciptDetails_ResultModelCont.OrderID, typeof(long));            //OrderID
            dt.Columns.Add(ReciptDetails_ResultModelCont.TotalAmount, typeof(string));      //TotalAmount
            dt.Columns.Add(ReciptDetails_ResultModelCont.DiscountAmount, typeof(string));   //DiscountAmount
            dt.Columns.Add(ReciptDetails_ResultModelCont.TaxAmount, typeof(string));        //TaxAmount
            dt.Columns.Add(ReciptDetails_ResultModelCont.GrossAmount, typeof(string));      //GrossAmount
            dt.Columns.Add(ReciptDetails_ResultModelCont.UPCCode, typeof(string));          //UPCCode
            dt.Columns.Add(ReciptDetails_ResultModelCont.ProductName, typeof(string));      //ProductName
            dt.Columns.Add(ReciptDetails_ResultModelCont.Quantity, typeof(string));         //Quantity
            dt.Columns.Add(ReciptDetails_ResultModelCont.SellPrice, typeof(string));        //SellPrice
            dt.Columns.Add(ReciptDetails_ResultModelCont.Discount, typeof(string));         //Discount
            dt.Columns.Add(ReciptDetails_ResultModelCont.finalPrice, typeof(string));       //finalPrice
            dt.Columns.Add(ReciptDetails_ResultModelCont.StoreName, typeof(string));        //StoreName
            dt.Columns.Add(ReciptDetails_ResultModelCont.SMAddress, typeof(string));        //SMAddress
            dt.Columns.Add(ReciptDetails_ResultModelCont.SAddress2, typeof(string));        //SAddress2
            dt.Columns.Add(ReciptDetails_ResultModelCont.SCity, typeof(string));            //SCity
            dt.Columns.Add(ReciptDetails_ResultModelCont.SZipCode, typeof(string));         //SZipCode
            dt.Columns.Add(ReciptDetails_ResultModelCont.SPhone, typeof(string));           //SPhone

            foreach (var item in Receiptdata)
            {
                DataRow dr = dt.NewRow();
                dr[ReciptDetails_ResultModelCont.OrderID] = item.OrderID;
                dr[ReciptDetails_ResultModelCont.TotalAmount] = CommonModelCont.AddDollorSign + item.TotalAmount;
                dr[ReciptDetails_ResultModelCont.DiscountAmount] = CommonModelCont.AddDollorSign + item.DiscountAmount;
                dr[ReciptDetails_ResultModelCont.TaxAmount] = CommonModelCont.AddDollorSign + item.TaxAmount;
                dr[ReciptDetails_ResultModelCont.GrossAmount] = CommonModelCont.AddDollorSign + item.GrossAmount;
                dr[ReciptDetails_ResultModelCont.UPCCode] = item.UPCCode;
                dr[ReciptDetails_ResultModelCont.ProductName] = item.ProductName;
                dr[ReciptDetails_ResultModelCont.Quantity] = item.Quantity;
                dr[ReciptDetails_ResultModelCont.SellPrice] = CommonModelCont.AddDollorSign + item.SellPrice;
                dr[ReciptDetails_ResultModelCont.Discount] = item.Discount + " %";
                dr[ReciptDetails_ResultModelCont.finalPrice] = CommonModelCont.AddDollorSign + item.finalPrice;
                dr[ReciptDetails_ResultModelCont.StoreName] = item.StoreName;
                dr[ReciptDetails_ResultModelCont.SMAddress] = item.SMAddress;
                dr[ReciptDetails_ResultModelCont.SAddress2] = item.SAddress2;
                dr[ReciptDetails_ResultModelCont.SCity] = item.SCity;
                dr[ReciptDetails_ResultModelCont.SZipCode] = item.SZipCode;
                dr[ReciptDetails_ResultModelCont.SPhone] = item.SPhone;

                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
