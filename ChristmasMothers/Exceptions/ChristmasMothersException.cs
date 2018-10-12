using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasMothers.Exceptions
{
    public class ChristmasMothersException : Exception
    {
        public ChristmasMothersException(string message) : base(message)
        {
        }
        public ChristmasMothersException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public ChristmasMothersException()
        {
        }
    }
}
