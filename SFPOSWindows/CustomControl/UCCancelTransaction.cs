using Microsoft.PointOfService;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.CustomControl
{
    public partial class UCCancelTransaction : UserControl
    {
        #region Properties
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public bool IsCancel = false;
        private PosExplorer myPosExplorer;
        private Scanner myScanner;

        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        
        #endregion

        #region Events
        
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != "")
                {
                    CancelTransaction();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }

        private void UCCancelTransaction_Load(object sender, EventArgs e)
        {
            try
            {
               txtPassword.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    CancelTransaction();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Incorrect password for cancel transaction.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public UCCancelTransaction()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
            //txtPassword.Focus();
        }

        public void CancelTransaction()
        {
            try
            {
                txtPassword.Focus();
                if (txtPassword.Text.Trim().ToLower().Contains("cl"))
                {
                    txtPassword.Text = "";
                }

                else if (txtPassword.Text.Trim().ToLower().Contains("cn"))
                {
                    this.Hide();
                    OnMyEvent(this, new EventArgs());
                }

                else if (txtPassword.Text != "")
                {
                    string password = txtPassword.Text;
                    DataTable dt = new DataTable();
                    if (LoginInfo.Connections)
                    {
                        DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

                        //var query = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.StoreID == LoginInfo.StoreID && o.Password == password)
                        //             join RM in _db.tbl_RoleMaster.Where(o => o.IsDelete == false && o.RoleType == "Manager" || o.RoleType == "Administrator")
                        //             on EM.RoleID equals RM.RoleID
                        //             select new
                        //             {
                        //                 EM.Password
                        //             }).ToList();

                        var query = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.StoreID == LoginInfo.StoreID && o.Password == password)
                                     join RM in _db.tbl_RoleMaster.Where(o => o.IsDelete == false)
                                     on EM.RoleID equals RM.RoleID
                                     join RPM in _db.tbl_RolePermission.Where(o => o.MenuID == OrderInfo.MenuIdforPermission)
                                     on RM.RoleID equals RPM.RoleID

                                     select new
                                     {
                                         EM.Password,
                                         RPM.ViewRecords
                                     }).ToList();

                        dt = ClsCommon.LinqToDataTable(query);
                    }
                    else
                    {
                        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }

                        string query = ""; // "select RoleID  from tbl_RoleMaster as rm where rm.RoleType = 'Manager'";
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        //da.Fill(dt);

                        //query = "select pm.Password from tbl_EmployeeMaster as pm where pm.IsDelete = 0 and pm.StoreID = " + LoginInfo.StoreID
                        //    + " and pm.Password = '" + password + "' and pm.RoleID=2 or pm.RoleID=1";

                        query = "select pm.Password, rpm.ViewRecords from tbl_EmployeeMaster as pm  inner join tbl_RolePermission as rpm on pm.RoleID = rpm.RoleID where pm.IsDelete = 0 and pm.StoreID = " + LoginInfo.StoreID
                            + " and pm.Password = '" + password + "' and rpm.MenuID = " + OrderInfo.MenuIdforPermission;

                        da = new SqlCeDataAdapter(query, conn);
                        dt = new DataTable();
                        da.Fill(dt);

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        if (password == dt.Rows[0]["Password"].ToString())
                        {
                            if (Convert.ToBoolean(dt.Rows[0]["ViewRecords"].ToString()) == true)
                            {
                                IsCancel = true;
                                this.Hide();
                                OnMyEvent(this, new EventArgs());
                            }
                            else
                            {
                                txtPassword.Text = "";
                                ClsCommon.MsgBox("Information", "User do not have permission!", false);
                                txtPassword.Focus();
                                IsCancel = false;
                            }
                        }
                        else
                        {
                            txtPassword.Text = "";
                            ClsCommon.MsgBox("Information", "Incorrect password!", false);
                            txtPassword.Focus();
                            IsCancel = false;
                        }
                    }
                    else
                    {
                        ClsCommon.MsgBox("Information", "Incorrect password  or User do not have permission!", false);
                        IsCancel = false;
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                }

                else
                {
                    ClsCommon.MsgBox("Information", "Please enter password.!", false);
                    IsCancel = false;
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Incorrect password!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }

       

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {                    
                    this.Hide();
                    OnMyEvent(this, new EventArgs());
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
