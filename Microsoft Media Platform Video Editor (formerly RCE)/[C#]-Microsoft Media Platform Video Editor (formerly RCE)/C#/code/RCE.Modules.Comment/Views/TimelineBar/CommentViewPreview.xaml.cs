// <copyright file="CommentViewPreview.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Infrastructure;
    using SMPTETimecode;

    /// <summary>
    /// Provides the implementation for CommentView view.
    /// </summary>
    public partial class CommentViewPreview : ICommentViewPreview
    {
        /// <summary>
        /// Duration of the timeline.
        /// </summary>
        private TimeCode duration;

        /// <summary>
        /// Width of the timelime.
        /// </summary>
        private double actualWidth;

        /// <summary>
        /// Value indicating if the comment is moving.
        /// </summary>
        private bool movingComment;

        /// <summary>
        /// Last mouse click time to identify the double click event.
        /// </summary>
        private long lastClickTicks;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentViewPreview"/> class.
        /// </summary>
        public CommentViewPreview()
        {
            InitializeComponent();
        }

        public ICommentEditBoxPresentationModel Model
        {
            get { return this.DataContext as CommentEditBoxPresentationModel; }
            set { this.DataContext = value; }
        }

        /// <summary>
        /// Updates the width according to the duration of the comment.
        /// </summary>
        /// <param name="commentDuration">The comment duration.</param>
        public void UpdateCommentDuration(TimeCode commentDuration)
        {
            double width = this.TimeCodeToPixel(commentDuration);

            this.DurationLine.Visibility = Visibility.Visible;

            if (width < 0)
            {
                width = 0;
            }

            this.DurationLine.Width = width;
        }

        /// <summary>
        /// Set the timeline duration.
        /// </summary>
        /// <param name="currentDuration">The timeline duration.</param>
        public void SetTimelineDuration(TimeCode currentDuration)
        {
            this.duration = currentDuration;
        }

        public void RefreshPreview(double width)
        {
            this.actualWidth = width;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((DateTime.Now.Ticks - this.lastClickTicks) < UtilityHelper.MouseDoubleClickDurationValue)
            {
                this.Model.ShowEditBox();
            }
            else
            {
                this.movingComment = true;
            }

            this.lastClickTicks = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Handles the MouseMove event of the CommentView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.movingComment)
            {
                double position = e.GetPosition(this.Parent as UIElement).X;
                double currentPosition = this.PixelToTimeCode(position).TotalSeconds;

                this.Model.SetPosition(TimeSpan.FromSeconds(currentPosition));
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.movingComment = false;
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the CommentView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.movingComment = false;
        }

        /// <summary>
        /// Converts the pixel to the timecode.
        /// </summary>
        /// <param name="px">The value in pixel.</param>
        /// <returns>The <see cref="TimeCode"/>.</returns>
        private TimeCode PixelToTimeCode(double px)
        {
            return TimeCode.FromAbsoluteTime((this.duration.TotalSeconds * px) / this.actualWidth, this.duration.FrameRate);
        }

        /// <summary>
        /// Converts the time to pixel.
        /// </summary>
        /// <param name="timecode">The timecode value to be converted.</param>
        /// <returns>The value in pixel unit.</returns>
        private double TimeCodeToPixel(TimeCode timecode)
        {
            return (this.actualWidth * timecode.TotalSeconds) / this.duration.TotalSeconds;
        }
    }
}