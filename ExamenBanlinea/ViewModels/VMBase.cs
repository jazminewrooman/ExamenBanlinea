using System;
using MvvmHelpers;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ExamenBanlinea.ViewModels
{
    public class VMBase : BaseViewModel
    {
        public IUserDialogs Diag;
        public INavigation Nav;

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
                            Diag.ShowLoading(MsgBusy, MaskType.Black);
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

        public VMBase(IUserDialogs diag, INavigation nav)
        {
            Diag = diag;
            Nav = nav;
        }

    }
}
