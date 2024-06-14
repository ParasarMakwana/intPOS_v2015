using SFPOS.DAL.Frontend;
using SFPOS.Entities.FrontEnd;
using System.Collections.Generic;

namespace SFPOS.BAL.Frontend
{
    public class TransSuspendService
    {
        TransSuspendDAL objTransSuspendDAL = new TransSuspendDAL();

        public TransSuspendMasterModel AddTransSuspend(TransSuspendMasterModel objTransSuspendMasterModel, int TransType)
        {
            return objTransSuspendDAL.AddTransSuspend(objTransSuspendMasterModel, TransType);
        }

        public List<TransSuspendMasterModel> GetAllTransSuspendDetail(string TransSuspendCode)
        {
            return objTransSuspendDAL.GetAllTransSuspendDetail(TransSuspendCode);
        }
    }
}
