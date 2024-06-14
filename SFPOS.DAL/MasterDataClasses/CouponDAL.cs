using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class CouponDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public CouponMasterModel AddCoupon(CouponMasterModel objCouponMasterModel, int TransType)
        {
            try
            {
                tbl_CouponMaster objtbl_CouponMaster = new tbl_CouponMaster();
                if (TransType == 1)//ADD
                {
                    objCouponMasterModel.CouponID = objCouponMasterModel.CouponID;
                    objtbl_CouponMaster.CoupenCode = objCouponMasterModel.CoupenCode;
                    objtbl_CouponMaster.CouponName = objCouponMasterModel.CouponName;
                    objtbl_CouponMaster.StartDate = objCouponMasterModel.StartDate;
                    objtbl_CouponMaster.EndDate = objCouponMasterModel.EndDate;
                    objtbl_CouponMaster.MinPurchaseAmt = objCouponMasterModel.MinPurchaseAmt;
                    objtbl_CouponMaster.Discount = objCouponMasterModel.Discount;
                    objtbl_CouponMaster.UsedCount = objCouponMasterModel.UsedCount;
                    objtbl_CouponMaster.AvailableCount = objCouponMasterModel.AvailableCount;
                    objtbl_CouponMaster.CreatedDate = DateTime.Now;
                    objtbl_CouponMaster.CreatedBy = LoginInfo.UserId;
                    objtbl_CouponMaster.IsActive = true;
                    objtbl_CouponMaster.IsDelete = false;
                    objtbl_CouponMaster.IsAllowMultipleTime = objCouponMasterModel.IsAllowMultipleTime;
                    objtbl_CouponMaster.IsRestricted = objCouponMasterModel.IsRestricted;
                    objtbl_CouponMaster.AllowAllDepartment = objCouponMasterModel.AllowAllDepartment;
                    objtbl_CouponMaster.SelectedDepartment = objCouponMasterModel.SelectedDepartment;
                    _db.tbl_CouponMaster.Add(objtbl_CouponMaster);
                    _db.SaveChanges();
                    objCouponMasterModel.CouponID = objtbl_CouponMaster.CouponID;

                    tbl_CouponAppliedDepartment objtbl_CouponAppliedDepartment = new tbl_CouponAppliedDepartment();

                    foreach(var deparment in objCouponMasterModel.DepartmentID)
                    {
                            //objtbl_CouponAppliedDepartment.CouponAppliedDepID = objCouponMasterModel.CouponAppliedDepID;
                            objtbl_CouponAppliedDepartment.CouponID = objCouponMasterModel.CouponID;
                            objtbl_CouponAppliedDepartment.DepartmentID = deparment;
                            objtbl_CouponAppliedDepartment.IsActive = true;
                            objtbl_CouponAppliedDepartment.CreatedBy = LoginInfo.UserId;
                            objtbl_CouponAppliedDepartment.CreateDate = DateTime.Now;
                            _db.tbl_CouponAppliedDepartment.Add(objtbl_CouponAppliedDepartment);
                            _db.SaveChanges();
                    }
                    
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_CouponMaster = _db.tbl_CouponMaster.Where(p => p.CouponID == objCouponMasterModel.CouponID).FirstOrDefault();
                    if (objtbl_CouponMaster != null)
                    {
                        objtbl_CouponMaster.CouponID = objCouponMasterModel.CouponID;
                        objtbl_CouponMaster.CoupenCode = objCouponMasterModel.CoupenCode;
                        objtbl_CouponMaster.CouponName = objCouponMasterModel.CouponName;
                        objtbl_CouponMaster.StartDate = objCouponMasterModel.StartDate;
                        objtbl_CouponMaster.EndDate = objCouponMasterModel.EndDate;
                        objtbl_CouponMaster.MinPurchaseAmt = objCouponMasterModel.MinPurchaseAmt;
                        objtbl_CouponMaster.Discount = objCouponMasterModel.Discount;
                        objtbl_CouponMaster.UsedCount = objCouponMasterModel.UsedCount;
                        objtbl_CouponMaster.AvailableCount = objCouponMasterModel.AvailableCount;
                        objtbl_CouponMaster.UpdatedDate = DateTime.Now;
                        objtbl_CouponMaster.UpdatedBy = LoginInfo.UserId;
                        objtbl_CouponMaster.IsActive = true;
                        objtbl_CouponMaster.IsDelete = false;
                        objtbl_CouponMaster.IsAllowMultipleTime = objCouponMasterModel.IsAllowMultipleTime;
                        objtbl_CouponMaster.IsRestricted = objCouponMasterModel.IsRestricted;
                        objtbl_CouponMaster.AllowAllDepartment = objCouponMasterModel.AllowAllDepartment;
                        objtbl_CouponMaster.SelectedDepartment = objCouponMasterModel.SelectedDepartment;
                        _db.Entry(objtbl_CouponMaster).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        tbl_CouponAppliedDepartment objtbl_CouponAppliedDepartment = new tbl_CouponAppliedDepartment();

                        // Set all records with the same CouponID to IsActive = false
                        _db.tbl_CouponAppliedDepartment
                            .Where(cad => cad.CouponID == objCouponMasterModel.CouponID)
                            .ToList()
                            .ForEach(record => record.IsActive = false);

                        // Iterate through the selected departments
                        foreach (var department in objCouponMasterModel.DepartmentID)
                        {
                            bool recordExists = _db.tbl_CouponAppliedDepartment
                                .Any(cad => cad.CouponID == objCouponMasterModel.CouponID && cad.DepartmentID == department);

                            objtbl_CouponAppliedDepartment = _db.tbl_CouponAppliedDepartment
                                .Where(cad => cad.CouponID == objCouponMasterModel.CouponID && cad.DepartmentID == department).FirstOrDefault();

                            if (recordExists)
                            {
                                // Existing record: set IsActive to true
                                objtbl_CouponAppliedDepartment.IsActive = true;
                                objtbl_CouponAppliedDepartment.UpdateBy = LoginInfo.UserId;
                                objtbl_CouponAppliedDepartment.UpdatedDate = DateTime.Now;
                                _db.Entry(objtbl_CouponAppliedDepartment).State = System.Data.Entity.EntityState.Modified;
                                _db.SaveChanges();
                            }
                            else
                            {
                                tbl_CouponAppliedDepartment Objtbl_CouponAppliedDepartment = new tbl_CouponAppliedDepartment();
                                // New record: add it
                                //objtbl_CouponAppliedDepartment.CouponAppliedDepID = objCouponMasterModel.CouponAppliedDepID;
                                Objtbl_CouponAppliedDepartment.CouponID = objCouponMasterModel.CouponID;
                                Objtbl_CouponAppliedDepartment.DepartmentID = department;
                                Objtbl_CouponAppliedDepartment.IsActive = true;
                                Objtbl_CouponAppliedDepartment.CreatedBy = LoginInfo.UserId;
                                Objtbl_CouponAppliedDepartment.CreateDate = DateTime.Now;
                                Objtbl_CouponAppliedDepartment.UpdateBy = LoginInfo.UserId;
                                Objtbl_CouponAppliedDepartment.UpdatedDate = DateTime.Now;
                                _db.tbl_CouponAppliedDepartment.Add(Objtbl_CouponAppliedDepartment);
                                _db.SaveChanges();
                            }
                        }

                        // Save changes to the database
                        

                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_CouponMaster = _db.tbl_CouponMaster.Where(p => p.CouponID == objCouponMasterModel.CouponID).FirstOrDefault();
                    if (objtbl_CouponMaster != null)
                    {
                        objtbl_CouponMaster.UsedCount = objCouponMasterModel.UsedCount;
                      
                        if (objCouponMasterModel.UsedCount == objCouponMasterModel.AvailableCount)
                        {
                            objtbl_CouponMaster.IsDelete = true;
                            objtbl_CouponMaster.IsActive = false;
                        }
                        else
                        {
                            objtbl_CouponMaster.IsDelete = false;
                            objtbl_CouponMaster.IsActive = true;
                        }
                        objtbl_CouponMaster.UpdatedDate = DateTime.Now;
                        objtbl_CouponMaster.UpdatedBy = LoginInfo.UserId;
                        _db.Entry(objtbl_CouponMaster).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objCouponMasterModel;
        }

        public List<CouponMasterModel> GetAllCoupon()
        {
            var lstCouponMasterModel = new List<CouponMasterModel>();

            var onjtbl_CouponMaster = _db.tbl_CouponMaster.Where(x => x.IsDelete == false).OrderByDescending(x => x.CouponID).AsNoTracking().ToList();
            if (onjtbl_CouponMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_CouponMaster, CouponMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_CouponMaster objtbl_CouponMaster in onjtbl_CouponMaster)
                {
                    CouponMasterModel _CouponMasterModel = iMapper.Map<tbl_CouponMaster, CouponMasterModel>(objtbl_CouponMaster);
                    lstCouponMasterModel.Add(_CouponMasterModel);
                }
            }
            return lstCouponMasterModel;
        }

        public List<CouponMasterModel> GetCouponCode(string CoupenCode)
        {
            var lstCouponMasterModel = new List<CouponMasterModel>();
            var onjtbl_CouponMaster = _db.tbl_CouponMaster.Where(x => x.IsDelete == false && x.CoupenCode == CoupenCode && x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).OrderByDescending(x => x.CouponID).ToList();
            if (onjtbl_CouponMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_CouponMaster, CouponMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_CouponMaster objtbl_CouponMaster in onjtbl_CouponMaster)
                {
                    CouponMasterModel _CouponMasterModel = iMapper.Map<tbl_CouponMaster, CouponMasterModel>(objtbl_CouponMaster);
                    lstCouponMasterModel.Add(_CouponMasterModel);
                }
            }
            return lstCouponMasterModel;
        }

    }
}
