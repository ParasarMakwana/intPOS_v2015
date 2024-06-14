using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
     public class MenuMasterModel
    {
        public long MenuID { get; set; }
        public string MenuName { get; set; }
    }
    public class MenuMasterModelCont
    {
        public const string MenuName = "MenuName";
        public const string MenuID = "MenuID";
    }
}
