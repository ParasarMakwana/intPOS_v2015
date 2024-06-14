using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    public class ProductLedgerService
    {
        ProductLedgerDAL objProductLedgerDAL = new ProductLedgerDAL();
        public ProductLedgerMasterModel AddProductLedger(ProductLedgerMasterModel objProductLedgerMasterModel, int TransType)
        {
            return objProductLedgerDAL.AddProductLedger(objProductLedgerMasterModel, TransType);
        }
    }
}
