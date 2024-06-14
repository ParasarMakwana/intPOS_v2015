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
    public class ProductVendorDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        //ADD // EDIT // DELETE
        public ProductVendorMasterModel AddEditDeleteProductVendor(ProductVendorMasterModel objProductVendorMasterModel, int TransType)
        {
            try
            {
                tbl_ProductVendorMaster objtbl_ProductVendorMaster = new tbl_ProductVendorMaster();
                tbl_ProductMaster objtbl_ProductMaster = new tbl_ProductMaster();
                if (TransType == 1)//ADD
                {
                    objProductVendorMasterModel.ProductVendorID = objProductVendorMasterModel.ProductVendorID;
                    objtbl_ProductVendorMaster.ProductID = objProductVendorMasterModel.ProductID;
                    objtbl_ProductVendorMaster.ItemCode = objProductVendorMasterModel.ItemCode;
                    objtbl_ProductVendorMaster.VendorID = objProductVendorMasterModel.VendorID;
                    objtbl_ProductVendorMaster.UnitCost = objProductVendorMasterModel.UnitCost;
                    objtbl_ProductVendorMaster.IsActive = true;
                    objtbl_ProductVendorMaster.IsDelete = false;
                    objtbl_ProductVendorMaster.IsDefault = objProductVendorMasterModel.IsDefault;
                    objtbl_ProductVendorMaster.CreatedDate = DateTime.Now;
                    objtbl_ProductVendorMaster.CreatedBy = LoginInfo.UserId;
                    objProductVendorMasterModel.ProductVendorID = objtbl_ProductVendorMaster.ProductVendorID;
                    _db.tbl_ProductVendorMaster.Add(objtbl_ProductVendorMaster);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_ProductVendorMaster = _db.tbl_ProductVendorMaster.Where(p => p.ProductVendorID == objProductVendorMasterModel.ProductVendorID).FirstOrDefault();
                    if (objtbl_ProductVendorMaster != null)
                    {
                        objtbl_ProductVendorMaster.ProductVendorID = objProductVendorMasterModel.ProductVendorID;
                        objtbl_ProductVendorMaster.ItemCode = objProductVendorMasterModel.ItemCode;
                        objtbl_ProductVendorMaster.UnitCost = objProductVendorMasterModel.UnitCost;
                        objtbl_ProductVendorMaster.VendorID = objProductVendorMasterModel.VendorID;
                        objtbl_ProductVendorMaster.IsActive = true;
                        objtbl_ProductVendorMaster.IsDelete = false;
                        objtbl_ProductVendorMaster.IsDefault = objProductVendorMasterModel.IsDefault;
                        objtbl_ProductVendorMaster.UpdatedDate = DateTime.Now;
                        objtbl_ProductVendorMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_ProductVendorMaster = _db.tbl_ProductVendorMaster.Where(p => p.ProductVendorID == objProductVendorMasterModel.ProductVendorID).FirstOrDefault();
                    if (objtbl_ProductVendorMaster != null)
                    {
                        objtbl_ProductVendorMaster.ProductVendorID = objProductVendorMasterModel.ProductVendorID;
                        objtbl_ProductVendorMaster.IsDelete = true;
                        objtbl_ProductVendorMaster.IsActive = false;

                        objtbl_ProductVendorMaster.UpdatedDate = DateTime.Now;
                        objtbl_ProductVendorMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 4)
                {
                    objtbl_ProductVendorMaster = _db.tbl_ProductVendorMaster.Where(p => p.ProductVendorID == objProductVendorMasterModel.ProductVendorID).FirstOrDefault();
                    objtbl_ProductMaster = _db.tbl_ProductMaster.Where(p => p.ProductID == objtbl_ProductVendorMaster.ProductID).FirstOrDefault();
                    if (objtbl_ProductVendorMaster != null)
                    {
                        objtbl_ProductVendorMaster.ProductVendorID = objProductVendorMasterModel.ProductVendorID;
                        objtbl_ProductVendorMaster.IsDefault = objProductVendorMasterModel.IsDefault;
                        objtbl_ProductVendorMaster.ItemCode = objProductVendorMasterModel.ItemCode;
                        objtbl_ProductVendorMaster.UnitCost = objProductVendorMasterModel.UnitCost;
                        objtbl_ProductMaster.CertCode = objProductVendorMasterModel.ItemCode;
                        objtbl_ProductMaster.UnitCost = objProductVendorMasterModel.UnitCost;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objProductVendorMasterModel;
        }

        public List<ProductVendorMasterModel> GetAllProductVendor(long ProductID)
        {
            var lstProductVendorMasterModel = new List<ProductVendorMasterModel>();

            var onjtbl_ProductVendor = _db.SP_GetProductVendorList(ProductID).ToList();

            if (onjtbl_ProductVendor.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetProductVendorList_Result, ProductVendorMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetProductVendorList_Result objtbl_ProductVendor in onjtbl_ProductVendor)
                {
                    ProductVendorMasterModel _ProductVendorMasterModel = iMapper.Map<SP_GetProductVendorList_Result, ProductVendorMasterModel>(objtbl_ProductVendor);
                    lstProductVendorMasterModel.Add(_ProductVendorMasterModel);
                }
            }
            return lstProductVendorMasterModel;
        }

        public bool CheckProductVendorName(long ProductID, long VendorID)
        {
            bool result = false;
            try
            {
                tbl_ProductVendorMaster _tbl_ProductVendor = _db.tbl_ProductVendorMaster.FirstOrDefault(x => x.IsDelete == false && x.VendorID == VendorID && x.ProductID == ProductID);
                if (_tbl_ProductVendor != null)
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

        public string CheckProductItemCode(string itemCode, long ProductID, long VendorID)
        {
            string result = "";
            try
            {
                tbl_ProductVendorMaster _tbl_ProductVendor = _db.tbl_ProductVendorMaster.FirstOrDefault(x => x.IsDelete == false && x.ItemCode == itemCode && x.ProductID == ProductID && x.VendorID == VendorID);
                if (_tbl_ProductVendor != null)
                {
                    result = _tbl_ProductVendor.ItemCode;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        public bool CheckName(string ItemCode)
        {
            bool result = false;
            try
            {
                tbl_ProductVendorMaster _tbl_ProductVendor = _db.tbl_ProductVendorMaster.FirstOrDefault(x => x.ItemCode == ItemCode && x.IsDelete == false);
                if (_tbl_ProductVendor != null)
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
        ////Get Details
        //public ProductVendorMasterModel ProductVendorDetail(int ProductVendorID)
        //{
        //    tbl_ProductVendorMaster _tbl_ProductVendor = new tbl_ProductVendorMaster();
        //    ProductVendorMasterModel _ProductVendorMasterModel = new ProductVendorMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductVendorMaster, ProductVendorMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _ProductVendorMasterModel = iMapper.Map<tbl_ProductVendorMaster, ProductVendorMasterModel>(_tbl_ProductVendor);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _ProductVendorMasterModel;
        //}
    }
}
