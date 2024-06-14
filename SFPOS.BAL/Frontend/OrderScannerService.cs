using SFPOS.DAL;
using SFPOS.DAL.Frontend;
using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class OrderScannerService
    {
        OrderScannerDAL objOrderScannerDAL = new OrderScannerDAL();
        public List<OrderScanner_ResultModel> GetScannedUPCCode(string UPCCode)
        {
            return objOrderScannerDAL.GetScannedUPCCode(UPCCode);
        }
        public OrderMasterModel AddOrder(OrderMasterModel objOrderMasterModel, int TransType)
        {
            return objOrderScannerDAL.AddOrder(objOrderMasterModel, TransType);
        }
        public OrderMasterModel AddOrderAdo(OrderMasterModel objOrderMasterModel, int TransType)
        {
            return objOrderScannerDAL.AddOrderAdo(objOrderMasterModel, TransType);
        }
        public OrderDetailmasterModel AddOrderDetail(OrderDetailmasterModel objOrderDetailMasterModel, int TransType)
        {
            return objOrderScannerDAL.AddOrderDetail(objOrderDetailMasterModel, TransType);
        }
        public OrderDetailmasterModel AddOrderDetailado(OrderDetailmasterModel objOrderDetailMasterModel, int TransType)
        {
            return objOrderScannerDAL.AddOrderDetailado(objOrderDetailMasterModel, TransType);
        }
        public List<OrderMasterModel> GetAllOrderDetail()
        {
            return objOrderScannerDAL.GetAllOrderDetail();
        }
        public List<OrderDetailmasterModel> GetAllSubOrderDetail(long OrderID)
        {
            return objOrderScannerDAL.GetAllSubOrderDetail(OrderID);
        }
        public List<OrderMasterModel> GetSaleOrderDetail(DateTime FromDate, DateTime ToDate)
        {
            return objOrderScannerDAL.GetSaleOrderDetail(FromDate, ToDate);
        }
       
    }
}
