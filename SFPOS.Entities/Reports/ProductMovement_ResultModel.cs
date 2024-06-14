using System;

namespace SFPOS.Entities.Reports
{
    public class ProductMovement_ResultModel
    {
        public Nullable<System.DateTime> datevalue { get; set; }
        public decimal Qty { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Total_Cost { get; set; }
        public string UPCCode { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public string ItemCode { get; set; }
        public string Total_Revenue { get; set; }
        public string Margin { get; set; }
        public string Gross_Profit { get; set; }

        public long ProductID { get; set; }
        //public string UPCCode { get; set; }
        //public string ProductName { get; set; }
        public Nullable<long> ProductVendorID { get; set; }
        //public string ItemCode { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<decimal> Price { get; set; }
        //public string VendorName { get; set; }
    }
}
