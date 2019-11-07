// <copyright file="ThemedImagePathConverter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DurationConverter.cs                     
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
    using System.Windows.Data;

    /// <summary>
    /// <see cref="IValueConverter"/> to convert <see cref="String"/> a image-resource path string to a custom image path for the Application's theme.
    /// </summary>
    public class ThemedImagePathConverter : IValueConverter
    {
        /// <summary>
        /// Convert the passed-in string to a path to a image resource 
        /// customized for the application's current Theme.  The algorithm looks for the "images" substring
        /// and replaces it with "images/Themes/"{ThemeName} (i.e. "White") that is passed in
        /// to the converter via a ConverterParameter.  This converter will be used for Module tab icon files.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property: System.String</param>
        /// <param name="parameter">A parameter used in the converter logic representing the name of Application Theme in use</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string themeName = parameter as string;
            string newPath = value as string;

            if (!string.IsNullOrEmpty(newPath) && !string.IsNullOrEmpty(themeName))
            {
                if (themeName != "Default"
                    && newPath.ToUpper(CultureInfo.InvariantCulture).Contains("IMAGES"))
                {
                    string themePath = "images/Themes/" + themeName;
                    newPath = newPath.Replace("images", themePath);
                    newPath = newPath.Replace("Images", themePath);
                    return newPath;
                }
            }

            return value;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
