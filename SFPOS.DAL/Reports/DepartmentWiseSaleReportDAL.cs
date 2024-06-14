using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.Reports
{
    public class DepartmentWiseSaleReportDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<DepartmentWiseSale_ResultModel> GetAllDepartmentWiseSales(long StoreID)
        {
            var lstdepartmentWiseSale_Result = new List<DepartmentWiseSale_ResultModel>();

            var onjSP_GetReportDayMonthYearSell = _db.SP_GetReportDepartmentWiseSales(StoreID).ToList();
            if (onjSP_GetReportDayMonthYearSell.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetReportDepartmentWiseSales_Result, DepartmentWiseSale_ResultModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetReportDepartmentWiseSales_Result objSP_GetReportdepartmentWiseSales_Result in onjSP_GetReportDayMonthYearSell)
                {
                    DepartmentWiseSale_ResultModel _departmentWiseSale_Result = iMapper.Map<SP_GetReportDepartmentWiseSales_Result, DepartmentWiseSale_ResultModel>(objSP_GetReportdepartmentWiseSales_Result);
                    lstdepartmentWiseSale_Result.Add(_departmentWiseSale_Result);
                }
            }
            return lstdepartmentWiseSale_Result;
        }

        //public List<DepartmentSale_ResultModel> GetAllDepartmentSales(long StoreID,long DepartmentID, DateTime startDate, DateTime endDate)
        //{
        //    var lstDepartmentSale_Result = new List<DepartmentSale_ResultModel>();

        //    var onjSP_GetdepartmentSale = _db.SP_GetDepartmentReportList(StoreID,DepartmentID, startDate, endDate).ToList();
        //    if (onjSP_GetdepartmentSale.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetDepartmentReportList_Result, DepartmentSale_ResultModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_GetDepartmentReportList_Result objSP_GetReportdepartmentWiseSales_Result in onjSP_GetdepartmentSale)
        //        {
        //            DepartmentSale_ResultModel _departmentWiseSale_Result = iMapper.Map<SP_GetDepartmentReportList_Result, DepartmentSale_ResultModel>(objSP_GetReportdepartmentWiseSales_Result);
        //            lstDepartmentSale_Result.Add(_departmentWiseSale_Result);
        //        }
        //    }
        //    return lstDepartmentSale_Result;
        //}

        //public List<DepartmentWiseDailySaleModel> DepartmentWiseDailySale()
        //{
        //    var lstDepartmentWiseDailySaleModel = new List<DepartmentWiseDailySaleModel>();

        //    var onjSP_Report_DepartmentWiseSales_DailySalesHistory = _db.SP_Report_DepartmentWiseSales_DailySalesHistory(LoginInfo.StoreID).ToList();
        //    if (onjSP_Report_DepartmentWiseSales_DailySalesHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_DepartmentWiseSales_DailySalesHistory_Result, DepartmentWiseDailySaleModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_DepartmentWiseSales_DailySalesHistory_Result objSP_Report_DepartmentWiseSales_DailySalesHistory_Result in onjSP_Report_DepartmentWiseSales_DailySalesHistory)
        //        {
        //            DepartmentWiseDailySaleModel _DepartmentWiseDailySaleModel = iMapper.Map<SP_Report_DepartmentWiseSales_DailySalesHistory_Result, DepartmentWiseDailySaleModel>(objSP_Report_DepartmentWiseSales_DailySalesHistory_Result);
        //            lstDepartmentWiseDailySaleModel.Add(_DepartmentWiseDailySaleModel);
        //        }
        //    }
        //    return lstDepartmentWiseDailySaleModel;
        //}

        public List<DepartmentWiseSaleHistoryModel> DepartmentWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            var lstDepartmentWiseDailySaleModel = new List<DepartmentWiseSaleHistoryModel>();

            var onjSP_Rpt_DepartmentWiseSaleHistory = _db.SP_Rpt_DepartmentWiseSaleHistory(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_DepartmentWiseSaleHistory.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_DepartmentWiseSaleHistory_Result, DepartmentWiseSaleHistoryModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_DepartmentWiseSaleHistory_Result objDepartmentWiseSaleHistory_Result in onjSP_Rpt_DepartmentWiseSaleHistory)
                {
                    DepartmentWiseSaleHistoryModel _DepartmentWiseDailySaleModel = iMapper.Map<SP_Rpt_DepartmentWiseSaleHistory_Result, DepartmentWiseSaleHistoryModel>(objDepartmentWiseSaleHistory_Result);
                    lstDepartmentWiseDailySaleModel.Add(_DepartmentWiseDailySaleModel);
                }
            }
            return lstDepartmentWiseDailySaleModel;
        }

        //public List<DepartmentWisePaymentModel> DepartmentWisePayment(DateTime FromDate, DateTime ToDate)
        //{
        //    var lstDepartmentWiseDailySaleModel = new List<DepartmentWisePaymentModel>();

        //    var onjSP_Report_DepartmentWiseSales_PaymentHistory = _db.SP_Report_DepartmentWiseSales_PaymentHistory(LoginInfo.StoreID, FromDate, ToDate).ToList();
        //    if (onjSP_Report_DepartmentWiseSales_DailySalesHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_DepartmentWiseSales_PaymentHistory_Result, DepartmentWisePaymentModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_DepartmentWiseSales_SalesHistory_Result objSP_Report_DepartmentWiseSales_DailySalesHistory_Result in onjSP_Report_DepartmentWiseSales_DailySalesHistory)
        //        {
        //            DepartmentWisePaymentModel _DepartmentWisePaymentModel = iMapper.Map<SP_Report_DepartmentWiseSales_SalesHistory_Result, DepartmentWisePaymentModel>(objSP_Report_DepartmentWiseSales_DailySalesHistory_Result);
        //            lstDepartmentWiseDailySaleModel.Add(_DepartmentWisePaymentModel);
        //        }
        //    }
        //    return lstDepartmentWiseDailySaleModel;
        //}

        //public List<DepartmentWiseDailyPaymentModel> DepartmentWiseDailyPayment()
        //{
        //    var lstDepartmentWiseDailyPaymentModel = new List<DepartmentWiseDailyPaymentModel>();

        //    //var onjSP_Report_DepartmentWiseSales_PaymentHistory = _db.SP_Report_DepartmentWiseSales_PaymentHistory(LoginInfo.StoreID, FromDate, ToDate).ToList();
        //    //if (onjSP_Report_DepartmentWiseSales_DailySalesHistory.Count > 0)
        //    //{
        //    //    var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_DepartmentWiseSales_PaymentHistory_Result, DepartmentWisePaymentModel>(); });
        //    //    IMapper iMapper = config.CreateMapper();
        //    //    foreach (SP_Report_DepartmentWiseSales_SalesHistory_Result objSP_Report_DepartmentWiseSales_DailySalesHistory_Result in onjSP_Report_DepartmentWiseSales_DailySalesHistory)
        //    //    {
        //    //        DepartmentWisePaymentModel _DepartmentWisePaymentModel = iMapper.Map<SP_Report_DepartmentWiseSales_SalesHistory_Result, DepartmentWisePaymentModel>(objSP_Report_DepartmentWiseSales_DailySalesHistory_Result);
        //    //        lstDepartmentWiseDailySaleModel.Add(_DepartmentWisePaymentModel);
        //    //    }
        //    //}
        //    return lstDepartmentWiseDailyPaymentModel;
        //}
    }
}
