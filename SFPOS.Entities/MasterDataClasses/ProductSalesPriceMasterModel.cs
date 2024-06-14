using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ProductSalesPriceMasterModel
    {
        public string ProductName { get; set; }
        public long ProductSalePriceID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
    }
    public class ProductSalesPriceMasterModelCont
    {
        public const string ProductSalePriceID = "ProductSalePriceID";
        public const string SellPrice = "SellPrice";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string UpdatedBy = "UpdatedBy";
        public const string UpdatedDate = "UpdatedDate";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string IsDelete = "IsDelete";
        public const string IsActive = "IsActive";
    }
}
