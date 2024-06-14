using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class CouponMasterModel
    {
        public long CouponID { get; set; }
        public string CoupenCode { get; set; }
        public string CouponName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> MinPurchaseAmt { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<long> AvailableCount { get; set; }
        public Nullable<long> UsedCount { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsAllowMultipleTime { get; set; }
        public Nullable<bool> IsRestricted { get; set; }
        public Nullable<bool> AllowAllDepartment { get; set; }
        public Nullable<bool> SelectedDepartment { get; set; }

        public long CouponAppliedDepID { get; set; }
        public List<int> DepartmentID { get; set; }
    }

    public class CouponMasterModelCont
    {
        public const string CouponID = "CouponID";
        public const string CoupenCode = "CoupenCode";
        public const string CouponName = "CouponName";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string MinPurchaseAmt = "MinPurchaseAmt";
        public const string Discount = "Discount";
        public const string AvailableCount = "AvailableCount";
        public const string UsedCount = "UsedCount";
        public const string IsActive = "IsActive";
        public const string IsDelete = "IsDelete";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string UpdatedBy = "UpdatedBy";
        public const string UpdatedDate = "UpdatedDate";
        public const string IsAllowMultipleTime = "IsAllowMultipleTime";
        public const string IsRestricted = "IsRestricted";
        public const string DepartmentID = "DepartmentID";
        public const string AllowAllDepartment = "AllowAllDepartment";
        public const string SelectedDepartment = "SelectedDepartment";
    }

}
