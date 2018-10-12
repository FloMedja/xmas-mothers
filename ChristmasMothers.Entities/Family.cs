using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public class Candidate : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public virtual Collection<Parent> Parents{ get; set; }
        public virtual Collection<Child> Children { get; set; }

        //TODO :FM -  choose Adress pattern  ( string or separate field ) 
        //TODO :FM --  Check for others attributes to complete 

    }
}
