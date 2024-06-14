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
    public class ProductSalesPriceDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        //ADD // EDIT // DELETE
        public ProductSalesPriceMasterModel AddEditDeleteProductSalesPrice(ProductSalesPriceMasterModel objProductSalesPriceMasterModel, int TransType)
        {
            try
            {
                tbl_ProductSalePriceMaster objtbl_ProductSalePriceMaster = new tbl_ProductSalePriceMaster();
                if (TransType == 1)//ADD
                {
                    objProductSalesPriceMasterModel.ProductSalePriceID = objProductSalesPriceMasterModel.ProductSalePriceID;
                    objtbl_ProductSalePriceMaster.ProductID = objProductSalesPriceMasterModel.ProductID;
                    objtbl_ProductSalePriceMaster.SellPrice = objProductSalesPriceMasterModel.SellPrice;
                    objtbl_ProductSalePriceMaster.StartDate = objProductSalesPriceMasterModel.StartDate;
                    objtbl_ProductSalePriceMaster.EndDate = objProductSalesPriceMasterModel.EndDate;
                    objtbl_ProductSalePriceMaster.IsActive = true;
                    objtbl_ProductSalePriceMaster.IsDelete = false;
                    objtbl_ProductSalePriceMaster.CreatedDate = DateTime.Now;
                    objtbl_ProductSalePriceMaster.CreatedBy = LoginInfo.UserId;
                    objProductSalesPriceMasterModel.ProductSalePriceID = objtbl_ProductSalePriceMaster.ProductSalePriceID;
                    _db.tbl_ProductSalePriceMaster.Add(objtbl_ProductSalePriceMaster);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_ProductSalePriceMaster = _db.tbl_ProductSalePriceMaster.Where(p => p.ProductSalePriceID == objProductSalesPriceMasterModel.ProductSalePriceID).FirstOrDefault();
                    if (objtbl_ProductSalePriceMaster != null)
                    {
                        objtbl_ProductSalePriceMaster.ProductSalePriceID = objProductSalesPriceMasterModel.ProductSalePriceID;
                        objtbl_ProductSalePriceMaster.ProductID = objProductSalesPriceMasterModel.ProductID;
                        objtbl_ProductSalePriceMaster.SellPrice = objProductSalesPriceMasterModel.SellPrice;
                        objtbl_ProductSalePriceMaster.StartDate = objProductSalesPriceMasterModel.StartDate;
                        objtbl_ProductSalePriceMaster.EndDate = objProductSalesPriceMasterModel.EndDate;
                        objtbl_ProductSalePriceMaster.UpdatedDate = DateTime.Now;
                        objtbl_ProductSalePriceMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_ProductSalePriceMaster = _db.tbl_ProductSalePriceMaster.Where(p => p.ProductSalePriceID == objProductSalesPriceMasterModel.ProductSalePriceID).FirstOrDefault();
                    if (objtbl_ProductSalePriceMaster != null)
                    {
                        objtbl_ProductSalePriceMaster.ProductSalePriceID = objProductSalesPriceMasterModel.ProductSalePriceID;
                        objtbl_ProductSalePriceMaster.IsDelete = true;
                        objtbl_ProductSalePriceMaster.UpdatedDate = DateTime.Now;
                        objtbl_ProductSalePriceMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objProductSalesPriceMasterModel;
        }

        public List<ProductSalesPriceMasterModel> GetAllProductSalesPrice(long ProductID)
        {
            var lstProductSalesPriceMasterModel = new List<ProductSalesPriceMasterModel>();

            //var onjtbl_ProductVendor = _db.tbl_ProductSalePriceMaster.Where(x => x.IsDelete == false).ToList();
            var onjtbl_ProductVendor = _db.SP_GetProductSalePriceList(ProductID).ToList();

            if (onjtbl_ProductVendor.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetProductSalePriceList_Result, ProductSalesPriceMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetProductSalePriceList_Result objtbl_ProductVendor in onjtbl_ProductVendor)
                {
                    ProductSalesPriceMasterModel _ProductSalesPriceMasterModel = iMapper.Map<SP_GetProductSalePriceList_Result, ProductSalesPriceMasterModel>(objtbl_ProductVendor);
                    lstProductSalesPriceMasterModel.Add(_ProductSalesPriceMasterModel);
                }
            }
            return lstProductSalesPriceMasterModel;
        }

        // Check
        public bool CheckUniqueDate(DateTime StartDate, DateTime EndDate,long PrimaryID)
        {
            bool result = false;
            try
            {
                tbl_ProductSalePriceMaster _tbl_ProductSalePriceMaster = _db.tbl_ProductSalePriceMaster.FirstOrDefault(x => x.IsDelete == false && x.StartDate == StartDate && x.EndDate == EndDate && x.ProductSalePriceID != PrimaryID);
                if (_tbl_ProductSalePriceMaster != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        public bool CheckName(long ProductID, decimal Discount, DateTime StartDate, DateTime EndDate)
        {
            bool result = false;
            try
            {
                tbl_ProductSalePriceMaster _tbl_ProductSalePriceMaster = _db.tbl_ProductSalePriceMaster.FirstOrDefault(x => x.IsDelete == false && x.StartDate == StartDate && x.EndDate == EndDate && x.ProductID == ProductID && x.SellPrice == Discount);
                if (_tbl_ProductSalePriceMaster != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        //Get Details
        //public ProductSalesPriceMasterModel ProductSalesPricedetail(int ProductSalePriceID)
        //{
        //    tbl_ProductSalePriceMaster _tbl_ProductSalesPrice = new tbl_ProductSalePriceMaster();
        //    ProductSalesPriceMasterModel _ProductSalesPriceMasterModel = new ProductSalesPriceMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductSalePriceMaster, ProductSalesPriceMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _ProductSalesPriceMasterModel = iMapper.Map<tbl_ProductSalePriceMaster, ProductSalesPriceMasterModel>(_tbl_ProductSalesPrice);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _ProductSalesPriceMasterModel;
        //}
    }
}
