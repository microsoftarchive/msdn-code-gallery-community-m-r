// <copyright file="NotificationView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: NotificationView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Views
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// The notification view that shows progress information.
    /// </summary>
    public partial class NotificationView : UserControl, INotificationView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationView"/> class.
        /// </summary>
        public NotificationView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows a progress bar.
        /// </summary>
        public void ShowProgressBar()
        {
            Application.Current.RootVisual.Opacity = 0.5;
            this.ProgressBar.Opacity = 1;
            this.Spinner.BeginAnimation();
            this.ProgressBar.IsOpen = true;
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        public void HideProgressBar()
        {
            this.ProgressBar.IsOpen = false;
            this.Spinner.StopAnimation();
            Application.Current.RootVisual.Opacity = 1;
        }
    }
}
