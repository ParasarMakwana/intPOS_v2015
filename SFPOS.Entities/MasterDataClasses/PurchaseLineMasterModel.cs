using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class PurchaseLineMasterModel
    {
        public long PurchaseLineID { get; set; }
        public Nullable<long> PurchaseHeaderID { get; set; }
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
        public System.DateTime CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public bool IsDelete { get; set; }
        public bool isReceived { get; set; }
    }
    public class PurchaseLineMasterModelCont
    {
        public const string PurchaseLineID = "PurchaseLineID";
        public const string ItemCode = "ItemCode";
        public const string Quantity = "Quantity";
        public const string UnitCost = "UnitCost";
        public const string Tax = "Tax";
        public const string TaxAmount = "TaxAmount";
        public const string LineAmtExclTax = "LineAmtExclTax";
        public const string LineAmtInclTax = "LineAmtInclTax";
        public const string UPCCode = "UPCCode";
        public const string Received = "Received";
    }
}
