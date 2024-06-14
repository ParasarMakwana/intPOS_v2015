using System;

namespace SFPOS.Entities.FrontEnd
{
    public class PaymentTransMasterModel
    {
        public long PaymentTransID { get; set; }
        public Nullable<long> OrderID { get; set; }
        public Nullable<long> PaymentMethodID { get; set; }
        public Nullable<long> StoreID { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> CreditCardAmount { get; set; }
        public Nullable<decimal> CheckAmount { get; set; }
        public Nullable<decimal> FoodStampAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> ChangeAmount { get; set; }
        public string CardNumber { get; set; }
        public string CounterIP { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
    }
    public class PaymentTransMasterModelCont
    {
        public const string PaymentTransID = "PaymentTransID";
        public const string OrderID = "OrderID";
        public const string PaymentMethodID = "PaymentMethodID";
        public const string CashAmount = "CashAmount";
        public const string CreditCardAmount = "CreditCardAmount";
        public const string FoodStampAmount = "FoodStampAmount";
        public const string CheckAmount = "CheckAmount";
        public const string CardNumber = "CardNumber";
        public const string CounterIP = "CounterIP";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string StoreID = "StoreID";
        public const string Balance = "Balance";
        public const string ChangeAmount = "ChangeAmount";
    }
}
