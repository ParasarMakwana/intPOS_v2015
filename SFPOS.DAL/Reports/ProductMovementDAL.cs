using AutoMapper;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.Reports
{
    public class ProductMovementDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public List<ProductMovement_ResultModel> GetProductDetails(string UPCCode, int flag )
        {
            var lstProductMovement_ResultModel = new List<ProductMovement_ResultModel>();
            var lstProductMovementDetails = _db.SP_GetProductMovementList(LoginInfo.StoreID,UPCCode, flag).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetProductMovementList_Result, ProductMovement_ResultModel>(); });
            IMapper iMapper = config.CreateMapper();


            foreach (SP_GetProductMovementList_Result objGetProductMovementDetails_Result in lstProductMovementDetails)
            {
                ProductMovement_ResultModel _SP_ProductMovement_ResultModel = iMapper.Map<SP_GetProductMovementList_Result, ProductMovement_ResultModel>(objGetProductMovementDetails_Result);
                lstProductMovement_ResultModel.Add(_SP_ProductMovement_ResultModel);
            }
            return lstProductMovement_ResultModel;
        }
        public List<ProductMovement_ResultModel> GetProductDetails(string UPCCode, DateTime STARTDATE, DateTime ENDDATE)
        {
            var lstProductMovement_ResultModel = new List<ProductMovement_ResultModel>();
            var lstProductMovementDetails = _db.SP_GetProductMovementProductDetailList(LoginInfo.StoreID, UPCCode, STARTDATE.Date, ENDDATE.Date).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetProductMovementProductDetailList_Result, ProductMovement_ResultModel>(); });
            IMapper iMapper = config.CreateMapper();


            foreach (SP_GetProductMovementProductDetailList_Result objGetProductMovementDetails_Result in lstProductMovementDetails)
            {
                ProductMovement_ResultModel _SP_ProductMovement_ResultModel = iMapper.Map<SP_GetProductMovementProductDetailList_Result, ProductMovement_ResultModel>(objGetProductMovementDetails_Result);
                lstProductMovement_ResultModel.Add(_SP_ProductMovement_ResultModel);
            }
            return lstProductMovement_ResultModel;
        }
    }
}
