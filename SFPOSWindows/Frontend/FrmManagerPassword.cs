using MetroFramework.Forms;
using Microsoft.PointOfService;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmManagerPassword : MetroForm
    {
        #region Properties
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        public bool isCartEmpty = false;

        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public delegate void onSupervisorEventHandler(object sender, EventArgs e);
        public event onSupervisorEventHandler OnSupervisorEvent;
        #endregion

        #region Events
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    ManagerPassword();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //DeviceRemove();
                this.Close();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != "")
                {
                    ManagerPassword();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
            }
        }
        private void FrmManagerPassword_Load(object sender, EventArgs e)
        {
            try
            {
                //var deviceCollection = myPosExplorer.GetDevices(DeviceType.Scanner);
                //foreach (DeviceInfo deviceInfo in deviceCollection)
                //{
                //    if (deviceInfo.ServiceObjectName == XMLData.Scanner)
                //    {
                //        myScanner = (Scanner)myPosExplorer.CreateInstance(deviceInfo);
                //        myScanner.Open();
                //        myScanner.Claim(1000);
                //        myScanner.DataEvent += myScanner_DataEvent;
                //        myScanner.DeviceEnabled = true;
                //        myScanner.DataEventEnabled = true;
                //        myScanner.DecodeData = true;
                //    }
                //}
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
            }
        }
        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            ASCIIEncoding myEncoding = new ASCIIEncoding();
            string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
            if (UPCCode.Length > 1)
            {
                //UPCCode = UPCCode.Substring(0, UPCCode.Length - 1);
                if (myScanner.ScanDataType != BarCodeSymbology.Code39)
                    UPCCode = UPCCode.Remove(UPCCode.Length - 1);
                UPCCode = UPCCode.Replace("0", "");
            }
            txtPassword.Text = UPCCode;
            if (txtPassword.Text != "")
            {
                ManagerPassword();
            }
            myScanner.DataEventEnabled = true;
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Down)
                {
                    btnOK.Focus();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
            }
        }
        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Down)
                {
                    btnCancel.Focus();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
            }
        }
        private void FrmManagerPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //DeviceRemove();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public FrmManagerPassword()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
            txtPassword.Focus();
        }
        
        public void ManagerPassword()
        {
            try
            {
                if (txtPassword.Text.Trim().ToLower().Contains("cl"))
                {
                    txtPassword.Text = "";
                }

                else if (txtPassword.Text.Trim().ToLower().Contains("cn"))
                {
                    this.Close();
                    OnMyEvent(this, new EventArgs());
                }

                else if (txtPassword.Text != "")
                {
                    string password = txtPassword.Text;
                    DataTable dt = new DataTable();
                    if (LoginInfo.Connections)
                    {
                        DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

                        var query = (from EM in _db.tbl_EmployeeMaster.Where(o => o.IsDelete == false && o.StoreID == LoginInfo.StoreID && o.Password == password)
                                     join RM in _db.tbl_RoleMaster.Where(o => o.IsDelete == false && o.RoleType == "Manager" || o.RoleType == "Administrator")
                                     on EM.RoleID equals RM.RoleID
                                     select new
                                     {
                                         EM.Password,
                                         EM.FirstName
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

                        string query = "select RoleID  from tbl_RoleMaster as rm where rm.RoleType = 'Manager'";
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);

                        //query = "select pm.Password,pm.FirstName from tbl_EmployeeMaster as pm where pm.IsDelete = 0 and pm.StoreID = " + LoginInfo.StoreID
                        //    + " and pm.RoleID = " + dt.Rows[0]["RoleID"].ToString() + " and pm.Password = '" + password + "'";
                        query = "select pm.Password,pm.FirstName from tbl_EmployeeMaster as pm where pm.IsDelete = 0 and pm.StoreID = " + LoginInfo.StoreID
                             + " and pm.Password = '" + password + "' and pm.RoleID=2 or pm.RoleID=1";
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
                            //DeviceRemove();
                            //FrmSupervisor obj = new FrmSupervisor();
                            //ManagerAction.ManagerName = dt.Rows[0]["FirstName"].ToString();
                            //if(isCartEmpty)
                            //{
                            //    obj.btnResume.Enabled = true;
                            //}
                            //else
                            //{
                            //    obj.btnResume.Enabled = false;
                            //}
                            //obj.Show();
                            //DeviceAdd();
                            this.Close();
                            OnSupervisorEvent(this, new EventArgs());
                        }
                        else
                        {
                            txtPassword.Text = "";
                            ClsCommon.MsgBox("Information","Incorrect password!", false);
                            txtPassword.Focus();
                        }
                    }
                    else
                    {
                        txtPassword.Text = "";
                        ClsCommon.MsgBox("Information","Incorrect password!", false);
                        txtPassword.Focus();
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information","Please enter password.!", false);
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Incorrect password!", false);
                txtPassword.Focus();
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
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
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion
    }
}
