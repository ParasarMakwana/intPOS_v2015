using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.CrystalReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmPurchaseHeaderLine : MetroForm
    {
        #region Properties
        SerialPort ComPort = new SerialPort();
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        //private SerialPinChangedEventHandler SerialPinChangedEventHandler1;
        delegate void SetTextCallback(string text);
        string InputData = String.Empty;

        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        PurchaseOrderMasterModel objPurchaseOrderMasterModel = new PurchaseOrderMasterModel();
        PostedPurchaseLineMasterModel objPostedPurchaseLineMasterModel = new PostedPurchaseLineMasterModel();
        PurchaseLineService _PurchaseLineService = new PurchaseLineService();
        ProductService _ProductService = new ProductService();
        PostedPurchaseHeaderService _PostedPurchaseHeaderService = new PostedPurchaseHeaderService();
        PostedPurchaseLineService _ProductPurchaseLineService = new PostedPurchaseLineService();
        PurchaseOrderService _PurchaseorderService = new PurchaseOrderService();
        PurchaseLineMasterModel objPurchaseLineMasterModel = new PurchaseLineMasterModel();
        public long productPurchaseHeaderId = 0;
        public long VendorID = 0;
        public bool IsReceived = false;

        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        ErrorProvider ep = new ErrorProvider();
        InvoiceMasterModel objInvoiceMasterModel = new InvoiceMasterModel();
        InvoiceService _InvoiceService = new InvoiceService();
        ProductLedgerMasterModel objProductLedgerMasterModel = new ProductLedgerMasterModel();
        ProductLedgerService _ProductLedgerService = new ProductLedgerService();

        List<GetPurchaseLine_ResultModel> Productdata_ = new List<GetPurchaseLine_ResultModel>();

        List<GetPurchaseLine_ResultModel> _GridProductdata = new List<GetPurchaseLine_ResultModel>();
        long PrimaryId = 0;
        #endregion

        #region Events

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                #region GENERATE INVOICE
                bool flagSave = false;
                if (PurchaseLineGrdView.RowCount > -1)
                {
                    flagSave = CheckValidation(CommonModelCont.EmptyString);
                    if (flagSave)
                    {
                        #region MOVE_TO_POSTED_PURCHASE_HEADER/LINE

                        #region Delete_PurchaseHeader
                        objPurchaseOrderMasterModel.PurchaseHeaderID = productPurchaseHeaderId;
                        var Delete_PurchaseHeader = _PurchaseorderService.AddEditDeletePurchaseOrder(objPurchaseOrderMasterModel, 4);
                        #endregion

                        if (PurchaseLineGrdView.Rows.Count > 0)
                        {
                            for (int row = 0; row < PurchaseLineGrdView.Rows.Count; row++)
                            {
                                bool IsExists = false;
                                IsExists = _PurchaseLineService.PurchaseOrderDetail(Convert.ToInt64(productPurchaseHeaderId), Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells["ProductID"].Value));
                                if (!IsExists)
                                {
                                    objPurchaseLineMasterModel.ProductID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells["ProductID"].Value);
                                    objPurchaseLineMasterModel.ItemCode = PurchaseLineGrdView.Rows[row].Cells["ItemCode"].Value.ToString();
                                    objPurchaseLineMasterModel.PurchaseHeaderID = Convert.ToInt64(productPurchaseHeaderId);
                                    objPurchaseLineMasterModel.Quantity = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["Quantity"].Value);
                                    objPurchaseLineMasterModel.Tax = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["Tax"].Value);
                                    objPurchaseLineMasterModel.TaxAmount = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["TaxAmount"].Value);
                                    objPurchaseLineMasterModel.UnitCost = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["UnitCost"].Value);
                                    objPurchaseLineMasterModel.LineAmtExclTax = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["LineAmtExclTax"].Value);
                                    objPurchaseLineMasterModel.LineAmtInclTax = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["LineAmtInclTax"].Value);
                                    objPurchaseLineMasterModel.TaxGroupID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells["TaxGroupID"].Value);
                                    objPurchaseLineMasterModel.PurchaseType = PurchaseLineMasterModelCont.Received;
                                    objPurchaseLineMasterModel = _PurchaseLineService.AddEditDeletePurchaseOrder(objPurchaseLineMasterModel, 1);

                                    objPurchaseLineMasterModel.PurchaseLineID = objPurchaseLineMasterModel.PurchaseLineID;
                                    var Received_Line = _PurchaseLineService.AddEditDeletePurchaseOrder(objPurchaseLineMasterModel, 4);
                                }
                                else
                                {
                                    #region Receive_PurchaseLine
                                    objPurchaseLineMasterModel.PurchaseLineID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells["PurchaseLineID"].Value);
                                    var ReceivedLine = _PurchaseLineService.AddEditDeletePurchaseOrder(objPurchaseLineMasterModel, 4);
                                    #endregion
                                }
                            }
                        }

                        #endregion

                        #region Insert_InvoiceInformation
                        objInvoiceMasterModel.PostedPurchaseHeaderID = productPurchaseHeaderId;
                        objInvoiceMasterModel.PONumber = Convert.ToInt64(txtVendorInvoice.Text);
                        objInvoiceMasterModel.Invoice_Number = txtInvoiceNo.Text;
                        objInvoiceMasterModel.Date = Convert.ToDateTime(datePickerDate.Value);
                        objInvoiceMasterModel.ShippedBy = txtShippedBy.Text;
                        objInvoiceMasterModel.ReceivedBy = txtReceivedBy.Text;
                        objInvoiceMasterModel.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                        objInvoiceMasterModel.Adjustment = txtAdjustment.Text;

                        var add_Invoice = _InvoiceService.AddInvoice(objInvoiceMasterModel, 1);

                        if (add_Invoice != null)
                        {
                            ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.OrderReceived,  false);
                            Close();


                            PoRdlcReport objPoRdlcReport = new PoRdlcReport();
                            objPoRdlcReport.PONumber = Convert.ToInt64(objInvoiceMasterModel.PONumber);
                            objPoRdlcReport.ShowDialog();

                            #region PRODUCT_LEDGER 
                            if (PurchaseLineGrdView.Rows.Count > 0)
                            {
                                for (int row = 0; row < PurchaseLineGrdView.Rows.Count; row++)
                                {
                                    objInvoiceMasterModel.PostedPurchaseHeaderID = productPurchaseHeaderId;
                                    objProductLedgerMasterModel.ProductID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells[ProductMasterModelCont.ProductID].Value);
                                    objProductLedgerMasterModel.Qty = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells[PurchaseLineMasterModelCont.Quantity].Value);
                                    objProductLedgerMasterModel.UPCCode = PurchaseLineGrdView.Rows[row].Cells[PurchaseLineMasterModelCont.UPCCode].Value.ToString();
                                    objProductLedgerMasterModel.PurchasePrice = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells[PurchaseLineMasterModelCont.UnitCost].Value);
                                    objProductLedgerMasterModel.TaxAmount = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells[PurchaseLineMasterModelCont.TaxAmount].Value);
                                    objProductLedgerMasterModel.LedgerTypeID = 1;
                                    objProductLedgerMasterModel.TaxGroupCodeID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells[TaxGroupMasterModelCont.TaxGroupID].Value);
                                    objProductLedgerMasterModel.DepartmentID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells[DepartmentMasterModelCont.DepartmentID].Value);
                                    objProductLedgerMasterModel.SectionID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells[SectionMasterModelCont.SectionID].Value);
                                    objProductLedgerMasterModel.UnitOfMeasureID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells[UoMMasterModelCont.UnitMeasureID].Value);
                                    objProductLedgerMasterModel.RemainingQty = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells[PurchaseLineMasterModelCont.Quantity].Value);
                                    var add_ProductLedger = _ProductLedgerService.AddProductLedger(objProductLedgerMasterModel, 1);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                        }
                        #endregion
                    }
                    else
                    {
                       ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.InvoiceInfoAlert, false);
                    }
                }
                else
                {
                   ClsCommon.MsgBox(AlertMessages.InformationAlert, "Please enter product.!", false);
                }
                #endregion

                ComPort.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtShippedBy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtShippedBy);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtAdjustment_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtAdjustment);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtInvoiceNo);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtReceivedBy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtReceivedBy);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtTotalAmt);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void PurchaseLineGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (PrimaryId > 0)
                        {
                            DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                objPurchaseLineMasterModel.PurchaseLineID = PrimaryId;
                                var DeleteLine = _PurchaseLineService.AddEditDeletePurchaseOrder(objPurchaseLineMasterModel, 3);
                                if (DeleteLine != null)
                                {
                                    ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                                }
                            }
                            dataLoad();
                            PrimaryId = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void PurchaseLineGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt32(PurchaseLineGrdView.Rows[e.RowIndex].Cells[PurchaseLineMasterModelCont.PurchaseLineID].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void PurchaseLineGrdView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {

                    int i = int.Parse(e.RowIndex.ToString());
                    int currentColumnIndex = int.Parse(e.ColumnIndex.ToString());

                    decimal Quantity = (PurchaseLineGrdView.Rows[i].Cells[PurchaseLineMasterModelCont.Quantity].EditedFormattedValue.ToString()) != "" ? Convert.ToDecimal(PurchaseLineGrdView.Rows[i].Cells[PurchaseLineMasterModelCont.Quantity].EditedFormattedValue.ToString()) : 0;
                    decimal Tax = (PurchaseLineGrdView.Rows[i].Cells[PurchaseLineMasterModelCont.Tax].EditedFormattedValue.ToString()) != "" ? Convert.ToDecimal(PurchaseLineGrdView.Rows[i].Cells[PurchaseLineMasterModelCont.Tax].EditedFormattedValue.ToString()) : 0;
                    decimal unitCost = (PurchaseLineGrdView.Rows[i].Cells[PurchaseLineMasterModelCont.UnitCost].EditedFormattedValue.ToString()) != "" ? Convert.ToDecimal(PurchaseLineGrdView.Rows[i].Cells[PurchaseLineMasterModelCont.UnitCost].EditedFormattedValue.ToString()) : 0;
                    decimal totalAmount = Quantity * unitCost;
                    decimal InclTax = totalAmount + (totalAmount / 100) * Tax;
                    decimal TaxAmount = Convert.ToDecimal(((totalAmount / 100) * Tax).ToString());


                    Productdata_[i].Quantity = Convert.ToDecimal(PurchaseLineGrdView.Rows[i].Cells[PurchaseLineMasterModelCont.Quantity].EditedFormattedValue.ToString());
                    //Productdata_[i].TaxAmount = Functions.GetDecimal((Productdata_[i].UnitCost * Productdata_[i].Quantity / 100 * Productdata_[i].Tax).ToString());
                    //Productdata_[i].LineAmtExclTax = Functions.GetDecimal((Productdata_[i].UnitCost * Productdata_[i].Quantity).ToString());
                    //Productdata_[i].LineAmtInclTax = Functions.GetDecimal(((Productdata_[i].UnitCost * Productdata_[i].Quantity) + (Productdata_[i].UnitCost * Productdata_[i].Quantity / 100 * Productdata_[i].Tax)).ToString());

                    Productdata_[i].TaxAmount = TaxAmount;
                    Productdata_[i].LineAmtExclTax = totalAmount;
                    Productdata_[i].LineAmtInclTax = InclTax;

                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        private void PurchaseLineGrdView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            ClsCommon.MsgBox("Information","Please enter numeric value",false);
        }

        private void txtSearchUPCCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchUPCCode.Text != CommonModelCont.EmptyString)
                {
                    List<GetPurchaseLine_ResultModel> Productdata = UPCScanner(txtSearchUPCCode.Text.Trim());
                    if (Productdata.Count > 0)
                    {
                        int x = 0;
                        foreach (GetPurchaseLine_ResultModel objPurchaseLine_ResultModel in Productdata)
                        {
                            if (Productdata_.Count > 0)
                            {
                                for (int i = 0; i < Productdata_.Count; i++)
                                {
                                    if (Productdata_[i].UPCCode == objPurchaseLine_ResultModel.UPCCode)
                                    {
                                        Productdata_[i].Quantity = Convert.ToDecimal(Productdata_[i].Quantity) + 1;
                                        x = 1;
                                        Productdata_[i].TaxAmount = Functions.GetDecimal((Productdata_[i].UnitCost * Convert.ToDecimal(Productdata_[i].Quantity) / 100 * Productdata_[i].Tax).ToString());
                                    }
                                }
                                if (x == 0)
                                {
                                    objPurchaseLine_ResultModel.Quantity = 1;
                                    Productdata_.Add(objPurchaseLine_ResultModel);
                                }
                            }
                            else
                            {
                                objPurchaseLine_ResultModel.Quantity = 1;
                                Productdata_.Add(objPurchaseLine_ResultModel);
                            }
                        }
                        dataLoad();

                        txtSearchUPCCode.Text = "";
                        txtSearchUPCCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmPurchaseHeaderLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (PurchaseLineGrdView.RowCount > 0)
                {
                    if (!IsReceived)
                    {
                        bool IsExists = false;
                        DialogResult res = MessageBox.Show("Are you want to save this order.?", AlertMessages.SuccessAlert, MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            for (int row = 0; row < PurchaseLineGrdView.RowCount; row++)
                            {
                                IsExists = _PurchaseLineService.PurchaseOrderDetail(Convert.ToInt64(productPurchaseHeaderId), Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells["ProductID"].Value));
                                if (!IsExists)
                                {
                                    objPurchaseLineMasterModel.ProductID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells["ProductID"].Value);
                                    objPurchaseLineMasterModel.ItemCode = PurchaseLineGrdView.Rows[row].Cells["ItemCode"].Value.ToString();
                                    objPurchaseLineMasterModel.PurchaseHeaderID = Convert.ToInt64(productPurchaseHeaderId);
                                    objPurchaseLineMasterModel.Quantity = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["Quantity"].Value);
                                    objPurchaseLineMasterModel.Tax = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["Tax"].Value);
                                    objPurchaseLineMasterModel.TaxAmount = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["TaxAmount"].Value);
                                    objPurchaseLineMasterModel.UnitCost = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["UnitCost"].Value);
                                    objPurchaseLineMasterModel.LineAmtExclTax = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["LineAmtExclTax"].Value);
                                    objPurchaseLineMasterModel.LineAmtInclTax = Convert.ToDecimal(PurchaseLineGrdView.Rows[row].Cells["LineAmtInclTax"].Value);
                                    objPurchaseLineMasterModel.TaxGroupID = Convert.ToInt64(PurchaseLineGrdView.Rows[row].Cells["TaxGroupID"].Value);
                                    objPurchaseLineMasterModel.PurchaseType = PurchaseLineMasterModelCont.Received;
                                    objPurchaseLineMasterModel = _PurchaseLineService.AddEditDeletePurchaseOrder(objPurchaseLineMasterModel, 1);
                                }
                            }
                        }
                    }
                }
                ComPort.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
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
        public FrmPurchaseHeaderLine()
        {
            InitializeComponent();
            ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived_1);

            ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            OpenPort();
        }
        public void OpenPort()
        {
            try
            {

                string[] Ports = SerialPort.GetPortNames();
                if (Ports.Count() > 0)
                {
                    if (ComPort.IsOpen == false)
                    {
                        ComPort.PortName = ComInfo.ComPort;
                        ComPort.BaudRate = ComInfo.BaudRate;
                        ComPort.DataBits = ComInfo.DataBits;
                        ComPort.StopBits = ComInfo.StopBits;
                        ComPort.Handshake = ComInfo.Handshake;
                        ComPort.Parity = ComInfo.Parity;
                        ComPort.RtsEnable = true;
                        ComPort.DtrEnable = true;
                        ComPort.Open();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            InputData = ComPort.ReadExisting();
            if (InputData != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(SetText), new object[] { InputData });
            }
        }

        public void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            InputData = ComPort.ReadExisting();
            if (InputData != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(SetText), new object[] { InputData });
            }
        }

        private void SetText(string text)
        {
            if (text.StartsWith("S08"))
            {
                txtSearchUPCCode.Text = text.Remove(0, 4);
                txtSearchUPCCode.Text = txtSearchUPCCode.Text.Remove(txtSearchUPCCode.Text.Length - 1);
                int Count = txtSearchUPCCode.Text.Length;
                if (Count < 13)
                {
                    Count = 13 - Count;
                    for (int i = 0; i < Count; i++)
                    {
                        txtSearchUPCCode.Text = "0" + txtSearchUPCCode.Text;
                    }
                }
            }
        }

        //public void OpenPort()
        //{
        //    ComPort.PortName = Convert.ToString("COM1");
        //    ComPort.BaudRate = Convert.ToInt32(9600);
        //    ComPort.DataBits = Convert.ToInt16(7);
        //    ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
        //    ComPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "XOnXOff");
        //    ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
        //    ComPort.RtsEnable = true;
        //    ComPort.DtrEnable = true;
        //    ComPort.Open();
        //}

        public void lineDataLoad()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var results = from pl in _db.tbl_PurchaseLine
                              join pm in _db.tbl_ProductMaster
                              on pl.ProductID equals pm.ProductID
                              where pl.PurchaseHeaderID == productPurchaseHeaderId
                              select new
                              {
                                  PurchaseLineID = pl.PurchaseLineID,
                                  PurchaseHeaderID = pl.PurchaseHeaderID,
                                  ProductID = pl.ProductID,
                                  UnitMeasureID = pm.UnitMeasureID,
                                  DepartmentID = pm.DepartmentID,
                                  SectionID = pm.SectionID,
                                  TaxGroupID = pl.TaxGroupID,
                                  UPCCode = pm.UPCCode,
                                  ItemCode = pl.ItemCode,
                                  ProductName = pm.ProductName,
                                  Quantity = pl.Quantity,
                                  UnitCost = pl.UnitCost,
                                  Tax = pl.Tax,
                                  TaxAmount = pl.TaxAmount,
                                  LineAmtExclTax = pl.LineAmtExclTax,
                                  LineAmtInclTax = pl.LineAmtInclTax
                              };

                if (results != null)
                {
                    foreach (var item in results)
                    {
                        GetPurchaseLine_ResultModel obj = new GetPurchaseLine_ResultModel();
                        obj.PurchaseLineID = item.PurchaseLineID;
                        obj.PurchaseHeaderID = item.PurchaseHeaderID;
                        obj.ProductID = item.ProductID;
                        obj.UnitMeasureID = item.UnitMeasureID;
                        obj.DepartmentID = item.DepartmentID;
                        obj.SectionID = item.SectionID;
                        obj.TaxGroupID = item.TaxGroupID;
                        obj.UPCCode = item.UPCCode;
                        obj.ItemCode = item.ItemCode;
                        obj.ProductName = item.ProductName;
                        obj.Quantity = item.Quantity;
                        obj.UnitCost = item.UnitCost;
                        obj.Tax = item.Tax;
                        obj.TaxAmount = item.TaxAmount;
                        obj.LineAmtExclTax = item.LineAmtExclTax;
                        obj.LineAmtInclTax = item.LineAmtInclTax;
                        Productdata_.Add(obj);
                    }
                    //PurchaseLineGrdView.DataSource = typeof(List);
                    //PurchaseLineGrdView.DataSource = Productdata_;
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        public void dataLoad()
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(PurchaseLineMasterModelCont.PurchaseLineID, typeof(long));
                dt.Columns.Add(PurchaseOrderMasterModelCont.PurchaseHeaderID, typeof(long));
                dt.Columns.Add(ProductMasterModelCont.ProductID, typeof(long));
                dt.Columns.Add(UoMMasterModelCont.UnitMeasureID, typeof(long));
                dt.Columns.Add(DepartmentMasterModelCont.DepartmentID, typeof(long));
                dt.Columns.Add(SectionMasterModelCont.SectionID, typeof(long));
                dt.Columns.Add(TaxGroupMasterModelCont.TaxGroupID, typeof(long));
                dt.Columns.Add(ProductMasterModelCont.UPCCode, typeof(string));
                dt.Columns.Add(PurchaseLineMasterModelCont.ItemCode, typeof(string));
                dt.Columns.Add(ProductMasterModelCont.ProductName, typeof(string));
                dt.Columns.Add(PurchaseLineMasterModelCont.Quantity, typeof(long));
                dt.Columns.Add(PurchaseLineMasterModelCont.UnitCost, typeof(decimal));
                dt.Columns.Add(PurchaseLineMasterModelCont.Tax, typeof(decimal));
                dt.Columns.Add(PurchaseLineMasterModelCont.TaxAmount, typeof(decimal));
                dt.Columns.Add(PurchaseLineMasterModelCont.LineAmtExclTax, typeof(decimal));
                dt.Columns.Add(PurchaseLineMasterModelCont.LineAmtInclTax, typeof(decimal));

                foreach (var item in Productdata_)
                {
                    DataRow dr = dt.NewRow();
                    if (item.PurchaseLineID != null)
                        dr["PurchaseLineID"] = item.PurchaseLineID;
                    dr["PurchaseHeaderID"] = item.PurchaseHeaderID;
                    dr["ProductID"] = item.ProductID;
                    dr["UnitMeasureID"] = item.UnitMeasureID == null ? 0 : item.UnitMeasureID;
                    dr["DepartmentID"] = item.DepartmentID;
                    dr["SectionID"] = item.SectionID;
                    dr["TaxGroupID"] = item.TaxGroupID == null ? 0 : item.TaxGroupID;
                    dr["UPCCode"] = item.UPCCode;
                    dr["ItemCode"] = item.ItemCode;
                    dr["ProductName"] = item.ProductName;
                    dr["Quantity"] = item.Quantity;
                    dr["UnitCost"] = item.UnitCost;
                    dr["Tax"] = item.Tax;
                    dr["TaxAmount"] = item.TaxAmount;
                    dr["LineAmtExclTax"] = item.LineAmtExclTax;
                    dr["LineAmtInclTax"] = item.LineAmtInclTax;
                    dt.Rows.Add(dr);
                }
                PurchaseLineGrdView.DataSource = dt;

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmPurchaseHeaderLine + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckValidation(string ControlName)
        {

            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.datePickerDate:
                    //datePickerDate
                    if (datePickerDate.Value < Convert.ToDateTime(txtOrderDate.Text))
                    {
                        datePickerDate.Focus();
                        ep.SetError(datePickerDate, AlertMessages.StartDateValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerDate, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtTotalAmt:
                    //txtTotalAmt
                    if ((string.IsNullOrWhiteSpace(txtTotalAmt.Text)))
                    {
                        txtTotalAmt.Focus();
                        ep.SetError(txtTotalAmt, AlertMessages.TotalAmountValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtTotalAmt.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtTotalAmt.Focus();
                        ep.SetError(txtTotalAmt, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtTotalAmt, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtShippedBy:
                    //txtShippedBy
                    if ((string.IsNullOrWhiteSpace(txtShippedBy.Text)))
                    {
                        txtShippedBy.Focus();
                        ep.SetError(txtShippedBy, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtShippedBy.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtShippedBy.Focus();
                        ep.SetError(txtShippedBy, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtShippedBy, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtInvoiceNo:
                    //txtInvoiceNo
                    if ((string.IsNullOrWhiteSpace(txtInvoiceNo.Text)))
                    {
                        txtInvoiceNo.Focus();
                        ep.SetError(txtInvoiceNo, AlertMessages.invoiceValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtInvoiceNo.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        txtInvoiceNo.Focus();
                        ep.SetError(txtInvoiceNo, AlertMessages.invoiceValidation2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtInvoiceNo, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtReceivedBy:
                    //txtReceivedBy
                    if ((string.IsNullOrWhiteSpace(txtReceivedBy.Text)))
                    {
                        txtReceivedBy.Focus();
                        ep.SetError(txtReceivedBy, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtReceivedBy.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtReceivedBy.Focus();
                        ep.SetError(txtReceivedBy, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtReceivedBy, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //datePickerDate
                    if (datePickerDate.Value < Convert.ToDateTime(txtOrderDate.Text))
                    {
                        datePickerDate.Focus();
                        ep.SetError(datePickerDate, AlertMessages.StartDateValid);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(datePickerDate, CommonModelCont.EmptyString);
                    }
                    //txtTotalAmt
                    if ((string.IsNullOrWhiteSpace(txtTotalAmt.Text)))
                    {
                        txtTotalAmt.Focus();
                        ep.SetError(txtTotalAmt, AlertMessages.TotalAmountValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtTotalAmt.Text, CommonModelCont.NumericOnetoNine_Validation_withDot)).Success))
                    {
                        txtTotalAmt.Focus();
                        ep.SetError(txtTotalAmt, AlertMessages.OnlyNumberAllow);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtTotalAmt, CommonModelCont.EmptyString);
                    }
                    //txtShippedBy
                    if ((string.IsNullOrWhiteSpace(txtShippedBy.Text)))
                    {
                        txtShippedBy.Focus();
                        ep.SetError(txtShippedBy, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtShippedBy.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtShippedBy.Focus();
                        ep.SetError(txtShippedBy, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtShippedBy, CommonModelCont.EmptyString);
                    }
                    //txtInvoiceNo
                    if ((string.IsNullOrWhiteSpace(txtInvoiceNo.Text)))
                    {
                        txtInvoiceNo.Focus();
                        ep.SetError(txtInvoiceNo, AlertMessages.invoiceValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtInvoiceNo.Text, CommonModelCont.NumericOnetoNine_Validation)).Success))
                    {
                        txtInvoiceNo.Focus();
                        ep.SetError(txtInvoiceNo, AlertMessages.invoiceValidation2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtInvoiceNo, CommonModelCont.EmptyString);
                    }
                    //txtReceivedBy
                    if ((string.IsNullOrWhiteSpace(txtReceivedBy.Text)))
                    {
                        txtReceivedBy.Focus();
                        ep.SetError(txtReceivedBy, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtReceivedBy.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtReceivedBy.Focus();
                        ep.SetError(txtReceivedBy, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtReceivedBy, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public List<GetPurchaseLine_ResultModel> UPCScanner(string UPCCode)
        {
            return _PurchaseLineService.GetAllPurchaseLine(UPCCode, productPurchaseHeaderId);
        }

        #endregion
    }
}
