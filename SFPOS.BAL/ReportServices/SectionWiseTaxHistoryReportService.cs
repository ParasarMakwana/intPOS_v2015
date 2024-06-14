using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class SectionWiseTaxHistoryReportService
    {
        SectionWiseTaxHistoryReportDAL objTaxReportDAL = new SectionWiseTaxHistoryReportDAL();
        public List<SectionWiseTaxHistoryReportModel> SectionWiseTaxHistoryReport(DateTime FromDate, DateTime ToDate)
        {
            return objTaxReportDAL.SectionWiseTaxHistoryReport(FromDate, ToDate);
        }
    }
}
