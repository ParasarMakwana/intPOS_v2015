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
    public class UoMDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        //ADD // EDIT // DELETE
        public UoMMasterModel AddEditDeleteUOM(UoMMasterModel objUoMMasterModel, int TransType)
        {
            try
            {
                tbl_UnitMeasureMaster objtbl_UnitMeasureMaster = new tbl_UnitMeasureMaster();
                if (TransType == 1)//ADD
                {
                    objUoMMasterModel.UnitMeasureID = objUoMMasterModel.UnitMeasureID;
                    objtbl_UnitMeasureMaster.UnitMeasureCode = objUoMMasterModel.UnitMeasureCode;
                    objtbl_UnitMeasureMaster.Description = objUoMMasterModel.Description;
                    objtbl_UnitMeasureMaster.IsActive = true;
                    objtbl_UnitMeasureMaster.CreatedDate = DateTime.Now;
                    objtbl_UnitMeasureMaster.CreatedBy = LoginInfo.UserId;
                    _db.tbl_UnitMeasureMaster.Add(objtbl_UnitMeasureMaster);
                    objUoMMasterModel.UnitMeasureID = objtbl_UnitMeasureMaster.UnitMeasureID;
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_UnitMeasureMaster = _db.tbl_UnitMeasureMaster.Where(p => p.UnitMeasureID == objUoMMasterModel.UnitMeasureID).FirstOrDefault();
                    if (objtbl_UnitMeasureMaster != null)
                    {
                        objtbl_UnitMeasureMaster.UnitMeasureID = objUoMMasterModel.UnitMeasureID;
                        objtbl_UnitMeasureMaster.UnitMeasureCode = objUoMMasterModel.UnitMeasureCode;
                        objtbl_UnitMeasureMaster.Description = objUoMMasterModel.Description;
                        objtbl_UnitMeasureMaster.UpdatedDate = DateTime.Now;
                        objtbl_UnitMeasureMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_UnitMeasureMaster = _db.tbl_UnitMeasureMaster.Where(p => p.UnitMeasureID == objUoMMasterModel.UnitMeasureID).FirstOrDefault();
                    if (objtbl_UnitMeasureMaster != null)
                    {
                        objtbl_UnitMeasureMaster.UnitMeasureID = objUoMMasterModel.UnitMeasureID;
                        objtbl_UnitMeasureMaster.UnitMeasureCode = objUoMMasterModel.UnitMeasureCode;
                        objtbl_UnitMeasureMaster.IsDelete = true;
                        objtbl_UnitMeasureMaster.UpdatedDate = DateTime.Now;
                        objtbl_UnitMeasureMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objUoMMasterModel;
        }

        //List
        public List<UoMMasterModel> GetAllUoM()
        {
            var lstUoMMasterModel = new List<UoMMasterModel>();

            // var onjtbl_UnitMeasureMaster = _db.tbl_UnitMeasureMaster.Where(x => x.IsDelete == false).ToList();
            var onjtbl_UnitMeasureMaster = _db.SP_GetUoMList().ToList();

            if (onjtbl_UnitMeasureMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetUoMList_Result, UoMMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetUoMList_Result objtbl_UnitMeasureMaster in onjtbl_UnitMeasureMaster)
                {
                    UoMMasterModel _UoMMasterModel = iMapper.Map<SP_GetUoMList_Result, UoMMasterModel>(objtbl_UnitMeasureMaster);
                    lstUoMMasterModel.Add(_UoMMasterModel);
                }
            }
            return lstUoMMasterModel;
        }

        //Check
        public bool CheckUoMName(string UoMName,int PrimaryId)
        {
            bool result = false;
            try
            {
                tbl_UnitMeasureMaster _tbl_UnitMeasureMaster = _db.tbl_UnitMeasureMaster.FirstOrDefault(x => x.UnitMeasureCode == UoMName && x.IsDelete == false && x.UnitMeasureID != PrimaryId);
                if (_tbl_UnitMeasureMaster != null)
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

        public bool CheckName(string UnitMeasureType)
        {
            bool result = false;
            try
            {
                tbl_UnitMeasureMaster _tbl_UnitMeasureMaster = _db.tbl_UnitMeasureMaster.FirstOrDefault(x => x.UnitMeasureCode == UnitMeasureType && x.IsDelete == false);
                if (_tbl_UnitMeasureMaster != null)
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

        public long GetUnitMeasureID(string UnitMeasureType)
        {
            long result = 0;
            try
            {
                tbl_UnitMeasureMaster _tbl_UnitMeasureMaster = _db.tbl_UnitMeasureMaster.FirstOrDefault(x => x.UnitMeasureCode == UnitMeasureType && x.IsDelete == false);
                if (_tbl_UnitMeasureMaster != null)
                {
                    result = _tbl_UnitMeasureMaster.UnitMeasureID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        //Get Details
        //public UoMMasterModel UoMDetail(int UoMID)
        //{
        //    tbl_UnitMeasureMaster _tbl_UnitMeasureMaster = new tbl_UnitMeasureMaster();
        //    UoMMasterModel _UoMMasterModel = new UoMMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_UnitMeasureMaster, UoMMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _UoMMasterModel = iMapper.Map<tbl_UnitMeasureMaster, UoMMasterModel>(_tbl_UnitMeasureMaster);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _UoMMasterModel;
        //}
    }
}
