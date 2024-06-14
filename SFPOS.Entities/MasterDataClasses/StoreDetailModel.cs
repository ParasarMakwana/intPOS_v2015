using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
   public class StoreDetailModel
    {
        public int StoreID { get; set; }

        public string StoreName { get; set; }

        public string PersonName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }
    }
}
