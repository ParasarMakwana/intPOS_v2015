using System;

namespace SFPOS.Entities.Reports
{
    public class CashierSaleTotalByTransModel
    {
        public long EmployeeID { get; set; }
        public string EmpName { get; set; }
        public string OrdNo { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> OverridePriceTotal { get; set; }        
        public Nullable<decimal> TotalSalesAmount { get; set; }
        public string CreatedDate { get; set; }
    }
}
