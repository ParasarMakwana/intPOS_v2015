using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class ExceptionLogMasterModel
    {
        public long ExceptionID { get; set; }
        public string ExceptionName { get; set; }
        public string Discription { get; set; }
        public string PageName { get; set; }
        public long PageLine { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string CounterIP { get; set; }
    }
}
