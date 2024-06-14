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
    public class ProductUoMDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        //ADD // EDIT // DELETE
        public ProductUoMMasterModel AddEditDeleteProductUoM(ProductUoMMasterModel objProductUoMMasterModel, int TransType)
        {
            try
            {
                tbl_ProductUoM objtbl_ProductUoM = new tbl_ProductUoM();
                if (TransType == 1)//ADD
                {
                    objProductUoMMasterModel.ProductUoMID = objProductUoMMasterModel.ProductUoMID;
                    objtbl_ProductUoM.ProductID = objProductUoMMasterModel.ProductID;
                    objtbl_ProductUoM.UnitMeasureID = objProductUoMMasterModel.UnitMeasureID;
                    objtbl_ProductUoM.Discription = objProductUoMMasterModel.Discription;
                    objtbl_ProductUoM.QtyPerUoM = objProductUoMMasterModel.QtyPerUoM;
                    objtbl_ProductUoM.IsActive = true;
                    objtbl_ProductUoM.IsDelete = false;
                    objtbl_ProductUoM.CreatedDate = DateTime.Now;
                    objtbl_ProductUoM.CreatedBy = LoginInfo.UserId;
                    objProductUoMMasterModel.ProductUoMID = objtbl_ProductUoM.ProductUoMID;
                    _db.tbl_ProductUoM.Add(objtbl_ProductUoM);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_ProductUoM = _db.tbl_ProductUoM.Where(p => p.ProductUoMID == objProductUoMMasterModel.ProductUoMID).FirstOrDefault();
                    if (objtbl_ProductUoM != null)
                    {
                        objtbl_ProductUoM.ProductUoMID = objProductUoMMasterModel.ProductUoMID;
                        objtbl_ProductUoM.ProductID = objProductUoMMasterModel.ProductID;
                        objtbl_ProductUoM.UnitMeasureID = objProductUoMMasterModel.UnitMeasureID;
                        objtbl_ProductUoM.Discription = objProductUoMMasterModel.Discription;
                        objtbl_ProductUoM.QtyPerUoM = objProductUoMMasterModel.QtyPerUoM;
                        objtbl_ProductUoM.UpdatedDate = DateTime.Now;
                        objtbl_ProductUoM.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_ProductUoM = _db.tbl_ProductUoM.Where(p => p.ProductUoMID == objProductUoMMasterModel.ProductUoMID).FirstOrDefault();
                    if (objtbl_ProductUoM != null)
                    {
                        objtbl_ProductUoM.ProductUoMID = objProductUoMMasterModel.ProductUoMID;
                        objtbl_ProductUoM.IsDelete = true;
                        objtbl_ProductUoM.UpdatedDate = DateTime.Now;
                        objtbl_ProductUoM.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objProductUoMMasterModel;
        }

        //public List<ProductUoMMasterModel> GetAllProductUoM()
        //{
        //    var lstProductUoMMasterModel = new List<ProductUoMMasterModel>();

        //    var onjtbl_ProductUoM = _db.tbl_ProductUoM.Where(x => x.IsDelete == false).ToList();

        //    if (onjtbl_ProductUoM.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductUoM, ProductUoMMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (tbl_ProductUoM objtbl_ProductUoM in onjtbl_ProductUoM)
        //        {
        //            ProductUoMMasterModel _ProductUoMMasterModel = iMapper.Map<tbl_ProductUoM, ProductUoMMasterModel>(objtbl_ProductUoM);
        //            lstProductUoMMasterModel.Add(_ProductUoMMasterModel);
        //        }
        //    }
        //    return lstProductUoMMasterModel;
        //}

        //public List<ProductUoMMasterModel> GetProductUOM()
        //{
        //    var lstProductUoMMasterModel = new List<ProductUoMMasterModel>();

        //    var onjtbl_ProductUoM = _db.tbl_ProductUoM.Where(x => x.IsDelete == false).ToList();

        //    //var onjtbl_ProductUoM = (from pu in _db.tbl_ProductUoM
        //    //                         join pm in _db.tbl_ProductMaster.Where(o => o.IsDelete == false)
        //    //                        on pu.ProductID equals pm.ProductID
        //    //                         select new
        //    //                         List<tbl_ProductUoM>
        //    //                         {new tbl_ProductUoM { ProductID = pu.ProductID,ProductUoMID = pu.ProductUoMID,UnitMeasureID = pu.UnitMeasureID,
        //    //                                UpdatedBy = pu.UpdatedBy,UpdatedDate = pu.UpdatedDate,QtyPerUoM = pu.QtyPerUoM,
        //    //                                Discription = pu.Discription,IsActive = pu.IsActive,IsDelete = pu.IsDelete,CreatedBy = pu.CreatedBy,
        //    //                                CreatedDate = pu.CreatedDate} }).Cast<tbl_ProductUoM>().ToList();

        //    if (onjtbl_ProductUoM.Count > 0)
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductUoM, ProductUoMMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        foreach (tbl_ProductUoM objtbl_ProductUoM in onjtbl_ProductUoM)
        //        {
        //            ProductUoMMasterModel _ProductUoMMasterModel = iMapper.Map<tbl_ProductUoM, ProductUoMMasterModel>(objtbl_ProductUoM);
        //            lstProductUoMMasterModel.Add(_ProductUoMMasterModel);
        //        }
        //    }
        //    return lstProductUoMMasterModel;
        //}

        // Check
        public bool CheckProductUoMName(long ProductUoM,long ProductUoMID)
        {
            bool result = false;
            try
            {
                tbl_ProductUoM _tbl_ProductUoM = _db.tbl_ProductUoM.FirstOrDefault(x => x.UnitMeasureID == ProductUoM && x.IsDelete == false && x.ProductID == ProductUoMID);
                if (_tbl_ProductUoM != null)
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
        //public ProductUoMMasterModel ProductUoMDetail(int ProductUoMID)
        //{
        //    tbl_ProductUoM _tbl_ProductUoM = new tbl_ProductUoM();
        //    ProductUoMMasterModel _ProductUoMMasterModel = new ProductUoMMasterModel();

        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_ProductUoM, ProductUoMMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _ProductUoMMasterModel = iMapper.Map<tbl_ProductUoM, ProductUoMMasterModel>(_tbl_ProductUoM);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _ProductUoMMasterModel;
        //}
    }
}
