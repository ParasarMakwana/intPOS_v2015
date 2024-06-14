using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;

namespace SFPOS.BAL.ReportServices
{
    public class DepartmentWiseSaleService
    {
        DepartmentWiseSaleReportDAL objDepartmentWiseSaleReportDAL = new DepartmentWiseSaleReportDAL();
        public List<DepartmentWiseSale_ResultModel> GetAllDepartmentWiseSales(long StoreID)
        {
            return objDepartmentWiseSaleReportDAL.GetAllDepartmentWiseSales(StoreID);
        }

        //public List<DepartmentSale_ResultModel> GetAllDepartmentSales(long StoreID, long DepartmentID, DateTime startDate, DateTime endDate)
        //{
        //    return objDepartmentWiseSaleReportDAL.GetAllDepartmentSales(StoreID, DepartmentID, startDate, endDate);
        //}

        //public List<DepartmentWiseDailySaleModel> DepartmentWiseDailySale()
        //{
        //    return objDepartmentWiseSaleReportDAL.DepartmentWiseDailySale();
        //}
        
        public List<DepartmentWiseSaleHistoryModel> DepartmentWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            return objDepartmentWiseSaleReportDAL.DepartmentWiseSaleHistory(FromDate, ToDate);
        }

        //public List<DepartmentWiseDailyPaymentModel> DepartmentWiseDailyPayment()
        //{
        //    return objDepartmentWiseSaleReportDAL.DepartmentWiseDailyPayment();
        //}

        //public List<DepartmentWisePaymentModel> DepartmentWisePayment(DateTime FromDate, DateTime ToDate)
        //{
        //    return objDepartmentWiseSaleReportDAL.DepartmentWisePayment(FromDate, ToDate);
        //}
    }
}
