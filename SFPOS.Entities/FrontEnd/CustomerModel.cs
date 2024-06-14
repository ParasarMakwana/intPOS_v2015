using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class CustomerModel
    {
        public long CustomerID { get; set; }
        public Nullable<long> StoreID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ZipCode { get; set; }
        public string Message { get; set; }
        public Nullable<bool> NewsLetter { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
