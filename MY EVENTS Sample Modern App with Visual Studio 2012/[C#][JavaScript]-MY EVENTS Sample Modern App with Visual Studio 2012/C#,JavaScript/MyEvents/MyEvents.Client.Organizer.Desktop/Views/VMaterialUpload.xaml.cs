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
    /// Interaction logic for VMaterialUpload.xaml
    /// </summary>
    public partial class VMaterialUpload : UserControl
    {
        private Api.Client.Session selectedSession;
       
        /// <summary>
        /// Constructor with the session to wich the materials are going to be uploaded
        /// </summary>
        /// <param name="selectedSession"></param>
        public VMaterialUpload(Session selectedSession)
        {
            InitializeComponent();
            this.selectedSession = selectedSession;
            (this.DataContext as UploadMaterialViewModel).CurrentSession = selectedSession;
        }
    }
}
