using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
    public class MenuService
    {
        MenuDAL objMenuDAL = new MenuDAL();
        public List<MenuMasterModel> GetAllMenu()
        {
            return objMenuDAL.GetAllMenu();
        }
    }
}
