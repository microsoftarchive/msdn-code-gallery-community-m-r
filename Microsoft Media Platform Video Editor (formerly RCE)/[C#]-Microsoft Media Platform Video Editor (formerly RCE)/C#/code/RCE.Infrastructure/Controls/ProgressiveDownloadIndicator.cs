// <copyright file="ProgressiveDownloadIndicator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProgressiveDownloadIndicator.cs                     
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
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    [TemplatePart(Name = "IndicatorContainer", Type = typeof(Canvas))]
    public class ProgressiveDownloadIndicator : Control
    {
        public static readonly DependencyProperty DownloadingIndicatorBackgroundProperty = DependencyProperty.Register("DownloadingIndicatorBackground", typeof(Brush), typeof(ProgressiveDownloadIndicator), new PropertyMetadata(new SolidColorBrush(Colors.Green)));

        public static readonly DependencyProperty DownloadedPortionBackgroundProperty = DependencyProperty.Register("DownloadedPortionBackground", typeof(Brush), typeof(ProgressiveDownloadIndicator), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

        private Canvas indicatorContainer;

        private Rectangle currentIndicator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressiveDownloadIndicator"/> class.
        /// </summary>
        public ProgressiveDownloadIndicator()
        {
            this.DefaultStyleKey = typeof(ProgressiveDownloadIndicator);
        }
        
        public Brush DownloadedPortionBackground
        {
            get { return (Brush)GetValue(DownloadedPortionBackgroundProperty); }
            set { SetValue(DownloadedPortionBackgroundProperty, value); }
        }

        public Brush DownloadingIndicatorBackground
        {
            get { return (Brush)GetValue(DownloadingIndicatorBackgroundProperty); }
            set { SetValue(DownloadingIndicatorBackgroundProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.indicatorContainer = GetTemplateChild("IndicatorContainer") as Canvas;
        }

        public void Refresh()
        {
           this.Refresh(this.ActualWidth);
        }

        public void Refresh(double width)
        {
            if (this.indicatorContainer != null && this.indicatorContainer.Children != null)
            {
                foreach (UIElement uie in this.indicatorContainer.Children)
                {
                    Rectangle rectangle = uie as Rectangle;

                    if (rectangle != null)
                    {
                        ProgresIndicator progressIndicator = rectangle.Tag as ProgresIndicator;

                        if (progressIndicator != null)
                        {
                            var rectangleWidth = width * (progressIndicator.DownloadProgress - progressIndicator.DownloadProgressOffset);
                            rectangle.Width = width <= 0 ? 0 : width;

                            rectangle.SetValue(Canvas.LeftProperty, width * progressIndicator.DownloadProgressOffset);
                        }
                    }
                }
            }
        }

        public void ClearProgress()
        {
            this.currentIndicator = null;
            this.indicatorContainer.Children.Clear();
        }

        public void ReportProgress(double downloadProgress, double downloadProgressOffset)
        {
            this.ReportProgress(downloadProgress, downloadProgressOffset, this.ActualWidth);
        }
        
        public void ReportProgress(double downloadProgress, double downloadProgressOffset, double actualWidth)
        {
            if (downloadProgress == -1 && downloadProgressOffset == -1)
            {
                this.ClearProgress();
                return;
            }

            this.AddIndicator(downloadProgressOffset, actualWidth);

            if (this.currentIndicator != null)
            {
                var indicatorWidth = actualWidth * (downloadProgress - downloadProgressOffset);
                this.currentIndicator.Width = indicatorWidth <= 0 ? 0 : indicatorWidth;
                this.currentIndicator.Tag = new ProgresIndicator(downloadProgress, downloadProgressOffset);

                if (downloadProgress == 1)
                {
                    this.currentIndicator.Fill = this.DownloadedPortionBackground;
                }
            } 
        }

        private void AddIndicator(double downloadProgressOffset, double actualWidth)
        {
            if (this.currentIndicator != null && (double)this.currentIndicator.GetValue(Canvas.LeftProperty) == actualWidth * downloadProgressOffset)
            {
                return;
            }

            if (this.currentIndicator != null)
            {
                this.currentIndicator.Fill = this.DownloadedPortionBackground;
            }

            // check to see if there is an indicator already added for this byte range
            this.currentIndicator = this.indicatorContainer != null ? this.indicatorContainer.Children.Where((uie) =>
                                                                                               uie is Rectangle && (double)(uie as Rectangle).GetValue(Canvas.LeftProperty) ==
                                                                                               actualWidth * downloadProgressOffset)
                                                                                               .FirstOrDefault() as Rectangle : null;

            // no indicators have been added so far
            if (this.currentIndicator == null && this.indicatorContainer != null)
            {
                this.currentIndicator = new Rectangle { Width = 0, Height = this.indicatorContainer.ActualHeight, Fill = this.DownloadedPortionBackground };
                this.indicatorContainer.Children.Add(this.currentIndicator);
                this.currentIndicator.SetValue(Canvas.LeftProperty, actualWidth * downloadProgressOffset);
                this.currentIndicator.SetValue(Canvas.TopProperty, 0.0);
            }
        }

        private class ProgresIndicator
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ProgressiveDownloadIndicator"/> class.
            /// </summary>
            public ProgresIndicator(double downloadProgress, double downloadProgressOffset)
            {
                this.DownloadProgress = downloadProgress;
                this.DownloadProgressOffset = downloadProgressOffset;
            }

            public double DownloadProgress { get; private set; }

            public double DownloadProgressOffset { get; private set; }
        }
    }
}