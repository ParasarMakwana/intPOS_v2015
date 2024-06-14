using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class PostedPurchaseLineMasterModel
    {
        public long PostedPurchaseLineID { get; set; }
        public Nullable<long> PostedPurchaseHeaderID { get; set; }
        public Nullable<long> PurchaseLineID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string ItemCode { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<long> TaxGroupID { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> LineAmtExclTax { get; set; }
        public Nullable<decimal> LineAmtInclTax { get; set; }
        public string PurchaseType { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
    }
}
