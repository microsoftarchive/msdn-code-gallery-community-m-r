using System;
using System.Configuration;
using System.Windows.Data;

namespace MyEvents.Client.Organizer.Desktop.Converters
{

    /// <summary>
    /// Class that contains the logic to transform a facebook id to a photo
    /// </summary>
    public class FacebookIdToPhotoURLConverter : IValueConverter
    {
        /// <summary>
        /// Converts from a facebook id to the photo url
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool _offlineMode = false;
            bool.TryParse(ConfigurationManager.AppSettings.Get("OfflineMode"), out _offlineMode);
            if (_offlineMode)
                return "/Resources/Images/FacebookUserWithoutPhoto.gif";

            if (value != null)
            {
                string result = string.Empty;
                result = string.Format(@"https://graph.facebook.com/{0}/picture", value.ToString());
                return result;
            }
            else
            {
                return null;
            }
           
        }

        /// <summary>
        /// Converts from a facebook id to the photo url
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
