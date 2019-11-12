using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Reversi.Common.Converters
{
    /// <summary>
    /// Value converter that translates a non-null value to <see cref="Visibility.Visible"/> 
    /// and a null value to <see cref="Visibility.Collapsed"/>, or the reverse if the parameter 
    /// is not null.
    /// </summary>
    public sealed class NullStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value != null ^ parameter != null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
