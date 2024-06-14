using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class LottoTotalTrans_ResultModel
    {
        public string EmpName { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
        public Nullable<decimal> PayoutAmount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
