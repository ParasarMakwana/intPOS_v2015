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
    
    public partial class SP_Rpt_TaxReport_Result
    {
        public string DepartmentName { get; set; }
        public Nullable<decimal> TOTALSALES { get; set; }
        public Nullable<decimal> TAXEXEMPTSALES { get; set; }
        public Nullable<decimal> FOODSTAMPEXEMPTEDSALES { get; set; }
        public Nullable<decimal> TAXABLESALES { get; set; }
        public Nullable<decimal> SALESTAXCOLLECTED { get; set; }
        public Nullable<decimal> SUBTOTAL { get; set; }
    }
}