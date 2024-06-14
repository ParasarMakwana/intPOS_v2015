using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class VendorSaleService
    {
        VendorSaleReportDAL objVendorSaleReportDAL = new VendorSaleReportDAL();
        //public List<VendorSale_ResultModel> GetAllVendorSales(long StoreID, long VendorID, DateTime startdate, DateTime enddate)
        //{
        //    return objVendorSaleReportDAL.GetAllVendorSales(StoreID, VendorID, startdate, enddate);
        //}
    }
}
