using System;

namespace SFPOS.Entities.Reports
{
    public class RegisterSaleTotalModel
    {
        public string CounterIP { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> OverridePriceTotal { get; set; }
        public string CreatedDate { get; set; }
    }
}
