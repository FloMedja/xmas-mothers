using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChristmasMothers.Web.Api.Configurations
{
    public class LogLevelConfiguration : ILogLevelConfiguration
    {
        private readonly IConfiguration _configuration;

        public LogLevelConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool TryGetLevel(string name, out LogLevel level)
        {
            var logLevel = _configuration[$"Logging:LogLevel:{name}"];

            if (string.IsNullOrWhiteSpace(logLevel))
            {
                level = LogLevel.None;
                return false;
            }

            if (string.IsNullOrEmpty(logLevel))
            {
                level = LogLevel.None;
                return false;
            }

            if (Enum.TryParse(logLevel, true, out level))
                return true;

            throw new InvalidOperationException($"Configuration value '{logLevel}' for category '{name}' is not supported.");
        }
    }
}