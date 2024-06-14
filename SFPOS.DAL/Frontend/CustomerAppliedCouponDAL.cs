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
    public class CustomerAppliedCouponDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public List<CustomerAppliedCouponModel> GetCustomerAppliedCouponDAL(long CustomerId, long CouponId)
        {
            var couponModel = new List<CustomerAppliedCouponModel>();
            var lstCouponModel = _db.tbl_CustomerAppliedCoupon.Where(p=> p.CustomerId == CustomerId && p.CouponId == CouponId && p.IsActive == true).ToList();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_CustomerAppliedCoupon, CustomerAppliedCouponModel>(); });
            IMapper iMapper = config.CreateMapper();

            CustomerAppliedCouponModel _CouponModel;
            foreach (tbl_CustomerAppliedCoupon objGetCouponResult in lstCouponModel)
            {
                _CouponModel = iMapper.Map<tbl_CustomerAppliedCoupon, CustomerAppliedCouponModel>(objGetCouponResult);
                couponModel.Add(_CouponModel);
            }
            return couponModel;
        }

        public CustomerAppliedCouponModel AddCustomerAppliedCoupon(CustomerAppliedCouponModel objCustomerAppliedCouponModel)
        {
            try
            {
                tbl_CustomerAppliedCoupon objtbl_CustomerAppliedCoupon = new tbl_CustomerAppliedCoupon();
                objtbl_CustomerAppliedCoupon.CouponId = objCustomerAppliedCouponModel.CouponId;
                objtbl_CustomerAppliedCoupon.CustomerId = objCustomerAppliedCouponModel.CustomerId;
                objtbl_CustomerAppliedCoupon.IsActive = true;
                objtbl_CustomerAppliedCoupon.IsDelete = false;
                objtbl_CustomerAppliedCoupon.CreatedDate = DateTime.Now;
                objtbl_CustomerAppliedCoupon.CreatedBy = LoginInfo.UserId;
                
                _db.tbl_CustomerAppliedCoupon.Add(objtbl_CustomerAppliedCoupon);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                string e = ex.Message;
            }
            return objCustomerAppliedCouponModel;
        }

    }
}
