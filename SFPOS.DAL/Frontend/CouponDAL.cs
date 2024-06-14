using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.Frontend
{
    public class CouponDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public List<CouponModel> GetCoupon(decimal price, long CustomerId)
        {
            var couponModel = new List<CouponModel>();
            var lstCouponModel = _db.SP_GetCouponCustomerVerify(CustomerId, price, DateTime.Now);

            //_db.tbl_CouponMaster.Where(p=> p.MinPurchaseAmt>= price && (p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now) && p.IsActive == true).ToList();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetCouponCustomerVerify_Result, CouponModel>(); });
            IMapper iMapper = config.CreateMapper();

            CouponModel _CouponModel;
            foreach (SP_GetCouponCustomerVerify_Result objGetCouponResult in lstCouponModel)
            {
                _CouponModel = iMapper.Map<SP_GetCouponCustomerVerify_Result, CouponModel>(objGetCouponResult);
                couponModel.Add(_CouponModel);
            }
            return couponModel;
        }

    }
}
