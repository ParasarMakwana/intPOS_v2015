using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class InventoryService
    {
        InventoryReportDAL objInventoryReportDAL = new InventoryReportDAL();

        //public List<InventoryReport_Result> GetAllInventory(string Fromdate, string ToDate)
        //{
        //    return objInventoryReportDAL.GetAllInventory(Fromdate, ToDate);
        //}
    }
}
