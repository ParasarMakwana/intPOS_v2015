using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class CityDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<CityMasterModel> GetAllCity(int Stateid)
        {
            var lstCityMasterModel = new List<CityMasterModel>();

            var onjtbl_City = _db.tbl_City.Where(x => x.StateID == Stateid).OrderBy(x => x.CityName).ToList();

            if (onjtbl_City.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_City, CityMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_City objtbl_City in onjtbl_City)
                {
                    CityMasterModel _CityMasterModel = iMapper.Map<tbl_City, CityMasterModel>(objtbl_City);
                    lstCityMasterModel.Add(_CityMasterModel);
                }
            }
            return lstCityMasterModel;
        }

        public List<CityMasterModel> GetCity()
        {
            var lstCityMasterModel = new List<CityMasterModel>();

            var onjtbl_City = _db.tbl_City.OrderBy(x => x.CityName).ToList();

            if (onjtbl_City.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_City, CityMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_City objtbl_City in onjtbl_City)
                {
                    CityMasterModel _CityMasterModel = iMapper.Map<tbl_City, CityMasterModel>(objtbl_City);
                    lstCityMasterModel.Add(_CityMasterModel);
                }
            }
            return lstCityMasterModel;
        }

        public bool CheckName(string CityName)
        {
            bool result = false;
            try
            {
                tbl_City _tbl_City = _db.tbl_City.FirstOrDefault(x => x.CityName == CityName);
                if (_tbl_City != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        public long GetCityID(string CityName)
        {
            long result = 0;
            try
            {
                tbl_City _tbl_City = _db.tbl_City.FirstOrDefault(x => x.CityName == CityName);
                if (_tbl_City != null)
                {
                    result = _tbl_City.CityID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

    }
}
