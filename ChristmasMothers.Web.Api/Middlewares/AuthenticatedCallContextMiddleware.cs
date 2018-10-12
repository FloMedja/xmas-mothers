using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ChristmasMothers.Extensions;

namespace ChristmasMothers.Web.Api.Middlewares
{
    internal class AuthenticatedCallContextMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticatedCallContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                ICallContext callContext = context.RequestServices.GetService<ICallContext>();
                var logger = context.RequestServices.GetService<ILoggerFactory>().CreateLogger<AuthenticatedCallContextMiddleware>();
                callContext.Principal = context.User;
                var subClaim = callContext.Principal.FindFirst("sub");
                if (subClaim != null)
                {
                    Guid sub;
                    if (Guid.TryParse(subClaim.Value, out sub))
                    {
                        callContext.UserId = sub;
                    }
                    else
                    {
                        logger.LogWarning("No subject for current user");
                    }
                }
                var accountIdClaim = callContext.Principal.FindFirst("id");
                if (accountIdClaim != null)
                {
                    Guid accountId;
                    if (Guid.TryParse(accountIdClaim.Value, out accountId))
                    {
                        callContext.AccountId = accountId;
                    }
                }

                callContext.Sid = callContext.Principal.FindFirst(ClaimTypes.PrimarySid)?.Value;
                callContext.ClientId = callContext.Principal.FindFirst("client_id")?.Value;
                callContext.Username = callContext.Principal.FindFirst("preferred_username")?.Value;
                callContext.Firstname = callContext.Principal.FindFirst("given_name")?.Value;
                callContext.Lastname = callContext.Principal.FindFirst("family_name")?.Value;
                callContext.Email = callContext.Principal.FindFirst("email")?.Value;

                var locale = callContext.Principal.FindFirst("locale")?.Value;
                if (!locale.IsNullOrWhiteSpace())
                {
                    CultureInfo.CurrentCulture = new CultureInfo(locale);
                    CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
                    callContext.Culture = CultureInfo.CurrentCulture;
                }
            }

            return _next(context);
        }
    }
}