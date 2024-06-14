using ClosedXML.Excel;
using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmProductVendor : MetroForm
    {
        #region Properties
        public long ProductId { get; set; }
        public long VendorId { get; set; }
        public static long PrimaryId = 0;

        public long PId { get; set; }
        public long VId { get; set; }
        public string ItemCode { get; set; }
        public string productName = "";

        ProductVendorMasterModel objProductVendorMasterModel = new ProductVendorMasterModel();
        ProductVendorService _ProductVendorService = new ProductVendorService();
        ErrorProvider ep = new ErrorProvider();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        List<ProductVendorMasterModel> lstProductVendorMasterModel = new List<ProductVendorMasterModel>();
        VendorService _VendorService = new VendorService();
        #endregion

        #region Events

        private void ProductVendorGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt32(ProductVendorGrdView.Rows[e.RowIndex].Cells[ProductVendorMasterModelCont.ProductVendorID].Value);
                    PId = Convert.ToInt64(ProductVendorGrdView.Rows[e.RowIndex].Cells[ProductVendorMasterModelCont.ProductID].Value);
                    VId = Convert.ToInt64(ProductVendorGrdView.Rows[e.RowIndex].Cells[ProductVendorMasterModelCont.VendorID].Value);
                    ItemCode = ProductVendorGrdView.Rows[e.RowIndex].Cells[ProductVendorMasterModelCont.ItemCode].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ProductVendorGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    UpdateData(e.RowIndex);
                }
                if (e.ColumnIndex == 1)
                {
                    if (PrimaryId > 0)
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objProductVendorMasterModel.ProductVendorID = PrimaryId;
                            var add = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 3);
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                            }
                            PrimaryId = 0;
                            dataLoad();
                        }
                    }
                }
                if (e.ColumnIndex == 17)
                {
                    if (PrimaryId > 0)
                    {
                        
                        DialogResult result = MessageBox.Show("Do you want to make this vendor as default vendor?", AlertMessages.ConfirmAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objProductVendorMasterModel.ProductVendorID = PrimaryId;
                            objProductVendorMasterModel.IsDefault = true;
                            
                            var add = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 4);
                            if (add != null)
                            {
                                UpdateVendor(PId, VId, ItemCode);
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Update, false);
                            }
                            PrimaryId = 0;
                            dataLoad();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnAdd_Click(object sender, EventArgs e)
        {
            FrmMetro_AddProducVendor objFrmMetro_AddProducVendor = new FrmMetro_AddProducVendor();
            objFrmMetro_AddProducVendor.ProductId = ProductId;
            objFrmMetro_AddProducVendor.productName = productName;

            objFrmMetro_AddProducVendor.LoadCmbVendorName();
            objFrmMetro_AddProducVendor.ShowDialog();
            dataLoad();
        }
        #endregion

        #region Functions

        public void UpdateVendor(long? ProductID, long? VendorID, string CertCode)
        {
            DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
            var vlist = _db.tbl_ProductVendorMaster.Where(x => x.ProductID == ProductID && x.VendorID != VendorID).ToList();
            foreach (tbl_ProductVendorMaster tbl in vlist)
            {
                tbl.IsDefault = false;
                tbl.UpdatedBy = LoginInfo.UserId;
                tbl.UpdatedDate = DateTime.Now;
            }
            var Plist = _db.tbl_ProductMaster.Where(x => x.ProductID == ProductID).ToList();
            foreach (tbl_ProductMaster tbl1 in Plist)
            {
                tbl1.ProductVendorID = Convert.ToInt64(VendorID);
                tbl1.CertCode = CertCode;
                tbl1.UpdatedBy = LoginInfo.UserId;
                tbl1.UpdatedDate = DateTime.Now;
            }
            _db.SaveChanges();
        }

        public FrmProductVendor()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                lstProductVendorMasterModel = _ProductVendorService.GetAllProductVendor(ProductId);




                //var onjtbl_ProductUoM = (from pu in _db.tbl_ProductVendorMaster.Where(o => o.IsDelete == false)
                //                         join pm in _db.tbl_ProductMaster.Where(o => o.ProductID == ProductId)
                //                         on pu.ProductID equals pm.ProductID
                //                         join vm in _db.tbl_VendorMaster
                //                         on pu.VendorID equals vm.VendorID
                //                         orderby pu.ProductVendorID descending
                //                         select new
                //                         {
                //                             ProductVendorID = pu.ProductVendorID,
                //                             ProductName = pm.ProductName,
                //                             ItemCode = pu.ItemCode,
                //                             ProductID = pu.ProductID,
                //                             VendorName = vm.VendorName,
                //                             VendorID = pu.VendorID,
                //                             UnitCost = pu.UnitCost
                //                         }).ToList();

                //ProductVendorGrdView.DataSource = null;

                ProductVendorGrdView.DataSource = lstProductVendorMasterModel;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.ProductVendorID].Visible = false;
                ProductVendorGrdView.Columns[ProductMasterModelCont.ProductID].Visible = false;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.VendorID].Visible = false;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.Pack].Visible = false;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.IsActive].Visible = false;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.IsDelete].Visible = false;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.CreatedDate].Visible = false;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.CreatedBy].Visible = false;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.UpdatedDate].Visible = false;
                ProductVendorGrdView.Columns[ProductVendorMasterModelCont.UpdatedBy].Visible = false;
                //ProductVendorGrdView.Columns[ProductVendorMasterModelCont.IsDefault].Visible = false;



                //ProductVendorGrdView.Columns[ProductVendorMasterModelCont.IsDefault].Visible = true;
                ProductVendorGrdView.Columns["IsDefault"].HeaderText = "Default";
                ProductVendorGrdView.Columns["UnitCost"].HeaderText = "Unit Cost";
                ProductVendorGrdView.Columns["ItemCode"].HeaderText = "Item Code";
                ProductVendorGrdView.Columns["ProductName"].HeaderText = "Product";
                ProductVendorGrdView.Columns["VendorName"].HeaderText = "Vendor";

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }


        #endregion

        #region IMPORT/EXPORT
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HRD=Yes'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'";

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


                            ProductService _ProductService = new ProductService();
                            DataTable uniqueProduct = dt.DefaultView.ToTable(true, "ProductName");
                            string PName = "";
                            for (int row = 0; row < uniqueProduct.Rows.Count; row++)
                            {
                                #region Check Product
                                bool IsProduct = _ProductService.CheckName(uniqueProduct.Rows[row]["ProductName"].ToString());
                                if (!IsProduct)
                                {
                                    if (PName == "")
                                    {
                                        PName = uniqueProduct.Rows[row]["ProductName"].ToString();
                                    }
                                    else
                                    {
                                        PName = ", " + uniqueProduct.Rows[row]["ProductName"].ToString();
                                    }
                                }
                                #endregion
                            }

                            if (PName == "")
                            {
                                VendorService _VendorService = new VendorService();
                                DataTable uniqueVendor = dt.DefaultView.ToTable(true, "VendorName");
                                string VName = "";
                                for (int row = 0; row < uniqueVendor.Rows.Count; row++)
                                {
                                    #region Check Product
                                    bool IsVendor = _VendorService.CheckName(uniqueVendor.Rows[row]["VendorName"].ToString());
                                    if (!IsVendor)
                                    {
                                        if (VName == "")
                                        {
                                            VName = uniqueVendor.Rows[row]["VendorName"].ToString();
                                        }
                                        else
                                        {
                                            VName = ", " + uniqueVendor.Rows[row]["VendorName"].ToString();
                                        }
                                    }
                                    #endregion
                                }
                                if (VName == "")
                                {

                                    #region Add ProductSalePrice
                                    for (int row = 0; row < dt.Rows.Count; row++)
                                    {
                                        long ProductID = 0;
                                        long VendorID = 0;
                                        ProductID = _ProductService.GetProductID(dt.Rows[row]["ProductName"].ToString());
                                        VendorID = _VendorService.GetVendorID(dt.Rows[row]["VendorName"].ToString());
                                        if (ProductID != 0 && VendorID != 0)
                                        {
                                            bool IsProductSale = _ProductVendorService.CheckName(dt.Rows[row]["ItemCode"].ToString().Trim());
                                            if (!IsProductSale)
                                            {
                                                count++;
                                                objProductVendorMasterModel.ItemCode = (dt.Rows[row]["ItemCode"].ToString());
                                                objProductVendorMasterModel.ProductID = ProductID;
                                                objProductVendorMasterModel.VendorID = VendorID;
                                                objProductVendorMasterModel.UnitCost = Convert.ToDecimal(dt.Rows[row]["UnitCost"].ToString().Trim());
                                                var add = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 1);
                                            }
                                        }
                                        else
                                        {
                                            ClsCommon.MsgBox("Information", dt.Rows[row]["ProductName"].ToString() + " Product is not available!", false);
                                        }
                                    }
                                    ClsCommon.MsgBox("Information", "Total " + count + " Product Vendor imported successfully.!", false);
                                    #endregion
                                    dataLoad();
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", VName + " Vendor is not available!", false);
                                }
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", PName + " Product is not available!", false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }


        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = tableLoad();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xlsx (*.xlsx)|*.xlsx";
                sfd.FileName = "ProductVendor";
                sfd.Title = "Save an Excel File";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ClsCommon.MsgBox("Information", "Data will be exported and you will be notified when it is ready.", false);
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
                        wb.Worksheets.Add(dt, "ProductSalePrice");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information", "Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong while exporting ProductVendor !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {

                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("ItemCode", typeof(string));
                dt.Columns.Add("VendorName", typeof(string));
                dt.Columns.Add("UnitCost", typeof(string));

                foreach (var item in lstProductVendorMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductName"] = item.ProductName;
                    dr["ItemCode"] = item.ItemCode;
                    dr["VendorName"] = item.VendorName;
                    dr["UnitCost"] = item.UnitCost;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void ProductVendorGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }
        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddProducVendor objFrmMetro_AddProducVendor = new FrmMetro_AddProducVendor();
                objFrmMetro_AddProducVendor.PrimaryId = PrimaryId;
                objFrmMetro_AddProducVendor.productName = productName;
                objFrmMetro_AddProducVendor.ProductId = ProductId;
                objFrmMetro_AddProducVendor.LoadCmbVendorName();

                if (ProductVendorGrdView.Rows[RowIndex].Cells[ProductVendorMasterModelCont.ItemCode].Value.ToString()!="" && ProductVendorGrdView.Rows[RowIndex].Cells[ProductVendorMasterModelCont.ItemCode].Value.ToString() != null)
                    objFrmMetro_AddProducVendor.txtVendorUPCCode.Text = !String.IsNullOrEmpty(ProductVendorGrdView.Rows[RowIndex].Cells[ProductVendorMasterModelCont.ItemCode].Value.ToString()) ? ProductVendorGrdView.Rows[RowIndex].Cells[ProductVendorMasterModelCont.ItemCode].Value.ToString() : "";
                objFrmMetro_AddProducVendor.cmbVendorName.SelectedValue = ProductVendorGrdView.Rows[RowIndex].Cells[ProductVendorMasterModelCont.VendorID].Value;
                objFrmMetro_AddProducVendor.txtUnitCost.Text = ProductVendorGrdView.Rows[RowIndex].Cells[ProductVendorMasterModelCont.UnitCost].Value.ToString();
                objFrmMetro_AddProducVendor.toggleDefault.Checked = Convert.ToBoolean(ProductVendorGrdView.Rows[RowIndex].Cells[ProductVendorMasterModelCont.IsDefault].Value);
                objFrmMetro_AddProducVendor.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btndv_Click(object sender, EventArgs e)
        {
            try
            {
                if (PrimaryId > 0)
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    DialogResult result = MessageBox.Show("Do you want to make this vendor as default vendor?", AlertMessages.ConfirmAlert, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        objProductVendorMasterModel.ProductVendorID = PrimaryId;
                        objProductVendorMasterModel.ItemCode = _db.tbl_ProductVendorMaster.Where(x => x.ProductVendorID == PrimaryId).FirstOrDefault().ItemCode == null ? string.Empty : _db.tbl_ProductVendorMaster.Where(x => x.ProductVendorID == PrimaryId).FirstOrDefault().ItemCode;
                        objProductVendorMasterModel.UnitCost = _db.tbl_ProductVendorMaster.Where(x => x.ProductVendorID == PrimaryId).FirstOrDefault().UnitCost == null ? 0 : _db.tbl_ProductVendorMaster.Where(x => x.ProductVendorID == PrimaryId).FirstOrDefault().UnitCost;
                        objProductVendorMasterModel.IsDefault = true;

                        var add = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 4);
                        if (add != null)
                        {
                            UpdateVendor(PId, VId, ItemCode);
                            ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Update, false);
                        }
                        PrimaryId = 0;
                        dataLoad();
                    }
                }
                else
                {

                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
