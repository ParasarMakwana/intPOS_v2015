using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class CounterMasterModel
    {
        public long CounterID { get; set; }
        public Nullable<long> StoreID { get; set; }
        public Nullable<int> CounterNo { get; set; }
        public string CounterIP { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public Nullable<System.DateTime> LogoutTime { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string MacAddress { get; set; }
        public string CurrentLoginUser { get; set; }
        public string CurrentVersion { get; set; }
        public bool IsLTSystemActive { get; set; }
    }
}
