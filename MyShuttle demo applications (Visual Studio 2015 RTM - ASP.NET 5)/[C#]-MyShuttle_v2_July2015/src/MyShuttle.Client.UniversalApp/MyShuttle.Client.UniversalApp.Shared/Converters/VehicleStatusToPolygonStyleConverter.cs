using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using MyShuttle.Client.Core.DocumentResponse;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class VehicleStatusToPolygonStyleConverter : IValueConverter
    {
        private readonly Style _freeVehicleStyle;
        private readonly Style _occupiedVehicleStyle;

        public VehicleStatusToPolygonStyleConverter()
        {
            _freeVehicleStyle = Application.Current.Resources["FreeVehicleIndicatorPolygon"] as Style;
            _occupiedVehicleStyle = Application.Current.Resources["OccupiedVehicleIndicatorPolygon"] as Style;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var vehicleStatus = (VehicleStatus)value;
            return (vehicleStatus == VehicleStatus.Free ? _freeVehicleStyle : _occupiedVehicleStyle);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
