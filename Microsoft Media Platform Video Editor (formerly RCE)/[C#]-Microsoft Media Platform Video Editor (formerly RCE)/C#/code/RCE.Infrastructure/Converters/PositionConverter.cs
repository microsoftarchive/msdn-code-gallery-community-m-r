// <copyright file="PositionConverter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PositionConverter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Converters
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Windows.Data;

    /// <summary>
    /// Converts a double into a time string representation.
    /// </summary>
    public class PositionConverter : IValueConverter
    {
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double seconds = 0;
            double minutes = 0;
            double hours = 0;

            double time;

            if (value != null && double.TryParse(value.ToString(), out time))
            {
                seconds = Math.Floor(time) % 60;
                minutes = Math.Floor(time / 60) % 60;
                hours = Math.Floor(time / 3600) % 60;
            }

            return hours.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + ":" + minutes.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + ":" + seconds.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        /// <param name="value">The target data being passed to the source.</param><param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param><param name="parameter">An optional parameter to be used in the converter logic.</param><param name="culture">The culture of the conversion.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Regex timePattern = new Regex(@"^(\d+):(\d+):(\d+)$");

            double time = -1;

            if (value != null && timePattern.IsMatch(value.ToString()))
            {
                try
                {
                    var timecode = value.ToString().Split(':');
                    var hours = int.Parse(timecode[0], CultureInfo.InvariantCulture);
                    var minutes = int.Parse(timecode[1], CultureInfo.InvariantCulture);
                    var seconds = int.Parse(timecode[2], CultureInfo.InvariantCulture);

                    time = seconds + (minutes * 60) + (hours * 3600);
                }
                catch
                {
                    time = -1;
                }
            }

            return time;
        }
    }
}