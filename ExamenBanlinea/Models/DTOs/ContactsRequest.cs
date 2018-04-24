using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PropertyChanged;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public bool IsValid { get; set; }
        [JsonIgnore]
        public ObservableCollection<Countries> lstCountries { get; set; }
    }

    public class Email
    {
        public string EmailAddr { get; set; }
        [JsonIgnore]
        public bool IsValid { get; set; }
    }

    public class Contact : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Company { get; set; }
        [JsonIgnore]
        public ObservableCollection<Email> Emails { get; set; }
        public List<string> EmailsAddress { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public ObservableCollection<PhoneNumber> PhoneNumbers { get; set; }
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