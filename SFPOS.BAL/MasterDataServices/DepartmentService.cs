using SFPOS.Entities;
using SPPOS.DAL;
using System.Collections.Generic;

namespace SFPOS.BAL
{
    public class DepartmentService
    {
        DepartmentDAL objDepartmentDAL = new DepartmentDAL();
        public DepartmentMasterModel AddDepartment(DepartmentMasterModel objDepartmentMasterModel, int TransType)
        {
            return objDepartmentDAL.AddDepartment(objDepartmentMasterModel, TransType);
        }
        public List<DepartmentMasterModel> GetAllDepartment()
        {
            return objDepartmentDAL.GetAllDepartment();
        }
        public bool CheckDepartmentName(string DepartmentName, long PrimaryId)
        {
            return objDepartmentDAL.CheckDepartmentName(DepartmentName, PrimaryId);
        }
        public DepartmentMasterModel DeleteAllDepartment(DepartmentMasterModel objDepartmentMasterModel)
        {
            return objDepartmentDAL.DeleteAllDepartment(objDepartmentMasterModel);
        }

        public bool CheckName(string DepartmentName)
        {
            return objDepartmentDAL.CheckName(DepartmentName);
        }

        public long GetDepartmentID(string DepartmentName)
        {
            return objDepartmentDAL.GetDepartmentID(DepartmentName);
        }

        public bool CheckDepartmentButtonFlag()
        {
            return objDepartmentDAL.CheckDepartmentButtonFlag();
        }

    }
}
