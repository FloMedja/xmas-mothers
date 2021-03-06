﻿using System;
using System.Collections.ObjectModel;
using System.Net.Mime;

namespace ChristmasMothers.Entities
{
    //TODO : FM -- complete Attribute
    public class Parent : IPerson
    {
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string CellNumber { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public string GiftDeliveryOption { get; set; }
        public virtual Collection<Child> Children { get; set; }

    }
}
