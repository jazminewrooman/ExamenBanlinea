using System;

namespace ExamenBanlinea.Interfaces
{
    /// <summary>
    /// Modelo para manejar la posicion GPS
    /// </summary>
    public class Geo
    {
        public Double latitude { get; set; }
        public Double longitud { get; set; }
    }

    /// <summary>
    /// Interface para obtener la posicion de manera nativa en cada plataforma
    /// </summary>
    public interface IGeo
    {
        void GetLatLon();

        Action<Geo> LatLonUpd { get; set; }
    }
}
