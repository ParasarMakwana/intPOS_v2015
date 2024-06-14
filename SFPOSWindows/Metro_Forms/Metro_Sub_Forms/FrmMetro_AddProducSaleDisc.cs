using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddProducSaleDisc : MetroForm
    {
        #region Properties
        public long ProductId { get; set; }
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public long PrimaryId = 0;
        public string productName = "";
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
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
                    ProductSalesDiscountService _ProductSalesDiscountService = new ProductSalesDiscountService();

                    IsExists = _ProductSalesDiscountService.CheckUniqueDate(Convert.ToDateTime(datePickerStartDate.Value), Convert.ToDateTime(datePickerEndDate.Value), PrimaryId);
                    if (!IsExists)
                    {
                        ProductSalesDiscountMasterModel objProductSalesDiscountMasterModel = new ProductSalesDiscountMasterModel();

                        objProductSalesDiscountMasterModel.ProductID = ProductId;
                        objProductSalesDiscountMasterModel.Discount = Convert.ToDecimal(txtSalesDiscount.Text);
                        objProductSalesDiscountMasterModel.StartDate = Convert.ToDateTime(datePickerStartDate.Value);
                        objProductSalesDiscountMasterModel.EndDate = Convert.ToDateTime(datePickerEndDate.Value);
                        if (PrimaryId <= 0)
                        {
                            var add = _ProductSalesDiscountService.AddProductSaleDiscount(objProductSalesDiscountMasterModel, 1);
                            if (add != null)
                            {
                                DialogResult result = MessageBox.Show(AlertMessages.Add, AlertMessages.SuccessAlert, MessageBoxButtons.OK);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show(AlertMessages.Error, AlertMessages.InformationAlert, MessageBoxButtons.OK);
                            }
                        }
                        else if (PrimaryId > 0)
                        {
                            objProductSalesDiscountMasterModel.ProductSaleDiscountID = PrimaryId;
                            var add = _ProductSalesDiscountService.AddProductSaleDiscount(objProductSalesDiscountMasterModel, 2);
                            PrimaryId = 0;
                            if (add != null)
                            {
                                DialogResult result = MessageBox.Show(AlertMessages.Update, AlertMessages.SuccessAlert, MessageBoxButtons.OK);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show(AlertMessages.Error, AlertMessages.InformationAlert, MessageBoxButtons.OK);
                            }
                        }
                        UpdateLog();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.AlreadyExist, AlertMessages.InformationAlert, MessageBoxButtons.OK);
                    }
                    Clear();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSalesDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtSalesDiscount);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmMetro_AddProducSaleDisc_Load(object sender, EventArgs e)
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
        public FrmMetro_AddProducSaleDisc()
        {
            InitializeComponent();
        }

        public void LoadCmbProductName()
        {
            try
            {
                //List<ProductMasterModel> lstProductMasterModel = new List<ProductMasterModel>();
                //ProductService _ProductService = new ProductService();

                //lstProductMasterModel = _ProductService.GetAllProduct();
                //cmbProductName.DisplayMember = ProductSalesDiscountMasterModelCont.ProductName;
                //cmbProductName.ValueMember = ProductSalesDiscountMasterModelCont.ProductID;
                //cmbProductName.DataSource = lstProductMasterModel;
                //cmbProductName.SelectedValue = ProductId;
                //cmbProductName.Enabled = false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductSaleDiscount + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtSalesDiscount:
                    //txtSalesDiscount
                    if ((string.IsNullOrWhiteSpace(txtSalesDiscount.Text)))
                    {
                        txtSalesDiscount.Focus();
                        ep.SetError(txtSalesDiscount, AlertMessages.SalesDiscountValid1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtSalesDiscount.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtSalesDiscount.Focus();
                        ep.SetError(txtSalesDiscount, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtSalesDiscount.Text) > 100)
                    {
                        txtSalesDiscount.Focus();
                        ep.SetError(txtSalesDiscount, AlertMessages.SalesDiscountValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtSalesDiscount, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.datePickerStartDate:
                    //datePickerStartDate
                    if (datePickerStartDate.Value < DateTime.Now.Date)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.StartDateValid);
                        status = false;
                    }
                    else if (datePickerEndDate.Value < datePickerStartDate.Value.Date)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.StartDateValid2);
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
                    else if (datePickerEndDate.Value < datePickerStartDate.Value.Date)
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
                    //txtSalesDiscount
                    if ((string.IsNullOrWhiteSpace(txtSalesDiscount.Text)))
                    {
                        txtSalesDiscount.Focus();
                        ep.SetError(txtSalesDiscount, AlertMessages.SalesDiscountValid1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtSalesDiscount.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtSalesDiscount.Focus();
                        ep.SetError(txtSalesDiscount, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtSalesDiscount.Text) > 100)
                    {
                        txtSalesDiscount.Focus();
                        ep.SetError(txtSalesDiscount, AlertMessages.SalesDiscountValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtSalesDiscount, CommonModelCont.EmptyString);
                    }
                    //datePickerStartDate
                    if (datePickerStartDate.Value < DateTime.Now.Date)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.StartDateValid);
                        status = false;
                    }
                    else if (datePickerEndDate.Value.Date < datePickerStartDate.Value.Date)
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
                    else if (datePickerEndDate.Value < datePickerStartDate.Value.Date)
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
            txtSalesDiscount.Text = null;
            ep.SetError(txtSalesDiscount, CommonModelCont.EmptyString);
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
            objFrmMetroMaster.ChangeSyncStatus("tbl_ProductSaleDiscountMaster");
        }
        #endregion

       
    }
}
