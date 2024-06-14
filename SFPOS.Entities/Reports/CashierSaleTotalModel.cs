using System;

namespace SFPOS.Entities.Reports
{
    public class CashierSaleTotalModel
    {
        public string EmpName { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public Nullable<decimal> CancelAmount { get; set; }
        public Nullable<decimal> OverridePriceTotal { get; set; }        
        public string CreatedDate { get; set; }
    }
}
