using System;

namespace SFPOS.Entities.Reports
{
    public class LastDaysSalesReport_ResultModel
    {
        public string Day { get; set; }
        public Nullable<decimal> TotalSales { get; set; }
    }
}
