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
    
    public partial class tbl_CouponMaster
    {
        public long CouponID { get; set; }
        public string CoupenCode { get; set; }
        public string CouponName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> MinPurchaseAmt { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<long> AvailableCount { get; set; }
        public Nullable<long> UsedCount { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsAllowMultipleTime { get; set; }
        public Nullable<bool> IsRestricted { get; set; }
        public Nullable<bool> AllowAllDepartment { get; set; }
        public Nullable<bool> SelectedDepartment { get; set; }
    }
}