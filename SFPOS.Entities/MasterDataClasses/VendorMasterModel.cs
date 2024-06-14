using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class VendorMasterModel
    {
        public long VendorID { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public Nullable<long> State { get; set; }
        public Nullable<long> Country { get; set; }
        public string PhoneNo { get; set; }
        public string Fax { get; set; }
        public string ZipCode { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string EmailID { get; set; }
        public string ContactPerson { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
    }
    public class VendorMasterModelCont
    {
        public const string VendorID = "VendorID";
        public const string Address2 = "Address2";
        public const string State = "State";
        public const string Country = "Country";
        public const string VendorName = "VendorName";
        public const string Name = "Name";
        public const string Vendor_Name = "Vendor_Name";

        public const string Address = "Address";
        public const string EmailID = "EmailID";
        public const string PhoneNo = "PhoneNo";
        public const string Email = "Email";
        public const string Phone = "Phone";
        public const string Fax = "Fax";
        public const string ContactPerson = "ContactPerson";
        public const string Contact_Person = "Contact_Person";
        public const string ZipCode = "ZipCode";
        public const string CountryName = "CountryName";
        public const string StateName = "StateName";
        public const string CityName = "CityName";
        public const string cmbVendorFirst = "--- Select Vendor ---";
    }
}
