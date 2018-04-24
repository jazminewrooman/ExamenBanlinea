using System;
using Acr.UserDialogs;
using System.ComponentModel;
using System.Collections.Generic;
using ExamenBanlinea.Models.DTOs;
using ExamenBanlinea.Services;
using ExamenBanlinea.Helpers;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using Refit;
using MvvmHelpers;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace ExamenBanlinea.ViewModels
{
    public class VMNewContact : VMBase, INotifyPropertyChanged
    {
        public List<Models.DTOs.Contact> lstContacts { get; set; }
        public ObservableCollection<Countries> lstCountries { get; set; }
        public Models.DTOs.Contact contact { get; set; }
        public ImageSource sourceuserpic { get; set; }

        public ICommand SaveContactCommand { get; set; }
        public ICommand CancelCommand { get; }
        public ICommand AddEmailCommand { get; }
        public ICommand DelEmailCommand { get; }
        public ICommand AddNumberCommand { get; }
        public ICommand DelNumberCommand { get; }
        public ICommand CameraCommand { get; }
        public ICommand GalleryCommand { get; }

        public VMNewContact(IUserDialogs diag, INavigation nav) : base(diag, nav)
        {
            sourceuserpic = ImageSource.FromFile("user.png");
            SaveContactCommand = new Command(async () => await SaveContact());
            CameraCommand = new Command(async () =>
            {
                await PhotoFromCamera();
            });
            GalleryCommand = new Command(async () => await PhotoFromGallery());
            CancelCommand = new Command(async () =>
            {
                await Nav.PopAsync();
            });
            AddEmailCommand = new Command(() =>
            {
                contact.Emails.Add(new Email { EmailAddr = String.Empty, IsValid = false  });
            });
            DelEmailCommand = new Command(async () => await DelEmail());
            AddNumberCommand = new Command(() =>
            {
                contact.PhoneNumbers.Add(new PhoneNumber
                {
                    Country = null,
                    Number = String.Empty,
                    IsValid = false,
                    lstCountries = lstCountries,
                });
            });
            DelNumberCommand = new Command(async () => await DelNumber());

            //lstContacts = new List<Models.DTOs.Contact>() { contact };
            Init();
        }

        public async Task Init()
        {
            try
            {
                var api = RestService.For<IContactsApi>("https://contactmanager.banlinea.com");
                MsgBusy = "Cargando Paises. Espere por favor ...";
                Busy = true;
                var tmp = await api.GetCountries();
                lstCountries = new ObservableCollection<Countries>(tmp);
                Busy = false;
            }
            catch (Refit.ApiException ex)
            {
                Busy = false;
                await Diag.AlertAsync(new AlertConfig() { Message = "Error en la API", OkText = "Ok" });
                lstCountries = new ObservableCollection<Countries>() { new Countries { Code = 52, Name = "México", Enabled = true } };
            }
            finally
            {
                contact = new Contact
                {
                    Company = String.Empty,
                    Emails = new ObservableCollection<Email>() { new Email { EmailAddr = String.Empty, IsValid = false } },
                    EmailsAddress = new List<string>() { String.Empty },
                    Name = String.Empty,
                    LastName = String.Empty,
                    PhoneNumbers = new ObservableCollection<PhoneNumber>() { new PhoneNumber { Country = null, Number = String.Empty, IsValid = false, lstCountries = lstCountries } },
                    Photo = String.Empty
                };
                OnPropertyChanged("contact");
            }
        }

        private string Base64Photo(System.IO.Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return Convert.ToBase64String(bytes);
        }

        private async Task PhotoFromGallery()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Diag.AlertAsync(new AlertConfig() { Message = "No se puede accesar la galeria", OkText = "Ok" });
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });
            if (file == null)
                return;
            contact.Photo = Base64Photo(file.GetStream());
            sourceuserpic = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
            OnPropertyChanged("sourceuserpic");
        }

        private async Task PhotoFromCamera()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Diag.AlertAsync(new AlertConfig() { Message = "No hay camara", OkText = "Ok" });
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });
            if (file == null)
                return;
            contact.Photo = Base64Photo(file.GetStream());
            sourceuserpic = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
            OnPropertyChanged("sourceuserpic");
        }

        private async Task DelEmail()
        {
            var cfg = new AlertConfig();
            cfg.Message = "Debe existir al menos un Email";
            cfg.OkText = "Ok";
            if (contact.Emails.Count <= 1)
                await Diag.AlertAsync(cfg);
            if (contact.Emails.Count > 1)
                contact.Emails.Remove(contact.Emails.LastOrDefault());
        }

        private async Task DelNumber()
        {
            var cfg = new AlertConfig();
            cfg.Message = "Debe existir al menos un numero";
            cfg.OkText = "Ok";
            if (contact.PhoneNumbers.Count <= 1)
                await Diag.AlertAsync(cfg);
            if (contact.PhoneNumbers.Count > 1)
                contact.PhoneNumbers.Remove(contact.PhoneNumbers.LastOrDefault());
        }

        private async Task SaveContact()
        {
            int errs = 0; int emailerrs = 0; int numberrs = 0;
            var cfg = new AlertConfig();
            cfg.Message = "Faltan algunos campos, revise por favor. ";
            cfg.OkText = "Ok";

            if (String.IsNullOrEmpty(contact.Company))
            {
                errs++;
                cfg.Message += $"{Environment.NewLine}Falta la compañia";
            }
            foreach (Email e in contact.Emails)
            {
                if (!e.IsValid)
                    emailerrs++;
            }
            if (emailerrs > 0)
            {
                errs++;
                cfg.Message += $"{Environment.NewLine}Falta algun email o no es valido";
            }
            if (String.IsNullOrEmpty(contact.LastName))
            {
                errs++;
                cfg.Message += $"{Environment.NewLine}Falta el apellido";
            }
            if (String.IsNullOrEmpty(contact.Name))
            {
                errs++;
                cfg.Message += $"{Environment.NewLine}Falta el nombre";
            }
            foreach (PhoneNumber n in contact.PhoneNumbers)
            {
                if (!n.IsValid)
                    numberrs++;
            }
            if (numberrs > 0)
            {
                errs++;
                cfg.Message += $"{Environment.NewLine}Falta algun numero o no es valido";
            }
            if (String.IsNullOrEmpty(contact.Photo))
            {
                errs++;
                cfg.Message += $"{Environment.NewLine}Falta la fotografia";
            }
                
            if (errs > 0)
                await Diag.AlertAsync(cfg);
            else
            {
                var tmp = Settings.LstContacts;
                contact.EmailsAddress = contact.Emails.Select(x => x.EmailAddr).ToList();
                tmp.Add(contact);
                Settings.LstContacts = tmp;
                await Nav.PopAsync();
            }
        }
    }
}
