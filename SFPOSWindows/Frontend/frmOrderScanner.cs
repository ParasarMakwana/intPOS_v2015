﻿using Microsoft.PointOfService;
using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Printer;
using SFPOS.Printer.Enums;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class frmOrderScanner : Form
    {
        #region Properties
        decimal Balance = 0;
        decimal FSEligibleAmt = 0;
        decimal TaxableAmount = 0;
        decimal FSEligibleVoidAmt = 0;
        bool IsPK = false;
        decimal _SellPrice;
        bool IsGroup = false;
        bool AddItem = false;
        bool IsBeep = true;
        static decimal Group = 0;
        static decimal UnGroup = 0;
        static bool IsRefundItem = false;

        string LinkedParent = "";

        static int RowNo = 0;
        bool IsCasePrice = false;
        //static bool IsPowerOn = true;

        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        private Scale myScale;

        //SerialPort ComPort = new SerialPort();
        //internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        //internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        //delegate void SetTextCallback(string text);

        TransSuspendService _TransSuspendService = new TransSuspendService();
        OrderScanner_ResultModel _OrderScanner_ResultModel = new OrderScanner_ResultModel();
        ReceiptService _ReceiptService = new ReceiptService();
        public static SqlCeDataAdapter DataAdapter = null;
        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
        List<OrderScanner_ResultModel> Productdata_ = new List<OrderScanner_ResultModel>();
        OrderScannerService _OrderScannerService = new OrderScannerService();
        OrderMasterModel objOrderMasterModel = new OrderMasterModel();
        OrderDetailmasterModel objOrderDetailmasterModel = new OrderDetailmasterModel();
        ProductLedgerMasterModel objProductLedgerMasterModel = new ProductLedgerMasterModel();
        ProductLedgerService _ProductLedgerService = new ProductLedgerService();
        PaymentTransService _PaymentTransService = new PaymentTransService();
        int RowIndex = 0;
        int CurrentIndex = 0;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();

        string previousKey;
        public string OrignalUPCCode = "";

        static bool IsLinkVoid = false;
        OrderScanner_ResultModel VoidModel = new OrderScanner_ResultModel();
        OrderScanner_ResultModel LinkVoidModel = new OrderScanner_ResultModel();
        static OrderScanner_ResultModel TempData_G = new OrderScanner_ResultModel();
        static OrderScanner_ResultModel TempData_U = new OrderScanner_ResultModel();

        #endregion

        #region Events
        //private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        Thread.Sleep(200);
        //        string InputData = ComPort.ReadExisting();
        //        if (InputData != String.Empty)
        //        {
        //            if (InputData.StartsWith("S08"))
        //            {
        //                this.BeginInvoke(new SetTextCallback(SetTextScanner), new object[] { InputData });
        //            }
        //            if (InputData.StartsWith("S11"))
        //            {
        //                this.BeginInvoke(new SetTextCallback(SetTextScale), new object[] { InputData });
        //            }
        //        }
        //        else
        //        {
        //            //lblWeight.Text = "0.00 lb";
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //}

        private void frmOrderScanner_Load(object sender, EventArgs e)
        {
            try
            {
                var deviceCollection = myPosExplorer.GetDevices(DeviceType.Scanner);
                foreach (DeviceInfo deviceInfo in deviceCollection)
                {
                    if (deviceInfo.ServiceObjectName == XMLData.Scanner)
                    {
                        myScanner = (Scanner)myPosExplorer.CreateInstance(deviceInfo);
                        myScanner.Open();
                        //if (myScanner.PowerNotify == PowerNotification.Enabled)
                        //{
                        myScanner.Claim(1000);
                        myScanner.DataEvent += myScanner_DataEvent;
                        myScanner.DeviceEnabled = true;
                        myScanner.DataEventEnabled = true;
                        myScanner.DecodeData = true;
                        //}
                        //else
                        //{
                        //    if (myScanner.PowerNotify == PowerNotification.Disabled)
                        //    {
                        //        ClsCommon.MsgBox("Information", "Power on the scanner and restart ezPOSPro", false);
                        //        //return;
                        //    }
                        //}
                        //myScanner = (Scanner)myPosExplorer.CreateInstance(deviceInfo);
                        //myScanner.Open();
                        //myScanner.Claim(1000);
                        //myScanner.DataEvent += myScanner_DataEvent;
                        //myScanner.DeviceEnabled = true;
                        //myScanner.DataEventEnabled = true;
                        //myScanner.DecodeData = true;
                    }
                }

                var deviceScale = myPosExplorer.GetDevices(DeviceType.Scale);
                foreach (DeviceInfo deviceInfo in deviceScale)
                {
                    if (deviceInfo.ServiceObjectName == XMLData.Scale)
                    {
                        myScale = (Scale)myPosExplorer.CreateInstance(deviceInfo);
                        myScale.Open();
                        //if (myScale.PowerNotify == PowerNotification.Enabled)
                        //{
                        myScale.Claim(1000);
                        myScale.StatusNotify = StatusNotify.Enabled;
                        myScale.DeviceEnabled = true;
                        //}
                        //else
                        //{
                        //    if (myScale.PowerNotify == PowerNotification.Disabled)
                        //    {
                        //        ClsCommon.MsgBox("Information", "Power on the scale and restart ezPOSPro", false);
                        //        return;
                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }

        }

        void myPosExplorer_DeviceAddedEvent(object sender, DeviceChangedEventArgs e)
        {
            if (e.Device.Type == "Scanner")
            {
                myScanner = (Scanner)myPosExplorer.CreateInstance(e.Device);
                myScanner.Open();
                myScanner.Claim(1000);
                myScanner.DataEvent += new DataEventHandler(myScanner_DataEvent);
                myScanner.DeviceEnabled = true;
                myScanner.DataEventEnabled = true;
                myScanner.DecodeData = true;
            }
        }

        void myPosExplorer_DeviceRemovedEvent(object sender, DeviceChangedEventArgs e)
        {
            if (e.Device.Type == "Scanner")
            {
                myScanner.DataEventEnabled = false;
                myScanner.DeviceEnabled = false;
                myScanner.Release();
                myScanner.Close();
            }
        }

        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            ASCIIEncoding myEncoding = new ASCIIEncoding();
            string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
            if (UPCCode.Length > 1)
            {
                if (myScanner.ScanDataType != BarCodeSymbology.Code39)
                    UPCCode = UPCCode.Substring(0, UPCCode.Length - 1);
            }
            txtSearchUPCCode.Text = UPCCode;
            ProductAdd();
            txtSearchUPCCode.Text = "";
            myScanner.DataEventEnabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                lblTime.Text = Functions.GetCurrentDateTime().ToLongTimeString();
                lblDate.Text = Functions.GetCurrentDateTime().ToShortDateString();
                if (LoginInfo.Connections)
                {
                    PicNoInternetLine.Visible = false;
                    PicNoInternet.Visible = false;
                }
                else
                {
                    PicNoInternetLine.Visible = true;
                    PicNoInternet.Visible = true;
                }
                if (myScale.ScaleLiveWeight.ToString().Length > 6)
                {
                    lblWeight.Text = "0.00 lb";
                }
                else
                {
                    lblWeight.Text = (myScale.ScaleLiveWeight.ToString("0.00")) + " lb";
                }
                //if (IsPowerOn == true)
                //{
                //    //GetProductScale();
                //}
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridProducts.RowCount <= 0)
                {
                    Close();
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Your cart must be empty for signout!", false);
                    txtSearchUPCCode.Text = "";
                    txtSearchUPCCode.Focus();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void dataGridProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    RowIndex = Convert.ToInt32(dataGridProducts.Rows[e.RowIndex].Index);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void frmOrderScanner_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //ComPort.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void dataGridProducts_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                previousKey += e.KeyChar.ToString();
                if (e.KeyChar == (char)13)
                {
                    if (previousKey.ToUpper() == "VD\r")
                    {
                        DeviceRemove();
                        FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                        obj.ShowDialog();
                        DeviceAdd();

                        if (obj.Isverified == true)
                        {
                            string Qty = dataGridProducts.Rows[RowIndex].Cells["Qty"].Value.ToString();
                            string SellPrice = dataGridProducts.Rows[RowIndex].Cells["SellPrice"].Value.ToString();
                            string UPCCode = dataGridProducts.Rows[RowIndex].Cells["UPCCode"].Value.ToString();
                            string TaxPercentage = dataGridProducts.Rows[RowIndex].Cells["Tax"].Value.ToString();
                            bool IsForceTax = Functions.GetBoolean(dataGridProducts.Rows[RowIndex].Cells["IsForceTax"].Value.ToString());
                            string FinalPrice = dataGridProducts.Rows[RowIndex].Cells["FinalPrice"].Value.ToString();
                            bool CasePriceApplied = Functions.GetBoolean(dataGridProducts.Rows[RowIndex].Cells["CasePriceApplied"].Value.ToString());
                            bool IsScale = Functions.GetBoolean(dataGridProducts.Rows[RowIndex].Cells["IsScale"].Value.ToString());
                            bool IsVoid = Functions.GetBoolean(dataGridProducts.Rows[RowIndex].Cells["IsVoid"].Value.ToString());
                            if (IsVoid)
                            {
                                ClsCommon.MsgBox("Information", "You can't void the already voided product!", false);
                                previousKey = null;
                                txtSearchUPCCode.Focus();
                            }
                            else
                            {
                                VoidCommand(Qty, UPCCode, SellPrice, TaxPercentage, IsForceTax, FinalPrice, CasePriceApplied, IsScale);
                                previousKey = null;
                                txtSearchUPCCode.Focus();
                            }
                        }
                    }
                    else if (e.KeyChar == (char)13 && previousKey == "\r")
                    {
                        previousKey = null;
                        string UPCCode = dataGridProducts.Rows[RowIndex].Cells["UPCCode"].Value.ToString();
                        if (Functions.GetBoolean(dataGridProducts.Rows[RowIndex].Cells["IsScale"].Value.ToString()) == true)
                        {
                            ClsCommon.MsgBox("Information", "You must add scaled item from the UPCCode field.", false);
                        }
                        else
                        {
                            txtSearchUPCCode.Text = UPCCode;
                        }
                        ProductAdd();
                    }
                    else
                    {
                        txtSearchUPCCode.Text = previousKey;
                        previousKey = null;
                        txtSearchUPCCode.Focus();
                        if (txtSearchUPCCode.Text.ToUpper().Contains("DP"))
                        {
                            if (txtSearchUPCCode.Text.Contains("\r"))
                            {
                                txtSearchUPCCode.Text = txtSearchUPCCode.Text.Replace("\r", "");
                            }
                            AddDepartment();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void dataGridProducts_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Up))
                {
                    moveUp();
                }
                else if (e.KeyCode.Equals(Keys.Down))
                {
                    moveDown();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
            e.Handled = true;
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //GetScalePowerOn();
                if (CurrentIndex == 0)
                {
                    LoginInfo.SyncType = 2;
                    backgroundWorker_DataSync.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnShortCut_Click(object sender, EventArgs e)
        {
            try
            {
                FrmShortcutKey objFrmShortcutKey = new FrmShortcutKey();
                objFrmShortcutKey.ShowDialog();
                txtSearchUPCCode.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchUPCCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    ProductAdd();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchUPCCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Down)
                {
                    if (dataGridProducts.RowCount > 0)
                    {
                        dataGridProducts.Focus();
                        RowIndex = dataGridProducts.Rows.Count - 1;
                        dataGridProducts.Rows[RowIndex].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                txtSearchUPCCode.Focus();
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckConnection())
                {
                    DialogResult result = MessageBox.Show("Do you want to sync data with the server?", "Data Sync", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.OK))
                    {
                        LoginInfo.SyncType = 2;
                        frmDataSync2 _frmDataSync = new frmDataSync2();
                        _frmDataSync.ShowDialog();
                        ClsCommon.MsgBox("Information", "Data synchronization process is done", false);
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Can't connet with B/E server, Please connect with B/E server first.", false);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Data synchronization process is fail, Please try again!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
            txtSearchUPCCode.Focus();
        }

        #endregion

        #region Functions
        public frmOrderScanner()
        {
            InitializeComponent();
            try
            {
                timer1.Start();
                SyncTimer.Start();
                int TimeSeconds = 60000;
                TimeSeconds = TimeSeconds * XMLData.LiveToLocalTime;
                SyncTimer.Interval = TimeSeconds;
                SyncTimer.Start();
                //Local Sync Timer
                //SyncTimer_LocalToLive.Interval = TimeSeconds * XMLData.LocalToLiveTime;
                //SyncTimer_LocalToLive.Start();
                dataLoad();
                myPosExplorer = new PosExplorer(this);
                myPosExplorer.DeviceAddedEvent += myPosExplorer_DeviceAddedEvent;
                myPosExplorer.DeviceRemovedEvent += myPosExplorer_DeviceRemovedEvent;
                //ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                //OpenPort();

                if (CurrentIndex == 0)
                {
                    LoginInfo.SyncType = 2;
                    backgroundWorker_DataSync.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void DeviceRemove()
        {
            try
            {
                myScanner.DataEventEnabled = false;
                myScanner.DeviceEnabled = false;
                myScanner.Release();
                myScanner.Close();

                myScale.DeviceEnabled = false;
                myScale.Release();
                myScale.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void DeviceAdd()
        {
            try
            {
                var deviceCollection = myPosExplorer.GetDevices(DeviceType.Scanner);
                foreach (DeviceInfo deviceInfo in deviceCollection)
                {
                    if (deviceInfo.ServiceObjectName == XMLData.Scanner)
                    {
                        myScanner = (Scanner)myPosExplorer.CreateInstance(deviceInfo);
                        myScanner.Open();
                        //if (myScanner.PowerNotify == PowerNotification.Enabled)
                        //{
                        myScanner.Claim(1000);
                        myScanner.DataEvent += myScanner_DataEvent;
                        myScanner.DeviceEnabled = true;
                        myScanner.DataEventEnabled = true;
                        myScanner.DecodeData = true;
                        //}
                        //else
                        //{
                        //    if (myScanner.PowerNotify == PowerNotification.Disabled)
                        //    {
                        //        ClsCommon.MsgBox("Information", "Power on the scanner", false);
                        //        return;
                        //    }
                        //}
                    }
                }

                var deviceScale = myPosExplorer.GetDevices(DeviceType.Scale);
                foreach (DeviceInfo deviceInfo in deviceScale)
                {
                    if (deviceInfo.ServiceObjectName == XMLData.Scale)
                    {
                        myScale = (Scale)myPosExplorer.CreateInstance(deviceInfo);
                        myScale.Open();
                        myScale.Claim(1000);
                        myScale.StatusNotify = StatusNotify.Enabled;
                        myScale.DeviceEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void ProductAdd()
        {
            try
            {
                if (txtSearchUPCCode.Text != CommonModelCont.EmptyString)
                {
                    #region Refresh Some Common Variable
                    if (LoginInfo.tnfn)
                    {
                        if (txtSearchUPCCode.Text.Trim().ToLower().Contains("1tnfn") || txtSearchUPCCode.Text.Trim().ToLower().Contains("@"))
                            txtSearchUPCCode.Text = txtSearchUPCCode.Text.ToLower().Replace("1tnfn", "").Replace("@", "");
                    }
                    if (LoginInfo.CasePrice)
                    {
                        if (txtSearchUPCCode.Text.Trim().ToLower().Contains("cs"))
                        {
                            txtSearchUPCCode.Text = txtSearchUPCCode.Text.ToLower().Replace("cs", "");
                        }
                    }
                    #endregion

                    #region CL
                    if (txtSearchUPCCode.Text.ToLower().Contains("cl"))
                    {
                        txtSearchUPCCode.Text = "";
                        LoginInfo.tnfn = false;
                        LoginInfo.CasePrice = false;
                    }
                    #endregion

                    #region TC --TaxCarry
                    else if (txtSearchUPCCode.Text.ToLower().Contains("tc"))
                    {
                        txtSearchUPCCode.Text = "";
                        LoginInfo.TaxCarry = true;
                        DeviceRemove();
                        FrmCancelTransaction obj = new FrmCancelTransaction();
                        obj.ShowDialog();
                        DeviceAdd();
                        if (obj.IsCancel == true)
                        {
                            itemCount();
                        }
                    }
                    #endregion

                    #region SO
                    else if (txtSearchUPCCode.Text.ToLower().Contains("so"))
                    {
                        txtSearchUPCCode.Text = "";
                        if (dataGridProducts.RowCount <= 0)
                        {
                            Close();
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "Your cart must be empty for signout!", false);
                            txtSearchUPCCode.Text = "";
                            txtSearchUPCCode.Focus();
                        }
                    }
                    #endregion

                    #region RF
                    else if (txtSearchUPCCode.Text.Trim().ToUpper().StartsWith("RF"))
                    {
                       DeviceRemove();
                        FrmCancelTransaction obj = new FrmCancelTransaction();
                        obj.ShowDialog();
                        DeviceAdd();
                        if (obj.IsCancel == true)
                        {
                            RefundCommand();
                        }
                    }
                    #endregion

                    #region VD
                    else if (txtSearchUPCCode.Text.Trim().ToUpper().StartsWith("VD"))
                    {
                        DeviceRemove();

                        FrmCancelTransaction obj = new FrmCancelTransaction();
                        obj.ShowDialog();

                        DeviceAdd();
                        if (obj.IsCancel == true)
                        {
                            VoidCommand("", "", "", "", false, "", false, false);
                        }
                    }
                    #endregion

                    #region DP
                    else if (txtSearchUPCCode.Text.Trim().ToLower().Contains("dp"))
                    {
                        AddDepartment();
                    }
                    #endregion

                    #region PV
                    else if (txtSearchUPCCode.Text.Trim().ToLower().Contains("pv"))
                    {
                        OpenPV();
                    }
                    #endregion

                    #region MM
                    else if (txtSearchUPCCode.Text.Trim().ToLower().Contains("mm"))
                    {
                        //AddManualWeight();
                        string EnterUPCCode = txtSearchUPCCode.Text;
                        string weight = "";
                        int Count = txtSearchUPCCode.Text.Length;

                        if (txtSearchUPCCode.Text.Trim().ToUpper().Contains("MM"))
                        {
                            string[] Product = txtSearchUPCCode.Text.ToUpper().Split('M');
                            weight = Product[0];
                            lblWeight.Text = weight.Insert(weight.Length - 2, ".") + " lb";
                            Product[1] = Product[2];
                            txtSearchUPCCode.Text = Product[1];
                            ProductAdd();
                        }
                    }
                    #endregion

                    #region NS
                    else if (txtSearchUPCCode.Text.Trim().ToLower().StartsWith("ns"))
                    {
                        txtSearchUPCCode.Text = "";
                        if (dataGridProducts.RowCount <= 0)
                        {
                            LoginInfo.tnfn = false;

                           DeviceRemove();

                            FrmCurrentUserPwd objFrmCurrentUserPwd = new FrmCurrentUserPwd();
                            objFrmCurrentUserPwd.ShowDialog();

                            DeviceAdd();

                            if (objFrmCurrentUserPwd.Isverified == true)
                            {
                                if (CheckMyPrinter())
                                {
                                    Printer printer = new Printer(XMLData.PrinterName, PrinterType.Epson);
                                    printer.OpenDrawer();
                                    printer.PrintDocument();
                                    txtSearchUPCCode.Text = "";
                                }
                            }
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "Your cart must be empty for no sale command!", false);
                            txtSearchUPCCode.Focus();
                        }
                    }
                    #endregion

                    #region RP
                    else if (txtSearchUPCCode.Text.Trim().ToLower().StartsWith("rp"))
                    {
                        txtSearchUPCCode.Text = "";
                        if (dataGridProducts.RowCount <= 0)
                        {
                            DataTable dt = new DataTable();
                            if (LoginInfo.Connections)
                            {
                                dt = ReceiptDetailSP(LoginInfo.LastOrderID, true);
                                MultiPaymentInfo.lstPaymentTransMasterModel = _PaymentTransService.GetPaymentTrans(Functions.GetLong(dt.Rows[0]["OrderID"].ToString()));

                                if (Functions.GetBoolean(dt.Rows[0]["IsCancel"].ToString()) == true)
                                {
                                    ClsCommon.MsgBox("Information", "Can't reprint the canceled order.!", false);
                                    txtSearchUPCCode.Text = "";
                                    return;
                                }
                            }
                            else
                            {
                                #region OrderReceipt

                                string query = "SELECT MAX(OrderID) as OrderID  FROM tbl_OrderMaster where counterIP = '" + LoginInfo.CounterIP + "'";

                                SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                                da.Fill(dt);
                                long orderid = Functions.GetLong(dt.Rows[0]["OrderID"].ToString());
                                query = "SELECT OM.IsCancel,OM.OrderID,OM.TotalAmount,OM.TaxAmount,OM.GrossAmount,OM.PaymentMethodID,OM.OrdNo"
                                    + " ,OM.CashAmount,OM.CheckAmount,OM.CreditCardAmount,OM.FoodStampAmount,OM.RefundAmount,OM.Balance,OM.ChangeAmount,OM.TaxableAmount"
                                    + " ,OD.UPCCode,OD.ProductName,OD.Quantity,OD.SellPrice,OD.Discount,OD.finalPrice,OD.IsScale,OD.IsFoodStamp,OD.IsTax"
                                    + " ,OD.GroupQty,OD.GroupPrice,OD.CaseQty,OD.CasePrice,OM.CouponCode,OM.CouponDiscAmt"
                                    + " ,OD.DiscountApplyed,OM.TaxExempted ,OD.CasePriceApplied,SM.StoreName,SM.Address AS SMAddress,SM.Address2 AS SAddress2 "
                                    + " ,SM.ZipCode AS SZipCode,SM.Phone AS SPhone,SM.Fax AS SFax,EM.FirstName "
                                    + " FROM tbl_OrderMaster AS OM "
                                    + " INNER JOIN tbl_OrderDetail AS OD ON OM.OrderID = OD.OrderID "
                                    + " INNER JOIN tbl_StoreMaster AS SM ON OM.StoreID = SM.StoreID "
                                    + " INNER JOIN tbl_EmployeeMaster AS EM ON EM.StoreID = SM.StoreID "
                                    + " WHERE OM.OrderID = " + orderid
                                    + " AND EM.EmployeeID = " + LoginInfo.UserId;
                                da = new SqlCeDataAdapter(query, conn);
                                dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    if (Functions.GetBoolean(dt.Rows[0]["IsCancel"].ToString()) == true)
                                    {
                                        ClsCommon.MsgBox("Information", "Can't reprint the canceled order.!", false);
                                        txtSearchUPCCode.Text = "";
                                        return;
                                    }
                                }
                                if (conn.State == ConnectionState.Open)
                                {
                                    conn.Close();
                                }

                                query = "SELECT * FROM tbl_PaymentTrans WHERE OrderID = " + orderid;
                                da = new SqlCeDataAdapter(query, conn);
                                DataTable Dt_Payment = new DataTable();
                                da.Fill(Dt_Payment);

                                for (int i = 0; i < Dt_Payment.Rows.Count; i++)
                                {
                                    PaymentTransMasterModel lstPaymentTransMasterModel = new PaymentTransMasterModel();
                                    lstPaymentTransMasterModel.OrderID = Functions.GetLong(Dt_Payment.Rows[i]["OrderID"].ToString());
                                    lstPaymentTransMasterModel.PaymentMethodID = Functions.GetLong(Dt_Payment.Rows[i]["PaymentMethodID"].ToString());
                                    lstPaymentTransMasterModel.CashAmount = Functions.GetDecimal(Dt_Payment.Rows[i]["CashAmount"].ToString());
                                    lstPaymentTransMasterModel.CreditCardAmount = Functions.GetDecimal(Dt_Payment.Rows[i]["CreditCardAmount"].ToString());
                                    lstPaymentTransMasterModel.CheckAmount = Functions.GetDecimal(Dt_Payment.Rows[i]["CheckAmount"].ToString());
                                    lstPaymentTransMasterModel.Balance = Functions.GetDecimal(Dt_Payment.Rows[i]["Balance"].ToString());
                                    lstPaymentTransMasterModel.ChangeAmount = Functions.GetDecimal(Dt_Payment.Rows[i]["ChangeAmount"].ToString());
                                    lstPaymentTransMasterModel.FoodStampAmount = Functions.GetDecimal(Dt_Payment.Rows[i]["FoodStampAmount"].ToString());
                                    lstPaymentTransMasterModel.StoreID = Functions.GetLong(Dt_Payment.Rows[i]["StoreID"].ToString());
                                    lstPaymentTransMasterModel.CreatedBy = Functions.GetLong(Dt_Payment.Rows[i]["CreatedBy"].ToString());
                                    lstPaymentTransMasterModel.CreatedDate = Convert.ToDateTime(Dt_Payment.Rows[i]["CreatedDate"].ToString());
                                    lstPaymentTransMasterModel.CardNumber = Dt_Payment.Rows[i]["CardNumber"].ToString();
                                    MultiPaymentInfo.lstPaymentTransMasterModel.Add(lstPaymentTransMasterModel);
                                }
                                #endregion
                            }
                            if (dt.Rows.Count > 0)
                            {
                                OrderInfo.PaymentType = Convert.ToInt32(dt.Rows[0]["PaymentMethodID"].ToString());
                                if (Functions.GetDecimal(dt.Rows[0]["TaxableAmount"].ToString()) > 0)
                                {
                                    OrderInfo.IsFSVoidtax = false;
                                }
                                else
                                {
                                    OrderInfo.IsFSVoidtax = true;
                                }
                                OrderInfo.FSTotal = Functions.GetDecimal(dt.Rows[0]["FoodStampAmount"].ToString());
                                if (OrderInfo.FSTotal > 0)
                                {
                                    OrderInfo.IsFSClicked = true;
                                }
                            }
                            if (CheckMyPrinter())
                            {
                                Printer printer = new Printer(XMLData.PrinterName, PrinterType.Epson);
                                printer.LowPaper();
                                string status = printer.GetStatus();
                                if (status == "1024" || status == "0" || ClsCommon.CheckPrinterRoll == false)
                                {
                                    //printer.Balance = Functions.GetDecimal(dt.Rows[0]["Balance"].ToString());
                                    printer.RePrint = true;
                                    if (OrderInfo.IsFSVoidtax == false)
                                        printer.TaxableAmount = Functions.GetDecimal(dt.Rows[0]["TaxableAmount"].ToString());
                                    printer.dt = dt;
                                    printer.ReceiptPrint();
                                    printer.PartialPaperCut();
                                    printer.PrintDocument();
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", "Something went wrong with printer. Please check paper roll.", false);
                                }
                            }
                            OrderInfo.CashAmt = 0;
                            OrderInfo.Change = 0;
                            OrderInfo.CheckAmt = 0;
                            OrderInfo.CreditAmt = 0;
                            OrderInfo.PaymentType = 0;
                            OrderInfo.FSTotal = 0;
                            OrderInfo.IsFSClicked = false;
                            MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "Your cart must be empty for Reprint!", false);
                        }
                        txtSearchUPCCode.Focus();
                    }
                    #endregion

                    #region FNTL
                    else if (txtSearchUPCCode.Text.Trim().ToLower().StartsWith("fntl"))
                    {
                        txtSearchUPCCode.Text = "";
                        if (dataGridProducts.Rows.Count > 0)
                        {
                            if (FSEligibleAmt > 0)
                            {
                               DeviceRemove();
                                OrderInfo.IsFSClicked = true;
                                FrmFoodStamp objFoodStamp = new FrmFoodStamp();
                                objFoodStamp.txtFSEligible.Text = (FSEligibleAmt - OrderInfo.FSTotal).ToString();
                                objFoodStamp.txtFSAmount.Focus();
                                objFoodStamp.ShowDialog();
                                DeviceAdd();
                                objFoodStamp.txtFSAmount.Text = "";
                                objFoodStamp.txtFSEligible.Text = "";
                                lblFSTotal.Text = Functions.GetDisplayAmount(OrderInfo.FSTotal.ToString());
                                decimal voidTaxAmt = 0;

                                decimal TotalTax = 0;

                                decimal? TotalFSAmt = OrderInfo.FSTotal;
                                for (int row = 0; row < dataGridProducts.Rows.Count; row++)
                                {
                                    decimal? LineFsAmt = 0;
                                    if (TotalFSAmt > 0)
                                    {
                                        if (Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString()))
                                        {
                                            if (TotalFSAmt > Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                            {
                                                LineFsAmt = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                            }
                                            else
                                            {
                                                LineFsAmt = TotalFSAmt;
                                            }
                                            TotalFSAmt -= LineFsAmt;
                                        }
                                        else
                                        {
                                            LineFsAmt = 0;
                                        }
                                    }
                                    dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FoodStampTotal].Value = LineFsAmt;
                                }

                                if (OrderInfo.FSTotal == FSEligibleAmt)
                                {
                                    TaxableAmount = 0;
                                    FSEligibleVoidAmt = 0;
                                    for (int row = 0; row < dataGridProducts.Rows.Count; row++)
                                    {
                                        if (Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString()) == true
                                            && Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                                        {
                                            if (Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FoodStampTotal].Value.ToString()) < Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                            {
                                                decimal tempVal = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FoodStampTotal].Value.ToString())
                                                    - Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));

                                                decimal tax = tempVal * Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.Tax].Value.ToString());
                                                tax = tax / 100;
                                                tax = Functions.GetDecimal(tax.ToString("0.00"));
                                                if (tax < 0)
                                                {
                                                    FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                                }
                                                else
                                                {
                                                    FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString()) - tax;
                                                }
                                                TotalTax += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                            }
                                            else
                                            {
                                                FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                                TotalTax += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                            }
                                        }
                                        else if (Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                                        {
                                            voidTaxAmt += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                            TaxableAmount += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                        }
                                    }
                                    if (voidTaxAmt != 0 && OrderInfo.FSTotal != 0)
                                    {
                                        if (OrderInfo.FSTotal < Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        {
                                            FSEligibleVoidAmt = Functions.GetDecimal(FSEligibleVoidAmt.ToString("0.00"));
                                            lblTaxAmount.Text = Functions.GetDisplayAmount(voidTaxAmt.ToString());
                                            lblFinalAmount.Text = (Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) - voidTaxAmt).ToString();
                                        }
                                    }
                                    else if (FSEligibleVoidAmt > 0)
                                    {
                                        FSEligibleVoidAmt = Functions.GetDecimal(FSEligibleVoidAmt.ToString("0.00"));
                                        lblTaxAmount.Text = (TotalTax - FSEligibleVoidAmt).ToString();
                                        lblFinalAmount.Text = (Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) - FSEligibleVoidAmt).ToString();
                                    }
                                }

                                else if (OrderInfo.FSTotal == Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                {
                                    if (FSEligibleVoidAmt > 0)
                                    {
                                        FSEligibleVoidAmt = Functions.GetDecimal(FSEligibleVoidAmt.ToString("0.00"));
                                        lblTaxAmount.Text = (Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) - FSEligibleVoidAmt).ToString();
                                        lblFinalAmount.Text = (Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) - FSEligibleVoidAmt).ToString();
                                        TaxableAmount = (Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)));
                                    }
                                }

                                else
                                {
                                    TaxableAmount = 0;
                                    FSEligibleVoidAmt = 0;

                                    for (int row = 0; row < dataGridProducts.Rows.Count; row++)
                                    {
                                        if (Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString()) == true
                                            && Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                                        {
                                            if (Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FoodStampTotal].Value.ToString()) < Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                            {
                                                decimal tempVal = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty))
                                                    - Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FoodStampTotal].Value.ToString());

                                                decimal tax = tempVal * Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.Tax].Value.ToString());
                                                tax = tax / 100;
                                                tax = Convert.ToDecimal(tax.ToString("0.00"));
                                                FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString()) - tax;
                                                TotalTax += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                            }
                                            else
                                            {
                                                FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                                TotalTax += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                            }
                                        }
                                        else if (Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                                        {
                                            voidTaxAmt += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                            TaxableAmount += Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                        }
                                    }

                                    if (voidTaxAmt != 0 && OrderInfo.FSTotal != 0)
                                    {
                                        if (OrderInfo.FSTotal < Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        {
                                            lblTaxAmount.Text = Functions.GetDisplayAmount(voidTaxAmt.ToString());
                                        }
                                    }
                                    else if (FSEligibleVoidAmt > 0)
                                    {
                                        FSEligibleVoidAmt = Functions.GetDecimal(FSEligibleVoidAmt.ToString("0.00"));
                                        lblTaxAmount.Text = (TotalTax - FSEligibleVoidAmt).ToString();
                                        lblFinalAmount.Text = (Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) - FSEligibleVoidAmt).ToString();
                                    }
                                }

                                lblFinalAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(lblSubTotal.Text.Replace(CommonModelCont.AddDollorSign, string.Empty)) + Functions.GetDecimal(lblTaxAmount.Text.Replace(CommonModelCont.AddDollorSign, string.Empty))).ToString());

                                if (OrderInfo.FSTotal > FSEligibleAmt)
                                {
                                    lblFinalAmount.Text = (Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) - FSEligibleAmt).ToString();
                                }
                                else
                                {
                                    lblFinalAmount.Text = (Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) - OrderInfo.FSTotal).ToString();
                                }

                                OrderInfo.FSEligibleVoidAmt = FSEligibleVoidAmt;

                                if (OrderInfo.FSTotal >= FSEligibleAmt && Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) == 0)
                                {
                                    OrderInfo.FSEligibleVoidAmt = FSEligibleVoidAmt;
                                    OrderInfo.IsFSVoidtax = true;
                                    OrderInfo.PaymentType = 4;
                                    GenerateOrder();
                                    lblWeight.Text = "0.00 lb";
                                    lblFSTotal.Text = Functions.GetDisplayAmount("0.00");
                                    OrderInfo.FSTotal = 0;
                                    FSEligibleVoidAmt = 0;
                                    OrderInfo.FSEligibleVoidAmt = 0;
                                    OrderInfo.IsFSClicked = false;
                                    OrderInfo.IsFSVoidtax = false;
                                    TaxableAmount = 0;
                                    Balance = 0;
                                    OrderInfo.CashAmt = 0;
                                    OrderInfo.Change = 0;
                                    OrderInfo.IsOrder = false;
                                    OrderInfo.remainingFSAmt = 0;
                                    OrderInfo.CheckAmt = 0;
                                    OrderInfo.CreditAmt = 0;
                                    CouponInfo.CouponCode = "";
                                    CouponInfo.isCoupon = false;
                                    CouponInfo.Discount = 0;
                                    CouponInfo.DiscAmt = 0;
                                }

                                txtSearchUPCCode.Focus();
                            }
                            else
                            {
                                txtSearchUPCCode.Focus();
                                ClsCommon.MsgBox("Information", "No any food stamp eligible product in cart.!!", false);
                            }
                        }
                        else
                        {
                            txtSearchUPCCode.Focus();
                            ClsCommon.MsgBox("Information", "Your cart is empty!!!", false);
                        }
                    }
                    #endregion

                    #region TL
                    else if (txtSearchUPCCode.Text.Trim().ToLower().StartsWith("tl"))
                    {
                        txtSearchUPCCode.Text = "";
                        if (dataGridProducts.Rows.Count > 0)
                        {
                            string TotalAmount = "";
                           DeviceRemove();
                            FrmTotal objTotal = new FrmTotal();

                            TotalAmount = objTotal.txtTotalAmt.Text = Functions.GetDisplayAmount(((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)));
                            objTotal.txtRemainingAmount.Text = Functions.GetDisplayAmount(((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)));
                            Balance = Functions.GetDecimal(objTotal.txtTotalAmt.Text.Replace("$", ""));
                            if (Functions.GetDecimal(objTotal.txtTotalAmt.Text.Replace("$", "")) < 0)
                            {
                                objTotal.lblReceiveAmt.Text = "Refund";
                                objTotal.lblTotalReceivedAmt.Text = "Total Refunded";
                            }
                            objTotal.ShowDialog();
                            DeviceAdd();
                            if (OrderInfo.IsOrder == true)
                            {
                                GenerateOrder();
                                if (OrderInfo.PaymentType == 1 || OrderInfo.PaymentType == 3)
                                {
                                    if (OrderInfo.Change > 0)
                                    {
                                        FrnOrderSuccess objFrnOrderSuccess = new FrnOrderSuccess();
                                        objFrnOrderSuccess.lblTotal.Text = TotalAmount;
                                        objFrnOrderSuccess.lblChange.Text = Functions.GetDisplayAmount(OrderInfo.Change.ToString());
                                        if (OrderInfo.PaymentType == 1)
                                            objFrnOrderSuccess.lblReceive.Text = Functions.GetDisplayAmount(OrderInfo.CashAmt.ToString());
                                        if (OrderInfo.PaymentType == 3)
                                            objFrnOrderSuccess.lblReceive.Text = Functions.GetDisplayAmount(OrderInfo.CheckAmt.ToString());
                                        objFrnOrderSuccess.ShowDialog();
                                    }
                                }
                            }
                            Balance = 0;
                            OrderInfo.CashAmt = 0;
                            OrderInfo.Change = 0;
                            OrderInfo.IsOrder = false;
                            OrderInfo.FSTotal = 0;
                            OrderInfo.remainingFSAmt = 0;
                            OrderInfo.IsFSClicked = false;
                            OrderInfo.CheckAmt = 0;
                            OrderInfo.CreditAmt = 0;
                            lblWeight.Text = "0.00 lb";
                            CouponInfo.CouponCode = "";
                            CouponInfo.isCoupon = false;
                            CouponInfo.Discount = 0;
                            CouponInfo.DiscAmt = 0;
                            lblFSTotal.Text = Functions.GetDisplayAmount("0.00");
                            lblFinalAmount.Text = Functions.GetDisplayAmount((Functions.GetDecimal(lblSubTotal.Text.Replace(CommonModelCont.AddDollorSign, string.Empty)) + Functions.GetDecimal(lblTaxAmount.Text.Replace(CommonModelCont.AddDollorSign, string.Empty))).ToString());
                            txtSearchUPCCode.Focus();
                            OrderInfo.FSEligibleVoidAmt = 0;
                            FSEligibleVoidAmt = 0;
                            MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
                        }
                        else
                        {
                            txtSearchUPCCode.Focus();
                            ClsCommon.MsgBox("Information", "Your cart is empty!!!", false);
                        }
                    }
                    #endregion

                    #region CN
                    else if (txtSearchUPCCode.Text.Trim().ToLower().StartsWith("cn"))
                    {
                        txtSearchUPCCode.Text = "";
                        LoginInfo.tnfn = false;
                        LoginInfo.CasePrice = false;

                       DeviceRemove();

                        FrmCancelTransaction objCancel = new FrmCancelTransaction();
                        objCancel.ShowDialog();

                        DeviceAdd();

                        if (objCancel.IsCancel == true)
                        {
                            ClsCommon.ResetStaticValues();
                            IsPK = false;
                            if (LoginInfo.Connections)
                            {
                                #region InsertOrder
                                objOrderMasterModel = new OrderMasterModel();
                                objOrderMasterModel.CardNumber = CommonModelCont.EmptyString;
                                objOrderMasterModel.CustomerID = 0;
                                objOrderMasterModel.PaymentMethodID = OrderInfo.PaymentType;
                                objOrderMasterModel.StoreID = LoginInfo.StoreID;
                                objOrderMasterModel.Status = AlertMessages.ConfirmedStatus;
                                objOrderMasterModel.TotalAmount = Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderMasterModel.GrossAmount = Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderMasterModel.TaxAmount = Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderMasterModel.CashAmount = OrderInfo.CashAmt;
                                objOrderMasterModel.CheckAmount = OrderInfo.CheckAmt;
                                objOrderMasterModel.CreditCardAmount = OrderInfo.CreditAmt;
                                objOrderMasterModel.RefundAmount = 0;
                                objOrderMasterModel.ChangeAmount = Functions.GetDecimal(OrderInfo.Change.ToString());
                                objOrderMasterModel.Balance = Balance;
                                objOrderMasterModel.FoodStampAmount = OrderInfo.FSTotal;
                                objOrderMasterModel.CreatedDate = DateTime.Now;
                                objOrderMasterModel.CreatedBy = LoginInfo.UserId;
                                objOrderMasterModel.CounterIP = LoginInfo.CounterIP;
                                objOrderMasterModel.IsCancel = true;
                                objOrderMasterModel.TaxExempted = OrderInfo.FSEligibleVoidAmt;
                                objOrderMasterModel.OrdNo = GetORDNO();

                                objOrderMasterModel.CouponCode = CouponInfo.CouponCode;
                                objOrderMasterModel.CouponDiscAmt = CouponInfo.Discount;

                                var addOrder = _OrderScannerService.AddOrder(objOrderMasterModel, 1);
                                LoginInfo.LastOrderID = addOrder.OrderID;
                                #endregion

                                #region InsertOrderDetail
                                decimal? TotalFSAmt = OrderInfo.FSTotal;
                                objOrderDetailmasterModel = new OrderDetailmasterModel();
                                for (int row = 0; row < dataGridProducts.Rows.Count; row++)
                                {
                                    string regex = "(\\(.*\\))";
                                    objOrderDetailmasterModel.OrderID = addOrder.OrderID;
                                    objOrderDetailmasterModel.ProductID = Functions.GetLong(dataGridProducts.Rows[row].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                                    objOrderDetailmasterModel.ProductName = Regex.Replace(dataGridProducts.Rows[row].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").Replace(" - Group Discount", "");
                                    objOrderDetailmasterModel.UPCCode = dataGridProducts.Rows[row].Cells[ProductMasterModelCont.UPCCode].Value.ToString();
                                    objOrderDetailmasterModel.Quantity = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                                    objOrderDetailmasterModel.DepartmentID = Functions.GetLong(dataGridProducts.Rows[row].Cells[DepartmentMasterModelCont.DepartmentID].Value.ToString());
                                    objOrderDetailmasterModel.SectionID = Functions.GetLong(dataGridProducts.Rows[row].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                                    //objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));

                                    string sale = dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty);
                                    if (sale.Contains("\n"))
                                    {
                                        string[] split = sale.Split(new Char[] { '\n' });
                                        objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(split[1]);
                                    }
                                    else
                                    {
                                        objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    }

                                    objOrderDetailmasterModel.Discount = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.Discount].Value.ToString());
                                    objOrderDetailmasterModel.finalPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));

                                    objOrderDetailmasterModel.GroupPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.GroupPrice].Value.ToString());
                                    objOrderDetailmasterModel.GroupQty = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.GroupQty].Value.ToString());
                                    objOrderDetailmasterModel.CasePrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.CasePrice].Value.ToString());
                                    objOrderDetailmasterModel.CaseQty = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.CaseQty].Value.ToString());

                                    objOrderDetailmasterModel.StoreID = LoginInfo.StoreID;
                                    objOrderDetailmasterModel.CreatedBy = LoginInfo.UserId;
                                    objOrderDetailmasterModel.CreatedDate = DateTime.Now;
                                    objOrderDetailmasterModel.IsScale = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsScale].Value.ToString());
                                    objOrderDetailmasterModel.IsFoodStamp = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString());

                                    decimal? LineFsAmt = 0;
                                    if (TotalFSAmt > 0)
                                    {
                                        if (Functions.GetBoolean(objOrderDetailmasterModel.IsFoodStamp.ToString()))
                                        {
                                            if (TotalFSAmt > objOrderDetailmasterModel.finalPrice)
                                            {
                                                LineFsAmt = objOrderDetailmasterModel.finalPrice;
                                            }
                                            else
                                            {
                                                LineFsAmt = TotalFSAmt;
                                            }
                                            TotalFSAmt -= LineFsAmt;
                                        }
                                        else
                                        {
                                            LineFsAmt = 0;
                                        }
                                    }

                                    objOrderDetailmasterModel.FoodStampTotal = LineFsAmt;
                                    objOrderDetailmasterModel.IsTax = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString());
                                    objOrderDetailmasterModel.DiscountApplyed = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.DiscountApplyed].Value.ToString());
                                    objOrderDetailmasterModel.TaxAmount = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                    objOrderDetailmasterModel.IsRefund = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsRefund].Value.ToString());
                                    objOrderDetailmasterModel.IsForceTax = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsForceTax].Value.ToString());
                                    objOrderDetailmasterModel.IsCancel = true;
                                    objOrderDetailmasterModel.CasePriceApplied = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.CasePriceApplied].Value.ToString());
                                    var addOrderDetail = _OrderScannerService.AddOrderDetail(objOrderDetailmasterModel, 1);
                                }
                                #endregion
                            }
                            else
                            {
                                //ClsCommon.MsgBox("Information","Local");
                                DataAdapter = new SqlCeDataAdapter();
                                SqlCeCommand cmd = conn.CreateCommand();
                                cmd = conn.CreateCommand();
                                if (conn.State == ConnectionState.Closed)
                                {
                                    conn.Open();
                                }

                                #region Order Master

                                objOrderMasterModel = new OrderMasterModel();
                                objOrderMasterModel.CardNumber = CommonModelCont.EmptyString;
                                objOrderMasterModel.CustomerID = 0;
                                objOrderMasterModel.PaymentMethodID = OrderInfo.PaymentType;
                                objOrderMasterModel.StoreID = LoginInfo.StoreID;
                                objOrderMasterModel.Status = AlertMessages.ConfirmedStatus;
                                //objOrderMasterModel.DiscountAmount = Functions.GetDecimal((lblDiscountAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderMasterModel.TotalAmount = Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderMasterModel.GrossAmount = Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderMasterModel.TaxAmount = Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderMasterModel.CashAmount = OrderInfo.CashAmt;
                                objOrderMasterModel.CheckAmount = OrderInfo.CheckAmt;
                                objOrderMasterModel.CreditCardAmount = OrderInfo.CreditAmt;
                                objOrderMasterModel.RefundAmount = 0;
                                objOrderMasterModel.ChangeAmount = Functions.GetDecimal(OrderInfo.Change.ToString());
                                objOrderMasterModel.Balance = Balance;
                                objOrderMasterModel.FoodStampAmount = OrderInfo.FSTotal;
                                objOrderMasterModel.CreatedDate = DateTime.Now;
                                objOrderMasterModel.CreatedBy = LoginInfo.UserId;
                                objOrderMasterModel.CounterIP = LoginInfo.CounterIP;
                                objOrderMasterModel.IsCancel = true;
                                objOrderMasterModel.OrdNo = "L" + GetORDNO();

                                cmd.CommandText = "INSERT INTO tbl_OrderMaster(CustomerID,PaymentMethodID,StoreID,TotalAmount,TaxAmount,GrossAmount,CashAmount,CheckAmount,CreditCardAmount,RefundAmount,ChangeAmount,Balance,FoodStampAmount,CardNumber,Status,CreatedDate,CreatedBy,CounterIP,OrdNo,IsCancel) " +
                                                  "VALUES(@CustomerID,@PaymentMethodID,@StoreID,@TotalAmount,@TaxAmount,@GrossAmount,@CashAmount,@CheckAmount,@CreditCardAmount,@RefundAmount,@ChangeAmount,@Balance,@FoodStampAmount,@CardNumber,@Status,@CreatedDate,@CreatedBy,@CounterIP,@OrdNo,@IsCancel)";
                                #region Parameters
                                if (objOrderMasterModel.CustomerID != null)
                                {
                                    cmd.Parameters.AddWithValue("@CustomerID", objOrderMasterModel.CustomerID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CustomerID", DBNull.Value);
                                }
                                if (objOrderMasterModel.PaymentMethodID != null)
                                {
                                    cmd.Parameters.AddWithValue("@PaymentMethodID", objOrderMasterModel.PaymentMethodID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@PaymentMethodID", DBNull.Value);
                                }
                                if (objOrderMasterModel.StoreID != null)
                                {
                                    cmd.Parameters.AddWithValue("@StoreID", objOrderMasterModel.StoreID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                                }
                                if (objOrderMasterModel.TotalAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@TotalAmount", objOrderMasterModel.TotalAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TotalAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.TaxAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@TaxAmount", objOrderMasterModel.TaxAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TaxAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.GrossAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@GrossAmount", objOrderMasterModel.GrossAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@GrossAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.CashAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@CashAmount", objOrderMasterModel.CashAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CashAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.CheckAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@CheckAmount", objOrderMasterModel.CheckAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CheckAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.CreditCardAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreditCardAmount", objOrderMasterModel.CreditCardAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreditCardAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.RefundAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@RefundAmount", objOrderMasterModel.RefundAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@RefundAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.ChangeAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@ChangeAmount", objOrderMasterModel.ChangeAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ChangeAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.Balance != null)
                                {
                                    cmd.Parameters.AddWithValue("@Balance", objOrderMasterModel.Balance);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Balance", DBNull.Value);
                                }
                                if (objOrderMasterModel.FoodStampAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@FoodStampAmount", objOrderMasterModel.FoodStampAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FoodStampAmount", DBNull.Value);
                                }
                                if (objOrderMasterModel.CardNumber != null)
                                {
                                    cmd.Parameters.AddWithValue("@CardNumber", objOrderMasterModel.CardNumber);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CardNumber", DBNull.Value);
                                }
                                if (objOrderMasterModel.Status != null)
                                {
                                    cmd.Parameters.AddWithValue("@Status", objOrderMasterModel.Status);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                                }
                                if (objOrderMasterModel.CreatedDate != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreatedDate", objOrderMasterModel.CreatedDate);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                                }
                                if (objOrderMasterModel.CreatedBy != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreatedBy", objOrderMasterModel.CreatedBy);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                                }
                                if (objOrderMasterModel.CounterIP != null)
                                {
                                    cmd.Parameters.AddWithValue("@CounterIP", objOrderMasterModel.CounterIP);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CounterIP", DBNull.Value);
                                }
                                if (objOrderMasterModel.OrdNo != null)
                                {
                                    cmd.Parameters.AddWithValue("@OrdNo", objOrderMasterModel.OrdNo);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OrdNo", DBNull.Value);
                                }
                                if (objOrderMasterModel.IsCancel != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsCancel", objOrderMasterModel.IsCancel);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsCancel", DBNull.Value);
                                }
                                #endregion

                                cmd.ExecuteScalar();
                                cmd.CommandText = "Select @@Identity";
                                long OrderID = Convert.ToInt32(cmd.ExecuteScalar());
                                LoginInfo.LastOrderID = OrderID;

                                #endregion

                                #region Order Detail
                                objOrderDetailmasterModel = new OrderDetailmasterModel();
                                for (int i = 0; i < dataGridProducts.Rows.Count; i++)
                                {
                                    string regex = "(\\(.*\\))";
                                    cmd = conn.CreateCommand();
                                    objOrderDetailmasterModel.OrderID = OrderID;
                                    objOrderDetailmasterModel.ProductID = Functions.GetLong(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                                    objOrderDetailmasterModel.ProductName = Regex.Replace(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").Replace(" - Group Discount", "");
                                    objOrderDetailmasterModel.UPCCode = dataGridProducts.Rows[i].Cells[ProductMasterModelCont.UPCCode].Value.ToString();
                                    objOrderDetailmasterModel.Quantity = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                                    objOrderDetailmasterModel.DepartmentID = Functions.GetLong(dataGridProducts.Rows[i].Cells[DepartmentMasterModelCont.DepartmentID].Value.ToString());
                                    objOrderDetailmasterModel.SectionID = Functions.GetLong(dataGridProducts.Rows[i].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                                    //objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    string sale = dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty);
                                    if (sale.Contains("\n"))
                                    {
                                        string[] split = sale.Split(new Char[] { '\n' });
                                        objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(split[1]);
                                    }
                                    else
                                    {
                                        objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    }

                                    objOrderDetailmasterModel.Discount = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Discount].Value.ToString());
                                    objOrderDetailmasterModel.finalPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));

                                    objOrderDetailmasterModel.GroupPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.GroupPrice].Value.ToString());
                                    objOrderDetailmasterModel.GroupQty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.GroupQty].Value.ToString());
                                    objOrderDetailmasterModel.CasePrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CasePrice].Value.ToString());
                                    objOrderDetailmasterModel.CaseQty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CaseQty].Value.ToString());

                                    objOrderDetailmasterModel.StoreID = LoginInfo.StoreID;
                                    objOrderDetailmasterModel.CreatedBy = LoginInfo.UserId;
                                    objOrderDetailmasterModel.CreatedDate = DateTime.Now;
                                    objOrderDetailmasterModel.IsScale = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsScale].Value.ToString());
                                    objOrderDetailmasterModel.IsFoodStamp = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString());
                                    objOrderDetailmasterModel.IsTax = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString());
                                    objOrderDetailmasterModel.FoodStampTotal = OrderInfo.FSTotal;
                                    objOrderDetailmasterModel.DiscountApplyed = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.DiscountApplyed].Value.ToString());
                                    objOrderDetailmasterModel.TaxAmount = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                    objOrderDetailmasterModel.IsRefund = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsRefund].Value.ToString());
                                    objOrderDetailmasterModel.IsCancel = true;

                                    cmd.CommandText = "INSERT INTO tbl_OrderDetail(OrderID,ProductID,UPCCode,ProductName,Quantity,DepartmentID,SectionID,SellPrice,Discount,finalPrice,StoreID,CreatedBy,CreatedDate,IsScale,IsFoodStamp,IsTax,FoodStampTotal,DiscountApplyed,TaxAmount,IsRefund,IsCancel,GroupQty,GroupPrice,CaseQty,CasePrice) " +
                                                     "VALUES(@OrderID,@ProductID,@UPCCode,@ProductName,@Quantity,@DepartmentID,@SectionID,@SellPrice,@Discount,@finalPrice,@StoreID,@CreatedBy,@CreatedDate,@IsScale,@IsFoodStamp,@IsTax,@FoodStampTotal,@DiscountApplyed,@TaxAmount,@IsRefund,@IsCancel,@GroupQty,@GroupPrice,@CaseQty,@CasePrice)";
                                    #region Parameters
                                    if (objOrderDetailmasterModel.OrderID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@OrderID", objOrderDetailmasterModel.OrderID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@OrderID", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.ProductID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@ProductID", objOrderDetailmasterModel.ProductID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.StoreID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@StoreID", objOrderDetailmasterModel.StoreID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.UPCCode != null)
                                    {
                                        cmd.Parameters.AddWithValue("@UPCCode", objOrderDetailmasterModel.UPCCode);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@UPCCode", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.ProductName != null)
                                    {
                                        cmd.Parameters.AddWithValue("@ProductName", objOrderDetailmasterModel.ProductName);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@ProductName", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.Quantity != null)
                                    {
                                        cmd.Parameters.AddWithValue("@Quantity", objOrderDetailmasterModel.Quantity);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Quantity", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.DepartmentID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@DepartmentID", objOrderDetailmasterModel.DepartmentID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.SectionID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@SectionID", objOrderDetailmasterModel.SectionID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.SellPrice != null)
                                    {
                                        cmd.Parameters.AddWithValue("@SellPrice", objOrderDetailmasterModel.SellPrice);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@SellPrice", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.Discount != null)
                                    {
                                        cmd.Parameters.AddWithValue("@Discount", objOrderDetailmasterModel.Discount);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Discount", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.finalPrice != null)
                                    {
                                        cmd.Parameters.AddWithValue("@finalPrice", objOrderDetailmasterModel.finalPrice);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@finalPrice", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.CreatedBy != null)
                                    {
                                        cmd.Parameters.AddWithValue("@CreatedBy", objOrderDetailmasterModel.CreatedBy);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.CreatedDate != null)
                                    {
                                        cmd.Parameters.AddWithValue("@CreatedDate", objOrderDetailmasterModel.CreatedDate);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.IsScale != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsScale", objOrderDetailmasterModel.IsScale);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsScale", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.IsFoodStamp != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsFoodStamp", objOrderDetailmasterModel.IsFoodStamp);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.IsTax != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsTax", objOrderDetailmasterModel.IsTax);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsTax", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.FoodStampTotal != null)
                                    {
                                        cmd.Parameters.AddWithValue("@FoodStampTotal", objOrderDetailmasterModel.FoodStampTotal);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@FoodStampTotal", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.DiscountApplyed != null)
                                    {
                                        cmd.Parameters.AddWithValue("@DiscountApplyed", objOrderDetailmasterModel.DiscountApplyed);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@DiscountApplyed", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.TaxAmount != null)
                                    {
                                        cmd.Parameters.AddWithValue("@TaxAmount", objOrderDetailmasterModel.TaxAmount);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@TaxAmount", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.IsRefund != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsRefund", objOrderDetailmasterModel.IsRefund);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsRefund", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.IsCancel != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsCancel", objOrderDetailmasterModel.IsCancel);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsCancel", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.GroupPrice != null)
                                    {
                                        cmd.Parameters.AddWithValue("@GroupPrice", objOrderDetailmasterModel.GroupPrice);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@GroupPrice", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.GroupQty != null)
                                    {
                                        cmd.Parameters.AddWithValue("@GroupQty", objOrderDetailmasterModel.GroupQty);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@GroupQty", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.CasePrice != null)
                                    {
                                        cmd.Parameters.AddWithValue("@CasePrice", objOrderDetailmasterModel.CasePrice);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@CasePrice", DBNull.Value);
                                    }
                                    if (objOrderDetailmasterModel.CaseQty != null)
                                    {
                                        cmd.Parameters.AddWithValue("@CaseQty", objOrderDetailmasterModel.CaseQty);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@CaseQty", DBNull.Value);
                                    }

                                    #endregion

                                    cmd.ExecuteScalar();
                                    cmd.CommandText = "Select @@Identity";
                                    long OrderDetailID = Convert.ToInt32(cmd.ExecuteScalar());
                                }
                                #endregion

                                if (conn.State == ConnectionState.Open)
                                {
                                    conn.Close();
                                }
                            }
                            MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
                            Productdata_ = new List<OrderScanner_ResultModel>();
                            dataLoad();
                            lblWeight.Text = "0.00 lb";
                            CouponInfo.CouponCode = "";
                            CouponInfo.isCoupon = false;
                            CouponInfo.Discount = 0;
                            CouponInfo.DiscAmt = 0;
                        }
                        txtSearchUPCCode.Focus();
                    }
                    #endregion

                    #region MG
                    else if (txtSearchUPCCode.Text.Trim().ToLower().StartsWith("mg"))
                    {
                        txtSearchUPCCode.Text = "";
                       DeviceRemove();

                        FrmManagerPassword objFrmManagerPassword = new FrmManagerPassword();
                        if (dataGridProducts.Rows.Count > 0)
                        {
                            objFrmManagerPassword.isCartEmpty = false;
                        }
                        else
                        {
                            objFrmManagerPassword.isCartEmpty = true;
                        }
                        objFrmManagerPassword.ShowDialog();

                        DeviceAdd();
                        if (ManagerAction.suspend == true)
                        {
                            TransSuspendMasterModel objTransSuspendMasterModel = new TransSuspendMasterModel();
                            objTransSuspendMasterModel.TransSuspendCode = GetTransSuspendNo();

                            if (LoginInfo.Connections)
                            {
                                #region InsertTransSuspend
                                for (int i = 0; i < dataGridProducts.Rows.Count; i++)
                                {
                                    string regex = "(\\(.*\\))";
                                    objTransSuspendMasterModel.ProductName = Regex.Replace(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").Trim().Replace(" - Group Discount", "");
                                    objTransSuspendMasterModel.ProductID = Functions.GetLong(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                                    objTransSuspendMasterModel.UPCCode = dataGridProducts.Rows[i].Cells[ProductMasterModelCont.UPCCode].Value.ToString();
                                    objTransSuspendMasterModel.Quantity = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                                    objTransSuspendMasterModel.DepartmentID = Functions.GetLong(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.DepartmentID].Value.ToString());
                                    objTransSuspendMasterModel.SectionID = Functions.GetLong(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SectionID].Value.ToString());
                                    objTransSuspendMasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.FinalPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.TotalAmount = Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.TotalTaxAmount = Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.GrossAmount = Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.Tax = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Tax].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.DiscountApplyed = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.DiscountApplyed].Value.ToString());
                                    objTransSuspendMasterModel.IsScale = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsScale].Value.ToString());
                                    objTransSuspendMasterModel.IsFoodStamp = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString());
                                    objTransSuspendMasterModel.IsTax = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString());

                                    objTransSuspendMasterModel.GroupPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.GroupPrice].Value.ToString());
                                    objTransSuspendMasterModel.GroupQty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.GroupQty].Value.ToString());
                                    objTransSuspendMasterModel.CasePrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CasePrice].Value.ToString());
                                    objTransSuspendMasterModel.CaseQty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CaseQty].Value.ToString());
                                    objTransSuspendMasterModel.CasePriceApplied = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CasePriceApplied].Value.ToString());
                                    objTransSuspendMasterModel.Status = false;
                                    objTransSuspendMasterModel.StoreID = LoginInfo.StoreID;
                                    objTransSuspendMasterModel.CreatedBy = LoginInfo.UserId;
                                    objTransSuspendMasterModel.CreatedDate = DateTime.Now;
                                    objTransSuspendMasterModel.IsDelete = false;

                                    objTransSuspendMasterModel = _TransSuspendService.AddTransSuspend(objTransSuspendMasterModel, 1);
                                }
                                #endregion

                                #region OrderReceipt
                                DataTable dt = GetSuspendTrans(objTransSuspendMasterModel.TransSuspendCode);

                                if (dt.Rows.Count >= 0)
                                {
                                    OrderInfo.PaymentType = 5;
                                    OrderInfo.IsFSVoidtax = false;
                                    if (CheckMyPrinter())
                                    {
                                        Printer printer = new Printer(XMLData.PrinterName, PrinterType.Epson);
                                        printer.LowPaper();
                                        string status = printer.GetStatus();
                                        if (status == "1024" || status == "0" || ClsCommon.CheckPrinterRoll == false)
                                        {
                                            printer.dt = dt;
                                            printer.TaxableAmount = TaxableAmount;
                                            printer.ReceiptPrint();
                                            printer.PartialPaperCut();
                                            printer.PrintDocument();
                                        }
                                        else
                                        {
                                            ClsCommon.MsgBox("Information", "Something went wrong with printer. Please check paper roll.", false);
                                        }
                                    }
                                    OrderInfo.CashAmt = 0;
                                    OrderInfo.Change = 0;
                                    OrderInfo.IsOrder = false;
                                    OrderInfo.FSTotal = 0;
                                    OrderInfo.remainingFSAmt = 0;
                                    OrderInfo.IsFSClicked = false;
                                    OrderInfo.CheckAmt = 0;
                                    OrderInfo.CreditAmt = 0;
                                    lblFSTotal.Text = Functions.GetDisplayAmount("0.00");
                                    TaxableAmount = 0;
                                    OrderInfo.IsFSVoidtax = true;
                                    Balance = 0;
                                    lblWeight.Text = "0.00 lb";
                                    txtSearchUPCCode.Focus();
                                    FSEligibleVoidAmt = 0;
                                }
                                #endregion
                            }
                            else
                            {
                                #region InsertTransSuspend OFFLINE
                                DataAdapter = new SqlCeDataAdapter();
                                SqlCeCommand cmd = conn.CreateCommand();
                                cmd = conn.CreateCommand();
                                if (conn.State == ConnectionState.Closed)
                                {
                                    conn.Open();
                                }

                                for (int i = 0; i < dataGridProducts.Rows.Count; i++)
                                {
                                    string regex = "(\\(.*\\))";
                                    objTransSuspendMasterModel.ProductName = Regex.Replace(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").Trim().Replace(" - Group Discount", "");
                                    objTransSuspendMasterModel.ProductID = Functions.GetLong(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                                    objTransSuspendMasterModel.UPCCode = dataGridProducts.Rows[i].Cells[ProductMasterModelCont.UPCCode].Value.ToString();
                                    objTransSuspendMasterModel.Quantity = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                                    objTransSuspendMasterModel.DepartmentID = Functions.GetLong(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.DepartmentID].Value.ToString());
                                    objTransSuspendMasterModel.SectionID = Functions.GetLong(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SectionID].Value.ToString());
                                    objTransSuspendMasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.FinalPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.TotalAmount = Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.TotalTaxAmount = Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.GrossAmount = Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                                    objTransSuspendMasterModel.Tax = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Tax].Value.ToString());
                                    objTransSuspendMasterModel.DiscountApplyed = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.DiscountApplyed].Value.ToString());
                                    objTransSuspendMasterModel.IsScale = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsScale].Value.ToString());
                                    objTransSuspendMasterModel.IsFoodStamp = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString());
                                    objTransSuspendMasterModel.IsTax = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString());

                                    objTransSuspendMasterModel.GroupPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.GroupPrice].Value.ToString());
                                    objTransSuspendMasterModel.GroupQty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.GroupQty].Value.ToString());
                                    objTransSuspendMasterModel.CasePrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CasePrice].Value.ToString());
                                    objTransSuspendMasterModel.CaseQty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CaseQty].Value.ToString());
                                    objTransSuspendMasterModel.CasePriceApplied = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CasePriceApplied].Value.ToString());
                                    objTransSuspendMasterModel.Status = false;
                                    objTransSuspendMasterModel.StoreID = LoginInfo.StoreID;
                                    objTransSuspendMasterModel.CreatedBy = LoginInfo.UserId;
                                    objTransSuspendMasterModel.CreatedDate = DateTime.Now;
                                    objTransSuspendMasterModel.IsDelete = false;

                                    cmd.CommandText = "INSERT INTO tbl_TransSuspenMaster(TransSuspendCode,ProductID,ProductName,UPCCode,DepartmentID,SectionID,Quantity,SellPrice,FinalPrice,TotalAmount,Tax,GrossAmount,IsScale,IsFoodStamp,IsTax,DiscountApplyed,Status,StoreID,CreatedBy,CreatedDate,IsDelete,TotalTaxAmount,GroupPrice,GroupQty,CasePrice,CaseQty,CasePriceApplied) " +
                                                      "VALUES(@TransSuspendCode, @ProductID, @ProductName, @UPCCode, @DepartmentID, @SectionID, @Quantity, @SellPrice, @FinalPrice, @TotalAmount, @Tax, @GrossAmount, @IsScale, @IsFoodStamp, @IsTax, @DiscountApplyed, @Status, @StoreID, @CreatedBy, @CreatedDate, @IsDelete,@TotalTaxAmount,@GroupPrice,@GroupQty,@CasePrice,@CaseQty,@CasePriceApplied)";

                                    #region Parameters
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@TransSuspendID", objTransSuspendMasterModel.TransSuspendID);
                                    if (objTransSuspendMasterModel.TransSuspendCode != null)
                                    {
                                        cmd.Parameters.AddWithValue("@TransSuspendCode", objTransSuspendMasterModel.TransSuspendCode);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@TransSuspendCode", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.ProductID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@ProductID", objTransSuspendMasterModel.ProductID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.ProductName != null)
                                    {
                                        cmd.Parameters.AddWithValue("@ProductName", objTransSuspendMasterModel.ProductName);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@ProductName", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.UPCCode != null)
                                    {
                                        cmd.Parameters.AddWithValue("@UPCCode", objTransSuspendMasterModel.UPCCode);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@UPCCode", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.DepartmentID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@DepartmentID", objTransSuspendMasterModel.DepartmentID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.SectionID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@SectionID", objTransSuspendMasterModel.SectionID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.Quantity != null)
                                    {
                                        cmd.Parameters.AddWithValue("@Quantity", objTransSuspendMasterModel.Quantity);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Quantity", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.SellPrice != null)
                                    {
                                        cmd.Parameters.AddWithValue("@SellPrice", objTransSuspendMasterModel.SellPrice);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@SellPrice", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.FinalPrice != null)
                                    {
                                        cmd.Parameters.AddWithValue("@FinalPrice", objTransSuspendMasterModel.FinalPrice);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@FinalPrice", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.TotalAmount != null)
                                    {
                                        cmd.Parameters.AddWithValue("@TotalAmount", objTransSuspendMasterModel.TotalAmount);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@TotalAmount", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.Tax != null)
                                    {
                                        cmd.Parameters.AddWithValue("@Tax", objTransSuspendMasterModel.Tax);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Tax", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.GrossAmount != null)
                                    {
                                        cmd.Parameters.AddWithValue("@GrossAmount", objTransSuspendMasterModel.GrossAmount);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@GrossAmount", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.IsScale != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsScale", objTransSuspendMasterModel.IsScale);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsScale", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.IsFoodStamp != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsFoodStamp", objTransSuspendMasterModel.IsFoodStamp);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.IsTax != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsTax", objTransSuspendMasterModel.IsTax);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsTax", DBNull.Value);
                                    }
                                    cmd.Parameters.AddWithValue("@GroupPrice", objTransSuspendMasterModel.GroupPrice);
                                    cmd.Parameters.AddWithValue("@GroupQty", objTransSuspendMasterModel.GroupQty);
                                    cmd.Parameters.AddWithValue("@CasePrice", objTransSuspendMasterModel.CasePrice);
                                    cmd.Parameters.AddWithValue("@CaseQty", objTransSuspendMasterModel.CaseQty);
                                    if (objTransSuspendMasterModel.CasePriceApplied != null)
                                    {
                                        cmd.Parameters.AddWithValue("@CasePriceApplied", objTransSuspendMasterModel.CasePriceApplied);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@CasePriceApplied", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.DiscountApplyed != null)
                                    {
                                        cmd.Parameters.AddWithValue("@DiscountApplyed", objTransSuspendMasterModel.DiscountApplyed);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@DiscountApplyed", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.Status != null)
                                    {
                                        cmd.Parameters.AddWithValue("@Status", objTransSuspendMasterModel.Status);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.StoreID != null)
                                    {
                                        cmd.Parameters.AddWithValue("@StoreID", objTransSuspendMasterModel.StoreID);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.CreatedDate != null)
                                    {
                                        cmd.Parameters.AddWithValue("@CreatedDate", objTransSuspendMasterModel.CreatedDate);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.CreatedBy != null)
                                    {
                                        cmd.Parameters.AddWithValue("@CreatedBy", objTransSuspendMasterModel.CreatedBy);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.IsDelete != null)
                                    {
                                        cmd.Parameters.AddWithValue("@IsDelete", objTransSuspendMasterModel.IsDelete);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                                    }
                                    if (objTransSuspendMasterModel.TotalTaxAmount != null)
                                    {
                                        cmd.Parameters.AddWithValue("@TotalTaxAmount", objTransSuspendMasterModel.TotalTaxAmount);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@TotalTaxAmount", DBNull.Value);
                                    }

                                    #endregion

                                    cmd.ExecuteScalar();
                                }
                                #endregion

                                #region OrderReceipt OFFLINE
                                DataTable dt = new DataTable();

                                string query = "SELECT TM.TransSuspendID,TM.TransSuspendCode,TM.ProductID,TM.ProductName,TM.UPCCode,TM.Quantity,TM.SellPrice " +
                                                ",TM.FinalPrice,TM.TotalAmount,TM.Tax,TM.GrossAmount,TM.IsScale,TM.IsFoodStamp,TM.IsTax,TM.TotalTaxAmount AS TaxAmount" +
                                                ",TM.DiscountApplyed,TM.Status,TM.StoreID,TM.CreatedBy,TM.CreatedDate,TM.IsDelete,TM.GroupQty,TM.GroupPrice,TM.CaseQty,TM.CasePrice" +
                                                ",SM.StoreName,SM.Address AS SMAddress,SM.Address2 AS SAddress2,TM.TransSuspendCode AS OrdNo,TM.CasePriceApplied" +
                                                ",SM.ZipCode AS SZipCode,SM.Phone AS SPhone,SM.Fax AS SFax,EM.FirstName,0 AS CouponDiscAmt " +
                                                "FROM tbl_TransSuspenMaster AS TM " +
                                                "INNER JOIN tbl_StoreMaster AS SM  ON TM.StoreID = SM.StoreID " +
                                                "INNER JOIN tbl_EmployeeMaster AS EM ON EM.EmployeeID = TM.CreatedBy " +
                                                "WHERE TM.TransSuspendCode = '" + objTransSuspendMasterModel.TransSuspendCode + "' AND TM.IsDelete = 0";

                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                DataAdapter.Fill(dt);

                                if (conn.State == ConnectionState.Open)
                                {
                                    conn.Close();
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    OrderInfo.PaymentType = 5;
                                    OrderInfo.IsFSVoidtax = false;
                                    if (CheckMyPrinter())
                                    {
                                        Printer printer = new Printer(XMLData.PrinterName, PrinterType.Epson);
                                        printer.LowPaper();
                                        string status = printer.GetStatus();
                                        if (status == "1024" || status == "0" || ClsCommon.CheckPrinterRoll == false)
                                        {
                                            printer.dt = dt;
                                            printer.TaxableAmount = TaxableAmount;
                                            printer.ReceiptPrint();
                                            printer.PartialPaperCut();
                                            printer.PrintDocument();
                                        }
                                        else
                                        {
                                            ClsCommon.MsgBox("Information", "Something went wrong with printer. Please check paper roll.", false);
                                        }
                                    }

                                    OrderInfo.CashAmt = 0;
                                    OrderInfo.Change = 0;
                                    OrderInfo.IsOrder = false;
                                    OrderInfo.FSTotal = 0;
                                    OrderInfo.remainingFSAmt = 0;
                                    OrderInfo.IsFSClicked = false;
                                    OrderInfo.CheckAmt = 0;
                                    OrderInfo.CreditAmt = 0;
                                    lblFSTotal.Text = Functions.GetDisplayAmount("0.00");
                                    TaxableAmount = 0;
                                    OrderInfo.IsFSVoidtax = true;
                                    Balance = 0;
                                    lblWeight.Text = "0.00 lb";
                                    txtSearchUPCCode.Focus();
                                    FSEligibleVoidAmt = 0;
                                }
                                #endregion
                            }
                            MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
                            Productdata_ = new List<OrderScanner_ResultModel>();
                            ManagerAction.suspend = false;
                            dataLoad();
                            txtSearchUPCCode.Focus();
                            ClsCommon.ResetStaticValues();
                        }

                        else if (ManagerAction.resume == true)
                        {
                            OrderScanner_ResultModel lstOrderScanner_ResultModel;
                            TransSuspendMasterModel objTransSuspendMasterModel;

                            for (int row = 0; row < ManagerAction.dtresume.Rows.Count; row++)
                            {
                                if (LoginInfo.Connections)
                                {
                                    objTransSuspendMasterModel = new TransSuspendMasterModel();
                                    objTransSuspendMasterModel.TransSuspendID = Convert.ToInt64(ManagerAction.dtresume.Rows[row]["TransSuspendID"].ToString());
                                    objTransSuspendMasterModel.IsDelete = true;
                                    objTransSuspendMasterModel.Status = true;
                                    objTransSuspendMasterModel = _TransSuspendService.AddTransSuspend(objTransSuspendMasterModel, 2);
                                }
                                else
                                {
                                    #region Update
                                    if (conn.State == ConnectionState.Closed)
                                    {
                                        conn.Open();
                                    }
                                    DataAdapter = new SqlCeDataAdapter();
                                    SqlCeCommand cmd = conn.CreateCommand();
                                    cmd = conn.CreateCommand();
                                    cmd.CommandText = "UPDATE tbl_TransSuspenMaster SET IsDelete=1,Status=1 WHERE TransSuspendCode='" + ManagerAction.dtresume.Rows[row]["TransSuspendCode"].ToString() + "'";
                                    cmd.ExecuteNonQuery();
                                    if (conn.State == ConnectionState.Open)
                                    {
                                        conn.Close();
                                    }
                                    #endregion
                                }

                                lstOrderScanner_ResultModel = new OrderScanner_ResultModel();
                                lstOrderScanner_ResultModel.UPCCode = ManagerAction.dtresume.Rows[row]["UPCCode"].ToString();
                                lstOrderScanner_ResultModel.ProductName = ManagerAction.dtresume.Rows[row]["ProductName"].ToString();
                                lstOrderScanner_ResultModel.ProductID = Functions.GetLong(ManagerAction.dtresume.Rows[row]["ProductID"].ToString());
                                lstOrderScanner_ResultModel.Tax = Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["Tax"].ToString());
                                lstOrderScanner_ResultModel.SellPrice = Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["SellPrice"].ToString());
                                lstOrderScanner_ResultModel.FinalPrice = Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["FinalPrice"].ToString());
                                lstOrderScanner_ResultModel.IsTax = Functions.GetBoolean(ManagerAction.dtresume.Rows[row]["IsTax"].ToString());
                                lstOrderScanner_ResultModel.IsScale = Functions.GetBoolean(ManagerAction.dtresume.Rows[row]["IsScale"].ToString());
                                lstOrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(ManagerAction.dtresume.Rows[row]["IsFoodStamp"].ToString());
                                lstOrderScanner_ResultModel.DiscountApplyed = Functions.GetBoolean(ManagerAction.dtresume.Rows[row]["DiscountApplyed"].ToString());
                                lstOrderScanner_ResultModel.Qty = Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["Quantity"].ToString());
                                lstOrderScanner_ResultModel.DepartmentID = Functions.GetLong(ManagerAction.dtresume.Rows[row]["DepartmentID"].ToString());
                                lstOrderScanner_ResultModel.SectionID = Functions.GetLong(ManagerAction.dtresume.Rows[row]["SectionID"].ToString());
                                lstOrderScanner_ResultModel.TaxAmount = ((Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["FinalPrice"].ToString()) / 100) * Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["Tax"].ToString()));
                                lstOrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["GroupPrice"].ToString());
                                lstOrderScanner_ResultModel.GroupQty = Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["GroupQty"].ToString());
                                lstOrderScanner_ResultModel.CasePrice = Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["CasePrice"].ToString());
                                lstOrderScanner_ResultModel.CaseQty = Functions.GetDecimal(ManagerAction.dtresume.Rows[row]["CaseQty"].ToString());
                                lstOrderScanner_ResultModel.CasePriceApplied = Functions.GetBoolean(ManagerAction.dtresume.Rows[row]["CasePriceApplied"].ToString());
                                Productdata_.Add(lstOrderScanner_ResultModel);
                            }
                            MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
                            ManagerAction.resume = false;
                            OrderInfo.IsFSVoidtax = false;
                            dataLoad();
                            txtSearchUPCCode.Text = "";
                            txtSearchUPCCode.Focus();
                        }

                        else if (ManagerAction.TillStatus == true)
                        {
                            TillStatus();
                            ManagerAction.TillStatus = false;
                            txtSearchUPCCode.Text = "";
                            txtSearchUPCCode.Focus();
                        }

                        txtSearchUPCCode.Text = "";
                        txtSearchUPCCode.Focus();
                    }
                    #endregion

                    #region @1tnfn
                    else if (txtSearchUPCCode.Text.Trim().ToLower().StartsWith("1tnfn"))
                    {
                        LoginInfo.tnfn = true;
                        txtSearchUPCCode.Text = "@";
                        txtSearchUPCCode.SelectionStart = txtSearchUPCCode.Text.Length;
                        txtSearchUPCCode.SelectionLength = 0;
                    }
                    #endregion

                    #region @cs
                    else if (txtSearchUPCCode.Text.Trim().ToLower().StartsWith("cs"))
                    {
                        LoginInfo.CasePrice = true;
                    }
                    #endregion

                    #region PK
                    else if (txtSearchUPCCode.Text.Length > 4)
                    {
                        OrignalUPCCode = txtSearchUPCCode.Text.Trim();
                        if (txtSearchUPCCode.Text.Trim().ToLower().Contains("pk"))
                        {
                            IsPK = true;
                            string EnterUPCCode = txtSearchUPCCode.Text;
                            string[] UPCCOde = EnterUPCCode.ToUpper().Split('P');

                            txtSearchUPCCode.Text = UPCCOde[1].ToUpper().Replace("K", "");

                            if (UPCCOde[0] == "")
                            {
                                txtSearchUPCCode.Text = null;
                                txtSearchUPCCode.Focus();
                                ClsCommon.MsgBox("Information", "You must key in the price ! ", false);
                                return;
                            }
                            else
                            {
                                _SellPrice = Functions.GetDecimal(UPCCOde[0]) / 100;
                            }
                            txtSearchUPCCode.Text = txtSearchUPCCode.Text.ToLower().Replace("pk", "");
                            int Count = txtSearchUPCCode.Text.Length;

                            Count = 13 - Count;
                            for (int i = 0; i < Count; i++)
                            {
                                txtSearchUPCCode.Text = "0" + txtSearchUPCCode.Text;
                            }
                            ProductAdd();
                        }
                        else
                        {
                            int Count = txtSearchUPCCode.Text.Length;
                            string[] UpcCode = new string[2];
                            string AfterStar = "";
                            if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                            {
                                UpcCode = txtSearchUPCCode.Text.Trim().Split('*');
                                AfterStar = UpcCode[1];
                                Count = AfterStar.Length;
                            }
                            Count = 13 - Count;
                            if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                            {
                                UpcCode = txtSearchUPCCode.Text.Trim().Split('*');
                                for (int i = 0; i < Count; i++)
                                {
                                    AfterStar = "0" + AfterStar;
                                }
                                UpcCode[1] = AfterStar;
                            }
                            else
                            {
                                for (int i = 0; i < Count; i++)
                                {
                                    txtSearchUPCCode.Text = "0" + txtSearchUPCCode.Text;
                                }
                                UpcCode[0] = "1";
                                UpcCode[1] = txtSearchUPCCode.Text;
                            }
                            List<OrderScanner_ResultModel> Productdata = new List<OrderScanner_ResultModel>();

                            #region Product Scan
                            if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                            {
                                Productdata = UPCScanner(AfterStar);
                            }
                            else
                            {
                                Productdata = UPCScanner(txtSearchUPCCode.Text.Trim());
                            }
                            #endregion

                            #region UPC-E
                            if (Productdata.Count == 0)
                            {
                                //04963406 = 0004900000634
                                string[] UPC_E = new string[2];
                                if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                {
                                    UPC_E = txtSearchUPCCode.Text.Trim().Split('*');
                                    UPC_E[1] = Functions.GetUPC_E(UPC_E[1].ToString());
                                    Productdata = UPCScanner(UPC_E[1]);
                                }
                                else
                                {
                                    UPC_E[0] = "1";
                                    UPC_E[1] = Functions.GetUPC_E(OrignalUPCCode);
                                    Productdata = UPCScanner(UPC_E[1]);
                                }
                            }
                            #endregion

                            #region Product Label
                            if (Productdata.Count == 0)
                            {
                                if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                {
                                    Productdata = AddLabeledPrice(AfterStar);
                                }
                                else
                                {
                                    Productdata = AddLabeledPrice(txtSearchUPCCode.Text);
                                }
                            }
                            #endregion

                            if (Productdata.Count > 0)
                            {
                                Productdata[0].Qty = Functions.GetDecimal(UpcCode[0]);

                                #region IsGroup
                                if (!String.IsNullOrEmpty(Productdata[0].GroupQty.ToString()) && Productdata[0].IsGroupPrice == true)
                                {
                                    if (Productdata[0].GroupQty > 0)
                                    {
                                        IsGroup = true;
                                    }
                                    else
                                    {
                                        IsGroup = false;
                                    }
                                }
                                else
                                {
                                    IsGroup = false;
                                }
                                #endregion

                                foreach (OrderScanner_ResultModel objOrderScanner_ResultModel in Productdata)
                                {
                                    if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                    {
                                        objOrderScanner_ResultModel.Qty = Functions.GetDecimal(UpcCode[0]);
                                        objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Qty;
                                    }
                                    else if (objOrderScanner_ResultModel.IsScale == true)
                                    {
                                        #region IsScale
                                        if (lblWeight.Text != "0.00 lb")
                                        {
                                            if (IsPK == true)
                                            {
                                                //objOrderScanner_ResultModel.Qty = Functions.GetDecimal(lblWeight.Text.Replace("lb", ""));
                                                if (LoginInfo.TareWeight > 0)
                                                {
                                                    objOrderScanner_ResultModel.Qty = Functions.GetDecimal(lblWeight.Text.Replace("lb", ""));
                                                    objOrderScanner_ResultModel.Qty = objOrderScanner_ResultModel.Qty - LoginInfo.TareWeight;
                                                    LoginInfo.TareWeight = 0;
                                                }
                                                else
                                                {
                                                    objOrderScanner_ResultModel.Qty = Functions.GetDecimal(lblWeight.Text.Replace("lb", ""));
                                                }
                                                objOrderScanner_ResultModel.SellPrice = _SellPrice;
                                                objOrderScanner_ResultModel.FinalPrice = _SellPrice * objOrderScanner_ResultModel.Qty;
                                            }
                                            else
                                            {
                                                objOrderScanner_ResultModel.Qty = Functions.GetDecimal(lblWeight.Text.Replace("lb", ""));
                                                objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Qty;
                                            }
                                        }
                                        else
                                        {
                                            txtSearchUPCCode.Text = "";
                                            txtSearchUPCCode.Focus();
                                            ClsCommon.MsgBox("Information", "There is nothing on scale,please put the scale then scan item.", false);
                                            return;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        objOrderScanner_ResultModel.Qty = 1;
                                    }

                                    if (IsGroup == true && LoginInfo.CasePrice == false && objOrderScanner_ResultModel.IsScale == false)
                                    {
                                        OrderScanner_ResultModel objOrderScanner_ResultModel_ = new OrderScanner_ResultModel();
                                        objOrderScanner_ResultModel_ = objOrderScanner_ResultModel;
                                        if (!AddItem)
                                        {
                                            #region Grouping
                                            decimal TotalItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false).Sum(item => item.Qty);
                                            decimal GroupItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true).Sum(item => item.Qty) / objOrderScanner_ResultModel_.GroupQty;
                                            decimal Isvoid = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == true && x.CasePriceApplied == false).Sum(item => item.Qty);
                                            Isvoid = Math.Abs(Isvoid);
                                            TotalItem = TotalItem - Isvoid;
                                            TotalItem = TotalItem + objOrderScanner_ResultModel_.Qty;
                                            Group = 0; UnGroup = 0;
                                            for (int i = 0; TotalItem != 0; i++)
                                            {
                                                if (TotalItem >= objOrderScanner_ResultModel_.GroupQty)
                                                {
                                                    Group++;
                                                    TotalItem -= objOrderScanner_ResultModel_.GroupQty;
                                                }
                                                else
                                                {
                                                    UnGroup = TotalItem;
                                                    TotalItem = 0;
                                                }
                                            }
                                            UnGroup += Isvoid;
                                            #endregion

                                            if (Group > 0)
                                            {
                                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false);
                                                #region LinkedUPCCode
                                                if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                {
                                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                }
                                                #endregion

                                                objOrderScanner_ResultModel_.DiscountApplyed = true;
                                                objOrderScanner_ResultModel_.Qty = Convert.ToInt32(Math.Floor(Convert.ToDecimal(objOrderScanner_ResultModel_.GroupQty * Group)));
                                                objOrderScanner_ResultModel_.FinalPrice = objOrderScanner_ResultModel_.GroupPrice * Group;
                                                objOrderScanner_ResultModel_.TaxAmount = (objOrderScanner_ResultModel_.FinalPrice * objOrderScanner_ResultModel_.Tax) / 100;
                                                objOrderScanner_ResultModel_.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel_.TaxAmount.ToString()).ToString("0.00"));
                                                objOrderScanner_ResultModel_.RowNo = GetRowNo();
                                                objOrderScanner_ResultModel_.IsVerifed = 1;
                                                Productdata_.Add(objOrderScanner_ResultModel_);

                                                #region LinkedUPCCode
                                                if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                {
                                                    LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, objOrderScanner_ResultModel_.Qty, 1, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                                }
                                                #endregion

                                                objOrderScanner_ResultModel_ = new OrderScanner_ResultModel();
                                                objOrderScanner_ResultModel_ = objOrderScanner_ResultModel;
                                                if (UnGroup > 0)
                                                {
                                                    decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                                    if (itemCount > UnGroup)
                                                    {
                                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);

                                                        #region LinkedUPCCode
                                                        if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                        {
                                                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                        }
                                                        #endregion

                                                        itemCount = 0;
                                                    }
                                                    UnGroup = UnGroup - itemCount;
                                                    for (int i = 0; i < UnGroup; i++)
                                                    {
                                                        LinkUPCCodeAdd(objOrderScanner_ResultModel_.UPCCode, 1, 0, "", objOrderScanner_ResultModel_.IsForceTax);

                                                        #region LinkedUPCCode
                                                        if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                        {
                                                            LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                                        }
                                                        #endregion
                                                    }
                                                }
                                                else
                                                {
                                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);
                                                    #region LinkedUPCCode
                                                    if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                    {
                                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                    }
                                                    #endregion
                                                }
                                            }
                                            else if (UnGroup > 0)
                                            {
                                                if (Group > 0)
                                                {
                                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1);
                                                    #region LinkedUPCCode
                                                    if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                    {
                                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                    }
                                                    #endregion
                                                }
                                                decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                                UnGroup = UnGroup - itemCount;
                                                for (int i = 0; i < UnGroup; i++)
                                                {
                                                    LinkUPCCodeAdd(objOrderScanner_ResultModel_.UPCCode, 1, 0, "", objOrderScanner_ResultModel_.IsForceTax);

                                                    #region LinkedUPCCode
                                                    if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                    {
                                                        LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                                    }
                                                    #endregion
                                                }
                                            }
                                            else
                                            {
                                                objOrderScanner_ResultModel.RowNo = GetRowNo();
                                                Productdata_.Add(objOrderScanner_ResultModel);
                                            }
                                            dataLoad();
                                        }
                                    }
                                    else if (IsGroup == true && LoginInfo.CasePrice == false && objOrderScanner_ResultModel.IsScale == true)
                                    {
                                        Group = 0;
                                        UnGroup = 0;
                                        decimal TotalItem_ = 0;
                                        decimal TotalItem = TotalItem_ = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false).Sum(item => item.Qty);
                                        decimal GroupItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true).Sum(item => item.Qty) / objOrderScanner_ResultModel.GroupQty;
                                        decimal Isvoid = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == true && x.CasePriceApplied == false).Sum(item => item.Qty);
                                        decimal? Isvoid_price = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == true && x.CasePriceApplied == false).Sum(item => item.FinalPrice);
                                        Isvoid = Math.Abs(Isvoid);
                                        TotalItem = TotalItem - Isvoid;
                                        TotalItem = TotalItem + objOrderScanner_ResultModel.Qty;
                                        TotalItem_ = TotalItem;
                                        for (int i = 0; TotalItem != 0; i++)
                                        {
                                            if (TotalItem >= objOrderScanner_ResultModel.GroupQty)
                                            {
                                                Group++;
                                                TotalItem -= objOrderScanner_ResultModel.GroupQty;
                                            }
                                            else
                                            {
                                                UnGroup = TotalItem;
                                                TotalItem = 0;
                                            }
                                        }
                                        //UnGroup += Isvoid;

                                        decimal groupPrice = 0;
                                        decimal groupQty = 0;
                                        decimal? UnGroupPrice = 0;
                                        decimal UnGroupQty = 0;
                                        //decimal? FinalAmount = 0;
                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false);
                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false);
                                        //dataLoad();
                                        groupQty = Group * objOrderScanner_ResultModel.GroupQty;
                                        UnGroupQty = UnGroup;
                                        //FinalAmount = (groupQty + UnGroupQty) * objOrderScanner_ResultModel.SellPrice;

                                        groupPrice = Group * objOrderScanner_ResultModel.GroupPrice;
                                        //UnGroupPrice = UnGroup * objOrderScanner_ResultModel.SellPrice;
                                        UnGroupPrice = UnGroup * (objOrderScanner_ResultModel.GroupPrice / objOrderScanner_ResultModel.GroupQty);

                                        //FinalAmount = TotalItem_ * objOrderScanner_ResultModel.SellPrice;
                                        objOrderScanner_ResultModel.Qty = groupQty + UnGroupQty;

                                        if (objOrderScanner_ResultModel.Qty == objOrderScanner_ResultModel.GroupQty && Group > 0)
                                        {
                                            objOrderScanner_ResultModel.DiscountApplyed = true;
                                            objOrderScanner_ResultModel.FinalPrice = Functions.GetDecimal(Functions.GetDecimal((objOrderScanner_ResultModel.Qty * objOrderScanner_ResultModel.SellPrice).ToString()).ToString("0.00")) * (objOrderScanner_ResultModel.GroupPrice / objOrderScanner_ResultModel.GroupQty);
                                            objOrderScanner_ResultModel.FinalPrice = groupPrice;
                                            //objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.FinalPrice;
                                        }
                                        else if (objOrderScanner_ResultModel.Qty >= objOrderScanner_ResultModel.GroupQty && Group > 0)
                                        {
                                            objOrderScanner_ResultModel.DiscountApplyed = true;
                                            //objOrderScanner_ResultModel.FinalPrice = Functions.GetDecimal(Functions.GetDecimal((objOrderScanner_ResultModel.Qty * objOrderScanner_ResultModel.SellPrice).ToString()).ToString("0.00")) * (objOrderScanner_ResultModel.GroupPrice / objOrderScanner_ResultModel.GroupQty);
                                            objOrderScanner_ResultModel.FinalPrice = groupPrice + UnGroupPrice;
                                            //objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.FinalPrice;
                                        }
                                        else
                                        {
                                            objOrderScanner_ResultModel.DiscountApplyed = false;
                                            objOrderScanner_ResultModel.FinalPrice = Functions.GetDecimal(Functions.GetDecimal((objOrderScanner_ResultModel.Qty * objOrderScanner_ResultModel.SellPrice).ToString()).ToString("0.00"));
                                        }
                                        objOrderScanner_ResultModel.FinalPrice = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.FinalPrice.ToString()).ToString("0.00")) + Math.Abs(Convert.ToDecimal(Isvoid_price.ToString()));
                                        objOrderScanner_ResultModel.Qty = objOrderScanner_ResultModel.Qty + Math.Abs(Isvoid);

                                        objOrderScanner_ResultModel.TaxAmount = (objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Tax) / 100;
                                        objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        AddItem = false;
                                        Group = 0;
                                        UnGroup = 0;
                                        objOrderScanner_ResultModel.RowNo = GetRowNo();
                                        Productdata_.Add(objOrderScanner_ResultModel);
                                        //dataLoad();

                                        #region LinkedUPCCode
                                        if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                                        {
                                            txtSearchUPCCode.Text = (objOrderScanner_ResultModel.Qty).ToString() + "*" + objOrderScanner_ResultModel.LinkedUPCCode;
                                            LinkedParent = objOrderScanner_ResultModel.UPCCode;
                                            ProductAdd();
                                            LinkedParent = "";
                                        }
                                        #endregion
                                        dataLoad();
                                    }
                                    else
                                    {
                                        #region Tax Cal
                                        if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                        {
                                            objOrderScanner_ResultModel.TaxAmount = (objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Tax) / 100;
                                            objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        }
                                        else if (objOrderScanner_ResultModel.IsScale == true)
                                        {
                                            if (IsPK == true)
                                            {
                                            }
                                            else
                                            {
                                                objOrderScanner_ResultModel.TaxAmount = objOrderScanner_ResultModel.TaxAmount * objOrderScanner_ResultModel.Qty;
                                                objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                            }
                                        }
                                        else
                                        {
                                            objOrderScanner_ResultModel.TaxAmount = objOrderScanner_ResultModel.TaxAmount * objOrderScanner_ResultModel.Qty;
                                            objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        }
                                        #endregion

                                        #region CaseQty-CasePrice 20190710                                        
                                        if (LoginInfo.CasePrice)
                                        {
                                            if (objOrderScanner_ResultModel.CaseQty > 0 && objOrderScanner_ResultModel.CasePrice > 0)
                                            {
                                                IsGroup = false;
                                                objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.CasePrice * objOrderScanner_ResultModel.Qty;
                                                objOrderScanner_ResultModel.TaxAmount = (objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Tax) / 100;
                                                objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                                objOrderScanner_ResultModel.Qty = objOrderScanner_ResultModel.Qty * objOrderScanner_ResultModel.CaseQty;
                                                objOrderScanner_ResultModel.CasePriceApplied = true;
                                            }
                                            else
                                            {
                                                objOrderScanner_ResultModel.CasePriceApplied = false;
                                            }
                                            LoginInfo.CasePrice = false;
                                        }
                                        else
                                        {
                                            objOrderScanner_ResultModel.CasePriceApplied = false;
                                        }
                                        #endregion

                                        objOrderScanner_ResultModel.RowNo = GetRowNo(); Productdata_.Add(objOrderScanner_ResultModel);
                                        //dataLoad();

                                        #region LinkedUPCCode
                                        if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                                        {
                                            txtSearchUPCCode.Text = objOrderScanner_ResultModel.LinkedUPCCode;
                                            if (Convert.ToInt32(objOrderScanner_ResultModel.Qty) > 1)
                                            {
                                                txtSearchUPCCode.Text = (objOrderScanner_ResultModel.Qty).ToString() + "*" + txtSearchUPCCode.Text;
                                            }
                                            LinkedParent = objOrderScanner_ResultModel.UPCCode;
                                            ProductAdd();
                                            LinkedParent = "";
                                        }
                                        #endregion
                                        dataLoad();
                                    }
                                }
                                //if (IsBeep == true)
                                //{
                                //    GetBeepSound();
                                //}
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "Can't sale this product.!", false);
                                LoginInfo.tnfn = false;
                                LoginInfo.CasePrice = false;
                            }
                            txtSearchUPCCode.Text = null;
                            txtSearchUPCCode.Focus();
                        }

                    }
                    #endregion

                    #region 4 Digit Search
                    else if (txtSearchUPCCode.Text.Length < 5)
                    {
                        if (txtSearchUPCCode.Text != "")
                        {
                            #region UPCCode
                            OrignalUPCCode = txtSearchUPCCode.Text.Trim();
                            int Count = txtSearchUPCCode.Text.Length;
                            string[] UpcCode = new string[2];
                            string AfterStar = "";
                            if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                            {
                                int pos = txtSearchUPCCode.Text.LastIndexOf("*") + 1;
                                AfterStar = txtSearchUPCCode.Text.Substring(pos, txtSearchUPCCode.Text.Length - pos);
                                Count = txtSearchUPCCode.Text.Substring(pos, txtSearchUPCCode.Text.Length - pos).Length;
                            }
                            if (Count < 5)
                            {
                                Count = 13 - Count;
                                if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                {
                                    UpcCode = txtSearchUPCCode.Text.Trim().Split('*');
                                    for (int i = 0; i < Count; i++)
                                    {
                                        AfterStar = "0" + AfterStar;
                                    }
                                    UpcCode[1] = AfterStar;
                                }
                                else
                                {
                                    for (int i = 0; i < Count; i++)
                                    {
                                        txtSearchUPCCode.Text = "0" + txtSearchUPCCode.Text;
                                    }
                                    UpcCode[0] = "1";
                                    UpcCode[1] = txtSearchUPCCode.Text;
                                }
                            }
                            else
                            {
                                UpcCode[0] = "1";
                                UpcCode[1] = txtSearchUPCCode.Text;
                            }
                            #endregion

                            List<OrderScanner_ResultModel> Productdata = new List<OrderScanner_ResultModel>();

                            #region Product Scan

                            if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                            {
                                Productdata = UPCScanner(AfterStar);
                            }
                            else
                            {
                                Productdata = UPCScanner(txtSearchUPCCode.Text.Trim());
                            }
                            #endregion

                            #region UPC-E
                            if (Productdata.Count == 0)
                            {
                                //04963406 = 0004900000634
                                string[] UPC_E = new string[2];
                                if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                {
                                    UPC_E = txtSearchUPCCode.Text.Trim().Split('*');
                                    UPC_E[1] = Functions.GetUPC_E(UPC_E[1].ToString());
                                    Productdata = UPCScanner(UPC_E[1]);
                                }
                                else
                                {
                                    UPC_E[0] = "1";
                                    UPC_E[1] = Functions.GetUPC_E(OrignalUPCCode);
                                    Productdata = UPCScanner(UPC_E[1]);
                                }
                            }
                            #endregion

                            #region Product Label
                            if (Productdata.Count == 0)
                            {
                                if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                {
                                    Productdata = AddLabeledPrice(AfterStar);
                                }
                                else
                                {
                                    Productdata = AddLabeledPrice(txtSearchUPCCode.Text);
                                }
                            }
                            #endregion

                            if (Productdata.Count > 0)
                            {
                                Productdata[0].Qty = Functions.GetDecimal(UpcCode[0]);

                                #region IsGroup
                                if (!String.IsNullOrEmpty(Productdata[0].GroupQty.ToString()) && Productdata[0].IsGroupPrice == true)
                                {
                                    if (Productdata[0].GroupQty > 0)
                                    {
                                        IsGroup = true;
                                    }
                                    else
                                    {
                                        IsGroup = false;
                                    }
                                }
                                else
                                {
                                    IsGroup = false;
                                }
                                #endregion

                                foreach (OrderScanner_ResultModel objOrderScanner_ResultModel in Productdata)
                                {
                                    if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                    {
                                        int pos = txtSearchUPCCode.Text.LastIndexOf('*');
                                        objOrderScanner_ResultModel.Qty = Functions.GetDecimal(txtSearchUPCCode.Text.Substring(0, pos));
                                        if (objOrderScanner_ResultModel.Qty < 1)
                                        {
                                            ClsCommon.MsgBox("Information", "Quantity must be greater than 0.!", false);
                                            txtSearchUPCCode.Focus();
                                            txtSearchUPCCode.Text = "";
                                            return;
                                        }
                                        objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Qty;
                                        lblWeight.Text = "0.00 lb";
                                    }
                                    else if (objOrderScanner_ResultModel.IsScale == true)
                                    {
                                        #region IsScale
                                        //lblWeight.Text = "2.45 lb";
                                        if (lblWeight.Text != "0.00 lb")
                                        {
                                            if (LoginInfo.TareWeight > 0)
                                            {
                                                objOrderScanner_ResultModel.Qty = Functions.GetDecimal(lblWeight.Text.Replace("lb", ""));
                                                objOrderScanner_ResultModel.Qty = objOrderScanner_ResultModel.Qty - LoginInfo.TareWeight;
                                                LoginInfo.TareWeight = 0;
                                            }
                                            else
                                            {
                                                objOrderScanner_ResultModel.Qty = Functions.GetDecimal(lblWeight.Text.Replace("lb", ""));
                                            }
                                            objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Qty;
                                        }
                                        else
                                        {
                                            txtSearchUPCCode.Text = "";
                                            txtSearchUPCCode.Focus();
                                            ClsCommon.MsgBox("Information", "There is nothing on scale,please put the scale then scan item.", false);
                                            return;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        objOrderScanner_ResultModel.Qty = 1;
                                    }

                                    if (IsGroup == true && LoginInfo.CasePrice == false && objOrderScanner_ResultModel.IsScale == false)
                                    {
                                        OrderScanner_ResultModel objOrderScanner_ResultModel_ = new OrderScanner_ResultModel();
                                        objOrderScanner_ResultModel_ = objOrderScanner_ResultModel;
                                        if (!AddItem)
                                        {
                                            #region Grouping
                                            decimal TotalItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false).Sum(item => item.Qty);
                                            decimal GroupItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true).Sum(item => item.Qty) / objOrderScanner_ResultModel_.GroupQty;
                                            decimal Isvoid = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == true && x.CasePriceApplied == false).Sum(item => item.Qty);
                                            Isvoid = Math.Abs(Isvoid);
                                            TotalItem = TotalItem - Isvoid;
                                            TotalItem = TotalItem + objOrderScanner_ResultModel_.Qty;
                                            Group = 0; UnGroup = 0;
                                            for (int i = 0; TotalItem != 0; i++)
                                            {
                                                if (TotalItem >= objOrderScanner_ResultModel_.GroupQty)
                                                {
                                                    Group++;
                                                    TotalItem -= objOrderScanner_ResultModel_.GroupQty;
                                                }
                                                else
                                                {
                                                    UnGroup = TotalItem;
                                                    TotalItem = 0;
                                                }
                                            }
                                            UnGroup += Isvoid;
                                            #endregion

                                            if (Group > 0)
                                            {
                                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false);
                                                #region LinkedUPCCode
                                                if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                {
                                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false);
                                                }
                                                #endregion

                                                objOrderScanner_ResultModel_.DiscountApplyed = true;
                                                objOrderScanner_ResultModel_.Qty = Convert.ToInt32(Math.Floor(Convert.ToDecimal(objOrderScanner_ResultModel_.GroupQty * Group)));
                                                objOrderScanner_ResultModel_.FinalPrice = objOrderScanner_ResultModel_.GroupPrice * Group;
                                                objOrderScanner_ResultModel_.TaxAmount = (objOrderScanner_ResultModel_.FinalPrice * objOrderScanner_ResultModel_.Tax) / 100;
                                                objOrderScanner_ResultModel_.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel_.TaxAmount.ToString()).ToString("0.00"));
                                                objOrderScanner_ResultModel_.RowNo = GetRowNo();
                                                objOrderScanner_ResultModel_.IsVerifed = 1;
                                                Productdata_.Add(objOrderScanner_ResultModel_);

                                                #region LinkedUPCCode
                                                if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                {
                                                    LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, objOrderScanner_ResultModel_.Qty, 1, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                                }
                                                #endregion

                                                objOrderScanner_ResultModel_ = new OrderScanner_ResultModel();
                                                objOrderScanner_ResultModel_ = objOrderScanner_ResultModel;
                                                if (UnGroup > 0)
                                                {
                                                    decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                                    if (itemCount > UnGroup)
                                                    {
                                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);
                                                        #region LinkedUPCCode
                                                        if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                        {
                                                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                        }
                                                        #endregion
                                                        itemCount = 0;
                                                    }
                                                    UnGroup = UnGroup - itemCount;
                                                    for (int i = 0; i < UnGroup; i++)
                                                    {
                                                        LinkUPCCodeAdd(objOrderScanner_ResultModel_.UPCCode, 1, 0, "", objOrderScanner_ResultModel_.IsForceTax);

                                                        #region LinkedUPCCode
                                                        if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                        {
                                                            LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                                        }
                                                        #endregion
                                                    }
                                                }
                                                else
                                                {
                                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);
                                                    #region LinkedUPCCode
                                                    if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                    {
                                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                    }
                                                    #endregion
                                                }
                                            }
                                            else if (UnGroup > 0)
                                            {
                                                if (Group > 0)
                                                {
                                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1);
                                                    #region LinkedUPCCode
                                                    if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                    {
                                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                    }
                                                    #endregion
                                                }
                                                decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                                UnGroup = UnGroup - itemCount;
                                                for (int i = 0; i < UnGroup; i++)
                                                {
                                                    LinkUPCCodeAdd(objOrderScanner_ResultModel_.UPCCode, 1, 0, "", objOrderScanner_ResultModel_.IsForceTax);

                                                    #region LinkedUPCCode
                                                    if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                    {
                                                        LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                                    }
                                                    #endregion
                                                }
                                            }
                                            else
                                            {
                                                objOrderScanner_ResultModel.RowNo = GetRowNo();
                                                Productdata_.Add(objOrderScanner_ResultModel);
                                            }
                                            dataLoad();
                                        }
                                    }
                                    else if (IsGroup == true && LoginInfo.CasePrice == false && objOrderScanner_ResultModel.IsScale == true)
                                    {
                                        Group = 0;
                                        UnGroup = 0;
                                        decimal TotalItem_ = 0;
                                        decimal TotalItem = TotalItem_ = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false).Sum(item => item.Qty);
                                        decimal GroupItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true).Sum(item => item.Qty) / objOrderScanner_ResultModel.GroupQty;
                                        decimal Isvoid = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == true && x.CasePriceApplied == false).Sum(item => item.Qty);
                                        decimal? Isvoid_price = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == true && x.CasePriceApplied == false).Sum(item => item.FinalPrice);
                                        Isvoid = Math.Abs(Isvoid);
                                        TotalItem = TotalItem - Isvoid;
                                        TotalItem = TotalItem + objOrderScanner_ResultModel.Qty;
                                        TotalItem_ = TotalItem;
                                        for (int i = 0; TotalItem != 0; i++)
                                        {
                                            if (TotalItem >= objOrderScanner_ResultModel.GroupQty)
                                            {
                                                Group++;
                                                TotalItem -= objOrderScanner_ResultModel.GroupQty;
                                            }
                                            else
                                            {
                                                UnGroup = TotalItem;
                                                TotalItem = 0;
                                            }
                                        }
                                        //UnGroup += Isvoid;

                                        decimal groupPrice = 0;
                                        decimal groupQty = 0;
                                        decimal? UnGroupPrice = 0;
                                        decimal UnGroupQty = 0;

                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false);
                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false);

                                        groupQty = Group * objOrderScanner_ResultModel.GroupQty;
                                        UnGroupQty = UnGroup;
                                        groupPrice = Group * objOrderScanner_ResultModel.GroupPrice;
                                        UnGroupPrice = UnGroup * (objOrderScanner_ResultModel.GroupPrice / objOrderScanner_ResultModel.GroupQty);

                                        objOrderScanner_ResultModel.Qty = groupQty + UnGroupQty;

                                        if (objOrderScanner_ResultModel.Qty == objOrderScanner_ResultModel.GroupQty && Group > 0)
                                        {
                                            objOrderScanner_ResultModel.DiscountApplyed = true;
                                            objOrderScanner_ResultModel.FinalPrice = Functions.GetDecimal(Functions.GetDecimal((objOrderScanner_ResultModel.Qty * objOrderScanner_ResultModel.SellPrice).ToString()).ToString("0.00")) * (objOrderScanner_ResultModel.GroupPrice / objOrderScanner_ResultModel.GroupQty);
                                            objOrderScanner_ResultModel.FinalPrice = groupPrice;
                                        }
                                        else if (objOrderScanner_ResultModel.Qty >= objOrderScanner_ResultModel.GroupQty && Group > 0)
                                        {
                                            objOrderScanner_ResultModel.DiscountApplyed = true;
                                            objOrderScanner_ResultModel.FinalPrice = groupPrice + UnGroupPrice;
                                        }
                                        else
                                        {
                                            objOrderScanner_ResultModel.DiscountApplyed = false;
                                            objOrderScanner_ResultModel.FinalPrice = Functions.GetDecimal(Functions.GetDecimal((objOrderScanner_ResultModel.Qty * objOrderScanner_ResultModel.SellPrice).ToString()).ToString("0.00"));
                                        }
                                        objOrderScanner_ResultModel.FinalPrice = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.FinalPrice.ToString()).ToString("0.00")) + Math.Abs(Convert.ToDecimal(Isvoid_price.ToString()));
                                        objOrderScanner_ResultModel.Qty = objOrderScanner_ResultModel.Qty + Math.Abs(Isvoid);
                                        objOrderScanner_ResultModel.TaxAmount = (objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Tax) / 100;
                                        objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                                        AddItem = false;
                                        Group = 0;
                                        UnGroup = 0;
                                        objOrderScanner_ResultModel.RowNo = GetRowNo();
                                        Productdata_.Add(objOrderScanner_ResultModel);

                                        #region LinkedUPCCode
                                        if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                                        {
                                            txtSearchUPCCode.Text = (objOrderScanner_ResultModel.Qty).ToString() + "*" + objOrderScanner_ResultModel.LinkedUPCCode;
                                            LinkedParent = objOrderScanner_ResultModel.UPCCode;
                                            ProductAdd();
                                            LinkedParent = "";
                                        }
                                        #endregion
                                        dataLoad();
                                    }
                                    else
                                    {
                                        #region Tax Cal
                                        if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                                        {
                                            objOrderScanner_ResultModel.TaxAmount = (objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Tax) / 100;
                                            objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        }
                                        else if (objOrderScanner_ResultModel.IsScale == true)
                                        {
                                            objOrderScanner_ResultModel.TaxAmount = objOrderScanner_ResultModel.TaxAmount * objOrderScanner_ResultModel.Qty;
                                            objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        }
                                        else
                                        {
                                            objOrderScanner_ResultModel.TaxAmount = objOrderScanner_ResultModel.TaxAmount * objOrderScanner_ResultModel.Qty;
                                            objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        }
                                        #endregion

                                        #region CaseQty-CasePrice 20190710                                        
                                        if (LoginInfo.CasePrice)
                                        {
                                            if (objOrderScanner_ResultModel.CaseQty > 0 && objOrderScanner_ResultModel.CasePrice > 0)
                                            {
                                                IsGroup = false;
                                                objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.CasePrice * objOrderScanner_ResultModel.Qty;
                                                objOrderScanner_ResultModel.TaxAmount = (objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Tax) / 100;
                                                objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                                objOrderScanner_ResultModel.Qty = objOrderScanner_ResultModel.Qty * objOrderScanner_ResultModel.CaseQty;
                                                objOrderScanner_ResultModel.CasePriceApplied = true;
                                            }
                                            else
                                            {
                                                objOrderScanner_ResultModel.CasePriceApplied = false;
                                            }
                                            LoginInfo.CasePrice = false;
                                        }
                                        else
                                        {
                                            objOrderScanner_ResultModel.CasePriceApplied = false;
                                        }
                                        #endregion

                                        objOrderScanner_ResultModel.RowNo = GetRowNo(); Productdata_.Add(objOrderScanner_ResultModel);

                                        #region LinkedUPCCode
                                        if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                                        {
                                            txtSearchUPCCode.Text = objOrderScanner_ResultModel.LinkedUPCCode;
                                            if (Convert.ToInt32(objOrderScanner_ResultModel.Qty) > 1)
                                            {
                                                txtSearchUPCCode.Text = (objOrderScanner_ResultModel.Qty).ToString() + "*" + txtSearchUPCCode.Text;
                                            }
                                            LinkedParent = objOrderScanner_ResultModel.UPCCode;
                                            ProductAdd();
                                            LinkedParent = "";
                                            dataLoad();
                                        }
                                        #endregion
                                        dataLoad();
                                    }
                                }

                                //if (IsBeep == true)
                                //{
                                //    GetBeepSound();
                                //}
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "Can't sale this product.!", false);
                                LoginInfo.tnfn = false;
                                LoginInfo.CasePrice = false;
                            }
                        }
                        txtSearchUPCCode.Text = null;
                        txtSearchUPCCode.Focus();
                    }
                    #endregion
                }
                if (txtSearchUPCCode.Text != "@")
                {
                    txtSearchUPCCode.Focus();
                    txtSearchUPCCode.Text = CommonModelCont.EmptyString;
                }
                IsBeep = true;
            }
            catch (Exception ex)
            {
                //ClsCommon.MsgBox("Information", ex.GetType().Name + " ---------- " + ex.Message + " ---------- " + CommonTextBoxs.frmOrderScanner + ex.StackTrace + " ----------- " + ex.LineNumber());
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());

                txtSearchUPCCode.Focus();
                txtSearchUPCCode.Text = "";
                LoginInfo.tnfn = false;
                LoginInfo.CasePrice = false;
            }
        }

        public List<OrderScanner_ResultModel> UPCScanner(string UPCCode)
        {
            List<OrderScanner_ResultModel> lstOrderScanner_ResultModel = new List<OrderScanner_ResultModel>();
            try
            {
                OrderScanner_ResultModel _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                string _UPCCode = UPCCode;
                long _ProductID = 0;
                long _TaxGroupID = 0;
                decimal _SellPrice = 0;
                decimal _Discount = 0;
                decimal _Tax = 0;
                DataTable dt_P = new DataTable();
                if (LoginInfo.Connections)
                {
                    #region Live Connetion
                    SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(UPCCode));
                    if (dt_P.Rows.Count > 0)
                    {
                        #region Product Data
                        _OrderScanner_ResultModel.ProductID = _ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                        _OrderScanner_ResultModel.TaxGroupID = _TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                        _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                        _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                        _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString());
                        _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                        _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                        _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                        _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                        _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                        _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                        _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                        _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                        _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                        _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                        _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());
                        _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());
                        _OrderScanner_ResultModel.IsGroupPrice = Functions.GetBoolean(dt_P.Rows[0]["IsGroupPrice"].ToString());
                        _OrderScanner_ResultModel.ParentUPCCode = LinkedParent;
                        LoginInfo.TareWeight = Functions.GetDecimal(dt_P.Rows[0]["TareWeight"].ToString());

                        if (LoginInfo.tnfn)
                        {
                            _OrderScanner_ResultModel.IsFoodStamp = false;
                        }
                        else
                        {
                            _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());
                        }

                        if (_OrderScanner_ResultModel.AgeVerification == false)
                        {
                            #region Search
                            DataTable dt = new DataTable();

                            var query = from PS in _db.tbl_ProductSalePriceMaster
                                        where PS.ProductID == _ProductID && PS.StartDate <= DateTime.Now && PS.EndDate >= DateTime.Now
                                        select PS;

                            dt = ClsCommon.LinqToDataTable(query);


                            if (dt.Rows.Count > 0)
                            {
                                _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt.Rows[0]["SellPrice"].ToString());
                            }
                            dt.Dispose();

                            #region Tax Calculation
                            if (LoginInfo.tnfn)
                            {
                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                _Tax = LoginInfo.StoreDefaultTax;
                                _OrderScanner_ResultModel.IsTax = true;
                                LoginInfo.tnfn = false;
                            }
                            else
                            {
                                dt = new DataTable();
                                if (_TaxGroupID > 0)
                                {
                                    if (LoginInfo.IsStoreTax)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                    else
                                    {
                                        dt = TaxCalculation(_TaxGroupID);

                                        if (dt.Rows.Count > 0)
                                        {
                                            _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    _OrderScanner_ResultModel.IsTax = false;
                                }
                                dt.Dispose();
                            }
                            #endregion

                            dt_P.Dispose();

                            _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                            _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                            _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                            _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                            _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                            #endregion

                            lstOrderScanner_ResultModel.Add(_OrderScanner_ResultModel);
                        }
                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == false && LoginInfo.CashierAgeVerified == false)
                        {
                            #region AgeVerification Form Show
                            ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                            #endregion

                            if (AgeVerifidInfo.AgeVerified == true && AgeVerifidInfo.AgeChecked == true && LoginInfo.CashierAgeVerified == true)
                            {
                                #region Search

                                DataTable dt = new DataTable();

                                var query = from PS in _db.tbl_ProductSalePriceMaster
                                            where PS.ProductID == _ProductID && PS.StartDate <= DateTime.Now && PS.EndDate >= DateTime.Now
                                            select PS;

                                dt = ClsCommon.LinqToDataTable(query);

                                if (dt.Rows.Count > 0)
                                {
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt.Rows[0]["SellPrice"].ToString());
                                }
                                dt.Dispose();

                                #region Tax Calculation
                                if (LoginInfo.tnfn)
                                {
                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    _Tax = LoginInfo.StoreDefaultTax;
                                    _OrderScanner_ResultModel.IsTax = true;
                                    LoginInfo.tnfn = false;
                                }
                                else
                                {
                                    dt = new DataTable();
                                    if (_TaxGroupID > 0)
                                    {
                                        if (LoginInfo.IsStoreTax)
                                        {
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            dt = TaxCalculation(_TaxGroupID);
                                            if (dt.Rows.Count > 0)
                                            {
                                                _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                _OrderScanner_ResultModel.IsTax = true;
                                                LoginInfo.tnfn = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsTax = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        _OrderScanner_ResultModel.IsTax = false;
                                    }
                                    dt.Dispose();
                                }
                                #endregion


                                dt_P.Dispose();

                                _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());

                                #endregion

                                lstOrderScanner_ResultModel.Add(_OrderScanner_ResultModel);
                            }
                        }
                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == false && LoginInfo.CashierAgeVerified == true)
                        {
                            #region AgeVerification Form Show
                            ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                            #endregion
                        }
                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == true && LoginInfo.CashierAgeVerified == true)
                        {
                            #region Search
                            DataTable dt = new DataTable();

                            var query = from PS in _db.tbl_ProductSalePriceMaster
                                        where PS.ProductID == _ProductID && PS.StartDate <= DateTime.Now && PS.EndDate >= DateTime.Now
                                        select PS;

                            dt = ClsCommon.LinqToDataTable(query);

                            if (dt.Rows.Count > 0)
                            {
                                _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt.Rows[0]["SellPrice"].ToString());
                            }
                            dt.Dispose();

                            #region Tax Calculation
                            if (LoginInfo.tnfn)
                            {
                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                _Tax = LoginInfo.StoreDefaultTax;
                                _OrderScanner_ResultModel.IsTax = true;
                                LoginInfo.tnfn = false;
                            }
                            else
                            {
                                dt = new DataTable();
                                if (_TaxGroupID > 0)
                                {
                                    if (LoginInfo.IsStoreTax)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                    else
                                    {
                                        dt = TaxCalculation(_TaxGroupID);

                                        if (dt.Rows.Count > 0)
                                        {
                                            _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    _OrderScanner_ResultModel.IsTax = false;
                                }
                                dt.Dispose();
                            }
                            #endregion

                            dt_P.Dispose();
                            _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                            _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                            _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                            _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                            _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                            #endregion

                            lstOrderScanner_ResultModel.Add(_OrderScanner_ResultModel);
                        }
                        else
                        {
                            txtSearchUPCCode.Text = null;
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region Local Connetion
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                    "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                    "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.CaseQty,PM.CasePrice," +
                                    "PM.UnitMeasureID,PM.IsGroupPrice " +
                                    "FROM tbl_ProductMaster AS PM " +
                                    "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", _UPCCode);
                    DataAdapter.Fill(dt_P);
                    if (dt_P.Rows.Count > 0)
                    {
                        #region Product Data
                        _OrderScanner_ResultModel.ProductID = _ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                        _OrderScanner_ResultModel.TaxGroupID = _TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                        _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                        _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                        _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                        _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                        _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                        LoginInfo.TareWeight = Functions.GetDecimal(dt_P.Rows[0]["TareWeight"].ToString());

                        #region Product Others Fields
                        _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                        _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                        _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                        _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                        _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                        _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                        _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                        _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                        _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());
                        _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());
                        _OrderScanner_ResultModel.IsGroupPrice = Functions.GetBoolean(dt_P.Rows[0]["IsGroupPrice"].ToString());
                        _OrderScanner_ResultModel.ParentUPCCode = LinkedParent;

                        if (LoginInfo.tnfn)
                        {
                            _OrderScanner_ResultModel.IsFoodStamp = false;
                        }
                        else
                        {
                            _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());
                        }
                        #endregion

                        if (_OrderScanner_ResultModel.AgeVerification == false)
                        {
                            #region Search
                            DataTable dt = new DataTable();
                            dt = new DataTable();
                            query = "SELECT SellPrice FROM tbl_ProductSalePriceMaster WHERE ProductID = @ProductID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@ProductID_", _ProductID);
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                            DataAdapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt.Rows[0]["SellPrice"].ToString());
                            }
                            dt.Dispose();

                            #region Tax Calculation
                            if (LoginInfo.tnfn)
                            {
                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                _Tax = LoginInfo.StoreDefaultTax;
                                _OrderScanner_ResultModel.IsTax = true;
                                LoginInfo.tnfn = false;
                            }
                            else
                            {
                                dt = new DataTable();
                                if (_TaxGroupID > 0)
                                {
                                    if (LoginInfo.IsStoreTax)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                    else
                                    {
                                        query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                        DataAdapter = new SqlCeDataAdapter(query, conn);
                                        DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _TaxGroupID);
                                        DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                        DataAdapter.Fill(dt);
                                        if (dt.Rows.Count > 0)
                                        {
                                            _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    _OrderScanner_ResultModel.IsTax = false;
                                }
                                dt.Dispose();
                            }
                            #endregion

                            dt_P.Dispose();

                            _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                            _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                            _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                            _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                            _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                            #endregion

                            lstOrderScanner_ResultModel.Add(_OrderScanner_ResultModel);
                        }

                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == false && LoginInfo.CashierAgeVerified == false)
                        {
                            #region AgeVerification Form Show
                            ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                            #endregion

                            if (AgeVerifidInfo.AgeVerified == true && AgeVerifidInfo.AgeChecked == true && LoginInfo.CashierAgeVerified == true)
                            {
                                #region Search

                                DataTable dt = new DataTable();
                                dt = new DataTable();
                                query = "SELECT SellPrice FROM tbl_ProductSalePriceMaster WHERE ProductID = @ProductID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@ProductID_", _ProductID);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                DataAdapter.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt.Rows[0]["SellPrice"].ToString());
                                }
                                dt.Dispose();

                                #region Tax Calculation
                                if (LoginInfo.tnfn)
                                {
                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    _Tax = LoginInfo.StoreDefaultTax;
                                    _OrderScanner_ResultModel.IsTax = true;
                                    LoginInfo.tnfn = false;
                                }
                                else
                                {
                                    dt = new DataTable();
                                    if (_TaxGroupID > 0)
                                    {
                                        if (LoginInfo.IsStoreTax)
                                        {
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                            DataAdapter = new SqlCeDataAdapter(query, conn);
                                            DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _TaxGroupID);
                                            DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                            DataAdapter.Fill(dt);
                                            if (dt.Rows.Count > 0)
                                            {
                                                _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                _OrderScanner_ResultModel.IsTax = true;
                                                LoginInfo.tnfn = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsTax = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        _OrderScanner_ResultModel.IsTax = false;
                                    }
                                    dt.Dispose();
                                }
                                #endregion


                                dt_P.Dispose();

                                _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());

                                #endregion

                                lstOrderScanner_ResultModel.Add(_OrderScanner_ResultModel);
                            }
                        }

                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == false && LoginInfo.CashierAgeVerified == true)
                        {
                            #region AgeVerification Form Show
                            ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                            #endregion
                        }

                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == true && LoginInfo.CashierAgeVerified == true)
                        {
                            #region Search
                            DataTable dt = new DataTable();
                            dt = new DataTable();
                            query = "SELECT SellPrice FROM tbl_ProductSalePriceMaster WHERE ProductID = @ProductID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@ProductID_", _ProductID);
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                            DataAdapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt.Rows[0]["SellPrice"].ToString());
                            }
                            dt.Dispose();

                            #region Tax Calculation
                            if (LoginInfo.tnfn)
                            {
                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                _Tax = LoginInfo.StoreDefaultTax;
                                _OrderScanner_ResultModel.IsTax = true;
                                LoginInfo.tnfn = false;
                            }
                            else
                            {
                                dt = new DataTable();
                                if (_TaxGroupID > 0)
                                {
                                    if (LoginInfo.IsStoreTax)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                    else
                                    {
                                        query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                        DataAdapter = new SqlCeDataAdapter(query, conn);
                                        DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _TaxGroupID);
                                        DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                        DataAdapter.Fill(dt);
                                        if (dt.Rows.Count > 0)
                                        {
                                            _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    _OrderScanner_ResultModel.IsTax = false;
                                }
                                dt.Dispose();
                            }
                            #endregion

                            dt_P.Dispose();
                            _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                            _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                            _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                            _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                            _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());

                            #endregion

                            lstOrderScanner_ResultModel.Add(_OrderScanner_ResultModel);
                        }

                        else
                        {
                            txtSearchUPCCode.Text = null;
                        }
                        #endregion
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    #endregion
                }
                return lstOrderScanner_ResultModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                return lstOrderScanner_ResultModel;
            }
        }

        public List<OrderScanner_ResultModel> AddLabeledPrice(string UPCCode)
        {
            lblWeight.Text = "0.00 lb";
            List<OrderScanner_ResultModel> lstOrderScanner_ResultModel = new List<OrderScanner_ResultModel>();
            try
            {
                OrderScanner_ResultModel _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                string TempUPCCode = UPCCode;
                string _UPCCode = TempUPCCode;
                long _ProductID = 0;
                long _TaxGroupID = 0;
                decimal _SellPrice = 0;
                decimal _Discount = 0;
                decimal _Tax = 0;
                DataTable dt_P = new DataTable();

                if (UPCCode.Length > 5)
                {
                    string LastPrice = UPCCode.Substring(UPCCode.Length - 5, 5);
                    TempUPCCode = UPCCode.Replace(LastPrice, "00000");

                    if (TempUPCCode != CommonModelCont.DefaultUPC)
                    {
                        if (LoginInfo.Connections)
                        {
                            #region Live Connection

                            DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                            dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(TempUPCCode));

                            if (dt_P.Rows.Count > 0)
                            {
                                #region Product Data

                                _OrderScanner_ResultModel.Qty = 1;
                                _OrderScanner_ResultModel.IsScale = false;
                                _OrderScanner_ResultModel.ProductID = _ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                _OrderScanner_ResultModel.TaxGroupID = _TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                                _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                                _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                                _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString());
                                _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                                _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                                LoginInfo.TareWeight = Functions.GetDecimal(dt_P.Rows[0]["TareWeight"].ToString());

                                _OrderScanner_ResultModel.ParentUPCCode = LinkedParent;
                                _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                                _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                                _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                                _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                                _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());
                                _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());
                                _OrderScanner_ResultModel.IsGroupPrice = Functions.GetBoolean(dt_P.Rows[0]["IsGroupPrice"].ToString());
                                if (LoginInfo.tnfn)
                                {
                                    _OrderScanner_ResultModel.IsFoodStamp = false;
                                }
                                else
                                {
                                    if (String.IsNullOrEmpty(dt_P.Rows[0][OrderScanner_ResultModelCont.IsFoodStamp].ToString()))
                                    {
                                        _OrderScanner_ResultModel.IsFoodStamp = false;
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0][OrderScanner_ResultModelCont.IsFoodStamp].ToString());
                                    }
                                }

                                if (_OrderScanner_ResultModel.AgeVerification == false)
                                {
                                    #region Search
                                    LastPrice = LastPrice.Remove(0, 1);
                                    LastPrice = LastPrice.Insert(2, ".");

                                    _SellPrice = Functions.GetDecimal(LastPrice);
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice;

                                    #region Tax Calculation

                                    if (LoginInfo.tnfn)
                                    {
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                    }
                                    else
                                    {
                                        DataTable dt = new DataTable();

                                        if (_TaxGroupID > 0)
                                        {
                                            if (LoginInfo.IsStoreTax)
                                            {
                                                _Tax = LoginInfo.StoreDefaultTax;
                                                _OrderScanner_ResultModel.IsTax = true;
                                                LoginInfo.tnfn = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                            else
                                            {
                                                dt = TaxCalculation(_TaxGroupID);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                    LoginInfo.tnfn = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                                else
                                                {
                                                    _OrderScanner_ResultModel.IsTax = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                        dt.Dispose();
                                    }
                                    #endregion

                                    dt_P.Dispose();

                                    _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                    _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                    _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                    _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                    _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                    _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                                    #endregion
                                }
                                else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == false)
                                {
                                    #region AgeVerification Form Show
                                    ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                                    #endregion

                                    if (AgeVerifidInfo.AgeVerified == true && AgeVerifidInfo.AgeChecked == true && LoginInfo.CashierAgeVerified == true)
                                    {
                                        #region Search

                                        LastPrice = LastPrice.Remove(0, 1);
                                        LastPrice = LastPrice.Insert(2, ".");

                                        _SellPrice = Functions.GetDecimal(LastPrice);
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice;

                                        #region Tax Calculation
                                        if (LoginInfo.tnfn)
                                        {
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                        }
                                        else
                                        {
                                            DataTable dt = new DataTable();
                                            if (_TaxGroupID > 0)
                                            {
                                                if (LoginInfo.IsStoreTax)
                                                {
                                                    _Tax = LoginInfo.StoreDefaultTax;
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                    LoginInfo.tnfn = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                                else
                                                {
                                                    dt = TaxCalculation(_TaxGroupID);

                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                        _OrderScanner_ResultModel.IsTax = true;
                                                        LoginInfo.tnfn = false;
                                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                    }
                                                    else
                                                    {
                                                        _OrderScanner_ResultModel.IsTax = false;
                                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                _OrderScanner_ResultModel.IsTax = false;
                                            }
                                            dt.Dispose();
                                        }
                                        #endregion

                                        dt_P.Dispose();

                                        _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                        _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                        _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                        _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                        _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                                        #endregion
                                    }
                                }
                                else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == false && LoginInfo.CashierAgeVerified == true)
                                {
                                    #region AgeVerification Form Show
                                    ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                                    #endregion
                                }
                                else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == true && LoginInfo.CashierAgeVerified == true)
                                {
                                    #region Search
                                    //int FirstChar = Convert.ToInt32(LastPrice.Substring(0, 1));
                                    //FirstChar = FirstChar - 1;
                                    //LastPrice = LastPrice.Remove(0, 1);
                                    //LastPrice = LastPrice.Insert(LastPrice.Length - FirstChar, ".");

                                    LastPrice = LastPrice.Remove(0, 1);
                                    LastPrice = LastPrice.Insert(2, ".");

                                    _SellPrice = Functions.GetDecimal(LastPrice);
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice;

                                    #region Tax Calculation
                                    if (LoginInfo.tnfn)
                                    {
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                    }
                                    else
                                    {
                                        DataTable dt = new DataTable();
                                        if (_TaxGroupID > 0)
                                        {
                                            if (LoginInfo.IsStoreTax)
                                            {
                                                _Tax = LoginInfo.StoreDefaultTax;
                                                _OrderScanner_ResultModel.IsTax = true;
                                                LoginInfo.tnfn = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                            else
                                            {
                                                dt = TaxCalculation(_TaxGroupID);

                                                if (dt.Rows.Count > 0)
                                                {
                                                    _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                    LoginInfo.tnfn = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                                else
                                                {
                                                    _OrderScanner_ResultModel.IsTax = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                        dt.Dispose();
                                    }
                                    #endregion

                                    dt_P.Dispose();

                                    _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                    _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                    _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                    _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                    _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                    _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                                    #endregion
                                }
                                else
                                {
                                    txtSearchUPCCode.Text = null;
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region Local Connetion
                            if (conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }

                            string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                            "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                            "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp," +
                                            "PM.UnitMeasureID,PM.CaseQty,PM.CasePrice,PM.IsGroupPrice " +
                                            "FROM tbl_ProductMaster AS PM " +
                                            "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", _UPCCode);
                            DataAdapter.Fill(dt_P);
                            if (dt_P.Rows.Count > 0)
                            {
                                #region Product Data

                                _OrderScanner_ResultModel.Qty = 1;
                                _OrderScanner_ResultModel.IsScale = false;
                                _OrderScanner_ResultModel.ProductID = _ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                _OrderScanner_ResultModel.TaxGroupID = _TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                                _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                                _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                                _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                                _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                                _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                                LoginInfo.TareWeight = Functions.GetDecimal(dt_P.Rows[0]["TareWeight"].ToString());

                                _OrderScanner_ResultModel.ParentUPCCode = LinkedParent;
                                _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                                _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                                _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                                _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                                _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());
                                _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());
                                _OrderScanner_ResultModel.IsGroupPrice = Functions.GetBoolean(dt_P.Rows[0]["IsGroupPrice"].ToString());
                                if (LoginInfo.tnfn)
                                {
                                    _OrderScanner_ResultModel.IsFoodStamp = false;
                                }
                                else
                                {
                                    if (String.IsNullOrEmpty(dt_P.Rows[0][OrderScanner_ResultModelCont.IsFoodStamp].ToString()))
                                    {
                                        _OrderScanner_ResultModel.IsFoodStamp = false;
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0][OrderScanner_ResultModelCont.IsFoodStamp].ToString());
                                    }
                                }

                                if (_OrderScanner_ResultModel.AgeVerification == false)
                                {
                                    #region Search
                                    //int FirstChar = Convert.ToInt32(LastPrice.Substring(0, 1));
                                    //FirstChar = FirstChar - 1;
                                    //LastPrice = LastPrice.Remove(0, 1);
                                    //LastPrice = LastPrice.Insert(2, ".");

                                    LastPrice = LastPrice.Remove(0, 1);
                                    LastPrice = LastPrice.Insert(2, ".");

                                    _SellPrice = Functions.GetDecimal(LastPrice);
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice;

                                    #region Tax Calculation

                                    if (LoginInfo.tnfn)
                                    {
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                    }
                                    else
                                    {
                                        DataTable dt = new DataTable();

                                        if (_TaxGroupID > 0)
                                        {
                                            if (LoginInfo.IsStoreTax)
                                            {
                                                _Tax = LoginInfo.StoreDefaultTax;
                                                _OrderScanner_ResultModel.IsTax = true;
                                                LoginInfo.tnfn = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                            else
                                            {
                                                query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                                DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _TaxGroupID);
                                                DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                                DataAdapter.Fill(dt);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                    LoginInfo.tnfn = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                                else
                                                {
                                                    _OrderScanner_ResultModel.IsTax = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                        dt.Dispose();
                                    }
                                    #endregion

                                    dt_P.Dispose();

                                    _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                    _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                    _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                    _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                    _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                    _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                                    #endregion
                                }
                                else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == false)
                                {
                                    #region AgeVerification Form Show
                                    ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                                    #endregion

                                    if (AgeVerifidInfo.AgeVerified == true && AgeVerifidInfo.AgeChecked == true && LoginInfo.CashierAgeVerified == true)
                                    {
                                        #region Search
                                        //int FirstChar = Convert.ToInt32(LastPrice.Substring(0, 1));
                                        //FirstChar = FirstChar - 1;
                                        //LastPrice = LastPrice.Remove(0, 1);
                                        //LastPrice = LastPrice.Insert(LastPrice.Length - FirstChar, ".");

                                        LastPrice = LastPrice.Remove(0, 1);
                                        LastPrice = LastPrice.Insert(2, ".");

                                        _SellPrice = Functions.GetDecimal(LastPrice);
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice;

                                        #region Tax Calculation
                                        if (LoginInfo.tnfn)
                                        {
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                        }
                                        else
                                        {
                                            DataTable dt = new DataTable();
                                            if (_TaxGroupID > 0)
                                            {
                                                if (LoginInfo.IsStoreTax)
                                                {
                                                    _Tax = LoginInfo.StoreDefaultTax;
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                    LoginInfo.tnfn = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                                else
                                                {
                                                    query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                                    DataAdapter = new SqlCeDataAdapter(query, conn);
                                                    DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _TaxGroupID);
                                                    DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                                    DataAdapter.Fill(dt);
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                        _OrderScanner_ResultModel.IsTax = true;
                                                        LoginInfo.tnfn = false;
                                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                    }
                                                    else
                                                    {
                                                        _OrderScanner_ResultModel.IsTax = false;
                                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                _OrderScanner_ResultModel.IsTax = false;
                                            }
                                            dt.Dispose();
                                        }
                                        #endregion

                                        dt_P.Dispose();

                                        _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                        _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                        _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                        _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                        _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                                        #endregion
                                    }
                                }
                                else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == false && LoginInfo.CashierAgeVerified == true)
                                {
                                    #region AgeVerification Form Show
                                    ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                                    #endregion
                                }
                                else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == true && LoginInfo.CashierAgeVerified == true)
                                {
                                    #region Search
                                    //int FirstChar = Convert.ToInt32(LastPrice.Substring(0, 1));
                                    //FirstChar = FirstChar - 1;
                                    //LastPrice = LastPrice.Remove(0, 1);
                                    //LastPrice = LastPrice.Insert(LastPrice.Length - FirstChar, ".");

                                    LastPrice = LastPrice.Remove(0, 1);
                                    LastPrice = LastPrice.Insert(2, ".");

                                    _SellPrice = Functions.GetDecimal(LastPrice);
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice;

                                    #region Tax Calculation
                                    if (LoginInfo.tnfn)
                                    {
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                    }
                                    else
                                    {
                                        DataTable dt = new DataTable();
                                        if (_TaxGroupID > 0)
                                        {
                                            if (LoginInfo.IsStoreTax)
                                            {
                                                _Tax = LoginInfo.StoreDefaultTax;
                                                _OrderScanner_ResultModel.IsTax = true;
                                                LoginInfo.tnfn = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                            else
                                            {
                                                query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                                DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _TaxGroupID);
                                                DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                                DataAdapter.Fill(dt);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                    LoginInfo.tnfn = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                                else
                                                {
                                                    _OrderScanner_ResultModel.IsTax = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                        dt.Dispose();
                                    }
                                    #endregion

                                    dt_P.Dispose();

                                    _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                    _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                    _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                    _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                    _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                    _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice).ToString());
                                    #endregion
                                }
                                else
                                {
                                    txtSearchUPCCode.Text = null;
                                }
                                #endregion
                            }

                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                            #endregion
                        }
                    }
                }

                if (_OrderScanner_ResultModel.DepartmentID != null && _OrderScanner_ResultModel.UPCCode != null && _OrderScanner_ResultModel.LabeledPrice == true)
                    lstOrderScanner_ResultModel.Add(_OrderScanner_ResultModel);
                return lstOrderScanner_ResultModel;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                return lstOrderScanner_ResultModel;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Alt | Keys.S))
                {
                    FrmShortcutKey objFrmShortcutKey = new FrmShortcutKey();
                    objFrmShortcutKey.ShowDialog();
                    return true;
                }
                else if (keyData == (Keys.Alt | Keys.P))
                {
                    FrmProduct_ objfrmProductDetail = new FrmProduct_();
                    objfrmProductDetail.ShowDialog();
                    return true;
                }
                else if (keyData == (Keys.Alt | Keys.O))
                {
                    FrmOrderHistory objfrmOrderHistory = new FrmOrderHistory();
                    objfrmOrderHistory.ShowDialog();
                    return true;
                }
                else if (keyData == (Keys.F2))
                {
                   DeviceRemove();
                    frmSettings objMenuSettings = new frmSettings();
                    objMenuSettings.ShowDialog();
                    DeviceAdd();
                    return true;
                }
                else if (keyData == (Keys.F5))
                {
                    DialogResult result = MessageBox.Show("Do you want to sync data with the server?", "Data Sync", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.OK))
                    {
                        if (CurrentIndex == 0)
                        {
                            LoginInfo.SyncType = 2;
                            backgroundWorker_DataSync.RunWorkerAsync();
                        }
                    }
                    return true;
                }
                else if (keyData == Keys.F4)
                {
                    FrmReEnterPwd obj = new FrmReEnterPwd();
                    obj.ShowDialog();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void dataLoad()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(ProductMasterModelCont.UPCCode, typeof(string));
                dt.Columns.Add(ProductMasterModelCont.ProductID, typeof(long));
                dt.Columns.Add(ProductMasterModelCont.ProductName, typeof(string));
                dt.Columns.Add(OrderScanner_ResultModelCont.Qty, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.SellPrice, typeof(string));
                dt.Columns.Add(OrderScanner_ResultModelCont.SalePrice, typeof(string));
                dt.Columns.Add(OrderScanner_ResultModelCont.Tax, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.Discount, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.DiscountAmount, typeof(string));
                dt.Columns.Add(OrderScanner_ResultModelCont.TaxAmount, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.FinalPrice, typeof(string));
                dt.Columns.Add(DepartmentMasterModelCont.DepartmentID, typeof(long));
                dt.Columns.Add(SectionMasterModelCont.SectionID, typeof(long));
                dt.Columns.Add(TaxGroupMasterModelCont.TaxGroupID, typeof(long));
                dt.Columns.Add(UoMMasterModelCont.UnitMeasureID, typeof(long));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsScale, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsFoodStamp, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsTax, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.DiscountApplyed, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsVoid, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsRefund, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.IsForceTax, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.CasePriceApplied, typeof(bool));
                dt.Columns.Add(OrderScanner_ResultModelCont.Abb, typeof(string));
                dt.Columns.Add(OrderScanner_ResultModelCont.Image, typeof(System.Byte[]));
                dt.Columns.Add(OrderScanner_ResultModelCont.FoodStampTotal, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.GroupPrice, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.GroupQty, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.CasePrice, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.CaseQty, typeof(decimal));
                dt.Columns.Add(OrderScanner_ResultModelCont.LinkedUPCCode, typeof(string));
                dt.Columns.Add(OrderScanner_ResultModelCont.ParentUPCCode, typeof(string));

                foreach (var item in Productdata_)
                {
                    DataRow dr = dt.NewRow();
                    if (item.UPCCode != "")
                    {
                        dr[ProductMasterModelCont.UPCCode] = item.UPCCode;
                        dr[ProductMasterModelCont.ProductID] = item.ProductID;
                        dr[ProductMasterModelCont.ProductName] = item.ProductName + " (" + item.UPCCode + ")";
                        dr[OrderScanner_ResultModelCont.Qty] = item.Qty;
                        dr[OrderScanner_ResultModelCont.SellPrice] = Functions.GetDisplayAmount(item.SellPrice.ToString());
                        dr[OrderScanner_ResultModelCont.SalePrice] = Functions.GetDisplayAmount(item.SellPrice.ToString());
                        dr[OrderScanner_ResultModelCont.Tax] = item.Tax == null ? 0 : item.Tax;
                        dr[OrderScanner_ResultModelCont.Discount] = item.Discount == null ? 0 : item.Discount;
                        dr[OrderScanner_ResultModelCont.DiscountAmount] = Functions.GetDisplayAmount(String.IsNullOrEmpty(item.DiscountAmount.ToString()) ? "0" : item.DiscountAmount.ToString());
                        dr[OrderScanner_ResultModelCont.TaxAmount] = item.TaxAmount == null ? 0 : item.TaxAmount;
                        dr[OrderScanner_ResultModelCont.FinalPrice] = Functions.GetDisplayAmount(item.FinalPrice.ToString());
                        dr[OrderScanner_ResultModelCont.IsRefund] = item.IsRefund;
                        dr[OrderScanner_ResultModelCont.GroupPrice] = item.GroupPrice;
                        dr[OrderScanner_ResultModelCont.GroupQty] = item.GroupQty;
                        dr[OrderScanner_ResultModelCont.CasePrice] = item.CasePrice;
                        dr[OrderScanner_ResultModelCont.CaseQty] = item.CaseQty;

                        if (item.IsVoid)
                        {
                            dr[OrderScanner_ResultModelCont.IsVoid] = true;
                        }
                        else
                        {
                            dr[OrderScanner_ResultModelCont.IsVoid] = false;
                        }
                        dr[OrderScanner_ResultModelCont.IsForceTax] = item.IsForceTax == null ? false : item.IsForceTax;
                        dr[DepartmentMasterModelCont.DepartmentID] = item.DepartmentID == null ? 0 : item.DepartmentID;
                        dr[SectionMasterModelCont.SectionID] = item.SectionID == null ? 0 : item.SectionID;
                        dr[TaxGroupMasterModelCont.TaxGroupID] = item.TaxGroupID == null ? 0 : item.TaxGroupID;
                        dr[UoMMasterModelCont.UnitMeasureID] = item.UnitMeasureID == null ? 0 : item.UnitMeasureID;
                        if (item.IsScale == null)
                        {
                            dr[OrderScanner_ResultModelCont.IsScale] = 0;
                        }
                        else
                        {
                            dr[OrderScanner_ResultModelCont.IsScale] = item.IsScale;
                        }
                        if (item.IsFoodStamp == null)
                        {
                            dr[OrderScanner_ResultModelCont.IsFoodStamp] = 0;
                        }
                        else
                        {
                            dr[OrderScanner_ResultModelCont.IsFoodStamp] = item.IsFoodStamp;
                        }
                        if (item.IsTax == null)
                        {
                            dr[OrderScanner_ResultModelCont.IsTax] = 0;
                        }
                        else
                        {
                            dr[OrderScanner_ResultModelCont.IsTax] = item.IsTax;
                        }
                        if (item.CasePriceApplied == null)
                        {
                            dr[OrderScanner_ResultModelCont.CasePriceApplied] = 0;
                        }
                        else
                        {
                            dr[OrderScanner_ResultModelCont.CasePriceApplied] = item.CasePriceApplied;
                        }

                        dr[OrderScanner_ResultModelCont.DiscountApplyed] = item.DiscountApplyed;

                        dr[OrderScanner_ResultModelCont.LinkedUPCCode] = item.LinkedUPCCode;

                        if (item.ParentUPCCode == "" && item.ParentUPCCode == null)
                        {
                            dr[OrderScanner_ResultModelCont.ParentUPCCode] = "";
                        }
                        else
                        {
                            dr[OrderScanner_ResultModelCont.ParentUPCCode] = item.ParentUPCCode;
                        }
                        if (item.DiscountApplyed == true)
                        {
                            dr[ProductMasterModelCont.ProductName] = item.ProductName + "(" + item.UPCCode + ")" + " - Group Discount";
                            if (item.IsScale == true)
                            {
                                dr[OrderScanner_ResultModelCont.SalePrice] = item.GroupQty + " For $" + item.GroupPrice;
                            }
                            else
                            {
                                dr[OrderScanner_ResultModelCont.SalePrice] = Convert.ToInt32(Math.Floor(Convert.ToDecimal(item.GroupQty))) + " For $" + item.GroupPrice;
                            }
                        }

                        #region ADD ABBRIVIATIONS
                        if (item.IsScale == true)
                        {
                            if (item.IsFoodStamp == true && item.IsTax == true)
                            {
                                dr[OrderScanner_ResultModelCont.Abb] = "TF";
                            }
                            else if (item.IsFoodStamp == false)
                            {
                                dr[OrderScanner_ResultModelCont.Abb] = "FA";
                            }
                            else
                            {
                                dr[OrderScanner_ResultModelCont.Abb] = "FC";
                            }
                        }
                        else
                        {
                            if (item.IsFoodStamp == true && item.IsTax == true)
                            {
                                dr[OrderScanner_ResultModelCont.Abb] = "TF";
                            }
                            else if (item.IsFoodStamp == true)
                            {
                                dr[OrderScanner_ResultModelCont.Abb] = "F";
                            }
                            else if (item.IsTax == true)
                            {
                                dr[OrderScanner_ResultModelCont.Abb] = "T";
                            }
                            else if (item.IsTax == false && item.UPCCode == "")
                            {
                                dr[OrderScanner_ResultModelCont.Abb] = "";
                            }
                            else
                            {
                                dr[OrderScanner_ResultModelCont.Abb] = "FC";
                            }
                        }
                        #endregion

                        #region FLAG FOR FROCETAX
                        if (item.IsForceTax == true)
                        {
                            var imageConverter = new ImageConverter();
                            dr[OrderScanner_ResultModelCont.Image] = imageConverter.ConvertTo(Resources.Taxflag, Type.GetType("System.Byte[]"));
                        }
                        else
                        {
                            var imageConverter = new ImageConverter();
                            dr[OrderScanner_ResultModelCont.Image] = imageConverter.ConvertTo(Resources.transparent, System.Type.GetType("System.Byte[]"));
                        }
                        #endregion

                        dt.Rows.Add(dr);
                    }
                }
                dataGridProducts.DataSource = dt;

                #region DESIGN
                dataGridProducts.Columns[ProductMasterModelCont.ProductName].Width = 450;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Qty].Width = 100;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.SalePrice].Width = 150;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.FinalPrice].Width = 150;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Abb].Width = 40;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Image].Width = 40;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.SalePrice].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.SalePrice].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.FinalPrice].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.FinalPrice].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Qty].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridProducts.ColumnHeadersHeight = 50;
                dataGridProducts.RowTemplate.MinimumHeight = 50;
                #endregion

                itemCount();

                dataGridProducts.ClearSelection();
                int nRowIndex = dataGridProducts.Rows.Count - 1;

                if (dataGridProducts.RowCount >= 1)
                {
                    dataGridProducts.ScrollBars = ScrollBars.Both;
                    dataGridProducts.Rows[nRowIndex].Selected = true;
                    dataGridProducts.FirstDisplayedScrollingRowIndex = dataGridProducts.RowCount - 1;
                }
                else
                {
                    dataGridProducts.ScrollBars = ScrollBars.None;
                }
                ChangeColor();
                txtSearchUPCCode.Text = CommonModelCont.EmptyString;
                txtSearchUPCCode.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void itemCount()
        {
            try
            {
                decimal count = 0;
                decimal SubTotal = 0;
                decimal TaxAmount = 0;
                decimal Total = 0;
                string te = "0";
                decimal ForceTaxAmount = 0;
                FSEligibleAmt = 0;
                TaxableAmount = 0;
                FSEligibleVoidAmt = 0;
                
                for (int i = 0; i < dataGridProducts.Rows.Count; i++)
                {
                    if (dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value != null)
                    {
                        te = (dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value).ToString().Replace(CommonModelCont.AddDollorSign, string.Empty);
                    }
                    SubTotal += Functions.GetDecimal(te);
                    TaxAmount += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());

                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsScale].Value.ToString()) == false)
                    {
                        count += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                    }
                    else
                    {
                        if (Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString()) > 0)
                        {
                            count += 1;
                        }
                        else
                        {
                            count -= 1;
                        }
                    }
                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString()) == true)
                    {
                        FSEligibleAmt += Functions.GetDecimal(te);
                    }
                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                    {
                        TaxableAmount += Functions.GetDecimal(te);
                    }
                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString()) == true
                       && Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                    {

                        FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                    }
                    else if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                    {
                        FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                    }
                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsForceTax].Value.ToString()) == true
                       && Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                    {
                        ForceTaxAmount += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                    }
                }
                //TaxCarry
                if (LoginInfo.TaxCarry)
                {
                    //OrderInfo.TaxCarryAmount = TaxAmount;
                    //TaxAmount = 0;

                    OrderInfo.TaxCarryAmount = TaxAmount - ForceTaxAmount;
                    TaxAmount = ForceTaxAmount;
                }
                Total += SubTotal + TaxAmount;
                if (count > 0)
                {
                    lblTotalCount.Text = count.ToString();
                }
                else
                {
                    lblTotalCount.Text = "0";
                }
                lblSubTotal.Text = Functions.GetDisplayAmount(SubTotal.ToString());
                lblTaxAmount.Text = Functions.GetDisplayAmount(TaxAmount.ToString());
                lblFinalAmount.Text = Functions.GetDisplayAmount(Total.ToString());
                lblFSTotal.Text = Functions.GetDisplayAmount("0.00");
                //ClsCommon.MsgBox("Information",FSEligibleVoidAmt.ToString());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void ageVerifi(long DepartmentID, long SectionID)
        {
            try
            {
                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                DataTable dt = new DataTable();
                if (LoginInfo.Connections)
                {
                    var queryDepartment = from DT in _db.tbl_DepartmentMaster
                                          where DT.DepartmentID == DepartmentID
                                          select DT;

                    dt = ClsCommon.LinqToDataTable(queryDepartment);

                    //string query = "";
                    //query = "SELECT AgeVarificationAge FROM tbl_DepartmentMaster WHERE DepartmentID=@DepartmentID";
                    //DataAdapter = new SqlCeDataAdapter(query, conn);
                    //DataAdapter.SelectCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                    //DataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        AgeVerifidInfo.DepartmentAge = Functions.GetLong(dt.Rows[0]["AgeVarificationAge"].ToString());
                    }
                    dt.Dispose();
                    dt = new DataTable();
                    //query = "SELECT AgeVarificationAge FROM tbl_SectionMaster WHERE SectionID = @SectionID";
                    //DataAdapter = new SqlCeDataAdapter(query, conn);
                    //DataAdapter.SelectCommand.Parameters.AddWithValue("@SectionID", SectionID);
                    //DataAdapter.Fill(dt);

                    var querySection = from DT in _db.tbl_SectionMaster
                                       where DT.SectionID == SectionID
                                       select DT;

                    dt = ClsCommon.LinqToDataTable(querySection);

                    if (dt.Rows.Count > 0)
                    {
                        AgeVerifidInfo.SectionAge = Functions.GetLong(dt.Rows[0]["AgeVarificationAge"].ToString());
                    }
                    dt.Dispose();
                    dt = new DataTable();

                    //query = "SELECT AgeVarificationAge FROM tbl_StoreMaster WHERE StoreID = @StoreID";
                    //DataAdapter = new SqlCeDataAdapter(query, conn);
                    //DataAdapter.SelectCommand.Parameters.AddWithValue("@StoreID", LoginInfo.StoreID);
                    //DataAdapter.Fill(dt);

                    var queryStore = from DT in _db.tbl_StoreMaster
                                     where DT.StoreID == LoginInfo.StoreID
                                     select DT;

                    dt = ClsCommon.LinqToDataTable(queryStore);

                    if (dt.Rows.Count > 0)
                    {
                        AgeVerifidInfo.StoreAge = Functions.GetLong(dt.Rows[0]["AgeVarificationAge"].ToString());
                    }
                }
                else
                {
                    string query = "";
                    query = "SELECT AgeVarificationAge FROM tbl_DepartmentMaster WHERE DepartmentID=@DepartmentID";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                    DataAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        AgeVerifidInfo.DepartmentAge = Functions.GetLong(dt.Rows[0]["AgeVarificationAge"].ToString());
                    }
                    dt.Dispose();

                    dt = new DataTable();
                    query = "SELECT AgeVarificationAge FROM tbl_SectionMaster WHERE SectionID = @SectionID";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@SectionID", SectionID);
                    DataAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        AgeVerifidInfo.SectionAge = Functions.GetLong(dt.Rows[0]["AgeVarificationAge"].ToString());
                    }
                    dt.Dispose();
                    dt = new DataTable();

                    query = "SELECT AgeVarificationAge FROM tbl_StoreMaster WHERE StoreID = @StoreID";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@StoreID", LoginInfo.StoreID);
                    DataAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        AgeVerifidInfo.StoreAge = Functions.GetLong(dt.Rows[0]["AgeVarificationAge"].ToString());
                    }
                }
                FrmAgeVerification _FrmAgeVerification = new FrmAgeVerification();
                _FrmAgeVerification.ShowDialog();

                if (AgeVerifidInfo.SectionAge > 0)
                {
                    if (LoginInfo.CashierAge < AgeVerifidInfo.SectionAge)
                    {
                        FrmCancelTransaction objFrmCancelTransaction = new FrmCancelTransaction();
                        objFrmCancelTransaction.ShowDialog();
                        if (objFrmCancelTransaction.IsCancel == true)
                        {
                            LoginInfo.CashierAgeVerified = true;
                        }
                    }
                    else
                    {
                        LoginInfo.CashierAgeVerified = true;
                    }
                }
                else if (AgeVerifidInfo.DepartmentAge > 0)
                {
                    if (LoginInfo.CashierAge < AgeVerifidInfo.DepartmentAge)
                    {
                        FrmCancelTransaction objFrmCancelTransaction = new FrmCancelTransaction();
                        objFrmCancelTransaction.ShowDialog();
                        if (objFrmCancelTransaction.IsCancel == true)
                        {
                            LoginInfo.CashierAgeVerified = true;
                        }
                    }
                    else
                    {
                        LoginInfo.CashierAgeVerified = true;
                    }
                }
                else if (AgeVerifidInfo.StoreAge > 0)
                {
                    if (LoginInfo.CashierAge < AgeVerifidInfo.StoreAge)
                    {
                        FrmCancelTransaction objFrmCancelTransaction = new FrmCancelTransaction();
                        objFrmCancelTransaction.ShowDialog();
                        if (objFrmCancelTransaction.IsCancel == true)
                        {
                            LoginInfo.CashierAgeVerified = true;
                        }
                    }
                    else
                    {
                        LoginInfo.CashierAgeVerified = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void GenerateOrder()
        {
            try
            {
                if (Productdata_.Count != 0 || dataGridProducts.RowCount > 0)
                {
                    Printer printer = new Printer(XMLData.PrinterName, PrinterType.Epson);
                    printer.LowPaper();
                    string status = printer.GetStatus();
                    if (status == "1024" || status == "0" || ClsCommon.CheckPrinterRoll == false)
                    {
                        if (CheckConnection())
                        {
                            #region InsertOrder
                            objOrderMasterModel = new OrderMasterModel();
                            objOrderMasterModel.CardNumber = CommonModelCont.EmptyString;
                            objOrderMasterModel.CustomerID = 0;
                            objOrderMasterModel.PaymentMethodID = OrderInfo.PaymentType;
                            objOrderMasterModel.StoreID = LoginInfo.StoreID;
                            objOrderMasterModel.Status = AlertMessages.ConfirmedStatus;
                            objOrderMasterModel.TotalAmount = Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) + Functions.GetDecimal((lblFSTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                            objOrderMasterModel.GrossAmount = Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                            objOrderMasterModel.TaxAmount = Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                            objOrderMasterModel.TaxableAmount = TaxableAmount;
                            if ((Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))) < 0)
                            {
                                objOrderMasterModel.RefundAmount = OrderInfo.CashAmt + OrderInfo.CheckAmt + OrderInfo.CreditAmt;
                                objOrderMasterModel.CashAmount = -(OrderInfo.CashAmt);
                                objOrderMasterModel.CheckAmount = -(OrderInfo.CheckAmt);
                                objOrderMasterModel.CreditCardAmount = -(OrderInfo.CreditAmt);
                            }
                            else
                            {
                                objOrderMasterModel.CashAmount = OrderInfo.CashAmt;
                                objOrderMasterModel.CheckAmount = OrderInfo.CheckAmt;
                                objOrderMasterModel.CreditCardAmount = OrderInfo.CreditAmt;
                                objOrderMasterModel.RefundAmount = 0;
                            }
                            objOrderMasterModel.ChangeAmount = Functions.GetDecimal(OrderInfo.Change.ToString());
                            if (OrderInfo.PaymentType == 1)
                            {
                                if (OrderInfo.FSTotal != 0 && OrderInfo.CheckAmt != 0 && OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.FSTotal - OrderInfo.CheckAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CheckAmt != 0 && OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.CheckAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CheckAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0)
                                {
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CheckAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                            }
                            else if (OrderInfo.PaymentType == 2)
                            {
                                if (OrderInfo.FSTotal != 0 && OrderInfo.CashAmt != 0 && OrderInfo.CheckAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt - OrderInfo.CashAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CashAmt != 0 && OrderInfo.CheckAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CashAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - OrderInfo.CashAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CheckAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0)
                                {
                                    if (Balance > 0)
                                    {
                                        objOrderMasterModel.Balance = Balance;
                                    }
                                }
                                else if (OrderInfo.CashAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CheckAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - OrderInfo.CheckAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else
                                {
                                    objOrderMasterModel.Balance = Balance;
                                }
                            }
                            else if (OrderInfo.PaymentType == 3)
                            {
                                if (OrderInfo.FSTotal != 0 && OrderInfo.CashAmt != 0 && OrderInfo.CreditAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.FSTotal - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CashAmt != 0 && OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CashAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CashAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;

                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CreditAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.FSTotal;

                                }
                                else if (OrderInfo.FSTotal != 0)
                                {
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CashAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                           + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                          - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                          + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - OrderInfo.CreditAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                            }
                            else if (OrderInfo.PaymentType == 4)
                            {
                                objOrderMasterModel.Balance = Balance;
                            }
                            objOrderMasterModel.FoodStampAmount = OrderInfo.FSTotal;
                            objOrderMasterModel.CreatedDate = DateTime.Now;
                            objOrderMasterModel.CreatedBy = LoginInfo.UserId;
                            objOrderMasterModel.CounterIP = LoginInfo.CounterIP;
                            objOrderMasterModel.OrdNo = GetORDNO();
                            objOrderMasterModel.IsCancel = false;
                            objOrderMasterModel.TaxExempted = OrderInfo.FSEligibleVoidAmt;
                            objOrderMasterModel.CouponCode = CouponInfo.CouponCode;
                            objOrderMasterModel.CouponDiscAmt = CouponInfo.DiscAmt;

                            if (LoginInfo.TaxCarry)
                            {
                                objOrderMasterModel.TaxExempted = OrderInfo.TaxCarryAmount;
                                objOrderMasterModel.IsTaxCarry = true;
                            }
                            else
                            {
                                objOrderMasterModel.IsTaxCarry = false;
                            }

                            var addOrder = _OrderScannerService.AddOrder(objOrderMasterModel, 1);
                            LoginInfo.LastOrderID = addOrder.OrderID;
                            #endregion

                            #region InsertOrderDetail
                            objOrderDetailmasterModel = new OrderDetailmasterModel();
                            decimal? TotalFSAmt = OrderInfo.FSTotal;
                            for (int row = 0; row < dataGridProducts.Rows.Count; row++)
                            {
                                string regex = "(\\(.*\\))";
                                objOrderDetailmasterModel.OrderID = addOrder.OrderID;
                                objOrderDetailmasterModel.ProductID = Functions.GetLong(dataGridProducts.Rows[row].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                                objOrderDetailmasterModel.ProductName = Regex.Replace(dataGridProducts.Rows[row].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").Trim().Replace(" - Group Discount", "");
                                objOrderDetailmasterModel.UPCCode = dataGridProducts.Rows[row].Cells[ProductMasterModelCont.UPCCode].Value.ToString();
                                objOrderDetailmasterModel.Quantity = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                                objOrderDetailmasterModel.DepartmentID = Functions.GetLong(dataGridProducts.Rows[row].Cells[DepartmentMasterModelCont.DepartmentID].Value.ToString());
                                objOrderDetailmasterModel.SectionID = Functions.GetLong(dataGridProducts.Rows[row].Cells[SectionMasterModelCont.SectionID].Value.ToString());

                                //objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));

                                string sale = dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty);
                                if (sale.Contains("\n"))
                                {
                                    string[] split = sale.Split(new Char[] { '\n' });
                                    objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(split[1]);
                                }
                                else
                                {
                                    objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                }
                                objOrderDetailmasterModel.Discount = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.Discount].Value.ToString());
                                objOrderDetailmasterModel.finalPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderDetailmasterModel.GroupPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.GroupPrice].Value.ToString());
                                objOrderDetailmasterModel.GroupQty = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.GroupQty].Value.ToString());
                                objOrderDetailmasterModel.CasePrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.CasePrice].Value.ToString());
                                objOrderDetailmasterModel.CaseQty = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.CaseQty].Value.ToString());
                                objOrderDetailmasterModel.StoreID = LoginInfo.StoreID;
                                objOrderDetailmasterModel.CreatedBy = LoginInfo.UserId;
                                objOrderDetailmasterModel.CreatedDate = DateTime.Now;
                                objOrderDetailmasterModel.IsScale = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsScale].Value.ToString());
                                objOrderDetailmasterModel.IsFoodStamp = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString());

                                objOrderDetailmasterModel.FoodStampTotal = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FoodStampTotal].Value.ToString());

                                objOrderDetailmasterModel.IsTax = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString());
                                objOrderDetailmasterModel.DiscountApplyed = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.DiscountApplyed].Value.ToString());
                                //if (objOrderDetailmasterModel.DiscountApplyed == true)
                                //{
                                //    int lastindex = Regex.Replace(dataGridProducts.Rows[row].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").IndexOf("\n");
                                //    objOrderDetailmasterModel.ProductName = Regex.Replace(dataGridProducts.Rows[row].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").Remove(0, lastindex).Trim();
                                //}
                                objOrderDetailmasterModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString()).ToString("N2"));
                                objOrderDetailmasterModel.IsRefund = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsRefund].Value.ToString());
                                if (objOrderDetailmasterModel.IsRefund == true)
                                    printer.Refund = true;
                                else
                                    printer.Refund = false;
                                objOrderDetailmasterModel.IsForceTax = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsForceTax].Value.ToString());
                                objOrderDetailmasterModel.IsCancel = false;
                                objOrderDetailmasterModel.CasePriceApplied = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.CasePriceApplied].Value.ToString());
                                objOrderDetailmasterModel.IsTaxCarry = objOrderMasterModel.IsTaxCarry;
                                var addOrderDetail = _OrderScannerService.AddOrderDetail(objOrderDetailmasterModel, 1);

                                #region InsertProductLedger
                                objProductLedgerMasterModel = new ProductLedgerMasterModel();
                                objProductLedgerMasterModel.OrderID = addOrder.OrderID;
                                objProductLedgerMasterModel.OrderLineID = addOrderDetail.OrderDetailID;
                                objProductLedgerMasterModel.ProductID = Functions.GetLong(dataGridProducts.Rows[row].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                                objProductLedgerMasterModel.Qty = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                                objProductLedgerMasterModel.UPCCode = dataGridProducts.Rows[row].Cells[ProductMasterModelCont.UPCCode].Value.ToString();

                                //objProductLedgerMasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                if (sale.Contains("\n"))
                                {
                                    string[] split = sale.Split(new Char[] { '\n' });
                                    objProductLedgerMasterModel.SellPrice = Functions.GetDecimal(split[1]);
                                }
                                else
                                {
                                    objProductLedgerMasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                }
                                objProductLedgerMasterModel.FinalPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objProductLedgerMasterModel.TaxAmount = Math.Round(Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString()), 2);
                                objProductLedgerMasterModel.LedgerTypeID = 2;
                                objProductLedgerMasterModel.DiscountPrice = Functions.GetDecimal(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.Discount].Value.ToString());
                                objProductLedgerMasterModel.TaxGroupCodeID = Functions.GetLong(dataGridProducts.Rows[row].Cells[TaxGroupMasterModelCont.TaxGroupID].Value.ToString());
                                objProductLedgerMasterModel.DepartmentID = Functions.GetLong(dataGridProducts.Rows[row].Cells[DepartmentMasterModelCont.DepartmentID].Value.ToString());
                                objProductLedgerMasterModel.SectionID = Functions.GetLong(dataGridProducts.Rows[row].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                                objProductLedgerMasterModel.UnitOfMeasureID = Functions.GetLong(dataGridProducts.Rows[row].Cells[UoMMasterModelCont.UnitMeasureID].Value.ToString());
                                objProductLedgerMasterModel.StoreID = LoginInfo.StoreID;
                                objProductLedgerMasterModel.CreatedDate = DateTime.Now;
                                objProductLedgerMasterModel.CreatedBy = LoginInfo.UserId;
                                objProductLedgerMasterModel.IsForceTax = Functions.GetBoolean(dataGridProducts.Rows[row].Cells[OrderScanner_ResultModelCont.IsForceTax].Value.ToString());

                                var add_ProductLedger = _ProductLedgerService.AddProductLedger(objProductLedgerMasterModel, 1);

                                //#region Update Reamining Qty
                                decimal Qty = Functions.GetDecimal(objProductLedgerMasterModel.Qty.ToString());
                                decimal _Qty = Qty;
                                decimal RemainingQty = 0;

                                for (int i = 0; Qty >= 0; i++)
                                {
                                    if (Qty >= 0)
                                    {
                                        DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                        var ProductLedger = (from PL in _db.tbl_ProductLedger.Where
                                                     (o => o.ProductID == objProductLedgerMasterModel.ProductID
                                                     && o.RemainingQty > 0 && o.LedgerTypeID == 1)
                                                             select new
                                                             {
                                                                 ProductLedgerID = PL.ProductLedgerID,
                                                                 RemainingQty = PL.RemainingQty
                                                             }).FirstOrDefault();
                                        if (ProductLedger != null)
                                        {
                                            RemainingQty = Functions.GetDecimal(ProductLedger.RemainingQty.ToString());
                                            _Qty = Qty - RemainingQty;
                                            if (_Qty > 0)
                                            {
                                                var updateContacts = from x in _db.tbl_ProductLedger
                                                                     where x.ProductLedgerID == ProductLedger.ProductLedgerID
                                                                     select x;
                                                foreach (var contact in updateContacts)
                                                    contact.RemainingQty = 0;
                                            }
                                            else
                                            {
                                                var updateContacts = from x in _db.tbl_ProductLedger
                                                                     where x.ProductLedgerID == ProductLedger.ProductLedgerID
                                                                     select x;
                                                foreach (var contact in updateContacts)
                                                    contact.RemainingQty = RemainingQty - Qty;
                                            }
                                            _db.SaveChanges();
                                            Qty = _Qty;
                                        }
                                        else
                                        {
                                            Qty = -1;
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region InsertPaymentTrans
                            PaymentTransMasterModel objPaymentTransMasterModel = new PaymentTransMasterModel();

                            for (int i = 0; i < MultiPaymentInfo.lstPaymentTransMasterModel.Count; i++)
                            {
                                if (i == MultiPaymentInfo.lstPaymentTransMasterModel.Count - 1)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].ChangeAmount = Functions.GetDecimal(OrderInfo.Change.ToString());
                                }
                                objPaymentTransMasterModel.OrderID = addOrder.OrderID;
                                objPaymentTransMasterModel.CardNumber = CommonModelCont.EmptyString;
                                objPaymentTransMasterModel.PaymentMethodID = MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID;
                                objPaymentTransMasterModel.CashAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount;
                                objPaymentTransMasterModel.CheckAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount;
                                objPaymentTransMasterModel.CreditCardAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount;
                                objPaymentTransMasterModel.FoodStampAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount;

                                if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 1)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount;
                                    if (i != 0)
                                    {
                                        MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].Balance
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount : 0)
                                            - (MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount : 0);
                                    }
                                }
                                else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 2)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount;
                                    if (i != 0)
                                    {
                                        MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].Balance
                                            // - (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount : 0)
                                            - (MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount : 0);
                                        // - (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount : 0);
                                    }
                                }
                                else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 3)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount;
                                    if (i != 0)
                                    {
                                        MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].Balance
                                              //- (MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount : 0)
                                              - (MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount : 0);
                                        //- (MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount : 0)
                                        //- (MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount : 0);
                                    }
                                }
                                else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 4)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount;
                                    if (i != 0)
                                    {
                                        MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].Balance
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount : 0)
                                            - (MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount : 0);
                                    }
                                }

                                objPaymentTransMasterModel.Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance;
                                objPaymentTransMasterModel.ChangeAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].ChangeAmount;
                                objPaymentTransMasterModel.CreatedDate = DateTime.Now;
                                objPaymentTransMasterModel.CreatedBy = LoginInfo.UserId;
                                objPaymentTransMasterModel.CounterIP = LoginInfo.CounterIP;
                                objPaymentTransMasterModel.StoreID = LoginInfo.StoreID;
                                var addPayment = _PaymentTransService.AddPaymentTrans(objPaymentTransMasterModel, 1);
                            }
                            #endregion

                            #region OrderReceipt
                            DataTable dt = ReceiptDetailSP(addOrder.OrderID, false);
                            if (dt.Rows.Count > 0)
                            {
                                if (CheckMyPrinter())
                                {
                                    printer.RePrint = false;
                                    printer.TaxableAmount = TaxableAmount;
                                    printer.dt = dt;
                                    printer.OpenDrawer();
                                    printer.ReceiptPrint();
                                    printer.PartialPaperCut();
                                    printer.PrintDocument();
                                }
                            }
                            #endregion

                            MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
                            if (CouponInfo.isCoupon)
                            {
                                CouponMasterModel objCouponMasterModel = new CouponMasterModel();
                                CouponService _CouponService = new CouponService();
                                objCouponMasterModel.CoupenCode = CouponInfo.CouponCode;
                                objCouponMasterModel.UsedCount = CouponInfo.usedCoupon + 1;
                                objCouponMasterModel.AvailableCount = CouponInfo.availableCoupon;
                                _CouponService.AddCoupon(objCouponMasterModel, 3);
                            }
                        }
                        else
                        {
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            #region Order Master
                            objOrderMasterModel = new OrderMasterModel();
                            objOrderMasterModel.CardNumber = CommonModelCont.EmptyString;
                            objOrderMasterModel.CustomerID = 0;
                            objOrderMasterModel.PaymentMethodID = OrderInfo.PaymentType;
                            objOrderMasterModel.StoreID = LoginInfo.StoreID;
                            objOrderMasterModel.Status = AlertMessages.ConfirmedStatus;
                            objOrderMasterModel.TotalAmount = Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)) + Functions.GetDecimal((lblFSTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                            objOrderMasterModel.GrossAmount = Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                            objOrderMasterModel.TaxAmount = Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty));
                            objOrderMasterModel.TaxableAmount = TaxableAmount;
                            if ((Functions.GetDecimal((lblFinalAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))) < 0)
                            {
                                objOrderMasterModel.RefundAmount = OrderInfo.CashAmt + OrderInfo.CheckAmt + OrderInfo.CreditAmt;
                                objOrderMasterModel.CashAmount = -(OrderInfo.CashAmt);
                                objOrderMasterModel.CheckAmount = -(OrderInfo.CheckAmt);
                                objOrderMasterModel.CreditCardAmount = -(OrderInfo.CreditAmt);
                            }
                            else
                            {
                                objOrderMasterModel.CashAmount = OrderInfo.CashAmt;
                                objOrderMasterModel.CheckAmount = OrderInfo.CheckAmt;
                                objOrderMasterModel.CreditCardAmount = OrderInfo.CreditAmt;
                                objOrderMasterModel.RefundAmount = 0;
                            }
                            objOrderMasterModel.ChangeAmount = Functions.GetDecimal(OrderInfo.Change.ToString());
                            if (OrderInfo.PaymentType == 1)
                            {
                                if (OrderInfo.FSTotal != 0 && OrderInfo.CheckAmt != 0 && OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.FSTotal - OrderInfo.CheckAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CheckAmt != 0 && OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.CheckAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CheckAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0)
                                {
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CheckAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                            }
                            else if (OrderInfo.PaymentType == 2)
                            {
                                if (OrderInfo.FSTotal != 0 && OrderInfo.CashAmt != 0 && OrderInfo.CheckAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt - OrderInfo.CashAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CashAmt != 0 && OrderInfo.CheckAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CashAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - OrderInfo.CashAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CheckAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                        + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CheckAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0)
                                {
                                    if (Balance > 0)
                                    {
                                        objOrderMasterModel.Balance = Balance;
                                    }
                                }
                                else if (OrderInfo.CashAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CheckAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - OrderInfo.CheckAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else
                                {
                                    objOrderMasterModel.Balance = Balance;
                                }
                            }
                            else if (OrderInfo.PaymentType == 3)
                            {
                                if (OrderInfo.FSTotal != 0 && OrderInfo.CashAmt != 0 && OrderInfo.CreditAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.FSTotal - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CashAmt != 0 && OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CashAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CashAmt - OrderInfo.FSTotal;
                                    objOrderMasterModel.Balance = Balance;

                                }
                                else if (OrderInfo.FSTotal != 0 && OrderInfo.CreditAmt != 0)
                                {

                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                        - OrderInfo.CreditAmt - OrderInfo.FSTotal;

                                }
                                else if (OrderInfo.FSTotal != 0)
                                {
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CashAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                           + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                          - OrderInfo.CashAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                                else if (OrderInfo.CreditAmt != 0)
                                {
                                    Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                          + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - OrderInfo.CreditAmt;
                                    objOrderMasterModel.Balance = Balance;
                                }
                            }
                            else if (OrderInfo.PaymentType == 4)
                            {
                                objOrderMasterModel.Balance = Balance;
                            }
                            objOrderMasterModel.FoodStampAmount = OrderInfo.FSTotal;
                            objOrderMasterModel.CreatedDate = DateTime.Now;
                            objOrderMasterModel.CreatedBy = LoginInfo.UserId;
                            objOrderMasterModel.CounterIP = LoginInfo.CounterIP;
                            objOrderMasterModel.IsCancel = false;
                            objOrderMasterModel.TaxExempted = OrderInfo.FSEligibleVoidAmt;

                            if (LoginInfo.TaxCarry)
                            {
                                objOrderMasterModel.TaxExempted = OrderInfo.TaxCarryAmount;
                                objOrderMasterModel.IsTaxCarry = true;
                            }
                            else
                            {
                                objOrderMasterModel.IsTaxCarry = false;
                            }

                            objOrderMasterModel.OrdNo = "L" + GetORDNO();

                            cmd.CommandText = "INSERT INTO tbl_OrderMaster(CustomerID,PaymentMethodID,StoreID,TotalAmount,TaxAmount,GrossAmount,CashAmount,CheckAmount,CreditCardAmount,RefundAmount,ChangeAmount,Balance,FoodStampAmount,CardNumber,Status,CreatedDate,CreatedBy,CounterIP,OrdNo,IsCancel,TaxableAmount,TaxExempted,IsTaxCarry) " +
                                              "VALUES(@CustomerID,@PaymentMethodID,@StoreID,@TotalAmount,@TaxAmount,@GrossAmount,@CashAmount,@CheckAmount,@CreditCardAmount,@RefundAmount,@ChangeAmount,@Balance,@FoodStampAmount,@CardNumber,@Status,@CreatedDate,@CreatedBy,@CounterIP,@OrdNo,@IsCancel,@TaxableAmount,@TaxExempted,@IsTaxCarry)";
                            #region Parameters
                            if (objOrderMasterModel.CustomerID != null)
                            {
                                cmd.Parameters.AddWithValue("@CustomerID", objOrderMasterModel.CustomerID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CustomerID", DBNull.Value);
                            }
                            if (objOrderMasterModel.PaymentMethodID != null)
                            {
                                cmd.Parameters.AddWithValue("@PaymentMethodID", objOrderMasterModel.PaymentMethodID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PaymentMethodID", DBNull.Value);
                            }
                            if (objOrderMasterModel.StoreID != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreID", objOrderMasterModel.StoreID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            }
                            if (objOrderMasterModel.TotalAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@TotalAmount", objOrderMasterModel.TotalAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TotalAmount", DBNull.Value);
                            }
                            //if (objOrderMasterModel.DiscountAmount != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@DiscountAmount", objOrderMasterModel.DiscountAmount);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@DiscountAmount", DBNull.Value);
                            //}
                            if (objOrderMasterModel.TaxAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxAmount", objOrderMasterModel.TaxAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.GrossAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@GrossAmount", objOrderMasterModel.GrossAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GrossAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.CashAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@CashAmount", objOrderMasterModel.CashAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CashAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.CheckAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@CheckAmount", objOrderMasterModel.CheckAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CheckAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.CreditCardAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@CreditCardAmount", objOrderMasterModel.CreditCardAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreditCardAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.RefundAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@RefundAmount", objOrderMasterModel.RefundAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RefundAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.ChangeAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@ChangeAmount", objOrderMasterModel.ChangeAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ChangeAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.Balance != null)
                            {
                                cmd.Parameters.AddWithValue("@Balance", objOrderMasterModel.Balance);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Balance", DBNull.Value);
                            }
                            if (objOrderMasterModel.FoodStampAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@FoodStampAmount", objOrderMasterModel.FoodStampAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@FoodStampAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.CardNumber != null)
                            {
                                cmd.Parameters.AddWithValue("@CardNumber", objOrderMasterModel.CardNumber);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CardNumber", DBNull.Value);
                            }
                            if (objOrderMasterModel.Status != null)
                            {
                                cmd.Parameters.AddWithValue("@Status", objOrderMasterModel.Status);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                            }
                            if (objOrderMasterModel.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", objOrderMasterModel.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (objOrderMasterModel.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", objOrderMasterModel.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (objOrderMasterModel.CounterIP != null)
                            {
                                cmd.Parameters.AddWithValue("@CounterIP", objOrderMasterModel.CounterIP);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CounterIP", DBNull.Value);
                            }
                            if (objOrderMasterModel.OrdNo != null)
                            {
                                cmd.Parameters.AddWithValue("@OrdNo", objOrderMasterModel.OrdNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OrdNo", DBNull.Value);
                            }
                            if (objOrderMasterModel.IsCancel != null)
                            {
                                cmd.Parameters.AddWithValue("@IsCancel", objOrderMasterModel.IsCancel);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsCancel", DBNull.Value);
                            }
                            if (objOrderMasterModel.TaxableAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxableAmount", objOrderMasterModel.TaxableAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxableAmount", DBNull.Value);
                            }
                            if (objOrderMasterModel.TaxExempted != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxExempted", objOrderMasterModel.TaxExempted);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxExempted", DBNull.Value);
                            }
                            if (objOrderMasterModel.IsTaxCarry != null)
                            {
                                cmd.Parameters.AddWithValue("@IsTaxCarry", objOrderMasterModel.IsTaxCarry);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsTaxCarry", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteScalar();
                            cmd.CommandText = "Select @@Identity";
                            long OrderID = Convert.ToInt32(cmd.ExecuteScalar());
                            LoginInfo.LastOrderID = OrderID;

                            #endregion

                            #region Order Detail
                            objOrderDetailmasterModel = new OrderDetailmasterModel();
                            decimal? TotalFSAmt = OrderInfo.FSTotal;
                            for (int i = 0; i < dataGridProducts.Rows.Count; i++)
                            {
                                string regex = "(\\(.*\\))";
                                cmd = conn.CreateCommand();
                                objOrderDetailmasterModel.OrderID = OrderID;
                                objOrderDetailmasterModel.ProductID = Functions.GetLong(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                                objOrderDetailmasterModel.ProductName = Regex.Replace(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").Trim().Replace(" - Group Discount", "");
                                objOrderDetailmasterModel.UPCCode = dataGridProducts.Rows[i].Cells[ProductMasterModelCont.UPCCode].Value.ToString();
                                objOrderDetailmasterModel.Quantity = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                                objOrderDetailmasterModel.DepartmentID = Functions.GetLong(dataGridProducts.Rows[i].Cells[DepartmentMasterModelCont.DepartmentID].Value.ToString());
                                objOrderDetailmasterModel.SectionID = Functions.GetLong(dataGridProducts.Rows[i].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                                //objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                string sale = dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty);
                                if (sale.Contains("\n"))
                                {
                                    string[] split = sale.Split(new Char[] { '\n' });
                                    objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(split[1]);
                                }
                                else
                                {
                                    objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                }
                                objOrderDetailmasterModel.Discount = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Discount].Value.ToString());
                                objOrderDetailmasterModel.finalPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objOrderDetailmasterModel.GroupPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.GroupPrice].Value.ToString());
                                objOrderDetailmasterModel.GroupQty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.GroupQty].Value.ToString());
                                objOrderDetailmasterModel.CasePrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CasePrice].Value.ToString());
                                objOrderDetailmasterModel.CaseQty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CaseQty].Value.ToString());
                                objOrderDetailmasterModel.StoreID = LoginInfo.StoreID;
                                objOrderDetailmasterModel.CreatedBy = LoginInfo.UserId;
                                objOrderDetailmasterModel.CreatedDate = DateTime.Now;
                                objOrderDetailmasterModel.IsScale = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsScale].Value.ToString());
                                objOrderDetailmasterModel.IsFoodStamp = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString());


                                objOrderDetailmasterModel.FoodStampTotal = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FoodStampTotal].Value.ToString());

                                objOrderDetailmasterModel.IsTax = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString());
                                objOrderDetailmasterModel.DiscountApplyed = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.DiscountApplyed].Value.ToString());
                                //if (objOrderDetailmasterModel.DiscountApplyed == true)
                                //{
                                //    int lastindex = Regex.Replace(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").IndexOf("\n");
                                //    objOrderDetailmasterModel.ProductName = Regex.Replace(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductName].Value.ToString(), regex, "").Remove(0, lastindex).Trim();
                                //}
                                objOrderDetailmasterModel.TaxAmount = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                objOrderDetailmasterModel.IsRefund = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsRefund].Value.ToString());
                                if (objOrderDetailmasterModel.IsRefund == true)
                                    printer.Refund = true;
                                else
                                    printer.Refund = false;
                                objOrderDetailmasterModel.IsCancel = false;
                                objOrderDetailmasterModel.IsForceTax = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsForceTax].Value.ToString());
                                objOrderDetailmasterModel.CasePriceApplied = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.CasePriceApplied].Value.ToString());
                                objOrderDetailmasterModel.IsTaxCarry = objOrderMasterModel.IsTaxCarry;

                                cmd.CommandText = "INSERT INTO tbl_OrderDetail(OrderID,ProductID,UPCCode,ProductName,Quantity,DepartmentID,SectionID,SellPrice,Discount,finalPrice,StoreID,CreatedBy,CreatedDate,IsScale,IsFoodStamp,IsTax,FoodStampTotal,DiscountApplyed,TaxAmount,IsRefund,IsCancel,IsForceTax,CasePriceApplied,GroupQty,GroupPrice,CaseQty,CasePrice,IsTaxCarry) " +
                                                 "VALUES(@OrderID,@ProductID,@UPCCode,@ProductName,@Quantity,@DepartmentID,@SectionID,@SellPrice,@Discount,@finalPrice,@StoreID,@CreatedBy,@CreatedDate,@IsScale,@IsFoodStamp,@IsTax,@FoodStampTotal,@DiscountApplyed,@TaxAmount,@IsRefund,@IsCancel,@IsForceTax,@CasePriceApplied,@GroupQty,@GroupPrice,@CaseQty,@CasePrice,@IsTaxCarry)";
                                #region Parameters
                                if (objOrderDetailmasterModel.OrderID != null)
                                {
                                    cmd.Parameters.AddWithValue("@OrderID", objOrderDetailmasterModel.OrderID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OrderID", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.ProductID != null)
                                {
                                    cmd.Parameters.AddWithValue("@ProductID", objOrderDetailmasterModel.ProductID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.StoreID != null)
                                {
                                    cmd.Parameters.AddWithValue("@StoreID", objOrderDetailmasterModel.StoreID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.UPCCode != null)
                                {
                                    cmd.Parameters.AddWithValue("@UPCCode", objOrderDetailmasterModel.UPCCode);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@UPCCode", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.ProductName != null)
                                {
                                    cmd.Parameters.AddWithValue("@ProductName", objOrderDetailmasterModel.ProductName);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ProductName", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.Quantity != null)
                                {
                                    cmd.Parameters.AddWithValue("@Quantity", objOrderDetailmasterModel.Quantity);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Quantity", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.DepartmentID != null)
                                {
                                    cmd.Parameters.AddWithValue("@DepartmentID", objOrderDetailmasterModel.DepartmentID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.SectionID != null)
                                {
                                    cmd.Parameters.AddWithValue("@SectionID", objOrderDetailmasterModel.SectionID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.SellPrice != null)
                                {
                                    cmd.Parameters.AddWithValue("@SellPrice", objOrderDetailmasterModel.SellPrice);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@SellPrice", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.Discount != null)
                                {
                                    cmd.Parameters.AddWithValue("@Discount", objOrderDetailmasterModel.Discount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Discount", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.finalPrice != null)
                                {
                                    cmd.Parameters.AddWithValue("@finalPrice", objOrderDetailmasterModel.finalPrice);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@finalPrice", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.CreatedBy != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreatedBy", objOrderDetailmasterModel.CreatedBy);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.CreatedDate != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreatedDate", objOrderDetailmasterModel.CreatedDate);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.IsScale != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsScale", objOrderDetailmasterModel.IsScale);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsScale", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.IsFoodStamp != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsFoodStamp", objOrderDetailmasterModel.IsFoodStamp);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.IsTax != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsTax", objOrderDetailmasterModel.IsTax);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsTax", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.FoodStampTotal != null)
                                {
                                    cmd.Parameters.AddWithValue("@FoodStampTotal", objOrderDetailmasterModel.FoodStampTotal);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FoodStampTotal", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.DiscountApplyed != null)
                                {
                                    cmd.Parameters.AddWithValue("@DiscountApplyed", objOrderDetailmasterModel.DiscountApplyed);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@DiscountApplyed", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.TaxAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@TaxAmount", objOrderDetailmasterModel.TaxAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TaxAmount", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.IsRefund != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsRefund", objOrderDetailmasterModel.IsRefund);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsRefund", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.IsCancel != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsCancel", objOrderDetailmasterModel.IsCancel);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsCancel", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.IsForceTax != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsForceTax", objOrderDetailmasterModel.IsForceTax);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsForceTax", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.CasePriceApplied != null)
                                {
                                    cmd.Parameters.AddWithValue("@CasePriceApplied", objOrderDetailmasterModel.CasePriceApplied);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CasePriceApplied", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.GroupPrice != null)
                                {
                                    cmd.Parameters.AddWithValue("@GroupPrice", objOrderDetailmasterModel.GroupPrice);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@GroupPrice", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.GroupQty != null)
                                {
                                    cmd.Parameters.AddWithValue("@GroupQty", objOrderDetailmasterModel.GroupQty);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@GroupQty", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.CasePrice != null)
                                {
                                    cmd.Parameters.AddWithValue("@CasePrice", objOrderDetailmasterModel.CasePrice);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CasePrice", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.CaseQty != null)
                                {
                                    cmd.Parameters.AddWithValue("@CaseQty", objOrderDetailmasterModel.CaseQty);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CaseQty", DBNull.Value);
                                }
                                if (objOrderDetailmasterModel.IsTaxCarry != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsTaxCarry", objOrderDetailmasterModel.IsTaxCarry);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsTaxCarry", DBNull.Value);
                                }
                                #endregion

                                cmd.ExecuteScalar();
                                cmd.CommandText = "Select @@Identity";
                                long OrderDetailID = Convert.ToInt32(cmd.ExecuteScalar());

                                #region InsertProductLedger
                                objProductLedgerMasterModel = new ProductLedgerMasterModel();
                                cmd = conn.CreateCommand();
                                objProductLedgerMasterModel.OrderID = OrderID;
                                objProductLedgerMasterModel.OrderLineID = OrderDetailID;
                                objProductLedgerMasterModel.ProductID = Functions.GetLong(dataGridProducts.Rows[i].Cells[ProductMasterModelCont.ProductID].Value.ToString());
                                objProductLedgerMasterModel.Qty = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                                objProductLedgerMasterModel.UPCCode = dataGridProducts.Rows[i].Cells[ProductMasterModelCont.UPCCode].Value.ToString();
                                //objProductLedgerMasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                //string sale = dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty);
                                if (sale.Contains("\n"))
                                {
                                    string[] split = sale.Split(new Char[] { '\n' });
                                    objProductLedgerMasterModel.SellPrice = Functions.GetDecimal(split[1]);
                                }
                                else
                                {
                                    objProductLedgerMasterModel.SellPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.SellPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                }

                                objProductLedgerMasterModel.FinalPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value.ToString().Replace(CommonModelCont.AddDollorSign, string.Empty));
                                objProductLedgerMasterModel.TaxAmount = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                                objProductLedgerMasterModel.LedgerTypeID = 2;
                                objProductLedgerMasterModel.DiscountPrice = Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Discount].Value.ToString());
                                objProductLedgerMasterModel.TaxGroupCodeID = Functions.GetLong(dataGridProducts.Rows[i].Cells[TaxGroupMasterModelCont.TaxGroupID].Value.ToString());
                                objProductLedgerMasterModel.DepartmentID = Functions.GetLong(dataGridProducts.Rows[i].Cells[DepartmentMasterModelCont.DepartmentID].Value.ToString());
                                objProductLedgerMasterModel.SectionID = Functions.GetLong(dataGridProducts.Rows[i].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                                objProductLedgerMasterModel.UnitOfMeasureID = Functions.GetLong(dataGridProducts.Rows[i].Cells[UoMMasterModelCont.UnitMeasureID].Value.ToString());
                                objProductLedgerMasterModel.StoreID = LoginInfo.StoreID;
                                objProductLedgerMasterModel.CreatedBy = LoginInfo.UserId;
                                objProductLedgerMasterModel.CreatedDate = DateTime.Now;
                                objProductLedgerMasterModel.IsForceTax = Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsForceTax].Value.ToString());

                                cmd.CommandText = "INSERT INTO tbl_ProductLedger(ProductID,OrderLineID,LedgerTypeID,OrderID,PostedPurchaseHeaderID,StoreID,TaxGroupCodeID,DepartmentID,SectionID,UnitOfMeasureID,UPCCode,QRCode,Qty,RemainingQty,PurchasePrice,SellPrice,FinalPrice,DiscountPrice,TaxAmount,CreatedDate,CreatedBy,IsForceTax) " +
                                                  "VALUES(@ProductID,@OrderLineID,@LedgerTypeID,@OrderID,@PostedPurchaseHeaderID,@StoreID,@TaxGroupCodeID,@DepartmentID,@SectionID,@UnitOfMeasureID,@UPCCode,@QRCode,@Qty,@RemainingQty,@PurchasePrice,@SellPrice,@FinalPrice,@DiscountPrice,@TaxAmount,@CreatedDate,@CreatedBy,@IsForceTax)";
                                #region Parameters
                                if (objProductLedgerMasterModel.ProductID != null)
                                {
                                    cmd.Parameters.AddWithValue("@ProductID", objProductLedgerMasterModel.ProductID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.OrderLineID != null)
                                {
                                    cmd.Parameters.AddWithValue("@OrderLineID", objProductLedgerMasterModel.OrderLineID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OrderLineID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.LedgerTypeID != null)
                                {
                                    cmd.Parameters.AddWithValue("@LedgerTypeID", objProductLedgerMasterModel.LedgerTypeID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@LedgerTypeID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.OrderID != null)
                                {
                                    cmd.Parameters.AddWithValue("@OrderID", objProductLedgerMasterModel.OrderID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OrderID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.PostedPurchaseHeaderID != null)
                                {
                                    cmd.Parameters.AddWithValue("@PostedPurchaseHeaderID", objProductLedgerMasterModel.PostedPurchaseHeaderID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@PostedPurchaseHeaderID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.StoreID != null)
                                {
                                    cmd.Parameters.AddWithValue("@StoreID", objProductLedgerMasterModel.StoreID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.TaxGroupCodeID != null)
                                {
                                    cmd.Parameters.AddWithValue("@TaxGroupCodeID", objProductLedgerMasterModel.TaxGroupCodeID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TaxGroupCodeID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.DepartmentID != null)
                                {
                                    cmd.Parameters.AddWithValue("@DepartmentID", objProductLedgerMasterModel.DepartmentID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.SectionID != null)
                                {
                                    cmd.Parameters.AddWithValue("@SectionID", objProductLedgerMasterModel.SectionID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.UnitOfMeasureID != null)
                                {
                                    cmd.Parameters.AddWithValue("@UnitOfMeasureID", objProductLedgerMasterModel.UnitOfMeasureID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@UnitOfMeasureID", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.UPCCode != null)
                                {
                                    cmd.Parameters.AddWithValue("@UPCCode", objProductLedgerMasterModel.UPCCode);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@UPCCode", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.QRCode != null)
                                {
                                    cmd.Parameters.AddWithValue("@QRCode", objProductLedgerMasterModel.QRCode);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@QRCode", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.Qty != null)
                                {
                                    cmd.Parameters.AddWithValue("@Qty", objProductLedgerMasterModel.Qty);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Qty", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.RemainingQty != null)
                                {
                                    cmd.Parameters.AddWithValue("@RemainingQty", objProductLedgerMasterModel.RemainingQty);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@RemainingQty", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.PurchasePrice != null)
                                {
                                    cmd.Parameters.AddWithValue("@PurchasePrice", objProductLedgerMasterModel.PurchasePrice);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@PurchasePrice", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.SellPrice != null)
                                {
                                    cmd.Parameters.AddWithValue("@SellPrice", objProductLedgerMasterModel.SellPrice);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@SellPrice", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.FinalPrice != null)
                                {
                                    cmd.Parameters.AddWithValue("@FinalPrice", objProductLedgerMasterModel.FinalPrice);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FinalPrice", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.DiscountPrice != null)
                                {
                                    cmd.Parameters.AddWithValue("@DiscountPrice", objProductLedgerMasterModel.DiscountPrice);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@DiscountPrice", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.TaxAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@TaxAmount", objProductLedgerMasterModel.TaxAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TaxAmount", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.CreatedDate != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreatedDate", objProductLedgerMasterModel.CreatedDate);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.CreatedBy != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreatedBy", objProductLedgerMasterModel.CreatedBy);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                                }
                                if (objProductLedgerMasterModel.IsForceTax != null)
                                {
                                    cmd.Parameters.AddWithValue("@IsForceTax", objProductLedgerMasterModel.IsForceTax);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@IsForceTax", DBNull.Value);
                                }
                                #endregion
                                cmd.ExecuteScalar();
                                #endregion
                            }
                            #endregion

                            #region InsertPaymentTrans
                            PaymentTransMasterModel objPaymentTransMasterModel = new PaymentTransMasterModel();

                            for (int i = 0; i < MultiPaymentInfo.lstPaymentTransMasterModel.Count; i++)
                            {
                                if (i == MultiPaymentInfo.lstPaymentTransMasterModel.Count - 1)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].ChangeAmount = Functions.GetDecimal(OrderInfo.Change.ToString() != null ? OrderInfo.Change.ToString() : "0");
                                }
                                else
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].ChangeAmount = 0;
                                }

                                objPaymentTransMasterModel.OrderID = OrderID;
                                objPaymentTransMasterModel.CardNumber = CommonModelCont.EmptyString;
                                objPaymentTransMasterModel.PaymentMethodID = MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID;
                                objPaymentTransMasterModel.CashAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount : 0;
                                objPaymentTransMasterModel.CheckAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount : 0;
                                objPaymentTransMasterModel.CreditCardAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount : 0;
                                objPaymentTransMasterModel.FoodStampAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount : 0;

                                if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 1)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount;
                                    if (i != 0)
                                    {
                                        MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].Balance
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount : 0)
                                            - (MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount : 0);
                                    }
                                }
                                else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 2)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount;
                                    if (i != 0)
                                    {
                                        MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].Balance
                                            // - (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount : 0)
                                            - (MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount : 0);
                                        // - (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount : 0);
                                    }
                                }
                                else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 3)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount;
                                    if (i != 0)
                                    {
                                        MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].Balance
                                              //- (MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].FoodStampAmount : 0)
                                              - (MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].CheckAmount : 0);
                                        //- (MultiPaymentInfo.lstPaymentTransMasterModel[i].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount : 0)
                                        //- (MultiPaymentInfo.lstPaymentTransMasterModel[i].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount : 0);
                                    }
                                }
                                else if (MultiPaymentInfo.lstPaymentTransMasterModel[i].PaymentMethodID == 4)
                                {
                                    MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = (Functions.GetDecimal((lblSubTotal.Text).Replace(CommonModelCont.AddDollorSign, string.Empty))
                                         + Functions.GetDecimal((lblTaxAmount.Text).Replace(CommonModelCont.AddDollorSign, string.Empty)))
                                         - MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount;
                                    if (i != 0)
                                    {
                                        MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].Balance
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CashAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CheckAmount : 0)
                                            //- (MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i - 1].CreditCardAmount : 0)
                                            - (MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount != null ? MultiPaymentInfo.lstPaymentTransMasterModel[i].FoodStampAmount : 0);
                                    }
                                }

                                objPaymentTransMasterModel.Balance = MultiPaymentInfo.lstPaymentTransMasterModel[i].Balance;

                                objPaymentTransMasterModel.ChangeAmount = MultiPaymentInfo.lstPaymentTransMasterModel[i].ChangeAmount;
                                objPaymentTransMasterModel.CreatedDate = DateTime.Now;
                                objPaymentTransMasterModel.CreatedBy = LoginInfo.UserId;
                                objPaymentTransMasterModel.CounterIP = LoginInfo.CounterIP;
                                objPaymentTransMasterModel.StoreID = LoginInfo.StoreID;

                                cmd.Parameters.Clear();

                                cmd.CommandText = "INSERT INTO tbl_PaymentTrans(OrderID,CardNumber,PaymentMethodID,CashAmount,CheckAmount,CreditCardAmount,FoodStampAmount,Balance,ChangeAmount,CreatedDate,CreatedBy,CounterIP,StoreID) " +
                                                    "VALUES(@OrderID,@CardNumber,@PaymentMethodID,@CashAmount,@CheckAmount,@CreditCardAmount,@FoodStampAmount,@Balance,@ChangeAmount,@CreatedDate,@CreatedBy,@CounterIP,@StoreID)";
                                #region Parameters
                                if (objPaymentTransMasterModel.OrderID != null)
                                {
                                    cmd.Parameters.AddWithValue("@OrderID", objPaymentTransMasterModel.OrderID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OrderID", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.CardNumber != null)
                                {
                                    cmd.Parameters.AddWithValue("@CardNumber", objPaymentTransMasterModel.CardNumber);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CardNumber", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.PaymentMethodID != null)
                                {
                                    cmd.Parameters.AddWithValue("@PaymentMethodID", objPaymentTransMasterModel.PaymentMethodID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@PaymentMethodID", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.CashAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@CashAmount", objPaymentTransMasterModel.CashAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CashAmount", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.CheckAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@CheckAmount", objPaymentTransMasterModel.CheckAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CheckAmount", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.CreditCardAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreditCardAmount", objPaymentTransMasterModel.CreditCardAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreditCardAmount", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.FoodStampAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@FoodStampAmount", objPaymentTransMasterModel.FoodStampAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FoodStampAmount", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.Balance != null)
                                {
                                    cmd.Parameters.AddWithValue("@Balance", objPaymentTransMasterModel.Balance);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Balance", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.ChangeAmount != null)
                                {
                                    cmd.Parameters.AddWithValue("@ChangeAmount", objPaymentTransMasterModel.ChangeAmount);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ChangeAmount", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.CreatedDate != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreatedDate", objPaymentTransMasterModel.CreatedDate);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.CreatedBy != null)
                                {
                                    cmd.Parameters.AddWithValue("@CreatedBy", objPaymentTransMasterModel.CreatedBy);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.CounterIP != null)
                                {
                                    cmd.Parameters.AddWithValue("@CounterIP", objPaymentTransMasterModel.CounterIP);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CounterIP", DBNull.Value);
                                }
                                if (objPaymentTransMasterModel.StoreID != null)
                                {
                                    cmd.Parameters.AddWithValue("@StoreID", objPaymentTransMasterModel.StoreID);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                                }
                                #endregion

                                cmd.ExecuteScalar();
                                cmd.CommandText = "Select @@Identity";
                                long Payment = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            #endregion

                            #region OrderReceipt
                            DataTable dt = new DataTable();
                            string query = "SELECT OM.OrderID,OM.TotalAmount,OM.TaxAmount,OM.GrossAmount,OM.PaymentMethodID,OM.OrdNo"
                                + " ,OM.CashAmount,OM.CheckAmount,OM.CreditCardAmount,OM.FoodStampAmount,OM.RefundAmount,OM.Balance,OM.ChangeAmount"
                                + " ,OD.UPCCode,OD.ProductName,OD.Quantity,OD.SellPrice,OD.Discount,OD.finalPrice,OD.IsScale"
                                + " ,OD.IsFoodStamp as IsFoodStamp,OD.IsTax as IsTax,OD.DiscountApplyed AS DiscountApplyed,OM.TaxExempted,OD.CasePriceApplied"
                                + " ,OD.GroupQty,OD.GroupPrice,OD.CaseQty,OD.CasePrice,OM.CouponCode ,OM.CouponDiscAmt AS CouponDiscAmt"
                                + " ,SM.StoreName , SM.Address AS SMAddress,SM.Address2 AS SAddress2"
                                + " ,SM.ZipCode AS SZipCode,SM.Phone AS SPhone,SM.Fax AS SFax,EM.FirstName "
                                + " FROM tbl_OrderMaster AS OM "
                                + " INNER JOIN tbl_OrderDetail AS OD ON OM.OrderID=OD.OrderID "
                                + " INNER JOIN tbl_StoreMaster AS SM ON OM.StoreID=SM.StoreID "
                                + " INNER JOIN tbl_EmployeeMaster AS EM ON EM.StoreID = SM.StoreID "
                                + " WHERE OM.OrderID = " + OrderID
                                + " AND EM.EmployeeID = " + LoginInfo.UserId;
                            SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                            da.Fill(dt);
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                            if (dt.Rows.Count >= 0)
                            {
                                if (CheckMyPrinter())
                                {
                                    printer.RePrint = false;
                                    printer.TaxableAmount = TaxableAmount;
                                    printer.dt = dt;
                                    printer.OpenDrawer();
                                    printer.ReceiptPrint();
                                    printer.PartialPaperCut();
                                    printer.PrintDocument();
                                }
                            }
                            #endregion

                            MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
                        }
                        Productdata_ = new List<OrderScanner_ResultModel>();
                        dataLoad();
                        txtSearchUPCCode.Focus();
                        ClsCommon.ResetStaticValues();
                    }
                    else
                    {
                        ClsCommon.RetryMsg("Information", "Something went wrong with printer. Please check paper roll and retry." + " Status = " + status, false);
                        if (RetryOrder.isRetry)
                        {
                            RetryOrder.isRetry = false;
                            GenerateOrder();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                ClsCommon.MsgBox("Information", "Error in Order  >> " + ex.Message.ToString(), false);
                MultiPaymentInfo.lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
            }
        }

        public DataTable ReceiptDetailSP(long OrderID, bool Reprint)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ClsCommon.ListToDataTable(_ReceiptService.GetReceiptDetails(OrderID, LoginInfo.UserId, Reprint));
                return dt;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                return dt;
            }
        }
        
        public DataTable GetSuspendTrans(string TransSuspendCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ClsCommon.ListToDataTable(_TransSuspendService.GetAllTransSuspendDetail(TransSuspendCode));

                DataColumn newColumn = new DataColumn("OrdNo", typeof(string));
                if (dt.Rows.Count > 0)
                {
                    newColumn.DefaultValue = dt.Rows[0]["TransSuspendCode"].ToString().Replace("TS", "");
                }
                dt.Columns.Add(newColumn);

                //List<TransSuspendMasterModel> TransSuspenddata = _TransSuspendService.GetAllTransSuspendDetail(TransSuspendCode);
                //dt.Columns.Add(TransSuspendMasterModelCont.TransSuspendID, typeof(long));
                //dt.Columns.Add(TransSuspendMasterModelCont.TransSuspendCode, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.TotalAmount, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.TaxAmount, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.GrossAmount, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.UPCCode, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.ProductName, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.Quantity, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.SellPrice, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.FinalPrice, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.StoreName, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.SMAddress, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.SAddress2, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.SZipCode, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.SPhone, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.SFax, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.FirstName, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.IsScale, typeof(bool));
                //dt.Columns.Add(TransSuspendMasterModelCont.IsFoodStamp, typeof(bool));
                //dt.Columns.Add(TransSuspendMasterModelCont.IsTax, typeof(bool));
                //dt.Columns.Add(TransSuspendMasterModelCont.DiscountApplyed, typeof(bool));
                //dt.Columns.Add(TransSuspendMasterModelCont.ProductID, typeof(long));
                //dt.Columns.Add("OrdNo", typeof(string));
                //dt.Columns.Add("CasePriceApplied", typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.GroupPrice, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.GroupQty, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.CasePrice, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.CaseQty, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.CouponCode, typeof(string));
                //dt.Columns.Add(TransSuspendMasterModelCont.CouponDiscAmt, typeof(string));

                //foreach (var item in TransSuspenddata)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr[TransSuspendMasterModelCont.TransSuspendID] = item.TransSuspendID;
                //    dr[TransSuspendMasterModelCont.TransSuspendCode] = item.TransSuspendCode;
                //    dr[TransSuspendMasterModelCont.TotalAmount] = item.TotalAmount;
                //    dr[TransSuspendMasterModelCont.TaxAmount] = item.TotalTaxAmount;
                //    dr[TransSuspendMasterModelCont.GrossAmount] = item.GrossAmount;
                //    dr[TransSuspendMasterModelCont.UPCCode] = item.UPCCode;
                //    dr[TransSuspendMasterModelCont.ProductName] = item.ProductName;
                //    dr[TransSuspendMasterModelCont.Quantity] = item.Quantity;
                //    dr[TransSuspendMasterModelCont.SellPrice] = item.SellPrice;
                //    dr[TransSuspendMasterModelCont.FinalPrice] = item.FinalPrice;
                //    dr[TransSuspendMasterModelCont.StoreName] = item.StoreName;
                //    dr[TransSuspendMasterModelCont.SMAddress] = item.SMAddress;
                //    dr[TransSuspendMasterModelCont.SAddress2] = item.SAddress2;
                //    dr[TransSuspendMasterModelCont.SZipCode] = item.SZipCode;
                //    dr[TransSuspendMasterModelCont.SPhone] = item.SPhone;
                //    dr[TransSuspendMasterModelCont.SFax] = item.SFax;
                //    dr[TransSuspendMasterModelCont.FirstName] = item.FirstName;
                //    dr[TransSuspendMasterModelCont.IsScale] = item.IsScale;
                //    dr[TransSuspendMasterModelCont.IsFoodStamp] = item.IsFoodStamp;
                //    dr[TransSuspendMasterModelCont.IsTax] = item.IsTax;
                //    dr[TransSuspendMasterModelCont.DiscountApplyed] = item.DiscountApplyed;
                //    dr[TransSuspendMasterModelCont.ProductID] = item.ProductID;
                //    dr["OrdNo"] = item.TransSuspendCode.Replace("TS", "");
                //    dr["CasePriceApplied"] = item.CasePriceApplied;
                //    dr[TransSuspendMasterModelCont.GroupPrice] = item.GroupPrice;
                //    dr[TransSuspendMasterModelCont.GroupQty] = item.GroupQty;
                //    dr[TransSuspendMasterModelCont.CasePrice] = item.CasePrice;
                //    dr[TransSuspendMasterModelCont.CaseQty] = item.CaseQty;
                //    dr[TransSuspendMasterModelCont.CouponCode] = item.CouponCode;
                //    dr[TransSuspendMasterModelCont.CouponDiscAmt] = item.CouponDiscAmt;

                //    dt.Rows.Add(dr);
                //}
                return dt;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                return dt;
            }
        }

        public string GetTransSuspendNo()
        {
            string TransNo = "TS";
            try
            {
                DateTime date = DateTime.Now;

                string uniqueID = DateTime.Now.ToString("yy") + String.Format("{0:00}", DateTime.Now.Month) + String.Format("{0:00}", DateTime.Now.Day) + String.Format("{0:00}", DateTime.Now.Hour + String.Format("{0:00}", DateTime.Now.Minute) + String.Format("{0:00}", DateTime.Now.Second));

                TransNo += uniqueID;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
            return TransNo;
        }

        public string GetORDNO()
        {
            try
            {
                long OrderNumber = 0;
                //string hostName = Dns.GetHostName();
                //string ip = Dns.GetHostByName(hostName).AddressList[0].ToString();

                //string[] parts = ip.Split('.');
                //string lastpart = String.Format("{0:000}", parts[3]);


                if (LoginInfo.ORDNO == "")
                {
                    ReadWriteOrderNo(1);
                    if (LoginInfo.ORDNO != "0")
                    {
                        if (XMLData.UpdatedDate.Date == DateTime.Now.Date)
                        {
                            LoginInfo.ORDNO = LoginInfo.ORDNO.Replace("L", "");
                        }
                        //else
                        //{
                        //    LoginInfo.ORDNO = "0";
                        //}
                        OrderNumber = Functions.GetLong(LoginInfo.ORDNO);
                    }
                    else
                    {
                        LoginInfo.ORDNO = "0";
                        OrderNumber = Functions.GetLong(LoginInfo.ORDNO);
                    }
                    if (OrderNumber > 0)
                        OrderNumber = Functions.GetLong(OrderNumber.ToString().Substring(OrderNumber.ToString().Length - 4));
                }
                else
                {
                    OrderNumber = Functions.GetLong(LoginInfo.ORDNO.Replace("L", ""));
                    OrderNumber = Functions.GetLong(OrderNumber.ToString().Substring(OrderNumber.ToString().Length - 4));
                }
                OrderNumber = OrderNumber + 1;
                //LoginInfo.ORDNO = lastpart + DateTime.Now.ToString("yyyy") + String.Format("{0:00}", DateTime.Now.Month) + String.Format("{0:00}", DateTime.Now.Day) + OrderNumber.ToString("0000");
                LoginInfo.ORDNO = DateTime.Now.ToString("yyyy") + String.Format("{0:00}", DateTime.Now.Month) + String.Format("{0:00}", DateTime.Now.Day) + LoginInfo.CashierID + OrderNumber.ToString("0000");
                ReadWriteOrderNo(0);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "*** GetORDNO ***" + ex.StackTrace, ex.LineNumber());
            }
            return LoginInfo.ORDNO;
        }

        public void ReadWriteOrderNo(int Mode)
        {
            try
            {
                if (Mode == 1)
                {
                    DataTable dt = new DataTable();
                    string query = "SELECT * FROM tbl_ORDNo WHERE CounterIP='" + LoginInfo.CounterIP + "'";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(dt.Rows[0]["ORD"].ToString()))
                        {
                            LoginInfo.ORDNO = dt.Rows[0]["ORD"].ToString().Replace("L", "");
                            if (!String.IsNullOrEmpty(dt.Rows[0]["UpdatedDate"].ToString()))
                                XMLData.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                        }
                        else
                        {
                            LoginInfo.ORDNO = "0";
                        }
                    }
                    else
                    {
                        LoginInfo.ORDNO = "0";
                    }
                }
                else
                {
                    string query = "SELECT * FROM tbl_ORDNo WHERE CounterIP='" + LoginInfo.CounterIP + "'";
                    DataTable dt = new DataTable();
                    SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        #region Update
                        DataAdapter = new SqlCeDataAdapter();
                        SqlCeCommand cmd = conn.CreateCommand();
                        cmd = conn.CreateCommand();

                        cmd.CommandText = "UPDATE tbl_ORDNo SET ORD='" + LoginInfo.ORDNO + "',UpdatedDate='" + DateTime.Now + "' WHERE CounterIP='" + LoginInfo.CounterIP + "'";
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        cmd.ExecuteNonQuery();
                        #endregion
                    }
                    else
                    {
                        #region Insert

                        SqlCeCommand cmd = conn.CreateCommand();
                        cmd = conn.CreateCommand();
                        cmd.CommandText = query = "DELETE tbl_TransSuspenMaster";
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        int modified = cmd.ExecuteNonQuery();

                        DataAdapter = new SqlCeDataAdapter();
                        cmd = conn.CreateCommand();

                        cmd.CommandText = "INSERT INTO tbl_ORDNo(ORD,CounterIP,UpdatedDate) " +
                            "VALUES('" + LoginInfo.ORDNO + "','" + LoginInfo.CounterIP + "','" + DateTime.Now + "')";
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        cmd.ExecuteNonQuery();

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "*** GetORDNO ***" + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable CashierReceiptMasterSP()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ClsCommon.ListToDataTable(_ReceiptService.GetCashierReceipt());
                return dt;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                return dt;
            }
        }

        public void AddDepartment()
        {
            try
            {
                lblWeight.Text = "0.00 lb";
                if (txtSearchUPCCode.Text.Trim().ToLower().Contains("dp"))
                {
                    string EnterUPCCode = txtSearchUPCCode.Text;
                    string weight = "";
                    string[] Product;
                    bool IsScale = false;
                    decimal _SellPrice = 0;
                    decimal _Discount = 0;
                    decimal _Tax = 0;
                    string[] Qty;
                    string Quantity = "";
                    if (EnterUPCCode.Trim().ToUpper().Contains("MM"))
                    {
                        IsScale = true;
                        Product = EnterUPCCode.ToUpper().Split('M');
                        weight = Functions.GetDecimal(Product[0].Insert(Product[0].Length - 2, ".")).ToString();
                        if (Product[2] != null && Product[2] != "")
                        {
                            Product = Product[2].ToUpper().Split('D');
                            txtSearchUPCCode.Text = Product[1].ToUpper().Replace("P", "");
                        }
                        if (Product[0] != null && Product[0] != "")
                        {
                            _SellPrice = Functions.GetDecimal(Product[0]) / 100;
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "You must key in the price ! ", false);
                            LoginInfo.tnfn = false;
                            txtSearchUPCCode.Text = null;
                            txtSearchUPCCode.Focus();
                            return;
                        }
                    }
                    else
                    {
                        Product = EnterUPCCode.ToUpper().Split('D');
                        if (Product[0].Contains("*"))
                        {
                            Qty = Product[0].Split('*');
                            if (Qty[1] == "")
                            {
                                ClsCommon.MsgBox("Information", "You must key in the price first! ", false);
                                txtSearchUPCCode.Text = null;
                                LoginInfo.tnfn = false;
                                txtSearchUPCCode.Focus();
                                return;
                            }
                            else
                            {
                                txtSearchUPCCode.Text = Product[1].ToUpper().Replace("P", "");
                                _SellPrice = Functions.GetDecimal(Qty[1]) / 100;
                                Quantity = Qty[0];
                            }
                        }
                        else if (Product[0] == "")
                        {
                            ClsCommon.MsgBox("Information", "You must key in the price first! ", false);
                            txtSearchUPCCode.Text = null;
                            LoginInfo.tnfn = false;
                            txtSearchUPCCode.Focus();
                            return;
                        }
                        else
                        {
                            txtSearchUPCCode.Text = Product[1].ToUpper().Replace("P", "");
                            Quantity = "1";
                            string s = Product[0];
                            decimal result;
                            if (decimal.TryParse(s, out result))
                            {
                                _SellPrice = Functions.GetDecimal(Product[0]) / 100;
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "You must key in the correct price!", false);
                                txtSearchUPCCode.Text = null;
                                LoginInfo.tnfn = false;
                                txtSearchUPCCode.Focus();
                                return;
                            }
                        }
                    }

                    txtSearchUPCCode.Text = txtSearchUPCCode.Text.Remove(txtSearchUPCCode.Text.Length - 1);
                    DataTable dt = new DataTable();
                    //SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

                    //if (LoginInfo.Connections)
                    //{
                    //    var query = from DM in _db.tbl_DepartmentMaster
                    //                where DM.DepartmentNo == Functions.GetLong(txtSearchUPCCode.Text)
                    //                    select DM;

                    //    dt = ClsCommon.LinqToDataTable(query);
                    //}
                    //else
                    //{
                    //    string query = "SELECT * FROM tbl_DepartmentMaster " +
                    //                   "WHERE DepartmentNo=@DepartmentID";
                    //    DataAdapter = new SqlCeDataAdapter(query, conn);
                    //    DataAdapter.SelectCommand.Parameters.AddWithValue("@DepartmentID", txtSearchUPCCode.Text);
                    //    DataAdapter.Fill(dt);
                    //}
                    dt = GetDepartment(Functions.GetLong(txtSearchUPCCode.Text));

                    if (dt.Rows.Count > 0)
                    {
                        OrderScanner_ResultModel _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                        _OrderScanner_ResultModel.ProductID = 0;
                        _OrderScanner_ResultModel.IsScale = IsScale;
                        _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt.Rows[0]["TaxGroupID"].ToString());
                        _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt.Rows[0]["DepartmentID"].ToString());
                        _OrderScanner_ResultModel.AgeVerification = (!String.IsNullOrEmpty(dt.Rows[0]["AgeVarificationAge"].ToString()) ? true : false);
                        if (dt.Rows[0]["AgeVarificationAge"].ToString() == "0")
                        {
                            _OrderScanner_ResultModel.AgeVerification = false;
                        }
                        _OrderScanner_ResultModel.SectionID = 0;
                        _OrderScanner_ResultModel.SellPrice = _SellPrice;

                        if (weight != "")
                        {
                            _OrderScanner_ResultModel.Qty = Functions.GetDecimal(weight);
                        }
                        else
                        {
                            _OrderScanner_ResultModel.Qty = Functions.GetDecimal(Quantity);
                        }

                        _OrderScanner_ResultModel.UPCCode = "D" + Product[1];
                        _OrderScanner_ResultModel.ProductName = dt.Rows[0]["DepartmentName"].ToString();
                        _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt.Rows[0]["UnitMeasureID"].ToString());

                        if (LoginInfo.tnfn)
                        {
                            _OrderScanner_ResultModel.IsFoodStamp = false;
                        }
                        else
                        {
                            _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt.Rows[0]["IsFoodStamp"].ToString());
                        }
                        _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());

                        #region Tax Calculation
                        if (LoginInfo.tnfn)
                        {
                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                            _Tax = LoginInfo.StoreDefaultTax;
                            _OrderScanner_ResultModel.IsTax = true;
                            LoginInfo.tnfn = false;
                        }
                        else
                        {
                            dt = new DataTable();
                            if (_OrderScanner_ResultModel.TaxGroupID > 0)
                            {
                                if (LoginInfo.IsStoreTax)
                                {
                                    _Tax = LoginInfo.StoreDefaultTax;
                                    _OrderScanner_ResultModel.IsTax = true;
                                    LoginInfo.tnfn = false;
                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                }
                                else
                                {
                                    dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                    if (dt.Rows.Count > 0)
                                    {
                                        _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsTax = false;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                }
                            }
                            else
                            {
                                LoginInfo.tnfn = false;
                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                _OrderScanner_ResultModel.IsTax = false;
                            }
                            dt.Dispose();
                        }
                        #endregion

                        _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                        _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                        _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                        _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty);
                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                        _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString());

                        if (_OrderScanner_ResultModel.AgeVerification == false)
                        {
                            Productdata_.Add(_OrderScanner_ResultModel);
                        }
                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == false)
                        {
                            #region AgeVerification Form Show
                            ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                            #endregion

                            if (AgeVerifidInfo.AgeVerified == true && AgeVerifidInfo.AgeChecked == true && LoginInfo.CashierAgeVerified == true)
                            {
                                Productdata_.Add(_OrderScanner_ResultModel);
                            }
                        }
                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == false && LoginInfo.CashierAgeVerified == false)
                        {
                            #region AgeVerification Form Show
                            ageVerifi(Functions.GetLong(_OrderScanner_ResultModel.DepartmentID.ToString()), Functions.GetLong(_OrderScanner_ResultModel.SectionID.ToString()));
                            #endregion
                        }
                        else if (_OrderScanner_ResultModel.AgeVerification == true && AgeVerifidInfo.AgeChecked == true && AgeVerifidInfo.AgeVerified == true && LoginInfo.CashierAgeVerified == true)
                        {
                            Productdata_.Add(_OrderScanner_ResultModel);
                        }
                        txtSearchUPCCode.Text = null;
                        txtSearchUPCCode.Focus();
                        dataLoad();

                        //if (IsBeep == true)
                        //    GetBeepSound();
                    }
                    else
                    {
                        ClsCommon.MsgBox("Information", "Can't sale this product !!!", false);
                        LoginInfo.tnfn = false;
                        LoginInfo.CasePrice = false;
                        txtSearchUPCCode.Text = null;
                    }
                }
            }
            catch (Exception ex)
            {
                txtSearchUPCCode.Text = null;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                txtSearchUPCCode.Focus();
            }
        }

        public DataTable TaxCalculation(long TaxGroupID)
        {
            DataTable dt = new DataTable();
            try
            {
                if (LoginInfo.Connections)
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var queryTax = from TX in _db.tbl_TaxRateMaster
                                   where TX.TaxGroupID == TaxGroupID && TX.StartDate <= DateTime.Now && TX.EndDate >= DateTime.Now && TX.IsDelete == false
                                   select TX;

                    dt = ClsCommon.LinqToDataTable(queryTax);
                }
                else
                {
                    string query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date AND IsDelete = 0 ORDER BY StartDate,EndDate";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _OrderScanner_ResultModel.TaxGroupID);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    DataAdapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                return dt;
            }
        }

        public DataTable GetDepartment(long DepartmentNo)
        {
            DataTable dt = new DataTable();
            try
            {
                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                if (LoginInfo.Connections)
                {
                    var query = from DM in _db.tbl_DepartmentMaster
                                where DM.DepartmentNo == DepartmentNo
                                select DM;

                    dt = ClsCommon.LinqToDataTable(query);
                }
                else
                {
                    string query = "SELECT * FROM tbl_DepartmentMaster " +
                                   "WHERE DepartmentNo=@DepartmentID";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@DepartmentID", txtSearchUPCCode.Text);
                    DataAdapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                return dt;
            }
        }

        public void OpenPV()
        {
            try
            {
                txtSearchUPCCode.Text = "";
               DeviceRemove();
                frmPriceCheck obj = new frmPriceCheck();
                obj.ShowDialog();
                DeviceAdd();
                txtSearchUPCCode.Text = "";
                txtSearchUPCCode.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void RefundCommand()
        {
            try
            {
                decimal _SellPrice = 0;
                decimal _Discount = 0;
                decimal _Tax = 0;

                if (txtSearchUPCCode.Text.Trim().ToUpper().StartsWith("RF"))
                {
                    txtSearchUPCCode.Text = txtSearchUPCCode.Text.ToUpper().Replace("RF", "");

                    if (txtSearchUPCCode.Text.Trim().ToLower().Contains("dp"))
                    {
                        string[] ProductName = txtSearchUPCCode.Text.ToUpper().Split('D');
                        //ProductName[0] = ((Functions.GetDecimal(ProductName[0])) / 100).ToString();
                        decimal value;
                        if (Decimal.TryParse(ProductName[0], out value))
                        {
                            ProductName[0] = ((Functions.GetDecimal(ProductName[0])) / 100).ToString();
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "Please enter correct price format.", false);
                            return;
                        }
                        ProductName[1] = ProductName[1].ToUpper().Replace("P", "");
                        string UPCCode = "dp" + ProductName[1];
                        ProductName[1] = ProductName[1].Remove(ProductName[1].Length - 1);

                        DataTable dt = new DataTable();
                        //string query = "SELECT * FROM tbl_DepartmentMaster " +
                        //               "WHERE DepartmentID=@DepartmentID";
                        //DataAdapter = new SqlCeDataAdapter(query, conn);
                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@DepartmentID", ProductName[1]);
                        //DataAdapter.Fill(dt);
                        dt = GetDepartment(Functions.GetLong(ProductName[1]));
                        txtSearchUPCCode.Text = "";

                        if (dt.Rows.Count > 0)
                        {
                            OrderScanner_ResultModel _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                            _OrderScanner_ResultModel.ProductID = 0;
                            _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt.Rows[0]["TaxGroupID"].ToString());
                            _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt.Rows[0]["DepartmentID"].ToString());
                            _OrderScanner_ResultModel.SectionID = 0;
                            //_OrderScanner_ResultModel.SellPrice = _SellPrice = ProductName[0];
                            _OrderScanner_ResultModel.UPCCode = UPCCode;
                            _OrderScanner_ResultModel.ProductName = dt.Rows[0]["DepartmentName"].ToString();
                            _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt.Rows[0]["UnitMeasureID"].ToString());
                            if (LoginInfo.tnfn)
                            {
                                _OrderScanner_ResultModel.IsFoodStamp = false;
                            }
                            else
                            {
                                _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt.Rows[0]["IsFoodStamp"].ToString());
                            }
                            _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(ProductName[0]);
                            _OrderScanner_ResultModel.Qty = -1;
                            _OrderScanner_ResultModel.IsRefund = true;

                            #region Tax Cal
                            if (LoginInfo.tnfn)
                            {
                                _Tax = LoginInfo.StoreDefaultTax;
                                _OrderScanner_ResultModel.IsTax = true;
                                LoginInfo.tnfn = false;
                            }
                            else
                            {
                                dt = new DataTable();
                                if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                {
                                    if (LoginInfo.IsStoreTax)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                    else
                                    {
                                        //query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                        //DataAdapter = new SqlCeDataAdapter(query, conn);
                                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _OrderScanner_ResultModel.TaxGroupID);
                                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                        //DataAdapter.Fill(dt);

                                        dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                        if (dt.Rows.Count > 0)
                                        {
                                            _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                            _OrderScanner_ResultModel.IsTax = true;
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsTax = false;
                                }

                                dt.Dispose();
                            }
                            #endregion

                            _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                            _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                            _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                            _OrderScanner_ResultModel.TaxAmount = (Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString()));
                            _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                            _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString());
                            Productdata_.Add(_OrderScanner_ResultModel);

                            txtSearchUPCCode.Text = null;

                            dataLoad();
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "Can't refund for this product !!!", false);
                            txtSearchUPCCode.Text = null;
                        }
                    }

                    else if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                    {
                        string[] ProductName = txtSearchUPCCode.Text.Split('*');
                        string[] UPC_e = txtSearchUPCCode.Text.Split('*');

                        #region Check UPC Code Count
                        ProductName[1] = ClsCommon.GetFullUPCCode(ProductName[1]);
                        #endregion

                        #region Normal UPC Code
                        DataTable dt_P = new DataTable();
                        string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                        "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                        "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.IsGroupPrice," +
                                        "PM.UnitMeasureID " +
                                        "FROM tbl_ProductMaster AS PM " +
                                        "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";

                        if (LoginInfo.Connections)
                        {
                            SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                            dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName[1]));
                        }
                        else
                        {
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName[1]);
                            DataAdapter.Fill(dt_P);
                        }
                        #endregion

                        #region UPC_E Code
                        if (UPC_e[1].Length > 6)
                            UPC_e[1] = Functions.GetUPC_E(UPC_e[1]);
                        if (dt_P.Rows.Count == 0)
                        {
                            dt_P.Dispose();
                            dt_P = new DataTable();
                            //DataAdapter = new SqlCeDataAdapter(query, conn);
                            //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPC_e[1]);
                            //DataAdapter.Fill(dt_P);

                            if (LoginInfo.Connections)
                            {
                                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(UPC_e[1]));
                            }
                            else
                            {
                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPC_e[1]);
                                DataAdapter.Fill(dt_P);
                            }
                        }
                        #endregion

                        #region Labled Price
                        if (dt_P.Rows.Count == 0)
                        {
                            string LastPrice = ProductName[1].Substring(ProductName[1].Length - 5, 5);
                            ProductName[1] = ProductName[1].Replace(LastPrice, "00000");

                            dt_P.Dispose();
                            dt_P = new DataTable();

                            if (LoginInfo.Connections)
                            {
                                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName[1]));
                            }
                            else
                            {
                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName[1]);
                                DataAdapter.Fill(dt_P);
                            }

                            int FirstChar = Convert.ToInt32(LastPrice.Substring(0, 1));
                            FirstChar = FirstChar - 1;
                            LastPrice = LastPrice.Remove(0, 1);
                            LastPrice = LastPrice.Insert(2, ".");

                            _SellPrice = Functions.GetDecimal(LastPrice);
                        }
                        #endregion

                        #region PK Product
                        if (dt_P.Rows.Count == 0)
                        {
                            dt_P.Dispose();
                            dt_P = new DataTable();
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            string EnterUPCCode = txtSearchUPCCode.Text.Trim();
                            string[] UPCCOde = EnterUPCCode.ToUpper().Split('P');

                            ProductName[0] = UPCCOde[1].ToUpper().Replace("K", "");

                            if (UPCCOde[0] == "")
                            {
                                txtSearchUPCCode.Text = null;
                                txtSearchUPCCode.Focus();
                                ClsCommon.MsgBox("Information", "You must key in the price ! ", false);
                                return;
                            }
                            else
                            {
                                _SellPrice = Functions.GetDecimal(UPCCOde[0]) / 100;
                            }
                            ProductName[0] = ProductName[0].ToLower().Replace("pk", "");
                            ProductName[0] = ClsCommon.GetFullUPCCode(ProductName[0]);
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName[0]);
                            DataAdapter.Fill(dt_P);
                        }
                        #endregion

                        txtSearchUPCCode.Text = "";
                        if (dt_P.Rows.Count > 0)
                        {
                            #region Product Data
                            OrderScanner_ResultModel _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                            if (LoginInfo.Connections)
                            {
                                _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                                if (Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString()) == true)
                                {
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + (lblWeight.Text.Replace("lb", "")));
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + ProductName[0]);
                                }
                            }
                            else
                            {
                                _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                                if (Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString()) == true)
                                {
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + (lblWeight.Text.Replace("lb", "")));
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + ProductName[0]);
                                }
                            }

                            _OrderScanner_ResultModel.IsRefund = true;
                            _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                            _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                            _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                            _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                            //_OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                            if (_SellPrice > 0)
                            {
                                _OrderScanner_ResultModel.SellPrice = _SellPrice;
                            }
                            else
                            {
                                if (LoginInfo.Connections)
                                {
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString());
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                                }
                            }
                            _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                            _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());

                            #region Product Others Fields
                            _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                            _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                            _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                            _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                            _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();

                            //_OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                            _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());

                            _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                            _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());
                            _OrderScanner_ResultModel.IsGroupPrice = Functions.GetBoolean(dt_P.Rows[0]["IsGroupPrice"].ToString());
                            #endregion

                            #region Tax Cal
                            if (LoginInfo.tnfn)
                            {
                                _Tax = LoginInfo.StoreDefaultTax;
                                _OrderScanner_ResultModel.IsTax = true;
                                _OrderScanner_ResultModel.IsFoodStamp = false;
                                LoginInfo.tnfn = false;
                            }
                            else
                            {
                                DataTable dt_Tax = new DataTable();
                                if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                {
                                    if (LoginInfo.IsStoreTax)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                    else
                                    {
                                        //query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                        //DataAdapter = new SqlCeDataAdapter(query, conn);
                                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _OrderScanner_ResultModel.TaxGroupID);
                                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                        //DataAdapter.Fill(dt_Tax);

                                        dt_Tax = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                        if (dt_Tax.Rows.Count > 0)
                                        {
                                            _Tax = Functions.GetDecimal(dt_Tax.Rows[0]["Tax"].ToString());
                                            _OrderScanner_ResultModel.IsTax = true;
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsTax = false;
                                }
                                dt_Tax.Dispose();
                            }
                            #endregion

                            #region IsGroup
                            if (!String.IsNullOrEmpty(_OrderScanner_ResultModel.GroupQty.ToString()) && _OrderScanner_ResultModel.IsGroupPrice == true)
                            {
                                if (_OrderScanner_ResultModel.GroupQty > 0)
                                {
                                    IsGroup = true;
                                }
                                else
                                {
                                    IsGroup = false;
                                }
                            }
                            else
                            {
                                IsGroup = false;
                            }
                            #endregion

                            _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                            _OrderScanner_ResultModel.Tax = Functions.GetDecimal(((_Tax)).ToString());
                            _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                            _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                            _OrderScanner_ResultModel.TaxAmount = (Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString()));
                            _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                            _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString());

                            #endregion

                            if (IsGroup == true && LoginInfo.CasePrice == false && _OrderScanner_ResultModel.IsScale == false)
                            {
                                OrderScanner_ResultModel objOrderScanner_ResultModel_ = new OrderScanner_ResultModel();
                                objOrderScanner_ResultModel_ = _OrderScanner_ResultModel;
                                if (!AddItem)
                                {
                                    #region Grouping
                                    decimal TotalItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false).Sum(item => item.Qty);
                                    decimal GroupItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true).Sum(item => item.Qty) / objOrderScanner_ResultModel_.GroupQty;
                                    TotalItem = TotalItem + objOrderScanner_ResultModel_.Qty;
                                    TotalItem = Math.Abs(TotalItem);
                                    Group = 0; UnGroup = 0;
                                    for (int i = 0; TotalItem != 0; i++)
                                    {
                                        if (TotalItem >= objOrderScanner_ResultModel_.GroupQty)
                                        {
                                            Group++;
                                            TotalItem -= objOrderScanner_ResultModel_.GroupQty;
                                        }
                                        else
                                        {
                                            UnGroup = TotalItem;
                                            TotalItem = 0;
                                        }
                                    }
                                    #endregion

                                    if (Group > 0)
                                    {
                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false);
                                        #region LinkedUPCCode
                                        if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                        {
                                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                        }
                                        #endregion

                                        objOrderScanner_ResultModel_.DiscountApplyed = true;
                                        objOrderScanner_ResultModel_.Qty = -Convert.ToInt32(Math.Floor(Convert.ToDecimal(objOrderScanner_ResultModel_.GroupQty * Group)));
                                        objOrderScanner_ResultModel_.FinalPrice = -objOrderScanner_ResultModel_.GroupPrice * Group;
                                        objOrderScanner_ResultModel_.TaxAmount = -(objOrderScanner_ResultModel_.FinalPrice * objOrderScanner_ResultModel_.Tax) / 100;
                                        objOrderScanner_ResultModel_.TaxAmount = -Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel_.TaxAmount.ToString()).ToString("0.00"));
                                        objOrderScanner_ResultModel_.RowNo = GetRowNo();
                                        objOrderScanner_ResultModel_.IsVerifed = 1;
                                        Productdata_.Add(objOrderScanner_ResultModel_);

                                        #region LinkedUPCCode
                                        if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                        {
                                            IsRefundItem = true;
                                            LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, objOrderScanner_ResultModel_.Qty, 1, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                        }
                                        #endregion

                                        objOrderScanner_ResultModel_ = new OrderScanner_ResultModel();
                                        objOrderScanner_ResultModel_ = _OrderScanner_ResultModel;
                                        if (UnGroup > 0)
                                        {
                                            decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                            if (itemCount > UnGroup)
                                            {
                                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);

                                                #region LinkedUPCCode
                                                if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                {
                                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                }
                                                #endregion

                                                itemCount = 0;
                                            }
                                            UnGroup = UnGroup - itemCount;
                                            for (int i = 0; i < UnGroup; i++)
                                            {
                                                IsRefundItem = true;
                                                LinkUPCCodeAdd(objOrderScanner_ResultModel_.UPCCode, 1, 0, "", objOrderScanner_ResultModel_.IsForceTax);

                                                #region LinkedUPCCode
                                                if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                {
                                                    IsRefundItem = true;
                                                    LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);
                                            #region LinkedUPCCode
                                            if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                            {
                                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                            }
                                            #endregion
                                        }
                                    }
                                    else if (UnGroup > 0)
                                    {
                                        if (Group > 0)
                                        {
                                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1);
                                            #region LinkedUPCCode
                                            if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                            {
                                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                            }
                                            #endregion
                                        }
                                        decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                        UnGroup = UnGroup - itemCount;
                                        for (int i = 0; i < UnGroup; i++)
                                        {
                                            IsRefundItem = true;
                                            LinkUPCCodeAdd(objOrderScanner_ResultModel_.UPCCode, 1, 0, "", objOrderScanner_ResultModel_.IsForceTax);

                                            #region LinkedUPCCode
                                            if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                            {
                                                IsRefundItem = true;
                                                LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.RowNo = GetRowNo();
                                        Productdata_.Add(_OrderScanner_ResultModel);
                                    }
                                    dataLoad();
                                }
                            }
                            else
                            {
                                Productdata_.Add(_OrderScanner_ResultModel);

                                if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                {
                                    txtSearchUPCCode.Text = "RF" + ProductName[0] + "*" + dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                    dt_P.Dispose();
                                    RefundCommand();
                                }
                                else
                                {
                                    dt_P.Dispose();
                                }
                            }
                            dataLoad();
                        }
                    }

                    else
                    {
                        string ProductName = txtSearchUPCCode.Text.Trim();
                        string[] UPC_e = txtSearchUPCCode.Text.Split();

                        #region Check UPC Code Count
                        int Count = ProductName.Length;
                        if (Count < 13)
                        {
                            Count = 13 - Count;
                            for (int i = 0; i < Count; i++)
                            {
                                ProductName = "0" + ProductName;
                            }
                        }
                        #endregion

                        if (UPC_e[0].Length > 6)
                            UPC_e[0] = Functions.GetUPC_E(UPC_e[0]);

                        DataTable dt_P = new DataTable();
                        //string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                        //            "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                        //            "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.IsGroupPrice," +
                        //            "PM.UnitMeasureID " +
                        //            "FROM tbl_ProductMaster AS PM " +
                        //            "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0";
                        //DataAdapter = new SqlCeDataAdapter(query, conn);
                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                        //DataAdapter.Fill(dt_P);

                        string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                        "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                        "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.IsGroupPrice," +
                                        "PM.UnitMeasureID " +
                                        "FROM tbl_ProductMaster AS PM " +
                                        "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";

                        if (LoginInfo.Connections)
                        {
                            SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                            dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName));
                        }
                        else
                        {
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                            DataAdapter.Fill(dt_P);
                        }

                        if (dt_P.Rows.Count == 0)
                        {
                            dt_P.Dispose();
                            dt_P = new DataTable();
                            //DataAdapter = new SqlCeDataAdapter(query, conn);
                            //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPC_e[0]);
                            //DataAdapter.Fill(dt_P);
                            if (LoginInfo.Connections)
                            {
                                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(UPC_e[0]));
                            }
                            else
                            {
                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPC_e[0]);
                                DataAdapter.Fill(dt_P);
                            }
                        }
                        if (dt_P.Rows.Count == 0)
                        {
                            string LastPrice = ProductName.Substring(ProductName.Length - 5, 5);
                            ProductName = ProductName.Replace(LastPrice, "00000");

                            dt_P.Dispose();
                            dt_P = new DataTable();

                            //DataAdapter = new SqlCeDataAdapter(query, conn);
                            //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                            //DataAdapter.Fill(dt_P);

                            if (LoginInfo.Connections)
                            {
                                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName));
                            }
                            else
                            {
                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                                DataAdapter.Fill(dt_P);
                            }

                            int FirstChar = Convert.ToInt32(LastPrice.Substring(0, 1));
                            FirstChar = FirstChar - 1;
                            LastPrice = LastPrice.Remove(0, 1);
                            LastPrice = LastPrice.Insert(2, ".");

                            _SellPrice = Functions.GetDecimal(LastPrice);
                        }
                        if (dt_P.Rows.Count == 0)
                        {
                            dt_P.Dispose();
                            dt_P = new DataTable();
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            string EnterUPCCode = txtSearchUPCCode.Text.Trim();
                            string[] UPCCOde = EnterUPCCode.ToUpper().Split('P');

                            ProductName = UPCCOde[1].ToUpper().Replace("K", "");

                            if (UPCCOde[0] == "")
                            {
                                txtSearchUPCCode.Text = null;
                                txtSearchUPCCode.Focus();
                                ClsCommon.MsgBox("Information", "You must key in the price ! ", false);
                                return;
                            }
                            else
                            {
                                _SellPrice = Functions.GetDecimal(UPCCOde[0]) / 100;
                            }
                            ProductName = ProductName.ToLower().Replace("pk", "");
                            Count = ProductName.Length;

                            Count = 13 - Count;
                            for (int i = 0; i < Count; i++)
                            {
                                ProductName = "0" + ProductName;
                            }
                            DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                            DataAdapter.Fill(dt_P);
                        }

                        txtSearchUPCCode.Text = "";

                        if (dt_P.Rows.Count > 0)
                        {
                            #region Product Data
                            OrderScanner_ResultModel _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                            if (LoginInfo.Connections)
                            {
                                _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                                if (Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString()) == true)
                                {
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + (lblWeight.Text.Replace("lb", "")));

                                }
                                else
                                {
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-1");
                                }
                            }
                            else
                            {
                                _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                                if (Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString()) == true)
                                {
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + (lblWeight.Text.Replace("lb", "")));
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-1");
                                }
                            }

                            _OrderScanner_ResultModel.IsRefund = true;
                            _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                            _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                            _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                            _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                            //_OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                            if (_SellPrice > 0)
                            {
                                _OrderScanner_ResultModel.SellPrice = _SellPrice;
                            }
                            else
                            {
                                if (LoginInfo.Connections)
                                {
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString());
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                                }
                            }
                            _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                            _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                            _OrderScanner_ResultModel.IsGroupPrice = Functions.GetBoolean(dt_P.Rows[0]["IsGroupPrice"].ToString());

                            #region Product Others Fields
                            _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                            _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                            _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                            _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                            _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();

                            _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                            _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                            _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());

                            #endregion

                            #region Tax Cal
                            if (LoginInfo.tnfn)
                            {
                                _Tax = LoginInfo.StoreDefaultTax;
                                _OrderScanner_ResultModel.IsTax = true;
                                _OrderScanner_ResultModel.IsFoodStamp = false;
                                LoginInfo.tnfn = false;
                            }
                            else
                            {
                                DataTable dt_Tax = new DataTable();
                                if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                {
                                    if (LoginInfo.IsStoreTax)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        LoginInfo.tnfn = false;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    }
                                    else
                                    {
                                        //query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                        //DataAdapter = new SqlCeDataAdapter(query, conn);
                                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _OrderScanner_ResultModel.TaxGroupID);
                                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                        //DataAdapter.Fill(dt_Tax);
                                        dt_Tax = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                        if (dt_Tax.Rows.Count > 0)
                                        {
                                            _Tax = Functions.GetDecimal(dt_Tax.Rows[0]["Tax"].ToString());
                                            _OrderScanner_ResultModel.IsTax = true;
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsTax = false;
                                }

                                dt_Tax.Dispose();
                            }
                            #endregion

                            #region IsGroup
                            if (!String.IsNullOrEmpty(_OrderScanner_ResultModel.GroupQty.ToString()) && _OrderScanner_ResultModel.IsGroupPrice == true)
                            {
                                if (_OrderScanner_ResultModel.GroupQty > 0)
                                {
                                    IsGroup = true;
                                }
                                else
                                {
                                    IsGroup = false;
                                }
                            }
                            else
                            {
                                IsGroup = false;
                            }
                            #endregion

                            _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                            _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                            _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                            _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                            _OrderScanner_ResultModel.TaxAmount = (Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString()));
                            _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                            _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString());

                            #endregion


                            if (IsGroup == true && LoginInfo.CasePrice == false && _OrderScanner_ResultModel.IsScale == false)
                            {
                                OrderScanner_ResultModel objOrderScanner_ResultModel_ = new OrderScanner_ResultModel();
                                objOrderScanner_ResultModel_ = _OrderScanner_ResultModel;
                                if (!AddItem)
                                {
                                    #region Grouping
                                    decimal TotalItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false).Sum(item => item.Qty);
                                    decimal GroupItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true).Sum(item => item.Qty) / objOrderScanner_ResultModel_.GroupQty;
                                    TotalItem = TotalItem + objOrderScanner_ResultModel_.Qty;
                                    TotalItem = Math.Abs(TotalItem);

                                    Group = 0; UnGroup = 0;
                                    for (int i = 0; TotalItem != 0; i++)
                                    {
                                        if (TotalItem >= objOrderScanner_ResultModel_.GroupQty)
                                        {
                                            Group++;
                                            TotalItem -= objOrderScanner_ResultModel_.GroupQty;
                                        }
                                        else
                                        {
                                            UnGroup = TotalItem;
                                            TotalItem = 0;
                                        }
                                    }
                                    #endregion

                                    if (Group > 0)
                                    {
                                        Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false);
                                        #region LinkedUPCCode
                                        if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                        {
                                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                        }
                                        #endregion

                                        objOrderScanner_ResultModel_.DiscountApplyed = true;
                                        objOrderScanner_ResultModel_.Qty = -Convert.ToInt32(Math.Floor(Convert.ToDecimal(objOrderScanner_ResultModel_.GroupQty * Group)));
                                        objOrderScanner_ResultModel_.FinalPrice = -objOrderScanner_ResultModel_.GroupPrice * Group;
                                        objOrderScanner_ResultModel_.TaxAmount = -(objOrderScanner_ResultModel_.FinalPrice * objOrderScanner_ResultModel_.Tax) / 100;
                                        objOrderScanner_ResultModel_.TaxAmount = -Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel_.TaxAmount.ToString()).ToString("0.00"));
                                        objOrderScanner_ResultModel_.RowNo = GetRowNo();
                                        objOrderScanner_ResultModel_.IsVerifed = 1;
                                        Productdata_.Add(objOrderScanner_ResultModel_);

                                        #region LinkedUPCCode
                                        if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                        {
                                            IsRefundItem = true;
                                            LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, objOrderScanner_ResultModel_.Qty, 1, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                        }
                                        #endregion

                                        objOrderScanner_ResultModel_ = new OrderScanner_ResultModel();
                                        objOrderScanner_ResultModel_ = _OrderScanner_ResultModel;
                                        if (UnGroup > 0)
                                        {
                                            decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                            if (itemCount > UnGroup)
                                            {
                                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);

                                                #region LinkedUPCCode
                                                if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                {
                                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                                }
                                                #endregion

                                                itemCount = 0;
                                            }
                                            itemCount = Math.Abs(itemCount);
                                            UnGroup = UnGroup - itemCount;
                                            for (int i = 0; i < UnGroup; i++)
                                            {
                                                IsRefundItem = true;
                                                LinkUPCCodeAdd(objOrderScanner_ResultModel_.UPCCode, 1, 0, "", objOrderScanner_ResultModel_.IsForceTax);

                                                #region LinkedUPCCode
                                                if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                                {
                                                    IsRefundItem = true;
                                                    LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);
                                            #region LinkedUPCCode
                                            if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                            {
                                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                            }
                                            #endregion
                                        }
                                    }
                                    else if (UnGroup > 0)
                                    {
                                        if (Group > 0)
                                        {
                                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1);
                                            #region LinkedUPCCode
                                            if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                            {
                                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel_.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1 && x.ParentUPCCode == objOrderScanner_ResultModel_.UPCCode);
                                            }
                                            #endregion
                                        }
                                        decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel_.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                        itemCount = Math.Abs(itemCount);
                                        UnGroup = UnGroup - itemCount;
                                        for (int i = 0; i < UnGroup; i++)
                                        {
                                            IsRefundItem = true;
                                            LinkUPCCodeAdd(objOrderScanner_ResultModel_.UPCCode, 1, 0, "", objOrderScanner_ResultModel_.IsForceTax);

                                            #region LinkedUPCCode
                                            if (objOrderScanner_ResultModel_.LinkedUPCCode != "" && objOrderScanner_ResultModel_.LinkedUPCCode != null)
                                            {
                                                IsRefundItem = true;
                                                LinkUPCCodeAdd(objOrderScanner_ResultModel_.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel_.UPCCode, objOrderScanner_ResultModel_.IsForceTax);
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.RowNo = GetRowNo();
                                        Productdata_.Add(_OrderScanner_ResultModel);
                                    }
                                    dataLoad();
                                }
                            }
                            else
                            {
                                Productdata_.Add(_OrderScanner_ResultModel);
                                if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                {
                                    txtSearchUPCCode.Text = "RF" + dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                    dt_P.Dispose();
                                    RefundCommand();
                                }
                                else
                                {
                                    dt_P.Dispose();
                                }
                            }
                        }
                    }
                    dataLoad();
                    txtSearchUPCCode.Text = "";
                    txtSearchUPCCode.Focus();
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Can't refund for this product !!!", false);
                    txtSearchUPCCode.Text = null;
                }
                txtSearchUPCCode.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                txtSearchUPCCode.Focus();
            }
        }

        public void VoidCommand(string qty, string UPCCode, string SellPrice, string TaxPercentage, bool IsForceTax, string FinalPrice, bool CaseQtyPriceApplyed, bool IsScale_)
        {
            bool IsGroup = false;
            try
            {
                OrderScanner_ResultModel _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                decimal _SellPrice = 0;
                decimal _Discount = 0;
                decimal _Tax = 0;

                #region CheckCasePrice Item                     
                if (LoginInfo.CasePrice)
                {
                    ClsCommon.MsgBox("Information", "To void case-price applied product, Select the product from the cart and void!", false);
                    txtSearchUPCCode.Text = null;
                    txtSearchUPCCode.Focus();
                    LoginInfo.CasePrice = false;
                    return;
                }
                #endregion

                #region Void Item
                if (qty != "" && UPCCode != "")
                {
                    string[] ProductName = new string[2];
                    ProductName[0] = qty;
                    ProductName[1] = UPCCode;

                    #region Validation 
                    decimal temoQty = 0;
                    if (!CaseQtyPriceApplyed)
                    {
                        if (Productdata_.Count > 0)
                        {
                            for (int i = 0; i < Productdata_.Count; i++)
                            {
                                if (ProductName[1] == Productdata_[i].LinkedUPCCode && IsLinkVoid != true)
                                {
                                    ClsCommon.MsgBox("Information", "You can't directly void the linked product!", false);
                                    txtSearchUPCCode.Text = null;
                                    txtSearchUPCCode.Focus();
                                    return;
                                }
                                else
                                {
                                    if (!IsScale_)
                                    {
                                        if (Productdata_[i].UPCCode == ProductName[1] && Productdata_[i].CasePriceApplied == false)
                                        {
                                            temoQty = temoQty + Productdata_[i].Qty;
                                        }
                                        else if (Productdata_[i].UPCCode == ProductName[1] && Productdata_[i].CasePriceApplied == true)
                                        {
                                            temoQty = Productdata_[i].Qty;
                                        }
                                    }
                                    else
                                    {
                                        if (Productdata_[i].UPCCode == ProductName[1] && Productdata_[i].IsVoid == false)
                                        {
                                            temoQty = temoQty + Productdata_[i].Qty;
                                        }
                                        else if (Productdata_[i].UPCCode == ProductName[1] && Productdata_[i].IsVoid == true)
                                        {
                                            temoQty = temoQty - Math.Abs(Productdata_[i].Qty);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        temoQty = Functions.GetDecimal(qty);
                    }
                    #endregion

                    if (Functions.GetDecimal(ProductName[0]) <= temoQty)
                    {
                        if (ProductName[1].ToLower().Contains("dp"))
                        {
                            ProductName[1] = ProductName[1].ToUpper().Replace("DP", "");
                            ProductName[1] = ProductName[1].Remove(ProductName[1].Length - 1);

                            DataTable dt = new DataTable();
                            dt = GetDepartment(Functions.GetLong(ProductName[1]));

                            txtSearchUPCCode.Text = "";
                            if (dt.Rows.Count > 0)
                            {
                                _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                                _OrderScanner_ResultModel.ProductID = 0;
                                _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt.Rows[0]["TaxGroupID"].ToString());
                                _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt.Rows[0]["DepartmentID"].ToString());
                                _OrderScanner_ResultModel.SectionID = 0;
                                _OrderScanner_ResultModel.SellPrice = _SellPrice = (Functions.GetDecimal(SellPrice != "" ? SellPrice.Replace("$", "").Trim() : "0"));
                                _OrderScanner_ResultModel.UPCCode = UPCCode;
                                _OrderScanner_ResultModel.ProductName = dt.Rows[0]["DepartmentName"].ToString();
                                _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt.Rows[0]["UnitMeasureID"].ToString());
                                _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt.Rows[0]["IsFoodStamp"].ToString());
                                _OrderScanner_ResultModel.Qty = -(Functions.GetDecimal(qty));
                                _OrderScanner_ResultModel.IsRefund = false;
                                _OrderScanner_ResultModel.IsVoid = true;

                                #region Tax Cal
                                if (IsForceTax == true)
                                {
                                    _Tax = LoginInfo.StoreDefaultTax;
                                    _OrderScanner_ResultModel.IsTax = true;
                                    _OrderScanner_ResultModel.IsForceTax = true;
                                    _OrderScanner_ResultModel.IsFoodStamp = false;
                                    LoginInfo.tnfn = false;
                                }
                                else
                                {
                                    dt = new DataTable();
                                    if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                    {
                                        if (LoginInfo.IsStoreTax)
                                        {
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                            if (dt.Rows.Count > 0)
                                            {
                                                _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                _OrderScanner_ResultModel.IsTax = true;
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsTax = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsTax = false;
                                    }

                                    dt.Dispose();
                                }
                                #endregion

                                _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                _OrderScanner_ResultModel.TaxAmount = (Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString()));
                                _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty);
                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                                _OrderScanner_ResultModel.FinalPrice = (Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString()));

                                #region Check Max Void Amount
                                LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount + Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                #endregion

                                if (LoginInfo.TotalVoidAmount > LoginInfo.MaxVoidAmount)
                                {
                                    LoginInfo.IsManagerReq = true;
                                    ClsCommon.MsgBox("Information", "You reached your maximum void amount, Please enter the manager' password!", false);

                                    DeviceRemove();
                                    FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                                    obj.ShowDialog();
                                    DeviceAdd();

                                    if (obj.Isverified == true)
                                    {
                                        LoginInfo.IsManagerReq = false;
                                        _OrderScanner_ResultModel.RowNo = GetRowNo();
                                        VoidModel = _OrderScanner_ResultModel;
                                    }
                                    else
                                    {
                                        LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount - Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                        LoginInfo.IsManagerReq = false;
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.RowNo = GetRowNo();
                                    VoidModel = _OrderScanner_ResultModel;
                                }

                                txtSearchUPCCode.Text = null;
                                txtSearchUPCCode.Focus();
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "Can't void this product !!!", false);
                                txtSearchUPCCode.Text = null;
                            }
                        }
                        else
                        {
                            #region Void Product
                            DataTable dt_P = new DataTable();
                            if (LoginInfo.Connections)
                            {
                                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName[1]));
                            }
                            else
                            {
                                string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                            "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                            "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.CaseQty,PM.CasePrice,PM.IsGroupPrice," +
                                            "PM.UnitMeasureID " +
                                            "FROM tbl_ProductMaster AS PM " +
                                            "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";

                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName[1]);
                                DataAdapter.Fill(dt_P);
                            }
                            txtSearchUPCCode.Text = "";
                            if (dt_P.Rows.Count > 0)
                            {
                                #region Product Data
                                _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                                _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + ProductName[0].Replace("-", ""));
                                _OrderScanner_ResultModel.IsVoid = true;
                                _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                                _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                                _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                                if (LoginInfo.Connections)
                                {
                                    _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString());
                                }
                                else
                                {
                                    _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                                }
                                _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                if (SellPrice != "")
                                {
                                    if (Functions.GetDecimal(SellPrice != "" ? SellPrice.Replace("$", "").Trim() : "0") >= 0)
                                    {
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice = (Functions.GetDecimal(SellPrice != "" ? SellPrice.Replace("$", "").Trim() : "0"));
                                    }
                                }
                                _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                                _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                                _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                                _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());
                                _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());

                                #region IsGroup
                                if (!String.IsNullOrEmpty(dt_P.Rows[0]["GroupQty"].ToString()) && !String.IsNullOrEmpty(dt_P.Rows[0]["GroupPrice"].ToString())
                                    && Functions.GetBoolean(dt_P.Rows[0]["IsGroupPrice"].ToString()) == true)
                                {
                                    //if (dt_P.Rows[0]["GroupQty"].ToString() != "0.00")
                                    if(Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString()) > 0)
                                    {
                                        IsGroup = true;
                                    }
                                    else
                                    {
                                        IsGroup = false;
                                    }
                                }
                                else
                                {
                                    IsGroup = false;
                                }
                                #endregion

                                _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                                _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                                _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                                _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                if (LoginInfo.Connections)
                                {
                                    _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                                }
                              
                                _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());

                                #region Tax Cal
                                if (IsForceTax == true)
                                {
                                    _Tax = LoginInfo.StoreDefaultTax;
                                    _OrderScanner_ResultModel.IsTax = true;
                                    _OrderScanner_ResultModel.IsFoodStamp = false;
                                    _OrderScanner_ResultModel.IsForceTax = true;
                                    LoginInfo.tnfn = false;
                                }
                                else
                                {
                                    DataTable dt = new DataTable();
                                    if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                    {
                                        if (LoginInfo.IsStoreTax)
                                        {
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                            if (dt.Rows.Count > 0)
                                            {
                                                _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                _OrderScanner_ResultModel.IsTax = true;
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsTax = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsTax = false;
                                    }
                                    dt.Dispose();
                                }
                                #endregion

                                #region Calculations

                                _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                                _OrderScanner_ResultModel.FinalPrice = (Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString()));

                                if (IsGroup == true && _OrderScanner_ResultModel.IsScale == false)
                                {
                                    if (_OrderScanner_ResultModel.Qty == _OrderScanner_ResultModel.GroupQty)
                                    {
                                        _OrderScanner_ResultModel.FinalPrice = -Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                        _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100;
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                    }
                                }
                                else if (IsGroup == true && _OrderScanner_ResultModel.IsScale == true)
                                {

                                }
                                if (CaseQtyPriceApplyed)
                                {
                                    if (_OrderScanner_ResultModel.CaseQty > 0 && _OrderScanner_ResultModel.CasePrice > 0)
                                    {
                                        IsGroup = false;
                                        _OrderScanner_ResultModel.FinalPrice = -Functions.GetDecimal(FinalPrice.Replace("$", ""));
                                        _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100;
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        _OrderScanner_ResultModel.Qty = _OrderScanner_ResultModel.Qty;
                                        _OrderScanner_ResultModel.CasePriceApplied = false;
                                    }
                                    else
                                    {

                                    }
                                    LoginInfo.CasePrice = false;
                                }

                                #endregion

                                #region Check Max Void Amount
                                LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount + Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                #endregion

                                if (LoginInfo.TotalVoidAmount > LoginInfo.MaxVoidAmount)
                                {
                                    LoginInfo.IsManagerReq = true;
                                    ClsCommon.MsgBox("Information", "You reached your maximum void amount, Please enter the manager' password!", false);

                                   DeviceRemove();
                                    FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                                    obj.ShowDialog();
                                    DeviceAdd();

                                    if (obj.Isverified == true)
                                    {
                                        LoginInfo.IsManagerReq = false;
                                        _OrderScanner_ResultModel.RowNo = GetRowNo();
                                        //Productdata_.Add(_OrderScanner_ResultModel);
                                        if (IsLinkVoid)
                                        {
                                            LinkVoidModel = _OrderScanner_ResultModel;
                                        }
                                        else
                                        {
                                            VoidModel = _OrderScanner_ResultModel;
                                        }

                                        #region Linked Product
                                        if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                        {
                                            IsLinkVoid = true;
                                            VoidCommand(ProductName[0], dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                            dt_P.Dispose();
                                            IsLinkVoid = false;
                                        }
                                        else
                                        {
                                            dt_P.Dispose();
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount - Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString())); LoginInfo.IsManagerReq = false;
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.RowNo = GetRowNo();
                                    //Productdata_.Add(_OrderScanner_ResultModel);
                                    if (IsLinkVoid)
                                    {
                                        LinkVoidModel = _OrderScanner_ResultModel;
                                    }
                                    else
                                    {
                                        VoidModel = _OrderScanner_ResultModel;
                                    }

                                    #region Linked Product
                                    if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                    {
                                        IsLinkVoid = true;
                                        VoidCommand(ProductName[0], dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                        dt_P.Dispose();
                                        IsLinkVoid = false;
                                    }
                                    else
                                    {
                                        dt_P.Dispose();
                                    }
                                    #endregion
                                }
                                #endregion

                                txtSearchUPCCode.Text = null;
                                txtSearchUPCCode.Focus();
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        ClsCommon.MsgBox("Information", "Can't void product more than available qty in the cart!", false);
                        txtSearchUPCCode.Text = null;
                        txtSearchUPCCode.Focus();
                        return;
                    }
                }

                else if (txtSearchUPCCode.Text.Trim().ToUpper().StartsWith("VD"))
                {
                    txtSearchUPCCode.Text = txtSearchUPCCode.Text.ToUpper().Replace("VD", "");

                    if (txtSearchUPCCode.Text.Trim().ToUpper().Contains("DP"))
                    {
                        string[] ProductName = txtSearchUPCCode.Text.ToUpper().Split('D');
                        ProductName[0] = (Functions.GetDecimal(ProductName[0]) / 100).ToString();
                        ProductName[1] = ProductName[1].ToUpper().Replace("P", "");
                        string DeptCode = "DP" + ProductName[1];
                        ProductName[1] = ProductName[1].Remove(ProductName[1].Length - 1);

                        #region Validation 
                        decimal temoQty = 0;
                        if (Productdata_.Count > 0)
                        {
                            for (int i = 0; i < Productdata_.Count; i++)
                            {
                                if (Productdata_[i].UPCCode == DeptCode && Productdata_[i].CasePriceApplied == false)
                                {
                                    temoQty = temoQty + Productdata_[i].Qty;
                                    if (Productdata_[i].IsForceTax == true)
                                    {
                                        ClsCommon.MsgBox("Information", "To void Forcetax applied product, Select the product from the cart and void!", false);
                                        txtSearchUPCCode.Text = null;
                                        txtSearchUPCCode.Focus();
                                        return;
                                    }
                                    if (Productdata_[i].IsScale == true)
                                    {
                                        ClsCommon.MsgBox("Information", "To void Scale applied product, Select the product from the cart and void!", false);
                                        txtSearchUPCCode.Text = null;
                                        txtSearchUPCCode.Focus();
                                        return;
                                    }
                                    if (Productdata_[i].ParentUPCCode != "" && Productdata_[i].ParentUPCCode != null)
                                    {
                                        ClsCommon.MsgBox("Information", "You can't directly void the linked product!", false);
                                        txtSearchUPCCode.Text = null;
                                        txtSearchUPCCode.Focus();
                                        return;
                                    }
                                }
                            }
                        }
                        #endregion

                        if (1 <= temoQty)
                        {
                            DataTable dt = new DataTable();
                            dt = GetDepartment(Functions.GetLong(ProductName[1]));
                            txtSearchUPCCode.Text = "";
                            if (dt.Rows.Count > 0)
                            {
                                _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                                _OrderScanner_ResultModel.ProductID = 0;
                                _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt.Rows[0]["TaxGroupID"].ToString());
                                _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt.Rows[0]["DepartmentID"].ToString());
                                _OrderScanner_ResultModel.SectionID = 0;
                                _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(ProductName[0]);
                                _OrderScanner_ResultModel.UPCCode = DeptCode;
                                _OrderScanner_ResultModel.ProductName = dt.Rows[0]["DepartmentName"].ToString();
                                _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt.Rows[0]["UnitMeasureID"].ToString());
                                _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt.Rows[0]["IsFoodStamp"].ToString());
                                _OrderScanner_ResultModel.Qty = -1;
                                _OrderScanner_ResultModel.IsRefund = false;
                                _OrderScanner_ResultModel.IsVoid = true;

                                #region Tax Cal
                                if (LoginInfo.tnfn)
                                {
                                    _Tax = LoginInfo.StoreDefaultTax;
                                    _OrderScanner_ResultModel.IsTax = true;
                                    _OrderScanner_ResultModel.IsFoodStamp = false;

                                    LoginInfo.tnfn = false;
                                }
                                else
                                {
                                    dt = new DataTable();
                                    if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                    {
                                        if (LoginInfo.IsStoreTax)
                                        {
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            //query = "SELECT Tax FROM tbl_TaxRateMaster WHERE TaxGroupID = @TaxGroupID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                                            //DataAdapter = new SqlCeDataAdapter(query, conn);
                                            //DataAdapter.SelectCommand.Parameters.AddWithValue("@TaxGroupID_", _OrderScanner_ResultModel.TaxGroupID);
                                            //DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                                            //DataAdapter.Fill(dt);
                                            dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                            if (dt.Rows.Count > 0)
                                            {
                                                _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                _OrderScanner_ResultModel.IsTax = true;
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsTax = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsTax = false;
                                    }

                                    dt.Dispose();
                                }
                                #endregion

                                _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                _OrderScanner_ResultModel.TaxAmount = (Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString()));
                                _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty);
                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                                _OrderScanner_ResultModel.FinalPrice = (Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString()));

                                #region Check Max Void Amount
                                LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount + Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                #endregion

                                if (LoginInfo.TotalVoidAmount > LoginInfo.MaxVoidAmount)
                                {
                                    LoginInfo.IsManagerReq = true;
                                    ClsCommon.MsgBox("Information", "You reached your maximum void amount, Please enter the manager' password!", false);

                                   DeviceRemove();
                                    FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                                    obj.ShowDialog();
                                    DeviceAdd();

                                    if (obj.Isverified == true)
                                    {
                                        LoginInfo.IsManagerReq = false;
                                        _OrderScanner_ResultModel.RowNo = GetRowNo();
                                        //Productdata_.Add(_OrderScanner_ResultModel);
                                        if (IsLinkVoid)
                                        {
                                            LinkVoidModel = _OrderScanner_ResultModel;
                                        }
                                        else
                                        {
                                            VoidModel = _OrderScanner_ResultModel;
                                        }
                                    }
                                    else
                                    {
                                        LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount - Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString())); LoginInfo.IsManagerReq = false;
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.RowNo = GetRowNo();
                                    VoidModel = _OrderScanner_ResultModel;
                                }

                                txtSearchUPCCode.Text = null;
                            }
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "Can't void product more than available qty in the cart!", false);
                            txtSearchUPCCode.Text = null;
                            txtSearchUPCCode.Focus();
                            return;
                        }
                    }

                    else if (txtSearchUPCCode.Text.Trim().ToLower().Contains("*"))
                    {
                        string[] ProductName = txtSearchUPCCode.Text.Split('*');
                        string[] UPC_e = txtSearchUPCCode.Text.Split('*');

                        #region Check UPC Code Count
                        int Count = ProductName[1].Length;
                        if (Count < 13)
                        {
                            Count = 13 - Count;
                            for (int i = 0; i < Count; i++)
                            {
                                ProductName[1] = "0" + ProductName[1];
                            }
                        }
                        if (UPC_e[1].Length > 6)
                            UPC_e[1] = Functions.GetUPC_E(UPC_e[1]);
                        #endregion

                        #region Validation
                        string FinalUPCCode = "";
                        decimal temoQty = 0;
                        decimal lbltempQty = 0;
                        decimal UPC_EtempQty = 0;
                        string Price = "";
                        string Product = "";
                        if (Productdata_.Count > 0)
                        {
                            for (int i = 0; i < Productdata_.Count; i++)
                            {
                                if (Productdata_[i].UPCCode == ProductName[1] && Productdata_[i].CasePriceApplied == false)
                                {
                                    temoQty = temoQty + Productdata_[i].Qty;
                                    if (Productdata_[i].IsForceTax == true)
                                    {
                                        ClsCommon.MsgBox("Information", "To void Forcetax applied product, Select the product from the cart and void!", false);
                                        txtSearchUPCCode.Text = null;
                                        txtSearchUPCCode.Focus();
                                        return;
                                    }
                                    if (Productdata_[i].IsScale == true)
                                    {
                                        ClsCommon.MsgBox("Information", "To void Scale applied product, Select the product from the cart and void!", false);
                                        txtSearchUPCCode.Text = null;
                                        txtSearchUPCCode.Focus();
                                        return;
                                    }
                                    if (Productdata_[i].ParentUPCCode != "" && Productdata_[i].ParentUPCCode != null)
                                    {
                                        ClsCommon.MsgBox("Information", "You can't directly void the linked product!", false);
                                        txtSearchUPCCode.Text = null;
                                        txtSearchUPCCode.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    Product = ProductName[1].Substring(ProductName[1].Length - 5, 5);
                                    Price = Product;
                                    Product = ProductName[1].Replace(Product, "00000");
                                    if (Productdata_[i].UPCCode == Product)
                                    {
                                        lbltempQty = lbltempQty + Productdata_[i].Qty;
                                        if (Productdata_[i].IsForceTax == true)
                                        {
                                            ClsCommon.MsgBox("Information", "To void Forcetax applied product,Please select the product from the cart and void!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                        if (Productdata_[i].IsScale == true)
                                        {
                                            ClsCommon.MsgBox("Information", "To void Scale applied product, Select the product from the cart and void!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                        if (Productdata_[i].ParentUPCCode != "" && Productdata_[i].ParentUPCCode != null)
                                        {
                                            ClsCommon.MsgBox("Information", "You can't directly void the linked product!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                    }
                                    else if (Productdata_[i].UPCCode == UPC_e[1])
                                    {
                                        UPC_EtempQty = UPC_EtempQty + Productdata_[i].Qty;
                                        if (Productdata_[i].IsForceTax == true)
                                        {
                                            ClsCommon.MsgBox("Information", "To void Forcetax applied product,Please select the product from the cart and void!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                        if (Productdata_[i].IsScale == true)
                                        {
                                            ClsCommon.MsgBox("Information", "To void Scale applied product, Select the product from the cart and void!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                        if (Productdata_[i].ParentUPCCode != "" && Productdata_[i].ParentUPCCode != null)
                                        {
                                            ClsCommon.MsgBox("Information", "You can't directly void the linked product!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (Functions.GetDecimal(ProductName[0]) <= temoQty
                            || Functions.GetDecimal(ProductName[0]) <= lbltempQty
                            || Functions.GetDecimal(ProductName[0]) <= UPC_EtempQty)
                        {
                            #region Void Product
                            DataTable dt_P = new DataTable();

                            if (LoginInfo.Connections)
                            {
                                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                if (Functions.GetDecimal(ProductName[0]) <= lbltempQty)
                                {
                                    //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", Product);
                                    dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(Product));
                                    FinalUPCCode = Product;
                                }
                                else if (Functions.GetDecimal(ProductName[0]) <= temoQty)
                                {
                                    //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName[1]);
                                    dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName[1]));
                                    FinalUPCCode = ProductName[1];
                                }
                                else if (Functions.GetDecimal(ProductName[0]) <= UPC_EtempQty)
                                {
                                    //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPC_e[1]);
                                    dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(UPC_e[1]));
                                    FinalUPCCode = UPC_e[1];
                                }
                            }
                            else
                            {
                                string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                            "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                            "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.CaseQty,PM.CasePrice," +
                                            "PM.UnitMeasureID " +
                                            "FROM tbl_ProductMaster AS PM " +
                                            "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";
                                DataAdapter = new SqlCeDataAdapter(query, conn);


                                if (Functions.GetDecimal(ProductName[0]) <= lbltempQty)
                                {
                                    DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", Product);
                                    FinalUPCCode = Product;
                                }
                                else if (Functions.GetDecimal(ProductName[0]) <= temoQty)
                                {
                                    DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName[1]);
                                    FinalUPCCode = ProductName[1];
                                }
                                else if (Functions.GetDecimal(ProductName[0]) <= UPC_EtempQty)
                                {
                                    DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPC_e[1]);
                                    FinalUPCCode = UPC_e[1];
                                }

                                DataAdapter.Fill(dt_P);
                            }
                            txtSearchUPCCode.Text = "";
                            if (dt_P.Rows.Count > 0)
                            {
                                #region Product Data
                                _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                                if (LoginInfo.Connections)
                                {
                                    if (Functions.GetDecimal(ProductName[0]) <= lbltempQty)
                                    {
                                        int FirstChar = Convert.ToInt32(Price.Substring(0, 1));
                                        FirstChar = FirstChar - 1;
                                        Price = Price.Remove(0, 1);
                                        Price = Price.Insert(2, ".");
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(Price);
                                    }
                                    else if (Functions.GetDecimal(ProductName[0]) <= temoQty)
                                    {
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice = (Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString()));
                                    }
                                    else if (Functions.GetDecimal(ProductName[0]) <= UPC_EtempQty)
                                    {
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice = (Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString()));
                                    }
                                }
                                else
                                {
                                    if (Functions.GetDecimal(ProductName[0]) <= lbltempQty)
                                    {
                                        int FirstChar = Convert.ToInt32(Price.Substring(0, 1));
                                        FirstChar = FirstChar - 1;
                                        Price = Price.Remove(0, 1);
                                        Price = Price.Insert(2, ".");
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(Price);
                                    }
                                    else if (Functions.GetDecimal(ProductName[0]) <= temoQty)
                                    {
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice = (Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString()));
                                    }
                                    else if (Functions.GetDecimal(ProductName[0]) <= UPC_EtempQty)
                                    {
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice = (Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString()));
                                    }
                                }
                                _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + ProductName[0]);
                                _OrderScanner_ResultModel.IsVoid = true;
                                _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                                _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                                _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                                _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                                _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                                _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                                _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());
                                _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());

                                #region IsGroup
                                if (!String.IsNullOrEmpty(dt_P.Rows[0]["GroupQty"].ToString()) && !String.IsNullOrEmpty(dt_P.Rows[0]["GroupPrice"].ToString()))
                                {
                                    if (Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString()) > 0)
                                    {
                                        IsGroup = true;
                                    }
                                    else
                                    {
                                        IsGroup = false;
                                    }

                                }
                                else
                                {
                                    IsGroup = false;
                                }
                                #endregion
                                #endregion

                                #region Product Others Fields
                                _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                                _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                                _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                                _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                if (LoginInfo.Connections)
                                {
                                    _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                                }
                                _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());

                                #endregion

                                #region Tax Cal
                                if (LoginInfo.tnfn)
                                {
                                    _Tax = LoginInfo.StoreDefaultTax;
                                    _OrderScanner_ResultModel.IsTax = true;
                                    _OrderScanner_ResultModel.IsFoodStamp = false;
                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                    LoginInfo.tnfn = false;
                                }
                                else
                                {

                                    DataTable dt = new DataTable();
                                    if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                    {
                                        if (LoginInfo.IsStoreTax)
                                        {
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            LoginInfo.tnfn = false;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        }
                                        else
                                        {
                                            dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                            if (dt.Rows.Count > 0)
                                            {
                                                _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                _OrderScanner_ResultModel.IsTax = true;
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsTax = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsTax = false;
                                    }
                                    dt.Dispose();
                                }
                                #endregion

                                #region Calculations

                                _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                                _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString());

                                if (IsGroup)
                                {
                                    if (_OrderScanner_ResultModel.Qty == _OrderScanner_ResultModel.GroupQty)
                                    {
                                        _OrderScanner_ResultModel.FinalPrice = -Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                        _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100;
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                    }
                                }
                                if (IsCasePrice)
                                {
                                    if (_OrderScanner_ResultModel.CaseQty > 0 && _OrderScanner_ResultModel.CasePrice > 0)
                                    {
                                        IsGroup = false;
                                        _OrderScanner_ResultModel.FinalPrice = -_OrderScanner_ResultModel.CasePrice * _OrderScanner_ResultModel.Qty;
                                        _OrderScanner_ResultModel.TaxAmount = -((_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100) * _OrderScanner_ResultModel.Qty;
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                                        _OrderScanner_ResultModel.Qty = _OrderScanner_ResultModel.Qty * _OrderScanner_ResultModel.CaseQty;
                                        _OrderScanner_ResultModel.CasePriceApplied = true;
                                    }
                                    else
                                    {

                                    }
                                    LoginInfo.CasePrice = false;
                                }
                                #endregion

                                #region Check Max Void Amount
                                LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount + Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                #endregion

                                if (LoginInfo.TotalVoidAmount > LoginInfo.MaxVoidAmount)
                                {
                                    LoginInfo.IsManagerReq = true;
                                    ClsCommon.MsgBox("Information", "You reached your maximum void amount, Please enter the manager' password!", false);

                                   DeviceRemove();
                                    FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                                    obj.ShowDialog();
                                    DeviceAdd();

                                    if (obj.Isverified == true)
                                    {
                                        LoginInfo.IsManagerReq = false;
                                        _OrderScanner_ResultModel.RowNo = GetRowNo();
                                        if (IsLinkVoid)
                                        {
                                            LinkVoidModel = _OrderScanner_ResultModel;
                                        }
                                        else
                                        {
                                            VoidModel = _OrderScanner_ResultModel;
                                        }

                                        #region Linked Product
                                        if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                        {
                                            IsLinkVoid = true;
                                            VoidCommand(_OrderScanner_ResultModel.Qty.ToString(), dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                            dt_P.Dispose();
                                            IsLinkVoid = false;
                                        }
                                        else
                                        {
                                            dt_P.Dispose();
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount - Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString())); LoginInfo.IsManagerReq = false;
                                    }
                                }
                                else
                                {
                                    _OrderScanner_ResultModel.RowNo = GetRowNo();
                                    if (IsLinkVoid)
                                    {
                                        LinkVoidModel = _OrderScanner_ResultModel;
                                    }
                                    else
                                    {
                                        VoidModel = _OrderScanner_ResultModel;
                                    }

                                    #region Linked Product
                                    if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                    {
                                        IsLinkVoid = true;
                                        VoidCommand(_OrderScanner_ResultModel.Qty.ToString(), dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                        dt_P.Dispose();
                                        IsLinkVoid = false;
                                    }
                                    else
                                    {
                                        dt_P.Dispose();
                                    }
                                    #endregion

                                }
                            }
                            #endregion
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "Can't void product more than available qty in the cart!", false);
                            txtSearchUPCCode.Text = null;
                            txtSearchUPCCode.Focus();
                            return;
                        }
                    }

                    else
                    {
                        string ProductName = txtSearchUPCCode.Text.Trim();
                        string UPC_e = txtSearchUPCCode.Text.Trim();
                        if (ProductName.ToUpper().Contains("PK"))
                        {
                            string EnterUPCCode = ProductName;
                            string[] UPCCOde = EnterUPCCode.ToUpper().Split('P');

                            ProductName = UPCCOde[1].ToUpper().Replace("K", "");

                            if (UPCCOde[0] == "")
                            {
                                txtSearchUPCCode.Text = null;
                                txtSearchUPCCode.Focus();
                                ClsCommon.MsgBox("Information", "You must key in the price ! ", false);
                                return;
                            }
                            else
                            {
                                _SellPrice = Functions.GetDecimal(UPCCOde[0]) / 100;
                            }
                            ProductName = ProductName.ToLower().Replace("pk", "");
                            int Count = ProductName.Length;

                            Count = 13 - Count;
                            for (int i = 0; i < Count; i++)
                            {
                                ProductName = "0" + ProductName;
                            }

                            #region Validation 
                            decimal temoQty = 0;
                            if (Productdata_.Count > 0)
                            {
                                for (int i = 0; i < Productdata_.Count; i++)
                                {
                                    if (Productdata_[i].UPCCode == ProductName && Productdata_[i].CasePriceApplied == false)
                                    {
                                        temoQty = temoQty + Productdata_[i].Qty;
                                    }
                                }
                            }
                            #endregion

                            if (Functions.GetDecimal(lblWeight.Text.Replace("lb", "")) <= temoQty)
                            {
                                #region Void Product

                                DataTable dt_P = new DataTable();
                                if (LoginInfo.Connections)
                                {
                                    SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                    dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName));
                                }
                                else
                                {
                                    string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                                "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                                "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.CaseQty,PM.CasePrice," +
                                                "PM.UnitMeasureID " +
                                                "FROM tbl_ProductMaster AS PM " +
                                                "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";
                                    DataAdapter = new SqlCeDataAdapter(query, conn);
                                    DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                                    DataAdapter.Fill(dt_P);
                                }
                                txtSearchUPCCode.Text = "";
                                if (dt_P.Rows.Count > 0)
                                {
                                    #region Product Data
                                    _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + lblWeight.Text.Replace("lb", ""));
                                    _OrderScanner_ResultModel.IsVoid = true;
                                    _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                    _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                                    _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                                    _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                                    _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                    if (SellPrice != "")
                                    {
                                        if (Functions.GetDecimal(SellPrice != "" ? SellPrice.Replace("$", "").Trim() : "0") >= 0)
                                        {
                                            _OrderScanner_ResultModel.SellPrice = _SellPrice = (Functions.GetDecimal(SellPrice != "" ? SellPrice.Replace("$", "").Trim() : "0"));
                                        }
                                    }
                                    _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                                    _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                                    _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                                    _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                    _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());
                                    _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());

                                    #region IsGroup
                                    if (!String.IsNullOrEmpty(dt_P.Rows[0]["GroupQty"].ToString()) && !String.IsNullOrEmpty(dt_P.Rows[0]["GroupPrice"].ToString()))
                                    {
                                        if (Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString()) > 0)
                                        {
                                            IsGroup = true;
                                        }
                                        else
                                        {
                                            IsGroup = false;
                                        }
                                    }
                                    else
                                    {
                                        IsGroup = false;
                                    }
                                    #endregion

                                    _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                                    _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                    _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                                    _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                                    _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                    if (LoginInfo.Connections)
                                    {
                                        _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                                    }
                                    _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());

                                    #region Tax Cal
                                    if (IsForceTax == true)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        _OrderScanner_ResultModel.IsFoodStamp = false;
                                        _OrderScanner_ResultModel.IsForceTax = true;
                                        LoginInfo.tnfn = false;
                                    }
                                    else
                                    {
                                        DataTable dt = new DataTable();
                                        if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                        {
                                            if (LoginInfo.IsStoreTax)
                                            {
                                                _Tax = LoginInfo.StoreDefaultTax;
                                                _OrderScanner_ResultModel.IsTax = true;
                                                LoginInfo.tnfn = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                            else
                                            {
                                                dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                                if (dt.Rows.Count > 0)
                                                {
                                                    _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                }
                                                else
                                                {
                                                    _OrderScanner_ResultModel.IsTax = false;

                                                }
                                            }
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                        dt.Dispose();
                                    }
                                    #endregion

                                    #region Calculations

                                    _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                    _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                    _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                    _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                    _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                                    _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                                    _OrderScanner_ResultModel.FinalPrice = (Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString()));

                                    if (IsGroup)
                                    {
                                        if (_OrderScanner_ResultModel.Qty == _OrderScanner_ResultModel.GroupQty)
                                        {
                                            _OrderScanner_ResultModel.FinalPrice = -Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                            _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100;
                                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        }
                                    }
                                    if (IsCasePrice)
                                    {
                                        if (_OrderScanner_ResultModel.CaseQty > 0 && _OrderScanner_ResultModel.CasePrice > 0)
                                        {
                                            IsGroup = false;
                                            _OrderScanner_ResultModel.FinalPrice = -_OrderScanner_ResultModel.CasePrice * _OrderScanner_ResultModel.Qty;
                                            _OrderScanner_ResultModel.TaxAmount = -((_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100) * _OrderScanner_ResultModel.Qty;
                                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                            _OrderScanner_ResultModel.Qty = _OrderScanner_ResultModel.Qty * _OrderScanner_ResultModel.CaseQty;
                                            _OrderScanner_ResultModel.CasePriceApplied = false;
                                        }
                                        else
                                        {

                                        }
                                        LoginInfo.CasePrice = false;
                                    }
                                    #endregion

                                    #region Check Max Void Amount
                                    LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount + Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                    #endregion

                                    if (LoginInfo.TotalVoidAmount > LoginInfo.MaxVoidAmount)
                                    {
                                        LoginInfo.IsManagerReq = true;
                                        ClsCommon.MsgBox("Information", "You reached your maximum void amount, Please enter the manager' password!", false);

                                       DeviceRemove();
                                        FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                                        obj.ShowDialog();
                                        DeviceAdd();

                                        if (obj.Isverified == true)
                                        {
                                            LoginInfo.IsManagerReq = false;
                                            _OrderScanner_ResultModel.RowNo = GetRowNo();
                                            //Productdata_.Add(_OrderScanner_ResultModel);
                                            if (IsLinkVoid)
                                            {
                                                LinkVoidModel = _OrderScanner_ResultModel;
                                            }
                                            else
                                            {
                                                VoidModel = _OrderScanner_ResultModel;
                                            }

                                            #region Linked Product
                                            if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                            {
                                                IsLinkVoid = true;
                                                VoidCommand(ProductName, dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                                dt_P.Dispose();
                                                IsLinkVoid = false;
                                            }
                                            else
                                            {
                                                dt_P.Dispose();
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount - Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString())); LoginInfo.IsManagerReq = false;
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.RowNo = GetRowNo();
                                        //Productdata_.Add(_OrderScanner_ResultModel);
                                        if (IsLinkVoid)
                                        {
                                            LinkVoidModel = _OrderScanner_ResultModel;
                                        }
                                        else
                                        {
                                            VoidModel = _OrderScanner_ResultModel;
                                        }

                                        #region Linked Product
                                        if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                        {
                                            IsLinkVoid = true;
                                            VoidCommand(ProductName, dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                            dt_P.Dispose();
                                            IsLinkVoid = false;
                                        }
                                        else
                                        {
                                            dt_P.Dispose();
                                        }
                                        #endregion

                                    }
                                    #endregion

                                    txtSearchUPCCode.Text = null;
                                    txtSearchUPCCode.Focus();
                                }
                                #endregion
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "Can't void product more than available qty in the cart!", false);
                            }
                            txtSearchUPCCode.Text = null;
                            txtSearchUPCCode.Focus();
                            return;
                        }
                        else
                        {
                            #region Check UPC Code Count
                            int Count = ProductName.Length;
                            if (Count < 13)
                            {
                                Count = 13 - Count;
                                for (int i = 0; i < Count; i++)
                                {
                                    ProductName = "0" + ProductName;
                                }
                            }
                            #endregion

                            #region Validation 
                            decimal temoQty = 0;
                            decimal tempQty = 0;
                            bool IsScale = false;
                            string Price = "";
                            string Product = "";
                            if (Productdata_.Count > 0)
                            {
                                for (int i = 0; i < Productdata_.Count; i++)
                                {
                                    if (Productdata_[i].UPCCode == ProductName && Productdata_[i].CasePriceApplied == false)
                                    {
                                        temoQty = temoQty + Productdata_[i].Qty;
                                        if (Productdata_[i].IsScale == true)
                                        {
                                            IsScale = true;
                                        }
                                        if (Productdata_[i].IsForceTax == true)
                                        {
                                            ClsCommon.MsgBox("Information", "To void Forcetax applied product, Select the product from the cart and void!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                        if (Productdata_[i].IsScale == true)
                                        {
                                            ClsCommon.MsgBox("Information", "To void Scale applied product, Select the product from the cart and void!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                        if (Productdata_[i].ParentUPCCode != "" && Productdata_[i].ParentUPCCode != null)
                                        {
                                            ClsCommon.MsgBox("Information", "You can't directly void the linked product!", false);
                                            txtSearchUPCCode.Text = null;
                                            txtSearchUPCCode.Focus();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        Product = ProductName.Substring(ProductName.Length - 5, 5);
                                        Price = Product;
                                        Product = ProductName.Replace(Product, "00000");
                                        if (Productdata_[i].UPCCode == Product)
                                        {
                                            tempQty = tempQty + Productdata_[i].Qty;
                                            if (Productdata_[i].IsForceTax == true)
                                            {
                                                ClsCommon.MsgBox("Information", "To void Forcetax applied product, Select the product from the cart and void!", false);
                                                txtSearchUPCCode.Text = null;
                                                txtSearchUPCCode.Focus();
                                                return;
                                            }
                                            if (Productdata_[i].IsScale == true)
                                            {
                                                ClsCommon.MsgBox("Information", "To void Scale applied product, Select the product from the cart and void!", false);
                                                txtSearchUPCCode.Text = null;
                                                txtSearchUPCCode.Focus();
                                                return;
                                            }
                                            if (Productdata_[i].ParentUPCCode != "" && Productdata_[i].ParentUPCCode != null)
                                            {
                                                ClsCommon.MsgBox("Information", "You can't directly void the linked product!", false);
                                                txtSearchUPCCode.Text = null;
                                                txtSearchUPCCode.Focus();
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            if ((1 <= temoQty || 1 <= tempQty) && IsScale == false)
                            {
                                #region Void Product
                                DataTable dt_P = new DataTable();
                                if (LoginInfo.Connections)
                                {
                                    SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                    if (1 <= tempQty)
                                    {
                                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", Product);
                                        dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(Product));
                                    }
                                    else
                                    {
                                        //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                                        dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName));
                                    }
                                }
                                else
                                {
                                    string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                                "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                                "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.CaseQty,PM.CasePrice," +
                                                "PM.UnitMeasureID " +
                                                "FROM tbl_ProductMaster AS PM " +
                                                "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";
                                    DataAdapter = new SqlCeDataAdapter(query, conn);
                                    if (1 <= tempQty)
                                    {
                                        DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", Product);
                                    }
                                    else
                                    {
                                        DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                                    }
                                    DataAdapter.Fill(dt_P);

                                }
                                txtSearchUPCCode.Text = "";
                                if (dt_P.Rows.Count > 0)
                                {
                                    #region Product Data
                                    _OrderScanner_ResultModel = new OrderScanner_ResultModel();

                                    if (1 <= tempQty)
                                    {
                                        int FirstChar = Convert.ToInt32(Price.Substring(0, 1));
                                        FirstChar = FirstChar - 1;
                                        Price = Price.Remove(0, 1);
                                        Price = Price.Insert(2, ".");
                                        _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(Price);
                                    }
                                    else if (1 <= temoQty)
                                    {
                                        if (LoginInfo.Connections)
                                        {
                                            _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString());
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                                        }
                                    }
                                    _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-1");
                                    _OrderScanner_ResultModel.IsVoid = true;
                                    _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                    _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                                    _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                                    _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());

                                    _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                                    _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                                    _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                                    _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                    _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());
                                    _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());

                                    #region IsGroup
                                    if (!String.IsNullOrEmpty(dt_P.Rows[0]["GroupQty"].ToString()) && !String.IsNullOrEmpty(dt_P.Rows[0]["GroupPrice"].ToString()))
                                    {
                                        if (Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString()) > 0)
                                        {
                                            IsGroup = true;
                                        }
                                        else
                                        {
                                            IsGroup = false;
                                        }
                                    }
                                    else
                                    {
                                        IsGroup = false;
                                    }
                                    #endregion

                                    #region Product Others Fields
                                    _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                                    _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                    _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                                    _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                                    _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                    if (LoginInfo.Connections)
                                    {
                                        _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                                    }
                                    _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());
                                    #endregion

                                    #region Tax Cal
                                    if (LoginInfo.tnfn)
                                    {
                                        _Tax = LoginInfo.StoreDefaultTax;
                                        _OrderScanner_ResultModel.IsTax = true;
                                        _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                        _OrderScanner_ResultModel.IsFoodStamp = false;
                                        LoginInfo.tnfn = false;
                                    }
                                    else
                                    {
                                        DataTable dt = new DataTable();
                                        if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                        {
                                            if (LoginInfo.IsStoreTax)
                                            {
                                                _Tax = LoginInfo.StoreDefaultTax;
                                                _OrderScanner_ResultModel.IsTax = true;
                                                LoginInfo.tnfn = false;
                                                _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            }
                                            else
                                            {
                                                dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                                if (dt.Rows.Count > 0)
                                                {
                                                    _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                }
                                                else
                                                {
                                                    _OrderScanner_ResultModel.IsTax = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsTax = false;
                                        }
                                        dt.Dispose();
                                    }
                                    #endregion

                                    _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                    _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                    _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                    _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                    _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                    _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                                    _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                    _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString());

                                    if (IsGroup)
                                    {
                                        if (_OrderScanner_ResultModel.Qty == _OrderScanner_ResultModel.GroupQty)
                                        {
                                            _OrderScanner_ResultModel.FinalPrice = -Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                            _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100;
                                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        }
                                    }
                                    if (IsCasePrice)
                                    {
                                        if (_OrderScanner_ResultModel.CaseQty > 0 && _OrderScanner_ResultModel.CasePrice > 0)
                                        {
                                            IsGroup = false;
                                            _OrderScanner_ResultModel.FinalPrice = -_OrderScanner_ResultModel.CasePrice * _OrderScanner_ResultModel.Qty;
                                            _OrderScanner_ResultModel.TaxAmount = -((_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100) * _OrderScanner_ResultModel.Qty;
                                            _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                            _OrderScanner_ResultModel.Qty = _OrderScanner_ResultModel.Qty * _OrderScanner_ResultModel.CaseQty;
                                            _OrderScanner_ResultModel.CasePriceApplied = false;
                                        }
                                        else
                                        {

                                        }
                                        LoginInfo.CasePrice = false;
                                    }

                                    #endregion

                                    #region Check Max Void Amount
                                    LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount + Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                    #endregion

                                    if (LoginInfo.TotalVoidAmount > LoginInfo.MaxVoidAmount)
                                    {
                                        LoginInfo.IsManagerReq = true;
                                        ClsCommon.MsgBox("Information", "You reached your maximum void amount, Please enter the manager' password!", false);

                                       DeviceRemove();
                                        FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                                        obj.ShowDialog();
                                        DeviceAdd();

                                        if (obj.Isverified == true)
                                        {
                                            LoginInfo.IsManagerReq = false;
                                            _OrderScanner_ResultModel.RowNo = GetRowNo();
                                            if (IsLinkVoid)
                                            {
                                                LinkVoidModel = _OrderScanner_ResultModel;
                                            }
                                            else
                                            {
                                                VoidModel = _OrderScanner_ResultModel;
                                            }

                                            #region Linked Product
                                            if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                            {
                                                IsLinkVoid = true;
                                                VoidCommand("1", dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                                dt_P.Dispose();
                                                IsLinkVoid = false;
                                            }
                                            else
                                            {
                                                dt_P.Dispose();
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount - Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.Value.ToString())); LoginInfo.IsManagerReq = false;
                                        }
                                    }
                                    else
                                    {
                                        _OrderScanner_ResultModel.RowNo = GetRowNo();
                                        if (IsLinkVoid)
                                        {
                                            LinkVoidModel = _OrderScanner_ResultModel;
                                        }
                                        else
                                        {
                                            VoidModel = _OrderScanner_ResultModel;
                                        }

                                        #region Linked Product
                                        if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                        {
                                            IsLinkVoid = true;
                                            VoidCommand("1", dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                            dt_P.Dispose();
                                            IsLinkVoid = false;
                                        }
                                        else
                                        {
                                            dt_P.Dispose();
                                        }
                                        #endregion
                                    }

                                }
                                #endregion
                            }
                            else if (IsScale)
                            {
                                if (0 <= temoQty || 0 <= tempQty)
                                {
                                    #region Void Product
                                    DataTable dt_P = new DataTable();
                                    if (LoginInfo.Connections)
                                    {
                                        SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                        if (1 <= tempQty)
                                        {
                                            //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", Product);
                                            dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(Product));
                                        }
                                        else
                                        {
                                            //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                                            dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(ProductName));
                                        }
                                    }
                                    else
                                    {
                                        string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                                    "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                                    "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.CaseQty,PM.CasePrice," +
                                                    "PM.UnitMeasureID " +
                                                    "FROM tbl_ProductMaster AS PM " +
                                                    "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";
                                        DataAdapter = new SqlCeDataAdapter(query, conn);
                                        if (1 <= tempQty)
                                        {
                                            DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", Product);
                                        }
                                        else
                                        {
                                            DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", ProductName);
                                        }
                                        DataAdapter.Fill(dt_P);
                                    }
                                    txtSearchUPCCode.Text = "";
                                    if (dt_P.Rows.Count > 0)
                                    {
                                        #region Product Data
                                        _OrderScanner_ResultModel = new OrderScanner_ResultModel();
                                        if (LoginInfo.Connections)
                                        {
                                            _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString());
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                                        }
                                        _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-" + lblWeight.Text.Replace("lb", ""));
                                        _OrderScanner_ResultModel.IsVoid = true;
                                        _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                        _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                                        _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                                        _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());

                                        _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                                        _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                                        _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                                        _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                        _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());
                                        _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());

                                        #region IsGroup
                                        if (!String.IsNullOrEmpty(dt_P.Rows[0]["GroupQty"].ToString()) && !String.IsNullOrEmpty(dt_P.Rows[0]["GroupPrice"].ToString()))
                                        {
                                            if (Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString()) > 0)
                                            {
                                                IsGroup = true;
                                            }
                                            else
                                            {
                                                IsGroup = false;
                                            }

                                        }
                                        else
                                        {
                                            IsGroup = false;
                                        }
                                        #endregion

                                        #region Product Others Fields
                                        _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                                        _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                        _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                                        _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                                        _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                        if (LoginInfo.Connections)
                                        {
                                            _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                                        }
                                        _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());
                                        #endregion

                                        #region Tax Cal
                                        if (LoginInfo.tnfn)
                                        {
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                            _OrderScanner_ResultModel.IsFoodStamp = false;
                                            LoginInfo.tnfn = false;
                                        }
                                        else
                                        {
                                            DataTable dt = new DataTable();
                                            if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                            {
                                                if (LoginInfo.IsStoreTax)
                                                {
                                                    _Tax = LoginInfo.StoreDefaultTax;
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                    LoginInfo.tnfn = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                                else
                                                {
                                                    dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                        _OrderScanner_ResultModel.IsTax = true;
                                                    }
                                                    else
                                                    {
                                                        _OrderScanner_ResultModel.IsTax = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsTax = false;
                                            }
                                            dt.Dispose();
                                        }
                                        #endregion

                                        //Cal
                                        _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                        _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                        _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                        _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                        _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString());

                                        if (IsGroup)
                                        {
                                            if (_OrderScanner_ResultModel.Qty == _OrderScanner_ResultModel.GroupQty)
                                            {
                                                _OrderScanner_ResultModel.FinalPrice = -Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                                _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100;
                                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                            }
                                        }
                                        if (IsCasePrice)
                                        {
                                            if (_OrderScanner_ResultModel.CaseQty > 0 && _OrderScanner_ResultModel.CasePrice > 0)
                                            {
                                                IsGroup = false;
                                                _OrderScanner_ResultModel.FinalPrice = -_OrderScanner_ResultModel.CasePrice * _OrderScanner_ResultModel.Qty;
                                                _OrderScanner_ResultModel.TaxAmount = -((_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100) * _OrderScanner_ResultModel.Qty;
                                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                                _OrderScanner_ResultModel.Qty = _OrderScanner_ResultModel.Qty * _OrderScanner_ResultModel.CaseQty;
                                                _OrderScanner_ResultModel.CasePriceApplied = false;
                                            }
                                            else
                                            {

                                            }
                                            LoginInfo.CasePrice = false;
                                        }
                                        #endregion

                                        #region Check Max Void Amount
                                        LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount + Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                        #endregion

                                        if (LoginInfo.TotalVoidAmount > LoginInfo.MaxVoidAmount)
                                        {
                                            LoginInfo.IsManagerReq = true;
                                            ClsCommon.MsgBox("Information", "You reached your maximum void amount, Please enter the manager' password!", false);

                                           DeviceRemove();
                                            FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                                            obj.ShowDialog();
                                            DeviceAdd();

                                            if (obj.Isverified == true)
                                            {
                                                LoginInfo.IsManagerReq = false;
                                                _OrderScanner_ResultModel.RowNo = GetRowNo();
                                                if (IsLinkVoid)
                                                {
                                                    LinkVoidModel = _OrderScanner_ResultModel;
                                                }
                                                else
                                                {
                                                    VoidModel = _OrderScanner_ResultModel;
                                                }

                                                #region Linked Product
                                                if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                                {
                                                    IsLinkVoid = true;
                                                    VoidCommand("1", dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                                    dt_P.Dispose();
                                                    IsLinkVoid = false;
                                                }
                                                else
                                                {
                                                    dt_P.Dispose();
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount - Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.Value.ToString())); LoginInfo.IsManagerReq = false;
                                            }
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.RowNo = GetRowNo();
                                            if (IsLinkVoid)
                                            {
                                                LinkVoidModel = _OrderScanner_ResultModel;
                                            }
                                            else
                                            {
                                                VoidModel = _OrderScanner_ResultModel;
                                            }

                                            #region Linked Product
                                            if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                            {
                                                IsLinkVoid = true;
                                                VoidCommand("1", dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                                dt_P.Dispose();
                                                IsLinkVoid = false;
                                            }
                                            else
                                            {
                                                dt_P.Dispose();
                                            }
                                            #endregion
                                        }

                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                string[] UPC_E = UPC_e.ToUpper().Split('D');
                                if (UPC_E[0].Length > 6)
                                    UPC_E[0] = Functions.GetUPC_E(UPC_E[0].ToString());

                                if (Productdata_.Count > 0)
                                {
                                    for (int i = 0; i < Productdata_.Count; i++)
                                    {
                                        if (Productdata_[i].UPCCode == UPC_E[0] && Productdata_[i].CasePriceApplied == false)
                                        {
                                            temoQty = temoQty + Productdata_[i].Qty;
                                        }
                                    }
                                }

                                if (1 <= temoQty)
                                {
                                    DataTable dt_P = new DataTable();
                                    if (LoginInfo.Connections)
                                    {
                                        SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                        dt_P = ClsCommon.ListToDataTable(_OrderScannerService.GetScannedUPCCode(UPC_E[0]));
                                    }
                                    else
                                    {
                                        string query = "SELECT PM.ProductID,PM.UPCCode,PM.ProductName,PM.Price,PM.DepartmentID,PM.SectionID," +
                                                    "PM.TaxGroupID,PM.AgeVerification,PM.TareWeight,PM.LabeledPrice,PM.LinkedUPCCode," +
                                                    "PM.IsScaled,PM.GroupQty,PM.GroupPrice,PM.IsFoodStamp,PM.CaseQty,PM.CasePrice," +
                                                    "PM.UnitMeasureID " +
                                                    "FROM tbl_ProductMaster AS PM " +
                                                    "WHERE PM.UPCCode = @UPCCode AND PM.IsDelete = 0 AND PM.IsActive = 1";
                                        DataAdapter = new SqlCeDataAdapter(query, conn);
                                        DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPC_E[0]);
                                        DataAdapter.Fill(dt_P);
                                    }
                                    if (dt_P.Rows.Count > 0)
                                    {
                                        #region Product Data
                                        _OrderScanner_ResultModel = new OrderScanner_ResultModel();

                                        _OrderScanner_ResultModel.Qty = Functions.GetDecimal("-1");
                                        _OrderScanner_ResultModel.IsVoid = true;
                                        _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                        _OrderScanner_ResultModel.TaxGroupID = Functions.GetLong(dt_P.Rows[0]["TaxGroupID"].ToString());
                                        _OrderScanner_ResultModel.DepartmentID = Functions.GetLong(dt_P.Rows[0]["DepartmentID"].ToString());
                                        _OrderScanner_ResultModel.SectionID = Functions.GetLong(dt_P.Rows[0]["SectionID"].ToString());
                                        if (LoginInfo.Connections)
                                        {
                                            _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["SellPrice"].ToString());
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.SellPrice = _SellPrice = Functions.GetDecimal(dt_P.Rows[0]["Price"].ToString());
                                        }
                                        _OrderScanner_ResultModel.LabeledPrice = Functions.GetBoolean(dt_P.Rows[0]["LabeledPrice"].ToString());
                                        _OrderScanner_ResultModel.AgeVerification = Functions.GetBoolean(dt_P.Rows[0]["AgeVerification"].ToString());
                                        _OrderScanner_ResultModel.GroupQty = Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString());
                                        _OrderScanner_ResultModel.GroupPrice = Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                        _OrderScanner_ResultModel.CasePrice = Functions.GetDecimal(dt_P.Rows[0]["CasePrice"].ToString());
                                        _OrderScanner_ResultModel.CaseQty = Functions.GetDecimal(dt_P.Rows[0]["CaseQty"].ToString());

                                        #region IsGroup
                                        if (!String.IsNullOrEmpty(dt_P.Rows[0]["GroupQty"].ToString()) && !String.IsNullOrEmpty(dt_P.Rows[0]["GroupPrice"].ToString()))
                                        {
                                            if (Functions.GetDecimal(dt_P.Rows[0]["GroupQty"].ToString()) > 0)
                                            {
                                                IsGroup = true;
                                            }
                                            else
                                            {
                                                IsGroup = false;
                                            }

                                        }
                                        else
                                        {
                                            IsGroup = false;
                                        }
                                        #endregion

                                        #region Product Others Fields
                                        _OrderScanner_ResultModel.UPCCode = dt_P.Rows[0]["UPCCode"].ToString();
                                        _OrderScanner_ResultModel.ProductID = Functions.GetLong(dt_P.Rows[0]["ProductID"].ToString());
                                        _OrderScanner_ResultModel.ProductName = dt_P.Rows[0]["ProductName"].ToString();
                                        _OrderScanner_ResultModel.UnitMeasureID = Functions.GetLong(dt_P.Rows[0]["UnitMeasureID"].ToString());
                                        _OrderScanner_ResultModel.LinkedUPCCode = dt_P.Rows[0]["LinkedUPCCode"].ToString();
                                        if (LoginInfo.Connections)
                                        {
                                            _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScale"].ToString());
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.IsScale = Functions.GetBoolean(dt_P.Rows[0]["IsScaled"].ToString());
                                        }
                                        _OrderScanner_ResultModel.IsFoodStamp = Functions.GetBoolean(dt_P.Rows[0]["IsFoodStamp"].ToString());

                                        #endregion

                                        #region Tax Cal
                                        if (LoginInfo.tnfn)
                                        {
                                            _Tax = LoginInfo.StoreDefaultTax;
                                            _OrderScanner_ResultModel.IsTax = true;
                                            _OrderScanner_ResultModel.IsFoodStamp = false;
                                            LoginInfo.tnfn = false;
                                        }
                                        else
                                        {
                                            DataTable dt = new DataTable();
                                            if (_OrderScanner_ResultModel.TaxGroupID > 0)
                                            {
                                                if (LoginInfo.IsStoreTax)
                                                {
                                                    _Tax = LoginInfo.StoreDefaultTax;
                                                    _OrderScanner_ResultModel.IsTax = true;
                                                    LoginInfo.tnfn = false;
                                                    _OrderScanner_ResultModel.IsForceTax = LoginInfo.tnfn;
                                                }
                                                else
                                                {
                                                    dt = TaxCalculation(Functions.GetLong(_OrderScanner_ResultModel.TaxGroupID.ToString()));
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        _Tax = Functions.GetDecimal(dt.Rows[0]["Tax"].ToString());
                                                        _OrderScanner_ResultModel.IsTax = true;
                                                    }
                                                    else
                                                    {
                                                        _OrderScanner_ResultModel.IsTax = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                _OrderScanner_ResultModel.IsTax = false;
                                            }
                                            dt.Dispose();
                                        }
                                        #endregion

                                        _OrderScanner_ResultModel.SellPrice = Functions.GetDecimal((_SellPrice).ToString());
                                        _OrderScanner_ResultModel.Discount = Functions.GetDecimal((_Discount).ToString());
                                        _OrderScanner_ResultModel.DiscountAmount = Functions.GetDecimal((((_SellPrice / 100) * _Discount)).ToString());
                                        _OrderScanner_ResultModel.Tax = Functions.GetDecimal((_Tax).ToString());
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(((_SellPrice / 100) * _Tax).ToString());
                                        _OrderScanner_ResultModel.TaxAmount = _OrderScanner_ResultModel.TaxAmount * _OrderScanner_ResultModel.Qty;
                                        _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                        _OrderScanner_ResultModel.FinalPrice = Functions.GetDecimal((_OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty).ToString());

                                        if (IsGroup)
                                        {
                                            if (_OrderScanner_ResultModel.Qty == _OrderScanner_ResultModel.GroupQty)
                                            {
                                                _OrderScanner_ResultModel.FinalPrice = -Functions.GetDecimal(dt_P.Rows[0]["GroupPrice"].ToString());
                                                _OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100;
                                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                            }
                                        }
                                        if (IsCasePrice)
                                        {
                                            if (_OrderScanner_ResultModel.CaseQty > 0 && _OrderScanner_ResultModel.CasePrice > 0)
                                            {
                                                IsGroup = false;
                                                _OrderScanner_ResultModel.FinalPrice = -_OrderScanner_ResultModel.CasePrice * _OrderScanner_ResultModel.Qty;
                                                _OrderScanner_ResultModel.TaxAmount = -((_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100) * _OrderScanner_ResultModel.Qty;
                                                _OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));

                                                _OrderScanner_ResultModel.Qty = _OrderScanner_ResultModel.Qty * _OrderScanner_ResultModel.CaseQty;
                                                _OrderScanner_ResultModel.CasePriceApplied = false;
                                            }
                                            else
                                            {

                                            }
                                            LoginInfo.CasePrice = false;
                                        }
                                        #endregion

                                        #region Check Max Void Amount
                                        LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount + Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString()));
                                        #endregion

                                        if (LoginInfo.TotalVoidAmount > LoginInfo.MaxVoidAmount)
                                        {
                                            LoginInfo.IsManagerReq = true;
                                            ClsCommon.MsgBox("Information", "You reached your maximum void amount, Please enter the manager' password!", false);

                                           DeviceRemove();
                                            FrmCurrentUserPwd obj = new FrmCurrentUserPwd();
                                            obj.ShowDialog();
                                            DeviceAdd();

                                            if (obj.Isverified == true)
                                            {
                                                LoginInfo.IsManagerReq = false;
                                                _OrderScanner_ResultModel.RowNo = GetRowNo();
                                                if (IsLinkVoid)
                                                {
                                                    LinkVoidModel = _OrderScanner_ResultModel;
                                                }
                                                else
                                                {
                                                    VoidModel = _OrderScanner_ResultModel;
                                                }

                                                #region Linked Product
                                                if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                                {
                                                    IsLinkVoid = true;
                                                    VoidCommand("1", dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                                    dt_P.Dispose();
                                                    IsLinkVoid = false;
                                                }
                                                else
                                                {
                                                    dt_P.Dispose();
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                LoginInfo.TotalVoidAmount = LoginInfo.TotalVoidAmount - Math.Abs(Functions.GetDecimal(_OrderScanner_ResultModel.FinalPrice.ToString())); LoginInfo.IsManagerReq = false;
                                            }
                                        }
                                        else
                                        {
                                            _OrderScanner_ResultModel.RowNo = GetRowNo();
                                            if (IsLinkVoid)
                                            {
                                                LinkVoidModel = _OrderScanner_ResultModel;
                                            }
                                            else
                                            {
                                                VoidModel = _OrderScanner_ResultModel;
                                            }

                                            #region Linked Product
                                            if (dt_P.Rows[0]["LinkedUPCCode"].ToString() != "" && dt_P.Rows[0]["LinkedUPCCode"].ToString() != null)
                                            {
                                                IsLinkVoid = true;
                                                VoidCommand("1", dt_P.Rows[0]["LinkedUPCCode"].ToString(), "", "", false, "", false, false);
                                                dt_P.Dispose();
                                                IsLinkVoid = false;
                                            }
                                            else
                                            {
                                                dt_P.Dispose();
                                            }
                                            #endregion
                                        }
                                    }
                                }

                                else
                                {
                                    ClsCommon.MsgBox("Information", "Can't void product more than available qty in the cart!", false);
                                    txtSearchUPCCode.Text = null;
                                    txtSearchUPCCode.Focus();
                                    return;
                                }
                            }
                        }
                    }
                    txtSearchUPCCode.Text = null;
                    txtSearchUPCCode.Focus();
                }

                else
                {
                    ClsCommon.MsgBox("Information", "Can't void this product !!!", false);
                    txtSearchUPCCode.Text = null;
                    txtSearchUPCCode.Focus();
                }
                dataLoad();

                #endregion

                #region GROUP
                OrderScanner_ResultModel objOrderScanner_ResultModel = new OrderScanner_ResultModel();
                objOrderScanner_ResultModel = _OrderScanner_ResultModel;
                if (IsGroup == true && objOrderScanner_ResultModel.IsScale == false)
                {
                    decimal TotalItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false).Sum(item => item.Qty);
                    decimal GroupItem = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true).Sum(item => item.Qty) / objOrderScanner_ResultModel.GroupQty;
                    decimal Isvoid = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == true && x.CasePriceApplied == false).Sum(item => item.Qty);
                    Isvoid = Math.Abs(Isvoid);
                    decimal VoidModelVoid = Math.Abs(VoidModel.Qty);
                    Isvoid = Isvoid + VoidModelVoid;
                    TotalItem = TotalItem - Isvoid;

                    #region Grouping
                    Group = 0; UnGroup = 0;
                    for (int i = 0; TotalItem != 0; i++)
                    {
                        if (TotalItem >= objOrderScanner_ResultModel.GroupQty)
                        {
                            Group++;
                            TotalItem -= objOrderScanner_ResultModel.GroupQty;
                        }
                        else
                        {
                            UnGroup = TotalItem;
                            TotalItem = 0;
                        }
                    }
                    UnGroup = UnGroup + Isvoid;
                    #endregion

                    if (Group > 0)
                    {
                        if (Group != GroupItem)
                        {
                            var grouplist = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false).First();
                            if (grouplist != null)
                            {
                                grouplist.DiscountApplyed = true;
                                grouplist.Qty = objOrderScanner_ResultModel.GroupQty * Group;
                                grouplist.FinalPrice = grouplist.GroupPrice * Group;
                                grouplist.TaxAmount = (grouplist.FinalPrice * grouplist.Tax) / 100;
                                grouplist.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(grouplist.TaxAmount.ToString()).ToString("0.00"));
                                grouplist.IsVerifed = 1;

                                #region LinkedUPCCode
                                if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                                {
                                    var Linkedlist = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false).First();
                                    Linkedlist.Qty = objOrderScanner_ResultModel.GroupQty * Group;
                                    Linkedlist.FinalPrice = Linkedlist.SellPrice * Linkedlist.Qty;
                                    Linkedlist.TaxAmount = (Linkedlist.FinalPrice * Linkedlist.Tax) / 100;
                                    Linkedlist.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(Linkedlist.TaxAmount.ToString()).ToString("0.00"));
                                    Linkedlist.IsVerifed = 1;
                                    Linkedlist.ParentUPCCode = objOrderScanner_ResultModel.UPCCode;
                                }
                                #endregion
                            }
                            else
                            {
                                objOrderScanner_ResultModel.DiscountApplyed = true;
                                objOrderScanner_ResultModel.Qty = objOrderScanner_ResultModel.GroupQty * Group;
                                objOrderScanner_ResultModel.FinalPrice = objOrderScanner_ResultModel.GroupPrice * Group;
                                objOrderScanner_ResultModel.TaxAmount = (objOrderScanner_ResultModel.FinalPrice * objOrderScanner_ResultModel.Tax) / 100;
                                objOrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objOrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                                objOrderScanner_ResultModel.RowNo = GetRowNo();
                                objOrderScanner_ResultModel.IsVerifed = 1;
                                Productdata_.Add(objOrderScanner_ResultModel);

                                #region LinkedUPCCode
                                if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                                {
                                    LinkUPCCodeAdd(objOrderScanner_ResultModel.LinkedUPCCode, objOrderScanner_ResultModel.Qty, 1, objOrderScanner_ResultModel.UPCCode, objOrderScanner_ResultModel.IsForceTax);
                                }
                                #endregion
                            }

                            if (UnGroup > 0)
                            {
                                decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                                UnGroup = UnGroup - itemCount;
                                for (int i = 0; i < UnGroup; i++)
                                {
                                    LinkUPCCodeAdd(objOrderScanner_ResultModel.UPCCode, 1, 0, "", objOrderScanner_ResultModel.IsForceTax);

                                    #region LinkedUPCCode
                                    if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                                    {
                                        LinkUPCCodeAdd(objOrderScanner_ResultModel.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel.UPCCode, objOrderScanner_ResultModel.IsForceTax);
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0);
                                #region LinkedUPCCode
                                if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                                {
                                    Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false && x.IsVerifed == 0 && x.ParentUPCCode == objOrderScanner_ResultModel.UPCCode);
                                }
                                #endregion
                            }
                        }
                    }
                    else if (UnGroup > 0)
                    {
                        if (Group == 0)
                        {
                            Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == true && x.IsVerifed == 1);
                            #region LinkedUPCCode
                            if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                            {
                                Productdata_.RemoveAll(x => x.UPCCode == objOrderScanner_ResultModel.LinkedUPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.IsVerifed == 1 && x.ParentUPCCode == objOrderScanner_ResultModel.UPCCode);
                            }
                            #endregion
                        }

                        decimal itemCount = Productdata_.Where(x => x.UPCCode == objOrderScanner_ResultModel.UPCCode && x.IsVoid == false && x.CasePriceApplied == false && x.DiscountApplyed == false).Sum(item => item.Qty);
                        UnGroup = UnGroup - itemCount;
                        for (int i = 0; i < UnGroup; i++)
                        {
                            LinkUPCCodeAdd(objOrderScanner_ResultModel.UPCCode, 1, 0, "", objOrderScanner_ResultModel.IsForceTax);

                            #region LinkedUPCCode
                            if (objOrderScanner_ResultModel.LinkedUPCCode != "" && objOrderScanner_ResultModel.LinkedUPCCode != null)
                            {
                                LinkUPCCodeAdd(objOrderScanner_ResultModel.LinkedUPCCode, 1, 0, objOrderScanner_ResultModel.UPCCode, objOrderScanner_ResultModel.IsForceTax);
                            }
                            #endregion
                        }
                    }
                }

                if (IsGroup == true && _OrderScanner_ResultModel.IsScale == true)
                {
                    decimal Isvoid_ = Productdata_.Where(x => x.UPCCode == _OrderScanner_ResultModel.UPCCode && x.IsVoid == true && x.CasePriceApplied == false).Sum(item => item.Qty);
                    Isvoid_ = Math.Abs(Isvoid_);
                    OrderScanner_ResultModel tem = new OrderScanner_ResultModel();
                    tem = Productdata_.Where(x => x.UPCCode == _OrderScanner_ResultModel.UPCCode && x.IsVoid == false
                    && x.CasePriceApplied == false && x.Qty == Isvoid_).FirstOrDefault();
                    if (tem != null)
                    {
                        tem.DiscountApplyed = false;
                        tem.FinalPrice = Functions.GetDecimal(Functions.GetDecimal((Isvoid_ * tem.SellPrice).ToString()).ToString("0.00"));

                        tem.TaxAmount = (tem.FinalPrice * tem.Tax) / 100;
                        tem.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(tem.TaxAmount.ToString()).ToString("0.00"));
                        dataLoad();
                    }

                    #region LinkedUPCCode
                    if (_OrderScanner_ResultModel.LinkedUPCCode != "" && _OrderScanner_ResultModel.LinkedUPCCode != null)
                    {
                        txtSearchUPCCode.Text = (_OrderScanner_ResultModel.Qty).ToString() + "*" + _OrderScanner_ResultModel.LinkedUPCCode;
                        ProductAdd();
                        dataLoad();
                    }
                    #endregion

                    //_OrderScanner_ResultModel.FinalPrice = _OrderScanner_ResultModel.SellPrice * _OrderScanner_ResultModel.Qty;
                    //_OrderScanner_ResultModel.TaxAmount = (_OrderScanner_ResultModel.FinalPrice * _OrderScanner_ResultModel.Tax) / 100;
                    //_OrderScanner_ResultModel.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(_OrderScanner_ResultModel.TaxAmount.ToString()).ToString("0.00"));
                }
                #endregion

                if (!IsLinkVoid)
                {
                    Productdata_.Add(VoidModel);
                    if (VoidModel.LinkedUPCCode != null && VoidModel.LinkedUPCCode != "")
                    {
                        Productdata_.Add(LinkVoidModel);
                    }
                    VoidModel = null;
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                txtSearchUPCCode.Focus();
            }
        }

        public void ChangeColor()
        {
            try
            {
                int nRowIndex = dataGridProducts.Rows.Count - 1;

                string i1 = nRowIndex.ToString();
                if (nRowIndex >= 0)
                {
                    for (int i = 0; i < dataGridProducts.Rows.Count; i++)
                    {
                        bool termp_ = Functions.GetBoolean(dataGridProducts.Rows[i].Cells["IsVoid"].Value.ToString());
                        if (termp_)
                        {
                            dataGridProducts.ClearSelection();
                            dataGridProducts.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells["DiscountApplyed"].Value.ToString()))
                        {
                            //dataGridProducts.ClearSelection();
                            //dataGridProducts.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                        }
                        else
                        {
                            dataGridProducts.ScrollBars = ScrollBars.Both;
                            dataGridProducts.Rows[nRowIndex].Selected = true;
                            dataGridProducts.FirstDisplayedScrollingRowIndex = dataGridProducts.RowCount - 1;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void moveUp()
        {
            try
            {
                if (dataGridProducts.RowCount > 0)
                {
                    if (dataGridProducts.SelectedRows.Count > 0)
                    {
                        int rowCount = dataGridProducts.Rows.Count;
                        RowIndex = dataGridProducts.SelectedCells[0].OwningRow.Index;
                        if (RowIndex <= 0)
                        {
                            txtSearchUPCCode.Focus();
                        }
                        else if (rowCount == 1)
                        {
                            dataGridProducts.Rows[0].Selected = true;
                        }
                        else if (RowIndex + 1 == rowCount)
                        {
                            RowIndex = RowIndex - 1;
                            dataGridProducts.Rows[RowIndex].Selected = true;
                            dataGridProducts.FirstDisplayedScrollingRowIndex = RowIndex;
                        }
                        else if (RowIndex < rowCount)
                        {
                            RowIndex = RowIndex - 1;
                            dataGridProducts.Rows[RowIndex].Selected = true;
                            dataGridProducts.FirstDisplayedScrollingRowIndex = RowIndex;
                        }
                        else
                        {
                            RowIndex = RowIndex - 1;
                            dataGridProducts.Rows[RowIndex].Selected = true;
                            dataGridProducts.FirstDisplayedScrollingRowIndex = RowIndex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                txtSearchUPCCode.Focus();
            }
        }

        private void moveDown()
        {
            try
            {
                if (dataGridProducts.RowCount > 0)
                {
                    if (dataGridProducts.SelectedRows.Count > 0)
                    {
                        int rowCount = dataGridProducts.Rows.Count;
                        RowIndex = dataGridProducts.SelectedCells[0].OwningRow.Index;
                        if (rowCount == 1)
                        {
                            dataGridProducts.Rows[0].Selected = true;
                        }
                        else if (RowIndex + 1 == rowCount)
                        {
                            txtSearchUPCCode.Focus();
                        }
                        else if (RowIndex < rowCount)
                        {
                            RowIndex = RowIndex + 1;
                            dataGridProducts.Rows[RowIndex].Selected = true;
                            dataGridProducts.FirstDisplayedScrollingRowIndex = RowIndex;
                        }
                        else
                        {
                            RowIndex = RowIndex - 1;
                            dataGridProducts.Rows[RowIndex].Selected = true;
                            dataGridProducts.FirstDisplayedScrollingRowIndex = RowIndex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                txtSearchUPCCode.Focus();
            }
        }

        public void TillStatus()
        {
            try
            {
                DataTable dt = new DataTable();
                if (LoginInfo.Connections)
                {
                    SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    dt = CashierReceiptMasterSP();

                    DateTime dtStart = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                    DateTime dtEnd = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                    var ObjVoidOrderDetail = (from OD in _db.tbl_OrderDetail.Where(o => o.Quantity < 0 && o.IsRefund == false
                                              && o.CreatedDate >= dtStart
                                              && o.CreatedDate <= dtEnd
                                              && o.StoreID == LoginInfo.StoreID && o.CreatedBy == LoginInfo.UserId)
                                              select new
                                              {
                                                  finalPrice = OD.finalPrice/* + OD.TaxAmount*/,
                                              }).ToList();
                    if (ObjVoidOrderDetail.Count > 0)
                    {
                        PrintTotalOrder.VoidAmount = Functions.GetDecimal(ObjVoidOrderDetail.Sum(x => x.finalPrice).ToString());
                        PrintTotalOrder.VoidCount = ObjVoidOrderDetail.Count();
                    }
                    if (dt.Rows.Count > 0)
                    {
                        PrintTotalOrder.dt = dt;
                        PrintTotalOrder.StoreName = lblStoreName.Text;

                        string regex = "(\\(.*\\))";
                        PrintTotalOrder.CashierName = Regex.Replace(lblLoginName.Text, regex, "");
                        PrintTotalOrder.PrintReceiptForCashier();
                    }
                }
                else
                {
                    string query = "SELECT SUM(IIF(OM.IsCancel = 0, OM.CashAmount,0))-SUM(IIF(OM.IsCancel = 0, OM.ChangeAmount,0)) AS CashAmount"
                                + ", SUM(IIF(OM.IsCancel = 0, OM.CreditCardAmount,0)) AS CreditCardAmount"
                                + ", SUM(IIF(OM.IsCancel = 0, OM.CheckAmount,0)) AS CheckAmount"
                                + ", SUM(IIF(OM.IsCancel = 0, OM.FoodStampAmount,0)) AS FoodStampAmount"
                                + ", ISNULL(SUM(RefundAmount),0) AS RefundAmount"
                                + ", SUM(IIF(OM.RefundAmount <> 0,1,0)) AS RefundCount"
                                + ", SUM(IIF(OM.IsCancel = 1, OM.GrossAmount,0)) AS CancelledAmount"
                                + ", SUM(IIF(OM.IsCancel = 1 AND OM.RefundAmount = 0,1,0)) AS CancelledCount"
                                + " FROM tbl_OrderMaster where StoreID = " + LoginInfo.StoreID + " and CreatedBy = " + LoginInfo.UserId
                                + " and CreatedDate >= '" + DateTime.Now.ToString("MM/dd/yyyy") + "'";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        PrintTotalOrder.dt = dt;
                    }

                    dt.Dispose();
                    dt = new DataTable();

                    query = "SELECT SUM(finalPrice) AS VoidAmount ,SUM(TaxAmount) AS TaxAmount,COUNT(IsVoid) AS VoidCount"
                            + " FROM tbl_OrderDetail where StoreID = " + LoginInfo.StoreID + " and CreatedBy = " + LoginInfo.UserId + "and IsRefund = false and Quantity < 0 "
                            + " and CreatedDate >= '" + DateTime.Now.ToString("MM/dd/yyyy") + "'";

                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        PrintTotalOrder.VoidAmount = Functions.GetDecimal(dt.Rows[0]["VoidAmount"].ToString()) + Functions.GetDecimal(dt.Rows[0]["TaxAmount"].ToString());
                        PrintTotalOrder.VoidCount = Convert.ToInt32(dt.Rows[0]["VoidCount"].ToString());
                    }

                    if (PrintTotalOrder.dt.Rows.Count > 0)
                    {
                        PrintTotalOrder.StoreName = lblStoreName.Text;
                        string regex = "(\\(.*\\))";
                        PrintTotalOrder.CashierName = Regex.Replace(lblLoginName.Text, regex, "");
                        PrintTotalOrder.PrintReceiptForCashier();
                    }
                }

                txtSearchUPCCode.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckMyPrinter()
        {
            try
            {
                ManagementScope scope = new ManagementScope(@"\root\cimv2");
                scope.Connect();

                ManagementObjectSearcher searcher = new
                    ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                string printerName = "";
                foreach (ManagementObject printer in searcher.Get())
                {
                    printerName = printer["Name"].ToString().ToUpper();
                    if (printerName.Equals((XMLData.PrinterName).ToUpper()))
                    {
                        if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetRowNo()
        {
            RowNo++;
            return RowNo;
        }

        public void LinkUPCCodeAdd(string UPCCode, decimal Qty, int IsVerifed, string ParentUPCCode, bool? tnfn)
        {
            List<OrderScanner_ResultModel> Productdata = new List<OrderScanner_ResultModel>();
            //if (TempData_G != null && TempData_G.UPCCode == UPCCode)
            //{
            //    Productdata.Add(TempData_G);
            //}
            //else if (TempData_U != null && TempData_U.UPCCode == UPCCode)
            //{
            //    Productdata.Add(TempData_U);
            //}

            #region Product Scan
            if (Productdata.Count == 0)
            {
                Productdata = UPCScanner(UPCCode);
            }
            #endregion

            #region UPC-E
            if (Productdata.Count == 0)
            {
                //04963406 = 0004900000634
                string UPC_E = "";
                UPC_E = Functions.GetUPC_E(UPCCode);
                Productdata = UPCScanner(UPC_E);
            }
            #endregion

            #region Product Label
            if (Productdata.Count == 0)
            {
                Productdata = AddLabeledPrice(txtSearchUPCCode.Text);
            }
            #endregion

            if (Productdata.Count > 0)
            {
                foreach (OrderScanner_ResultModel objData in Productdata)
                {
                    if (objData.GroupQty > 1)
                    {
                        TempData_G = objData;
                    }
                    else
                    {
                        TempData_U = objData;
                    }

                    if (objData.IsScale == true)
                    {
                        objData.Qty = Functions.GetDecimal(Functions.GetDecimal(Qty.ToString()).ToString("0.00"));
                    }
                    else
                    {
                        objData.Qty = Convert.ToInt32(Math.Floor(Convert.ToDecimal(Qty.ToString())));
                    }

                    objData.IsVerifed = IsVerifed;
                    objData.FinalPrice = objData.SellPrice * objData.Qty;
                    if (tnfn == true)
                    {
                        objData.IsForceTax = tnfn;
                        objData.Tax = LoginInfo.StoreDefaultTax;
                        objData.IsTax = true;
                    }
                    objData.TaxAmount = (objData.FinalPrice * objData.Tax) / 100;
                    objData.TaxAmount = Functions.GetDecimal(Functions.GetDecimal(objData.TaxAmount.ToString()).ToString("0.00"));
                    if (IsRefundItem)
                    {
                        objData.Qty = -objData.Qty;
                        objData.FinalPrice = -objData.FinalPrice;
                        objData.TaxAmount = -objData.TaxAmount;
                        IsRefundItem = false;
                    }
                    objData.ParentUPCCode = ParentUPCCode;
                    objData.RowNo = GetRowNo();
                    Productdata_.Add(objData);
                }
            }
        }

        #endregion

        #region DataSync Functions

        DateTime _LastSyncDateTime;
        private void backgroundWorker_DataSync_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                CurrentIndex++;
                LiveToLocalSync();
                LocalToLiveSync();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void backgroundWorker_DataSync_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                CurrentIndex = 0;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LiveToLocalSync()
        {
            try
            {
                if (CheckConnection())
                {
                    #region Get LastSync Datetime
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var LastSync = _db.tbl_SyncLog.OrderByDescending(x => x.SyncLogID).Where(x =>  x.MacAddress == LoginInfo.MacAddress).FirstOrDefault();
                    if (LastSync != null)
                    {
                        _LastSyncDateTime = Convert.ToDateTime(LastSync.SyncDateTime);

                        LastSync.SyncDateTime = DateTime.Now;
                        LastSync.UpdatedBy = LoginInfo.UserId;
                        LastSync.UpdatedDate = DateTime.Now;
                        _db.SaveChanges();

                        #region Get Updated Table Name

                        var UpdatedTableName = _db.tbl_UpdateLog.Where(x =>  x.MacAddress == LoginInfo.MacAddress && x.IsSync == false)
                                 .ToList();

                        foreach (tbl_UpdateLog data in UpdatedTableName)
                        {
                            string tbl = data.TblName;
                            DateTime? SyncTime = data.SyncDate;

                            switch (tbl)
                            {
                                case "tbl_DepartmentMaster":
                                    tbl_DepartmentMaster(SyncTime);
                                    break;
                                case "tbl_EmployeeMaster":
                                    tbl_EmployeeMaster(SyncTime);
                                    break;
                                case "tbl_ProductMaster":
                                    tbl_ProductMaster(SyncTime);
                                    break;
                                case "tbl_ProductSalePriceMaster":
                                    tbl_ProductSalePriceMaster(SyncTime);
                                    break;
                                case "tbl_ProductUoM":
                                    tbl_ProductUoM(SyncTime);
                                    break;
                                case "tbl_RoleMaster":
                                    tbl_RoleMaster(SyncTime);
                                    break;
                                case "tbl_SectionMaster":
                                    tbl_SectionMaster(SyncTime);
                                    break;
                                case "tbl_ShortcutkeyMaster":
                                    tbl_ShortcutkeyMaster(SyncTime);
                                    break;
                                case "tbl_StoreMaster":
                                    tbl_StoreMaster(SyncTime);
                                    break;
                                case "tbl_TaxGroupMaster":
                                    tbl_TaxGroupMaster(SyncTime);
                                    break;
                                case "tbl_TaxRateMaster":
                                    tbl_TaxRateMaster(SyncTime);
                                    break;
                                case "tbl_UnitMeasureMaster":
                                    tbl_UnitMeasureMaster(SyncTime);
                                    break;
                                case "tbl_RolePermission":
                                    tbl_RolePermission(SyncTime);
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }

                        }

                        #endregion
                    }

                    #endregion
                    else
                    {
                        ClsCommon.MsgBox("Information", "Please sync first with full data", false);
                    }
                }
            }
            catch (Exception ex)
            {
                //ClsCommon.MsgBox("Information","LiveToLocalSync >> " + ex.Message);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LocalToLiveSync()
        {
            try
            {
                if (CheckConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    #region OrderMaster & OrderDetailsMaster
                    string query = "Select * from tbl_OrderMaster";
                    DataTable dt_i = new DataTable();
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.Fill(dt_i);
                    if (dt_i.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_i.Rows.Count; i++)
                        {
                            DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

                            #region InsertOrder
                            OrderMasterModel objOrderMasterModel = new OrderMasterModel();
                            objOrderMasterModel.OrderID = Functions.GetLong(dt_i.Rows[i]["OrderID"].ToString());
                            objOrderMasterModel.CustomerID = Functions.GetLong(dt_i.Rows[i]["CustomerID"].ToString());
                            objOrderMasterModel.PaymentMethodID = Functions.GetLong(dt_i.Rows[i][OrderScanner_ResultModelCont.PaymentMethodID].ToString());
                            objOrderMasterModel.StoreID = Functions.GetLong(dt_i.Rows[i]["StoreID"].ToString());
                            objOrderMasterModel.TotalAmount = Functions.GetDecimal(dt_i.Rows[i]["TotalAmount"].ToString());
                            objOrderMasterModel.TaxAmount = Functions.GetDecimal(dt_i.Rows[i]["TaxAmount"].ToString());
                            objOrderMasterModel.GrossAmount = Functions.GetDecimal(dt_i.Rows[i]["GrossAmount"].ToString());
                            objOrderMasterModel.CashAmount = Functions.GetDecimal(dt_i.Rows[i]["CashAmount"].ToString());
                            objOrderMasterModel.CreditCardAmount = Functions.GetDecimal(dt_i.Rows[i]["CreditCardAmount"].ToString());
                            objOrderMasterModel.CheckAmount = Functions.GetDecimal(dt_i.Rows[i]["CheckAmount"].ToString());
                            objOrderMasterModel.FoodStampAmount = Functions.GetDecimal(dt_i.Rows[i]["FoodStampAmount"].ToString());
                            objOrderMasterModel.Balance = Functions.GetDecimal(dt_i.Rows[i]["Balance"].ToString());
                            objOrderMasterModel.RefundAmount = Functions.GetDecimal(dt_i.Rows[i]["RefundAmount"].ToString());
                            objOrderMasterModel.ChangeAmount = Functions.GetDecimal(dt_i.Rows[i]["ChangeAmount"].ToString());
                            objOrderMasterModel.CardNumber = dt_i.Rows[i]["CardNumber"].ToString();
                            objOrderMasterModel.Status = dt_i.Rows[i]["Status"].ToString();
                            objOrderMasterModel.CreatedDate = Convert.ToDateTime(dt_i.Rows[i]["CreatedDate"].ToString());
                            objOrderMasterModel.CreatedBy = Functions.GetLong(dt_i.Rows[i]["CreatedBy"].ToString());
                            objOrderMasterModel.CounterIP = dt_i.Rows[i]["CounterIP"].ToString();
                            objOrderMasterModel.OrdNo = dt_i.Rows[i]["OrdNo"].ToString();
                            objOrderMasterModel.IsCancel = Functions.GetBoolean(dt_i.Rows[i]["IsCancel"].ToString());
                            objOrderMasterModel.TaxableAmount = Functions.GetDecimal(dt_i.Rows[i]["TaxableAmount"].ToString());
                            objOrderMasterModel.TaxExempted = Functions.GetDecimal(dt_i.Rows[i]["TaxExempted"].ToString());
                            objOrderMasterModel.IsTaxCarry = Functions.GetBoolean(dt_i.Rows[i]["IsTaxCarry"].ToString());
                            var addOrder = _OrderScannerService.AddOrder(objOrderMasterModel, 1);
                            #endregion

                            #region InsertOrderDetail
                            query = "SELECT * FROM tbl_OrderDetail WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            DataTable dt_j = new DataTable();
                            DataAdapter.Fill(dt_j);

                            if (dt_j.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt_j.Rows.Count; j++)
                                {
                                    OrderDetailmasterModel objOrderDetailmasterModel = new OrderDetailmasterModel();
                                    objOrderDetailmasterModel.OrderID = addOrder.OrderID;
                                    objOrderDetailmasterModel.ProductID = Functions.GetLong(dt_j.Rows[j]["ProductID"].ToString());
                                    objOrderDetailmasterModel.UPCCode = dt_j.Rows[j]["UPCCode"].ToString();
                                    objOrderDetailmasterModel.ProductName = dt_j.Rows[j]["ProductName"].ToString();
                                    objOrderDetailmasterModel.Quantity = Functions.GetDecimal(dt_j.Rows[j]["Quantity"].ToString());
                                    objOrderDetailmasterModel.DepartmentID = Functions.GetLong(dt_j.Rows[j]["DepartmentID"].ToString());
                                    objOrderDetailmasterModel.SectionID = Functions.GetLong(dt_j.Rows[j]["SectionID"].ToString());
                                    objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dt_j.Rows[j]["SellPrice"].ToString());
                                    objOrderDetailmasterModel.Discount = Functions.GetDecimal(dt_j.Rows[j]["Discount"].ToString());
                                    objOrderDetailmasterModel.finalPrice = Functions.GetDecimal(dt_j.Rows[j]["finalPrice"].ToString());
                                    objOrderDetailmasterModel.StoreID = Functions.GetLong(dt_j.Rows[j]["StoreID"].ToString());
                                    objOrderDetailmasterModel.IsScale = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsScale].ToString());
                                    objOrderDetailmasterModel.IsFoodStamp = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsFoodStamp].ToString());
                                    objOrderDetailmasterModel.IsTax = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsTax].ToString());
                                    objOrderDetailmasterModel.FoodStampTotal = Functions.GetDecimal(dt_j.Rows[j]["FoodStampTotal"].ToString());
                                    objOrderDetailmasterModel.DiscountApplyed = Functions.GetBoolean(dt_j.Rows[j]["DiscountApplyed"].ToString());
                                    objOrderDetailmasterModel.TaxAmount = Functions.GetDecimal(dt_j.Rows[j]["TaxAmount"].ToString());
                                    objOrderDetailmasterModel.IsRefund = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsRefund].ToString());
                                    objOrderDetailmasterModel.IsCancel = Functions.GetBoolean(dt_j.Rows[j]["IsCancel"].ToString());
                                    objOrderDetailmasterModel.IsForceTax = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsForceTax].ToString());
                                    objOrderDetailmasterModel.CreatedBy = Functions.GetLong(dt_j.Rows[j]["CreatedBy"].ToString());
                                    objOrderDetailmasterModel.CreatedDate = Convert.ToDateTime(dt_j.Rows[j]["CreatedDate"].ToString());
                                    objOrderDetailmasterModel.CasePriceApplied = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.CasePriceApplied].ToString());
                                    objOrderDetailmasterModel.GroupPrice = Functions.GetDecimal(dt_j.Rows[j]["GroupPrice"].ToString());
                                    objOrderDetailmasterModel.GroupQty = Functions.GetDecimal(dt_j.Rows[j]["GroupQty"].ToString());
                                    objOrderDetailmasterModel.CasePrice = Functions.GetDecimal(dt_j.Rows[j]["CasePrice"].ToString());
                                    objOrderDetailmasterModel.CaseQty = Functions.GetDecimal(dt_j.Rows[j]["CaseQty"].ToString());
                                    objOrderDetailmasterModel.IsTaxCarry = Functions.GetBoolean(dt_i.Rows[i]["IsTaxCarry"].ToString());
                                    objOrderDetailmasterModel = _OrderScannerService.AddOrderDetail(objOrderDetailmasterModel, 1);
                                }
                            }
                            #endregion

                            #region InsertProductLedger
                            query = "SELECT * FROM tbl_ProductLedger WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            DataTable dt_k = new DataTable();
                            DataAdapter.Fill(dt_k);

                            if (dt_k.Rows.Count > 0)
                            {
                                for (int k = 0; k < dt_k.Rows.Count; k++)
                                {
                                    ProductLedgerMasterModel objProductLedgerMasterModel = new ProductLedgerMasterModel();
                                    objProductLedgerMasterModel.ProductID = Functions.GetLong(dt_k.Rows[k]["ProductID"].ToString());
                                    objProductLedgerMasterModel.LedgerTypeID = 2;
                                    objProductLedgerMasterModel.OrderID = addOrder.OrderID;
                                    objProductLedgerMasterModel.OrderLineID = Functions.GetLong(dt_k.Rows[k]["OrderLineID"].ToString());
                                    objProductLedgerMasterModel.PostedPurchaseHeaderID = Functions.GetLong(dt_k.Rows[k]["PostedPurchaseHeaderID"].ToString());
                                    objProductLedgerMasterModel.TaxGroupCodeID = Functions.GetLong(dt_k.Rows[k]["TaxGroupCodeID"].ToString());
                                    objProductLedgerMasterModel.DepartmentID = Functions.GetLong(dt_k.Rows[k]["DepartmentID"].ToString());
                                    objProductLedgerMasterModel.SectionID = Functions.GetLong(dt_k.Rows[k]["SectionID"].ToString());
                                    objProductLedgerMasterModel.UnitOfMeasureID = Functions.GetLong(dt_k.Rows[k]["UnitOfMeasureID"].ToString());
                                    objProductLedgerMasterModel.UPCCode = dt_k.Rows[k]["UPCCode"].ToString();
                                    objProductLedgerMasterModel.QRCode = dt_k.Rows[k]["QRCode"].ToString();
                                    objProductLedgerMasterModel.Qty = Functions.GetDecimal(dt_k.Rows[k]["Qty"].ToString());
                                    objProductLedgerMasterModel.SellPrice = Functions.GetDecimal(dt_k.Rows[k]["SellPrice"].ToString());
                                    objProductLedgerMasterModel.FinalPrice = Functions.GetDecimal(dt_k.Rows[k]["FinalPrice"].ToString());
                                    objProductLedgerMasterModel.DiscountPrice = Functions.GetDecimal(dt_k.Rows[k]["DiscountPrice"].ToString());
                                    objProductLedgerMasterModel.TaxAmount = Functions.GetDecimal(dt_k.Rows[k]["TaxAmount"].ToString());
                                    objProductLedgerMasterModel.CreatedDate = Convert.ToDateTime(dt_k.Rows[k]["CreatedDate"].ToString());
                                    objProductLedgerMasterModel.CreatedBy = Functions.GetLong(dt_k.Rows[k]["CreatedBy"].ToString());
                                    objProductLedgerMasterModel.StoreID = Functions.GetLong(dt_k.Rows[k]["StoreID"].ToString());
                                    objProductLedgerMasterModel.IsForceTax = Functions.GetBoolean(dt_k.Rows[k]["IsForceTax"].ToString());
                                    var add_ProductLedger = _ProductLedgerService.AddProductLedger(objProductLedgerMasterModel, 1);

                                    #region Update Reamining Qty

                                    decimal Qty = Functions.GetDecimal(objProductLedgerMasterModel.Qty.ToString());
                                    decimal _Qty = Qty;
                                    decimal RemainingQty = 0;

                                    for (int z = 0; Qty >= 0; z++)
                                    {
                                        if (Qty >= 0)
                                        {
                                            var ProductLedger = (from PL in _db.tbl_ProductLedger.Where
                                                         (o => o.ProductID == objProductLedgerMasterModel.ProductID
                                                         && o.RemainingQty > 0 && o.LedgerTypeID == 1)
                                                                 select new
                                                                 {
                                                                     ProductLedgerID = PL.ProductLedgerID,
                                                                     RemainingQty = PL.RemainingQty
                                                                 }).FirstOrDefault();
                                            if (ProductLedger != null)
                                            {
                                                RemainingQty = Functions.GetDecimal(ProductLedger.RemainingQty.ToString());
                                                _Qty = Qty - RemainingQty;
                                                if (_Qty > 0)
                                                {
                                                    var updateContacts = from x in _db.tbl_ProductLedger
                                                                         where x.ProductLedgerID == ProductLedger.ProductLedgerID
                                                                         select x;
                                                    foreach (var contact in updateContacts)
                                                        contact.RemainingQty = 0;
                                                }
                                                else
                                                {
                                                    var updateContacts = from x in _db.tbl_ProductLedger
                                                                         where x.ProductLedgerID == ProductLedger.ProductLedgerID
                                                                         select x;
                                                    foreach (var contact in updateContacts)
                                                        contact.RemainingQty = RemainingQty - Qty;
                                                }
                                                _db.SaveChanges();
                                                Qty = _Qty;
                                            }
                                            else
                                            {
                                                Qty = -1;
                                            }
                                        }
                                    }
                                    #endregion
                                }

                            }
                            #endregion

                            #region InsertPaymentTrans
                            query = "SELECT * FROM tbl_PaymentTrans WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            DataAdapter = new SqlCeDataAdapter(query, conn);
                            DataTable dt_P = new DataTable();
                            DataAdapter.Fill(dt_P);

                            if (dt_P.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt_P.Rows.Count; j++)
                                {
                                    PaymentTransMasterModel objPaymentTransMasterModel = new PaymentTransMasterModel();
                                    objPaymentTransMasterModel.OrderID = addOrder.OrderID;
                                    objPaymentTransMasterModel.PaymentMethodID = Functions.GetLong(dt_P.Rows[j][PaymentTransMasterModelCont.PaymentMethodID].ToString());
                                    objPaymentTransMasterModel.StoreID = Functions.GetLong(dt_P.Rows[j][PaymentTransMasterModelCont.StoreID].ToString());
                                    objPaymentTransMasterModel.CashAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.CashAmount].ToString());
                                    objPaymentTransMasterModel.CheckAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.CheckAmount].ToString());
                                    objPaymentTransMasterModel.CreditCardAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.CreditCardAmount].ToString());
                                    objPaymentTransMasterModel.FoodStampAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.FoodStampAmount].ToString());
                                    objPaymentTransMasterModel.Balance = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.Balance].ToString());
                                    objPaymentTransMasterModel.ChangeAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.ChangeAmount].ToString());
                                    objPaymentTransMasterModel.CardNumber = (dt_P.Rows[j][PaymentTransMasterModelCont.CardNumber].ToString());
                                    objPaymentTransMasterModel.CounterIP = (dt_P.Rows[j][PaymentTransMasterModelCont.CounterIP].ToString());
                                    objPaymentTransMasterModel.CreatedBy = Functions.GetLong(dt_P.Rows[j][PaymentTransMasterModelCont.CreatedBy].ToString());
                                    objPaymentTransMasterModel.CreatedDate = Convert.ToDateTime(dt_P.Rows[j][PaymentTransMasterModelCont.CreatedDate].ToString());
                                    objPaymentTransMasterModel = _PaymentTransService.AddPaymentTrans(objPaymentTransMasterModel, 1);
                                }
                            }
                            #endregion

                            #region Delete from Local DB
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();
                            cmd.CommandText = query = "Delete tbl_OrderMaster WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            int modified = cmd.ExecuteNonQuery();

                            cmd = conn.CreateCommand();
                            cmd.CommandText = query = "Delete tbl_OrderDetail WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            modified = cmd.ExecuteNonQuery();

                            cmd = conn.CreateCommand();
                            cmd.CommandText = query = "Delete tbl_ProductLedger WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            modified = cmd.ExecuteNonQuery();

                            cmd = conn.CreateCommand();
                            cmd.CommandText = query = "Delete tbl_PaymentTrans WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            modified = cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                    #endregion

                    #region tbl_ExceptionLog
                    query = "Select * from tbl_ExceptionLog";
                    DataTable dt_Exception = new DataTable();
                    SqlCeDataAdapter da_Exception = new SqlCeDataAdapter(query, conn);
                    da_Exception.Fill(dt_Exception);
                    if (dt_Exception.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_Exception.Rows.Count; i++)
                        {
                            ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
                            objExceptionLogMasterModel.ExceptionName = dt_Exception.Rows[i]["ExceptionName"].ToString();
                            objExceptionLogMasterModel.Discription = dt_Exception.Rows[i]["Discription"].ToString();
                            objExceptionLogMasterModel.PageLine = Functions.GetLong(dt_Exception.Rows[i]["PageLine"].ToString());
                            objExceptionLogMasterModel.PageName = dt_Exception.Rows[i]["PageName"].ToString();
                            objExceptionLogMasterModel.CreatedDate = Convert.ToDateTime(dt_Exception.Rows[i]["CreatedDate"].ToString());
                            objExceptionLogMasterModel.CounterIP = dt_Exception.Rows[i]["CounterIP"].ToString();

                            _ExceptionLogService.AddExceptionLog(objExceptionLogMasterModel.ExceptionName, objExceptionLogMasterModel.Discription, objExceptionLogMasterModel.PageName, objExceptionLogMasterModel.PageLine);
                        }

                        SqlCeCommand cmd = conn.CreateCommand();
                        cmd = conn.CreateCommand();
                        cmd.CommandText = query = "DELETE tbl_ExceptionLog";
                        int modified = cmd.ExecuteNonQuery();
                    }

                    #endregion

                    #region tbl_TransSuspenMaster
                    query = "Select * from tbl_TransSuspenMaster";
                    DataTable dt = new DataTable();
                    SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TransSuspendMasterModel objTransSuspendMasterModel = new TransSuspendMasterModel();
                            objTransSuspendMasterModel.TransSuspendCode = dt.Rows[i]["TransSuspendCode"].ToString();
                            objTransSuspendMasterModel.ProductName = dt.Rows[i]["ProductName"].ToString();
                            objTransSuspendMasterModel.ProductID = Functions.GetLong(dt.Rows[i]["ProductID"].ToString());
                            objTransSuspendMasterModel.UPCCode = dt.Rows[i]["UPCCode"].ToString();
                            objTransSuspendMasterModel.DepartmentID = Functions.GetLong(dt.Rows[i]["DepartmentID"].ToString());
                            objTransSuspendMasterModel.SectionID = Functions.GetLong(dt.Rows[i]["SectionID"].ToString());
                            objTransSuspendMasterModel.Quantity = Functions.GetDecimal(dt.Rows[i]["Quantity"].ToString());
                            objTransSuspendMasterModel.SellPrice = Functions.GetDecimal(dt.Rows[i]["SellPrice"].ToString());
                            objTransSuspendMasterModel.FinalPrice = Functions.GetDecimal(dt.Rows[i]["FinalPrice"].ToString());
                            objTransSuspendMasterModel.TotalAmount = Functions.GetDecimal(dt.Rows[i]["TotalAmount"].ToString());
                            objTransSuspendMasterModel.GrossAmount = Functions.GetDecimal(dt.Rows[i]["GrossAmount"].ToString());
                            objTransSuspendMasterModel.Tax = Functions.GetDecimal(dt.Rows[i]["Tax"].ToString());
                            objTransSuspendMasterModel.TotalTaxAmount = Functions.GetDecimal(dt.Rows[i]["TotalTaxAmount"].ToString());
                            objTransSuspendMasterModel.DiscountApplyed = Functions.GetBoolean(dt.Rows[i]["DiscountApplyed"].ToString());
                            objTransSuspendMasterModel.IsScale = Functions.GetBoolean(dt.Rows[i]["IsScale"].ToString());
                            objTransSuspendMasterModel.IsFoodStamp = Functions.GetBoolean(dt.Rows[i]["IsFoodStamp"].ToString());
                            objTransSuspendMasterModel.IsTax = Functions.GetBoolean(dt.Rows[i]["IsTax"].ToString());
                            objTransSuspendMasterModel.Status = Functions.GetBoolean(dt.Rows[i]["Status"].ToString());
                            objTransSuspendMasterModel.StoreID = Functions.GetLong(dt.Rows[i]["StoreID"].ToString());
                            objTransSuspendMasterModel.CreatedBy = Functions.GetLong(dt.Rows[i]["CreatedBy"].ToString());
                            objTransSuspendMasterModel.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString());
                            objTransSuspendMasterModel.IsDelete = Functions.GetBoolean(dt.Rows[i]["IsDelete"].ToString());

                            objTransSuspendMasterModel.GroupQty = Functions.GetDecimal(dt.Rows[i]["GroupQty"].ToString());
                            objTransSuspendMasterModel.GroupPrice = Functions.GetDecimal(dt.Rows[i]["GroupPrice"].ToString());
                            objTransSuspendMasterModel.CaseQty = Functions.GetDecimal(dt.Rows[i]["CaseQty"].ToString());
                            objTransSuspendMasterModel.CasePrice = Functions.GetDecimal(dt.Rows[i]["CasePrice"].ToString());
                            objTransSuspendMasterModel.CasePriceApplied = Functions.GetBoolean(dt.Rows[i]["CasePriceApplied"].ToString());

                            objTransSuspendMasterModel = _TransSuspendService.AddTransSuspend(objTransSuspendMasterModel, 1);
                        }

                        SqlCeCommand cmd = conn.CreateCommand();
                        cmd = conn.CreateCommand();
                        cmd.CommandText = query = "DELETE tbl_TransSuspenMaster";
                        int modified = cmd.ExecuteNonQuery();
                    }
                    #endregion

                    ChangeSyncStatus("tbl_OrderMaster");
                    ChangeSyncStatus("tbl_OrderDetail");
                    ChangeSyncStatus("tbl_ProductLedger");
                    ChangeSyncStatus("tbl_PaymentTrans");
                    ChangeSyncStatus("tbl_LoginMaster");
                    ChangeSyncStatus("tbl_ExceptionLog");
                    ChangeSyncStatus("tbl_TransSuspenMaster");

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //ClsCommon.MsgBox("Information","LocalToLiveSync >> " + ex.Message);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void ChangeSyncStatus(string TblName)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var UpdatedTableName = _db.tbl_UpdateLog.Where(x => x.TblName == TblName && x.IsSync == false &&  x.MacAddress == LoginInfo.MacAddress).ToList();
                foreach (tbl_UpdateLog tbl in UpdatedTableName)
                {
                    tbl.IsSync = true;
                    tbl.IsChange = false;
                    tbl.SyncDate = DateTime.Now;
                    tbl.UpdatedDate = DateTime.Now;
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                //ClsCommon.MsgBox("Information","Error - ChangeSyncStatus");
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool CheckConnection()
        {
            try
            {
                bool Status;
                var task = Task.Run(() =>
                {
                    Status = db_Connection();
                });
                bool isCompletedSuccessfully = task.Wait(TimeSpan.FromMilliseconds(3000));
                if (isCompletedSuccessfully)
                {
                    Status = db_Connection();
                }
                else
                {
                    Status = false;
                }
                LoginInfo.Connections = Status;
                return Status;
            }
            catch (Exception)
            {
                return false;
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public bool db_Connection()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var Empl = _db.tbl_EmployeeMaster.FirstOrDefault();
                LoginInfo.Connections = true;
                return true;
            }
            catch (SqlException)
            {
                LoginInfo.Connections = false;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void tbl_DepartmentMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_DepartmentMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_DepartmentMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_DepartmentMaster WHERE DepartmentID=" + Data.DepartmentID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_DepartmentMaster SET DepartmentID=@DepartmentID,DepartmentName=@DepartmentName," +
                                "IsFoodStamp = @IsFoodStamp,TaxGroupID = @TaxGroupID,UnitMeasureID = @UnitMeasureID,AgeVarificationAge = @AgeVarificationAge,DepartmentNo=@DepartmentNo,SubNo=@SubNo," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE DepartmentID=@DepartmentID;";

                            #region Parameters
                            //if (Data.DepartmentID != null)
                            //{
                            cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            //}
                            if (Data.DepartmentName != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentName", Data.DepartmentName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentName", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.DepartmentNo != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentNo", Data.DepartmentNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentNo", DBNull.Value);
                            }
                            if (Data.SubNo != null)
                            {
                                cmd.Parameters.AddWithValue("@SubNo", Data.SubNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SubNo", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_DepartmentMaster(DepartmentID,DepartmentName,IsFoodStamp,TaxGroupID,UnitMeasureID,AgeVarificationAge,DepartmentNo,SubNo,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@DepartmentID,@DepartmentName,@IsFoodStamp,@TaxGroupID,@UnitMeasureID,@AgeVarificationAge,@DepartmentNo,@SubNo,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            #region Parameters
                            //if (Data.DepartmentID != null)
                            //{
                            cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            //}
                            if (Data.DepartmentName != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentName", Data.DepartmentName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentName", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.DepartmentNo != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentNo", Data.DepartmentNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentNo", DBNull.Value);
                            }
                            if (Data.SubNo != null)
                            {
                                cmd.Parameters.AddWithValue("@SubNo", Data.SubNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SubNo", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_DepartmentMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_DepartmentMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_EmployeeMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_EmployeeMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_EmployeeMaster Data in tbl_Data)
                    {
                        string query = "Select EmployeeID from tbl_EmployeeMaster WHERE EmployeeID=" + Data.EmployeeID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_EmployeeMaster SET RoleID=@RoleID,StoreID=@StoreID,FirstName=@FirstName,LastName=@LastName,EmailID=@EmailID,PhoneNo=@PhoneNo,Password=@Password,MaxVoidAmount=@MaxVoidAmount,BirthDate=@BirthDate" +
                                ",IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE EmployeeID=@EmployeeID;";

                            #region Parameters
                            //if (Data.EmployeeID != null)
                            //{
                            cmd.Parameters.AddWithValue("@EmployeeID", Data.EmployeeID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
                            //}
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.StoreID != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            }
                            if (Data.FirstName != null)
                            {
                                cmd.Parameters.AddWithValue("@FirstName", Data.FirstName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                            }
                            if (Data.LastName != null)
                            {
                                cmd.Parameters.AddWithValue("@LastName", Data.LastName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                            }
                            if (Data.EmailID != null)
                            {
                                cmd.Parameters.AddWithValue("@EmailID", Data.EmailID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EmailID", DBNull.Value);
                            }
                            if (Data.PhoneNo != null)
                            {
                                cmd.Parameters.AddWithValue("@PhoneNo", Data.PhoneNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PhoneNo", DBNull.Value);
                            }
                            if (Data.Password != null)
                            {
                                cmd.Parameters.AddWithValue("@Password", Data.Password);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Password", DBNull.Value);
                            }
                            if (Data.MaxVoidAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@MaxVoidAmount", Data.MaxVoidAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MaxVoidAmount", DBNull.Value);
                            }
                            if (Data.BirthDate != null)
                            {
                                cmd.Parameters.AddWithValue("@BirthDate", Data.BirthDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@BirthDate", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_EmployeeMaster(EmployeeID,RoleID,StoreID,FirstName,LastName,EmailID,PhoneNo,Password,MaxVoidAmount,BirthDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@EmployeeID,@RoleID,@StoreID,@FirstName,@LastName,@EmailID,@PhoneNo,@Password,@MaxVoidAmount,@BirthDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            #region Parameters
                            //if (Data.EmployeeID != null)
                            //{
                            cmd.Parameters.AddWithValue("@EmployeeID", Data.EmployeeID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
                            //}
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.StoreID != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            }
                            if (Data.FirstName != null)
                            {
                                cmd.Parameters.AddWithValue("@FirstName", Data.FirstName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                            }
                            if (Data.LastName != null)
                            {
                                cmd.Parameters.AddWithValue("@LastName", Data.LastName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                            }
                            if (Data.EmailID != null)
                            {
                                cmd.Parameters.AddWithValue("@EmailID", Data.EmailID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EmailID", DBNull.Value);
                            }
                            if (Data.PhoneNo != null)
                            {
                                cmd.Parameters.AddWithValue("@PhoneNo", Data.PhoneNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PhoneNo", DBNull.Value);
                            }
                            if (Data.Password != null)
                            {
                                cmd.Parameters.AddWithValue("@Password", Data.Password);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Password", DBNull.Value);
                            }
                            if (Data.MaxVoidAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@MaxVoidAmount", Data.MaxVoidAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MaxVoidAmount", DBNull.Value);
                            }
                            if (Data.BirthDate != null)
                            {
                                cmd.Parameters.AddWithValue("@BirthDate", Data.BirthDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@BirthDate", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_EmployeeMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_EmployeeMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_ProductMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();

                //tbl_Data = _db.tbl_ProductMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();

                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_ProductMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_ProductMaster WHERE ProductID=" + Data.ProductID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ProductMaster SET ProductName=@ProductName,UPCCode=@UPCCode,CertCode=@CertCode,DepartmentID=@DepartmentID,SectionID=@SectionID,UnitMeasureID=@UnitMeasureID,Price=@Price,TaxGroupID=@TaxGroupID," +
                                "IsFoodStamp=@IsFoodStamp,AgeVerification=@AgeVerification,IsScaled=@IsScaled,TareWeight=@TareWeight,GroupQty=@GroupQty,GroupPrice=@GroupPrice,LinkedUPCCode=@LinkedUPCCode,LabeledPrice=@LabeledPrice,CaseQty=@CaseQty,CasePrice=@CasePrice,IsGroupPrice=@IsGroupPrice," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE ProductID=@ProductID;";
                            #region Parameters
                            //if (Data.ProductID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            //}
                            if (Data.ProductName != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductName", Data.ProductName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductName", DBNull.Value);
                            }
                            if (Data.UPCCode != null)
                            {
                                cmd.Parameters.AddWithValue("@UPCCode", Data.UPCCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UPCCode", DBNull.Value);
                            }
                            if (Data.CertCode != null)
                            {
                                cmd.Parameters.AddWithValue("@CertCode", Data.CertCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CertCode", DBNull.Value);
                            }
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.SectionID != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.Price != null)
                            {
                                cmd.Parameters.AddWithValue("@Price", Data.Price);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Price", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.Image != null)
                            {
                                cmd.Parameters.AddWithValue("@Image", Data.Image);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.AgeVerification != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVerification", Data.AgeVerification);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVerification", DBNull.Value);
                            }
                            if (Data.IsScaled != null)
                            {
                                cmd.Parameters.AddWithValue("@IsScaled", Data.IsScaled);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsScaled", DBNull.Value);
                            }
                            if (Data.TareWeight != null)
                            {
                                cmd.Parameters.AddWithValue("@TareWeight", Data.TareWeight);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TareWeight", DBNull.Value);
                            }
                            if (Data.GroupQty != null)
                            {
                                cmd.Parameters.AddWithValue("@GroupQty", Data.GroupQty);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GroupQty", DBNull.Value);
                            }
                            if (Data.GroupPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@GroupPrice", Data.GroupPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GroupPrice", DBNull.Value);
                            }
                            if (Data.LinkedUPCCode != null)
                            {
                                cmd.Parameters.AddWithValue("@LinkedUPCCode", Data.LinkedUPCCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LinkedUPCCode", DBNull.Value);
                            }
                            if (Data.LabeledPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@LabeledPrice", Data.LabeledPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LabeledPrice", DBNull.Value);
                            }
                            if (Data.CaseQty != null)
                            {
                                cmd.Parameters.AddWithValue("@CaseQty", Data.CaseQty);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CaseQty", DBNull.Value);
                            }
                            if (Data.CasePrice != null)
                            {
                                cmd.Parameters.AddWithValue("@CasePrice", Data.CasePrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CasePrice", DBNull.Value);
                            }
                            if (Data.IsGroupPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@IsGroupPrice", Data.IsGroupPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsGroupPrice", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_ProductMaster(ProductID,ProductName,UPCCode,CertCode,DepartmentID,SectionID,UnitMeasureID,Price,TaxGroupID,IsFoodStamp,AgeVerification,IsScaled,TareWeight,GroupQty,GroupPrice,LinkedUPCCode,LabeledPrice,CaseQty,CasePrice,IsGroupPrice,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@ProductID,@ProductName,@UPCCode,@CertCode,@DepartmentID,@SectionID,@UnitMeasureID,@Price,@TaxGroupID,@IsFoodStamp,@AgeVerification,@IsScaled,@TareWeight,@GroupQty,@GroupPrice,@LinkedUPCCode,@LabeledPrice,@CaseQty,@CasePrice,@IsGroupPrice,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            #region Parameters
                            //if (Data.ProductID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            //}
                            if (Data.ProductName != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductName", Data.ProductName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductName", DBNull.Value);
                            }
                            if (Data.UPCCode != null)
                            {
                                cmd.Parameters.AddWithValue("@UPCCode", Data.UPCCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UPCCode", DBNull.Value);
                            }
                            if (Data.CertCode != null)
                            {
                                cmd.Parameters.AddWithValue("@CertCode", Data.CertCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CertCode", DBNull.Value);
                            }
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.SectionID != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.Price != null)
                            {
                                cmd.Parameters.AddWithValue("@Price", Data.Price);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Price", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.Image != null)
                            {
                                cmd.Parameters.AddWithValue("@Image", Data.Image);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.AgeVerification != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVerification", Data.AgeVerification);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVerification", DBNull.Value);
                            }
                            if (Data.IsScaled != null)
                            {
                                cmd.Parameters.AddWithValue("@IsScaled", Data.IsScaled);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsScaled", DBNull.Value);
                            }
                            if (Data.TareWeight != null)
                            {
                                cmd.Parameters.AddWithValue("@TareWeight", Data.TareWeight);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TareWeight", DBNull.Value);
                            }
                            if (Data.GroupQty != null)
                            {
                                cmd.Parameters.AddWithValue("@GroupQty", Data.GroupQty);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GroupQty", DBNull.Value);
                            }
                            if (Data.GroupPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@GroupPrice", Data.GroupPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GroupPrice", DBNull.Value);
                            }
                            if (Data.LinkedUPCCode != null)
                            {
                                cmd.Parameters.AddWithValue("@LinkedUPCCode", Data.LinkedUPCCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LinkedUPCCode", DBNull.Value);
                            }
                            if (Data.LabeledPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@LabeledPrice", Data.LabeledPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LabeledPrice", DBNull.Value);
                            }
                            if (Data.CaseQty != null)
                            {
                                cmd.Parameters.AddWithValue("@CaseQty", Data.CaseQty);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CaseQty", DBNull.Value);
                            }
                            if (Data.CasePrice != null)
                            {
                                cmd.Parameters.AddWithValue("@CasePrice", Data.CasePrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CasePrice", DBNull.Value);
                            }
                            if (Data.IsGroupPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@IsGroupPrice", Data.IsGroupPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsGroupPrice", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_ProductMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_ProductMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_ProductSalePriceMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductSalePriceMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_ProductSalePriceMaster Data in tbl_Data)
                    {
                        string query = "Select ProductSalePriceID from tbl_ProductSalePriceMaster WHERE ProductSalePriceID=" + Data.ProductSalePriceID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ProductSalePriceMaster SET ProductID=@ProductID,SellPrice=@SellPrice,StartDate=@StartDate,EndDate=@EndDate," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE ProductSalePriceID=@ProductSalePriceID;";
                            #region Parameters
                            //if (Data.ProductSalePriceID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductSalePriceID", Data.ProductSalePriceID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductSalePriceID", DBNull.Value);
                            //}
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.SellPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@SellPrice", Data.SellPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SellPrice", DBNull.Value);
                            }
                            if (Data.StartDate != null)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", Data.StartDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                            }
                            if (Data.EndDate != null)
                            {
                                cmd.Parameters.AddWithValue("@EndDate", Data.EndDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                            }

                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_ProductSalePriceMaster(ProductSalePriceID,ProductID,SellPrice,StartDate,EndDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@ProductSalePriceID,@ProductID,@SellPrice,@StartDate,@EndDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            //if (Data.ProductSalePriceID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductSalePriceID", Data.ProductSalePriceID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductSalePriceID", DBNull.Value);
                            //}
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.SellPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@SellPrice", Data.SellPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SellPrice", DBNull.Value);
                            }
                            if (Data.StartDate != null)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", Data.StartDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                            }
                            if (Data.EndDate != null)
                            {
                                cmd.Parameters.AddWithValue("@EndDate", Data.EndDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                            }

                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_ProductSalePriceMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_ProductSalePriceMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_ProductUoM(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductUoM.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_ProductUoM Data in tbl_Data)
                    {
                        string query = "Select ProductUoMID from tbl_ProductUoM WHERE ProductUoMID=" + Data.ProductUoMID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ProductUoM SET ProductID=@ProductID,UnitMeasureID=@UnitMeasureID,Discription=@Discription,QtyPerUoM=@QtyPerUoM," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE ProductUoMID=@ProductUoMID;";

                            #region Parameters
                            //if (Data.ProductUoMID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductUoMID", Data.ProductUoMID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductUoMID", DBNull.Value);
                            //}
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.Discription != null)
                            {
                                cmd.Parameters.AddWithValue("@Discription", Data.Discription);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Discription", DBNull.Value);
                            }
                            if (Data.QtyPerUoM != null)
                            {
                                cmd.Parameters.AddWithValue("@QtyPerUoM", Data.QtyPerUoM);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@QtyPerUoM", DBNull.Value);
                            }

                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_ProductUoM(ProductUoMID,ProductID,UnitMeasureID,Discription,QtyPerUoM,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@ProductUoMID,@ProductID,@UnitMeasureID,@Discription,@QtyPerUoM,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            #region Parameters
                            //if (Data.ProductUoMID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductUoMID", Data.ProductUoMID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductUoMID", DBNull.Value);
                            //}
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.Discription != null)
                            {
                                cmd.Parameters.AddWithValue("@Discription", Data.Discription);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Discription", DBNull.Value);
                            }
                            if (Data.QtyPerUoM != null)
                            {
                                cmd.Parameters.AddWithValue("@QtyPerUoM", Data.QtyPerUoM);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@QtyPerUoM", DBNull.Value);
                            }

                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_ProductUoM");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_ProductUoM" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_RoleMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_RoleMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_RoleMaster Data in tbl_Data)
                    {
                        string query = "Select RoleID from tbl_RoleMaster WHERE RoleID=" + Data.RoleID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_RoleMaster SET RoleID=@RoleID,RoleType=@RoleType," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE RoleID=@RoleID;";
                            //if (Data.RoleID != null)
                            //{
                            cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            //}
                            if (Data.RoleType != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleType", Data.RoleType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleType", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_RoleMaster(RoleID,RoleType,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@RoleID,@RoleType,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            //if (Data.RoleID != null)
                            //{
                            cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            //}
                            if (Data.RoleType != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleType", Data.RoleType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleType", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_RoleMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_RoleMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_RolePermission(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_RolePermission.ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_RolePermission Data in tbl_Data)
                    {
                        string query = "Select PermissionID from tbl_RolePermission WHERE PermissionID=" + Data.PermissionID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_RolePermission SET PermissionID=@PermissionID,RoleID=@RoleID," +
                                "MenuID=@MenuID,ViewRecords=@ViewRecords,InsertRecords=@InsertRecords,EditRecords=@EditRecords,DeleteRecords=@DeleteRecords " +
                                "WHERE PermissionID=@PermissionID;";
                            #region Parameters
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@PermissionID", Data.PermissionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PermissionID", DBNull.Value);
                            }
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.MenuID != null)
                            {
                                cmd.Parameters.AddWithValue("@MenuID", Data.MenuID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MenuID", DBNull.Value);
                            }
                            if (Data.ViewRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@ViewRecords", Data.ViewRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ViewRecords", DBNull.Value);
                            }
                            if (Data.InsertRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@InsertRecords", Data.InsertRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@InsertRecords", DBNull.Value);
                            }
                            if (Data.EditRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@EditRecords", Data.EditRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EditRecords", DBNull.Value);
                            }
                            if (Data.DeleteRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@DeleteRecords", Data.DeleteRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DeleteRecords", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_RolePermission(PermissionID,RoleID,MenuID,ViewRecords,InsertRecords,EditRecords,DeleteRecords) " +
                                "VALUES(@PermissionID,@RoleID,@MenuID,@ViewRecords,@InsertRecords,@EditRecords,@DeleteRecords)";
                            #region Parameters
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@PermissionID", Data.PermissionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PermissionID", DBNull.Value);
                            }
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.MenuID != null)
                            {
                                cmd.Parameters.AddWithValue("@MenuID", Data.MenuID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MenuID", DBNull.Value);
                            }
                            if (Data.ViewRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@ViewRecords", Data.ViewRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ViewRecords", DBNull.Value);
                            }
                            if (Data.InsertRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@InsertRecords", Data.InsertRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@InsertRecords", DBNull.Value);
                            }
                            if (Data.EditRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@EditRecords", Data.EditRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EditRecords", DBNull.Value);
                            }
                            if (Data.DeleteRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@DeleteRecords", Data.DeleteRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DeleteRecords", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_RolePermission");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_RolePermission" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_SectionMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_SectionMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_SectionMaster Data in tbl_Data)
                    {
                        string query = "Select SectionID from tbl_SectionMaster WHERE SectionID=" + Data.SectionID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_SectionMaster SET SectionID=@SectionID,DepartmentID=@DepartmentID,SectionName=@SectionName," +
                                "IsFoodStamp = @IsFoodStamp,TaxGroupID = @TaxGroupID,UnitMeasureID = @UnitMeasureID,AgeVarificationAge = @AgeVarificationAge," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE SectionID=@SectionID;";
                            #region Parameters
                            //if (Data.SectionID != null)
                            //{
                            cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            //}
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.SectionName != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionName", Data.SectionName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionName", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_SectionMaster(SectionID,DepartmentID,SectionName,IsFoodStamp,TaxGroupID,UnitMeasureID,AgeVarificationAge,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@SectionID,@DepartmentID,@SectionName,@IsFoodStamp,@TaxGroupID,@UnitMeasureID,@AgeVarificationAge,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            //if (Data.SectionID != null)
                            //{
                            cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            //}
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.SectionName != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionName", Data.SectionName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionName", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_SectionMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_SectionMaster" + ex.StackTrace, ex.LineNumber());
            }

        }

        public void tbl_ShortcutkeyMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ShortcutkeyMaster.ToList();//Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_ShortcutkeyMaster Data in tbl_Data)
                    {
                        string query = "Select ShortcutKeyID from tbl_ShortcutkeyMaster WHERE ShortcutKeyID=" + Data.ShortcutKeyID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ShortcutkeyMaster SET ShortcutKeyID=@ShortcutKeyID,ShortcutKey=@ShortcutKey,Description=@Description " +
                                "WHERE ShortcutKeyID=@ShortcutKeyID;";
                            #region Parameters
                            //if (Data.ShortcutKeyID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ShortcutKeyID", Data.ShortcutKeyID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ShortcutKeyID", DBNull.Value);
                            //}
                            if (Data.ShortcutKey != null)
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKey", Data.ShortcutKey);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKey", DBNull.Value);
                            }
                            if (Data.Description != null)
                            {
                                cmd.Parameters.AddWithValue("@Description", Data.Description);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_ShortcutkeyMaster(ShortcutKeyID,ShortcutKey,Description) " +
                                "VALUES(@ShortcutKeyID,@ShortcutKey,@Description)";
                            #region Parameters
                            //if (Data.ShortcutKeyID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ShortcutKeyID", Data.ShortcutKeyID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ShortcutKeyID", DBNull.Value);
                            //}
                            if (Data.ShortcutKey != null)
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKey", Data.ShortcutKey);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKey", DBNull.Value);
                            }
                            if (Data.Description != null)
                            {
                                cmd.Parameters.AddWithValue("@Description", Data.Description);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_ShortcutkeyMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_ShortcutkeyMaster" + ex.StackTrace, ex.LineNumber());
            }

        }

        public void tbl_StoreMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_StoreMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_StoreMaster Data in tbl_Data)
                    {
                        string query = "Select StoreID from tbl_StoreMaster WHERE StoreID=" + Data.StoreID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_StoreMaster SET StoreName=@StoreName,Address=@Address,Address2=@Address2,Phone=@Phone,Fax=@Fax,City=@City,State=@State,Country=@Country,ZipCode=@ZipCode,AgeVarificationAge=@AgeVarificationAge,Disclaimer=@Disclaimer,DefaultTax=@DefaultTax," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy,IsStoreTax=@IsStoreTax " +
                                "WHERE StoreID=@StoreID;";
                            #region Parameters
                            //if (Data.StoreID != null)
                            //{
                            cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            //}
                            if (Data.StoreName != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreName", Data.StoreName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreName", DBNull.Value);
                            }
                            if (Data.Address != null)
                            {
                                cmd.Parameters.AddWithValue("@Address", Data.Address);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Address", DBNull.Value);
                            }
                            if (Data.Address2 != null)
                            {
                                cmd.Parameters.AddWithValue("@Address2", Data.Address2);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Address2", DBNull.Value);
                            }
                            if (Data.Phone != null)
                            {
                                cmd.Parameters.AddWithValue("@Phone", Data.Phone);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                            }
                            if (Data.Fax != null)
                            {
                                cmd.Parameters.AddWithValue("@Fax", Data.Fax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Fax", DBNull.Value);
                            }
                            if (Data.City != null)
                            {
                                cmd.Parameters.AddWithValue("@City", Data.City);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@City", DBNull.Value);
                            }
                            if (Data.State != null)
                            {
                                cmd.Parameters.AddWithValue("@State", Data.State);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@State", DBNull.Value);
                            }
                            if (Data.Country != null)
                            {
                                cmd.Parameters.AddWithValue("@Country", Data.Country);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Country", DBNull.Value);
                            }
                            if (Data.ZipCode != null)
                            {
                                cmd.Parameters.AddWithValue("@ZipCode", Data.ZipCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ZipCode", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.Disclaimer != null)
                            {
                                cmd.Parameters.AddWithValue("@Disclaimer", Data.Disclaimer);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Disclaimer", DBNull.Value);
                            }
                            if (Data.DefaultTax != null)
                            {
                                cmd.Parameters.AddWithValue("@DefaultTax", Data.DefaultTax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DefaultTax", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            if (Data.IsStoreTax != null)
                            {
                                cmd.Parameters.AddWithValue("@IsStoreTax", Data.IsStoreTax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsStoreTax", DBNull.Value);
                            }

                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_StoreMaster(StoreID,StoreName,Address,Address2,Phone,Fax,City,State,Country,ZipCode,AgeVarificationAge,Disclaimer,DefaultTax,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsStoreTax) " +
                                 "VALUES(@StoreID,@StoreName,@Address,@Address2,@Phone,@Fax,@City,@State,@Country,@ZipCode,@AgeVarificationAge,@Disclaimer,@DefaultTax,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy,@IsStoreTax)";
                            #region Parameters
                            //if (Data.StoreID != null)
                            //{
                            cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            //}
                            if (Data.StoreName != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreName", Data.StoreName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreName", DBNull.Value);
                            }
                            if (Data.Address != null)
                            {
                                cmd.Parameters.AddWithValue("@Address", Data.Address);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Address", DBNull.Value);
                            }
                            if (Data.Address2 != null)
                            {
                                cmd.Parameters.AddWithValue("@Address2", Data.Address2);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Address2", DBNull.Value);
                            }
                            if (Data.Phone != null)
                            {
                                cmd.Parameters.AddWithValue("@Phone", Data.Phone);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                            }
                            if (Data.Fax != null)
                            {
                                cmd.Parameters.AddWithValue("@Fax", Data.Fax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Fax", DBNull.Value);
                            }
                            if (Data.City != null)
                            {
                                cmd.Parameters.AddWithValue("@City", Data.City);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@City", DBNull.Value);
                            }
                            if (Data.State != null)
                            {
                                cmd.Parameters.AddWithValue("@State", Data.State);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@State", DBNull.Value);
                            }
                            if (Data.Country != null)
                            {
                                cmd.Parameters.AddWithValue("@Country", Data.Country);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Country", DBNull.Value);
                            }
                            if (Data.ZipCode != null)
                            {
                                cmd.Parameters.AddWithValue("@ZipCode", Data.ZipCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ZipCode", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.Disclaimer != null)
                            {
                                cmd.Parameters.AddWithValue("@Disclaimer", Data.Disclaimer);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Disclaimer", DBNull.Value);
                            }
                            if (Data.DefaultTax != null)
                            {
                                cmd.Parameters.AddWithValue("@DefaultTax", Data.DefaultTax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DefaultTax", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            if (Data.IsStoreTax != null)
                            {
                                cmd.Parameters.AddWithValue("@IsStoreTax", Data.IsStoreTax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsStoreTax", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_StoreMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_StoreMaster" + ex.StackTrace, ex.LineNumber());
            }

        }

        public void tbl_TaxGroupMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_TaxGroupMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_TaxGroupMaster Data in tbl_Data)
                    {
                        string query = "Select TaxGroupID from tbl_TaxGroupMaster WHERE TaxGroupID=" + Data.TaxGroupID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_TaxGroupMaster SET TaxGroupID=@TaxGroupID,TaxGroupName=@TaxGroupName," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE TaxGroupID=@TaxGroupID;";
                            #region Parameters
                            //if (Data.TaxGroupID != null)
                            //{
                            cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            //}
                            if (Data.TaxGroupName != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", Data.TaxGroupName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_TaxGroupMaster(TaxGroupID,TaxGroupName,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@TaxGroupID,@TaxGroupName,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            //if (Data.TaxGroupID != null)
                            //{
                            cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            //}
                            if (Data.TaxGroupName != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", Data.TaxGroupName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_TaxGroupMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_TaxGroupMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_TaxRateMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_TaxRateMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_TaxRateMaster Data in tbl_Data)
                    {
                        string query = "Select TaxRateID from tbl_TaxRateMaster WHERE TaxRateID=" + Data.TaxRateID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_TaxRateMaster SET TaxGroupID=@TaxGroupID,Tax=@Tax,StartDate=@StartDate,EndDate=@EndDate," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE TaxRateID=@TaxRateID;";
                            #region Parameters
                            //if (Data.TaxRateID != null)
                            //{
                            cmd.Parameters.AddWithValue("@TaxRateID", Data.TaxRateID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TaxRateID", DBNull.Value);
                            //}
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            //if (Data.Tax != null)
                            //{
                            cmd.Parameters.AddWithValue("@Tax", Data.Tax);
                            ////}
                            ////else
                            ////{
                            ////    cmd.Parameters.AddWithValue("@Tax", DBNull.Value);
                            ////}
                            if (Data.StartDate != null)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", Data.StartDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                            }
                            if (Data.EndDate != null)
                            {
                                cmd.Parameters.AddWithValue("@EndDate", Data.EndDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                            }

                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_TaxRateMaster(TaxRateID,TaxGroupID,Tax,StartDate,EndDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@TaxRateID,@TaxGroupID,@Tax,@StartDate,@EndDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            //if (Data.TaxRateID != null)
                            //{
                            cmd.Parameters.AddWithValue("@TaxRateID", Data.TaxRateID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TaxRateID", DBNull.Value);
                            //}
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            //if (Data.Tax != null)
                            //{
                            cmd.Parameters.AddWithValue("@Tax", Data.Tax);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@Tax", DBNull.Value);
                            //}
                            if (Data.StartDate != null)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", Data.StartDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                            }
                            if (Data.EndDate != null)
                            {
                                cmd.Parameters.AddWithValue("@EndDate", Data.EndDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                            }

                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_TaxRateMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_TaxRateMaster" + ex.StackTrace, ex.LineNumber());
            }

        }

        public void tbl_UnitMeasureMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_UnitMeasureMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_UnitMeasureMaster Data in tbl_Data)
                    {
                        string query = "Select UnitMeasureID from tbl_UnitMeasureMaster WHERE UnitMeasureID=" + Data.UnitMeasureID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_UnitMeasureMaster SET UnitMeasureID=@UnitMeasureID,UnitMeasureCode=@UnitMeasureCode,Description=@Description," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE UnitMeasureID=@UnitMeasureID;";
                            #region Parameters
                            //if (Data.UnitMeasureID != null)
                            //{
                            cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            //}
                            if (Data.UnitMeasureCode != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureCode", Data.UnitMeasureCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureCode", DBNull.Value);
                            }
                            if (Data.Description != null)
                            {
                                cmd.Parameters.AddWithValue("@Description", Data.Description);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_UnitMeasureMaster(UnitMeasureID,UnitMeasureCode,Description,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@UnitMeasureID,@UnitMeasureCode,@Description,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            //if (Data.UnitMeasureID != null)
                            //{
                            cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            //}
                            if (Data.UnitMeasureCode != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureCode", Data.UnitMeasureCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureCode", DBNull.Value);
                            }
                            if (Data.Description != null)
                            {
                                cmd.Parameters.AddWithValue("@Description", Data.Description);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_UnitMeasureMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmOrderScanner-tbl_UnitMeasureMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SyncTimer_LocalToLive_Tick(object sender, EventArgs e)
        {
            try
            {
                //GetScalePowerOn();
                if (CurrentIndex == 0)
                {
                    LoginInfo.SyncType = 2;
                    backgroundWorker_LocalToLive.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "*** SyncTimer_LocalToLive_Tick *** " + CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void backgroundWorker_LocalToLive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                CurrentIndex++;
                //LiveToLocalSync();
                //LocalToLiveSync();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "*** _LocalToLive_ *** " + CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void backgroundWorker_LocalToLive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                CurrentIndex = 0;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "***** _LocalToLive_ ***** " + CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion
    }
}