//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SFPOS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_OrderDetail
    {
        public long OrderDetailID { get; set; }
        public Nullable<long> OrderID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string UPCCode { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> finalPrice { get; set; }
        public Nullable<long> StoreID { get; set; }
        public Nullable<bool> IsScale { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public Nullable<bool> IsTax { get; set; }
        public Nullable<decimal> FoodStampTotal { get; set; }
        public Nullable<bool> DiscountApplyed { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<bool> IsRefund { get; set; }
        public Nullable<bool> IsCancel { get; set; }
        public Nullable<bool> IsForceTax { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public Nullable<long> SectionID { get; set; }
        public Nullable<bool> CasePriceApplied { get; set; }
        public Nullable<decimal> GroupQty { get; set; }
        public Nullable<decimal> GroupPrice { get; set; }
        public Nullable<decimal> CaseQty { get; set; }
        public Nullable<decimal> CasePrice { get; set; }
        public Nullable<bool> IsTaxCarry { get; set; }
        public Nullable<bool> IsReturn { get; set; }
        public Nullable<decimal> OverridePrice { get; set; }
        public Nullable<bool> IsForceTaxDept { get; set; }
        public Nullable<bool> IsSync { get; set; }
        public Nullable<bool> IsManWTRefund { get; set; }
    }
}