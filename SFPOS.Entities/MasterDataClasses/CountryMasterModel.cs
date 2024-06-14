namespace SFPOS.Entities.MasterDataClasses
{
    public class CountryMasterModel
    {
        public long CountryID { get; set; }
        public string CountryName { get; set; }

    }
    public class CountryMasterModelCont
    {
        public const string CountryID = "CountryID";
        public const string CountryName = "CountryName";
        public const string Country = "Country";

    }
}
