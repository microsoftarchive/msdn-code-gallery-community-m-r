// <copyright file="VideoPreview.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: VideoPreview.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Infrastructure.Models;
    using Microsoft.Practices.ServiceLocation;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Services;
    using RCE.Modules.Library;

    /// <summary>
    /// Preview control for <see cref="VideoAsset"/>.
    /// </summary>
    public partial class VideoPreview : AssetPreview
    {
        /// <summary>
        /// Margin in the X axis.
        /// </summary>
        private const double MarginX = 24;

        /// <summary>
        /// Margin in the Y axis. It is used in resizing the preview.
        /// </summary>
        private const double MarginY = 35;

        /// <summary>
        /// The <see cref="IThumbnailService"/> service used to get the asset thumbnail frame.
        /// </summary>
        private readonly IThumbnailService thumbnailService;

        /// <summary>
        /// The <see cref="DependencyProperty"/> to have the Asset of the preview.
        /// </summary>
        private static readonly DependencyProperty AssetProperty =
            DependencyProperty.RegisterAttached("Asset", typeof(Asset), typeof(VideoPreview), new PropertyMetadata(AssetChanged));

        /// <summary>
        /// Flag indiating if the control is loading for the first time.
        /// true if has been loaded.
        /// </summary>
        private bool isLoaded;

        private long lastClickTicks;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoPreview"/> class.
        /// </summary>
        public VideoPreview()
        {
            InitializeComponent();

            this.Loaded += this.VideoPreview_Loaded;
            this.thumbnailService = ServiceLocator.Current.GetInstance(typeof(IThumbnailService)) as IThumbnailService;
        }

        /// <summary>
        /// Gets or sets the asset.
        /// </summary>
        /// <value>The <see cref="Asset"/>.</value>
        public override Asset Asset
        {
            get 
            { 
                return GetAsset(this); 
            }

            set 
            { 
                SetAsset(this, value);
            }
        }

        /// <summary>
        /// Gets the video asset associated with the preview.
        /// </summary>
        /// <value>The video asset.</value>
        private VideoAsset VideoAsset
        {
            get { return this.Asset as VideoAsset; }
        }

        /// <summary>
        /// Gets the asset.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="VideoAsset"/>.</returns>
        public static Asset GetAsset(DependencyObject obj)
        {
            object value = obj.GetValue(AssetProperty);

            return value as Asset;
        }

        /// <summary>
        /// Sets the asset.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/>.</param>
        /// <param name="value">The <see cref="VideoAsset"/>.</param>
        public static void SetAsset(DependencyObject obj, Asset value)
        {
            obj.SetValue(AssetProperty, value);
        }

        /// <summary>
        /// Scales the current preview control to the specified size.
        /// </summary>
        /// <param name="size">The size to which the preview control is to be scaled.</param>
        public override void Scale(Size size)
        {
            if (size.Width > MarginX && size.Width > MarginY)
            {
                VideoGrid.Width = size.Width;
                VideoGrid.Height = size.Height;
                
                Size previewSize = this.GetSizeMaintainingAspectRatio(size.Width, size.Height);
                FramePreviewImage.Width = previewSize.Width;
                FramePreviewImage.Height = previewSize.Height;
            }
        }

        /// <summary>
        /// Change the preview image of the asset as asset is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/>.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void AssetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VideoPreview preview = d as VideoPreview;

            if (preview != null && preview.thumbnailService != null)
            {
                Uri frameUri = new Uri(preview.thumbnailService.GetThumbnailSource(preview.Asset), UriKind.RelativeOrAbsolute);
                preview.FramePreviewImage.Source = new BitmapImage(frameUri);
            }
        }

        /// <summary>
        /// Handles the Loaded event of the VideoPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void VideoPreview_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.isLoaded)
            {
                Size aspectSize = this.GetSizeMaintainingAspectRatio(this.ActualWidth, this.ActualHeight);
                this.FramePreviewImage.Width = aspectSize.Width;
                this.FramePreviewImage.Height = aspectSize.Height;
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
            width -= MarginX;

            if (width <= 0)
            {
                width = 1;
            }

            height -= MarginY;

            if (height <= 0)
            {
                height = 1;
            }
           
            VideoAsset videoAsset = this.Asset as VideoAsset;

            if (videoAsset != null)
            {
                double aspectRatioWidth = Convert.ToDouble(videoAsset.Width.GetValueOrDefault());
                double aspectRatioHeight = Convert.ToDouble(videoAsset.Height.GetValueOrDefault());

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

        private void HandleMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((DateTime.Now.Ticks - this.lastClickTicks) < UtilityHelper.MouseDoubleClickDurationValue)
            {
                this.lastClickTicks = 0;

                this.NameTextBox.IsReadOnly = false;
                this.NameTextBox.Text = string.Empty;
                this.NameTextBox.Background = new SolidColorBrush(Colors.White);
                this.NameTextBox.UpdateLayout();
                this.NameTextBox.LostFocus += this.HandleNameTextBoxLostFocus;
                Dispatcher.BeginInvoke(() => this.NameTextBox.Focus());
            }

            this.lastClickTicks = DateTime.Now.Ticks;
        }

        private void HandleNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.NameTextBox.IsReadOnly = true;
            this.NameTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 176, 176, 176));
        }
    }
}
