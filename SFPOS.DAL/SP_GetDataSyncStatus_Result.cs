//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SFPOS.DAL
{
    using System;
    
    public partial class SP_GetDataSyncStatus_Result
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
