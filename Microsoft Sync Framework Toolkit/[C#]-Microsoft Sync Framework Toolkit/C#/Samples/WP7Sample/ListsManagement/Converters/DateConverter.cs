// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Windows.Data;
using System.Windows.Media;

namespace ListsManagement.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && targetType == typeof(Brush))
            {
                int i = ((DateTime)value).Subtract(DateTime.Now).Days;
                if (i <= 0)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                else if (i <= 3)
                {
                    return new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    return new SolidColorBrush(Colors.White);
                }

            }
            else 
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
