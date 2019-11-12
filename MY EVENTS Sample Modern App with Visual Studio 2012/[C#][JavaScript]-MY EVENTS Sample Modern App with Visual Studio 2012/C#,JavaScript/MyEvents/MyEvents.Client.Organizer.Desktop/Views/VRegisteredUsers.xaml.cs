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
using MyEvents.Client.Organizer.Desktop.ViewModel;

namespace MyEvents.Client.Organizer.Desktop.Views
{
    /// <summary>
    /// Interaction logic for VRegisteredUsers.xaml
    /// </summary>
    public partial class VRegisteredUsers : UserControl
    {
        /// <summary>
        /// Constructor with the event from which the registered users are obtained
        /// </summary>
        public VRegisteredUsers(Api.Client.EventDefinition eventDefinition)
        {
            InitializeComponent();
            var dataContext = (this.DataContext as RegisteredUsersViewModel);
            dataContext.CurrentEvent = eventDefinition;
        }
    }
}
