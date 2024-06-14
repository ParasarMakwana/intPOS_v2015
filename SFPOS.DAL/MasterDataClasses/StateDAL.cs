using AutoMapper;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFPOS.Common;

namespace SFPOS.DAL.MasterDataClasses
{
    public class StateDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public List<StateMasterModel> GetAllState(int countryid)
        {
            var lstStateMasterModel = new List<StateMasterModel>();

            var onjtbl_State = _db.tbl_State.Where(x => x.CountryID == countryid).ToList();

            if (onjtbl_State.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_State, StateMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_State objtbl_State in onjtbl_State)
                {
                    StateMasterModel _StateMasterModel = iMapper.Map<tbl_State, StateMasterModel>(objtbl_State);
                    lstStateMasterModel.Add(_StateMasterModel);
                }
            }
            return lstStateMasterModel;
        }

        public List<StateMasterModel> GetState()
        {
            var lstStateMasterModel = new List<StateMasterModel>();

            var onjtbl_State = _db.tbl_State.OrderBy(x => x.StateName).ToList();

            if (onjtbl_State.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_State, StateMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_State objtbl_State in onjtbl_State)
                {
                    StateMasterModel _StateMasterModel = iMapper.Map<tbl_State, StateMasterModel>(objtbl_State);
                    lstStateMasterModel.Add(_StateMasterModel);
                }
            }
            return lstStateMasterModel;
        }
        public bool CheckName(string StateName)
        {
            bool result = false;
            try
            {
                tbl_State _tbl_State = _db.tbl_State.FirstOrDefault(x => x.StateName == StateName);
                if (_tbl_State != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        public long GetStateID(string StateName)
        {
            long result = 0;
            try
            {
                tbl_State _tbl_State = _db.tbl_State.FirstOrDefault(x => x.StateName == StateName);
                if (_tbl_State != null)
                {
                    result = _tbl_State.StateID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }
    }
}
