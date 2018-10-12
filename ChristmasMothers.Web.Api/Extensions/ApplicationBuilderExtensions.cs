using Microsoft.AspNetCore.Builder;
using ChristmasMothers.Web.Api.Middlewares;

namespace ChristmasMothers.Web.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApi(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorHandlingMiddleware>();
            builder.UseMiddleware<CallContextMiddleware>();
            builder.UseAuthentication();
            builder.UseMiddleware<AuthenticatedCallContextMiddleware>();
        }
    }
}