using MetroFramework.Forms;
using Microsoft.PointOfService;
using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Text;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmResume : MetroForm
    {
        #region Properties
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        public static SqlCeDataAdapter DataAdapter = null;
        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);

        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        #endregion

        #region Events
        private void txtUPCCode_KeyPress(object sender, KeyPressEventArgs e)
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }
        private void FrmResume_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DeviceRemove();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                DeviceRemove();
                this.Close();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }
        private void FrmResume_Load(object sender, EventArgs e)
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
                        myScanner.Claim(1000);
                        myScanner.DataEvent += myScanner_DataEvent;
                        myScanner.DeviceEnabled = true;
                        myScanner.DataEventEnabled = true;
                        myScanner.DecodeData = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }
        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            ASCIIEncoding myEncoding = new ASCIIEncoding();
            string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
            if (UPCCode.Length > 1)
            {
                //UPCCode = UPCCode.Substring(0, UPCCode.Length - 1);
            }
            txtUPCCode.Text = UPCCode;
            ProductAdd();
            myScanner.DataEventEnabled = true;
        }
        #endregion

        #region Functions
        public FrmResume()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
        }

        public void ProductAdd()
        {
            try
            {
                if (txtUPCCode.Text != CommonModelCont.EmptyString)
                {
                    if (txtUPCCode.Text.Trim().ToLower().StartsWith("ts"))
                    {
                        DataTable dt = new DataTable();
                        if (LoginInfo.Connections)
                        {
                            dt = GetSuspendTrans(txtUPCCode.Text);
                        }
                        else
                        {
                            string query = "Select * from tbl_TransSuspenMaster WHERE TransSuspendCode='" + txtUPCCode.Text + "'";
                            dt = new DataTable();
                            SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                            da.Fill(dt);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            DateTime TrnsDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString()).AddHours(24);
                            DateTime CurrentDate = DateTime.Now;

                            if (TrnsDate >= CurrentDate)
                            {
                                ManagerAction.dtresume = new DataTable();
                                ManagerAction.dtresume = dt;
                                ManagerAction.resume = true;
                                ManagerAction.suspend = false;
                                ManagerAction.TillStatus = false;
                                this.Close();
                                OnMyEvent(this, new EventArgs());
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information","This transcation is old more then 24 hours", false);
                                txtUPCCode.Text = "";
                                ManagerAction.resume = false;
                                ManagerAction.suspend = false;
                                ManagerAction.TillStatus = false;
                            }
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information","No record found", false);
                            txtUPCCode.Text = "";
                            ManagerAction.resume = false;
                            ManagerAction.suspend = false;
                            ManagerAction.TillStatus = false;
                        }
                    }

                    else if (txtUPCCode.Text.Trim().ToLower().StartsWith("cn"))
                    {
                        DeviceRemove();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable GetSuspendTrans(string TransSuspendCode)
        {
            DataTable dt = new DataTable();
            try
            {
                TransSuspendService _TransSuspendService = new TransSuspendService();
                dt = ClsCommon.ListToDataTable(_TransSuspendService.GetAllTransSuspendDetail(TransSuspendCode));
                return dt;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
                return dt;
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
