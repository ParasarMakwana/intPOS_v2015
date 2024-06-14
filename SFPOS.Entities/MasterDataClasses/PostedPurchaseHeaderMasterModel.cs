using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class PostedPurchaseHeaderMasterModel
    {
        public long PostedPurchaseHeaderID { get; set; }
        public Nullable<long> PurchaseHeaderID { get; set; }
        public Nullable<long> VendorID { get; set; }
        public string PONumber { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }


    }
}
