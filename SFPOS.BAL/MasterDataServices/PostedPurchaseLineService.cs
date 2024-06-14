using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    public class PostedPurchaseLineService
    {
        PostedPurchaseLineDAL objPostedPurchaseLineDAL = new PostedPurchaseLineDAL();
        public PostedPurchaseLineMasterModel AddPostedPurchaseLine(PostedPurchaseLineMasterModel objPostedPurchaseLineMasterModel)
        {
            return objPostedPurchaseLineDAL.AddPostedPurchaseLine(objPostedPurchaseLineMasterModel);
        }
    }
}
