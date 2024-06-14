using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;

namespace SFPOS.BAL.MasterDataServices
{
    public class PostedPurchaseHeaderService
    {
        PostedPurchaseHeaderDAL objPostedPurchaseHeaderDAL = new PostedPurchaseHeaderDAL();
        public PostedPurchaseHeaderMasterModel AddPostedPurchaseHeader(PostedPurchaseHeaderMasterModel objPostedPurchaseHeaderMasterModel)
        {
            return objPostedPurchaseHeaderDAL.AddPostedPurchaseHeader(objPostedPurchaseHeaderMasterModel);
        }
    }
}
