using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasMothers.Entities
{
    public class Match : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ChildId { get; set; }
        public virtual Child Child { get; set;}
        public Guid XMasMotherId { get; set; }
        public virtual XMasMother XMasMother { get; set;}
        public virtual DateTimeOffset DateOfMatch { get; set; }
    }
}
