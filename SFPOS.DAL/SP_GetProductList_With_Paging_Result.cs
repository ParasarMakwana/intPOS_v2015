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
    
    public partial class SP_GetProductList_With_Paging_Result
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string UPCCode { get; set; }
        public string CertCode { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public Nullable<long> SectionID { get; set; }
        public Nullable<long> UnitMeasureID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<long> TaxGroupID { get; set; }
        public string Image { get; set; }
        public Nullable<bool> LabeledPrice { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<long> ProductVendorID { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public Nullable<bool> AgeVerification { get; set; }
        public Nullable<bool> IsScaled { get; set; }
        public Nullable<decimal> TareWeight { get; set; }
        public Nullable<decimal> GroupQty { get; set; }
        public Nullable<decimal> GroupPrice { get; set; }
        public string LinkedUPCCode { get; set; }
        public Nullable<decimal> CaseQty { get; set; }
        public Nullable<decimal> CasePrice { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string UnitMeasureCode { get; set; }
        public string TaxGroupName { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<bool> IsGroupPrice { get; set; }
        public Nullable<int> Pack { get; set; }
        public string Size { get; set; }
        public string SecondaryPLU { get; set; }
        public string PalletQTY { get; set; }
        public string CountryofOrigin { get; set; }
        public string VendorName { get; set; }
    }
}
