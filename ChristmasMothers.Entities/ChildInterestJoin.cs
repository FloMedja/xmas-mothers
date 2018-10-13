using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChristmasMothers.Entities
{
    public class ChildInterestJoin 
    {
        public Guid ChildId { get; set; }
        public virtual Child Child { get; set; }
        public Guid InterestId { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
