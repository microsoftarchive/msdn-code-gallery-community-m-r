// <copyright file="MarkerBrowserViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MarkerBrowserViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Views
{
    using System;
    using System.Windows;
    using Infrastructure;
    using Infrastructure.Events;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Regions;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Windows;

    public class MarkerBrowserViewModel : BaseModel, IMarkerBrowserViewModel, IWindowMetadataProvider
    {
        private readonly IRegionManager regionManager;

        public MarkerBrowserViewModel(IMarkerBrowserView markerBrowserView, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            markerBrowserView.SetViewModel(this);
            eventAggregator.GetEvent<RequestMarkersBrowserRegionEvent>().Subscribe(this.AddMarkersBrowserRegion, true);
            eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);
            this.View = markerBrowserView;
            this.regionManager = regionManager;
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        public object View
        {
            get; private set;
        }

        public VerticalWindowPosition VerticalPosition
        {
            get
            {
                return VerticalWindowPosition.Center;
            }
        }

        public HorizontalWindowPosition HorizontalPosition
        {
            get
            {
                return HorizontalWindowPosition.Left;
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return ResizeDirection.Both;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(450, 355);
            }
        }

        public FrameworkElement SelectedView { get; set; }

        public object Title
        {
            get { return "Marker Browser"; }
        }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        private void AddMarkersBrowserRegion(object obj)
        {
            this.regionManager.Regions[RegionNames.MainRegion].Add(this.View);
        }
    }
}
