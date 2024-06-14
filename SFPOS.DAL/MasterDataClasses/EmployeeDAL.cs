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
    public class EmployeeDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public EmployeeMasterModel AddEmployee(EmployeeMasterModel objEmployeeMasterModel, int TransType)
        {
            try
            {
                tbl_EmployeeMaster objtbl_EmployeeMaster = new tbl_EmployeeMaster();
                if (TransType == 1)//ADD
                {
                    objEmployeeMasterModel.EmployeeID = objEmployeeMasterModel.EmployeeID;
                    objtbl_EmployeeMaster.FirstName = objEmployeeMasterModel.FirstName;
                    objtbl_EmployeeMaster.LastName = objEmployeeMasterModel.LastName;
                    objtbl_EmployeeMaster.EmailID = objEmployeeMasterModel.EmailID;
                    objtbl_EmployeeMaster.Password = objEmployeeMasterModel.Password;
                    objtbl_EmployeeMaster.PhoneNo = objEmployeeMasterModel.PhoneNo;
                    objtbl_EmployeeMaster.StoreID = objEmployeeMasterModel.StoreID;
                    objtbl_EmployeeMaster.RoleID = objEmployeeMasterModel.RoleID;
                    objtbl_EmployeeMaster.IsActive = true;
                    objtbl_EmployeeMaster.CreatedDate = DateTime.Now;
                    objtbl_EmployeeMaster.CreatedBy = LoginInfo.UserId;
                    objtbl_EmployeeMaster.MaxVoidAmount = objEmployeeMasterModel.MaxVoidAmount;
                    objtbl_EmployeeMaster.BirthDate = objEmployeeMasterModel.BirthDate;
                    objtbl_EmployeeMaster.IsCashPayout = false;
                    _db.tbl_EmployeeMaster.Add(objtbl_EmployeeMaster);
                    objEmployeeMasterModel.EmployeeID = objtbl_EmployeeMaster.EmployeeID;
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_EmployeeMaster = _db.tbl_EmployeeMaster.Where(p => p.EmployeeID == objEmployeeMasterModel.EmployeeID).FirstOrDefault();
                    if (objtbl_EmployeeMaster != null)
                    {
                        objtbl_EmployeeMaster.EmployeeID = objEmployeeMasterModel.EmployeeID;
                        objtbl_EmployeeMaster.FirstName = objEmployeeMasterModel.FirstName;
                        objtbl_EmployeeMaster.LastName = objEmployeeMasterModel.LastName;
                        objtbl_EmployeeMaster.EmailID = objEmployeeMasterModel.EmailID;
                        objtbl_EmployeeMaster.Password = objEmployeeMasterModel.Password;
                        objtbl_EmployeeMaster.PhoneNo = objEmployeeMasterModel.PhoneNo;
                        objtbl_EmployeeMaster.StoreID = objEmployeeMasterModel.StoreID;
                        objtbl_EmployeeMaster.RoleID = objEmployeeMasterModel.RoleID;
                        objtbl_EmployeeMaster.IsActive = objEmployeeMasterModel.IsActive;
                        objtbl_EmployeeMaster.MaxVoidAmount = objEmployeeMasterModel.MaxVoidAmount;
                        objtbl_EmployeeMaster.BirthDate = objEmployeeMasterModel.BirthDate;
                        objtbl_EmployeeMaster.UpdatedDate = DateTime.Now;
                        objtbl_EmployeeMaster.UpdatedBy = LoginInfo.UserId;
                        objtbl_EmployeeMaster.IsCashPayout = (objtbl_EmployeeMaster.IsCashPayout.ToString() == "" ? false : objtbl_EmployeeMaster.IsCashPayout);
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_EmployeeMaster = _db.tbl_EmployeeMaster.Where(p => p.EmployeeID == objEmployeeMasterModel.EmployeeID).FirstOrDefault();
                    if (objtbl_EmployeeMaster != null)
                    {
                        objtbl_EmployeeMaster.EmployeeID = objEmployeeMasterModel.EmployeeID;
                        objtbl_EmployeeMaster.FirstName = objEmployeeMasterModel.FirstName;
                        objtbl_EmployeeMaster.IsDelete = true;
                        objtbl_EmployeeMaster.UpdatedDate = DateTime.Now;
                        objtbl_EmployeeMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 4) // update IsCashPayout flag
                {
                    objtbl_EmployeeMaster = _db.tbl_EmployeeMaster.Where(p => p.EmployeeID == objEmployeeMasterModel.EmployeeID).FirstOrDefault();
                    if(objtbl_EmployeeMaster != null)
                    {
                        objtbl_EmployeeMaster.IsCashPayout = objEmployeeMasterModel.IsCashPayout;
                        objtbl_EmployeeMaster.UpdatedDate = DateTime.Now;
                        objtbl_EmployeeMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 5) // update IsLottoFunction flag
                {
                    objtbl_EmployeeMaster = _db.tbl_EmployeeMaster.Where(p => p.EmployeeID == objEmployeeMasterModel.EmployeeID).FirstOrDefault();
                    if (objtbl_EmployeeMaster != null)
                    {
                        objtbl_EmployeeMaster.IsLottoFunction = objEmployeeMasterModel.IsLottoFunction;
                        objtbl_EmployeeMaster.UpdatedDate = DateTime.Now;
                        objtbl_EmployeeMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objEmployeeMasterModel;
        }

        //List
        public List<EmployeeMasterModel> GetAllEmployee()
        {
            var lstEmployeeMasterModel = new List<EmployeeMasterModel>();

            // var onjtbl_EmployeeMaster = _db.tbl_EmployeeMaster.Where(x => x.IsDelete == false).OrderBy(x => x.FirstName).ToList();
           
            var onjtbl_EmployeeMaster = _db.SP_GetEmployeeList().ToList(); 
            if (onjtbl_EmployeeMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetEmployeeList_Result, EmployeeMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetEmployeeList_Result objtbl_EmployeeMaster in onjtbl_EmployeeMaster)
                {
                    EmployeeMasterModel _EmployeeMasterModel = iMapper.Map<SP_GetEmployeeList_Result, EmployeeMasterModel>(objtbl_EmployeeMaster);
                    lstEmployeeMasterModel.Add(_EmployeeMasterModel);
                }
            }
            return lstEmployeeMasterModel;
        }

        public List<EmployeeMasterModel> GetAllEmployeeByTransDate(DateTime Datval)
        {
            var lstEmployeeMasterModel = new List<EmployeeMasterModel>();

            // var onjtbl_EmployeeMaster = _db.tbl_EmployeeMaster.Where(x => x.IsDelete == false).OrderBy(x => x.FirstName).ToList();

            var onjtbl_EmployeeMaster = _db.SP_GetEmployeeList_ByTransDate(Datval).ToList();
            if (onjtbl_EmployeeMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetEmployeeList_ByTransDate_Result, EmployeeMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetEmployeeList_ByTransDate_Result objtbl_EmployeeMaster in onjtbl_EmployeeMaster)
                {
                    EmployeeMasterModel _EmployeeMasterModel = iMapper.Map<SP_GetEmployeeList_ByTransDate_Result, EmployeeMasterModel>(objtbl_EmployeeMaster);
                    lstEmployeeMasterModel.Add(_EmployeeMasterModel);
                }
            }
            return lstEmployeeMasterModel;
        }
        

        //Check
        public bool CheckEmployeeName(string EmployeeName, long EmployeeID)
        {
            bool result = false;
            try
            {
                tbl_EmployeeMaster _tbl_EmployeeMaster = _db.tbl_EmployeeMaster.FirstOrDefault(x => x.EmailID == EmployeeName && x.IsDelete == false && x.EmployeeID != EmployeeID);
                if (_tbl_EmployeeMaster != null)
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
        //Check Duplicate Password
        public bool CheckEmployeePasswordDuplicate(string password, long employeeId)
        {
            bool result = false;
            try
            {
                tbl_EmployeeMaster employee = _db.tbl_EmployeeMaster.FirstOrDefault(x => x.Password == password && x.IsDelete == false && x.EmployeeID != employeeId);
                if(employee != null)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                
            }
            return result;
        }
        public bool CheckName(string EmployeeName)
        {
            bool result = false;
            try
            {
                tbl_EmployeeMaster _tbl_EmployeeMaster = _db.tbl_EmployeeMaster.FirstOrDefault(x => x.EmailID == EmployeeName && x.IsDelete == false);
                if (_tbl_EmployeeMaster != null)
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
    }
}
