using ClosedXML.Excel;
using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmProductSalesPrice : MetroForm
    {
        #region Properties

        public long ProductId { get; set; }
        public static long PrimaryId = 0;
        public string productName = "";

        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ProductService _ProductService = new ProductService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        List<ProductSalesPriceMasterModel> lstProductSalesPriceMasterModel = new List<ProductSalesPriceMasterModel>();
        ProductSalesPriceService _ProductSalesPriceService = new ProductSalesPriceService();
        ProductSalesPriceMasterModel objProductSalesPriceMasterModel = new ProductSalesPriceMasterModel();

        #endregion

        #region Events
        private void ProductSalesPriceGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt32(ProductSalesPriceGrdView.Rows[e.RowIndex].Cells[ProductSalesPriceMasterModelCont.ProductSalePriceID].Value);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ProductSalesPriceGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                            ProductSalesPriceService _ProductSalesPriceService = new ProductSalesPriceService();
                            ProductSalesPriceMasterModel objProductSalesPriceMasterModel = new ProductSalesPriceMasterModel();

                            objProductSalesPriceMasterModel.ProductSalePriceID = PrimaryId;
                            var add = _ProductSalesPriceService.AddProductSalesPrice(objProductSalesPriceMasterModel, 3);
                            UpdateLog();
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                            }
                            PrimaryId = 0;
                            dataLoad();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnAdd_Click(object sender, EventArgs e)
        {
            FrmMetro_AddProducSalePrice objFrmMetro_AddProducSalePrice = new FrmMetro_AddProducSalePrice();
            objFrmMetro_AddProducSalePrice.ProductId = ProductId;
            objFrmMetro_AddProducSalePrice.productName = productName;
            //objFrmMetro_AddProducSalePrice.LoadCmbProductName();
            objFrmMetro_AddProducSalePrice.ShowDialog();
            dataLoad();
        }

        #endregion

        #region Functions

        public FrmProductSalesPrice()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                lstProductSalesPriceMasterModel = _ProductSalesPriceService.GetAllProductSalesPrice(ProductId);
                //var onjtbl_ProductUoM = (from pu in _db.tbl_ProductSalePriceMaster.Where(o => o.IsDelete == false)
                //                         join pm in _db.tbl_ProductMaster.Where(o => o.IsDelete == false && o.ProductID == ProductId)
                //                         on pu.ProductID equals pm.ProductID
                //                         orderby pu.ProductSalePriceID
                //                         select new
                //                         {
                //                             Product = pm.ProductName,
                //                             ProductSalePriceID = pu.ProductSalePriceID,
                //                             ProductID = pu.ProductID,
                //                             Price = pu.SellPrice,
                //                             StartDate = pu.StartDate,
                //                             EndDate = pu.EndDate
                //                         }).ToList();

                ProductSalesPriceGrdView.DataSource = lstProductSalesPriceMasterModel;
                ProductSalesPriceGrdView.Columns[ProductMasterModelCont.ProductID].Visible = false;
                ProductSalesPriceGrdView.Columns[ProductSalesPriceMasterModelCont.ProductSalePriceID].Visible = false;
                ProductSalesPriceGrdView.Columns[ProductSalesPriceMasterModelCont.IsActive].Visible = false;
                ProductSalesPriceGrdView.Columns[ProductSalesPriceMasterModelCont.IsDelete].Visible = false;
                ProductSalesPriceGrdView.Columns[ProductSalesPriceMasterModelCont.CreatedDate].Visible = false;
                ProductSalesPriceGrdView.Columns[ProductSalesPriceMasterModelCont.CreatedBy].Visible = false;
                ProductSalesPriceGrdView.Columns[ProductSalesPriceMasterModelCont.UpdatedDate].Visible = false;
                ProductSalesPriceGrdView.Columns[ProductSalesPriceMasterModelCont.UpdatedBy].Visible = false;

                ProductSalesPriceGrdView.Columns["StartDate"].HeaderText = "Start Date";
                ProductSalesPriceGrdView.Columns["EndDate"].HeaderText = "End Date";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_ProductSalePriceMaster");
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
                                #region Add ProductSalePrice
                                for (int row = 0; row < dt.Rows.Count; row++)
                                {
                                    long ProductID = 0;
                                    ProductID = _ProductService.GetProductID(dt.Rows[row]["ProductName"].ToString());
                                    if (ProductID != 0)
                                    {
                                        bool IsProductSale = _ProductSalesPriceService.CheckName(ProductID, Convert.ToDecimal(dt.Rows[row]["Price"].ToString().Trim()), Convert.ToDateTime(dt.Rows[row]["StartDate"].ToString()).Date, Convert.ToDateTime(dt.Rows[row]["EndDate"].ToString()).Date);
                                        if (!IsProductSale)
                                        {
                                            count++;
                                            objProductSalesPriceMasterModel.StartDate = Convert.ToDateTime(dt.Rows[row]["StartDate"].ToString());
                                            objProductSalesPriceMasterModel.EndDate = Convert.ToDateTime(dt.Rows[row]["EndDate"].ToString());
                                            objProductSalesPriceMasterModel.SellPrice = Convert.ToDecimal(dt.Rows[row]["Price"].ToString().Trim());
                                            objProductSalesPriceMasterModel.ProductID = ProductID;
                                            var add = _ProductSalesPriceService.AddProductSalesPrice(objProductSalesPriceMasterModel, 1);
                                        }
                                    }
                                    else
                                    {
                                        ClsCommon.MsgBox("Information",dt.Rows[row]["ProductName"].ToString() + " Product is not available!", false);
                                    }
                                }
                                ClsCommon.MsgBox("Information","Total " + count + " Product Price imported successfully.!", false);
                                #endregion
                                dataLoad();
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information",PName + " Product is not available!", false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
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
                sfd.FileName = "ProductSalePrice";
                sfd.Title = "Save an Excel File";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ClsCommon.MsgBox("Information","Data will be exported and you will be notified when it is ready.", false);
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            ClsCommon.MsgBox("Information","It wasn't possible to write the data to the disk." + ex.Message, false);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "ProductSalePrice");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information","Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong while exporting product Sale Price !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("Price", typeof(string));
                dt.Columns.Add("EndDate", typeof(string));
                dt.Columns.Add("StartDate", typeof(string));

                foreach (var item in lstProductSalesPriceMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductName"] = item.ProductName;
                    dr["Price"] = item.SellPrice;
                    dr["StartDate"] = item.StartDate;
                    dr["EndDate"] = item.EndDate;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void ProductSalesPriceGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }

        public void UpdateData(int RowIndex)
        {
            try
            {
                if (Convert.ToDateTime(ProductSalesPriceGrdView.Rows[RowIndex].Cells[ProductSalesPriceMasterModelCont.EndDate].Value) >= DateTime.Now.Date)
                {
                    FrmMetro_AddProducSalePrice objFrmMetro_AddProducSalePrice = new FrmMetro_AddProducSalePrice();
                    objFrmMetro_AddProducSalePrice.txtSalesPrice.Text = ProductSalesPriceGrdView.Rows[RowIndex].Cells[ProductSalesPriceMasterModelCont.SellPrice].Value.ToString().Trim();
                    objFrmMetro_AddProducSalePrice.datePickerStartDate.Value = Convert.ToDateTime(ProductSalesPriceGrdView.Rows[RowIndex].Cells[ProductSalesPriceMasterModelCont.StartDate].Value);
                    objFrmMetro_AddProducSalePrice.datePickerEndDate.Value = Convert.ToDateTime(ProductSalesPriceGrdView.Rows[RowIndex].Cells[ProductSalesPriceMasterModelCont.EndDate].Value);
                    objFrmMetro_AddProducSalePrice.ProductId = ProductId;
                    objFrmMetro_AddProducSalePrice.PrimaryId = PrimaryId;
                    objFrmMetro_AddProducSalePrice.productName = productName;
                    //objFrmMetro_AddProducSalePrice.LoadCmbProductName();
                    objFrmMetro_AddProducSalePrice.ShowDialog();
                    dataLoad();
                }
                else
                {
                   ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.EndDatePast, false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
