using System;

namespace SFPOS.Entities.MasterDataClasses
{
    public class tbl_FilterColumnsModel
    {

        public long FilterColumnsID { get; set; }
        public string FilterColumnsName { get; set; }
        public string FilterPageName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsMaster { get; set; }
        public Nullable<short> SeqNo { get; set; }
    }
    public class tbl_FilterColumnsModelCont
    {
        public const string FilterColumnsID = "FilterColumnsID";
        public const string FilterColumnsName = "FilterColumnsName";
        //public const string Name = "Name";
        public const string FilterPageName = "FilterPageName";
        public const string IsActive = "IsActive";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string UpdatedBy = "UpdatedBy";
        public const string UpdatedDate = "UpdatedDate";
        public const string IsMaster = "IsMaster";
        public const string SeqNo = "SeqNo";
    }

    public class ClsFilterCheckBox
    {
        public string FilterColumnsName { get; set; }
        public short? KeySeq { get; set; }
    }
}
