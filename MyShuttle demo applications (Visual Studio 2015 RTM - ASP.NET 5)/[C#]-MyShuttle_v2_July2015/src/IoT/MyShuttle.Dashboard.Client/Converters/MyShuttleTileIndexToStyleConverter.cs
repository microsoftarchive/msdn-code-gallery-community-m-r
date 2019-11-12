namespace MyShuttle.Dashboard.Client.Converters
{
    using System;
    using System.Globalization;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public sealed class MyShuttleTileIndexToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var styles = new[]{ "Green", "Black", "Red" };

            var styleKey = string.Format(CultureInfo.CurrentCulture, "MyShuttle{0}Tile", styles[(int) value]);

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
