using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChristmasMothers.Dal.Configurations;
using ChristmasMothers.Dal.EntityFramework.Configurations;
using ChristmasMothers.Dal.EntityFramework.Repositories;
using ChristmasMothers.Dal.Repositories;

namespace ChristmasMothers.Dal.EntityFramework.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddDal(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EntityFrameworkDbContext>();
            services.AddSingleton<IDalConfiguration>(serviceProvider => new DalConfiguration()
            {
                ConnectionString = EnvironmentVariables.ChristmasMotherConnectionString != null ? EnvironmentVariables.ChristmasMotherConnectionString : configuration.GetConnectionString("ChristmasMother"),
                DatabaseType = EnvironmentVariables.ChristmasMotherDataBaseType != null ? Enum.Parse<DatabaseType>(EnvironmentVariables.ChristmasMotherDataBaseType)
                    : Enum.Parse<DatabaseType>(configuration.GetConnectionString("ChristmasMotherDatabaseType")),
                Schema = EnvironmentVariables.ChristmasMotherSchema != null ? EnvironmentVariables.ChristmasMotherSchema : configuration.GetConnectionString("ChristmasMotherSchema")
            });
            
            services.AddScoped<IRequisitionRepository, RequisitionRepository>();
        }
    }
}