using System;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public interface IPerson : IEntity<Guid>
    {        
        string GivenName { get; set; }
        string FamilyName { get; set; }
        int Age { get; set; }
    }
}
