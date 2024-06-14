using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOS.Entities.MasterDataClasses;
using System.Drawing;

namespace SFPOSWindows
{
    public partial class frmSplash : MetroForm
    {
        #region Properties
        private DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        private string connectionString = "Data Source = 71.95.23.110; Initial Catalog = intPOS_Key;User ID=POSAdmin; Pwd=POS@123; Integrated Security=False";
        //private string connectionString = "Data Source =tbs24; Initial Catalog = intPOSKeyTest1;User ID=sa; Pwd=sa@123; Integrated Security=False";
        static public int time = 0;
        public bool IsDemoVersion = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        //FrmMetro_AddDemoStore _frmMetro_AddDemoStore;
        public static Boolean IsSuccess = false;
        #endregion

        #region Events
        private void frmSplash_Load(object sender, EventArgs e)
        {
            //if (XMLData.IsDemoVersion == 1)
            // {
            //     Functions.SetIcon(this);

            //     if (XMLData.Type == 1)
            //         pictureBox1.Image = SFPOSWindows.Properties.Resources.intPOSDemo_B;
            //     else
            //         pictureBox1.Image = SFPOSWindows.Properties.Resources.intPOSDemo_F;
            //     pictureBox1.Refresh();
            // }


            this.BringToFront();
            timer1.Interval = 1;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = time + timer1.Interval;
            progressBar.Value = 10 + time / timer1.Interval;
            if (progressBar.Value >= 100)
            {

                timer1.Stop();
                Hide();
                OpenScreen();
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            timer1.Stop();
            Hide();
            OpenScreen();
        }
        #endregion

        #region Functions
        private Boolean CheckDemoLicensekey()
        {
            Boolean IsExpired = true;
            try
            {
                string LicenseKey = XMLData.Key;

                if (LicenseKey != string.Empty)
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        string query1 = "select isnull(ExpiryDate,CreatedDate) as ExpiryDate, GETDATE() as CurrentDate, isnull(DATEDIFF(day,GETDATE(),isnull(ExpiryDate,CreatedDate)),0) as RemDay from tbl_KeyMaster where GeneratedKey = @GeneratedKey";
                        SqlCommand sqlCmd = new SqlCommand(query1, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@GeneratedKey", LicenseKey);
                        SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        sqlCon.Open();
                        adp.Fill(dt);
                        sqlCon.Close();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["RemDay"].ToString()) > 0)
                            {
                                IsExpired = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmSplash + ex.StackTrace, ex.LineNumber());
            }
            return IsExpired;
        }

        public void OpenScreen()
        {
            #region Check Database Connection Details
            if (XMLData.IsDemoVersion == 1 && ClsCommon.IsDemoversion == true)
            {
                ClsCommon.IsDemoversion = false;
                LoginInfo.SettingScreen = "tabLicenceKey";
                FrmMetro_AddDemoStore _frmMetro_AddDemoStore = new FrmMetro_AddDemoStore();
                IsSuccess = false;
                _frmMetro_AddDemoStore.Show();
            }
            else if (XMLData.DbConnectionString != "")
            {
                #region Check Key Active
                if (XMLData.POSStatus)
                {
                    Boolean IsExpired = false;
                    if (XMLData.IsDemoVersion == 1)
                    {
                        IsExpired = CheckDemoLicensekey();
                    }
                    if (XMLData.IsDemoVersion == 1 && IsExpired == true)
                    {
                        ClsCommon.MsgBox("Information", "License key is expired, Please renew license key!", false);
                        Application.Exit();
                    }
                    else
                    {
                        #region Check Sync 
                        if (XMLData.SyncStatus || XMLData.Type == 1)
                        {
                            if (XMLData.Type == 2)
                            {
                                bool check = CheckConnection();
                                if (CheckConnection())
                                {
                                    LoginInfo.SyncType = 2;
                                    frmDataSync2 _frmDataSync = new frmDataSync2();
                                    _frmDataSync.ShowDialog();
                                }
                                frmLogin_P2 _frmLogin = new frmLogin_P2();
                                _frmLogin.ShowDialog();
                                Application.Exit();
                            }
                            else
                            {
                                frmLogin _frmLogin = new frmLogin();
                                _frmLogin.ShowDialog();
                                Application.Exit();
                            }
                        }
                        else
                        {
                            bool check = CheckConnection();
                            //MessageBox.Show(check.ToString());
                            if (CheckConnection())
                            {
                                //MessageBox.Show("in check");
                                LoginInfo.SyncType = 1;
                                frmDataSync _frmDataSync = new frmDataSync();
                                _frmDataSync.ShowDialog();
                            }
                            else
                            {
                                //MessageBox.Show("else check");
                                MessageBox.Show("Please check database");
                            }
                        }
                        #endregion
                    }
                }
                else if (XMLData.IsDemoVersion == 1)
                {
                    LoginInfo.SettingScreen = "tabLicenceKey";
                    FrmMetro_AddDemoStore _frmMetro_AddDemoStore = new FrmMetro_AddDemoStore();
                    IsSuccess = false;
                    _frmMetro_AddDemoStore.Show();
                    //OpenFrmSettingForm();
                }
                else
                {
                    LoginInfo.SettingScreen = "tabLicenceKey";
                    frmSettings _frmSettings = new frmSettings();
                    _frmSettings.ShowDialog();
                    Application.Exit();
                }
                #endregion
            }
            else
            {
                LoginInfo.SettingScreen = "tabDatabaseConnection";
                frmSettings _frmSettings = new frmSettings();
                _frmSettings.ShowDialog();
                Application.Exit();
            }
            #endregion            
        }

        public frmSplash()
        {
            InitializeComponent();

            //ClsCommon.Read_XMLFile(); // Read the isDemoversion or not

            if (XMLData.Type == 1)
            {
                pictureBox1.Image = Properties.Resources.intPOSDemo_B;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.intPOSDemo_F;
            }
            int ScreenIndex = Convert.ToInt32(XMLData.POSScreen);
            this.Location = Screen.AllScreens[ScreenIndex].Bounds.Location;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.AllScreens[ScreenIndex].Bounds;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point()
            {
                X = Math.Max(Screen.AllScreens[XMLData.POSScreen].WorkingArea.X, Screen.AllScreens[XMLData.POSScreen].WorkingArea.X + (Screen.AllScreens[XMLData.POSScreen].WorkingArea.Width - this.Width) / 2),
                Y = Math.Max(Screen.AllScreens[XMLData.POSScreen].WorkingArea.Y, Screen.AllScreens[XMLData.POSScreen].WorkingArea.Y + (Screen.AllScreens[XMLData.POSScreen].WorkingArea.Height - this.Height) / 2)
            };
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
                return false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmSettings_BE + ex.StackTrace, ex.LineNumber());
                return false;
            }
        }
        #endregion

        private void frmSplash_Shown(object sender, EventArgs e)
        {
             ClsCommon.SetScreen(this, XMLData.POSScreen);
        }
    }
}
