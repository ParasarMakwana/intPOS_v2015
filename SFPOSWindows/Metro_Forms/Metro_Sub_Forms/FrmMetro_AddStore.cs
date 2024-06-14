using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddStore : MetroForm
    {
        #region Properties
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public long PrimaryId = 0;
        #endregion
        
        #region Events

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    StoreService _StoreService = new StoreService();
                    bool IsStore = _StoreService.CheckStoreName(txtStoreName.Text.Trim(), PrimaryId);
                    if (!IsStore)
                    {
                        StoreMasterModel objStoreMasterModel = new StoreMasterModel();

                        objStoreMasterModel.StoreName = txtStoreName.Text;
                        objStoreMasterModel.Address = txtAddress.Text;
                        objStoreMasterModel.Address2 = txtAddress2.Text;
                        objStoreMasterModel.City = Convert.ToInt64(cmbCity.SelectedValue);
                        objStoreMasterModel.State = Convert.ToInt64(cmbState.SelectedValue);
                        objStoreMasterModel.ZipCode = txtZipcode.Text.Trim();
                        objStoreMasterModel.Country = Convert.ToInt64(cmbCountry.SelectedValue);
                        objStoreMasterModel.Phone = txtPhone.Text.Trim();
                        objStoreMasterModel.Fax = txtFax.Text.Trim();
                        objStoreMasterModel.DefaultTax = Convert.ToDecimal(txtDefaultTax.Text.Trim());
                        objStoreMasterModel.Disclaimer = txtDisclaimer.Text.Trim();

                        if (ToggleStoreTax.Checked)
                            objStoreMasterModel.IsStoreTax = true;
                        else
                            objStoreMasterModel.IsStoreTax = false;
                        
                        if (txtAgeVerificationAge.Text.Trim() == "")
                        {
                            objStoreMasterModel.AgeVarificationAge = null;
                        }
                        else
                        {
                            objStoreMasterModel.AgeVarificationAge = Convert.ToInt32(txtAgeVerificationAge.Text.Trim());
                        }
                        if (IsStore == false && PrimaryId <= 0)
                        {
                            var add = _StoreService.AddStore(objStoreMasterModel, 1);
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
                            objStoreMasterModel.StoreID = PrimaryId;
                            var add = _StoreService.AddStore(objStoreMasterModel, 2);
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }
        
        private void txtStoreName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtStoreName);
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtFax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtFax);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
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

        private void cmbCountry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.cmbCountry);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbState_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.cmbState);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbCity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.cmbCity);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCountry.SelectedValue != null)
                {
                    int countryid = Convert.ToInt32(cmbCountry.SelectedValue.ToString());
                    RefreshState(countryid);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbState.SelectedValue != null)
                {
                    int stateid = Convert.ToInt32(cmbState.SelectedValue.ToString());
                    RefreshCity(stateid);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
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

        public FrmMetro_AddStore()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            txtStoreName.Text = null;
            txtAddress.Text = null;
            txtAddress2.Text = null;
            txtZipcode.Text = null;
            txtPhone.Text = null;
            txtFax.Text = null;
            ep.SetError(txtStoreName, CommonModelCont.EmptyString);
            ep.SetError(txtAddress, CommonModelCont.EmptyString);
            ep.SetError(txtAddress2, CommonModelCont.EmptyString);
            ep.SetError(txtZipcode, CommonModelCont.EmptyString);
            ep.SetError(txtPhone, CommonModelCont.EmptyString);
            ep.SetError(txtFax, CommonModelCont.EmptyString);
            cmbCountry.SelectedIndex = 0;
            ep.SetError(cmbCountry, CommonModelCont.EmptyString);
            cmbState.SelectedIndex = 0;
            ep.SetError(cmbState, CommonModelCont.EmptyString);
            cmbCity.SelectedIndex = 0;
            ep.SetError(cmbCity, CommonModelCont.EmptyString);

            PrimaryId = 0;
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtStoreName:
                    //txtStoreName
                    if ((string.IsNullOrWhiteSpace(txtStoreName.Text)))
                    {
                        txtStoreName.Focus();
                        ep.SetError(txtStoreName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtStoreName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtStoreName.Focus();
                        ep.SetError(txtStoreName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtStoreName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtPhone:
                    //txtPhone
                    if ((string.IsNullOrWhiteSpace(txtPhone.Text)))
                    {
                        txtPhone.Focus();
                        ep.SetError(txtPhone, AlertMessages.PhoneNoValid1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtPhone.Text, CommonModelCont.phone_Validation)).Success))
                    {
                        txtPhone.Focus();
                        ep.SetError(txtPhone, AlertMessages.PhoneNoValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPhone, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtFax:
                    //txtFax
                    if (((string.IsNullOrWhiteSpace(txtFax.Text))))
                    {
                        txtFax.Focus();
                        ep.SetError(txtFax, AlertMessages.FaxValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtFax.Text, CommonModelCont.phone_Validation)).Success))
                    {
                        txtFax.Focus();
                        ep.SetError(txtFax, AlertMessages.FaxNoValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtFax, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbCountry:
                    //cmbCountry
                    if ((string.IsNullOrWhiteSpace(cmbCountry.Text)))
                    {
                        cmbCountry.Focus();
                        ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbCountry.SelectedIndex < 0)
                    {
                        cmbCountry.Focus();
                        ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbCountry, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbState:
                    //cmbState
                    if ((string.IsNullOrWhiteSpace(cmbState.Text)))
                    {
                        cmbState.Focus();
                        ep.SetError(cmbState, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbState.SelectedIndex < 0)
                    {
                        cmbState.Focus();
                        ep.SetError(cmbState, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbState, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbCity:
                    //cmbCity
                    if ((string.IsNullOrWhiteSpace(cmbCity.Text)))
                    {
                        cmbCity.Focus();
                        ep.SetError(cmbCity, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbCity.SelectedIndex < 0)
                    {
                        cmbCity.Focus();
                        ep.SetError(cmbCity, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbCity, CommonModelCont.EmptyString);
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
                    //txtStoreName
                    if ((string.IsNullOrWhiteSpace(txtStoreName.Text)))
                    {
                        txtStoreName.Focus();
                        ep.SetError(txtStoreName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtStoreName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtStoreName.Focus();
                        ep.SetError(txtStoreName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtStoreName, CommonModelCont.EmptyString);
                    }
                    //txtPhone
                    if ((string.IsNullOrWhiteSpace(txtPhone.Text)))
                    {
                        txtPhone.Focus();
                        ep.SetError(txtPhone, AlertMessages.PhoneNoValid1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtPhone.Text, CommonModelCont.phone_Validation)).Success))
                    {
                        txtPhone.Focus();
                        ep.SetError(txtPhone, AlertMessages.PhoneNoValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPhone, CommonModelCont.EmptyString);
                    }
                    //txtFax
                    if (((string.IsNullOrWhiteSpace(txtFax.Text))))
                    {
                        txtFax.Focus();
                        ep.SetError(txtFax, AlertMessages.FaxValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtFax.Text, CommonModelCont.phone_Validation)).Success))
                    {
                        txtFax.Focus();
                        ep.SetError(txtFax, AlertMessages.FaxNoValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtFax, CommonModelCont.EmptyString);
                    }
                    //cmbCountry
                    if ((string.IsNullOrWhiteSpace(cmbCountry.Text)))
                    {
                        cmbCountry.Focus();
                        ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbCountry.SelectedIndex < 0)
                    {
                        cmbCountry.Focus();
                        ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbCountry, CommonModelCont.EmptyString);
                    }
                    //cmbState
                    if ((string.IsNullOrWhiteSpace(cmbState.Text)))
                    {
                        cmbState.Focus();
                        ep.SetError(cmbState, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbState.SelectedIndex < 0)
                    {
                        cmbState.Focus();
                        ep.SetError(cmbState, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbState, CommonModelCont.EmptyString);
                    }
                    //cmbCity
                    if ((string.IsNullOrWhiteSpace(cmbCity.Text)))
                    {
                        cmbCity.Focus();
                        ep.SetError(cmbCity, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbCity.SelectedIndex < 0)
                    {
                        cmbCity.Focus();
                        ep.SetError(cmbCity, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbCity, CommonModelCont.EmptyString);
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

        public void RefreshCity(int stateid)
        {
            try
            {
                CityService _CityService = new CityService();

                List<CityMasterModel> lstCityMasterModel = new List<CityMasterModel>();
                lstCityMasterModel = _CityService.GetAllCity(stateid);
                cmbCity.ValueMember = CityMasterModelCont.CityID;
                cmbCity.DisplayMember = CityMasterModelCont.CityName;
                cmbCity.DataSource = lstCityMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        public void RefreshState(int countryid)
        {
            try
            {
                StateService _StateService = new StateService();
                List<StateMasterModel> lstStateMasterModel = new List<StateMasterModel>();
                lstStateMasterModel = _StateService.GetAllState(countryid);
                cmbState.ValueMember = StateMasterModelCont.StateID;
                cmbState.DisplayMember = StateMasterModelCont.StateName;
                cmbState.DataSource = lstStateMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        public void RefreshCountry()
        {
            try
            {
                CountryService _CountryService = new CountryService();
                List<CountryMasterModel> lstCountryMasterModel = new List<CountryMasterModel>();
                lstCountryMasterModel = _CountryService.GetAllCountry();
                cmbCountry.DisplayMember = CountryMasterModelCont.CountryName;
                cmbCountry.ValueMember = CountryMasterModelCont.CountryID;
                cmbCountry.DataSource = lstCountryMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_StoreMaster");
        }
        #endregion

    }
}
