using SFPOS.DAL;
using SFPOS.DAL.Frontend;
using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class PaymentTransService
    {
        PaymentTransDAL objPaymentTransDAL = new PaymentTransDAL();

        public PaymentTransMasterModel AddPaymentTrans(PaymentTransMasterModel objPaymentTransMasterModel, int TransType)
        {
            return objPaymentTransDAL.AddPaymentTrans(objPaymentTransMasterModel, TransType);
        }
        public List<PaymentTransMasterModel> GetPaymentTrans(long OrderID)
        {
            return objPaymentTransDAL.GetPaymentTrans(OrderID);
        }
    }
}
