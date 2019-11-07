// <copyright file="IntegerToBrushConverter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IntegerToBrushConverter.cs                     
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class IntegerToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? val = value as int?;
            if (!val.HasValue)
            {
                return new SolidColorBrush(Colors.Black);
            }

            if (val.Value < 0 || val.Value > 16)
            {
                return new SolidColorBrush(Colors.Black);
            }

            Color[] colors = 
                            {
                                Colors.Red, Colors.Green, Colors.Blue, Colors.Orange, Colors.Purple, Colors.Brown, 
                                Colors.White, Colors.Cyan, Colors.Yellow, Color.FromArgb(255, 255, 62, 150), Color.FromArgb(255, 132, 112, 255), 
                                Color.FromArgb(255, 0, 134, 139), Color.FromArgb(255, 0, 250, 154), 
                                Color.FromArgb(255, 124, 252, 0), Color.FromArgb(255, 250, 128, 114), 
                                Color.FromArgb(255, 197, 193, 170), Color.FromArgb(255, 142, 142, 56), 
                            };
                        
            return new SolidColorBrush(colors[val.Value]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush val = value as SolidColorBrush;
            Dictionary<Color, int> mappings = new Dictionary<Color, int>
                {
                    { Colors.Red, 0 }, 
                    { Colors.Green, 1 }, 
                    { Colors.Blue, 2 }, 
                    { Colors.Orange, 3 }, 
                    { Colors.Purple, 4 }, 
                    { Colors.Brown, 5 }, 
                    { Colors.White, 6 }, 
                    { Colors.Cyan, 7 }, 
                    { Colors.Yellow, 8 }, 
                    { Color.FromArgb(255, 255, 62, 150),  9 }, 
                    { Color.FromArgb(255, 132, 112, 255), 10 }, 
                    { Color.FromArgb(255, 0, 134, 139), 11 }, 
                    { Color.FromArgb(255, 0, 250, 154), 12 }, 
                    { Color.FromArgb(255, 124, 252, 0), 13 }, 
                    { Color.FromArgb(255, 250, 128, 114), 14 }, 
                    { Color.FromArgb(255, 56, 142, 142), 15 }, 
                    { Color.FromArgb(255, 142, 142, 56), 16 }
                };

            if (val == null || mappings.ContainsKey(val.Color))
            {
                return -1;
            }

            return mappings[val.Color];
        }
    }
}