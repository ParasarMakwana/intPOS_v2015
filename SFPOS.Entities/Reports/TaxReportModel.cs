using System;

namespace SFPOS.Entities.Reports
{
    public class TaxReportModel
    {
        public string DepartmentName { get; set; }
        public Nullable<decimal> TOTALSALES { get; set; }
        public Nullable<decimal> TAXEXEMPTSALES { get; set; }
        public Nullable<decimal> FOODSTAMPEXEMPTEDSALES { get; set; }
        public Nullable<decimal> TAXABLESALES { get; set; }
        public Nullable<decimal> SALESTAXCOLLECTED { get; set; }
        public Nullable<decimal> SUBTOTAL { get; set; }
    }
}
