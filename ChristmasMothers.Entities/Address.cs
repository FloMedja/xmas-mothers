using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public class Address : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string NoUnitOrApp { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

    }
}
