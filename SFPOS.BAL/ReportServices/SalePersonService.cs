using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class SalePersonService
    {
        SalesPersonReportDAL objSalesPersonReportDAL = new SalesPersonReportDAL();
        //public List<SalesPerson_ResultModel> GetAllSalesPerson(long StoreID, long EmployeeID, DateTime startdate, DateTime enddate)
        //{
        //    return objSalesPersonReportDAL.GetAllSalesPerson(StoreID, EmployeeID, startdate, enddate);
        //}
    }
}
