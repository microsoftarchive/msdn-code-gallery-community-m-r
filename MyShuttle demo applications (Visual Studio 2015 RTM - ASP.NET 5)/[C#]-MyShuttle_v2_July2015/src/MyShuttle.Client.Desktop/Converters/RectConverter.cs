using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace MyShuttle.Client.Desktop.Converters
{
    public class RectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null && values.Length == 4 && values[0] is double && values[1] is double && values[2] is double && values[3] is double)
            {
                var rect = new Rect(new Point((double)values[0], (double)values[1]), new Size((double)values[2], (double)values[3]));
                return rect;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
