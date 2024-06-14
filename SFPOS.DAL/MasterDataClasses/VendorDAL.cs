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
    public class VendorDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public VendorMasterModel AddVendor(VendorMasterModel objVendorMasterModel, int TransType)
        {
            try
            {
                tbl_VendorMaster objtbl_VendorMaster = new tbl_VendorMaster();
                if (TransType == 1)//ADD
                {
                    objVendorMasterModel.VendorID = objVendorMasterModel.VendorID;
                    objtbl_VendorMaster.VendorName = objVendorMasterModel.VendorName;
                    objtbl_VendorMaster.Address = objVendorMasterModel.Address;
                    objtbl_VendorMaster.Address2 = objVendorMasterModel.Address2;
                    objtbl_VendorMaster.City = objVendorMasterModel.City;
                    objtbl_VendorMaster.State = objVendorMasterModel.State;
                    objtbl_VendorMaster.ZipCode = objVendorMasterModel.ZipCode;
                    objtbl_VendorMaster.Country = objVendorMasterModel.Country;
                    objtbl_VendorMaster.PhoneNo = objVendorMasterModel.PhoneNo;
                    objtbl_VendorMaster.EmailID = objVendorMasterModel.EmailID;
                    objtbl_VendorMaster.ContactPerson = objVendorMasterModel.ContactPerson;
                    objtbl_VendorMaster.Fax = objVendorMasterModel.Fax;
                    objtbl_VendorMaster.IsActive = true;
                    objtbl_VendorMaster.CreatedDate = DateTime.Now;
                    objtbl_VendorMaster.CreatedBy = LoginInfo.UserId;
                    objtbl_VendorMaster.VendorID = objtbl_VendorMaster.VendorID;
                    _db.tbl_VendorMaster.Add(objtbl_VendorMaster);
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_VendorMaster = _db.tbl_VendorMaster.Where(p => p.VendorID == objVendorMasterModel.VendorID).FirstOrDefault();
                    if (objtbl_VendorMaster != null)
                    {
                        objtbl_VendorMaster.VendorID = objVendorMasterModel.VendorID;
                        objtbl_VendorMaster.VendorName = objVendorMasterModel.VendorName;
                        objtbl_VendorMaster.Address = objVendorMasterModel.Address;
                        objtbl_VendorMaster.Address2 = objVendorMasterModel.Address2;
                        objtbl_VendorMaster.City = objVendorMasterModel.City;
                        objtbl_VendorMaster.State = objVendorMasterModel.State;
                        objtbl_VendorMaster.ZipCode = objVendorMasterModel.ZipCode;
                        objtbl_VendorMaster.Country = objVendorMasterModel.Country;
                        objtbl_VendorMaster.PhoneNo = objVendorMasterModel.PhoneNo;
                        objtbl_VendorMaster.EmailID = objVendorMasterModel.EmailID;
                        objtbl_VendorMaster.ContactPerson = objVendorMasterModel.ContactPerson;
                        objtbl_VendorMaster.Fax = objVendorMasterModel.Fax;
                        objtbl_VendorMaster.IsActive = true;
                        objtbl_VendorMaster.UpdatedDate = DateTime.Now;
                        objtbl_VendorMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_VendorMaster = _db.tbl_VendorMaster.Where(p => p.VendorID == objVendorMasterModel.VendorID).FirstOrDefault();
                    if (objtbl_VendorMaster != null)
                    {
                        objtbl_VendorMaster.VendorID = objVendorMasterModel.VendorID;
                        objtbl_VendorMaster.VendorName = objVendorMasterModel.VendorName;
                        objtbl_VendorMaster.IsDelete = true;
                        objtbl_VendorMaster.UpdatedDate = DateTime.Now;
                        objtbl_VendorMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objVendorMasterModel;
        }

        //List
        public List<VendorMasterModel> GetAllVendor()
        {
            var lstVendorMasterModel = new List<VendorMasterModel>();

            // var onjtbl_VendorMaster = _db.tbl_VendorMaster.Where(x => x.IsDelete == false).ToList();
            var onjtbl_VendorMaster = _db.SP_GetVendorList().ToList();

            if (onjtbl_VendorMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetVendorList_Result, VendorMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetVendorList_Result objtbl_VendorMaster in onjtbl_VendorMaster)
                {
                    VendorMasterModel _VendorMasterModel = iMapper.Map<SP_GetVendorList_Result, VendorMasterModel>(objtbl_VendorMaster);
                    lstVendorMasterModel.Add(_VendorMasterModel);
                }
            }
            return lstVendorMasterModel;
        }

        //Check
        public bool CheckVendorName(string VendorName ,long PrimaryId)
        {
            bool result = false;
            try
            {
                tbl_VendorMaster _tbl_VendorMaster = _db.tbl_VendorMaster.FirstOrDefault(x => x.VendorName == VendorName && x.IsDelete == false && x.VendorID != PrimaryId);
                if (_tbl_VendorMaster != null)
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


        public bool CheckName(string VendorName)
        {
            bool result = false;
            try
            {
                tbl_VendorMaster _tbl_VendorMaster = _db.tbl_VendorMaster.FirstOrDefault(x => x.VendorName == VendorName && x.IsDelete == false);
                if (_tbl_VendorMaster != null)
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

        public long GetVendorID(string VendorName)
        {
            long result = 0;
            try
            {
                tbl_VendorMaster _tbl_VendorMaster = _db.tbl_VendorMaster.FirstOrDefault(x => x.VendorName == VendorName && x.IsDelete == false);
                if (_tbl_VendorMaster != null)
                {
                    result = _tbl_VendorMaster.VendorID;
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }
        //Get Details
        //public VendorMasterModel VendorDetail(int VendorID)
        //{
        //    tbl_VendorMaster _tbl_VendorMaster = new tbl_VendorMaster();
        //    VendorMasterModel _VendorMasterModel = new VendorMasterModel();
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_VendorMaster, VendorMasterModel>(); });
        //        IMapper iMapper = config.CreateMapper();
        //        _VendorMasterModel = iMapper.Map<tbl_VendorMaster, VendorMasterModel>(_tbl_VendorMaster);
        //    }
        //    catch (Exception e)
        //    {
        //        string ex = e.Message;
        //    }
        //    return _VendorMasterModel;
        //}
    }
}
