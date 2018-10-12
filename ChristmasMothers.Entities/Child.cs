using System;
using System.Collections.Generic;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public class Child : IPerson
    {
       public virtual List<Parent> Parent { get; set; }
    }
}
