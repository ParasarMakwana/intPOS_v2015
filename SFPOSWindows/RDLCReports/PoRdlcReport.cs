
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SFPOSWindows.CrystalReports
{
    public partial class PoRdlcReport : Form
    {
        public long PONumber = 0;
        InvoiceDetailService _InvoiceDetailService = new InvoiceDetailService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public PoRdlcReport()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dt = new DataSet();
            dt = InvoiceDetailSP(PONumber);
            reportViewer1.LocalReport.DataSources.Clear();
            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = Path.Combine(exeFolder, @"Report1.rdlc");

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.RefreshReport();
        }

        public DataSet InvoiceDetailSP(long PONumber)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                List<InvoiceDetails_ResultModel> Invoicedata = _InvoiceDetailService.GetInvoiceDetails(PONumber);
               
                dt.Columns.Add(InvoiceDetails_ResultModelCont.Invoice_Number, typeof(string));   //Invoice_Number
                dt.Columns.Add(InvoiceDetails_ResultModelCont.Date, typeof(DateTime));           //Date
                dt.Columns.Add(InvoiceDetails_ResultModelCont.ShippedBy, typeof(string));        //ShippedBy
                dt.Columns.Add(InvoiceDetails_ResultModelCont.Adjustment, typeof(string));       //Adjustment
                dt.Columns.Add(InvoiceDetails_ResultModelCont.TotalAmount, typeof(string));      //TotalAmount
                dt.Columns.Add(InvoiceDetails_ResultModelCont.PONumber, typeof(string));         //PONumber
                dt.Columns.Add(InvoiceDetails_ResultModelCont.OrderDate, typeof(DateTime));      //OrderDate
                dt.Columns.Add(InvoiceDetails_ResultModelCont.VendorName, typeof(string));       //VendorName
                dt.Columns.Add(InvoiceDetails_ResultModelCont.Address, typeof(string));          //Address
                dt.Columns.Add(InvoiceDetails_ResultModelCont.Address2, typeof(string));         //Address2
                dt.Columns.Add(InvoiceDetails_ResultModelCont.VCity, typeof(string));            //VCity
                dt.Columns.Add(InvoiceDetails_ResultModelCont.ZipCode, typeof(string));          //ZipCode   
                dt.Columns.Add(InvoiceDetails_ResultModelCont.PhoneNo, typeof(string));          //PhoneNo
                dt.Columns.Add(InvoiceDetails_ResultModelCont.ItemCode, typeof(string));         //ItemCode
                dt.Columns.Add(InvoiceDetails_ResultModelCont.Quantity, typeof(string));         //Quantity
                dt.Columns.Add(InvoiceDetails_ResultModelCont.UnitCost, typeof(string));         //UnitCost
                dt.Columns.Add(InvoiceDetails_ResultModelCont.Tax, typeof(string));              //Tax
                dt.Columns.Add(InvoiceDetails_ResultModelCont.TaxAmount, typeof(string));        //TaxAmount
                dt.Columns.Add(InvoiceDetails_ResultModelCont.PurchaseType, typeof(string));     //PurchaseType
                dt.Columns.Add(InvoiceDetails_ResultModelCont.LineAmtExclTax, typeof(string));   //LineAmtExclTax
                dt.Columns.Add(InvoiceDetails_ResultModelCont.LineAmtInclTax, typeof(string));   //LineAmtInclTax
                dt.Columns.Add(InvoiceDetails_ResultModelCont.ProductName, typeof(string));      //ProductName
                dt.Columns.Add(InvoiceDetails_ResultModelCont.UPCCode, typeof(string));          //UPCCode
                dt.Columns.Add(InvoiceDetails_ResultModelCont.UnitMeasureCode, typeof(string));  //UnitMeasureCode
                dt.Columns.Add(InvoiceDetails_ResultModelCont.StoreName, typeof(string));        //StoreName
                dt.Columns.Add(InvoiceDetails_ResultModelCont.SMAddress, typeof(string));        //SMAddress
                dt.Columns.Add(InvoiceDetails_ResultModelCont.SAddress2, typeof(string));        //SAddress2
                dt.Columns.Add(InvoiceDetails_ResultModelCont.SCity, typeof(string));            //SCity
                dt.Columns.Add(InvoiceDetails_ResultModelCont.SZipCode, typeof(string));         //SZipCode
                dt.Columns.Add(InvoiceDetails_ResultModelCont.SPhone, typeof(string));           //SPhone

                foreach (var item in Invoicedata)
                {
                    DataRow dr = dt.NewRow();
                    dr[InvoiceDetails_ResultModelCont.Invoice_Number] = item.Invoice_Number;
                    dr[InvoiceDetails_ResultModelCont.Date] = Convert.ToDateTime(item.Date).Date;
                    dr[InvoiceDetails_ResultModelCont.ShippedBy] = item.ShippedBy;
                    dr[InvoiceDetails_ResultModelCont.Adjustment] = item.Adjustment;
                    dr[InvoiceDetails_ResultModelCont.TotalAmount] = CommonModelCont.AddDollorSign + item.TotalAmount;
                    dr[InvoiceDetails_ResultModelCont.PONumber] = item.PONumber;
                    dr[InvoiceDetails_ResultModelCont.OrderDate] = Convert.ToDateTime(item.OrderDate).Date;
                    dr[InvoiceDetails_ResultModelCont.VendorName] = item.VendorName;
                    dr[InvoiceDetails_ResultModelCont.Address] = item.Address;
                    dr[InvoiceDetails_ResultModelCont.Address2] = item.Address2;
                    dr[InvoiceDetails_ResultModelCont.VCity] = item.VCity;
                    dr[InvoiceDetails_ResultModelCont.ZipCode] = item.ZipCode;
                    dr[InvoiceDetails_ResultModelCont.PhoneNo] = item.PhoneNo;
                    dr[InvoiceDetails_ResultModelCont.ItemCode] = item.ItemCode;
                    dr[InvoiceDetails_ResultModelCont.Quantity] = item.Quantity;
                    dr[InvoiceDetails_ResultModelCont.UnitCost] = CommonModelCont.AddDollorSign + item.UnitCost;
                    dr[InvoiceDetails_ResultModelCont.Tax] = item.Tax + " %";
                    dr[InvoiceDetails_ResultModelCont.TaxAmount] = CommonModelCont.AddDollorSign + item.TaxAmount;
                    dr[InvoiceDetails_ResultModelCont.PurchaseType] = item.PurchaseType;
                    dr[InvoiceDetails_ResultModelCont.LineAmtExclTax] = CommonModelCont.AddDollorSign + item.LineAmtExclTax;
                    dr[InvoiceDetails_ResultModelCont.LineAmtInclTax] = CommonModelCont.AddDollorSign + item.LineAmtInclTax;
                    dr[InvoiceDetails_ResultModelCont.ProductName] = item.ProductName;
                    dr[InvoiceDetails_ResultModelCont.UPCCode] = item.UPCCode;
                    dr[InvoiceDetails_ResultModelCont.UnitMeasureCode] = item.UnitMeasureCode;
                    dr[InvoiceDetails_ResultModelCont.StoreName] = item.StoreName;
                    dr[InvoiceDetails_ResultModelCont.SMAddress] = item.SMAddress;
                    dr[InvoiceDetails_ResultModelCont.SAddress2] = item.SAddress2;
                    dr[InvoiceDetails_ResultModelCont.SCity] = item.SCity;
                    dr[InvoiceDetails_ResultModelCont.SZipCode] = item.SZipCode;
                    dr[InvoiceDetails_ResultModelCont.SPhone] = item.SPhone;
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
               _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPORdlcReport + ex.StackTrace, ex.LineNumber());
            }
            return ds;

        }
    }
}
