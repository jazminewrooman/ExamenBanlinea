using System;
using System.Collections.Generic;
using ExamenBanlinea.ViewModels;
using Xamarin.Forms;

namespace ExamenBanlinea
{
    public partial class NewContact : ContentPage
    {
        public NewContact()
        {
            InitializeComponent();

            VMNewContact vm = new VMNewContact(Acr.UserDialogs.UserDialogs.Instance, Navigation);
            BindingContext = vm;


        }

    }
}
