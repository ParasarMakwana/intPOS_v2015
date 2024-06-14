using System;

namespace SFPOS.Entities.Reports
{
    public class CashierSaleStatusModel
    {
        public string EmpName { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TotalAmt { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> FoodStampAmount { get; set; }
        public Nullable<decimal> CheckAmount { get; set; }
        public Nullable<decimal> CreditCardAmount { get; set; }
        public Nullable<decimal> CheckRefund { get; set; }
        public Nullable<decimal> RefundAmount { get; set; }
        public Nullable<decimal> CancelledAmount { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public Nullable<decimal> CouponDiscAmt { get; set; }
        public Nullable<decimal> OverwritePriceTotal { get; set; }

    }
}
