using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class CustomerMasterModel
    {
        
        public long CustomerID { get; set; }
        public long StoreID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ZipCode { get; set; }
        public string Message { get; set; }
        public bool NewsLetter { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string ResellerID { get; set; }
        public bool TaxExempted { get; set; }
        public string BusinessName { get; set; }
        public string City { get; set; }
    }

    public class CustomerMasterModelCont
    {
        public const string CustomerID = "CustomerID";
        public const string StoreID = "StoreID";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string Email = "Email";
        public const string MobileNo = "MobileNo";
        public const string ZipCode = "ZipCode";
        public const string Message = "Message";
        public const string NewsLetter = "NewsLetter";
        public const string CreatedDate = "CreatedDate";
        public const string IsActive = "IsActive";
        public const string IsDelete = "IsDelete";
        public const string ResellerID = "ResellerID";
        public const string TaxExempted = "TaxExempted";
        public const string BusinessName = "BusinessName";
        public const string City = "City";
    }
}
