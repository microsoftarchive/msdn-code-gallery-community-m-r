using System;
using MyShuttle.Client.Core.Model.Enums;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class TabSelectedToStyleConverter : IValueConverter
    {
        private readonly Style _primaryTextBlockStyle;
        private readonly Style _primaryDisabledTextBlockStyle;

        public TabSelectedToStyleConverter()
        {
            _primaryTextBlockStyle = Application.Current.Resources["PrimaryTextBlock"] as Style;
            _primaryDisabledTextBlockStyle = Application.Current.Resources["PrimaryDisabledTextBlock"] as Style;
        }

        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            var rideDetailWorkflow = (RideDetailWorkflow)value;
            var workflowPointToCompare = (RideDetailWorkflow)Enum.Parse(typeof(RideDetailWorkflow), parameter.ToString());    
            if (rideDetailWorkflow == workflowPointToCompare)
            {
                return _primaryTextBlockStyle;
            }
            return _primaryDisabledTextBlockStyle;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language)
        {
            throw new System.NotImplementedException();
        }
    }
}
