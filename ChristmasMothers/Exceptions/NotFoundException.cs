using System;

namespace ChristmasMothers.Exceptions
{
    public class NotFoundException : ChristmasMotherException
    {
        public NotFoundException(Guid id, Type type) : base(FormatMessage(id, type))
        {
           
        }
        public NotFoundException(string name, Type type) : base(FormatMessage(name, type))
        {

        }
        private static string FormatMessage(Guid id, Type type)
        {
            return $"Could not find `{ type.Name }` with id: `{ id }`.";
        }
        private static string FormatMessage(string name, Type type)
        {
            return $"Could not find `{ type.Name }` with name: `{ name }`.";
        }
    }
}
