// <copyright file="TitlePreview.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TitlePreview.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline
{
    using System;
    using System.Windows;
    using Infrastructure.Models;

    using RCE.Infrastructure;

    /// <summary>
    /// Preview control for the <see cref="TitleAsset"/> in the timeline.
    /// </summary>
    public partial class TitlePreview : IPreview
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TitlePreview"/> class.
        /// </summary>
        /// <param name="asset">The asset.</param>
        public TitlePreview(TitleAsset asset)
        {
            // Required to initialize variables
            InitializeComponent();

            if (asset == null)
            {
                throw new ArgumentNullException("asset");
            }

            this.DataContext = asset;
        }

        public event EventHandler<DataEventArgs<bool>> ItemLocked;

        /// <summary>
        /// Sets the selected.
        /// </summary>
        /// <param name="selected">If set to <c>true</c> [selected].</param>
        public void SetSelected(bool selected)
        {
            this.SelectionBox.Visibility = selected ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}