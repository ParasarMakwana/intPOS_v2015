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

    public class OrderScanner_ResultModel
    {
        public string UPCCode { get; set; }
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string UnitMeasureCode { get; set; }
        public Nullable<decimal> SellPrice { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> TareWeight { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> FinalPrice { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public Nullable<long> SectionID { get; set; }
        public Nullable<long> TaxGroupID { get; set; }
        public Nullable<long> UnitMeasureID { get; set; }
        public Nullable<bool> IsScale { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public Nullable<bool> IsTax { get; set; }
        public Nullable<bool> AgeVerification { get; set; }
        public Nullable<bool> IsLinkedCase { get; set; }
        public string LinkedUPCCode { get; set; }

        public decimal Qty { get; set; }
        public decimal SpecialQty { get; set; }
        public decimal GroupQty { get; set; }
        public decimal GroupPrice { get; set; }
        public bool DiscountApplyed { get; set; }
        public decimal CaseQty { get; set; }
        public decimal CasePrice { get; set; }
        public bool CasePriceApplied { get; set; }
        public bool LabeledPrice { get; set; }
        public bool IsRefund { get; set; }
        public bool IsVoid { get; set; }
        public Nullable<bool> IsForceTax { get; set; }
        public string Abb { get; set; }
        public int RowNo { get; set; }
        public decimal FoodStampTotal { get; set; }
        public int IsVerifed { get; set; }
        public string ParentUPCCode { get; set; }
        public bool IsGroupPrice { get; set; }
        public bool VoidApplyed { get; set; }
        public bool IsTaxCarry { get; set; }
        public bool IsReturn { get; set; }
        //Komel - 20201023
        public decimal? OverridePrice { get; set; }
        public bool IsOverridePrice { get; set; }
        public bool IsForceTaxDept { get; set; }
        public string ForcedTaxSuffix { get; set; }
        public Nullable<long> ForcedTaxSection { get; set; }
        public Nullable<bool> IsManWTRefund { get; set; }
        public Nullable<decimal> FSEligibleAmount { get; set; }
    }

    public class OrderScanner_ResultModelCont
    {
        public const string ProductName = "ProductName";
        public const string Qty = "Qty";
        public const string SellPrice = "SellPrice";
        public const string SalePrice = "SalePrice";
        public const string Tax = "Tax";
        public const string Discount = "Discount";
        public const string DiscountAmount = "DiscountAmount";
        public const string TaxAmount = "TaxAmount";
        public const string FinalPrice = "FinalPrice";
        public const string IsFoodStamp = "IsFoodStamp";
        public const string IsScale = "IsScale";
        public const string PaymentMethodID = "PaymentMethodID";
        public const string IsTax = "IsTax";
        public const string DiscountApplyed = "DiscountApplyed";
        public const string IsRefund = "IsRefund";
        public const string IsForceTax = "IsForceTax";
        public const string IsVoid = "IsVoid";
        public const string Abb = "Abb";
        public const string Image = "Image";
        public const string DepartmentID = "DepartmentID";
        public const string SectionID = "SectionID";
        public const string CasePriceApplied = "CasePriceApplied";
        public const string RowNumber = "RowNumber";
        public const string FoodStampTotal = "FoodStampTotal";
        public const string GroupQty = "GroupQty";
        public const string GroupPrice = "GroupPrice";
        public const string CaseQty = "CaseQty";
        public const string CasePrice = "CasePrice";
        public const string ParentUPCCode = "ParentUPCCode";
        public const string IsGroupPrice = "IsGroupPrice";
        public const string LinkedUPCCode = "LinkedUPCCode";
        public const string VoidApplyed = "VoidApplyed";
        public const string SpecialQty = "SpecialQty";
        public const string IsTaxCarry = "IsTaxCarry";
        public const string IsReturn = "IsReturn";
        //Komel - 20201023
        public const string OverridePrice = "OverridePrice";
        public const string IsOverridePrice = "IsOverridePrice";
        //Vishal - 20201214
        public const string IsForceTaxDept = "IsForceTaxDept";
        public const string ForcedTaxSuffix = "ForcedTaxSuffix";
        public const string IsForceTaxAppliesDept = "IsForceTaxAppliesDept";
        public const string ForcedTaxSection = "ForcedTaxSection";
        public const string IsManWTRefund = "IsManWTRefund";
        public const string FSEligibleAmount = "FSEligibleAmount";
    }
}
