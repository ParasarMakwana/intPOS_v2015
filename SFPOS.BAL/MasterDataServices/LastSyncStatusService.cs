using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    public class LastSyncStatusService
    {
        LastSyncStatusDAL objLastSyncStatusDAL = new LastSyncStatusDAL();
        public List<LastSyncStatusMasterModel> GetAllLastSyncStatus()
        {
            return objLastSyncStatusDAL.GetAllLastSyncStatus();
        }
    }
}
