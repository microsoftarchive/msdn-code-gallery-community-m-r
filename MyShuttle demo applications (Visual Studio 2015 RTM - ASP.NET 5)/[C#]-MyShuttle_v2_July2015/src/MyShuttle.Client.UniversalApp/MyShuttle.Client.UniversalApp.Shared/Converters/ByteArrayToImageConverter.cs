using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MyShuttle.Client.UniversalApp.Converters
{
    /// <summary>
    /// Converter from Byte Array to Image.
    /// </summary>
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                using (var ms = new InMemoryRandomAccessStream())
                {
                    var val = (byte[])value;
                    using (var writer = new DataWriter(ms.GetOutputStreamAt(0)))
                    {
                        writer.WriteBytes(val);
                        writer.StoreAsync().GetResults();
                    }
                    var image = new BitmapImage();
                    image.SetSource(ms);
                    return image;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
