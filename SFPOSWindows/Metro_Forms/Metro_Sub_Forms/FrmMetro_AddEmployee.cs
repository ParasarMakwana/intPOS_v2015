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
    public partial class FrmMetro_AddEmployee : MetroForm
    {
        #region Properties
        public long EmployeeID = 0;
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
                    EmployeeService _EmployeeService = new EmployeeService();

                    bool isPasswordDuplicate = _EmployeeService.CheckEmployeePasswordDuplicate(txtPwd.Text.Trim(), EmployeeID);
                    if (!isPasswordDuplicate)
                    {
                        bool IsEmployee = _EmployeeService.CheckEmployeeName(txtUserID.Text.Trim(), EmployeeID);
                        if (!IsEmployee)
                        {
                            EmployeeMasterModel objEmployeeMasterModel = new EmployeeMasterModel();
                            objEmployeeMasterModel.FirstName = txtFirstName.Text.Trim();
                            objEmployeeMasterModel.LastName = txtLastName.Text.Trim();
                            objEmployeeMasterModel.StoreID = Convert.ToInt64(cmbStoreName.SelectedValue);
                            objEmployeeMasterModel.RoleID = Convert.ToInt64(cmbRoleName.SelectedValue);
                            objEmployeeMasterModel.EmailID = txtUserID.Text.Trim();
                            objEmployeeMasterModel.Password = txtPwd.Text.Trim();
                            objEmployeeMasterModel.PhoneNo = txtPhone.Text.Trim();
                            objEmployeeMasterModel.MaxVoidAmount = (String.IsNullOrEmpty(txtMaxVoidAmount.Text) ? 0 : Convert.ToDecimal(txtMaxVoidAmount.Text));
                            objEmployeeMasterModel.BirthDate = Convert.ToDateTime(datePickerBirthDate.Value);
                            if (toggleActive.Checked)
                                objEmployeeMasterModel.IsActive = true;
                            else
                                objEmployeeMasterModel.IsActive = false;
                            if (EmployeeID <= 0)
                            {
                                var add = _EmployeeService.AddEmployee(objEmployeeMasterModel, 1);
                                if (add != null)
                                {
                                    ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                                }
                                else
                                {
                                    ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                                }
                            }
                            else if (EmployeeID > 0)
                            {
                                objEmployeeMasterModel.EmployeeID = EmployeeID;
                                var add = _EmployeeService.AddEmployee(objEmployeeMasterModel, 2);
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
                    else
                    {
                        ClsCommon.MsgBox(AlertMessages.InformationAlert, CommonMessage.passwordDuplicate, false);
                    }

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

        private void cmbRoleName_Enter(object sender, EventArgs e)
        {
            if (cmbRoleName.SelectedIndex == 0)
            {
                //cmbRoleName.Text = CommonModelCont.EmptyString;
                cmbRoleName.ForeColor = Color.Black;
            }
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
                CheckValidation(CommonTextBoxs.txtFirstName);
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
                CheckValidation(CommonTextBoxs.txtLastName);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
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

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtPwd);
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
        public FrmMetro_AddEmployee()
        {
            InitializeComponent();
            CmbRole();
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

        public void CmbRole()
        {
            try
            {
                List<RoleMasterModel> LstRoleMasterModel = new List<RoleMasterModel>();
                RoleService _RoleService = new RoleService();

                LstRoleMasterModel = _RoleService.GetAllRole();
                LstRoleMasterModel.Insert(0, new RoleMasterModel { RoleID = 0, RoleType = RoleMasterModelCont.cmbRoleFirst });
                cmbRoleName.DisplayMember = RoleMasterModelCont.RoleType;
                cmbRoleName.ValueMember = RoleMasterModelCont.RoleID;
                cmbRoleName.DataSource = LstRoleMasterModel;
                cmbRoleName.AutoCompleteSource = AutoCompleteSource.ListItems;
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
                case "cmbStoreName":
                    //cmbStoreName
                    if ((string.IsNullOrWhiteSpace(cmbStoreName.Text)))
                    {
                        cmbStoreName.Focus();
                        ep.SetError(cmbStoreName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbStoreName.SelectedIndex < 0)
                    {
                        cmbStoreName.Focus();
                        ep.SetError(cmbStoreName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbStoreName, CommonModelCont.EmptyString);
                    }
                    break;
                case "cmbRoleName":
                    //cmbRoleName
                    if ((string.IsNullOrWhiteSpace(cmbRoleName.Text)))
                    {
                        cmbRoleName.Focus();
                        ep.SetError(cmbRoleName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbRoleName.SelectedIndex < 0)
                    {
                        cmbRoleName.Focus();
                        ep.SetError(cmbRoleName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbRoleName, CommonModelCont.EmptyString);
                    }
                    break;
                case "txtUserID":
                    //txtUserID
                    if ((string.IsNullOrWhiteSpace(txtUserID.Text)))
                    {
                        txtUserID.Focus();
                        ep.SetError(txtUserID, "UserId can't be null!");
                        status = false;
                    }
                    else if ((!(Regex.Match(txtUserID.Text, CommonModelCont.UserID_Validation)).Success))
                    {
                        txtUserID.Focus();
                        ep.SetError(txtUserID, "Please enter correct format of user Id. 'Format : ****' ");
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtUserID, CommonModelCont.EmptyString);
                    }
                    break;
                case "txtPhone":
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
                case "txtPassword":
                    //txtPassword
                    if (txtPwd.Text.Length < 3)
                    {
                        txtPwd.Focus();
                        ep.SetError(txtPwd, AlertMessages.PasswordValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtPhone.Text, CommonModelCont.phone_Validation)).Success))
                    {
                        txtPwd.Focus();
                        ep.SetError(txtPwd, AlertMessages.PasswordValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPwd, CommonModelCont.EmptyString);
                    }
                    break;
                case "datePickerBirthDate":
                    //datePickerBirthDate
                    if (datePickerBirthDate.Value == DateTime.Now.Date)
                    {
                        datePickerBirthDate.Focus();
                        ep.SetError(datePickerBirthDate, "Please enter valid birthdate!");
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerBirthDate, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
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
                    //cmbStoreName
                    if ((string.IsNullOrWhiteSpace(cmbStoreName.Text)))
                    {
                        cmbStoreName.Focus();
                        ep.SetError(cmbStoreName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbStoreName.SelectedIndex <= 0)
                    {
                        cmbStoreName.Focus();
                        ep.SetError(cmbStoreName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbStoreName, CommonModelCont.EmptyString);
                    }
                    //cmbRoleName
                    if ((string.IsNullOrWhiteSpace(cmbRoleName.Text)))
                    {
                        cmbRoleName.Focus();
                        ep.SetError(cmbRoleName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbRoleName.SelectedIndex <= 0)
                    {
                        cmbRoleName.Focus();
                        ep.SetError(cmbRoleName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbRoleName, CommonModelCont.EmptyString);
                    }
                    //txtUserID
                    if ((string.IsNullOrWhiteSpace(txtUserID.Text)))
                    {
                        txtUserID.Focus();
                        ep.SetError(txtUserID, "UserId can't be null!");
                        status = false;
                    }
                    else if ((!(Regex.Match(txtUserID.Text, CommonModelCont.UserID_Validation)).Success))
                    {
                        txtUserID.Focus();
                        ep.SetError(txtUserID, "Please enter correct format of user Id. 'Format : ****' ");
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtUserID, CommonModelCont.EmptyString);
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
                    //txtPassword
                    if (txtPwd.Text.Length < 3)
                    {
                        txtPwd.Focus();
                        ep.SetError(txtPwd, AlertMessages.PasswordValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtPhone.Text, CommonModelCont.phone_Validation)).Success))
                    {
                        txtPwd.Focus();
                        ep.SetError(txtPwd, AlertMessages.PasswordValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPwd, CommonModelCont.EmptyString);
                    }
                    //datePickerBirthDate
                    if (datePickerBirthDate.Value >= DateTime.Now.Date)
                    {
                        datePickerBirthDate.Focus();
                        ep.SetError(datePickerBirthDate, "Please enter valid birthdate!");
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerBirthDate, CommonModelCont.EmptyString);
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
            txtPhone.Text = null;
            ep.SetError(txtPhone, CommonModelCont.EmptyString);
            txtUserID.Text = null;
            ep.SetError(txtUserID, CommonModelCont.EmptyString);
            txtPwd.Text = null;
            ep.SetError(txtPwd, CommonModelCont.EmptyString);
            cmbStoreName.SelectedIndex = 0;
            ep.SetError(cmbStoreName, CommonModelCont.EmptyString);
            cmbRoleName.SelectedIndex = 0;
            ep.SetError(cmbRoleName, CommonModelCont.EmptyString);

            txtMaxVoidAmount.Text = null;
           

            EmployeeID = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_EmployeeMaster");
        }

        #endregion
    }
}
