using SFPOS.DAL.Reports;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.ReportServices
{
    public class RegisterSaleService
    {
        RegisterSaleReportDAL objCounterWiseReportDAL = new RegisterSaleReportDAL();
        //public List<CounterWiseDailySaleModel> CounterWiseDailySale()
        //{
        //    return objCounterWiseReportDAL.CounterWiseDailySale();
        //}

        //public List<CounterInvoiceWiseDailySaleModel> CounterInvoiceWiseDailySale()
        //{
        //    return objCounterWiseReportDAL.CounterInvoiceWiseDailySale();
        //}

        public List<RegisterSaleTotalModel> RegisterSaleTotal(DateTime FromDate, DateTime ToDate)
        {
            return objCounterWiseReportDAL.RegisterSaleTotal(FromDate, ToDate);
        }

        public List<RegisterSaleTotalByTransModel> RegisterSaleTotalByTrans(DateTime FromDate, DateTime ToDate)
        {
            return objCounterWiseReportDAL.RegisterSaleTotalByTrans(FromDate, ToDate);
        }

        //public List<CounterInvoiceWiseDailyPaymentHistoryModel> CounterInvoiceWiseDailyPaymentHistory()
        //{
        //    return objCounterWiseReportDAL.CounterInvoiceWiseDailyPaymentHistory();
        //}

        public List<RegisterSaleStatusByTransModel> RegisterSaleStatusByTrans(DateTime FromDate, DateTime ToDate)
        {
            return objCounterWiseReportDAL.RegisterSaleStatusByTrans(FromDate, ToDate);
        }

        //public List<CounterWiseDailyPaymentHistoryModel> CounterWiseDailyPaymentHistory()
        //{
        //    return objCounterWiseReportDAL.CounterWiseDailyPaymentHistory();
        //}

        public List<RegisterSaleStatusModel> RegisterSaleStatus(DateTime FromDate, DateTime ToDate)
        {
            return objCounterWiseReportDAL.RegisterSaleStatus(FromDate, ToDate);
        }

    }
}
