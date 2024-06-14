using AutoMapper;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFPOS.Common;

namespace SFPOS.DAL.MasterDataClasses
{
    public class CountryDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<CountryMasterModel> GetAllCountry()
        {
            var lstCountryMasterModel = new List<CountryMasterModel>();

            var onjtbl_Country = _db.tbl_Country.OrderBy(x => x.CountryName).ToList();

            if (onjtbl_Country.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_Country, CountryMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_Country objtbl_Country in onjtbl_Country)
                {
                    CountryMasterModel _CountryMasterModel = iMapper.Map<tbl_Country, CountryMasterModel>(objtbl_Country);
                    lstCountryMasterModel.Add(_CountryMasterModel);
                }
            }
            return lstCountryMasterModel;
        }

        public bool CheckName(string CountryName)
        {
            bool result = false;
            try
            {
                tbl_Country _tbl_Country = _db.tbl_Country.FirstOrDefault(x => x.CountryName == CountryName);
                if (_tbl_Country != null)
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

        public long GetCountryID(string CountryName)
        {
            long result = 0;
            try
            {
                tbl_Country _tbl_Country = _db.tbl_Country.FirstOrDefault(x => x.CountryName == CountryName);
                if (_tbl_Country != null)
                {
                    result = _tbl_Country.CountryID;
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
