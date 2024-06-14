using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.Reports
{
    public class SectionWiseTaxHistoryReportDAL
    {
        private DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public List<SectionWiseTaxHistoryReportModel> SectionWiseTaxHistoryReport(DateTime FromDate, DateTime ToDate)
        {
            var lstTaxReportModel = new List<SectionWiseTaxHistoryReportModel>();

            var onjSP_Rpt_TaxReport = _db.SP_Rpt_SectionWiseTaxHistory(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_TaxReport.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_SectionWiseTaxHistory_Result, SectionWiseTaxHistoryReportModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_SectionWiseTaxHistory_Result objSP_Rpt_TaxReport_Result in onjSP_Rpt_TaxReport)
                {
                    SectionWiseTaxHistoryReportModel _TaxReportModel = iMapper.Map<SP_Rpt_SectionWiseTaxHistory_Result, SectionWiseTaxHistoryReportModel>(objSP_Rpt_TaxReport_Result);
                    lstTaxReportModel.Add(_TaxReportModel);
                }
            }
            return lstTaxReportModel;
        }
    }
}
