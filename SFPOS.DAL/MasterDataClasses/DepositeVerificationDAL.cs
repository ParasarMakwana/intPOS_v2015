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
   public class DepositeVerificationDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<ReportStatusModel> GetSystemReportData(long EmployeeID, DateTime dateval)
        {
            var lstReportStatusModel = new List<ReportStatusModel>();

            var onjtbl_ProductMaster = _db.SP_TillStatusEmployeeReport_System(EmployeeID, dateval).ToList();

            if (onjtbl_ProductMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_TillStatusEmployeeReport_System_Result, ReportStatusModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_TillStatusEmployeeReport_System_Result objtbl_ProductMaster in onjtbl_ProductMaster)
                {
                    ReportStatusModel _ProductMasterModel = iMapper.Map<SP_TillStatusEmployeeReport_System_Result, ReportStatusModel>(objtbl_ProductMaster);
                    lstReportStatusModel.Add(_ProductMasterModel);
                }
            }
            return lstReportStatusModel;
        }

        public List<RegisterReportDataModel> GetRegisterReportData(DateTime date)
        {
            var lstRegisterReportDataModel = new List<RegisterReportDataModel>();

            var onjtbl_ProductMaster = _db.SP_TillStatusEmployeeReportsTotal(0,date).ToList();

            if (onjtbl_ProductMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_TillStatusEmployeeReportsTotal_Result, RegisterReportDataModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_TillStatusEmployeeReportsTotal_Result objtbl_ProductMaster in onjtbl_ProductMaster)
                {
                    RegisterReportDataModel _ProductMasterModel = iMapper.Map<SP_TillStatusEmployeeReportsTotal_Result, RegisterReportDataModel>(objtbl_ProductMaster);
                    lstRegisterReportDataModel.Add(_ProductMasterModel);
                }
            }
            return lstRegisterReportDataModel;
        }
   }
}
