using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ProductVendorMasterModel
    {
        public long ProductVendorID { get; set; }
        public string ProductName { get; set; }
        public string ItemCode { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string VendorName { get; set; }
        public Nullable<long> VendorID { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<int> Pack { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }

        public bool IsDefault { get; set; }
    }
    public class ProductVendorMasterModelCont
    {
        public const string ProductVendorID = "ProductVendorID";
        public const string ItemCode = "ItemCode";
        public const string UnitCost = "UnitCost";
        public const string VendorID = "VendorID";
        public const string Pack = "Pack";
        public const string UpdatedBy = "UpdatedBy";
        public const string UpdatedDate = "UpdatedDate";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string IsDelete = "IsDelete";
        public const string IsActive = "IsActive";
        public const string IsDefault = "IsDefault";
        public const string ProductID = "ProductID";
    }


}
