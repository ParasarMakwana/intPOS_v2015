using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class InvoiceDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD
        public InvoiceMasterModel AddInvoice(InvoiceMasterModel objInvoiceMasterModel, int TransType)
        {
            try
            {
                tbl_InvoiceMaster objtbl_InvoiceMaster = new tbl_InvoiceMaster();
                if (TransType == 1)
                {
                    objInvoiceMasterModel.InvoiceID = objInvoiceMasterModel.InvoiceID;
                    objtbl_InvoiceMaster.PostedPurchaseHeaderID = objInvoiceMasterModel.PostedPurchaseHeaderID;
                    objtbl_InvoiceMaster.PONumber = objInvoiceMasterModel.PONumber;
                    objtbl_InvoiceMaster.Invoice_Number = objInvoiceMasterModel.Invoice_Number;
                    objtbl_InvoiceMaster.Date = objInvoiceMasterModel.Date;
                    objtbl_InvoiceMaster.ShippedBy = objInvoiceMasterModel.ShippedBy;
                    objtbl_InvoiceMaster.ReceivedBy = objInvoiceMasterModel.ReceivedBy;
                    objtbl_InvoiceMaster.TotalAmount = objInvoiceMasterModel.TotalAmount;
                    objtbl_InvoiceMaster.Adjustment = objInvoiceMasterModel.Adjustment;
                    objtbl_InvoiceMaster.StoreID = LoginInfo.StoreID;
                    objtbl_InvoiceMaster.CreatedDate = DateTime.Now;
                    objtbl_InvoiceMaster.CreatedBy = LoginInfo.UserId;
                    _db.tbl_InvoiceMaster.Add(objtbl_InvoiceMaster);
                    objInvoiceMasterModel.InvoiceID = objtbl_InvoiceMaster.InvoiceID;
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objInvoiceMasterModel;
        }

    }
}
