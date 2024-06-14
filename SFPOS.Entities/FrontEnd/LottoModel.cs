using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class LottoModel
    {
        public long? LottoID { get; set; }
        public int? LottoType { get; set; }

        public decimal? LottoPrice { get; set; }

        public string CounterIP { get; set; }

        public string MacAddress { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public long? StoreID { get; set; }

    }
}
