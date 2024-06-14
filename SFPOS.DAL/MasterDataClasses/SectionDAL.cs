using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.spDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class SectionDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public SectionMasterModel AddSection(SectionMasterModel objSectionMasterModel, int TransType)
        {
            try
            {
                tbl_SectionMaster objtbl_SectionMaster = new tbl_SectionMaster();
                if (TransType == 1)//ADD
                {
                    objtbl_SectionMaster.DepartmentID = objSectionMasterModel.DepartmentID;
                    objSectionMasterModel.SectionID = objSectionMasterModel.SectionID;
                    objtbl_SectionMaster.SectionName = objSectionMasterModel.SectionName;
                    objtbl_SectionMaster.AgeVarificationAge = objSectionMasterModel.AgeVarificationAge;
                    objtbl_SectionMaster.TaxGroupID = objSectionMasterModel.TaxGroupID;
                    objtbl_SectionMaster.IsActive = true;
                    objtbl_SectionMaster.CreatedDate = DateTime.Now;
                    objtbl_SectionMaster.CreatedBy = LoginInfo.UserId;
                    _db.tbl_SectionMaster.Add(objtbl_SectionMaster);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_SectionMaster = _db.tbl_SectionMaster.Where(p => p.SectionID == objSectionMasterModel.SectionID).FirstOrDefault();
                    if (objtbl_SectionMaster != null)
                    {
                        objtbl_SectionMaster.SectionID = objSectionMasterModel.SectionID;
                        objtbl_SectionMaster.SectionName = objSectionMasterModel.SectionName;
                        objtbl_SectionMaster.AgeVarificationAge = objSectionMasterModel.AgeVarificationAge;
                        objtbl_SectionMaster.TaxGroupID = objSectionMasterModel.TaxGroupID;
                        objtbl_SectionMaster.DepartmentID = objSectionMasterModel.DepartmentID;
                        objtbl_SectionMaster.UpdatedDate = DateTime.Now;
                        objtbl_SectionMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_SectionMaster = _db.tbl_SectionMaster.Where(p => p.SectionID == objSectionMasterModel.SectionID).FirstOrDefault();
                    if (objtbl_SectionMaster != null)
                    {
                        objtbl_SectionMaster.SectionID = objSectionMasterModel.SectionID;
                        objtbl_SectionMaster.SectionName = objSectionMasterModel.SectionName;
                        objtbl_SectionMaster.AgeVarificationAge = objSectionMasterModel.AgeVarificationAge;

                        objtbl_SectionMaster.IsDelete = true;
                        objtbl_SectionMaster.UpdatedDate = DateTime.Now;
                        objtbl_SectionMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objSectionMasterModel;
        }

        //List
        public List<SectionMasterModel> GetAllSection()
        {
            var lstSectionMasterModel = new List<SectionMasterModel>();

            var objtbl_SectionMaster = _db.tbl_SectionMaster.Where(x => x.IsDelete == false).ToList();
           

            if (objtbl_SectionMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_SectionMaster, SectionMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_SectionMaster obj_tbl_SectionMaster in objtbl_SectionMaster)
                {
                    SectionMasterModel _SectionMasterModel = iMapper.Map<tbl_SectionMaster, SectionMasterModel>(obj_tbl_SectionMaster);
                    lstSectionMasterModel.Add(_SectionMasterModel);
                }
            }
            return lstSectionMasterModel;
        }
        
        //Check
        public bool CheckSectionName(string SectionName)
        {
            bool result = false;
            try
            {
                tbl_SectionMaster _tbl_SectionMaster = _db.tbl_SectionMaster.FirstOrDefault(x => x.SectionName == SectionName && x.IsDelete == false);
                if (_tbl_SectionMaster != null)
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
        public List<SP_GetSectionList_Result_Model> SectionDetail(int DepartmentID)
        {
            var lstSectionMasterModel = new List<SP_GetSectionList_Result_Model>();
            List<SP_GetSectionList_Result_Model> lst = new List<SP_GetSectionList_Result_Model>();
            // var objtbl_SectionMaster = _db.tbl_SectionMaster.Where(x => x.IsDelete == false && x.CategoryID == CategoryID).ToList();
            var objSectionMaster = _db.SP_GetSectionList(DepartmentID).ToList();
            
            try
            {
                if (objSectionMaster.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetSectionList_Result, DepartmentMasterModel>(); });
                    IMapper iMapper = config.CreateMapper();
                    foreach (SP_GetSectionList_Result obj_SectionMaster in objSectionMaster)
                    {
                        SP_GetSectionList_Result_Model _SP_GetSectionList_Result_Model = iMapper.Map<SP_GetSectionList_Result, SP_GetSectionList_Result_Model>(obj_SectionMaster);
                        lstSectionMasterModel.Add(_SP_GetSectionList_Result_Model);
                    }
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return lstSectionMasterModel;
        }


        public bool CheckName(string SectionName)
        {
            bool result = false;
            try
            {
                tbl_SectionMaster _tbl_SectionMaster = _db.tbl_SectionMaster.FirstOrDefault(x => x.SectionName == SectionName && x.IsDelete == false);
                if (_tbl_SectionMaster != null)
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

        public long GetSectionID(string SectionName)
        {
            long result = 0;
            try
            {
                tbl_SectionMaster _tbl_SectionMaster = _db.tbl_SectionMaster.FirstOrDefault(x => x.SectionName == SectionName && x.IsDelete == false);
                if (_tbl_SectionMaster != null)
                {
                    result = _tbl_SectionMaster.SectionID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        //Get Details
        public SectionMasterModel SectionDetailBySecID(long SectionID)
        {
            tbl_SectionMaster _tbl_SectionMaster = new tbl_SectionMaster();
            SectionMasterModel _SectionMasterModel = new SectionMasterModel();
            _tbl_SectionMaster = _db.tbl_SectionMaster.FirstOrDefault(x => x.SectionID == SectionID && x.IsActive == true);
            try
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_SectionMaster, SectionMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                _SectionMasterModel = iMapper.Map<tbl_SectionMaster, SectionMasterModel>(_tbl_SectionMaster);
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return _SectionMasterModel;
        }

    }
}
