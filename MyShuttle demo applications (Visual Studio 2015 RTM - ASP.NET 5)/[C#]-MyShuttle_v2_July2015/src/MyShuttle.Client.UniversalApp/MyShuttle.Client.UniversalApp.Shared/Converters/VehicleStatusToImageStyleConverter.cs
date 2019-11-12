using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using MyShuttle.Client.Core.DocumentResponse;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class VehicleStatusToImageStyleConverter : IValueConverter
    {
        private readonly Style _freeVehicleStyle;
        private readonly Style _occupiedVehicleStyle;
        private readonly Style _selectedVehicleStyle;

        public VehicleStatusToImageStyleConverter()
        {
            _freeVehicleStyle = Application.Current.Resources["FreeVehiclePushpinImage"] as Style;
            _occupiedVehicleStyle = Application.Current.Resources["OccupiedVehiclePushpinImage"] as Style;
            _selectedVehicleStyle = Application.Current.Resources["SelectedVehiclePushpinImage"] as Style;
        }

        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            var vehicle = (Vehicle)value;

            if (vehicle.IsSelected)
            {
                return _selectedVehicleStyle;
            }

            if (vehicle.VehicleStatus == VehicleStatus.Free)
            {
                return _freeVehicleStyle;
            }

            return _occupiedVehicleStyle;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language)
        {
            throw new System.NotImplementedException();
        }
    }
}
