using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.spDataClasses
{
    public class SP_GetSectionList_Result_Model
    {
        public long SectionID { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public string SectionName { get; set; }
        public string Ramark { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DepartmentName { get; set; }

        public int AgeVarificationAge { get; set; }
        public Nullable<long> TaxGroupID { get; set; }
        public Nullable<long> UnitMeasureID { get; set; }
        public Nullable<bool> IsFoodStamp { get; set; }
        public string TaxGroupName { get; set; }
        public string UnitMeasureCode { get; set; }
        
    }
}
