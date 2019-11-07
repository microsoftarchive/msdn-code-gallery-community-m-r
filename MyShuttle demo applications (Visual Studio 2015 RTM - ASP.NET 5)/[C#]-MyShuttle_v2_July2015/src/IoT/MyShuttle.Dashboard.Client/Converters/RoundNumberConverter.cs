using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MyShuttle.Dashboard.Client.Converters
{
    public class RoundNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int decimals = 0;
            if (parameter != null && int.TryParse(parameter.ToString(), out decimals))
            {

            }
            if (value is double || value is float)
            {
                var number = (double)value;
                if (number == 0)
                {
                    return "-";
                }
                var result = Math.Round(number, decimals);
                if (decimals == 0)
                {
                    return (int)result;
                }
                else
                {
                    string spec = "{0:0." + new string(Enumerable.Range(0, decimals).Select(_ => '0').ToArray()) + "}";
                    return string.Format(spec, result);
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
