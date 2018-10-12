using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public class Candidate : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public virtual Application Application { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //TODO :FM -  choose Adress pattern  ( string or separate field ) 
        //TODO :FM --  Check for others attributes to complete 

    }
}
