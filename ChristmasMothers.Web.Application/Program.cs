using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using ChristmasMother;
using ChristmasMothers.Web.Application.Configurations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ChristmasMothers.Web.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{EnvironmentVariables.AspNetCoreEnvironment}.json", true)
                .Build();

            var serverOptions = new ServerOptions();
            config.GetSection("Server").Bind(serverOptions);

            var builder = WebHost.CreateDefaultBuilder(args);
            var dev = EnvironmentVariables.IsDevelopment();
            if (dev) builder.UseKestrel(opts =>
                        {
                            opts.AddServerHeader = false;
                            opts.Listen(IPAddress.Loopback, serverOptions.Port, listenOpts =>
                            {
                                listenOpts.UseHttps(new X509Certificate2(serverOptions.Certificate, serverOptions.Password));
                            });
                        });
            else
            {
                builder.UseKestrel();
            }

            builder.UseStartup<Startup>();
            if (dev) builder.UseUrls(serverOptions.Url);
            return builder.Build();
        }
    }
}
