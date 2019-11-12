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

namespace RCE.Modules.Timeline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.Practices.ServiceLocation;
    using RCE.Infrastructure.Services;
    using SMPTETimecode;

    /// <summary>
    /// Preview control for the <see cref="VideoAsset"/> in the timeline.
    /// </summary>
    public partial class VideoPreview : IPreview
    {
        private readonly TimelineElement element;

        /// <summary>
        /// Width of the frame.
        /// </summary>
        private const double FrameWidth = 70;

        /// <summary>
        /// Height of the frame.
        /// </summary>
        private const double FrameHeight = 35;

        /// <summary>
        /// The <see cref="IThumbnailService"/> service used to get the asset thumbnail frame.
        /// </summary>
        private readonly IThumbnailService thumbnailService;

        /// <summary>
        /// List maintains the frame images of the asets.
        /// </summary>
        private IList<Image> frameImages;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoPreview"/> class.
        /// </summary>
        /// <param name="element">The <see cref="TimelineElement"/> element.</param>
        public VideoPreview(TimelineElement element)
        {
            this.element = element;
            this.InitializeComponent();

            if (this.element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element.Asset.PropertyChanged += this.OnPropertyChanged;

            this.DataContext = element;
            this.VolumeLevels.CurrentTimelineElement = element;
            this.VolumeLevels.ItemLocked += this.OnItemLocked;

            this.thumbnailService = ServiceLocator.Current.GetInstance(typeof(IThumbnailService)) as IThumbnailService;
        }

        public event EventHandler<DataEventArgs<bool>> ItemLocked;

        /// <summary>
        /// Sets the current video asset as selected asset.
        /// </summary>
        /// <param name="selected">If set to <c>true</c> [selected].</param>
        public void SetSelected(bool selected)
        {
            this.SelectionBox.Visibility = selected ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Refreshes the preview. This includes the progress bar and the filmstrip.
        /// </summary>
        /// <param name="currentWidth">Width of the current element.</param>
        public void Refresh(double currentWidth)
        {
            this.UpdateFilmstrip(currentWidth);
        }

        /// <summary>
        /// Updates the filmstrip.
        /// </summary>
        /// <param name="currentWidth">Width of the current element.</param>
        public void UpdateFilmstrip(double currentWidth)
        {
            if (this.frameImages == null)
            {
                this.frameImages = new List<Image>();
            }

            int i;

            double numberOfFrames = Math.Ceiling(currentWidth / FrameWidth);

            for (i = this.frameImages.Count; i < numberOfFrames; i++)
            {
                Image image = new Image
                                  {
                                      Visibility = Visibility.Collapsed,
                                      Width = FrameWidth,
                                      Height = FrameHeight
                                  };

                this.frameImages.Add(image);
            }

            this.UpdateFrames(numberOfFrames, currentWidth);

            i = 0;

            while (i < numberOfFrames)
            {
                Image image = this.frameImages[i];
                image.Visibility = Visibility.Visible;
                i++;
            }

            while (i < this.frameImages.Count)
            {
                this.frameImages[i].Visibility = Visibility.Collapsed;
                i++;
            }
        }

        /// <summary>
        /// Converts pixel to seconds.
        /// </summary>
        /// <param name="px">The pixel value.</param>
        /// <param name="element">The element.</param>
        /// <param name="width">The element width.</param>
        /// <returns>Conversion value fromPixel to seconds as <see cref="double"/>.</returns>
        private static double PixelToSeconds(double px, TimelineElement element, double width)
        {
            width = (width == 0 || double.IsNaN(width)) ? 1 : width;
            
            double absouluteTime = element.Duration.TotalSeconds * px / width;

            if (double.IsNaN(absouluteTime) || double.IsInfinity(absouluteTime))
            {
                absouluteTime = 0;
            }

            return Math.Floor(TimeCode.FromAbsoluteTime(absouluteTime, element.Duration.FrameRate).TotalSeconds);
        }

        /// <summary>
        /// Updates the frames.
        /// </summary>
        /// <param name="numberOfFrames">The number of frames.</param>
        /// <param name="currentWidth">Width of the current.</param>
        private void UpdateFrames(double numberOfFrames, double currentWidth)
        {
            this.FramesStackPanel.Children.Clear();

            TimelineElement element = DataContext as TimelineElement;

            if (element != null)
            {
                int totalSecondsPerFrame = (int)PixelToSeconds(FrameWidth, element, currentWidth);
                int startSeconds = (int)element.InPosition.TotalSeconds;

                for (int i = 0; i < numberOfFrames; i++)
                {
                    Image image = this.frameImages[i];

                    string uriString = this.thumbnailService.GetThumbnailSource(element.Asset, startSeconds, image.Width, image.Height);

                    image.Source = new BitmapImage(new Uri(Uri.EscapeUriString(uriString), UriKind.RelativeOrAbsolute));

                    this.FramesStackPanel.Children.Add(image);
                    startSeconds += totalSecondsPerFrame;
                }
            }
        }

        private void OnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsStereo")
            {
                this.SetIsStereo(((VideoAsset)this.element.Asset).IsStereo);
            }
        }

        private void InvokeItemLocked(bool itemLocked)
        {
            EventHandler<DataEventArgs<bool>> handler = this.ItemLocked;
            if (handler != null)
            {
                handler(this, new DataEventArgs<bool>(itemLocked));
            }
        }

        private void OnItemLocked(object sender, DataEventArgs<bool> e)
        {
            this.InvokeItemLocked(e.Data);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetIsStereo(((VideoAsset)this.element.Asset).IsStereo);
        }

        private void SetIsStereo(bool isStereo)
        {
            VisualStateManager.GoToState(this, isStereo ? "StereoVisible" : "MonoVisible", true);
        }
    }
}