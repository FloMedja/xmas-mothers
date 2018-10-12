using ChristmasMothers.Exceptions;

namespace ChristmasMothers.Exceptions
{
    public class UnauthorizedException : ChristmasMotherException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}