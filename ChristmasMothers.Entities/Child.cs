using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public class Child : IPerson
    {

        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string CellNumber { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Sexe { get; set; }
        public string ChildTrackingId { get; set; }
        public bool IsMatched { get; set; }
        public string Comments { get; set; }
        public virtual Guid GiftGiverId { get; set; }
        public virtual XMasMother GiftGiver { get; set; }
        public virtual Guid ParentId { get; set; }
        public virtual Parent Parent { get; set; }
        public virtual Collection<ChildInterestJoin> ChildInterests { get; set; }

    }
}
