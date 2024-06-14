using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class ProductSalesPriceService
    {
        ProductSalesPriceDAL objProductSalesPriceDAL = new ProductSalesPriceDAL();
        public ProductSalesPriceMasterModel AddProductSalesPrice(ProductSalesPriceMasterModel objProductSalesPriceMasterModel, int TransType)
        {
            return objProductSalesPriceDAL.AddEditDeleteProductSalesPrice(objProductSalesPriceMasterModel, TransType);
        }

        public List<ProductSalesPriceMasterModel> GetAllProductSalesPrice(long ProductID)
        {
            return objProductSalesPriceDAL.GetAllProductSalesPrice(ProductID);
        }
        public bool CheckUniqueDate(DateTime StartDate, DateTime EndDate, long PrimaryID)
        {
            return objProductSalesPriceDAL.CheckUniqueDate(StartDate, EndDate, PrimaryID);
        }
        public bool CheckName(long ProductID, decimal Discount, DateTime StartDate, DateTime EndDate)
        {
            return objProductSalesPriceDAL.CheckName(ProductID, Discount, StartDate, EndDate);
        }
        //public ProductSalesPriceMasterModel VendorDetail(int ProductSalePriceID)
        //{
        //    return objProductSalesPriceDAL.ProductSalesPricedetail(ProductSalePriceID);
        //}
    }
}
