using System;
using System.Collections.Generic;
using ExamenBanlinea.ViewModels;
using Xamarin.Forms;

namespace ExamenBanlinea
{
    public partial class ContactsList : ContentPage
    {
        public VMContactsList vm;

        public ContactsList()
        {
            InitializeComponent();

            vm = new VMContactsList(Acr.UserDialogs.UserDialogs.Instance, Navigation);
            BindingContext = vm;
        }

        protected override void OnAppearing()
		{

            if (vm == null)
            {
                vm = BindingContext as VMContactsList;
                vm.IniciaGPS();
            }
            if (vm != null)
                vm.Reload();

            base.OnAppearing();
		}
	}
}
