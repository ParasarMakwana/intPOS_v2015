using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class OrderDetailmasterModel
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
        public decimal GroupQty { get; set; }
        public decimal GroupPrice { get; set; }
        public decimal CaseQty { get; set; }
        public decimal CasePrice { get; set; }
        public Nullable<bool> IsTaxCarry { get; set; }
        public Nullable<bool> IsReturn { get; set; }
        public Nullable<decimal> OverridePrice { get; set; }
        public bool IsForceTaxDept { get; set; }     
        public bool IsSync { get; set; }
        public bool IsManWTRefund { get; set; }
    }
}
