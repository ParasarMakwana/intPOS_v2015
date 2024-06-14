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
    public class SectionSaleReportDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //public List<Section_ResultModel> GetAllSectionSales(long StoreID, long DepartmentID,DateTime startdate,DateTime enddate)
        //{
        //    var lstSectionSale_Result = new List<Section_ResultModel>();

        //    var onjSP_GetSectionSale = _db.SP_GetSectionReportList(StoreID, DepartmentID, startdate, enddate).ToList();
        //    if (onjSP_GetSectionSale.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetSectionReportList_Result, Section_ResultModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_GetSectionReportList_Result objSP_GetReportSectionWiseSales_Result in onjSP_GetSectionSale)
        //        {
        //            Section_ResultModel _Section_Result = iMapper.Map<SP_GetSectionReportList_Result, Section_ResultModel>(objSP_GetReportSectionWiseSales_Result);
        //            lstSectionSale_Result.Add(_Section_Result);
        //        }
        //    }
        //    return lstSectionSale_Result;
        //}

        public List<SectionWiseSaleHistoryModel> SectionWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            var lstSectionSale_Result = new List<SectionWiseSaleHistoryModel>();

            var onjSP_GetSectionSale = _db.SP_Rpt_SectionWiseSaleHistory(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_GetSectionSale.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_SectionWiseSaleHistory_Result, SectionWiseSaleHistoryModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_SectionWiseSaleHistory_Result objSP_GetReportSectionWiseSales_Result in onjSP_GetSectionSale)
                {
                    SectionWiseSaleHistoryModel _Section_Result = iMapper.Map<SP_Rpt_SectionWiseSaleHistory_Result, SectionWiseSaleHistoryModel>(objSP_GetReportSectionWiseSales_Result);
                    lstSectionSale_Result.Add(_Section_Result);
                }
            }
            return lstSectionSale_Result;
        }
    }
}
