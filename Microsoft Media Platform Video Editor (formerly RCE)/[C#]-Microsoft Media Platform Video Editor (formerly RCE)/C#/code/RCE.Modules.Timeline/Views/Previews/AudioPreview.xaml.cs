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

namespace RCE.Modules.Timeline
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using Infrastructure.Models;

    using RCE.Infrastructure;

    /// <summary>
    /// Preview control for the <see cref="AudioAsset"/>.
    /// </summary>
    public partial class AudioPreview : IPreview
    {
        private readonly TimelineElement element;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPreview"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        public AudioPreview(TimelineElement element)
        {
            this.element = element;
            this.InitializeComponent();

            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element.Asset.PropertyChanged += this.OnPropertyChanged;

            this.DataContext = this.element;
            this.VolumeLevels.CurrentTimelineElement = this.element;

            this.VolumeLevels.ItemLocked += this.OnItemLocked;
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

        private void SetIsStereo(bool isStereo)
        {
            VisualStateManager.GoToState(this, isStereo ? "StereoVisible" : "MonoVisible", true);
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

        private void OnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsStereo")
            {
                this.SetIsStereo(((AudioAsset)this.element.Asset).IsStereo);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetIsStereo(((AudioAsset)this.element.Asset).IsStereo);
        }
    }
}
