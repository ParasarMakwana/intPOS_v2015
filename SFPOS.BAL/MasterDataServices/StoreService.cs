using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPOS.DAL;
using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;

namespace SFPOS.BAL.MasterDataServices
{
    public class StoreService
    {
        StoreDAL objStoreDAL = new StoreDAL();
        public StoreMasterModel AddStore(StoreMasterModel objStoreMasterModel, int TransType)
        {
            return objStoreDAL.AddStore(objStoreMasterModel, TransType);
        }
        public List<StoreMasterModel> GetAllStore()
        {
            return objStoreDAL.GetAllStore();
        }
        public bool CheckStoreName(string StoreName ,long PrimaryId)
        {
            return objStoreDAL.CheckStoreName(StoreName , PrimaryId);
        }
        public bool CheckName(string StoreName)
        {
            return objStoreDAL.CheckName(StoreName);
        }

        public long GetStoreID(string StoreName)
        {
            return objStoreDAL.GetStoreID(StoreName);
        }
        //public StoreMasterModel StoreDetail(int StoreID)
        //{
        //    return objStoreDAL.StoreDetail(StoreID);
        //}
    }
}
