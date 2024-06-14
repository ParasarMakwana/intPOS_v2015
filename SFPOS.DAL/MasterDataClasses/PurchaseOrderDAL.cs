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
    public class PurchaseOrderDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public PurchaseOrderMasterModel AddEditDeletePurchaseOrder(PurchaseOrderMasterModel objPurchaseOrderMasterModel, int TransType)
        {
            try
            {
                tbl_PurchaseHeader objtbl_PurchaseHeader = new tbl_PurchaseHeader();
                if (TransType == 1)//ADD
                {
                    objPurchaseOrderMasterModel.PurchaseHeaderID = objPurchaseOrderMasterModel.PurchaseHeaderID;
                    objtbl_PurchaseHeader.VendorID = objPurchaseOrderMasterModel.VendorID;
                    objtbl_PurchaseHeader.PONumber = objPurchaseOrderMasterModel.PONumber;
                    objtbl_PurchaseHeader.OrderDate = objPurchaseOrderMasterModel.OrderDate;
                    objtbl_PurchaseHeader.IsActive = true;
                    objtbl_PurchaseHeader.IsDelete = false;
                    objtbl_PurchaseHeader.isReceived = false;
                    objtbl_PurchaseHeader.CreatedDate = DateTime.Now;
                    objtbl_PurchaseHeader.CreatedBy = LoginInfo.UserId;
                    objPurchaseOrderMasterModel.PurchaseHeaderID = objtbl_PurchaseHeader.PurchaseHeaderID;
                    _db.tbl_PurchaseHeader.Add(objtbl_PurchaseHeader);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_PurchaseHeader = _db.tbl_PurchaseHeader.Where(p => p.PurchaseHeaderID == objPurchaseOrderMasterModel.PurchaseHeaderID).FirstOrDefault();
                    if (objtbl_PurchaseHeader != null)
                    {
                        objPurchaseOrderMasterModel.PurchaseHeaderID = objPurchaseOrderMasterModel.PurchaseHeaderID;
                        objtbl_PurchaseHeader.VendorID = objPurchaseOrderMasterModel.VendorID;
                        objtbl_PurchaseHeader.PONumber = objPurchaseOrderMasterModel.PONumber;
                        objtbl_PurchaseHeader.OrderDate = objPurchaseOrderMasterModel.OrderDate;
                        objtbl_PurchaseHeader.IsActive = true;
                        objtbl_PurchaseHeader.IsDelete = false;
                        objtbl_PurchaseHeader.UpdatedDate = DateTime.Now;
                        objtbl_PurchaseHeader.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_PurchaseHeader = _db.tbl_PurchaseHeader.Where(p => p.PurchaseHeaderID == objPurchaseOrderMasterModel.PurchaseHeaderID).FirstOrDefault();
                    if (objtbl_PurchaseHeader != null)
                    {
                        objtbl_PurchaseHeader.PurchaseHeaderID = objPurchaseOrderMasterModel.PurchaseHeaderID;
                        objtbl_PurchaseHeader.IsDelete = true;
                        objtbl_PurchaseHeader.IsActive = false;
                        objtbl_PurchaseHeader.UpdatedDate = DateTime.Now;
                        objtbl_PurchaseHeader.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 4)//RECEIVE
                {
                    objtbl_PurchaseHeader = _db.tbl_PurchaseHeader.Where(p => p.PurchaseHeaderID == objPurchaseOrderMasterModel.PurchaseHeaderID).FirstOrDefault();
                    if (objtbl_PurchaseHeader != null)
                    {
                        objtbl_PurchaseHeader.PurchaseHeaderID = objPurchaseOrderMasterModel.PurchaseHeaderID;
                        objtbl_PurchaseHeader.isReceived = true;
                        objtbl_PurchaseHeader.UpdatedDate = DateTime.Now;
                        objtbl_PurchaseHeader.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objPurchaseOrderMasterModel;
        }

        //List
        public List<PurchaseOrderMasterModel> GetAllPurchaseOrder()
        {
            var lstPurchaseOrderMasterModel = new List<PurchaseOrderMasterModel>();

            //  var onjtbl_PurchaseHeader = _db.tbl_PurchaseHeader.Where(x => x.IsDelete == false).OrderByDescending(x=>x.PurchaseHeaderID).ToList();
            var onjtbl_PurchaseHeader = _db.SP_GetPurchaseOrderList().ToList();

            if (onjtbl_PurchaseHeader.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetPurchaseOrderList_Result, PurchaseOrderMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetPurchaseOrderList_Result objtbl_PurchaseHeader in onjtbl_PurchaseHeader)
                {
                    PurchaseOrderMasterModel _PurchaseOrderMasterModel = iMapper.Map<SP_GetPurchaseOrderList_Result, PurchaseOrderMasterModel>(objtbl_PurchaseHeader);
                    lstPurchaseOrderMasterModel.Add(_PurchaseOrderMasterModel);
                }
            }
            return lstPurchaseOrderMasterModel;
        }

        //Get Details
        public PurchaseOrderMasterModel PurchaseOrderDetail(int PurchaseOrderID)
        {
            tbl_PurchaseHeader _tbl_PurchaseHeader = new tbl_PurchaseHeader();
            PurchaseOrderMasterModel _PurchaseOrderMasterModel = new PurchaseOrderMasterModel();
            try
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_PurchaseHeader, PurchaseOrderMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                _PurchaseOrderMasterModel = iMapper.Map<tbl_PurchaseHeader, PurchaseOrderMasterModel>(_tbl_PurchaseHeader);
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return _PurchaseOrderMasterModel;
        }

        //Check InvoicNos.
        public bool CheckInvoiceNo(string InvoiceNo,long PrimaryId)
        {
            bool result = false;
            try
            {
                tbl_PurchaseHeader _tbl_PurchaseHeader = _db.tbl_PurchaseHeader.FirstOrDefault(x => x.PONumber == InvoiceNo && x.IsDelete == false && x.isReceived == false && x.PurchaseHeaderID != PrimaryId);
                if (_tbl_PurchaseHeader != null)
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
