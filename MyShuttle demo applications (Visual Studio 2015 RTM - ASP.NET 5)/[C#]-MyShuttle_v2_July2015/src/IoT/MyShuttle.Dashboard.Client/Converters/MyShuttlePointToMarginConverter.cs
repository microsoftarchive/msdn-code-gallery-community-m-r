namespace MyShuttle.Dashboard.Client.Converters
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.Foundation;

    public sealed class MyShuttlePointToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var point = (Point)value;
            var margin = new Thickness { Left = point.X - 7, Top = point.Y - 7, Right = 0, Bottom = 0 };
            return margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
