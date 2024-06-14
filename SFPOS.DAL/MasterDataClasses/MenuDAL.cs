using AutoMapper;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFPOS.Common;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class MenuDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public List<MenuMasterModel> GetAllMenu()
        {
            var lstMenuMasterModel = new List<MenuMasterModel>();

            var onjtbl_MenuMaster = _db.tbl_MenuMaster.ToList();

            if (onjtbl_MenuMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_MenuMaster, MenuMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_MenuMaster objtbl_MenuMaster in onjtbl_MenuMaster)
                {
                    MenuMasterModel _MenuMasterModel = iMapper.Map<tbl_MenuMaster, MenuMasterModel>(objtbl_MenuMaster);
                    lstMenuMasterModel.Add(_MenuMasterModel);
                }
            }
            return lstMenuMasterModel;
        }
    }
}
