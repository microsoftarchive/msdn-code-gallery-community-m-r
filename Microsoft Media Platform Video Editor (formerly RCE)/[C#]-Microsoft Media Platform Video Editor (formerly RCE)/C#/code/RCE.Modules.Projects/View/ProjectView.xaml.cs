// <copyright file="ProjectView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Windows;
    using System.Windows.Browser;
    using System.Windows.Controls;
    using System.Windows.Data;

    using RCE.Infrastructure;

    /// <summary>
    /// Provides the implementation for <see cref="IProjectView"/>.
    /// </summary>
    public partial class ProjectView : UserControl, IProjectView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectView"/> class.
        /// </summary>
        public ProjectView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the <see cref="IProjectViewPresenter"/> presenter of the view.
        /// </summary>
        /// <value>A <see also="IProjectViewPresenter"/> that represents the presenter of the view.</value>
        public IProjectViewPresenter Model
        {
            get
            {
                return this.DataContext as IProjectViewPresenter;
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Handles the project Hyperlink click event.
        /// </summary>
        /// <param name="sender">The <see cref="HyperlinkButton"/>.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>.</param>
        private void ProjectLink_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton linkButton = sender as HyperlinkButton;

            if (linkButton != null && linkButton.Content != null)
            {
                Uri source = Application.Current.Host.Source;

                StringBuilder uriBuilder = new StringBuilder();

                string hostname = source.AbsoluteUri.Substring(0, source.AbsoluteUri.IndexOf("ClientBin", StringComparison.OrdinalIgnoreCase));

                uriBuilder.Append(hostname);
                uriBuilder.AppendFormat(CultureInfo.InvariantCulture, "Default.aspx?projectId={0}", linkButton.Content);

                Uri navigateUri = new Uri(uriBuilder.ToString());

                HtmlPage.Window.Navigate(navigateUri);
            }
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Binding deleteCommand = new Binding("DeleteCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["DeleteCommand"]).SetBinding(BindingHelper.ValueProperty, deleteCommand);
        }
    }
}