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
    public class DayMonthYearSaleDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<DayMonthYearSaleReport_ResultModel> GetAllDayMontYearSale(long StoreID,int Month , int Year)   
        {
            var lstDayMonthYearSaleReport_Result = new List<DayMonthYearSaleReport_ResultModel>();
            
            var onjSP_GetReportDayMonthYearSell = _db.SP_GetReportDayMonthYearSell(StoreID, Month, Year).ToList();
            if (onjSP_GetReportDayMonthYearSell.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetReportDayMonthYearSell_Result, DayMonthYearSaleReport_ResultModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetReportDayMonthYearSell_Result objSP_GetReportDayMonthYearSell_Result in onjSP_GetReportDayMonthYearSell)
                {
                    DayMonthYearSaleReport_ResultModel _DayMonthYearSaleReport_Result = iMapper.Map<SP_GetReportDayMonthYearSell_Result, DayMonthYearSaleReport_ResultModel>(objSP_GetReportDayMonthYearSell_Result);
                    lstDayMonthYearSaleReport_Result.Add(_DayMonthYearSaleReport_Result);
                }
            }
            return lstDayMonthYearSaleReport_Result;
        }
    }
}
