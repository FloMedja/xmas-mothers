using System;
using Microsoft.Extensions.Configuration;
using ChristmasMothers.Dal.Configurations;
using ChristmasMothers.Dal.EntityFramework.Configurations;

namespace ChristmasMothers.Dal.EntityFramework.Tests.Repositories
{
    public abstract class RepositoryTest
    {
        public IConfigurationRoot Configuration { get; set; }
        public IDalConfiguration DalConfiguration { get; set; }

        public RepositoryTest()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{EnvironmentVariables.AspNetCoreEnvironment}.json", true);

            Configuration = builder.Build();

            DalConfiguration = new DalConfiguration
            {
                ConnectionString = Configuration.GetConnectionString("ChristmasMother"),
                DatabaseType = Enum.Parse<DatabaseType>(Configuration.GetConnectionString("ChristmasMotherDatabaseType"))
            };
        }
         
    }
}