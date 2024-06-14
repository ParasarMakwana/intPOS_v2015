using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ProductLedgerMasterModel
    {
        public long ProductLedgerID { get; set; }
        public Nullable<long> OrderLineID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> LedgerTypeID { get; set; }
        public Nullable<long> OrderID { get; set; }
        public Nullable<long> PostedPurchaseHeaderID { get; set; }
        public Nullable<long> StoreID { get; set; }
        public Nullable<long> TaxGroupCodeID { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public Nullable<long> SectionID { get; set; }
        public Nullable<long> UnitOfMeasureID { get; set; }
        public string UPCCode { get; set; }
        public string QRCode { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> RemainingQty { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<decimal> FinalPrice { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<bool> IsForceTax { get; set; }
        public Nullable<decimal> OverridePrice { get; set; }        
    }
}
