//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SFPOS.DAL
{
    using System;
    
    public partial class SP_TillStatusEmployeeWise_Result
    {
        public Nullable<decimal> GrossAmount { get; set; }
        public Nullable<decimal> FoodStampAmount { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> CheckAmount { get; set; }
        public Nullable<decimal> CreditCardAmount { get; set; }
        public Nullable<decimal> CheckRefund { get; set; }
        public Nullable<decimal> RefundAmount { get; set; }
        public Nullable<int> OrderCount { get; set; }
        public Nullable<int> RefundCount { get; set; }
        public Nullable<decimal> CancelledAmount { get; set; }
        public Nullable<int> CancelledCount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<int> OverridePriceCount { get; set; }
        public Nullable<decimal> OverridePriceTotal { get; set; }
    }
}
