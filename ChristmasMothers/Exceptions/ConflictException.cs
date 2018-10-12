using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasMothers.Exceptions
{
    public class ConflictException : ChristmasMotherException
    {
        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConflictException()
        {
        }
    }
}
