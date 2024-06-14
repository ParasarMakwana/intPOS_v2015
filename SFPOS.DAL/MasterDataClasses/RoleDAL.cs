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
    public class RoleDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public RoleMasterModel AddRole(RoleMasterModel objRoleMasterModel, int TransType)
        {
            try
            {
                tbl_RoleMaster objtbl_RoleMaster = new tbl_RoleMaster();
                if (TransType == 1)//ADD
                {
                    objRoleMasterModel.RoleID = objRoleMasterModel.RoleID;
                    objtbl_RoleMaster.RoleType = objRoleMasterModel.RoleType;
                    objtbl_RoleMaster.OverrideAmount = objRoleMasterModel.OverrideAmount;
                    objtbl_RoleMaster.IsDelete = false;
                    objtbl_RoleMaster.IsActive = true;
                    objtbl_RoleMaster.CreatedDate = DateTime.Now;
                    objtbl_RoleMaster.CreatedBy = LoginInfo.UserId;
                    objRoleMasterModel.RoleID = objtbl_RoleMaster.RoleID;
                    _db.tbl_RoleMaster.Add(objtbl_RoleMaster);

                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_RoleMaster = _db.tbl_RoleMaster.Where(p => p.RoleID == objRoleMasterModel.RoleID).FirstOrDefault();
                    if (objtbl_RoleMaster != null)
                    {
                        objtbl_RoleMaster.RoleID = objRoleMasterModel.RoleID;
                        objtbl_RoleMaster.RoleType = objRoleMasterModel.RoleType;
                        objtbl_RoleMaster.OverrideAmount = objRoleMasterModel.OverrideAmount;
                        objtbl_RoleMaster.UpdatedDate = DateTime.Now;
                        objtbl_RoleMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_RoleMaster = _db.tbl_RoleMaster.Where(p => p.RoleID == objRoleMasterModel.RoleID).FirstOrDefault();
                    if (objtbl_RoleMaster != null)
                    {
                        /// Aditya - 20201022
                        //objtbl_RoleMaster.RoleID = objRoleMasterModel.RoleID;
                        //objtbl_RoleMaster.RoleType = objRoleMasterModel.RoleType;
                        //objtbl_RoleMaster.OverwriteAmount = objRoleMasterModel.OverwriteAmount;
                        objtbl_RoleMaster.IsDelete = true;
                        objtbl_RoleMaster.UpdatedDate = DateTime.Now;
                        objtbl_RoleMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objRoleMasterModel;
        }

        //List
        public List<RoleMasterModel> GetAllRole()
        {
            var lstRoleMasterModel = new List<RoleMasterModel>();

            // var onjtbl_RoleMaster = _db.tbl_RoleMaster.Where(x => x.IsDelete == false).ToList();
            var onjtbl_RoleMaster = _db.SP_GetRoleList().ToList();

            if (onjtbl_RoleMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetRoleList_Result, RoleMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetRoleList_Result objtbl_RoleMaster in onjtbl_RoleMaster)
                {
                    RoleMasterModel _RoleMasterModel = iMapper.Map<SP_GetRoleList_Result, RoleMasterModel>(objtbl_RoleMaster);
                    lstRoleMasterModel.Add(_RoleMasterModel);
                }
            }
            return lstRoleMasterModel;
        }

        public List<RoleMasterModel> GetSearchRole(string SearchStr)
        {
            var lstRoleMasterModel = new List<RoleMasterModel>();

            var onjtbl_RoleMaster = _db.tbl_RoleMaster.Where(x => x.IsDelete == false && x.RoleType.ToLower().StartsWith(SearchStr.ToLower())).ToList();

            if (onjtbl_RoleMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_RoleMaster, RoleMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_RoleMaster objtbl_RoleMaster in onjtbl_RoleMaster)
                {
                    RoleMasterModel _RoleMasterModel = iMapper.Map<tbl_RoleMaster, RoleMasterModel>(objtbl_RoleMaster);
                    lstRoleMasterModel.Add(_RoleMasterModel);
                }
            }
            return lstRoleMasterModel;
        }

        //Check
        public bool CheckRoleName(string RoleType, long PrimaryId)
        {
            bool result = false;
            try
            {
                tbl_RoleMaster _tbl_RoleMaster = _db.tbl_RoleMaster.FirstOrDefault(x => x.RoleType == RoleType && x.RoleID != PrimaryId && x.IsDelete == false);
                if (_tbl_RoleMaster != null)
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

        public bool CheckName(string RoleType)
        {
            bool result = false;
            try
            {
                tbl_RoleMaster _tbl_RoleMaster = _db.tbl_RoleMaster.FirstOrDefault(x => x.RoleType == RoleType && x.IsDelete == false);
                if (_tbl_RoleMaster != null)
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

        public long GetRoleID(string RoleType)
        {
            long result = 0;
            try
            {
                tbl_RoleMaster _tbl_RoleMaster = _db.tbl_RoleMaster.FirstOrDefault(x => x.RoleType == RoleType && x.IsDelete == false);
                if (_tbl_RoleMaster != null)
                {
                    result = _tbl_RoleMaster.RoleID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        ////Get Details
        //public RoleMasterModel RoleDetail(int RoleID)
        //{
        //    tbl_RoleMaster _tbl_RoleMaster = new tbl_RoleMaster();
        //    RoleMasterModel _RoleMasterModel = new RoleMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_RoleMaster, RoleMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _RoleMasterModel = iMapper.Map<tbl_RoleMaster, RoleMasterModel>(_tbl_RoleMaster);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _RoleMasterModel;
        //}
    }
}
