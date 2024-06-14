using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class LoginMasterModel
    {
        public long LoginID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<long> StoreID { get; set; }
        public string CounterIP { get; set; }
        public Nullable<System.DateTime> LoginDate { get; set; }
        public Nullable<System.DateTime> LogoutTime { get; set; }
    }
}
