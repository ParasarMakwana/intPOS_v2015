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

namespace SFPOSWindows.CustomControl
{
    public partial class UCResume : UserControl
    {
        #region Properties
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        private PosExplorer myPosExplorer;
        
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }
        private void FrmResume_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               
                //PortOpen_Close(false);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
               
                this.Hide();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }
        private void FrmResume_Load(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
            }
        }
       
        #endregion

        #region Functions
        public UCResume()
        {
            InitializeComponent();
          
        }

        public void ProductAdd()
        {
            try
            {
                if (txtUPCCode.Text != CommonModelCont.EmptyString)
                {

                    if (txtUPCCode.Text.Trim().ToLower().StartsWith("cn"))
                    {
                        this.Hide();
                        OnMyEvent(this, new EventArgs());
                    }
                    //(txtUPCCode.Text.Trim().ToLower().StartsWith("ts") )
                    else
                    {
                        if (!txtUPCCode.Text.Trim().ToLower().StartsWith("ts"))
                        {
                            txtUPCCode.Text = "TS" + txtUPCCode.Text;
                        }
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
                                this.Hide();
                                OnMyEvent(this, new EventArgs());
                            }
                            else
                            {
                                ClsCommon.MsgBox("Information", "This transcation is old more then 24 hours", false);
                                txtUPCCode.Text = "";
                                ManagerAction.resume = false;
                                ManagerAction.suspend = false;
                                ManagerAction.TillStatus = false;
                            }
                        }
                        else
                        {
                            ClsCommon.MsgBox("Information", "No record found", false);
                            txtUPCCode.Text = "";
                            ManagerAction.resume = false;
                            ManagerAction.suspend = false;
                            ManagerAction.TillStatus = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmResume + ex.StackTrace, ex.LineNumber());
                return dt;
            }
        }

       
        #endregion
    }
}
