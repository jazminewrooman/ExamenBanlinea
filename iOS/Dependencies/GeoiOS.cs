using System;
using ExamenBanlinea.Interfaces;
using CoreLocation;
using UIKit;
using ExamenBanlinea.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(GeoiOS))]

namespace ExamenBanlinea.iOS
{
    public class GeoiOS : IGeo
    {
        //Esta es la clase que permite los servicios de CoreLocation
        protected CLLocationManager locMgr;
        public Action<Geo> LatLonUpd { get; set; }

        /// <summary>
        /// Inicializa el componente y requiere permisos al usuario (de requerirlo)
        /// </summary>
        public GeoiOS()
        {
            this.locMgr = new CLLocationManager();
            this.locMgr.PausesLocationUpdatesAutomatically = false;

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                //Se usa este para que pueda ser en background
                locMgr.RequestAlwaysAuthorization();
                //locMgr.RequestWhenInUseAuthorization ();
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                locMgr.AllowsBackgroundLocationUpdates = true;
            }
        }

        /// <summary>
        /// Inicia el GPS para recibir location, y actualiza los datos hacia la UI por medio de "LatLonUpd"
        /// </summary>
        public void GetLatLon()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                //precision en metros
                locMgr.DesiredAccuracy = 1;
                //recibimos cambios en la posicion
                locMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    //Y creamos un objeto del tipo "Geo" para regresarlo a la UI
                    Geo g = new Geo { latitude = e.Locations[e.Locations.Length - 1].Coordinate.Latitude, longitud = e.Locations[e.Locations.Length - 1].Coordinate.Longitude };
                    LatLonUpd(g);
                };
                locMgr.StartUpdatingLocation();
            }
        }
    }
}
