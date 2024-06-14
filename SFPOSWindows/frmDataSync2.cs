using MetroFramework.Forms;
using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows
{
    public partial class frmDataSync2 : MetroForm
    {
        public static SqlCeDataAdapter DataAdapter = null;
        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
        OrderScannerService _OrderScannerService = new OrderScannerService();
        ProductLedgerService _ProductLedgerService = new ProductLedgerService();
        TransSuspendService _TransSuspendService = new TransSuspendService();
        PaymentTransService _PaymentTransService = new PaymentTransService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        LottoService _LottoService = new LottoService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        DateTime _LastSyncDateTime;
        List<OrderMasterModel> lstOrderMasterModel = new List<OrderMasterModel>();
        List<OrderDetailmasterModel> lstOrderDetailModel = new List<OrderDetailmasterModel>();
        List<ProductLedgerMasterModel> lstProductLedgerMasterModel = new List<ProductLedgerMasterModel>();
        static public int time = 0;
        static public int TableCount = 0;
        static public int temptime = 0;
        int isError = 1;
        public bool UpdateLocalDB = false;

        public frmDataSync2()
        {
            InitializeComponent();
        }
        private void frmDataSync_Load(object sender, EventArgs e)
        {
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    Functions.SetIcon(this);
            //}
            this.BringToFront();
            timer1.Interval = 1;
            timer1.Start();
            //sync();
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //ClsCommon.MsgBox("Information","Full Sync Started");
                //lblStatus.Text = "Checking Updated Data...";
                ////System.Threading.Thread.Sleep(5000);
                if (LoginInfo.SyncType == 2)
                {
                    if(UpdateLocalDB)
                    {
                        UpdateLocalDB _updateDB = new UpdateLocalDB();
                        _updateDB.UpdateLocalDatabase(ClsCommon.SqlCeConn);
                    }
                    LiveToLocalSync();

                    LocalToLiveSync();
                }
            }
            catch (Exception ex)
            {
                isError = 0;
                _ExceptionLogService.AddExceptionLog(e.GetType().Name, ex.Message, "frmDataSync2-backgroundWorker1_DoWork" + ex.StackTrace, ex.LineNumber());
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                //ClsCommon.MsgBox("Information","Full Sync Completed");
                if (LoginInfo.SyncType == 2)
                {
                    TableCount = 0;
                    //Hide();
                }
            }
            catch (Exception ex)
            {
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "frmDataSync2-backgroundWorker1_RunWorkerCompleted" + ex.StackTrace, ex.LineNumber());
            }
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TableCount > 0)
            {
                time = 100 / TableCount;
            }
            else
            {
                time = 100;
            }
            if (temptime <= time)
            {
                temptime = temptime + 1;
            }
            progressBar.Value = temptime;
            if (temptime < 101)
                lblPre.Invoke((Action)(() => lblPre.Text = temptime + " % "));
            if (progressBar.Value == 100)
            {
                if (TableCount == 0)
                {
                    time = 0;
                    temptime = 0;
                    timer1.Stop();
                    Hide();
                }
            }
        }
        public void ChangeSyncStatus(string TblName)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var UpdatedTableName = _db.tbl_UpdateLog.Where(x => x.TblName == TblName && x.IsSync == false && x.MacAddress == LoginInfo.MacAddress).ToList();
                foreach (tbl_UpdateLog tbl in UpdatedTableName)
                {
                    tbl.IsSync = true;
                    tbl.IsChange = false;
                    tbl.SyncDate = DateTime.Now;
                    tbl.UpdatedDate = DateTime.Now;
                }
                _db.SaveChanges();
                TableCount--;
            }
            catch(Exception ex)
            {
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "frmDataSync2-ChangeSyncStatus" + ex.StackTrace, ex.LineNumber());
            }

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
            catch (Exception ex)
            {
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "frmDataSync2-db_Connection()" + ex.StackTrace, ex.LineNumber());
                return false;
            }
        }
        public void LiveToLocalSync()
        {
            try
            {
                bool IsConnet = CheckConnection();
                if (IsConnet)
                {
                    #region Get LastSync Datetime
                    //ClsCommon.MsgBox("Information", LoginInfo.CounterIP, false);
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

                    var LastSync = _db.tbl_SyncLog.OrderByDescending(x => x.SyncLogID).Where(x => x.MacAddress == LoginInfo.MacAddress || x.CounterIP == LoginInfo.CounterIP).FirstOrDefault();
                    //var LastSync = _db.tbl_SyncLog.OrderByDescending(x => x.SyncLogID).Where(x => LoginInfo.lstMacAddress.Contains(x.MacAddress) || LoginInfo.lstMacAddress.Contains(x.CounterIP )).FirstOrDefault();
                    if (LastSync != null)
                    {
                        _LastSyncDateTime = Convert.ToDateTime(LastSync.SyncDateTime);

                        LastSync.SyncDateTime = DateTime.Now;
                        LastSync.UpdatedBy = LoginInfo.UserId;
                        LastSync.UpdatedDate = DateTime.Now;
                        _db.SaveChanges();


                        #region Get Updated Table Name
                        //var UpdatedTableName = _db.tbl_UpdateLog.Where(x => (x.MacAddress == LoginInfo.MacAddress || LoginInfo.lstMacAddress.Contains(x.MacAddress)) && x.IsSync == false).ToList();
                        var UpdatedTableName = _db.tbl_UpdateLog.Where(x => (x.MacAddress == LoginInfo.MacAddress || x.CounterIP == LoginInfo.CounterIP) && x.IsSync == false).ToList();
                        TableCount = UpdatedTableName.Count;
                        foreach (tbl_UpdateLog data in UpdatedTableName)
                        {
                            string tbl = data.TblName;
                            DateTime? SyncTime = data.SyncDate;
                            switch (tbl)
                            {
                                case "tbl_DepartmentMaster":
                                    tbl_DepartmentMaster(SyncTime);
                                    break;
                                case "tbl_EmployeeMaster":
                                    tbl_EmployeeMaster(SyncTime);
                                    break;
                                case "tbl_ProductMaster":
                                    tbl_ProductMaster(SyncTime);
                                    break;
                                case "tbl_ProductSaleDiscountMaster":
                                    tbl_ProductSaleDiscountMaster(SyncTime);
                                    break;
                                case "tbl_ProductSalePriceMaster":
                                    tbl_ProductSalePriceMaster(SyncTime);
                                    break;
                                case "tbl_ProductUoM":
                                    tbl_ProductUoM(SyncTime);
                                    break;
                                case "tbl_RoleMaster":
                                    tbl_RoleMaster(SyncTime);
                                    break;
                                case "tbl_SectionMaster":
                                    tbl_SectionMaster(SyncTime);
                                    break;
                                case "tbl_ShortcutkeyMaster":
                                    tbl_ShortcutkeyMaster(SyncTime);
                                    break;
                                case "tbl_StoreMaster":
                                    tbl_StoreMaster(SyncTime);
                                    break;
                                case "tbl_TaxGroupMaster":
                                    tbl_TaxGroupMaster(SyncTime);
                                    break;
                                case "tbl_TaxRateMaster":
                                    tbl_TaxRateMaster(SyncTime);
                                    break;
                                case "tbl_UnitMeasureMaster":
                                    tbl_UnitMeasureMaster(SyncTime);
                                    break;
                                case "tbl_RolePermission":
                                    tbl_RolePermission(SyncTime);
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }
                        }
                        #endregion
                    }


                    #endregion
                    else
                    {
                        ClsCommon.MsgBox("Information", "Please sync first with full data - " + Convert.ToString(LoginInfo.MacAddress), false);
                    }

                    tbl_RolePermission();
                }

            }
            catch(Exception ex)
            {
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "frmDataSync2-LocalToLiveSync" + ex.StackTrace, ex.LineNumber());
            }
        
        }
        public void LocalToLiveSync()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                #region OrderMaster & OrderDetailsMaster & InsertProductLedger & InsertPaymentTrans 
                string query = "Select * from tbl_OrderMaster";
                DataTable dt_i = new DataTable();
                DataAdapter = new SqlCeDataAdapter(query, conn);
                DataAdapter.Fill(dt_i);
                if (dt_i.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_i.Rows.Count; i++)
                    {
                        DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                        LoginInfo.LocalSync = true;

                        #region InsertOrder
                        OrderMasterModel objOrderMasterModel = new OrderMasterModel();
                        objOrderMasterModel.OrderID = Functions.GetLong(dt_i.Rows[i]["OrderID"].ToString());
                        objOrderMasterModel.CustomerID = Functions.GetLong(dt_i.Rows[i]["CustomerID"].ToString());
                        objOrderMasterModel.PaymentMethodID = Functions.GetLong(dt_i.Rows[i][OrderScanner_ResultModelCont.PaymentMethodID].ToString());
                        objOrderMasterModel.StoreID = Functions.GetLong(dt_i.Rows[i]["StoreID"].ToString());
                        objOrderMasterModel.TotalAmount = Functions.GetDecimal(dt_i.Rows[i]["TotalAmount"].ToString());
                        //objOrderMasterModel.DiscountAmount = Functions.GetDecimal(dt_i.Rows[i]["DiscountAmount"].ToString());
                        objOrderMasterModel.TaxAmount = Functions.GetDecimal(dt_i.Rows[i]["TaxAmount"].ToString());
                        objOrderMasterModel.GrossAmount = Functions.GetDecimal(dt_i.Rows[i]["GrossAmount"].ToString());
                        objOrderMasterModel.CashAmount = Functions.GetDecimal(dt_i.Rows[i]["CashAmount"].ToString());
                        objOrderMasterModel.CreditCardAmount = Functions.GetDecimal(dt_i.Rows[i]["CreditCardAmount"].ToString());
                        objOrderMasterModel.ChangeAmount = Functions.GetDecimal(dt_i.Rows[i]["ChangeAmount"].ToString());
                        objOrderMasterModel.CheckAmount = Functions.GetDecimal(dt_i.Rows[i]["CheckAmount"].ToString());
                        objOrderMasterModel.FoodStampAmount = Functions.GetDecimal(dt_i.Rows[i]["FoodStampAmount"].ToString());
                        objOrderMasterModel.Balance = Functions.GetDecimal(dt_i.Rows[i]["Balance"].ToString());
                        objOrderMasterModel.RefundAmount = Functions.GetDecimal(dt_i.Rows[i]["RefundAmount"].ToString());
                        objOrderMasterModel.CardNumber = dt_i.Rows[i]["CardNumber"].ToString();
                        objOrderMasterModel.Status = dt_i.Rows[i]["Status"].ToString();
                        objOrderMasterModel.CreatedDate = Convert.ToDateTime(dt_i.Rows[i]["CreatedDate"].ToString());
                        objOrderMasterModel.CreatedBy = Functions.GetLong(dt_i.Rows[i]["CreatedBy"].ToString());
                        objOrderMasterModel.CounterIP = dt_i.Rows[i]["CounterIP"].ToString();
                        objOrderMasterModel.OrdNo = dt_i.Rows[i]["OrdNo"].ToString();
                        objOrderMasterModel.CouponCode = "";
                        objOrderMasterModel.CouponDiscAmt = 0;
                        objOrderMasterModel.SyncDate = DateTime.Now;
                        objOrderMasterModel.IsCancel = Functions.GetBoolean(dt_i.Rows[i]["IsCancel"].ToString());
                        objOrderMasterModel.TaxableAmount = Functions.GetDecimal(dt_i.Rows[i]["TaxableAmount"].ToString());
                        objOrderMasterModel.TaxExempted = Functions.GetDecimal(dt_i.Rows[i]["TaxExempted"].ToString());
                        objOrderMasterModel.IsTaxCarry = Functions.GetBoolean(dt_i.Rows[i]["IsTaxCarry"].ToString());
                        objOrderMasterModel.ReturnAmount = Functions.GetDecimal(dt_i.Rows[i]["ReturnAmount"].ToString());
                        objOrderMasterModel.OverridePrice = Functions.GetDecimal(dt_i.Rows[i]["OverridePriceTotal"].ToString());
                        //var addOrder = _OrderScannerService.AddOrder(objOrderMasterModel, 1);

                        ///////// Order Master ////////
                        lstOrderMasterModel = new List<OrderMasterModel>();
                        lstOrderMasterModel.Add(objOrderMasterModel);
                        #endregion

                        lstOrderDetailModel = new List<OrderDetailmasterModel>();
                        lstProductLedgerMasterModel = new List<ProductLedgerMasterModel>();

                        #region InsertOrderDetail
                        query = "SELECT * FROM tbl_OrderDetail WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                        DataAdapter = new SqlCeDataAdapter(query, conn);
                        DataTable dt_j = new DataTable();
                        DataAdapter.Fill(dt_j);
                        var ordercnt = dt_j.Rows.Count;
                        if (dt_j != null && dt_j.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt_j.Rows.Count; j++)
                            {
                                OrderDetailmasterModel objOrderDetailmasterModel = new OrderDetailmasterModel();
                                objOrderDetailmasterModel.OrderID = LoginInfo.LastOrderID;//addOrder.OrderID;
                                objOrderDetailmasterModel.ProductID = Functions.GetLong(dt_j.Rows[j]["ProductID"].ToString());
                                objOrderDetailmasterModel.UPCCode = dt_j.Rows[j]["UPCCode"].ToString();
                                objOrderDetailmasterModel.ProductName = dt_j.Rows[j]["ProductName"].ToString();
                                objOrderDetailmasterModel.Quantity = Functions.GetDecimal(dt_j.Rows[j]["Quantity"].ToString());
                                objOrderDetailmasterModel.DepartmentID = Functions.GetLong(dt_j.Rows[j]["DepartmentID"].ToString());
                                objOrderDetailmasterModel.SectionID = Functions.GetLong(dt_j.Rows[j]["SectionID"].ToString());
                                objOrderDetailmasterModel.SellPrice = Functions.GetDecimal(dt_j.Rows[j]["SellPrice"].ToString());
                                objOrderDetailmasterModel.Discount = Functions.GetDecimal(dt_j.Rows[j]["Discount"].ToString());
                                objOrderDetailmasterModel.finalPrice = Functions.GetDecimal(dt_j.Rows[j]["finalPrice"].ToString());
                                objOrderDetailmasterModel.StoreID = Functions.GetLong(dt_j.Rows[j]["StoreID"].ToString());
                                objOrderDetailmasterModel.IsScale = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsScale].ToString());
                                objOrderDetailmasterModel.IsFoodStamp = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsFoodStamp].ToString());
                                objOrderDetailmasterModel.IsTax = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsTax].ToString());
                                objOrderDetailmasterModel.FoodStampTotal = Functions.GetDecimal(dt_j.Rows[j]["FoodStampTotal"].ToString());
                                objOrderDetailmasterModel.DiscountApplyed = Functions.GetBoolean(dt_j.Rows[j]["DiscountApplyed"].ToString());
                                objOrderDetailmasterModel.TaxAmount = Functions.GetDecimal(dt_j.Rows[j]["TaxAmount"].ToString());
                                objOrderDetailmasterModel.IsRefund = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.IsRefund].ToString());
                                objOrderDetailmasterModel.IsCancel = Functions.GetBoolean(dt_j.Rows[j]["IsCancel"].ToString());
                                objOrderDetailmasterModel.IsForceTax = Functions.GetBoolean(dt_j.Rows[j]["IsForceTax"].ToString());
                                objOrderDetailmasterModel.CreatedBy = Functions.GetLong(dt_j.Rows[j]["CreatedBy"].ToString());
                                objOrderDetailmasterModel.CreatedDate = Convert.ToDateTime(dt_j.Rows[j]["CreatedDate"].ToString());
                                objOrderDetailmasterModel.CasePriceApplied = Functions.GetBoolean(dt_j.Rows[j][OrderScanner_ResultModelCont.CasePriceApplied].ToString());
                                objOrderDetailmasterModel.GroupPrice = Functions.GetDecimal(dt_j.Rows[j]["GroupPrice"].ToString());
                                objOrderDetailmasterModel.GroupQty = Functions.GetDecimal(dt_j.Rows[j]["GroupQty"].ToString());
                                objOrderDetailmasterModel.CasePrice = Functions.GetDecimal(dt_j.Rows[j]["CasePrice"].ToString());
                                objOrderDetailmasterModel.CaseQty = Functions.GetDecimal(dt_j.Rows[j]["CaseQty"].ToString());
                                objOrderDetailmasterModel.IsTaxCarry = Functions.GetBoolean(dt_i.Rows[i]["IsTaxCarry"].ToString());
                                objOrderDetailmasterModel.IsReturn = Functions.GetBoolean(dt_j.Rows[j]["IsReturn"].ToString());
                                objOrderDetailmasterModel.OverridePrice = Functions.GetDecimal(dt_j.Rows[j]["OverridePrice"].ToString());
                                objOrderDetailmasterModel.IsManWTRefund = Functions.GetBoolean(dt_j.Rows[j]["ManWT"].ToString());
                                //objOrderDetailmasterModel = _OrderScannerService.AddOrderDetail(objOrderDetailmasterModel, 1);
                                lstOrderDetailModel.Add(objOrderDetailmasterModel);
                            }
                        }
                        #endregion

                        #region InsertProductLedger

                        query = "SELECT * FROM tbl_ProductLedger WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                        DataAdapter = new SqlCeDataAdapter(query, conn);
                        DataTable dt_k = new DataTable();
                        DataAdapter.Fill(dt_k);

                        if (dt_k != null && dt_k.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt_k.Rows.Count; k++)
                            {
                                ProductLedgerMasterModel objProductLedgerMasterModel = new ProductLedgerMasterModel();
                                objProductLedgerMasterModel.ProductID = Functions.GetLong(dt_k.Rows[k]["ProductID"].ToString());
                                objProductLedgerMasterModel.LedgerTypeID = 2;
                                objProductLedgerMasterModel.OrderID = LoginInfo.LastOrderID;//addOrder.OrderID;
                                objProductLedgerMasterModel.OrderLineID = 0;//Functions.GetLong(dt_k.Rows[k]["OrderLineID"].ToString());
                                objProductLedgerMasterModel.PostedPurchaseHeaderID = Functions.GetLong(dt_k.Rows[k]["PostedPurchaseHeaderID"].ToString());
                                objProductLedgerMasterModel.TaxGroupCodeID = Functions.GetLong(dt_k.Rows[k]["TaxGroupCodeID"].ToString());
                                objProductLedgerMasterModel.DepartmentID = Functions.GetLong(dt_k.Rows[k]["DepartmentID"].ToString());
                                objProductLedgerMasterModel.SectionID = Functions.GetLong(dt_k.Rows[k]["SectionID"].ToString());
                                objProductLedgerMasterModel.UnitOfMeasureID = Functions.GetLong(dt_k.Rows[k]["UnitOfMeasureID"].ToString());
                                objProductLedgerMasterModel.UPCCode = dt_k.Rows[k]["UPCCode"].ToString();
                                objProductLedgerMasterModel.QRCode = dt_k.Rows[k]["QRCode"].ToString();
                                objProductLedgerMasterModel.Qty = Functions.GetDecimal(dt_k.Rows[k]["Qty"].ToString());
                                objProductLedgerMasterModel.SellPrice = Functions.GetDecimal(dt_k.Rows[k]["SellPrice"].ToString());
                                objProductLedgerMasterModel.FinalPrice = Functions.GetDecimal(dt_k.Rows[k]["FinalPrice"].ToString());
                                objProductLedgerMasterModel.DiscountPrice = Functions.GetDecimal(dt_k.Rows[k]["DiscountPrice"].ToString());
                                objProductLedgerMasterModel.TaxAmount = Functions.GetDecimal(dt_k.Rows[k]["TaxAmount"].ToString());
                                objProductLedgerMasterModel.IsForceTax = Functions.GetBoolean(dt_k.Rows[k]["IsForceTax"].ToString());
                                objProductLedgerMasterModel.CreatedDate = Convert.ToDateTime(dt_k.Rows[k]["CreatedDate"].ToString());
                                objProductLedgerMasterModel.CreatedBy = Functions.GetLong(dt_k.Rows[k]["CreatedBy"].ToString());
                                objProductLedgerMasterModel.StoreID = Functions.GetLong(dt_k.Rows[k]["StoreID"].ToString());
                                objProductLedgerMasterModel.OverridePrice = Functions.GetDecimal(dt_k.Rows[k]["OverridePrice"].ToString());
                                //var add_ProductLedger = _ProductLedgerService.AddProductLedger(objProductLedgerMasterModel, 1);
                                lstProductLedgerMasterModel.Add(objProductLedgerMasterModel);
                                #region Update Reamining Qty

                                decimal Qty = Functions.GetDecimal(objProductLedgerMasterModel.Qty.ToString());
                                decimal _Qty = Qty;
                                decimal RemainingQty = 0;

                                for (int z = 0; Qty >= 0; z++)
                                {
                                    if (Qty >= 0)
                                    {
                                        var ProductLedger = (from PL in _db.tbl_ProductLedger.Where
                                                     (o => o.ProductID == objProductLedgerMasterModel.ProductID
                                                     && o.RemainingQty > 0 && o.LedgerTypeID == 1)
                                                             select new
                                                             {
                                                                 ProductLedgerID = PL.ProductLedgerID,
                                                                 RemainingQty = PL.RemainingQty
                                                             }).FirstOrDefault();
                                        if (ProductLedger != null)
                                        {
                                            RemainingQty = Functions.GetDecimal(ProductLedger.RemainingQty.ToString());
                                            _Qty = Qty - RemainingQty;
                                            if (_Qty > 0)
                                            {
                                                var updateContacts = from x in _db.tbl_ProductLedger
                                                                     where x.ProductLedgerID == ProductLedger.ProductLedgerID
                                                                     select x;
                                                foreach (var contact in updateContacts)
                                                    contact.RemainingQty = 0;
                                            }
                                            else
                                            {
                                                var updateContacts = from x in _db.tbl_ProductLedger
                                                                     where x.ProductLedgerID == ProductLedger.ProductLedgerID
                                                                     select x;
                                                foreach (var contact in updateContacts)
                                                    contact.RemainingQty = RemainingQty - Qty;
                                            }
                                            _db.SaveChanges();
                                            Qty = _Qty;
                                        }
                                        else
                                        {
                                            Qty = -1;
                                        }
                                    }
                                }
                                #endregion
                            }

                        }
                        #endregion

                        #region InsertNewOrder
                        ///// Insert Query for Order all 3 objects as parameter
                        DataTable dtOrderMaster = new DataTable("tblOrderMaster");
                        DataTable dtDetailMaster = new DataTable("tblDetailMaster");
                        DataTable dtProductLedger = new DataTable("tblProductLedger");
                        dtOrderMaster = ClsCommon.ListToDataTable(lstOrderMasterModel);
                        dtDetailMaster = ClsCommon.ListToDataTable(lstOrderDetailModel);
                        dtProductLedger = ClsCommon.ListToDataTable(lstProductLedgerMasterModel);

                        try
                        {
                            var conStr = XMLData.DbConnectionString;

                            using (SqlConnection con = new SqlConnection(conStr))
                            {
                                using (SqlCommand scmd = new SqlCommand("CRUDOrderMaster", con))
                                {
                                    scmd.CommandType = CommandType.StoredProcedure;

                                    scmd.Parameters.AddWithValue("@dtOrderMaster", dtOrderMaster);
                                    scmd.Parameters.AddWithValue("@dtOrderDetailModel", dtDetailMaster);
                                    scmd.Parameters.AddWithValue("@dtProductLedgerMasterModel", dtProductLedger);
                                    scmd.Parameters.AddWithValue("@ordercnt", ordercnt);
                                    scmd.Parameters.Add("@IdentityValue", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                                    con.Open();
                                    scmd.ExecuteNonQuery();
                                    LoginInfo.LastOrderID = Convert.ToInt64(scmd.Parameters["@IdentityValue"].Value);
                                    //con.Close();
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            isError = 0;
                            _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                        }


                        //try
                        //{
                        //    SQLiteConnection conn = new SQLiteConnection(ClsCommon.SQLiteConn);
                        //    conn.Open();
                        //    SQLiteCommand cemd = new SQLiteCommand("delete from tbl_OrderItemsInCart where UserId = " + LoginInfo.UserId, conn);
                        //    cemd.ExecuteNonQuery();
                        //    cemd = new SQLiteCommand("delete from tbl_PaymentTrans where UserId = " + LoginInfo.UserId, conn);
                        //    cemd.ExecuteNonQuery();
                        //    conn.Close();
                        //    isReload = false;
                        //    OrderInfo.IsPaymentResume = false;
                        //}
                        //catch (Exception ex)
                        //{
                        //    _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                        //}

                        #endregion

                        #region InsertPaymentTrans
                        query = "SELECT * FROM tbl_PaymentTrans WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                        DataAdapter = new SqlCeDataAdapter(query, conn);
                        DataTable dt_P = new DataTable();
                        DataAdapter.Fill(dt_P);

                        if (dt_P != null && dt_P.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt_P.Rows.Count; j++)
                            {
                                PaymentTransMasterModel objPaymentTransMasterModel = new PaymentTransMasterModel();
                                objPaymentTransMasterModel.OrderID = LoginInfo.LastOrderID;// addOrder.OrderID;
                                objPaymentTransMasterModel.PaymentMethodID = Functions.GetLong(dt_P.Rows[j][PaymentTransMasterModelCont.PaymentMethodID].ToString());
                                objPaymentTransMasterModel.StoreID = Functions.GetLong(dt_P.Rows[j][PaymentTransMasterModelCont.StoreID].ToString());
                                objPaymentTransMasterModel.CashAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.CashAmount].ToString());
                                objPaymentTransMasterModel.CheckAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.CheckAmount].ToString());
                                objPaymentTransMasterModel.CreditCardAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.CreditCardAmount].ToString());
                                objPaymentTransMasterModel.FoodStampAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.FoodStampAmount].ToString());
                                objPaymentTransMasterModel.Balance = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.Balance].ToString());
                                objPaymentTransMasterModel.ChangeAmount = Functions.GetDecimal(dt_P.Rows[j][PaymentTransMasterModelCont.ChangeAmount].ToString());
                                objPaymentTransMasterModel.CardNumber = (dt_P.Rows[j][PaymentTransMasterModelCont.CardNumber].ToString());
                                objPaymentTransMasterModel.CounterIP = (dt_P.Rows[j][PaymentTransMasterModelCont.CounterIP].ToString());
                                objPaymentTransMasterModel.CreatedBy = Functions.GetLong(dt_P.Rows[j][PaymentTransMasterModelCont.CreatedBy].ToString());
                                objPaymentTransMasterModel.CreatedDate = Convert.ToDateTime(dt_P.Rows[j][PaymentTransMasterModelCont.CreatedDate].ToString());
                                objPaymentTransMasterModel = _PaymentTransService.AddPaymentTrans(objPaymentTransMasterModel, 1);
                            }
                        }
                        #endregion

                        #region InsertEPXPaymentTrans
                        //query = "SELECT * FROM tbl_OrderEPXLog WHERE OrderId=" + dt_i.Rows[i]["OrderID"];
                        //DataAdapter = new SqlCeDataAdapter(query, conn);
                        //DataTable dt_EPXI = new DataTable();
                        //DataAdapter.Fill(dt_EPXI);

                        //if (dt_EPXI != null && dt_EPXI.Rows.Count > 0)
                        //{
                        //    for (int j = 0; j < dt_EPXI.Rows.Count; j++)
                        //    {
                        //        tbl_OrderEPXLog epx = new tbl_OrderEPXLog();
                        //        epx.OrderId = Convert.ToInt32(LoginInfo.LastOrderID);
                        //        epx.UserId = Functions.GetLong(dt_EPXI.Rows[j][OrderEPXLogModelCont.UserId].ToString());
                        //        epx.PaymentMethodId = Functions.GetInteger(dt_EPXI.Rows[j][OrderEPXLogModelCont.PaymentMethodId].ToString());
                        //        epx.StoreID = Functions.GetLong(dt_EPXI.Rows[j][OrderEPXLogModelCont.StoreID].ToString());
                        //        epx.Amount = Functions.GetDecimal(dt_EPXI.Rows[j][OrderEPXLogModelCont.Amount].ToString());
                        //        epx.Response = dt_EPXI.Rows[j][OrderEPXLogModelCont.Response].ToString();
                        //        epx.TransactionNo = dt_EPXI.Rows[j][OrderEPXLogModelCont.TransactionNo].ToString();
                        //        epx.RequestSend = dt_EPXI.Rows[j][OrderEPXLogModelCont.RequestSend].ToString();
                        //        epx.ResponseValue = dt_EPXI.Rows[j][OrderEPXLogModelCont.ResponseValue].ToString();
                        //        epx.TVR = dt_EPXI.Rows[j][OrderEPXLogModelCont.TVR].ToString();
                        //        epx.CardType = dt_EPXI.Rows[j][OrderEPXLogModelCont.CardType].ToString();
                        //        epx.EmvAid = dt_EPXI.Rows[j][OrderEPXLogModelCont.EmvAid].ToString();
                        //        epx.TSI = dt_EPXI.Rows[j][OrderEPXLogModelCont.TSI].ToString();
                        //        epx.FoodBalance = Functions.GetDecimal(dt_EPXI.Rows[j][OrderEPXLogModelCont.FoodBalance].ToString());
                        //        epx.CashBalance = Functions.GetDecimal(dt_EPXI.Rows[j][OrderEPXLogModelCont.CashBalance].ToString());
                        //        _db.tbl_OrderEPXLog.Add(epx);
                        //        _db.SaveChanges();
                        //    }
                        //}
                        #endregion

                        #region Delete from Local DB
                        if (isError==1)
                        {
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();
                            cmd.CommandText = query = "Delete tbl_OrderMaster WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            int modified = cmd.ExecuteNonQuery();

                            cmd = conn.CreateCommand();
                            cmd.CommandText = query = "Delete tbl_OrderDetail WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            modified = cmd.ExecuteNonQuery();

                            cmd = conn.CreateCommand();
                            cmd.CommandText = query = "Delete tbl_ProductLedger WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            modified = cmd.ExecuteNonQuery();

                            cmd = conn.CreateCommand();
                            cmd.CommandText = query = "Delete tbl_PaymentTrans WHERE OrderID=" + dt_i.Rows[i]["OrderID"];
                            modified = cmd.ExecuteNonQuery();
                        }
                        #endregion
                    }
                }
                #endregion

                #region tbl_ExceptionLog
                query = "Select * from tbl_ExceptionLog";
                DataTable dt_Exception = new DataTable();
                SqlCeDataAdapter da_Exception = new SqlCeDataAdapter(query, conn);
                da_Exception.Fill(dt_Exception);
                if (dt_Exception != null && dt_Exception.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_Exception.Rows.Count; i++)
                    {
                        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
                        objExceptionLogMasterModel.ExceptionName = dt_Exception.Rows[i]["ExceptionName"].ToString();
                        objExceptionLogMasterModel.Discription = dt_Exception.Rows[i]["Discription"].ToString();
                        objExceptionLogMasterModel.PageLine = Functions.GetLong(dt_Exception.Rows[i]["PageLine"].ToString());
                        objExceptionLogMasterModel.PageName = dt_Exception.Rows[i]["PageName"].ToString();
                        objExceptionLogMasterModel.CreatedDate = Convert.ToDateTime(dt_Exception.Rows[i]["CreatedDate"].ToString());
                        objExceptionLogMasterModel.CounterIP = dt_Exception.Rows[i]["CounterIP"].ToString();

                        _ExceptionLogService.AddExceptionLog(objExceptionLogMasterModel.ExceptionName, objExceptionLogMasterModel.Discription, objExceptionLogMasterModel.PageName, objExceptionLogMasterModel.PageLine);
                    }

                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = query = "DELETE tbl_ExceptionLog";
                    int modified = cmd.ExecuteNonQuery();
                }

                #endregion

                #region tbl_TransSuspenMaster

                query = "Select * from tbl_TransSuspenMaster";
                DataTable dt = new DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TransSuspendMasterModel objTransSuspendMasterModel = new TransSuspendMasterModel();
                        objTransSuspendMasterModel.TransSuspendCode = dt.Rows[i]["TransSuspendCode"].ToString();
                        objTransSuspendMasterModel.ProductName = dt.Rows[i]["ProductName"].ToString();
                        objTransSuspendMasterModel.ProductID = Functions.GetLong(dt.Rows[i]["ProductID"].ToString());
                        objTransSuspendMasterModel.UPCCode = dt.Rows[i]["UPCCode"].ToString();
                        objTransSuspendMasterModel.SectionID = Functions.GetLong(dt.Rows[i]["SectionID"].ToString());
                        objTransSuspendMasterModel.DepartmentID = Functions.GetLong(dt.Rows[i]["DepartmentID"].ToString());
                        objTransSuspendMasterModel.Quantity = Functions.GetDecimal(dt.Rows[i]["Quantity"].ToString());
                        objTransSuspendMasterModel.SellPrice = Functions.GetDecimal(dt.Rows[i]["SellPrice"].ToString());
                        objTransSuspendMasterModel.FinalPrice = Functions.GetDecimal(dt.Rows[i]["FinalPrice"].ToString());
                        objTransSuspendMasterModel.TotalAmount = Functions.GetDecimal(dt.Rows[i]["TotalAmount"].ToString());
                        objTransSuspendMasterModel.GrossAmount = Functions.GetDecimal(dt.Rows[i]["GrossAmount"].ToString());
                        objTransSuspendMasterModel.Tax = Functions.GetDecimal(dt.Rows[i]["Tax"].ToString());
                        objTransSuspendMasterModel.TotalTaxAmount = Functions.GetDecimal(dt.Rows[i]["TotalTaxAmount"].ToString());
                        objTransSuspendMasterModel.DiscountApplyed = Functions.GetBoolean(dt.Rows[i]["DiscountApplyed"].ToString());
                        objTransSuspendMasterModel.IsScale = Functions.GetBoolean(dt.Rows[i]["IsScale"].ToString());
                        objTransSuspendMasterModel.IsFoodStamp = Functions.GetBoolean(dt.Rows[i]["IsFoodStamp"].ToString());
                        objTransSuspendMasterModel.IsTax = Functions.GetBoolean(dt.Rows[i]["IsTax"].ToString());
                        objTransSuspendMasterModel.Status = Functions.GetBoolean(dt.Rows[i]["Status"].ToString());
                        objTransSuspendMasterModel.StoreID = Functions.GetLong(dt.Rows[i]["StoreID"].ToString());
                        objTransSuspendMasterModel.CreatedBy = Functions.GetLong(dt.Rows[i]["CreatedBy"].ToString());
                        objTransSuspendMasterModel.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString());
                        objTransSuspendMasterModel.IsDelete = Functions.GetBoolean(dt.Rows[i]["IsDelete"].ToString());

                        objTransSuspendMasterModel.GroupQty = Functions.GetDecimal(dt.Rows[i]["GroupQty"].ToString());
                        objTransSuspendMasterModel.GroupPrice = Functions.GetDecimal(dt.Rows[i]["GroupPrice"].ToString());
                        objTransSuspendMasterModel.CaseQty = Functions.GetDecimal(dt.Rows[i]["CaseQty"].ToString());
                        objTransSuspendMasterModel.CasePrice = Functions.GetDecimal(dt.Rows[i]["CasePrice"].ToString());
                        objTransSuspendMasterModel.CasePriceApplied = Functions.GetBoolean(dt.Rows[i]["CasePriceApplied"].ToString());

                        objTransSuspendMasterModel = _TransSuspendService.AddTransSuspend(objTransSuspendMasterModel, 1);
                    }
                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = query = "DELETE tbl_TransSuspenMaster";
                    int modified = cmd.ExecuteNonQuery();
                }
                #endregion

                #region LottoTrans
                query = "SELECT * FROM tbl_LottoTrans";
                DataAdapter = new SqlCeDataAdapter(query, conn);
                DataTable dt_LottoTrans = new DataTable();
                DataAdapter.Fill(dt_LottoTrans);
                if (dt_LottoTrans != null && dt_LottoTrans.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_LottoTrans.Rows.Count; j++)
                    {
                        LottoModel objLottoModel = new LottoModel();
                        objLottoModel.LottoType = Functions.GetInteger(dt_LottoTrans.Rows[j]["LottoType"].ToString());
                        objLottoModel.LottoPrice = Functions.GetDecimal(dt_LottoTrans.Rows[j]["LottoPrice"].ToString());
                        objLottoModel.CounterIP = dt_LottoTrans.Rows[j]["CounterIP"].ToString();
                        objLottoModel.MacAddress = dt_LottoTrans.Rows[j]["MacAddress"].ToString();
                        objLottoModel.IsActive = Functions.GetBoolean(dt_LottoTrans.Rows[j]["IsActive"].ToString()); ;
                        objLottoModel.IsDelete = Functions.GetBoolean(dt_LottoTrans.Rows[j]["IsDelete"].ToString()); ;
                        objLottoModel.CreatedDate = Convert.ToDateTime(dt_LottoTrans.Rows[j]["CreatedDate"].ToString());
                        objLottoModel.CreatedBy = Functions.GetLong(dt_LottoTrans.Rows[j]["CreatedBy"].ToString());
                        objLottoModel.StoreID = Functions.GetLong(dt_LottoTrans.Rows[j]["StoreID"].ToString());
                        objLottoModel = _LottoService.AddLotto(objLottoModel);
                    }

                    SqlCeCommand cmd = conn.CreateCommand();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = query = "Delete tbl_LottoTrans";
                    int modified = cmd.ExecuteNonQuery();
                }
                #endregion

                ChangeSyncStatus("tbl_OrderMaster");
                ChangeSyncStatus("tbl_OrderDetail");
                ChangeSyncStatus("tbl_ProductLedger");
                ChangeSyncStatus("tbl_LoginMaster");
                ChangeSyncStatus("tbl_ExceptionLog");
                ChangeSyncStatus("tbl_TransSuspenMaster");
                ChangeSyncStatus("tbl_LottoTrans");

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                isError = 0;
                _ExceptionLogService.AddExceptionLog(e.GetType().Name, e.Message, "frmDataSync2-LocalToLiveSync" + e.StackTrace, e.LineNumber());
            }
        }
        public void tbl_DepartmentMaster(DateTime? LastSyncDateTime)
        {
            try
            {

                lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Department Data)"));

                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_DepartmentMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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

                            cmd.CommandText = "UPDATE tbl_DepartmentMaster SET DepartmentID=@DepartmentID,DepartmentName=@DepartmentName, DepartmentBtn=@DepartmentBtn," +
                                "IsFoodStamp = @IsFoodStamp,TaxGroupID = @TaxGroupID,UnitMeasureID = @UnitMeasureID,AgeVarificationAge = @AgeVarificationAge,DepartmentNo=@DepartmentNo,SubNo=@SubNo,ForcedTaxSuffix=@ForcedTaxSuffix,ForcedTaxSection=@ForcedTaxSection," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE DepartmentID=@DepartmentID;";
                            cmd.Parameters.Clear();
                            #region Parameters
                            //if (Data.DepartmentID != null)
                            //{
                            cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            //}
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
                            if(Data.DepartmentBtn != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentBtn", Data.DepartmentBtn);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentBtn", DBNull.Value);
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
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                            if (Data.ForcedTaxSection != null)
                            {
                                cmd.Parameters.AddWithValue("@ForcedTaxSection", Data.ForcedTaxSection);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ForcedTaxSection", DBNull.Value);
                            }
                            if (Data.ForcedTaxSuffix != null)
                            {
                                cmd.Parameters.AddWithValue("@ForcedTaxSuffix", Data.ForcedTaxSuffix);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ForcedTaxSuffix", DBNull.Value);
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

                            cmd.CommandText = "INSERT INTO tbl_DepartmentMaster(DepartmentID,DepartmentName,IsFoodStamp,TaxGroupID,UnitMeasureID,AgeVarificationAge,DepartmentNo,SubNo,DepartmentBtn,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,ForcedTaxSection,ForcedTaxSuffix) " +
                                "VALUES(@DepartmentID,@DepartmentName,@IsFoodStamp,@TaxGroupID,@UnitMeasureID,@AgeVarificationAge,@DepartmentNo,@SubNo,@DepartmentBtn,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy,@ForcedTaxSection,@ForcedTaxSuffix)";
                            cmd.Parameters.Clear();
                            #region Parameters
                            //if (Data.DepartmentID != null)
                            //{
                            cmd.Parameters.AddWithValue("@DepartmentID", Data.DepartmentID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);
                            //}
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
                            if (Data.DepartmentBtn != null)
                            {
                                cmd.Parameters.AddWithValue("@DepartmentBtn", Data.DepartmentBtn);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DepartmentBtn", DBNull.Value);
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
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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

                            if (Data.ForcedTaxSection != null)
                            {
                                cmd.Parameters.AddWithValue("@ForcedTaxSection", Data.ForcedTaxSection);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ForcedTaxSection", DBNull.Value);
                            }
                            if (Data.ForcedTaxSuffix != null)
                            {
                                cmd.Parameters.AddWithValue("@ForcedTaxSuffix", Data.ForcedTaxSuffix);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ForcedTaxSuffix", DBNull.Value);
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_DepartmentMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        public void tbl_EmployeeMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Employee Data)"));
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_EmployeeMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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

                            cmd.CommandText = "UPDATE tbl_EmployeeMaster " +
                                                              "SET RoleID = @RoleID, " +
                                                              "    StoreID = @StoreID, " +
                                                              "    FirstName = @FirstName, " +
                                                              "    LastName = @LastName, " +
                                                              "    EmailID = @EmailID, " +
                                                              "    PhoneNo = @PhoneNo, " +
                                                              "    Password = @Password, " +
                                                              "    MaxVoidAmount = @MaxVoidAmount, " +
                                                              "    BirthDate = @BirthDate, " +
                                                              "    IsCashPayout = @IsCashPayout, " +
                                                              "    IsLottoFunction = @IsLottoFunction, " +
                                                              "    IsActive = @IsActive, " +
                                                              "    IsDelete = @IsDelete, " +
                                                              "    CreatedDate = @CreatedDate, " +
                                                              "    CreatedBy = @CreatedBy, " +
                                                              "    UpdatedDate = @UpdatedDate, " +
                                                              "    UpdatedBy = @UpdatedBy " +
                                                              "WHERE EmployeeID = @EmployeeID;";

                            //cmd.CommandText = "UPDATE tbl_EmployeeMaster SET RoleID=@RoleID,StoreID=@StoreID,FirstName=@FirstName,LastName=@LastName,EmailID=@EmailID,PhoneNo=@PhoneNo,Password=@Password,MaxVoidAmount=@MaxVoidAmount,BirthDate=@BirthDate," +
                            //    "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                            //    "WHERE EmployeeID=@EmployeeID;";

                            #region Parameters

                            #region EmployeeID

                            cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = Data.EmployeeID; // Assuming Data.EmployeeID is always provided and not nullable

                            #endregion

                            #region RoleID
                            if (Data.RoleID.HasValue)
                            {
                                cmd.Parameters.Add("@RoleID", SqlDbType.BigInt).Value = Data.RoleID.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@RoleID", SqlDbType.BigInt).Value = DBNull.Value;
                            }
                            #endregion

                            #region StoreID
                            if (Data.StoreID.HasValue)
                            {
                                cmd.Parameters.Add("@StoreID", SqlDbType.BigInt).Value = Data.StoreID.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@StoreID", SqlDbType.BigInt).Value = DBNull.Value;
                            }
                            #endregion

                            #region First Name
                            if (!string.IsNullOrEmpty(Data.FirstName))
                            {
                                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = Data.FirstName.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region Last Name
                            if (!string.IsNullOrEmpty(Data.LastName))
                            {
                                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = Data.LastName.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region Email ID 
                            if (!string.IsNullOrEmpty(Data.EmailID))
                            {
                                cmd.Parameters.Add("@EmailID", SqlDbType.NVarChar).Value = Data.EmailID.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@EmailID", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region PhoneNo
                            if (!string.IsNullOrEmpty(Data.PhoneNo))
                            {
                                cmd.Parameters.Add("@PhoneNo", SqlDbType.NVarChar).Value = Data.PhoneNo.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@PhoneNo", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region Password
                            if (!string.IsNullOrEmpty(Data.Password))
                            {
                                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Data.Password.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region IsActive & IsDelete
                            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Data.IsActive;
                            cmd.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = Data.IsDelete;
                            #endregion

                            #region IsCashPayout & IsLottoFunction
                            if (Data.IsCashPayout.HasValue)
                            {
                                cmd.Parameters.Add("@IsCashPayout", SqlDbType.Bit).Value = Data.IsCashPayout;
                            }
                            else
                            {
                                cmd.Parameters.Add("@IsCashPayout", SqlDbType.Bit).Value = false;
                            }

                            if (Data.IsLottoFunction.HasValue)
                            {
                                cmd.Parameters.Add("@IsLottoFunction", SqlDbType.Bit).Value = Data.IsLottoFunction;
                            }
                            else
                            {
                                cmd.Parameters.Add("@IsLottoFunction", SqlDbType.Bit).Value = false;
                            }
                            #endregion

                            #region MaxVoidAmount
                            if (Data.MaxVoidAmount.HasValue)
                            {
                                // If it's within the range, set the value as it is
                                cmd.Parameters.Add("@MaxVoidAmount", SqlDbType.Decimal).Value = Data.MaxVoidAmount;
                            }
                            else
                            {
                                cmd.Parameters.Add("@MaxVoidAmount", SqlDbType.Decimal).Value = DBNull.Value;
                            }
                            #endregion

                            #region BirthDate
                            if (Data.BirthDate.HasValue)
                            {
                                // Check if the value is within the valid range for DateTime
                                if (Data.BirthDate < SqlDateTime.MinValue.Value || Data.BirthDate > SqlDateTime.MaxValue.Value)
                                {
                                    // If it's outside the valid range, set it to the minimum or maximum value
                                    cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = Data.BirthDate < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : SqlDateTime.MaxValue.Value;
                                }
                                else
                                {
                                    // If it's within the range, set the value as it is
                                    cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = Data.BirthDate;
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = DBNull.Value;
                            }
                            #endregion

                            #region CreatedBy
                            if (Data.CreatedBy.HasValue)
                            {
                                cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = Data.CreatedBy;
                            }
                            else
                            {
                                cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = DBNull.Value;
                            }
                            #endregion

                            #region UpdatedBy
                            if (Data.UpdatedBy.HasValue)
                            {
                                cmd.Parameters.Add("@UpdatedBy", SqlDbType.BigInt).Value = Data.UpdatedBy;
                            }
                            else
                            {
                                cmd.Parameters.Add("@UpdatedBy", SqlDbType.BigInt).Value = DBNull.Value;
                            }
                            #endregion

                            #region CreatedDate
                            if (Data.CreatedDate.HasValue)
                            {
                                // Check if the value is within the valid range for DateTime
                                if (Data.CreatedDate < SqlDateTime.MinValue.Value || Data.CreatedDate > SqlDateTime.MaxValue.Value)
                                {
                                    // If it's outside the valid range, set it to the minimum or maximum value
                                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = Data.CreatedDate < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : SqlDateTime.MaxValue.Value;
                                }
                                else
                                {
                                    // If it's within the range, set the value as it is
                                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = Data.CreatedDate;
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DBNull.Value;
                            }
                            #endregion

                            #region UpdatedDate
                            if (Data.UpdatedDate.HasValue)
                            {
                                // Check if the value is within the valid range for DateTime
                                if (Data.UpdatedDate < SqlDateTime.MinValue.Value || Data.UpdatedDate > SqlDateTime.MaxValue.Value)
                                {
                                    // If it's outside the valid range, set it to the minimum or maximum value
                                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = Data.UpdatedDate < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : SqlDateTime.MaxValue.Value;
                                }
                                else
                                {
                                    // If it's within the range, set the value as it is
                                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = Data.UpdatedDate;
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DBNull.Value;
                            }
                            #endregion

                            #endregion

                            cmd.ExecuteNonQuery();
                            #endregion
                        }
                        else
                        {
                            #region Insert old code 
                            //DataAdapter = new SqlCeDataAdapter();
                            //SqlCeCommand cmd = conn.CreateCommand();
                            //cmd = conn.CreateCommand();

                            //cmd.CommandText = "INSERT INTO tbl_EmployeeMaster(EmployeeID,RoleID,StoreID,FirstName,LastName,EmailID,PhoneNo,Password,MaxVoidAmount,BirthDate,IsCashPayout,IsLottoFunction,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                            //    "VALUES(@EmployeeID,@RoleID,@StoreID,@FirstName,@LastName,@EmailID,@PhoneNo,@Password,@MaxVoidAmount,@BirthDate,@IsCashPayout,@IsLottoFunction,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            ////cmd.CommandText = "INSERT INTO tbl_EmployeeMaster(EmployeeID,RoleID,StoreID,FirstName,LastName,EmailID,PhoneNo,Password,MaxVoidAmount,BirthDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                            ////    "VALUES(@EmployeeID,@RoleID,@StoreID,@FirstName,@LastName,@EmailID,@PhoneNo,@Password,@MaxVoidAmount,@BirthDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            //#region Parameters
                            ////if (Data.EmployeeID != null)
                            ////{
                            //cmd.Parameters.AddWithValue("@EmployeeID", Data.EmployeeID);
                            ////}
                            ////else
                            ////{
                            ////    cmd.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
                            ////}
                            //if (Data.RoleID != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            //}
                            //if (Data.StoreID != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            //}
                            //if (Data.FirstName != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@FirstName", Data.FirstName);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                            //}
                            //if (Data.LastName != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@LastName", Data.LastName);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                            //}
                            //if (Data.EmailID != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@EmailID", Data.EmailID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@EmailID", DBNull.Value);
                            //}
                            //if (Data.PhoneNo != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@PhoneNo", Data.PhoneNo);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@PhoneNo", DBNull.Value);
                            //}
                            //if (Data.Password != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@Password", Data.Password);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@Password", DBNull.Value);
                            //}
                            //if (Data.MaxVoidAmount != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@MaxVoidAmount", Data.MaxVoidAmount);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@MaxVoidAmount", DBNull.Value);
                            //}
                            //if (Data.BirthDate != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@BirthDate", Data.BirthDate);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@BirthDate", DBNull.Value);
                            //}

                            //cmd.Parameters.AddWithValue("@IsCashPayout", Data.IsCashPayout ?? false);
                            //cmd.Parameters.AddWithValue("@IsLottoFunction", Data.IsLottoFunction ?? false);

                            ////if (Data.IsActive != null)
                            ////{
                            //cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            ////}
                            ////else
                            ////{
                            ////    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            ////}
                            ////if (Data.IsDelete != null)
                            ////{
                            //cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            ////}
                            ////else
                            ////{
                            ////    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            ////}
                            //if (Data.CreatedDate != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            //}
                            //if (Data.CreatedBy != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            //}
                            //if (Data.UpdatedDate != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            //}
                            //if (Data.UpdatedBy != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            //}
                            //#endregion

                            //cmd.ExecuteNonQuery();
                            #endregion

                            #region Insert
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            #region Old COde
                            //cmd.CommandText = "INSERT INTO tbl_EmployeeMaster(EmployeeID,RoleID,StoreID,FirstName,LastName,EmailID,PhoneNo,Password,MaxVoidAmount,BirthDate,IsCashPayout,IsLottoFunction,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                            //    "VALUES(@EmployeeID,@RoleID,@StoreID,@FirstName,@LastName,@EmailID,@PhoneNo,@Password,@MaxVoidAmount,@BirthDate,@IsCashPayout,@IsLottoFunction,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            ////cmd.CommandText = "INSERT INTO tbl_EmployeeMaster(EmployeeID,RoleID,StoreID,FirstName,LastName,EmailID,PhoneNo,Password,MaxVoidAmount,BirthDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                            ////    "VALUES(@EmployeeID,@RoleID,@StoreID,@FirstName,@LastName,@EmailID,@PhoneNo,@Password,@MaxVoidAmount,@BirthDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            //#region Parameters
                            ////if (Data.EmployeeID != null)
                            ////{
                            //cmd.Parameters.AddWithValue("@EmployeeID", Data.EmployeeID);
                            ////}
                            ////else
                            ////{
                            ////    cmd.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
                            ////}
                            //if (Data.RoleID != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            //}
                            //if (Data.StoreID != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            //}
                            //if (Data.FirstName != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@FirstName", Data.FirstName);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                            //}
                            //if (Data.LastName != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@LastName", Data.LastName);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                            //}
                            //if (Data.EmailID != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@EmailID", Data.EmailID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@EmailID", DBNull.Value);
                            //}
                            //if (Data.PhoneNo != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@PhoneNo", Data.PhoneNo);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@PhoneNo", DBNull.Value);
                            //}
                            //if (Data.Password != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@Password", Data.Password);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@Password", DBNull.Value);
                            //}
                            //if (Data.MaxVoidAmount != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@MaxVoidAmount", Data.MaxVoidAmount);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@MaxVoidAmount", DBNull.Value);
                            //}
                            //if (Data.BirthDate != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@BirthDate", Data.BirthDate);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@BirthDate", DBNull.Value);
                            //}

                            //cmd.Parameters.AddWithValue("@IsCashPayout", Data.IsCashPayout ?? false);
                            //cmd.Parameters.AddWithValue("@IsLottoFunction", Data.IsLottoFunction ?? false);

                            ////if (Data.IsActive != null)
                            ////{
                            //cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            ////}
                            ////else
                            ////{
                            ////    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            ////}
                            ////if (Data.IsDelete != null)
                            ////{
                            //cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            ////}
                            ////else
                            ////{
                            ////    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            ////}
                            //if (Data.CreatedDate != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
                            //}
                            //if (Data.CreatedBy != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                            //}
                            //if (Data.UpdatedDate != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                            //}
                            //if (Data.UpdatedBy != null)
                            //{
                            //    cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                            //}
                            //#endregion

                            //cmd.ExecuteNonQuery();
                            #endregion

                            //cmd.CommandText = "INSERT INTO tbl_EmployeeMaster(EmployeeID,RoleID,StoreID,FirstName,LastName,EmailID,PhoneNo,Password,MaxVoidAmount,BirthDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                            //                    "VALUES(@EmployeeID,@RoleID,@StoreID,@FirstName,@LastName,@EmailID,@PhoneNo,@Password,@MaxVoidAmount,@BirthDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            cmd.CommandText = "INSERT INTO tbl_EmployeeMaster(EmployeeID,RoleID,StoreID,FirstName,LastName,EmailID,PhoneNo,Password,MaxVoidAmount,BirthDate,IsCashPayout,IsLottoFunction,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                                 "VALUES(@EmployeeID,@RoleID,@StoreID,@FirstName,@LastName,@EmailID,@PhoneNo,@Password,@MaxVoidAmount,@BirthDate,@IsCashPayout,@IsLottoFunction,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";

                            #region Parameters
                            //cmd.Parameters.AddWithValue("@EmployeeID", Data.EmployeeID); // Assuming Data.EmployeeID is always provided and not nullable

                            //// For nullable parameters, if the value is null, insert DBNull.Value
                            //cmd.Parameters.AddWithValue("@RoleID", Data.RoleID.HasValue ? Data.RoleID.Value : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@StoreID", Data.StoreID.HasValue ? Data.StoreID.Value : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@FirstName", !string.IsNullOrEmpty(Data.FirstName) ? Data.FirstName : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@LastName", !string.IsNullOrEmpty(Data.LastName) ? Data.LastName : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@EmailID", !string.IsNullOrEmpty(Data.EmailID) ? Data.EmailID : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@PhoneNo", !string.IsNullOrEmpty(Data.PhoneNo) ? Data.PhoneNo : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@Password", !string.IsNullOrEmpty(Data.Password) ? Data.Password : (object)DBNull.Value);

                            //// For numeric nullable parameters, if the value is null, insert DBNull.Value
                            //cmd.Parameters.AddWithValue("@MaxVoidAmount", Data.MaxVoidAmount.HasValue ? Data.MaxVoidAmount.Value : (object)DBNull.Value);

                            //// For datetime nullable parameters, if the value is null, insert DBNull.Value
                            //cmd.Parameters.AddWithValue("@BirthDate", Data.BirthDate.HasValue ? Data.BirthDate.Value : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);

                            //// For nullable numeric parameters, if the value is null, insert DBNull.Value
                            //cmd.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy.HasValue ? Data.CreatedBy.Value : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@UpdatedBy", Data.UpdatedBy.HasValue ? Data.UpdatedBy.Value : (object)DBNull.Value);

                            //// For nullable datetime parameters, if the value is null, insert DBNull.Value
                            //cmd.Parameters.AddWithValue("@CreatedDate", Data.CreatedDate.HasValue ? Data.CreatedDate.Value : (object)DBNull.Value);
                            //cmd.Parameters.AddWithValue("@UpdatedDate", Data.UpdatedDate.HasValue ? Data.UpdatedDate.Value : (object)DBNull.Value);
                            #endregion

                            #region Parameters

                            #region EmployeeID

                            cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = Data.EmployeeID; // Assuming Data.EmployeeID is always provided and not nullable

                            #endregion

                            #region RoleID
                            if (Data.RoleID.HasValue)
                            {
                                cmd.Parameters.Add("@RoleID", SqlDbType.BigInt).Value = Data.RoleID.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@RoleID", SqlDbType.BigInt).Value = DBNull.Value;
                            }
                            #endregion

                            #region StoreID
                            if (Data.StoreID.HasValue)
                            {
                                cmd.Parameters.Add("@StoreID", SqlDbType.BigInt).Value = Data.StoreID.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@StoreID", SqlDbType.BigInt).Value = DBNull.Value;
                            }
                            #endregion

                            #region First Name
                            if (!string.IsNullOrEmpty(Data.FirstName))
                            {
                                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = Data.FirstName.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region Last Name
                            if (!string.IsNullOrEmpty(Data.LastName))
                            {
                                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = Data.LastName.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region Email ID 
                            if (!string.IsNullOrEmpty(Data.EmailID))
                            {
                                cmd.Parameters.Add("@EmailID", SqlDbType.NVarChar).Value = Data.EmailID.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@EmailID", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region PhoneNo
                            if (!string.IsNullOrEmpty(Data.PhoneNo))
                            {
                                cmd.Parameters.Add("@PhoneNo", SqlDbType.NVarChar).Value = Data.PhoneNo.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@PhoneNo", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region Password
                            if (!string.IsNullOrEmpty(Data.Password))
                            {
                                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Data.Password.ToString();
                            }
                            else
                            {
                                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = DBNull.Value;
                            }
                            #endregion

                            #region IsActive & IsDelete
                            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = Data.IsActive;
                            cmd.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = Data.IsDelete;
                            #endregion

                            #region IsCashPayout & IsLottoFunction
                            if (Data.IsCashPayout.HasValue)
                            {
                                cmd.Parameters.Add("@IsCashPayout", SqlDbType.Bit).Value = Data.IsCashPayout;
                            }
                            else
                            {
                                cmd.Parameters.Add("@IsCashPayout", SqlDbType.Bit).Value = false;
                            }

                            if (Data.IsLottoFunction.HasValue)
                            {
                                cmd.Parameters.Add("@IsLottoFunction", SqlDbType.Bit).Value = Data.IsLottoFunction;
                            }
                            else
                            {
                                cmd.Parameters.Add("@IsLottoFunction", SqlDbType.Bit).Value = false;
                            }
                            #endregion

                            #region MaxVoidAmount
                            if (Data.MaxVoidAmount.HasValue)
                            {
                                // Check if the value exceeds the range of the Decimal type
                                if (Data.MaxVoidAmount > Decimal.MaxValue || Data.MaxVoidAmount < Decimal.MinValue)
                                {
                                    // If it exceeds, handle it by selecting the maximum or minimum value
                                    cmd.Parameters.Add("@MaxVoidAmount", SqlDbType.Decimal).Value = Data.MaxVoidAmount > 0 ? Decimal.MaxValue : Decimal.MinValue;
                                }
                                else
                                {
                                    // If it's within the range, set the value as it is
                                    cmd.Parameters.Add("@MaxVoidAmount", SqlDbType.Decimal).Value = Data.MaxVoidAmount;
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add("@MaxVoidAmount", SqlDbType.Decimal).Value = DBNull.Value;
                            }
                            #endregion

                            #region BirthDate
                            if (Data.BirthDate.HasValue)
                            {
                                // Check if the value is within the valid range for DateTime
                                if (Data.BirthDate < SqlDateTime.MinValue.Value || Data.BirthDate > SqlDateTime.MaxValue.Value)
                                {
                                    // If it's outside the valid range, set it to the minimum or maximum value
                                    cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = Data.BirthDate < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : SqlDateTime.MaxValue.Value;
                                }
                                else
                                {
                                    // If it's within the range, set the value as it is
                                    cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = Data.BirthDate;
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = DBNull.Value;
                            }
                            #endregion

                            #region CreatedBy
                            if (Data.CreatedBy.HasValue)
                            {
                                cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = Data.CreatedBy;
                            }
                            else
                            {
                                cmd.Parameters.Add("@CreatedBy", SqlDbType.BigInt).Value = DBNull.Value;
                            }
                            #endregion

                            #region UpdatedBy
                            if (Data.UpdatedBy.HasValue)
                            {
                                cmd.Parameters.Add("@UpdatedBy", SqlDbType.BigInt).Value = Data.UpdatedBy;
                            }
                            else
                            {
                                cmd.Parameters.Add("@UpdatedBy", SqlDbType.BigInt).Value = DBNull.Value;
                            }
                            #endregion

                            #region CreatedDate
                            if (Data.CreatedDate.HasValue)
                            {
                                // Check if the value is within the valid range for DateTime
                                if (Data.CreatedDate < SqlDateTime.MinValue.Value || Data.CreatedDate > SqlDateTime.MaxValue.Value)
                                {
                                    // If it's outside the valid range, set it to the minimum or maximum value
                                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = Data.CreatedDate < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : SqlDateTime.MaxValue.Value;
                                }
                                else
                                {
                                    // If it's within the range, set the value as it is
                                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = Data.CreatedDate;
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DBNull.Value;
                            }
                            #endregion

                            #region UpdatedDate
                            if (Data.UpdatedDate.HasValue)
                            {
                                // Check if the value is within the valid range for DateTime
                                if (Data.UpdatedDate < SqlDateTime.MinValue.Value || Data.UpdatedDate > SqlDateTime.MaxValue.Value)
                                {
                                    // If it's outside the valid range, set it to the minimum or maximum value
                                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = Data.UpdatedDate < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : SqlDateTime.MaxValue.Value;
                                }
                                else
                                {
                                    // If it's within the range, set the value as it is
                                    cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = Data.UpdatedDate;
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DBNull.Value;
                            }
                            #endregion

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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_EmployeeMaster" + ex.StackTrace, ex.LineNumber());
            }

        }

        public void tbl_ProductMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                //lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Product Data)"));
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Store Data)"));
                }
                else
                {
                    lblStatus.Text = "Data sync in progress(Store Data)";
                }
                //System.Threading.Thread.Sleep(5000);
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_ProductMaster" + ex.StackTrace, ex.LineNumber());
            }
        }
        public void tbl_ProductSaleDiscountMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Product Sale Discount Data)"));
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductSaleDiscountMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_ProductSaleDiscountMaster Data in tbl_Data)
                    {
                        string query = "Select * from tbl_ProductSaleDiscountMaster WHERE ProductSaleDiscountID=" + Data.ProductSaleDiscountID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ProductSaleDiscountMaster SET ProductID=@ProductID,Discount=@Discount,StartDate=@StartDate,EndDate=@EndDate," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE ProductSaleDiscountID=@ProductSaleDiscountID;";
                            #region Parameters
                            //if (Data.ProductSaleDiscountID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductSaleDiscountID", Data.ProductSaleDiscountID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductSaleDiscountID", DBNull.Value);
                            //}
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.Discount != null)
                            {
                                cmd.Parameters.AddWithValue("@Discount", Data.Discount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Discount", DBNull.Value);
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

                            cmd.CommandText = "INSERT INTO tbl_ProductSaleDiscountMaster(ProductSaleDiscountID,ProductID,Discount,StartDate,EndDate,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@ProductSaleDiscountID,@ProductID,@Discount,@StartDate,@EndDate,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            //if (Data.ProductSaleDiscountID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductSaleDiscountID", Data.ProductSaleDiscountID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductSaleDiscountID", DBNull.Value);
                            //}
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.Discount != null)
                            {
                                cmd.Parameters.AddWithValue("@Discount", Data.Discount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Discount", DBNull.Value);
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
                ChangeSyncStatus("tbl_ProductSaleDiscountMaster");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_ProductSaleDiscountMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_ProductSalePriceMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Product Sale Price Data)"));
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductSalePriceMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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
                            //if (Data.ProductSalePriceID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductSalePriceID", Data.ProductSalePriceID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductSalePriceID", DBNull.Value);
                            //}
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
                            //if (Data.ProductSalePriceID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductSalePriceID", Data.ProductSalePriceID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductSalePriceID", DBNull.Value);
                            //}
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_ProductSalePriceMaster" + ex.StackTrace, ex.LineNumber());
            }
        }
        public void tbl_ProductUoM(DateTime? LastSyncDateTime)
        {
            try
            {
                //lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Product UOM Data)"));
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Store Data)"));
                }
                else
                {
                    lblStatus.Text = "Data sync in progress(Store Data)";
                }
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ProductUoM.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    foreach (tbl_ProductUoM Data in tbl_Data)
                    {
                        string query = "Select * from tbl_ProductUoM WHERE ProductUoMID=" + Data.ProductUoMID;
                        DataTable dt = new DataTable();
                        SqlCeDataAdapter da = new SqlCeDataAdapter(query, conn);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            #region Update
                            DataAdapter = new SqlCeDataAdapter();
                            SqlCeCommand cmd = conn.CreateCommand();
                            cmd = conn.CreateCommand();

                            cmd.CommandText = "UPDATE tbl_ProductUoM SET ProductID=@ProductID,UnitMeasureID=@UnitMeasureID,Discription=@Discription,QtyPerUoM=@QtyPerUoM," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE ProductUoMID=@ProductUoMID;";
                            #region Parameters
                            //if (Data.ProductUoMID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductUoMID", Data.ProductUoMID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductUoMID", DBNull.Value);
                            //}
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.Discription != null)
                            {
                                cmd.Parameters.AddWithValue("@Discription", Data.Discription);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Discription", DBNull.Value);
                            }
                            if (Data.QtyPerUoM != null)
                            {
                                cmd.Parameters.AddWithValue("@QtyPerUoM", Data.QtyPerUoM);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@QtyPerUoM", DBNull.Value);
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

                            cmd.CommandText = "INSERT INTO tbl_ProductUoM(ProductUoMID,ProductID,UnitMeasureID,Discription,QtyPerUoM,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) " +
                                 "VALUES(@ProductUoMID,@ProductID,@UnitMeasureID,@Discription,@QtyPerUoM,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy)";
                            #region Parameters
                            //if (Data.ProductUoMID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ProductUoMID", Data.ProductUoMID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ProductUoMID", DBNull.Value);
                            //}
                            if (Data.ProductID != null)
                            {
                                cmd.Parameters.AddWithValue("@ProductID", Data.ProductID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ProductID", DBNull.Value);
                            }
                            if (Data.UnitMeasureID != null)
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            }
                            if (Data.Discription != null)
                            {
                                cmd.Parameters.AddWithValue("@Discription", Data.Discription);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Discription", DBNull.Value);
                            }
                            if (Data.QtyPerUoM != null)
                            {
                                cmd.Parameters.AddWithValue("@QtyPerUoM", Data.QtyPerUoM);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@QtyPerUoM", DBNull.Value);
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
                ChangeSyncStatus("tbl_ProductUoM");
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_ProductUoM" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_RoleMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                //lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Role Data)"));
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Store Data)"));
                }
                else
                {
                    lblStatus.Text = "Data sync in progress(Store Data)";
                }
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_RoleMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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

                            cmd.CommandText = "UPDATE tbl_RoleMaster SET RoleID=@RoleID,RoleType=@RoleType,,OverrideAmount=@OverrideAmount," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy " +
                                "WHERE RoleID=@RoleID;";
                            //if (Data.RoleID != null)
                            //{
                            cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            //}
                            if (Data.RoleType != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleType", Data.RoleType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleType", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                            if (Data.OverrideAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@OverrideAmount", Data.OverrideAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OverrideAmount", DBNull.Value);
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

                            cmd.CommandText = "INSERT INTO tbl_RoleMaster(RoleID,RoleType,IsActive,IsDelete,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,OverrideAmount) " +
                                "VALUES(@RoleID,@RoleType,@IsActive,@IsDelete,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy,@OverrideAmount)";
                            //if (Data.RoleID != null)
                            //{
                            cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                            //}
                            if (Data.RoleType != null)
                            {
                                cmd.Parameters.AddWithValue("@RoleType", Data.RoleType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RoleType", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                            if (Data.OverrideAmount != null)
                            {
                                cmd.Parameters.AddWithValue("@OverrideAmount", Data.OverrideAmount);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OverrideAmount", DBNull.Value);
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_RoleMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_RolePermission()
        {
            try
            {
                //lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Role Permission Data)"));
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_RolePermission.ToList();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (tbl_Data.Count > 0)
                {
                    long PrevRoleID = 0;
                    foreach (tbl_RolePermission Data in tbl_Data)
                    {
                        try
                        {
                            if (Data.RoleID != null && Data.RoleID != PrevRoleID)
                            {
                                #region Delete
                                SqlCeCommand cmdDlt = conn.CreateCommand();
                                cmdDlt = conn.CreateCommand();

                                cmdDlt.CommandText = "DELETE from tbl_RolePermission WHERE RoleID=@RoleID;";

                                cmdDlt.Parameters.AddWithValue("@RoleID", Data.RoleID);

                                cmdDlt.ExecuteNonQuery();
                                PrevRoleID = Convert.ToInt64(Data.RoleID);
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            isError = 0;
                            _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_RolePermission" + ex.StackTrace, ex.LineNumber());
                        }
                        

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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_RolePermission" + ex.StackTrace, ex.LineNumber());
            }

        }

        public void tbl_RolePermission(DateTime? LastSyncDateTime)
        {
            try
            {
                //lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Role Permission Data)"));
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Store Data)"));
                }
                else
                {
                    lblStatus.Text = "Data sync in progress(Store Data)";
                }

                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_RolePermission.ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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
                            //if (Data.RoleID != null)
                            //{
                            cmd.Parameters.AddWithValue("@PermissionID", Data.PermissionID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@PermissionID", DBNull.Value);
                            //}
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_RolePermission" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_SectionMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Section Data)"));
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_SectionMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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
                            //if (Data.SectionID != null)
                            //{
                            cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            //}
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
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                            //if (Data.SectionID != null)
                            //{
                            cmd.Parameters.AddWithValue("@SectionID", Data.SectionID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@SectionID", DBNull.Value);
                            //}
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
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_SectionMaster" + ex.StackTrace, ex.LineNumber());
            }
        }
        public void tbl_ShortcutkeyMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                //lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Shortcut key Data)"));

                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Store Data)"));
                }
                else
                {
                    lblStatus.Text = "Data sync in progress(Store Data)";
                }

                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_ShortcutkeyMaster.ToList();//Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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
                            //if (Data.ShortcutKeyID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ShortcutKeyID", Data.ShortcutKeyID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ShortcutKeyID", DBNull.Value);
                            //}
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
                            //if (Data.ShortcutKeyID != null)
                            //{
                            cmd.Parameters.AddWithValue("@ShortcutKeyID", Data.ShortcutKeyID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@ShortcutKeyID", DBNull.Value);
                            //}
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_ShortcutkeyMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_StoreMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                //lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Store Data)"));
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Store Data)"));
                }
                else
                {
                    lblStatus.Text = "Data sync in progress(Store Data)";
                }

                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_StoreMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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

                            cmd.CommandText = "UPDATE tbl_StoreMaster SET StoreName=@StoreName,Address=@Address,Address2=@Address2,Phone=@Phone,Fax=@Fax,City=@City,State=@State,Country=@Country,ZipCode=@ZipCode,AgeVarificationAge=@AgeVarificationAge,Disclaimer=@Disclaimer,DefaultTax=@DefaultTax," +
                                "IsActive=@IsActive,IsDelete=@IsDelete,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy,IsStoreTax=@IsStoreTax " +
                                "WHERE StoreID=@StoreID;";
                            #region Parameters
                            //if (Data.StoreID != null)
                            //{
                            cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            //}
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
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                            //if (Data.StoreID != null)
                            //{
                            cmd.Parameters.AddWithValue("@StoreID", Data.StoreID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            //}
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
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_StoreMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_TaxGroupMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                //lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Tax Group Data)"));
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Store Data)"));
                }
                else
                {
                    lblStatus.Text = "Data sync in progress(Store Data)";
                }

                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_TaxGroupMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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
                            //if (Data.TaxGroupID != null)
                            //{
                            cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            //}
                            if (Data.TaxGroupName != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", Data.TaxGroupName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                            //if (Data.TaxGroupID != null)
                            //{
                            cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            //}
                            if (Data.TaxGroupName != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", Data.TaxGroupName);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupName", DBNull.Value);
                            }
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_TaxGroupMaster" + ex.StackTrace, ex.LineNumber());
            }
        }
        public void tbl_TaxRateMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Tax Rate Data)"));
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_TaxRateMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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
                            //if (Data.TaxRateID != null)
                            //{
                            cmd.Parameters.AddWithValue("@TaxRateID", Data.TaxRateID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TaxRateID", DBNull.Value);
                            //}
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            //if (Data.Tax != null)
                            //{
                            cmd.Parameters.AddWithValue("@Tax", Data.Tax);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@Tax", DBNull.Value);
                            //}
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

                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                            //if (Data.TaxRateID != null)
                            //{
                            cmd.Parameters.AddWithValue("@TaxRateID", Data.TaxRateID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@TaxRateID", DBNull.Value);
                            //}
                            if (Data.TaxGroupID != null)
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", Data.TaxGroupID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxGroupID", DBNull.Value);
                            }
                            //if (Data.Tax != null)
                            //{
                            cmd.Parameters.AddWithValue("@Tax", Data.Tax);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@Tax", DBNull.Value);
                            //}
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

                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_TaxRateMaster" + ex.StackTrace, ex.LineNumber());
            }

        }
        public void tbl_UnitMeasureMaster(DateTime? LastSyncDateTime)
        {
            try
            {
                lblStatus.Invoke((Action)(() => lblStatus.Text = "Data sync in progress(Unit Measure Data)"));
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                var tbl_Data = _db.tbl_UnitMeasureMaster.Where(x => x.CreatedDate > LastSyncDateTime || x.UpdatedDate > LastSyncDateTime).ToList();
                if (tbl_Data.Count > 0)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
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
                            //if (Data.UnitMeasureID != null)
                            //{
                            cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            //}
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
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                            //if (Data.UnitMeasureID != null)
                            //{
                            cmd.Parameters.AddWithValue("@UnitMeasureID", Data.UnitMeasureID);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@UnitMeasureID", DBNull.Value);
                            //}
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
                            //if (Data.IsActive != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsActive", Data.IsActive);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
                            //}
                            //if (Data.IsDelete != null)
                            //{
                            cmd.Parameters.AddWithValue("@IsDelete", Data.IsDelete);
                            //}
                            //else
                            //{
                            //    cmd.Parameters.AddWithValue("@IsDelete", DBNull.Value);
                            //}
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
                isError = 0;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "frmDataSync-tbl_UnitMeasureMaster" + ex.StackTrace, ex.LineNumber());
            }
        }

        private void frmDataSync2_Shown(object sender, EventArgs e)
        {
            ClsCommon.SetScreen(this, XMLData.POSScreen);
        }
    }
}
