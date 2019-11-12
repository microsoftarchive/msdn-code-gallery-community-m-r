// <copyright file="TimelineElementView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineElementView.xaml.cs                     
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
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using Infrastructure;
    using Infrastructure.Models;

    /// <summary>
    /// Displays a timeline element based on the type of the asset associated with the timeline element.
    /// </summary>
    public partial class TimelineElementView : INotifyPropertyChanged
    {
        /// <summary>
        /// The preview control used to show the timeline element.
        /// </summary>
        private readonly UserControl previewControl;

        /// <summary>
        /// The view scale used to calculate the width and position.
        /// </summary>
        private double viewScale;

        private bool areTimelineHandlersEnabled;

        private bool isMaximized;

        private int lockGroupId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineElementView"/> class.
        /// </summary>
        /// <param name="model">The timeline element.</param>
        public TimelineElementView(TimelineElement model)
        {
            this.InitializeComponent();

            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            this.Model = model;

            this.MouseEnter += (sender, e) => this.DeleteButton.Visibility = Visibility.Visible;
            this.MouseLeave += (sender, e) => this.DeleteButton.Visibility = Visibility.Collapsed;

            // Drag
            this.PreviewArea.MouseLeftButtonDown += this.PreviewArea_MouseLeftButtonDown;

            // Drag In/Out sliders
            this.LeftSlider.MouseLeftButtonDown += this.LeftSlider_MouseLeftButtonDown;
            this.RightSlider.MouseLeftButtonDown += this.RightSlider_MouseLeftButtonDown;

            // Preview
            if (model.Asset is VideoAsset)
            {
                this.previewControl = new VideoPreview(model);
            }
            else if (model.Asset is AudioAsset)
            {
                this.previewControl = new AudioPreview(model);

                ScaleElement(this.InLinkIcon);
                ScaleElement(this.InUnlinkIcon);
                ScaleElement(this.OutLinkIcon);
                ScaleElement(this.OutUnlinkIcon);
            }
            else if (model.Asset is ImageAsset)
            {
                this.previewControl = new ImagePreview(model.Asset as ImageAsset);
            }
            else if (model.Asset is TitleAsset)
            {
                this.previewControl = new TitlePreview(model.Asset as TitleAsset);
            }
            else if (model.Asset is OverlayAsset)
            {
                this.previewControl = new OverlayPreview(model.Asset as OverlayAsset);
            }

            if (this.previewControl != null)
            {
                this.PreviewArea.Child = this.previewControl;
                this.previewControl.Width = double.NaN;

                var preview = this.previewControl as IPreview;
                if (preview != null)
                {
                    preview.ItemLocked += this.OnItemLocked;
                }
            }

            this.LockGroupId = -1;
            this.DataContext = this;
        }

        /// <summary>
        /// Ocurrs when the element start being dragged.
        /// </summary>
        public event MouseButtonEventHandler StartDrag;

        /// <summary>
        /// Occurs when the in slice of the element start being dragged.
        /// </summary>
        public event MouseButtonEventHandler InSliceStartDrag;

        /// <summary>
        /// Ocurrs when the out slice of the element start being dragged.
        /// </summary>
        public event MouseButtonEventHandler OutSliceStartDrag;

        /// <summary>
        /// Ocurrs when a link is being cliked.
        /// </summary>
        public event EventHandler<LinkElementEventArgs> LinkClicked;

        /// <summary>
        /// Ocurrs when the delete button is being clicked.
        /// </summary>
        public event EventHandler<DataEventArgs<TimelineElement>> DeleteClicked;

        public event EventHandler<DataEventArgs<TimelineElement>> MinimizeClicked;

        public event EventHandler<DataEventArgs<TimelineElement>> MaximizeClicked;

        public event EventHandler<DataEventArgs<TimelineElement>> ElementLocked;

        public event EventHandler<DataEventArgs<TimelineElement>> ElementUnlocked;

        public event EventHandler<DataEventArgs<bool>> ItemLocked;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the timeline element associated with the view.
        /// </summary>
        /// <value>A timeline element.</value>
        public TimelineElement Model { get; private set; }

        public int LockGroupId
        {
            get
            {
                return this.lockGroupId;
            }

            set
            {
                this.lockGroupId = value;
                this.InvokePropertyChanged("LockGroupId");
            }
        }

        public int TrackNumber { get; set; }

        /// <summary>
        /// Sets the view scale and refreshes the current preview.
        /// </summary>
        /// <param name="value">The new view scale.</param>
        public void SetViewScale(double value)
        {
            this.viewScale = value;
            this.Refresh();
        }

        /// <summary>
        /// Selects / Unselects the element based on the <paramref name="selected"/>.
        /// </summary>
        /// <param name="selected">A value that indicates wether the element should be selected or not.</param>
        public void SetSelected(bool selected)
        {
            this.Focus();

            var preview = this.previewControl as IPreview;
            if (preview != null)
            {
                preview.SetSelected(selected);
            }
        }

        /// <summary>
        /// Refreshes the preview in case of being a VideoPreview.
        /// </summary>
        public void RefreshPreview()
        {
            VideoPreview videoPreview = this.previewControl as VideoPreview;

            if (videoPreview != null)
            {
                double currentWidth = this.MainCanvas.ActualWidth <= 0
                                          ? this.MainCanvas.Width
                                          : this.MainCanvas.ActualWidth;

                videoPreview.Refresh(currentWidth);
            }
        }

        /// <summary>
        /// Refreshes the width and the position of the view.
        /// </summary>
        public void Refresh()
        {
            var width = this.Model.Duration.TotalSeconds * this.viewScale;
            var left = this.Model.Position.TotalSeconds * this.viewScale;

            this.MainCanvas.Width = width;
            Canvas.SetLeft(this, left);

            // handlers
            if (!(this.Model.Asset is ImageAsset) && !(this.Model.Asset is TitleAsset) && !(this.Model.Asset is OverlayAsset))
            {
                this.LeftSliderIconEnabled.Visibility = this.areTimelineHandlersEnabled && this.Model.InPosition.TotalSeconds > 0
                                                            ? Visibility.Visible
                                                            : Visibility.Collapsed;
                this.LeftSliderIconDisabled.Visibility = !this.areTimelineHandlersEnabled || this.Model.InPosition.TotalSeconds == 0
                                                             ? Visibility.Visible
                                                             : Visibility.Collapsed;
            }

            var videoAsset = this.Model.Asset as VideoAsset;
            var audioAsset = this.Model.Asset as AudioAsset;

            double totalDuration;
            if (videoAsset != null)
            {
                totalDuration = videoAsset.Duration.TotalSeconds;
            }
            else if (audioAsset != null)
            {
                totalDuration = audioAsset.DurationInSeconds;
            }
            else
            {
                totalDuration = -1;
            }

            if (!(this.Model.Asset is ImageAsset))
            {
                this.RightSliderIconEnabled.Visibility = this.areTimelineHandlersEnabled && this.Model.OutPosition.TotalSeconds != totalDuration
                                                             ? Visibility.Visible
                                                             : Visibility.Collapsed;
                this.RightSliderIconDisabled.Visibility = !this.areTimelineHandlersEnabled || this.Model.OutPosition.TotalSeconds == totalDuration
                                                              ? Visibility.Visible
                                                              : Visibility.Collapsed;
            }

            this.LeftSlider.Cursor = this.areTimelineHandlersEnabled ? Cursors.SizeWE : Cursors.Hand;
            this.RightSlider.Cursor = this.areTimelineHandlersEnabled ? Cursors.SizeWE : Cursors.Hand;
        }

        /// <summary>
        /// Shows the link of the given position.
        /// </summary>
        /// <param name="position">The position of the link to show.</param>
        /// <param name="linked">If the link is linked or not.</param>
        public void ShowLink(LinkPosition position, bool linked)
        {
            if (position == LinkPosition.In)
            {
                this.InUnlinkIcon.Visibility = linked ? Visibility.Collapsed : Visibility.Visible;
                this.InLinkIcon.Visibility = linked ? Visibility.Visible : Visibility.Collapsed;
            }

            if (position == LinkPosition.Out)
            {
                this.OutUnlinkIcon.Visibility = linked ? Visibility.Collapsed : Visibility.Visible;
                this.OutLinkIcon.Visibility = linked ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Hides the link of the given position.
        /// </summary>
        /// <param name="position">The position of the link to hide.</param>
        public void HideLink(LinkPosition position)
        {
            if (position == LinkPosition.In)
            {
                this.InUnlinkIcon.Visibility = Visibility.Collapsed;
                this.InLinkIcon.Visibility = Visibility.Collapsed;
            }

            if (position == LinkPosition.Out)
            {
                this.OutUnlinkIcon.Visibility = Visibility.Collapsed;
                this.OutLinkIcon.Visibility = Visibility.Collapsed;
            }
        }

        public void EnableTimelineHandlers(bool timelineHandlersEnabled)
        {
            this.areTimelineHandlersEnabled = timelineHandlersEnabled;
            this.Refresh();
        }

        /// <summary>
        /// Scales the element.
        /// </summary>
        /// <param name="element">The element being scaled.</param>
        private static void ScaleElement(UIElement element)
        {
            ScaleTransform scaleTransform = element.RenderTransform as ScaleTransform;

            if (scaleTransform != null)
            {
                scaleTransform.ScaleX = 0.8;
                scaleTransform.ScaleY = 0.8;
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the PreviewArea. Raises the StartDrag event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void PreviewArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.StartDrag != null)
            {
                this.StartDrag(this, e);
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown of the LeftSlider. Raises the InSliceStartDrag event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The envet args.</param>
        private void LeftSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.InSliceStartDrag != null)
            {
                this.InSliceStartDrag(this, e);
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown of the RightSlider. Raises the OutSliceStartDrag event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void RightSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.OutSliceStartDrag != null)
            {
                this.OutSliceStartDrag(this, e);
            }
        }

        /// <summary>
        /// Handles the Click event of the InLink. Raises the LinkClicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void InLink_Click(object sender, RoutedEventArgs e)
        {
            this.OnLinkClicked(LinkPosition.In);
        }

        /// <summary>
        /// Handles the Click of the OutLink. Raises the LinkClicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OutLink_Click(object sender, RoutedEventArgs e)
        {
            this.OnLinkClicked(LinkPosition.Out);
        }

        /// <summary>
        /// Raises the LinkClicked event.
        /// </summary>
        /// <param name="linkPosition">The link position used to raise the event.</param>
        private void OnLinkClicked(LinkPosition linkPosition)
        {
            EventHandler<LinkElementEventArgs> linkClickedHandler = this.LinkClicked;
            if (linkClickedHandler != null)
            {
                linkClickedHandler(this, new LinkElementEventArgs(this.Model, linkPosition));
            }
        }

        /// <summary>
        /// Raises the DeleteClicked event.
        /// </summary>
        /// <param name="timelineElement">The element being deleted.</param>
        private void OnDeleteClicked(TimelineElement timelineElement)
        {
            EventHandler<DataEventArgs<TimelineElement>> handler = this.DeleteClicked;
            if (handler != null)
            {
                handler(this, new DataEventArgs<TimelineElement>(timelineElement));
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing the event data.</param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnDeleteClicked(this.Model);
        }

        private void OnMinimizeClicked(TimelineElement timelineElement)
        {
            EventHandler<DataEventArgs<TimelineElement>> handler = this.MinimizeClicked;
            if (handler != null)
            {
                handler(this, new DataEventArgs<TimelineElement>(timelineElement));
            }
        }

        private void OnMaximizeClicked(TimelineElement timelineElement)
        {
            EventHandler<DataEventArgs<TimelineElement>> handler = this.MaximizeClicked;
            if (handler != null)
            {
                handler(this, new DataEventArgs<TimelineElement>(timelineElement));
            }
        }

        private void ExpandCollapseVolumeLevels(object sender, RoutedEventArgs e)
        {
            if (this.isMaximized)
            {
                this.DeleteButton.Visibility = Visibility.Visible;
                this.OnMinimizeClicked(this.Model);
            }

            if (!this.isMaximized)
            {
                this.DeleteButton.Visibility = Visibility.Collapsed;
                this.OnMaximizeClicked(this.Model);
            }

            this.isMaximized = !this.isMaximized;
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

        private void LockTimelineButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.LockTimelineButton.IsChecked.HasValue && this.LockTimelineButton.IsChecked.Value)
            {
                TimelineElement element = this.Model;
                this.InvokeElementLocked(element);
            }
            else if (this.LockTimelineButton.IsChecked.HasValue && !this.LockTimelineButton.IsChecked.Value)
            {
                TimelineElement element = this.Model;
                this.InvokeElementUnlocked(element);
            }
        }

        private void InvokeElementLocked(TimelineElement element)
        {
            EventHandler<DataEventArgs<TimelineElement>> handler = this.ElementLocked;
            if (handler != null)
            {
                handler(this, new DataEventArgs<TimelineElement>(element));
            }
        }

        private void InvokeElementUnlocked(TimelineElement element)
        {
            EventHandler<DataEventArgs<TimelineElement>> handler = this.ElementUnlocked;
            if (handler != null)
            {
                handler(this, new DataEventArgs<TimelineElement>(element));
            }
        }

        private void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
