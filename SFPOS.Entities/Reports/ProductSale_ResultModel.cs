using System;

namespace SFPOS.Entities.Reports
{
    public class ProductSale_ResultModel
    {
        public string UPCCode { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> TOTAL_SALES_QTY { get; set; }
        public Nullable<decimal> TOTAL_SALES_PRICE { get; set; }
        public bool Taxable { get; set; }
        public string Vendor { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
    }
}
