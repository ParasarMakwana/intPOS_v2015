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
    
    public partial class SP_ReciptDetails_Result
    {
        public Nullable<bool> IsCancel { get; set; }
        public long OrderID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> GrossAmount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> PaymentMethodID { get; set; }
        public string OrdNo { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> CheckAmount { get; set; }
        public Nullable<decimal> CreditCardAmount { get; set; }
        public Nullable<decimal> FoodStampAmount { get; set; }
        public Nullable<decimal> RefundAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> ChangeAmount { get; set; }
        public Nullable<decimal> TaxableAmount { get; set; }
        public string UPCCode { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> finalPrice { get; set; }
        public Nullable<bool> IsScale { get; set; }
        public Nullable<bool> ManWT { get; set; }
        public Nullable<decimal> GroupQty { get; set; }
        public Nullable<decimal> GroupPrice { get; set; }
        public Nullable<decimal> CaseQty { get; set; }
        public Nullable<decimal> CasePrice { get; set; }
        public string CouponCode { get; set; }
        public Nullable<decimal> CouponDiscAmt { get; set; }
        public Nullable<bool> DiscountApplyed { get; set; }
        public Nullable<decimal> TaxExempted { get; set; }
        public Nullable<bool> CasePriceApplied { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public Nullable<bool> IsTax { get; set; }
        public string StoreName { get; set; }
        public string SMAddress { get; set; }
        public string SAddress2 { get; set; }
        public string SCity { get; set; }
        public string SZipCode { get; set; }
        public string SPhone { get; set; }
        public string SFax { get; set; }
        public string FirstName { get; set; }
        public long OrderDetailID { get; set; }
        public string ResponseValue { get; set; }
        public string TVR { get; set; }
        public string CardType { get; set; }
        public string EmvAid { get; set; }
        public string TSI { get; set; }
        public Nullable<decimal> FSEligibleAmount { get; set; }
    }
}
