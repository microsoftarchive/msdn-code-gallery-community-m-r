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
    public sealed class MyShuttleAlarmLevelToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var level = (int)value;
            var brushName = new[] {"Yellow", "Orange", "Red"};

            object brush;

            var s = string.Format("MyShuttle{0}AlarmLevelBrush", brushName[level]);

            Application.Current.Resources.TryGetValue(s, out brush);

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
