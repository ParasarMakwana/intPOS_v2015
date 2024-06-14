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
    public partial class FrmMetro_AddUoM : MetroForm
    {
        #region Properties
        UoMService _UoMService = new UoMService();
        ErrorProvider ep = new ErrorProvider();
        public long PrimaryId = 0;
        UoMMasterModel objUoMMasterModel = new UoMMasterModel();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        List<UoMMasterModel> lstUoMMasterModel = new List<UoMMasterModel>();
        #endregion

        #region Events

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    bool IsUOM = _UoMService.CheckUoMCode(txtUoMName.Text.Trim(), Convert.ToInt32(PrimaryId));
                    if (!IsUOM)
                    {
                        objUoMMasterModel.UnitMeasureCode = txtUoMName.Text.Trim();
                        objUoMMasterModel.Description = txtDescription.Text.Trim();
                        if (PrimaryId <= 0)
                        {
                            var add = _UoMService.AddEditDeleteUoM(objUoMMasterModel, 1);
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
                            objUoMMasterModel.UnitMeasureID = PrimaryId;
                            var Update = _UoMService.AddEditDeleteUoM(objUoMMasterModel, 2);
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtUoMName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtUoMName);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmUoM + ex.StackTrace, ex.LineNumber());
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
        public FrmMetro_AddUoM()
        {
            InitializeComponent();
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtUoMName:
                    //txtUoMName
                    if (string.IsNullOrWhiteSpace(txtUoMName.Text))
                    {
                        txtUoMName.Focus();
                        ep.SetError(txtUoMName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtUoMName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtUoMName.Focus();
                        ep.SetError(txtUoMName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtUoMName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtUoMName
                    if (string.IsNullOrWhiteSpace(txtUoMName.Text))
                    {
                        txtUoMName.Focus();
                        ep.SetError(txtUoMName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtUoMName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtUoMName.Focus();
                        ep.SetError(txtUoMName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtUoMName, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            PrimaryId = 0;

            txtUoMName.Text = null;
            ep.SetError(txtUoMName, CommonModelCont.EmptyString);

            txtDescription.Text = null;
            ep.SetError(txtDescription, CommonModelCont.EmptyString);

            
            flagSave = false;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_UnitMeasureMaster");
        }
        #endregion
    }
}
