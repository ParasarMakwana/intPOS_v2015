using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    public class InvoiceDetailService
    {
       InvoiceDetailDAL objInvoiceDetailDAL = new InvoiceDetailDAL();
        public List<InvoiceDetails_ResultModel> GetInvoiceDetails(long PONumber)
        {
            return objInvoiceDetailDAL.GetInvoiceDetails(PONumber);
        }
    }
}
