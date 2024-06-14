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
    public partial class FrmMetro_AddRole : MetroForm
    {
        #region Properties
        ErrorProvider ep = new ErrorProvider();
        public long PrimaryId = 0;
        RoleService _RoleService = new RoleService();
        List<RoleMasterModel> lstRoleMasterModel = new List<RoleMasterModel>();
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
                if (flagSave)
                {
                    RoleMasterModel objRoleMasterModel = new RoleMasterModel();
                    objRoleMasterModel.RoleType = txtRoleType.Text;
                    objRoleMasterModel.OverrideAmount = Functions.GetDecimal(txtOverwriteAmount.Text);
                    bool IsRoles = _RoleService.CheckRoleName(txtRoleType.Text.Trim(),PrimaryId);
                    if (!IsRoles)
                    {
                        if (PrimaryId <= 0)
                        {
                            var add = _RoleService.AddRole(objRoleMasterModel, 1);
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
                            objRoleMasterModel.RoleID = PrimaryId;
                            var add = _RoleService.AddRole(objRoleMasterModel, 2);
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtRoleType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtRoleType);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmRole + ex.StackTrace, ex.LineNumber());
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
        public FrmMetro_AddRole()
        {
            InitializeComponent();
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case "txtRoleType":
                    //txtRoleType
                    if ((string.IsNullOrWhiteSpace(txtRoleType.Text)))
                    {
                        txtRoleType.Focus();
                        ep.SetError(txtRoleType, AlertMessages.RoleTypeValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtRoleType.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtRoleType.Focus();
                        ep.SetError(txtRoleType, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtRoleType, CommonModelCont.EmptyString);
                    }
                    if (string.IsNullOrWhiteSpace(txtOverwriteAmount.Text))
                    {
                        //txtOverwriteAmount.Focus();
                        ep.SetError(txtOverwriteAmount, AlertMessages.OverwriteAmountVaild);
                        status = false;
                    }
                    else if (!(Regex.Match(txtOverwriteAmount.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success)
                    {
                        txtOverwriteAmount.Focus();
                        ep.SetError(txtOverwriteAmount, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtOverwriteAmount, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtRoleType
                    if ((string.IsNullOrWhiteSpace(txtRoleType.Text)))
                    {
                        txtRoleType.Focus();
                        ep.SetError(txtRoleType, AlertMessages.RoleTypeValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtRoleType.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtRoleType.Focus();
                        ep.SetError(txtRoleType, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtRoleType, CommonModelCont.EmptyString);
                    }
                    if (string.IsNullOrWhiteSpace(txtOverwriteAmount.Text))
                    {
                        //txtOverwriteAmount.Focus();
                        ep.SetError(txtOverwriteAmount, AlertMessages.OverwriteAmountVaild);
                        status = false;
                    }
                    else if (!(Regex.Match(txtOverwriteAmount.Text, CommonModelCont.OnlyTwoDecimal_Validation)).Success)
                    {
                        txtOverwriteAmount.Focus();
                        ep.SetError(txtOverwriteAmount, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtOverwriteAmount, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtRoleType.Text = null;
            txtOverwriteAmount.Text = null;
            ep.SetError(txtRoleType, CommonModelCont.EmptyString);

            flagSave = false;
            PrimaryId = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_RoleMaster");
        }
        #endregion
    }
}
