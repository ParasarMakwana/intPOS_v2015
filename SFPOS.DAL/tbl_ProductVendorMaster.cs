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
    
    public partial class tbl_ProductVendorMaster
    {
        public long ProductVendorID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<int> Pack { get; set; }
        public string ItemCode { get; set; }
        public Nullable<long> VendorID { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    }
}