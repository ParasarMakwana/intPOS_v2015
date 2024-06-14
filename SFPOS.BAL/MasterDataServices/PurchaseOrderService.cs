using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class PurchaseOrderService
    {
        PurchaseOrderDAL objPurchaseOrderDAL = new PurchaseOrderDAL();
        public PurchaseOrderMasterModel AddEditDeletePurchaseOrder(PurchaseOrderMasterModel objPurchaseOrderMasterModel, int TransType)
        {
            return objPurchaseOrderDAL.AddEditDeletePurchaseOrder(objPurchaseOrderMasterModel, TransType);
        }
        public List<PurchaseOrderMasterModel> GetAllPurchaseOrder()
        {
            return objPurchaseOrderDAL.GetAllPurchaseOrder();
        }
        public PurchaseOrderMasterModel PurchaseOrderDetail(int PurchaseOrderID)
        {
            return objPurchaseOrderDAL.PurchaseOrderDetail(PurchaseOrderID);
        }
        public bool CheckInvoiceNo(string InvoiceNo,long PrimaryId)
        {
            return objPurchaseOrderDAL.CheckInvoiceNo(InvoiceNo, PrimaryId);
        }
    }
}
