using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities;
using SFPOS.Entities.spDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class SectionService
    {
        SectionDAL objSectionDAL = new SectionDAL();
        public SectionMasterModel AddSection(SectionMasterModel objSectionModel, int TransType)
        {
            return objSectionDAL.AddSection(objSectionModel, TransType);
        }
        public List<SectionMasterModel> GetAllSection()
        {
            return objSectionDAL.GetAllSection();
        }
        public bool CheckSectionName(string SectionName)
        {
            return objSectionDAL.CheckSectionName(SectionName);
        }
        public List<SP_GetSectionList_Result_Model> SectionDetail(int SectionID)
        {
            return objSectionDAL.SectionDetail(SectionID);
        }

        public long GetSectionID(string SectionName)
        {
            return objSectionDAL.GetSectionID(SectionName);
        }
        public bool CheckName(string SectionName)
        {
            return objSectionDAL.CheckName(SectionName);
        }
        public SectionMasterModel SectionDetailBySecID(long SectionID)
        {
            return objSectionDAL.SectionDetailBySecID(SectionID);
        }
    }
}
