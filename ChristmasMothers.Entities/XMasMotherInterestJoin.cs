using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChristmasMothers.Entities
{
    public class XMasMotherInterestJoin
    {
        public Guid XMasMotherId { get; set; }
        public virtual XMasMother XMasMother { get; set; }
        public Guid InterestId { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
