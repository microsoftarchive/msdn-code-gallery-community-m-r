// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using ListsManagement.ViewModels;
using System.Threading;

namespace ListsManagement
{
    public partial class BootstrapView : PhoneApplicationPage
    {
        public BootstrapView()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(BootstrapView_Loaded);
        }

        void BootstrapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Check to see if user is logged in.
            if (SettingsViewModel.Instance.UserName == null)
            {
                this.loginPanel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.welcomePanel.Visibility = System.Windows.Visibility.Visible;
                this.welcomeTxt.Text = string.Format("Welcome {0}. Please wait while we load your offline cache.", SettingsViewModel.Instance.UserName);
                // Sleep for a second to show the welcome screen
                Thread.Sleep(1000);
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void addUsrBtn_Click(object sender, RoutedEventArgs e)
        {
            this.loginStatus.Text = "Please wait while we try to log you in...";
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri(string.Format(SyncContextInstance.LoginUriFormat, this.unameTxt.Text)));
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.loginStatus.Text = "Unable to login. Try again. Error: " + e.Error.Message;
            }
            else
            {
                this.loginStatus.Text = "Login successful. Saving settings file. Please wait...";
                SettingsViewModel.Instance.UserName = this.unameTxt.Text;
                SettingsViewModel.Instance.UserId = new Guid(e.Result);
                try
                {
                    SettingsViewModel.SaveSettingsFile();
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                catch (Exception)
                {
                    this.loginStatus.Text = "Error in saving settings file. Restart app.";
                }
            }
        }
    }
}