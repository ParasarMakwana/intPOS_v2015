﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ProductSalesDiscountMasterModel
    {
        public string ProductName { get; set; }
        public long ProductSaleDiscountID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
    }
    public class ProductSalesDiscountMasterModelCont
    {
        public const string StartDate = "StartDate";
        public const string Discount = "Discount";
        public const string EndDate = "EndDate";
        public const string ProductSaleDiscountID = "ProductSaleDiscountID";
        public const string ProductID = "ProductID";
        public const string ProductName = "ProductName";
        public const string UpdatedBy = "UpdatedBy";
        public const string UpdatedDate = "UpdatedDate";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string IsDelete = "IsDelete";
        public const string IsActive = "IsActive";

    }
}