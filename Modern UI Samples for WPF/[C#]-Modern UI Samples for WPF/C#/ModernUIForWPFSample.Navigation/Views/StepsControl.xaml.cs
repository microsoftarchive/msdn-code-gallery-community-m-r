// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StepsControl.xaml.cs" company="saramgsilva">
//   Copyright (c) 2014 saramgsilva. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for StepsControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace ModernUIForWPFSample.Navigation.Views
{
    /// <summary>
    /// Interaction logic for StepsControl.xaml.
    /// </summary>
    public partial class StepsControl : IContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepsControl"/> class.
        /// </summary>
        public StepsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            Debug.WriteLine("StepsControl- OnFragmentNavigation");
        }

        /// <summary>
        /// Called when this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            Debug.WriteLine("StepsControl -OnNavigatedFrom");
        }

        /// <summary>
        /// Called when a this instance becomes the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("StepsControl- OnNavigatedTo");
        }

        /// <summary>
        /// Called just before this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">
        /// An object that contains the navigation data.
        /// </param>
        /// <remarks>
        /// The method is also invoked when parent frames are about to navigate.
        /// </remarks>
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Debug.WriteLine("StepsControl- OnNavigatingFrom");
        }
    }
}
