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
    public partial class FrmMetro_AddPurchaseOrder : MetroForm
    {
        #region Properties
        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ErrorProvider ep = new ErrorProvider();
        List<PurchaseOrderMasterModel> lstPurchaseOrderMasterModel = new List<PurchaseOrderMasterModel>();
        PurchaseOrderService _PurchaseorderService = new PurchaseOrderService();
        VendorService _VendorService = new VendorService();
        List<VendorMasterModel> lstVendorMasterModel = new List<VendorMasterModel>();
        PurchaseOrderMasterModel objPurchaseOrderMasterModel = new PurchaseOrderMasterModel();
        bool flagSave = false;
        public long PrimaryId = 0;
        public bool IsReceived = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        public FrmMetro_AddPurchaseOrder()
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
                    bool IsVendorInvoice = _PurchaseorderService.CheckInvoiceNo(txtVendorInvoiceNo.Text.Trim(), PrimaryId);
                    if (!IsVendorInvoice)
                    {
                        objPurchaseOrderMasterModel.OrderDate = datePickerOrderDate.Value;
                        objPurchaseOrderMasterModel.VendorID = Convert.ToInt32(cmbVendorName.SelectedValue);
                        objPurchaseOrderMasterModel.PONumber = txtVendorInvoiceNo.Text;
                        if (PrimaryId <= 0)
                        {
                            var add = _PurchaseorderService.AddEditDeletePurchaseOrder(objPurchaseOrderMasterModel, 1);
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
                            objPurchaseOrderMasterModel.PurchaseHeaderID = PrimaryId;
                            var add = _PurchaseorderService.AddEditDeletePurchaseOrder(objPurchaseOrderMasterModel, 2);
                            if (add != null)
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.cmbVendorName:
                    //cmbVendorName
                    if ((string.IsNullOrWhiteSpace(cmbVendorName.Text)))
                    {
                        cmbVendorName.Focus();
                        ep.SetError(cmbVendorName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbVendorName.SelectedIndex < 0)
                    {
                        cmbVendorName.Focus();
                        ep.SetError(cmbVendorName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbVendorName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtVendorInvoiceNo:
                    //txtVendorInvoiceNo
                    if ((string.IsNullOrWhiteSpace(txtVendorInvoiceNo.Text)))
                    {
                        txtVendorInvoiceNo.Focus();
                        ep.SetError(txtVendorInvoiceNo, AlertMessages.VendorInvoiceNoValid1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtVendorInvoiceNo.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        txtVendorInvoiceNo.Focus();
                        ep.SetError(txtVendorInvoiceNo, AlertMessages.VendorInvoiceNoValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtVendorInvoiceNo, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.datePickerOrderDate:
                    //datePickerOrderDate
                    if (datePickerOrderDate.Value < DateTime.Now.Date)
                    {
                        datePickerOrderDate.Focus();
                        ep.SetError(datePickerOrderDate, AlertMessages.purchaseOrderOrderDate);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerOrderDate, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtVendorInvoiceNo
                    if ((string.IsNullOrWhiteSpace(txtVendorInvoiceNo.Text)))
                    {
                        txtVendorInvoiceNo.Focus();
                        ep.SetError(txtVendorInvoiceNo, AlertMessages.VendorInvoiceNoValid1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtVendorInvoiceNo.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        txtVendorInvoiceNo.Focus();
                        ep.SetError(txtVendorInvoiceNo, AlertMessages.VendorInvoiceNoValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtVendorInvoiceNo, CommonModelCont.EmptyString);
                    }
                    //datePickerOrderDate
                    if (datePickerOrderDate.Value < DateTime.Now.Date)
                    {
                        datePickerOrderDate.Focus();
                        ep.SetError(datePickerOrderDate, AlertMessages.purchaseOrderOrderDate);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerOrderDate, CommonModelCont.EmptyString);
                    }
                    //cmbVendorName
                    if ((string.IsNullOrWhiteSpace(cmbVendorName.Text)))
                    {
                        cmbVendorName.Focus();
                        ep.SetError(cmbVendorName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbVendorName.SelectedIndex < 0)
                    {
                        cmbVendorName.Focus();
                        ep.SetError(cmbVendorName, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbVendorName, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void Clear()
        {
            PrimaryId = 0;
            
            txtVendorInvoiceNo.Text = null;
            ep.SetError(txtVendorInvoiceNo, CommonModelCont.EmptyString);
            ep.SetError(datePickerOrderDate, CommonModelCont.EmptyString);
            cmbVendorName.SelectedIndex = 0;
            ep.SetError(cmbVendorName, CommonModelCont.EmptyString);
            flagSave = false;
            IsReceived = false;
        }

        private void txtVendorInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtVendorInvoiceNo);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
            }
        }

        public void cmbVendor()
        {
            try
            {
                lstVendorMasterModel = _VendorService.GetAllVendor();
                //lstVendorMasterModel.Insert(0, new VendorMasterModel { VendorID = 0, VendorName = VendorMasterModelCont.cmbVendorFirst });
                cmbVendorName.DisplayMember = VendorMasterModelCont.VendorName;
                cmbVendorName.ValueMember = VendorMasterModelCont.VendorID;
                cmbVendorName.DataSource = lstVendorMasterModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
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
