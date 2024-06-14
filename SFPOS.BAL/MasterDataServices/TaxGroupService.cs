using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class TaxGroupService
    {
        TaxGroupDAL objTaxGroupDAL = new TaxGroupDAL();
        public TaxGroupMasterModel AddTaxGroup(TaxGroupMasterModel objTaxGroupMasterModel, int TransType)
        {
            return objTaxGroupDAL.AddTaxGroup(objTaxGroupMasterModel, TransType);
        }
        public List<TaxGroupMasterModel> GetAllTaxGroup()
        {
            return objTaxGroupDAL.GetAllTaxGroup();
        }
        public bool CheckTaxGroupName(string TaxGroupName)
        {
            return objTaxGroupDAL.CheckTaxGroupName(TaxGroupName);
        }
        public bool CheckName(string TaxGroup)
        {
            return objTaxGroupDAL.CheckName(TaxGroup);
        }

        public long GetTaxGroupID(string TaxGroup)
        {
            return objTaxGroupDAL.GetTaxGroupID(TaxGroup);
        }
        //public TaxGroupMasterModel TaxGroupDetail(int TaxGroupID)
        //{
        //    return objTaxGroupDAL.TaxGroupDetail(TaxGroupID);
        //}
    }
}
