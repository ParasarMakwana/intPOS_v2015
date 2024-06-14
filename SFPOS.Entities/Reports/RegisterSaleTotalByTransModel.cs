using System;

namespace SFPOS.Entities.Reports
{
    public class RegisterSaleTotalByTransModel
    {
        public string CounterIP { get; set; }
        public string OrdNo { get; set; }
        public Nullable<bool> IsCancel { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> CancelAmount { get; set; }
        public Nullable<decimal> OverridePriceTotal { get; set; }        
        public string CreatedDate { get; set; }
    }
}
