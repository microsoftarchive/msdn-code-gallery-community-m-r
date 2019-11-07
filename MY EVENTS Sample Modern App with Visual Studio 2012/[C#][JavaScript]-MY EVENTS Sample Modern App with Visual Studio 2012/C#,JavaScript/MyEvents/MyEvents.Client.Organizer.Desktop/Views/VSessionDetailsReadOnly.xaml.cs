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
    /// Interaction logic for VEditSessionDetails.xaml
    /// </summary>
    public partial class VEditSessionDetailsReadOnly : UserControl
    {
        /// <summary>
        /// Constructor of the VEidSessionDetails view with parameter
        /// </summary>
        /// <param name="sessionToEdit"></param>
        public VEditSessionDetailsReadOnly(Session sessionToEdit)
        {
            InitializeComponent();
            (this.DataContext as EditSessionDetailsViewModel).CurrentSession = sessionToEdit;

        }
    }
}
