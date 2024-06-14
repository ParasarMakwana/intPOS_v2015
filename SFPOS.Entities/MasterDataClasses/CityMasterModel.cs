namespace SFPOS.Entities.MasterDataClasses
{
    public class CityMasterModel
    {
        public long CityID { get; set; }
        public string CityName { get; set; }
    }
    public class CityMasterModelCont
    {
        public const string CityID = "CityID";
        public const string CityName = "CityName";
        public const string City = "City";
    }
}
