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
    public partial class FrmMetro_AddVendor : MetroForm
    {
        #region Properties
        public long PrimaryId = 0;
        bool flagSave = false;
        VendorService _VendorService = new VendorService();
        ErrorProvider ep = new ErrorProvider();
        CountryService _CountryService = new CountryService();
        StateService _StateService = new StateService();
        CityService _CityService = new CityService();

        VendorMasterModel objVendorMasterModel = new VendorMasterModel();
        List<VendorMasterModel> lstVendorMasterModel = new List<VendorMasterModel>();
        List<StateMasterModel> lstStateMasterModel = new List<StateMasterModel>();
        List<CityMasterModel> lstCityMasterModel = new List<CityMasterModel>();
        List<CountryMasterModel> lstCountryMasterModel = new List<CountryMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        public FrmMetro_AddVendor()
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
                    bool IsVendor = _VendorService.CheckVendorName(txtVendorName.Text.Trim(), PrimaryId);
                    if (!IsVendor)
                    {
                        objVendorMasterModel.VendorName = txtVendorName.Text.Trim();
                        objVendorMasterModel.Address = txtAddress.Text.Trim();
                        objVendorMasterModel.Address2 = txtAddress2.Text.Trim();
                        //objVendorMasterModel.City = cmbCity.Text;
                        objVendorMasterModel.City = txtcity.Text.Trim();
                        objVendorMasterModel.State = Convert.ToInt64(cmbState.SelectedValue);
                        objVendorMasterModel.Country = Convert.ToInt64(cmbCountry.SelectedValue);
                        //objVendorMasterModel.State = cmbState.Text;
                        objVendorMasterModel.ZipCode = txtZipcode.Text.Trim();
                        //objVendorMasterModel.Country = cmbCountry.Text;
                        objVendorMasterModel.PhoneNo = txtPhone.Text.Trim();
                        objVendorMasterModel.Fax = txtFax.Text.Trim();
                        objVendorMasterModel.EmailID = txtEmail.Text.Trim();
                        objVendorMasterModel.ContactPerson = txtContactPerson.Text.Trim();
                        if (PrimaryId <= 0)
                        {
                            var add = _VendorService.AddVendor(objVendorMasterModel, 1);
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
                            objVendorMasterModel.VendorID = PrimaryId;
                            var Update = _VendorService.AddVendor(objVendorMasterModel, 2);
                            if (Update != null)
                            {
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
                        ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.AlreadyExist, false);
                    }
                    Clear();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }
        
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation("txtPhone");
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtFax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation("txtFax");
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }

        }

        private void txtZipcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation("txtZipcode");
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
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

        public void RefreshCity(int stateid)
        {
            try
            {
                //List<CityMasterModel> lstCityMasterModel = new List<CityMasterModel>();
                //lstCityMasterModel = _CityService.GetAllCity(stateid);
                ////lstCityMasterModel.Insert(0, new CityMasterModel { CityID = 0, CityName = "--- Select City ---" });
                //cmbCity.ValueMember = CityMasterModelCont.CityID;
                //cmbCity.DisplayMember = CityMasterModelCont.CityName;
                //cmbCity.DataSource = lstCityMasterModel;
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
                List<StateMasterModel> lstStateMasterModel = new List<StateMasterModel>();
                lstStateMasterModel = _StateService.GetAllState(countryid);
                //lstStateMasterModel.Insert(0, new StateMasterModel { StateID = 0, StateName = "--- Select State ---" });
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
                List<CountryMasterModel> lstCountryMasterModel = new List<CountryMasterModel>();
                lstCountryMasterModel = _CountryService.GetAllCountry();
                //lstCountryMasterModel.Insert(0, new CountryMasterModel { CountryID = 0, CountryName = "--- Select Country ---" });
                cmbCountry.DisplayMember = CountryMasterModelCont.CountryName;
                cmbCountry.ValueMember = CountryMasterModelCont.CountryID;
                cmbCountry.DataSource = lstCountryMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmStore + ex.StackTrace, ex.LineNumber());
            }
        }

        public void Clear()
        {
            txtVendorName.Text = null;
            ep.SetError(txtVendorName, CommonModelCont.EmptyString);
            txtAddress.Text = null;
            ep.SetError(txtAddress, CommonModelCont.EmptyString);
            txtAddress2.Text = null;
            ep.SetError(txtAddress2, CommonModelCont.EmptyString);
            txtZipcode.Text = null;
            ep.SetError(txtZipcode, CommonModelCont.EmptyString);
            txtPhone.Text = null;
            ep.SetError(txtPhone, CommonModelCont.EmptyString);
            txtFax.Text = null;
            ep.SetError(txtFax, CommonModelCont.EmptyString);
            txtEmail.Text = null;
            ep.SetError(txtEmail, CommonModelCont.EmptyString);
            txtContactPerson.Text = null;
            ep.SetError(txtContactPerson, CommonModelCont.EmptyString);
            cmbCountry.SelectedIndex = 0;
            ep.SetError(cmbCountry, CommonModelCont.EmptyString);
            cmbState.SelectedIndex = 0;
            ep.SetError(cmbState, CommonModelCont.EmptyString);
            //cmbCity.SelectedIndex = 0;
            //ep.SetError(cmbCity, CommonModelCont.EmptyString);
            txtcity.Text = null;
            ep.SetError(txtcity, CommonModelCont.EmptyString);
            PrimaryId = 0;
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtVendorName:
                    //txtVendorName
                    if ((string.IsNullOrWhiteSpace(txtVendorName.Text)))
                    {
                        txtVendorName.Focus();
                        ep.SetError(txtVendorName, AlertMessages.VendorNameValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtVendorName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtVendorName.Focus();
                        ep.SetError(txtVendorName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtVendorName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtContactPerson:
                    ////txtContactPerson
                    //if ((string.IsNullOrWhiteSpace(txtContactPerson.Text)))
                    //{
                    //    txtContactPerson.Focus();
                    //    ep.SetError(txtContactPerson, AlertMessages.ContactPersonValid);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtContactPerson.Text, CommonModelCont.Name_Validation)).Success))
                    //{
                    //    txtContactPerson.Focus();
                    //    ep.SetError(txtContactPerson, AlertMessages.NameValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtContactPerson, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonTextBoxs.txtEmail:
                    //txtEmail
                    //if ((string.IsNullOrWhiteSpace(txtEmail.Text)))
                    //{
                    //    txtEmail.Focus();
                    //    ep.SetError(txtEmail, AlertMessages.EmailValid3);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtEmail.Text, CommonModelCont.Email_Validation)).Success))
                    //{
                    //    txtEmail.Focus();
                    //    ep.SetError(txtEmail, AlertMessages.EmailValid1);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtEmail, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonTextBoxs.txtPhone:
                    //txtPhone
                    //if ((string.IsNullOrWhiteSpace(txtPhone.Text)))
                    //{
                    //    txtPhone.Focus();
                    //    ep.SetError(txtPhone, AlertMessages.PhoneNoValid1);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtPhone.Text, CommonModelCont.phone_Validation)).Success))
                    //{
                    //    txtPhone.Focus();
                    //    ep.SetError(txtPhone, AlertMessages.PhoneNoValid2);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtPhone, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonTextBoxs.txtFax:
                    //txtFax
                    //if (((string.IsNullOrWhiteSpace(txtFax.Text))))
                    //{
                    //    txtFax.Focus();
                    //    ep.SetError(txtFax, AlertMessages.FaxValid);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtFax.Text, CommonModelCont.phone_Validation)).Success))
                    //{
                    //    txtFax.Focus();
                    //    ep.SetError(txtFax, AlertMessages.FaxNoValid);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtFax, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonTextBoxs.cmbCountry:
                    //cmbCountry
                    //if ((string.IsNullOrWhiteSpace(cmbCountry.Text)))
                    //{
                    //    cmbCountry.Focus();
                    //    ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbCountry.SelectedIndex < 0)
                    //{
                    //    cmbCountry.Focus();
                    //    ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbCountry, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonTextBoxs.cmbState:
                    //cmbState
                    //if ((string.IsNullOrWhiteSpace(cmbState.Text)))
                    //{
                    //    cmbState.Focus();
                    //    ep.SetError(cmbState, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbState.SelectedIndex < 0)
                    //{
                    //    cmbState.Focus();
                    //    ep.SetError(cmbState, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbState, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonTextBoxs.cmbCity:
                    //cmbCity
                    //if ((string.IsNullOrWhiteSpace(cmbCity.Text)))
                    //{
                    //    cmbCity.Focus();
                    //    ep.SetError(cmbCity, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbCity.SelectedIndex < 0)
                    //{
                    //    cmbCity.Focus();
                    //    ep.SetError(cmbCity, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbCity, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonTextBoxs.txtZipcode:
                    //txtZipcode
                    //if ((string.IsNullOrWhiteSpace(txtZipcode.Text)))
                    //{
                    //    txtZipcode.Focus();
                    //    ep.SetError(txtZipcode, AlertMessages.ZipCodeValid);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtZipcode.Text, CommonModelCont.ZipCode_Validation)).Success))
                    //{
                    //    txtZipcode.Focus();
                    //    ep.SetError(txtZipcode, AlertMessages.StoreZipCodeValid);
                    //    status = false;

                    //}
                    //else
                    //{
                    //    ep.SetError(txtZipcode, CommonModelCont.EmptyString);
                    //}
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtVendorName
                    if ((string.IsNullOrWhiteSpace(txtVendorName.Text)))
                    {
                        txtVendorName.Focus();
                        ep.SetError(txtVendorName, AlertMessages.VendorNameValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtVendorName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtVendorName.Focus();
                        ep.SetError(txtVendorName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtVendorName, CommonModelCont.EmptyString);
                    }
                    //txtContactPerson
                    //if ((string.IsNullOrWhiteSpace(txtContactPerson.Text)))
                    //{
                    //    txtContactPerson.Focus();
                    //    ep.SetError(txtContactPerson, AlertMessages.ContactPersonValid);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtContactPerson.Text, CommonModelCont.Name_Validation)).Success))
                    //{
                    //    txtContactPerson.Focus();
                    //    ep.SetError(txtContactPerson, AlertMessages.NameValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtContactPerson, CommonModelCont.EmptyString);
                    //}
                    ////txtEmail
                    //if ((string.IsNullOrWhiteSpace(txtEmail.Text)))
                    //{
                    //    txtEmail.Focus();
                    //    ep.SetError(txtEmail, AlertMessages.EmailValid3);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtEmail.Text, CommonModelCont.Email_Validation)).Success))
                    //{
                    //    txtEmail.Focus();
                    //    ep.SetError(txtEmail, AlertMessages.EmailValid1);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtEmail, CommonModelCont.EmptyString);
                    //}
                    ////txtPhone
                    //if ((string.IsNullOrWhiteSpace(txtPhone.Text)))
                    //{
                    //    txtPhone.Focus();
                    //    ep.SetError(txtPhone, AlertMessages.PhoneNoValid1);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtPhone.Text, CommonModelCont.phone_Validation)).Success))
                    //{
                    //    txtPhone.Focus();
                    //    ep.SetError(txtPhone, AlertMessages.PhoneNoValid2);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtPhone, CommonModelCont.EmptyString);
                    //}
                    ////txtFax
                    //if (((string.IsNullOrWhiteSpace(txtFax.Text))))
                    //{
                    //    txtFax.Focus();
                    //    ep.SetError(txtFax, AlertMessages.FaxValid);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtFax.Text, CommonModelCont.phone_Validation)).Success))
                    //{
                    //    txtFax.Focus();
                    //    ep.SetError(txtFax, AlertMessages.FaxNoValid);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(txtFax, CommonModelCont.EmptyString);
                    //}
                    ////cmbCountry
                    //if ((string.IsNullOrWhiteSpace(cmbCountry.Text)))
                    //{
                    //    cmbCountry.Focus();
                    //    ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbCountry.SelectedIndex < 0)
                    //{
                    //    cmbCountry.Focus();
                    //    ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbCountry, CommonModelCont.EmptyString);
                    //}
                    ////cmbState
                    //if ((string.IsNullOrWhiteSpace(cmbState.Text)))
                    //{
                    //    cmbState.Focus();
                    //    ep.SetError(cmbState, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbState.SelectedIndex < 0)
                    //{
                    //    cmbState.Focus();
                    //    ep.SetError(cmbState, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbState, CommonModelCont.EmptyString);
                    //}
                    ////cmbCity
                    //if ((string.IsNullOrWhiteSpace(cmbCity.Text)))
                    //{
                    //    cmbCity.Focus();
                    //    ep.SetError(cmbCity, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else if (cmbCity.SelectedIndex < 0)
                    //{
                    //    cmbCity.Focus();
                    //    ep.SetError(cmbCity, AlertMessages.DropdownValidation);
                    //    status = false;
                    //}
                    //else
                    //{
                    //    ep.SetError(cmbCity, CommonModelCont.EmptyString);
                    //}
                    ////txtZipcode
                    //if ((string.IsNullOrWhiteSpace(txtZipcode.Text)))
                    //{
                    //    txtZipcode.Focus();
                    //    ep.SetError(txtZipcode, AlertMessages.ZipCodeValid);
                    //    status = false;
                    //}
                    //else if ((!(Regex.Match(txtZipcode.Text, CommonModelCont.ZipCode_Validation)).Success))
                    //{
                    //    txtZipcode.Focus();
                    //    ep.SetError(txtZipcode, AlertMessages.StoreZipCodeValid);
                    //    status = false;

                    //}
                    //else
                    //{
                    //    ep.SetError(txtZipcode, CommonModelCont.EmptyString);
                    //}
                    break;
            }
            return status;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation("txtEmail");
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation("txtVendorName");
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtContactPerson_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation("txtContactPerson");
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmVendor + ex.StackTrace, ex.LineNumber());
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
