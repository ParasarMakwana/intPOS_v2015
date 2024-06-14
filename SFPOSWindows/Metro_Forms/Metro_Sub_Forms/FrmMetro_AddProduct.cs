using MetroFramework.Forms;
using Microsoft.PointOfService;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.spDataClasses;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddProduct : MetroForm
    {
        #region Properties
        //SerialPort ComPort = new SerialPort();
        //internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        //internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);

        //delegate void SetTextCallback(string text);
        //string InputData = String.Empty;

        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        ConfirmMessageBox _ConfirmMessageBox;

        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ProductVendorService _ProductVendorService = new ProductVendorService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        ErrorProvider ep = new ErrorProvider();

        ProductMasterModel objArichiveProductMasterModel = new ProductMasterModel();
        List<ProductVendorMasterModel> lstProductVendorMasterModel = new List<ProductVendorMasterModel>();
        public long ProductID = 0;
        public string ImageFileName;
        bool flagSave = false;
        public string FSEligibleAmount;
        public string Price;
        #endregion

        #region Events
        private void metroBtnSalePrice_Click(object sender, EventArgs e)
        {
            try
            {
                AddSalePrice();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void metroBtnProductvendor_Click(object sender, EventArgs e)
        {
            try
            {
                AddVendor();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opnfd = new OpenFileDialog();
                opnfd.Filter = AlertMessages.ProductImageFormat;

                if (opnfd.ShowDialog() == DialogResult.OK)
                {
                    ImageProduct.Image = new Bitmap(opnfd.FileName);
                    string fileName = opnfd.FileName;
                    byte[] bytes = File.ReadAllBytes(fileName);
                    ImageFileName = Convert.ToBase64String(bytes);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Functions.GetDecimal(txtTareWeight.Text.Trim()) > 30)
                {
                    ClsCommon.MsgBox(AlertMessages.InformationAlert, CommonMessage.tareWeightMoreThen, false);
                }
                else
                {
                    flagSave = CheckValidation(CommonModelCont.EmptyString);
                    if (flagSave == true)
                    {
                        //MessageBox.Show("K_MetrobtnSave");
                        ProductService _ProductService = new ProductService();

                        bool IsProduct = _ProductService.CheckProductDetails(txtProductName.Text.Trim(), txtUPCCode.Text.Trim(), ProductID);
                        if (!IsProduct)
                        {
                            //MessageBox.Show("K_IsProduct");
                            ProductMasterModel objProductMasterModel = new ProductMasterModel();

                            objProductMasterModel.ProductName = txtProductName.Text;
                            objProductMasterModel.CertCode = txtCertificateCode.Text;
                            objProductMasterModel.UPCCode = ClsCommon.GetFullUPCCode(txtUPCCode.Text);
                            objProductMasterModel.DepartmentID = !string.IsNullOrEmpty(cmbDepartment.Text) ? Functions.GetInteger(cmbDepartment.SelectedValue.ToString()) : 0;
                            objProductMasterModel.SectionID = !string.IsNullOrEmpty(cmbSection.Text) ? Functions.GetInteger(cmbSection.SelectedValue.ToString()) : 0;
                            objProductMasterModel.UnitMeasureID = !string.IsNullOrEmpty(cmbUoM.Text) ? Functions.GetInteger(cmbUoM.SelectedValue.ToString()) : 0;
                            objProductMasterModel.TaxGroupID = !string.IsNullOrEmpty(cmbTaxGroup.Text) ? Functions.GetInteger(cmbTaxGroup.SelectedValue.ToString()) : 0;
                            objProductMasterModel.Price = Functions.GetDecimal(txtPrice.Text);
                            objProductMasterModel.GroupQty = (!String.IsNullOrEmpty(Convert.ToString(txtGroupQty.Text)) ? Functions.GetDecimal(txtGroupQty.Text) : 0);
                            objProductMasterModel.GroupPrice = (!String.IsNullOrEmpty(Convert.ToString(txtGroupPrice.Text)) ? Functions.GetDecimal(txtGroupPrice.Text) : 0);
                            objProductMasterModel.TareWeight = (!String.IsNullOrEmpty(Convert.ToString(txtTareWeight.Text)) ? Functions.GetDecimal(txtTareWeight.Text) : 0);
                            objProductMasterModel.LinkedUPCCode = txtLinkUPCCode.Text;
                            objProductMasterModel.CaseQty = (!String.IsNullOrEmpty(Convert.ToString(txtCaseQty.Text)) ? Functions.GetDecimal(txtCaseQty.Text) : 0);
                            objProductMasterModel.CasePrice = (!String.IsNullOrEmpty(Convert.ToString(txtCasePrice.Text)) ? Functions.GetDecimal(txtCasePrice.Text) : 0);
                            objProductMasterModel.Pack = (!String.IsNullOrEmpty(Convert.ToString(txtPack.Text)) ? Functions.GetInteger(txtPack.Text) : 0);
                            objProductMasterModel.Size = txtSize.Text.Trim();
                            objProductMasterModel.PalletQTY = (!String.IsNullOrEmpty(Convert.ToString(txtPalletQTY.Text)) ? txtPalletQTY.Text : "0");
                            objProductMasterModel.SecondaryPLU = txtSecondaryPLU.Text.Trim();

                            if (txtFSEligibleAmount.Text.ToString() != "")
                            {
                                if(!toggleFdStamp.Checked)
                                {
                                    ClsCommon.MsgBox(AlertMessages.InformationAlert, "Product has to be Food Stamp eligible to define FS Eligible Amount.", false);
                                    return;
                                }
                                objProductMasterModel.FSEligibleAmount = Functions.GetDecimal(txtFSEligibleAmount.Text);
                            }

                            if (toggleActive.Checked)
                                objProductMasterModel.IsActive = true;
                            else
                                objProductMasterModel.IsActive = false;

                            if (toggleFdStamp.Checked)
                                objProductMasterModel.IsFoodStamp = true;
                            else
                                objProductMasterModel.IsFoodStamp = false;

                            if (ToggleAgeVerify.Checked)
                                objProductMasterModel.AgeVerification = true;
                            else
                                objProductMasterModel.AgeVerification = false;

                            if (ToggleScaled.Checked)
                                objProductMasterModel.IsScaled = true;
                            else
                                objProductMasterModel.IsScaled = false;

                            if (ToggleLabeled.Checked)
                                objProductMasterModel.LabeledPrice = true;
                            else
                                objProductMasterModel.LabeledPrice = false;

                            if (ToggleGroupPrice.Checked)
                            {
                                objProductMasterModel.IsGroupPrice = true;
                                //objProductMasterModel.LinkedUPCCode = null;
                            }
                            else
                            {
                                objProductMasterModel.IsGroupPrice = false;
                                //objProductMasterModel.LinkedUPCCode = txtLinkUPCCode.Text;
                            }

                            objProductMasterModel.UnitCost = (!String.IsNullOrEmpty(Convert.ToString(txtUnitCost.Text)) ? Functions.GetDecimal(txtUnitCost.Text) : 0);
                            objProductMasterModel.UpdatedDate = DateTime.Now;
                            objProductMasterModel.ProductVendorID = Convert.ToInt64(cmbVendors.SelectedValue);
                            objProductMasterModel.CountryofOrigin = Convert.ToString(txtCountryofOrig.Text).Trim();
                            objProductMasterModel.Image = ImageFileName;

                            if (ProductID <= 0)
                            {
                                string DuplicateUPC = _ProductService.GetUPCCode(objProductMasterModel.UPCCode);
                                if (DuplicateUPC == "")
                                {
                                    var add = _ProductService.AddProduct(objProductMasterModel, 1);
                                    //MessageBox.Show("K_VendorServiceUpdate");
                                    if (Functions.GetInteger(cmbVendors.SelectedValue.ToString()) > 0)
                                    {
                                        ProductVendorService _ProductVendorService = new ProductVendorService();
                                        ProductVendorMasterModel objProductVendorMasterModel = new ProductVendorMasterModel();

                                        objProductVendorMasterModel.ProductID = add.ProductID;
                                        objProductVendorMasterModel.ItemCode = Convert.ToString(txtCertificateCode.Text).Trim();
                                        objProductVendorMasterModel.VendorID = Convert.ToInt64(cmbVendors.SelectedValue);
                                        objProductVendorMasterModel.UnitCost = Functions.GetDecimal(Convert.ToString(txtUnitCost.Text).Trim());
                                        objProductVendorMasterModel.IsDefault = true;
                                        bool IsProductUPC = _ProductVendorService.CheckProductVendorName(add.ProductID, Convert.ToInt64(cmbVendors.SelectedValue));
                                        DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                        //var vlist = _db.tbl_ProductVendorMaster.Where(x => x.ProductID == ProductID && x.VendorID == objProductVendorMasterModel.VendorID).ToList();
                                        var vlist = _db.tbl_ProductVendorMaster.Where(x => x.ProductID == objProductVendorMasterModel.ProductID && x.VendorID == objProductVendorMasterModel.VendorID).ToList();
                                        if (!IsProductUPC && vlist.Count == 0)
                                        {
                                            var addv = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 1);
                                        }
                                        else
                                        {
                                            objProductVendorMasterModel.ProductVendorID = vlist[0].ProductVendorID;
                                            var updt = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 4);
                                        }
                                        //ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.ProductVendorID].Value
                                        //FrmProduct objProduct = new FrmProduct();
                                        //objProduct.NewVendorId = objProductMasterModel.ProductVendorID;
                                        FrmProductVendor objfrmproductvendor = new FrmProductVendor();
                                        objfrmproductvendor.UpdateVendor(objProductVendorMasterModel.ProductID, objProductVendorMasterModel.VendorID, objProductVendorMasterModel.ItemCode);

                                    }
                                    //MessageBox.Show("K_DuplicateUPC");
                                    if (add != null)
                                    {
                                        //MessageBox.Show("K_Add - ID"+ objProductMasterModel.ProductID);
                                        ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                                    }
                                    else
                                    {
                                        ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                                    }
                                }
                                else
                                {
                                    ClsCommon.MsgBox(AlertMessages.InformationAlert, "UPCCode code is already exists.", false);
                                }
                                //MessageBox.Show("K_AFTER_ DuplicateUPC");
                            }

                            else if (ProductID > 0)
                            {
                                objProductMasterModel.ProductID = ProductID;
                                var add = _ProductService.AddProduct(objProductMasterModel, 2);
                                //MessageBox.Show("K_VendorServiceUpdate");
                                if (Functions.GetInteger(cmbVendors.SelectedValue.ToString()) > 0)
                                {
                                    ProductVendorService _ProductVendorService = new ProductVendorService();
                                    ProductVendorMasterModel objProductVendorMasterModel = new ProductVendorMasterModel();

                                    objProductVendorMasterModel.ProductID = ProductID;
                                    objProductVendorMasterModel.ItemCode = Convert.ToString(txtCertificateCode.Text).Trim();
                                    objProductVendorMasterModel.VendorID = Convert.ToInt64(cmbVendors.SelectedValue);
                                    objProductVendorMasterModel.UnitCost = Functions.GetDecimal(Convert.ToString(txtUnitCost.Text).Trim());
                                    objProductVendorMasterModel.IsDefault = true;
                                    bool IsProductUPC = _ProductVendorService.CheckProductVendorName(ProductID, Convert.ToInt64(cmbVendors.SelectedValue));
                                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                    var vlist = _db.tbl_ProductVendorMaster.Where(x => x.ProductID == ProductID && x.VendorID == objProductVendorMasterModel.VendorID).ToList();
                                    if (!IsProductUPC && vlist.Count == 0)
                                    {
                                        var addv = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 1);
                                    }
                                    else
                                    {
                                        objProductVendorMasterModel.ProductVendorID = vlist[0].ProductVendorID;
                                        var updt = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 4);
                                    }
                                    //ProductGrdView.Rows[RowIndex].Cells[ProductMasterModelCont.ProductVendorID].Value
                                    //FrmProduct objProduct = new FrmProduct();
                                    //objProduct.NewVendorId = objProductMasterModel.ProductVendorID;
                                    FrmProductVendor objfrmproductvendor = new FrmProductVendor();
                                    objfrmproductvendor.UpdateVendor(objProductVendorMasterModel.ProductID, objProductVendorMasterModel.VendorID, objProductVendorMasterModel.ItemCode);

                                }
                                //MessageBox.Show("K_DuplicateUPC");
                                if (add != null)
                                {
                                    ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Update, false);
                                }
                                else
                                {
                                    ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                                }
                            }
                            Clear();
                            UpdateLog();
                            //MessageBox.Show("K_After UpdateLog()");
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.AlreadyExist, false);
                        }

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("K_"+ ex);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void metroBtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                FrmProduct objFrmProduct = new FrmProduct();
                objFrmProduct.txtSearchProductName.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void GetProductData()
        {

            txtProductName.Text = objArichiveProductMasterModel.ProductName;
            if (!string.IsNullOrEmpty(objArichiveProductMasterModel.CertCode))
            {
                txtCertificateCode.Text = objArichiveProductMasterModel.CertCode.Trim();
            }
            else
            {
                txtCertificateCode.Text = CommonModelCont.EmptyString;
            }

            if (!string.IsNullOrEmpty(objArichiveProductMasterModel.UPCCode))
            {
                txtUPCCode.Text = objArichiveProductMasterModel.UPCCode.Trim();
            }
            else
            {
                txtUPCCode.Text = CommonModelCont.EmptyString;
            }
            if (objArichiveProductMasterModel.DepartmentID != null)
                cmbDepartment.SelectedValue = objArichiveProductMasterModel.DepartmentID;

            if (objArichiveProductMasterModel.SectionID != null)
                cmbSection.SelectedValue = objArichiveProductMasterModel.SectionID;

            if (objArichiveProductMasterModel.UnitMeasureID != null)
                cmbUoM.SelectedValue = objArichiveProductMasterModel.UnitMeasureID;

            if (objArichiveProductMasterModel.TaxGroupID != null)
                cmbTaxGroup.SelectedValue = objArichiveProductMasterModel.TaxGroupID;

            ProductID = objArichiveProductMasterModel.ProductID;

            if (!string.IsNullOrEmpty(objArichiveProductMasterModel.Image))
            {
                ImageProduct.Image = Base64ToImage(objArichiveProductMasterModel.Image);
            }
            else
            {
                ImageProduct.Image = new Bitmap(Resources.sf_);
            }
            txtPrice.Text = objArichiveProductMasterModel.Price == null ? "" : Convert.ToString(objArichiveProductMasterModel.Price);
            txtGroupQty.Text = objArichiveProductMasterModel.GroupQty == null ? "0" : Convert.ToString(objArichiveProductMasterModel.GroupQty);
            txtGroupPrice.Text = objArichiveProductMasterModel.GroupPrice == null ? "0" : Convert.ToString(objArichiveProductMasterModel.GroupPrice);
            txtTareWeight.Text = objArichiveProductMasterModel.TareWeight == null ? "0" : Convert.ToString(objArichiveProductMasterModel.TareWeight);
            txtLinkUPCCode.Text = string.IsNullOrEmpty(objArichiveProductMasterModel.LinkedUPCCode) ? "" : objArichiveProductMasterModel.LinkedUPCCode;
            txtCaseQty.Text = objArichiveProductMasterModel.CaseQty == null ? "0" : Convert.ToString(objArichiveProductMasterModel.CaseQty);
            txtCasePrice.Text = objArichiveProductMasterModel.CasePrice == null ? "0" : Convert.ToString(objArichiveProductMasterModel.CasePrice);
            txtUnitCost.Text = objArichiveProductMasterModel.UnitCost == null ? "0" : Convert.ToString(objArichiveProductMasterModel.UnitCost);
            txtPack.Text = objArichiveProductMasterModel.Pack == null ? "0" : Convert.ToString(objArichiveProductMasterModel.Pack);
            txtSize.Text = string.IsNullOrEmpty(objArichiveProductMasterModel.Size) ? "" : objArichiveProductMasterModel.Size;
            txtPalletQTY.Text = string.IsNullOrEmpty(objArichiveProductMasterModel.PalletQTY) ? "" : objArichiveProductMasterModel.PalletQTY;
            txtSecondaryPLU.Text = string.IsNullOrEmpty(objArichiveProductMasterModel.SecondaryPLU) ? "" : objArichiveProductMasterModel.SecondaryPLU;
            txtCountryofOrig.Text = string.IsNullOrEmpty(objArichiveProductMasterModel.CountryofOrigin) ? "" : objArichiveProductMasterModel.CountryofOrigin;
            DtUpdateddate.Text = objArichiveProductMasterModel.UpdatedDate == null ? "" : Convert.ToString(objArichiveProductMasterModel.UpdatedDate);
            txtFSEligibleAmount.Text = objArichiveProductMasterModel.IsFoodStamp == true ? Convert.ToString(objArichiveProductMasterModel.Price) : null;

            if (objArichiveProductMasterModel.ProductVendorID != null)
                cmbVendors.SelectedValue = objArichiveProductMasterModel.ProductVendorID;

            toggleFdStamp.Checked = objArichiveProductMasterModel.IsFoodStamp == null ? false : Convert.ToBoolean(Convert.ToString(objArichiveProductMasterModel.IsFoodStamp).ToLower());
            toggleActive.Checked = objArichiveProductMasterModel.IsActive == null ? false : Convert.ToBoolean(Convert.ToString(objArichiveProductMasterModel.IsActive).ToLower());
            ToggleScaled.Checked = objArichiveProductMasterModel.IsScaled == null ? false : Convert.ToBoolean(Convert.ToString(objArichiveProductMasterModel.IsScaled).ToLower());
            ToggleAgeVerify.Checked = objArichiveProductMasterModel.AgeVerification == null ? false : Convert.ToBoolean(Convert.ToString(objArichiveProductMasterModel.AgeVerification).ToLower());
            ToggleLabeled.Checked = objArichiveProductMasterModel.LabeledPrice == null ? false : Convert.ToBoolean(Convert.ToString(objArichiveProductMasterModel.LabeledPrice).ToLower());
            ToggleGroupPrice.Checked = objArichiveProductMasterModel.IsGroupPrice == null ? false : Convert.ToBoolean(Convert.ToString(objArichiveProductMasterModel.IsGroupPrice).ToLower());

            txtFSEligibleAmount.Enabled = toggleFdStamp.Checked == true && ToggleGroupPrice.Checked == false ? true : false;
            metroBtnProductvendor.Enabled = true;
            metroBtnSalePrice.Enabled = true;
            ProductID = objArichiveProductMasterModel.ProductID;
            //objFrmMetro_AddProduct.ShowDialog();
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

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtProductName);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtUPCCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtUPCCode);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtCertificateCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // CheckValidation(CommonTextBoxs.txtCertificateCode);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtPrice);
            }
            catch (Exception ex)
            {
                // _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }
        private void txtTareWeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtTareWeight);
            }
            catch (Exception ex)
            {
                // _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }
        private void txtTareWeight_TabIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtTareWeight.Text = Math.Round(Functions.GetDecimal(txtTareWeight.Text), 2, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception ex)
            {
                // _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }
        private void txtUnitCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtUnitCost);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDepartment.SelectedValue != null && cmbDepartment.SelectedIndex > 0)
                {
                    int departmentid = Functions.GetInteger(cmbDepartment.SelectedValue.ToString());
                    var SectionID = _db.tbl_SectionMaster.Where(p => p.DepartmentID == departmentid).Select(p => p.SectionID).FirstOrDefault();
                    var taxGroupId = _db.tbl_DepartmentMaster.Where(p => p.DepartmentID == departmentid).Select(p => p.TaxGroupID).FirstOrDefault();
                    if (SectionID != null)
                    {
                        if (taxGroupId != 0 && taxGroupId != null)
                        {
                            LoadCmbTaxGroupCode(Convert.ToInt32(taxGroupId));
                        }
                        else
                        {
                            LoadCmbTaxGroupCode(0);
                        }
                        LoadCmbSectionName(departmentid);
                    }
                    else
                    {
                        if (taxGroupId != 0 && taxGroupId != null)
                        {
                            LoadCmbTaxGroupCode(Convert.ToInt32(taxGroupId));
                        }
                        else
                        {
                            LoadCmbTaxGroupCode(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmMetro_AddProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            //PortOpen_Close(false);
            DeviceRemove();
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
            txtUPCCode.Text = UPCCode;
            if (myScanner.DataEventEnabled == false)
            {
                myScanner.DataEventEnabled = true;
            }
        }

        private void txtUPCCode_Leave(object sender, EventArgs e)
        {
            try
            {
                ProductService _ProductService = new ProductService();

                if (txtUPCCode.Text.Trim() != "")
                    txtUPCCode.Text = ClsCommon.GetFullUPCCode(txtUPCCode.Text.Trim());

                if (ProductID == 0)
                {
                    objArichiveProductMasterModel = _ProductService.GetArchivedUPCCode(txtUPCCode.Text.Trim());

                    if (objArichiveProductMasterModel != null && objArichiveProductMasterModel.ProductID > 0)
                    {
                        _ConfirmMessageBox = new ConfirmMessageBox();
                        _ConfirmMessageBox.lblTitle.Text = "Confirm Product Restore";
                        _ConfirmMessageBox.lblMsg.Text = "Entered UPC exists as an archived product, would you like to restore the existing product instead?";
                        _ConfirmMessageBox.OnMyEvent += new ConfirmMessageBox.onMyEventHandler(ConfirmEvent);
                        _ConfirmMessageBox.Show();
                        _ConfirmMessageBox.Dock = DockStyle.Fill;
                        _ConfirmMessageBox.BringToFront();
                    }
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "AddProduct" + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ConfirmEvent(object sender, EventArgs e)
        {
            try
            {
                if (_ConfirmMessageBox.IsCancel != true)
                {
                    ProductService _ProductService = new ProductService();
                    var add = _ProductService.AddProduct(objArichiveProductMasterModel, 4);
                    GetProductData();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void ToggleGroupPrice_CheckedChanged(object sender, EventArgs e)
        {
            txtFSEligibleAmount.Text = FSEligibleAmount == "" || FSEligibleAmount == null ? txtPrice.Text : FSEligibleAmount;
            if (toggleFdStamp.Checked == false)
            {
                txtFSEligibleAmount.Enabled = false;
                txtFSEligibleAmount.Text = null;
            }
            else if (toggleFdStamp.Checked == true && ToggleGroupPrice.Checked == false)
            {
                txtFSEligibleAmount.Enabled = true;
            }
            else
            {
                txtFSEligibleAmount.Enabled = false;
                //txtFSEligibleAmount.Text = FSEligibleAmount == "" || FSEligibleAmount == null ? txtPrice.Text : FSEligibleAmount;
                //txtFSEligibleAmount.Text = null;
            }
            //if (ToggleGroupPrice.Checked)
            //{
            //    txtLinkUPCCode.Text = null;
            //    txtLinkUPCCode.Enabled = false;
            //}
            //else
            //{
            //    txtLinkUPCCode.Enabled = true;
            //}
        }
        #endregion

        #region Functions
        public FrmMetro_AddProduct()
        {
            try
            {
                InitializeComponent();
                LoadCmbVendorName();
                LoadCmbDepartmentName();
                LoadCmbSectionName(0);
                LoadCmbUoMCode();
                LoadCmbTaxGroupCode(0);
                myPosExplorer = new PosExplorer(this);
                //ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                //OpenPort();

                DeviceAdd();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "AddProduct " + ex.StackTrace, ex.LineNumber());
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

        public void LoadCmbDepartmentName()
        {
            try
            {
                List<DepartmentMasterModel> lstDepartmentMasterModel = new List<DepartmentMasterModel>();
                DepartmentService _DepartmentService = new DepartmentService();

                lstDepartmentMasterModel = _DepartmentService.GetAllDepartment().Where(d => d.DepartmentNo != 0).ToList();
                lstDepartmentMasterModel.Insert(0, new DepartmentMasterModel { DepartmentID = 0, DepartmentName = DepartmentMasterModelCont.cmbDepartmentFirst });
                cmbDepartment.DisplayMember = DepartmentMasterModelCont.DepartmentName;
                cmbDepartment.ValueMember = DepartmentMasterModelCont.DepartmentID;
                cmbDepartment.DataSource = lstDepartmentMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbSectionName(int departmentid)
        {
            try
            {
                // departmentid = 0;
                List<SP_GetSectionList_Result_Model> lstSectionMasterModel = new List<SP_GetSectionList_Result_Model>();
                SectionService _SectionService = new SectionService();

                lstSectionMasterModel = _SectionService.SectionDetail(departmentid);
                if (lstSectionMasterModel.Count > 0)
                {
                    lstSectionMasterModel.Insert(0, new SP_GetSectionList_Result_Model { SectionID = 0, SectionName = SectionMasterModelCont.cmbSectionFirst });
                    cmbSection.DisplayMember = SectionMasterModelCont.SectionName;
                    cmbSection.ValueMember = SectionMasterModelCont.SectionID;
                    cmbSection.DataSource = lstSectionMasterModel;
                }
                else
                {
                    cmbSection.DataSource = null;
                    lstSectionMasterModel.Insert(0, new SP_GetSectionList_Result_Model { SectionID = 0, SectionName = SectionMasterModelCont.cmbSectionFirst });
                    cmbSection.DisplayMember = SectionMasterModelCont.SectionName;
                    cmbSection.ValueMember = SectionMasterModelCont.SectionID;
                    cmbSection.DataSource = lstSectionMasterModel;
                    cmbSection.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbUoMCode()
        {
            try
            {
                List<UoMMasterModel> lstUoMMasterModel = new List<UoMMasterModel>();
                UoMService _UoMService = new UoMService();

                lstUoMMasterModel = _UoMService.GetAllUoM();
                lstUoMMasterModel.Insert(0, new UoMMasterModel { UnitMeasureID = 0, UnitMeasureCode = UoMMasterModelCont.cmbUoMFirst });
                cmbUoM.DisplayMember = UoMMasterModelCont.UnitMeasureCode;
                cmbUoM.ValueMember = UoMMasterModelCont.UnitMeasureID;
                cmbUoM.DataSource = lstUoMMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbTaxGroupCode(long taxGroupId)
        {
            try
            {
                List<TaxGroupMasterModel> lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();
                TaxGroupService _TaxGroupService = new TaxGroupService();

                lstTaxGroupMasterModel = _TaxGroupService.GetAllTaxGroup();
                lstTaxGroupMasterModel.Insert(0, new TaxGroupMasterModel { TaxGroupID = 0, TaxGroupName = TaxGroupMasterModelCont.cmbTaxGroupCodeFirst });
                cmbTaxGroup.DataSource = lstTaxGroupMasterModel;
                if (taxGroupId == 0)
                {
                    cmbTaxGroup.DisplayMember = TaxGroupMasterModelCont.TaxGroupName;
                    cmbTaxGroup.ValueMember = TaxGroupMasterModelCont.TaxGroupID;
                }
                else
                {
                    TaxGroupMasterModel tblTaxGroup = new TaxGroupMasterModel();
                    tblTaxGroup = lstTaxGroupMasterModel.Where(p => p.TaxGroupID == taxGroupId && p.IsDelete == false).FirstOrDefault();
                    if(tblTaxGroup != null)
                        cmbTaxGroup.SelectedIndex = cmbTaxGroup.FindStringExact(tblTaxGroup.TaxGroupName);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtProductName:
                    //txtProductName
                    if (string.IsNullOrWhiteSpace(txtProductName.Text))
                    {
                        txtProductName.Focus();
                        ep.SetError(txtProductName, AlertMessages.ProductNameValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtProductName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtUPCCode:
                    //txtUPCCode
                    if (string.IsNullOrWhiteSpace(txtUPCCode.Text))
                    {
                        txtUPCCode.Focus();
                        ep.SetError(txtUPCCode, AlertMessages.ProductUpcCodeValid1);
                        status = false;
                    }
                    //else if ((!(Regex.Match(txtUPCCode.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    //{
                    //    txtUPCCode.Focus();
                    //    ep.SetError(txtUPCCode, AlertMessages.OnlyNumberAllow);
                    //    status = false;
                    //}
                    else
                    {
                        ep.SetError(txtUPCCode, CommonModelCont.EmptyString);
                    }
                    break;
                //case CommonTextBoxs.txtPalletQTY:
                //    //txtUPCCode
                //    if (string.IsNullOrWhiteSpace(txtPalletQTY.Text))
                //    {
                //        txtUPCCode.Focus();
                //        ep.SetError(txtPalletQTY, AlertMessages.ProductPalletQTY);
                //        status = false;
                //    }
                //    else
                //    {
                //        ep.SetError(txtPalletQTY, CommonModelCont.EmptyString);
                //    }
                //    break;
                case CommonTextBoxs.cmbDepartment:
                    //cmbDepartment
                    if ((string.IsNullOrWhiteSpace(cmbDepartment.Text)))
                    {
                        cmbDepartment.Focus();
                        ep.SetError(cmbDepartment, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbDepartment.SelectedIndex <= 0)
                    {
                        cmbDepartment.Focus();
                        ep.SetError(cmbDepartment, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbDepartment, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbSection:
                    //cmbSection
                    //if ((string.IsNullOrWhiteSpace(cmbSection.Text)))
                    //{
                    //    cmbSection.Focus();
                    //    ep.SetError(cmbSection, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbSection.SelectedIndex < 0)
                    //{
                    //    cmbDepartment.Focus();
                    //    ep.SetError(cmbSection, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbSection, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonTextBoxs.cmbTaxGroup:
                    //cmbTaxGroup
                    if ((string.IsNullOrWhiteSpace(cmbTaxGroup.Text)))
                    {
                        cmbTaxGroup.Focus();
                        ep.SetError(cmbTaxGroup, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbTaxGroup.SelectedIndex < 0)
                    {
                        cmbTaxGroup.Focus();
                        ep.SetError(cmbTaxGroup, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbTaxGroup, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbUoM:
                    //cmbUoM
                    if ((string.IsNullOrWhiteSpace(cmbUoM.Text)))
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbUoM.SelectedIndex < 0)
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbUoM, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtPrice:
                    //txtPrice
                    if ((string.IsNullOrWhiteSpace(txtPrice.Text)))
                    {
                        txtPrice.Focus();
                        ep.SetError(txtPrice, AlertMessages.SalesPriceValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtPrice.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success))
                    {
                        txtPrice.Focus();
                        ep.SetError(txtPrice, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Functions.GetDecimal(txtPrice.Text) < 0)
                    {
                        txtPrice.Focus();
                        ep.SetError(txtPrice, AlertMessages.PriceValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPrice, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtUnitCost:
                    //txtUnitCost
                    if ((string.IsNullOrWhiteSpace(txtUnitCost.Text)))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.SalesPriceValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtUnitCost.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Functions.GetDecimal(txtUnitCost.Text) < 0)
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.PriceValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtUnitCost, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtGroupPrice:
                    if (!string.IsNullOrWhiteSpace(txtGroupPrice.Text))
                    {
                        if ((!(Regex.Match(txtGroupPrice.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success))
                        {
                            txtGroupPrice.Focus();
                            ep.SetError(txtGroupPrice, AlertMessages.OnlyNumberAllow);
                            status = false;
                        }
                        else if (Functions.GetDecimal(txtGroupPrice.Text) < 0)
                        {
                            txtGroupPrice.Focus();
                            ep.SetError(txtGroupPrice, AlertMessages.PriceValidation);
                            status = false;
                        }
                        else
                        {
                            ep.SetError(txtGroupPrice, CommonModelCont.EmptyString);
                        }
                    }
                    break;
                case CommonTextBoxs.txtTareWeight:
                    //txtTareWeight

                    //if ((!(Regex.Match(txtTareWeight.Text, CommonModelCont.positive_decimal)).Success))
                    //{
                    //    txtTareWeight.Focus();
                    //    ep.SetError(txtTareWeight, AlertMessages.OnlyNumberAllow);
                    //    status = false;
                    //}
                    //else 
                    if (Functions.GetDecimal(txtTareWeight.Text) < 0)
                    {
                        txtTareWeight.Focus();
                        ep.SetError(txtTareWeight, AlertMessages.TareWeigthValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtTareWeight, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtFSEligibleAmount:
                    if(Functions.GetDecimal(txtFSEligibleAmount.Text) > Functions.GetDecimal(txtPrice.Text))
                    {
                        txtFSEligibleAmount.Clear();
                        txtFSEligibleAmount.Focus();
                        ep.SetError(txtFSEligibleAmount, AlertMessages.FSAmountValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtFSEligibleAmount, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtProductName
                    if (string.IsNullOrWhiteSpace(txtProductName.Text))
                    {
                        txtProductName.Focus();
                        ep.SetError(txtProductName, AlertMessages.ProductNameValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtProductName, CommonModelCont.EmptyString);
                    }
                    //txtUPCCode
                    if (string.IsNullOrWhiteSpace(txtUPCCode.Text))
                    {
                        txtUPCCode.Focus();
                        ep.SetError(txtUPCCode, AlertMessages.ProductUpcCodeValid1);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtUPCCode, CommonModelCont.EmptyString);
                    }

                    //if (string.IsNullOrWhiteSpace(txtPalletQTY.Text))
                    //{
                    //    txtUPCCode.Focus();
                    //    ep.SetError(txtPalletQTY, AlertMessages.ProductPalletQTY);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtPalletQTY, CommonModelCont.EmptyString);
                    //}

                    //cmbDepartment
                    if ((string.IsNullOrWhiteSpace(cmbDepartment.Text)))
                    {
                        cmbDepartment.Focus();
                        ep.SetError(cmbDepartment, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbDepartment.SelectedIndex <= 0)
                    {
                        cmbDepartment.Focus();
                        ep.SetError(cmbDepartment, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbDepartment, CommonModelCont.EmptyString);
                    }
                    //cmbSection
                    //if ((string.IsNullOrWhiteSpace(cmbSection.Text)))
                    //{
                    //    cmbSection.Focus();
                    //    ep.SetError(cmbSection, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbSection.SelectedIndex < 0)
                    //{
                    //    cmbDepartment.Focus();
                    //    ep.SetError(cmbSection, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbSection, CommonModelCont.EmptyString);
                    //}
                    //cmbTaxGroup
                    if ((string.IsNullOrWhiteSpace(cmbTaxGroup.Text)))
                    {
                        cmbTaxGroup.Focus();
                        ep.SetError(cmbTaxGroup, AlertMessages.TaxValid);
                        status = false;
                    }
                    else if (cmbTaxGroup.SelectedIndex < 0)
                    {
                        cmbTaxGroup.Focus();
                        ep.SetError(cmbTaxGroup, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbTaxGroup, CommonModelCont.EmptyString);
                    }
                    //txtPrice
                    if ((string.IsNullOrWhiteSpace(txtPrice.Text)))
                    {
                        txtPrice.Focus();
                        ep.SetError(txtPrice, AlertMessages.SalesPriceValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtPrice.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success))
                    {
                        txtPrice.Focus();
                        ep.SetError(txtPrice, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Functions.GetDecimal(txtPrice.Text) < 0)
                    {
                        txtPrice.Focus();
                        ep.SetError(txtPrice, AlertMessages.PriceValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPrice, CommonModelCont.EmptyString);
                    }
                    //txtTareWeight

                    //if ((!(Regex.Match(txtTareWeight.Text, CommonModelCont.positive_decimal)).Success))
                    // {
                    //     txtTareWeight.Focus();
                    //     ep.SetError(txtTareWeight, AlertMessages.OnlyNumberAllow);
                    //     status = false;
                    // }
                    // else 
                    if (Functions.GetDecimal(txtTareWeight.Text) < 0)
                    {
                        txtTareWeight.Focus();
                        ep.SetError(txtTareWeight, AlertMessages.PriceValidation);
                        status = false;
                    }

                    //txtUnitCost
                    if ((string.IsNullOrWhiteSpace(txtUnitCost.Text)))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.SalesPriceValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtUnitCost.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Functions.GetDecimal(txtUnitCost.Text) < 0)
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.PriceValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtUnitCost, CommonModelCont.EmptyString);
                    }

                    //txtPack
                    if ((string.IsNullOrWhiteSpace(txtPack.Text)))
                    {

                    }
                    else if ((!(Regex.Match(txtPack.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtPack, AlertMessages.PackValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPack, CommonModelCont.EmptyString);
                    }

                    //cmbUoM
                    if ((string.IsNullOrWhiteSpace(cmbUoM.Text)))
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbUoM.SelectedIndex < 0)
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbUoM, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtProductName.Text = null;
            ep.SetError(txtProductName, CommonModelCont.EmptyString);

            txtUPCCode.Text = null;
            ep.SetError(txtUPCCode, CommonModelCont.EmptyString);

            txtCertificateCode.Text = null;
            ep.SetError(txtCertificateCode, CommonModelCont.EmptyString);

            txtPrice.Text = null;
            ep.SetError(txtPrice, CommonModelCont.EmptyString);

            cmbDepartment.SelectedIndex = 0;
            ep.SetError(cmbDepartment, CommonModelCont.EmptyString);
            cmbSection.SelectedIndex = 0;
            ep.SetError(cmbSection, CommonModelCont.EmptyString);
            cmbUoM.SelectedIndex = 0;
            ep.SetError(cmbSection, CommonModelCont.EmptyString);
            cmbTaxGroup.SelectedIndex = 0;
            ep.SetError(cmbTaxGroup, CommonModelCont.EmptyString);
            ImageProduct.Image = new Bitmap(Resources.sf_);
            ImageFileName = null;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_ProductMaster");
        }

        public void AddSalePrice()
        {
            try
            {
                FrmProductSalesPrice objFrmProductSalesPrice = new FrmProductSalesPrice();
                objFrmProductSalesPrice.ProductId = ProductID;
                objFrmProductSalesPrice.productName = txtProductName.Text;
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void AddVendor()
        {
            try
            {
                this.Close();
                FrmProductVendor objFrmProductVendor = new FrmProductVendor();
                objFrmProductVendor.ProductId = ProductID;
                objFrmProductVendor.productName = txtProductName.Text;
                objFrmProductVendor.dataLoad();

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
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }



        #endregion

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSection.SelectedValue != null && cmbSection.SelectedIndex > 0)
            {
                int sectionid = Functions.GetInteger(cmbSection.SelectedValue.ToString());
                var taxGroupId = _db.tbl_SectionMaster.Where(p => p.SectionID == sectionid).Select(p => p.TaxGroupID).FirstOrDefault();
                if (taxGroupId != 0 && taxGroupId != null)
                {
                    LoadCmbTaxGroupCode(Convert.ToInt32(taxGroupId));
                }
                else
                {
                    LoadCmbTaxGroupCode(0);
                }
            }
            else
            {
                if (cmbDepartment.SelectedValue != null && cmbDepartment.SelectedIndex > 0)
                {
                    int departmentId = Functions.GetInteger(cmbDepartment.SelectedValue.ToString());
                    var taxGroupId = _db.tbl_DepartmentMaster.Where(p => p.DepartmentID == departmentId).Select(p => p.TaxGroupID).FirstOrDefault();
                    LoadCmbTaxGroupCode(Convert.ToInt32(taxGroupId));
                }
            }
        }

        private void txtGroupPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtGroupPrice);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }

        }

        private void toggleFdStamp_CheckedChanged(object sender, EventArgs e)
        {
            txtFSEligibleAmount.Text = FSEligibleAmount == "" || FSEligibleAmount == null ? txtPrice.Text : FSEligibleAmount;
            if (toggleFdStamp.Checked == false)
            {
                btnChangeFSAmount.Visible = false;
                txtFSEligibleAmount.Enabled = false;
                txtFSEligibleAmount.Text = null;
            }
            else if (toggleFdStamp.Checked == true && ToggleGroupPrice.Checked == false)
            {
                btnChangeFSAmount.Enabled = true;
                btnChangeFSAmount.Visible = true;
                //txtFSEligibleAmount.Enabled = true;
            }
            else
            {
                btnChangeFSAmount.Enabled = true;
                btnChangeFSAmount.Visible = true;
                //txtFSEligibleAmount.Enabled = false;
                //txtFSEligibleAmount.Text = FSEligibleAmount == "" || FSEligibleAmount == null ? txtPrice.Text : FSEligibleAmount;
                //txtFSEligibleAmount.Text = null;
            }
        }

        private void txtFSEligibleAmount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtFSEligibleAmount);
                FSEligibleAmount = txtFSEligibleAmount.Text;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (Price == txtFSEligibleAmount.Text)
            {
                Price = txtPrice.Text;
                FSEligibleAmount = txtPrice.Text;
                txtFSEligibleAmount.Text = txtPrice.Text;
            }
        }

        private void cmbVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cmbVendors.SelectedItem)
            //string a = cmbVendors.SelectedItem.ToString();

            if (cmbVendors.SelectedIndex > -1)
            {
                VendorMasterModel data = (VendorMasterModel)cmbVendors.SelectedItem;
                //PrimaryId = Convert.ToInt64(Convert.ToString(ProductGrdView.Rows[e.RowIndex].Cells[ProductMasterModelCont.ProductID].Value));
                if (data.VendorID != 0 && ProductID != 0)
                {
                    var productvendorlist = FieldsDefaultVendor(ProductID, data.VendorID);
                    txtUnitCost.Text = productvendorlist == null ? "" : productvendorlist.UnitCost.HasValue == false ? "" : productvendorlist.UnitCost.ToString();
                    txtCertificateCode.Text = productvendorlist == null ? "" : productvendorlist.ItemCode == null ? "" : productvendorlist.ItemCode.ToString();
                }
            }
        }

        public ProductVendorMasterModel FieldsDefaultVendor(long ProductId, long VendorId)
        {
            try
            {
                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                lstProductVendorMasterModel = _ProductVendorService.GetAllProductVendor(ProductId);
                var productVendorByDefault = lstProductVendorMasterModel.Where(l => l.VendorID == VendorId).FirstOrDefault();
                return productVendorByDefault;

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());

            }
            return null;
        }

        private void btnChangeFSAmount_Click(object sender, EventArgs e)
        {
            #region Extra code 
            DialogResult result = MessageBox.Show(this, "FS Eligible Amount field should only be changed for CRV items, \n are you sure you want to change..?", AlertMessages.ConfirmAlert, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //txtFSEligibleAmount.ReadOnly = false;
                txtFSEligibleAmount.Enabled = true;
            }
            #endregion
        }
    }
}
