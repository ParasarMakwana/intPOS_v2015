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
    using System.Collections.Generic;
    
    public partial class tbl_RolePermission
    {
        public long PermissionID { get; set; }
        public Nullable<long> RoleID { get; set; }
        public Nullable<long> MenuID { get; set; }
        public Nullable<bool> ViewRecords { get; set; }
        public Nullable<bool> InsertRecords { get; set; }
        public Nullable<bool> EditRecords { get; set; }
        public Nullable<bool> DeleteRecords { get; set; }
    }
}