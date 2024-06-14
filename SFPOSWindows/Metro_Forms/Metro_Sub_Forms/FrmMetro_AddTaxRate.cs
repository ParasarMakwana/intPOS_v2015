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
    public partial class FrmMetro_AddTaxRate : MetroForm
    {

        #region Properties
        public long PrimaryId = 0;
        TaxRateService _TaxRateService = new TaxRateService();
        TaxGroupService _TaxGroupService = new TaxGroupService();
        TaxRateMasterModel objTaxRateMasterModel = new TaxRateMasterModel();
        ErrorProvider ep = new ErrorProvider();
        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        List<TaxRateMasterModel> lstTaxRateMasterModel = new List<TaxRateMasterModel>();
        List<TaxGroupMasterModel> lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();
        bool flagSave = true;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        #region Events
        private void txtTax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtTax);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void datePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.datePickerStartDate);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void datePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.datePickerEndDate);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                bool IsDetail = false;
                if (flagSave)
                {
                    IsDetail = _TaxRateService.CheckTaxRateName(Convert.ToInt32(cmbTaxGroupCode.SelectedValue), Convert.ToDateTime(datePickerStartDate.Value), Convert.ToDateTime(datePickerEndDate.Value), PrimaryId);
                    if (!IsDetail)
                    {
                        objTaxRateMasterModel.TaxGroupID = Convert.ToInt32(cmbTaxGroupCode.SelectedValue);
                        objTaxRateMasterModel.Tax = Convert.ToDecimal(txtTax.Text);
                        objTaxRateMasterModel.StartDate = Convert.ToDateTime(datePickerStartDate.Value);
                        objTaxRateMasterModel.EndDate = Convert.ToDateTime(datePickerEndDate.Value);
                        if (PrimaryId <= 0)
                        {
                            var add = _TaxRateService.AddTaxRate(objTaxRateMasterModel, 1);
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
                            objTaxRateMasterModel.TaxRateID = PrimaryId;
                            var Update = _TaxRateService.AddTaxRate(objTaxRateMasterModel, 2);
                            PrimaryId = 0;
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
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

        public FrmMetro_AddTaxRate()
        {
            InitializeComponent();
        }

        public void LoadCmbTaxGroupCode()
        {
            try
            {
                lstTaxGroupMasterModel = _TaxGroupService.GetAllTaxGroup();
                cmbTaxGroupCode.DisplayMember = TaxGroupMasterModelCont.TaxGroupName;
                cmbTaxGroupCode.ValueMember = TaxGroupMasterModelCont.TaxGroupID;
                cmbTaxGroupCode.DataSource = lstTaxGroupMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmTaxRateDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.cmbTaxGroupCode:
                    //cmbTaxGroupCode
                    if ((string.IsNullOrWhiteSpace(cmbTaxGroupCode.Text)))
                    {
                        cmbTaxGroupCode.Focus();
                        ep.SetError(cmbTaxGroupCode, AlertMessages.TaxValid);
                        status = false;
                    }
                    else if (cmbTaxGroupCode.SelectedIndex < 0)
                    {
                        cmbTaxGroupCode.Focus();
                        ep.SetError(cmbTaxGroupCode, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtTax, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtTax:
                    //txtTax
                    if ((string.IsNullOrWhiteSpace(txtTax.Text)))
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.TaxValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtTax.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else if (Convert.ToDecimal(txtTax.Text) > 100)
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.TaxValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtTax, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.datePickerStartDate:
                    //datePickerStartDate
                    if (datePickerStartDate.Value < DateTime.Now.Date)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.StartDateValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerStartDate, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.datePickerEndDate:
                    //datePickerEndDate
                    if (datePickerEndDate.Value < DateTime.Now.Date)
                    {
                        datePickerEndDate.Focus();
                        ep.SetError(datePickerEndDate, AlertMessages.EndDateValid1);
                        status = false;
                    }
                    else if (datePickerEndDate.Value < datePickerStartDate.Value)
                    {
                        datePickerEndDate.Focus();
                        ep.SetError(datePickerEndDate, AlertMessages.EndDateValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerEndDate, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtTax
                    if ((string.IsNullOrWhiteSpace(txtTax.Text)))
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.TaxValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtTax.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtTax.Focus();
                        ep.SetError(txtTax, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtTax, CommonModelCont.EmptyString);
                    }
                    //cmbTaxGroupCode
                    if ((string.IsNullOrWhiteSpace(cmbTaxGroupCode.Text)))
                    {
                        cmbTaxGroupCode.Focus();
                        ep.SetError(cmbTaxGroupCode, AlertMessages.TaxValid);
                        status = false;
                    }
                    else if (cmbTaxGroupCode.SelectedIndex < 0)
                    {
                        cmbTaxGroupCode.Focus();
                        ep.SetError(cmbTaxGroupCode, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbTaxGroupCode, CommonModelCont.EmptyString);
                    }
                    //datePickerStartDate
                    if (datePickerStartDate.Value < DateTime.Now.Date)
                    {
                        datePickerStartDate.Focus();
                        ep.SetError(datePickerStartDate, AlertMessages.StartDateValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerStartDate, CommonModelCont.EmptyString);
                    }
                    //datePickerEndDate
                    if (datePickerEndDate.Value < DateTime.Now.Date)
                    {
                        datePickerEndDate.Focus();
                        ep.SetError(datePickerEndDate, AlertMessages.EndDateValid1);
                        status = false;
                    }
                    else if (datePickerEndDate.Value < datePickerStartDate.Value)
                    {
                        datePickerEndDate.Focus();
                        ep.SetError(datePickerEndDate, AlertMessages.EndDateValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerEndDate, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            txtTax.Text = null;
            ep.SetError(txtTax, CommonModelCont.EmptyString);
            datePickerStartDate.Text = DateTime.Now.Date.ToString();
            ep.SetError(datePickerStartDate, CommonModelCont.EmptyString);
            datePickerEndDate.Text = DateTime.Now.Date.ToString();
            ep.SetError(datePickerEndDate, CommonModelCont.EmptyString);
            cmbTaxGroupCode.SelectedIndex = 0;
            ep.SetError(cmbTaxGroupCode, CommonModelCont.EmptyString);

            flagSave = false;
            PrimaryId = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_TaxRateMaster");
        }
        #endregion
    }
}
