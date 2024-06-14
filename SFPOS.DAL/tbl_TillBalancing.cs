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
    using System.Collections.Generic;
    
    public partial class tbl_TillBalancing
    {
        public long TillBalancingID { get; set; }
        public Nullable<System.DateTime> SelectedDate { get; set; }
        public Nullable<long> CashierID { get; set; }
        public string CashierName { get; set; }
        public Nullable<decimal> A_Cash { get; set; }
        public Nullable<decimal> A_Check { get; set; }
        public Nullable<decimal> A_CreaditCard { get; set; }
        public Nullable<decimal> A_FoodStamp { get; set; }
        public Nullable<decimal> B_Cash { get; set; }
        public Nullable<decimal> B_Check { get; set; }
        public Nullable<decimal> B_CreaditCard { get; set; }
        public Nullable<decimal> B_FoodStamp { get; set; }
        public Nullable<decimal> S_Cash { get; set; }
        public Nullable<decimal> S_Check { get; set; }
        public Nullable<decimal> S_CreaditCard { get; set; }
        public Nullable<decimal> S_FoodStamp { get; set; }
        public Nullable<decimal> A_Total { get; set; }
        public Nullable<decimal> B_Total { get; set; }
        public Nullable<decimal> S_Total { get; set; }
        public Nullable<decimal> GrossAmount { get; set; }
        public Nullable<decimal> AveOrder { get; set; }
        public Nullable<decimal> RefundAmount { get; set; }
        public Nullable<decimal> CancelledAmount { get; set; }
        public Nullable<decimal> TotalOrder { get; set; }
        public Nullable<decimal> RefundOrder { get; set; }
        public Nullable<decimal> CancelledOrder { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<decimal> CheckRefund { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
    }
}