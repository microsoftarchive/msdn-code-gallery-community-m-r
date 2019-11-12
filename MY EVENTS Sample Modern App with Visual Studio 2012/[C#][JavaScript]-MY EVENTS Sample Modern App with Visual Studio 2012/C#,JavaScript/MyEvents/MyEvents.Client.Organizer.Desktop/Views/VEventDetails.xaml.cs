using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.ViewModel;
using MyEvents.Client.Organizer.Desktop.Views.UserControls;

namespace MyEvents.Client.Organizer.Desktop.Views
{
    /// <summary>
    /// Interaction logic for VEventDetails.xaml
    /// </summary>
    public partial class VEventDetails : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VEventDetails(EventDefinition eventDefinition)
        {
            InitializeComponent();

            (this.DataContext as EventDetailsViewModel).Event = eventDefinition;

            map.Center = new Location(eventDefinition.Latitude, eventDefinition.Longitude);
            map.Children.Clear();
            map.Children.Add(new CustomPushPin() 
            { 
                VerticalAlignment = System.Windows.VerticalAlignment.Center, 
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Width = 17,
                Height = 37
            });
        }
    }
}
