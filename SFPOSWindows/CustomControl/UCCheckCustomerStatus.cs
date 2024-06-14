using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Common;
using SFPOS.BAL.Frontend;
using SFPOS.Entities.FrontEnd;
using System.Text.RegularExpressions;

namespace SFPOSWindows.CustomControl
{
    public partial class UCCheckCustomerStatus : UserControl
    {
        #region Properties
        CouponService _CouponService = new CouponService();
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        public bool IsCheckforTC = false;
        CustomerMasterService _custmerMasterService = new CustomerMasterService();
        public decimal MinPurchaseAmount = 0;
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public bool IsCancel = false;
        CustomerModel LstCustomerMasterModel = new CustomerModel();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        #endregion

        #region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //flagSave = CheckValidation(CommonModelCont.EmptyString);
                flagSave = CheckValidation_Couponcode(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    GetCustomerValidate();
                }
            }
            catch (Exception ex)
            {
                txtCustomerPhone.Text = "";
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                IsCancel = true;
                this.Hide();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                txtCustomerPhone.Text = "";
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtCustomerPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //flagSave = CheckValidation(CommonModelCont.EmptyString);
                flagSave = CheckValidation_Couponcode(CommonModelCont.EmptyString);
                //if (flagSave)
                //{
                //    GetCustomerValidate();
                //}
            }
            catch (Exception ex)
            {
                txtCustomerPhone.Text = "";
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public UCCheckCustomerStatus()
        {
            InitializeComponent();
        }

        public void GetCustomerValidate()
        {
            try
            {
                if (txtCustomerPhone.Text != "")
                {
                    if (CustomerInfo.IsCustomerVerfied != true && IsCheckforTC == false)
                    {
                        LstCustomerMasterModel = _custmerMasterService.GetVerifyCustomer(txtCustomerPhone.Text);
                        if (LstCustomerMasterModel != null)
                        {
                            CustomerInfo.IsCustomerVerfied = true;
                            CustomerInfo.CustomerId = LstCustomerMasterModel.CustomerID;
                            OnMyEvent(this, new EventArgs());
                        }
                        else
                        {
                            txtCustomerPhone.Text = "";
                            ClsCommon.MsgBox("Information", "Customer not found!", false);
                        }
                    }
                    else if (CustomerInfo.IsCustomerVerfiedForTC != true && IsCheckforTC == true)
                    {
                        LstCustomerMasterModel = _custmerMasterService.GetCustomerMasterWithTaxExemption(txtCustomerPhone.Text);
                        if (LstCustomerMasterModel != null)
                        {
                            CustomerInfo.IsCustomerVerfiedForTC = true;
                            CustomerInfo.CustomerId = LstCustomerMasterModel.CustomerID;
                            OnMyEvent(this, new EventArgs());
                        }
                        else
                        {
                            txtCustomerPhone.Text = "";
                            ClsCommon.MsgBox("Information", "Customer does not exist or is not Tax Exempted", false);
                        }
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please enter customer phone number!", false);
                    txtCustomerPhone.Focus();
                }
            }
            catch (Exception ex)
            {
                txtCustomerPhone.Text = "";
                txtCustomerPhone.Focus();
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCheckCoupon + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;

            if (string.IsNullOrWhiteSpace(txtCustomerPhone.Text) || !(Regex.Match(txtCustomerPhone.Text, CommonModelCont.phone_Validation)).Success || txtCustomerPhone.Text.Length != 10)
            {
                txtCustomerPhone.Focus();
                ep.SetError(txtCustomerPhone, AlertMessages.PhoneNoValid2);
                status = false;
            }

            return status;
        }
        public bool CheckValidation_Couponcode(string ControlName)
        {
            bool status = true;

            if (string.IsNullOrWhiteSpace(txtCustomerPhone.Text) || txtCustomerPhone.Text.Length < 3) ///// || !(Regex.Match(txtCustomerPhone.Text, CommonModelCont.phone_Validation)).Success || txtCustomerPhone.Text.Length != 10
            {
                txtCustomerPhone.Focus();
                ep.SetError(txtCustomerPhone, AlertMessages.PhoneNoValid2);
                status = false;
            }
            else
            {
                txtCustomerPhone.Focus();
                ep.SetError(txtCustomerPhone, "");
                status = true;
            }
            return status;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {
                    this.Hide();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        private void UCCheckCustomerStatus_Load(object sender, EventArgs e)
        {
            txtCustomerPhone.Focus();
        }
    }
}
