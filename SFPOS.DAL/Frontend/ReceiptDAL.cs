using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.Frontend
{
    public class ReceiptDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<ReciptDetails_ResultModel> GetReceiptDetails(long OrderID, long EmployeeID, bool reprint)
        {
            var lstReceiptMasterModel = new List<ReciptDetails_ResultModel>();
            var lstReciptDetails = _db.SP_ReciptDetails(OrderID, EmployeeID, reprint,LoginInfo.CounterIP).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_ReciptDetails_Result, ReciptDetails_ResultModel>(); });
            IMapper iMapper = config.CreateMapper();

            ReciptDetails_ResultModel _SP_ReceiptMasterModel;
            foreach (SP_ReciptDetails_Result objGetReciptDetails_Result in lstReciptDetails)
            {
                _SP_ReceiptMasterModel = iMapper.Map<SP_ReciptDetails_Result, ReciptDetails_ResultModel>(objGetReciptDetails_Result);
                lstReceiptMasterModel.Add(_SP_ReceiptMasterModel);
            }
            return lstReceiptMasterModel;
        }

        public List<CashierReceipt_ResultModel> GetCashierReceipt()
        {
            var lstCashierReceipt_ResultModel = new List<CashierReceipt_ResultModel>();
            var lstCashierReceipt = _db.SP_GetCashierReceipt(LoginInfo.StoreID, LoginInfo.UserId, Convert.ToDateTime(LoginInfo.LoginTime.ToString("MM/dd/yyyy HH:mm:ss")), Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"))).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetCashierReceipt_Result, CashierReceipt_ResultModel>(); });
            IMapper iMapper = config.CreateMapper();

            CashierReceipt_ResultModel _SP_CashierReceipt_ResultModel;
            foreach (SP_GetCashierReceipt_Result objGetCashierReceipt_Result in lstCashierReceipt)
            {
                _SP_CashierReceipt_ResultModel = iMapper.Map<SP_GetCashierReceipt_Result, CashierReceipt_ResultModel>(objGetCashierReceipt_Result);
                lstCashierReceipt_ResultModel.Add(_SP_CashierReceipt_ResultModel);
            }
            return lstCashierReceipt_ResultModel;
        }
    }
}
