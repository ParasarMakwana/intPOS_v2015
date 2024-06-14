using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class RolePermissionService
    {
        RolePermissionDAL objRolePermissionDAL = new RolePermissionDAL();
        public RolePermissionMasterModel AddRolePermission(RolePermissionMasterModel objRoleMasterModel, int TransType)
        {
            return objRolePermissionDAL.AddRolePermission(objRoleMasterModel, TransType);
        }
        public List<RolePermissionMasterModel> GetAllRolePermission()
        {
            return objRolePermissionDAL.GetAllRolePermission();
        }
        public bool CheckRoleName(int RoleId,int MenuId)
        {
            return objRolePermissionDAL.CheckRoleName(RoleId,MenuId);
        }
    }
}
