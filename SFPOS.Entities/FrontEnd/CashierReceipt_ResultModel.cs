using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.FrontEnd
{
    public class CashierReceipt_ResultModel
    {
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> CheckAmount { get; set; }
        public Nullable<decimal> CreditCardAmount { get; set; }
        public Nullable<decimal> FoodStampAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public Nullable<int> RefundCount { get; set; }
        public Nullable<decimal> CancelledAmount { get; set; }
        public Nullable<int> CancelledCount { get; set; }
        public int OverridePriceCount { get; set; }
        public decimal OverridePriceTotal { get; set; }
        public Nullable<decimal> LottoSalesAmount { get; set; }
        public Nullable<decimal> LottoPayoutAmount { get; set; }
    }
}
