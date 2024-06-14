using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class StoreMasterModel
    {
        public long StoreID { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public Nullable<long> City { get; set; }
        public Nullable<long> State { get; set; }
        public Nullable<long> Country { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public Nullable<int> AgeVarificationAge { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        public Nullable<decimal> DefaultTax { get; set; }
        public string Disclaimer { get; set; }

        public bool IsStoreTax { get; set; }

    }
    public class StoreMasterModelCont
    {
        public const string StoreID = "StoreID";
        public const string Address2 = "Address2";
        public const string StoreName = "StoreName";
        public const string Address = "Address";
        public const string Phone = "Phone";
        public const string Fax = "Fax";
        public const string ZipCode = "ZipCode";
        public const string CreatedDate = "ZipCode";
        public const string AgeVarificationAge = "AgeVarificationAge";
        public const string DefaultTax = "DefaultTax";
        public const string Disclaimer = "Disclaimer";
        public const string CreatedBy = "CreatedBy";
        public const string IsActive = "IsActive";
        public const string IsDelete = "IsDelete";
        public const string UpdatedBy = "UpdatedBy";
        public const string UpdatedDate = "UpdatedDate";
        public const string CountryName = "CountryName";
        public const string StateName = "StateName";
        public const string CityName = "CityName";
        public const string IsStoreTax = "IsStoreTax"; 
        public const string cmbStoreFirst = "--- Select Store ---";
    }
}
