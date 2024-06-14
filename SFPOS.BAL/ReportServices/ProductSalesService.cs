using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class ProductSalesService
    {
        ProductSalesDAL objProductSalesDAL = new ProductSalesDAL();
        public List<ProductSale_ResultModel> GetProductDetails(DateTime StartDate, DateTime EndDate, long DepartmentID, long SectionID, long VenderID, int Flag)
        {
            return objProductSalesDAL.GetProductDetails(StartDate, EndDate, DepartmentID, SectionID, VenderID, Flag);
        }

        //public List<ProductWiseDailySaleModel> ProductWiseDailySale()
        //{
        //    return objProductSalesDAL.ProductWiseDailySale();
        //}

        public List<ProductWiseSaleHistoryModel> ProductWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            return objProductSalesDAL.ProductWiseSaleHistory(FromDate, ToDate);
        }
    }
}
