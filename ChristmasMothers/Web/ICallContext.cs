using System;
using System.Globalization;
using System.Security.Claims;

namespace ChristmasMothers.Web
{
    public interface ICallContext
    {
        CultureInfo Culture { get; set; }
        string ClientId { get; set; }
        string CallerId { get; set; }
        string RemoteIp { get; set; }
        Guid CorrelationId { get; set; }
        string RequestId { get; set; }
        Guid? UserId { get; set; }
        Guid? AccountId { get; set; }
        string Sid { get; set; }
        string Username { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Email { get; set; }
        ClaimsPrincipal Principal { get; set; }

    }
}