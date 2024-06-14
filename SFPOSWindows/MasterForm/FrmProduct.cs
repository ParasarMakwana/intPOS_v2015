using ClosedXML.Excel;
using Microsoft.PointOfService;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.spDataClasses;
using SFPOS.Link;
using SFPOSWindows.Metro_Forms;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmProduct : Form
    {
        #region Properties

        //public SerialPort ComPort = new SerialPort();
        //internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        //internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        //delegate void SetTextCallback(string text);
        //string InputData = String.Empty;
        private PosExplorer myPosExplorer;
        private Scanner myScanner;

        bool FirstCall = false;

        ProductService _ProductService = new ProductService();
        ColumnFilterService _ColumnFilterService = new ColumnFilterService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        ProductMasterModel objProductMasterModel = new ProductMasterModel();
        ProductVendorService _ProductVendorService = new ProductVendorService();

        public long PrimaryId = 0;
        public string prod_Name = "";
        OpenFileDialog opnfd = new OpenFileDialog();
        List<ProductMasterModel> lstProductMasterModel = new List<ProductMasterModel>();
        List<ProductMasterModel> F_lstProductMasterModel = new List<ProductMasterModel>();
        List<ProductMasterModel> M_lstProductMasterModel = new List<ProductMasterModel>();
        List<ProductMasterModel> SearchFilter_ProductMasterModel = new List<ProductMasterModel>();
        List<ProductMasterModel> SearchFilterResult_ProductMasterModel = new List<ProductMasterModel>();
        List<ProductVendorMasterModel> lstProductVendorMasterModel = new List<ProductVendorMasterModel>();
        IDictionary<string,string> dictGridColumnDisp = new Dictionary<string, string>();
        public static int StartIndex = 0;
        public static int EndIndex = 50;
        public bool first = false;
        public static bool ShowAll = false;
        public static bool IsFilterClick = false;
        public static bool isResetFilter = true;
        public static bool IsSearchFilterClick = false;
        public static bool IsFilterColAvailable = true;
        public static int pageIndex = 1;
        IDictionary<string, bool> PreviousColumnFilterList = new Dictionary<string, bool>();
        public int DepID = 0;
        List<string> lstSearchFilterCols = new List<string>();
        public string OG_SearchFilterText = "";
        string[] EditablecolumnNames;
        public static bool IsEditMode = false;
        List<ClsFilterCheckBox> lstFilterBoxData = new List<ClsFilterCheckBox>();
        BindingSource bs = new BindingSource();
        public long? NewVendorId;
        #endregion

        #region Events
        private void ProductGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt64(Convert.ToString(ProductGrdView.Rows[e.RowIndex].Cells[ProductMasterModelCont.ProductID].Value));
                    prod_Name = Convert.ToString(ProductGrdView.Rows[e.RowIndex].Cells[ProductMasterModelCont.ProductName].Value).Trim();
                    metroBtnProductvendor.Enabled = true;
                    metroBtnSaleDisc.Enabled = true;
                    metroBtnSalePrice.Enabled = true;
                    if (IsEditMode)
                    {
                        saveUpdateGridData(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ProductGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (ProductGrdView.Columns[e.ColumnIndex].ToolTipText == "Edit")
                {
                    UpdateData(e.RowIndex);
                }
                if (ProductGrdView.Columns[e.ColumnIndex].ToolTipText == "Delete")
                {
                    ProductMasterModel objProductMasterModel = new ProductMasterModel();
                    if (PrimaryId >= 0)
                    {
                        DialogResult result = MessageBox.Show(this, AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            objProductMasterModel.ProductID = PrimaryId;
                            var add = _ProductService.DeleteAllProduct(objProductMasterModel);
                            UpdateLog();
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                            }
                            Clear();
                        }
                    }
                }
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    first = false;
                    txtSearchFilter.Text = string.Empty;
                    lstFilterBoxData.Clear();
                    btnExport.Enabled = true;
                    btnImport.Enabled = true;
                    btnColumnFilter.Enabled = true;
                    IsEditMode = false;
                    SearchProduct(txtSearchProductName.Text);
                    txtSearchProductName.SelectionStart = 0;
                    txtSearchProductName.SelectionLength = txtSearchProductName.Text.Length;
                }
                catch (Exception ex)
                {
                    _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
                }
            }
        }

        private void metroBtnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                StartIndex = 0;
                EndIndex = 50;
                btnExport.Enabled = true;
                btnImport.Enabled = true;
                btnColumnFilter.Enabled = true;
                IsSearchFilterClick = false;
                IsEditMode = false;
                Clear();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnAdd_Click(object sender, EventArgs e)
        {
            DeviceRemove();
            FrmMetro_AddProduct objFrmMetro_AddProduct = new FrmMetro_AddProduct();

            objFrmMetro_AddProduct.ShowDialog();
            DeviceAdd();
            txtSearchProductName.Focus();
            txtSearchProductName.SelectionStart = 0;
            txtSearchProductName.SelectionLength = txtSearchProductName.Text.Length;
            //txtSearchProductName.SelectAll();
            this.first = false;
            Thread thread = new Thread(new ThreadStart(dataLoad));
            thread.Start();
        }

        private void metroBtnProductvendor_Click(object sender, EventArgs e)
        {
            AddVendor();
        }

        private void metroBtnSaleDisc_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmProductSalesDiscount objFrmProductSalesDiscount = new FrmProductSalesDiscount();
                //objFrmProductSalesDiscount.ProductId = PrimaryId;
                //objFrmProductSalesDiscount.productName = productName;
                //objFrmProductSalesDiscount.dataLoad();

                //DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                //var bmpEdit = new Bitmap(Resources.edit);
                //imgEdit.Image = bmpEdit;
                //objFrmProductSalesDiscount.ProductSalesDiscountGrdView.Columns.Add(imgEdit);
                //imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                //DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                //var bmp = new Bitmap(Resources.delete);
                //imgDelete.Image = bmp;
                //objFrmProductSalesDiscount.ProductSalesDiscountGrdView.Columns.Add(imgDelete);
                //imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                ////objFrmProductSalesDiscount.LoadCmbProductName();
                //objFrmProductSalesDiscount.ShowDialog();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void metroBtnSalePrice_Click(object sender, EventArgs e)
        {
            try
            {
                AddSalePrice();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        //private Boolean isScrolled = true;
        private void ProductGrdView_Scroll(object sender, ScrollEventArgs e)
        {
            int display = ProductGrdView.Rows.Count - ProductGrdView.DisplayedRowCount(false);
            if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement))// && isScrolled == true
            {
                if (e.NewValue >= ProductGrdView.Rows.Count - GetDisplayedRowsCount())
                {
                    if (picLoader.InvokeRequired)
                    {
                        picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = true; }));
                    }
                    StartIndex = StartIndex + 50;
                    EndIndex = EndIndex + 50;
                    F_lstProductMasterModel = new List<ProductMasterModel>();
                    if (!IsSearchFilterClick)
                    {
                        //isScrolled = false;

                        int didx = ProductGrdView.FirstDisplayedScrollingRowIndex;

                        F_lstProductMasterModel = lstProductMasterModel.Where(o => o.RowNumber > StartIndex && o.RowNumber <= EndIndex).ToList();

                        M_lstProductMasterModel.AddRange(F_lstProductMasterModel);

                        bs.DataSource = M_lstProductMasterModel
                           .Select(o => new
                           {
                               ProductID = o.ProductID,
                               CertCode = o.CertCode,
                               ProductName = o.ProductName,
                               UPCCode = o.UPCCode,
                               DepartmentID = o.DepartmentID,
                               SectionID = o.SectionID,
                               UnitMeasureID = o.UnitMeasureID,
                               TaxGroupID = o.TaxGroupID,
                               DepartmentName = o.DepartmentName,
                               SectionName = o.SectionName,
                               UnitMeasureCode = o.UnitMeasureCode,
                               TaxGroupName = o.TaxGroupName,
                               TareWeight = o.TareWeight,
                               AgeVerification = o.AgeVerification,
                               GroupPrice = o.GroupPrice,
                               GroupQty = o.GroupQty,
                               CaseQty = o.CaseQty,
                               CasePrice = o.CasePrice,
                               LinkedUPCCode = o.LinkedUPCCode,
                               IsScaled = o.IsScaled,
                               Image = o.Image,
                               Price = o.Price,
                               IsActive = o.IsActive,
                               IsFoodStamp = o.IsFoodStamp,
                               IsGroupPrice = o.IsGroupPrice,
                               LabeledPrice = o.LabeledPrice,
                               UnitCost = o.UnitCost,
                               ProductVendorID = o.ProductVendorID,
                               Pack = o.Pack,
                               Size = o.Size,
                               SecondaryPLU = o.SecondaryPLU,
                               PalletQTY = o.PalletQTY,
                               CountryofOrigin = o.CountryofOrigin,
                               VendorName = o.VendorName,
                               FSEligibleAmount = o.FSEligibleAmount
                           }).ToList();
                        ProductGrdView.DataSource = bs;

                        if (didx <= ProductGrdView.Rows.Count)
                        {
                            if (didx > 5)
                            {
                                didx = didx - 5;
                            }
                            ProductGrdView.FirstDisplayedScrollingRowIndex = didx;
                            //isScrolled = true;
                        }
                    }
                    else
                    {


                        List<ProductMasterModel> SearchFilter_ProductMasterNextModel = _ProductService.GetAllProduct_With_Paging(OG_SearchFilterText, pageIndex++);
                        if (SearchFilter_ProductMasterNextModel != null && SearchFilter_ProductMasterNextModel.Count > 0)
                        {
                            foreach (var item in SearchFilter_ProductMasterNextModel)
                            {
                                SearchFilter_ProductMasterModel.Add(item);
                            }
                            bs.DataSource = SearchFilter_ProductMasterModel
                                .Select(o => new
                                {
                                    ProductID = o.ProductID,
                                    CertCode = o.CertCode,
                                    ProductName = o.ProductName,
                                    UPCCode = o.UPCCode,
                                    DepartmentID = o.DepartmentID,
                                    SectionID = o.SectionID,
                                    UnitMeasureID = o.UnitMeasureID,
                                    TaxGroupID = o.TaxGroupID,
                                    DepartmentName = o.DepartmentName,
                                    SectionName = o.SectionName,
                                    UnitMeasureCode = o.UnitMeasureCode,
                                    TaxGroupName = o.TaxGroupName,
                                    TareWeight = o.TareWeight,
                                    AgeVerification = o.AgeVerification,
                                    GroupPrice = o.GroupPrice,
                                    GroupQty = o.GroupQty,
                                    CaseQty = o.CaseQty,
                                    CasePrice = o.CasePrice,
                                    LinkedUPCCode = o.LinkedUPCCode,
                                    IsScaled = o.IsScaled,
                                    Image = o.Image,
                                    Price = o.Price,
                                    IsActive = o.IsActive,
                                    IsFoodStamp = o.IsFoodStamp,
                                    IsGroupPrice = o.IsGroupPrice,
                                    LabeledPrice = o.LabeledPrice,
                                    UnitCost = o.UnitCost,
                                    ProductVendorID = o.ProductVendorID,
                                    Pack = o.Pack,
                                    Size = o.Size,
                                    SecondaryPLU = o.SecondaryPLU,
                                    PalletQTY = o.PalletQTY,
                                    CountryofOrigin = o.CountryofOrigin,
                                    VendorName = o.VendorName,
                                    FSEligibleAmount = o.FSEligibleAmount
                                }).ToList();
                            ProductGrdView.DataSource = bs;
                            ProductGrdView.ClearSelection();
                            ProductGrdView.FirstDisplayedScrollingRowIndex = display;
                        }
                        //pageIndex++;
                    }
                    if (InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            grdHideColumn();
                        }));
                    }
                    else
                    {
                        grdHideColumn();
                    }

                    if (picLoader.InvokeRequired)
                    {
                        picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = false; }));
                    }

                    if(IsEditMode)
                    {
                        btnEditList_Click(null, null);
                    }
                }
            }
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            //OpenPort();
            try
            {
                //picLoader.Visible = true;
                //first = false;
                Thread thread = new Thread(new ThreadStart(dataLoad));
                thread.Start();
                DeviceAdd();
                txtSearchProductName.Focus();
                txtSearchProductName.SelectionStart = 0;
                txtSearchProductName.SelectionLength = txtSearchProductName.Text.Length;
            }
            catch (Exception ex)
            {
                if (picLoader.InvokeRequired)
                {
                    picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = false; }));
                }
                //MessageBox.Show(ex.Message.ToString());
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            try
            {
                ASCIIEncoding myEncoding = new ASCIIEncoding();
                string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
                if (UPCCode.Length > 1)
                {
                    if (myScanner.ScanDataType != BarCodeSymbology.Code39)
                        UPCCode = UPCCode.Substring(0, UPCCode.Length - 1);
                }
                txtSearchProductName.Text = UPCCode;
                SearchProduct(txtSearchProductName.Text.Trim());
                if (myScanner.DataEventEnabled == false)
                {
                    myScanner.DataEventEnabled = true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmProduct_Leave(object sender, EventArgs e)
        {
            try
            {
                DeviceRemove();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ProductGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }

        internal delegate void SetDataSourceDelegate(List<ProductMasterModel> M_lstProductMasterModel);

        private void setDataSource(List<ProductMasterModel> M_lstProductMasterModel)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new SetDataSourceDelegate(setDataSource), M_lstProductMasterModel);
                }
                else
                {
                    ProductGrdView.DataSource = null;
                    bs.DataSource = M_lstProductMasterModel
                        .Select(o => new
                        {
                            ProductID = o.ProductID,
                            CertCode = o.CertCode,
                            ProductName = o.ProductName,
                            UPCCode = o.UPCCode,
                            DepartmentID = o.DepartmentID,
                            SectionID = o.SectionID,
                            UnitMeasureID = o.UnitMeasureID,
                            TaxGroupID = o.TaxGroupID,
                            DepartmentName = o.DepartmentName,
                            SectionName = o.SectionName,
                            UnitMeasureCode = o.UnitMeasureCode,
                            TaxGroupName = o.TaxGroupName,
                            TareWeight = o.TareWeight,
                            AgeVerification = o.AgeVerification,
                            GroupPrice = o.GroupPrice,
                            GroupQty = o.GroupQty,
                            CaseQty = o.CaseQty,
                            CasePrice = o.CasePrice,
                            LinkedUPCCode = o.LinkedUPCCode,
                            IsScaled = o.IsScaled,
                            Image = o.Image,
                            Price = o.Price,
                            IsActive = o.IsActive,
                            IsFoodStamp = o.IsFoodStamp,
                            IsGroupPrice = o.IsGroupPrice,
                            LabeledPrice = o.LabeledPrice,
                            UnitCost = o.UnitCost,
                            ProductVendorID = o.ProductVendorID,
                            Pack = o.Pack,
                            Size = o.Size,
                            SecondaryPLU = o.SecondaryPLU,
                            PalletQTY = o.PalletQTY,
                            UpdatedDate = o.UpdatedDate,
                            CountryofOrigin = o.CountryofOrigin,
                            VendorName = o.VendorName,
                            FSEligibleAmount = o.FSEligibleAmount
                        }).ToList();
                    ProductGrdView.DataSource = bs;

                    if (ProductGrdView.Columns[0].Name == "")
                    {
                        ProductGrdView.Columns.RemoveAt(0);
                    }

                    if (ProductGrdView.Rows.Count > 0 && (first == false))
                    {
                        DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                        var bmpEdit = new Bitmap(Resources.edit);
                        imgEdit.Image = bmpEdit;
                        imgEdit.ToolTipText = "Edit";
                        ProductGrdView.Columns.Add(imgEdit);
                        imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                        if (LoginInfo.RoleType == "Administrator")
                        {
                            if (ProductGrdView.Columns[0].Name == "")
                            {
                                ProductGrdView.Columns.RemoveAt(0);
                            }
                            DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                            var bmp = new Bitmap(Resources.delete);
                            imgDelete.Image = bmp;
                            imgDelete.ToolTipText = "Delete";
                            ProductGrdView.Columns.Add(imgDelete);
                            imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            first = true;
                        }

                        first = true;
                    }

                    if (picLoader.InvokeRequired)
                    {
                        picLoader.Invoke(new MethodInvoker(delegate { picLoader.Visible = false; }));
                    }
                    if (InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            grdHideColumn();
                        }));
                    }
                    else
                    {
                        grdHideColumn();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmProduct_VisibleChanged(object sender, EventArgs e)
        {
            if (FirstCall)
            {
                DeviceRemove();
            }
            FirstCall = true;
        }

        #endregion

        #region Functions
        public FrmProduct()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
            //ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            isResetFilter = true;
            IsFilterClick = false;
            IsSearchFilterClick = false;
            IsEditMode = false;
            bindColumnFiltersChecklistBox();
            getGridColumnType();
            txtSearchProductName.Focus();
            txtSearchProductName.SelectionStart = 0;
            txtSearchProductName.SelectionLength = txtSearchProductName.Text.Length;
            if (LoginInfo.RoleType == "Administrator")
            {
                btnEditList.Visible = true;
            }
        }

        public void AddVendor()
        {
            try
            {
                FrmProductVendor objFrmProductVendor = new FrmProductVendor();
                objFrmProductVendor.ProductId = PrimaryId;
                objFrmProductVendor.productName = prod_Name;
                objFrmProductVendor.dataLoad();

                //DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
                //dgvCmb.ValueType = typeof(bool);
                //dgvCmb.Name = "Chk";
                //dgvCmb.DataPropertyName = ProductVendorMasterModelCont.IsDefault;
                //dgvCmb.HeaderText = "IsDefault";
                //objFrmProductVendor.ProductVendorGrdView.Columns.Add(dgvCmb);

                DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                var bmpEdit = new Bitmap(Resources.edit);
                imgEdit.Image = bmpEdit;
                objFrmProductVendor.ProductVendorGrdView.Columns.Add(imgEdit);
                imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                var bmp = new Bitmap(Resources.delete);
                imgDelete.Image = bmp;
                objFrmProductVendor.ProductVendorGrdView.Columns.Add(imgDelete);
                imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                objFrmProductVendor.ShowDialog();

                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void AddSalePrice()
        {
            try
            {
                FrmProductSalesPrice objFrmProductSalesPrice = new FrmProductSalesPrice();
                objFrmProductSalesPrice.ProductId = PrimaryId;
                objFrmProductSalesPrice.productName = prod_Name;
                objFrmProductSalesPrice.dataLoad();

                DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                var bmpEdit = new Bitmap(Resources.edit);
                imgEdit.Image = bmpEdit;
                objFrmProductSalesPrice.ProductSalesPriceGrdView.Columns.Add(imgEdit);
                imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                var bmp = new Bitmap(Resources.delete);
                imgDelete.Image = bmp;
                objFrmProductSalesPrice.ProductSalesPriceGrdView.Columns.Add(imgDelete);

                imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                objFrmProductSalesPrice.ShowDialog();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void dataLoad()
        {
            try
            {
                this.Invoke((MethodInvoker)delegate {
                    txtSearchProductName.Focus();
                    txtSearchProductName.SelectionStart = 0;
                    txtSearchProductName.SelectionLength = txtSearchProductName.Text.Length;
                });

                if (picLoader.InvokeRequired)
                {
                    picLoader.BeginInvoke(new MethodInvoker(delegate { picLoader.Visible = true; }));
                }

                if (StartIndex == 0 && ShowAll == true)
                {
                    ShowAll = false;
                    lstProductMasterModel = _ProductService.GetAllProduct("", false);
                    StartIndex = 0;
                    EndIndex = 50;
                    F_lstProductMasterModel = lstProductMasterModel.Where(o => o.RowNumber > StartIndex && o.RowNumber <= EndIndex).ToList();
                    M_lstProductMasterModel = new List<ProductMasterModel>();
                    M_lstProductMasterModel.AddRange(F_lstProductMasterModel);
                    setDataSource(M_lstProductMasterModel);
                }
                else if (txtSearchProductName.Text != "")
                {
                    SearchProduct(txtSearchProductName.Text.Trim());
                }

                if (picLoader.InvokeRequired)
                {
                    picLoader.BeginInvoke(new MethodInvoker(delegate { picLoader.Visible = false; }));
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void grdHideColumn()
        {
            try
            {
                IEnumerable<string> columnnames = null;
                if (IsFilterClick)
                {
                    foreach (var item in ProductGrdView.Columns)
                    {
                        foreach (object chkBoxListitem in chkBoxList.Items)
                        {
                            string FilterGridColMapName = GridColumnnameDispMapping(Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName), 0);
                            if (chkBoxList.Items.Contains(FilterGridColMapName))
                            {
                                if (chkBoxListitem.ToString() == FilterGridColMapName)
                                {
                                    if (chkBoxList.CheckedItems.Contains(chkBoxListitem))
                                    {
                                        ProductGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = true;
                                        ChangeGridColumnHeader(((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString());
                                    }
                                    else
                                    {
                                        if (ProductGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName].ToolTipText.ToString() == "")
                                        {
                                            ProductGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (ProductGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName].ToolTipText.ToString() == "")
                                {
                                    ProductGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = false;
                                }
                                break;
                            }

                        }
                    }
                    ProductGrdView.Refresh();
                }
                else
                {
                    columnnames = _ColumnFilterService.GetFilterColumnsList("frmProduct");

                    if (columnnames != null && columnnames.Count() > 0)
                    {
                        foreach (var item in ProductGrdView.Columns)
                        {
                            string FilterGridColMapName =  GridColumnnameDispMapping(Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName), 0);
                            if (columnnames.Contains(FilterGridColMapName))
                            {
                                ProductGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = true;
                                ChangeGridColumnHeader(((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString());
                            }
                            else
                            {
                                if (ProductGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName].ToolTipText.ToString() == "")
                                {
                                    ProductGrdView.Columns[((System.Windows.Forms.DataGridViewColumn)item).DataPropertyName.ToString()].Visible = false;
                                }
                            }

                        }
                    }
                    else
                    {
                        IsFilterColAvailable = false;
                        grdHideColumnsOrg();
                    }
                }
                if (ProductGrdView.Columns[0].Name == "")
                {
                    ProductGrdView.Columns.RemoveAt(0);
                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    imgEdit.ToolTipText = "Edit";
                    ProductGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    if (LoginInfo.RoleType == "Administrator")
                    {
                        if (ProductGrdView.Columns[0].Name == "")
                        {
                            ProductGrdView.Columns.RemoveAt(0);
                        }
                        DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                        var bmp = new Bitmap(Resources.delete);
                        imgDelete.Image = bmp;
                        imgDelete.ToolTipText = "Delete";
                        ProductGrdView.Columns.Add(imgDelete);
                        imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                }
                if (StartIndex == 0)
                {

                }
                else
                {
                    ProductGrdView.FirstDisplayedScrollingRowIndex = StartIndex;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }


        public void grdHideColumnsOrg()
        {
            try
            {
                ProductGrdView.Columns[ProductMasterModelCont.ProductID].Visible = false;
                ProductGrdView.Columns[DepartmentMasterModelCont.DepartmentID].Visible = false;
                ProductGrdView.Columns[SectionMasterModelCont.SectionID].Visible = false;
                ProductGrdView.Columns[UoMMasterModelCont.UnitMeasureID].Visible = false;
                ProductGrdView.Columns[TaxGroupMasterModelCont.TaxGroupID].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.CertCode].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.Image].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.IsActive].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.IsFoodStamp].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.IsGroupPrice].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.AgeVerification].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.IsScaled].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.TareWeight].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.LabeledPrice].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.GroupQty].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.GroupPrice].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.CaseQty].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.CasePrice].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.ProductVendorID].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.Pack].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.Size].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.SecondaryPLU].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.PalletQTY].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.UpdatedDate].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.CountryofOrigin].Visible = false;
                ProductGrdView.Columns[ProductMasterModelCont.VendorName].Visible = false;
                //ProductGrdView.Columns[ProductMasterModelCont.ProductName].Width = 400;
                ProductGrdView.Columns[ProductMasterModelCont.ProductName].Width = 300;
                ProductGrdView.Columns["ProductName"].HeaderText = "Name";
                ProductGrdView.Columns["DepartmentName"].HeaderText = "Department";
                ProductGrdView.Columns["UPCCode"].HeaderText = "UPC Code";
                ProductGrdView.Columns["SectionName"].HeaderText = "Section";
                ProductGrdView.Columns["UnitMeasureCode"].HeaderText = "Unit";
                ProductGrdView.Columns["TaxGroupName"].HeaderText = "Tax";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }
        public Image Base64ToImage(string base64String)
        {
            Image image = null;
            try
            {

                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                image = System.Drawing.Image.FromStream(ms, true);
            }
            catch (Exception ex)
            {

            }
            return image;
        }

        public void Clear()
        {
            ShowAll = true;
            PrimaryId = 0;
            this.first = false;
            Thread thread = new Thread(new ThreadStart(dataLoad));
            thread.Start();
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_ProductMaster");
        }

        private int GetDisplayedRowsCount()
        {
            int count = ProductGrdView.Rows[ProductGrdView.FirstDisplayedScrollingRowIndex].Height;
            count = ProductGrdView.Height / count;
            return count;
        }

        //public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        Thread.Sleep(100);
        //        InputData = ComPort.ReadExisting();
        //        if (InputData != String.Empty)
        //        {
        //            if (InvokeRequired)
        //                this.BeginInvoke(new SetTextCallback(SetText), new object[] { InputData });
        //            else
        //                SetText(InputData);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
        //    }
        //}

        //public void OpenPort()
        //{
        //    try
        //    {
        //        string[] Ports = SerialPort.GetPortNames();
        //        if (Ports.Count() > 0)
        //        {
        //            if (!ComPort.IsOpen)
        //            {
        //                ComPort.PortName = ComInfo.ComPort;
        //                ComPort.BaudRate = ComInfo.BaudRate;
        //                ComPort.DataBits = ComInfo.DataBits;
        //                ComPort.StopBits = ComInfo.StopBits;
        //                ComPort.Handshake = ComInfo.Handshake;
        //                ComPort.Parity = ComInfo.Parity;
        //                ComPort.RtsEnable = true;
        //                ComPort.DtrEnable = true;
        //                ComPort.Open();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
        //    }
        //}

        //public void PortOpen_Close(bool Status)
        //{
        //    try
        //    {
        //        string[] Ports = SerialPort.GetPortNames();
        //        if (Ports.Count() > 0)
        //        {
        //            if (Status)
        //            {
        //                if (!ComPort.IsOpen)
        //                {
        //                    ComPort.Open();
        //                }
        //            }
        //            else
        //            {
        //                if (ComPort.IsOpen)
        //                {
        //                    ComPort.Close();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
        //    }
        //}

        //private void SetText(string text)
        //{
        //    try
        //    {
        //        if (text.StartsWith("S08"))
        //        {
        //            if (text.Contains("E"))
        //            {
        //                text = text.Remove(0, 4);
        //                text = text.Remove(text.Length - 1);
        //            }
        //            else
        //            {
        //                text = text.Remove(0, 4);
        //                text = text.Remove(text.Length - 1);
        //                int Count = text.Length;
        //                if (Count < 13)
        //                {
        //                    Count = 13 - Count;
        //                    for (int i = 0; i < Count; i++)
        //                    {
        //                        text = "0" + text;
        //                    }
        //                }
        //            }
        //            txtSearchProductName.Text = text;
        //            SearchProduct(txtSearchProductName.Text);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
        //    }
        //}

        public void UpdateData(int RowIndex)
        {
            try
            {
                if (RowIndex > -1)
                {
                    //PortOpen_Close(false);
                    //DeviceRemove();
                    FrmMetro_AddProduct objFrmMetro_AddProduct = new FrmMetro_AddProduct();
                    objFrmMetro_AddProduct.txtProductName.Text = prod_Name;
                    if (ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UPCCode].Value != null)
                    {
                        objFrmMetro_AddProduct.txtUPCCode.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UPCCode].Value.ToString().Trim();
                    }
                    else
                    {
                        objFrmMetro_AddProduct.txtUPCCode.Text = CommonModelCont.EmptyString;
                    }
                    if (ProductGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.DepartmentID].Value != null)
                        objFrmMetro_AddProduct.cmbDepartment.SelectedValue = ProductGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.DepartmentID].Value;

                    if (ProductGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.SectionID].Value != null)
                        objFrmMetro_AddProduct.cmbSection.SelectedValue = ProductGrdView.Rows[RowIndex].Cells[SectionMasterModelCont.SectionID].Value;
                    if (ProductGrdView.Rows[RowIndex].Cells[UoMMasterModelCont.UnitMeasureID].Value != null)
                        objFrmMetro_AddProduct.cmbUoM.SelectedValue = ProductGrdView.Rows[RowIndex].Cells[UoMMasterModelCont.UnitMeasureID].Value;
                    if (ProductGrdView.Rows[RowIndex].Cells[TaxGroupMasterModelCont.TaxGroupID].Value != null)
                        objFrmMetro_AddProduct.cmbTaxGroup.SelectedValue = ProductGrdView.Rows[RowIndex].Cells[TaxGroupMasterModelCont.TaxGroupID].Value;
                    if (ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Image].Value != null && ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Image].Value.ToString() != CommonModelCont.EmptyString)
                    {
                        objFrmMetro_AddProduct.ImageProduct.Image = Base64ToImage(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Image].Value.ToString());
                    }
                    else
                    {
                        objFrmMetro_AddProduct.ImageProduct.Image = new Bitmap(Resources.sf_);
                    }
                    objFrmMetro_AddProduct.txtPrice.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Price].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Price].Value.ToString();
                    objFrmMetro_AddProduct.toggleFdStamp.Checked = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.IsFoodStamp].Value.ToString().ToLower());
                    objFrmMetro_AddProduct.toggleActive.Checked = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.IsActive].Value);
                    objFrmMetro_AddProduct.ToggleScaled.Checked = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.IsScaled].Value);
                    objFrmMetro_AddProduct.ToggleAgeVerify.Checked = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.AgeVerification].Value);
                    objFrmMetro_AddProduct.ToggleLabeled.Checked = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.LabeledPrice].Value);
                    objFrmMetro_AddProduct.ToggleGroupPrice.Checked = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.IsGroupPrice].Value);
                    objFrmMetro_AddProduct.txtTareWeight.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.TareWeight].Value == null ? "0" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.TareWeight].Value.ToString();
                    objFrmMetro_AddProduct.txtLinkUPCCode.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.LinkedUPCCode].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.LinkedUPCCode].Value.ToString();
                    objFrmMetro_AddProduct.txtGroupQty.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.GroupQty].Value == null ? "0" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.GroupQty].Value.ToString();
                    objFrmMetro_AddProduct.txtGroupPrice.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.GroupPrice].Value == null ? "0" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.GroupPrice].Value.ToString();
                    objFrmMetro_AddProduct.txtCaseQty.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CaseQty].Value == null ? "0" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CaseQty].Value.ToString();
                    objFrmMetro_AddProduct.txtCasePrice.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CasePrice].Value == null ? "0" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CasePrice].Value.ToString();
                    objFrmMetro_AddProduct.txtPack.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Pack].Value == null ? "0" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Pack].Value.ToString();
                    objFrmMetro_AddProduct.txtSize.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Size].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Size].Value.ToString();
                    objFrmMetro_AddProduct.txtSecondaryPLU.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.SecondaryPLU].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.SecondaryPLU].Value.ToString();
                    objFrmMetro_AddProduct.txtPalletQTY.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.PalletQTY].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.PalletQTY].Value.ToString();
                    objFrmMetro_AddProduct.txtCountryofOrig.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CountryofOrigin].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CountryofOrigin].Value.ToString();
                    objFrmMetro_AddProduct.txtFSEligibleAmount.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.FSEligibleAmount].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.FSEligibleAmount].Value.ToString();
                    objFrmMetro_AddProduct.FSEligibleAmount = objFrmMetro_AddProduct.txtFSEligibleAmount.Text;
                    objFrmMetro_AddProduct.Price = objFrmMetro_AddProduct.txtPrice.Text;
                    if (ProductGrdView.Columns.Contains(ProductMasterModelCont.UpdatedDate))
                    {
                        objFrmMetro_AddProduct.DtUpdateddate.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UpdatedDate].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UpdatedDate].Value.ToString();
                    }
                    //int VCount = GetVendorCount(PrimaryId);
                    //if (VCount > 1)
                    //{
                    //    objFrmMetro_AddProduct.cmbVendors.SelectedIndex = 1;
                    //}
                    //else
                    //{
                    if (ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.ProductVendorID].Value != null)
                        objFrmMetro_AddProduct.cmbVendors.SelectedValue = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.ProductVendorID].Value ;
                    //}
                    objFrmMetro_AddProduct.ProductID = PrimaryId;
                    objFrmMetro_AddProduct.metroBtnProductvendor.Enabled = true;
                    objFrmMetro_AddProduct.metroBtnSalePrice.Enabled = true;
                    var productvendorlist = FieldsDefaultVendor(objFrmMetro_AddProduct.ProductID);
                    //objFrmMetro_AddProduct.txtUnitCost.Text = productvendorlist == null ? "" : productvendorlist.UnitCost == null ? "" : productvendorlist.UnitCost.ToString();
                    //objFrmMetro_AddProduct.txtCertificateCode.Text = productvendorlist == null ? "" : String.IsNullOrEmpty(productvendorlist.ItemCode) ? "" : productvendorlist.ItemCode.ToString();
                    objFrmMetro_AddProduct.txtUnitCost.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UnitCost].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UnitCost].Value.ToString();
                    objFrmMetro_AddProduct.txtCertificateCode.Text = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CertCode].Value == null ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CertCode].Value.ToString();

                    objFrmMetro_AddProduct.ShowDialog();
                    txtSearchProductName.Focus();
                    txtSearchProductName.SelectionStart = 0;
                    txtSearchProductName.SelectionLength = txtSearchProductName.Text.Length;
                    //PortOpen_Close(true);
                    //DeviceAdd();
                    //picLoader.Visible = true;
                    this.first = false;
                    Thread thread = new Thread(new ThreadStart(dataLoad));
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public ProductVendorMasterModel FieldsDefaultVendor(long ProductId)
        {
            try
            {
                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                lstProductVendorMasterModel = _ProductVendorService.GetAllProductVendor(ProductId);
                var productVendorByDefault = lstProductVendorMasterModel.Where(l => l.IsDefault == true).FirstOrDefault();
                return productVendorByDefault;

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());

            }
            return null;
        }

        public int GetVendorCount(long PrimaryId)
        {
            DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
            int count = _db.tbl_ProductVendorMaster.Where(x => x.ProductID == PrimaryId && x.IsDelete == false).Count();
            return count;
        }

        public void SearchProduct(string searchStr)
        {
            try
            {
                string OriginalUPC = searchStr;
                if (searchStr != CommonModelCont.EmptyString && searchStr != null && searchStr != AlertMessages.ProductSearch)
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

                    M_lstProductMasterModel = _ProductService.GetAllProduct(searchStr, false);
                    if (M_lstProductMasterModel.Count > 0)
                    {
                        setDataSource(M_lstProductMasterModel);

                        if (ProductGrdView.RowCount == 0)
                        {
                            if (searchStr.All(char.IsDigit))
                            {
                                string[] UPC_E = new string[2];
                                UPC_E[0] = OriginalUPC;
                                UPC_E[0] = Functions.GetUPC_E(UPC_E[0].ToString());
                                UPC_E[1] = UPC_E[0];
                                if (UPC_E[1] != "")
                                {
                                    //M_lstProductMasterModel = lstProductMasterModel.Where(o => o.ProductName.ToLower().StartsWith(UPC_E[1].ToLower()) || o.UPCCode.StartsWith(UPC_E[1].ToLower())).ToList();
                                    M_lstProductMasterModel = _ProductService.GetAllProduct(UPC_E[1], false);
                                }
                                setDataSource(M_lstProductMasterModel);
                            }
                        }
                        if (ProductGrdView.RowCount == 0)
                        {
                            if (searchStr.All(char.IsDigit))
                            {
                                string LastPrice = searchStr.Substring(searchStr.Length - 5, 5);
                                searchStr = searchStr.Replace(LastPrice, "00000");
                                //M_lstProductMasterModel = lstProductMasterModel.Where(o => o.ProductName.ToLower().StartsWith(searchStr.ToLower()) || o.UPCCode.StartsWith(searchStr.ToLower())).ToList();
                                M_lstProductMasterModel = _ProductService.GetAllProduct(searchStr, false);
                                setDataSource(M_lstProductMasterModel);
                            }
                        }
                        if (InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                grdHideColumn();
                            }));
                        }
                        else
                        {
                            grdHideColumn();
                        }
                        IsSearchFilterClick = false;
                        //if (!IsFilterColAvailable)
                        //{
                        //    ClsCommon.MsgBox("Information", "Please contact to Administrator !!!", false);
                        //}
                    }
                    else
                    {
                        ProductGrdView.DataSource = null;
                        ProductGrdView.Columns.Clear();
                        txtSearchProductName.Text = string.Empty;
                        txtSearchProductName.Focus();
                        txtSearchProductName.SelectionStart = 0;
                        txtSearchProductName.SelectionLength = txtSearchProductName.Text.Length;
                        ClsCommon.MsgBox("Information", "No product found.", false);
                    }
                }
                else
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void DeviceRemove()
        {
            try
            {
                if (XMLData.ScannerInUsed)
                {
                    if (myScanner != null && myScanner.DataEventEnabled == true)
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
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
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbSectionName(int departmentid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region IMPORT/EXPORT

        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HRD=Yes'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'";

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                string filePath = openFileDialog1.FileName;
                string extension = Path.GetExtension(filePath);
                string conStr, sheetName;
                int count = 0;
                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath);
                        break;
                }

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(dt);
                            con.Close();

                            DepartmentService _DepartmentService = new DepartmentService();
                            DataTable uniqueDepartment = dt.DefaultView.ToTable(true, "DepartmentName");
                            string SName = "";
                            for (int row = 0; row < uniqueDepartment.Rows.Count; row++)
                            {
                                #region Check Department
                                bool IsDepartment = _DepartmentService.CheckName(uniqueDepartment.Rows[row]["DepartmentName"].ToString());
                                if (!IsDepartment)
                                {
                                    if (SName == "")
                                    {
                                        SName = uniqueDepartment.Rows[row]["DepartmentName"].ToString();
                                    }
                                    else
                                    {
                                        SName = ", " + uniqueDepartment.Rows[row]["DepartmentName"].ToString();
                                    }
                                }
                                #endregion
                            }
                            if (SName == "")
                            {
                                SectionService _SectionService = new SectionService();
                                DataTable uniqueSection = dt.DefaultView.ToTable(true, "SectionName");
                                string SectionName = "";
                                for (int row = 0; row < uniqueSection.Rows.Count; row++)
                                {
                                    #region Check Section
                                    bool IsRole = _SectionService.CheckName(uniqueSection.Rows[row]["SectionName"].ToString());
                                    if (!IsRole)
                                    {
                                        if (SectionName == "")
                                        {
                                            SectionName = uniqueSection.Rows[row]["SectionName"].ToString();
                                        }
                                        else
                                        {
                                            SectionName = ", " + uniqueSection.Rows[row]["SectionName"].ToString();
                                        }
                                    }
                                    #endregion
                                }
                                if (SectionName == "")
                                {
                                    TaxGroupService _TaxGroupService = new TaxGroupService();
                                    DataTable uniqueTaxGroup = dt.DefaultView.ToTable(true, "TaxGroupName");
                                    string TaxGroup = "";
                                    for (int row = 0; row < uniqueTaxGroup.Rows.Count; row++)
                                    {
                                        #region Check TaxGroup
                                        bool IsRole = _TaxGroupService.CheckName(uniqueTaxGroup.Rows[row]["TaxGroupName"].ToString());
                                        if (!IsRole)
                                        {
                                            if (TaxGroup == "")
                                            {
                                                TaxGroup = uniqueTaxGroup.Rows[row]["TaxGroupName"].ToString();
                                            }
                                            else
                                            {
                                                TaxGroup = ", " + uniqueTaxGroup.Rows[row]["TaxGroupName"].ToString();
                                            }
                                        }
                                        #endregion
                                    }
                                    if (TaxGroup == "")
                                    {
                                        UoMService _UoMService = new UoMService();
                                        DataTable uniqueUoM = dt.DefaultView.ToTable(true, "UnitMeasureCode");
                                        string UoM = "";
                                        for (int row = 0; row < uniqueUoM.Rows.Count; row++)
                                        {
                                            #region Check UoM
                                            bool IsRole = _UoMService.CheckName(uniqueUoM.Rows[row]["UnitMeasureCode"].ToString());
                                            if (!IsRole)
                                            {
                                                if (UoM == "")
                                                {
                                                    UoM = uniqueUoM.Rows[row]["UnitMeasureCode"].ToString();
                                                }
                                                else
                                                {
                                                    UoM = ", " + uniqueUoM.Rows[row]["UnitMeasureCode"].ToString();
                                                }
                                            }
                                            #endregion
                                        }
                                        if (UoM == "")
                                        {
                                            #region Add UoM
                                            for (int row = 0; row < dt.Rows.Count; row++)
                                            {
                                                long DepartmentID = 0;
                                                long SectionID = 0;
                                                long TaxGroupID = 0;
                                                long UnitMeasureID = 0;

                                                DepartmentID = _DepartmentService.GetDepartmentID(dt.Rows[row]["DepartmentName"].ToString());
                                                SectionID = _SectionService.GetSectionID(dt.Rows[row]["SectionName"].ToString());
                                                TaxGroupID = _TaxGroupService.GetTaxGroupID(dt.Rows[row]["TaxGroupName"].ToString());
                                                UnitMeasureID = _UoMService.GetUnitMeasureID(dt.Rows[row]["UnitMeasureCode"].ToString());
                                                if (DepartmentID != 0 && SectionID != 0 && TaxGroupID != 0 && UnitMeasureID != 0)
                                                {
                                                    EmployeeMasterModel objEmployeeMasterModel = new EmployeeMasterModel();
                                                    bool IsProduct = _ProductService.CheckName(dt.Rows[row]["ProductName"].ToString());
                                                    if (!IsProduct)
                                                    {
                                                        count++;
                                                        objProductMasterModel.UPCCode = dt.Rows[row]["UPCCode"].ToString().Trim();
                                                        objProductMasterModel.ProductName = dt.Rows[row]["ProductName"].ToString().Trim();
                                                        objProductMasterModel.CertCode = dt.Rows[row]["CertCode"].ToString().Trim();
                                                        objProductMasterModel.UnitMeasureID = UnitMeasureID;
                                                        objProductMasterModel.Price = Convert.ToDecimal(dt.Rows[row]["Price"].ToString().Trim());
                                                        objProductMasterModel.IsFoodStamp = Convert.ToBoolean(dt.Rows[row]["IsFoodStamp"].ToString());
                                                        objProductMasterModel.DepartmentID = DepartmentID;
                                                        objProductMasterModel.SectionID = SectionID;
                                                        objProductMasterModel.TaxGroupID = TaxGroupID;
                                                        var add = _ProductService.AddProduct(objProductMasterModel, 1);
                                                    }
                                                }
                                                else
                                                {
                                                    ClsCommon.MsgBox("Information", dt.Rows[row]["ProductName"].ToString() + " Product is not available!", false);
                                                }
                                            }
                                            ClsCommon.MsgBox("Information", "Total " + count + " Product imported successfully.!", false);
                                            #endregion
                                            Thread thread = new Thread(new ThreadStart(dataLoad));
                                            thread.Start();
                                        }
                                        else
                                        {
                                            ClsCommon.MsgBox("Information", UoM + " UoM is not available!", false);
                                        }
                                    }
                                    else
                                    {
                                        ClsCommon.MsgBox("Information", TaxGroup + " TaxGroup is not available!", false);
                                    }
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", SectionName + " Section is not available!", false);
                                }
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", SName + " Department is not available!", false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
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
                sfd.FileName = "Product";
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
                        wb.Worksheets.Add(dt, "Product");
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
            Dictionary<string, string> lstcol = new Dictionary<string, string>();
            try
            {
                //dt.Columns.Add(ProductMasterModelCont.ProductName, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.UPCCode, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.CertCode, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.Price, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.IsActive, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.DepartmentName, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.SectionName, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.UnitMeasureCode, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.TaxGroupName, typeof(string));
                //dt.Columns.Add(ProductMasterModelCont.IsFoodStamp, typeof(string));

                //foreach (var item in lstProductMasterModel)
                //{
                //    DataRow dr = dt.NewRow();

                //    dr[ProductMasterModelCont.ProductName] = item.ProductName;
                //    dr[ProductMasterModelCont.UPCCode] = item.UPCCode;
                //    dr[ProductMasterModelCont.CertCode] = item.CertCode;
                //    dr[ProductMasterModelCont.Price] = item.Price;
                //    dr[ProductMasterModelCont.IsActive] = item.IsActive;
                //    dr[ProductMasterModelCont.DepartmentName] = item.DepartmentName;
                //    dr[ProductMasterModelCont.SectionName] = item.SectionName;
                //    dr[ProductMasterModelCont.UnitMeasureCode] = item.UnitMeasureCode;
                //    dr[ProductMasterModelCont.TaxGroupName] = item.TaxGroupName;
                //    dr[ProductMasterModelCont.IsFoodStamp] = item.IsFoodStamp;
                //    dt.Rows.Add(dr);
                //}
                foreach (var item in ProductGrdView.Columns)
                {
                    if (((System.Windows.Forms.DataGridViewColumn)item).Visible && ((System.Windows.Forms.DataGridViewColumn)item).Name != "")
                    {
                        string colhname = Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).HeaderText);
                        string colname = Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).Name);
                        lstcol.Add(colhname, colname);
                        dt.Columns.Add(colhname, typeof(string));
                    }
                }

                //foreach (var item in lstProductMasterModel)
                //{
                //    DataRow dr = dt.NewRow();

                //    dr[ProductMasterModelCont.ProductName] = item.ProductName;
                //    dr[ProductMasterModelCont.UPCCode] = item.UPCCode;
                //    dr[ProductMasterModelCont.CertCode] = item.CertCode;
                //    dr[ProductMasterModelCont.Price] = item.Price;
                //    dr[ProductMasterModelCont.IsActive] = item.IsActive;
                //    dr[ProductMasterModelCont.DepartmentName] = item.DepartmentName;
                //    dr[ProductMasterModelCont.SectionName] = item.SectionName;
                //    dr[ProductMasterModelCont.UnitMeasureCode] = item.UnitMeasureCode;
                //    dr[ProductMasterModelCont.TaxGroupName] = item.TaxGroupName;
                //    dr[ProductMasterModelCont.IsFoodStamp] = item.IsFoodStamp;
                //    dt.Rows.Add(dr);
                //}
                foreach (var item in ProductGrdView.Columns)
                {
                    if (((System.Windows.Forms.DataGridViewColumn)item).Visible && ((System.Windows.Forms.DataGridViewColumn)item).Name != "")
                    {
                        string colhname = Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).HeaderText);
                        string colname = Convert.ToString(((System.Windows.Forms.DataGridViewColumn)item).Name);
                        lstcol.Add(colhname, colname);
                        dt.Columns.Add(colhname, typeof(string));
                    }
                }

                foreach (var item in lstProductMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (lstcol.ContainsKey(dt.Columns[i].ColumnName))
                        {
                            string propname = lstcol.Where(w => w.Key == dt.Columns[i].ColumnName).Select(s => s.Value).FirstOrDefault().ToString();
                            dr[dt.Columns[i].ColumnName] = item.GetPropertyValue(propname);
                        }


                    }
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }


        #endregion

        private void btnColumnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                ColumnFilterpanel.Visible = true;
                ColumnFilterpanel.BringToFront();

                var columnnames = _ColumnFilterService.GetFilterColumnsList("frmProduct");
                for (int i = 0; i < chkBoxList.Items.Count; i++)
                {
                    if (PreviousColumnFilterList != null && PreviousColumnFilterList.Count > 0)
                    {
                        foreach (KeyValuePair<string, bool> kvp in PreviousColumnFilterList)
                        {
                            if (chkBoxList.Items[i].ToString() == kvp.Key)
                            {
                                if (isResetFilter)
                                {
                                    if (ProductGrdView != null && ProductGrdView.Rows.Count > 0)
                                    {
                                        if (ProductGrdView.Columns.Contains(chkBoxList.Items[i].ToString()) == true && ProductGrdView.Columns[chkBoxList.Items[i].ToString()].Visible == true)
                                        {
                                            chkBoxList.SetItemChecked(i, kvp.Value);
                                        }
                                    }
                                }
                                else
                                {
                                    if (columnnames.Contains(chkBoxList.Items[i].ToString()))
                                    {
                                        chkBoxList.SetItemChecked(i, kvp.Value);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (columnnames.Contains(chkBoxList.Items[i].ToString()))
                        {
                            chkBoxList.SetItemChecked(i, true);
                        }
                        else
                        {
                            chkBoxList.SetItemChecked(i, false);
                        }
                    }
                }
                bool IsSelected = false;
                for (int i = 0; i < cmbSearchFilter.Items.Count; i++)
                {
                    string ccbcolval = cmbSearchFilter.Items[i].ToString();
                    if (lstSearchFilterCols.Count > 0 && lstSearchFilterCols.Contains(ccbcolval))
                    {
                        cmbSearchFilter.SelectedIndex = i;
                        IsSelected = true;
                    }
                    if(!IsSelected)
                    {
                        cmbSearchFilter.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            ColumnFilterpanel.Visible = false;
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAppliedfilter();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SaveAppliedfilter()
        {
            try
            {
                IsFilterClick = true;
                cmbSearchFilter.Items.Clear();
                cmbSearchFilter.Items.Add("--Please Select--");
                if (ProductGrdView != null && ProductGrdView.Rows.Count > 0)
                {
                    grdHideColumn();
                }
                isResetFilter = false;
                ColumnFilterpanel.Visible = false;
                bool ischkboxitemchecked = false;
                PreviousColumnFilterList = new Dictionary<string, bool>();
                for (int i = 0; i < chkBoxList.Items.Count; i++)
                {
                    string chklistname = chkBoxList.Items[i].ToString();
                    if (chkBoxList.GetItemChecked(i))
                    {
                        if (ProductGrdView != null && ProductGrdView.Columns.Contains(chklistname) == true && ProductGrdView.Columns[chklistname].Visible == true)
                        {
                            chkBoxList.SetItemChecked(i, true);
                        }
                        //CCBoxItem itemcc = new CCBoxItem(chklistname, i);
                        if (chkBoxList.Items[i].ToString() != "UpdatedDate" && chkBoxList.Items[i].ToString() != "CreatedDate")
                        {
                            cmbSearchFilter.Items.Add(chklistname);
                        }
                        PreviousColumnFilterList.Add(chklistname, true);
                        ischkboxitemchecked = true;
                    }
                    else
                    {
                        chkBoxList.SetItemChecked(i, false);
                        PreviousColumnFilterList.Add(chklistname, false);
                    }
                }
                //ccb.MaxDropDownItems = 5;
                //ccb.DisplayMember = "Name";
                //ccb.ValueSeparator = ", ";
                cmbSearchFilter.SelectedIndex = 0;
                if (!ischkboxitemchecked)
                {
                    ClsCommon.MsgBox("Information", "Select atleast one column filter !!!", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void bindColumnFiltersChecklistBox()
        {
            try
            {
                //New 
                chkBoxList.Items.Clear();
                cmbSearchFilter.Items.Clear();
                cmbSearchFilter.Items.Add("--Please Select--");
                IEnumerable<string> columnnames = null;
                IEnumerable<string> SelectedFilterCol = null;
                if (lstFilterBoxData.Count == 0)
                {
                    lstFilterBoxData = _ColumnFilterService.GetFilterMasterColumnsList("frmProduct");
                }
                SelectedFilterCol = _ColumnFilterService.GetFilterColumnsList("frmProduct");
                columnnames = lstFilterBoxData.Select(s => s.FilterColumnsName).ToList();
                //IEnumerable<string> second = new[] { "DepartmentName", "SectionName", "UnitMeasureCode", "TaxGroupName" };
                //columnnames = columnnames.Concat(second);

                int i = 0;

                foreach (var item in columnnames)
                {
                    if (item.ToString() != "UpdatedDate" && item.ToString() != "CreatedDate")
                    {
                        chkBoxList.Items.Add(item.ToString());
                        if (SelectedFilterCol != null && SelectedFilterCol.Count() > 0)
                        {
                            if (SelectedFilterCol.Contains(item))
                            {
                                chkBoxList.SetItemChecked(i, true);
                                cmbSearchFilter.Items.Add(Convert.ToString(item));
                                i++;
                            }
                            else
                            {
                                chkBoxList.SetItemChecked(i, false);
                            }
                        }
                    }
                }
                //ccb.MaxDropDownItems = 5;
                //ccb.DisplayMember = "Name";
                //ccb.ValueSeparator = ", ";
                cmbSearchFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ccb_DropDownClosed(object sender, EventArgs e)
        {
            txtOut.AppendText("DropdownClosed\r\n");
            //txtOut.AppendText(string.Format("value changed: {0}\r\n", ccb.ValueChanged));
            txtOut.AppendText(string.Format("value: {0}\r\n", cmbSearchFilter.Text));
            // Display all checked items.
            StringBuilder sb = new StringBuilder("Items checked: ");
            //foreach (CCBoxItem item in ccb.CheckedItems)
            //{
            //    sb.Append(item.Name).Append(ccb.ValueSeparator);
            //}
            //sb.Remove(sb.Length - ccb.ValueSeparator.Length, ccb.ValueSeparator.Length);
            txtOut.AppendText(sb.ToString());
            txtOut.AppendText("\r\n");
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchFilter.Text))
                {
                    if (cmbSearchFilter.SelectedIndex != 0)
                    {
                        //if (PreviousColumnFilterList != null && PreviousColumnFilterList.Count > 0)
                        //{
                        //    SaveAppliedfilter();
                        //}
                        bool isNumeric, isBool, isDecimal;
                        int numericval = 0;
                        decimal decimalval = 0;
                        bool booleanval = false;

                        isNumeric = int.TryParse(txtSearchFilter.Text.Trim(), out numericval);
                        isBool = bool.TryParse(txtSearchFilter.Text.Trim(), out booleanval);
                        isDecimal = decimal.TryParse(txtSearchFilter.Text.Trim(), out decimalval);

                        DataTable st = ClsCommon.ListToDataTable(SearchFilter_ProductMasterModel);
                        DataView dv = new DataView(st);
                        var kd = string.Empty;
                        lstSearchFilterCols.Clear();
                        //foreach (CCBoxItem item in ccb.CheckedItems)
                        //{
                        string selectedname = Convert.ToString(cmbSearchFilter.SelectedItem);
                        string tablecolval = GridColumnnameDispMapping(selectedname, 1);
                        string type = dv.Table.Columns[tablecolval].DataType.ToString();
                        lstSearchFilterCols.Add(selectedname);
                        if (type.ToLower().Contains("int") && isNumeric)
                        {
                            if (kd != "")
                            {
                                kd += " OR ";
                            }
                            kd += string.Format(tablecolval + "={0}", txtSearchFilter.Text);
                        }
                        else if (type.ToLower().Contains("decimal") && isDecimal)
                        {
                            if (kd != "")
                            {
                                kd += " OR ";
                            }
                            kd += string.Format(tablecolval + "= {0}", txtSearchFilter.Text);

                        }
                        else if (type.ToLower().Contains("bit") && isBool)
                        {
                            if (kd != "")
                            {
                                kd += " OR ";
                            }
                            kd += string.Format(tablecolval + "= {0}", txtSearchFilter.Text);
                        }
                        else if (type.ToLower().Contains("string"))
                        {
                            if (kd != "")
                            {
                                kd += " OR ";
                            }
                            kd += string.Format(tablecolval + " LIKE '%{0}%'", txtSearchFilter.Text);
                        }
                        //}
                        if (!string.IsNullOrEmpty(kd))
                        {
                            kd = "AND " + kd;
                            SearchFilter_ProductMasterModel = _ProductService.GetAllProduct_With_Paging(kd, 1);
                            ProductGrdView.DataSource = null;
                            first = false;
                            setDataSource(SearchFilter_ProductMasterModel);
                            OG_SearchFilterText = kd;
                            //txtSearchFilter.Text = string.Empty;
                            txtSearchProductName.Text = string.Empty;
                            ColumnFilterpanel.Visible = false;
                            IsSearchFilterClick = true;

                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "No result found !!!", false);
                        }
                    }
                    else
                    {
                        ClsCommon.MsgBox("Information", "Selete atleast one search filter !!!", false);
                    }

                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please enter text search filter !!!", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnSaveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var columnnames = _ColumnFilterService.GetFilterColumnsList("frmProduct");

                for (int i = 0; i < chkBoxList.Items.Count; i++)
                {
                    tbl_FilterColumnsModel Obj = new tbl_FilterColumnsModel();

                    if (columnnames.Contains(chkBoxList.Items[i].ToString()))
                    {
                        Obj.FilterColumnsName = chkBoxList.Items[i].ToString();
                        Obj.FilterPageName = "frmProduct";
                        if (chkBoxList.GetItemChecked(i))
                        {
                            Obj.CreatedBy = LoginInfo.UserId;
                            Obj.CreatedDate = DateTime.Now;
                            Obj.IsActive = true;
                        }
                        else
                        {
                            Obj.CreatedBy = LoginInfo.UserId;
                            Obj.UpdatedBy = LoginInfo.UserId;
                            Obj.UpdatedDate = DateTime.Now;
                            Obj.IsActive = false;
                        }
                        Obj.IsMaster = false;
                        if (lstFilterBoxData.Any(w => w.FilterColumnsName == Obj.FilterColumnsName))
                            Obj.SeqNo = lstFilterBoxData.Where(w => w.FilterColumnsName == Obj.FilterColumnsName).Select(s => s.KeySeq).FirstOrDefault();
                        else
                            Obj.SeqNo = 0;
                        _ColumnFilterService.AddProductColumnFilter(Obj);
                    }
                    else
                    {
                        if (chkBoxList.GetItemChecked(i))
                        {
                            Obj.FilterColumnsName = chkBoxList.Items[i].ToString();
                            Obj.FilterPageName = "frmProduct";
                            Obj.CreatedBy = LoginInfo.UserId;
                            Obj.CreatedDate = DateTime.Now;
                            Obj.IsActive = true;
                            Obj.IsMaster = false;
                            if (lstFilterBoxData.Any(w => w.FilterColumnsName == Obj.FilterColumnsName))
                                Obj.SeqNo = lstFilterBoxData.Where(w => w.FilterColumnsName == Obj.FilterColumnsName).Select(s => s.KeySeq).FirstOrDefault();
                            else
                                Obj.SeqNo = 0;

                            _ColumnFilterService.AddProductColumnFilter(Obj);
                        }
                    }
                }
                if (ProductGrdView != null && ProductGrdView.Rows.Count > 0)
                {
                    grdHideColumn();
                }
                bindColumnFiltersChecklistBox();
                ColumnFilterpanel.Visible = false;

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ChangeGridColumnHeader(string columnName)
        {
            if(ProductGrdView.Columns.Contains(columnName))
            {
                ProductGrdView.Columns[columnName].HeaderText = GridColumnnameDispMapping(columnName,0);
            }
        }

        private void btnEditList_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductGrdView != null && ProductGrdView.Rows.Count > 0)
                {
                    IsEditMode = true;
                    ProductGrdView.CellValueChanged += ProductGrdView_CellValueChanged;
                    btnExport.Enabled = false;
                    btnImport.Enabled = false;
                    btnColumnFilter.Enabled = false;

                    DataTable dt = new DataTable();
                    for (int i = 0; i < ProductGrdView.ColumnCount; i++)
                    {
                        if (ProductGrdView.Columns[i].Visible == true)
                        {
                            if (!string.IsNullOrEmpty(ProductGrdView.Columns[i].Name.ToString()))
                            {
                                dt.Columns.Add(ProductGrdView.Columns[i].Name);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ProductGrdView.Columns[i].Name.ToString()) && (ProductGrdView.Columns[i].Name.ToString() == "ProductID" || ProductGrdView.Columns[i].Name.ToString() == "DepartmentID" || ProductGrdView.Columns[i].Name.ToString() == "SectionID" || ProductGrdView.Columns[i].Name.ToString() == "TaxGroupID" || ProductGrdView.Columns[i].Name.ToString() == "UnitMeasureID" || ProductGrdView.Columns[i].Name.ToString() == "ProductVendorID" || ProductGrdView.Columns[i].Name.ToString() == "UPCCode"))
                            {
                                dt.Columns.Add(ProductGrdView.Columns[i].Name);
                            }
                        }
                    }

                    EditablecolumnNames = (from dc in dt.Columns.Cast<DataColumn>()
                                           select dc.ColumnName).ToArray();
                    List<string> lstcol = new List<string>() { "DepartmentID", "ProductID", "ProductVendorID", "SectionID", "TaxGroupID", "UnitMeasureID" };
                    List<string> lstcollist = new List<string>() { "DepartmentName", "SectionName", "UnitMeasureCode", "TaxGroupName", "VendorName" };
                    for (int i = 0; i < ProductGrdView.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        foreach (var item in EditablecolumnNames)
                        {
                            if (lstcollist.Contains(item))
                            {
                                switch (item)
                                {
                                    case "DepartmentName":
                                        dr[item] = ProductGrdView.Rows[i].Cells[item].Value == null ? DepartmentMasterModelCont.cmbDepartmentFirst : Convert.ToString(ProductGrdView.Rows[i].Cells[item].Value).Trim();
                                        break;
                                    case "SectionName":
                                        dr[item] = ProductGrdView.Rows[i].Cells[item].Value == null ? SectionMasterModelCont.cmbSectionFirst : Convert.ToString(ProductGrdView.Rows[i].Cells[item].Value).Trim();
                                        break;
                                    case "UnitMeasureCode":
                                        dr[item] = ProductGrdView.Rows[i].Cells[item].Value == null ? UoMMasterModelCont.cmbUoMFirst : Convert.ToString(ProductGrdView.Rows[i].Cells[item].Value).Trim();
                                        break;
                                    case "TaxGroupName":
                                        dr[item] = ProductGrdView.Rows[i].Cells[item].Value == null ? TaxGroupMasterModelCont.cmbTaxGroupCodeFirst : Convert.ToString(ProductGrdView.Rows[i].Cells[item].Value).Trim();
                                        break;
                                    case "VendorName":
                                        dr[item] = ProductGrdView.Rows[i].Cells[item].Value == null ? VendorMasterModelCont.cmbVendorFirst : Convert.ToString(ProductGrdView.Rows[i].Cells[item].Value).Trim();
                                        break;
                                }
                            }
                            else
                            {
                                dr[item] = ProductGrdView.Rows[i].Cells[item].Value == null && lstcol.Contains(item) ? "0" : Convert.ToString(ProductGrdView.Rows[i].Cells[item].Value).Trim();
                            }
                        }
                        dt.Rows.Add(dr);
                    }

                    ProductGrdView.Columns.Clear();

                    var ProductColCon = getGridColumnType();
                    int index = 0;
                    foreach (var item in ProductColCon)
                    {
                        if (EditablecolumnNames.Contains(item.ColumnName))
                        {

                            switch (item.valueName)
                            {
                                case "Text":
                                    DataGridViewTextBoxColumn curColText = new DataGridViewTextBoxColumn();
                                    curColText.Name = item.ColumnName;
                                    curColText.HeaderText = item.HeaderName;
                                    curColText.DataPropertyName = item.ColumnName;
                                    ProductGrdView.Columns.Insert(index, curColText);
                                    break;
                                case "CheckBox":
                                    DataGridViewCheckBoxColumn curColCheck = new DataGridViewCheckBoxColumn();
                                    curColCheck.Name = item.ColumnName;
                                    curColCheck.HeaderText = item.HeaderName;
                                    curColCheck.DataPropertyName = item.ColumnName;
                                    ProductGrdView.Columns.Insert(index, curColCheck);
                                    break;
                                case "ComboBox":
                                    DataGridViewComboBoxColumn comboBC = new DataGridViewComboBoxColumn();
                                    comboBC.Name = item.ColumnName;
                                    comboBC.HeaderText = item.HeaderName;
                                    comboBC = bindGridViewcombo(comboBC, index);
                                    ProductGrdView.Columns.Insert(index, comboBC);
                                    break;
                            }
                            index++;
                        }
                    }

                    ProductGrdView.DataSource = dt;
                    FillComboBoxVal();
                    ProductGrdView.Columns["ProductVendorID"].Visible = false;
                    ProductGrdView.Columns["ProductID"].Visible = false;
                    ProductGrdView.Columns["DepartmentID"].Visible = false;
                    ProductGrdView.Columns["SectionID"].Visible = false;
                    ProductGrdView.Columns["TaxGroupID"].Visible = false;
                    ProductGrdView.Columns["UnitMeasureID"].Visible = false;

                    if (ProductGrdView.Columns.Contains("UpdatedDate"))
                        ProductGrdView.Columns["UpdatedDate"].Visible = false;
                    if (ProductGrdView.Columns.Contains("CreatedDate"))
                        ProductGrdView.Columns["CreatedDate"].Visible = false;

                    for (int i = 0; i < ProductGrdView.ColumnCount; i++)
                    {
                        ProductGrdView.Columns[i].ReadOnly = false;
                    }
                }
                else
                {
                    ClsCommon.MsgBox(AlertMessages.InformationAlert, "Product cart is empty.", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private List<productMasterModelCount> getGridColumnType()
        {
            List<productMasterModelCount> ps = new List<productMasterModelCount>() {
                new productMasterModelCount() { ColumnName = "CertCode" , valueName = "Text" , HeaderName = "Cert Code" },
                new productMasterModelCount() { ColumnName = "ProductName" , valueName = "Text" , HeaderName = "Name" },
                new productMasterModelCount() { ColumnName = "UPCCode" , valueName = "Text" , HeaderName = "UPC Code" },
                new productMasterModelCount() { ColumnName = "UPC_Code" , valueName = "Text" , HeaderName = "UPC_Code" },
                new productMasterModelCount() { ColumnName = "DepartmentName" , valueName = "ComboBox" , HeaderName = "Department" },
                new productMasterModelCount() { ColumnName = "SectionName" , valueName = "ComboBox" , HeaderName = "Section" },
                new productMasterModelCount() { ColumnName = "UnitMeasureCode" , valueName = "ComboBox" , HeaderName = "Unit" },
                new productMasterModelCount() { ColumnName = "TaxGroupName" , valueName = "ComboBox" , HeaderName = "Tax" },
                new productMasterModelCount() { ColumnName = "LinkedUPCCode" , valueName = "Text" , HeaderName = "Linked UPCCode" },
                new productMasterModelCount() { ColumnName = "Price" , valueName = "Text" , HeaderName = "Price" },
                new productMasterModelCount() { ColumnName = "IsActive" , valueName = "CheckBox" , HeaderName = "IsActive" },
                new productMasterModelCount() { ColumnName = "IsFoodStamp" , valueName = "CheckBox" , HeaderName = "Food Stamp" },
                new productMasterModelCount() { ColumnName = "IsScaled" , valueName = "CheckBox" , HeaderName = "Scaled" },
                new productMasterModelCount() { ColumnName = "TareWeight" , valueName = "Text" , HeaderName = "TareWeight" },
                new productMasterModelCount() { ColumnName = "GroupQty" , valueName = "Text" , HeaderName = "GroupQty" },
                new productMasterModelCount() { ColumnName = "GroupPrice" , valueName = "Text" , HeaderName = "GroupPrice" },
                new productMasterModelCount() { ColumnName = "LabeledPrice" , valueName = "Text" , HeaderName = "Labeled" },
                new productMasterModelCount() { ColumnName = "CaseQty" , valueName = "Text" , HeaderName = "CaseQty" },
                new productMasterModelCount() { ColumnName = "CasePrice" , valueName = "Text" , HeaderName = "CasePrice" },
                new productMasterModelCount() { ColumnName = "UnitCost" , valueName = "Text" , HeaderName = "Unit Cost" },
                new productMasterModelCount() { ColumnName = "VendorName" , valueName = "ComboBox" , HeaderName = "Vendor Name" },
                new productMasterModelCount() { ColumnName = "IsGroupPrice" , valueName = "CheckBox" , HeaderName = "Group Price" },
                new productMasterModelCount() { ColumnName = "Pack" , valueName = "Text" , HeaderName = "Pack" },
                new productMasterModelCount() { ColumnName = "Size" , valueName = "Text" , HeaderName = "Size" },
                new productMasterModelCount() { ColumnName = "SecondaryPLU" , valueName = "Text" , HeaderName = "Secondary PLU" },
                new productMasterModelCount() { ColumnName = "AgeVerification" , valueName = "CheckBox" , HeaderName = "AgeVerification" },
                new productMasterModelCount() { ColumnName = "PalletQTY" , valueName = "Text" , HeaderName = "Pallet QTY" },
                new productMasterModelCount() { ColumnName = "CountryofOrigin" , valueName = "Text" , HeaderName = "Country of Origin" }

        };
            return ps;
        }

        private DataGridViewComboBoxColumn bindGridViewcombo(DataGridViewComboBoxColumn obj,int i=0)
        {
            try
            {
                switch (obj.Name)
                {
                    case "DepartmentName":

                        List<DepartmentMasterModel> lstDepartmentMasterModel = new List<DepartmentMasterModel>();
                        DepartmentService _DepartmentService = new DepartmentService();

                        lstDepartmentMasterModel = _DepartmentService.GetAllDepartment();
                        lstDepartmentMasterModel.Insert(0, new DepartmentMasterModel { DepartmentID = 0, DepartmentName = DepartmentMasterModelCont.cmbDepartmentFirst });
                        obj.DataSource = lstDepartmentMasterModel;
                        obj.DisplayMember = DepartmentMasterModelCont.DepartmentName;
                        obj.ValueMember = DepartmentMasterModelCont.DepartmentID;
                        obj.DataPropertyName = DepartmentMasterModelCont.DepartmentName;
                        break;
                    case "SectionName":

                        //List<SP_GetSectionList_Result_Model> lstSectionMasterModel = new List<SP_GetSectionList_Result_Model>();
                        //SectionService _SectionService = new SectionService();

                        //lstSectionMasterModel = _SectionService.SectionDetail(0);
                        //lstSectionMasterModel.Insert(0, new SP_GetSectionList_Result_Model { SectionID = 0, SectionName = SectionMasterModelCont.cmbSectionFirst });
                        //obj.DataSource = lstSectionMasterModel;
                        obj.DisplayMember = SectionMasterModelCont.SectionName;
                        obj.ValueMember = SectionMasterModelCont.SectionID;
                        obj.DataPropertyName = SectionMasterModelCont.SectionName;
                        break;

                    case "UnitMeasureCode":

                        List<UoMMasterModel> lstUoMMasterModel = new List<UoMMasterModel>();
                        UoMService _UoMService = new UoMService();

                        lstUoMMasterModel = _UoMService.GetAllUoM();
                        lstUoMMasterModel.Insert(0, new UoMMasterModel { UnitMeasureID = 0, UnitMeasureCode = UoMMasterModelCont.cmbUoMFirst });
                        obj.DataSource = lstUoMMasterModel;
                        obj.DisplayMember = UoMMasterModelCont.UnitMeasureCode;
                        obj.ValueMember = UoMMasterModelCont.UnitMeasureID;
                        obj.DataPropertyName = UoMMasterModelCont.UnitMeasureCode;
                        break;
                    case "TaxGroupName":

                        List<TaxGroupMasterModel> lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();
                        TaxGroupService _TaxGroupService = new TaxGroupService();

                        lstTaxGroupMasterModel = _TaxGroupService.GetAllTaxGroup();
                        lstTaxGroupMasterModel.Insert(0, new TaxGroupMasterModel { TaxGroupID = 0, TaxGroupName = TaxGroupMasterModelCont.cmbTaxGroupCodeFirst });
                        obj.DataSource = lstTaxGroupMasterModel;
                        obj.DisplayMember = TaxGroupMasterModelCont.TaxGroupName;
                        obj.ValueMember = TaxGroupMasterModelCont.TaxGroupID;
                        obj.DataPropertyName = TaxGroupMasterModelCont.TaxGroupName;
                        break;
                    case "ProductVendorID":
                        List<VendorMasterModel> lstVendorMasterModel = new List<VendorMasterModel>();
                        VendorService _VendorService = new VendorService();

                        lstVendorMasterModel = _VendorService.GetAllVendor();
                        lstVendorMasterModel.Insert(0, new VendorMasterModel { VendorID = 0, VendorName = VendorMasterModelCont.cmbVendorFirst });
                        obj.DisplayMember = VendorMasterModelCont.VendorName;
                        obj.ValueMember = VendorMasterModelCont.VendorID;
                        obj.DataSource = lstVendorMasterModel;
                        break;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }

            return obj;
        }

        private void ProductGrdView_DataError(object sender, DataGridViewDataErrorEventArgs e) { }

        private void ProductGrdView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (ProductGrdView.CurrentCell is DataGridViewComboBoxCell)
            {
                ProductGrdView.CurrentCell.Value = ProductGrdView.CurrentCell.EditedFormattedValue;
                ProductGrdView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                ProductGrdView.EndEdit();
            }
        }

        private void ProductGrdView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;
                for (int i = 0; i < ProductGrdView.Rows.Count; i++)
                {
                    if (i == e.RowIndex)
                    {
                        saveUpdateGridData(e.RowIndex);
                        if(ProductGrdView.Columns.Contains("DepartmentName") && ProductGrdView.Columns.Contains("SectionName"))
                            if (e.ColumnIndex == (ProductGrdView.Columns["DepartmentName"]).Index)
                            {
                                BindSection(Convert.ToInt32(DepID), e.RowIndex, e.ColumnIndex);
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void saveUpdateGridData(int RowIndex)
        {
            try
            {
                ProductMasterModel objProductMasterModel = new ProductMasterModel();
                objProductMasterModel.ProductID = Convert.ToInt64(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                objProductMasterModel = _ProductService.ProductDetail(objProductMasterModel.ProductID);
                foreach (var item in EditablecolumnNames)
                {

                    switch (item)
                    {
                        case ProductMasterModelCont.ProductID:
                            objProductMasterModel.ProductID = Convert.ToInt64(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                            break;

                        case ProductMasterModelCont.CertCode:

                            if (ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CertCode].Value != null)
                            {
                                objProductMasterModel.CertCode = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CertCode].Value.ToString().Trim();
                            }
                            else
                            { objProductMasterModel.CertCode = CommonModelCont.EmptyString; }

                            break;

                        case ProductMasterModelCont.UPCCode:

                            if (ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UPCCode].Value != null)
                            {
                                objProductMasterModel.UPCCode = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UPCCode].Value.ToString().Trim();
                            }
                            else
                            {
                                objProductMasterModel.UPCCode = CommonModelCont.EmptyString;
                            }
                            break;

                        case ProductMasterModelCont.ProductName:

                            if (ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.ProductName].Value != null)
                            {
                                objProductMasterModel.ProductName = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.ProductName].Value.ToString().Trim();
                            }
                            else
                            { objProductMasterModel.ProductName = CommonModelCont.EmptyString; }

                            break;

                        case "DepartmentID":
                            if (ProductGrdView.Columns.Contains("DepartmentName"))
                            {
                                List<DepartmentMasterModel> depylist = new List<DepartmentMasterModel>();
                                depylist = (List<DepartmentMasterModel>)((System.Windows.Forms.DataGridViewComboBoxCell)((ProductGrdView.Rows[RowIndex].Cells["DepartmentName"]))).DataSource;
                                var items = (from deptitem in depylist where deptitem.DepartmentName == ProductGrdView.Rows[RowIndex].Cells["DepartmentName"].EditedFormattedValue.ToString() select deptitem.DepartmentID).FirstOrDefault();
                                objProductMasterModel.DepartmentID = items;
                                DepID = Convert.ToInt32(objProductMasterModel.DepartmentID);
                                //if (ColIndex == (ProductGrdView.Columns["DepartmentName"]).Index)
                                //{
                                //    BindSection(Convert.ToInt32(DepID), RowIndex, ColIndex);
                                //}
                            }
                            break;
                        case ProductMasterModelCont.DepartmentName:
                            objProductMasterModel.DepartmentName = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.DepartmentName].Value.ToString().Trim();
                            break;
                        case "SectionID":
                            if (ProductGrdView.Columns.Contains("SectionName"))
                            {
                                List<SP_GetSectionList_Result_Model> Seclist = new List<SP_GetSectionList_Result_Model>();
                                Seclist = (List<SP_GetSectionList_Result_Model>)((System.Windows.Forms.DataGridViewComboBoxCell)((ProductGrdView.Rows[RowIndex].Cells["SectionName"]))).DataSource;
                                var Seclistitems = (from deptitem in Seclist where deptitem.SectionName == ProductGrdView.Rows[RowIndex].Cells["SectionName"].EditedFormattedValue.ToString() select deptitem.SectionID).FirstOrDefault();
                                objProductMasterModel.SectionID = Seclistitems;
                            }
                            break;
                        case ProductMasterModelCont.SectionName:
                            objProductMasterModel.SectionName = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.SectionName].Value.ToString().Trim();
                            break;
                        case "UnitMeasureID":
                            if (ProductGrdView.Columns.Contains("UnitMeasureCode"))
                            {
                                List<UoMMasterModel> UnitMlist = new List<UoMMasterModel>();
                                UnitMlist = (List<UoMMasterModel>)((System.Windows.Forms.DataGridViewComboBoxCell)((ProductGrdView.Rows[RowIndex].Cells["UnitMeasureCode"]))).DataSource;
                                var UnitMlistitems = (from deptitem in UnitMlist where deptitem.UnitMeasureCode == ProductGrdView.Rows[RowIndex].Cells["UnitMeasureCode"].EditedFormattedValue.ToString() select deptitem.UnitMeasureID).FirstOrDefault();
                                objProductMasterModel.UnitMeasureID = UnitMlistitems;
                            }
                            break;
                        case ProductMasterModelCont.UnitMeasureCode:
                            objProductMasterModel.UnitMeasureCode = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UnitMeasureCode].Value.ToString().Trim();
                            break;
                        case "TaxGroupID":
                            if (ProductGrdView.Columns.Contains("TaxGroupName"))
                            {
                                List<TaxGroupMasterModel> Taxlist = new List<TaxGroupMasterModel>();
                                Taxlist = (List<TaxGroupMasterModel>)((System.Windows.Forms.DataGridViewComboBoxCell)((ProductGrdView.Rows[RowIndex].Cells["TaxGroupName"]))).DataSource;
                                var Taxlistitems = (from deptitem in Taxlist where deptitem.TaxGroupName == ProductGrdView.Rows[RowIndex].Cells["TaxGroupName"].EditedFormattedValue.ToString() select deptitem.TaxGroupID).FirstOrDefault();
                                objProductMasterModel.TaxGroupID = Taxlistitems;
                            }
                            break;
                        case ProductMasterModelCont.TaxGroupName:
                            objProductMasterModel.TaxGroupName = ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.TaxGroupName].Value.ToString().Trim();
                            break;
                        case ProductMasterModelCont.ProductVendorID:
                            if (ProductGrdView.Columns.Contains("VendorName"))
                            {
                                List<ProductVendorMasterModel> PVenderlist = new List<ProductVendorMasterModel>();
                                PVenderlist = (List<ProductVendorMasterModel>)((System.Windows.Forms.DataGridViewComboBoxCell)((ProductGrdView.Rows[RowIndex].Cells["VendorName"]))).DataSource;
                                var PVenderlistitems = (from deptitem in PVenderlist where deptitem.VendorName == ProductGrdView.Rows[RowIndex].Cells["VendorName"].EditedFormattedValue.ToString() select deptitem.ProductVendorID).FirstOrDefault();
                                objProductMasterModel.ProductVendorID = PVenderlistitems;
                            }
                            break;
                        case ProductMasterModelCont.Price:
                            objProductMasterModel.Price = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Price].Value.ToString()) ? 0 : Convert.ToDecimal(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Price].Value.ToString());
                            break;
                        case ProductMasterModelCont.IsFoodStamp:
                            objProductMasterModel.IsFoodStamp = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.IsFoodStamp].FormattedValue.ToString().ToLower());
                            break;
                        case ProductMasterModelCont.IsActive:
                            objProductMasterModel.IsActive = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.IsActive].FormattedValue);
                            break;
                        case ProductMasterModelCont.IsScaled:
                            objProductMasterModel.IsScaled = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.IsScaled].FormattedValue);
                            break;
                        case ProductMasterModelCont.AgeVerification:
                            objProductMasterModel.AgeVerification = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.AgeVerification].FormattedValue);
                            break;
                        case ProductMasterModelCont.LabeledPrice:
                            objProductMasterModel.LabeledPrice = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.LabeledPrice].FormattedValue);
                            break;
                        case ProductMasterModelCont.IsGroupPrice:
                            objProductMasterModel.IsGroupPrice = Convert.ToBoolean(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.IsGroupPrice].FormattedValue);
                            break;
                        case ProductMasterModelCont.TareWeight:
                            objProductMasterModel.TareWeight = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.TareWeight].Value.ToString()) ? 0 : Convert.ToDecimal(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.TareWeight].Value.ToString());
                            break;
                        case ProductMasterModelCont.LinkedUPCCode:
                            objProductMasterModel.LinkedUPCCode = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.LinkedUPCCode].Value.ToString()) ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.LinkedUPCCode].Value.ToString();
                            break;
                        case ProductMasterModelCont.GroupQty:
                            objProductMasterModel.GroupQty = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.GroupQty].Value.ToString()) ? 0 : Convert.ToDecimal(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.GroupQty].Value.ToString());
                            break;
                        case ProductMasterModelCont.GroupPrice:
                            objProductMasterModel.GroupPrice = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.GroupPrice].Value.ToString()) ? 0 : Convert.ToDecimal(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.GroupPrice].Value.ToString());
                            break;
                        case ProductMasterModelCont.CaseQty:
                            objProductMasterModel.CaseQty = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CaseQty].Value.ToString()) ? 0 : Convert.ToDecimal(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CaseQty].Value.ToString());
                            break;
                        case ProductMasterModelCont.CasePrice:
                            objProductMasterModel.CasePrice = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CasePrice].Value.ToString()) ? 0 : Convert.ToDecimal(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CasePrice].Value.ToString());
                            break;
                        case ProductMasterModelCont.UnitCost:
                            objProductMasterModel.UnitCost = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UnitCost].Value.ToString()) ? 0 : Convert.ToDecimal(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.UnitCost].Value.ToString());
                            break;
                        case ProductMasterModelCont.Pack:
                            objProductMasterModel.Pack = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Pack].Value.ToString()) ? 0 : Convert.ToInt32(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Pack].Value.ToString());
                            break;
                        case ProductMasterModelCont.Size:
                            objProductMasterModel.Size = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Size].Value.ToString()) ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.Size].Value.ToString();
                            break;
                        case ProductMasterModelCont.SecondaryPLU:
                            objProductMasterModel.SecondaryPLU = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.SecondaryPLU].Value.ToString()) ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.SecondaryPLU].Value.ToString();
                            break;
                        case ProductMasterModelCont.PalletQTY:
                            objProductMasterModel.PalletQTY = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.PalletQTY].Value.ToString()) ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.PalletQTY].Value.ToString();
                            break;
                        case ProductMasterModelCont.CountryofOrigin:
                            objProductMasterModel.CountryofOrigin = string.IsNullOrEmpty(ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CountryofOrigin].Value.ToString()) ? "" : ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.CountryofOrigin].Value.ToString();
                            break;
                    }
                }

                if (objProductMasterModel.ProductID > 0)
                {
                    var add = _ProductService.AddProduct(objProductMasterModel, 2);
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void BindSection(int DepartmentID, int RowIndex, int ColIndex)
        {
            try
            {
                List<SP_GetSectionList_Result_Model> lstSectionMasterModel = new List<SP_GetSectionList_Result_Model>();
                SectionService _SectionService = new SectionService();
                lstSectionMasterModel = _SectionService.SectionDetail(DepartmentID);
                lstSectionMasterModel.Insert(0, new SP_GetSectionList_Result_Model { SectionID = 0, SectionName = SectionMasterModelCont.cmbSectionFirst });
                DataGridViewComboBoxCell comboBoxCell = (ProductGrdView.Rows[RowIndex].Cells[DepartmentMasterModelCont.SectionName] as DataGridViewComboBoxCell);
                //comboBoxCell.DataSource = null;
                if (comboBoxCell != null)
                {
                    comboBoxCell.DataSource = lstSectionMasterModel;
                    comboBoxCell.Value = SectionMasterModelCont.cmbSectionFirst;
                    ProductGrdView.UpdateCellValue(comboBoxCell.ColumnIndex, RowIndex);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private string GridColumnnameDispMapping(string Searchtext,int SearchBy) //SearchBy 0 : By Key,1 By Value
        {
            string retval = Searchtext;
            try
            {
                if (dictGridColumnDisp.Count == 0)
                {
                    dictGridColumnDisp.Add("CertCode", "Cert Code");
                    dictGridColumnDisp.Add("ProductName", "Name");
                    dictGridColumnDisp.Add("UPCCode", "UPC Code");
                    dictGridColumnDisp.Add("DepartmentName", "Department");
                    dictGridColumnDisp.Add("SectionName", "Section");
                    dictGridColumnDisp.Add("UnitMeasureCode", "Unit");
                    dictGridColumnDisp.Add("TaxGroupName", "Tax");
                    dictGridColumnDisp.Add("LinkedUPCCode", "Linked UPCCode");
                    dictGridColumnDisp.Add("IsScaled", "Scaled");
                    dictGridColumnDisp.Add("IsActive", "Active");
                    dictGridColumnDisp.Add("IsFoodStamp", "Food Stamp");
                    dictGridColumnDisp.Add("IsGroupPrice", "Group Price");
                    dictGridColumnDisp.Add("LabeledPrice", "Labeled");
                    dictGridColumnDisp.Add("UnitCost", "Unit Cost");
                    dictGridColumnDisp.Add("VendorName", "Vendor");
                    dictGridColumnDisp.Add("SecondaryPLU", "Secondary PLU");
                    dictGridColumnDisp.Add("PalletQTY", "Pallet QTY");
                    dictGridColumnDisp.Add("CountryofOrigin", "Country of Origin");
                }
                if (SearchBy == 0)
                {
                    if (dictGridColumnDisp.ContainsKey(Searchtext))
                    {
                        retval = dictGridColumnDisp.Where(w => w.Key == Searchtext).Select(s => s.Value).FirstOrDefault().ToString();
                    }
                }
                else
                {
                    if (dictGridColumnDisp.Any(w => w.Value == Searchtext))
                    {
                        retval = dictGridColumnDisp.Where(w => w.Value == Searchtext).Select(s => s.Key).FirstOrDefault().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
            return retval;
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                //for (int i = 0; i < chkBoxList.Items.Count; i++)
                //{
                //    chkBoxList.SetItemChecked(i, false);
                //}
                //for (int i = 0; i < ccb.Items.Count; i++)
                //{
                //    ccb.SetItemChecked(i, false);
                //}
                cmbSearchFilter.SelectedIndex = 0;
                txtSearchFilter.Text = string.Empty;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void FillComboBoxVal()
        {
            try
            {
                foreach (DataGridViewRow row in ProductGrdView.Rows)
                {
                    int depid = Convert.ToInt32(row.Cells[DepartmentMasterModelCont.DepartmentID].Value);
                    if (row.Cells[DepartmentMasterModelCont.SectionName] != null)
                    {
                        List<SP_GetSectionList_Result_Model> lstSectionMasterModel = new List<SP_GetSectionList_Result_Model>();
                        SectionService _SectionService = new SectionService();
                        lstSectionMasterModel = _SectionService.SectionDetail(depid);
                        lstSectionMasterModel.Insert(0, new SP_GetSectionList_Result_Model { SectionID = 0, SectionName = SectionMasterModelCont.cmbSectionFirst });
                        DataGridViewComboBoxCell comboBoxCell = (row.Cells[DepartmentMasterModelCont.SectionName] as DataGridViewComboBoxCell);
                        comboBoxCell.DataSource = lstSectionMasterModel;
                    }

                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

    }
}
