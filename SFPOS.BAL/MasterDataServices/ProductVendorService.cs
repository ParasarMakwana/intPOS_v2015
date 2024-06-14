using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class ProductVendorService
    {
        ProductVendorDAL objProductVendorDAL = new ProductVendorDAL();
        public ProductVendorMasterModel AddProductVendor(ProductVendorMasterModel objProductVendorMasterModel, int TransType)
        {
            return objProductVendorDAL.AddEditDeleteProductVendor(objProductVendorMasterModel, TransType);
        }
        public List<ProductVendorMasterModel> GetAllProductVendor(long ProductID)
        {
            return objProductVendorDAL.GetAllProductVendor(ProductID);
        }
        public bool CheckProductVendorName(long ProductID,long VendorID)
        {
            return objProductVendorDAL.CheckProductVendorName(ProductID, VendorID);
        }

        public string CheckProductItemCode(string itemCode,long ProductID, long VendorID)
        {
            return objProductVendorDAL.CheckProductItemCode(itemCode, ProductID, VendorID);
        }

        public bool CheckName(string ItemCode)
        {
            return objProductVendorDAL.CheckName(ItemCode);
        }
        //public ProductVendorMasterModel VendorDetail(int VendorID)
        //{
        //    return objProductVendorDAL.ProductVendorDetail(VendorID);
        //}
    }
}
