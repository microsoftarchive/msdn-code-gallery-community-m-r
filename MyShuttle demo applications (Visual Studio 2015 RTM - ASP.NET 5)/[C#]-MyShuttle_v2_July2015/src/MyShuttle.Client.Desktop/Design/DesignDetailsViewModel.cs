using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Desktop.ViewModels;

namespace MyShuttle.Client.Desktop
{
    public class DesignDetailsViewModel
    {
        public StatisticsViewModel Statistics { get; set; }

        public Vehicle SelectedVehicle { get; set; }

        public Driver SelectedDriver { get; set; }

        public bool IsLoadingDriver { get; set; }

        public DesignDetailsViewModel()
        {
            Statistics = new StatisticsViewModel();
        }
    }
}
