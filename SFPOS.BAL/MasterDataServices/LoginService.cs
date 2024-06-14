using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    public class LoginService
    {
        LoginDAL objLoginDAL = new LoginDAL();
        public LoginMasterModel AddLogin(LoginMasterModel objLoginMasterModel, int TransType)
        {
            return objLoginDAL.AddLogin(objLoginMasterModel, TransType);
        }
        public List<LoginMasterModel> GetAllLoginDetail()
        {
            return objLoginDAL.GetAllLoginDetail();
        }
    }
}
