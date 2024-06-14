using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class CountryService
    {
        CountryDAL objCountryDAL = new CountryDAL();
        public List<CountryMasterModel> GetAllCountry()
        {
            return objCountryDAL.GetAllCountry();
        }
        public bool CheckName(string CountryName)
        {
            return objCountryDAL.CheckName(CountryName);
        }

        public long GetCountryID(string CountryName)
        {
            return objCountryDAL.GetCountryID(CountryName);
        }
    }
}
