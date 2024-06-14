using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class ProductDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        //ADD // EDIT // DELETE
        public ProductMasterModel AddEditDeleteProduct(ProductMasterModel objProductMasterModel, int TransType)
        {
            try
            {
                tbl_ProductMaster objtbl_ProductMaster = new tbl_ProductMaster();
                if (TransType == 1)//ADD
                {
                    objProductMasterModel.ProductID = 0;
                    objtbl_ProductMaster.UnitMeasureID = objProductMasterModel.UnitMeasureID;
                    objtbl_ProductMaster.DepartmentID = objProductMasterModel.DepartmentID;
                    objtbl_ProductMaster.SectionID = objProductMasterModel.SectionID;
                    objtbl_ProductMaster.TaxGroupID = objProductMasterModel.TaxGroupID;
                    objtbl_ProductMaster.ProductName = objProductMasterModel.ProductName;
                    objtbl_ProductMaster.UPCCode = objProductMasterModel.UPCCode;
                    objtbl_ProductMaster.CertCode = objProductMasterModel.CertCode;
                    objtbl_ProductMaster.Price = objProductMasterModel.Price;
                    objtbl_ProductMaster.Image = objProductMasterModel.Image;
                    objtbl_ProductMaster.IsActive = objProductMasterModel.IsActive;
                    objtbl_ProductMaster.IsDelete = false;
                    objtbl_ProductMaster.CreatedDate = DateTime.Now;
                    objtbl_ProductMaster.CreatedBy = LoginInfo.UserId;
                    objtbl_ProductMaster.IsFoodStamp = objProductMasterModel.IsFoodStamp;
                    objtbl_ProductMaster.IsGroupPrice = objProductMasterModel.IsGroupPrice;
                    objtbl_ProductMaster.GroupPrice = objProductMasterModel.GroupPrice;
                    objtbl_ProductMaster.GroupQty = objProductMasterModel.GroupQty;
                    objtbl_ProductMaster.CaseQty = objProductMasterModel.CaseQty;
                    objtbl_ProductMaster.CasePrice = objProductMasterModel.CasePrice;
                    objtbl_ProductMaster.TareWeight = objProductMasterModel.TareWeight;
                    objtbl_ProductMaster.LinkedUPCCode = objProductMasterModel.LinkedUPCCode;
                    objtbl_ProductMaster.AgeVerification = objProductMasterModel.AgeVerification;
                    objtbl_ProductMaster.IsScaled = objProductMasterModel.IsScaled;
                    objtbl_ProductMaster.LabeledPrice = objProductMasterModel.LabeledPrice;
                    objtbl_ProductMaster.UnitCost = objProductMasterModel.UnitCost;
                    objtbl_ProductMaster.UpdatedDate = objProductMasterModel.UpdatedDate;
                    objtbl_ProductMaster.ProductVendorID = objProductMasterModel.ProductVendorID;
                    objtbl_ProductMaster.Pack = objProductMasterModel.Pack;
                    objtbl_ProductMaster.Size = objProductMasterModel.Size;
                    objtbl_ProductMaster.SecondaryPLU = objProductMasterModel.SecondaryPLU;
                    objtbl_ProductMaster.PalletQTY = objProductMasterModel.PalletQTY;
                    objtbl_ProductMaster.CountryofOrigin = objProductMasterModel.CountryofOrigin;
                    objtbl_ProductMaster.FSEligibleAmount = objProductMasterModel.FSEligibleAmount;
                    _db.tbl_ProductMaster.Add(objtbl_ProductMaster);
                    objProductMasterModel.ProductID = objtbl_ProductMaster.ProductID;
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_ProductMaster = _db.tbl_ProductMaster.Where(p => p.ProductID == objProductMasterModel.ProductID).FirstOrDefault();
                    if (objtbl_ProductMaster != null)
                    {
                        objtbl_ProductMaster.ProductID = objProductMasterModel.ProductID;
                        objtbl_ProductMaster.UnitMeasureID = objProductMasterModel.UnitMeasureID;
                        objtbl_ProductMaster.DepartmentID = objProductMasterModel.DepartmentID;
                        objtbl_ProductMaster.SectionID = objProductMasterModel.SectionID;
                        objtbl_ProductMaster.TaxGroupID = objProductMasterModel.TaxGroupID;
                        objtbl_ProductMaster.ProductName = objProductMasterModel.ProductName;
                        objtbl_ProductMaster.Price = objProductMasterModel.Price;
                        objtbl_ProductMaster.UPCCode = objProductMasterModel.UPCCode;
                        objtbl_ProductMaster.CertCode = objProductMasterModel.CertCode;
                        objtbl_ProductMaster.Image = objProductMasterModel.Image;
                        objtbl_ProductMaster.IsActive = objProductMasterModel.IsActive;
                        objtbl_ProductMaster.IsFoodStamp = objProductMasterModel.IsFoodStamp;
                        objtbl_ProductMaster.IsGroupPrice = objProductMasterModel.IsGroupPrice;
                        objtbl_ProductMaster.GroupPrice = objProductMasterModel.GroupPrice;
                        objtbl_ProductMaster.GroupQty = objProductMasterModel.GroupQty;
                        objtbl_ProductMaster.CaseQty = objProductMasterModel.CaseQty;
                        objtbl_ProductMaster.CasePrice = objProductMasterModel.CasePrice;
                        objtbl_ProductMaster.TareWeight = objProductMasterModel.TareWeight;
                        objtbl_ProductMaster.LinkedUPCCode = objProductMasterModel.LinkedUPCCode;
                        objtbl_ProductMaster.AgeVerification = objProductMasterModel.AgeVerification;
                        objtbl_ProductMaster.IsScaled = objProductMasterModel.IsScaled;
                        objtbl_ProductMaster.LabeledPrice = objProductMasterModel.LabeledPrice;
                        objtbl_ProductMaster.UnitCost = objProductMasterModel.UnitCost;
                        objtbl_ProductMaster.UpdatedDate = objProductMasterModel.UpdatedDate;
                        objtbl_ProductMaster.ProductVendorID = objProductMasterModel.ProductVendorID;
                        objtbl_ProductMaster.Pack = objProductMasterModel.Pack;
                        objtbl_ProductMaster.Size = objProductMasterModel.Size;
                        objtbl_ProductMaster.SecondaryPLU = objProductMasterModel.SecondaryPLU;
                        objtbl_ProductMaster.PalletQTY = objProductMasterModel.PalletQTY;
                        objtbl_ProductMaster.CountryofOrigin = objProductMasterModel.CountryofOrigin;
                        objtbl_ProductMaster.FSEligibleAmount = objProductMasterModel.FSEligibleAmount;
                        objtbl_ProductMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_ProductMaster = _db.tbl_ProductMaster.Where(p => p.ProductID == objProductMasterModel.ProductID).FirstOrDefault();
                    if (objtbl_ProductMaster != null)
                    {
                        objtbl_ProductMaster.ProductID = objProductMasterModel.ProductID;
                        //objtbl_ProductMaster.ProductName = objProductMasterModel.ProductName;
                        objtbl_ProductMaster.IsDelete = true;
                        objtbl_ProductMaster.UpdatedDate = DateTime.Now;
                        objtbl_ProductMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 4)//DELETE
                {
                    objtbl_ProductMaster = _db.tbl_ProductMaster.Where(p => p.ProductID == objProductMasterModel.ProductID).FirstOrDefault();
                    if (objtbl_ProductMaster != null)
                    {
                        objtbl_ProductMaster.ProductID = objProductMasterModel.ProductID;
                        objtbl_ProductMaster.IsDelete = false;
                        objtbl_ProductMaster.UpdatedDate = DateTime.Now;
                        objtbl_ProductMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
                objProductMasterModel.ProductID = objtbl_ProductMaster.ProductID;
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objProductMasterModel;
        }

        public List<ProductMasterModel> GetAllProduct(string SearchString ,bool IsDynamic)
        {
            var lstProductMasterModel = new List<ProductMasterModel>();

            //  var onjtbl_ProductMaster = _db.tbl_ProductMaster.Where(x => x.IsDelete == false).OrderBy(x => x.ProductName).ToList();
            var onjtbl_ProductMaster = _db.SP_GetProductList(SearchString,IsDynamic).ToList();

            if (onjtbl_ProductMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetProductList_Result, ProductMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetProductList_Result objtbl_ProductMaster in onjtbl_ProductMaster)
                {
                    ProductMasterModel _ProductMasterModel = iMapper.Map<SP_GetProductList_Result, ProductMasterModel>(objtbl_ProductMaster);
                    lstProductMasterModel.Add(_ProductMasterModel);
                }
            }
            return lstProductMasterModel;
        }

       
        //Check
        public bool CheckProductDetails(string ProductName, string UPCCode, long ProductID)
        {
            bool result = false;
            try
            {
                tbl_ProductMaster _tbl_ProductMaster = _db.tbl_ProductMaster.FirstOrDefault(x => x.UPCCode == UPCCode && x.IsDelete == false && x.ProductID != ProductID);
                if (_tbl_ProductMaster != null)
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

        public ProductMasterModel GetArchivedUPCCode(string UPCCode)
        {
            tbl_ProductMaster _tbl_ProductMaster = new tbl_ProductMaster();
            ProductMasterModel _ProductMasterModel = new ProductMasterModel();
            _tbl_ProductMaster = _db.tbl_ProductMaster.FirstOrDefault(x => x.UPCCode == UPCCode && x.IsDelete == true);
            try
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductMaster, ProductMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                _ProductMasterModel = iMapper.Map<tbl_ProductMaster, ProductMasterModel>(_tbl_ProductMaster);
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return _ProductMasterModel;
        }

        public ProductMasterModel DeleteAllProduct(ProductMasterModel objProductMasterModel)
        {
            tbl_ProductMaster objtbl_ProductMaster = new tbl_ProductMaster();
            objtbl_ProductMaster = _db.tbl_ProductMaster.Where(p => p.ProductID == objProductMasterModel.ProductID).FirstOrDefault();
            if (objtbl_ProductMaster != null)
            {               
                _db.Entry(objtbl_ProductMaster).State = EntityState.Deleted;
            }

            var objtbl_ProductVendor = _db.tbl_ProductVendorMaster.Where(p => p.ProductID == objProductMasterModel.ProductID && p.IsDelete == false).ToList();
            if (objtbl_ProductVendor.Count > 0)
            {
                foreach (tbl_ProductVendorMaster ojtbl_ProductVendor in objtbl_ProductVendor)
                {                   
                    _db.Entry(ojtbl_ProductVendor).State = EntityState.Deleted;
                }
            }

            var objtbl_ProductSalesPrice = _db.tbl_ProductSalePriceMaster.Where(p => p.ProductID == objProductMasterModel.ProductID && p.IsDelete == false).ToList();
            if (objtbl_ProductSalesPrice.Count > 0)
            {
                foreach (tbl_ProductSalePriceMaster ojtbl_ProductSalesPrice in objtbl_ProductSalesPrice)
                {
                    _db.Entry(ojtbl_ProductSalesPrice).State = EntityState.Deleted;
                }
            }
                       
            _db.SaveChanges();
            return objProductMasterModel;
        }

        public ProductMasterModel UndoDeleteAllProduct(ProductMasterModel objProductMasterModel)
        {
            tbl_ProductMaster objtbl_ProductMaster = new tbl_ProductMaster();
            objtbl_ProductMaster = _db.tbl_ProductMaster.Where(p => p.ProductID == objProductMasterModel.ProductID).FirstOrDefault();
            if (objtbl_ProductMaster != null)
            {
                _db.Entry(objtbl_ProductMaster).State = EntityState.Deleted;
            }

            var objtbl_ProductVendor = _db.tbl_ProductVendorMaster.Where(p => p.ProductID == objProductMasterModel.ProductID && p.IsDelete == true).ToList();
            if (objtbl_ProductVendor.Count > 0)
            {
                foreach (tbl_ProductVendorMaster ojtbl_ProductVendor in objtbl_ProductVendor)
                {
                    _db.Entry(ojtbl_ProductVendor).State = EntityState.Deleted;
                }
            }

            var objtbl_ProductSalesPrice = _db.tbl_ProductSalePriceMaster.Where(p => p.ProductID == objProductMasterModel.ProductID && p.IsDelete == true).ToList();
            if (objtbl_ProductSalesPrice.Count > 0)
            {
                foreach (tbl_ProductSalePriceMaster ojtbl_ProductSalesPrice in objtbl_ProductSalesPrice)
                {
                    _db.Entry(ojtbl_ProductSalesPrice).State = EntityState.Deleted;
                }
            }

            _db.SaveChanges();
            return objProductMasterModel;
        }

        public List<ProductMasterModel> GetVendorWiseProduct()
        {
            var lstProductMasterModel = new List<ProductMasterModel>();


            //SELECT* FROM[tbl_ProductMaster] where ProductID in
            //(SELECT ProductID from[tbl_ProductVendorMaster] where VendoreID in
            //(SELECT VendorID FROM[tbl_PurchaseHeader] where PurchaseOrderID = 1))

            var onjtbl_ProductMaster = _db.tbl_ProductMaster.Where(x => x.IsDelete == false).OrderBy(x => x.ProductName).ToList();

            if (onjtbl_ProductMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductMaster, ProductMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_ProductMaster objtbl_ProductMaster in onjtbl_ProductMaster)
                {
                    ProductMasterModel _ProductMasterModel = iMapper.Map<tbl_ProductMaster, ProductMasterModel>(objtbl_ProductMaster);
                    lstProductMasterModel.Add(_ProductMasterModel);
                }
            }
            return lstProductMasterModel;
        }

        public bool CheckName(string ProductName)
        {
            bool result = false;
            try
            {
                tbl_ProductMaster _tbl_ProductMaster = _db.tbl_ProductMaster.FirstOrDefault(x => x.ProductName == ProductName && x.IsDelete == false);
                if (_tbl_ProductMaster != null)
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

        public long GetProductID(string ProductName)
        {
            long result = 0;
            try
            {
                tbl_ProductMaster _tbl_ProductMaster = _db.tbl_ProductMaster.FirstOrDefault(x => x.ProductName == ProductName && x.IsDelete == false);
                if (_tbl_ProductMaster != null)
                {
                    result = _tbl_ProductMaster.ProductID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        public string GetUPCCode(string UPCCode)
        {
            string result = "";
            try
            {
                tbl_ProductMaster _tbl_ProductMaster = _db.tbl_ProductMaster.FirstOrDefault(x => x.UPCCode == UPCCode);
                if (_tbl_ProductMaster != null)
                {
                    result = _tbl_ProductMaster.UPCCode;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }



        //Get Details
        public ProductMasterModel ProductDetail(long ProductUoMID)
        {
            tbl_ProductMaster _tbl_ProductMaster = new tbl_ProductMaster();
            ProductMasterModel _ProductMasterModel = new ProductMasterModel();
            _tbl_ProductMaster= _db.tbl_ProductMaster.FirstOrDefault(x => x.ProductID == ProductUoMID && x.IsActive==true);
            try
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductMaster, ProductMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                _ProductMasterModel = iMapper.Map<tbl_ProductMaster, ProductMasterModel>(_tbl_ProductMaster);
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return _ProductMasterModel;
        }
        /// <summary>
        /// Created by Vishal Chavan
        /// </summary>
        /// <param name="SearchString"></param>
        /// <param name="pageNo"></param>
        /// <returns>List<ProductMasterModel> of products</returns>

        public List<ProductMasterModel> GetAllProduct_With_Paging(string SearchString, int pageNo)
        {
            var lstProductMasterModel = new List<ProductMasterModel>();

            //  var onjtbl_ProductMaster = _db.tbl_ProductMaster.Where(x => x.IsDelete == false).OrderBy(x => x.ProductName).ToList();
            var onjtbl_ProductMaster = _db.SP_GetProductList_With_Paging(SearchString, pageNo).ToList();

            if (onjtbl_ProductMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetProductList_With_Paging_Result, ProductMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetProductList_With_Paging_Result objtbl_ProductMaster in onjtbl_ProductMaster)
                {
                    ProductMasterModel _ProductMasterModel = iMapper.Map<SP_GetProductList_With_Paging_Result, ProductMasterModel>(objtbl_ProductMaster);
                    lstProductMasterModel.Add(_ProductMasterModel);
                }
            }
            return lstProductMasterModel;
        }



    }
}
