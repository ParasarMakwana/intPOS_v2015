using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ReportTotalSales_ResultModel
    {
        public Nullable<decimal> Totalsales_TODAY { get; set; }
        public Nullable<decimal> Totalsales_MONTH { get; set; }
        public Nullable<decimal> Totalsales_YEAR { get; set; }
    }
}
