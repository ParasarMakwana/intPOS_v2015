using SFPOS.DAL.Frontend;
using SFPOS.Entities.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.Frontend
{
    public class CustomerMasterService
    {
        CustomerMasterDAL objCustomerDAL = new CustomerMasterDAL();
        public CustomerModel GetVerifyCustomer(string mobileNo)
        {
            return objCustomerDAL.GetCustomerMaster(mobileNo);
        }

        public CustomerModel GetCustomerMasterWithTaxExemption(string mobileNo)
        {
            return objCustomerDAL.GetCustomerMasterWithTaxExemption(mobileNo);
        }
    }
}
