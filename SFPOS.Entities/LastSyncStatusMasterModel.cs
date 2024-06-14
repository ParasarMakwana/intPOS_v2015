using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities
{
    public class LastSyncStatusMasterModel
    {
        public Nullable<int> CounterNo { get; set; }
        public string CounterIP { get; set; }
        public string TblName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsSync { get; set; }
        public Nullable<System.DateTime> SyncDate { get; set; }
        public string SyncStatus { get; set; }
    }
}
