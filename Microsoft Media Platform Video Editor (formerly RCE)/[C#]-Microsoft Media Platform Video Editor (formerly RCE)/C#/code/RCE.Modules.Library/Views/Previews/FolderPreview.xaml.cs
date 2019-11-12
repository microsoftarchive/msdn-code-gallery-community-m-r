// <copyright file="FolderPreview.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FolderPreview.xaml.cs                     
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
    using System;
    using System.Windows;
    using RCE.Infrastructure.Models;

    /// <summary>
    /// Control to dispaly the preview of the Folder Asset.
    /// </summary>
    public partial class FolderPreview : AssetPreview
    {
        // 24 = 12(Canvas left) + 12 (Canvas Top)

        /// <summary>
        /// Total margin value in x coordinate.
        /// </summary>
        private const double MarginX = 10;

        // 35 = 15 (Height of the first row in grid) + 20 (Height of the 3rd row of the grid)

        /// <summary>
        /// Total margin value in y coordinate.
        /// </summary>
        private const double MarginY = 12;

        /// <summary>
        /// DependencyProperty for Asset.
        /// </summary>
        private static readonly DependencyProperty AssetProperty =
            DependencyProperty.RegisterAttached("Asset", typeof(Asset), typeof(FolderPreview), null);

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderPreview"/> class.
        /// </summary>
        public FolderPreview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when Add asset clicked.
        /// </summary>
        public override event EventHandler AddingAsset;

        /// <summary>
        /// Gets or sets the asset.
        /// </summary>
        /// <value>The asset.</value>
        public override Asset Asset
        {
            get { return GetAsset(this); }
            set { SetAsset(this, value); }
        }

        /// <summary>
        /// Gets the asset.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/>.</param>
        /// <returns>Value of the AssetProperty.</returns>
        public static Asset GetAsset(DependencyObject obj)
        {
            return obj.GetValue(AssetProperty) as Asset;
        }

        /// <summary>
        /// Sets the asset.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/>.</param>
        /// <param name="value">The <see cref="FolderAsset"/> value.</param>
        public static void SetAsset(DependencyObject obj, Asset value)
        {
            obj.SetValue(AssetProperty, value);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
        }

        /// <summary>
        /// Scales the current preview control to the specified size.
        /// </summary>
        /// <param name="size">The size to which the preview control is to be scaled.</param>
        public override void Scale(Size size)
        {
            if (size.Width > MarginX && size.Width > MarginY)
            {
                FolderGrid.Width = size.Width;
                FolderGrid.Height = size.Height;
            }
        }
    }
}
