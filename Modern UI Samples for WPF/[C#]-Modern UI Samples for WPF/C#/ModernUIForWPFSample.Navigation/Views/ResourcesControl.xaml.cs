// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcesControl.xaml.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for ResourcesControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Windows.Navigation;
using FirstFloor.ModernUI.Windows;
using FragmentNavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs;
using NavigatingCancelEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs;
using NavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs;

namespace ModernUIForWPFSample.Navigation.Views
{
    /// <summary>
    /// Interaction logic for ResourcesControl.xaml.
    /// </summary>
    public partial class ResourcesControl : IContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesControl"/> class.
        /// </summary>
        public ResourcesControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the RequestNavigate event of the Hyperlink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RequestNavigateEventArgs"/> instance containing the event data.</param>
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        /// <summary>
        /// The on fragment navigation.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            Debug.WriteLine("ResourcesControl - OnFragmentNavigation");
        }

        /// <summary>
        /// The on navigated from.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            Debug.WriteLine("ResourcesControl - OnNavigatedFrom");
        }

        /// <summary>
        /// The on navigated to.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("ResourcesControl - OnNavigatedTo");
        }

        /// <summary>
        /// The on navigating from.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Debug.WriteLine("ResourcesControl - OnNavigatingFrom");
        }
    }
}
