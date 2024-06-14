using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Printer;
using SFPOS.Printer.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using System.IO;
using SFPOSWindows.MasterForm;
using System.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing;

namespace SFPOSWindows.Reports
{
    public partial class FrmSalesOrderDetail : MetroForm
    {
        public long OrderID = 0;
        public long EmployeeId = 0;
        private int m_currentPageIndex;
        public static bool IsCancel = false;
        private IList<Stream> m_streams;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmSalesOrderDetail()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                List<OrderDetailmasterModel> objOrderDetailmasterModel = new List<OrderDetailmasterModel>();
                OrderScannerService _OrderScannerService = new OrderScannerService();
                objOrderDetailmasterModel = _OrderScannerService.GetAllSubOrderDetail(OrderID);
                OrderDetailGrdView.DataSource = objOrderDetailmasterModel.Select(o => new
                {

                    ProductName = o.ProductName,
                    UPCCode = o.UPCCode,
                    Quantity = o.Quantity,
                    SalePrice = o.SellPrice,
                    FinalPrice = o.finalPrice
                }).ToList();

                OrderDetailGrdView.Columns["UPCCode"].HeaderText = "UPC Code";
                OrderDetailGrdView.Columns["SalePrice"].HeaderText = "Sale Price";
                OrderDetailGrdView.Columns["FinalPrice"].HeaderText = "Final Price";
                OrderDetailGrdView.Columns["ProductName"].HeaderText = "Product Name";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmSalesOrderDetails" + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PaymentTransMasterModel objPaymentTransMasterModel = new PaymentTransMasterModel();
                PaymentTransService _PaymentTransService = new PaymentTransService();
                DataTable dt = ReceiptDetailSP(OrderID, EmployeeId, false);

                MultiPaymentInfo.lstPaymentTransMasterModel = _PaymentTransService.GetPaymentTrans(OrderID);

                if (Functions.GetBoolean(dt.Rows[0]["IsCancel"].ToString()) == true)
                {
                    //ClsCommon.MsgBox("Information","Can't print the cancelled order.!",false);
                    //return; 
                    IsCancel = true;
                }
                else
                {
                    OrderInfo.PaymentType = Convert.ToInt32(dt.Rows[0]["PaymentMethodID"].ToString());
                    OrderInfo.CashAmt = Functions.GetDecimal(dt.Rows[0]["CashAmount"].ToString());
                    OrderInfo.Change = Functions.GetDecimal(dt.Rows[0]["ChangeAmount"].ToString());
                    OrderInfo.CheckAmt = Functions.GetDecimal(dt.Rows[0]["CheckAmount"].ToString());
                    OrderInfo.CreditAmt = Functions.GetDecimal(dt.Rows[0]["CreditCardAmount"].ToString());
                    OrderInfo.FSTotal = Functions.GetDecimal(dt.Rows[0]["FoodStampAmount"].ToString());
                    if (OrderInfo.FSTotal != 0)
                    {
                        OrderInfo.IsFSClicked = true;
                    }
                    if (Functions.GetDecimal(dt.Rows[0]["TaxableAmount"].ToString()) > 0)
                    {
                        OrderInfo.IsFSVoidtax = false;
                    }
                    else
                    {
                        OrderInfo.IsFSVoidtax = true;
                    }
                }
                if (CheckMyPrinter())
                {
                    Printer printer = new Printer(XMLData.PrinterName, PrinterType.Epson);
                    printer.dt = dt;
                    printer.RePrint = true;
                    printer.IsCancel = IsCancel;
                    //printer.Balance = Functions.GetDecimal(dt.Rows[0]["Balance"].ToString());
                    printer.TaxableAmount = Functions.GetDecimal(dt.Rows[0]["TaxableAmount"].ToString()); ;
                    printer.ReceiptPrint();                    
                    printer.PartialPaperCut();
                    printer.PrintDocument();
                }
                OrderInfo.CashAmt = 0;
                OrderInfo.Change = 0;
                OrderInfo.CheckAmt = 0;
                OrderInfo.CreditAmt = 0;
                OrderInfo.PaymentType = 0;
                OrderInfo.FSTotal = 0;
                OrderInfo.IsFSClicked = false;
                IsCancel = false;
                MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmSalesOrderDetails" + ex.StackTrace, ex.LineNumber());
                MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
            }
        }

        public static bool CheckMyPrinter()
        {
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            string printerName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                printerName = printer["Name"].ToString().ToUpper();
                if (printerName.Equals((XMLData.PrinterName).ToUpper()))
                {
                    if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public DataTable ReceiptDetailSP(long OrderID, long EmployeeId, bool Reprint)
        {
            DataTable dt = new DataTable();
            try
            {
                ReceiptService _ReceiptService = new ReceiptService();
                List<ReciptDetails_ResultModel> Receiptdata = _ReceiptService.GetReceiptDetails(OrderID, EmployeeId, Reprint);
                dt.Columns.Add(ReciptDetails_ResultModelCont.IsCancel, typeof(bool));
                dt.Columns.Add(ReciptDetails_ResultModelCont.OrderID, typeof(long));
                dt.Columns.Add(ReciptDetails_ResultModelCont.TotalAmount, typeof(string));
                //dt.Columns.Add(ReciptDetails_ResultModelCont.DiscountAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.TaxAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.GrossAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.UPCCode, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.ProductName, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.Quantity, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.SellPrice, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.Discount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.finalPrice, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.StoreName, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.SMAddress, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.SAddress2, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.SCity, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.SZipCode, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.SPhone, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.SFax, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.FirstName, typeof(string));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsScale, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsFoodStamp, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.PaymentMethodID, typeof(int));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsTax, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.DiscountApplyed, typeof(bool));
                dt.Columns.Add(ReciptDetails_ResultModelCont.OrdNo, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CashAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CheckAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CreditCardAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.FoodStampAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.RefundAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.ChangeAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.Balance, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.TaxableAmount, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.TaxExempted, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CasePriceApplied, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.GroupPrice, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.GroupQty, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CasePrice, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CaseQty, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CouponCode, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CouponDiscAmt, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.CreatedDate, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.OrderDetailID, typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.FSEligibleAmount,typeof(string));
                dt.Columns.Add(ReciptDetails_ResultModelCont.ManWT, typeof(bool));
                foreach (var item in Receiptdata)
                {
                    DataRow dr = dt.NewRow();
                    dr[ReciptDetails_ResultModelCont.IsCancel] = item.IsCancel;
                    dr[ReciptDetails_ResultModelCont.OrderID] = item.OrderID;
                    dr[ReciptDetails_ResultModelCont.TotalAmount] = item.TotalAmount;
                    //dr[ReciptDetails_ResultModelCont.DiscountAmount] = item.DiscountAmount;
                    dr[ReciptDetails_ResultModelCont.TaxAmount] = item.TaxAmount;
                    dr[ReciptDetails_ResultModelCont.GrossAmount] = item.GrossAmount;
                    dr[ReciptDetails_ResultModelCont.UPCCode] = item.UPCCode;
                    dr[ReciptDetails_ResultModelCont.ProductName] = item.ProductName;
                    dr[ReciptDetails_ResultModelCont.Quantity] = item.Quantity;
                    dr[ReciptDetails_ResultModelCont.SellPrice] = item.SellPrice;
                    dr[ReciptDetails_ResultModelCont.Discount] = item.Discount;
                    dr[ReciptDetails_ResultModelCont.finalPrice] = item.finalPrice;
                    dr[ReciptDetails_ResultModelCont.StoreName] = item.StoreName;
                    dr[ReciptDetails_ResultModelCont.SMAddress] = item.SMAddress;
                    dr[ReciptDetails_ResultModelCont.SAddress2] = item.SAddress2;
                    dr[ReciptDetails_ResultModelCont.SCity] = item.SCity;
                    dr[ReciptDetails_ResultModelCont.SZipCode] = item.SZipCode;
                    dr[ReciptDetails_ResultModelCont.SPhone] = item.SPhone;
                    dr[ReciptDetails_ResultModelCont.SFax] = item.SFax;
                    dr[ReciptDetails_ResultModelCont.FirstName] = item.FirstName;
                    dr[OrderScanner_ResultModelCont.IsScale] = item.IsScale;
                    dr[OrderScanner_ResultModelCont.IsFoodStamp] = item.IsFoodStamp;
                    dr[OrderScanner_ResultModelCont.IsTax] = item.IsTax;
                    dr[OrderScanner_ResultModelCont.DiscountApplyed] = item.DiscountApplyed;
                    dr[OrderScanner_ResultModelCont.PaymentMethodID] = item.PaymentMethodID;
                    dr[ReciptDetails_ResultModelCont.OrdNo] = item.OrdNo;
                    dr[ReciptDetails_ResultModelCont.CashAmount] = item.CashAmount;
                    dr[ReciptDetails_ResultModelCont.CheckAmount] = item.CheckAmount;
                    dr[ReciptDetails_ResultModelCont.CreditCardAmount] = item.CreditCardAmount;
                    dr[ReciptDetails_ResultModelCont.FoodStampAmount] = item.FoodStampAmount;
                    dr[ReciptDetails_ResultModelCont.RefundAmount] = item.RefundAmount;
                    dr[ReciptDetails_ResultModelCont.ChangeAmount] = item.ChangeAmount;
                    dr[ReciptDetails_ResultModelCont.Balance] = item.Balance;
                    dr[ReciptDetails_ResultModelCont.TaxableAmount] = item.TaxableAmount;
                    dr[ReciptDetails_ResultModelCont.TaxExempted] = item.TaxExempted;
                    dr[ReciptDetails_ResultModelCont.CasePriceApplied] = item.CasePriceApplied;
                    dr[ReciptDetails_ResultModelCont.GroupPrice] = item.GroupPrice;
                    dr[ReciptDetails_ResultModelCont.GroupQty] = item.GroupQty;
                    dr[ReciptDetails_ResultModelCont.CasePrice] = item.CasePrice;
                    dr[ReciptDetails_ResultModelCont.CaseQty] = item.CaseQty;
                    dr[ReciptDetails_ResultModelCont.CouponCode] = item.CouponCode;
                    dr[ReciptDetails_ResultModelCont.CouponDiscAmt] = item.CouponDiscAmt;
                    dr[ReciptDetails_ResultModelCont.CreatedDate] = item.CreatedDate;
                    dr[ReciptDetails_ResultModelCont.OrderDetailID] = item.OrderDetailID;
                    dr[ReciptDetails_ResultModelCont.FSEligibleAmount] = item.FSEligibleAmount;
                    dr[ReciptDetails_ResultModelCont.ManWT] = item.ManWT;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmSalesOrderDetails" + ex.StackTrace, ex.LineNumber());
                return dt;
            }
        }

        private void BtnPrintFullSize_Click(object sender, EventArgs e)
        {
            try
            {

                PaymentTransMasterModel objPaymentTransMasterModel = new PaymentTransMasterModel();
                PaymentTransService _PaymentTransService = new PaymentTransService();
                DataTable dt = ReceiptDetailSP(OrderID, EmployeeId, false);
                DataTable cloneDT = ReceiptDetailSP(OrderID, EmployeeId, false);

                MultiPaymentInfo.lstPaymentTransMasterModel = _PaymentTransService.GetPaymentTrans(OrderID);
                FrmScaleSync frm = new FrmScaleSync();
                DataTable dt2 = frm.ToDataTable(MultiPaymentInfo.lstPaymentTransMasterModel);
                this.BringToFront();
                this.reportViewer1.RefreshReport();
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("ReceiptRPTDetails", dt);
                ReportDataSource rds1 = new ReportDataSource("tbl_PaymentTrans", dt2);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.LocalReport.DataSources.Add(rds1);

                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"Receipt_FullPage_Print.rdlc");

                DataTable dtCloned = cloneDT.Clone();
                dtCloned.Columns["Quantity"].DataType = typeof(decimal);
                foreach (DataRow row in cloneDT.Rows)
                {
                    if (row["IsScale"].ToString().ToLower() == "true")
                    {
                        row["Quantity"] = 1;
                        dtCloned.ImportRow(row);
                    }
                    else
                    {
                       dtCloned.ImportRow(row);
                    }
                }
                var sum = dtCloned.Compute("Sum(Quantity)","");

                ReportParameter rp1 = new ReportParameter("StoreDisclaimer", LoginInfo.StoreDisclaimer);
                ReportParameter rp2 = new ReportParameter("TotalQty", sum.ToString());
                ReportParameter rp3 = new ReportParameter("IsFSVoidtax", (Functions.GetDecimal(dt.Rows[0]["TaxableAmount"].ToString()) > 0 ? false : true).ToString());
                ReportParameter rp4 = new ReportParameter("IsCanceled", dt.Rows[0]["IsCancel"].ToString());
                // LoginInfo.StoreDisclaimer
                reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                reportViewer1.RefreshReport();

                if (Functions.GetBoolean(dt.Rows[0]["IsCancel"].ToString()) == true)
                {
                    //ClsCommon.MsgBox("Information","Can't print the cancelled order.!",false);
                    //return; 
                    IsCancel = true;
                }
                else
                {
                    OrderInfo.PaymentType = Convert.ToInt32(dt.Rows[0]["PaymentMethodID"].ToString());
                    OrderInfo.CashAmt = Functions.GetDecimal(dt.Rows[0]["CashAmount"].ToString());
                    OrderInfo.Change = Functions.GetDecimal(dt.Rows[0]["ChangeAmount"].ToString());
                    OrderInfo.CheckAmt = Functions.GetDecimal(dt.Rows[0]["CheckAmount"].ToString());
                    OrderInfo.CreditAmt = Functions.GetDecimal(dt.Rows[0]["CreditCardAmount"].ToString());
                    OrderInfo.FSTotal = Functions.GetDecimal(dt.Rows[0]["FoodStampAmount"].ToString());
                    if (OrderInfo.FSTotal != 0)
                    {
                        OrderInfo.IsFSClicked = true;
                    }
                    if (Functions.GetDecimal(dt.Rows[0]["TaxableAmount"].ToString()) > 0)
                    {
                        OrderInfo.IsFSVoidtax = false;
                    }
                    else
                    {
                        OrderInfo.IsFSVoidtax = true;
                    }
                }
                printDialog1.AllowSomePages = true;
                printDialog1.ShowHelp = true;

                DialogResult result = printDialog1.ShowDialog();
                var pd = new PrintDialog();
                var settings = printDialog1.PrinterSettings;
                XMLData.PrinterName = settings.PrinterName;

                if (result == DialogResult.OK)
                {
                    Export(reportViewer1.LocalReport);
                    Print(XMLData.PrinterName);
                }
                OrderInfo.CashAmt = 0;
                OrderInfo.Change = 0;
                OrderInfo.CheckAmt = 0;
                OrderInfo.CreditAmt = 0;
                OrderInfo.PaymentType = 0;
                OrderInfo.FSTotal = 0;
                OrderInfo.IsFSClicked = false;
                IsCancel = false;
                MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmSalesOrderDetails" + ex.StackTrace, ex.LineNumber());
                MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
            }
        }

        private void FrmSalesOrderDetail_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }


        private void Export(LocalReport report)
        {
            //5.5in, 7in
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.00in</MarginTop>
                <MarginLeft>2.10in</MarginLeft>
                <MarginRight>2.10in</MarginRight>
                <MarginBottom>0.00in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Print(string priterName)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                //printDoc.PrinterSettings.PrinterName = "ZDesigner GK420d";
                printDoc.PrinterSettings.PrinterName = priterName;
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            Rectangle adjustedRect = new Rectangle(
                 ev.PageBounds.Left - (int)0/*(int)ev.PageSettings.HardMarginX*/,
                 ev.PageBounds.Top - (int)0/*(int)ev.PageSettings.HardMarginY*/,
                 ev.PageBounds.Width,
                 ev.PageBounds.Height);

            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            ev.Graphics.DrawImage(pageImage, adjustedRect);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
    }
}
