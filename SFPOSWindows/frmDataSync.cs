using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SFPOSWindows
{
    public partial class frmDataSync : MetroForm
    {
        //SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public static SqlCeDataAdapter DataAdapter = null;
        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
        //DateTime LastSyncDateTime;
        OrderScannerService _OrderScannerService = new OrderScannerService();
        ProductLedgerService _ProductLedgerService = new ProductLedgerService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();

        public frmDataSync()
        {
            InitializeComponent();
        }
        private void frmDataSync_Load(object sender, EventArgs e)
        {
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    Functions.SetIcon(this);
            //}
            ClsCommon.SetScreen(this, XMLData.POSScreen);
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //ClsCommon.MsgBox("Information","Full Sync Started");
            if (LoginInfo.SyncType == 1)
            {
                FullSync();
            }
            else
            {
                Hide();
                frmLogin_P2 _frmLogin = new frmLogin_P2();
                _frmLogin.ShowDialog();
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //ClsCommon.MsgBox("Information","Full Sync Completed");
            if (LoginInfo.SyncType == 1)
            {
                Hide();
                LoginInfo.SyncType = 0;
                XMLData.SyncStatus = true;
                ClsCommon.Update_XMLFile();
                frmLogin_P2 _frmLogin = new frmLogin_P2();
                _frmLogin.ShowDialog();
            }
            else if (LoginInfo.SyncType == 2)
            {
                ClsCommon.MsgBox("Information", "Sync End", false);
            }
            else if (LoginInfo.SyncType == 3)
            {

            }
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //ClsCommon.MsgBox("Information","Start");
                //string path = Application.StartupPath + "\\AppData\\POSDB.sdf";
                //ClsCommon.MsgBox("Information",path);
                SqlCeConnection conn = new SqlCeConnection(@"Data Source =E:\\Asp_net\\SFPOS\\SFPOSWindows\\AppData\\POSDB.sdf; Password ='123456'");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataAdapter = new SqlCeDataAdapter();
                SqlCeCommand cmd = conn.CreateCommand();
                string query = "";

                cmd = conn.CreateCommand();
                cmd.CommandText = query = "INSERT INTO tbl_Country(CountryName) Values('UK');";
                int modified = cmd.ExecuteNonQuery();
                string query2 = "Select @@Identity";
                cmd.CommandText = query2;
                int ID = Convert.ToInt32(cmd.ExecuteScalar());

                query = "Select * from tbl_Country";
                DataTable dt = new DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                da.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                string exx = ex.Message;
            }
        }
        public void FullSync()
        {
            bool IsConnet = CheckConnection();
            if (IsConnet)
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                #region Add LastSync Datetime
                tbl_SyncLog _tbl_SyncLog = new tbl_SyncLog();
                _tbl_SyncLog.SyncType = "Automatic";
                _tbl_SyncLog.SyncDateTime = DateTime.Now;
                _tbl_SyncLog.CounterIP = LoginInfo.CounterIP;
                _tbl_SyncLog.MacAddress = LoginInfo.MacAddress;
                _tbl_SyncLog.UpdatedDate = DateTime.Now;
                _db.tbl_SyncLog.Add(_tbl_SyncLog);
                _db.SaveChanges();
                #endregion

                #region Get Updated Table Name

                var UpdatedTableName = _db.tbl_UpdateLog.Where(x => x.MacAddress == LoginInfo.MacAddress).ToList();

                foreach (tbl_UpdateLog data in UpdatedTableName)
                {
                    string tbl = data.TblName;
                    switch (tbl)
                    {
                        case "tbl_DepartmentMaster":
                            tbl_DepartmentMaster();
                            break;
                        case "tbl_EmployeeMaster":
                            tbl_EmployeeMaster();
                            break;
                        case "tbl_ProductMaster":
                            tbl_ProductMaster();
                            break;
                        case "tbl_ProductSalePriceMaster":
                            tbl_ProductSalePriceMaster();
                            break;
                        case "tbl_RoleMaster":
                            tbl_RoleMaster();
                            break;
                        case "tbl_RolePermission":
                            tbl_RolePermission();
                            break;
                        case "tbl_SectionMaster":
                            tbl_SectionMaster();
                            break;
                        case "tbl_ShortcutkeyMaster":
                            tbl_ShortcutkeyMaster();
                            break;
                        case "tbl_StoreMaster":
                            tbl_StoreMaster();
                            break;
                        case "tbl_TaxGroupMaster":
                            tbl_TaxGroupMaster();
                            break;
                        case "tbl_TaxRateMaster":
                            tbl_TaxRateMaster();
                            break;
                        case "tbl_UnitMeasureMaster":
                            tbl_UnitMeasureMaster();
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                #endregion
            }
        }
        public void ChangeSyncStatus(string TblName)
        {
            DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
            var UpdatedTableName = _db.tbl_UpdateLog.Where(x => x.TblName == TblName && x.MacAddress == LoginInfo.MacAddress).ToList();
            foreach (tbl_UpdateLog tbl in UpdatedTableName)
            {
                tbl.IsSync = true;
                tbl.IsChange = false;
                tbl.SyncDate = DateTime.Now;
                tbl.UpdatedDate = DateTime.Now;
            }
            _db.SaveChanges();
        }
        public bool CheckConnection()
        {
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
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void tbl_DepartmentMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_DepartmentMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_DepartmentMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_DepartmentMaster WHERE DepartmentID=" + Data.DepartmentID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_DepartmentMaster SET DepartmentID=@DepartmentID,DepartmentName=@DepartmentName," +
                                "IsFoodStamp = @IsFoodStamp,TaxGroupID = @TaxGroupID,UnitMeasureID = @UnitMeasureID,AgeVarificationAge = @AgeVarificationAge,DepartmentNo=@DepartmentNo,SubNo=@SubNo,DepartmentBtn=@DepartmentBtn,BtnCode=@BtnCode," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE DepartmentID=@DepartmentID;";
                            cmd.Parameters.Clear();
                            #region Parameters
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.DepartmentName != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentName", Data.DepartmentName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentName", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.DepartmentNo != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentNo", Data.DepartmentNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentNo", DBNull.Value);
                            }
                            if (Data.SubNo != null)
                            {
                                cmd.Parameters.AddWithValue("@SubNo", Data.SubNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SubNo", DBNull.Value);
                            }
                            if (Data.DepartmentBtn != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentBtn", Data.DepartmentBtn);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentBtn", DBNull.Value);
                            }
                            if (Data.BtnCode != null)
                            {
                                cmd.Parameters.AddWithValue("@BtnCode", Data.BtnCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@BtnCode", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_DepartmentMaster(DepartmentID,DepartmentName,IsFoodStamp,TaxGroupID,UnitMeasureID,AgeVarificationAge,DepartmentNo,SubNo,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,DepartmentBtn,BtnCode) " +
                                "VALUES(@DepartmentID,@DepartmentName,@IsFoodStamp,@TaxGroupID,@UnitMeasureID,@AgeVarificationAge,@DepartmentNo,@SubNo,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy,@DepartmentBtn,@BtnCode)";
                            cmd.Parameters.Clear();
                            #region Parameters
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.DepartmentName != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentName", Data.DepartmentName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentName", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.DepartmentNo != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentNo", Data.DepartmentNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentNo", DBNull.Value);
                            }
                            if (Data.SubNo != null)
                            {
                                cmd.Parameters.AddWithValue("@SubNo", Data.SubNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SubNo", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            if (Data.DepartmentBtn != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentBtn", Data.DepartmentBtn);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentBtn", DBNull.Value);
                            }
                            if (Data.BtnCode != null)
                            {
                                cmd.Parameters.AddWithValue("@BtnCode", Data.BtnCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@BtnCode", DBNull.Value);
                            }


                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }
                }
                ChangeSyncStatus("tbl_DepartmentMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_DepartmentMaster" + ex.StackTrace, ex.LineNumber());
            }
        }
        public void tbl_EmployeeMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_EmployeeMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_EmployeeMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_EmployeeMaster WHERE EmployeeID=" + Data.EmployeeID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_EmployeeMaster SET RoleID=@RoleID,StoreID=@StoreID,FirstName=@FirstName,LastName=@LastName,EmailID=@EmailID,PhoneNo=@PhoneNo,Password=@Password,MaxVoidAmount=@MaxVoidAmount," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE EmployeeID=@EmployeeID;";

                            #region Parameters
                            if (Data.EmployeeID != null)
                            {
                                cmd.Parameters.AddWithValue("@EmployeeID", Data.EmployeeID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
                            }
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.StoreID != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            }
                            if (Data.FirstName != null)
                            {
                                cmd.Parameters.AddWithValue("@FirstName", Data.FirstName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                            }
                            if (Data.LastName != null)
                            {
                                cmd.Parameters.AddWithValue("@LastName", Data.LastName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                            }
                            if (Data.EmailID != null)
                            {
                                cmd.Parameters.AddWithValue("@EmailID", Data.EmailID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EmailID", DBNull.Value);
                            }
                            if (Data.PhoneNo != null)
                            {
                                cmd.Parameters.AddWithValue("@PhoneNo", Data.PhoneNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PhoneNo", DBNull.Value);
                            }
                            if (Data.Password != null)
                            {
                                cmd.Parameters.AddWithValue("@Password", Data.Password);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Password", DBNull.Value);
                            }
                            if (Data.MaxVoidAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@MaxVoidAmount", Data.MaxVoidAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MaxVoidAmount", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_EmployeeMaster(EmployeeID,RoleID,StoreID,FirstName,LastName,EmailID,PhoneNo,Password,MaxVoidAmount,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@EmployeeID,@RoleID,@StoreID,@FirstName,@LastName,@EmailID,@PhoneNo,@Password,@MaxVoidAmount,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            #region Parameters
                            if (Data.EmployeeID != null)
                            {
                                cmd.Parameters.AddWithValue("@EmployeeID", Data.EmployeeID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
                            }
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.StoreID != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            }
                            if (Data.FirstName != null)
                            {
                                cmd.Parameters.AddWithValue("@FirstName", Data.FirstName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                            }
                            if (Data.LastName != null)
                            {
                                cmd.Parameters.AddWithValue("@LastName", Data.LastName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                            }
                            if (Data.EmailID != null)
                            {
                                cmd.Parameters.AddWithValue("@EmailID", Data.EmailID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EmailID", DBNull.Value);
                            }
                            if (Data.PhoneNo != null)
                            {
                                cmd.Parameters.AddWithValue("@PhoneNo", Data.PhoneNo);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PhoneNo", DBNull.Value);
                            }
                            if (Data.Password != null)
                            {
                                cmd.Parameters.AddWithValue("@Password", Data.Password);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Password", DBNull.Value);
                            }
                            if (Data.MaxVoidAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@MaxVoidAmount", Data.MaxVoidAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MaxVoidAmount", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_EmployeeMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_EmployeeMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_ProductMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_ProductMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_ProductMaster WHERE ProductID=" + Data.ProductID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ProductMaster SET ProductName=@ProductName,UPCCode=@UPCCode,CertCode=@CertCode,DepartmentID=@DepartmentID,SectionID=@SectionID,UnitMeasureID=@UnitMeasureID,Price=@Price,TaxGroupID=@TaxGroupID," +
                                "IsFoodStamp=@IsFoodStamp,AgeVerification=@AgeVerification,IsScaled=@IsScaled,TareWeight=@TareWeight,GroupQty=@GroupQty,GroupPrice=@GroupPrice,LinkedUPCCode=@LinkedUPCCode,LabeledPrice=@LabeledPrice,CaseQty=@CaseQty,CasePrice=@CasePrice,IsGroupPrice=@IsGroupPrice," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE ProductID=@ProductID;";
                            #region Parameters
                            //if (Data.ProductID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            //}
                            if (Data.ProductName != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductName", Data.ProductName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductName", DBNull.Value);
                            }
                            if (Data.UPCCode != null)
                            {
                                cmd.Parameters.AddWithValue("@UPCCode", Data.UPCCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UPCCode", DBNull.Value);
                            }
                            if (Data.CertCode != null)
                            {
                                cmd.Parameters.AddWithValue("@CertCode", Data.CertCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CertCode", DBNull.Value);
                            }
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.SectionID != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.Price != null)
                            {
                                cmd.Parameters.AddWithValue("@Price", Data.Price);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Price", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.Image != null)
                            {
                                cmd.Parameters.AddWithValue("@Image", Data.Image);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.AgeVerification != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVerification", Data.AgeVerification);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVerification", DBNull.Value);
                            }
                            if (Data.IsScaled != null)
                            {
                                cmd.Parameters.AddWithValue("@IsScaled", Data.IsScaled);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsScaled", DBNull.Value);
                            }
                            if (Data.TareWeight != null)
                            {
                                cmd.Parameters.AddWithValue("@TareWeight", Data.TareWeight);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TareWeight", DBNull.Value);
                            }
                            if (Data.GroupQty != null)
                            {
                                cmd.Parameters.AddWithValue("@GroupQty", Data.GroupQty);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GroupQty", DBNull.Value);
                            }
                            if (Data.GroupPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@GroupPrice", Data.GroupPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GroupPrice", DBNull.Value);
                            }
                            if (Data.LinkedUPCCode != null)
                            {
                                cmd.Parameters.AddWithValue("@LinkedUPCCode", Data.LinkedUPCCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LinkedUPCCode", DBNull.Value);
                            }
                            if (Data.LabeledPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@LabeledPrice", Data.LabeledPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LabeledPrice", DBNull.Value);
                            }
                            if (Data.CaseQty != null)
                            {
                                cmd.Parameters.AddWithValue("@CaseQty", Data.CaseQty);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CaseQty", DBNull.Value);
                            }
                            if (Data.CasePrice != null)
                            {
                                cmd.Parameters.AddWithValue("@CasePrice", Data.CasePrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CasePrice", DBNull.Value);
                            }
                            if (Data.IsGroupPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@IsGroupPrice", Data.IsGroupPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsGroupPrice", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_ProductMaster(ProductID,ProductName,UPCCode,CertCode,DepartmentID,SectionID,UnitMeasureID,Price,TaxGroupID,IsFoodStamp,AgeVerification,IsScaled,TareWeight,GroupQty,GroupPrice,LinkedUPCCode,LabeledPrice,CaseQty,CasePrice,IsGroupPrice,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@ProductID,@ProductName,@UPCCode,@CertCode,@DepartmentID,@SectionID,@UnitMeasureID,@Price,@TaxGroupID,@IsFoodStamp,@AgeVerification,@IsScaled,@TareWeight,@GroupQty,@GroupPrice,@LinkedUPCCode,@LabeledPrice,@CaseQty,@CasePrice,@IsGroupPrice,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            #region Parameters
                            //if (Data.ProductID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            //}
                            if (Data.ProductName != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductName", Data.ProductName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductName", DBNull.Value);
                            }
                            if (Data.UPCCode != null)
                            {
                                cmd.Parameters.AddWithValue("@UPCCode", Data.UPCCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UPCCode", DBNull.Value);
                            }
                            if (Data.CertCode != null)
                            {
                                cmd.Parameters.AddWithValue("@CertCode", Data.CertCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CertCode", DBNull.Value);
                            }
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.SectionID != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.Price != null)
                            {
                                cmd.Parameters.AddWithValue("@Price", Data.Price);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Price", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.Image != null)
                            {
                                cmd.Parameters.AddWithValue("@Image", Data.Image);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.AgeVerification != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVerification", Data.AgeVerification);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVerification", DBNull.Value);
                            }
                            if (Data.IsScaled != null)
                            {
                                cmd.Parameters.AddWithValue("@IsScaled", Data.IsScaled);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsScaled", DBNull.Value);
                            }
                            if (Data.TareWeight != null)
                            {
                                cmd.Parameters.AddWithValue("@TareWeight", Data.TareWeight);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TareWeight", DBNull.Value);
                            }
                            if (Data.GroupQty != null)
                            {
                                cmd.Parameters.AddWithValue("@GroupQty", Data.GroupQty);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GroupQty", DBNull.Value);
                            }
                            if (Data.GroupPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@GroupPrice", Data.GroupPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GroupPrice", DBNull.Value);
                            }
                            if (Data.LinkedUPCCode != null)
                            {
                                cmd.Parameters.AddWithValue("@LinkedUPCCode", Data.LinkedUPCCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LinkedUPCCode", DBNull.Value);
                            }
                            if (Data.LabeledPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@LabeledPrice", Data.LabeledPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@LabeledPrice", DBNull.Value);
                            }
                            if (Data.CaseQty != null)
                            {
                                cmd.Parameters.AddWithValue("@CaseQty", Data.CaseQty);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CaseQty", DBNull.Value);
                            }
                            if (Data.CasePrice != null)
                            {
                                cmd.Parameters.AddWithValue("@CasePrice", Data.CasePrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CasePrice", DBNull.Value);
                            }
                            if (Data.IsGroupPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@IsGroupPrice", Data.IsGroupPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsGroupPrice", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_ProductMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_ProductMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_ProductSalePriceMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductSalePriceMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {
                    foreach (tbl_ProductSalePriceMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_ProductSalePriceMaster WHERE ProductSalePriceID=" + Data.ProductSalePriceID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ProductSalePriceMaster SET ProductID=@ProductID,SellPrice=@SellPrice,StartDate=@StartDate,EndDate=@EndDate," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE ProductSalePriceID=@ProductSalePriceID;";
                            #region Parameters
                            if (Data.ProductSalePriceID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductSalePriceID", Data.ProductSalePriceID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductSalePriceID", DBNull.Value);
                            }
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.SellPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@SellPrice", Data.SellPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SellPrice", DBNull.Value);
                            }
                            if (Data.StartDate != null)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", Data.StartDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                            }
                            if (Data.EndDate != null)
                            {
                                cmd.Parameters.AddWithValue("@EndDate", Data.EndDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                            }

                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_ProductSalePriceMaster(ProductSalePriceID,ProductID,SellPrice,StartDate,EndDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@ProductSalePriceID,@ProductID,@SellPrice,@StartDate,@EndDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            if (Data.ProductSalePriceID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductSalePriceID", Data.ProductSalePriceID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductSalePriceID", DBNull.Value);
                            }
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.SellPrice != null)
                            {
                                cmd.Parameters.AddWithValue("@SellPrice", Data.SellPrice);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SellPrice", DBNull.Value);
                            }
                            if (Data.StartDate != null)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", Data.StartDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                            }
                            if (Data.EndDate != null)
                            {
                                cmd.Parameters.AddWithValue("@EndDate", Data.EndDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                            }

                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_ProductSalePriceMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_ProductSalePriceMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_RoleMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_RoleMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_RoleMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_RoleMaster WHERE RoleID=" + Data.RoleID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_RoleMaster SET RoleID=@RoleID,RoleType=@RoleType," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE RoleID=@RoleID;";
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.RoleType != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleType", Data.RoleType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleType", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_RoleMaster(RoleID,RoleType,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@RoleID,@RoleType,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.RoleType != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleType", Data.RoleType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleType", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_RoleMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_RoleMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_RolePermission()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_RolePermission.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_RolePermission Data in tbl_Data)
                    {
                        string query = "Select * from tbl_RolePermission WHERE PermissionID=" + Data.PermissionID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_RolePermission SET PermissionID=@PermissionID,RoleID=@RoleID," +
                                "MenuID=@MenuID,ViewRecords=@ViewRecords,InsertRecords=@InsertRecords,EditRecords=@EditRecords,DeleteRecords=@DeleteRecords " +
                                "WHERE PermissionID=@PermissionID;";
                            #region Parameters
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@PermissionID", Data.PermissionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PermissionID", DBNull.Value);
                            }
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.MenuID != null)
                            {
                                cmd.Parameters.AddWithValue("@MenuID", Data.MenuID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MenuID", DBNull.Value);
                            }
                            if (Data.ViewRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@ViewRecords", Data.ViewRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ViewRecords", DBNull.Value);
                            }
                            if (Data.InsertRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@InsertRecords", Data.InsertRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@InsertRecords", DBNull.Value);
                            }
                            if (Data.EditRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@EditRecords", Data.EditRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EditRecords", DBNull.Value);
                            }
                            if (Data.DeleteRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@DeleteRecords", Data.DeleteRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DeleteRecords", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_RolePermission(PermissionID,RoleID,MenuID,ViewRecords,InsertRecords,EditRecords,DeleteRecords) " +
                                "VALUES(@PermissionID,@RoleID,@MenuID,@ViewRecords,@InsertRecords,@EditRecords,@DeleteRecords)";
                            #region Parameters
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@PermissionID", Data.PermissionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PermissionID", DBNull.Value);
                            }
                            if (Data.RoleID != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            }
                            if (Data.MenuID != null)
                            {
                                cmd.Parameters.AddWithValue("@MenuID", Data.MenuID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MenuID", DBNull.Value);
                            }
                            if (Data.ViewRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@ViewRecords", Data.ViewRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ViewRecords", DBNull.Value);
                            }
                            if (Data.InsertRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@InsertRecords", Data.InsertRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@InsertRecords", DBNull.Value);
                            }
                            if (Data.EditRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@EditRecords", Data.EditRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EditRecords", DBNull.Value);
                            }
                            if (Data.DeleteRecords != null)
                            {
                                cmd.Parameters.AddWithValue("@DeleteRecords", Data.DeleteRecords);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DeleteRecords", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_RolePermission");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_RolePermission" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_SectionMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_SectionMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_SectionMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_SectionMaster WHERE SectionID=" + Data.SectionID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_SectionMaster SET SectionID=@SectionID,DepartmentID=@DepartmentID,SectionName=@SectionName," +
                                "IsFoodStamp = @IsFoodStamp,TaxGroupID = @TaxGroupID,UnitMeasureID = @UnitMeasureID,AgeVarificationAge = @AgeVarificationAge," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE SectionID=@SectionID;";
                            #region Parameters
                            if (Data.SectionID != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            }
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.SectionName != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionName", Data.SectionName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionName", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_SectionMaster(SectionID,DepartmentID,SectionName,IsFoodStamp,TaxGroupID,UnitMeasureID,AgeVarificationAge,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@SectionID,@DepartmentID,@SectionName,@IsFoodStamp,@TaxGroupID,@UnitMeasureID,@AgeVarificationAge,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            if (Data.SectionID != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            }
                            if (Data.DepartmentID != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            }
                            if (Data.SectionName != null)
                            {
                                cmd.Parameters.AddWithValue("@SectionName", Data.SectionName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SectionName", DBNull.Value);
                            }
                            if (Data.IsFoodStamp != null)
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", Data.IsFoodStamp);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsFoodStamp", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_SectionMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_SectionMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_ShortcutkeyMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ShortcutkeyMaster.ToList();//Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_ShortcutkeyMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_ShortcutkeyMaster WHERE ShortcutKeyID=" + Data.ShortcutKeyID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ShortcutkeyMaster SET ShortcutKeyID=@ShortcutKeyID,ShortcutKey=@ShortcutKey,Description=@Description " +
                                "WHERE ShortcutKeyID=@ShortcutKeyID;";
                            #region Parameters
                            if (Data.ShortcutKeyID != null)
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKeyID", Data.ShortcutKeyID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKeyID", DBNull.Value);
                            }
                            if (Data.ShortcutKey != null)
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKey", Data.ShortcutKey);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKey", DBNull.Value);
                            }
                            if (Data.Description != null)
                            {
                                cmd.Parameters.AddWithValue("@Description", Data.Description);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_ShortcutkeyMaster(ShortcutKeyID,ShortcutKey,Description) " +
                                "VALUES(@ShortcutKeyID,@ShortcutKey,@Description)";
                            #region Parameters
                            if (Data.ShortcutKeyID != null)
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKeyID", Data.ShortcutKeyID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKeyID", DBNull.Value);
                            }
                            if (Data.ShortcutKey != null)
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKey", Data.ShortcutKey);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ShortcutKey", DBNull.Value);
                            }
                            if (Data.Description != null)
                            {
                                cmd.Parameters.AddWithValue("@Description", Data.Description);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_ShortcutkeyMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_ShortcutkeyMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_StoreMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_StoreMaster.ToList();

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_StoreMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_StoreMaster WHERE StoreID=" + Data.StoreID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_StoreMaster SET StoreName=@StoreName,Address=@Address,Address2=@Address2,Phone=@Phone,"
                                + "Fax=@Fax,City=@City,State=@State,Country=@Country,ZipCode=@ZipCode,AgeVarificationAge=@AgeVarificationAge,Disclaimer=@Disclaimer,DefaultTax=@DefaultTax,IsStoreTax=@IsStoreTax,"
                                + "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy "
                                + "WHERE StoreID=@StoreID;";
                            #region Parameters
                            if (Data.StoreID != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            }
                            if (Data.StoreName != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreName", Data.StoreName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreName", DBNull.Value);
                            }
                            if (Data.Address != null)
                            {
                                cmd.Parameters.AddWithValue("@Address", Data.Address);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Address", DBNull.Value);
                            }
                            if (Data.Address2 != null)
                            {
                                cmd.Parameters.AddWithValue("@Address2", Data.Address2);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Address2", DBNull.Value);
                            }
                            if (Data.Phone != null)
                            {
                                cmd.Parameters.AddWithValue("@Phone", Data.Phone);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                            }
                            if (Data.Fax != null)
                            {
                                cmd.Parameters.AddWithValue("@Fax", Data.Fax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Fax", DBNull.Value);
                            }
                            if (Data.City != null)
                            {
                                cmd.Parameters.AddWithValue("@City", Data.City);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@City", DBNull.Value);
                            }
                            if (Data.State != null)
                            {
                                cmd.Parameters.AddWithValue("@State", Data.State);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@State", DBNull.Value);
                            }
                            if (Data.Country != null)
                            {
                                cmd.Parameters.AddWithValue("@Country", Data.Country);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Country", DBNull.Value);
                            }
                            if (Data.ZipCode != null)
                            {
                                cmd.Parameters.AddWithValue("@ZipCode", Data.ZipCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ZipCode", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.Disclaimer != null)
                            {
                                cmd.Parameters.AddWithValue("@Disclaimer", Data.Disclaimer);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Disclaimer", DBNull.Value);
                            }
                            if (Data.DefaultTax != null)
                            {
                                cmd.Parameters.AddWithValue("@DefaultTax", Data.DefaultTax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DefaultTax", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            if (Data.IsStoreTax != null)
                            {
                                cmd.Parameters.AddWithValue("@IsStoreTax", Data.IsStoreTax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsStoreTax", DBNull.Value);
                            }

                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_StoreMaster(StoreID,StoreName,Address,Address2,Phone,Fax,City,State,Country,ZipCode,AgeVarificationAge,Disclaimer,DefaultTax,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsStoreTax) " +
                                 "VALUES(@StoreID,@StoreName,@Address,@Address2,@Phone,@Fax,@City,@State,@Country,@ZipCode,@AgeVarificationAge,@Disclaimer,@DefaultTax,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy,@IsStoreTax)";
                            #region Parameters
                            if (Data.StoreID != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            }
                            if (Data.StoreName != null)
                            {
                                cmd.Parameters.AddWithValue("@StoreName", Data.StoreName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StoreName", DBNull.Value);
                            }
                            if (Data.Address != null)
                            {
                                cmd.Parameters.AddWithValue("@Address", Data.Address);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Address", DBNull.Value);
                            }
                            if (Data.Address2 != null)
                            {
                                cmd.Parameters.AddWithValue("@Address2", Data.Address2);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Address2", DBNull.Value);
                            }
                            if (Data.Phone != null)
                            {
                                cmd.Parameters.AddWithValue("@Phone", Data.Phone);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                            }
                            if (Data.Fax != null)
                            {
                                cmd.Parameters.AddWithValue("@Fax", Data.Fax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Fax", DBNull.Value);
                            }
                            if (Data.City != null)
                            {
                                cmd.Parameters.AddWithValue("@City", Data.City);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@City", DBNull.Value);
                            }
                            if (Data.State != null)
                            {
                                cmd.Parameters.AddWithValue("@State", Data.State);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@State", DBNull.Value);
                            }
                            if (Data.Country != null)
                            {
                                cmd.Parameters.AddWithValue("@Country", Data.Country);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Country", DBNull.Value);
                            }
                            if (Data.ZipCode != null)
                            {
                                cmd.Parameters.AddWithValue("@ZipCode", Data.ZipCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ZipCode", DBNull.Value);
                            }
                            if (Data.AgeVarificationAge != null)
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", Data.AgeVarificationAge);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@AgeVarificationAge", DBNull.Value);
                            }
                            if (Data.Disclaimer != null)
                            {
                                cmd.Parameters.AddWithValue("@Disclaimer", Data.Disclaimer);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Disclaimer", DBNull.Value);
                            }
                            if (Data.DefaultTax != null)
                            {
                                cmd.Parameters.AddWithValue("@DefaultTax", Data.DefaultTax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DefaultTax", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            if (Data.IsStoreTax != null)
                            {
                                cmd.Parameters.AddWithValue("@IsStoreTax", Data.IsStoreTax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsStoreTax", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_StoreMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_StoreMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_TaxGroupMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_TaxGroupMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_TaxGroupMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_TaxGroupMaster WHERE TaxGroupID=" + Data.TaxGroupID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_TaxGroupMaster SET TaxGroupID=@TaxGroupID,TaxGroupName=@TaxGroupName," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE TaxGroupID=@TaxGroupID;";
                            #region Parameters
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.TaxGroupName != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", Data.TaxGroupName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_TaxGroupMaster(TaxGroupID,TaxGroupName,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@TaxGroupID,@TaxGroupName,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.TaxGroupName != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", Data.TaxGroupName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_TaxGroupMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_TaxGroupMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_TaxRateMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_TaxRateMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_TaxRateMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_TaxRateMaster WHERE TaxRateID=" + Data.TaxRateID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_TaxRateMaster SET TaxGroupID=@TaxGroupID,Tax=@Tax,StartDate=@StartDate,EndDate=@EndDate," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE TaxRateID=@TaxRateID;";
                            #region Parameters
                            if (Data.TaxRateID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxRateID", Data.TaxRateID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxRateID", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.Tax != null)
                            {
                                cmd.Parameters.AddWithValue("@Tax", Data.Tax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Tax", DBNull.Value);
                            }
                            if (Data.StartDate != null)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", Data.StartDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                            }
                            if (Data.EndDate != null)
                            {
                                cmd.Parameters.AddWithValue("@EndDate", Data.EndDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                            }

                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_TaxRateMaster(TaxRateID,TaxGroupID,Tax,StartDate,EndDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@TaxRateID,@TaxGroupID,@Tax,@StartDate,@EndDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            if (Data.TaxRateID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxRateID", Data.TaxRateID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxRateID", DBNull.Value);
                            }
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            if (Data.Tax != null)
                            {
                                cmd.Parameters.AddWithValue("@Tax", Data.Tax);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Tax", DBNull.Value);
                            }
                            if (Data.StartDate != null)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", Data.StartDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                            }
                            if (Data.EndDate != null)
                            {
                                cmd.Parameters.AddWithValue("@EndDate", Data.EndDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                            }

                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_TaxRateMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_TaxRateMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_UnitMeasureMaster()
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_UnitMeasureMaster.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {

                    foreach (tbl_UnitMeasureMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_UnitMeasureMaster WHERE UnitMeasureID=" + Data.UnitMeasureID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_UnitMeasureMaster SET UnitMeasureID=@UnitMeasureID,UnitMeasureCode=@UnitMeasureCode,Description=@Description," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE UnitMeasureID=@UnitMeasureID;";
                            #region Parameters
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.UnitMeasureCode != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureCode", Data.UnitMeasureCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureCode", DBNull.Value);
                            }
                            if (Data.Description != null)
                            {
                                cmd.Parameters.AddWithValue("@Description", Data.Description);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "INSERT INTO tbl_UnitMeasureMaster(UnitMeasureID,UnitMeasureCode,Description,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                "VALUES(@UnitMeasureID,@UnitMeasureCode,@Description,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.UnitMeasureCode != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureCode", Data.UnitMeasureCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureCode", DBNull.Value);
                            }
                            if (Data.Description != null)
                            {
                                cmd.Parameters.AddWithValue("@Description", Data.Description);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                            }
                            if (Data.IsActive != null)
                            {
                                cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            }
                            if (Data.IsDelete != null)
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            }
                            if (Data.CreatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            }
                            if (Data.CreatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            }
                            if (Data.UpdatedDate != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            }
                            if (Data.UpdatedBy != null)
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            }
                            #endregion
                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                    }

                }
                ChangeSyncStatus("tbl_UnitMeasureMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_UnitMeasureMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
    }
}
