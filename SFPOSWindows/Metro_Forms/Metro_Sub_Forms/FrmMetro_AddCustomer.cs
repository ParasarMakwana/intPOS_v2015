using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddCustomer : MetroForm
    {
        #region Properties
        public long CustomerID = 0;
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        #region Events
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    CustomerService _CustomerService = new CustomerService();

                    //bool IsEmployee = _CustomerService.CheckCustomerEmail(txtEmail.Text.Trim(), CustomerID);
                    //if (!IsEmployee)
                    //{
                    CustomerMasterModel objCustomerMasterModel = new CustomerMasterModel();
                    objCustomerMasterModel.FirstName = txtFirstName.Text.Trim();
                    objCustomerMasterModel.LastName = txtLastName.Text.Trim();
                    objCustomerMasterModel.StoreID = Convert.ToInt64(cmbStoreName.SelectedValue);
                    objCustomerMasterModel.Email = txtEmail.Text.Trim();
                    objCustomerMasterModel.MobileNo = txtMobile.Text.Trim();
                    objCustomerMasterModel.ZipCode = txtZipcode.Text.Trim();
                    objCustomerMasterModel.Message = txtMessage.Text.Trim();
                    objCustomerMasterModel.ResellerID = txtResllerID.Text.Trim();
                    objCustomerMasterModel.BusinessName = txtBusinessName.Text.Trim();
                    objCustomerMasterModel.City = txtCity.Text.Trim();
                    if (toggleActive.Checked)
                        objCustomerMasterModel.IsActive = true;
                    else
                        objCustomerMasterModel.IsActive = false;

                    if (toggleNewsLetter.Checked)
                        objCustomerMasterModel.NewsLetter = true;
                    else
                        objCustomerMasterModel.NewsLetter = false;

                    if (toggletaxExe.Checked)
                    {
                        objCustomerMasterModel.TaxExempted = true;
                        if(txtBusinessName.Text.ToString() == "")
                        {
                            ClsCommon.MsgBox(AlertMessages.Error, "Please add Business Name.", false);
                        }
                        if(txtResllerID.Text.ToString() == "")
                        {
                            ClsCommon.MsgBox(AlertMessages.Error, "Please add Reseller ID.", false);
                        }
                    }
                    else
                    {
                        objCustomerMasterModel.TaxExempted = false;
                    }

                    if (CustomerID <= 0)
                    {
                        var add = _CustomerService.AddCustomer(objCustomerMasterModel, 1);
                        if (add != null)
                        {
                            ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                        }
                    }
                    else if (CustomerID > 0)
                    {
                        objCustomerMasterModel.CustomerID = CustomerID;
                        var add = _CustomerService.AddCustomer(objCustomerMasterModel, 2);
                        if (add != null)
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Update, false);
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                        }
                        //}
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void metroBtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void cmbStoreName_Enter(object sender, EventArgs e)
        {
            if (cmbStoreName.SelectedIndex == 0)
            {
                // cmbStoreName.Text = CommonModelCont.EmptyString;
                cmbStoreName.ForeColor = Color.Black;
            }
        }

        private void cmbStoreName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStoreName.SelectedIndex == 0)
            {
                cmbStoreName.ForeColor = Color.Silver;
            }

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!toggletaxExe.Checked)
                {
                    CheckValidation(CommonTextBoxs.txtFirstName);
                }
                
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!toggletaxExe.Checked)
                {
                    CheckValidation(CommonTextBoxs.txtLastName);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtBusinessName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (toggletaxExe.Checked)
                {
                    CheckValidation("txtBusinessName");
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtResellerId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (toggletaxExe.Checked)
                {
                    CheckValidation("txtResellerId");
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                    CheckValidation("txtCity");
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtZipcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtZipcode);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtPhone);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtEmail);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
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
        public FrmMetro_AddCustomer()
        {
            InitializeComponent();
            CmbStore();
        }

        public void CmbStore()
        {
            try
            {
                List<StoreMasterModel> LstStoreMasterModel = new List<StoreMasterModel>();
                StoreService _StoreService = new StoreService();

                LstStoreMasterModel = _StoreService.GetAllStore();
                LstStoreMasterModel.Insert(0, new StoreMasterModel { StoreID = 0, StoreName = StoreMasterModelCont.cmbStoreFirst });
                cmbStoreName.DisplayMember = StoreMasterModelCont.StoreName;
                cmbStoreName.ValueMember = StoreMasterModelCont.StoreID;
                cmbStoreName.DataSource = LstStoreMasterModel;
                cmbStoreName.AutoCompleteSource = AutoCompleteSource.ListItems;

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case "txtFirstName":
                    //txtFirstName
                    if ((string.IsNullOrWhiteSpace(txtFirstName.Text)))
                    {
                        txtFirstName.Focus();
                        ep.SetError(txtFirstName, AlertMessages.EmpFirstName1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtFirstName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtFirstName.Focus();
                        ep.SetError(txtFirstName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtFirstName, CommonModelCont.EmptyString);
                    }
                    break;
                case "txtLastName":
                    //txtLastName
                    if ((string.IsNullOrWhiteSpace(txtLastName.Text)))
                    {
                        txtLastName.Focus();
                        ep.SetError(txtLastName, AlertMessages.EmpLastName2);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtLastName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtLastName.Focus();
                        ep.SetError(txtLastName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtLastName, CommonModelCont.EmptyString);
                    }
                    break;
                case "txtBusinessName":
                    //txtBusinessName
                    if ((string.IsNullOrWhiteSpace(txtBusinessName.Text)))
                    {
                        txtBusinessName.Focus();
                        ep.SetError(txtBusinessName, AlertMessages.BusinessName);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtBusinessName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtBusinessName.Focus();
                        ep.SetError(txtBusinessName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtBusinessName, CommonModelCont.EmptyString);
                    }
                    break;
                case "txtResellerId":
                    //txtBusinessName
                    if ((string.IsNullOrWhiteSpace(txtResllerID.Text)))
                    {
                        txtResllerID.Focus();
                        ep.SetError(txtResllerID, AlertMessages.ResellerID);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtResllerID, CommonModelCont.EmptyString);
                    }
                    break;
                //case "cmbStoreName":
                //    //cmbStoreName
                //    if ((string.IsNullOrWhiteSpace(cmbStoreName.Text)))
                //    {
                //        cmbStoreName.Focus();
                //        ep.SetError(cmbStoreName, AlertMessages.DropdownValidation);
                //        status = false;
                //    }
                //    else if (cmbStoreName.SelectedIndex < 0)
                //    {
                //        cmbStoreName.Focus();
                //        ep.SetError(cmbStoreName, AlertMessages.DropdownValidation);
                //        status = false;
                //    }
                //    else
                //    {
                //        ep.SetError(cmbStoreName, CommonModelCont.EmptyString);
                //    }
                //    break;
                case "txtEmail":
                    if ((string.IsNullOrWhiteSpace(txtEmail.Text)))
                    {
                        txtEmail.Focus();
                        ep.SetError(txtEmail, AlertMessages.EmailValid3);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtEmail.Text, CommonModelCont.Email_Validation)).Success))
                    {
                        txtEmail.Focus();
                        ep.SetError(txtEmail, AlertMessages.EmailValid1);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtEmail, CommonModelCont.EmptyString);
                    }
                    break;
                case "txtPhone":
                    //txtPhone
                    if (string.IsNullOrWhiteSpace(txtMobile.Text) || !(Regex.Match(txtMobile.Text, CommonModelCont.phone_Validation_Customer)).Success || txtMobile.Text.Length < 4)
                    {
                        txtMobile.Focus();
                        ep.SetError(txtMobile, AlertMessages.PhoneNoValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtMobile, CommonModelCont.EmptyString);
                    }
                    break;
                case "txtCity":
                    //txtBusinessName
                    if ((string.IsNullOrWhiteSpace(txtCity.Text)))
                    {
                        txtCity.Focus();
                        ep.SetError(txtCity, AlertMessages.City);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtCity.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtCity.Focus();
                        ep.SetError(txtCity, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtCity, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtZipcode:
                    //txtZipcode
                    if ((string.IsNullOrWhiteSpace(txtZipcode.Text)))
                    {
                        txtZipcode.Focus();
                        ep.SetError(txtZipcode, AlertMessages.ZipCodeValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtZipcode.Text, CommonModelCont.ZipCode_Validation)).Success))
                    {
                        txtZipcode.Focus();
                        ep.SetError(txtZipcode, AlertMessages.StoreZipCodeValid);
                        status = false;

                    }
                    else
                    {
                        ep.SetError(txtZipcode, CommonModelCont.EmptyString);
                    }
                    break;

                case CommonModelCont.EmptyString:
                    //default-ALL
                    
                    if (!toggletaxExe.Checked)
                    {
                        ep.Clear();
                        //txtFirstName
                        if ((string.IsNullOrWhiteSpace(txtFirstName.Text)))
                        {
                            txtFirstName.Focus();
                            ep.SetError(txtFirstName, AlertMessages.EmpFirstName1);
                            status = false;
                        }
                        else if ((!(Regex.Match(txtFirstName.Text, CommonModelCont.Name_Validation)).Success))
                        {
                            txtFirstName.Focus();
                            ep.SetError(txtFirstName, AlertMessages.NameValidation);
                            status = false;
                        }
                        else
                        {
                            ep.SetError(txtFirstName, CommonModelCont.EmptyString);
                        }
                        //txtLastName
                        if ((string.IsNullOrWhiteSpace(txtLastName.Text)))
                        {
                            txtLastName.Focus();
                            ep.SetError(txtLastName, AlertMessages.EmpLastName2);
                            status = false;
                        }
                        else if ((!(Regex.Match(txtLastName.Text, CommonModelCont.Name_Validation)).Success))
                        {
                            txtLastName.Focus();
                            ep.SetError(txtLastName, AlertMessages.NameValidation);
                            status = false;
                        }
                        else
                        {
                            ep.SetError(txtLastName, CommonModelCont.EmptyString);
                        }
                    }
                    else
                    {
                        ep.Clear();
                        //txtBusinessName
                        if ((string.IsNullOrWhiteSpace(txtBusinessName.Text)))
                        {
                            txtBusinessName.Focus();
                            ep.SetError(txtBusinessName, AlertMessages.BusinessName);
                            status = false;
                        }
                        else if ((!(Regex.Match(txtBusinessName.Text, CommonModelCont.Name_Validation)).Success))
                        {
                            txtBusinessName.Focus();
                            ep.SetError(txtBusinessName, AlertMessages.NameValidation);
                            status = false;
                        }
                        else
                        {
                            ep.SetError(txtBusinessName, CommonModelCont.EmptyString);
                        }
                        //txtResellerID
                        if ((string.IsNullOrWhiteSpace(txtResllerID.Text)))
                        {
                            txtResllerID.Focus();
                            ep.SetError(txtResllerID, AlertMessages.ResellerID);
                            status = false;
                        }
                        else
                        {
                            ep.SetError(txtResllerID, CommonModelCont.EmptyString);
                        }

                    }
                    //cmbStoreName
                    //if ((string.IsNullOrWhiteSpace(cmbStoreName.Text)))
                    //{
                    //    cmbStoreName.Focus();
                    //    ep.SetError(cmbStoreName, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbStoreName.SelectedIndex <= 0)
                    //{
                    //    cmbStoreName.Focus();
                    //    ep.SetError(cmbStoreName, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbStoreName, CommonModelCont.EmptyString);
                    //}

                    //txtEmail
                    if ((string.IsNullOrWhiteSpace(txtEmail.Text)))
                    {
                        txtEmail.Focus();
                        ep.SetError(txtEmail, AlertMessages.EmailValid3);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtEmail.Text, CommonModelCont.Email_Validation)).Success))
                    {
                        txtEmail.Focus();
                        ep.SetError(txtEmail, AlertMessages.EmailValid1);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtEmail, CommonModelCont.EmptyString);
                    }

                    //txtPhone
                    if (string.IsNullOrWhiteSpace(txtMobile.Text) || !(Regex.Match(txtMobile.Text, CommonModelCont.phone_Validation_Customer)).Success || txtMobile.Text.Length < 4)
                    {
                        txtMobile.Focus();
                        ep.SetError(txtMobile, AlertMessages.PhoneNoValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtMobile, CommonModelCont.EmptyString);
                    }

                    //txtCity
                    if ((string.IsNullOrWhiteSpace(txtCity.Text)))
                    {
                        txtCity.Focus();
                        ep.SetError(txtCity, AlertMessages.City);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtCity.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtCity.Focus();
                        ep.SetError(txtCity, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtCity, CommonModelCont.EmptyString);
                    }

                    //txtZipcode
                    if ((string.IsNullOrWhiteSpace(txtZipcode.Text)))
                    {
                        txtZipcode.Focus();
                        ep.SetError(txtZipcode, AlertMessages.ZipCodeValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtZipcode.Text, CommonModelCont.ZipCode_Validation)).Success))
                    {
                        txtZipcode.Focus();
                        ep.SetError(txtZipcode, AlertMessages.StoreZipCodeValid);
                        status = false;

                    }
                    else
                    {
                        ep.SetError(txtZipcode, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtFirstName.Text = null;
            ep.SetError(txtFirstName, CommonModelCont.EmptyString);
            txtLastName.Text = null;
            ep.SetError(txtLastName, CommonModelCont.EmptyString);
            txtCity.Text = null;
            ep.SetError(txtCity, CommonModelCont.EmptyString);
            cmbStoreName.SelectedIndex = 0;
            ep.SetError(cmbStoreName, CommonModelCont.EmptyString);
            txtEmail.Text = null;
            ep.SetError(txtEmail, CommonModelCont.EmptyString);
            txtMobile.Text = null;
            ep.SetError(txtMobile, CommonModelCont.EmptyString);
            txtZipcode.Text = null;
            ep.SetError(txtZipcode, CommonModelCont.EmptyString);
            txtMessage.Text = null;
            ep.SetError(txtMessage, CommonModelCont.EmptyString);
            txtResllerID.Text = null;
            ep.SetError(txtResllerID, CommonModelCont.EmptyString);
            txtBusinessName.Text = null;
            ep.SetError(txtBusinessName, CommonModelCont.EmptyString);
            toggleNewsLetter.Checked = false;
            toggletaxExe.Checked = false;
            CustomerID = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_CustomerMaster");
        }

        #endregion

    }
}
