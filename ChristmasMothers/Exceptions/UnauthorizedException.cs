using ChristmasMothers.Exceptions;

namespace ChristmasMothers.Exceptions
{
    public class UnauthorizedException : ChristmasMothersException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}