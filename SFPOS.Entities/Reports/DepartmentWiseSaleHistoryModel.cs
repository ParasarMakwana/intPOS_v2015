using System;

namespace SFPOS.Entities.Reports
{
    public class DepartmentWiseSaleHistoryModel
    {
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal SalesAmount { get; set; }
    }
}
