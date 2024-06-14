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
    public class LastDaysSalesReportDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<LastDaysSalesReport_ResultModel> GetLastDaySale(long StoreID, int Flag)
        {
            var lstLastDaysSalesReport_ResultModel = new List<LastDaysSalesReport_ResultModel>();

            var onjSP_GetReportLastDaysSales = _db.SP_GetReportLastDaysSales(StoreID, Flag).ToList();
            if (onjSP_GetReportLastDaysSales.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetReportLastDaysSales_Result,LastDaysSalesReport_ResultModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetReportLastDaysSales_Result objSP_GetReportLastDaysSales_Result in onjSP_GetReportLastDaysSales)
                {
                    LastDaysSalesReport_ResultModel _DayMonthYearSaleReport_Result = iMapper.Map<SP_GetReportLastDaysSales_Result, LastDaysSalesReport_ResultModel>(objSP_GetReportLastDaysSales_Result);
                    lstLastDaysSalesReport_ResultModel.Add(_DayMonthYearSaleReport_Result);
                }
            }
            return lstLastDaysSalesReport_ResultModel;
        }
    }
}
