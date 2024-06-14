using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using SFPOS.Entities;
using System.Threading.Tasks;
using AutoMapper;
using SFPOS.DAL;
using SFPOS.Common;
using System.Data.Entity;
using SFPOS.Entities.MasterDataClasses;

namespace SPPOS.DAL
{
    public class DepartmentDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public DepartmentMasterModel AddDepartment(DepartmentMasterModel objDepartmentMasterModel, int TransType)
        {
            try
            {
                tbl_DepartmentMaster objtbl_DepartmentMaster = new tbl_DepartmentMaster();
                if (TransType == 1)//ADD
                {
                    objDepartmentMasterModel.DepartmentID = objDepartmentMasterModel.DepartmentID;
                    objtbl_DepartmentMaster.DepartmentName = objDepartmentMasterModel.DepartmentName;
                    objtbl_DepartmentMaster.AgeVarificationAge = objDepartmentMasterModel.AgeVarificationAge;
                    objtbl_DepartmentMaster.UnitMeasureID = objDepartmentMasterModel.UnitMeasureID;
                    objtbl_DepartmentMaster.TaxGroupID = objDepartmentMasterModel.TaxGroupID;
                    objtbl_DepartmentMaster.DepartmentNo = objDepartmentMasterModel.DepartmentNo;
                    objtbl_DepartmentMaster.SubNo = objDepartmentMasterModel.SubNo;
                    objtbl_DepartmentMaster.IsFoodStamp = objDepartmentMasterModel.IsFoodStamp;
                    objtbl_DepartmentMaster.IsActive = objDepartmentMasterModel.IsActive;
                    objtbl_DepartmentMaster.BtnCode = objDepartmentMasterModel.BtnCode;
                    objtbl_DepartmentMaster.DepartmentBtnIndex = objDepartmentMasterModel.DepartmentBtnIndex;
                    objtbl_DepartmentMaster.DepartmentBtn = objDepartmentMasterModel.DepartmentBtn;
                    objtbl_DepartmentMaster.IsForceTax = objDepartmentMasterModel.IsForceTax;
                    objtbl_DepartmentMaster.ForcedTaxSuffix = objDepartmentMasterModel.ForcedTaxSuffix;
                    objtbl_DepartmentMaster.ForcedTaxSection = objDepartmentMasterModel.ForcedTaxSection;
                    objtbl_DepartmentMaster.CreatedDate = DateTime.Now;
                    objtbl_DepartmentMaster.CreatedBy = LoginInfo.UserId;
                    _db.tbl_DepartmentMaster.Add(objtbl_DepartmentMaster);
                    objDepartmentMasterModel.DepartmentID = objtbl_DepartmentMaster.DepartmentID;
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_DepartmentMaster = _db.tbl_DepartmentMaster.Where(p => p.DepartmentID == objDepartmentMasterModel.DepartmentID).FirstOrDefault();
                    if (objtbl_DepartmentMaster != null)
                    {
                        objtbl_DepartmentMaster.DepartmentID = objDepartmentMasterModel.DepartmentID;
                        objtbl_DepartmentMaster.DepartmentName = objDepartmentMasterModel.DepartmentName;
                        objtbl_DepartmentMaster.AgeVarificationAge = objDepartmentMasterModel.AgeVarificationAge;
                        objtbl_DepartmentMaster.UnitMeasureID = objDepartmentMasterModel.UnitMeasureID;
                        objtbl_DepartmentMaster.TaxGroupID = objDepartmentMasterModel.TaxGroupID;
                        objtbl_DepartmentMaster.DepartmentNo = objDepartmentMasterModel.DepartmentNo;
                        objtbl_DepartmentMaster.SubNo = objDepartmentMasterModel.SubNo;
                        objtbl_DepartmentMaster.IsFoodStamp = objDepartmentMasterModel.IsFoodStamp;
                        objtbl_DepartmentMaster.BtnCode = objDepartmentMasterModel.BtnCode;
                        objtbl_DepartmentMaster.DepartmentBtnIndex = objDepartmentMasterModel.DepartmentBtnIndex;
                        objtbl_DepartmentMaster.DepartmentBtn = objDepartmentMasterModel.DepartmentBtn;
                        objtbl_DepartmentMaster.IsActive = objDepartmentMasterModel.IsActive;
                        objtbl_DepartmentMaster.IsForceTax = objDepartmentMasterModel.IsForceTax;
                        objtbl_DepartmentMaster.ForcedTaxSuffix = objDepartmentMasterModel.ForcedTaxSuffix;
                        objtbl_DepartmentMaster.ForcedTaxSection = objDepartmentMasterModel.ForcedTaxSection;
                        objtbl_DepartmentMaster.UpdatedDate = DateTime.Now;
                        objtbl_DepartmentMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_DepartmentMaster = _db.tbl_DepartmentMaster.Where(p => p.DepartmentID == objDepartmentMasterModel.DepartmentID).FirstOrDefault();
                    if (objtbl_DepartmentMaster != null)
                    {
                        objtbl_DepartmentMaster.DepartmentID = objDepartmentMasterModel.DepartmentID;
                        objtbl_DepartmentMaster.IsDelete = true;
                        objtbl_DepartmentMaster.IsActive = false;
                        objtbl_DepartmentMaster.UpdatedDate = DateTime.Now;
                        objtbl_DepartmentMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objDepartmentMasterModel;
        }

        //List
        public List<DepartmentMasterModel> GetAllDepartment()
        {
            var lstDepartmentMasterModel = new List<DepartmentMasterModel>();

            //var onjtbl_DepartmentMaster = _db.tbl_DepartmentMaster.Where(x => x.IsDelete == false).OrderBy(x => x.DepartmentName).ToList();

            var onjtbl_DepartmentMaster = _db.SP_GetDepartmentList().ToList();

            if (onjtbl_DepartmentMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetDepartmentList_Result, DepartmentMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetDepartmentList_Result objtbl_DepartmentMaster in onjtbl_DepartmentMaster)
                {
                    DepartmentMasterModel _DepartmentMasterModel = iMapper.Map<SP_GetDepartmentList_Result, DepartmentMasterModel>(objtbl_DepartmentMaster);
                    lstDepartmentMasterModel.Add(_DepartmentMasterModel);
                }
            }
            return lstDepartmentMasterModel;
        }

        //Check
        public bool CheckDepartmentName(string DepartmentName, long PrimaryId)
        {
            bool result = false;
            try
            {
                tbl_DepartmentMaster _tbl_DepartmentMaster = _db.tbl_DepartmentMaster.FirstOrDefault(x => x.DepartmentName == DepartmentName && x.IsDelete == false && x.DepartmentID != PrimaryId);
                if (_tbl_DepartmentMaster != null)
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

        //Get Details
        //public DepartmentMasterModel DepartmentDetail(int DepartmentID)
        //{
        //    tbl_DepartmentMaster _tbl_DepartmentMaster = new tbl_DepartmentMaster();
        //    DepartmentMasterModel _DepartmentMasterModel = new DepartmentMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_DepartmentMaster, DepartmentMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _DepartmentMasterModel = iMapper.Map<tbl_DepartmentMaster, DepartmentMasterModel>(_tbl_DepartmentMaster);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _DepartmentMasterModel;
        //}

        public DepartmentMasterModel DeleteAllDepartment(DepartmentMasterModel objDepartmentMasterModel)
        {
            tbl_DepartmentMaster objtbl_DepartmentMaster = new tbl_DepartmentMaster();
            objtbl_DepartmentMaster = _db.tbl_DepartmentMaster.Where(p => p.DepartmentID == objDepartmentMasterModel.DepartmentID).FirstOrDefault();
            if (objtbl_DepartmentMaster != null)
            {
                objtbl_DepartmentMaster.DepartmentID = objDepartmentMasterModel.DepartmentID;
                objtbl_DepartmentMaster.IsDelete = true;
                objtbl_DepartmentMaster.UpdatedDate = DateTime.Now;
                objtbl_DepartmentMaster.UpdatedBy = LoginInfo.UserId;
            }

            var objtbl_SectionMaster = _db.tbl_SectionMaster.Where(p => p.DepartmentID == objDepartmentMasterModel.DepartmentID && p.IsDelete == false).ToList();
            if (objtbl_SectionMaster.Count > 0)
            {
                foreach (tbl_SectionMaster ojtbl_SectionMaster in objtbl_SectionMaster)
                {
                    ojtbl_SectionMaster.IsDelete = true;
                    ojtbl_SectionMaster.UpdatedDate = DateTime.Now;
                    ojtbl_SectionMaster.UpdatedBy = LoginInfo.UserId;
                }
            }
            _db.SaveChanges();
            return objDepartmentMasterModel;
        }

        public bool CheckName(string DepartmentName)
        {
            bool result = false;
            try
            {
                tbl_DepartmentMaster _tbl_DepartmentMaster = _db.tbl_DepartmentMaster.FirstOrDefault(x => x.DepartmentName == DepartmentName && x.IsDelete == false);
                if (_tbl_DepartmentMaster != null)
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

        public bool CheckDepartmentButtonFlag()
        {
            bool result = false;
            try
            {
                var _tbl_DepartmentMaster = _db.tbl_DepartmentMaster.Where(x => x.DepartmentBtn == true && x.IsDelete == false).Count();
                if (_tbl_DepartmentMaster > 10)
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

        public long GetDepartmentID(string DepartmentName)
        {
            long result = 0;
            try
            {
                tbl_DepartmentMaster _tbl_DepartmentMaster = _db.tbl_DepartmentMaster.FirstOrDefault(x => x.DepartmentName == DepartmentName && x.IsDelete == false);
                if (_tbl_DepartmentMaster != null)
                {
                    result = _tbl_DepartmentMaster.DepartmentID;
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
