using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ChristmasMothers.Exceptions;

namespace ChristmasMothers.Web.Api.Middlewares
{
    /// <summary>
    /// Handles exceptions in subsequent request handlers to produce normalized responses
    /// based on the exception type.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// Maps of ChristmasMother exception types to HTTP status codes.
        /// </summary>
        private static readonly IDictionary<Type, HttpStatusCode> ExceptionMap =
            new Dictionary<Type, HttpStatusCode>
            {
                { typeof(NotFoundException), HttpStatusCode.NotFound },
                { typeof(UnauthorizedException), HttpStatusCode.Unauthorized },
                { typeof(ConflictException), HttpStatusCode.Conflict },
                { typeof(ChristmasMotherException), HttpStatusCode.BadRequest }
            };

        /// <summary>
        /// The next request handler.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The loggger.
        /// </summary>
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger(typeof(ErrorHandlingMiddleware));
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (ChristmasMotherException ex)
            {
                await HandleChristmasMotherExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleSystemExceptionAsync(context, ex);
            }
        }

        private async Task HandleChristmasMotherExceptionAsync(HttpContext context, ChristmasMotherException exception)
        {
            _logger.LogWarning(exception.Message);

            var result = JsonConvert.SerializeObject(new
            {
                message = exception.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetHttpStatusCode(exception);
            await context.Response.WriteAsync(result);
        }

        private async Task HandleSystemExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(0, exception, exception.Message);

            var result = JsonConvert.SerializeObject(new
            {
                error = exception.Message,
                stackTrace = exception.StackTrace,
                source = exception.Source
            });

            var code = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }

        private static HttpStatusCode GetHttpStatusCode(ChristmasMotherException exception)
        {
            var t = exception.GetType();
            while (!ExceptionMap.ContainsKey(t))
            {
                t = t.BaseType;
            }
            return ExceptionMap[t];
        }
    }
}