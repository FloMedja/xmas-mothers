using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ChristmasMothers.Extensions;

namespace ChristmasMothers.Web.Api.Middlewares
{
    internal class CallContextMiddleware
    {
        private readonly RequestDelegate _next;

        public CallContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var callContext = context.RequestServices.GetService<ICallContext>();
            var logger = context.RequestServices.GetService<ILoggerFactory>().CreateLogger<CallContextMiddleware>();
            var configuration = context.RequestServices.GetService<IConfiguration>();

            context.Request.Headers.TryGetValue("X-CALLER-ID", out var callerId);
            context.Request.Headers.TryGetValue("X-CORRELATION-ID", out var correlationId);
            context.Request.Headers.TryGetValue("X-LANGUAGE", out var language);
            context.Request.Headers.TryGetValue("X-Forwarded-For", out var remoteIp);

            callContext.RequestId = context.TraceIdentifier;
            callContext.Principal = context.User;
            callContext.CallerId = callerId.Count > 0 ? callerId[0] : null;
            callContext.CorrelationId = correlationId.Count > 0 ? Guid.Parse(correlationId[0]) : Guid.NewGuid();
            callContext.Username = "anonymous";

            if (language.Count > 0)
            {
                CultureInfo.CurrentCulture = new CultureInfo(language[0]);
                CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
            }
            else
            {
                CultureInfo.CurrentCulture = new CultureInfo(configuration["DefaultCulture"]);
                CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
            }
            callContext.Culture = CultureInfo.CurrentCulture;
            if (remoteIp.Count > 0)
            {
                callContext.RemoteIp = remoteIp[0];
            }
            if (callContext.RemoteIp.IsNullOrWhiteSpace())
            {
                callContext.RemoteIp = context.Connection.RemoteIpAddress.ToString();
            }
            if (callContext.RemoteIp.IsNullOrWhiteSpace())
            {
                context.Request.Headers.TryGetValue("REMOTE_ADDR", out remoteIp);
                if (remoteIp.Count > 0)
                {
                    callContext.RemoteIp = remoteIp[0];
                }
                else
                {
                    logger.LogWarning("Cannot find the client's remote ip.");
                }
            }

            return _next(context);
        }
    }
}