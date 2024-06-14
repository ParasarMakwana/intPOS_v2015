using MetroFramework.Forms;
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

namespace SFPOSWindows.Frontend
{
    public partial class FrmCancelTransaction : MetroForm
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //DeviceRemove();
                //PortOpen_Close(false);
                this.Close();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }

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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmCancelTransaction_Load(object sender, EventArgs e)
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }
        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            ASCIIEncoding myEncoding = new ASCIIEncoding();
            string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
            if (UPCCode.Length > 1)
            {
                if (myScanner.ScanDataType != BarCodeSymbology.Code39)
                    UPCCode = UPCCode.Remove(UPCCode.Length - 1);
                UPCCode = UPCCode.Replace("0", "");
            }
            txtPassword.Text = UPCCode;
            if (txtPassword.Text != "")
            {
                CancelTransaction();
            }
            myScanner.DataEventEnabled = true;
        }

        private void FrmCancelTransaction_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //PortOpen_Close(false);
                //DeviceRemove();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public FrmCancelTransaction()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
        }
        public void CancelTransaction()
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
                                                    EM.Password
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

                        query = "select pm.Password from tbl_EmployeeMaster as pm where pm.IsDelete = 0 and pm.StoreID = " + LoginInfo.StoreID
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
                            IsCancel = true;
                            this.Close();
                            OnMyEvent(this, new EventArgs());
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
                        ClsCommon.MsgBox("Information", "Incorrect password!", false);
                        IsCancel = false;
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                }

                else
                {
                    ClsCommon.MsgBox("Information", "Please enter password.!", false);
                    IsCancel = false;
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Incorrect password!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
                txtPassword.Text = "";
                txtPassword.Focus();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {
                    //DeviceRemove();
                    //PortOpen_Close(false);
                    this.Close();
                    OnMyEvent(this, new EventArgs());
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmCancelTransaction + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
