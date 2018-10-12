using ChristmasMothers.Dal.Configurations;

namespace ChristmasMothers.Dal.EntityFramework.Configurations
{
    public class DalConfiguration : IDalConfiguration
    {
        public string ConnectionString { get; set; }
        public DatabaseType DatabaseType { get; set; }
        public string Schema { get; set; }
    }
}