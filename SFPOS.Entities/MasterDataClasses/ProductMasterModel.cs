using System;
using System.Collections.Generic;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ProductMasterModel
    {
        public Nullable<long> RowNumber { get; set; }
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
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public Nullable<bool> AgeVerification { get; set; }
        public Nullable<bool> IsScaled { get; set; }
        public Nullable<bool> IsGroupPrice { get; set; }
        public Nullable<decimal> TareWeight { get; set; }
        public Nullable<decimal> GroupQty { get; set; }
        public Nullable<decimal> GroupPrice { get; set; }
        public string LinkedUPCCode { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string UnitMeasureCode { get; set; }
        public string TaxGroupName { get; set; }

        public Nullable<bool> LabeledPrice { get; set; }

        public Nullable<decimal> CaseQty { get; set; }
        public Nullable<decimal> CasePrice { get; set; }

        public Nullable<decimal> UnitCost { get; set; }

        public Nullable<long> ProductVendorID { get; set; }

        public Nullable<int> Pack { get; set; }
        public string Size { get; set; }
        public string SecondaryPLU { get; set; }
        public string PalletQTY { get; set; }
        public string CountryofOrigin { get; set; }
        public string VendorName { get; set; }
        public Nullable<decimal> FSEligibleAmount { get; set; }
    }
    public class ProductMasterModelCont
    {
        public const string ProductID = "ProductID";
        public const string ProductName = "ProductName";
        public const string PrintProductName = "PrintProductName";
        //public const string Name = "Name";
        public const string UPCCode = "UPCCode";
        public const string UPC_Code = "UPC_Code";
        public const string CertCode = "CertCode";
        public const string Image = "Image";
        public const string Price = "Price";
        public const string IsActive = "IsActive";
        public const string IsFoodStamp = "IsFoodStamp";
        public const string AgeVerification = "AgeVerification";
        public const string IsScaled = "IsScaled";
        public const string TareWeight = "TareWeight";
        public const string GroupQty = "GroupQty";
        public const string GroupPrice = "GroupPrice";
        public const string LinkedUPCCode = "LinkedUPCCode";
        public const string DepartmentName = "DepartmentName";
        public const string SectionName = "SectionName";
        public const string UnitMeasureCode = "UnitMeasureCode";
        public const string TaxGroupName = "TaxGroupName";
        public const string LabeledPrice = "LabeledPrice";

        public const string CaseQty = "CaseQty";
        public const string CasePrice = "CasePrice";
        public const string UnitCost = "UnitCost";
        public const string ProductVendorID = "ProductVendorID";
        public const string IsGroupPrice = "IsGroupPrice";

        public const string Pack = "Pack";
        public const string Size = "Size";

        public const string SecondaryPLU = "SecondaryPLU";
        public const string PalletQTY = "PalletQTY";
        public const string UpdatedDate = "UpdatedDate";
        public const string CountryofOrigin = "CountryofOrigin";
        public const string VendorName = "VendorName";
        public const string FSEligibleAmount = "FSEligibleAmount";
    }
    public class productMasterModelCount {
        public string ColumnName { get; set; }
        public string valueName { get; set; }
        public string HeaderName { get; set; }
    }
}
