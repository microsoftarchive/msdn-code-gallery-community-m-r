// <copyright file="MarkersListViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MarkersListViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers.Views
{
    using System.Collections.ObjectModel;

    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Composite.Regions;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Services.Contracts;

    using Sequence = RCE.Infrastructure.Models.Sequence;

    public class MarkersListViewModel : BaseModel, IMarkersListViewModel
    {
        private readonly ISequenceRegistry sequenceRegistry;
        
        private readonly IMarkersListView view;

        private readonly IRegionManager regionManager;

        private ObservableCollection<Marker> currentMarkers;
        
        public MarkersListViewModel(
            IMarkersListView view,
            ISequenceRegistry sequenceRegistry,
            IEventAggregator eventAggregator,
            IRegionManager regionManager)
        {
            this.sequenceRegistry = sequenceRegistry;
            this.sequenceRegistry.CurrentSequenceChanged += this.CurrentSequenceChanged;
            this.view = view;
            this.LoadMarkers();
            this.view.SetViewModel(this);
            this.regionManager = regionManager;

            eventAggregator.GetEvent<DisplayMarkerBrowserWindowEvent>().Subscribe(this.DisplayView, ThreadOption.PublisherThread, true, this.FilterDisplayMarkerBrowserWindowEvent);
        }

        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>The header info.</value>
        public string HeaderInfo
        {
            get { return "Markers"; }
        }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public ObservableCollection<Marker> Markers
        {
            get
            {
                return this.currentMarkers;
            }

            set
            {
                this.currentMarkers = value;
                this.OnPropertyChanged("Markers");
            }
        }

        public object View 
        { 
            get
            {
                return this.view;
            }
        }

        private void LoadMarkers()
        {
            this.LoadMarkers(this.sequenceRegistry.CurrentSequence);
        }

        private void LoadMarkers(Sequence sequenceModel)
        {
            this.Markers = sequenceModel == null ? new ObservableCollection<Marker>() : sequenceModel.Markers;
        }

        private void CurrentSequenceChanged(object sender, Infrastructure.DataEventArgs<ISequenceModel> dataEventArgs)
        {
            this.LoadMarkers(this.sequenceRegistry.CurrentSequence);
        }

        private bool FilterDisplayMarkerBrowserWindowEvent(SelectedMarkersBrowserTab selectedTab)
        {
            return selectedTab == SelectedMarkersBrowserTab.Markers;
        }

        private void DisplayView(SelectedMarkersBrowserTab selectedTab)
        {
            this.regionManager.Regions[RegionNames.MarkerBrowserRegion].Activate(this.View);
        }
    }
}