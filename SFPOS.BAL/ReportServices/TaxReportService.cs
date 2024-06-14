using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class TaxReportService
    {
        TaxReportDAL objTaxReportDAL = new TaxReportDAL();
        public List<TaxReportModel> TaxReport(DateTime FromDate, DateTime ToDate)
        {
            return objTaxReportDAL.TaxReport(FromDate, ToDate);
        }
    }
}
