using System;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public interface IPerson : IEntity<Guid>
    {        
        string FirstName { get; set; }
        string LastName { get; set; }
        uint Age { get; set; }

    }
}
