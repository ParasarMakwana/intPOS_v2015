using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;
using System;

namespace SFPOS.BAL.MasterDataServices
{
    public class TillStatusService
    {
        TillStatusDAL objTillStatusDAL = new TillStatusDAL();
        public List<TillReportModel> GetTillStatusEmployeeReport(long EmployeeID, DateTime dateval)
        {
            return objTillStatusDAL.GetTillStatusEmployeeReport(EmployeeID, dateval);
        }

        public List<ReportStatusModel> GetTillStatusEmployeeReport_System(long EmployeeID, DateTime dateval)
        {
            return objTillStatusDAL.GetTillStatusEmployeeReport_System(EmployeeID, dateval);
        }

        public TillReportModel AddTillStatus(TillReportModel objTillReportModel, int TransType)
        {
            return objTillStatusDAL.AddTillStatus(objTillReportModel, TransType);
        }

    }
}
