using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace BcSoft.EDC.Surface.Helper
{
    public class ViewerFactory
    {
        public static FrameworkElement Create(string filePath)
        {
            FrameworkElement viewer = null;
            if(System.IO.File.Exists(filePath))
            {
                var fileExtension = System.IO.Path.GetExtension(filePath);
                switch(fileExtension.ToUpper())
                {
                    case ".PNG":
                    case ".JPG":
                    case ".JPEG":
                        viewer = new Image() { Source = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute)) };
                        break;
                    case ".PDF":
                        viewer = new MoonPdfLib.MoonPdfPanel();
                        (viewer as MoonPdfLib.MoonPdfPanel).OpenFile(filePath);
                        break;
                    case ".WMV":
                    case ".AVI":
                    case ".MP4":
                        viewer = new MediaElement();
                        (viewer as MediaElement).Source = new Uri(filePath, UriKind.RelativeOrAbsolute);
                        break;
                }
            }

            return viewer;
        }
    }
}
