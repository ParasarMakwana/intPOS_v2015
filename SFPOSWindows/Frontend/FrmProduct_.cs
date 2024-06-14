using MetroFramework.Forms;
using Microsoft.PointOfService;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SFPOSWindows.MasterForm
{
    public partial class FrmProduct_ : MetroForm
    {
        #region Properties

        //SerialPort ComPort = new SerialPort();
        //internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        //internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        //delegate void SetTextCallback(string text);
        ////private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        //string InputData = String.Empty;
        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        ProductService _ProductService = new ProductService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        ProductMasterModel objProductMasterModel = new ProductMasterModel();

        public long PrimaryId = 0;
        public string productName = "";
        OpenFileDialog opnfd = new OpenFileDialog();
        List<ProductMasterModel> lstProductMasterModel = new List<ProductMasterModel>();
        List<ProductMasterModel> F_lstProductMasterModel = new List<ProductMasterModel>();
        List<ProductMasterModel> M_lstProductMasterModel = new List<ProductMasterModel>();
        public static int StartIndex = 0;
        public static int EndIndex = 50;
        #endregion

        #region Events

        private void txtSearchProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    string searchStr = txtSearchProductName.Text;
                    if (searchStr != CommonModelCont.EmptyString && searchStr != null && searchStr != AlertMessages.ProductSearch)
                    {
                        if (txtSearchProductName.Text.All(char.IsDigit))
                        {
                            int Count = txtSearchProductName.Text.Length;
                            if (Count < 13)
                            {
                                Count = 13 - Count;
                                for (int i = 0; i < Count; i++)
                                {
                                    txtSearchProductName.Text = "0" + txtSearchProductName.Text;
                                }
                            }
                            searchStr = txtSearchProductName.Text;
                        }

                        if (searchStr.StartsWith("0"))
                        {

                            ProductGrdView.DataSource = lstProductMasterModel
                        .Where(o => o.ProductName.ToLower().StartsWith(searchStr.ToLower())
                                     || o.UPCCode.StartsWith(searchStr.ToLower()))
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
                            LinkedUPCCode = o.LinkedUPCCode,
                            IsScaled = o.IsScaled,
                            Image = o.Image,
                            Price = o.Price,
                            IsActive = o.IsActive,
                            IsFoodStamp = o.IsFoodStamp,
                            LabeledPrice = o.LabeledPrice
                        }).Take(500).ToList();
                        }
                        else
                        {
                            ProductGrdView.DataSource = lstProductMasterModel
                        .Where(o => o.ProductName.ToLower().StartsWith(searchStr.ToLower())
                                     || o.UPCCode.StartsWith(searchStr.ToLower()))
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
                            LinkedUPCCode = o.LinkedUPCCode,
                            IsScaled = o.IsScaled,
                            Image = o.Image,
                            Price = o.Price,
                            IsActive = o.IsActive,
                            IsFoodStamp = o.IsFoodStamp,
                            LabeledPrice = o.LabeledPrice
                        }).ToList();
                        }
                        grdHideColumn();
                    }
                }
                catch (Exception ex)
                {
                    _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
                }
            }
        }

        private void metroBtnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }
        
        private void ProductGrdView_Scroll(object sender, ScrollEventArgs e)
        {
            int display = ProductGrdView.Rows.Count - ProductGrdView.DisplayedRowCount(false);
            if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
            {
                if (e.NewValue >= ProductGrdView.Rows.Count - GetDisplayedRowsCount())
                {
                    StartIndex = StartIndex + 50;
                    EndIndex = EndIndex + 50;
                    F_lstProductMasterModel = new List<ProductMasterModel>();
                    F_lstProductMasterModel = lstProductMasterModel.Where(o => o.RowNumber > StartIndex && o.RowNumber <= EndIndex).ToList();
                    M_lstProductMasterModel.AddRange(F_lstProductMasterModel);
                    ProductGrdView.DataSource = M_lstProductMasterModel
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
                            Image = o.Image,
                            Price = o.Price,
                            IsActive = o.IsActive,
                            IsFoodStamp = o.IsFoodStamp
                        }).ToList();
                    grdHideColumn();
                }
            }
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            picLoader.Visible = true;
            System.Threading.Thread thread =
              new System.Threading.Thread(new System.Threading.ThreadStart(dataLoad));
            thread.Start();
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
            txtSearchProductName.Text = UPCCode;
            myScanner.DataEventEnabled = true;
        }

        internal delegate void SetDataSourceDelegate(List<ProductMasterModel> M_lstProductMasterModel);

        private void setDataSource(List<ProductMasterModel> M_lstProductMasterModel)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource), M_lstProductMasterModel);
            }
            else
            {
                ProductGrdView.DataSource = M_lstProductMasterModel
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
                        Image = o.Image,
                        Price = o.Price,
                        IsActive = o.IsActive,
                        IsFoodStamp = o.IsFoodStamp
                    }).ToList();

                //if (ProductGrdView.Rows.Count > 0 && (first == true))
                //{
                //    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                //    var bmpEdit = new Bitmap(Resources.edit);
                //    imgEdit.Image = bmpEdit;
                //    ProductGrdView.Columns.Add(imgEdit);
                //    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                //    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                //    var bmp = new Bitmap(Resources.delete);
                //    imgDelete.Image = bmp;
                //    ProductGrdView.Columns.Add(imgDelete);
                //    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //    first = false;
                //}

                picLoader.Visible = false;
                grdHideColumn();
            }
        }

        private void FrmProduct__FormClosing(object sender, FormClosingEventArgs e)
        {
            DeviceRemove();
        }
        #endregion

        #region Functions

        public FrmProduct_()
        {
            InitializeComponent();
            //ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            //OpenPort();
            myPosExplorer = new PosExplorer(this);
        }
        
        public void dataLoad()
        {
            try
            {
                //bool IsConnect = CheckConnection();
                if (StartIndex == 0)
                {
                    if (LoginInfo.Connections)
                    {
                        lstProductMasterModel = _ProductService.GetAllProduct("",false);
                        F_lstProductMasterModel = lstProductMasterModel.Where(o => o.RowNumber > StartIndex && o.RowNumber <= EndIndex).ToList();
                        M_lstProductMasterModel.AddRange(F_lstProductMasterModel);
                        setDataSource(M_lstProductMasterModel);
                    }
                    else
                    {
                        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
                        conn.Open();
                        DataTable dt = new DataTable();
                        string query = "SELECT  PM.ProductID,PM.ProductName,PM.UPCCode,PM.CertCode,"
                    + " PM.DepartmentID,PM.SectionID,PM.UnitMeasureID,PM.Price,PM.TaxGroupID,PM.Image,PM.LabeledPrice,"
                    + " PM.IsActive,PM.IsDelete,PM.CreatedDate,PM.CreatedBy,PM.UpdatedDate,PM.UpdatedBy,"
                    + " PM.IsFoodStamp,PM.AgeVerification,PM.IsScaled,PM.TareWeight,PM.GroupQty,PM.GroupPrice,PM.LinkedUPCCode,"
                    + " CM.DepartmentName,SCM.SectionName,UOM.UnitMeasureCode,TGM.TaxGroupName FROM"
                    + " tbl_ProductMaster PM"
                    + " Left JOIN tbl_DepartmentMaster CM On CM.DepartmentID = PM.DepartmentID"
                    + " Left JOIN tbl_SectionMaster SCM ON SCM.DepartmentID = PM.DepartmentID and SCM.SectionID = PM.SectionID"
                    + " Left JOIN tbl_UnitMeasureMaster UOM ON UOM.UnitMeasureID = PM.UnitMeasureID"
                    + " Left JOIN tbl_TaxGroupMaster TGM ON TGM.TaxGroupID = PM.TaxGroupID WHERE PM.IsDelete = 0 ORDER BY PM.ProductID  ";
                        SqlCeDataAdapter DataAdapter = new SqlCeDataAdapter(query, conn);
                        DataAdapter.Fill(dt);
                        ProductGrdView.DataSource = dt;
                        conn.Close();
                    }
                }
                else
                {
                    StartIndex = 0;
                    EndIndex = 50;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProduct_ + ex.StackTrace, ex.LineNumber());
            }
        }
        
        public void grdHideColumn()
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

                ProductGrdView.Columns["ProductName"].HeaderText = "Name";
                ProductGrdView.Columns["DepartmentName"].HeaderText = "Department";
                ProductGrdView.Columns["UPCCode"].HeaderText = "UPC Code";
                ProductGrdView.Columns["SectionName"].HeaderText = "Section";
                ProductGrdView.Columns["UnitMeasureCode"].HeaderText = "Unit";
                ProductGrdView.Columns["TaxGroupName"].HeaderText = "Tax";

                ProductGrdView.Columns["ProductName"].Width = 200;
                ProductGrdView.Columns["UPCCode"].Width = 100;

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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProduct_ + ex.StackTrace, ex.LineNumber());
            }
        }
      
        public void Clear()
        {
            try
            {
                txtSearchProductName.Text = "";
                PrimaryId = 0;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(dataLoad));
                thread.Start();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProduct_ + ex.StackTrace, ex.LineNumber());
            }
        }
      
        private int GetDisplayedRowsCount()
        {
            try
            {
                int count = ProductGrdView.Rows[ProductGrdView.FirstDisplayedScrollingRowIndex].Height;
                count = ProductGrdView.Height / count;
                return count;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProduct_ + ex.StackTrace, ex.LineNumber());
                return 0;
            }
        }
        
        public void DeviceRemove()
        {
            try
            {
                myScanner.DataEventEnabled = false;
                myScanner.DeviceEnabled = false;
                myScanner.Release();
                myScanner.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProduct_ + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion
    }
}
