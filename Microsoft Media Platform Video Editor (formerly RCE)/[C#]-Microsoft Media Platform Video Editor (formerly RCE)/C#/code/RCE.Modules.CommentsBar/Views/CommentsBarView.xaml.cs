// <copyright file="CommentsBarView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentsBarView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Infrastructure;
    using SMPTETimecode;

    /// <summary>
    /// Provides the implementation for <see cref="ICommentsBarView"/>.
    /// </summary>
    public partial class CommentsBarView : UserControl, ICommentsBarView
    {
        /// <summary>
        /// Default position of the comment from the top of the comment bar.
        /// </summary>
        private const int DefaultHeight = 20;

        /// <summary>
        /// Duration of the timeline.
        /// </summary>
        private TimeCode duration;

        /// <summary>
        /// Width of the timelime.
        /// </summary>
        private double actualWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsBarView"/> class.
        /// </summary>
        public CommentsBarView()
        {
            InitializeComponent();
        }

        public ICommentsBarPresenter Model
        {
            get { return this.DataContext as ICommentsBarPresenter; }
            set { this.DataContext = value; }
        }

        public void AddPreview(object preview, double position, object editBox, object displayBox)
        {
            UIElement previewElement = preview as UIElement;
            UIElement editBoxElement = editBox as UIElement;
            
            if (previewElement != null && editBoxElement != null)
            {
                this.CommentsLayerCanvas.Children.Add(previewElement);
                this.EditBoxCanvas.Children.Add(editBoxElement);
                
                UIElement displayBoxElement = displayBox as UIElement;
                if (displayBoxElement != null)
                {
                    this.EditBoxCanvas.Children.Add(displayBoxElement);
                }

                double x = this.TimeCodeToPixel(TimeCode.FromSeconds(position, this.duration.FrameRate));
                double y = DefaultHeight - 2;
                Canvas.SetLeft(previewElement, x);
                Canvas.SetTop(previewElement, y);
                Canvas.SetLeft(editBoxElement, x);
                
                if (displayBoxElement != null)
                {
                    Canvas.SetLeft(displayBoxElement, x);
                }
            }
        }

        public void SetEditBoxMargins(int left, int up, int right, int down)
        {
            this.EditBoxCanvas.Margin = new Thickness(left, up, right, down);
        }
        
        public void UpdatePreview(object preview, double position, object editBox, object displayBox)
        {
            UIElement previewElement = preview as UIElement;
            UIElement editBoxElement = editBox as UIElement;

            if (previewElement != null && editBoxElement != null 
                && this.CommentsLayerCanvas.Children.Contains(previewElement) && this.EditBoxCanvas.Children.Contains(editBoxElement))
            {
                double x = this.TimeCodeToPixel(TimeCode.FromSeconds(position, this.duration.FrameRate));
                double y = DefaultHeight - 2;
                Canvas.SetLeft(previewElement, x);
                Canvas.SetTop(previewElement, y);
                UIElement displayBoxElement = displayBox as UIElement;
                if (displayBoxElement != null)
                {
                    Canvas.SetLeft(displayBoxElement, x);
                }

                Canvas.SetLeft(editBoxElement, x);
            }
        }

        public void RemovePreview(object preview, object editBox)
        {
            UIElement previewElement = preview as UIElement;
            UIElement editBoxElement = editBox as UIElement;

            if (previewElement != null && editBoxElement != null)
            {
                this.CommentsLayerCanvas.Children.Remove(previewElement);
                this.EditBoxCanvas.Children.Remove(editBoxElement);
            }
        }

        public void RemoveAllPreviews()
        {
            this.CommentsLayerCanvas.Children.Clear();
            this.EditBoxCanvas.Children.Clear();
        }

        public void CloseOptions()
        {
            this.OptionsPopup.IsOpen = false;
        }

        /// <summary>
        /// Set the duration for the comment.
        /// </summary>
        /// <param name="currentDuration">The TimeCode.</param>
        public void SetDuration(TimeCode currentDuration)
        {
            this.duration = currentDuration;
        }

        /// <summary>
        /// Refreshes all the comments layout in the UI.
        /// </summary>
        /// <param name="width">The width.</param>
        public void RefreshPreviews(double width)
        {
            this.actualWidth = width;
        }

        public void ShowOptions(double seconds)
        {
            double x = this.TimeCodeToPixel(TimeCode.FromSeconds(seconds, this.duration.FrameRate)); 
            
            Canvas.SetLeft(this.OptionsBox, x);

            this.OptionsPopup.IsOpen = true;
        }

        public void SetEditBoxZeeIndex(int zeeIndex)
        {
            this.EditBoxCanvas.SetValue(Canvas.ZIndexProperty, zeeIndex);
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Binding optionSelectedCommand = new Binding("OptionSelectedCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["OptionSelectedCommand"]).SetBinding(BindingHelper.ValueProperty, optionSelectedCommand);
        }
    }
}
