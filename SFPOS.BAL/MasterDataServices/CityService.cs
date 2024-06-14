using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class CityService
    {
        CityDAL objCityDAL = new CityDAL();
        public List<CityMasterModel> GetAllCity(int Stateid)
        {
            return objCityDAL.GetAllCity(Stateid);
        }

        public List<CityMasterModel> GetCity()
        {
            return objCityDAL.GetCity();
        }
        public bool CheckName(string CityName)
        {
            return objCityDAL.CheckName(CityName);
        }

        public long GetCityID(string CityName)
        {
            return objCityDAL.GetCityID(CityName);
        }
    }
}
