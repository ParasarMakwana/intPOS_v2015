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
    public class TaxRateDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public TaxRateMasterModel AddTaxRate(TaxRateMasterModel objTaxRateMasterModel, int TransType)
        {
            try
            {
                tbl_TaxRateMaster objtbl_TaxRateMaster = new tbl_TaxRateMaster();
                if (TransType == 1)//ADD
                {
                    objTaxRateMasterModel.TaxRateID = objTaxRateMasterModel.TaxRateID;
                    objtbl_TaxRateMaster.TaxGroupID = objTaxRateMasterModel.TaxGroupID;
                    objtbl_TaxRateMaster.Tax = objTaxRateMasterModel.Tax;
                    objtbl_TaxRateMaster.StartDate = objTaxRateMasterModel.StartDate;
                    objtbl_TaxRateMaster.EndDate = objTaxRateMasterModel.EndDate;
                    objtbl_TaxRateMaster.IsActive = true;
                    objtbl_TaxRateMaster.IsDelete = false;
                    objtbl_TaxRateMaster.CreatedDate = DateTime.Now;
                    objtbl_TaxRateMaster.CreatedBy = LoginInfo.UserId;
                    objTaxRateMasterModel.TaxRateID = objtbl_TaxRateMaster.TaxRateID;
                    _db.tbl_TaxRateMaster.Add(objtbl_TaxRateMaster);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_TaxRateMaster = _db.tbl_TaxRateMaster.Where(p => p.TaxRateID == objTaxRateMasterModel.TaxRateID).FirstOrDefault();
                    if (objtbl_TaxRateMaster != null)
                    {
                        objtbl_TaxRateMaster.TaxRateID = objTaxRateMasterModel.TaxRateID;
                        objtbl_TaxRateMaster.TaxGroupID = objTaxRateMasterModel.TaxGroupID;
                        objtbl_TaxRateMaster.Tax = objTaxRateMasterModel.Tax;
                        objtbl_TaxRateMaster.StartDate = objTaxRateMasterModel.StartDate;
                        objtbl_TaxRateMaster.EndDate = objTaxRateMasterModel.EndDate;
                        objtbl_TaxRateMaster.UpdatedDate = DateTime.Now;
                        objtbl_TaxRateMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_TaxRateMaster = _db.tbl_TaxRateMaster.Where(p => p.TaxRateID == objTaxRateMasterModel.TaxRateID).FirstOrDefault();
                    if (objtbl_TaxRateMaster != null)
                    {
                        objtbl_TaxRateMaster.TaxRateID = objTaxRateMasterModel.TaxRateID;
                        objtbl_TaxRateMaster.IsDelete = true;
                        objtbl_TaxRateMaster.UpdatedDate = DateTime.Now;
                        objtbl_TaxRateMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objTaxRateMasterModel;
        }

        //List
        public List<TaxRateMasterModel> GetAllTaxRate()
        {
            var lstTaxRateMasterModel = new List<TaxRateMasterModel>();

            var onjtbl_TaxRateMaster = _db.SP_GetTaxRateList().ToList();

            if (onjtbl_TaxRateMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetTaxRateList_Result, TaxRateMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetTaxRateList_Result objtbl_TaxRateMaster in onjtbl_TaxRateMaster)
                {
                    TaxRateMasterModel _TaxRateMasterModel = iMapper.Map<SP_GetTaxRateList_Result, TaxRateMasterModel>(objtbl_TaxRateMaster);
                    lstTaxRateMasterModel.Add(_TaxRateMasterModel);
                }
            }
            return lstTaxRateMasterModel;
        }

        //Check
        public bool CheckTaxRateName(long TaxRateName, DateTime StartDate, DateTime EndDate, long PrimaryID)
        {
            bool result = false;
            try
            {
                tbl_TaxRateMaster _tbl_TaxRateMaster = _db.tbl_TaxRateMaster.FirstOrDefault(x => x.TaxGroupID == TaxRateName && x.IsDelete == false && x.TaxRateID != PrimaryID && x.StartDate == StartDate && x.EndDate == EndDate);
                if (_tbl_TaxRateMaster != null)
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

        public bool CheckName(decimal TaxRateName, DateTime StartDate, DateTime EndDate)
        {
            bool result = false;
            try
            {
                tbl_TaxRateMaster _tbl_TaxRateMaster = _db.tbl_TaxRateMaster.FirstOrDefault(x => x.Tax == TaxRateName && x.StartDate == StartDate && x.EndDate == EndDate && x.IsDelete == false);
                if (_tbl_TaxRateMaster != null)
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
        //public TaxRateMasterModel TaxRateDetail(int TaxRateID)
        //{
        //    tbl_TaxRateMaster _tbl_TaxRateMaster = new tbl_TaxRateMaster();
        //    TaxRateMasterModel _TaxRateMasterModel = new TaxRateMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_TaxRateMaster, TaxRateMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _TaxRateMasterModel = iMapper.Map<tbl_TaxRateMaster, TaxRateMasterModel>(_tbl_TaxRateMaster);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _TaxRateMasterModel;
        //}
    }
}
