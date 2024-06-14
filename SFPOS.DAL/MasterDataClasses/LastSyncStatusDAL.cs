using AutoMapper;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFPOS.Common;

namespace SFPOS.DAL.MasterDataClasses
{
    public class LastSyncStatusDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<LastSyncStatusMasterModel> GetAllLastSyncStatus()
        {
            var lstLastSyncStatusMasterModel = new List<LastSyncStatusMasterModel>();

            //var onjtbl_LastSyncStatusMaster = _db.tbl_LastSyncStatusMaster.Where(x => x.IsDelete == false).OrderBy(x => x.LastSyncStatusName).ToList();

            var onjtbl_LastSyncStatusMaster = _db.SP_GetDataSyncStatus().ToList();

            if (onjtbl_LastSyncStatusMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetDataSyncStatus_Result, LastSyncStatusMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetDataSyncStatus_Result objtbl_LastSyncStatusMaster in onjtbl_LastSyncStatusMaster)
                {
                    LastSyncStatusMasterModel _LastSyncStatusMasterModel = iMapper.Map<SP_GetDataSyncStatus_Result, LastSyncStatusMasterModel>(objtbl_LastSyncStatusMaster);
                    lstLastSyncStatusMasterModel.Add(_LastSyncStatusMasterModel);
                }
            }
            return lstLastSyncStatusMasterModel;
        }
    }
}
