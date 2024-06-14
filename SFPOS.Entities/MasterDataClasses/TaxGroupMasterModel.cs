using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class TaxGroupMasterModel
    {
        public long TaxGroupID { get; set; }
        public string TaxGroupName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
    }
    public class TaxGroupMasterModelCont
    {
        public const string TaxGroupID = "TaxGroupID";
        public const string Tax_Group_Code = "Tax_Group_Code";
        public const string TaxGroupName = "TaxGroupName";
        public const string cmbTaxGroupCodeFirst = "--- TaxGroupCode ---";
    }
}
