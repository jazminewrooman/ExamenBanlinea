using System;
using System.Collections.Generic;

namespace ExamenBanlinea.Models.DTOs
{
    public class Country
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }

    public class PhoneNumber
    {
        public Country Country { get; set; }
        public string Number { get; set; }
    }

    public class Contact
    {
        public string Company { get; set; }
        public IList<string> EmailsAddress { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
        public string Photo { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class RegisteredBy
    {
        public string Name { get; set; }
    }

    public class ContactsRequest
    {
        public IList<Contact> Contacts { get; set; }
        public Location Location { get; set; }
        public RegisteredBy RegisteredBy { get; set; }
        public int Type { get; set; }
    }
}