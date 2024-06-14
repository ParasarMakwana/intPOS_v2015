using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
//using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace SFPOS.DAL.Frontend
{
    public class OrderScannerDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //Search UPCCode
        public List<OrderScanner_ResultModel> GetScannedUPCCode(string UPCCode)
        {
            var lstOrderScanner_ResultModel = new List<OrderScanner_ResultModel>();
            var onjtbl_OrderMaster = _db.SP_ScanUPCCode(UPCCode).ToList();

            if (onjtbl_OrderMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_ScanUPCCode_Result, OrderScanner_ResultModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_ScanUPCCode_Result objtbl_OrderMaster in onjtbl_OrderMaster)
                {
                    OrderScanner_ResultModel _OrderMasterModel = iMapper.Map<SP_ScanUPCCode_Result, OrderScanner_ResultModel>(objtbl_OrderMaster);
                    lstOrderScanner_ResultModel.Add(_OrderMasterModel);
                }
            }
            return lstOrderScanner_ResultModel;
        }

        //ADD 
        public OrderMasterModel AddOrder(OrderMasterModel objOrderMasterModel, int TransType)
        {
            try
            {
                tbl_OrderMaster objtbl_OrderMaster = new tbl_OrderMaster();
                if (TransType == 1)//ADD
                {
                    objOrderMasterModel.OrderID = objOrderMasterModel.OrderID;
                    objtbl_OrderMaster.CardNumber = objOrderMasterModel.CardNumber;
                    objtbl_OrderMaster.CustomerID = objOrderMasterModel.CustomerID;
                    //objtbl_OrderMaster.DiscountAmount = objOrderMasterModel.DiscountAmount;
                    objtbl_OrderMaster.PaymentMethodID = objOrderMasterModel.PaymentMethodID;
                    objtbl_OrderMaster.StoreID = objOrderMasterModel.StoreID;
                    objtbl_OrderMaster.Status = objOrderMasterModel.Status;
                    objtbl_OrderMaster.TotalAmount = objOrderMasterModel.TotalAmount;
                    objtbl_OrderMaster.TaxAmount = objOrderMasterModel.TaxAmount;
                    objtbl_OrderMaster.GrossAmount = objOrderMasterModel.GrossAmount;
                    objtbl_OrderMaster.CashAmount = objOrderMasterModel.CashAmount;
                    objtbl_OrderMaster.CheckAmount = objOrderMasterModel.CheckAmount;
                    objtbl_OrderMaster.CreditCardAmount = objOrderMasterModel.CreditCardAmount;
                    objtbl_OrderMaster.FoodStampAmount = objOrderMasterModel.FoodStampAmount;
                    objtbl_OrderMaster.RefundAmount = objOrderMasterModel.RefundAmount;
                    objtbl_OrderMaster.ChangeAmount = objOrderMasterModel.ChangeAmount;
                    objtbl_OrderMaster.TaxableAmount = objOrderMasterModel.TaxableAmount;
                    objtbl_OrderMaster.Balance = objOrderMasterModel.Balance;
                    objtbl_OrderMaster.CreatedDate = objOrderMasterModel.CreatedDate;
                    objtbl_OrderMaster.CreatedBy = objOrderMasterModel.CreatedBy;
                    objtbl_OrderMaster.CounterIP = objOrderMasterModel.CounterIP;
                    objtbl_OrderMaster.IsCancel = objOrderMasterModel.IsCancel;
                    objtbl_OrderMaster.OrdNo = objOrderMasterModel.OrdNo;
                    objtbl_OrderMaster.TaxExempted = objOrderMasterModel.TaxExempted;
                    objtbl_OrderMaster.CouponCode = objOrderMasterModel.CouponCode;
                    objtbl_OrderMaster.CouponDiscAmt = objOrderMasterModel.CouponDiscAmt;
                    objtbl_OrderMaster.IsTaxCarry = objOrderMasterModel.IsTaxCarry;
                    objtbl_OrderMaster.ReturnAmount = objOrderMasterModel.ReturnAmount;
                    objtbl_OrderMaster.OverridePriceTotal = objOrderMasterModel.OverridePrice;
                    //Sync Date  - 20200709  -  Komel Lakhani
                    objtbl_OrderMaster.SyncDate = objOrderMasterModel.SyncDate;
                    if (LoginInfo.LocalSync)
                    {
                        
                        objtbl_OrderMaster.SyncDate = DateTime.Now;
                        LoginInfo.LocalSync = false;
                    }
                    //
                    _db.tbl_OrderMaster.Add(objtbl_OrderMaster);
                    _db.SaveChanges();
                    objOrderMasterModel.OrderID = objtbl_OrderMaster.OrderID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
                Functions.ErrorLog("OrderScannerDAL", "AddOrder(tbl_OrderMaster)", e);
            }
            return objOrderMasterModel;
        }

        //ADD data using ado.net vishnu prajapati 28/4/23
        public OrderMasterModel AddOrderAdo(OrderMasterModel objOrderMasterModel, int TransType)
        {
            try
            {
                
                tbl_OrderMaster objtbl_OrderMaster = new tbl_OrderMaster();
                DataTable dt = new DataTable();
                if (TransType == 1)//ADD
                {
                   // dt.Columns.Add("OrderID", typeof(int));
                    dt.Columns.Add("CardNumber", typeof(string));
                    dt.Columns.Add("CustomerID", typeof(long));
                    dt.Columns.Add("PaymentMethodID",typeof(long));
                    dt.Columns.Add("StoreID", typeof(long));
                    dt.Columns.Add("Status", typeof(string));
                    dt.Columns.Add("TotalAmount", typeof(decimal));
                    dt.Columns.Add("TaxAmount", typeof(decimal));
                    dt.Columns.Add("GrossAmount", typeof(decimal));
                    dt.Columns.Add("CashAmount", typeof(decimal));
                    dt.Columns.Add("CheckAmount", typeof(decimal));
                    dt.Columns.Add("CreditCardAmount", typeof(decimal));
                    dt.Columns.Add("FoodStampAmount", typeof(decimal));
                    dt.Columns.Add("RefundAmount", typeof(decimal));
                    dt.Columns.Add("ChangeAmount", typeof(decimal));
                    dt.Columns.Add("TaxableAmount", typeof(decimal));
                    dt.Columns.Add("Balance", typeof(decimal));
                    dt.Columns.Add("CreatedDate", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(long));
                    dt.Columns.Add("CounterIP", typeof(string));
                    dt.Columns.Add("IsCancel", typeof(bool));
                    dt.Columns.Add("OrdNo", typeof(string));
                    dt.Columns.Add("TaxExempted", typeof(decimal));
                    dt.Columns.Add("CouponCode", typeof(string));
                    dt.Columns.Add("CouponDiscAmt", typeof(decimal));
                    dt.Columns.Add("IsTaxCarry", typeof(bool));
                    dt.Columns.Add("ReturnAmount", typeof(decimal));
                    dt.Columns.Add("OverridePriceTotal", typeof(decimal));
                    dt.Columns.Add("SyncDate", typeof(DateTime));

                    DataRow row = dt.NewRow();
                   // row["OrderID"] = objOrderMasterModel.OrderID;
                    row["CardNumber"] = objOrderMasterModel.CardNumber;
                    row["CustomerID"] = objOrderMasterModel.CustomerID;
                    row["PaymentMethodID"] = objOrderMasterModel.PaymentMethodID;
                    row["StoreID"] = objOrderMasterModel.StoreID;
                    row["Status"] = objOrderMasterModel.Status;
                    row["TotalAmount"] = objOrderMasterModel.TotalAmount;
                    row["TaxAmount"] = objOrderMasterModel.TaxAmount;
                    row["GrossAmount"] = objOrderMasterModel.GrossAmount;
                    row["CashAmount"] = objOrderMasterModel.CashAmount;
                    row["CheckAmount"] = objOrderMasterModel.CheckAmount;
                    row["CreditCardAmount"] = objOrderMasterModel.CreditCardAmount;
                    row["FoodStampAmount"] = objOrderMasterModel.FoodStampAmount;
                    row["RefundAmount"] = objOrderMasterModel.RefundAmount;
                    row["ChangeAmount"] = objOrderMasterModel.ChangeAmount;
                    row["TaxableAmount"] = 0.00;
                    if (objOrderMasterModel.TaxableAmount != null)
                        {
                            row["TaxableAmount"] = objOrderMasterModel.TaxableAmount;
                        }
                    row["Balance"] = objOrderMasterModel.Balance;
                    row["CreatedDate"] = objOrderMasterModel.CreatedDate;
                    row["CreatedBy"] = objOrderMasterModel.CreatedBy;
                    row["CounterIP"] = objOrderMasterModel.CounterIP;
                    row["IsCancel"] = objOrderMasterModel.IsCancel;
                    row["OrdNo"] = objOrderMasterModel.OrdNo;
                    row["TaxExempted"] = objOrderMasterModel.TaxExempted;
                    row["CouponCode"] = objOrderMasterModel.CouponCode;
                    row["CouponDiscAmt"] = objOrderMasterModel.CouponDiscAmt;
                    row["IsTaxCarry"] = false;
                    if (objOrderMasterModel.IsTaxCarry != null)
                    {
                        row["IsTaxCarry"] = objOrderMasterModel.IsTaxCarry;
                    }
                    row["ReturnAmount"] = 0.00;
                    if (objOrderMasterModel.ReturnAmount != null)
                    {
                        row["ReturnAmount"] = objOrderMasterModel.ReturnAmount;
                    }
                    row["OverridePriceTotal"] = 0.00;
                    if (objOrderMasterModel.OverridePrice != null)
                    {
                        row["OverridePriceTotal"] = objOrderMasterModel.OverridePrice;
                    }
                    row["SyncDate"] = objOrderMasterModel.CreatedDate;
                    if (LoginInfo.LocalSync)
                    {

                        row["SyncDate"] = DateTime.Now;
                        LoginInfo.LocalSync = false;
                    }

                   
                    dt.Rows.Add(row);
                    dt.AcceptChanges();
                }
                var conStr = XMLData.DbConnectionString;
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand("Insert_OrderMasterDetail", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@orderhdrMaster", dt);
                        cmd.Parameters.AddWithValue("@orderDetail", null);
                        cmd.Parameters.AddWithValue("@intId", 1);
                        cmd.Parameters.Add("@IdentityValue", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        LoginInfo.LastOrderID = Convert.ToInt64(cmd.Parameters["@IdentityValue"].Value);
                        con.Close();
                    }

                }
                
            }
            catch (Exception e)
            {
                string ex = e.Message;
                Functions.ErrorLog("OrderScannerDAL", "AddOrder(tbl_OrderMaster)", e);
            }
            objOrderMasterModel.OrderID = LoginInfo.LastOrderID;
            return objOrderMasterModel;
        }

        public List<OrderMasterModel> GetAllOrderDetail()
        {
            var lstOrderMasterModel = new List<OrderMasterModel>();
            DateTime temp = DateTime.Now.AddDays(-1);
            var onjtbl_OrderMaster = _db.tbl_OrderMaster.Where(x => x.StoreID == LoginInfo.StoreID &&  x.CounterIP == LoginInfo.CounterIP && x.CreatedDate > temp).OrderByDescending(x => x.CreatedDate).ToList();

            if (onjtbl_OrderMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_OrderMaster, OrderMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_OrderMaster objtbl_OrderMaster in onjtbl_OrderMaster)
                {
                    OrderMasterModel _OrderMasterModel = iMapper.Map<tbl_OrderMaster, OrderMasterModel>(objtbl_OrderMaster);
                    lstOrderMasterModel.Add(_OrderMasterModel);
                }
            }
            return lstOrderMasterModel;
        }

        public OrderDetailmasterModel AddOrderDetail(OrderDetailmasterModel objOrderDetailmasterModel, int TransType)
        {
            try
            {
                tbl_OrderDetail objtbl_OrderDetail = new tbl_OrderDetail();
                if (TransType == 1)//ADD
                {
                    objOrderDetailmasterModel.OrderDetailID = objOrderDetailmasterModel.OrderDetailID;
                    objtbl_OrderDetail.OrderID = objOrderDetailmasterModel.OrderID;
                    objtbl_OrderDetail.ProductID = objOrderDetailmasterModel.ProductID;
                    objtbl_OrderDetail.ProductName = objOrderDetailmasterModel.ProductName;
                    objtbl_OrderDetail.UPCCode = objOrderDetailmasterModel.UPCCode;
                    objtbl_OrderDetail.Quantity = objOrderDetailmasterModel.Quantity;
                    objtbl_OrderDetail.SellPrice = objOrderDetailmasterModel.SellPrice;
                    objtbl_OrderDetail.Discount = objOrderDetailmasterModel.Discount;
                    objtbl_OrderDetail.finalPrice = objOrderDetailmasterModel.finalPrice;
                    objtbl_OrderDetail.StoreID = objOrderDetailmasterModel.StoreID;
                    objtbl_OrderDetail.CreatedDate = objOrderDetailmasterModel.CreatedDate;
                    objtbl_OrderDetail.CreatedBy = objOrderDetailmasterModel.CreatedBy;
                    objtbl_OrderDetail.IsScale = objOrderDetailmasterModel.IsScale;
                    objtbl_OrderDetail.IsFoodStamp = objOrderDetailmasterModel.IsFoodStamp;
                    objtbl_OrderDetail.IsTax = objOrderDetailmasterModel.IsTax;
                    objtbl_OrderDetail.FoodStampTotal = objOrderDetailmasterModel.FoodStampTotal;
                    objtbl_OrderDetail.DiscountApplyed = objOrderDetailmasterModel.DiscountApplyed;
                    objtbl_OrderDetail.TaxAmount = objOrderDetailmasterModel.TaxAmount;
                    objtbl_OrderDetail.IsRefund = objOrderDetailmasterModel.IsRefund;
                    objtbl_OrderDetail.IsCancel = objOrderDetailmasterModel.IsCancel;
                    objtbl_OrderDetail.IsForceTax = objOrderDetailmasterModel.IsForceTax;
                    objtbl_OrderDetail.DepartmentID = objOrderDetailmasterModel.DepartmentID;
                    objtbl_OrderDetail.SectionID = objOrderDetailmasterModel.SectionID;
                    objtbl_OrderDetail.CasePriceApplied = objOrderDetailmasterModel.CasePriceApplied;
                    objtbl_OrderDetail.GroupPrice = objOrderDetailmasterModel.GroupPrice;
                    objtbl_OrderDetail.GroupQty = objOrderDetailmasterModel.GroupQty;
                    objtbl_OrderDetail.CasePrice = objOrderDetailmasterModel.CasePrice;
                    objtbl_OrderDetail.CaseQty = objOrderDetailmasterModel.CaseQty;
                    objtbl_OrderDetail.IsTaxCarry = objOrderDetailmasterModel.IsTaxCarry;
                    objtbl_OrderDetail.IsReturn = objOrderDetailmasterModel.IsReturn;
                    objtbl_OrderDetail.OverridePrice = objOrderDetailmasterModel.OverridePrice;
                    objtbl_OrderDetail.IsForceTaxDept = objOrderDetailmasterModel.IsForceTaxDept;
                    objtbl_OrderDetail.IsManWTRefund = objOrderDetailmasterModel.IsManWTRefund;
                    _db.tbl_OrderDetail.Add(objtbl_OrderDetail);
                    _db.SaveChanges();
                    objOrderDetailmasterModel.OrderDetailID = objtbl_OrderDetail.OrderDetailID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
                Functions.ErrorLog("OrderScannerDAL", "AddOrderDetail(tbl_OrderDetail)", e);
            }
            return objOrderDetailmasterModel;
        }
        public OrderDetailmasterModel AddOrderDetailado(OrderDetailmasterModel objOrderDetailmasterModel, int TransType)
        {
            try
            {
                tbl_OrderDetail objtbl_OrderDetail = new tbl_OrderDetail();
                DataTable dt = new DataTable();
                if (TransType == 1)//ADD
                {
                    dt.Columns.Add("OrderID", typeof(long));
                    dt.Columns.Add("ProductID", typeof(long)); 
                    dt.Columns.Add("ProductName", typeof(string));
                    dt.Columns.Add("UPCCode", typeof(string));
                    dt.Columns.Add("Quantity", typeof(decimal));
                    dt.Columns.Add("SellPrice", typeof(decimal));
                    dt.Columns.Add("Discount", typeof(decimal));
                    dt.Columns.Add("finalPrice", typeof(decimal));
                    dt.Columns.Add("StoreID", typeof(long));
                    dt.Columns.Add("CreatedDate", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(long));
                    dt.Columns.Add("IsScale", typeof(bool));
                    dt.Columns.Add("IsFoodStamp", typeof(bool));
                    dt.Columns.Add("IsTax", typeof(bool));
                    dt.Columns.Add("FoodStampTotal", typeof(decimal));
                    dt.Columns.Add("DiscountApplyed", typeof(bool));
                    dt.Columns.Add("TaxAmount", typeof(decimal)); 
                    dt.Columns.Add("IsRefund", typeof(bool));  
                    dt.Columns.Add("IsCancel", typeof(bool));
                    dt.Columns.Add("IsForceTax", typeof(bool)); 
                    dt.Columns.Add("DepartmentID", typeof(long));
                    dt.Columns.Add("SectionID", typeof(long));
                    dt.Columns.Add("CasePriceApplied", typeof(bool));
                    dt.Columns.Add("GroupPrice", typeof(decimal));
                    dt.Columns.Add("GroupQty", typeof(decimal)); 
                    dt.Columns.Add("CasePrice", typeof(decimal));
                    dt.Columns.Add("CaseQty", typeof(decimal));
                    dt.Columns.Add("IsTaxCarry", typeof(bool));
                    dt.Columns.Add("IsReturn", typeof(bool));
                    dt.Columns.Add("OverridePrice", typeof(decimal)); 
                    dt.Columns.Add("IsForceTaxDept", typeof(bool));
                    dt.Columns.Add("IsManWTRefund", typeof(bool));

                    DataRow row = dt.NewRow();
                    row["OrderID"] = objOrderDetailmasterModel.OrderID;
                    row["ProductID"] = objOrderDetailmasterModel.ProductID;
                    row["ProductName"] = objOrderDetailmasterModel.ProductName;
                    row["UPCCode"] = objOrderDetailmasterModel.UPCCode;
                    row["Quantity"] = objOrderDetailmasterModel.Quantity;
                    row["SellPrice"] = objOrderDetailmasterModel.SellPrice;
                    row["Discount"] = objOrderDetailmasterModel.Discount;
                    row["finalPrice"] = objOrderDetailmasterModel.finalPrice;
                    row["StoreID"] = objOrderDetailmasterModel.StoreID;
                    row["CreatedDate"] = objOrderDetailmasterModel.CreatedDate;
                    row["CreatedBy"] = objOrderDetailmasterModel.CreatedBy;
                    row["IsScale"] = objOrderDetailmasterModel.IsScale;
                    row["IsFoodStamp"] = objOrderDetailmasterModel.IsFoodStamp;
                    row["IsTax"] = objOrderDetailmasterModel.IsTax;
                    row["FoodStampTotal"] = objOrderDetailmasterModel.FoodStampTotal;
                    row["DiscountApplyed"] = objOrderDetailmasterModel.DiscountApplyed;
                    row["TaxAmount"] = objOrderDetailmasterModel.TaxAmount;
                    row["IsRefund"] = objOrderDetailmasterModel.IsRefund;
                    row["IsCancel"] = objOrderDetailmasterModel.IsCancel;
                    row["IsForceTax"] = objOrderDetailmasterModel.IsForceTax;
                    row["DepartmentID"] = objOrderDetailmasterModel.DepartmentID;
                    row["SectionID"] = objOrderDetailmasterModel.SectionID;
                    row["CasePriceApplied"] = objOrderDetailmasterModel.CasePriceApplied;
                    row["GroupPrice"] = objOrderDetailmasterModel.GroupPrice;
                    row["GroupQty"] = objOrderDetailmasterModel.GroupQty;
                    row["CasePrice"] = objOrderDetailmasterModel.CasePrice;
                    row["CaseQty"] = objOrderDetailmasterModel.CaseQty;
                    row["IsTaxCarry"] = 0;
                    if (objOrderDetailmasterModel.IsTaxCarry != null)
                    {
                        row["IsTaxCarry"] = objOrderDetailmasterModel.IsTaxCarry;
                    }
                    row["IsReturn"] = 0;
                    if(objOrderDetailmasterModel.IsReturn != null)
                    {
                        row["IsReturn"] = objOrderDetailmasterModel.IsReturn;
                    } 
                    row["OverridePrice"] = 0.00;
                    if (objOrderDetailmasterModel.IsReturn != null)
                    {
                        row["OverridePrice"] = objOrderDetailmasterModel.OverridePrice;
                    }
                    row["IsForceTaxDept"] = objOrderDetailmasterModel.IsForceTaxDept;
                    row["IsManWTRefund"] = objOrderDetailmasterModel.IsManWTRefund;

                    dt.Rows.Add(row);
                    dt.AcceptChanges();
                    var conStr = XMLData.DbConnectionString;
                    using (SqlConnection con = new SqlConnection(conStr))
                    {
                        using (SqlCommand cmd = new SqlCommand("Insert_OrderMasterDetail", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@orderhdrMaster", null);
                            cmd.Parameters.AddWithValue("@orderDetail", dt);
                            cmd.Parameters.AddWithValue("@intId", 2);
                            cmd.Parameters.Add("@IdentityValue", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                            con.Open();
                            cmd.ExecuteNonQuery();
                          //  LoginInfo.LastOrderID = Convert.ToInt64(cmd.Parameters["@IdentityValue"].Value);
                            con.Close();
                        }

                    }
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
                Functions.ErrorLog("OrderScannerDAL", "AddOrderDetail(tbl_OrderDetail)", e);
            }
            return objOrderDetailmasterModel;
        }

        public List<OrderDetailmasterModel> GetAllSubOrderDetail(long OrderID)
        {
            var lstOrderDetailmasterModel = new List<OrderDetailmasterModel>();

            var onjtbl_OrderMaster = _db.tbl_OrderDetail.Where(x => x.OrderID == OrderID).ToList();

            if (onjtbl_OrderMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_OrderDetail, OrderDetailmasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_OrderDetail objtbl_OrderDetail in onjtbl_OrderMaster)
                {
                    OrderDetailmasterModel _OrderDetailmasterModel = iMapper.Map<tbl_OrderDetail, OrderDetailmasterModel>(objtbl_OrderDetail);
                    lstOrderDetailmasterModel.Add(_OrderDetailmasterModel);
                }
            }
            return lstOrderDetailmasterModel;
        }

        public List<OrderMasterModel> GetSaleOrderDetail(DateTime FromDate, DateTime ToDate)
        {
            var lstOrderMasterModel = new List<OrderMasterModel>();
            var onjtbl_OrderMaster = _db.SP_GetOrderMasterList(LoginInfo.StoreID, FromDate, ToDate).ToList();

            if (onjtbl_OrderMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetOrderMasterList_Result, OrderMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetOrderMasterList_Result objtbl_OrderMaster in onjtbl_OrderMaster)
                {
                    OrderMasterModel _OrderMasterModel = iMapper.Map<SP_GetOrderMasterList_Result, OrderMasterModel>(objtbl_OrderMaster);
                    lstOrderMasterModel.Add(_OrderMasterModel);
                }
            }
            return lstOrderMasterModel;
        }
    }
}
