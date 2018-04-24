using System;
using Xamarin.Forms;
using System.Globalization;
using System.IO;

namespace ExamenBanlinea.Helpers.Converters
{
    public class PhotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (!String.IsNullOrEmpty(value.ToString()))
                {
                    var imageAsBytes = System.Convert.FromBase64String(value.ToString());
                    return ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                }
                else
                    return ImageSource.FromFile("user.png");
            }
            else
                return ImageSource.FromFile("user.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
