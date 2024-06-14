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
    public class CustomerMasterDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        public CustomerModel GetCustomerMaster(string moblieNo)
        {
            var customerModel = new List<CustomerModel>();
            var lstcustomerModel = _db.tbl_CustomerMaster.Where(p => p.MobileNo == moblieNo && p.IsActive == true).ToList();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_CustomerMaster, CustomerModel>(); });
            IMapper iMapper = config.CreateMapper();

            CustomerModel _customerModel;
            foreach (tbl_CustomerMaster objGetCouponResult in lstcustomerModel)
            {
                _customerModel = iMapper.Map<tbl_CustomerMaster, CustomerModel>(objGetCouponResult);
                customerModel.Add(_customerModel);
            }
            return customerModel.FirstOrDefault();
        }

        public CustomerModel GetCustomerMasterWithTaxExemption(string moblieNo)
        {
            var customerModel = new List<CustomerModel>();
            var lstcustomerModel = _db.tbl_CustomerMaster.Where(p => p.MobileNo == moblieNo && p.TaxExempted == true && p.IsActive == true).ToList();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_CustomerMaster, CustomerModel>(); });
            IMapper iMapper = config.CreateMapper();

            CustomerModel _customerModel;
            foreach (tbl_CustomerMaster objGetCouponResult in lstcustomerModel)
            {
                _customerModel = iMapper.Map<tbl_CustomerMaster, CustomerModel>(objGetCouponResult);
                customerModel.Add(_customerModel);
            }
            return customerModel.FirstOrDefault();
        }
    }
}
