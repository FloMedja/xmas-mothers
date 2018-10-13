using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public class XMasMother : IPerson
    {
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string CellNumber { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public string XMasMotherTrackingId { get; set; }
        public bool GiftDeliver { get; set; }
        public virtual Collection<Child> MatchedChildren { get; set; }
        public virtual Collection<XMasMotherInterestJoin> XMasMotherInterests { get; set; }

    }
}
