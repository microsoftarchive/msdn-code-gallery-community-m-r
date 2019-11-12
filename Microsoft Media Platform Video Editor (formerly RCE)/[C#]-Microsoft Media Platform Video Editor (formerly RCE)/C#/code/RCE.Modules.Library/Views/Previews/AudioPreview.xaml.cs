// <copyright file="AudioPreview.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AudioPreview.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library
{
    using System.Windows;

    using Infrastructure.Models;

    /// <summary>
    /// Preview control for <see cref="AudioAsset"/>.
    /// </summary>
    public partial class AudioPreview : AssetPreview
    {
        /// <summary>
        /// The <see cref="DependencyProperty"/> to have the Asset of the preview.
        /// </summary>
        private static readonly DependencyProperty AssetProperty =
            DependencyProperty.Register("Asset", typeof(Asset), typeof(AudioPreview), null);

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPreview"/> class.
        /// </summary>
        public AudioPreview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the asset.
        /// </summary>
        /// <value>The <see cref="Asset"/>.</value>
        public override Asset Asset
        {
            get { return this.GetValue(AssetProperty) as Asset; }
            set { this.SetValue(AssetProperty, value); }
        }

        /// <summary>
        /// Scales the current preview control to the specified size.
        /// </summary>
        /// <param name="size">The size to which the preview control is to be scaled.</param>
        public override void Scale(Size size)
        {
            AudioGrid.Width = size.Width;
            AudioGrid.Height = size.Height;
        }

        /// <summary>
        /// Handles the Click event of the AddAsset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AddAsset_Click(object sender, RoutedEventArgs e)
        {
            this.OnAddingAsset();
        }
    }
}
