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
    
    public partial class SP_InventoryReport_Result
    {
        public Nullable<long> ProductID { get; set; }
        public string ProductName { get; set; }
        public string UPCCode { get; set; }
        public string ItemCode { get; set; }
        public Nullable<decimal> PURCHASE_QTY { get; set; }
        public Nullable<decimal> SALE_QTY { get; set; }
        public Nullable<decimal> TOTAL_QTY { get; set; }
    }
}
