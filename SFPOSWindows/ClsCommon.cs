using SFPOS.Common;
using SFPOS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SFPOSWindows
{
    public class ClsCommon
    {
        //Vishal
        //**For Local**E:\asp_net2\ezPOSPro-Touch\SFPOSWindows\AppData
        //public static string SqlCeConn = "Data Source=D:\\Projects\\intPOS\\SFPOSWindows\\AppData\\intPOS.sdf;Password ='123456'";
        //public static bool CheckPrinterRoll = true;
        //public static string SQLiteConnFile = "D:\\Projects\\intPOS\\SFPOSWindows\\AppData\\intPOSLite.sqlite3";
        //public static string SQLiteConn = "Data Source=D:\\Projects\\intPOS\\SFPOSWindows\\AppData\\intPOSLite.sqlite3";
        //Check POS Type

        //**For Live**

        public static string SqlCeConn = "Data Source=" + Application.StartupPath + "\\AppData\\intPOS.sdf; Password ='123456'";
        public static bool CheckPrinterRoll = true;

        public static string SQLiteConnFile = Application.StartupPath + "\\AppData\\intPOSLite.sqlite3";
        public static string SQLiteConn = "Data Source=" + Application.StartupPath + "\\AppData\\intPOSLite.sqlite3";

        public static bool IsDemoversion = false;

        //Check POS Type

        public static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }
        public static void Read_XMLFile()
        {
            try
            {
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string path = Path.Combine(exeFolder, "ErrorLog.xml");
                XMLData.InstallationDate = DateTime.Now;
                
                #region Read the Temp File for Demoversion
                FileInfo fi = new FileInfo(Path.Combine(exeFolder, "Temp.txt"));
                if (fi.Exists)
                {
                    IsDemoversion = true;
                    DecryptDemo_File();
                    Update_XMLFile();
                    DeleteDemo_File();
                }
                #endregion

                #region Decrypt_File and Read
                Decrypt_File();
                #endregion

                XDocument xdoc = XDocument.Load(path);
                XElement ezPOSProSettings = xdoc.Element("ezPOSProSettings");

                var _POSInfo = (from POSInfo in ezPOSProSettings.Elements("ezPOSPro")
                                from x in POSInfo.Elements("POSInfo")
                                select new
                                {
                                    POSType = x.Attribute("POSType").Value,
                                    InstallationDate = x.Attribute("InstallationDate").Value ,
                                    Info = x.Attribute("Info").Value,
                                    Version = x.Attribute("Version").Value
                                }).ToList()[0];

                var _Status = (from Status in ezPOSProSettings.Elements("ezPOSPro")
                               from x in Status.Elements("Status")
                               select new
                               {
                                   Type = (x.Attribute("Type").Value),
                                   POSStatus = (x.Attribute("POSStatus").Value),
                                   SyncStatus = (x.Attribute("SyncStatus").Value)
                               }).ToList()[0];

                var _Scanner = (from Scanner in ezPOSProSettings.Elements("ezPOSPro")
                                from x in Scanner.Elements("Scanner")
                                select new
                                {
                                    ScannerType = (x.Attribute("ScannerType").Value),
                                    ScaleType = (x.Attribute("ScaleType").Value),
                                    ScannerInUsed = (x.Attribute("ScannerInUsed").Value),
                                    IsSync = (x.Attribute("IsSync") != null ? x.Attribute("IsSync").Value : true.ToString()),
                                    SyncTime = (x.Attribute("Synctime") != null ? x.Attribute("Synctime").Value : "100")
                                }).ToList()[0];

                var _POSKey = (from POSKey in ezPOSProSettings.Elements("ezPOSPro")
                               from x in POSKey.Elements("POSKey")
                               select new
                               {
                                   Key = (x.Attribute("Key").Value)
                               }).ToList()[0];

                var _ReciptPrinter = (from ReciptPrinter in ezPOSProSettings.Elements("ezPOSPro")
                                      from x in ReciptPrinter.Elements("ReciptPrinter")
                                      select new
                                      {
                                          PrinterName = (x.Attribute("PrinterName").Value),
                                          Disclaimer = (x.Attribute("Disclaimer").Value)
                                      }).ToList()[0];

                var _DbConnet = (from DbConnet in ezPOSProSettings.Elements("ezPOSPro")
                                 from x in DbConnet.Elements("DbConnet")
                                 select new
                                 {
                                     ServerName = (x.Attribute("ServerName").Value),
                                     DbName = (x.Attribute("DbName").Value),
                                     UserName = (x.Attribute("UserName").Value),
                                     Password = (x.Attribute("Password").Value)
                                 }).ToList()[0];

                var _DbConnection = (from DbConnection in ezPOSProSettings.Elements("ezPOSPro")
                                     from x in DbConnection.Elements("DbConnection")
                                     select new
                                     {
                                         DbConnectionString = (x.Attribute("DbConnectionString").Value),
                                         PriorityCode = (x.Attribute("PriorityCode").Value)
                                     }).ToList()[0];

                var _DataSyncTime = (from DataSyncTime in ezPOSProSettings.Elements("ezPOSPro")
                                     from x in DataSyncTime.Elements("DataSyncTime")
                                     select new
                                     {
                                         LiveToLocal = (x.Attribute("LiveToLocal").Value),
                                         LocalToLive = (x.Attribute("LocalToLive").Value),
                                         OrderSuccessScreen = (x.Attribute("OrderSuccessScreen").Value)
                                     }).ToList()[0];

                var _POSVersion = new { Value = "" };
                try
                {
                    if (ezPOSProSettings.Elements("ezPOSPro").Elements().Where(S => S.Name == "intPOSVer").Any())
                    {
                        _POSVersion = (from intPOSVer in ezPOSProSettings.Elements("ezPOSPro")
                                       from x in intPOSVer.Elements("intPOSVer")
                                       select new
                                       {
                                           Value = (x.Attribute("intPOSVer").Value)
                                       }).ToList()[0];
                    }
                }
                catch (Exception e)
                { }

                var _POSScreen = new { POSScreen = "", CustomerScreen="" };
                try
                {
                    if (ezPOSProSettings.Elements("ezPOSPro").Elements().Where(S => S.Name == "ScreenSetting").Any())
                    {
                        _POSScreen = (from ScreenSetting in ezPOSProSettings.Elements("ezPOSPro")
                                       from x in ScreenSetting.Elements("ScreenSetting")
                                       select new
                                       {
                                           POSScreen = (x.Attribute("POSScreen").Value),
                                           CustomerScreen = (x.Attribute("CustomerScreen").Value)
                                       }).ToList()[0];
                    }
                }
                catch (Exception e)
                { }
                // MessageBox.Show("XML read done");
                #region XMLData
                XMLData.POSType = _POSInfo.POSType;
                XMLData.InstallationDate = Convert.ToDateTime(_POSInfo.InstallationDate);
                XMLData.Info = _POSInfo.Info;
                XMLData.Version = _POSInfo.Version;
                XMLData.Type = Convert.ToInt32(_Status.Type);
                //XMLData.Type = 1;
                XMLData.EPXShow = 0;
                if (Convert.ToString(_POSVersion.Value) != "" && Convert.ToString(_POSVersion.Value) != string.Empty && Convert.ToString(_POSVersion.Value).Length == 1)
                    XMLData.IsDemoVersion = Convert.ToInt32(_POSVersion.Value);
                else
                    XMLData.IsDemoVersion = 0;
                //XMLData.IsDemoVersion = 0;
                XMLData.POSStatus = Convert.ToBoolean(_Status.POSStatus);
                XMLData.SyncStatus = Convert.ToBoolean(_Status.SyncStatus);
                XMLData.Scanner = _Scanner.ScannerType;
                XMLData.Scale = _Scanner.ScaleType;
                XMLData.IsSync = Convert.ToBoolean(_Scanner.IsSync);
                if (XMLData.IsSync == true)
                    XMLData.SyncTime = Convert.ToInt32(_Scanner.SyncTime);
                XMLData.Key = _POSKey.Key;
                XMLData.PrinterName = _ReciptPrinter.PrinterName;
                XMLData.Disclaimer = _ReciptPrinter.Disclaimer;
                XMLData.ServerName = _DbConnet.ServerName;
                XMLData.DbName = _DbConnet.DbName;
                XMLData.UserName = _DbConnet.UserName;
                XMLData.Password = _DbConnet.Password;
                XMLData.DbConnectionString = _DbConnection.DbConnectionString;
                // XMLData.DbConnectionString = "Data Source =tbs24; Initial Catalog = intPOS_Dev;User ID=sa; Pwd=sa@123; Integrated Security=False";
                // XMLData.DbConnectionString = "Data Source=\\SQLEXPRESS;Initial Catalog=C:\\Prabhu\\POS System\\Master Data\\intPOS.mdf;Integrated Security=true";
                //XMLData.POSStatus = false;
                //XMLData.IsDemoVersion = 1;
                XMLData.PriorityCode = "";
                XMLData.LiveToLocalTime = !String.IsNullOrEmpty(_DataSyncTime.LiveToLocal) ? Convert.ToInt32(_DataSyncTime.LiveToLocal) : 5;
                if (XMLData.LiveToLocalTime == 0) { XMLData.LiveToLocalTime = 5; }
                XMLData.LocalToLiveTime = !String.IsNullOrEmpty(_DataSyncTime.LocalToLive) ? Convert.ToInt32(_DataSyncTime.LocalToLive) : 1;
                if (XMLData.LocalToLiveTime == 0) { XMLData.LiveToLocalTime = 1; }
                XMLData.OrderSuccessScreen = !String.IsNullOrEmpty(_DataSyncTime.OrderSuccessScreen) ? Convert.ToInt32(_DataSyncTime.OrderSuccessScreen) : 20;
                if (XMLData.OrderSuccessScreen == 0) { XMLData.OrderSuccessScreen = 20; }

                XMLData.POSScreen = Convert.ToUInt16(_POSScreen.POSScreen);
                XMLData.CustomerScreen = Convert.ToInt16(_POSScreen.CustomerScreen);
                #endregion

                LoginInfo.POSStatus = Convert.ToBoolean(_Status.POSStatus);

                #region Delete Decrypt_File
                Delete_File();
                #endregion

                //#region Read the Temp File for Demoversion
                //FileInfo fi = new FileInfo(Path.Combine(exeFolder, "Temp.txt"));
                //if (fi.Exists)
                //{
                //    IsDemoversion = true;
                //    DecryptDemo_File();
                //    Update_XMLFile();
                //    DeleteDemo_File();
                //}
                //#endregion
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message.ToString() + " Line " + e.LineNumber());
                //MessageBox.Show("XML Read Error:  " + e.Message.ToString());
                Delete_File();
                XMLData.IsDemoVersion = 0;
            }
        }

        public static void Update_XMLFile()
        {
            try
            {
                string installationPath = Application.StartupPath;
                XDocument doc =
                             new XDocument(
                               new XElement("ezPOSProSettings",
                                 new XElement("ezPOSPro",
                                     new XElement("POSInfo", new XAttribute("POSType", XMLData.POSType), new XAttribute("InstallationDate", XMLData.InstallationDate), new XAttribute("Info", XMLData.Info), new XAttribute("Version", "2.2.1")),
                                     new XElement("Status", new XAttribute("Type", XMLData.Type), new XAttribute("POSStatus", XMLData.POSStatus), new XAttribute("SyncStatus", XMLData.SyncStatus)),
                                     new XElement("Scanner", new XAttribute("ScannerType", XMLData.Scanner), new XAttribute("ScaleType", XMLData.Scale), new XAttribute("ScannerInUsed", XMLData.ScannerInUsed), new XAttribute("IsSync", XMLData.IsSync), new XAttribute("Synctime", XMLData.SyncTime)),
                                     new XElement("POSKey", new XAttribute("Key", XMLData.Key)),
                                     new XElement("ReciptPrinter", new XAttribute("PrinterName", XMLData.PrinterName), new XAttribute("Disclaimer", XMLData.Disclaimer)),
                                     new XElement("DbConnet", new XAttribute("ServerName", XMLData.ServerName), new XAttribute("DbName", XMLData.DbName), new XAttribute("UserName", XMLData.UserName), new XAttribute("Password", XMLData.Password)),
                                     new XElement("DbConnection", new XAttribute("DbConnectionString", XMLData.DbConnectionString), new XAttribute("PriorityCode", "")),
                                     new XElement("DataSyncTime", new XAttribute("LiveToLocal", XMLData.LiveToLocalTime), new XAttribute("LocalToLive", XMLData.LocalToLiveTime), new XAttribute("OrderSuccessScreen", XMLData.OrderSuccessScreen)),
                                     new XElement("intPOSVer", new XAttribute("intPOSVer", XMLData.IsDemoVersion)),
                                     new XElement("ScreenSetting", new XAttribute("POSScreen", XMLData.POSScreen), new XAttribute("CustomerScreen", XMLData.CustomerScreen))
                                 )));

                var currentDirectory = Path.Combine(installationPath);
                bool IsExists = System.IO.Directory.Exists(currentDirectory);
                if (!IsExists)
                {
                    System.IO.Directory.CreateDirectory(currentDirectory);
                }
                var fileName = Path.Combine(currentDirectory, "ErrorLog.xml");
                doc.Save(fileName);

                #region Encrypt_File and Delete
                Encrypt_File();
                Delete_File();
                #endregion
            }
            catch (Exception e)
            {
                Delete_File();
                MessageBox.Show("XML Update Error:  " + e.ToString());
            }
        }

        public static void Read_ExtraSettingXMLFile()
        {
            try
            {
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string path = Path.Combine(exeFolder, "ExtraPOSSettings.xml");

                if (File.Exists(path))
                {
                    XDocument xdoc = XDocument.Load(path);
                    XElement ezPOSProExtraSettings = xdoc.Element("ExtraPOSSettings");

                    var _EpxService = new { Value = "" };
                    if (ezPOSProExtraSettings.Elements("intPOS").Elements().Where(S => S.Name == "EpxService").Any())
                    {
                        _EpxService = (from SignReciptPrinter in ezPOSProExtraSettings.Elements("intPOS")
                                       from x in SignReciptPrinter.Elements("EpxService")
                                       select new
                                       {
                                           Value = (x.Attribute("Value").Value)
                                       }).ToList()[0];
                    }

                    var _EPXTerminalIP = new { Value = "" };
                    if (ezPOSProExtraSettings.Elements("intPOS").Elements().Where(S => S.Name == "EPXTerminalIP").Any())
                    {
                        _EPXTerminalIP = (from SignReciptPrinter in ezPOSProExtraSettings.Elements("intPOS")
                                          from x in SignReciptPrinter.Elements("EPXTerminalIP")
                                          select new
                                          {
                                              Value = (x.Attribute("Value").Value)
                                          }).ToList()[0];
                    }

                    var _EPXTerminalPort = new { Value = "" };
                    if (ezPOSProExtraSettings.Elements("intPOS").Elements().Where(S => S.Name == "EPXTerminalPort").Any())
                    {
                        _EPXTerminalPort = (from SignReciptPrinter in ezPOSProExtraSettings.Elements("intPOS")
                                            from x in SignReciptPrinter.Elements("EPXTerminalPort")
                                            select new
                                            {
                                                Value = (x.Attribute("Value").Value)
                                            }).ToList()[0];
                    }

                    #region XMLData
                    XMLData.EPXPaymenrServiceON = !string.IsNullOrEmpty(Convert.ToString(_EpxService.Value)) && Convert.ToString(_EpxService.Value).ToLower() == "true" ? true : false;
                    XMLData.EPXTerminalIP = _EPXTerminalIP.Value;
                    XMLData.EPXTerminalPort = _EPXTerminalPort.Value;

                    #endregion
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("XML Read Error:  " + e.Message.ToString());

            }
        }

        public static void Update_ExtraSettingXMLFile()
        {
            try
            {
                string installationPath = Application.StartupPath;
                XDocument doc =
                             new XDocument(
                               new XElement("ExtraPOSSettings",
                                 new XElement("intPOS",
                                     new XElement("EpxService", new XAttribute("Value", XMLData.EPXPaymenrServiceON.ToString())),
                                     new XElement("EPXTerminalIP", new XAttribute("Value", XMLData.EPXTerminalIP)),
                                     new XElement("EPXTerminalPort", new XAttribute("Value", XMLData.EPXTerminalPort)),
                                     new XElement("intPOSVer", new XAttribute("Value", XMLData.IsDemoVersion))
                                 )));

                var currentDirectory = Path.Combine(installationPath);
                bool IsExists = System.IO.Directory.Exists(currentDirectory);
                if (!IsExists)
                {
                    System.IO.Directory.CreateDirectory(currentDirectory);
                }
                var fileName = Path.Combine(currentDirectory, "ExtraPOSSettings.xml");
                doc.Save(fileName);
            }
            catch (Exception e)
            {
                //MessageBox.Show("XML Update Error:  " + e.ToString());
            }
        }

        public static bool CheckConnection()
        {
            //MessageBox.Show("CheckConnection IN");
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
            //MessageBox.Show("CheckConnection Status:" + Status);
            return Status;
        }

        public static bool db_Connection()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var Empl = _db.tbl_EmployeeMaster.FirstOrDefault();
                //MessageBox.Show(Empl.FirstName + Empl.LastName + Empl.StoreID);

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void ResetStaticValues()
        {
            AgeVerifidInfo.AgeVerified = false;
            AgeVerifidInfo.AgeChecked = false;
            LoginInfo.TotalVoidAmount = 0;
            LoginInfo.CashierAgeVerified = false;
            LoginInfo.TaxCarry = false;
            OrderInfo.CashAmt = 0;
            OrderInfo.Change = 0;
            OrderInfo.IsOrder = false;
            OrderInfo.FSTotal = 0;
            OrderInfo.remainingFSAmt = 0;
            OrderInfo.IsFSClicked = false;
            OrderInfo.CheckAmt = 0;
            OrderInfo.CreditAmt = 0;
            CouponInfo.CouponCode = "";
            CouponInfo.isCoupon = false;
            CouponInfo.Discount = 0;
            CouponInfo.DiscAmt = 0;
            OrderInfo.IsRefundOrder = false;
            CustomerInfo.IsCustomerVerfiedForTC = false;
        }

        public static void MsgBox(string Title, string Msg, bool IsCancel)
        {
            CustomMessageBox _CustomMessageBox = new CustomMessageBox();
            _CustomMessageBox.lblTitle.Text = Title;
            _CustomMessageBox.lblMsg.Text = Msg;
            _CustomMessageBox.IsCancel = IsCancel;
            _CustomMessageBox.btnRetry.Visible = false;

            Screen[] screens = Screen.AllScreens;
            if (screens.Count<Screen>() > 1)
            {
                if (screens[1].Primary == true)
                {
                    setFormLocation(_CustomMessageBox, screens[1]);
                }
                else
                {
                    setFormLocation(_CustomMessageBox, screens[0]);
                }
            }
            else
            {
                setFormLocation(_CustomMessageBox, screens[0]);
            }
            _CustomMessageBox.ShowDialog();
            //_CustomMessageBox.Focus();
        }

        public static void MsgBox2(string Title, string Msg, bool IsCancel)
        {
            CustomMessageBox2 _CustomMessageBox2 = new CustomMessageBox2();
            _CustomMessageBox2.lblTitle.Text = Title;
            _CustomMessageBox2.lblMsg.Text = Msg;

            Screen[] screens = Screen.AllScreens;
            setFormLocation(_CustomMessageBox2, screens[0]);
            _CustomMessageBox2.ShowDialog();
            //_CustomMessageBox.Focus();
        }

        public static void MsgBoxForVerified(string Title, string Msg1, string Msg2, string Msg3, bool IsCancel)
        {
            SubmittingMessageBox _SubmittingMessageBox = new SubmittingMessageBox();
            _SubmittingMessageBox.lblTitle.Text = Title;
            _SubmittingMessageBox.lblMsg.Text = Msg1;
            _SubmittingMessageBox.lbl2.Text = Msg2;
            _SubmittingMessageBox.checVarified.Text = Msg3;
            _SubmittingMessageBox.IsCancel = IsCancel;
            _SubmittingMessageBox.ShowDialog();
        }
        private static void setFormLocation(Form form, Screen screen)
        {
            // first method
            Rectangle bounds = screen.Bounds;
            form.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);

            // second method
            //Point location = screen.Bounds.Location;
            //Size size = screen.Bounds.Size;

            //form.Left = location.X;
            //form.Top = location.Y;
            //form.Width = size.Width;
            //form.Height = size.Height;
        }

        public static void RetryMsg(string Title, string Msg, bool IsCancel)
        {
            CustomMessageBox _CustomMessageBox = new CustomMessageBox();
            _CustomMessageBox.lblTitle.Text = Title;
            _CustomMessageBox.lblMsg.Text = Msg;
            _CustomMessageBox.IsCancel = IsCancel;
            _CustomMessageBox.btnRetry.Visible = true;

            //_CustomMessageBox.StartPosition = FormStartPosition.CenterParent;

            Screen[] screens = Screen.AllScreens;
            setFormLocation(_CustomMessageBox, screens[0]);
            _CustomMessageBox.ShowDialog();
        }

        public static void GetPhysical_IP_Adress()
        {
            try
            {
                //IP_Adres
                string hostName = Dns.GetHostName();
                LoginInfo.CounterIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                //Physical_Adress 
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                string sMacAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == string.Empty)// only return MAC Address from first card  
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                        LoginInfo.MacAddress = sMacAddress;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static DataTable LinqToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {
                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        #region Decrypt-Encrypt Key
        public static string Decrypt(string CodeKey)
        {
            string _CodeKey = "";
            string characters = CodeKey;
            char[] array = characters.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                string Char = array[i].ToString();
                switch (Char)
                {
                    #region Check 
                    case "0":
                        Char = "H";
                        break;
                    case "1":
                        Char = "G";
                        break;
                    case "2":
                        Char = "J";
                        break;
                    case "3":
                        Char = "B";
                        break;
                    case "4":
                        Char = "C";
                        break;
                    case "5":
                        Char = "N";
                        break;
                    case "6":
                        Char = "F";
                        break;
                    case "7":
                        Char = "K";
                        break;
                    case "8":
                        Char = "O";
                        break;
                    case "9":
                        Char = "E";
                        break;
                    case "A":
                        Char = "P";
                        break;
                    case "B":
                        Char = "Y";
                        break;
                    case "C":
                        Char = "8";
                        break;
                    case "D":
                        Char = "4";
                        break;
                    case "E":
                        Char = "W";
                        break;
                    case "F":
                        Char = "U";
                        break;
                    case "G":
                        Char = "3";
                        break;
                    case "H":
                        Char = "I";
                        break;
                    case "I":
                        Char = "A";
                        break;
                    case "J":
                        Char = "D";
                        break;
                    case "K":
                        Char = "7";
                        break;
                    case "L":
                        Char = "0";
                        break;
                    case "M":
                        Char = "T";
                        break;
                    case "N":
                        Char = "9";
                        break;
                    case "O":
                        Char = "S";
                        break;
                    case "P":
                        Char = "2";
                        break;
                    case "Q":
                        Char = "Z";
                        break;
                    case "R":
                        Char = "X";
                        break;
                    case "S":
                        Char = "5";
                        break;
                    case "T":
                        Char = "1";
                        break;
                    case "U":
                        Char = "Q";
                        break;
                    case "V":
                        Char = "R";
                        break;
                    case "W":
                        Char = "6";
                        break;
                    case "X":
                        Char = "V";
                        break;
                    case "Y":
                        Char = "M";
                        break;
                    case "Z":
                        Char = "L";
                        break;
                    case "-":
                        Char = "-";
                        break;
                    default:
                        Char = "*";
                        break;
                        #endregion
                }
                _CodeKey = _CodeKey + Char;
            }
            return _CodeKey;
        }

        public static string Encryp(string CodeKey)
        {
            string _CodeKey = "";
            string characters = CodeKey;
            char[] array = characters.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                string Char = array[i].ToString();
                switch (Char)
                {
                    #region Check 
                    case "H":
                        Char = "0";
                        break;
                    case "G":
                        Char = "1";
                        break;
                    case "J":
                        Char = "2";
                        break;
                    case "B":
                        Char = "3";
                        break;
                    case "C":
                        Char = "4";
                        break;
                    case "N":
                        Char = "5";
                        break;
                    case "F":
                        Char = "6";
                        break;
                    case "K":
                        Char = "7";
                        break;
                    case "O":
                        Char = "8";
                        break;
                    case "E":
                        Char = "9";
                        break;
                    case "P":
                        Char = "A";
                        break;
                    case "Y":
                        Char = "B";
                        break;
                    case "8":
                        Char = "C";
                        break;
                    case "4":
                        Char = "D";
                        break;
                    case "W":
                        Char = "E";
                        break;
                    case "U":
                        Char = "F";
                        break;
                    case "3":
                        Char = "G";
                        break;
                    case "I":
                        Char = "H";
                        break;
                    case "A":
                        Char = "I";
                        break;
                    case "D":
                        Char = "J";
                        break;
                    case "7":
                        Char = "K";
                        break;
                    case "0":
                        Char = "L";
                        break;
                    case "T":
                        Char = "M";
                        break;
                    case "9":
                        Char = "N";
                        break;
                    case "S":
                        Char = "O";
                        break;
                    case "2":
                        Char = "P";
                        break;
                    case "Z":
                        Char = "Q";
                        break;
                    case "X":
                        Char = "R";
                        break;
                    case "5":
                        Char = "S";
                        break;
                    case "1":
                        Char = "T";
                        break;
                    case "Q":
                        Char = "U";
                        break;
                    case "R":
                        Char = "V";
                        break;
                    case "6":
                        Char = "W";
                        break;
                    case "V":
                        Char = "X";
                        break;
                    case "M":
                        Char = "Y";
                        break;
                    case "L":
                        Char = "Z";
                        break;
                    case "-":
                        Char = "-";
                        break;
                    default:
                        Char = "*";
                        break;
                        #endregion
                }
                _CodeKey = _CodeKey + Char;
            }
            return _CodeKey;
        }
        #endregion

        #region Encrypt-Decrypt File
        private static void Encrypt_File()
        {
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string inputFilePath = Path.Combine(exeFolder, "ErrorLog.xml");
            string outputfilePath = Path.Combine(exeFolder, "ErrorRobotDelete.xml");

            string EncryptionKey = "U0ZBcHA6U0ZAQXBw";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }


        }

        private static void DeleteDemo_File()
        {
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string fileName = Path.Combine(exeFolder, "Temp.txt");
            if ((System.IO.File.Exists(fileName)))
            {
                System.IO.File.Delete(fileName);
            }
        }

        private static void DecryptDemo_File()
        {
            //string exeFolder = Path.GetDirectoryName(Application.StartupPath);
            string inputFilePath = Path.Combine(Application.StartupPath, "Temp.txt");
            string[] DataFile = File.ReadAllLines(inputFilePath).ToArray();

            var currentDirectory = Path.Combine(Application.StartupPath);
            bool IsExists = System.IO.Directory.Exists(currentDirectory);
            if (DataFile[0].ToUpper().Contains("DEMO"))
            {
                string SQLLocalDbName = "MSSQLLocalDB";
                var fileNamedb = Path.Combine(Application.StartupPath, "AppData\\intPOS.mdf");
                fileNamedb = fileNamedb.Replace("\\\\", "\\");
                fileNamedb = fileNamedb.Replace("Frontend", "Backend");

                XMLData.IsDemoVersion = 1;
                XMLData.DbConnectionString = "Data Source=(LocalDB)\\" + SQLLocalDbName + ";AttachDbFilename=" + fileNamedb + ";Integrated Security=True;Connect Timeout=30";
            }
            else
            {
                XMLData.IsDemoVersion = 0;
            }
            var ComponentType = Path.Combine(Application.StartupPath);
            if (ComponentType.Contains("Backend"))
            {
                XMLData.POSType = "intPOS-Backend";
                XMLData.Type = 1;
            }
            else
            {
                XMLData.POSType = "intPOS-Frontend";
                XMLData.Type = 2;
            }
        }
        private static void DecryptDemo_File_X()
        {
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string inputFilePath = Path.Combine(exeFolder, "ConfigXMLDataEvl.xml");

            string outputfilePath = Path.Combine(exeFolder, "ErrorLog.xml");

            string EncryptionKey = "U0ZBcHA6U0ZAQXBw";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }

        }

        private static void Decrypt_File()
        {
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string inputFilePath = Path.Combine(exeFolder, "ErrorRobotDelete.xml");

            string outputfilePath = Path.Combine(exeFolder, "ErrorLog.xml");

            string EncryptionKey = "U0ZBcHA6U0ZAQXBw";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        private static void Delete_File()
        {
            string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string fileName = Path.Combine(exeFolder, "ErrorLog.xml");
            if ((System.IO.File.Exists(fileName)))
            {
                System.IO.File.Delete(fileName);
            }
        }
        #endregion

        #region Encrypt-Decrypt String
        private string Encrypt_String(string clearText)
        {
            string EncryptionKey = "U0ZBcHA6U0ZAQXBw";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private string Decrypt_String(string cipherText)
        {
            string EncryptionKey = "U0ZBcHA6U0ZAQXBw";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion

        #region Get Full UPCCode
        public static string GetFullUPCCode(string UPCCode)
        {
            if (UPCCode != "")
            {
                int Count = UPCCode.Length;
                if (Count < 13)
                {
                    Count = 13 - Count;
                    for (int i = 0; i < Count; i++)
                    {
                        UPCCode = "0" + UPCCode;
                    }
                }
            }
            return UPCCode;
        }
        #endregion

        #region EPXCreditandFoodStamp
        public static string EPXSubmitCreditAndFoodStamp(string amount, string tranType, string transNo)
        {
            try
            {
                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                string IP = XMLData.EPXTerminalIP;
                string Port = XMLData.EPXTerminalPort;
                string Amount = amount;

                DateTime myDate = DateTime.Now;
                int batchId = Convert.ToInt32(myDate.Year.ToString() + myDate.Month.ToString() + myDate.Day.ToString());
                //clientSocket.Connect(IP, Convert.ToInt32(Port));
                //lblMsg.Text = "Client Socket Program - Server Connected ...";

                byte[] bytes = new byte[30000];
                //IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = IPAddress.Parse(IP); //host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Convert.ToInt32(Port));

                // Create a TCP/IP  socket.    
                Socket socket = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(remoteEP);

                //Console.WriteLine("Socket connected to {0}",
                //    socket.RemoteEndPoint.ToString());

                // MessageBox.Show("Socket connected to " + socket.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.    
                var str = "<DETAIL><TRAN_TYPE>" + tranType + "</TRAN_TYPE><AMOUNT>" + amount + "</AMOUNT><BATCH_ID>" + batchId + "</BATCH_ID><TRAN_NBR>" + transNo + "</TRAN_NBR></DETAIL>";
                byte[] msg = Encoding.ASCII.GetBytes("<DETAIL><TRAN_TYPE>" + tranType + "</TRAN_TYPE><AMOUNT>" + amount + "</AMOUNT><BATCH_ID>" + batchId + "</BATCH_ID><TRAN_NBR>" + transNo + "</TRAN_NBR></DETAIL>");

                tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                epx.UserId = Convert.ToInt32(LoginInfo.UserId);
                epx.StoreID = Convert.ToInt32(LoginInfo.StoreID);
                epx.RequestSend = str;
                epx.Amount = Convert.ToDecimal(amount);
                epx.TransactionNo = transNo;
                _db.tbl_OrderEPXLog.Add(epx);
                _db.SaveChanges();
                // Send the data through the socket.    
                int bytesSent = socket.Send(msg);

                // Receive the response from the remote device.    
                int bytesRec = socket.Receive(bytes);


                string resptext = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                //socket.re
                //MessageBox.Show("Echoed test = " + resptext);

                // string a = System.Net.Http.Headers.HttpHeaders.Remove(resptext);
                //string saaa = "";
                //string HTTPHeaderDelimiter = "\r\n\r\n";
                //if (resptext.IndexOf("HTTP/1.1 200 OK") > -1)
                //{
                //    saaa = resptext.Substring(resptext.IndexOf(HTTPHeaderDelimiter) + HTTPHeaderDelimiter.Length).Trim();

                //    using (StreamWriter writetext = new StreamWriter("C:\\EPXDemo\\Response.txt", true))
                //    {
                //        writetext.WriteLine("========== Without Header ===========");
                //        writetext.WriteLine(Environment.NewLine);
                //        writetext.WriteLine(saaa);
                //    }

                //    //    var xml = XDocument.Parse(saaa);
                //    //    var element = xml.GetElementByName("RESPONSE", "AUTH_RESP");
                //    //    if (element != null)
                //    //    {
                //    //        if (!string.IsNullOrEmpty(element.Value) && element.Value.Trim() == "00")
                //    //        {
                //    //            MessageBox.Show("Successful Transaction");
                //    //        }
                //    //        else
                //    //        {
                //    //            var element2 = xml.GetElementByName("RESPONSE", "AUTH_RESP_TEXT");
                //    //            if (element2 != null)
                //    //            {
                //    //                MessageBox.Show(element.Value + " - " + element2.Value);
                //    //            }
                //    //        }
                //    //    }
                //    //    else
                //    //    {
                //    //        var element2 = xml.GetElementByName("RESPONSE", "AUTH_RESP_TEXT");
                //    //        if (element2 != null)
                //    //        {
                //    //            MessageBox.Show(element.Value + " - " + element2.Value);
                //    //        }
                //    //    }

                //}
                //else
                //{
                //    return;
                //}

                //Console.WriteLine("Echoed test = {0}",
                //    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.    
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return resptext;

            }
            catch (ArgumentNullException ane)
            {
                return "ArgumentNullException : " + ane.ToString();
            }
            catch (SocketException se)
            {
                return "SocketException : " + se.ToString();

            }
            catch (Exception ex)
            {
                return "Unexpected exception : " + ex.ToString();
            }
        }
        #endregion

        public static void SetScreen_X(Form frm, int ScreenType)
        {
            //if (ScreenType != 0)
            //if (frm.Name.ToLower() != "cutomer")
            {
                frm.Opacity = 0;
                Point point = frm.Location;
                Size size = frm.Size;
                frm.Location = Screen.AllScreens[ScreenType].Bounds.Location;
                frm.WindowState = FormWindowState.Normal;
                frm.Bounds = Screen.AllScreens[ScreenType].Bounds;
                //if (frm.Name.ToLower() != "cutomer")
                //{
                // frm.Size = size;
                // int ciWidth = 0;
                // int ciHeight = 0;
                // if (frm.Location.X < 0)
                // {
                // ciWidth = (0 - ((Screen.AllScreens[ScreenType].WorkingArea.Width + size.Width) / 2));
                // ciHeight = (Screen.AllScreens[ScreenType].WorkingArea.Height - size.Height) / 2;
                // }
                // else
                // {
                // ciWidth = (Screen.AllScreens[ScreenType].WorkingArea.Width - size.Width) / 2;
                // ciHeight = (Screen.AllScreens[ScreenType].WorkingArea.Height - size.Height) / 2;
                // }



                // if (frm.Height == Screen.AllScreens[ScreenType].Bounds.Height)
                // ciHeight = 0;



                // if (frm.Location.X > 0)
                // frm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width + ciWidth), ciHeight);
                // else
                // frm.Location = new Point((ciWidth), ciHeight);
                //}
                //else
                {
                    frm.Size = size;
                    int ciWidth = 0;
                    int ciHeight = 0;
                    if (frm.Location.X < 0)
                    {
                        ciWidth = (0 - ((Screen.AllScreens[ScreenType].WorkingArea.Width + size.Width) / 2));
                        ciHeight = (Screen.AllScreens[ScreenType].WorkingArea.Height - size.Height) / 2;
                    }
                    else
                    {
                        ciWidth = (Screen.AllScreens[ScreenType].WorkingArea.Width - size.Width) / 2;
                        ciHeight = (Screen.AllScreens[ScreenType].WorkingArea.Height - size.Height) / 2;
                    }



                    if (frm.Name.ToLower() == "cutomer" || frm.Name.ToLower() == "frmorderscanner_p8")
                    {
                        frm.Width = Screen.AllScreens[ScreenType].WorkingArea.Width;
                        frm.Height = Screen.AllScreens[ScreenType].WorkingArea.Height;
                        frm.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        if (size.Width > Screen.AllScreens[ScreenType].WorkingArea.Width)
                        {
                            ciWidth = ciWidth < 0 ? (0 - Screen.AllScreens[ScreenType].WorkingArea.Width) : Screen.AllScreens[ScreenType].WorkingArea.Width;
                            frm.Width = Screen.AllScreens[ScreenType].WorkingArea.Width;
                        }
                        if (size.Height > Screen.AllScreens[ScreenType].WorkingArea.Height)
                        {
                            ciHeight = 0;
                            frm.Height = Screen.AllScreens[ScreenType].WorkingArea.Height;
                        }
                    }



                    if (frm.Height == Screen.AllScreens[ScreenType].Bounds.Height)
                        ciHeight = 0;



                    if (frm.Location.X > 0)
                        frm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width + ciWidth), ciHeight);
                    else
                        frm.Location = new Point((ciWidth), ciHeight);
                }
            }

        }



        public static void SetScreen(Form frm, int ScreenType)
        {
            Point point = frm.Location;
            Size size = frm.Size;
            frm.WindowState = FormWindowState.Normal;
            frm.Location = Screen.AllScreens[ScreenType].Bounds.Location;
            //frm.Bounds = Screen.AllScreens[ScreenType].Bounds;



            frm.Size = size;
            int ciWidth = 0;
            int ciHeight = 0;
            if (frm.Location.X < 0)
            {
                ciWidth = (0 - ((Screen.AllScreens[ScreenType].WorkingArea.Width + size.Width) / 2));
                ciHeight = (Screen.AllScreens[ScreenType].WorkingArea.Height - size.Height) / 2;
            }
            else
            {
                ciWidth = (Screen.AllScreens[ScreenType].WorkingArea.Width - size.Width) / 2;
                ciHeight = (Screen.AllScreens[ScreenType].WorkingArea.Height - size.Height) / 2;
            }



            if (frm.Name.ToLower() == "cutomer" || frm.Name.ToLower() == "frmorderscanner_p8")
            {
                // frm.Width = Screen.AllScreens[ScreenType].WorkingArea.Width;
                //frm.Height = Screen.AllScreens[ScreenType].WorkingArea.Height;
                frm.WindowState = FormWindowState.Maximized;
            }
            else
            {
                if (size.Width > Screen.AllScreens[ScreenType].WorkingArea.Width)
                {
                    ciWidth = ciWidth < 0 ? (0 - Screen.AllScreens[ScreenType].WorkingArea.Width) : Screen.AllScreens[ScreenType].WorkingArea.Width;
                    frm.Width = Screen.AllScreens[ScreenType].WorkingArea.Width;
                }
                if (size.Height > Screen.AllScreens[ScreenType].WorkingArea.Height)
                {
                    ciHeight = 0;
                    frm.Height = Screen.AllScreens[ScreenType].WorkingArea.Height;
                }



                if (frm.Location.X > 0)
                    frm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width + ciWidth), ciHeight);
                else
                    frm.Location = new Point((ciWidth), ciHeight);
            }
        }
    }
}

