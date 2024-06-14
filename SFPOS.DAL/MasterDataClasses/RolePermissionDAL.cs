using AutoMapper;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFPOS.Common;

namespace SFPOS.DAL.MasterDataClasses
{
    public class RolePermissionDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public RolePermissionMasterModel AddRolePermission(RolePermissionMasterModel objRolePermissionMasterModel, int TransType)
        {
            try
            {
                tbl_RolePermission objtbl_RolePermission = new tbl_RolePermission();
                if (TransType == 1)//ADD
                {
                    objRolePermissionMasterModel.PermissionID = objRolePermissionMasterModel.PermissionID;
                    objtbl_RolePermission.RoleID = objRolePermissionMasterModel.RoleID;
                    objtbl_RolePermission.MenuID = objRolePermissionMasterModel.MenuID;
                    objtbl_RolePermission.InsertRecords = objRolePermissionMasterModel.InsertRecords;
                    objtbl_RolePermission.EditRecords = objRolePermissionMasterModel.EditRecords;
                    objtbl_RolePermission.DeleteRecords = objRolePermissionMasterModel.DeleteRecords;
                    objtbl_RolePermission.ViewRecords = objRolePermissionMasterModel.ViewRecords;
                    objRolePermissionMasterModel.PermissionID = objtbl_RolePermission.PermissionID;
                    _db.tbl_RolePermission.Add(objtbl_RolePermission);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_RolePermission = _db.tbl_RolePermission.Where(p => p.PermissionID == objRolePermissionMasterModel.PermissionID).FirstOrDefault();
                    if (objtbl_RolePermission != null)
                    {
                        objtbl_RolePermission.PermissionID = objRolePermissionMasterModel.PermissionID;

                        objtbl_RolePermission.InsertRecords = objRolePermissionMasterModel.InsertRecords;
                        objtbl_RolePermission.EditRecords = objRolePermissionMasterModel.EditRecords;
                        objtbl_RolePermission.DeleteRecords = objRolePermissionMasterModel.DeleteRecords;
                        objtbl_RolePermission.ViewRecords = objRolePermissionMasterModel.ViewRecords;
                        //objtbl_RolePermission.UpdatedDate = DateTime.Now;
                        //objtbl_RolePermission.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_RolePermission = _db.tbl_RolePermission.Where(p => p.RoleID == objRolePermissionMasterModel.RoleID && p.MenuID == objRolePermissionMasterModel.MenuID).FirstOrDefault();
                    if (objtbl_RolePermission != null)
                    {
                        _db.tbl_RolePermission.Remove(objtbl_RolePermission);                        
                    }
                }

                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objRolePermissionMasterModel;
        }


        //List
        public List<RolePermissionMasterModel> GetAllRolePermission()
        {
            var lstRolePermissionMasterModel = new List<RolePermissionMasterModel>();

            var onjtbl_RolePermissionMaster = _db.tbl_RolePermission.ToList();

            if (onjtbl_RolePermissionMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_RolePermission, RolePermissionMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_RolePermission objtbl_RolePermissionMaster in onjtbl_RolePermissionMaster)
                {
                    RolePermissionMasterModel _RolePermissionMasterModel = iMapper.Map<tbl_RolePermission, RolePermissionMasterModel>(objtbl_RolePermissionMaster);
                    lstRolePermissionMasterModel.Add(_RolePermissionMasterModel);
                }
            }
            return lstRolePermissionMasterModel;
        }

        public bool CheckRoleName(int RoleId, int MenuId)
        {
            bool result = false;
            try
            {
                tbl_RolePermission _tbl_RolePermission = _db.tbl_RolePermission.FirstOrDefault(x => x.RoleID == RoleId && x.MenuID == MenuId);
                if (_tbl_RolePermission != null)
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
