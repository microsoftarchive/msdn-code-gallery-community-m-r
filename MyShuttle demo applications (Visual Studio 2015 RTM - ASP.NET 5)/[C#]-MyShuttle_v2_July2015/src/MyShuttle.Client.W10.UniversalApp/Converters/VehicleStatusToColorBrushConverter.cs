namespace MyShuttle.Client.W10.UniversalApp.Converters
{
    using Core.DocumentResponse;
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    public class VehicleStatusToColorBrushConverter : IValueConverter
    {
        private readonly SolidColorBrush _freeVehicleColorBrush;
        private readonly SolidColorBrush _occupiedVehicleColorBrush;

        public VehicleStatusToColorBrushConverter()
        {
            _freeVehicleColorBrush = Application.Current.Resources["FreeStatusColorBrush"] as SolidColorBrush;
            _occupiedVehicleColorBrush = Application.Current.Resources["OccupiedStatusColorBrush"] as SolidColorBrush;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var vehicleStatus = (VehicleStatus)value;

            return (vehicleStatus == VehicleStatus.Free) ?
                _freeVehicleColorBrush :
                _occupiedVehicleColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new System.NotImplementedException();
        }
    }
}
