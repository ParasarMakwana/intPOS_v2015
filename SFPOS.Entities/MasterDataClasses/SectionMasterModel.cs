
using System;

namespace SFPOS.Entities
{
    public class SectionMasterModel
    {
        public long SectionID { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public string SectionName { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public Nullable<long> TaxGroupID { get; set; }
        public Nullable<long> UnitMeasureID { get; set; }
        public string Ramark { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<int> AgeVarificationAge { get; set; }

        public string DepartmentName { get; set; }
        public string TaxGroupName { get; set; }
        public string UnitMeasureCode { get; set; }
    }
    public class SectionMasterModelCont
    {
        public const string SectionID = "SectionID";
        public const string DepartmentID = "DepartmentID";
        public const string SectionName = "SectionName";
        public const string Name = "Name";
        public const string DepartmentName = "DepartmentName";
        public const string Ramark = "Ramark";
        public const string IsActive = "IsActive";
        public const string IsDelete = "IsDelete";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string UpdatedBy = "UpdatedBy";
        public const string UpdatedDate = "UpdatedDate";
        public const string cmbSectionFirst = "---Section---";
        public const string AgeVarificationAge = "AgeVarificationAge";
        public const string TaxGroupID = "TaxGroupID";
        public const string TaxGroupName = "TaxGroupName";
        public const string IsFoodStamp = "IsFoodStamp";
        public const string UnitMeasureID = "UnitMeasureID";
        public const string UnitMeasureCode = "UnitMeasureCode";

    }
}
