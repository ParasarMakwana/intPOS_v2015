using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class GetPurchaseLine_ResultModel
    {
        public Nullable<long> PurchaseLineID { get; set; }
        public Nullable<long> PurchaseHeaderID { get; set; }
        public string ProductName { get; set; }
        public long? ProductID { get; set; }
        public string UPCCode { get; set; }
        public string ItemCode { get; set; }
        public decimal? Quantity { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public Nullable<long> SectionID { get; set; }
        public Nullable<long> UnitMeasureID { get; set; }
        public Nullable<long> TaxGroupID { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> LineAmtExclTax { get; set; }
        public Nullable<decimal> LineAmtInclTax { get; set; }
    }
}
