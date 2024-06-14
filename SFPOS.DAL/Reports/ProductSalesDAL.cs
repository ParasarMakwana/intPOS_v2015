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
    public class ProductSalesDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public List<ProductSale_ResultModel> GetProductDetails(DateTime StartDate , DateTime EndDate , long DepartmentID , long SectionID, long VenderID,int Flag)
        {
            var lstSP_ProductSale_ResultModel = new List<ProductSale_ResultModel>();
            var lstProductSaleDetails = _db.SP_GetProductwiseSalesList(LoginInfo.StoreID,StartDate,EndDate,DepartmentID,SectionID, VenderID, Flag).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetProductwiseSalesList_Result, ProductSale_ResultModel>(); });
            IMapper iMapper = config.CreateMapper();
            
            foreach (SP_GetProductwiseSalesList_Result objGetProductSaleDetails_Result in lstProductSaleDetails)
            {
                ProductSale_ResultModel _SP_ProductSale_ResultModel = iMapper.Map<SP_GetProductwiseSalesList_Result, ProductSale_ResultModel>(objGetProductSaleDetails_Result);
                lstSP_ProductSale_ResultModel.Add(_SP_ProductSale_ResultModel);
            }
            return lstSP_ProductSale_ResultModel;
        }

        //public List<ProductWiseDailySaleModel> ProductWiseDailySale()
        //{
        //    var lstProductWiseDailySaleModel = new List<ProductWiseDailySaleModel>();
        //    var lsSP_Report_ProductWiseSales_DailySalesHistory = _db.SP_Report_ProductWiseSales_DailySalesHistory(LoginInfo.StoreID).ToList();

        //    var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Report_ProductWiseSales_DailySalesHistory_Result, ProductWiseDailySaleModel>(); });
        //    IMapper iMapper = config.CreateMapper();


        //    foreach (SP_Report_ProductWiseSales_DailySalesHistory_Result objGetProductSaleDetails_Result in lsSP_Report_ProductWiseSales_DailySalesHistory)
        //    {
        //        ProductWiseDailySaleModel _SP_ProductSale_ResultModel = iMapper.Map<SP_Report_ProductWiseSales_DailySalesHistory_Result, ProductWiseDailySaleModel>(objGetProductSaleDetails_Result);
        //        lstProductWiseDailySaleModel.Add(_SP_ProductSale_ResultModel);
        //    }
        //    return lstProductWiseDailySaleModel;
        //}

        public List<ProductWiseSaleHistoryModel> ProductWiseSaleHistory(DateTime FromDate, DateTime ToDate)
        {
            var lstProductWiseSaleHistoryModel = new List<ProductWiseSaleHistoryModel>();
            var lsSP_Rpt_ProductWiseSaleHistory = _db.SP_Rpt_ProductWiseSaleHistory(LoginInfo.StoreID, FromDate, ToDate).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_Rpt_ProductWiseSaleHistory_Result, ProductWiseSaleHistoryModel>(); });
            IMapper iMapper = config.CreateMapper();


            foreach (SP_Rpt_ProductWiseSaleHistory_Result objProductWiseSaleHistory_Result in lsSP_Rpt_ProductWiseSaleHistory)
            {
                ProductWiseSaleHistoryModel _SP_ProductSale_ResultModel = iMapper.Map<SP_Rpt_ProductWiseSaleHistory_Result, ProductWiseSaleHistoryModel>(objProductWiseSaleHistory_Result);
                lstProductWiseSaleHistoryModel.Add(_SP_ProductSale_ResultModel);
            }
            return lstProductWiseSaleHistoryModel;
        }
    }
}
