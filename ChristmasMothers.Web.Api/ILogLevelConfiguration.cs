using Microsoft.Extensions.Logging;

namespace ChristmasMothers.Web.Api
{
    public interface ILogLevelConfiguration
    {
        bool TryGetLevel(string name, out LogLevel level);
    }
}