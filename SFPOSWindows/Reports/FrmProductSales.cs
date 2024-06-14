using ClosedXML.Excel;
using Microsoft.PointOfService;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using SFPOS.Entities.spDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace SFPOSWindows.MasterForm
{
    public partial class FrmProductSales : Form
    {
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        DepartmentService _DepartmentService = new DepartmentService();
        SectionService _SectionService = new SectionService();
        List<DepartmentMasterModel> lstDepartmentMasterModel = new List<DepartmentMasterModel>();
        List<SP_GetSectionList_Result_Model> lstSectionMasterModel = new List<SP_GetSectionList_Result_Model>();
        ProductSalesService _ProductSalesService = new ProductSalesService();
        List<ProductSale_ResultModel> lstProductSale_ResultModel = new List<ProductSale_ResultModel>();
        bool FirstCall = false;
        //SerialPort ComPort = new SerialPort();
        //internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        //delegate void SetTextCallback(string text);
        //string InputData = String.Empty;
        private PosExplorer myPosExplorer;
        private Scanner myScanner;

        public FrmProductSales()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
        }

        public void loadData()
        {
            lstProductSale_ResultModel = _ProductSalesService.GetProductDetails(DateTime.Now, DateTime.Now, 0, 0, 0,0);
            ProductSaleGrdView.DataSource = lstProductSale_ResultModel
                                                .Select(o => new
                                                {
                                                    UPCCode = o.UPCCode,
                                                    ProductName = o.ProductName,
                                                    DepartmentName = o.DepartmentName,
                                                    SectionName = o.SectionName,
                                                    TOTAL_SALES_QTY = o.TOTAL_SALES_QTY,
                                                    TOTAL_SALES_PRICE = o.TOTAL_SALES_PRICE,
                                                    Vendor = o.Vendor,
                                                    TaxAmount = o.TaxAmount,
                                                    Taxable =o.Taxable
                                                }).ToList();          
            ProductSaleGrdView.Columns["Taxable"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ProductSaleGrdView.Columns["UPCCode"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
        }

        public void LoadCmbDepartmentName()
        {
            try
            {
                lstDepartmentMasterModel = _DepartmentService.GetAllDepartment();
                lstDepartmentMasterModel.Insert(0, new DepartmentMasterModel { DepartmentID = 0, DepartmentName = DepartmentMasterModelCont.cmbDepartmentFirst });
                cmbDepartment.DisplayMember = DepartmentMasterModelCont.DepartmentName;
                cmbDepartment.ValueMember = DepartmentMasterModelCont.DepartmentID;
                cmbDepartment.DataSource = lstDepartmentMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmProductSales + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbSectionName(int Departmentid)
        {
            try
            {
                lstSectionMasterModel = _SectionService.SectionDetail(Departmentid);
                lstSectionMasterModel.Insert(0, new SP_GetSectionList_Result_Model { SectionID = 0, SectionName = SectionMasterModelCont.cmbSectionFirst });

                if (lstSectionMasterModel.Count > 0)
                {
                    cmbSection.DisplayMember = SectionMasterModelCont.SectionName;
                    cmbSection.ValueMember = SectionMasterModelCont.SectionID;
                    cmbSection.DataSource = lstSectionMasterModel;
                }
                else
                {
                    cmbSection.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmProductSales + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbVendorName()
        {
            try
            {

                List<VendorMasterModel> lstVendorMasterModel = new List<VendorMasterModel>();
                VendorService _VendorService = new VendorService();

                lstVendorMasterModel = _VendorService.GetAllVendor();
                lstVendorMasterModel.Insert(0, new VendorMasterModel { VendorID = 0, VendorName = VendorMasterModelCont.cmbVendorFirst });
                //lstVendorMasterModel.Insert(1, new VendorMasterModel { VendorID = 1, VendorName = "Multiple Vendors" });
                cmbVendors.DisplayMember = VendorMasterModelCont.VendorName;
                cmbVendors.ValueMember = VendorMasterModelCont.VendorID;
                cmbVendors.DataSource = lstVendorMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDepartment.SelectedValue != null)
                {
                    int DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue.ToString());
                    LoadCmbSectionName(DepartmentID);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmProductSales + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbSection.SelectedValue) == 0 && (Convert.ToInt32(cmbDepartment.SelectedValue) != 0
                && Convert.ToDateTime(startDate.Value).Date != DateTime.Now))
            {
                lstProductSale_ResultModel = _ProductSalesService.GetProductDetails(Convert.ToDateTime(startDate.Value).Date
                                                                                    , Convert.ToDateTime(endDate.Value).Date
                                                                                    , Convert.ToInt64(cmbDepartment.SelectedValue)
                                                                                    , Convert.ToInt64(cmbSection.SelectedValue)
                                                                                    , Convert.ToInt64(cmbVendors.SelectedValue)
                                                                                    , 1);
                ProductSaleGrdView.DataSource = lstProductSale_ResultModel
                                                .Select(o => new
                                                {
                                                    UPCCode = o.UPCCode,
                                                    ProductName = o.ProductName,
                                                    DepartmentName = o.DepartmentName,
                                                    SectionName = o.SectionName,
                                                    TOTAL_SALES_QTY = o.TOTAL_SALES_QTY,
                                                    TOTAL_SALES_PRICE = o.TOTAL_SALES_PRICE,
                                                    Vendor = o.Vendor,
                                                    TaxAmount = o.TaxAmount,
                                                    Taxable = o.Taxable
                                                }).ToList();

                ProductSaleGrdView.Columns["Taxable"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                ProductSaleGrdView.Columns["UPCCode"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }
            else if ((Convert.ToInt32(cmbVendors.SelectedValue) != 0)
               && (Convert.ToDateTime(startDate.Value).Date != DateTime.Now))
            {
                lstProductSale_ResultModel = _ProductSalesService.GetProductDetails(Convert.ToDateTime(startDate.Value).Date
                                                                                    , Convert.ToDateTime(endDate.Value).Date
                                                                                    , Convert.ToInt64(cmbDepartment.SelectedValue)
                                                                                    , Convert.ToInt64(cmbSection.SelectedValue)
                                                                                    , Convert.ToInt64(cmbVendors.SelectedValue)
                                                                                    , 4);
                ProductSaleGrdView.DataSource = lstProductSale_ResultModel
                                                .Select(o => new
                                                {
                                                    UPCCode = o.UPCCode,
                                                    ProductName = o.ProductName,
                                                    DepartmentName = o.DepartmentName,
                                                    SectionName = o.SectionName,
                                                    TOTAL_SALES_QTY = o.TOTAL_SALES_QTY,
                                                    TOTAL_SALES_PRICE = o.TOTAL_SALES_PRICE,
                                                    Vendor = o.Vendor,
                                                    TaxAmount = o.TaxAmount,
                                                    Taxable = o.Taxable
                                                }).ToList();

                ProductSaleGrdView.Columns["Taxable"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                ProductSaleGrdView.Columns["UPCCode"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

            }
            else if ((Convert.ToInt32(cmbDepartment.SelectedValue) != 0 || Convert.ToInt32(cmbSection.SelectedValue) != 0)
                && (Convert.ToDateTime(startDate.Value).Date != DateTime.Now))
            {
                lstProductSale_ResultModel = _ProductSalesService.GetProductDetails(Convert.ToDateTime(startDate.Value).Date
                                                                                    , Convert.ToDateTime(endDate.Value).Date
                                                                                    , Convert.ToInt64(cmbDepartment.SelectedValue)
                                                                                    , Convert.ToInt64(cmbSection.SelectedValue)
                                                                                    , Convert.ToInt64(cmbVendors.SelectedValue)
                                                                                    , 2);
                ProductSaleGrdView.DataSource = lstProductSale_ResultModel
                                                .Select(o => new
                                                {
                                                    UPCCode = o.UPCCode,
                                                    ProductName = o.ProductName,
                                                    DepartmentName = o.DepartmentName,
                                                    SectionName = o.SectionName,
                                                    TOTAL_SALES_QTY = o.TOTAL_SALES_QTY,
                                                    TOTAL_SALES_PRICE = o.TOTAL_SALES_PRICE,
                                                    Vendor = o.Vendor,
                                                    TaxAmount = o.TaxAmount,
                                                    Taxable = o.Taxable
                                                }).ToList();

                ProductSaleGrdView.Columns["Taxable"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                ProductSaleGrdView.Columns["UPCCode"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }
            else if (Convert.ToInt32(cmbDepartment.SelectedValue) == 0 && Convert.ToInt32(cmbSection.SelectedValue) == 0)
            {
                lstProductSale_ResultModel = _ProductSalesService.GetProductDetails(Convert.ToDateTime(startDate.Value).Date
                                                                                    , Convert.ToDateTime(endDate.Value).Date
                                                                                    , Convert.ToInt64(cmbDepartment.SelectedValue)
                                                                                    , Convert.ToInt64(cmbSection.SelectedValue)
                                                                                    , Convert.ToInt64(cmbVendors.SelectedValue)
                                                                                    , 3);
                ProductSaleGrdView.DataSource = lstProductSale_ResultModel
                                                 .Select(o => new
                                                 {
                                                     UPCCode = o.UPCCode,
                                                     ProductName = o.ProductName,
                                                     DepartmentName = o.DepartmentName,
                                                     SectionName = o.SectionName,
                                                     TOTAL_SALES_QTY = o.TOTAL_SALES_QTY,
                                                     TOTAL_SALES_PRICE = o.TOTAL_SALES_PRICE,
                                                     Vendor = o.Vendor,
                                                     TaxAmount = o.TaxAmount,
                                                     Taxable = o.Taxable
                                                 }).ToList();

                ProductSaleGrdView.Columns["UPCCode"].Width = 100;
                ProductSaleGrdView.Columns["Taxable"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                ProductSaleGrdView.Columns["UPCCode"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }
            else
            {
                loadData();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    if (txtSearch.Text != "" && txtSearch.Text != null)
                    {
                        if (txtSearch.Text.All(char.IsDigit))
                        {
                            int Count = txtSearch.Text.Length;
                            if (Count < 13)
                            {
                                Count = 13 - Count;
                                for (int i = 0; i < Count; i++)
                                {
                                    txtSearch.Text = "0" + txtSearch.Text;
                                }
                            }
                        }
                        lstProductSale_ResultModel = lstProductSale_ResultModel
                                .Where(o => o.ProductName.ToLower().StartsWith(txtSearch.Text.ToLower())
                                             || o.UPCCode.StartsWith(txtSearch.Text.ToLower())).ToList();
                        ProductSaleGrdView.DataSource = lstProductSale_ResultModel;
                    }
                    else
                    {
                        loadData();
                    }
                }
                catch (Exception ex)
                {
                    _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmProductSales + ex.StackTrace, ex.LineNumber());
                }
            }
        }

        private void FrmProductSales_Load(object sender, EventArgs e)
        {
            try
            {
                loadData();
                DeviceAdd();
            }
            catch (Exception ex)
            {

            }
            //OpenPort();
        }
        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            ASCIIEncoding myEncoding = new ASCIIEncoding();
            string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
            if (UPCCode.Length > 1)
            {
                if (myScanner.ScanDataType != BarCodeSymbology.Code39)
                    UPCCode = UPCCode.Substring(0, UPCCode.Length - 1);
            }
            txtSearch.Text = UPCCode;
            if (myScanner.DataEventEnabled == false)
            {
                myScanner.DataEventEnabled = true;
            }
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
                            if (myScanner.DataEventEnabled == false)
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
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }
        private void FrmProductSales_Leave(object sender, EventArgs e)
        {
            DeviceRemove();
        }

        private void FrmProductSales_VisibleChanged(object sender, EventArgs e)
        {
            if (FirstCall)
            {
                DeviceRemove();
            }
            FirstCall = true;
        }

        private void ProductSaleGrdView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        { //get the current column details
            string strColumnName = ProductSaleGrdView.Columns[e.ColumnIndex].Name;
            ProductSaleGrdView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
            SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

            lstProductSale_ResultModel.Sort(new ProductSaleComparer(strColumnName, strSortOrder));
            ProductSaleGrdView.DataSource = null;
            ProductSaleGrdView.DataSource = lstProductSale_ResultModel.Select(o => new
            {
                UPCCode = o.UPCCode,
                ProductName = o.ProductName,
                DepartmentName = o.DepartmentName,
                SectionName = o.SectionName,
                TOTAL_SALES_QTY = o.TOTAL_SALES_QTY,
                TOTAL_SALES_PRICE = o.TOTAL_SALES_PRICE,
                Vendor = o.Vendor,
                TaxAmount = o.TaxAmount,
                Taxable = o.Taxable
            }).ToList();

            ProductSaleGrdView.Columns["Taxable"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ProductSaleGrdView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
            ProductSaleGrdView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
        }
        /// <summary>
        /// Get the current sort order of the column and return it
        /// set the new SortOrder to the columns.
        /// </summary>
        /// <param name="columnIndex"></param>
        ///// <returns>SortOrder of the current column</returns>
        private SortOrder getSortOrder(int columnIndex)
        {
            try
            {
                if (ProductSaleGrdView.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                    ProductSaleGrdView.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                {
                    ProductSaleGrdView.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    return SortOrder.Ascending;
                }
                else
                {
                    ProductSaleGrdView.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    return SortOrder.Descending;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = tableLoad();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xlsx (*.xlsx)|*.xlsx";
                sfd.FileName = "ProductSales";
                sfd.Title = "Save an Excel File";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ClsCommon.MsgBox("Information", "Data will be exported and you will be notified when it is ready.", false);
                    if (picLoader.InvokeRequired)
                    {
                        picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = true; }));
                    }
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            ClsCommon.MsgBox("Information", "It wasn't possible to write the data to the disk." + ex.Message, false);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "ProductSales");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        if (picLoader.InvokeRequired)
                        {
                            picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = false; }));
                        }
                        ClsCommon.MsgBox("Information", "Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong while exporting Product !!!", false);
                if (picLoader.InvokeRequired)
                {
                    picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = false; }));
                }
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }



        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {

                dt.Columns.Add(ProductSale_ResultModelCont.UPCCode, typeof(string));
                dt.Columns.Add(ProductSale_ResultModelCont.ProductName, typeof(string));
                dt.Columns.Add(ProductSale_ResultModelCont.DepartmentName, typeof(string));
                dt.Columns.Add(ProductSale_ResultModelCont.SectionName, typeof(string));
                dt.Columns.Add(ProductSale_ResultModelCont.TOTAL_SALES_QTY, typeof(string));
                dt.Columns.Add(ProductSale_ResultModelCont.TOTAL_SALES_PRICE, typeof(string));
                dt.Columns.Add(ProductSale_ResultModelCont.Vendor, typeof(string));
                dt.Columns.Add(ProductSale_ResultModelCont.TaxAmount, typeof(string));
                dt.Columns.Add(ProductSale_ResultModelCont.Taxable, typeof(string));

                foreach (var item in lstProductSale_ResultModel)
                {
                    DataRow dr = dt.NewRow();

                    dr[ProductSale_ResultModelCont.UPCCode] = item.UPCCode;
                    dr[ProductSale_ResultModelCont.ProductName] = item.ProductName;
                    dr[ProductSale_ResultModelCont.DepartmentName] = item.DepartmentName;
                    dr[ProductSale_ResultModelCont.SectionName] = item.SectionName;
                    dr[ProductSale_ResultModelCont.TOTAL_SALES_QTY] = item.TOTAL_SALES_QTY;
                    dr[ProductSale_ResultModelCont.TOTAL_SALES_PRICE] = item.TOTAL_SALES_PRICE;
                    dr[ProductSale_ResultModelCont.Vendor] = item.Vendor;
                    dr[ProductSale_ResultModelCont.TaxAmount] = item.TaxAmount;
                    dr[ProductSale_ResultModelCont.Taxable] = item.Taxable;                    
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
    }
    public class ProductSale_ResultModelCont
    {
        public const string ProductName = "ProductName";
        public const string DepartmentName = "DepartmentName";
        public const string SectionName = "SectionName";
        public const string TOTAL_SALES_QTY = "TOTAL_SALES_QTY";
        public const string TOTAL_SALES_PRICE = "TOTAL_SALES_PRICE";
        public const string Taxable = "Taxable";
        public const string Vendor = "Vendor";        
        public const string UPCCode = "UPCCode";
        public const string TaxAmount = "TaxAmount";
    }


    class ProductSaleComparer : IComparer<ProductSale_ResultModel>
    {
        string memberName = string.Empty; // specifies the member name to be sorted
        SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder.

        /// <summary>
        /// constructor to set the sort column and sort order.
        /// </summary>
        /// <param name="strMemberName"></param>
        /// <param name="sortingOrder"></param>
        public ProductSaleComparer(string strMemberName, SortOrder sortingOrder)
        {
            memberName = strMemberName;
            sortOrder = sortingOrder;
        }

        /// <summary>
        /// Compares two Students based on member name and sort order
        /// and return the result.
        /// </summary>
        /// <param name="Student1"></param>
        /// <param name="Student2"></param>
        /// <returns></returns>
        public int Compare(ProductSale_ResultModel Student1, ProductSale_ResultModel Student2)
        {
            int returnValue = 1;
            switch (memberName)
            {
                case "DepartmentName":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.DepartmentName.CompareTo(Student2.DepartmentName);
                    }
                    else
                    {
                        returnValue = Student2.DepartmentName.CompareTo(Student1.DepartmentName);
                    }

                    break;
                case "ProductName":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.ProductName.CompareTo(Student2.ProductName);
                    }
                    else
                    {
                        returnValue = Student2.ProductName.CompareTo(Student1.ProductName);
                    }
                    break;
                case "SectionName":
                    if (string.IsNullOrEmpty(Student1.SectionName) || string.IsNullOrEmpty(Student2.SectionName))
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ProductName.CompareTo(Student2.ProductName);
                        }
                        else
                        {
                            returnValue = Student2.ProductName.CompareTo(Student1.ProductName);
                        }
                        break;
                    }
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.SectionName.CompareTo(Student2.SectionName);
                    }
                    else
                    {
                        returnValue = Student2.SectionName.CompareTo(Student1.SectionName);
                    }
                    break;
                case "TOTAL_SALES_QTY":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.TOTAL_SALES_QTY.ToString().CompareTo(Student2.TOTAL_SALES_QTY.ToString());
                    }
                    else
                    {
                        returnValue = Student2.TOTAL_SALES_QTY.ToString().CompareTo(Student1.TOTAL_SALES_QTY.ToString());
                    }
                    break;
                case "TOTAL_SALES_PRICE":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.TOTAL_SALES_PRICE.ToString().CompareTo(Student2.TOTAL_SALES_PRICE.ToString());
                    }
                    else
                    {
                        returnValue = Student2.TOTAL_SALES_PRICE.ToString().CompareTo(Student1.TOTAL_SALES_PRICE.ToString());
                    }
                    break;
                case "UPCCode":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.UPCCode.CompareTo(Student2.UPCCode);
                    }
                    else
                    {
                        returnValue = Student2.UPCCode.CompareTo(Student1.UPCCode);
                    }
                    break;
                case "Vendor":
                    if (string.IsNullOrEmpty(Student1.Vendor) || string.IsNullOrEmpty(Student2.Vendor))
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ProductName.CompareTo(Student2.ProductName);
                        }
                        else
                        {
                            returnValue = Student2.ProductName.CompareTo(Student1.ProductName);
                        }
                        break;
                    }
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.Vendor.CompareTo(Student2.Vendor);
                    }
                    else
                    {
                        returnValue = Student2.Vendor.CompareTo(Student1.Vendor);
                    }
                    break;
                case "TaxAmount":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.TaxAmount.ToString().CompareTo(Student2.TaxAmount.ToString());
                    }
                    else
                    {
                        returnValue = Student2.TaxAmount.ToString().CompareTo(Student1.TaxAmount.ToString());
                    }
                    break;
                case "Taxable":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.Taxable.CompareTo(Student2.Taxable);
                    }
                    else
                    {
                        returnValue = Student2.Taxable.CompareTo(Student1.Taxable);
                    }
                    break;
                default:
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = Student1.ProductName.CompareTo(Student2.ProductName);
                    }
                    else
                    {
                        returnValue = Student2.ProductName.CompareTo(Student1.ProductName);
                    }
                    break;
            }
            return returnValue;
        }
    }
}
