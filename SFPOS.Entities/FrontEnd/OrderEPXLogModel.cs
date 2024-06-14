using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class OrderEPXLogModel
    {
        public int? EpxId { get; set; }
        public long UserId { get; set; }
        public long OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public long StoreID { get; set; }
        public decimal Amount { get; set; }
        public string Response { get; set; }
        public string TransactionNo { get; set; }
        public string RequestSend { get; set; }

    }
}
