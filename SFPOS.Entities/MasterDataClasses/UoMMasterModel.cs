using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class UoMMasterModel
    {
        public long UnitMeasureID { get; set; }
        public string UnitMeasureCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
    public class UoMMasterModelCont
    {
        public const string UnitMeasureID = "UnitMeasureID";
        public const string UnitMeasureCode = "UnitMeasureCode";

        public const string Unit_Of_Measure = "Unit_Of_Measure";
        
        public const string Description = "Description";
        public const string cmbUoMFirst = "--- UoM ---";
        
    }
}
