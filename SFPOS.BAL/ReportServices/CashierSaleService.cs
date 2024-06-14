using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class CashierSaleService
    {
        CashierSaleReportDAL objEmployeeSaleReportDAL = new CashierSaleReportDAL();
        //public List<EmployeeSale_ResultModel> GetAllEmployeeSales()
        //{
        //    return objEmployeeSaleReportDAL.GetAllEmployeeSales();
        //}
        //public List<EmployeeWiseDailySaleModel> EmployeeWiseDailySale()
        //{
        //    return objEmployeeSaleReportDAL.EmployeeWiseDailySale();
        //}

        //public List<EmployeeInvoiceWiseDailySaleModel> EmployeeInvoiceWiseDailySale()
        //{
        //    return objEmployeeSaleReportDAL.EmployeeInvoiceWiseDailySale();
        //}

        public List<CashierSaleTotalModel> CashierSaleTotal(DateTime FromDate, DateTime ToDate)
        {
            return objEmployeeSaleReportDAL.CashierSaleTotal(FromDate, ToDate);
        }

        public List<CashierSaleTotalByTransModel> CashierSaleTotalByTrans(DateTime FromDate, DateTime ToDate)
        {
            return objEmployeeSaleReportDAL.CashierSaleTotalByTrans(FromDate, ToDate);
        }

        //public List<EmployeeWiseDailyPaymentModel> EmployeeWiseDailyPayment()
        //{
        //    return objEmployeeSaleReportDAL.EmployeeWiseDailyPayment();
        //}

        //public List<EmployeeInvoiceWiseDailyPaymentModel> EmployeeInvoiceWiseDailyPayment()
        //{
        //    return objEmployeeSaleReportDAL.EmployeeInvoiceWiseDailyPayment();
        //}

        public List<CashierSaleStatusModel> CashierSaleStatus(DateTime FromDate, DateTime ToDate)
        {
            return objEmployeeSaleReportDAL.CashierSaleStatus(FromDate, ToDate);
        }

        public List<CashierSaleStatusByTransModel> CashierSaleStatusByTrans(DateTime FromDate, DateTime ToDate)
        {
            return objEmployeeSaleReportDAL.CashierSaleStatusByTrans(FromDate, ToDate);
        }
    }
}
