using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class UoMService
    {
        UoMDAL objUoMDAL = new UoMDAL();
        public UoMMasterModel AddEditDeleteUoM(UoMMasterModel objUoMMasterModel, int TransType)
        {
            return objUoMDAL.AddEditDeleteUOM(objUoMMasterModel, TransType);
        }
        public List<UoMMasterModel> GetAllUoM()
        {
            return objUoMDAL.GetAllUoM();
        }
        public bool CheckUoMCode(string UoMName,int PrimaryID)
        {
            return objUoMDAL.CheckUoMName(UoMName, PrimaryID);
        }

        public bool CheckName(string UnitMeasureCode)
        {
            return objUoMDAL.CheckName(UnitMeasureCode);
        }

        public long GetUnitMeasureID(string UnitMeasureCode)
        {
            return objUoMDAL.GetUnitMeasureID(UnitMeasureCode);
        }
        //public UoMMasterModel UoMDetail(int UoMID)
        //{
        //    return objUoMDAL.UoMDetail(UoMID);
        //}
    }
}
