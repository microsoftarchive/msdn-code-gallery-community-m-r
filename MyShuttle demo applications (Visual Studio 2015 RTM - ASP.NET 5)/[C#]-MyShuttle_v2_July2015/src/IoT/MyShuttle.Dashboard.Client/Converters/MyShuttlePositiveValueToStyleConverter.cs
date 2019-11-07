using Windows.UI;
using Windows.UI.Xaml.Media;
using MyShuttle.Dashboard.Client.Models;

namespace MyShuttle.Dashboard.Client.Converters
{
    using System;
    using System.Globalization;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public sealed class MyShuttlePositiveValueToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var styles = new[] { "Green", "Red" };
            var driver = (double)value;

            var styleKey = string.Format(CultureInfo.CurrentCulture, "MyShuttle{0}TextBlock", styles[driver < 0 ? 1 : 0]);

            object style;
            Application.Current.Resources.TryGetValue(styleKey, out style);

            return style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
