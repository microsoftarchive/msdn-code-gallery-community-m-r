using System.Windows;
using WPF.MDI;

namespace WPFMDIForm
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void userRegistration_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.Children.Clear();
            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = " User Registration",
                Height = (System.Windows.SystemParameters.PrimaryScreenHeight - 15),
                Width = (System.Windows.SystemParameters.PrimaryScreenWidth - 15),
                Style = null,
                //Here UserRegistration is the class that you have created for mainWindow.xaml user control.
                Content = new UserRegistration()
            });
        }

        private void compRegistration_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.Children.Clear();
            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = " Company Registration",
                Height = (System.Windows.SystemParameters.PrimaryScreenHeight - 15),
                Width = (System.Windows.SystemParameters.PrimaryScreenWidth - 15),
                Style = null,
                //Here compRegistration is the class that you have created for mainWindow.xaml user control.
                Content = new CompRegistration()
            });
        } 

    }
}
