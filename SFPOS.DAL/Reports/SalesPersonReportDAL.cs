using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.Reports
{
    public class SalesPersonReportDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //public List<SalesPerson_ResultModel> GetAllSalesPerson(long StoreID, long EmployeeID, DateTime startdate, DateTime enddate)
        //{
        //    var lstSalesPerson_Result = new List<SalesPerson_ResultModel>();

        //    var onjSP_GetSalesPersonReport = _db.SP_GetSalesPersonList(StoreID, EmployeeID, startdate, enddate).ToList();
        //    if (onjSP_GetSalesPersonReport.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetSalesPersonList_Result, SalesPerson_ResultModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_GetSalesPersonList_Result objSP_GetSalesPersonList_Result in onjSP_GetSalesPersonReport)
        //        {
        //            SalesPerson_ResultModel _VendorSale_Result = iMapper.Map<SP_GetSalesPersonList_Result, SalesPerson_ResultModel>(objSP_GetSalesPersonList_Result);
        //            lstSalesPerson_Result.Add(_VendorSale_Result);
        //        }
        //    }
        //    return lstSalesPerson_Result;
        //}
    }
}
