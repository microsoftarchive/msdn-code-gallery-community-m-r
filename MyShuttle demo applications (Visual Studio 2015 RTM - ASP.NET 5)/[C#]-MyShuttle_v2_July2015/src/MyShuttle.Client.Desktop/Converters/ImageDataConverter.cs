using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MyShuttle.Client.Desktop.Converters
{
    public class ImageDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var pictureBytes = value as byte[];
            if (pictureBytes != null)
            {
                var data = pictureBytes;
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(data);
                image.EndInit();
                image.Freeze();
                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
