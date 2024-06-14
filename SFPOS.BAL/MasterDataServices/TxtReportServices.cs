using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
     public class TxtReportServices
    {
        TxtReportDAL objTxtReportDAL = new TxtReportDAL();

        public List<TillReportModel> TillStatusEmployeeReport(DateTime FromDate)
        {
            return objTxtReportDAL.TillStatusEmployeeReport(FromDate);
        }
    }
}
