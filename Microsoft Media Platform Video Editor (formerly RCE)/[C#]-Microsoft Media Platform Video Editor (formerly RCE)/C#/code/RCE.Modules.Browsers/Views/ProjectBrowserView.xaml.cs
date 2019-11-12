// <copyright file="ProjectBrowserView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectBrowserView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Views
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Browser;
    using System.Windows.Controls;

    public partial class ProjectBrowserView : UserControl, IProjectBrowserView
    {
        public ProjectBrowserView()
        {
            InitializeComponent();
        }

        public void SetViewModel(object viewModel)
        {
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HtmlPage.Window.Dispatcher.BeginInvoke(() => this.DisplayCreateNewProjectConfirmation());            
        }

        private void DisplayCreateNewProjectConfirmation() 
        {
            MessageBoxResult result = MessageBox.Show(
                                                        "This will create a new project in the application. Are you sure you want to proceed?",
                                                        "Create New Project",
                                                        MessageBoxButton.OKCancel);
            if (MessageBoxResult.OK == result)
            {
                this.CreateNewProject();
            }
        }

        private void CreateNewProject() 
        {
            Uri source = Application.Current.Host.Source;

            if (source != null)
            {
                string uri = source.AbsoluteUri.Substring(0, source.AbsoluteUri.IndexOf("ClientBin", StringComparison.OrdinalIgnoreCase));

                Uri navigateUri = new Uri(string.Format(CultureInfo.InvariantCulture, uri, "Default.aspx"));

                HtmlPage.Window.Navigate(navigateUri);
            }
        }
    }
}
