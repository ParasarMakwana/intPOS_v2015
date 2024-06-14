using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddProducSalePrice : MetroForm
    {
        #region Properties
        public long ProductId { get; set; }
        public long PrimaryId = 0;
        public string productName = "";

        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion
        
        #region Events

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                bool IsExists = false;
                if (flagSave)
                {
                    ProductSalesPriceService _ProductSalesPriceService = new ProductSalesPriceService();

                    IsExists = _ProductSalesPriceService.CheckUniqueDate(Convert.ToDateTime(datePickerStartDate.Value), Convert.ToDateTime(datePickerEndDate.Value), PrimaryId);
                    if (!IsExists)
                    {
                        ProductSalesPriceMasterModel objProductSalesPriceMasterModel = new ProductSalesPriceMasterModel();

                        objProductSalesPriceMasterModel.ProductID = ProductId;
                        objProductSalesPriceMasterModel.SellPrice = Convert.ToDecimal(txtSalesPrice.Text);
                        objProductSalesPriceMasterModel.StartDate = Convert.ToDateTime(datePickerStartDate.Value);
                        objProductSalesPriceMasterModel.EndDate = Convert.ToDateTime(datePickerEndDate.Value);
                        if (PrimaryId <= 0)
                        {
                            var add = _ProductSalesPriceService.AddProductSalesPrice(objProductSalesPriceMasterModel, 1);
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                        else if (PrimaryId > 0)
                        {
                            objProductSalesPriceMasterModel.ProductSalePriceID = PrimaryId;
                            var add = _ProductSalesPriceService.AddProductSalesPrice(objProductSalesPriceMasterModel, 2);
                            PrimaryId = 0;
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Update, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                        UpdateLog();
                    }
                    else
                    {
                        ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.AlreadyExist, false);
                    }
                    Clear();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
            }
        }
        
        private void txtSalesPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtSalesPrice);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSalePrice + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmMetro_AddProducSalePrice_Load(object sender, EventArgs e)
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

        #endregion

        #region Functions
        public FrmMetro_AddProducSalePrice()
        {
            InitializeComponent();
        }
        
        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtSalesPrice:
                    //txtSalesPrice
                    if ((string.IsNullOrWhiteSpace(txtSalesPrice.Text)))
                    {
                        txtSalesPrice.Focus();
                        ep.SetError(txtSalesPrice, AlertMessages.SalesPriceValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtSalesPrice.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtSalesPrice.Focus();
                        ep.SetError(txtSalesPrice, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtSalesPrice.Text) <= 0)
                    {
                        txtSalesPrice.Focus();
                        ep.SetError(txtSalesPrice, AlertMessages.PriceValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtSalesPrice, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.datePickerStartDate:
                    //datePickerStartDate
                    if (datePickerStartDate.Value < DateTime.Now.Date)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.EndDateValid1);
                        status = false;
                    }
                    else if (datePickerEndDate.Value < datePickerStartDate.Value)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.EndDateValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerStartDate, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.datePickerEndDate:
                    //datePickerEndDate
                    if (datePickerEndDate.Value < DateTime.Now.Date)
                    {
                        datePickerEndDate.Focus();
                        ep.SetError(datePickerEndDate, AlertMessages.EndDateValid1);
                        status = false;
                    }
                    else if (datePickerEndDate.Value < datePickerStartDate.Value)
                    {
                        datePickerEndDate.Focus();
                        ep.SetError(datePickerEndDate, AlertMessages.EndDateValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerEndDate, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtSalesPrice
                    if ((string.IsNullOrWhiteSpace(txtSalesPrice.Text)))
                    {
                        txtSalesPrice.Focus();
                        ep.SetError(txtSalesPrice, AlertMessages.SalesPriceValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtSalesPrice.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtSalesPrice.Focus();
                        ep.SetError(txtSalesPrice, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtSalesPrice.Text) <= 0)
                    {
                        txtSalesPrice.Focus();
                        ep.SetError(txtSalesPrice, AlertMessages.PriceValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtSalesPrice, CommonModelCont.EmptyString);
                    }
                    //datePickerStartDate
                    if (datePickerStartDate.Value < DateTime.Now.Date)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.StartDateValid);
                        status = false;
                    }
                    else if (datePickerEndDate.Value < datePickerStartDate.Value)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.StartDateValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerStartDate, CommonModelCont.EmptyString);
                    }
                    //datePickerEndDate
                    if (datePickerEndDate.Value < DateTime.Now.Date)
                    {
                        datePickerEndDate.Focus();
                        ep.SetError(datePickerEndDate, AlertMessages.EndDateValid1);
                        status = false;
                    }
                    else if (datePickerEndDate.Value < datePickerStartDate.Value)
                    {
                        datePickerEndDate.Focus();
                        ep.SetError(datePickerEndDate, AlertMessages.EndDateValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerEndDate, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtSalesPrice.Text = null;
            ep.SetError(txtSalesPrice, CommonModelCont.EmptyString);
            datePickerStartDate.Text = DateTime.Now.Date.ToString();
            ep.SetError(datePickerStartDate, CommonModelCont.EmptyString);
            datePickerEndDate.Text = DateTime.Now.Date.ToString();
            ep.SetError(datePickerEndDate, CommonModelCont.EmptyString);


            flagSave = false;
            PrimaryId = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_ProductSalePriceMaster");
        }
        #endregion

      
    }
}
