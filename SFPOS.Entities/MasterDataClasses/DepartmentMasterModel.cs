using System;

namespace SFPOS.Entities
{
    public class DepartmentMasterModel
    {
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public Nullable<long> TaxGroupID { get; set; }
        public Nullable<long> UnitMeasureID { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<int> AgeVarificationAge { get; set; }

        public Nullable<long> DepartmentNo { get; set; }
        public Nullable<long> SubNo { get; set; }

        public Nullable<bool> DepartmentBtn { get; set; }
        public string BtnCode { get; set; }
        public long DepartmentBtnIndex { get; set; }
        public bool IsForceTax { get; set; }
        public string ForcedTaxSuffix { get; set; }
        public Nullable<long> ForcedTaxSection { get; set; }
    }

    public class DepartmentMasterModelCont
    {
        public const string DepartmentID = "DepartmentID";
        public const string DepartmentName = "DepartmentName";
        public const string SectionName = "SectionName";
        public const string DepartmentNo = "DepartmentNo";
        public const string SubNo = "SubNo";
        public const string Name = "Name";
        public const string AgeVarificationAge = "AgeVarificationAge";
        public const string BtnCode = "BtnCode";
        public const string DepartmentBtn = "DepartmentBtn";
        public const string DepartmentBtnIndex = "DepartmentBtnIndex";
        public const string IsForceTax = "IsForceTax";
        public const string ForcedTaxSuffix = "ForcedTaxSuffix";
        public const string ForcedTaxSection = "ForcedTaxSection";

        public const string Ramark = "Ramark";
        public const string UnitMeasureID = "UnitMeasureID";
        public const string TaxGroupID = "TaxGroupID";
        public const string IsActive = "IsActive";
        public const string IsFoodStamp = "IsFoodStamp";
        public const string cmbDepartmentFirst = "---Department---";
    }
}