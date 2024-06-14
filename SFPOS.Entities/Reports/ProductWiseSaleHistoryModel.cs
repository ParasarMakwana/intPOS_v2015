using System;

namespace SFPOS.Entities.Reports
{
    public class ProductWiseSaleHistoryModel
    {
        public Nullable<long> ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }

    }
}
