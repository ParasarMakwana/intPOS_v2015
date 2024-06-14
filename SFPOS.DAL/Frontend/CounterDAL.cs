using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.Frontend
{
    public class CounterDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public CounterMasterModel AddCounter(CounterMasterModel objCounterMasterModel, int TransType)
        {
            try
            {
                tbl_CounterMaster objtbl_CounterMaster = new tbl_CounterMaster();
                if (TransType == 1)//ADD
                {
                    objCounterMasterModel.CounterID = objCounterMasterModel.CounterID;
                    objtbl_CounterMaster.CounterIP = objCounterMasterModel.CounterIP;
                    objtbl_CounterMaster.StoreID = LoginInfo.StoreID;
                    _db.tbl_CounterMaster.Add(objtbl_CounterMaster);
                    objCounterMasterModel.CounterID = objtbl_CounterMaster.CounterID;
                }
                else if (TransType == 2)//Update
                {
                    objtbl_CounterMaster = _db.tbl_CounterMaster.Where(p => p.CounterID == objCounterMasterModel.CounterID).FirstOrDefault();
                    if (objtbl_CounterMaster != null)
                    {
                        objtbl_CounterMaster.IsLTSystemActive = objCounterMasterModel.IsLTSystemActive;
                        _db.Entry(objtbl_CounterMaster).State = EntityState.Modified;
                        //objtbl_CounterMaster.LogoutTime = DateTime.Now;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objCounterMasterModel;
        }

        public List<CounterMasterModel> GetAllCounter()
        {
            var lstCounterMasterModel = new List<CounterMasterModel>();

            //var onjtbl_CounterMaster = _db.tbl_CounterMaster.Where(x => x.StoreID == LoginInfo.StoreID).OrderBy(x => x.CounterNo).ToList();

            var onjtbl_CounterMaster = _db.tbl_CounterMaster.Where(x => x.IsActive == true).OrderBy(x => x.CounterIP).OrderBy(x => x.CounterNo).AsNoTracking().ToList();

            if (onjtbl_CounterMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_CounterMaster, CounterMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_CounterMaster objtbl_CounterMaster in onjtbl_CounterMaster)
                {
                    CounterMasterModel _CounterMasterModel = iMapper.Map<tbl_CounterMaster, CounterMasterModel>(objtbl_CounterMaster);
                    lstCounterMasterModel.Add(_CounterMasterModel);
                }
            }
            return lstCounterMasterModel;
        }
        public List<CounterMasterModel> GetCounterByMacAddress(string macAddress)
        {
            var lstCounterMasterModel = new List<CounterMasterModel>();

            var onjtbl_CounterMaster = _db.tbl_CounterMaster.Where(x => x.MacAddress == macAddress && x.IsActive == true).OrderBy(x => x.CounterIP).OrderBy(x => x.CounterNo).AsNoTracking().ToList();

            if (onjtbl_CounterMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_CounterMaster, CounterMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_CounterMaster objtbl_CounterMaster in onjtbl_CounterMaster)
                {
                    CounterMasterModel _CounterMasterModel = iMapper.Map<tbl_CounterMaster, CounterMasterModel>(objtbl_CounterMaster);
                    lstCounterMasterModel.Add(_CounterMasterModel);
                }
            }
            return lstCounterMasterModel;
        }
        public bool DeleteCounterByMacAddress(string macAddress)
        {
            bool Status = false;

            var onjtbl_CounterMaster = _db.SP_RemoveCounter(macAddress);
            if (onjtbl_CounterMaster > 0)
            {
                Status = true;
            }
            return Status;
        }

    }
}
