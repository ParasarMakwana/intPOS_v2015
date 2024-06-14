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

namespace SFPOSWindows.MasterForm
{
    public partial class AddEditPurchaseHeaderLine : Form
    {
        #region Properties
        public SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(CommonModelCont.EmptyString);
        List<ProductMasterModel> lstProductMasterModel = new List<ProductMasterModel>();
        ErrorProvider ep = new ErrorProvider();
        ProductService _ProductService = new ProductService();
        PurchaseLineService _ProductPurchaseHeaderLineService = new PurchaseLineService();
        public long PrimaryID = 0;
        public long ProductId = 0;
        public string ItemCode = CommonModelCont.EmptyString;
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        #region Events
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    PurchaseLineMasterModel objProductPurchaseHeaderLineMasterModel = new PurchaseLineMasterModel();
                    //objProductPurchaseHeaderLineMasterModel.ProductID = Convert.ToInt32(cmbProdName.SelectedValue);
                    objProductPurchaseHeaderLineMasterModel.ProductID = ProductId;
                    objProductPurchaseHeaderLineMasterModel.ItemCode = ItemCode;
                    objProductPurchaseHeaderLineMasterModel.PurchaseHeaderID = Convert.ToInt32(PrimaryID);
                    objProductPurchaseHeaderLineMasterModel.Quantity = Convert.ToDecimal(txtQty.Text);
                    objProductPurchaseHeaderLineMasterModel.Tax = Convert.ToDecimal(txtTax.Text);
                    objProductPurchaseHeaderLineMasterModel.TaxAmount = Convert.ToDecimal(txtTaxAmt.Text);
                    objProductPurchaseHeaderLineMasterModel.UnitCost = Convert.ToDecimal(txtUnitCost.Text);
                    objProductPurchaseHeaderLineMasterModel.LineAmtExclTax = Convert.ToDecimal(txtExclTax.Text);
                    objProductPurchaseHeaderLineMasterModel.LineAmtInclTax = Convert.ToDecimal(txtInclTax.Text);
                    objProductPurchaseHeaderLineMasterModel.TaxGroupID = Convert.ToInt64(cmbTaxGroup.SelectedValue);
                    objProductPurchaseHeaderLineMasterModel.PurchaseType = "Received";
                    var add = _ProductPurchaseHeaderLineService.AddEditDeletePurchaseOrder(objProductPurchaseHeaderLineMasterModel, 1);
                    if (add != null)
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Add, AlertMessages.SuccessAlert, MessageBoxButtons.OK);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Error, AlertMessages.InformationAlert, MessageBoxButtons.OK);
                    }
                    Clear();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonTextBoxs.txtQty);
                if (flagSave)
                    cal();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtUnitCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonTextBoxs.txtUnitCost);
                if (flagSave)
                    cal();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtTax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonTextBoxs.txtTax);
                if (flagSave)
                    cal();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbProdName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.cmbProdName);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void rbtnUpcCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnUpcCode.Checked)
            {
                lblUpcItem.Text = CommonTextBoxs.UPC_Code;
                txtUpcItem.Text = CommonModelCont.EmptyString;
                txtProductName.Text = CommonModelCont.EmptyString;
            }
            else { lblUpcItem.Text = CommonTextBoxs.Item_Code; }
        }

        private void rBtnItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnItem.Checked)
            {
                lblUpcItem.Text = CommonTextBoxs.Item_Code;
                txtProductName.Text = CommonModelCont.EmptyString;
                txtUpcItem.Text = CommonModelCont.EmptyString;
            }
            else { lblUpcItem.Text = CommonTextBoxs.UPC_Code; }
        }

        private void txtUpcItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtUpcItem);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtUpcItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getproductName(txtUpcItem.Text);
            }
        }

        private void txtUpcItem_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUpcItem.Text))
            {
                getproductName(txtUpcItem.Text);
            }
        }

        private void cmbTaxGroup_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.cmbTaxGroup);
               // getTax(Convert.ToInt64(cmbTaxGroup.SelectedValue));
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region Functions
        public AddEditPurchaseHeaderLine()
        {
            InitializeComponent();
        }

        //public void cmbProductName()
        //{
        //    try
        //    {
        //        var onjtbl_ProductUoM = (from pu in _db.tbl_ProductMaster.Where(o => o.IsDelete == false)

        //                                 join pm in _db.tbl_ProductVendorMaster.Where(o => o.IsDelete == false)
        //                                 on pu.ProductID equals pm.ProductID
        //                                 join ph in _db.tbl_PurchaseHeader.Where(o => o.IsDelete == false
        //                                                                            && o.isReceived == false
        //                                                                            && o.PurchaseHeaderID == PrimaryID)
        //                                 on pm.VendorID equals ph.VendorID
        //                                 select new
        //                                 {
        //                                     ProductName = pu.ProductName,
        //                                     ProductID = pu.ProductID,
        //                                     UPCCode = pu.UPCCode,
        //                                 }).ToList();


        //        cmbProdName.ValueMember = ProductMasterModelCont.ProductID;
        //        cmbProdName.DisplayMember = ProductMasterModelCont.ProductName;
        //        cmbProdName.DataSource = onjtbl_ProductUoM;
        //    }
        //    catch (Exception ex)
        //    {
        //        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
        //    }
        //}

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtQty:
                    //txtQty
                    if ((string.IsNullOrWhiteSpace(txtQty.Text)))
                    {
                        txtQty.Focus();
                        ep.SetError(txtQty, AlertMessages.LineQuantityValid);
                        status = false;
                    }
                    else if((!(Regex.Match(txtQty.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        txtQty.Focus();
                        ep.SetError(txtQty, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtQty, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtUnitCost:
                    //txtUnitCost
                    if ((string.IsNullOrWhiteSpace(txtUnitCost.Text)))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.LineUnitCostValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtUnitCost.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtUnitCost.Text) <= 0)
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
                case CommonTextBoxs.txtTax:
                    //txtTax
                    if ((string.IsNullOrWhiteSpace(txtTax.Text)))
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.TaxValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtTax.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtTax.Text) > 100)
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.TaxValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtTax, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbTaxGroup:
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
                    break;
                //case CommonTextBoxs.cmbProdName:
                //cmbProdName
                //if ((string.IsNullOrWhiteSpace(cmbProdName.Text)))
                //{
                //    cmbProdName.Focus();
                //    ep.SetError(cmbProdName, AlertMessages.DropdownValidation);
                //    status = false;
                //}
                //else if (cmbProdName.SelectedIndex < 0)
                //{
                //    cmbProdName.Focus();
                //    ep.SetError(cmbProdName, AlertMessages.DropdownValidation);
                //    status = false;
                //}
                //else
                //{
                //    ep.SetError(cmbProdName, CommonModelCont.EmptyString);
                //}
                //break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtQty
                    if ((string.IsNullOrWhiteSpace(txtQty.Text)))
                    {
                        txtQty.Focus();
                        ep.SetError(txtQty, AlertMessages.LineQuantityValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtQty.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        txtQty.Focus();
                        ep.SetError(txtQty, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtQty, CommonModelCont.EmptyString);
                    }
                    //txtUnitCost
                    if ((string.IsNullOrWhiteSpace(txtUnitCost.Text)))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.LineUnitCostValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtUnitCost.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtUnitCost.Text) <= 0)
                    {
                        txtUnitCost.Focus();
                        ep.SetError(txtUnitCost, AlertMessages.PriceValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtUnitCost, CommonModelCont.EmptyString);
                    }
                    //txtTax
                    if ((string.IsNullOrWhiteSpace(txtTax.Text)))
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.TaxValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtTax.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtTax.Text) > 100)
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.TaxValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtTax, CommonModelCont.EmptyString);
                    }
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
                    //cmbProdName
                    //if ((string.IsNullOrWhiteSpace(cmbProdName.Text)))
                    //{
                    //    cmbProdName.Focus();
                    //    ep.SetError(cmbProdName, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbProdName.SelectedIndex < 0)
                    //{
                    //    cmbProdName.Focus();
                    //    ep.SetError(cmbProdName, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbProdName, CommonModelCont.EmptyString);
                    //}

                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtExclTax.Text = null;
            txtInclTax.Text = null;
            txtQty.Text = null;
            txtTax.Text = null;
            txtTaxAmt.Text = null;
            txtUnitCost.Text = null;
            Close();
            ep.SetError(txtQty, CommonModelCont.EmptyString);
        }

        public void cal()
        {
            int Qty = txtQty.Text != CommonModelCont.EmptyString ? Convert.ToInt32(txtQty.Text) : 0;   //10
            decimal Price = txtUnitCost.Text != CommonModelCont.EmptyString ? Convert.ToDecimal(txtUnitCost.Text) : 0;//100
            decimal Tax = txtTax.Text != CommonModelCont.EmptyString ? Convert.ToDecimal(txtTax.Text) : 0; //10 %
            decimal totalAmount = Qty * Price;
            decimal InclTax = totalAmount + (totalAmount / 100) * Tax;

            txtTaxAmt.Text = ((totalAmount / 100) * Tax).ToString();
            txtExclTax.Text = totalAmount.ToString();
            txtInclTax.Text = InclTax.ToString();
        }

        public void getproductName(string Code)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtUpcItem.Text))
                {
                    if (rbtnUpcCode.Checked)
                    {
                        var objtbl_Product = (from pu in _db.tbl_ProductMaster.Where(o => o.IsDelete == false && o.UPCCode == Code)
                                              join pm in _db.tbl_ProductVendorMaster.Where(o => o.IsDelete == false)
                                              on pu.ProductID equals pm.ProductID
                                              join ph in _db.tbl_PurchaseHeader.Where(o => o.IsDelete == false
                                                                                         && o.isReceived == false
                                                                                         && o.PurchaseHeaderID == PrimaryID
                                                                                         )
                                              on pm.VendorID equals ph.VendorID
                                              select new
                                              {
                                                  ProductName = pu.ProductName,
                                                  ProductID = pu.ProductID,
                                                  UPCCode = pu.UPCCode,
                                                  ItemCode = pm.ItemCode
                                              }).ToList();
                        if (objtbl_Product.Count != 0)
                        {
                            txtProductName.Text = objtbl_Product.FirstOrDefault().ProductName;
                            ItemCode = objtbl_Product.FirstOrDefault().ItemCode;
                            ProductId = objtbl_Product.FirstOrDefault().ProductID;
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("UPC code not exists for this vendor!!", AlertMessages.InformationAlert, MessageBoxButtons.OK);
                            txtUpcItem.Text = CommonModelCont.EmptyString;
                        }
                    }
                    else if (rBtnItem.Checked)
                    {
                        var objtbl_Product = (from pu in _db.tbl_ProductMaster.Where(o => o.IsDelete == false)
                                              join pm in _db.tbl_ProductVendorMaster.Where(o => o.IsDelete == false && o.ItemCode == Code)
                                              on pu.ProductID equals pm.ProductID
                                              join ph in _db.tbl_PurchaseHeader.Where(o => o.IsDelete == false
                                                                                         && o.isReceived == false
                                                                                         && o.PurchaseHeaderID == PrimaryID
                                                                                         )
                                              on pm.VendorID equals ph.VendorID
                                              select new
                                              {
                                                  ProductName = pu.ProductName,
                                                  ProductID = pu.ProductID,
                                                  UPCCode = pu.UPCCode,
                                                  ItemCode = pm.ItemCode
                                              }).ToList();

                        if (objtbl_Product.Count != 0)
                        {
                            txtProductName.Text = objtbl_Product.FirstOrDefault().ProductName;
                            ItemCode = objtbl_Product.FirstOrDefault().ItemCode;
                            ProductId = objtbl_Product.FirstOrDefault().ProductID;
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("Item code not exists for this vendor!!", AlertMessages.InformationAlert, MessageBoxButtons.OK);
                            txtUpcItem.Text = CommonModelCont.EmptyString;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmAddEditPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadCmbTaxGroupCode()
        {
            try
            {
                List<TaxGroupMasterModel> lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();
                TaxGroupService _TaxGroupService = new TaxGroupService();

                lstTaxGroupMasterModel = _TaxGroupService.GetAllTaxGroup();

                lstTaxGroupMasterModel.Insert(0, new TaxGroupMasterModel { TaxGroupID = 0, TaxGroupName = TaxGroupMasterModelCont.cmbTaxGroupCodeFirst });
                cmbTaxGroup.DisplayMember = TaxGroupMasterModelCont.TaxGroupName;
                cmbTaxGroup.ValueMember = TaxGroupMasterModelCont.TaxGroupID;
                cmbTaxGroup.DataSource = lstTaxGroupMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }

        public void getTax(long TaxGroupID)
        {
            var objtbl_TaxRateMaster = (from TR in _db.tbl_TaxRateMaster.Where(o => o.IsDelete == false && o.TaxGroupID == TaxGroupID)
                                        select new
                                        {
                                            Tax = TR.Tax  
                                        }).ToList();
            if (objtbl_TaxRateMaster.Count != 0)
            {
                txtTax.Text = (objtbl_TaxRateMaster.FirstOrDefault().Tax).ToString();
            }

        }


        #endregion


    }
}
