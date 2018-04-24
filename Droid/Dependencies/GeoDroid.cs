using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms;
using ExamenBanlinea.Droid;
using ExamenBanlinea.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(GeoDroid))]

namespace ExamenBanlinea.Droid
{
    public class GeoDroid : Java.Lang.Object, IGeo, ILocationListener
    {
        LocationManager _locationManager;
        public Action<Geo> LatLonUpd { get; set; }

        public GeoDroid()
        {
        }

        public void GetLatLon()
        {
            LocationManager lm = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
            lm.RequestLocationUpdates(LocationManager.NetworkProvider, 0, 0, this);
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            if (location != null)
            {
                Geo g = new Geo { latitude = location.Latitude, longitud = location.Longitude };
                LatLonUpd(g);
            }
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }

    }
}
