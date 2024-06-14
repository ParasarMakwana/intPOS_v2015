using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFPOS.DAL.Frontend
{
    public class PaymentTransDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public PaymentTransMasterModel AddPaymentTrans(PaymentTransMasterModel objPaymentTransMasterModel, int TransType)
        {
            try
            {
                tbl_PaymentTrans objtbl_PaymentTrans = new tbl_PaymentTrans();
                if (TransType == 1)//ADD
                {
                    objPaymentTransMasterModel.PaymentTransID = objPaymentTransMasterModel.PaymentTransID;
                    objtbl_PaymentTrans.OrderID = objPaymentTransMasterModel.OrderID;
                    objtbl_PaymentTrans.CardNumber = objPaymentTransMasterModel.CardNumber;
                    objtbl_PaymentTrans.PaymentMethodID = objPaymentTransMasterModel.PaymentMethodID;
                    objtbl_PaymentTrans.StoreID = objPaymentTransMasterModel.StoreID;
                    objtbl_PaymentTrans.CashAmount = objPaymentTransMasterModel.CashAmount;
                    objtbl_PaymentTrans.CheckAmount = objPaymentTransMasterModel.CheckAmount;
                    objtbl_PaymentTrans.CreditCardAmount = objPaymentTransMasterModel.CreditCardAmount;
                    objtbl_PaymentTrans.FoodStampAmount = objPaymentTransMasterModel.FoodStampAmount;
                    objtbl_PaymentTrans.Balance = objPaymentTransMasterModel.Balance;
                    objtbl_PaymentTrans.ChangeAmount = objPaymentTransMasterModel.ChangeAmount;
                    objtbl_PaymentTrans.CreatedDate = objPaymentTransMasterModel.CreatedDate;
                    objtbl_PaymentTrans.CreatedBy = objPaymentTransMasterModel.CreatedBy;
                    objtbl_PaymentTrans.CounterIP = objPaymentTransMasterModel.CounterIP;
                    _db.tbl_PaymentTrans.Add(objtbl_PaymentTrans);
                    _db.SaveChanges();
                    objPaymentTransMasterModel.PaymentTransID = objtbl_PaymentTrans.PaymentTransID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
                Functions.ErrorLog("PaymentTransDAL", "AddPaymentTrans(tbl_PaymentTrans)", e);
            }
            return objPaymentTransMasterModel;
        }

        public List<PaymentTransMasterModel> GetPaymentTrans(long OrderID)
        {
            var lstPaymentTransMasterModel = new List<PaymentTransMasterModel>();
            var lstPaymentTrans = _db.tbl_PaymentTrans.Where(o => o.OrderID == OrderID).ToList();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_PaymentTrans, PaymentTransMasterModel>(); });
            IMapper iMapper = config.CreateMapper();

            PaymentTransMasterModel _PaymentTransMasterModel;
            foreach (tbl_PaymentTrans objGetPaymentTrans_Result in lstPaymentTrans)
            {
                _PaymentTransMasterModel = iMapper.Map<tbl_PaymentTrans, PaymentTransMasterModel>(objGetPaymentTrans_Result);
                lstPaymentTransMasterModel.Add(_PaymentTransMasterModel);
            }
            return lstPaymentTransMasterModel;
        }
    }
}
