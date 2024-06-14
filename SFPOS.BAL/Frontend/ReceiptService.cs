using SFPOS.DAL;
using SFPOS.DAL.Frontend;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.Frontend
{
    public class ReceiptService
    {
        ReceiptDAL objReceiptDAL = new ReceiptDAL();
        public List<ReciptDetails_ResultModel> GetReceiptDetails(long OrderID,long EmployeeID,bool rePrint)
        {
            return objReceiptDAL.GetReceiptDetails(OrderID,EmployeeID, rePrint);
        }

        public List<CashierReceipt_ResultModel> GetCashierReceipt()
        {
            return objReceiptDAL.GetCashierReceipt();
        }
    }
}
