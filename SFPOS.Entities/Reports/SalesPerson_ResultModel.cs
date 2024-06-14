using System;

namespace SFPOS.Entities.Reports
{
    public class SalesPerson_ResultModel
    {
        public string UPCCode { get; set; }
        public string ProductName { get; set; }
        public string FirstName { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Total_Amount { get; set; }
    }
    public class SalesPerson_ResultModelCont
    {
        public const string ProductName = "ProductName";
        public const string UPCCode = "UPCCode";
        public const string FirstName = "FirstName";
        public const string Quantity = "Quantity";
        public const string Total_Amount = "Total_Amount";
    }
}
