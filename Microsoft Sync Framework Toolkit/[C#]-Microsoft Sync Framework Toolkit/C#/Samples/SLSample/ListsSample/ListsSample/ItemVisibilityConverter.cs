// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ListsSample
{
    /// <summary>
    /// This is a simple converter which returns a visibility based on an object.  If the object is 
    /// null,the visibility is collapsed, otherwise, the visibility is visible.  This is used when
    /// binding to the selected item of a list box.
    /// </summary>
    public class ItemVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
