using System;
using Acr.UserDialogs;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExamenBanlinea.Models.DTOs;
using ExamenBanlinea.Helpers;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;
using MvvmHelpers;
using ExamenBanlinea.Interfaces;
using Refit;
using ExamenBanlinea.Services;

namespace ExamenBanlinea.ViewModels
{
    public class VMContactsList : VMBase, INotifyPropertyChanged
    {
        public ObservableCollection<Models.Contact> ContactsList { get; set; }
        public ICommand NewContactCommand { get; }
        public ICommand ApiCommand { get; }
        public Geo Position;

        public VMContactsList(IUserDialogs diag, INavigation nav) : base(diag, nav)
        {
            IniciaGPS();

            NewContactCommand = new Command(async () => await NewContact());
            ApiCommand = new Command(async () => await SaveApi());
            ContactsList = new ObservableCollection<Models.Contact>();
        }

        private async Task SaveApi()
        {
            try
            {
                if (Settings.LstContacts.Count == 0)
                {
                    await Diag.AlertAsync(new AlertConfig() { Message = "Agregue al menos un contacto", OkText = "Ok" });
                    return;
                }
                var request = new ContactsRequest()
                {
                    Contacts = Settings.LstContacts,
                    Location = Position != null ? new Location { Latitude = Position.latitude, Longitude = Position.longitud } : new Location { Latitude = 15.0, Longitude = 15.0 },
                    RegisteredBy = new RegisteredBy { Name = "Jazmine Aline Villegas Brena" },
                    Type = 1,
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(request);

                var api = RestService.For<IContactsApi>("https://contactmanager.banlinea.com");
                MsgBusy = "Guardando Contactos. Espere por favor ...";
                Busy = true;
                var response = await api.SaveContacts(request);
                Busy = false;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    await Diag.AlertAsync(new AlertConfig() { Message = "Contactos Guardados", OkText = "Ok" });
                else
                    await Diag.AlertAsync(new AlertConfig() { Message = "Error en la API", OkText = "Ok" });
            }
            catch (Refit.ApiException ex)
            {
                Busy = false;
                await Diag.AlertAsync(new AlertConfig() { Message = "Error en la API", OkText = "Ok" });
            }
        }

        public async Task IniciaGPS()
        {
            var plataforma = DependencyService.Get<IGeo>();
            if (plataforma != null)
            {
                Geo g = new Geo();
                plataforma.LatLonUpd = delegate (Geo gtmp)
                {
                    Position = gtmp;
                };
                plataforma.GetLatLon();
            }
        }

        public void Reload()
        {
            if (Settings.LstContacts != null)
            {
                ContactsList.Clear();
                foreach (Contact c in Settings.LstContacts)
                {
                    ContactsList.Add(new Models.Contact()
                    {
                        Name = $"{c.Name} {c.LastName}",
                        Email = c.EmailsAddress.FirstOrDefault(),
                        Number = c.PhoneNumbers.FirstOrDefault().Number,
                        Photo = c.Photo,
                    });
                }
                OnPropertyChanged("ContactsList");
            }
        }

        private async Task NewContact()
        {
            await Nav.PushAsync(new NewContact());
        }
    }
}
