using ClosedXML.Excel;
using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{

    public partial class FrmProductSalesDiscount : MetroForm
    {
        #region Properties
        public long ProductId { get; set; }
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        List<ProductMasterModel> lstProductMasterModel = new List<ProductMasterModel>();

        public static long PrimaryId = 0;
        public string productName = "";
        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(CommonModelCont.EmptyString);
        ErrorProvider ep = new ErrorProvider();
        ProductService _ProductService = new ProductService();
        ProductSalesDiscountService _ProductSalesDiscountService = new ProductSalesDiscountService();
        ProductSalesDiscountMasterModel objProductSalesDiscountMasterModel = new ProductSalesDiscountMasterModel();
        List<ProductSalesDiscountMasterModel> lstProductSalesDiscountMasterModel = new List<ProductSalesDiscountMasterModel>();
        #endregion

        #region Events

        private void ProductSalesDiscountGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt32(ProductSalesDiscountGrdView.Rows[e.RowIndex].Cells[ProductSalesDiscountMasterModelCont.ProductSaleDiscountID].Value);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ProductSalesDiscountGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                            objProductSalesDiscountMasterModel.ProductSaleDiscountID = PrimaryId;
                            var add = _ProductSalesDiscountService.AddProductSaleDiscount(objProductSalesDiscountMasterModel, 3);
                            UpdateLog();
                            if (add != null)
                            {
                                DialogResult res = MessageBox.Show(AlertMessages.DeleteSuccess, AlertMessages.SuccessAlert, MessageBoxButtons.OK);
                            }
                            PrimaryId = 0;
                            dataLoad();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnAdd_Click(object sender, EventArgs e)
        {
            FrmMetro_AddProducSaleDisc objFrmMetro_AddProducSaleDisc = new FrmMetro_AddProducSaleDisc();
            objFrmMetro_AddProducSaleDisc.ProductId = ProductId;
            objFrmMetro_AddProducSaleDisc.productName = productName;
            //objFrmMetro_AddProducSaleDisc.LoadCmbProductName();
            objFrmMetro_AddProducSaleDisc.ShowDialog();
            dataLoad();
        }

        #endregion

        #region Functions

        public FrmProductSalesDiscount()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                lstProductSalesDiscountMasterModel = _ProductSalesDiscountService.GetAllProductSaleDiscount(ProductId);
                //var onjtbl_ProductUoM = (from pu in _db.tbl_ProductSaleDiscountMaster.Where(o => o.IsDelete == false)
                //                         join pm in _db.tbl_ProductMaster.Where(o => o.IsDelete == false && o.ProductID == ProductId)
                //                         on pu.ProductID equals pm.ProductID
                //                         select new
                //                         {
                //                             Product = pm.ProductName,
                //                             ProductSaleDiscountID = pu.ProductSaleDiscountID,
                //                             ProductID = pu.ProductID,
                //                             Discount = pu.Discount,
                //                             StartDate = pu.StartDate,
                //                             EndDate = pu.EndDate
                //                         }).ToList();



                ProductSalesDiscountGrdView.DataSource = lstProductSalesDiscountMasterModel;
                ProductSalesDiscountGrdView.Columns[ProductSalesDiscountMasterModelCont.ProductID].Visible = false;
                ProductSalesDiscountGrdView.Columns[ProductSalesDiscountMasterModelCont.ProductSaleDiscountID].Visible = false;
                ProductSalesDiscountGrdView.Columns[ProductSalesDiscountMasterModelCont.IsActive].Visible = false;
                ProductSalesDiscountGrdView.Columns[ProductSalesDiscountMasterModelCont.IsDelete].Visible = false;
                ProductSalesDiscountGrdView.Columns[ProductSalesDiscountMasterModelCont.CreatedDate].Visible = false;
                ProductSalesDiscountGrdView.Columns[ProductSalesDiscountMasterModelCont.CreatedBy].Visible = false;
                ProductSalesDiscountGrdView.Columns[ProductSalesDiscountMasterModelCont.UpdatedDate].Visible = false;
                ProductSalesDiscountGrdView.Columns[ProductSalesDiscountMasterModelCont.UpdatedBy].Visible = false;

                ProductSalesDiscountGrdView.Columns["StartDate"].HeaderText = "Start Date";
                ProductSalesDiscountGrdView.Columns["EndDate"].HeaderText = "End Date";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_ProductSaleDiscountMaster");
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
                                #region Add ProductSaleDiscount
                                for (int row = 0; row < dt.Rows.Count; row++)
                                {
                                    long ProductID = 0;
                                    ProductID = _ProductService.GetProductID(dt.Rows[row]["ProductName"].ToString());
                                    if (ProductID != 0)
                                    {
                                        bool IsProductSale = _ProductSalesDiscountService.CheckName(ProductID, Convert.ToDecimal(dt.Rows[row]["Discount"].ToString().Trim()), Convert.ToDateTime(dt.Rows[row]["StartDate"].ToString()).Date, Convert.ToDateTime(dt.Rows[row]["EndDate"].ToString()).Date);
                                        if (!IsProductSale)
                                        {
                                            count++;
                                            objProductSalesDiscountMasterModel.StartDate = Convert.ToDateTime(dt.Rows[row]["StartDate"].ToString());
                                            objProductSalesDiscountMasterModel.EndDate = Convert.ToDateTime(dt.Rows[row]["EndDate"].ToString());
                                            objProductSalesDiscountMasterModel.Discount = Convert.ToDecimal(dt.Rows[row]["Discount"].ToString().Trim());
                                            objProductSalesDiscountMasterModel.ProductID = ProductID;
                                            var add = _ProductSalesDiscountService.AddProductSaleDiscount(objProductSalesDiscountMasterModel, 1);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(dt.Rows[row]["ProductName"].ToString() + " Product is not available!");
                                    }
                                }
                                MessageBox.Show("Total " + count + " Product discount imported successfully.!");
                                #endregion
                                dataLoad();
                            }
                            else
                            {
                                MessageBox.Show(PName + " Product is not available!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select valid format.!!");
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
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
                sfd.FileName = "ProductSaleDisc";
                sfd.Title = "Save an Excel File";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Data will be exported and you will be notified when it is ready.");
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "ProductSaleDisc");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        MessageBox.Show("Data will be exported successfully !!!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while exporting Product Sale Discount !!!");
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
        }


        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("Discount", typeof(string));
                dt.Columns.Add("EndDate", typeof(string));
                dt.Columns.Add("StartDate", typeof(string));

                foreach (var item in lstProductSalesDiscountMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductName"] = item.ProductName;
                    dr["Discount"] = item.Discount;
                    dr["StartDate"] = item.StartDate;
                    dr["EndDate"] = item.EndDate;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        #endregion

        private void ProductSalesDiscountGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }
        public void UpdateData(int RowIndex)
        {
            try
            {
                if (Convert.ToDateTime(ProductSalesDiscountGrdView.Rows[RowIndex].Cells[ProductSalesDiscountMasterModelCont.EndDate].Value) >= DateTime.Now.Date)
                {
                    FrmMetro_AddProducSaleDisc objFrmMetro_AddProducSaleDisc = new FrmMetro_AddProducSaleDisc();
                    objFrmMetro_AddProducSaleDisc.ProductId = ProductId;
                    objFrmMetro_AddProducSaleDisc.PrimaryId = PrimaryId;
                    objFrmMetro_AddProducSaleDisc.productName = productName;
                    //objFrmMetro_AddProducSaleDisc.LoadCmbProductName();
                    objFrmMetro_AddProducSaleDisc.txtSalesDiscount.Text = ProductSalesDiscountGrdView.Rows[RowIndex].Cells[ProductSalesDiscountMasterModelCont.Discount].Value.ToString().Trim();
                    objFrmMetro_AddProducSaleDisc.datePickerStartDate.Value = Convert.ToDateTime(ProductSalesDiscountGrdView.Rows[RowIndex].Cells[ProductSalesDiscountMasterModelCont.StartDate].Value);
                    objFrmMetro_AddProducSaleDisc.datePickerEndDate.Value = Convert.ToDateTime(ProductSalesDiscountGrdView.Rows[RowIndex].Cells[ProductSalesDiscountMasterModelCont.EndDate].Value);
                    objFrmMetro_AddProducSaleDisc.ShowDialog();
                    dataLoad();
                }
                else
                {
                    DialogResult result = MessageBox.Show(AlertMessages.EndDatePast, AlertMessages.InformationAlert, MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
