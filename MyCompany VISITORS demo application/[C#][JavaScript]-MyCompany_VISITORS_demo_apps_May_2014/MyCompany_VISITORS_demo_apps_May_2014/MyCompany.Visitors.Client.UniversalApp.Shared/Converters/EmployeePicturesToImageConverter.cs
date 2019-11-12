namespace MyCompany.Visitors.Client.UniversalApp.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Converter from ICollection of Employee Pictures value to Image.
    /// </summary>
    public class EmployeePicturesToImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts ICollection of EmployeePicture to Image
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return "/Assets/no_photo_big.png";
            using (var ms = new InMemoryRandomAccessStream())
            {
                var val = (ICollection<EmployeePicture>)value;
                if (val.Any())
                {
                    byte[] bImage = val.First().Content;

                    using (var writer = new DataWriter(ms.GetOutputStreamAt(0)))
                    {
                        writer.WriteBytes(bImage);
                        writer.StoreAsync().GetResults();
                    }
                    var image = new BitmapImage();
                    image.SetSource(ms);
                    return image;
                }
                return "/Assets/no_photo_big.png";
            }
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
