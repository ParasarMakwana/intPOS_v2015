using System;

namespace SFPOS.Entities.Reports
{
    public class DepartmentWiseSale_ResultModel
    {
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<decimal> TotalSales { get; set; }
    }
}
