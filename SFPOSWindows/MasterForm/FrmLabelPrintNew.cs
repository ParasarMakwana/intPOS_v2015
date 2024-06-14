using MetroFramework.Forms;
using Microsoft.PointOfService;
using Microsoft.Reporting.WinForms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SFPOSWindows.MasterForm
{
    public partial class FrmLabelPrintNew : MetroForm
    {
        #region Properties
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        string OrignalUPCCode = "";
        #endregion

        #region Events
        private void btnBarcode_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtUPCCode.Text))
                {
                    SearchProduct(txtUPCCode.Text);
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please add UpcCode.!", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());

            }
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>2.25in</PageWidth>
                <PageHeight>1in</PageHeight>
                <MarginTop>0.00in</MarginTop>
                <MarginLeft>0.00in</MarginLeft>
                <MarginRight>0.00in</MarginRight>
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

        private void Print()
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
                //ZDesigner GK420d
                if (XMLData.Disclaimer != "")
                {
                    printDoc.PrinterSettings.PrinterName = XMLData.Disclaimer;
                    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                    m_currentPageIndex = 0;
                    printDoc.Print();
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please set label printer name from setting screen", false);
                }
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

        private void txtUPCCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    SearchProduct(txtUPCCode.Text);
                }
                catch (Exception ex)
                {
                    _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
                }
            }
        }

        private void FrmLabelPrintNew_Load(object sender, EventArgs e)
        {
            DeviceAdd();
        }

        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            try
            {
                ASCIIEncoding myEncoding = new ASCIIEncoding();
                string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
                if (UPCCode.Length > 1)
                {
                    UPCCode = UPCCode.Substring(0, UPCCode.Length - 1);
                }
                txtUPCCode.Text = UPCCode;
                SearchProduct(txtUPCCode.Text);
                if (myScanner.DataEventEnabled == false)
                {
                    myScanner.DataEventEnabled = true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
            }
        }
        private void FrmLabelPrintNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeviceRemove();
        }


        #endregion

        #region Functions
        public FrmLabelPrintNew()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
        }

        public void DeviceRemove()
        {
            try
            {
                if (XMLData.ScannerInUsed)
                {
                    if (myScanner.DataEventEnabled == true)
                    {
                        myScanner.DataEventEnabled = false;
                        myScanner.DeviceEnabled = false;
                        myScanner.Release();
                        myScanner.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void DeviceAdd()
        {
            try
            {
                if (XMLData.ScannerInUsed)
                {
                    var deviceCollection = myPosExplorer.GetDevices(DeviceType.Scanner);
                    foreach (DeviceInfo deviceInfo in deviceCollection)
                    {
                        if (deviceInfo.ServiceObjectName == XMLData.Scanner)
                        {
                            myScanner = (Scanner)myPosExplorer.CreateInstance(deviceInfo);
                            if (myScanner.DeviceEnabled == false)
                            {
                                myScanner.Open();
                                myScanner.Claim(1000);
                                myScanner.DataEvent += myScanner_DataEvent;
                                myScanner.DeviceEnabled = true;
                                myScanner.DataEventEnabled = true;
                                myScanner.DecodeData = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable GetLableData(string UPCCode)
        {
            DataTable dt = new DataTable();
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

                var i = _db.SP_GetDisplayLableData(UPCCode);

                dt.Columns.Add("ProductID", typeof(string));
                dt.Columns.Add("UPCCode", typeof(string));
                dt.Columns.Add("UpdatedDate", typeof(string));
                dt.Columns.Add("ItemCode", typeof(string));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("VendorName", typeof(string));
                dt.Columns.Add("SecondaryPLU", typeof(string));
                dt.Columns.Add("Barcode", typeof(string));

                foreach (var item in i)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductID"] = item.ProductID;
                    dr["UPCCode"] = item.UPCCode;

                    //DateTime date = Convert.ToDateTime(item.UpdatedDate.ToString());
                    dr["UpdatedDate"] = DateTime.Now.ToString();
                    dr["ItemCode"] = item.ItemCode;
                    dr["ProductName"] = item.ProductName;
                    dr["Price"] = item.Price;
                    dr["VendorName"] = item.VendorName;
                    dr["SecondaryPLU"] = item.SecondaryPLU;
                    dr["Barcode"] = item.Barcode;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }

        public void SearchProduct(string searchStr)
        {
            try
            {
                OrignalUPCCode = searchStr;
                if (searchStr != CommonModelCont.EmptyString && searchStr != null)
                {
                    if (searchStr.All(char.IsDigit))
                    {
                        int Count = searchStr.Length;
                        if (Count < 13)
                        {
                            Count = 13 - Count;
                            for (int i = 0; i < Count; i++)
                            {
                                searchStr = "0" + searchStr;
                            }
                        }
                    }
                    txtUPCCode.Text = searchStr;
                }
                LoadRDLC(searchStr);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadRDLC(string searchStr)
        {
            try
            {
                this.BringToFront();
                this.reportViewer1.RefreshReport();
                DataTable dt = new DataTable();
                dt = GetLableData(txtUPCCode.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    string UPC_E = Functions.GetUPC_E(OrignalUPCCode);
                    dt = GetLableData(UPC_E);
                }

                if (dt.Rows.Count > 0)
                {
                    //dt.Rows[0]["UPCCode"] = "*555555555*";

                    //// create a linear barcode object
                    //Linear barcode = new Linear();

                    //// set barcode type to Code 128
                    //barcode.Type = BarcodeType.CODE128;
                    //// set barcode encoding data value
                    //barcode.Data = dt.Rows[0]["UPCCode"].ToString();

                    //// set drawing barcode image format
                    //barcode.Format = System.Drawing.Imaging.ImageFormat.Jpeg;

                    //dt.Rows[0]["UPCCode"] = barcode.drawBarcodeAsBytes();

                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("DisplayLableData", dt);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);

                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"DisplayLable.rdlc");
                    reportViewer1.LocalReport.ReportPath = reportPath;
                    reportViewer1.RefreshReport();
                    //txtUPCCode.Text = "";
                    Export(reportViewer1.LocalReport);
                    Print();
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Product data not found", false);
                }
                //Print();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void SearchProduct_R(string searchStr)
        {
            try
            {
                OrignalUPCCode = searchStr;
                if (searchStr != CommonModelCont.EmptyString && searchStr != null)
                {
                    if (searchStr.All(char.IsDigit))
                    {
                        int Count = searchStr.Length;
                        if (Count < 13)
                        {
                            Count = 13 - Count;
                            for (int i = 0; i < Count; i++)
                            {
                                searchStr = "0" + searchStr;
                            }
                        }
                    }
                    txtUPCCode.Text = searchStr;
                }
                LoadRDLC_R(searchStr);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadRDLC_R(string searchStr)
        {
            try
            {
                SP_GetDisplayLableData_Result item = new SP_GetDisplayLableData_Result();
                this.BringToFront();
                this.reportViewer1.RefreshReport();
                DataTable dt = new DataTable();
                dt = GetLableData(txtUPCCode.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    string UPC_E = Functions.GetUPC_E(OrignalUPCCode);
                    dt = GetLableData(UPC_E);
                }
                if (dt.Rows.Count > 0)
                {
                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("DisplayLableData", dt);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    //reportViewer1.ShowPrintButton = false;

                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder + "\\Reports\\", @"DisplayLable_R.rdlc");
                    reportViewer1.LocalReport.ReportPath = reportPath;
                    reportViewer1.RefreshReport();
                    //txtUPCCode.Text = "";

                    //LocalReport report = new LocalReport();
                    //report.ReportPath = reportPath;
                    //report.DataSources.Add(new ReportDataSource("DisplayLableData", dt));
                    //Export(report);
                    //Print();

                    //string PrintString = @"^\u0010^XA\r\n" +
                    //                     @"~TA000^MNN^MTT^POI^PMN,0^JMA^PR6,6^MD15^LRN^CI0\r\n" +
                    //                     @"^RU\r\n" +
                    //                     @"^MMT\r\n" +
                    //                     @"^PW639\r\n" +
                    //                     @"^LL0190\r\n" +
                    //                     @"^FO448,0^GFA,04480,04480,00020,:Z64:\r\n" +
                    //                     @"^FT140,85^A0N,60,60^FD $" + dt.Rows[0]["Price"] + " ^FS\r" + // Price
                    //                     @"^FT340,40^A0N,25,25^FD " + dt.Rows[0]["UPCCode"] + " ^FS\r" + // UPC Code
                    //                     @"^FT340,70^A0N,25,25^FD " + dt.Rows[0]["UpdatedDate"] + " ^FS\r" + // Date
                    //                     @"^FT150,140^A0N,30,30^FD " + dt.Rows[0]["ProductName"] + " ^FS\r" + // Item Name
                    //                     @"^FT155,180^A0N,15,15^FD " + dt.Rows[0]["VendorName"] + " ^FS\r" + // vendor name
                    //                     @"^FO340,78^GB160,33,1^FS" +
                    //                     @"^FT341,103^A0N,25,25^FD " + dt.Rows[0]["ItemCode"] + " ^FS\r" + // item code
                    //                     @"^BY1,2,30^FT330,180^B3N,,Y,N^FD " + dt.Rows[0]["Barcode"] + " ^FS\r" +
                    //                     @"^FN1^RFR,E\r\n" +
                    //                     @"^PH\r\n" +
                    //                     @"^XZ\r\n";

                    //string PrintString2 = @"^\u0010^XA\r\n" +
                    //                      @"~TA000^MNN^MTT^POI^PMN,0^JMA^PR6,6^MD15^LRN^CI0\r\n" +
                    //                      @"^RU\r\n" +
                    //                      @"^MMT\r\n" +
                    //                      @"^PW639\r\n" +
                    //                      @"^LL0190\r\n" +
                    //                      @"^FO448,0^GFA,04480,04480,00020,:Z64:\r\n" +
                    //                      @"^FT110,85^A0N,60,60^FD $" + dt.Rows[0]["Price"] + " ^FS" +
                    //                      @"^FT340,40^A0N,25,25^FD " + dt.Rows[0]["UPCCode"] + " ^FS" +
                    //                      @"^FT340,70^A0N,25,25^FD " + dt.Rows[0]["UpdatedDate"] + " ^FS" +
                    //                      @"^FT97,140^A0N,30,30^FD " + dt.Rows[0]["ProductName"] + " ^FS" +
                    //                      @"^FT97,170^A0N,15,15^FD " + dt.Rows[0]["VendorName"] + " ^FS" +
                    //                      @"^FO340,78^GB160,33,1^FS" +
                    //                      @"^FT341,103^A0N,25,25^FD " + dt.Rows[0]["ItemCode"] + " ^FS" +
                    //                      @"^BY1,2,30^FT300,177^B3N,,Y,N^FD " + dt.Rows[0]["Barcode"] + " ^FS" +
                    //                      @"^FN1^RFR,E\r\n" +
                    //                      @"^PH\r\n" +
                    //                      @"^XZ\r\n";

                    //string PrintString3 = @"^XA" +
                    //                      @"~TA000^MNN^MTT^POI^PMN,0^JMA^PR6,6^MD15^LRN^CI0\r\n" +
                    //                      @"^RU" +
                    //                      @"^MMT" +
                    //                      @"^PW639" +
                    //                      @"^LL0190" +
                    //                      @"^FO448,0^GFA,04480,04480,00020,:Z64:" +
                    //                      @"^FT110,85^A0N,60,60^FD $" + dt.Rows[0]["Price"] + "^FS" +
                    //                      @"^FT340,40^A0N,20,20^FD" + dt.Rows[0]["UPCCode"] + "^FS" +
                    //                      @"^FT340,70^A0N,25,25^FD" + dt.Rows[0]["UpdatedDate"] + "^FS" +
                    //                      @"^FT120,138^A0N,22,22^FD" + dt.Rows[0]["ProductName"] + "^FS" +
                    //                      @"^FT125,160^A0N,10,10^FD" + dt.Rows[0]["VendorName"] + "^FS" +
                    //                      @"^FO340,78^GB160,33,1^FS" +
                    //                      @"^FT341,103^A0N,20,20^FD" + dt.Rows[0]["ItemCode"] + "^FS" +
                    //                      @"^BY1,2,30^FT340,170^B3N,,Y,N^FD" + dt.Rows[0]["Barcode"] + "^FS" +
                    //                      @"^FN1^RFR,E" +
                    //                      @"^PH" +
                    //                      @"^XZ";


                    //string PrintString4 = @"^XA" +
                    //                     @"~TA000^MNN^MTT^POI^PMN,0^JMA^PR6,6^MD15^LRN^CI0\r\n" +
                    //                     @"^RU" +
                    //                     @"^MMT" +
                    //                     @"^PW639" +
                    //                     @"^LL0190" +
                    //                     @"^FO448,0^GFA,04480,04480,00020,:Z64:" +
                    //                     @"^FT110,85^A0N,60,60^FD $" + dt.Rows[0]["Price"] + "^FS" +
                    //                     @"^FT340,40^A0N,20,20^FD" + dt.Rows[0]["UPCCode"] + "^FS" +
                    //                     @"^FT340,70^A0N,25,25^FD" + dt.Rows[0]["UpdatedDate"] + "^FS" +
                    //                     @"^FT115,138^A0N,22,22^FD" + dt.Rows[0]["ProductName"] + "^FS" +
                    //                     @"^FT115,160^A0N,10,10^FD" + dt.Rows[0]["VendorName"] + "^FS";
                    //if (!String.IsNullOrEmpty(dt.Rows[0]["ItemCode"].ToString()))
                    //{
                    //    PrintString4 = PrintString4 +
                    //        @"^FO340,78^GB160,33,1^FS" +
                    //@"^FT341,103^A0N,20,20^FD" + dt.Rows[0]["ItemCode"] + "^FS";
                    //}
                    //PrintString4 = PrintString4 +
                    //   @"^BY1,2,30^FT340,170^B3N,,Y,N^FD" + dt.Rows[0]["Barcode"] + "^FS" +
                    //@"^FN1^RFR,E" +
                    //@"^PH" +
                    //@"^XZ";
                    //if (item.ItemCode != null)
                    //{                        @"^FO340,78^GB160,33,1^FS"};

                    string PrintString11 = @"^XA" +
                                          @"^LT60" +
                                          @"~JL" +
                                          @"~TA000^MNN^MTT^POI^PMN,0^JMA^PR6,6^MD15^LRN^CI0\r\n" +
                                          @"^RU" +
                                          @"^MMT" +
                                          @"^PW639" +
                                          @"^FO448,0^GFA,04480,04480,00020,:Z64:" +
                                          @"^FT120,120^A0N,60,60^FD $" + dt.Rows[0]["Price"] + "^FS" +
                                          @"^LL0190" +
                                          @"^FT120,168^A0N,25,25^FD" + dt.Rows[0]["ProductName"] + "^FS" +
                                          @"^FT125,190^A0N,15,15^FD" + dt.Rows[0]["VendorName"] + "^FS" +
                                          @"^FT340,70^A0N,25,25^FD" + dt.Rows[0]["UPCCode"] + "^FS" +
                                          @"^FT340,100^A0N,25,25^FD" + dt.Rows[0]["UpdatedDate"] + "^FS" +
                                          @"^FO341,120^A0N,25,25^FD" + dt.Rows[0]["ItemCode"] + "^FS";

                    if (!String.IsNullOrEmpty(dt.Rows[0]["ItemCode"].ToString()))
                    {
                        PrintString11 = PrintString11 + @"^FO340,110^GB160,33,1^FS";
                    }
                    PrintString11 = PrintString11 +

                    @"^BY1,2,30^FT340,205^B3N,,Y,N^FD" + dt.Rows[0]["Barcode"] + "^FS" +
                    @"^FN1^RFR,E" +
                    @"^XZ";

                    RawPrinterHelper_.SendStringToPrinter("ZDesigner GK420d", PrintString11);
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Product data not found", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

    }
}
