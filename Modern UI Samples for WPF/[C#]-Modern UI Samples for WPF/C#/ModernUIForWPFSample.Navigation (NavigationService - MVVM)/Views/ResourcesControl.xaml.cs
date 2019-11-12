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

namespace ModernUIForWPFSample.Navigation.Views
{
    /// <summary>
    /// Interaction logic for ResourcesControl.xaml.
    /// </summary>
    public partial class ResourcesControl 
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
    }
}
