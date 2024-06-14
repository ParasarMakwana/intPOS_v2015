using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class TxtReportDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<TillReportModel> TillStatusEmployeeReport(DateTime FromDate)
        {
            var lstRegisterSaleTotal = new List<TillReportModel>();

            var onjSP_Rpt_RegisterSaleTotal = _db.SP_TillStatusEmployeeReports(FromDate).ToList();
            if (onjSP_Rpt_RegisterSaleTotal.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_TillStatusEmployeeReports_Result, TillReportModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_TillStatusEmployeeReports_Result objRegisterSaleTotal_Result in onjSP_Rpt_RegisterSaleTotal)
                {
                    TillReportModel _RegisterSaleTotalModel = iMapper.Map<SP_TillStatusEmployeeReports_Result, TillReportModel>(objRegisterSaleTotal_Result);
                    lstRegisterSaleTotal.Add(_RegisterSaleTotalModel);
                }
            }
            return lstRegisterSaleTotal;
        }
    }
}
