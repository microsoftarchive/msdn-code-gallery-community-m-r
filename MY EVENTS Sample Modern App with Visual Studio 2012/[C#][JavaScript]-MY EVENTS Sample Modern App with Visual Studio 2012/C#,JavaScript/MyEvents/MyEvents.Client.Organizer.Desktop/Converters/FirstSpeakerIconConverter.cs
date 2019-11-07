using System;
using System.Windows.Data;

namespace MyEvents.Client.Organizer.Desktop.Converters
{
    /// <summary>
    /// Gives a white image for the first speaker or blue image for the rest of speakers in the Top Speaker list
    /// </summary>
    public class FirstSpeakerIconConverter : IValueConverter
    {
        /// <summary>
        /// Returns a white person icon for the first person on the list and a blue one for the rest
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
             var speakerPosition = (int)value;
            if (speakerPosition == 0)
            {
                return @"/MyEvents.Client.Organizer.Desktop;component/Resources/Images/personWhite.png";
            }
            else
            {
                return @"/MyEvents.Client.Organizer.Desktop;component/Resources/Images/personBlue.png";
            }           
        }

        /// <summary>
        /// Returns a white person icon for the first person on the list and a blue one for the rest
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
