using SFPOS.DAL.Frontend;
using SFPOS.Entities.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.Frontend
{
    public class CustomerAppliedCouponService
    {
        CustomerAppliedCouponDAL objCustomerAppliedCouponDAL = new CustomerAppliedCouponDAL();
        public List<CustomerAppliedCouponModel> GetCustomerAppliedCoupon(long CustomerId, long CouponId)
        {
            return objCustomerAppliedCouponDAL.GetCustomerAppliedCouponDAL(CustomerId, CouponId);
        }
        public CustomerAppliedCouponModel AddCustomerAppliedCoupon(CustomerAppliedCouponModel objCustomerAppliedCouponModel)
        {
            return objCustomerAppliedCouponDAL.AddCustomerAppliedCoupon(objCustomerAppliedCouponModel);
        }
    }
}
