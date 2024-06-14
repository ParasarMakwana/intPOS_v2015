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
    public class RegisterSaleReportDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //public List<CounterWiseDailySaleModel> CounterWiseDailySale()
        //{
        //    var lstCounterWiseDailySale = new List<CounterWiseDailySaleModel>();

        //    var onjSP_Report_CounterWiseSales_DailySalesHistory = _db.SP_Report_CounterWiseSales_DailySalesHistory(LoginInfo.StoreID).ToList();
        //    if (onjSP_Report_CounterWiseSales_DailySalesHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_CounterWiseSales_DailySalesHistory_Result, CounterWiseDailySaleModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_CounterWiseSales_DailySalesHistory_Result objSP_Report_CounterWiseSales_DailySalesHistory_Result in onjSP_Report_CounterWiseSales_DailySalesHistory)
        //        {
        //            CounterWiseDailySaleModel _CounterWiseDailySale_Result = iMapper.Map<SP_Report_CounterWiseSales_DailySalesHistory_Result, CounterWiseDailySaleModel>(objSP_Report_CounterWiseSales_DailySalesHistory_Result);
        //            lstCounterWiseDailySale.Add(_CounterWiseDailySale_Result);
        //        }
        //    }
        //    return lstCounterWiseDailySale;
        //}

        //public List<CounterInvoiceWiseDailySaleModel> CounterInvoiceWiseDailySale()
        //{
        //    var lstCounterInvoiceWiseDailySaleModel = new List<CounterInvoiceWiseDailySaleModel>();

        //    var onjSP_Report_CounterInvoiceWiseSales_DailySalesHistory = _db.SP_Report_CounterInvoiceWiseSales_DailySalesHistory(LoginInfo.StoreID).ToList();
        //    if (onjSP_Report_CounterInvoiceWiseSales_DailySalesHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_CounterInvoiceWiseSales_DailySalesHistory_Result, CounterInvoiceWiseDailySaleModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_CounterInvoiceWiseSales_DailySalesHistory_Result objSP_Report_CounterInvoiceWiseSales_DailySalesHistory_Result in onjSP_Report_CounterInvoiceWiseSales_DailySalesHistory)
        //        {
        //            CounterInvoiceWiseDailySaleModel _CounterInvoiceWiseDailySaleModel = iMapper.Map<SP_Report_CounterInvoiceWiseSales_DailySalesHistory_Result, CounterInvoiceWiseDailySaleModel>(objSP_Report_CounterInvoiceWiseSales_DailySalesHistory_Result);
        //            lstCounterInvoiceWiseDailySaleModel.Add(_CounterInvoiceWiseDailySaleModel);
        //        }
        //    }
        //    return lstCounterInvoiceWiseDailySaleModel;
        //}

        public List<RegisterSaleTotalModel> RegisterSaleTotal(DateTime FromDate, DateTime ToDate)
        {
            var lstRegisterSaleTotal = new List<RegisterSaleTotalModel>();

            var onjSP_Rpt_RegisterSaleTotal = _db.SP_Rpt_RegisterSaleTotal(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_RegisterSaleTotal.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_RegisterSaleTotal_Result, RegisterSaleTotalModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_RegisterSaleTotal_Result objRegisterSaleTotal_Result in onjSP_Rpt_RegisterSaleTotal)
                {
                    RegisterSaleTotalModel _RegisterSaleTotalModel = iMapper.Map<SP_Rpt_RegisterSaleTotal_Result, RegisterSaleTotalModel>(objRegisterSaleTotal_Result);
                    lstRegisterSaleTotal.Add(_RegisterSaleTotalModel);
                }
            }
            return lstRegisterSaleTotal;
        }

        public List<RegisterSaleTotalByTransModel> RegisterSaleTotalByTrans(DateTime FromDate, DateTime ToDate)
        {
            var lstRegisterSaleTotalByTrans = new List<RegisterSaleTotalByTransModel>();

            var onjSP_Rpt_RegisterSaleTotalByTrans = _db.SP_Rpt_RegisterSaleTotalByTrans(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_RegisterSaleTotalByTrans.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_RegisterSaleTotalByTrans_Result, RegisterSaleTotalByTransModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_RegisterSaleTotalByTrans_Result objRegisterSaleTotalByTrans_Result in onjSP_Rpt_RegisterSaleTotalByTrans)
                {
                    RegisterSaleTotalByTransModel _RegisterSaleTotalByTransModel = iMapper.Map<SP_Rpt_RegisterSaleTotalByTrans_Result, RegisterSaleTotalByTransModel>(objRegisterSaleTotalByTrans_Result);
                    lstRegisterSaleTotalByTrans.Add(_RegisterSaleTotalByTransModel);
                }
            }
            return lstRegisterSaleTotalByTrans;
        }

        public List<RegisterSaleStatusByTransModel> RegisterSaleStatusByTrans(DateTime FromDate, DateTime ToDate)
        {
            var lstRegisterSaleStatusByTrans = new List<RegisterSaleStatusByTransModel>();

            var onjSP_Rpt_RegisterSaleStatusByTrans = _db.SP_Rpt_RegisterSaleStatusByTrans(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_RegisterSaleStatusByTrans.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_RegisterSaleStatusByTrans_Result, RegisterSaleStatusByTransModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_RegisterSaleStatusByTrans_Result objRegisterSaleStatusByTrans_Result in onjSP_Rpt_RegisterSaleStatusByTrans)
                {
                    RegisterSaleStatusByTransModel _RegisterSaleStatusByTransModel = iMapper.Map<SP_Rpt_RegisterSaleStatusByTrans_Result, RegisterSaleStatusByTransModel>(objRegisterSaleStatusByTrans_Result);
                    lstRegisterSaleStatusByTrans.Add(_RegisterSaleStatusByTransModel);
                }
            }
            return lstRegisterSaleStatusByTrans;
        }

        //public List<CounterWiseDailyPaymentHistoryModel> CounterWiseDailyPaymentHistory()
        //{
        //    var lstCounterInvoiceWiseDailySaleModel = new List<CounterWiseDailyPaymentHistoryModel>();

        //    var onjSP_Report_CounterInvoiceWiseSales_DailySalesHistory = _db.SP_Report_CounterWiseSales_DailyPaymentHistory(LoginInfo.StoreID).ToList();
        //    if (onjSP_Report_CounterInvoiceWiseSales_DailySalesHistory.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_CounterWiseSales_DailyPaymentHistory_Result, CounterWiseDailyPaymentHistoryModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (SP_Report_CounterWiseSales_DailyPaymentHistory_Result objSP_Report_CounterInvoiceWiseSales_DailySalesHistory_Result in onjSP_Report_CounterInvoiceWiseSales_DailySalesHistory)
        //        {
        //            CounterWiseDailyPaymentHistoryModel _CounterInvoiceWiseDailySaleModel = iMapper.Map<SP_Report_CounterWiseSales_DailyPaymentHistory_Result, CounterWiseDailyPaymentHistoryModel>(objSP_Report_CounterInvoiceWiseSales_DailySalesHistory_Result);
        //            lstCounterInvoiceWiseDailySaleModel.Add(_CounterInvoiceWiseDailySaleModel);
        //        }
        //    }
        //    return lstCounterInvoiceWiseDailySaleModel;
        //}

        public List<RegisterSaleStatusModel> RegisterSaleStatus(DateTime FromDate, DateTime ToDate)
        {
            var lstRegisterSaleStatus = new List<RegisterSaleStatusModel>();

            var onjSP_Rpt_RegisterSaleStatus = _db.SP_Rpt_RegisterSaleStatus(LoginInfo.StoreID, FromDate, ToDate).ToList();
            if (onjSP_Rpt_RegisterSaleStatus.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_RegisterSaleStatus_Result, RegisterSaleStatusModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_Rpt_RegisterSaleStatus_Result objRegisterSaleStatus_Result in onjSP_Rpt_RegisterSaleStatus)
                {
                    RegisterSaleStatusModel _RegisterSaleStatusModel = iMapper.Map<SP_Rpt_RegisterSaleStatus_Result, RegisterSaleStatusModel>(objRegisterSaleStatus_Result);
                    lstRegisterSaleStatus.Add(_RegisterSaleStatusModel);
                }
            }
            return lstRegisterSaleStatus;
        }
    }
}
