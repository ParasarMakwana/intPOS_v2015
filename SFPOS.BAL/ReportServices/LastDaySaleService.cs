using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class LastDaySaleService
    {
        LastDaysSalesReportDAL objLastDaysSalesReportDAL = new LastDaysSalesReportDAL();
        public List<LastDaysSalesReport_ResultModel> GetLastDaySale(long StoreID, int Flag)
        {
            return objLastDaysSalesReportDAL.GetLastDaySale(StoreID, Flag);
        }
    }
}
