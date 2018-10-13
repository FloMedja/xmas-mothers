using System;
using System.ComponentModel.DataAnnotations;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public interface IPerson : IEntity<Guid>
    {        
        string GivenName { get; set; }
        string FamilyName { get; set; }
        string CellNumber { get; set; }
        Address Address { get; set; }
        string Email { get; set; }
    }
}
