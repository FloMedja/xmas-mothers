using System;
using System.Globalization;
using System.Security.Claims;

namespace ChristmasMothers.Web.Api
{
    internal class CallContext : ICallContext
    {
        public CultureInfo Culture { get; set; }
        public string ClientId { get; set; }
        public string CallerId { get; set; }
        public string RemoteIp { get; set; }
        public Guid CorrelationId { get; set; }
        public string RequestId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? AccountId { get; set; }
        public string Sid { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}