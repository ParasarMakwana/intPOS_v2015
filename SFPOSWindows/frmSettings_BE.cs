using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.PointOfService;
using System.Text;

namespace SFPOSWindows
{
    public partial class frmSettings_BE : Form
    {
        //SerialPort ComPort = new SerialPort();
        //internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        //internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        //delegate void SetTextCallback(string text);
        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        private Scale myScale;
        static bool dbValid = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        bool FirstCall = false;
        public frmSettings_BE()
        {
            InitializeComponent();
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    Functions.SetIcon(this);
            //}
            //ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            myPosExplorer = new PosExplorer(this);
            ClsCommon.Read_XMLFile();
            
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            DataLoad();

            if (XMLData.IsDemoVersion == 1)
            {
                foreach (TabPage tab in TabControl.TabPages)
                {
                    if (tab.Name == "tabDatabaseConnection")
                    {
                        TabControl.TabPages.Remove(tab);
                    }
                }
            }

            string TabCtrl = LoginInfo.SettingScreen;

            switch (TabCtrl)
            {
                case "tabezPOSProInfo":
                    TabControl.SelectedTab = tabezPOSProInfo;
                    break;
                case "tabLicenceKey":
                    TabControl.SelectedTab = tabLicenceKey;
                    break;
                case "tabDatabaseConnection":
                    TabControl.SelectedTab = tabDatabaseConnection;
                    break;
                case "tabReciptPrinter":
                    TabControl.SelectedTab = tabReciptPrinter;
                    break;
                case "tabSerialPort":
                    TabControl.SelectedTab = tabSerialPort;
                    break;
                default:
                    TabControl.SelectedTab = tabezPOSProInfo;
                    break;
            }

            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string path = Path.Combine(exeFolder, "eula.rtf");

            txtLicense.LoadFile(path);
            if (txtKey.Text == "")
                label8.Visible = true;
            else
                label8.Visible = false;
        }

        #region License Key
        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKey.Text != "" && txtKey.Text.Count() == 19)
                {
                    btnActive.Text = "Please wait...";
                    btnActive.Enabled = false;
                    if (XMLData.Type == 1)
                    {
                        KeyResponse _KeyResponse = new KeyResponse();
                        APIUtility _APIUtility = new APIUtility();

                        _KeyResponse = _APIUtility.ActiveKey(txtKey.Text.Trim());
                        if (_KeyResponse.Status == 1)
                        {
                            XMLData.Key = _KeyResponse.KeyDetails[0].GeneratedKey;
                            XMLData.PriorityCode = ClsCommon.Decrypt(_KeyResponse.KeyDetails[0].TotalNodes.ToString());
                            XMLData.POSStatus = true;
                            ClsCommon.Update_XMLFile();
                            UpdateSettingTbl();
                            //ClsCommon.MsgBox("Information", "Key verification successful.", false);
                            //Application.Exit();

                            ClsCommon.MsgBox("Information", _KeyResponse.Message, false);
                            label8.Visible = false;
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", _KeyResponse.Message, false);
                            txtKey.Text = "";
                        }
                    }
                    else if (XMLData.Type == 2)
                    {
                        Key_ActiveNodes();
                        if (XMLData.Key != "")
                        {
                            if (AllowLogin())
                            {
                                if (XMLData.Key == txtKey.Text.Trim())
                                {
                                    UpdateLogTblsEntry();
                                    XMLData.POSStatus = true;
                                    //XMLData.PriorityCode = XMLData.PriorityCode_BE;
                                    ClsCommon.Update_XMLFile();
                                    MessageBox.Show("Key verification successful.");
                                    Application.Exit();
                                    label8.Visible = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("This license key has been used for a maximum node, Please contact the system administrator!");
                                Application.Exit();
                                label8.Visible = true;
                            }
                        }
                        else
                        {
                            XMLData.Key = "";
                            MessageBox.Show("This key is not activated with backend");
                            label8.Visible = true;
                        }
                    }
                    btnActive.Text = "ACTIVATE";
                    btnActive.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please enter valid key!");
                    label8.Visible = true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtKey.Text.Length == 4)
                {
                    txtKey.Text = txtKey.Text + "-";
                }
                else if (txtKey.Text.Length == 9)
                {
                    txtKey.Text = txtKey.Text + "-";
                }
                else if (txtKey.Text.Length == 14)
                {
                    txtKey.Text = txtKey.Text + "-";
                }
                txtKey.SelectionStart = txtKey.Text.Length;
                txtKey.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region Scanner
        //private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        SerialPort sp = (SerialPort)sender;
        //        string indata = sp.ReadExisting();
        //        Console.WriteLine("Data Received:");
        //        Console.Write(indata);
        //        if (indata != String.Empty)
        //        {
        //            this.BeginInvoke(new SetTextCallback(SetText), new object[] { indata });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
        //    }
        //}
        //public void OpenPort()
        //{
        //    try
        //    {

        //        string[] Ports = SerialPort.GetPortNames();
        //        if (Ports.Count() > 0)
        //        {
        //            if (ComPort.IsOpen == false)
        //            {
        //                ComPort.PortName = ComInfo.ComPort;
        //                ComPort.BaudRate = ComInfo.BaudRate;
        //                ComPort.DataBits = ComInfo.DataBits;
        //                ComPort.StopBits = ComInfo.StopBits;
        //                ComPort.Handshake = ComInfo.Handshake;
        //                ComPort.Parity = ComInfo.Parity;
        //                ComPort.RtsEnable = true;
        //                ComPort.DtrEnable = true;
        //                ComPort.Open();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
        //    }
        //}
        //private void SetText(string text)
        //{
        //    this.rtbIncoming.Text += text;
        //}
        //public void LoadPort()
        //{
        //    try
        //    {
        //        cboPorts.Items.Clear();
        //        string[] ArrayComPortsNames = null;
        //        int index = -1;
        //        string ComPortName = null;
        //        ArrayComPortsNames = SerialPort.GetPortNames();
        //        do
        //        {
        //            index += 1;
        //            cboPorts.Items.Add(ArrayComPortsNames[index]);

        //        } while (!((ArrayComPortsNames[index] == ComPortName) || (index == ArrayComPortsNames.GetUpperBound(0))));
        //        Array.Sort(ArrayComPortsNames);

        //        if (index == ArrayComPortsNames.GetUpperBound(0))
        //        {
        //            ComPortName = ArrayComPortsNames[0];
        //        }
        //        ComPort.RtsEnable = true;
        //        ComPort.DtrEnable = true;
        //        cboBaudRate.Text = "9600";
        //        cboDataBits.Text = "7";
        //        cboStopBits.Text = "One";
        //        cboParity.Text = "Odd";
        //        cboHandShaking.Text = "RequestToSendXOnXOff";
        //        cboPorts.Text = ArrayComPortsNames[0];
        //        OpenPort();
        //    }
        //    catch (Exception ex)
        //    {
        //        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
        //    }
        //}

        public void LoadPort()
        {
            try
            {
                var deviceCollection = myPosExplorer.GetDevices(DeviceType.Scanner);
                foreach (DeviceInfo deviceInfo in deviceCollection)
                {
                    cmbScanner.Items.Add(deviceInfo.ServiceObjectName);
                }

                var deviceScale = myPosExplorer.GetDevices(DeviceType.Scale);
                foreach (DeviceInfo deviceInfo in deviceScale)
                {
                    cmbScale.Items.Add(deviceInfo.ServiceObjectName);
                }

                cmbScanner.SelectedItem = XMLData.Scanner;
                cmbScale.SelectedItem = XMLData.Scale;
            }
            catch (Exception ex)
            {

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
            lstItems.Items.Add(myEncoding.GetString(myScanner.ScanDataLabel));
            if (myScanner.DataEventEnabled == false)
            {
                myScanner.DataEventEnabled = true;
            }
        }

        public void DeviceRemove()
        {
            try
            {
                if (myScanner != null)
                {
                    if (myScanner.DataEventEnabled == true)
                    {
                        myScanner.DataEventEnabled = false;
                        myScanner.DeviceEnabled = false;
                        myScanner.Release();
                        myScanner.Close();
                    }
                    if (myScale.DataEventEnabled == true)
                    {
                        myScale.DataEventEnabled = false;
                        myScale.DeviceEnabled = false;
                        myScale.Release();
                        myScale.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void frmSettings_BE_Leave(object sender, EventArgs e)
        {
            try
            {
                DeviceRemove();
                //PortOpen_Close(false);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
            }
        }

        private void frmSettings_BE_VisibleChanged(object sender, EventArgs e)
        {
            if (FirstCall)
            {
                DeviceRemove();
                //PortOpen_Close(false);
            }
            FirstCall = true;
        }

        private void btnReadWeight_Click(object sender, EventArgs e)
        {
            lstItems.Items.Add(myScale.ReadWeight(1000).ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                XMLData.Scanner = cmbScanner.SelectedItem.ToString();
                XMLData.Scale = cmbScale.SelectedItem.ToString();
                if (toggleActive.Checked)
                    XMLData.ScannerInUsed = true;
                else
                    XMLData.ScannerInUsed = false;
                ClsCommon.Update_XMLFile();
                ClsCommon.MsgBox("Information", "Scanner/Scale details are saved, Please restart the application", false);
                //MessageBox.Show("Scanner/Scale details are saved");
                Application.Exit();
            }
            catch (Exception ex)
            {
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbScanner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var deviceCollection = myPosExplorer.GetDevices(DeviceType.Scanner);
                foreach (DeviceInfo deviceInfo in deviceCollection)
                {
                    if (deviceInfo.ServiceObjectName == cmbScanner.SelectedItem.ToString())
                    {
                        myScanner = (Scanner)myPosExplorer.CreateInstance(deviceInfo);
                        if (myScanner.DataEventEnabled == false)
                        {
                            myScanner.Open();
                            myScanner.Claim(1000);
                            myScanner.DataEvent += myScanner_DataEvent;
                            myScanner.DeviceEnabled = true;
                            myScanner.DataEventEnabled = true;
                            myScanner.DecodeData = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void cmbScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var deviceScale = myPosExplorer.GetDevices(DeviceType.Scale);
                foreach (DeviceInfo deviceInfo in deviceScale)
                {
                    if (deviceInfo.ServiceObjectName == cmbScale.SelectedItem.ToString())
                    {
                        if (myScale.DataEventEnabled == false)
                        {
                            myScale = (Scale)myPosExplorer.CreateInstance(deviceInfo);
                            myScale.Open();
                            myScale.Claim(1000);
                            myScale.StatusNotify = StatusNotify.Enabled;
                            myScale.DeviceEnabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Db Connection 
        private void btnTestConnections_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtDatabaseServerName.Text.Trim()))
                {
                    ClsCommon.MsgBox("Information", "Please enter database server name", false);
                }
                else if (String.IsNullOrEmpty(txtDatabaseName.Text.Trim()))
                {
                    ClsCommon.MsgBox("Information", "Please enter database name", false);
                }
                else if (String.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    ClsCommon.MsgBox("Information", "Please enter user name", false);

                }
                else if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    ClsCommon.MsgBox("Information", "Please enter password", false);
                }
                else
                {
                    btnTestConnections.Text = "Please wait...";
                    dbValid = db_Connection("data source=" + txtDatabaseServerName.Text.Trim() + ";initial catalog=" + txtDatabaseName.Text.Trim() + ";persist security info=True;user id=" + txtUserName.Text.Trim() + ";password=" + txtPassword.Text.Trim() + ";");
                    if (dbValid)
                    {
                        ClsCommon.MsgBox("Information", "Database details are validate, Please save/update details !", false);
                    }
                    else
                    {
                        ClsCommon.MsgBox("Information", "Database details are not validate, Please check and try again !", false);
                    }
                    btnTestConnections.Text = "TEST CONNECTIONS";
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
            }

        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (dbValid)
                {
                    XMLData.ServerName = txtDatabaseServerName.Text.Trim();
                    XMLData.DbName = txtDatabaseName.Text.Trim();
                    XMLData.UserName = txtUserName.Text.Trim();
                    XMLData.Password = txtPassword.Text.Trim();
                    XMLData.DbConnectionString = "data source=" + txtDatabaseServerName.Text.Trim() + ";initial catalog=" + txtDatabaseName.Text.Trim() + ";persist security info=True;user id=" + txtUserName.Text.Trim() + ";password=" + txtPassword.Text.Trim() + ";";
                    if (XMLData.Type == 1)
                    {
                        UpdateSettingTbl();
                    }
                    ClsCommon.Update_XMLFile();
                    ClsCommon.MsgBox("Information", "Database details save/update", false);
                    if (!XMLData.POSStatus)
                    {
                        TabControl.SelectedTab = tabLicenceKey;
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    XMLData.DbConnectionString = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Settings - btnConnect_Click:- " + ex.ToString());
            }
        }
        public void UpdateSettingTbl()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var SettingRow = _db.tbl_SettingMaster.FirstOrDefault();
                if (SettingRow != null)
                {
                    SettingRow.LicenceKey = XMLData.Key;
                    SettingRow.DatabaseServerName = XMLData.ServerName;
                    SettingRow.DatabaseName = XMLData.DbName;
                    SettingRow.UserName = XMLData.UserName;
                    SettingRow.Password = XMLData.Password;
                    SettingRow.ReciptPrinterName = XMLData.PrinterName;
                    SettingRow.Disclaimer = XMLData.Disclaimer;
                    SettingRow.SerialPort = XMLData.Scanner;
                    SettingRow.CreatedDate = XMLData.InstallationDate;
                    SettingRow.CreatedBy = LoginInfo.UserId;
                    SettingRow.UpdatedDate = DateTime.Now;
                    SettingRow.UpdatedBy = LoginInfo.UserId;
                }
                else
                {
                    tbl_SettingMaster _tbl_SettingMaster = new tbl_SettingMaster();
                    _tbl_SettingMaster.LicenceKey = XMLData.Key;
                    _tbl_SettingMaster.DatabaseServerName = XMLData.ServerName;
                    _tbl_SettingMaster.DatabaseName = XMLData.DbName;
                    _tbl_SettingMaster.UserName = XMLData.UserName;
                    _tbl_SettingMaster.Password = XMLData.Password;
                    _tbl_SettingMaster.ReciptPrinterName = XMLData.PrinterName;
                    _tbl_SettingMaster.Disclaimer = XMLData.Disclaimer;
                    _tbl_SettingMaster.SerialPort = XMLData.Scanner;
                    _tbl_SettingMaster.CreatedDate = XMLData.InstallationDate;
                    _tbl_SettingMaster.CreatedBy = LoginInfo.UserId;
                    _tbl_SettingMaster.UpdatedDate = DateTime.Now;
                    _tbl_SettingMaster.UpdatedBy = LoginInfo.UserId;
                    _db.tbl_SettingMaster.Add(_tbl_SettingMaster);
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Settings - UpdateSettingTbl" + ex.Message.ToString());
            }
        }
        public void UpdateLogTblsEntry()
        {
            try
            {
                #region Counter master
                DataContext _db_Counter = SFPOSDataContext.Create(XMLData.DbConnectionString);

                tbl_CounterMaster _tbl_CounterMaster = new tbl_CounterMaster();
                _tbl_CounterMaster.CounterIP = LoginInfo.CounterIP;
                _tbl_CounterMaster.IsActive = true;
                _tbl_CounterMaster.CreatedDate = DateTime.Now;
                _db_Counter.tbl_CounterMaster.Add(_tbl_CounterMaster);
                _db_Counter.SaveChanges();
                #endregion

                #region TblName
                string[] tblName_Live_Local = new string[13];
                tblName_Live_Local[0] = "tbl_DepartmentMaster";
                tblName_Live_Local[1] = "tbl_EmployeeMaster";
                tblName_Live_Local[2] = "tbl_ProductMaster";
                tblName_Live_Local[3] = "tbl_ProductSalePriceMaster";
                tblName_Live_Local[4] = "tbl_RoleMaster";
                tblName_Live_Local[5] = "tbl_RolePermission";
                tblName_Live_Local[6] = "tbl_SectionMaster";
                tblName_Live_Local[7] = "tbl_SettingMaster";
                tblName_Live_Local[8] = "tbl_ShortcutkeyMaster";
                tblName_Live_Local[9] = "tbl_StoreMaster";
                tblName_Live_Local[10] = "tbl_TaxGroupMaster";
                tblName_Live_Local[11] = "tbl_TaxRateMaster";
                tblName_Live_Local[12] = "tbl_UnitMeasureMaster";

                string[] tblName_Local_Live = new string[6];
                tblName_Local_Live[0] = "tbl_LoginMaster";
                tblName_Local_Live[1] = "tbl_OrderDetail";
                tblName_Local_Live[2] = "tbl_OrderMaster";
                tblName_Local_Live[3] = "tbl_PaymentTrans";
                tblName_Local_Live[4] = "tbl_ProductLedger";
                tblName_Local_Live[5] = "tbl_TransSuspenMaster";
                #endregion

                #region UpdateLog Master
                DataContext _db_Live_Local = SFPOSDataContext.Create(XMLData.DbConnectionString);
                for (int i = 0; i < tblName_Live_Local.Count(); i++)
                {
                    tbl_UpdateLog _tbl_UpdateLog = new tbl_UpdateLog();
                    _tbl_UpdateLog.TblName = tblName_Live_Local[i];
                    _tbl_UpdateLog.UpdateType = "Live -> Local";
                    _tbl_UpdateLog.UpdatedBy = 0;
                    _tbl_UpdateLog.UpdatedDate = null;
                    _tbl_UpdateLog.IsSync = false;
                    _tbl_UpdateLog.SyncDate = null;
                    _tbl_UpdateLog.CounterID = 0;
                    _tbl_UpdateLog.CounterIP = LoginInfo.CounterIP;
                    _tbl_UpdateLog.IsChange = false;
                    _db_Live_Local.tbl_UpdateLog.Add(_tbl_UpdateLog);
                }
                _db_Live_Local.SaveChanges();

                DataContext _db_Local_Live = SFPOSDataContext.Create(XMLData.DbConnectionString);
                for (int i = 0; i < tblName_Local_Live.Count(); i++)
                {
                    tbl_UpdateLog _tbl_UpdateLog = new tbl_UpdateLog();
                    _tbl_UpdateLog.TblName = tblName_Local_Live[i];
                    _tbl_UpdateLog.UpdateType = "Local -> Live";
                    _tbl_UpdateLog.UpdatedBy = 0;
                    _tbl_UpdateLog.UpdatedDate = null;
                    _tbl_UpdateLog.IsSync = false;
                    _tbl_UpdateLog.SyncDate = null;
                    _tbl_UpdateLog.CounterID = 0;
                    _tbl_UpdateLog.CounterIP = LoginInfo.CounterIP;
                    _tbl_UpdateLog.IsChange = false;
                    _db_Local_Live.tbl_UpdateLog.Add(_tbl_UpdateLog);
                }
                _db_Local_Live.SaveChanges();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Settings - UpdateLogTblsEntry" + ex.Message.ToString());
            }
        }

        #endregion

        #region Recipt Printer
        private void btnSaveUpdate_ReciptPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtPrinterName.Text.Trim()))
                {
                    ClsCommon.MsgBox("Information", "Please enter recipt printer name", false);
                }
                if (String.IsNullOrEmpty(txtLabelPrinter.Text.Trim()))
                {
                    ClsCommon.MsgBox("Information", "Please enter label printer name", false);
                }
                else
                {
                    XMLData.PrinterName = txtPrinterName.Text.Trim();
                    XMLData.Disclaimer = txtLabelPrinter.Text.Trim();
                    ClsCommon.Update_XMLFile();
                    ClsCommon.MsgBox("Information", "Recipt printer setting save/updated, Please restart the application", false);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Common Fuctions
        public static bool AllowLogin()
        {
            bool result = false;
            int CurrentNodes = 0;
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var objData = _db.tbl_CounterMaster.ToList();
                CurrentNodes = objData.Count;
                int Allow = Convert.ToInt32(XMLData.PriorityCode_BE);
                if (Allow > CurrentNodes)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public void Key_ActiveNodes()
        {
            try
            {
                if (ClsCommon.CheckConnection())
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var objData = _db.tbl_SettingMaster.Where(x => x.LicenceKey != null).FirstOrDefault();
                    if (objData != null)
                    {
                        if (objData.LicenceKey != null)
                        {
                            XMLData.Key = objData.LicenceKey;
                            GetPOSNodes_revers();
                        }
                        else
                        {
                            XMLData.Key = "";
                            XMLData.PriorityCode_BE = "";
                        }
                    }
                    else
                    {
                        XMLData.Key = "";
                        XMLData.PriorityCode_BE = "";
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please connect with backend-server for key validations!", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Settings - Key_ActiveNodes" + ex.Message.ToString());
            }
        }
        public static void GetPOSNodes_revers()
        {
            try
            {
                //T5O5-07HG-GDIN-JYGH
                Random rnd = new Random();
                string Key_ = XMLData.Key.Substring(10, 4);
                Key_ = ClsCommon.Encryp(Key_);
                int lenth = Convert.ToInt32(Key_.Substring(0, 1));
                string node = Key_.Substring(Key_.Length - lenth, lenth);
                XMLData.PriorityCode_BE = node;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Settings - GetPOSNodes_revers" + ex.Message.ToString());
            }

        }
        public bool CheckConnection(string Conn)
        {
            bool Status;
            var task = Task.Run(() =>
            {
                Status = db_Connection(Conn);
            });
            bool isCompletedSuccessfully = task.Wait(TimeSpan.FromMilliseconds(3000));
            if (isCompletedSuccessfully)
            {
                Status = db_Connection(Conn);
            }
            else
            {
                Status = false;
            }
            return Status;
        }
        public bool db_Connection(string Conn)
        {
            try
            {
                DataContext _dbu = SFPOSDataContext.Create(Conn);
                var Empl = _dbu.tbl_EmployeeMaster.FirstOrDefault();
                //
                if (Empl != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
                return false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
                return false;
            }
        }

        public void DataLoad()
        {
            try
            {
                lblPOSType.Text = "POS Type: " + XMLData.POSType;
                lblInstationoDate.Text = "Installation Date: " + XMLData.InstallationDate.ToShortDateString();
                txtKey.Text = XMLData.Key;
                txtDatabaseServerName.Text = XMLData.ServerName;
                txtDatabaseName.Text = XMLData.DbName;
                txtUserName.Text = XMLData.UserName;
                txtPassword.Text = XMLData.Password;
                txtPrinterName.Text = XMLData.PrinterName;
                txtLabelPrinter.Text = XMLData.Disclaimer;
                if (XMLData.ScannerInUsed)
                    toggleActive.Checked = true;
                else
                    toggleActive.Checked = false;


                //Release
                lblCurrentVersion.Text = XMLData.Version;
                lblReleaseUpdatedDate.Text = XMLData.InstallationDate.ToString();

                #region Release Info
                DataContext _db_Counter = SFPOSDataContext.Create(XMLData.DbConnectionString);
                tbl_CounterMaster _tbl_CounterMaster = new tbl_CounterMaster();
                _tbl_CounterMaster = _db_Counter.tbl_CounterMaster.Where(x => x.MacAddress == LoginInfo.MacAddress).FirstOrDefault();
                if (_tbl_CounterMaster != null)
                {
                    if (_tbl_CounterMaster.MacAddress != null)
                    {
                        //Release
                        lblCurrentVersion.Text = _tbl_CounterMaster.CurrentVersion.ToString();
                        lblCurrentRelase.Text = _tbl_CounterMaster.CurrentReleaseID.ToString();
                        lblLastRelase.Text = _tbl_CounterMaster.LastReleaseID.ToString();
                        lblReleaseUpdatedDate.Text = _tbl_CounterMaster.ReleaseUpdatedDate.ToString();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
