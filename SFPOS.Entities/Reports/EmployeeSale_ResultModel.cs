using System;

namespace SFPOS.Entities.Reports
{
    public class EmployeeSale_ResultModel
    {
        public long EmployeeID { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string RoleType { get; set; }
        public Nullable<decimal> CancelAmount { get; set; }

    }
    public class EmployeeSale_ResultModelCont
    {
        public const string EmployeeID = "EmployeeID";
        public const string UserID = "UserID";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string PhoneNo = "PhoneNo";
        public const string CreatedDate = "CreatedDate";
        public const string IsActive = "IsActive";
        public const string RoleType = "RoleType";
    }
}
