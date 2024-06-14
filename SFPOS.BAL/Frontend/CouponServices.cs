using SFPOS.DAL.Frontend;
using SFPOS.Entities.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.Frontend
{
    public class CouponServices
    {
        CouponDAL objCouponDAL = new CouponDAL();
        public List<CouponModel> GetCoupon(decimal price, long customerId)
        {
            return objCouponDAL.GetCoupon(price, customerId);

        }
    }
}
