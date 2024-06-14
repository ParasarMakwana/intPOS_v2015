using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmPurchaseOrders : Form
    {
        #region Properties
        //private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ErrorProvider ep = new ErrorProvider();
        List<PurchaseOrderMasterModel> lstPurchaseOrderMasterModel = new List<PurchaseOrderMasterModel>();
        PurchaseOrderService _PurchaseorderService = new PurchaseOrderService();
        VendorService _VendorService = new VendorService();
        List<VendorMasterModel> lstVendorMasterModel = new List<VendorMasterModel>();
        PurchaseOrderMasterModel objPurchaseOrderMasterModel = new PurchaseOrderMasterModel();
        
        public static long PrimaryId = 0;
        public bool IsReceived = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        #endregion

        #region Events

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            FrmMetro_AddPurchaseOrder objFrmMetro_AddPurchaseOrder = new FrmMetro_AddPurchaseOrder();
            objFrmMetro_AddPurchaseOrder.cmbVendor();
            objFrmMetro_AddPurchaseOrder.ShowDialog();
            dataLoad();
        }

        private void PurchaseOrderGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt32(PurchaseOrderGrdView.Rows[e.RowIndex].Cells[PurchaseOrderMasterModelCont.PurchaseHeaderID].Value.ToString());
                    IsReceived = Convert.ToBoolean(PurchaseOrderGrdView.Rows[e.RowIndex].Cells[PurchaseOrderMasterModelCont.isReceived].Value);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
            }
        }

        private void PurchaseOrderGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    FrmPurchaseHeaderLine objFrmPurchaseHeaderLine = new FrmPurchaseHeaderLine();
                    objFrmPurchaseHeaderLine.txtVendorInvoice.Text = PurchaseOrderGrdView.Rows[e.RowIndex].Cells[PurchaseOrderMasterModelCont.PONumber].Value.ToString();
                    objFrmPurchaseHeaderLine.productPurchaseHeaderId = PrimaryId;
                    objFrmPurchaseHeaderLine.VendorID = Convert.ToInt64(PurchaseOrderGrdView.Rows[e.RowIndex].Cells[VendorMasterModelCont.VendorID].Value);
                    objFrmPurchaseHeaderLine.txtVendorName.Text = PurchaseOrderGrdView.Rows[e.RowIndex].Cells[VendorMasterModelCont.VendorName].Value.ToString();
                    objFrmPurchaseHeaderLine.txtOrderDate.Text = Convert.ToDateTime(PurchaseOrderGrdView.Rows[e.RowIndex].Cells[PurchaseOrderMasterModelCont.OrderDate].Value).ToShortDateString();
                    objFrmPurchaseHeaderLine.lineDataLoad();

                    if (IsReceived)
                    {
                        objFrmPurchaseHeaderLine.btnPost.ForeColor = Color.White;
                        objFrmPurchaseHeaderLine.btnPost.Cursor = Cursors.No;                       
                        objFrmPurchaseHeaderLine.btnPost.Enabled = false;
                    }
                    objFrmPurchaseHeaderLine.IsReceived = IsReceived;
                    objFrmPurchaseHeaderLine.ShowDialog();
                    Clear();
                    dataLoad();
                }
                if (e.ColumnIndex == 1)
                {
                    if (PrimaryId > 0)
                    {
                        if (!IsReceived)
                        {
                            FrmMetro_AddPurchaseOrder objFrmMetro_AddPurchaseOrder = new FrmMetro_AddPurchaseOrder();
                            objFrmMetro_AddPurchaseOrder.PrimaryId = PrimaryId;
                            objFrmMetro_AddPurchaseOrder.cmbVendor();
                            objFrmMetro_AddPurchaseOrder.cmbVendorName.SelectedValue = PurchaseOrderGrdView.Rows[e.RowIndex].Cells[VendorMasterModelCont.VendorID].Value;
                            objFrmMetro_AddPurchaseOrder.txtVendorInvoiceNo.Text = PurchaseOrderGrdView.Rows[e.RowIndex].Cells[PurchaseOrderMasterModelCont.PONumber].Value.ToString();
                            objFrmMetro_AddPurchaseOrder.datePickerOrderDate.Value = Convert.ToDateTime(PurchaseOrderGrdView.Rows[e.RowIndex].Cells[PurchaseOrderMasterModelCont.OrderDate].Value);
                            objFrmMetro_AddPurchaseOrder.ShowDialog();
                            dataLoad();
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, "Can't edit this order because it is already received.", false);
                        }
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if (PrimaryId > 0)
                    {
                        if (!IsReceived)
                        {
                            DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                objPurchaseOrderMasterModel.PurchaseHeaderID = PrimaryId;

                                var add = _PurchaseorderService.AddEditDeletePurchaseOrder(objPurchaseOrderMasterModel, 3);
                                if (add != null)
                                {
                                    ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess, false);
                                }
                            }
                            Clear();
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, "Can't delete this order because it is already received.", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchVendorName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchStr = txtSearchVendorName.Text;
                if (searchStr != null && searchStr != CommonModelCont.EmptyString && searchStr != AlertMessages.PurchaseOrderSearch)
                {
                    PurchaseOrderGrdView.DataSource = lstPurchaseOrderMasterModel
                        .Where (o => o.VendorName.ToLower().StartsWith(searchStr.ToLower()) 
                                       || o.PONumber.StartsWith(searchStr))
                        .Select(o => new
                        {
                            VendorID = o.VendorID,
                            VendorName = o.VendorName,
                            PONumber = o.PONumber,
                            PurchaseHeaderID = o.PurchaseHeaderID,
                            OrderDate = Convert.ToDateTime(o.OrderDate).Date,
                            isReceived = o.isReceived
                        }).ToList();
                    PurchaseOrderGrdView.Columns[VendorMasterModelCont.VendorID].Visible = false;
                    PurchaseOrderGrdView.Columns[PurchaseOrderMasterModelCont.PurchaseHeaderID].Visible = false;
                    PurchaseOrderGrdView.Columns[PurchaseOrderMasterModelCont.isReceived].Visible = false;

                    PurchaseOrderGrdView.Columns["OrderDate"].HeaderText = "Order Date";
                    PurchaseOrderGrdView.Columns["VendorName"].HeaderText = "Vendor";
                    PurchaseOrderGrdView.Columns["PONumber"].HeaderText = "PO Number";
                }
                else
                {
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region Functions
        public FrmPurchaseOrders()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                lstPurchaseOrderMasterModel = _PurchaseorderService.GetAllPurchaseOrder();
           
                PurchaseOrderGrdView.DataSource = lstPurchaseOrderMasterModel.Select(o => new
                {
                    VendorID = o.VendorID,
                    VendorName = o.VendorName,
                    PONumber = o.PONumber,
                    PurchaseHeaderID = o.PurchaseHeaderID,
                    OrderDate = Convert.ToDateTime(o.OrderDate).Date,
                    isReceived = o.isReceived
                }).ToList();
                PurchaseOrderGrdView.Columns[VendorMasterModelCont.VendorID].Visible = false;
                PurchaseOrderGrdView.Columns[PurchaseOrderMasterModelCont.PurchaseHeaderID].Visible = false;
                PurchaseOrderGrdView.Columns[PurchaseOrderMasterModelCont.isReceived].Visible = false;

                PurchaseOrderGrdView.Columns["OrderDate"].HeaderText = "Order Date";
                PurchaseOrderGrdView.Columns["VendorName"].HeaderText = "Vendor";
                PurchaseOrderGrdView.Columns["PONumber"].HeaderText = "PO Number";

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmPurchaseOrders + ex.StackTrace, ex.LineNumber());
            }
        }


        public void Clear()
        {
            PrimaryId = 0;
            IsReceived = false;
        }
        #endregion

    }
}
