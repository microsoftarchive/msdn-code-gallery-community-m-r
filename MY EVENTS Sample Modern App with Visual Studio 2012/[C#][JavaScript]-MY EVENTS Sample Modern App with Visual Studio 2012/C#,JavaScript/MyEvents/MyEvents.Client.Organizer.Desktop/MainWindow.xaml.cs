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
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;
using MyEvents.Client.Organizer.Desktop.Views;

namespace MyEvents.Client.Organizer.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main window control.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Border x = new Border();
            
        }      

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.WindowState == System.Windows.WindowState.Maximized)
                    this.WindowState = System.Windows.WindowState.Normal;
                else
                    this.WindowState = System.Windows.WindowState.Maximized;
            }
            this.DragMove();
        }

        private void MinimizeButton(object sender, RoutedEventArgs e)
        {
            this.WindowState =WindowState.Minimized;
        }

        private void MaximizeButton(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }            
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
