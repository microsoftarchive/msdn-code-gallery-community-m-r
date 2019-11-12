namespace MyShuttle.Dashboard.Client.Converters
{
    using System;
    using System.Globalization;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public sealed class MyShuttleTopDriverTileIndexToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var styles = new[]{ "Black", "Gray" };

            var styleKey = string.Format(CultureInfo.CurrentCulture, "MyShuttle{0}TileBrush", styles[(int) value == 0 ? 0 : 1]);

            object brush;
            Application.Current.Resources.TryGetValue(styleKey, out brush);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
