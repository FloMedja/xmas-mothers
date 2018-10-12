namespace ChristmasMothers.Web.Application.Configurations
{
    public class ServerOptions
    {
        public const string CONFIGURATION_SECTION = "Server";

        public string Certificate { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public int Port { get; set; }
    }
}