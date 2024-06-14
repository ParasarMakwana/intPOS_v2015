using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ReciptDetails_ResultModel
    {
        public Nullable<bool> IsCancel { get; set; }
        public long OrderID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
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
        public bool ManWT { get; set; }
        public bool IsFoodStamp { get; set; }
        public bool IsTax { get; set; }
        public string StoreName { get; set; }
        public string SMAddress { get; set; }
        public string SAddress2 { get; set; }
        public string SCity { get; set; }
        public string SZipCode { get; set; }
        public string SPhone { get; set; }
        public string SFax { get; set; }
        public string FirstName { get; set; }
        public bool DiscountApplyed { get; set; }

        public Nullable<decimal> TaxExempted { get; set; }
        public Nullable<bool> CasePriceApplied { get; set; }

        public decimal GroupQty { get; set; }
        public decimal GroupPrice { get; set; }
        public decimal CaseQty { get; set; }
        public decimal CasePrice { get; set; }
        public string CouponCode { get; set; }
        public Nullable<decimal> CouponDiscAmt { get; set; }
        public long OrderDetailID { get; set; }
        public string ResponseValue { get; set; }
        public string TVR { get; set; }
        public string CardType { get; set; }
        public string EmvAid { get; set; }
        public string TSI { get; set; }
        public decimal FSEligibleAmount { get; set; }
        
    }
    public class ReciptDetails_ResultModelCont
    {
        public const string TaxableAmount = "TaxableAmount";
        public const string IsCancel = "IsCancel"; 
        public const string Qty = "Qty";
        public const string OrderID = "OrderID";
        public const string TotalAmount = "TotalAmount";
        public const string DiscountAmount = "DiscountAmount";
        public const string TaxAmount = "TaxAmount";
        public const string GrossAmount = "GrossAmount";
        public const string UPCCode = "UPCCode";
        public const string ProductName = "ProductName";
        public const string Quantity = "Quantity";
        public const string SellPrice = "SellPrice";
        public const string Discount = "Discount";
        public const string finalPrice = "finalPrice";
        public const string StoreName = "StoreName";
        public const string SMAddress = "SMAddress";
        public const string SAddress2 = "SAddress2";
        public const string SCity = "SCity";
        public const string SZipCode = "SZipCode";
        public const string SPhone = "SPhone";
        public const string SFax = "SFax";
        public const string FirstName = "FirstName";
        public const string OrdNo = "OrdNo";
        public const string CashAmount = "CashAmount";
        public const string CheckAmount = "CheckAmount";
        public const string CreditCardAmount = "CreditCardAmount";
        public const string FoodStampAmount = "FoodStampAmount";
        public const string RefundAmount = "RefundAmount";
        public const string Balance = "Balance";
        public const string ChangeAmount = "ChangeAmount";
        public const string TaxExempted = "TaxExempted";
        public const string CasePriceApplied = "CasePriceApplied";
        public const string GroupQty = "GroupQty";
        public const string GroupPrice = "GroupPrice";
        public const string CaseQty = "CaseQty";
        public const string CasePrice = "CasePrice";
        public const string CouponCode = "CouponCode";
        public const string CouponDiscAmt = "CouponDiscAmt";
        public const string CreatedDate = "CreatedDate";
        public const string OrderDetailID = "OrderDetailID";
        public const string ResponseValue = "ResponseValue";
        public const string TVR = "TVR";
        public const string CardType = "CardType";
        public const string EmvAid = "EmvAid";
        public const string TSI = "TSI";
        public const string IsManWTRefund = "IsManWTRefund";
        public const string FSEligibleAmount = "FSEligibleAmount";
        public const string ManWT = "ManWT";
    }
}
