using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class TotalSalesService
    {
        ReportTotalSalesDAL objReportTotalSalesDAL = new ReportTotalSalesDAL();

        public List<ReportTotalSales_ResultModel> GetTotalSales(long StoreID)
        {
            return objReportTotalSalesDAL.GetTotalSales(StoreID);
        }
    }
}
