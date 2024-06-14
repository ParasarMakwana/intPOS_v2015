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
    public class TillStatusDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<TillReportModel> GetTillStatusEmployeeReport(long EmployeeID, DateTime dateval)
        {
            var lstTillReportModel = new List<TillReportModel>();

            //  var onjtbl_ProductMaster = _db.tbl_ProductMaster.Where(x => x.IsDelete == false).OrderBy(x => x.ProductName).ToList();
            var onjtbl_ProductMaster = _db.SP_TillStatusEmployeeReport(EmployeeID, dateval).ToList();

            if (onjtbl_ProductMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_TillStatusEmployeeReport_Result, TillReportModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_TillStatusEmployeeReport_Result objtbl_ProductMaster in onjtbl_ProductMaster)
                {
                    TillReportModel _ProductMasterModel = iMapper.Map<SP_TillStatusEmployeeReport_Result, TillReportModel>(objtbl_ProductMaster);
                    lstTillReportModel.Add(_ProductMasterModel);
                }
            }
            return lstTillReportModel;
        }

        public List<ReportStatusModel> GetTillStatusEmployeeReport_System(long EmployeeID, DateTime dateval)
        {
            var lstReportStatusModel = new List<ReportStatusModel>();

            //  var onjtbl_ProductMaster = _db.tbl_ProductMaster.Where(x => x.IsDelete == false).OrderBy(x => x.ProductName).ToList();
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


        public TillReportModel AddTillStatus(TillReportModel objTillReportModel, int TransType)
        {
            try
            {
                tbl_TillStatusReport objtbl_TillStatusReport = new tbl_TillStatusReport();
                if (TransType == 1)//ADD
                {
                    objTillReportModel.TillStatusReportID = objTillReportModel.TillStatusReportID;
                    objtbl_TillStatusReport.SelectedDate = objTillReportModel.SelectedDate;
                    objtbl_TillStatusReport.CashierID = objTillReportModel.CashierID;
                    objtbl_TillStatusReport.CashierName = objTillReportModel.CashierName;
                    objtbl_TillStatusReport.Coin = objTillReportModel.Coin;
                    objtbl_TillStatusReport.Cash = objTillReportModel.Cash;
                    objtbl_TillStatusReport.CreditCard = objTillReportModel.CreditCard;
                    objtbl_TillStatusReport.Checks = objTillReportModel.Checks;
                    objtbl_TillStatusReport.CashPayout = objTillReportModel.CashPayout;
                    objtbl_TillStatusReport.TakeOut = objTillReportModel.TakeOut;
                    objtbl_TillStatusReport.BackInDrawer = objTillReportModel.BackInDrawer;
                    objtbl_TillStatusReport.SortBy = objTillReportModel.SortBy;
                    objtbl_TillStatusReport.OverBy = objTillReportModel.OverBy;
                    objtbl_TillStatusReport.CreatedDate = DateTime.Now;
                    objtbl_TillStatusReport.CreatedBy = LoginInfo.UserId;
                    objtbl_TillStatusReport.Lotto = objTillReportModel.Lotto;
                    objtbl_TillStatusReport.SelfService = objTillReportModel.SelfService;
                    objtbl_TillStatusReport.Scrathers = objTillReportModel.Scrathers;
                    _db.tbl_TillStatusReport.Add(objtbl_TillStatusReport);
                    objtbl_TillStatusReport.TillStatusReportID = objTillReportModel.TillStatusReportID;
                }
                else if(TransType == 2)//Edit
                {
                    objtbl_TillStatusReport = _db.tbl_TillStatusReport.Where(p => p.TillStatusReportID == objTillReportModel.TillStatusReportID).FirstOrDefault();
                    if (objtbl_TillStatusReport != null)
                    {
                        objtbl_TillStatusReport.TillStatusReportID = objTillReportModel.TillStatusReportID;
                        objtbl_TillStatusReport.SelectedDate = objTillReportModel.SelectedDate;
                        objtbl_TillStatusReport.CashierID = objTillReportModel.CashierID;
                        objtbl_TillStatusReport.CashierName = objTillReportModel.CashierName;
                        objtbl_TillStatusReport.Coin = objTillReportModel.Coin;
                        objtbl_TillStatusReport.Cash = objTillReportModel.Cash;
                        objtbl_TillStatusReport.CreditCard = objTillReportModel.CreditCard;
                        objtbl_TillStatusReport.Checks = objTillReportModel.Checks;
                        objtbl_TillStatusReport.CashPayout = objTillReportModel.CashPayout;
                        objtbl_TillStatusReport.TakeOut = objTillReportModel.TakeOut;
                        objtbl_TillStatusReport.BackInDrawer = objTillReportModel.BackInDrawer;
                        objtbl_TillStatusReport.SortBy = objTillReportModel.SortBy;
                        objtbl_TillStatusReport.OverBy = objTillReportModel.OverBy;
                        objtbl_TillStatusReport.UpdatedDate = DateTime.Now;
                        objtbl_TillStatusReport.UpdatedBy = LoginInfo.UserId;
                        objtbl_TillStatusReport.Lotto = objTillReportModel.Lotto;
                        objtbl_TillStatusReport.SelfService = objTillReportModel.SelfService;
                        objtbl_TillStatusReport.Scrathers = objTillReportModel.Scrathers;
                    }
               }
                _db.SaveChanges();
            }
            catch (Exception e)
            {

                string ex = e.Message;
            }
            return objTillReportModel;
        }

    }
}
