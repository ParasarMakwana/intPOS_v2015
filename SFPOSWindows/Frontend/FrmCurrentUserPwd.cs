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
    public partial class FrmCurrentUserPwd : MetroForm
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }
        }

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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
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
                if (LoginInfo.IsManagerReq)
                    lblTitle.Text = "Manager's password required";
                else
                    lblTitle.Text = "Current operator's password required";
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
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
                CheckPassword();
            }
            myScanner.DataEventEnabled = true;
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmCancelTransaction_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //DeviceRemove();
                //PortOpen_Close(false);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public FrmCurrentUserPwd()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);

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
                    this.Close();
                    OnMyEvent(this, new EventArgs());
                }

                else if (txtPassword.Text != "")
                {
                    string password = txtPassword.Text.Trim();
                    DataTable dt = new DataTable();
                    if (LoginInfo.Connections)
                    {
                        DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                        //string query = "";
                        if (LoginInfo.IsManagerReq)
                        {
                            var query = from EM in _db.tbl_EmployeeMaster
                                        where EM.StoreID == LoginInfo.StoreID && EM.IsDelete == false && EM.Password == password
                                        select EM;
                            dt = ClsCommon.LinqToDataTable(query);
                        }
                        else
                        {
                            var query = from EM in _db.tbl_EmployeeMaster
                                        where EM.StoreID == LoginInfo.StoreID && EM.IsDelete == false 
                                        && EM.Password == password && EM.EmployeeID == LoginInfo.UserId
                                        select EM;
                            dt = ClsCommon.LinqToDataTable(query);
                        }
                    }
                    else
                    {
                        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);

                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        if (LoginInfo.IsManagerReq)
                        {
                            string query = "";

                            query = "SELECT Password,MaxVoidAmount FROM tbl_EmployeeMaster WHERE IsDelete=0 AND StoreID="
                                + LoginInfo.StoreID + " AND Password='" + password + "' ORDER BY MaxVoidAmount DESC";
                            SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                        else
                        {
                            string query = "SELECT Password,MaxVoidAmount FROM tbl_EmployeeMaster WHERE IsDelete=0 AND StoreID="
                                + LoginInfo.StoreID + " AND EmployeeID=" + LoginInfo.UserId + " AND Password='" + password + "'";
                            SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                            da.Fill(dt);
                        }
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (password == dt.Rows[0]["Password"].ToString())
                        {
                            if (LoginInfo.IsManagerReq)
                            {
                                decimal MaxVoidAmount = Convert.ToDecimal(dt.Rows[0]["MaxVoidAmount"].ToString());
                                if (MaxVoidAmount >= LoginInfo.TotalVoidAmount)
                                {
                                    Isverified = true;
                                    this.Close();
                                    OnMyEvent(this, new EventArgs());
                                }
                                else
                                {
                                    txtPassword.Text = "";
                                    ClsCommon.MsgBox("Information","Password verified user do not have enough void amount to void your item!", false);
                                    txtPassword.Focus();
                                    Isverified = false;
                                }
                            }
                            else
                            {
                                Isverified = true;
                                this.Close();
                                OnMyEvent(this, new EventArgs());
                            }
                        }
                        else
                        {
                            txtPassword.Text = "";
                            ClsCommon.MsgBox("Information","Incorrect password!", false);
                            txtPassword.Focus();
                            Isverified = false;
                        }
                    }
                    else
                    {
                        txtPassword.Text = "";
                        ClsCommon.MsgBox("Information","Incorrect password!", false);
                        txtPassword.Focus();
                        Isverified = false;
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information","Please enter password.!", false);
                    Isverified = false;
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Incorrect password!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
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
            catch(Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmCurrentUserPwd + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
