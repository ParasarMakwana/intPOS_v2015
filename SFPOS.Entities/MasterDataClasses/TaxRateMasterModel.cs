using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class TaxRateMasterModel
    {
        public long TaxRateID { get; set; }
        public Nullable<long> TaxGroupID { get; set; }
        public decimal Tax { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public string TaxGroupName { get; set; }
    }
    public class TaxRateMasterModelCont
    {
        public const string Tax = "Tax";
        public const string TaxRateID = "TaxRateID";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string TaxGroupName = "TaxGroupName";
        public const string IsActive = "IsActive";
    }
}
