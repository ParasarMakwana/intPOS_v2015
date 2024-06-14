using System;

namespace SFPOS.Entities.Reports
{
    public class Section_ResultModel
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string UPCCode { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public decimal NORMAL_PRICE { get; set; }
        public decimal NORMAL_COST { get; set; }
        public Nullable<decimal> GROSS_PROFIT { get; set; }
        public string SectionName { get; set; }

    }
    public class Section_ResultModelCont
    {
        public const string ProductID = "ProductID";
        public const string ProductName = "ProductName";
        public const string UPCCode = "UPCCode";
        public const string Price = "Price";
        public const string Discount = "Discount";
        public const string NORMAL_PRICE = "NORMAL_PRICE";
        public const string NORMAL_COST = "NORMAL_COST";
        public const string GROSS_PROFIT = "GROSS_PROFIT";
        public const string SectionName = "SectionName"; 
    }


}
