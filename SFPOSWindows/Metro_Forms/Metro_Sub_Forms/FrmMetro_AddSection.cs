using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddSection : MetroForm
    {

        #region Properties
        private SectionService _SectionService = new SectionService();
        ErrorProvider ep = new ErrorProvider();
        SectionMasterModel objSectionMasterModel = new SectionMasterModel();
        public long PrimaryId = 0;
        public int DepartmentId = 0;
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        #endregion

        #region Events
        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    //bool IsDepartment = _SectionService.CheckSectionName(txtSectionName.Text.Trim());
                    //if (!IsDepartment)
                    //{
                    if (PrimaryId <= 0)
                    {
                        objSectionMasterModel.DepartmentID = DepartmentId;
                        objSectionMasterModel.SectionName = txtSectionName.Text.Trim();
                        if (txtAgeVerificationAge.Text.Trim() == "")
                        {
                            objSectionMasterModel.AgeVarificationAge = null;
                        }
                        else
                        {
                            objSectionMasterModel.AgeVarificationAge = Convert.ToInt32(txtAgeVerificationAge.Text.Trim());
                        }

                        objSectionMasterModel.TaxGroupID = Convert.ToInt32(cmbTaxGroup.SelectedValue);
                        var add = _SectionService.AddSection(objSectionMasterModel, 1);
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
                        objSectionMasterModel.SectionID = PrimaryId;
                        objSectionMasterModel.DepartmentID = DepartmentId;
                        objSectionMasterModel.SectionName = txtSectionName.Text.Trim();
                        if (txtAgeVerificationAge.Text.Trim() == "")
                        {
                            objSectionMasterModel.AgeVarificationAge = null;
                        }
                        else
                        {
                            objSectionMasterModel.AgeVarificationAge = Convert.ToInt32(txtAgeVerificationAge.Text.Trim());
                        }

                        objSectionMasterModel.TaxGroupID = Convert.ToInt32(cmbTaxGroup.SelectedValue);
                        var add = _SectionService.AddSection(objSectionMasterModel, 2);
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
                    //}
                    //else
                    //{
                    //    ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.AlreadyExist, false);
                    //}
                    Clear();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSectionName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtSectionName);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSection + ex.StackTrace, ex.LineNumber());
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

        public FrmMetro_AddSection()
        {
            InitializeComponent();
            LoadCmbTaxGroupCode();
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_SectionMaster");
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtSectionName:
                    //txtSectionName
                    if ((string.IsNullOrWhiteSpace(txtSectionName.Text)))
                    {
                        txtSectionName.Focus();
                        ep.SetError(txtSectionName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    //else if ((!(Regex.Match(txtSectionName.Text, CommonModelCont.Name_Validation)).Success))
                    //{
                    //    txtSectionName.Focus();
                    //    ep.SetError(txtSectionName, AlertMessages.NameValidation);
                    //    status = false;
                    //}
                    else
                    {
                        ep.SetError(txtSectionName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtSectionName
                    if ((string.IsNullOrWhiteSpace(txtSectionName.Text)))
                    {
                        txtSectionName.Focus();
                        ep.SetError(txtSectionName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    //else if ((!(Regex.Match(txtSectionName.Text, CommonModelCont.Name_Validation)).Success))
                    //{
                    //    txtSectionName.Focus();
                    //    ep.SetError(txtSectionName, AlertMessages.NameValidation);
                    //    status = false;
                    //}
                    else
                    {
                        ep.SetError(txtSectionName, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtSectionName.Text = null;
            ep.SetError(txtSectionName, CommonModelCont.EmptyString);

            flagSave = false;
            PrimaryId = 0;
        }

        public void LoadCmbTaxGroupCode()
        {
            try
            {
                List<TaxGroupMasterModel> lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();
                TaxGroupService _TaxGroupService = new TaxGroupService();

                lstTaxGroupMasterModel = _TaxGroupService.GetAllTaxGroup();
                lstTaxGroupMasterModel.Insert(0, new TaxGroupMasterModel { TaxGroupID = 0, TaxGroupName = TaxGroupMasterModelCont.cmbTaxGroupCodeFirst });
                cmbTaxGroup.DisplayMember = TaxGroupMasterModelCont.TaxGroupName;
                cmbTaxGroup.ValueMember = TaxGroupMasterModelCont.TaxGroupID;
                cmbTaxGroup.DataSource = lstTaxGroupMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion
    }
}
