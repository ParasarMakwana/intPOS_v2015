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
    public class PurchaseLineDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public PurchaseLineMasterModel AddEditDeletePurchaseOrder(PurchaseLineMasterModel objPurchaseLineMasterModel, int TransType)
        {
            try
            {
                tbl_PurchaseLine objtbl_PurchaseLine = new tbl_PurchaseLine();
                if (TransType == 1)//ADD
                {
                    objPurchaseLineMasterModel.PurchaseLineID = objPurchaseLineMasterModel.PurchaseLineID;
                    objtbl_PurchaseLine.PurchaseHeaderID = objPurchaseLineMasterModel.PurchaseHeaderID;
                    objtbl_PurchaseLine.ProductID = objPurchaseLineMasterModel.ProductID;
                    objtbl_PurchaseLine.ItemCode = objPurchaseLineMasterModel.ItemCode;
                    objtbl_PurchaseLine.Quantity = objPurchaseLineMasterModel.Quantity;
                    objtbl_PurchaseLine.UnitCost = objPurchaseLineMasterModel.UnitCost;
                    objtbl_PurchaseLine.Tax = objPurchaseLineMasterModel.Tax;
                    objtbl_PurchaseLine.TaxAmount = objPurchaseLineMasterModel.TaxAmount;
                    objtbl_PurchaseLine.LineAmtExclTax = objPurchaseLineMasterModel.LineAmtExclTax;
                    objtbl_PurchaseLine.LineAmtInclTax = objPurchaseLineMasterModel.LineAmtInclTax;
                    objtbl_PurchaseLine.PurchaseType = objPurchaseLineMasterModel.PurchaseType;
                    objtbl_PurchaseLine.TaxGroupID = objPurchaseLineMasterModel.TaxGroupID;
                    objtbl_PurchaseLine.CreatedDate = DateTime.Now;
                    objtbl_PurchaseLine.CreatedBy = LoginInfo.UserId;
                    objtbl_PurchaseLine.IsDelete = false;
                    objtbl_PurchaseLine.isReceived = false;
                    objPurchaseLineMasterModel.PurchaseLineID = objtbl_PurchaseLine.PurchaseLineID;
                    _db.tbl_PurchaseLine.Add(objtbl_PurchaseLine);
                }
                if (TransType == 2)//EDIT
                {
                    objtbl_PurchaseLine = _db.tbl_PurchaseLine.Where(p => p.PurchaseLineID == objPurchaseLineMasterModel.PurchaseLineID).FirstOrDefault();
                    if (objtbl_PurchaseLine != null)
                    {
                        objPurchaseLineMasterModel.PurchaseLineID = objPurchaseLineMasterModel.PurchaseLineID;
                        objtbl_PurchaseLine.Quantity = objPurchaseLineMasterModel.Quantity;
                        objtbl_PurchaseLine.UnitCost = objPurchaseLineMasterModel.UnitCost;
                        objtbl_PurchaseLine.ItemCode = objPurchaseLineMasterModel.ItemCode;
                        objtbl_PurchaseLine.Tax = objPurchaseLineMasterModel.Tax;
                        objtbl_PurchaseLine.TaxAmount = objPurchaseLineMasterModel.TaxAmount;
                        objtbl_PurchaseLine.LineAmtExclTax = objPurchaseLineMasterModel.LineAmtExclTax;
                        objtbl_PurchaseLine.LineAmtInclTax = objPurchaseLineMasterModel.LineAmtInclTax;
                        objtbl_PurchaseLine.PurchaseType = objPurchaseLineMasterModel.PurchaseType;
                        objtbl_PurchaseLine.UpdateDate = DateTime.Now;
                        objtbl_PurchaseLine.UpdatedBy = LoginInfo.UserId;
                        objtbl_PurchaseLine.IsDelete = false;
                        objtbl_PurchaseLine.isReceived = false;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_PurchaseLine = _db.tbl_PurchaseLine.Where(p => p.PurchaseLineID == objPurchaseLineMasterModel.PurchaseLineID).FirstOrDefault();
                    if (objtbl_PurchaseLine != null)
                    {
                        objtbl_PurchaseLine.IsDelete = true;
                        objtbl_PurchaseLine.UpdateDate = DateTime.Now;
                        objtbl_PurchaseLine.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 4)//RECEIVE
                {
                    objtbl_PurchaseLine = _db.tbl_PurchaseLine.Where(p => p.PurchaseLineID == objPurchaseLineMasterModel.PurchaseLineID).FirstOrDefault();
                    if (objtbl_PurchaseLine != null)
                    {
                        objtbl_PurchaseLine.isReceived = true;
                        objtbl_PurchaseLine.UpdateDate = DateTime.Now;
                        objtbl_PurchaseLine.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objPurchaseLineMasterModel;
        }

        //List
        public List<GetPurchaseLine_ResultModel> GetAllPurchaseLine(string UPCCode, long productPurchaseHeaderId)
        {
            var lstGetPurchaseLine_Result = new List<GetPurchaseLine_ResultModel>();

            var onjSP_GetPurchaseLine = _db.SP_GetPurchaseLine(UPCCode, productPurchaseHeaderId).ToList();
            if (onjSP_GetPurchaseLine.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetPurchaseLine_Result, GetPurchaseLine_ResultModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetPurchaseLine_Result objSP_InventoryReport_Result in onjSP_GetPurchaseLine)
                {
                    GetPurchaseLine_ResultModel _InventoryReport_Result = iMapper.Map<SP_GetPurchaseLine_Result, GetPurchaseLine_ResultModel>(objSP_InventoryReport_Result);
                    lstGetPurchaseLine_Result.Add(_InventoryReport_Result);
                }
            }
            return lstGetPurchaseLine_Result;

        }

        //Get Details
        public bool PurchaseOrderDetail(long PurchaseHeaderID, long ProductID)
        {
            //tbl_PurchaseLine _tbl_PurchaseLine = new tbl_PurchaseLine();
            //PurchaseLineMasterModel _PurchaseLineMasterModel = new PurchaseLineMasterModel();
            //try
            //{
            //    var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_PurchaseLine, PurchaseLineMasterModel>(); });
            //    IMapper iMapper = config.CreateMapper();
            //    _PurchaseLineMasterModel = iMapper.Map<tbl_PurchaseLine, PurchaseLineMasterModel>(_tbl_PurchaseLine);
            //}
            //catch (Exception e)
            //{
            //    string ex = e.Message;
            //}
            //return _PurchaseLineMasterModel;


            bool result = false;
            try
            {
                tbl_PurchaseLine _tbl_PurchaseLine = _db.tbl_PurchaseLine.FirstOrDefault(x => x.PurchaseHeaderID == PurchaseHeaderID && x.IsDelete == false && x.ProductID == ProductID);
                if (_tbl_PurchaseLine != null)
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

    }
}
