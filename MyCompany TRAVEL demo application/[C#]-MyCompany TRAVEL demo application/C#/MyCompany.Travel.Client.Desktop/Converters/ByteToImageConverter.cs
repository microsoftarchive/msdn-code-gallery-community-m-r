namespace MyCompany.Travel.Client.Desktop.Converters
{
    using System;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Byte to image converter
    /// </summary>
    public class ByteToImageConverter : IValueConverter
    {
        /// <summary>
        /// Convert a byte array to an image.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream((byte[])value);
                bi.EndInit();

                return bi;
            }
            return null;
        }

        /// <summary>
        /// Convert an image to a byte array
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
