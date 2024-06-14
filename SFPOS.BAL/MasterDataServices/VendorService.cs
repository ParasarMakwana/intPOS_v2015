using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class VendorService
    {
        VendorDAL objVendorDAL = new VendorDAL();
        public VendorMasterModel AddVendor(VendorMasterModel objVendorMasterModel, int TransType)
        {
            return objVendorDAL.AddVendor(objVendorMasterModel, TransType);
        }
        public List<VendorMasterModel> GetAllVendor()
        {
            return objVendorDAL.GetAllVendor();
        }
        public bool CheckVendorName(string VendorName,long PrimaryId)
        {
            return objVendorDAL.CheckVendorName(VendorName, PrimaryId);
        }
        public bool CheckName(string VendorName)
        {
            return objVendorDAL.CheckName(VendorName);
        }

        public long GetVendorID(string VendorName)
        {
            return objVendorDAL.GetVendorID(VendorName);
        }
        //public VendorMasterModel VendorDetail(int VendorID)
        //{
        //    return objVendorDAL.VendorDetail(VendorID);
        //}
    }
}
