using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;

namespace SFPOS.BAL.MasterDataServices
{
    public class ProductUoMService
    {
        ProductUoMDAL objProductUoMDAL = new ProductUoMDAL();
        public ProductUoMMasterModel AddProductUoM(ProductUoMMasterModel objProductUoMMasterModel, int TransType)
        {
            return objProductUoMDAL.AddEditDeleteProductUoM(objProductUoMMasterModel, TransType);
        }
        //public List<ProductUoMMasterModel> GetAllProductUoM()
        //{
        //    return objProductUoMDAL.GetAllProductUoM();
        //}
        public bool CheckProductUoMName(long ProductUoM,long ProductUoMID)
        {
            return objProductUoMDAL.CheckProductUoMName(ProductUoM, ProductUoMID);
        }
        //public ProductUoMMasterModel ProductUoMDetail(int ProductUoMID)
        //{
        //    return objProductUoMDAL.ProductUoMDetail(ProductUoMID);
        //}
        //public List<ProductUoMMasterModel> GetProductUOM()
        //{
        //    return objProductUoMDAL.GetProductUOM();
        //}
    }
}
