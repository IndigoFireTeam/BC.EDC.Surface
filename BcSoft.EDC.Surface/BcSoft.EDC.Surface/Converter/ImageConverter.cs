using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

namespace BcSoft.EDC.Surface.Converter
{
    public class ImageConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value==null)
            {
                return null;
            }

            var imagePath = AppDomain.CurrentDomain.BaseDirectory + value.ToString();
            if(System.IO.File.Exists(imagePath))
            {
                BitmapImage image = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

                return image;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
