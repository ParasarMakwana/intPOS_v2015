using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class TillReportModel
    {
        public long TillStatusReportID { get; set; }
        public DateTime SelectedDate { get; set; }
        public long CashierID { get; set; }
        public string CashierName { get; set; }
        public decimal Coin { get; set; }
        public decimal Cash { get; set; }
        public decimal CreditCard { get; set; }
        public decimal Checks { get; set; }
        public decimal CashPayout { get; set; }
        public decimal TakeOut { get; set; }
        public decimal BackInDrawer { get; set; }
        public decimal SortBy { get; set; }
        public decimal OverBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public decimal TotalSortOver { get; set; }
        public string Verifier { get; set; }
        public decimal Lotto { get; set; }
        public decimal SelfService { get; set; }
        public decimal Scrathers { get; set; }
    }
}
