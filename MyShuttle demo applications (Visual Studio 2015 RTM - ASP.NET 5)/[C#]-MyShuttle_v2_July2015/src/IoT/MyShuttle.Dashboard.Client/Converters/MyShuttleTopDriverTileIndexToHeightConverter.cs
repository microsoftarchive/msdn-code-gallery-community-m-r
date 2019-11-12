using MyShuttle.Dashboard.Client.Helpers;
using MyShuttle.Dashboard.Client.Models;

namespace MyShuttle.Dashboard.Client.Converters
{
    using System;
    using System.Globalization;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public sealed class MyShuttleTopDriverTileIndexToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isSmallRes = WindowHelper.IsSmallResolution();

            var tileHeight = !isSmallRes ? new[] { 410, 270, 270, 270, 130 } : new[] { 270, 130, 130, 130, 130 };

            var index = (int)value;

            return tileHeight[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
