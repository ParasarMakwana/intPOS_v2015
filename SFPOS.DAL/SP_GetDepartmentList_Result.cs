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
    
    public partial class SP_GetDepartmentList_Result
    {
        public long DepartmentID { get; set; }
        public Nullable<long> DepartmentNo { get; set; }
        public Nullable<long> SubNo { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<int> AgeVarificationAge { get; set; }
        public long UnitMeasureID { get; set; }
        public long TaxGroupID { get; set; }
        public bool IsFoodStamp { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string Remark { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> DepartmentBtn { get; set; }
        public string BtnCode { get; set; }
        public long DepartmentBtnIndex { get; set; }
        public bool IsForceTax { get; set; }
        public string ForcedTaxSuffix { get; set; }
        public Nullable<long> ForcedTaxSection { get; set; }
    }
}