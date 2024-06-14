using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFPOS.DAL.Reports
{
    public class CashierSaleReportDAL
    {
        private DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //public List<EmployeeSale_ResultModel> GetAllEmployeeSales()
        //{
        //    var lstEmployeeSale_Result = new List<EmployeeSale_ResultModel>();

        //    var onjSP_GetEmployeeReport = _db.SP_GetEmployeeReportList(LoginInfo.StoreID).ToList();
        //    if (onjSP_GetEmployeeReport.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetEmployeeReportList_Result, EmployeeSale_ResultModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_GetEmployeeReportList_Result objSP_GetEmployeeReportList_Result in onjSP_GetEmployeeReport)
        //        {
        //            EmployeeSale_ResultModel _EmployeeSale_Result = iMapper.Map<SP_GetEmployeeReportList_Result, EmployeeSale_ResultModel>(objSP_GetEmployeeReportList_Result);
        //            lstEmployeeSale_Result.Add(_EmployeeSale_Result);
        //        }
        //    }
        //    return lstEmployeeSale_Result;
        //}

        //public List<EmployeeWiseDailySaleModel> EmployeeWiseDailySale()
        //{
        //    var lstEmployeeWiseDailySaleModel = new List<EmployeeWiseDailySaleModel>();

        //    var onjSP_Report_EmployeeSales_DailySalesHistory = _db.SP_Report_EmployeeSales_DailySalesHistory(LoginInfo.StoreID).ToList();
        //    if (onjSP_Report_EmployeeSales_DailySalesHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_EmployeeSales_DailySalesHistory_Result, EmployeeWiseDailySaleModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_EmployeeSales_DailySalesHistory_Result objSP_Report_EmployeeSales_DailySalesHistory_Result in onjSP_Report_EmployeeSales_DailySalesHistory)
        //        {
        //            EmployeeWiseDailySaleModel _EmployeeWiseDailySaleModel = iMapper.Map<SP_Report_EmployeeSales_DailySalesHistory_Result, EmployeeWiseDailySaleModel>(objSP_Report_EmployeeSales_DailySalesHistory_Result);
        //            lstEmployeeWiseDailySaleModel.Add(_EmployeeWiseDailySaleModel);
        //        }
        //    }
        //    return lstEmployeeWiseDailySaleModel;
        //}

        //public List<EmployeeInvoiceWiseDailySaleModel> EmployeeInvoiceWiseDailySale()
        //{
        //    var lstEmployeeInvoiceWiseDailySaleModel = new List<EmployeeInvoiceWiseDailySaleModel>();

        //    var onjSP_Report_EmployeeInvoiceSales_DailySalesHistory = _db.SP_Report_EmployeeInvoiceSales_DailySalesHistory(LoginInfo.StoreID).ToList();
        //    if (onjSP_Report_EmployeeInvoiceSales_DailySalesHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_EmployeeInvoiceSales_DailySalesHistory_Result, EmployeeInvoiceWiseDailySaleModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_EmployeeInvoiceSales_DailySalesHistory_Result objSP_Report_EmployeeInvoiceSales_DailySalesHistory_Result in onjSP_Report_EmployeeInvoiceSales_DailySalesHistory)
        //        {
        //            EmployeeInvoiceWiseDailySaleModel _EmployeeInvoiceWiseDailySaleModel = iMapper.Map<SP_Report_EmployeeInvoiceSales_DailySalesHistory_Result, EmployeeInvoiceWiseDailySaleModel>(objSP_Report_EmployeeInvoiceSales_DailySalesHistory_Result);
        //            lstEmployeeInvoiceWiseDailySaleModel.Add(_EmployeeInvoiceWiseDailySaleModel);
        //        }
        //    }
        //    return lstEmployeeInvoiceWiseDailySaleModel;
        //}

        public List<CashierSaleTotalModel> CashierSaleTotal(DateTime FromDate, DateTime ToDate)
        {
            var lstCashierSaleTotal = new List<CashierSaleTotalModel>();

            var onjSP_Rpt_CashierSaleTotal = _db.SP_Rpt_CashierSaleTotal(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_CashierSaleTotal.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_CashierSaleTotal_Result, CashierSaleTotalModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_CashierSaleTotal_Result objCashierSaleTotal_Result in onjSP_Rpt_CashierSaleTotal)
                {
                    CashierSaleTotalModel _CashierSaleTotalModel = iMapper.Map<SP_Rpt_CashierSaleTotal_Result, CashierSaleTotalModel>(objCashierSaleTotal_Result);
                    lstCashierSaleTotal.Add(_CashierSaleTotalModel);
                }
            }
            return lstCashierSaleTotal;
        }

        public List<CashierSaleTotalByTransModel> CashierSaleTotalByTrans(DateTime FromDate, DateTime ToDate)
        {
            var lstCashierSaleTotalByTrans = new List<CashierSaleTotalByTransModel>();

            var onjSP_Rpt_CashierSaleTotalByTrans = _db.SP_Rpt_CashierSaleTotalByTrans(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_CashierSaleTotalByTrans.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_CashierSaleTotalByTrans_Result, CashierSaleTotalByTransModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_CashierSaleTotalByTrans_Result objCashierSaleTotalByTrans_Result in onjSP_Rpt_CashierSaleTotalByTrans)
                {
                    CashierSaleTotalByTransModel _CashierSaleTotalByTransModel = iMapper.Map<SP_Rpt_CashierSaleTotalByTrans_Result, CashierSaleTotalByTransModel>(objCashierSaleTotalByTrans_Result);
                    lstCashierSaleTotalByTrans.Add(_CashierSaleTotalByTransModel);
                }
            }
            return lstCashierSaleTotalByTrans;
        }

        //public List<EmployeeWiseDailyPaymentModel> EmployeeWiseDailyPayment()
        //{
        //    var lstEmployeeWiseDailyPaymentModel = new List<EmployeeWiseDailyPaymentModel>();

        //    var onjSP_Report_EmployeeSales_DailySalesHistory = _db.SP_Report_EmployeeWiseSales_DailyPaymentHistory(LoginInfo.StoreID).ToList();
        //    if (onjSP_Report_EmployeeSales_DailySalesHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_EmployeeWiseSales_DailyPaymentHistory_Result, EmployeeWiseDailyPaymentModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_EmployeeWiseSales_DailyPaymentHistory_Result objSP_Report_EmployeeWiseSales_DailyPaymentHistory_Result in onjSP_Report_EmployeeSales_DailySalesHistory)
        //        {
        //            EmployeeWiseDailyPaymentModel _EmployeeWiseDailyPaymentModel = iMapper.Map<SP_Report_EmployeeWiseSales_DailyPaymentHistory_Result, EmployeeWiseDailyPaymentModel>(objSP_Report_EmployeeWiseSales_DailyPaymentHistory_Result);
        //            lstEmployeeWiseDailyPaymentModel.Add(_EmployeeWiseDailyPaymentModel);
        //        }
        //    }
        //    return lstEmployeeWiseDailyPaymentModel;
        //}

        //public List<EmployeeInvoiceWiseDailyPaymentModel> EmployeeInvoiceWiseDailyPayment()
        //{
        //    var lstEmployeeInvoiceWiseDailyPaymentModel = new List<EmployeeInvoiceWiseDailyPaymentModel>();

        //    var onjSP_Report_EmployeeInvoiceSales_DailyPaymentHistory = _db.SP_Report_EmployeeInvoiceSales_DailyPaymentHistory(LoginInfo.StoreID).ToList();
        //    if (onjSP_Report_EmployeeInvoiceSales_DailyPaymentHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_EmployeeInvoiceSales_DailyPaymentHistory_Result, EmployeeInvoiceWiseDailyPaymentModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_EmployeeInvoiceSales_DailyPaymentHistory_Result objSP_Report_EmployeeInvoiceSales_DailySalesHistory_Result in onjSP_Report_EmployeeInvoiceSales_DailyPaymentHistory)
        //        {
        //            EmployeeInvoiceWiseDailyPaymentModel _EmployeeInvoiceWiseDailyPaymentModel = iMapper.Map<SP_Report_EmployeeInvoiceSales_DailyPaymentHistory_Result, EmployeeInvoiceWiseDailyPaymentModel>(objSP_Report_EmployeeInvoiceSales_DailySalesHistory_Result);
        //            lstEmployeeInvoiceWiseDailyPaymentModel.Add(_EmployeeInvoiceWiseDailyPaymentModel);
        //        }
        //    }
        //    return lstEmployeeInvoiceWiseDailyPaymentModel;
        //}

        public List<CashierSaleStatusModel> CashierSaleStatus(DateTime FromDate, DateTime ToDate)
        {
            var lstEmployeeWisePaymentModel = new List<CashierSaleStatusModel>();

            var onjSP_Rpt_CashierSaleStatus = _db.SP_Rpt_CashierSaleStatus(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_CashierSaleStatus.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_CashierSaleStatus_Result, CashierSaleStatusModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_CashierSaleStatus_Result objCashierSaleStatus_Result in onjSP_Rpt_CashierSaleStatus)
                {
                    CashierSaleStatusModel _CashierSaleStatusModel = iMapper.Map<SP_Rpt_CashierSaleStatus_Result, CashierSaleStatusModel>(objCashierSaleStatus_Result);
                    lstEmployeeWisePaymentModel.Add(_CashierSaleStatusModel);
                }
            }
            return lstEmployeeWisePaymentModel;
        }

        public List<CashierSaleStatusByTransModel> CashierSaleStatusByTrans(DateTime FromDate, DateTime ToDate)
        {
            var lstCashierSaleStatusByTrans = new List<CashierSaleStatusByTransModel>();

            var SP_Rpt_CashierSaleStatusByTrans = _db.SP_Rpt_CashierSaleStatusByTrans(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (SP_Rpt_CashierSaleStatusByTrans.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_CashierSaleStatusByTrans_Result, CashierSaleStatusByTransModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_CashierSaleStatusByTrans_Result objCashierSaleStatusByTrans_Result in SP_Rpt_CashierSaleStatusByTrans)
                {
                    CashierSaleStatusByTransModel _CashierSaleStatusByTransModel = iMapper.Map<SP_Rpt_CashierSaleStatusByTrans_Result, CashierSaleStatusByTransModel>(objCashierSaleStatusByTrans_Result);
                    lstCashierSaleStatusByTrans.Add(_CashierSaleStatusByTransModel);
                }
            }
            return lstCashierSaleStatusByTrans;
        }
    }
}
