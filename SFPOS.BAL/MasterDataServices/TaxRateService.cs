using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class TaxRateService
    {
        TaxRateDAL objTaxRateDAL = new TaxRateDAL();
        public TaxRateMasterModel AddTaxRate(TaxRateMasterModel objTaxRateMasterModel, int TransType)
        {
            return objTaxRateDAL.AddTaxRate(objTaxRateMasterModel, TransType);
        }
        public List<TaxRateMasterModel> GetAllTaxRate()
        {
            return objTaxRateDAL.GetAllTaxRate();
        }
        public bool CheckTaxRateName(long TaxRateName ,DateTime StartDate , DateTime EndDate , long PrimaryID)
        {
            return objTaxRateDAL.CheckTaxRateName(TaxRateName, StartDate, EndDate, PrimaryID);
        }
       
        public bool CheckName(decimal TaxRateName, DateTime StartDate, DateTime EndDate)
        {
            return objTaxRateDAL.CheckName(TaxRateName, StartDate, EndDate);
        }

        //public TaxRateMasterModel TaxRateDetail(int TaxRateID)
        //{
        //    return objTaxRateDAL.TaxRateDetail(TaxRateID);
        //}
    }
}
