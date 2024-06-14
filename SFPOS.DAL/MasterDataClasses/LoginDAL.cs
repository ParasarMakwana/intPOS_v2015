using AutoMapper;
using SFPOS.Common;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
    public class LoginDAL
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        //ADD // EDIT // DELETE
        public LoginMasterModel AddLogin(LoginMasterModel objLoginMasterModel, int TransType)
        {
            try
            {
                tbl_LoginMaster objtbl_LoginMaster = new tbl_LoginMaster();
                if (TransType == 1)//ADD
                {
                    objLoginMasterModel.LoginID = objLoginMasterModel.LoginID;
                    objtbl_LoginMaster.CounterIP = objLoginMasterModel.CounterIP;
                    objtbl_LoginMaster.EmployeeID = LoginInfo.UserId;
                    objtbl_LoginMaster.StoreID = LoginInfo.StoreID;
                    objtbl_LoginMaster.LoginDate = DateTime.Now;
                    _db.tbl_LoginMaster.Add(objtbl_LoginMaster);
                    objLoginMasterModel.LoginID = objtbl_LoginMaster.LoginID;
                }
                else if (TransType == 2)//Update
                {
                    objtbl_LoginMaster = _db.tbl_LoginMaster.Where(p => p.EmployeeID == LoginInfo.UserId).OrderByDescending(p => p.LoginID).FirstOrDefault();
                    if (objtbl_LoginMaster != null)
                    {
                        objtbl_LoginMaster.LogoutTime = DateTime.Now;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objLoginMasterModel;
        }

        public List<LoginMasterModel> GetAllLoginDetail()
        {
            var lstLoginMasterModel = new List<LoginMasterModel>();

            var onjtbl_LoginMaster = _db.tbl_LoginMaster.Where(o=>o.StoreID == LoginInfo.StoreID && o.LogoutTime == null).ToList();

            if (onjtbl_LoginMaster.Count > 0)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<tbl_LoginMaster, LoginMasterModel>(); });
                IMapper iMapper = config.CreateMapper();
                foreach (tbl_LoginMaster objtbl_LoginMaster in onjtbl_LoginMaster)
                {
                    LoginMasterModel _LoginMasterModel = iMapper.Map<tbl_LoginMaster, LoginMasterModel>(objtbl_LoginMaster);
                    lstLoginMasterModel.Add(_LoginMasterModel);
                }
            }
            return lstLoginMasterModel;
        }
    }
}
