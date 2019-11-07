// <copyright file="Spinner.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Spinner.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Controls
{
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Spinner animation user control to show progress.
    /// </summary>
    public partial class Spinner : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Spinner"/> class.
        /// </summary>
        public Spinner()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starts the spinner animation.
        /// </summary>
        public void BeginAnimation()
        {
            Storyboard animation = this.Resources["Animation"] as Storyboard;

            if (animation != null)
            {
                animation.Begin();
            }
        }

        /// <summary>
        /// Stops the spinner animation.
        /// </summary>
        public void StopAnimation()
        {
            Storyboard animation = this.Resources["Animation"] as Storyboard;

            if (animation != null)
            {
                animation.Stop();
            }
        }
    }
}