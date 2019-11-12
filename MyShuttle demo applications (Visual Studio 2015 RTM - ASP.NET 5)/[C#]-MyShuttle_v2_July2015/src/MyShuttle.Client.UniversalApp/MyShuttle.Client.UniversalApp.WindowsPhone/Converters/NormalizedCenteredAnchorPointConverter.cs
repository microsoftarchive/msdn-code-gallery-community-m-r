using System;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace MyShuttle.Client.UniversalApp.Converters
{
    class NormalizedCenteredAnchorPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // This is a "workaround" in order to set the normalized anchor point into an item template in the map control (Windows Phone bug).
            // Discussion about this: http://social.msdn.microsoft.com/Forums/lync/en-US/e82df791-3400-4e41-99cd-14d95134df72/windows-phone-81-store-app-mapcontrol-mapitemscontrol-bugs?forum=wpdevelop
            return new Point(0.5, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
