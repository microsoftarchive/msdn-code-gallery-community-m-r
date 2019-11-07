using Cirrious.CrossCore.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class PriceToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var loader = new ResourceLoader();
            var returnValue = string.Format("{0}{1}", loader.GetString("LocalCurrency"),((double)value).ToString(CultureInfo.InvariantCulture));

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
