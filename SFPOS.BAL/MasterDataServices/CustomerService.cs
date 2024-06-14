using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class CustomerService
    {
        CustomerDAL objCustomerDAL = new CustomerDAL();
        public CustomerMasterModel AddCustomer(CustomerMasterModel objCustomerMasterModel, int TransType)
        {
            return objCustomerDAL.AddCustomer(objCustomerMasterModel, TransType);
        }
        public List<CustomerMasterModel> GetAllCustomers()
        {
            return objCustomerDAL.GetAllCustomers();
        }
        public bool CheckCustomerEmail(string EmailID, long CustomerID)
        {
            return objCustomerDAL.CheckCustomerEmail(EmailID, CustomerID);
        }
        public bool CheckCustomerEmail(string FirstName)
        {
            return objCustomerDAL.CheckCustomerEmail(FirstName);
        }
        //public EmployeeMasterModel EmployeeDetail(int EmployeeID)
        //{
        //    return objEmployeeDAL.EmployeeDetail(EmployeeID);
        //}
    }
}
