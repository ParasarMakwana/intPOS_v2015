using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class RoleMasterModel
    {
        public long RoleID { get; set; }
        public string RoleType { get; set; }
        public decimal OverrideAmount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
    }
    public class RoleMasterModelCont
    {
        public const string RoleID = "RoleID";
        public const string IsDelete = "IsDelete";
        public const string IsActive = "IsActive";
        public const string UpdatedDate = "UpdatedDate";
        public const string UpdatedBy = "UpdatedBy";
        public const string CreatedBy = "CreatedBy";
        public const string RoleType = "RoleType";
        public const string OverrideAmount = "OverrideAmount";
        public const string cmbRoleFirst = "--- Select Role ---";
    }
}
