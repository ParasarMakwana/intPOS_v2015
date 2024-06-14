using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    public class InvoiceService
    {
        InvoiceDAL objInvoiceDAL = new InvoiceDAL();
        public InvoiceMasterModel AddInvoice(InvoiceMasterModel objInvoiceMasterModel, int TransType)
        {
            return objInvoiceDAL.AddInvoice(objInvoiceMasterModel, TransType);
        }
    }
}
