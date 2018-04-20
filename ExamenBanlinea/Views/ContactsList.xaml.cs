using System;
using System.Collections.Generic;
using ExamenBanlinea.ViewModels;
using Xamarin.Forms;

namespace ExamenBanlinea
{
    public partial class ContactsList : ContentPage
    {
        public ContactsList()
        {
            InitializeComponent();

            VMContactsList vM = new VMContactsList(Acr.UserDialogs.UserDialogs.Instance);
            BindingContext = vM;
        }
    }
}
