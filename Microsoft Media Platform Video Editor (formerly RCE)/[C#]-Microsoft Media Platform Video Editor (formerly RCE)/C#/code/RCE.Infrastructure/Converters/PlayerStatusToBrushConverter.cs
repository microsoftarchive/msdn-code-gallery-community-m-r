// <copyright file="PlayerStatusToBrushConverter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerStatusToBrushConverter.cs                     
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
    using System.Windows.Data;
    using System.Windows.Media;
    using RCE.Infrastructure.Models;

    public class PlayerStatusToBrushConverter : IValueConverter
    {
        public Brush NotReadyBrush { get; set; }

        public Brush ReadyBrush { get; set; }

        public Brush LoadingBrush { get; set; }

                /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is PlayerStatusType))
            {
                return this.NotReadyBrush;
            }

            PlayerStatusType playerStatus = (PlayerStatusType)value;
            Brush returnBrush = this.NotReadyBrush;

                    switch (playerStatus)
                    {
                        case PlayerStatusType.NotReady:
                    returnBrush = this.NotReadyBrush;
                    break;
                case PlayerStatusType.Ready:
                    returnBrush = this.ReadyBrush;
                    break;
                case PlayerStatusType.Loading:
                    returnBrush = this.LoadingBrush;
                    break;
            }

            return returnBrush;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
