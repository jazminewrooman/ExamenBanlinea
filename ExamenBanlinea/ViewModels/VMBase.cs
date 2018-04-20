using System;
using MvvmHelpers;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ExamenBanlinea.ViewModels
{
    public class VMBase : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IUserDialogs Diag;
        private string mmsgbusy;
        public string MsgBusy
        {
            get => (!String.IsNullOrEmpty(mmsgbusy) ? $"{mmsgbusy}..." : "");
            set
            {
                if (mmsgbusy != value)
                {
                    mmsgbusy = value;
                    OnPropertyChanged("MsgBusy");
                }
            }
        }

        private bool mbusy;
        public bool Busy
        {
            get { return mbusy; }
            set
            {
                if (mbusy != value)
                {
                    if (value)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Diag.ShowLoading($"Estamos trabajando.{Environment.NewLine}Permítenos procesar tu información.{Environment.NewLine}{MsgBusy}", MaskType.Black);
                            //Task.Delay(TimeSpan.FromMilliseconds(10000));
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Diag.HideLoading();
                        });
                    }
                    mbusy = value;
                    OnPropertyChanged("Busy");
                }
            }
        }

        public VMBase(IUserDialogs diag)
        {
            Diag = diag;
        }

    }
}
