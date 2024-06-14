using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class DayMonthYearSaleService
    {
        DayMonthYearSaleDAL objDayMonthYearSaleDAL = new DayMonthYearSaleDAL();
        public List<DayMonthYearSaleReport_ResultModel> GetAllDayMontYearSale(long StoreID, int Month, int Year)
        {
            return objDayMonthYearSaleDAL.GetAllDayMontYearSale(StoreID, Month, Year);
        }
    }
}
