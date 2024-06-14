using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SFPOS.BAL.MasterDataServices
{
    public class DepositeVerificationservice
    {
        DepositeVerificationDAL objDepositeVerification = new DepositeVerificationDAL();

        public List<ReportStatusModel> GetSystemReportData(long EmployeeID, DateTime dateval)
        {
            return objDepositeVerification.GetSystemReportData(EmployeeID, dateval);
        }

        public List<RegisterReportDataModel> GetRegisterReportData(DateTime date)
        {
            return objDepositeVerification.GetRegisterReportData(date);
        }
    }
}
