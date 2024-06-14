using Microsoft.PointOfService;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SFPOSWindows.CustomControl
{
    public partial class UCCurrentUserPwd : UserControl
    {
        #region Properties
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public bool Isverified = false;
        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        public int Flag = 0;
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
                    CheckPassword();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }
        }

        private void USCurrentUserPwd_Load(object sender, EventArgs e)
        {
            try
            {

                txtPassword.Focus();
                if (LoginInfo.IsManagerReq || OrderInfo.MenuIdforPermission == 15)//|| OrderInfo.MenuIdforPermission == 20
                    lblTitle.Text = "Manager's password required";
                else
                    lblTitle.Text = "Current operator's password required";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    CheckPassword();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Incorrect password for No sale!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public UCCurrentUserPwd()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
        }


        public void CheckPassword()
        {
            try
            {
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
                    if (PriceOverride.IsOverridePrice || LoginInfo.Command == "5")
                    {
                        MaxOverrideAmount();
                    }
                    else if(OrderInfo.MenuIdforPermission == 15)
                    {
                        MaxVoidAmount();
                    }
                    else 
                    {
                       Currentuserpassword();
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Please enter password.!", false);
                    Isverified = false;
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Incorrect password!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }

        }

        public void MaxVoidAmount()
        {
            string password = txtPassword.Text.Trim();
            DataTable dt = new DataTable();

            if (LoginInfo.Connections)
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
               var query = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.StoreID == LoginInfo.StoreID && o.Password == password)
                             join RPM in _db.tbl_RolePermission.Where(x=>x.MenuID == OrderInfo.MenuIdforPermission)
                            on EM.RoleID equals RPM.RoleID

                             select new
                             {
                                 EM.Password,
                                 EM.MaxVoidAmount,
                                 RPM.ViewRecords
                             }).ToList().OrderByDescending(o => o.MaxVoidAmount);

                dt = ClsCommon.LinqToDataTable(query);
            }
            else
            {
                SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
                conn.Open();

                string query = "";
                query = "select pm.Password, pm.MaxVoidAmount, rpm.ViewRecords  from tbl_EmployeeMaster as pm  inner join tbl_RolePermission as rpm on pm.RoleID = rpm.RoleID where pm.IsDelete = 0 and pm.StoreID = " + LoginInfo.StoreID
                          + " and pm.Password = '" + password + "' and rpm.MenuID = " + OrderInfo.MenuIdforPermission + " ORDER BY pm.MaxVoidAmount DESC";
                SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                if (password == dt.Rows[0]["Password"].ToString())
                {
                    Boolean haveper = false;

                    for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                    {
                        if (Convert.ToBoolean(dt.Rows[i1]["ViewRecords"].ToString()) == true)
                        {
                            if (Isverified == false)
                            {
                                haveper = true;

                                if (LoginInfo.IsManagerReq)
                                {
                                    decimal MaxVoidAmount = Convert.ToDecimal(dt.Rows[i1]["MaxVoidAmount"].ToString());
                                    if (MaxVoidAmount >= LoginInfo.TotalVoidAmount)
                                    {
                                        Isverified = true;

                                        this.Hide();
                                        OnMyEvent(this, new EventArgs());
                                    }
                                    else
                                    {
                                        txtPassword.Text = "";
                                        ClsCommon.MsgBox("Information", "Password verified user do not have enough void amount to void your item!", false);
                                        txtPassword.Focus();
                                        Isverified = false;
                                    }
                                }
                                else
                                {
                                    Isverified = true;

                                    this.Hide();
                                    OnMyEvent(this, new EventArgs());
                                }
                            }
                        }
                    }

                    if(haveper == false)
                    {
                        txtPassword.Text = "";
                        ClsCommon.MsgBox("Information", "User do not have permission!", false);
                        txtPassword.Focus();
                        Isverified = false;
                    }
                }
                else
                {
                    txtPassword.Text = "";
                    ClsCommon.MsgBox("Information", "Incorrect password!", false);
                    txtPassword.Focus();
                    Isverified = false;
                }
            }
            else
            {
                txtPassword.Text = "";
                ClsCommon.MsgBox("Information", "Incorrect password!", false);
                txtPassword.Focus();
                Isverified = false;
            }
        }

        public void MaxOverrideAmount()
        {
            string password = txtPassword.Text.Trim();
            DataTable dt = new DataTable();

            if (LoginInfo.Connections)
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

                var query = (from EM in _db.tbl_EmployeeMaster
                             join RM in _db.tbl_RoleMaster on EM.RoleID equals RM.RoleID
                             where (EM.IsDelete == false && EM.IsActive == true && EM.StoreID == LoginInfo.StoreID && EM.Password == password)
                             select new
                             {
                                 EM.Password,
                                 RM.OverrideAmount
                             }).ToList().OrderByDescending(o => o.OverrideAmount);

                dt = ClsCommon.LinqToDataTable(query);
            }
            else
            {
                SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
                conn.Open();

                if (LoginInfo.IsManagerReq)
                {
                    string query = "";
                    query = "SELECT Password,MaxVoidAmount FROM tbl_EmployeeMaster WHERE IsDelete=0 AND StoreID=" + LoginInfo.StoreID + " AND Password='" + password + "' ORDER BY MaxVoidAmount DESC";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                }
                else
                {
                    string query = "SELECT Password,MaxVoidAmount FROM tbl_EmployeeMaster WHERE IsDelete=0 AND StoreID=" + LoginInfo.StoreID + " AND EmployeeID=" + LoginInfo.UserId + " AND Password='" + password + "'";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                    da.Fill(dt);
                }
               conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                if (password == dt.Rows[0]["Password"].ToString())
                {
                     decimal MaxOverwriteAmount = Convert.ToDecimal(dt.Rows[0]["OverrideAmount"].ToString());
                    if (MaxOverwriteAmount >= PriceOverride.OverridePrice)
                    {
                        Isverified = true;
                        Flag = 5;
                        PriceOverride.MaxOverridePrice = MaxOverwriteAmount;
                        this.Hide();
                        OnMyEvent(this, new EventArgs());
                    }
                    else
                    {
                        txtPassword.Text = "";
                        ClsCommon.MsgBox("Information", "Password verified user do not have enough override amount!", false);
                        txtPassword.Focus();
                        Isverified = false;
                    }
                }
                else
                {
                    txtPassword.Text = "";
                    ClsCommon.MsgBox("Information", "Incorrect password!", false);
                    txtPassword.Focus();
                    Isverified = false;
                }
            }
            else
            {
                txtPassword.Text = "";
                ClsCommon.MsgBox("Information", "Incorrect password!", false);
                txtPassword.Focus();
                Isverified = false;
            }
        }

        public void Currentuserpassword()
        {
            string password = txtPassword.Text.Trim();
            DataTable dt = new DataTable();

            if (LoginInfo.Connections)
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                if (OrderInfo.MenuIdforPermission == 0)
                {
                    if (LoginInfo.IsManagerReq)
                    {
                        var query = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.StoreID == LoginInfo.StoreID && o.Password == password)
                                     select new
                                     {
                                         EM.Password,
                                         EM.MaxVoidAmount
                                     }).ToList().OrderByDescending(o => o.MaxVoidAmount);

                        dt = ClsCommon.LinqToDataTable(query);
                    }
                    else
                    {
                        var query = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.StoreID == LoginInfo.StoreID && o.EmployeeID == LoginInfo.UserId && o.Password == password)
                                     select new
                                     {
                                         EM.Password,
                                         EM.MaxVoidAmount
                                     }).ToList();

                        dt = ClsCommon.LinqToDataTable(query);
                    }
                }
                else
                {
                    var query = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.StoreID == LoginInfo.StoreID && o.Password == password)
                                 join RPM in _db.tbl_RolePermission.Where(x => x.MenuID == OrderInfo.MenuIdforPermission)
                                on EM.RoleID equals RPM.RoleID

                                 select new
                                 {
                                     EM.Password,
                                     EM.MaxVoidAmount,
                                     RPM.ViewRecords
                                 }).ToList().OrderByDescending(o => o.MaxVoidAmount);

                    dt = ClsCommon.LinqToDataTable(query);
                }
            }
            else
            {
                SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
                conn.Open();

                if (OrderInfo.MenuIdforPermission == 0)
                {
                    if (LoginInfo.IsManagerReq)
                    {
                        string query = "";
                        query = "SELECT Password,MaxVoidAmount FROM tbl_EmployeeMaster WHERE IsDelete=0 AND StoreID=" + LoginInfo.StoreID + " AND Password='" + password + "' ORDER BY MaxVoidAmount DESC";
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        dt = new DataTable();
                        da.Fill(dt);
                    }
                    else
                    {
                        string query = "SELECT Password,MaxVoidAmount FROM tbl_EmployeeMaster WHERE IsDelete=0 AND StoreID=" + LoginInfo.StoreID + " AND EmployeeID=" + LoginInfo.UserId + " AND Password='" + password + "'";
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                    }
                }
                else
                {
                    string query = "";
                    query = "select pm.Password, pm.MaxVoidAmount, rpm.ViewRecords  from tbl_EmployeeMaster as pm  inner join tbl_RolePermission as rpm on pm.RoleID = rpm.RoleID where pm.IsDelete = 0 and pm.StoreID = " + LoginInfo.StoreID
                              + " and pm.Password = '" + password + "' and rpm.MenuID = " + OrderInfo.MenuIdforPermission + " ORDER BY pm.MaxVoidAmount DESC";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                }
                    conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                if (password == dt.Rows[0]["Password"].ToString())
                {
                    if (OrderInfo.MenuIdforPermission == 0)
                    {
                        Isverified = true;

                        this.Hide();
                        OnMyEvent(this, new EventArgs());
                    }
                    else
                    {
                        Boolean haveper = false;

                        for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                        {
                            if (Convert.ToBoolean(dt.Rows[i1]["ViewRecords"].ToString()) == true)
                            {
                                haveper = true;
                            }
                        }

                        if (haveper == false)
                        {
                            txtPassword.Text = "";
                            ClsCommon.MsgBox("Information", "User do not have permission!", false);
                            txtPassword.Focus();
                            Isverified = false;
                        }
                        else
                        {
                            Isverified = true;

                            this.Hide();
                            OnMyEvent(this, new EventArgs());
                        }
                    }
                }
                else
                {
                    txtPassword.Text = "";
                    ClsCommon.MsgBox("Information", "Incorrect password!", false);
                    txtPassword.Focus();
                    Isverified = false;
                }
            }
            else
            {
                txtPassword.Text = "";
                ClsCommon.MsgBox("Information", "Incorrect password!", false);
                txtPassword.Focus();
                Isverified = false;
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
