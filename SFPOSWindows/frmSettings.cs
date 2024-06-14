using MetroFramework.Forms;
using Microsoft.PointOfService;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Printer;
using SFPOS.Printer.Enums;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Threading.Tasks;

namespace SFPOSWindows
{
    public partial class frmSettings : MetroForm
    {
        #region Properties
        //SerialPort ComPort = new SerialPort();
        //internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        //internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        //delegate void SetTextCallback(string text);

        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        private Scale myScale;

        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        static bool dbValid = false;
        WebClient client;
        #endregion

        #region Events
        public frmSettings()
        {
            InitializeComponent();
            //ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            myPosExplorer = new PosExplorer(this);
            ClsCommon.Read_XMLFile();
            ClsCommon.Read_ExtraSettingXMLFile();
            ClsCommon.SetScreen(this, XMLData.POSScreen);

        }
        private async void frmSettings_Load(object sender, EventArgs e)
        {
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    Functions.SetIcon(this);
            //}

            LoadPort();
            LoadScreens();
            DataLoad();

            this.BringToFront();
            if (XMLData.EPXShow != 1)
            {
                foreach (TabPage tab in TabControl.TabPages)
                {
                    if (tab.Text == "EPX")
                    {
                        TabControl.TabPages.Remove(tab);
                    }
                }
            }
            if (XMLData.IsDemoVersion == 1)
            {
                DemoVersionChange();
                #region Old Code for demoversion
                //foreach (TabPage tab in TabControl.TabPages)
                //{
                //    if (tab.Name == "tabDatabaseConnection")
                //    {
                //        TabControl.TabPages.Remove(tab);
                //    }
                //}
                #endregion
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
                case "tabDataSyncTime":
                    TabControl.SelectedTab = tabDataSyncTime;
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
                        ClsCommon.MsgBox("Information", "Database details are validate, Please save details !", false);
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "FrmSettings" + ex.StackTrace, ex.LineNumber());
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
                    ClsCommon.MsgBox("Information", "Database details save", false);

                    #region CheckTab
                    if (XMLData.DbConnectionString == "")
                    {
                        TabControl.SelectedTab = tabDatabaseConnection;
                    }
                    else if (!XMLData.POSStatus)
                    {
                        TabControl.SelectedTab = tabLicenceKey;
                    }
                    else if (XMLData.PrinterName == "")
                    {
                        TabControl.SelectedTab = tabReciptPrinter;
                    }
                    else if (XMLData.PrinterName == "")
                    {
                        TabControl.SelectedTab = tabReciptPrinter;
                    }
                    else if (XMLData.Scanner == "")
                    {
                        TabControl.SelectedTab = tabSerialPort;
                    }
                    else if (XMLData.LiveToLocalTime == 0)
                    {
                        TabControl.SelectedTab = tabDataSyncTime;
                    }
                    else
                    {
                        Application.Exit();
                    }
                    #endregion
                }
                else
                {
                    XMLData.DbConnectionString = "";
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Error: Settings - btnConnect_Click: - " + ex.Message.ToString(), false);
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
                    SettingRow.StoreID = 1;
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
                    _tbl_SettingMaster.StoreID = 1;
                    _tbl_SettingMaster.LicenceKey = XMLData.Key; //== null ? "" : XMLData.Key;
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "Error: Settings - UpdateSettingTbl" + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

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
                        #region Live
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
                            #region CheckTab
                            if (XMLData.DbConnectionString == "")
                            {
                                TabControl.SelectedTab = tabDatabaseConnection;
                            }
                            else if (!XMLData.POSStatus)
                            {
                                TabControl.SelectedTab = tabLicenceKey;
                            }
                            else if (XMLData.PrinterName == "")
                            {
                                TabControl.SelectedTab = tabReciptPrinter;
                            }
                            else if (XMLData.PrinterName == "")
                            {
                                TabControl.SelectedTab = tabReciptPrinter;
                            }
                            else if (XMLData.Scanner == "")
                            {
                                TabControl.SelectedTab = tabSerialPort;
                            }
                            else if (XMLData.LiveToLocalTime == 0)
                            {
                                TabControl.SelectedTab = tabDataSyncTime;
                            }
                            else
                            {
                                Application.Exit();
                            }
                            #endregion
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", _KeyResponse.Message, false);
                            txtKey.Text = "";
                        }
                        #endregion

                        #region Local

                        //XMLData.Key = txtKey.Text.Trim();
                        //XMLData.PriorityCode = "15";
                        //XMLData.POSStatus = true;
                        //ClsCommon.Update_XMLFile();
                        //UpdateSettingTbl();
                        ////ClsCommon.MsgBox("Information", "Key verification successful.", false);
                        ////Application.Exit();

                        //ClsCommon.MsgBox("Information", "Key verification successful.", false);

                        //#region CheckTab
                        //if (XMLData.DbConnectionString == "")
                        //{
                        //    TabControl.SelectedTab = tabDatabaseConnection;
                        //}
                        //else if (!XMLData.POSStatus)
                        //{
                        //    TabControl.SelectedTab = tabLicenceKey;
                        //}
                        //else if (XMLData.PrinterName == "")
                        //{
                        //    TabControl.SelectedTab = tabReciptPrinter;
                        //}
                        //else if (XMLData.PrinterName == "")
                        //{
                        //    TabControl.SelectedTab = tabReciptPrinter;
                        //}
                        //else if (XMLData.Scanner == "")
                        //{
                        //    TabControl.SelectedTab = tabSerialPort;
                        //}
                        //else if (XMLData.LiveToLocalTime == 0)
                        //{
                        //    TabControl.SelectedTab = tabDataSyncTime;
                        //}
                        //else
                        //{
                        //    Application.Exit();
                        //}
                        //#endregion

                        #endregion

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
                                    ClsCommon.MsgBox("Information", "Key verification successful", false);
                                    //Application.Exit();
                                    label8.Visible = false;
                                    #region CheckTab
                                    if (XMLData.DbConnectionString == "")
                                    {
                                        TabControl.SelectedTab = tabDatabaseConnection;
                                    }
                                    else if (!XMLData.POSStatus)
                                    {
                                        TabControl.SelectedTab = tabLicenceKey;
                                    }
                                    else if (XMLData.PrinterName == "")
                                    {
                                        TabControl.SelectedTab = tabReciptPrinter;
                                    }
                                    else if (XMLData.PrinterName == "")
                                    {
                                        TabControl.SelectedTab = tabReciptPrinter;
                                    }
                                    else if (XMLData.Scanner == "")
                                    {
                                        TabControl.SelectedTab = tabSerialPort;
                                    }
                                    else if (XMLData.LiveToLocalTime == 0)
                                    {
                                        TabControl.SelectedTab = tabDataSyncTime;
                                    }
                                    else
                                    {
                                        Application.Exit();
                                    }
                                    #endregion
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", "Please enter valid key!", false);
                                    txtKey.Text = "";
                                }
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "This license key has been used for a maximum node, Please contact the system administrator!", false);
                                txtKey.Text = "";
                                label8.Visible = true;
                                //Application.Exit();
                            }
                        }
                        else
                        {
                            XMLData.Key = "";
                            ClsCommon.MsgBox("Information", "This key is not activated with backend", false);
                            txtKey.Text = "";
                            label8.Visible = true;
                        }
                    }
                    btnActive.Text = "ACTIVATE";
                    btnActive.Enabled = true;
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please enter valid key!", false);
                    txtKey.Text = "";
                    label8.Visible = true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "FrmSettings" + ex.StackTrace, ex.LineNumber());
                ClsCommon.MsgBox("Information", "Please enter valid key!", false);
                txtKey.Text = "";
                label8.Visible = true;
            }
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
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

        public void UpdateLogTblsEntry()
        {
            try
            {
                #region Counter master
                DataContext _db_Counter = SFPOSDataContext.Create(XMLData.DbConnectionString);

                tbl_CounterMaster _tbl_CounterMaster = new tbl_CounterMaster();
                _tbl_CounterMaster = _db_Counter.tbl_CounterMaster.Where(x => x.MacAddress == LoginInfo.MacAddress).FirstOrDefault();
                if (_tbl_CounterMaster != null)
                {
                    if (_tbl_CounterMaster.MacAddress != null)
                    {
                        _tbl_CounterMaster.CounterIP = LoginInfo.CounterIP;
                        _tbl_CounterMaster.MacAddress = LoginInfo.MacAddress;
                        _tbl_CounterMaster.StoreID = 1;
                        _tbl_CounterMaster.CurrentVersion = XMLData.Version; //"2.0";
                        _tbl_CounterMaster.IsActive = true;
                        _tbl_CounterMaster.UpdatedDate = DateTime.Now;
                        //Release
                        _tbl_CounterMaster.CurrentReleaseID = 0;
                        _tbl_CounterMaster.LastReleaseID = 0;
                        _tbl_CounterMaster.ReleaseUpdatedBy = 1;
                        _tbl_CounterMaster.ReleaseUpdatedDate = DateTime.Now;
                        _tbl_CounterMaster.SystemType = (XMLData.Type == 1) ? "intPOS-Backend" : "intPOS-Frontend";
                    }
                }
                else
                {
                    _tbl_CounterMaster = new tbl_CounterMaster();
                    _tbl_CounterMaster.CounterIP = LoginInfo.CounterIP;
                    _tbl_CounterMaster.MacAddress = LoginInfo.MacAddress;
                    _tbl_CounterMaster.IsActive = true;
                    _tbl_CounterMaster.StoreID = 1;
                    _tbl_CounterMaster.CurrentVersion = XMLData.Version; //"2.0";
                    _tbl_CounterMaster.CreatedDate = DateTime.Now;
                    //Release
                    _tbl_CounterMaster.CurrentReleaseID = 0;
                    _tbl_CounterMaster.LastReleaseID = 0;
                    _tbl_CounterMaster.ReleaseUpdatedBy = 1;
                    _tbl_CounterMaster.ReleaseUpdatedDate = DateTime.Now;
                    _tbl_CounterMaster.SystemType = (XMLData.Type == 1) ? "intPOS-Backend" : "intPOS-Frontend";

                    _db_Counter.tbl_CounterMaster.Add(_tbl_CounterMaster);
                }
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

                string[] tblName_Local_Live = new string[7];
                tblName_Local_Live[0] = "tbl_LoginMaster";
                tblName_Local_Live[1] = "tbl_OrderDetail";
                tblName_Local_Live[2] = "tbl_OrderMaster";
                tblName_Local_Live[3] = "tbl_PaymentTrans";
                tblName_Local_Live[4] = "tbl_ProductLedger";
                tblName_Local_Live[5] = "tbl_TransSuspenMaster";
                tblName_Local_Live[6] = "tbl_ExceptionLog";
                #endregion

                #region UpdateLog Master
                DataContext _db_Live_Local = SFPOSDataContext.Create(XMLData.DbConnectionString);
                string tbl = "";
                for (int i = 0; i < tblName_Live_Local.Count(); i++)
                {
                    tbl_UpdateLog _tbl_UpdateLog = new tbl_UpdateLog();
                    tbl = tblName_Live_Local[i].ToString();
                    _tbl_UpdateLog = _db_Live_Local.tbl_UpdateLog.Where(x => x.MacAddress == LoginInfo.MacAddress && x.TblName == tbl).FirstOrDefault();
                    if (_tbl_UpdateLog != null)
                    {
                        if (_tbl_UpdateLog.TblName == tblName_Live_Local[i])
                        {
                            _tbl_UpdateLog.TblName = tblName_Live_Local[i];
                            _tbl_UpdateLog.UpdateType = "Live -> Local";
                            _tbl_UpdateLog.UpdatedBy = 0;
                            _tbl_UpdateLog.UpdatedDate = null;
                            _tbl_UpdateLog.IsSync = false;
                            _tbl_UpdateLog.SyncDate = null;
                            _tbl_UpdateLog.CounterID = 0;
                            _tbl_UpdateLog.CounterIP = LoginInfo.CounterIP;
                            _tbl_UpdateLog.MacAddress = LoginInfo.MacAddress;
                            _tbl_UpdateLog.IsChange = false;
                        }
                    }
                    else
                    {
                        _tbl_UpdateLog = new tbl_UpdateLog();
                        _tbl_UpdateLog.TblName = tblName_Live_Local[i];
                        _tbl_UpdateLog.UpdateType = "Live -> Local";
                        _tbl_UpdateLog.UpdatedBy = 0;
                        _tbl_UpdateLog.UpdatedDate = null;
                        _tbl_UpdateLog.IsSync = false;
                        _tbl_UpdateLog.SyncDate = null;
                        _tbl_UpdateLog.CounterID = 0;
                        _tbl_UpdateLog.CounterIP = LoginInfo.CounterIP;
                        _tbl_UpdateLog.MacAddress = LoginInfo.MacAddress;
                        _tbl_UpdateLog.IsChange = false;
                        _db_Live_Local.tbl_UpdateLog.Add(_tbl_UpdateLog);
                    }
                }
                _db_Live_Local.SaveChanges();

                DataContext _db_Local_Live = SFPOSDataContext.Create(XMLData.DbConnectionString);
                for (int i = 0; i < tblName_Local_Live.Count(); i++)
                {
                    tbl_UpdateLog _tbl_UpdateLog = new tbl_UpdateLog();
                    tbl = tblName_Local_Live[i].ToString();
                    _tbl_UpdateLog = _db_Local_Live.tbl_UpdateLog.Where(x => x.MacAddress == LoginInfo.MacAddress && x.TblName == tbl).FirstOrDefault();
                    if (_tbl_UpdateLog != null)
                    {
                        if (_tbl_UpdateLog.TblName == tblName_Local_Live[i])
                        {
                            _tbl_UpdateLog.TblName = tblName_Local_Live[i];
                            _tbl_UpdateLog.UpdateType = "Local -> Live";
                            _tbl_UpdateLog.UpdatedBy = 0;
                            _tbl_UpdateLog.UpdatedDate = null;
                            _tbl_UpdateLog.IsSync = false;
                            _tbl_UpdateLog.SyncDate = null;
                            _tbl_UpdateLog.CounterID = 0;
                            _tbl_UpdateLog.CounterIP = LoginInfo.CounterIP;
                            _tbl_UpdateLog.MacAddress = LoginInfo.MacAddress;
                            _tbl_UpdateLog.IsChange = false;
                        }
                    }
                    else
                    {
                        _tbl_UpdateLog = new tbl_UpdateLog();
                        _tbl_UpdateLog.TblName = tblName_Local_Live[i];
                        _tbl_UpdateLog.UpdateType = "Local -> Live";
                        _tbl_UpdateLog.UpdatedBy = 0;
                        _tbl_UpdateLog.UpdatedDate = null;
                        _tbl_UpdateLog.IsSync = false;
                        _tbl_UpdateLog.SyncDate = null;
                        _tbl_UpdateLog.CounterID = 0;
                        _tbl_UpdateLog.CounterIP = LoginInfo.CounterIP;
                        _tbl_UpdateLog.MacAddress = LoginInfo.MacAddress;
                        _tbl_UpdateLog.IsChange = false;
                        _db_Local_Live.tbl_UpdateLog.Add(_tbl_UpdateLog);
                    }
                }
                _db_Local_Live.SaveChanges();
                #endregion
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "Error: Settings - UpdateLogTblsEntry" + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region Scanner


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
            }
            catch (Exception ex)
            {

            }
        }

        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            try
            {
                ASCIIEncoding myEncoding = new ASCIIEncoding();
                string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
                if (UPCCode.Length > 1)
                {
                    if (myScanner.ScanDataType != BarCodeSymbology.Code39)
                        UPCCode = UPCCode.Substring(0, UPCCode.Length - 1);
                }
                lstItems.Items.Add(myEncoding.GetString(myScanner.ScanDataLabel));
                myScanner.DataEventEnabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnReadWeight_Click(object sender, EventArgs e)
        {
            try
            {
                lstItems.Items.Add(myScale.ReadWeight(1000).ToString());
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                XMLData.Scanner = cmbScanner.SelectedItem.ToString();
                XMLData.Scale = cmbScale.SelectedItem.ToString();
                XMLData.IsSync = ToggleSunc.Checked;
                if (XMLData.IsSync == true)
                {
                    if (txtSyncTime.Text == "")
                    {
                        ClsCommon.MsgBox("Information", "Please enter sync time", false);
                        return;
                    }
                    XMLData.SyncTime = Convert.ToInt32(txtSyncTime.Text.Trim());
                }
                else
                {
                    string message = "Sync mode should only be disabled for testing purposes. Are you sure want to disable?";
                    string title = "Information";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
                ClsCommon.Update_XMLFile();
                ClsCommon.MsgBox("Information", "Scanner/Scale details are saved", false);
                Application.Exit();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
            }
        }

        public void DeviceRemove()
        {
            try
            {
                if (myScanner.DataEventEnabled == true)
                {
                    myScanner.DataEventEnabled = false;
                    myScanner.DeviceEnabled = false;
                    myScanner.Release();
                    myScanner.Close();

                    myScale.DeviceEnabled = false;
                    myScale.Release();
                    myScale.Close();

                    LoginInfo.OpenScale = true;
                }
            }
            catch (Exception ex)
            {

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
                        myScanner.Open();

                        if (myScanner.DataEventEnabled == false)
                        {
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
                ClsCommon.MsgBox("Information", "Scanner device not attached to this system or maybe power off.", false);
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

                        myScale = (Scale)myPosExplorer.CreateInstance(deviceInfo);
                        myScale.Open();
                        if (myScale.DataEventEnabled == false)
                        {
                            myScale.Claim(1000);
                            myScale.StatusNotify = StatusNotify.Enabled;
                            myScale.DeviceEnabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Receipt Printer
        private void btnSaveUpdate_ReciptPrinter_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPrinterName.Text.Trim()))
            {
                ClsCommon.MsgBox("Information", "Please enter receipt printer name", false);
            }
            else
            {
                XMLData.PrinterName = txtPrinterName.Text.Trim();
                ClsCommon.Update_XMLFile();
                ClsCommon.MsgBox("Information", "Receipt printer setting save", false);

                #region CheckTab
                if (XMLData.DbConnectionString == "")
                {
                    TabControl.SelectedTab = tabDatabaseConnection;
                }
                else if (!XMLData.POSStatus)
                {
                    TabControl.SelectedTab = tabLicenceKey;
                }
                else if (XMLData.PrinterName == "")
                {
                    TabControl.SelectedTab = tabReciptPrinter;
                }
                else if (XMLData.PrinterName == "")
                {
                    TabControl.SelectedTab = tabReciptPrinter;
                }
                else if (XMLData.Scanner == "")
                {
                    TabControl.SelectedTab = tabSerialPort;
                }
                else if (XMLData.LiveToLocalTime == 0)
                {
                    TabControl.SelectedTab = tabDataSyncTime;
                }
                else
                {
                    Application.Exit();
                }
                #endregion
            }
        }
        #endregion

        #region Common Function
        public static bool AllowLogin()
        {
            bool result = false;
            int CurrentNodes = 0;
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var objData = _db.tbl_CounterMaster.Where(x => x.IsActive == true).ToList();
                CurrentNodes = objData.Count;
                int Allow = Convert.ToInt32(XMLData.PriorityCode_BE);
                if (Allow >= CurrentNodes)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public void Key_ActiveNodes()
        {
            try
            {
                if (db_Connection())
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var objData = _db.tbl_SettingMaster.FirstOrDefault();
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
                //MessageBox.Show("Error: Settings - Key_ActiveNodes " + ex.Message.ToString() + "  Source -- " + ex.StackTrace + " LineNumber -- " + ex.LineNumber());
            }
        }
        public static void GetPOSNodes_revers()
        {
            ExceptionLogService _ExceptionLogService = new ExceptionLogService();
            try
            {
                //T5O5-07HG-GDIJ-JYGH
                Random rnd = new Random();
                string Key_ = XMLData.Key.Substring(10, 4);
                //MessageBox.Show("Key_:" + Key_);
                Key_ = ClsCommon.Encryp(Key_);
                int lenth = Convert.ToInt32(Key_.Substring(0, 1));
                string node = Key_.Substring(Key_.Length - lenth, lenth);
                //MessageBox.Show("Node:" + node);
                XMLData.PriorityCode_BE = node;
            }
            catch (Exception ex)
            {
                //ClsCommon.MsgBox("Information","Error: Settings - GetPOSNodes_revers" + ex.Message.ToString());
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "Error: Settings - GetPOSNodes_revers" + ex.StackTrace, ex.LineNumber());
            }
        }
        public bool db_Connection(string Conn)
        {
            try
            {
                MessageBox.Show("Please make sure the database details are same as backend database details");
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "FrmSettings" + ex.StackTrace, ex.LineNumber());
                return false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "FrmSettings" + ex.StackTrace, ex.LineNumber());
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
                txtLiveToLocal.Text = XMLData.LiveToLocalTime.ToString();
                txtLocalToLive.Text = XMLData.LocalToLiveTime.ToString();
                txtOrderSuccessScreen.Text = XMLData.OrderSuccessScreen.ToString();
                cmbScanner.SelectedItem = XMLData.Scanner;
                cmbScale.SelectedItem = XMLData.Scale;
                ToggleSunc.Checked = XMLData.IsSync;
                if (XMLData.IsSync == true)
                    txtSyncTime.Text = XMLData.SyncTime.ToString();
                txtEpxIpAddress.Text = XMLData.EPXTerminalIP;
                txtEpxport.Text = XMLData.EPXTerminalPort;
                toggleEPXService.Checked = XMLData.EPXPaymenrServiceON;

                //Release
                lblCurrentVersion.Text = XMLData.Version;
                lblReleaseUpdatedDate.Text = XMLData.InstallationDate.ToString();

                //DisplayScreenSetting
                cmdPOSScreen.SelectedIndex = Convert.ToInt32(XMLData.POSScreen);
                cmdCustomerScreen.SelectedIndex = Convert.ToInt32(XMLData.CustomerScreen);

                #region Release Info

                if (XMLData.DbConnectionString != "")
                {
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
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
        private void frmSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            //PortOpen_Close(false);
            DeviceRemove();
        }

        #endregion

        #region  Data Sync Time Setting
        private void btnDataSyncTime_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtLiveToLocal.Text.Trim()))
            {
                ClsCommon.MsgBox("Information", "Please enter live to local sync time", false);
            }
            else if (String.IsNullOrEmpty(txtLocalToLive.Text.Trim()))
            {
                ClsCommon.MsgBox("Information", "Please enter local to live sync time", false);
            }
            else
            {
                if (txtLiveToLocal.Text.Trim().All(char.IsDigit) && txtLocalToLive.Text.Trim().All(char.IsDigit))
                {
                    XMLData.LiveToLocalTime = Convert.ToInt32(txtLiveToLocal.Text.Trim());
                    XMLData.LocalToLiveTime = Convert.ToInt32(txtLocalToLive.Text.Trim());
                    XMLData.OrderSuccessScreen = Convert.ToInt32(txtOrderSuccessScreen.Text.Trim());
                    ClsCommon.Update_XMLFile();
                    ClsCommon.MsgBox("Information", "Data sync time setting save", false);
                    //Application.Exit();
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please enter valid time formate time", false);
                }
            }
            #region CheckTab
            if (XMLData.DbConnectionString == "")
            {
                TabControl.SelectedTab = tabDatabaseConnection;
            }
            else if (!XMLData.POSStatus)
            {
                TabControl.SelectedTab = tabLicenceKey;
            }
            else if (XMLData.PrinterName == "")
            {
                TabControl.SelectedTab = tabReciptPrinter;
            }
            else if (XMLData.PrinterName == "")
            {
                TabControl.SelectedTab = tabReciptPrinter;
            }
            else if (XMLData.Scanner == "")
            {
                TabControl.SelectedTab = tabSerialPort;
            }
            else if (XMLData.LiveToLocalTime == 0 || XMLData.LocalToLiveTime == 0)
            {
                TabControl.SelectedTab = tabDataSyncTime;
            }
            else
            {
                Application.Exit();
            }
            #endregion
        }
        #endregion

        #region System Update
        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            btnActive.Text = "Please wait...";
            btnActive.Enabled = false;

            //CheckUpdate();
            btnActive.Text = "Check Update";
            btnActive.Enabled = true;
        }
        #endregion

        #region Others
        private void btnAutoTeste_Click(object sender, EventArgs e)
        {
            if (CheckMyPrinter())
            {
                var printer = new Printer(txtPrinterName.Text, PrinterType.Epson);
                printer.LowPaper();
                string status = printer.GetStatus();
                if (status == "4096")
                {
                    ClsCommon.MsgBox("Information", "Power on the printer.!", false);
                }
                else if (status == "1024" || status == "0")
                {
                    printer.AutoTest();
                    //printer.PartialPaperCut();
                    //printer.OpenDrawer();
                    OpenDrawer();
                    printer.PrintDocument();
                    ClsCommon.MsgBox("Information", "Print and cash drawer will open.!!", false);
                }
            }
            else
            {
                ClsCommon.MsgBox("Information", "Printer not found", false);
            }
        }
        private void btnInicializar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckMyPrinter())
                {
                    var printer = new Printer(txtPrinterName.Text, PrinterType.Epson);
                    printer.LowPaper();
                    //printer.PartialPaperCut();
                    printer.OpenDrawer();
                    printer.PrintDocument();
                    ClsCommon.MsgBox("Information", "Only cash drawer will open.!", false);
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Printer not found", false);
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Error", false);
            }
        }
        public void OpenDrawer()
        {
            var printer = new Printer(txtPrinterName.Text, PrinterType.Epson);
            printer.LowPaper();
            //printer.PartialPaperCut();
            printer.OpenDrawer();
            printer.PrintDocument();
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
                    if (printerName.Equals((txtPrinterName.Text).ToUpper()))
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
        public static bool db_Connection()
        {
            try
            {
                DataContext _dbu = SFPOSDataContext.Create(XMLData.DbConnectionString);
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
            catch (SqlException)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void CheckUpdate()
        {
            try
            {
                if (db_Connection())
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var VersionData = _db.tbl_CounterMaster.Where(x => x.MacAddress == LoginInfo.MacAddress || x.CounterIP == LoginInfo.MacAddress).FirstOrDefault();

                    if (VersionData != null)
                    {
                        if (VersionData.CurrentReleaseID != null)
                        {
                            var ReleaseData = _db.tbl_ReleaseMaster.Where(x => x.ReleaseID > VersionData.CurrentReleaseID).OrderByDescending(x => x.ReleaseID).FirstOrDefault();
                            if (ReleaseData != null)
                            {
                                ClsCommon.MsgBox2("Information", "The updated version is available, You want to update now?", false);
                                if (RetryOrder.UpdateNow)
                                {
                                    DownlaodFiles();
                                }
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "Current version is up to date for this system, Please contact system Administrator if any issues.", false);
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please connect with backend-server for update!", false);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: Settings - Key_ActiveNodes " + ex.Message.ToString() + "  Source -- " + ex.StackTrace + " LineNumber -- " + ex.LineNumber());
            }
        }
        public void DownlaodFiles()
        {
            try
            {
                //Run download file with multiple thread
                string url = "http://192.168.1.124/demo.zip";
                if (!string.IsNullOrEmpty(url))
                {
                    Thread thread = new Thread(() =>
                    {
                        Uri uri = new Uri(url);
                        string fileName = System.IO.Path.GetFileName(uri.AbsolutePath);
                        client.DownloadFileAsync(uri, "C:/ezPOSPro_Dep_Obj" + "/" + fileName);
                    });
                    if (thread.IsAlive == false)
                        thread.Start();
                }
                //ExtractZip();
            }
            catch (Exception ex)
            {
                string exx = ex.Message.ToString();
            }
        }
        #endregion

        private void btnEPXService_Click(object sender, EventArgs e)
        {
            try
            {
                //XMLData.Scanner = cmbScanner.SelectedItem.ToString();
                //XMLData.Scale = cmbScale.SelectedItem.ToString();

                XMLData.EPXPaymenrServiceON = toggleEPXService.Checked ? true : false;
                XMLData.EPXTerminalIP = txtEpxIpAddress.Text.Trim();
                XMLData.EPXTerminalPort = txtEpxport.Text.Trim();

                ClsCommon.Update_ExtraSettingXMLFile();
                ClsCommon.MsgBox("Information", "EPX details are saved", false);
                Application.Exit();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
            }
        }

        #region Display Screens Setting
        public void LoadScreens()
        {
            try
            {
                var ScreenCollection = Screen.AllScreens;
                foreach (Screen ScreenInfo in ScreenCollection)
                {
                    cmdCustomerScreen.Items.Add(ScreenInfo.DeviceName);
                    cmdPOSScreen.Items.Add(ScreenInfo.DeviceName);
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void cmdPOSScreen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdCustomerScreen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnSaveDisplay_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnSaveDiplay_Click(object sender, EventArgs e)
        {
            XMLData.POSScreen = cmdPOSScreen.SelectedIndex;
            XMLData.CustomerScreen = cmdCustomerScreen.SelectedIndex;
            ClsCommon.Update_XMLFile();
            ClsCommon.MsgBox("Information", "Diplay details are saved", false);

            #region CheckTab
            if (XMLData.DbConnectionString == "")
            {
                TabControl.SelectedTab = tabDatabaseConnection;
            }
            else if (!XMLData.POSStatus)
            {
                TabControl.SelectedTab = tabLicenceKey;
            }
            else if (XMLData.PrinterName == "")
            {
                TabControl.SelectedTab = tabReciptPrinter;
            }
            else if (XMLData.PrinterName == "")
            {
                TabControl.SelectedTab = tabReciptPrinter;
            }
            else if (XMLData.Scanner == "")
            {
                TabControl.SelectedTab = tabSerialPort;
            }
            else if (XMLData.LiveToLocalTime == 0)
            {
                TabControl.SelectedTab = tabDataSyncTime;
            }
            else
            {
                Application.Exit();
            }
            #endregion
        }

        private void ToggleAgeVerify_CheckedChanged(object sender, EventArgs e)
        {
            bool isSyncToggle = ToggleSunc.Checked;
            if (isSyncToggle == true)
            {
                txtSyncTime.Enabled = true;
                txtSyncTime.Text = "100";
            }
            else
            {
                txtSyncTime.Enabled = false;
                txtSyncTime.Text = "";
            }
        }

        private void DemoVersionChange()
        {
            // Database server details setting
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            txtPassword.Visible = false;
            txtUserName.Visible = false;
            txtDatabaseName.Visible = false;
            txtDatabaseServerName.Visible = false;
            btnTestConnections.Visible = false;
            btnConnect.Visible = false;


            //Display Screem Setting 
            label27.Location = new System.Drawing.Point(111, 24);
            label29.Location = new System.Drawing.Point(113, 68);
            label30.Location = new System.Drawing.Point(113, 103);
            cmdCustomerScreen.Location = new System.Drawing.Point(276, 61);
            cmdPOSScreen.Location = new System.Drawing.Point(276, 96);
            btnSaveDiplay.Location = new System.Drawing.Point(545, 80);
        }
    }
}
