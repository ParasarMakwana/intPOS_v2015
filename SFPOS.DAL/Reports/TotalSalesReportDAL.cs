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
    public class ReportTotalSalesDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<ReportTotalSales_ResultModel> GetTotalSales(long StoreID)
        {
            var lstGetReportTotalSales_ResultModel = new List<ReportTotalSales_ResultModel>();

            var objGetReportTotalSales = _db.SP_GetReportTotalSales(StoreID).ToList();
            if (objGetReportTotalSales != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetReportTotalSales_Result, ReportTotalSales_ResultModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetReportTotalSales_Result objSP_GetReportTotalSales_Result in objGetReportTotalSales)
                {
                    ReportTotalSales_ResultModel _GetReportTotalSales_ResultModel = iMapper.Map<SP_GetReportTotalSales_Result, ReportTotalSales_ResultModel>(objSP_GetReportTotalSales_Result);
                    lstGetReportTotalSales_ResultModel.Add(_GetReportTotalSales_ResultModel);
                }
            }
            return lstGetReportTotalSales_ResultModel;
        }
    }
}
