using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System.Collections.Generic;

namespace SFPOS.BAL.MasterDataServices
{
    public class StateService
    {
        StateDAL objStateDAL = new StateDAL();
        public List<StateMasterModel> GetAllState(int countryid)
        {
            return objStateDAL.GetAllState(countryid);
        }

        public List<StateMasterModel> GetState()
        {
            return objStateDAL.GetState();
        }
        public bool CheckName(string StateName)
        {
            return objStateDAL.CheckName(StateName);
        }

        public long GetStateID(string StateName)
        {
            return objStateDAL.GetStateID(StateName);
        }
    }
}
