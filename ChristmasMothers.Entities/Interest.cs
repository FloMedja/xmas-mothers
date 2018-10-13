using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChristmasMothers.Entities
{
    public class Interest : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public String Label { get; set; }
        public Collection<ChildInterestJoin> InterestedChildren { get; set; }
        public Collection<XMasMotherInterestJoin> InterestedXMasMothers { get; set; }

    }
}
