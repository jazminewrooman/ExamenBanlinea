using System;
using Acr.UserDialogs;
using System.ComponentModel;
using System.Collections.Generic;
using ExamenBanlinea.Models.DTOs;

namespace ExamenBanlinea.ViewModels
{
    public class VMContactsList : VMBase
    {
        public List<Contact> ContactsList { get; set; }

        public VMContactsList(IUserDialogs diag) : base(diag)
        {
            var lsttmp = new List<Contact>();
            lsttmp.Add(new Contact()
            {
                Company = "Alset",
                //EmailsAddress = { "jvillegas@alset.com.mx" },
                Name = "Jazmine",
                LastName = "Brena",
                //PhoneNumbers = { new PhoneNumber { Number = "55 2732 4281" } },
                //Photo
            });
            lsttmp.Add(new Contact()
            {
                Company = "Alset",
                //EmailsAddress = { "jvillegas@alset.com.mx" },
                Name = "Jazmine",
                LastName = "Brena",
                //PhoneNumbers = { new PhoneNumber { Number = "55 2732 4281" } },
                //Photo
            });

            ContactsList = lsttmp;
        }
    }
}
