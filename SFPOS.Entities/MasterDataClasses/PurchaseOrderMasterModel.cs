using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class PurchaseOrderMasterModel
    {
        public long PurchaseHeaderID { get; set; }
        public Nullable<long> VendorID { get; set; }
        public string PONumber { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<bool> isReceived { get; set; }
        public string VendorName { get; set; }
    }
    public class PurchaseOrderMasterModelCont
    {
        public const string PurchaseHeaderID = "PurchaseHeaderID";
        public const string PONumber = "PONumber";
        public const string OrderDate = "OrderDate";
        public const string isReceived = "isReceived";
        
    }
}
