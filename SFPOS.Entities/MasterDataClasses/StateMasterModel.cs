using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class StateMasterModel
    {
        public long StateID { get; set; }
        public long CountryID { get; set; }
        public string StateName { get; set; }

    }
    public class StateMasterModelCont
    {
        public const string StateID = "StateID";
        public const string StateName = "StateName";
        public const string State = "State";

    }
}
