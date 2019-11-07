using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MyEvents.Client.Organizer.Converters
{
    public class ByteToImageConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is byte[])
            {

                //WriteableBitmap bitmap = new WriteableBitmap(100, 100);

                //System.IO.Stream bitmapStream = null;
                //bitmapStream = bitmap.PixelBuffer.AsStream();
                //bitmapStream.Position = 0;
                //bitmapStream.Write(value as byte[], 0, (value as byte[]).Length);

                //bitmapStream.Flush();

                InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
                DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
                writer.WriteBytes(value as byte[]);
                var x = writer.StoreAsync().AsTask().Result;
                BitmapImage image = new BitmapImage();
                image.SetSource(randomAccessStream);

                return image;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
