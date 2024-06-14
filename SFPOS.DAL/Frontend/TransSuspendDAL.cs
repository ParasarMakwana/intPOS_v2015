using AutoMapper;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOS.DAL.Frontend
{
    public class TransSuspendDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD //EDIT //DELETE
        public TransSuspendMasterModel AddTransSuspend(TransSuspendMasterModel objTransSuspendMasterModel, int TransType)
        {
            try
            {
                tbl_TransSuspenMaster objtbl_TransSuspenMaster = new tbl_TransSuspenMaster();
                if (TransType == 1)//ADD
                {
                    objTransSuspendMasterModel.TransSuspendID = objTransSuspendMasterModel.TransSuspendID;
                    objtbl_TransSuspenMaster.TransSuspendCode = objTransSuspendMasterModel.TransSuspendCode;
                    objtbl_TransSuspenMaster.ProductID = objTransSuspendMasterModel.ProductID;
                    objtbl_TransSuspenMaster.ProductName = objTransSuspendMasterModel.ProductName;
                    objtbl_TransSuspenMaster.UPCCode = objTransSuspendMasterModel.UPCCode;
                    objtbl_TransSuspenMaster.DepartmentID = objTransSuspendMasterModel.DepartmentID;
                    objtbl_TransSuspenMaster.SectionID = objTransSuspendMasterModel.SectionID;
                    objtbl_TransSuspenMaster.Quantity = objTransSuspendMasterModel.Quantity;
                    objtbl_TransSuspenMaster.DiscountApplyed = objTransSuspendMasterModel.DiscountApplyed;
                    objtbl_TransSuspenMaster.Status = objTransSuspendMasterModel.Status;
                    objtbl_TransSuspenMaster.Tax = objTransSuspendMasterModel.Tax;
                    objtbl_TransSuspenMaster.GrossAmount = objTransSuspendMasterModel.GrossAmount;
                    objtbl_TransSuspenMaster.SellPrice = objTransSuspendMasterModel.SellPrice;
                    objtbl_TransSuspenMaster.FinalPrice = objTransSuspendMasterModel.FinalPrice;
                    objtbl_TransSuspenMaster.TotalAmount = objTransSuspendMasterModel.TotalAmount;
                    objtbl_TransSuspenMaster.TotalTaxAmount = objTransSuspendMasterModel.TotalTaxAmount;
                    objtbl_TransSuspenMaster.IsScale = objTransSuspendMasterModel.IsScale;
                    objtbl_TransSuspenMaster.CreatedDate = objTransSuspendMasterModel.CreatedDate;
                    objtbl_TransSuspenMaster.CreatedBy = objTransSuspendMasterModel.CreatedBy;
                    objtbl_TransSuspenMaster.IsFoodStamp = objTransSuspendMasterModel.IsFoodStamp;
                    objtbl_TransSuspenMaster.IsTax = objTransSuspendMasterModel.IsTax;

                    objtbl_TransSuspenMaster.GroupPrice = objTransSuspendMasterModel.GroupPrice;
                    objtbl_TransSuspenMaster.GroupQty = objTransSuspendMasterModel.GroupQty;
                    objtbl_TransSuspenMaster.CasePrice = objTransSuspendMasterModel.CasePrice;
                    objtbl_TransSuspenMaster.CaseQty = objTransSuspendMasterModel.CaseQty;
                    objtbl_TransSuspenMaster.CasePriceApplied = objTransSuspendMasterModel.CasePriceApplied;
                    objtbl_TransSuspenMaster.StoreID = objTransSuspendMasterModel.StoreID;
                    objtbl_TransSuspenMaster.IsDelete = objTransSuspendMasterModel.IsDelete;
                    objtbl_TransSuspenMaster.IsManWTRefund = objTransSuspendMasterModel.ManWT;
                    objtbl_TransSuspenMaster.FSEligibleAmount = objTransSuspendMasterModel.FSEligibleAmount;
                    _db.tbl_TransSuspenMaster.Add(objtbl_TransSuspenMaster);
                    _db.SaveChanges();
                    objTransSuspendMasterModel.TransSuspendID = objtbl_TransSuspenMaster.TransSuspendID;
                }
                if (TransType == 2)//DELETE
                {
                    objtbl_TransSuspenMaster = _db.tbl_TransSuspenMaster.Where(p => p.TransSuspendID == objTransSuspendMasterModel.TransSuspendID).FirstOrDefault();
                    if (objtbl_TransSuspenMaster != null)
                    {
                        objtbl_TransSuspenMaster.IsDelete = objTransSuspendMasterModel.IsDelete;
                        objtbl_TransSuspenMaster.Status = objTransSuspendMasterModel.Status;
                    }
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objTransSuspendMasterModel;
        }

        public List<TransSuspendMasterModel> GetAllTransSuspendDetail(string TransSuspendCode)
        {
            var lstTransSuspendMasterModel = new List<TransSuspendMasterModel>();
            
            var onjtbl_TransSuspenMaster = _db.SP_GetTransSuspendList(TransSuspendCode.Trim()).ToList();
            if (onjtbl_TransSuspenMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetTransSuspendList_Result, TransSuspendMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetTransSuspendList_Result objtbl_TransSuspenMaster in onjtbl_TransSuspenMaster)
                {
                    TransSuspendMasterModel _TransSuspendMasterModel = iMapper.Map<SP_GetTransSuspendList_Result, TransSuspendMasterModel>(objtbl_TransSuspenMaster);
                    lstTransSuspendMasterModel.Add(_TransSuspendMasterModel);
                }
            }

            return lstTransSuspendMasterModel;
        }
    }
}
