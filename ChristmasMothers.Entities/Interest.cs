using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChristmasMothers.Entities
{
    public class Interest : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public Collection<ChildInterestJoin> InterestedChildren { get; set; }
        public Collection<XMasMotherInterestJoin> InterestedXMasMothers { get; set; }

    }
}
