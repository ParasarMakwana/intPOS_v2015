using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class StoreDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public StoreMasterModel AddStore(StoreMasterModel objStoreMasterModel, int TransType)
        {
            try
            {
                tbl_StoreMaster objtbl_StoreMaster = new tbl_StoreMaster();
                if (TransType == 1)//ADD
                {
                    objStoreMasterModel.StoreID = objStoreMasterModel.StoreID;
                    objtbl_StoreMaster.StoreName = objStoreMasterModel.StoreName;
                    objtbl_StoreMaster.Address = objStoreMasterModel.Address;
                    objtbl_StoreMaster.Address2 = objStoreMasterModel.Address2;
                    objtbl_StoreMaster.City = objStoreMasterModel.City;
                    objtbl_StoreMaster.State = objStoreMasterModel.State;
                    objtbl_StoreMaster.ZipCode = objStoreMasterModel.ZipCode;
                    objtbl_StoreMaster.Country = objStoreMasterModel.Country;
                    objtbl_StoreMaster.Phone = objStoreMasterModel.Phone;
                    objtbl_StoreMaster.Fax = objStoreMasterModel.Fax;
                    objtbl_StoreMaster.AgeVarificationAge = objStoreMasterModel.AgeVarificationAge;
                    objtbl_StoreMaster.DefaultTax = objStoreMasterModel.DefaultTax;
                    objtbl_StoreMaster.Disclaimer = objStoreMasterModel.Disclaimer;
                    objtbl_StoreMaster.IsActive = true;
                    objtbl_StoreMaster.CreatedDate = DateTime.Now;
                    objtbl_StoreMaster.CreatedBy = LoginInfo.UserId;
                    objtbl_StoreMaster.IsStoreTax = objStoreMasterModel.IsStoreTax;
                    objStoreMasterModel.StoreID = objtbl_StoreMaster.StoreID;
                    _db.tbl_StoreMaster.Add(objtbl_StoreMaster);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_StoreMaster = _db.tbl_StoreMaster.Where(p => p.StoreID == objStoreMasterModel.StoreID).FirstOrDefault();
                    if (objtbl_StoreMaster != null)
                    {
                        objtbl_StoreMaster.StoreID = objStoreMasterModel.StoreID;
                        objtbl_StoreMaster.StoreName = objStoreMasterModel.StoreName;
                        objtbl_StoreMaster.Address = objStoreMasterModel.Address;
                        objtbl_StoreMaster.Address2 = objStoreMasterModel.Address2;
                        objtbl_StoreMaster.City = objStoreMasterModel.City;
                        objtbl_StoreMaster.State = objStoreMasterModel.State;
                        objtbl_StoreMaster.ZipCode = objStoreMasterModel.ZipCode;
                        objtbl_StoreMaster.Country = objStoreMasterModel.Country;
                        objtbl_StoreMaster.Phone = objStoreMasterModel.Phone;
                        objtbl_StoreMaster.Fax = objStoreMasterModel.Fax;
                        objtbl_StoreMaster.AgeVarificationAge = objStoreMasterModel.AgeVarificationAge;
                        objtbl_StoreMaster.DefaultTax = objStoreMasterModel.DefaultTax;
                        objtbl_StoreMaster.Disclaimer = objStoreMasterModel.Disclaimer;
                        objtbl_StoreMaster.IsStoreTax = objStoreMasterModel.IsStoreTax;
                        objtbl_StoreMaster.IsActive = true;
                        objtbl_StoreMaster.UpdatedDate = DateTime.Now;
                        objtbl_StoreMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_StoreMaster = _db.tbl_StoreMaster.Where(p => p.StoreID == objStoreMasterModel.StoreID).FirstOrDefault();
                    if (objtbl_StoreMaster != null)
                    {
                        objtbl_StoreMaster.StoreID = objStoreMasterModel.StoreID;
                        objtbl_StoreMaster.IsDelete = true;
                        objtbl_StoreMaster.UpdatedDate = DateTime.Now;
                        objtbl_StoreMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objStoreMasterModel;
        }

        //List
        public List<StoreMasterModel> GetAllStore()
        {
            var lstStoreMasterModel = new List<StoreMasterModel>();
            //var onjtbl_StoreMaster = _db.tbl_StoreMaster.Where(x => x.IsDelete == false).OrderBy(x => x.StoreName).ToList();
            var onjtbl_StoreMaster = _db.SP_GetStoreList().ToList();
            if (onjtbl_StoreMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetStoreList_Result, StoreMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetStoreList_Result objtbl_StoreMaster in onjtbl_StoreMaster)
                {
                    StoreMasterModel _StoreMasterModel = iMapper.Map<SP_GetStoreList_Result, StoreMasterModel>(objtbl_StoreMaster);
                    lstStoreMasterModel.Add(_StoreMasterModel);
                }
            }
            return lstStoreMasterModel;
        }

        //Check
        public bool CheckStoreName(string StoreName,long StoreId)
        {
            bool result = false;
            try
            {
                tbl_StoreMaster _tbl_StoreMaster = _db.tbl_StoreMaster.FirstOrDefault(x => x.StoreName == StoreName && x.IsDelete == false && x.StoreID != StoreId);
                if (_tbl_StoreMaster != null)
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

        public bool CheckName(string StoreName)
        {
            bool result = false;
            try
            {
                tbl_StoreMaster _tbl_StoreMaster = _db.tbl_StoreMaster.FirstOrDefault(x => x.StoreName == StoreName && x.IsDelete == false);
                if (_tbl_StoreMaster != null)
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

        public long GetStoreID(string StoreName)
        {
            long result = 0;
            try
            {
                tbl_StoreMaster _tbl_StoreMaster = _db.tbl_StoreMaster.FirstOrDefault(x => x.StoreName == StoreName && x.IsDelete == false);
                if (_tbl_StoreMaster != null)
                {
                    result = _tbl_StoreMaster.StoreID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }
        //Get Details
        //public StoreMasterModel StoreDetail(int StoreID)
        //{
        //    tbl_StoreMaster _tbl_StoreMaster = new tbl_StoreMaster();
        //    StoreMasterModel _StoreMasterModel = new StoreMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_StoreMaster, StoreMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _StoreMasterModel = iMapper.Map<tbl_StoreMaster, StoreMasterModel>(_tbl_StoreMaster);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _StoreMasterModel;
        //}
    }
}
