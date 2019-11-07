// <copyright file="AdsListViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AdsListViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Ads.Views
{
    using System.Collections.ObjectModel;
    using Infrastructure.Events;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Composite.Regions;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Services.Contracts;
    using Sequence = RCE.Infrastructure.Models.Sequence;

    public class AdsListViewPresentationModel : BaseModel, IAdsListViewPresentationModel
    {
        /// <summary>
        /// The <seealso cref="ISequenceRegistry"/> instance used to keep track of the current sequence
        /// </summary>
        private readonly ISequenceRegistry sequenceRegistry;

        private readonly IRegionManager regionManager;

        private ObservableCollection<AdOpportunity> currentAds;

        public AdsListViewPresentationModel(
            IAdsListView view,
            ISequenceRegistry sequenceRegistry,
            IRegionManager regionManager,
            IEventAggregator eventAggregator)
        {
            this.sequenceRegistry = sequenceRegistry;
            this.sequenceRegistry.CurrentSequenceChanged += this.CurrentSequenceChanged;

            eventAggregator.GetEvent<DisplayMarkerBrowserWindowEvent>().Subscribe(this.DisplayView, ThreadOption.PublisherThread, true, this.FilterDisplayMarkerBrowserWindowEvent);

            this.regionManager = regionManager;
            this.View = view;
            this.LoadAds();

            this.View.Model = this;
        }

        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>The header info.</value>
        public string HeaderInfo
        {
            get { return "Ads"; }
        }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public ObservableCollection<AdOpportunity> Ads
        {
            get
            {
                return this.currentAds;
            }

            set
            {
                this.currentAds = value;
                this.OnPropertyChanged("Ads");
            }
        }

        public IAdsListView View { get; private set; }

        private void LoadAds()
        {
            this.LoadAds(this.sequenceRegistry.CurrentSequence);
        }

        private void LoadAds(Sequence sequence)
        {
            this.Ads = sequence == null ? new ObservableCollection<AdOpportunity>() : sequence.AdOpportunities;
        }

        private void CurrentSequenceChanged(object sender, Infrastructure.DataEventArgs<ISequenceModel> dataEventArgs)
        {
            this.LoadAds(this.sequenceRegistry.CurrentSequence);
        }

        private bool FilterDisplayMarkerBrowserWindowEvent(SelectedMarkersBrowserTab selectedTab)
        {
            return selectedTab == SelectedMarkersBrowserTab.Ads;
        }

        private void DisplayView(SelectedMarkersBrowserTab selectedTab)
        {
            this.regionManager.Regions[RegionNames.MarkerBrowserRegion].Activate(this.View);
        }
    }
}
