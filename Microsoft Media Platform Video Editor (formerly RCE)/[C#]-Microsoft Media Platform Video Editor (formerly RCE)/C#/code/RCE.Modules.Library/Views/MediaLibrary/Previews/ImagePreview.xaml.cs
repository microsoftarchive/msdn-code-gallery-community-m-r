// <copyright file="ImagePreview.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImagePreview.xaml.cs                     
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
    using System.Windows.Controls;
    using RCE.Infrastructure.Models;

    /// <summary>
    /// Preview control for <see cref="ImageAsset"/>.
    /// </summary>
    public partial class ImagePreview : AssetPreview
    {
        // 24 = 12(Canvas left) + 12 (Canvas Top)

        /// <summary>
        /// Margin in the X axis.
        /// </summary>
        private const double MARGINX = 24;

        // 35 = 15 (Height of the first row in grid) + 20 (Height of the 3rd row of the grid)

        /// <summary>
        /// Margin in the Y axis. It is used in resizing the preview.
        /// </summary>
        private const double MARGINY = 35;

        /// <summary>
        /// The <see cref="DependencyProperty"/> to have the Asset of the preview.
        /// </summary>
        private static readonly DependencyProperty AssetProperty = DependencyProperty.RegisterAttached("Asset", typeof(Asset), typeof(ImagePreview), null);

        /// <summary>
        /// Flag indiating if the control is loading for the first time.
        /// true if has been loaded.
        /// </summary>
        private bool isLoaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePreview"/> class.
        /// </summary>
        public ImagePreview()
        {
            InitializeComponent();
            this.Loaded += this.ImagePreview_Loaded;
        }

        /// <summary>
        /// Gets or sets the asset.
        /// </summary>
        /// <value>The <see cref="Asset"/>.</value>
        public override Asset Asset
        {
            get { return GetAsset(this); }
            set { SetAsset(this, value); }
        }

        /// <summary>
        /// Gets the asset.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="ImageAsset"/>.</returns>
        public static Asset GetAsset(DependencyObject obj)
        {
            object value = obj.GetValue(AssetProperty);

            return value as Asset;
        }

        /// <summary>
        /// Sets the asset.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/>.</param>
        /// <param name="value">The <see cref="ImageAsset"/>.</param>
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
            if (size.Width > MARGINX && size.Width > MARGINY)
            {
                ImageGrid.Width = size.Width;
                ImageGrid.Height = size.Height;
                Size newSize = this.GetSizeMaintainingAspectRatio(size.Width, size.Height);
                FramePreviewImageRenderTransform.ScaleX = newSize.Width / this.FramePreviewImage.ActualWidth;
                FramePreviewImageRenderTransform.ScaleY = newSize.Height / this.FramePreviewImage.ActualHeight;
                this.SetCanvasLeftTopOfPreviewImage(size.Width, size.Height);
            }
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

        /// <summary>
        /// Handles the Loaded event of the ImagePreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ImagePreview_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.isLoaded)
            {
                // Set the FramePreviewImage size, canvas left, canvas right property according to the aspect ratio.
                Size aspectSize = this.GetSizeMaintainingAspectRatio(this.ActualWidth, this.ActualHeight);
                this.FramePreviewImage.Width = aspectSize.Width;
                this.FramePreviewImage.Height = aspectSize.Height;
                this.SetCanvasLeftTopOfPreviewImage(this.ActualWidth, this.ActualHeight);
                this.FramePreviewImage.Visibility = Visibility.Visible;
                this.isLoaded = true;
            }
        }

        /// <summary>
        /// Returns the best fit size for the asset in the given size.
        /// </summary>
        /// <param name="width">Max possible width.</param>
        /// <param name="height">Max possible height.</param>
        /// <returns>Returns the best fit size for the asset.</returns>
        private Size GetSizeMaintainingAspectRatio(double width, double height)
        {
            width -= MARGINX;
            height -= MARGINY;

            ImageAsset asset = this.Asset as ImageAsset;

            if (asset != null)
            {
                double aspectRatioWidth = Convert.ToDouble(asset.Width);
                double aspectRatioHeight = Convert.ToDouble(asset.Height);

                if (aspectRatioWidth == 0 || aspectRatioHeight == 0)
                {
                    return new Size(width, height);
                }

                if (width >= height * aspectRatioWidth / aspectRatioHeight)
                {
                    return new Size(height * aspectRatioWidth / aspectRatioHeight, height);
                }
                else
                {
                    return new Size(width, width * aspectRatioHeight / aspectRatioWidth);
                }
            }

            return new Size(width, height);
        }

        /// <summary>
        /// Set the canvas left and top property of the preview element.
        /// </summary>
        /// <param name="previewWidth">Max width of the control.</param>
        /// <param name="previewHeight">Max height of the control.</param>
        private void SetCanvasLeftTopOfPreviewImage(double previewWidth, double previewHeight)
        {
            previewHeight -= MARGINY;
            this.FramePreviewImage.SetValue(Canvas.LeftProperty, (previewWidth - (this.FramePreviewImage.ActualWidth * this.FramePreviewImageRenderTransform.ScaleX)) / 2);
            this.FramePreviewImage.SetValue(Canvas.TopProperty, (previewHeight - (this.FramePreviewImage.ActualHeight * this.FramePreviewImageRenderTransform.ScaleY)) / 2);
        }
    }
}
