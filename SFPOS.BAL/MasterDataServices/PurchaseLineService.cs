using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class PurchaseLineService
    {
        PurchaseLineDAL objPurchaseOrderDAL = new PurchaseLineDAL();
        public PurchaseLineMasterModel AddEditDeletePurchaseOrder(PurchaseLineMasterModel PurchaseLineMasterModel, int TransType)
        {
            return objPurchaseOrderDAL.AddEditDeletePurchaseOrder(PurchaseLineMasterModel, TransType);
        }
        public List<GetPurchaseLine_ResultModel> GetAllPurchaseLine(string UPCCode,long PurchaseHeaderId)
        {
            return objPurchaseOrderDAL.GetAllPurchaseLine(UPCCode,PurchaseHeaderId);
        }
        public bool PurchaseOrderDetail(long PurchaseHeaderID , long ProductID)
        {
            return objPurchaseOrderDAL.PurchaseOrderDetail(PurchaseHeaderID , ProductID);
        }
    }
}
