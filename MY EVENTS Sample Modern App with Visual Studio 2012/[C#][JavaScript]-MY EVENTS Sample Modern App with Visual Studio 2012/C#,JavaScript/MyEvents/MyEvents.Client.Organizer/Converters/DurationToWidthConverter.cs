using System;
using Windows.UI.Xaml.Data;

namespace MyEvents.Client.Organizer.Converters
{
    /// <summary>
    /// Convert the event duration to pixels.
    /// </summary>
    public class DurationToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int width = 0;
            if (value != null)
            {
                int.TryParse(value.ToString(), out width);

                return width * 4;
            }

            return width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
