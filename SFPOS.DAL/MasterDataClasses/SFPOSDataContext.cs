using System.Data.Entity.Core.EntityClient;

namespace SFPOS.DAL
{
    public class SFPOSDataContext : DataContext
    {
        public SFPOSDataContext(string constr) : base(constr) { }

        public static DataContext Create(string providerConnectionString) 
        {
            var entityBuilder = new EntityConnectionStringBuilder();

            entityBuilder.ProviderConnectionString = providerConnectionString;

            entityBuilder.Provider = "System.Data.SqlClient";

            entityBuilder.Metadata = @"res://*/DataModel.csdl|res://*/DataModel.ssdl|res://*/DataModel.msl";
            return new DataContext(entityBuilder.ConnectionString);
        }
    }
}
