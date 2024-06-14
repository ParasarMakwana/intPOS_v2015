using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class TillStatusModel
    {
        public decimal? finalPrice { get; set; }
        public decimal? salesLotto { get; set; }
        public decimal? payoutLotto { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
