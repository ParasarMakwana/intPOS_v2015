using System;

namespace SFPOS.Entities.Reports
{
    public class SectionWiseSaleHistoryModel
    {
        public long SectionID { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public string SectionName { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
    }
}
