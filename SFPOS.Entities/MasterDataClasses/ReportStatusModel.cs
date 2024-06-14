using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ReportStatusModel
    {
        public decimal GrossAmount { get; set; }
        public decimal FoodStampAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CheckAmount { get; set; }
        public decimal CreditCardAmount { get; set; }
        public decimal ReportedBalance { get; set; }
    }
    
    public class ReportReq
    {
        public decimal EmployeeID { get; set; }
        public DateTime Date { get; set; }
    }

    public class ReportsReq
    {
        public DateTime Date { get; set; }
    }

    public class RegisterReportDataModel
    {
        public decimal CashTotalAmount { get; set; }
        public decimal CheckTotalAmount { get; set; }
        public decimal CreditCardTotalAmount { get; set; }
        public decimal CashPayoutTotalAmount { get; set; }
    }
}
