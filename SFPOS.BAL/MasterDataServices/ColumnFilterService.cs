using SFPOS.DAL.MasterDataClasses;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.BAL.MasterDataServices
{
   public class ColumnFilterService
    {
        ColumnFilterServiceDAL objColumnFilterServiceDAL = new ColumnFilterServiceDAL();
        public List<string> GetFilterColumnsList(string Frmname)
        {
            return objColumnFilterServiceDAL.GetFilterColumnsList(Frmname);
        }
        public List<ClsFilterCheckBox> GetFilterMasterColumnsList(string Frmname)
        {
            return objColumnFilterServiceDAL.GetFilterMasterColumnsList(Frmname);
        }
        public tbl_FilterColumnsModel AddProductColumnFilter(tbl_FilterColumnsModel objtbl_FilterColumnsModel)
        {
            return objColumnFilterServiceDAL.AddProductColumnFilter(objtbl_FilterColumnsModel);
        }
    }
}
