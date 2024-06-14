using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;
using System;

namespace SFPOS.BAL.MasterDataServices
{
    public class EmployeeService
    {
        EmployeeDAL objEmployeeDAL = new EmployeeDAL();
        public EmployeeMasterModel AddEmployee(EmployeeMasterModel objEmployeeMasterModel, int TransType)
        {
            return objEmployeeDAL.AddEmployee(objEmployeeMasterModel, TransType);
        }
        public List<EmployeeMasterModel> GetAllEmployee()
        {
            return objEmployeeDAL.GetAllEmployee();
        }
        public List<EmployeeMasterModel> GetAllEmployeeByTransDate(DateTime Datval)
        {
            return objEmployeeDAL.GetAllEmployeeByTransDate(Datval);
        }
        public bool CheckEmployeeName(string EmployeeName,long EmployeeID)
        {
            return objEmployeeDAL.CheckEmployeeName(EmployeeName, EmployeeID);
        }
        public bool CheckName(string FirstName)
        {
            return objEmployeeDAL.CheckName(FirstName);
        }
        //public EmployeeMasterModel EmployeeDetail(int EmployeeID)
        //{
        //    return objEmployeeDAL.EmployeeDetail(EmployeeID);
        //}
        public bool CheckEmployeePasswordDuplicate(string password, long employeeId)
        {
            return objEmployeeDAL.CheckEmployeePasswordDuplicate(password, employeeId);
        }
    }
}
