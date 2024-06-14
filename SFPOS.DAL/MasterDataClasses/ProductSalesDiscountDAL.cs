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
    public class ProductSalesDiscountDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        //ADD // EDIT // DELETE
        public ProductSalesDiscountMasterModel AddEditDeleteProductSalesDiscount(ProductSalesDiscountMasterModel objProductSalesDiscountMasterModel, int TransType)
        {
            try
            {
                tbl_ProductSaleDiscountMaster objtbl_ProductSaleDiscountMaster = new tbl_ProductSaleDiscountMaster();
                if (TransType == 1)//ADD
                {
                    objProductSalesDiscountMasterModel.ProductSaleDiscountID = objProductSalesDiscountMasterModel.ProductSaleDiscountID;
                    objtbl_ProductSaleDiscountMaster.ProductID = objProductSalesDiscountMasterModel.ProductID;
                    objtbl_ProductSaleDiscountMaster.Discount = Convert.ToDecimal(objProductSalesDiscountMasterModel.Discount);
                    objtbl_ProductSaleDiscountMaster.StartDate = objProductSalesDiscountMasterModel.StartDate;
                    objtbl_ProductSaleDiscountMaster.EndDate = objProductSalesDiscountMasterModel.EndDate;
                    objtbl_ProductSaleDiscountMaster.IsActive = true;
                    objtbl_ProductSaleDiscountMaster.IsDelete = false;
                    objtbl_ProductSaleDiscountMaster.CreatedDate = DateTime.Now;
                    objtbl_ProductSaleDiscountMaster.CreatedBy = LoginInfo.UserId;
                    objProductSalesDiscountMasterModel.ProductSaleDiscountID = objtbl_ProductSaleDiscountMaster.ProductSaleDiscountID;
                    _db.tbl_ProductSaleDiscountMaster.Add(objtbl_ProductSaleDiscountMaster);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_ProductSaleDiscountMaster = _db.tbl_ProductSaleDiscountMaster.Where(p => p.ProductSaleDiscountID == objProductSalesDiscountMasterModel.ProductSaleDiscountID).FirstOrDefault();
                    if (objtbl_ProductSaleDiscountMaster != null)
                    {
                        objtbl_ProductSaleDiscountMaster.ProductSaleDiscountID = objProductSalesDiscountMasterModel.ProductSaleDiscountID;
                        objtbl_ProductSaleDiscountMaster.ProductID = objProductSalesDiscountMasterModel.ProductID;
                        objtbl_ProductSaleDiscountMaster.Discount = Convert.ToDecimal(objProductSalesDiscountMasterModel.Discount);
                        objtbl_ProductSaleDiscountMaster.StartDate = objProductSalesDiscountMasterModel.StartDate;
                        objtbl_ProductSaleDiscountMaster.EndDate = objProductSalesDiscountMasterModel.EndDate;
                        objtbl_ProductSaleDiscountMaster.UpdatedDate = DateTime.Now;
                        objtbl_ProductSaleDiscountMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_ProductSaleDiscountMaster = _db.tbl_ProductSaleDiscountMaster.Where(p => p.ProductSaleDiscountID == objProductSalesDiscountMasterModel.ProductSaleDiscountID).FirstOrDefault();
                    if (objtbl_ProductSaleDiscountMaster != null)
                    {
                        objtbl_ProductSaleDiscountMaster.ProductSaleDiscountID = objProductSalesDiscountMasterModel.ProductSaleDiscountID;
                        objtbl_ProductSaleDiscountMaster.IsDelete = true;
                        objtbl_ProductSaleDiscountMaster.IsActive = false;
                        objtbl_ProductSaleDiscountMaster.UpdatedDate = DateTime.Now;
                        objtbl_ProductSaleDiscountMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objProductSalesDiscountMasterModel;
        }

        public List<ProductSalesDiscountMasterModel> GetAllProductSalesDiscount(long ProductID)
        {
            var lstProductSalesDiscountMasterModel = new List<ProductSalesDiscountMasterModel>();

            //var onjtbl_ProductVendor = _db.tbl_ProductSaleDiscountMaster.Where(x => x.IsDelete == false).ToList();
            var onjtbl_ProductVendor = _db.SP_GetProductSaleDiscountList(ProductID).ToList();


            if (onjtbl_ProductVendor.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetProductSaleDiscountList_Result, ProductSalesDiscountMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetProductSaleDiscountList_Result objtbl_ProductSalesDisc in onjtbl_ProductVendor)
                {
                    ProductSalesDiscountMasterModel _ProductSalesDiscountMasterModel = iMapper.Map<SP_GetProductSaleDiscountList_Result, ProductSalesDiscountMasterModel>(objtbl_ProductSalesDisc);
                    lstProductSalesDiscountMasterModel.Add(_ProductSalesDiscountMasterModel);
                }
            }
            return lstProductSalesDiscountMasterModel;
        }

        // Check
        public bool CheckUniqueDate(DateTime StartDate, DateTime EndDate, long PrimaryId)
        {
            bool result = false;
            try
            {
                tbl_ProductSaleDiscountMaster _tbl_ProductSaleDiscountMaster = _db.tbl_ProductSaleDiscountMaster.FirstOrDefault(x => x.IsDelete == false && x.StartDate == StartDate && x.EndDate == EndDate && x.ProductSaleDiscountID != PrimaryId);
                if (_tbl_ProductSaleDiscountMaster != null)
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
                tbl_ProductSaleDiscountMaster _tbl_ProductSaleDiscountMaster = _db.tbl_ProductSaleDiscountMaster.FirstOrDefault(x => x.IsDelete == false && x.StartDate == StartDate && x.EndDate == EndDate && x.ProductID == ProductID && x.Discount == Discount);
                if (_tbl_ProductSaleDiscountMaster != null)
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
        //public ProductSalesDiscountMasterModel ProductSalesDiscountdetail(int ProductSalePriceID)
        //{
        //    tbl_ProductSaleDiscountMaster _tbl_ProductSalesDiscount = new tbl_ProductSaleDiscountMaster();
        //    ProductSalesDiscountMasterModel _ProductSalesDiscountMasterModel = new ProductSalesDiscountMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductSaleDiscountMaster, ProductSalesDiscountMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _ProductSalesDiscountMasterModel = iMapper.Map<tbl_ProductSaleDiscountMaster, ProductSalesDiscountMasterModel>(_tbl_ProductSalesDiscount);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _ProductSalesDiscountMasterModel;
        //}
    }
}
