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
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.ViewModel;

namespace MyEvents.Client.Organizer.Desktop.Views
{
    /// <summary>
    /// Interaction logic for VManageEvents.xaml
    /// </summary>
    public partial class VManageEvents : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VManageEvents()
        {
            InitializeComponent();
        }

        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (this.DataContext as ManageEventsViewModel).SelectedEvent = (EventDefinition)grdEvents.SelectedItem;
        }
    }
}
