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
    public class CustomerDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public CustomerMasterModel AddCustomer(CustomerMasterModel objCustomerMasterModel, int TransType)
        {
            try
            {
                tbl_CustomerMaster objtbl_CustomerMaster = new tbl_CustomerMaster();
                if (TransType == 1)//ADD
                {
                    objCustomerMasterModel.CustomerID = objCustomerMasterModel.CustomerID;
                    objtbl_CustomerMaster.StoreID = objCustomerMasterModel.StoreID;
                    objtbl_CustomerMaster.FirstName = objCustomerMasterModel.FirstName;
                    objtbl_CustomerMaster.LastName = objCustomerMasterModel.LastName;
                    objtbl_CustomerMaster.Email = objCustomerMasterModel.Email;
                    objtbl_CustomerMaster.MobileNo = objCustomerMasterModel.MobileNo;
                    objtbl_CustomerMaster.ZipCode = objCustomerMasterModel.ZipCode;
                    objtbl_CustomerMaster.Message = objCustomerMasterModel.Message;
                    objtbl_CustomerMaster.NewsLetter = objCustomerMasterModel.NewsLetter;
                    objtbl_CustomerMaster.ResellerID = objCustomerMasterModel.ResellerID;
                    objtbl_CustomerMaster.BusinessName = objCustomerMasterModel.BusinessName;
                    objtbl_CustomerMaster.TaxExempted = objCustomerMasterModel.TaxExempted; 
                    objtbl_CustomerMaster.IsActive = objCustomerMasterModel.IsActive;
                    objtbl_CustomerMaster.IsDelete = false;
                    objtbl_CustomerMaster.CreatedDate = DateTime.Now;
                    objtbl_CustomerMaster.City = objCustomerMasterModel.City;
                    //objtbl_CustomerMaster.CreatedBy = LoginInfo.UserId;
                    _db.tbl_CustomerMaster.Add(objtbl_CustomerMaster);
                    objCustomerMasterModel.CustomerID = objtbl_CustomerMaster.CustomerID;
                }
                else if (TransType == 2)//EDIT
                {
                    objtbl_CustomerMaster = _db.tbl_CustomerMaster.Where(p => p.CustomerID == objCustomerMasterModel.CustomerID).FirstOrDefault();
                    if (objtbl_CustomerMaster != null)
                    {
                        objCustomerMasterModel.CustomerID = objCustomerMasterModel.CustomerID;
                        objtbl_CustomerMaster.StoreID = objCustomerMasterModel.StoreID;
                        objtbl_CustomerMaster.FirstName = objCustomerMasterModel.FirstName;
                        objtbl_CustomerMaster.LastName = objCustomerMasterModel.LastName;
                        objtbl_CustomerMaster.Email = objCustomerMasterModel.Email;
                        objtbl_CustomerMaster.MobileNo = objCustomerMasterModel.MobileNo;
                        objtbl_CustomerMaster.ZipCode = objCustomerMasterModel.ZipCode;
                        objtbl_CustomerMaster.Message = objCustomerMasterModel.Message;
                        objtbl_CustomerMaster.NewsLetter = objCustomerMasterModel.NewsLetter;
                        objtbl_CustomerMaster.ResellerID = objCustomerMasterModel.ResellerID;
                        objtbl_CustomerMaster.BusinessName = objCustomerMasterModel.BusinessName;
                        objtbl_CustomerMaster.TaxExempted = objCustomerMasterModel.TaxExempted;
                        objtbl_CustomerMaster.IsActive = objCustomerMasterModel.IsActive;
                        objtbl_CustomerMaster.City = objCustomerMasterModel.City;
                        //objtbl_CustomerMaster.UpdatedDate = DateTime.Now;
                        //objtbl_CustomerMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                else if (TransType == 3)//DELETE
                {
                    objtbl_CustomerMaster = _db.tbl_CustomerMaster.Where(p => p.CustomerID == objCustomerMasterModel.CustomerID).FirstOrDefault();
                    if (objtbl_CustomerMaster != null)
                    {
                        objtbl_CustomerMaster.CustomerID = objCustomerMasterModel.CustomerID;
                        objtbl_CustomerMaster.IsDelete = true;
                        //objtbl_CustomerMaster.UpdatedDate = DateTime.Now;
                        //objtbl_CustomerMaster.UpdatedBy = LoginInfo.UserId;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objCustomerMasterModel;
        }

        //List
        public List<CustomerMasterModel> GetAllCustomers()
        {
            var lstCustomerMasterModel = new List<CustomerMasterModel>();

            var onjtbl_CustomerMaster = _db.SP_GetCustomerList().ToList(); 
            if (onjtbl_CustomerMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<SP_GetCustomerList_Result, CustomerMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (SP_GetCustomerList_Result objtbl_CustomerMaster in onjtbl_CustomerMaster)
                {
                    CustomerMasterModel _CustomerMasterModel = iMapper.Map<SP_GetCustomerList_Result, CustomerMasterModel>(objtbl_CustomerMaster);
                    lstCustomerMasterModel.Add(_CustomerMasterModel);
                }
            }
            return lstCustomerMasterModel;
        }

        //Check
        public bool CheckCustomerPhone(string MobileNo, long CustomerID)
        {
            bool result = false;
            try
            {
                tbl_CustomerMaster _tbl_CustomerMaster = _db.tbl_CustomerMaster.FirstOrDefault(x => x.MobileNo == MobileNo && x.IsDelete == false && x.CustomerID != CustomerID);
                if (_tbl_CustomerMaster != null)
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

        public bool CheckCustomerPhone(string MobileNo)
        {
            bool result = false;
            try
            {
                tbl_CustomerMaster _tbl_CustomerMaster = _db.tbl_CustomerMaster.FirstOrDefault(x => x.MobileNo == MobileNo && x.IsDelete == false);
                if (_tbl_CustomerMaster != null)
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
        
        public bool CheckCustomerEmail(string EmailID, long CustomerID)
        {
            bool result = false;
            try
            {
                tbl_CustomerMaster _tbl_CustomerMaster = _db.tbl_CustomerMaster.FirstOrDefault(x => x.Email == EmailID && x.IsDelete == false && x.CustomerID != CustomerID);
                if (_tbl_CustomerMaster != null)
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
        
        
        public bool CheckCustomerEmail(string EmailID)
        {
            bool result = false;
            try
            {
                tbl_CustomerMaster _tbl_CustomerMaster = _db.tbl_CustomerMaster.FirstOrDefault(x => x.Email == EmailID && x.IsDelete == false);
                if (_tbl_CustomerMaster != null)
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
