using MetroFramework.Forms;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.spDataClasses;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddDepartment : MetroForm
    {

        #region Properties
        public long DepartmentID = 0;
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        DepartmentService _DepartmentService = new DepartmentService();
        #endregion

        #region Events
        private void txtDepartmentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtDepartmentName);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    bool IsCategory = false; //_DepartmentService.CheckDepartmentName(txtDepartmentName.Text.Trim(), DepartmentID); 
                    if (!IsCategory)
                    {
                        DepartmentMasterModel objDepartmentMasterModel = new DepartmentMasterModel();

                        objDepartmentMasterModel.DepartmentName = txtDepartmentName.Text.Trim();
                        if (txtAgeVerificationAge.Text.Trim() == "")
                        {
                            objDepartmentMasterModel.AgeVarificationAge = null;
                        }
                        else
                        {
                            objDepartmentMasterModel.AgeVarificationAge = Convert.ToInt32(txtAgeVerificationAge.Text.Trim());
                        }
                        objDepartmentMasterModel.UnitMeasureID = Convert.ToInt32(cmbUoM.SelectedValue);
                        objDepartmentMasterModel.TaxGroupID = Convert.ToInt32(cmbTaxGroup.SelectedValue);
                        objDepartmentMasterModel.DepartmentNo = (String.IsNullOrEmpty(txtDepartmentNo.Text.ToString()) ? 0 : Convert.ToInt32(txtDepartmentNo.Text.Trim()));
                        objDepartmentMasterModel.SubNo = (String.IsNullOrEmpty(txtSubNo.Text.ToString()) ? 0 : Convert.ToInt32(txtSubNo.Text.Trim()));
                        objDepartmentMasterModel.BtnCode = (String.IsNullOrEmpty(txtDeptBtnCode.Text.ToString()) ? null : txtDeptBtnCode.Text.Trim());
                        objDepartmentMasterModel.DepartmentBtnIndex = (String.IsNullOrEmpty(txtDeptBtnCodeIndex.Text.ToString()) ? 0 : Convert.ToInt32(txtDeptBtnCodeIndex.Text.Trim()));
                        objDepartmentMasterModel.ForcedTaxSuffix = (String.IsNullOrEmpty(txtForcedTaxSuffix.Text.ToString()) ? null : txtForcedTaxSuffix.Text.Trim());
                        objDepartmentMasterModel.ForcedTaxSection = Convert.ToInt32(cmbSection.SelectedValue);
                        if (objDepartmentMasterModel.BtnCode != null && (objDepartmentMasterModel.BtnCode.ToUpper().StartsWith("PK")))
                        {
                            if (objDepartmentMasterModel.DepartmentNo != 0)
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, "For PK items deparment number must be 0", false);
                                return;
                            }
                        }

                        if (ToggleIsActive.Checked)
                            objDepartmentMasterModel.IsActive = true;
                        else
                            objDepartmentMasterModel.IsActive = false;

                        if (toggleFdStamp.Checked)
                            objDepartmentMasterModel.IsFoodStamp = true;
                        else
                            objDepartmentMasterModel.IsFoodStamp = false;
                        if (chkIsForceTax.Checked)
                            objDepartmentMasterModel.IsForceTax = true;
                        else
                            objDepartmentMasterModel.IsForceTax = false;

                        if (toggleBtnActive.Checked)
                            objDepartmentMasterModel.DepartmentBtn = true;
                        else
                            objDepartmentMasterModel.DepartmentBtn = false;
                        if (DepartmentID <= 0)
                        {
                            var add = _DepartmentService.AddDepartment(objDepartmentMasterModel, 1);
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }
                        }
                        else if (DepartmentID > 0)
                        {
                            objDepartmentMasterModel.DepartmentID = DepartmentID;
                            var add = _DepartmentService.AddDepartment(objDepartmentMasterModel, 2);
                            if (add != null)
                            {
                                ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Update, false);
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmDepartment + ex.StackTrace, ex.LineNumber());
            }

        }

        private void ToggleIsActive_CheckedChanged(object sender, EventArgs e)
        {
            if (ToggleIsActive.Checked)
                lblActive.Text = "Active";
            else
                lblActive.Text = "InActive";
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
        public FrmMetro_AddDepartment(int DepartmentID)
        {
            InitializeComponent();
            LoadCmbUoMCode();
            LoadCmbTaxGroupCode();
            LoadCmbSectionName(Convert.ToInt32(DepartmentID));
        }

        public void Clear()
        {
            txtDepartmentName.Text = null;
            ep.SetError(txtDepartmentName, CommonModelCont.EmptyString);
            flagSave = false;
            DepartmentID = 0;
        }

        public void LoadCmbUoMCode()
        {
            try
            {
                List<UoMMasterModel> lstUoMMasterModel = new List<UoMMasterModel>();
                UoMService _UoMService = new UoMService();

                lstUoMMasterModel = _UoMService.GetAllUoM();
                lstUoMMasterModel.Insert(0, new UoMMasterModel { UnitMeasureID = 0, UnitMeasureCode = UoMMasterModelCont.cmbUoMFirst });
                cmbUoM.DisplayMember = UoMMasterModelCont.UnitMeasureCode;
                cmbUoM.ValueMember = UoMMasterModelCont.UnitMeasureID;
                cmbUoM.DataSource = lstUoMMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProduct + ex.StackTrace, ex.LineNumber());
            }
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

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtDepartmentName:
                    //txtDepartmentName
                    if ((string.IsNullOrWhiteSpace(txtDepartmentName.Text)))
                    {
                        txtDepartmentName.Focus();
                        ep.SetError(txtDepartmentName, AlertMessages.DepartmentNameValidation);
                        status = false;
                    }
                    //else if ((!(Regex.Match(txtDepartmentName.Text, CommonModelCont.Name_Validation)).Success))
                    //{
                    //    txtDepartmentName.Focus();
                    //    ep.SetError(txtDepartmentName, AlertMessages.NameValidation);
                    //    status = false;
                    //}
                    else
                    {
                        ep.SetError(txtDepartmentName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbTaxGroup:
                    //cmbTaxGroup
                    if ((string.IsNullOrWhiteSpace(cmbTaxGroup.Text)))
                    {
                        cmbTaxGroup.Focus();
                        ep.SetError(cmbTaxGroup, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbTaxGroup.SelectedIndex < 0)
                    {
                        cmbTaxGroup.Focus();
                        ep.SetError(cmbTaxGroup, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbTaxGroup, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbUoM:
                    //cmbUoM
                    if ((string.IsNullOrWhiteSpace(cmbUoM.Text)))
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbUoM.SelectedIndex < 0)
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbUoM, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtDepartmentName
                    if ((string.IsNullOrWhiteSpace(txtDepartmentName.Text)))
                    {
                        txtDepartmentName.Focus();
                        ep.SetError(txtDepartmentName, AlertMessages.DepartmentNameValidation);
                        status = false;
                    }
                    //else if ((!(Regex.Match(txtDepartmentName.Text, CommonModelCont.Name_Validation)).Success))
                    //{
                    //    txtDepartmentName.Focus();
                    //    ep.SetError(txtDepartmentName, AlertMessages.NameValidation);
                    //    status = false;
                    //}
                    else
                    {
                        ep.SetError(txtDepartmentName, CommonModelCont.EmptyString);
                    }
                    //cmbTaxGroup
                    if ((string.IsNullOrWhiteSpace(cmbTaxGroup.Text)))
                    {
                        cmbTaxGroup.Focus();
                        ep.SetError(cmbTaxGroup, AlertMessages.TaxValid);
                        status = false;
                    }
                    else if (cmbTaxGroup.SelectedIndex < 0)
                    {
                        cmbTaxGroup.Focus();
                        ep.SetError(cmbTaxGroup, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbTaxGroup, CommonModelCont.EmptyString);
                    }
                    //cmbUoM
                    if ((string.IsNullOrWhiteSpace(cmbUoM.Text)))
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbUoM.SelectedIndex < 0)
                    {
                        cmbUoM.Focus();
                        ep.SetError(cmbUoM, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbUoM, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_DepartmentMaster");
        }
        #endregion

        public void toggleBtnActive_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleBtnActive.Checked)
            {
                if (_DepartmentService.CheckDepartmentButtonFlag())
                {
                    toggleBtnActive.Checked = false;
                    ClsCommon.MsgBox("Information", "You have reached your maximum limit of Department Mapping, Only 10 department should be map.", false);
                }
            }
        }

        public void LoadCmbSectionName(int departmentid)
        {
            try
            {
                // departmentid = 0;
                List<SP_GetSectionList_Result_Model> lstSectionMasterModel = new List<SP_GetSectionList_Result_Model>();
                SectionService _SectionService = new SectionService();

                lstSectionMasterModel = _SectionService.SectionDetail(departmentid);
                if (lstSectionMasterModel.Count > 0)
                {
                    lstSectionMasterModel.Insert(0, new SP_GetSectionList_Result_Model { SectionID = 0, SectionName = SectionMasterModelCont.cmbSectionFirst });
                    cmbSection.DisplayMember = SectionMasterModelCont.SectionName;
                    cmbSection.ValueMember = SectionMasterModelCont.SectionID;
                    cmbSection.DataSource = lstSectionMasterModel;
                }
                else
                {
                    cmbSection.DataSource = null;
                    lstSectionMasterModel.Insert(0, new SP_GetSectionList_Result_Model { SectionID = 0, SectionName = SectionMasterModelCont.cmbSectionFirst });
                    cmbSection.DisplayMember = SectionMasterModelCont.SectionName;
                    cmbSection.ValueMember = SectionMasterModelCont.SectionID;
                    cmbSection.DataSource = lstSectionMasterModel;
                    cmbSection.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmMetro_AddProduct + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
