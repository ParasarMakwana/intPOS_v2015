using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class EmployeeMasterModel
    {
        public long EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public long RoleID { get; set; }
        public long StoreID { get; set; }
        public string StoreName { get; set; }
        public string RoleType { get; set; }
        public Nullable<decimal> MaxVoidAmount { get; set; }

        public Nullable<System.DateTime> BirthDate { get; set; }
        public bool IsCashPayout { get; set; }
        public bool IsLottoFunction { get; set; }
    }

    public class EmployeeMasterModelCont
    {
        public const string EmployeeID = "EmployeeID";
        public const string StoreID = "StoreID";
        public const string RoleID = "RoleID";
        public const string FirstName = "FirstName";
        public const string First_Name = "First_Name";
        public const string LastName = "LastName";
        public const string Last_Name = "Last_Name";
        public const string EmailID = "EmailID";
        public const string Email = "Email";
        public const string PhoneNo = "PhoneNo";
        public const string Phone = "Phone";
        public const string Password = "Password";
        public const string IsActive = "IsActive";
        public const string IsDelete = "IsDelete";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string UpdatedDate = "UpdatedDate";
        public const string UpdatedBy = "UpdatedBy";
        public const string MaxVoidAmount = "MaxVoidAmount";
        public const string BirthDate = "BirthDate";
        public const string IsCashPayout = "IsCashPayout";
        public const string IsLottoFunction = "IsLottoFunction";
    }
}
