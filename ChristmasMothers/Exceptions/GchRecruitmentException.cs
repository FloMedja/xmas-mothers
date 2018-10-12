using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasMothers.Exceptions
{
    public class ChristmasMotherException : Exception
    {
        public ChristmasMotherException(string message) : base(message)
        {
        }
        public ChristmasMotherException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public ChristmasMotherException()
        {
        }
    }
}
