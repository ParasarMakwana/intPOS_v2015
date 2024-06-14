using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class OrderMasterModel
    {
        public long OrderID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> PaymentMethodID { get; set; }
        public Nullable<long> StoreID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> GrossAmount { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> CreditCardAmount { get; set; }
        public Nullable<decimal> CheckAmount { get; set; }
        public Nullable<decimal> FoodStampAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> RefundAmount { get; set; }
        public string CardNumber { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public string CounterIP { get; set; }
        public string OrdNo { get; set; }
        public Nullable<decimal> ChangeAmount { get; set; }
        public Nullable<bool> IsCancel { get; set; }
        public Nullable<decimal> TaxableAmount { get; set; }
        public Nullable<long> EmployeeID { get; set; }

        public Nullable<decimal> CancelAmount { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Nullable<decimal> TaxExempted { get; set; }

        public string CouponCode { get; set; }
        public Nullable<decimal> CouponDiscAmt { get; set; }
        public Nullable<bool> IsTaxCarry { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public Nullable<decimal> OverridePrice { get; set; }

        public Nullable<System.DateTime> SyncDate { get; set; }

    }
}
