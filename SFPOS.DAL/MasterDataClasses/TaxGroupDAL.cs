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
    public class TaxGroupDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public TaxGroupMasterModel AddTaxGroup(TaxGroupMasterModel objTaxGroupMasterModel, int TransType)
        {
            try
            {
                tbl_TaxGroupMaster objtbl_TaxGroupMaster = new tbl_TaxGroupMaster();
                if (TransType == 1)//ADD
                {
                    objTaxGroupMasterModel.TaxGroupID = objTaxGroupMasterModel.TaxGroupID;
                    objtbl_TaxGroupMaster.TaxGroupName = objTaxGroupMasterModel.TaxGroupName;
                    objtbl_TaxGroupMaster.IsActive = true;
                    objtbl_TaxGroupMaster.IsDelete = false;
                    objtbl_TaxGroupMaster.CreatedDate = DateTime.Now;
                    objtbl_TaxGroupMaster.CreatedBy = LoginInfo.UserId;
                    objTaxGroupMasterModel.TaxGroupID = objtbl_TaxGroupMaster.TaxGroupID;
                    _db.tbl_TaxGroupMaster.Add(objtbl_TaxGroupMaster);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_TaxGroupMaster = _db.tbl_TaxGroupMaster.Where(p => p.TaxGroupID == objTaxGroupMasterModel.TaxGroupID).FirstOrDefault();
                    if (objtbl_TaxGroupMaster != null)
                    {
                        objtbl_TaxGroupMaster.TaxGroupID = objTaxGroupMasterModel.TaxGroupID;
                        objtbl_TaxGroupMaster.TaxGroupName = objTaxGroupMasterModel.TaxGroupName;
                        objtbl_TaxGroupMaster.UpdatedDate = DateTime.Now;
                        objtbl_TaxGroupMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_TaxGroupMaster = _db.tbl_TaxGroupMaster.Where(p => p.TaxGroupID == objTaxGroupMasterModel.TaxGroupID).FirstOrDefault();
                    if (objtbl_TaxGroupMaster != null)
                    {
                        objtbl_TaxGroupMaster.TaxGroupID = objTaxGroupMasterModel.TaxGroupID;
                        objtbl_TaxGroupMaster.IsDelete = true;
                        objtbl_TaxGroupMaster.UpdatedDate = DateTime.Now;
                        objtbl_TaxGroupMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objTaxGroupMasterModel;
        }

        //List
        public List<TaxGroupMasterModel> GetAllTaxGroup()
        {
            var lstTaxGroupMasterModel = new List<TaxGroupMasterModel>();

            var onjtbl_TaxGroupMaster = _db.tbl_TaxGroupMaster.Where(x => x.IsDelete == false).ToList();

            if (onjtbl_TaxGroupMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_TaxGroupMaster, TaxGroupMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_TaxGroupMaster objtbl_TaxGroupMaster in onjtbl_TaxGroupMaster)
                {
                    TaxGroupMasterModel _TaxGroupMasterModel = iMapper.Map<tbl_TaxGroupMaster, TaxGroupMasterModel>(objtbl_TaxGroupMaster);
                    lstTaxGroupMasterModel.Add(_TaxGroupMasterModel);
                }
            }
            return lstTaxGroupMasterModel;
        }

        //Check
        public bool CheckTaxGroupName(string TaxGroupName)
        {
            bool result = false;
            try
            {
                tbl_TaxGroupMaster _tbl_TaxGroupMaster = _db.tbl_TaxGroupMaster.FirstOrDefault(x => x.TaxGroupName == TaxGroupName && x.IsDelete == false);
                if (_tbl_TaxGroupMaster != null)
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

        public bool CheckName(string TaxGroupName)
        {
            bool result = false;
            try
            {
                tbl_TaxGroupMaster _tbl_TaxGroupMaster = _db.tbl_TaxGroupMaster.FirstOrDefault(x => x.TaxGroupName == TaxGroupName && x.IsDelete == false);
                if (_tbl_TaxGroupMaster != null)
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

        public long GetTaxGroupID(string TaxGroupType)
        {
            long result = 0;
            try
            {
                tbl_TaxGroupMaster _tbl_TaxGroupMaster = _db.tbl_TaxGroupMaster.FirstOrDefault(x => x.TaxGroupName == TaxGroupType && x.IsDelete == false);
                if (_tbl_TaxGroupMaster != null)
                {
                    result = _tbl_TaxGroupMaster.TaxGroupID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }
        //Get Details
        //public TaxGroupMasterModel TaxGroupDetail(int TaxGroupID)
        //{
        //    tbl_TaxGroupMaster _tbl_TaxGroupMaster = new tbl_TaxGroupMaster();
        //    TaxGroupMasterModel _TaxGroupMasterModel = new TaxGroupMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_TaxGroupMaster, TaxGroupMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _TaxGroupMasterModel = iMapper.Map<tbl_TaxGroupMaster, TaxGroupMasterModel>(_tbl_TaxGroupMaster);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _TaxGroupMasterModel;
        //}
    }
}
