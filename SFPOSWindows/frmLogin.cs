using MetroFramework.Controls;
using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Frontend;
using SFPOSWindows.Metro_Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows
{
    public partial class frmLogin : MetroForm
    {
        ErrorProvider ep = new ErrorProvider();
        LoginMasterModel lstLoginMasterModel = new LoginMasterModel();
        LoginService _LoginService = new LoginService();
        public static SqlCeDataAdapter DataAdapter = null;
        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        Control ctrl;
        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl is MetroTextBox)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    this.SelectNextControl(ctrl, true, true, true, true);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.SelectNextControl(ctrl, false, true, true, true);
                }
                else return;
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SelectNextControl(ctrl, true, true, true, true);
                }
                else if (e.KeyCode == Keys.Up && e.Control)
                {
                    this.SelectNextControl(ctrl, false, true, true, true);
                }
                else return;
            }
        }

        public Boolean CheckRolePermission(long RoleID)
        {
            Boolean BackEndAccess = false;
            try
            {
                List<tbl_RolePermission> _tbl_RoleMaster = new List<tbl_RolePermission>();

                #region 
                if (XMLData.Type == 1)
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    _tbl_RoleMaster = _db.tbl_RolePermission.Where(x => x.RoleID == RoleID && x.MenuID == 14).ToList();
                }
                else
                {
                    if (LoginInfo.Connections)
                    {
                        DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                        _tbl_RoleMaster = _db.tbl_RolePermission.Where(x => x.RoleID == RoleID && x.MenuID == 14).ToList();
                    }
                    else
                    {
                        conn.Open();
                        DataTable dt = new DataTable();
                        string query = "SELECT MenuID,ViewRecords FROM tbl_RolePermission WHERE RoleID=@RoleID and MenuID=@MenuID";
                        DataAdapter = new SqlCeDataAdapter(query, conn);
                        DataAdapter.SelectCommand.Parameters.AddWithValue("@RoleID", RoleID);
                        DataAdapter.SelectCommand.Parameters.AddWithValue("@MenuID", 14);
                        DataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                tbl_RolePermission obj = new tbl_RolePermission();
                                obj.ViewRecords = Convert.ToBoolean(dt.Rows[0]["ViewRecords"].ToString());
                                obj.MenuID = int.Parse(dt.Rows[0]["MenuID"].ToString());
                                _tbl_RoleMaster.Add(obj);
                            }
                        }
                        dt.Dispose();
                        conn.Close();
                    }
                }
                #endregion

                #region Check Back End Accesss
                foreach (var property in _tbl_RoleMaster)
                {
                    if (property.ViewRecords == true && (property.MenuID).ToString() == "14")
                    {
                        BackEndAccess = true;
                    }

                }
                #endregion

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmLogin + ex.StackTrace, ex.LineNumber());
            }
            return BackEndAccess;

        }


        public void getRolePermission(long RoleID)
        {
            try
            {
                string MenuID;
                int RoleActive = 1;
                List<tbl_RolePermission> _tbl_RoleMaster = new List<tbl_RolePermission>();

                #region 
                if (XMLData.Type == 1)
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    _tbl_RoleMaster = _db.tbl_RolePermission.Where(x => x.RoleID == RoleID).ToList();
                }
                else
                {
                    if (LoginInfo.Connections)
                    {
                        DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                        _tbl_RoleMaster = _db.tbl_RolePermission.Where(x => x.RoleID == RoleID).ToList();
                    }
                    else
                    {
                        conn.Open();
                        DataTable dt = new DataTable();
                        string query = "SELECT MenuID,ViewRecords FROM tbl_RolePermission WHERE RoleID=@RoleID";
                        DataAdapter = new SqlCeDataAdapter(query, conn);
                        DataAdapter.SelectCommand.Parameters.AddWithValue("@RoleID", RoleID);
                        DataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                tbl_RolePermission obj = new tbl_RolePermission();
                                obj.ViewRecords = Convert.ToBoolean(dt.Rows[0]["ViewRecords"].ToString());
                                obj.MenuID = int.Parse(dt.Rows[0]["MenuID"].ToString());
                                _tbl_RoleMaster.Add(obj);
                            }
                        }
                        dt.Dispose();
                        conn.Close();
                    }
                }
                #endregion

              
                    foreach (var property in _tbl_RoleMaster)
                {
                    RoleActive++;
                    if (property.ViewRecords == true)
                    {
                        MenuID = (property.MenuID).ToString();
                        #region Permission
                        switch (MenuID)
                        {
                            case "1":
                                RolePermission.Store = true;
                                RolePermission.A_Store = RoleActive;
                                break;
                            case "2":
                                RolePermission.Product = true;
                                RolePermission.A_Product = RoleActive;
                                break;
                            case "3":
                                RolePermission.Procurement = true;
                                RolePermission.A_Procurement = RoleActive;
                                break;
                            case "4":
                                RolePermission.Tax_Setup = true;
                                RolePermission.A_Tax_Setup = RoleActive;
                                break;
                            case "5":
                                RolePermission.Role_Setup = true;
                                RolePermission.A_Role_Setup = RoleActive;
                                break;
                            case "6":
                                RolePermission.Live_Counters = true;
                                RolePermission.A_Live_Counters = RoleActive;
                                break;
                            case "7":
                                RolePermission.Sale_Statistics = true;
                                RolePermission.A_Sale_Statistics = RoleActive;
                                break;
                            case "8":
                                RolePermission.Reports = true;
                                RolePermission.A_Reports = RoleActive;
                                break;
                            case "9":
                                RolePermission.Sync = true;
                                RolePermission.A_Sync = RoleActive;
                                break;
                            case "10":
                                RolePermission.Settings = true;
                                RolePermission.A_Settings = RoleActive;
                                break;
                            case "11":
                                RolePermission.Dashboard = true;
                                RolePermission.A_Dashboard = RoleActive;
                                break;
                            case "12":
                                RolePermission.Coupon = true;
                                RolePermission.A_Coupon = RoleActive;
                                break;
                            case "13":
                                RolePermission.TillStatus = true;
                                RolePermission.A_TillStatus = RoleActive;
                                break;
                            //case "15":
                            //    RolePermission.Void = true;
                            //    RolePermission.A_Void = RoleActive;
                            //    break;
                            //case "16":
                            //    RolePermission.Refund = true;
                            //    RolePermission.A_Refund = RoleActive;
                            //    break;
                            //case "17":
                            //    RolePermission.Cancel = true;
                            //    RolePermission.A_Cancel = RoleActive;
                            //    break;
                            //case "18":
                            //    RolePermission.TC = true;
                            //    RolePermission.A_TC = RoleActive;
                            //    break;
                            //case "19":
                            //    RolePermission.ManagerMenu = true;
                            //    RolePermission.A_ManagerMenu = RoleActive;
                            //    break;
                            case "20":
                                RolePermission.DailyManagementReport = true;
                                RolePermission.A_DailyManagementReport = RoleActive;
                                break;
                            case "21":
                                RolePermission.DepositVerifierReport = true;
                                RolePermission.A_DepositVerifierReport = RoleActive;
                                break;
                            case "22":
                                RolePermission.DepositVerification = true;
                                RolePermission.A_DepositVerification = RoleActive;
                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmLogin + ex.StackTrace, ex.LineNumber());
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    Functions.SetIcon(this);
            //    pictureBox1.Width = 120;
            //    pictureBox1.Height = 100;
            //    pictureBox1.BackgroundImage = SFPOSWindows.Properties.Resources.intPOS_logo;
            //    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            //    pictureBox1.Refresh();
            //}
            this.BringToFront();
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtUserId.Text.Trim().ToLower().Contains("cl"))
            {
                txtUserId.Text = "";
            }
            else if (e.KeyChar == (char)13)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPassword.Text.Trim().ToLower().Contains("cl"))
            {
                txtPassword.Text = "";
            }
            else if (e.KeyChar == (char)13)
            {
                btnLogin.Focus();
                Login();
            }
        }

        public void Login()
        {
            try
            {
                LoginInfo.Connections = CheckConnection();
                if ((string.IsNullOrWhiteSpace(txtUserId.Text)))
                {
                    txtUserId.Focus();
                    ep.SetError(txtUserId, "UserId can't be null!");
                }
                else if ((!(Regex.Match(txtUserId.Text, CommonModelCont.UserID_Validation)).Success))
                {
                    txtUserId.Focus();
                    ep.SetError(txtUserId, "Please enter correct format of user Id. 'Format : ****' ");
                }
                else if (txtPassword.Text == CommonModelCont.EmptyString)
                {
                    txtPassword.Focus();
                    ep.SetError(txtPassword, AlertMessages.LoginValidation3);
                    ep.SetError(txtUserId, CommonModelCont.EmptyString);
                }
                else
                {
                    ep.SetError(txtUserId, CommonModelCont.EmptyString);
                    ep.SetError(txtPassword, CommonModelCont.EmptyString);
                    if (txtUserId.Text != CommonModelCont.EmptyString || txtPassword.Text != CommonModelCont.EmptyString)
                    {
                        if (XMLData.Type == 1)
                        {
                            if (LoginInfo.Connections)
                            {

                                //UpdateFSAmount();

                                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                var ObjLoginInfo = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.EmailID == txtUserId.Text && o.Password == txtPassword.Text)
                                                    join SM in _db.tbl_StoreMaster.Where(o => o.IsDelete == false)
                                                    on EM.StoreID equals SM.StoreID
                                                    join RM in _db.tbl_RoleMaster.Where(o => o.IsDelete == false)
                                                    on EM.RoleID equals RM.RoleID
                                                    select new
                                                    {
                                                        EmployeeID = EM.EmployeeID,
                                                        RoleID = EM.RoleID,
                                                        StoreID = EM.StoreID,
                                                        FirstName = EM.FirstName,
                                                        LastName = EM.LastName,
                                                        MaxVoidAmount = EM.MaxVoidAmount,
                                                        BirthDate = EM.BirthDate,
                                                        StoreName = SM.StoreName,
                                                        RoleType = RM.RoleType,
                                                        AgeVarificationAge = SM.AgeVarificationAge,
                                                        DefaultTax = SM.DefaultTax,
                                                        Disclaimer = SM.Disclaimer,
                                                        IsStoreTax = SM.IsStoreTax
                                                    }).ToList();

                                if (ObjLoginInfo != null && ObjLoginInfo.Count != 0)
                                {
                                    if (ObjLoginInfo[0].RoleID != 3)
                                    {
                                        if (CheckRolePermission(Convert.ToInt64(ObjLoginInfo[0].RoleID)))
                                        {
                                            getRolePermission(Convert.ToInt64(ObjLoginInfo[0].RoleID));
                                            LoginInfo.CashierID = txtUserId.Text.Trim();
                                            LoginInfo.UserId = ObjLoginInfo[0].EmployeeID;
                                            LoginInfo.StoreID = Convert.ToInt64(ObjLoginInfo[0].StoreID);
                                            LoginInfo.StoreName = ObjLoginInfo[0].StoreName;
                                            LoginInfo.MaxVoidAmount = (String.IsNullOrEmpty(ObjLoginInfo[0].MaxVoidAmount.ToString()) ? 0 : Convert.ToDecimal(ObjLoginInfo[0].MaxVoidAmount.ToString()));
                                            AgeVerifidInfo.AgeVerificationAge = Convert.ToInt32(ObjLoginInfo[0].AgeVarificationAge.Value.ToString() == "" ? "21" : ObjLoginInfo[0].AgeVarificationAge.Value.ToString());
                                            LoginInfo.UserName = ObjLoginInfo[0].FirstName + " " + ObjLoginInfo[0].LastName;
                                            LoginInfo.StoreDefaultTax = (!String.IsNullOrEmpty(ObjLoginInfo[0].DefaultTax.ToString()) ? Convert.ToDecimal(ObjLoginInfo[0].DefaultTax.ToString()) : 0);
                                            LoginInfo.StoreDisclaimer = ObjLoginInfo[0].Disclaimer.ToString();
                                            LoginInfo.IsStoreTax = Convert.ToBoolean(ObjLoginInfo[0].IsStoreTax);

                                            LoginInfo.CashierAge = DateTime.Now.Year - Convert.ToDateTime(ObjLoginInfo[0].BirthDate).Year;
                                            if (DateTime.Now.DayOfYear < Convert.ToDateTime(ObjLoginInfo[0].BirthDate).DayOfYear)
                                                LoginInfo.CashierAge = LoginInfo.CashierAge - 1;
                                            LoginInfo.RoleType = ObjLoginInfo[0].RoleType.ToString();
                                            Hide();
                                            FrmMetroMaster obj = new FrmMetroMaster();
                                            obj.lblLoginName.Text = ObjLoginInfo[0].FirstName.ToString() + " " + ObjLoginInfo[0].LastName.ToString() + "(" + ObjLoginInfo[0].RoleType + ")";
                                            //obj.lblStoreName.Text = LoginInfo.StoreName;
                                            obj.lblVersion.Text = XMLData.Version;
                                            obj.ShowDialog();
                                            Application.Exit();
                                        }
                                        else
                                        {
                                            ClsCommon.MsgBox("Information", "The user does not have permission to use back-end server!", false);
                                        }
                                    }
                                    else
                                    {
                                        ClsCommon.MsgBox("Information", "The operator can not use backend server!", false);
                                    }
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", AlertMessages.LoginValidation, false);
                                    txtUserId.Text = "";
                                    txtPassword.Text = "";
                                    txtUserId.Focus();
                                }
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "Connection failed with backend database, Try after some time!", false);
                                txtUserId.Text = "";
                                txtPassword.Text = "";
                                txtUserId.Focus();
                            }
                        }
                        else if (XMLData.Type == 2)
                        {
                            tbl_EmployeeMaster _tbl_EmployeeMaster = new tbl_EmployeeMaster();

                            if (LoginInfo.Connections)
                            {
                                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                                var ObjLoginInfo = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.EmailID == txtUserId.Text && o.Password == txtPassword.Text)
                                                    join SM in _db.tbl_StoreMaster.Where(o => o.IsDelete == false)
                                                    on EM.StoreID equals SM.StoreID
                                                    join RM in _db.tbl_RoleMaster.Where(o => o.IsDelete == false)
                                                    on EM.RoleID equals RM.RoleID
                                                    select new
                                                    {
                                                        EmployeeID = EM.EmployeeID,
                                                        RoleID = EM.RoleID,
                                                        StoreID = EM.StoreID,
                                                        FirstName = EM.FirstName,
                                                        LastName = EM.LastName,
                                                        MaxVoidAmount = EM.MaxVoidAmount,
                                                        BirthDate = EM.BirthDate,
                                                        StoreName = SM.StoreName,
                                                        RoleType = RM.RoleType,
                                                        AgeVarificationAge = SM.AgeVarificationAge,
                                                        DefaultTax = SM.DefaultTax,
                                                        Disclaimer = SM.Disclaimer,
                                                        IsStoreTax = SM.IsStoreTax
                                                    }).ToList();
                                if (ObjLoginInfo != null && ObjLoginInfo.Count != 0)
                                {
                                    _tbl_EmployeeMaster.EmployeeID = int.Parse(ObjLoginInfo[0].EmployeeID.ToString());
                                    _tbl_EmployeeMaster.RoleID = int.Parse(ObjLoginInfo[0].RoleID.ToString());
                                    _tbl_EmployeeMaster.StoreID = int.Parse(ObjLoginInfo[0].StoreID.ToString());
                                    _tbl_EmployeeMaster.FirstName = ObjLoginInfo[0].FirstName.ToString();
                                    _tbl_EmployeeMaster.LastName = ObjLoginInfo[0].LastName.ToString();

                                    if (ObjLoginInfo[0].BirthDate != null && ObjLoginInfo[0].BirthDate.ToString() != "")
                                    {
                                        LoginInfo.CashierAge = DateTime.Now.Year - Convert.ToDateTime(ObjLoginInfo[0].BirthDate.ToString()).Year;
                                        if (DateTime.Now.DayOfYear < Convert.ToDateTime(ObjLoginInfo[0].BirthDate).DayOfYear)
                                            LoginInfo.CashierAge = LoginInfo.CashierAge - 1;
                                    }
                                    else
                                    {
                                        LoginInfo.CashierAge = 0;
                                    }
                                    LoginInfo.CashierID = txtUserId.Text.Trim();
                                    LoginInfo.MaxVoidAmount = (String.IsNullOrEmpty(ObjLoginInfo[0].MaxVoidAmount.ToString()) ? 0 : Convert.ToDecimal(ObjLoginInfo[0].MaxVoidAmount.ToString()));
                                    LoginInfo.StoreName = ObjLoginInfo[0].StoreName.ToString();
                                    LoginInfo.StoreDefaultTax = (!String.IsNullOrEmpty(ObjLoginInfo[0].DefaultTax.ToString()) ? Convert.ToDecimal(ObjLoginInfo[0].DefaultTax.ToString()) : 0);
                                    LoginInfo.StoreDisclaimer = ObjLoginInfo[0].Disclaimer.ToString();
                                    LoginInfo.IsStoreTax = (String.IsNullOrEmpty(ObjLoginInfo[0].IsStoreTax.ToString()) ? false : Convert.ToBoolean(ObjLoginInfo[0].IsStoreTax.ToString()));
                                    AgeVerifidInfo.AgeVerificationAge = int.Parse(ObjLoginInfo[0].AgeVarificationAge.ToString() == "" ? "21" : ObjLoginInfo[0].AgeVarificationAge.ToString());
                                    LoginInfo.StoreName = ObjLoginInfo[0].StoreName.ToString();
                                    LoginInfo.RoleType = ObjLoginInfo[0].RoleType.ToString();
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", AlertMessages.LoginValidation, false);
                                    txtUserId.Text = "";
                                    txtPassword.Text = "";
                                    txtUserId.Focus();
                                }
                            }
                            else
                            {
                                if (conn.State == ConnectionState.Closed)
                                {
                                    conn.Open();
                                }
                                DataTable dt = new DataTable();
                                string query = "SELECT EM.EmployeeID,EM.RoleID,EM.StoreID,EM.FirstName,EM.LastName,EM.MaxVoidAmount,EM.BirthDate,SM.IsStoreTax,SM.DefaultTax,SM.Disclaimer,SM.StoreName,RM.RoleType,SM.AgeVarificationAge FROM tbl_EmployeeMaster EM join tbl_StoreMaster SM on sm.StoreID = em.StoreID join tbl_RoleMaster RM ON RM.RoleID = EM.RoleID WHERE EmailID = @EmailID AND Password = @Password AND EM.IsDelete = 0";
                                DataAdapter = new SqlCeDataAdapter(query, conn);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@EmailID", txtUserId.Text);
                                DataAdapter.SelectCommand.Parameters.AddWithValue("@Password", txtPassword.Text);
                                DataAdapter.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    _tbl_EmployeeMaster.EmployeeID = int.Parse(dt.Rows[0]["EmployeeID"].ToString());
                                    _tbl_EmployeeMaster.RoleID = int.Parse(dt.Rows[0]["RoleID"].ToString());
                                    _tbl_EmployeeMaster.StoreID = int.Parse(dt.Rows[0]["StoreID"].ToString());
                                    _tbl_EmployeeMaster.FirstName = dt.Rows[0]["FirstName"].ToString();
                                    _tbl_EmployeeMaster.LastName = dt.Rows[0]["LastName"].ToString();

                                    if (dt.Rows[0]["BirthDate"].ToString() != null && dt.Rows[0]["BirthDate"].ToString() != "")
                                    {
                                        LoginInfo.CashierAge = DateTime.Now.Year - Convert.ToDateTime(dt.Rows[0]["BirthDate"].ToString()).Year;
                                        if (DateTime.Now.DayOfYear < Convert.ToDateTime(dt.Rows[0]["BirthDate"].ToString()).DayOfYear)
                                            LoginInfo.CashierAge = LoginInfo.CashierAge - 1;
                                    }
                                    else
                                    {
                                        LoginInfo.CashierAge = 0;
                                    }
                                    LoginInfo.CashierID = txtUserId.Text.Trim();
                                    LoginInfo.MaxVoidAmount = (String.IsNullOrEmpty(dt.Rows[0]["MaxVoidAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["MaxVoidAmount"].ToString()));
                                    LoginInfo.StoreName = dt.Rows[0]["StoreName"].ToString();
                                    LoginInfo.StoreDefaultTax = (!String.IsNullOrEmpty(dt.Rows[0]["DefaultTax"].ToString()) ? Convert.ToDecimal(dt.Rows[0]["DefaultTax"].ToString()) : 0);
                                    LoginInfo.StoreDisclaimer = dt.Rows[0]["Disclaimer"].ToString();
                                    LoginInfo.IsStoreTax = (String.IsNullOrEmpty(dt.Rows[0]["IsStoreTax"].ToString()) ? false : Convert.ToBoolean(dt.Rows[0]["IsStoreTax"].ToString()));
                                    AgeVerifidInfo.AgeVerificationAge = int.Parse(dt.Rows[0]["AgeVarificationAge"].ToString() == "" ? "21" : dt.Rows[0]["AgeVarificationAge"].ToString());
                                    LoginInfo.RoleType = dt.Rows[0]["RoleType"].ToString(); ;
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", AlertMessages.LoginValidation, false);
                                    txtUserId.Text = "";
                                    txtPassword.Text = "";
                                    txtUserId.Focus();
                                }
                                dt.Dispose();
                                if (conn.State == ConnectionState.Open)
                                {
                                    conn.Close();
                                }
                            }

                            if (_tbl_EmployeeMaster.FirstName != null || _tbl_EmployeeMaster.LastName != null)
                            {
                                LoginInfo.UserId = _tbl_EmployeeMaster.EmployeeID;
                                LoginInfo.UserName = _tbl_EmployeeMaster.FirstName + " " + _tbl_EmployeeMaster.LastName;
                                if (CheckCurrentLogin())
                                {
                                    getRolePermission(Convert.ToInt64(_tbl_EmployeeMaster.RoleID));

                                    LoginInfo.StoreID = Convert.ToInt64(_tbl_EmployeeMaster.StoreID);
                                    Hide();

                                    //string hostName = Dns.GetHostName();
                                    //LoginInfo.CounterIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                                    frmOrderScanner objfrmOrderScanner = new frmOrderScanner();
                                    objfrmOrderScanner.lblLoginName.Text = _tbl_EmployeeMaster.FirstName + " " + _tbl_EmployeeMaster.LastName + "(" + LoginInfo.RoleType + ")";
                                    objfrmOrderScanner.lblStoreName.Text = LoginInfo.StoreName;
                                    objfrmOrderScanner.lblVersion.Text = XMLData.Version;

                                    lstLoginMasterModel.CounterIP = LoginInfo.CounterIP;
                                    if (CheckConnection())
                                    {
                                        //AddLoginTIme
                                        _LoginService.AddLogin(lstLoginMasterModel, 1);
                                        LoginLogoutTime(1);
                                    }
                                    objfrmOrderScanner.ShowDialog();
                                    if (CheckConnection())
                                    {
                                        //AddLogOutTime
                                        _LoginService.AddLogin(lstLoginMasterModel, 2);
                                        LoginLogoutTime(0);
                                    }
                                    Application.Exit();
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", "This user already logged in another system", false);
                                    txtUserId.Text = "";
                                    txtPassword.Text = "";
                                    txtUserId.Focus();
                                }
                            }
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", AlertMessages.LoginValidation, false);
                            txtUserId.Text = "";
                            txtPassword.Text = "";
                            txtUserId.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmLogin + ex.StackTrace, ex.LineNumber());
            }
        }
        public bool CheckCurrentLogin()
        {
            bool status = true;
            if (LoginInfo.Connections && ClsCommon.CheckPrinterRoll)
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var Counter = _db.tbl_CounterMaster.Where(x => x.LoginID == LoginInfo.UserId && x.LogoutTime == null && x.CounterIP != LoginInfo.CounterIP).FirstOrDefault();
                if (Counter != null)
                {
                    status = false;
                }
            }
            return status;
        }

        public void UpdateFSAmount()
        {
            using (SqlConnection connection = new SqlConnection(XMLData.DbConnectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command object for the stored procedure
                using (SqlCommand command = new SqlCommand("UpdateFSEligibleAmount", connection))
                {
                    // Set command type to stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Execute the command
                    command.ExecuteNonQuery();
                }
            }
        }
        public void LoginLogoutTime(int status)
        {
            DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
            var Counter = _db.tbl_CounterMaster.Where(x => x.CounterIP == LoginInfo.CounterIP).FirstOrDefault();
            if (Counter != null)
            {
                if (status == 1)
                {
                    Counter.LoginTime = DateTime.Now;
                    Counter.LoginID = LoginInfo.UserId;
                    Counter.CurrentLoginUser = LoginInfo.UserName;
                    Counter.CurrentVersion = XMLData.Version;
                    Counter.MacAddress = LoginInfo.MacAddress;
                    Counter.LogoutTime = null;
                }
                else
                {
                    Counter.LogoutTime = DateTime.Now;
                }
                _db.SaveChanges();
            }
        }

        public bool CheckConnection()
        {
            bool Status;
            var task = Task.Run(() =>
            {
                Status = db_Connection();
            });
            bool isCompletedSuccessfully = task.Wait(TimeSpan.FromMilliseconds(5000));
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

        public bool db_Connection()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var Empl = _db.tbl_EmployeeMaster.FirstOrDefault();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
