using AutoMapper;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFPOS.Common;

namespace SFPOS.DAL.MasterDataClasses
{
    public class InventoryReportDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //public List<InventoryReport_Result> GetAllInventory(string FromDate, string Todate)   
        //{
        //    var lstInventoryReport_Result = new List<InventoryReport_Result>();
            
        //    var onjSP_InventoryReport = _db.SP_InventoryReport(FromDate, Todate).ToList();
        //    if (onjSP_InventoryReport.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_InventoryReport_Result, InventoryReport_Result>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_InventoryReport_Result objSP_InventoryReport_Result in onjSP_InventoryReport)
        //        {
        //            InventoryReport_Result _InventoryReport_Result = iMapper.Map<SP_InventoryReport_Result, InventoryReport_Result>(objSP_InventoryReport_Result);
        //            lstInventoryReport_Result.Add(_InventoryReport_Result);
        //        }
        //    }
        //    return lstInventoryReport_Result;
        //}
    }
}
