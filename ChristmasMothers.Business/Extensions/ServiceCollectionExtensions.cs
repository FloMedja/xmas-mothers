using ChristmasMothers.Business.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ChristmasMothers.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinesses(this IServiceCollection services)
        {
            services.AddScoped<IHttpGetService, HttpGetService>();
        }
    }
}
