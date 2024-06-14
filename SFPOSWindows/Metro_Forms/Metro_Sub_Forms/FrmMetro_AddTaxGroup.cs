using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddTaxGroup : MetroForm
    {

        #region Properties
        ErrorProvider ep = new ErrorProvider();
        public long PrimaryId = 0;
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
                    TaxGroupService _TaxGroupService = new TaxGroupService();
                    bool IsTaxGroup = _TaxGroupService.CheckTaxGroupName(txtTaxGroupCode.Text.Trim());
                    if (!IsTaxGroup)
                    {
                        TaxGroupMasterModel objTaxGroupMasterModel = new TaxGroupMasterModel();
                        if (PrimaryId <= 0)
                        {
                            objTaxGroupMasterModel.TaxGroupName = txtTaxGroupCode.Text.Trim();
                            var add = _TaxGroupService.AddTaxGroup(objTaxGroupMasterModel, 1);
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
                            objTaxGroupMasterModel.TaxGroupID = PrimaryId;
                            objTaxGroupMasterModel.TaxGroupName = txtTaxGroupCode.Text.Trim();
                            var Update = _TaxGroupService.AddTaxGroup(objTaxGroupMasterModel, 2);
                            if (Update != null)
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
            }
            
        }

        private void txtTaxGroupCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtTaxGroupCode);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxGroup + ex.StackTrace, ex.LineNumber());
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
        public FrmMetro_AddTaxGroup()
        {
            InitializeComponent();
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtTaxGroupCode:
                    //txtTaxGroupCode
                    if ((string.IsNullOrWhiteSpace(txtTaxGroupCode.Text)))
                    {
                        txtTaxGroupCode.Focus();
                        ep.SetError(txtTaxGroupCode, AlertMessages.TaxGroupCodeValid);
                        status = false;
                    }
                    //else if ((!(Regex.Match(txtTaxGroupCode.Text, CommonModelCont.Name_Validation)).Success))
                    //{
                    //    txtTaxGroupCode.Focus();
                    //    ep.SetError(txtTaxGroupCode, "Tax group code name can't contain numeric value");
                    //    status = false;
                    //}
                    else
                    {
                        ep.SetError(txtTaxGroupCode, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtTaxGroupCode
                    if ((string.IsNullOrWhiteSpace(txtTaxGroupCode.Text)))
                    {
                        txtTaxGroupCode.Focus();
                        ep.SetError(txtTaxGroupCode, AlertMessages.TaxGroupCodeValid);
                        status = false;
                    }
                    //else if ((!(Regex.Match(txtTaxGroupCode.Text, CommonModelCont.Name_Validation)).Success))
                    //{
                    //    txtTaxGroupCode.Focus();
                    //    ep.SetError(txtTaxGroupCode, AlertMessages.NameValidation);
                    //    status = false;
                    //}
                    else
                    {
                        ep.SetError(txtTaxGroupCode, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtTaxGroupCode.Text = null;
            ep.SetError(txtTaxGroupCode, CommonModelCont.EmptyString);
            flagSave = false;
            PrimaryId = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_TaxGroupMaster");
        }
        #endregion
    }
}
