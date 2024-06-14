using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class ProductSalesDiscountService
    {
        ProductSalesDiscountDAL objProductSaleDiscountDAL = new ProductSalesDiscountDAL();
        public ProductSalesDiscountMasterModel AddProductSaleDiscount(ProductSalesDiscountMasterModel objProductSaleDiscountMasterModel, int TransType)
        {
            return objProductSaleDiscountDAL.AddEditDeleteProductSalesDiscount(objProductSaleDiscountMasterModel, TransType);
        }
        public List<ProductSalesDiscountMasterModel> GetAllProductSaleDiscount(long ProductID)
        {
            return objProductSaleDiscountDAL.GetAllProductSalesDiscount(ProductID);
        }
        public bool CheckUniqueDate(DateTime StartDate, DateTime EndDate, long PrimaryId)
        {
            return objProductSaleDiscountDAL.CheckUniqueDate(StartDate, EndDate, PrimaryId);
        }
        public bool CheckName(long ProductID, decimal Discount, DateTime StartDate, DateTime EndDate)
        {
            return objProductSaleDiscountDAL.CheckName(ProductID, Discount, StartDate, EndDate);
        }
        //public ProductSaleDiscountMasterModel VendorDetail(int ProductSalePriceID)
        //{
        //    return objProductSaleDiscountDAL.ProductSaleDiscountdetail(ProductSalePriceID);
        //}
    }
}
