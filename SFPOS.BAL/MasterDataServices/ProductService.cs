using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class ProductService
    {
        ProductDAL objProductDAL = new ProductDAL();
        public ProductMasterModel AddProduct(ProductMasterModel objProductMasterModel, int TransType)
        {
            return objProductDAL.AddEditDeleteProduct(objProductMasterModel, TransType);
        }
        public List<ProductMasterModel> GetAllProduct(string SearchString,bool IsDynamic)
        {
            return objProductDAL.GetAllProduct(SearchString, IsDynamic);
        }
        public List<ProductMasterModel> GetAllProduct_With_Paging(string SearchString, int pageNo)
        {
            return objProductDAL.GetAllProduct_With_Paging(SearchString, pageNo);
        }
        public bool CheckProductDetails(string ProductName, string UPCCode, long ProductID)
        {
            return objProductDAL.CheckProductDetails(ProductName, UPCCode, ProductID);
        }
        public ProductMasterModel DeleteAllProduct(ProductMasterModel objProductMasterModel)
        {
            return objProductDAL.DeleteAllProduct(objProductMasterModel);
        }
        public List<ProductMasterModel> GetVendorWiseProduct()
        {
            return objProductDAL.GetVendorWiseProduct();
        }

        public bool CheckName(string ProductName)
        {
            return objProductDAL.CheckName(ProductName);
        }

        public long GetProductID(string ProductName)
        {
            return objProductDAL.GetProductID(ProductName);
        }

        public string GetUPCCode(string UPCCode)
        {
            return objProductDAL.GetUPCCode(UPCCode);
        }
        public ProductMasterModel ProductDetail(long ProductID)
        {
            return objProductDAL.ProductDetail(ProductID);
        }

        public ProductMasterModel GetArchivedUPCCode(string UPCCode)
        {
            return objProductDAL.GetArchivedUPCCode(UPCCode);
        }
    }
}
