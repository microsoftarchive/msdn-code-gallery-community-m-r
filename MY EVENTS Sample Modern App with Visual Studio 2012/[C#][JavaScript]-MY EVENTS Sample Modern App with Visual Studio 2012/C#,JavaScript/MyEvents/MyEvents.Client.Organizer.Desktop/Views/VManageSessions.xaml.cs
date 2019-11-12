using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MyEvents.Client.Organizer.Desktop.ViewModel;

namespace MyEvents.Client.Organizer.Desktop.Views
{
    /// <summary>
    /// Interaction logic for VManageSessions.xaml
    /// </summary>
    public partial class VManageSessions : UserControl
    {   
        /// <summary>
        /// Constructor
        /// </summary>
        public VManageSessions(Api.Client.EventDefinition eventDefinition)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.Loaded += VManageSessions_Loaded;
            (this.DataContext as ManageSessionsViewModel).EventDefinition = eventDefinition;
        }

        void VManageSessions_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ManageSessionsViewModel).PopulateSessions();
        }      
    }
}
