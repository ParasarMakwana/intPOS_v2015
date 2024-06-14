using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.Entities.MasterDataClasses
{
    public class RolePermissionMasterModel
    {
        public long PermissionID { get; set; }
        public long RoleID { get; set; }
        public long MenuID { get; set; }
        public bool ViewRecords { get; set; }
        public bool InsertRecords { get; set; }
        public bool EditRecords { get; set; }
        public bool DeleteRecords { get; set; }
    }
    public class RolePermissionMasterModelCont
    {
        public const string MenuName = "MenuName";
        public const string MenuID = "MenuID";
        public const string PermissionID = "PermissionID";
        public const string DeleteRecord = "DeleteRecord";
        public const string EditRecord = "EditRecord";
        public const string InsertRecord = "InsertRecord";
        public const string ViewRecords = "ViewRecords";
        public const string Menu = "Menu";
        public const string Role = "Role";
        public const string MenuType = "MenuType";
        public const string SortNo = "SortNo";
    }
}
