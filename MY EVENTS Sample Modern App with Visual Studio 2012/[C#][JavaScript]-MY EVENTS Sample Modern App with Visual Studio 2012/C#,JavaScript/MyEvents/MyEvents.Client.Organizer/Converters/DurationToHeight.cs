using System;
using Windows.UI.Xaml.Data;

namespace MyEvents.Client.Organizer.Converters
{
    /// <summary>
    /// Event duration to height.
    /// </summary>
    public class DurationToHeightConverter : IValueConverter
    {
        /// <summary>
        /// Duration to height
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int height = 0;
            if (value != null)
            {
                int.TryParse(value.ToString(), out height);

                height = height * 2;
                if (height > 10)
                    height -= 10;
                return height;
            }

            return height;
        }

        /// <summary>
        /// Height to duration
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
