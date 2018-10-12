using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChristmasMothers.Web.Api.Configurations;

namespace ChristmasMothers.Web.Api.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityServerOptions>(configuration.GetSection(IdentityServerOptions.CONFIGURATION_SECTION));
            services.AddSingleton(configuration);
            services.AddScoped<ICallContext, CallContext>();
            services.AddScoped<ILogLevelConfiguration, LogLevelConfiguration>();
        }

       
    }
}