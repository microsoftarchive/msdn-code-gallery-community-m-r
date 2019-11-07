using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using MyShuttle.Dashboard.Client.Models;

namespace MyShuttle.Dashboard.Client.Converters
{
    public sealed class MyShuttleRateToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var rate = value as Rate;
            var brushName = new[] {"Good", "Medium", "Bad"};

            object brush;

            var index = rate?.AvgRate < 3 ? 2 : rate?.AvgRate < 6 ? 1 : 0;
            var s = string.Format("MyShuttle{0}RateBrush", brushName[index]);
            Application.Current.Resources.TryGetValue(s, out brush);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
