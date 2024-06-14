using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    
    public class CouponService
    {
        CouponDAL objCouponDAL = new CouponDAL();

        public CouponMasterModel AddCoupon(CouponMasterModel objCouponMasterModel, int TransType)
        {
            return objCouponDAL.AddCoupon(objCouponMasterModel, TransType);
        }

        public List<CouponMasterModel> GetAllCoupon()
        {
            return objCouponDAL.GetAllCoupon();
        }
        public List<CouponMasterModel> GetCouponCode(string CoupenCode)
        {
            return objCouponDAL.GetCouponCode(CoupenCode);
        }
    }
}
