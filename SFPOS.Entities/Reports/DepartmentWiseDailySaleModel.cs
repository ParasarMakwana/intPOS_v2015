using System;

namespace SFPOS.Entities.Reports
{
    public class DepartmentWiseDailySaleModel
    {
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public Nullable<decimal> CancelAmount { get; set; }

    }
}
