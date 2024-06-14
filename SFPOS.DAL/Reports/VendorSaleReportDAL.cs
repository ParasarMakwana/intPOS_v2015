using AutoMapper;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFPOS.Common;

namespace SFPOS.DAL.Reports
{
    public class VendorSaleReportDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //public List<VendorSale_ResultModel> GetAllVendorSales(long StoreID, long VendorID, DateTime startdate, DateTime enddate)
        //{
        //    var lstVendorSale_Result = new List<VendorSale_ResultModel>();

        //    var onjSP_GetVendorReport = _db.SP_GetVendorReportList(StoreID, VendorID, startdate, enddate).ToList();
        //    if (onjSP_GetVendorReport.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetVendorReportList_Result, VendorSale_ResultModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_GetVendorReportList_Result objSP_GetVendorReportList_Result in onjSP_GetVendorReport)
        //        {
        //            VendorSale_ResultModel _VendorSale_Result = iMapper.Map<SP_GetVendorReportList_Result, VendorSale_ResultModel>(objSP_GetVendorReportList_Result);
        //            lstVendorSale_Result.Add(_VendorSale_Result);
        //        }
        //    }
        //    return lstVendorSale_Result;
        //}
    }
}
