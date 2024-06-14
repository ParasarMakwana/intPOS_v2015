using System;

namespace SFPOS.Entities.Reports
{
    public class RegisterSaleStatusByTransModel
    {
        public string CounterIP { get; set; }
        public string OrdNo { get; set; }
        public Nullable<bool> IsCancel { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TotalAmt { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> CouponDiscAmt { get; set; }
        public Nullable<decimal> FoodStampAmount { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> CheckAmount { get; set; }
        public Nullable<decimal> CreditCardAmount { get; set; }
        public Nullable<decimal> CheckRefund { get; set; }
        public Nullable<decimal> RefundAmount { get; set; }
        public Nullable<decimal> CancelAmount { get; set; }
        public decimal OverwritePriceTotal { get; set; }

    }
}
