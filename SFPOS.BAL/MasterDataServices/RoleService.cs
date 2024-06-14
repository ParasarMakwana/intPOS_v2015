using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class RoleService
    {
        RoleDAL objRoleDAL = new RoleDAL();
        public RoleMasterModel AddRole(RoleMasterModel objRoleMasterModel, int TransType)
        {
            return objRoleDAL.AddRole(objRoleMasterModel, TransType);
        }
        public List<RoleMasterModel> GetAllRole()
        {
            return objRoleDAL.GetAllRole();
        }
        public List<RoleMasterModel> GetSearchRole(string SearchStr)
        {
            return objRoleDAL.GetSearchRole(SearchStr);
        }
        public bool CheckRoleName(string RoleName, long PrimaryId)
        {
            return objRoleDAL.CheckRoleName(RoleName, PrimaryId);
        }

        public bool CheckName(string RoleName)
        {
            return objRoleDAL.CheckName(RoleName);
        }

        public long GetRoleID(string RoleName)
        {
            return objRoleDAL.GetRoleID(RoleName);
        }
        //public RoleMasterModel RoleDetail(int RoleID)
        //{
        //    return objRoleDAL.RoleDetail(RoleID);
        //}
    }
}
