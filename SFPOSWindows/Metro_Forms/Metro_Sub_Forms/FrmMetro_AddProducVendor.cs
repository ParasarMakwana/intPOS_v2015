using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddProducVendor : MetroForm
    {

        #region Properties
        public long ProductId { get; set; }
        public long PrimaryId = 0;
        public string productName = "";
        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ProductVendorMasterModel objProductVendorMasterModel = new ProductVendorMasterModel();
        ProductVendorService _ProductVendorService = new ProductVendorService();
        ProductService _ProductService = new ProductService();
        VendorService _VendorService = new VendorService();
        ErrorProvider ep = new ErrorProvider();
        List<VendorMasterModel> lstVendorMasterModel = new List<VendorMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();

        bool flagSave = false;
        #endregion
        public FrmMetro_AddProducVendor()
        {
            InitializeComponent();
        }

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    bool IsProductUPC = _ProductVendorService.CheckProductVendorName(ProductId, Convert.ToInt64(cmbVendorName.SelectedValue));
                    //string VendorItemCode = _ProductVendorService.CheckProductItemCode(txtVendorUPCCode.Text, ProductId, Convert.ToInt64(cmbVendorName.SelectedValue));

                    objProductVendorMasterModel.ProductID = ProductId;
                    objProductVendorMasterModel.ItemCode = txtVendorUPCCode.Text.Trim();
                    objProductVendorMasterModel.VendorID = Convert.ToInt64(cmbVendorName.SelectedValue);
                    objProductVendorMasterModel.UnitCost = Convert.ToDecimal(txtUnitCost.Text.ToString().Trim());
                    if (toggleDefault.Checked)
                        objProductVendorMasterModel.IsDefault = true;
                    else
                        objProductVendorMasterModel.IsDefault = false;

                    if (!IsProductUPC)
                    {

                        if (PrimaryId <= 0)
                        {
                            var add = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 1);
                            if (add != null)
                            {
                                UpdateVendor(objProductVendorMasterModel.ProductID, objProductVendorMasterModel.VendorID);
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                        else if (PrimaryId > 0)
                        {
                            objProductVendorMasterModel.ProductVendorID = PrimaryId;
                            var add = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 2);
                            if (add != null)
                            {
                                UpdateVendor(objProductVendorMasterModel.ProductID, objProductVendorMasterModel.VendorID);
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Update, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                    }
                    else
                    {
                        if (PrimaryId > 0)
                        {
                            objProductVendorMasterModel.ProductVendorID = PrimaryId;
                            var add = _ProductVendorService.AddProductVendor(objProductVendorMasterModel, 2);
                            if (add != null)
                            {
                                UpdateVendor(objProductVendorMasterModel.ProductID, objProductVendorMasterModel.VendorID);
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Update, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.AlreadyExist, false);
                        }
                    }
                    Clear();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void metroBtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtVendorUPCCode:
                    //txtVendorUPCCode
                    if (string.IsNullOrWhiteSpace(txtVendorUPCCode.Text))
                    {
                        //txtVendorUPCCode.Focus();
                        //ep.SetError(txtVendorUPCCode, AlertMessages.VendorItemCodeValid);
                        //status = false;
                    }
                    else if ((!(Regex.Match(txtVendorUPCCode.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        //txtVendorUPCCode.Focus();
                        //ep.SetError(txtVendorUPCCode, AlertMessages.OnlyNumberAllow);
                        //status = false;
                    }
                    else
                    {
                        ep.SetError(txtVendorUPCCode, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbVendorName:
                    //cmbVendorName
                    if ((string.IsNullOrWhiteSpace(cmbVendorName.Text)))
                    {
                        cmbVendorName.Focus();
                        ep.SetError(cmbVendorName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbVendorName.SelectedIndex < 0)
                    {
                        cmbVendorName.Focus();
                        ep.SetError(cmbVendorName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbVendorName, CommonModelCont.EmptyString);
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
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtVendorUPCCode
                    if (string.IsNullOrWhiteSpace(txtVendorUPCCode.Text))
                    {
                        //txtVendorUPCCode.Focus();
                        //ep.SetError(txtVendorUPCCode, AlertMessages.ProductVendorUpcCodeValid);
                        //status = false;
                    }
                    else if ((!(Regex.Match(txtVendorUPCCode.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        //txtVendorUPCCode.Focus();
                        //ep.SetError(txtVendorUPCCode, AlertMessages.OnlyNumberAllow);
                        //status = false;
                    }
                    else
                    {
                        ep.SetError(txtVendorUPCCode, CommonModelCont.EmptyString);
                    }
                    //cmbVendorName
                    if ((string.IsNullOrWhiteSpace(cmbVendorName.Text)))
                    {
                        cmbVendorName.Focus();
                        ep.SetError(cmbVendorName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbVendorName.SelectedIndex < 0)
                    {
                        cmbVendorName.Focus();
                        ep.SetError(cmbVendorName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbVendorName, CommonModelCont.EmptyString);
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
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtVendorUPCCode.Text = null;
            ep.SetError(txtVendorUPCCode, CommonModelCont.EmptyString);
            txtUnitCost.Text = null;
            ep.SetError(txtUnitCost, CommonModelCont.EmptyString);
            cmbVendorName.SelectedIndex = 0;
            ep.SetError(cmbVendorName, CommonModelCont.EmptyString);


            flagSave = false;
            PrimaryId = 0;
        }

        public void UpdateVendor(long? ProductID, long? VendorID)
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
                tbl1.UpdatedBy = LoginInfo.UserId;
                tbl1.UpdatedDate = DateTime.Now;
            }
            _db.SaveChanges();
        }

        public void LoadCmbProductName()
        {
            try
            {
                //List<ProductMasterModel> lstProductMasterModel = new List<ProductMasterModel>();
                //lstProductMasterModel = _ProductService.GetAllProduct();
                //cmbProductName.DisplayMember = ProductMasterModelCont.ProductName;
                //cmbProductName.ValueMember = ProductMasterModelCont.ProductID;
                //cmbProductName.DataSource = lstProductMasterModel;
                //cmbProductName.SelectedValue = ProductId;
                //cmbProductName.Enabled = false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbVendorName()
        {
            try
            {
                lstVendorMasterModel = _VendorService.GetAllVendor();
                //lstVendorMasterModel.Insert(0, new VendorMasterModel { VendorID = 0, VendorName = VendorMasterModelCont.cmbVendorFirst });
                cmbVendorName.DisplayMember = VendorMasterModelCont.VendorName;
                cmbVendorName.ValueMember = VendorMasterModelCont.VendorID;
                cmbVendorName.DataSource = lstVendorMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtVendorUPCCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtVendorUPCCode);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmMetro_AddProducVendor_Load(object sender, EventArgs e)
        {
            try
            {
                txtProductName.Text = productName;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
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
    }
}
