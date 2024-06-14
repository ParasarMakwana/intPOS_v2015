using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;

namespace SFPOS.BAL.ReportServices
{
    public class SectionSaleService
    {
        SectionSaleReportDAL objSectionSaleReportDAL = new SectionSaleReportDAL();
        //public List<Section_ResultModel> GetAllSectionSales(long StoreID, long DepartmentID, DateTime startdate, DateTime enddate)
        //{
        //    return objSectionSaleReportDAL.GetAllSectionSales(StoreID, DepartmentID, startdate, enddate);
        //}

        //public List<SectionWiseDailySaleModel> SectionWiseDailySale()
        //{
        //    return objSectionSaleReportDAL.SectionWiseDailySale();
        //}

        public List<SectionWiseSaleHistoryModel> SectionWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            return objSectionSaleReportDAL.SectionWiseSaleHistory(FromDate, ToDate);
        }
    }
}
