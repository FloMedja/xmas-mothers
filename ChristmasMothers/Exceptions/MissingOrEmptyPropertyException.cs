using System;

namespace ChristmasMothers.Exceptions
{
    public class MissingOrEmptyPropertyException : ChristmasMotherException
    {
        public MissingOrEmptyPropertyException(string propertyName, Type type)
            : base(FormatMessage(propertyName, type)) { }

        private static string FormatMessage(string propertyName, Type type)
        {
            return $"Missing or empty `{ propertyName }` in `{ type.Name }`.";
        }
    }
}
