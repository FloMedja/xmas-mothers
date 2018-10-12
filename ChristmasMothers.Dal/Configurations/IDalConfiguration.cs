namespace ChristmasMothers.Dal.Configurations
{
    public interface IDalConfiguration
    {
        string ConnectionString { get; set; }
        DatabaseType DatabaseType { get; set; }
        string Schema { get; set; }
    }
}