using SFPOS.DAL.Frontend;
using SFPOS.Entities.FrontEnd;
using System.Collections.Generic;

namespace SFPOS.BAL.Frontend
{
    public class CounterService
    {
        CounterDAL objCounterDAL = new CounterDAL();

        public CounterMasterModel AddCounter(CounterMasterModel objCounterMasterModel, int TransType)
        {
            return objCounterDAL.AddCounter(objCounterMasterModel, TransType);
        }

        public List<CounterMasterModel> GetAllCounter()   
        {
            return objCounterDAL.GetAllCounter();
        }
        public List<CounterMasterModel> GetCounterByMacAddress(string macAddress)
        {
            return objCounterDAL.GetCounterByMacAddress(macAddress);
        }
        public bool DeleteCounterByMacAddress(string macAddress)
        {
            return objCounterDAL.DeleteCounterByMacAddress(macAddress);
        }
    }
}
