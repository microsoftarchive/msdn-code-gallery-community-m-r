// <copyright file="SequenceMetadataViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SequenceMetadataViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Views
{
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Media;
    using Infrastructure;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Regions;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using SMPTETimecode;

    public class SequenceMetadataViewModel : BaseModel, ISequenceMetadataViewModel
    {
        private readonly ISequenceRegistry sequenceRegistry;

        private readonly IEventAggregator eventAggregator;

        private readonly IRegionManager regionManager;

        public SequenceMetadataViewModel(ISequenceMetadataView sequenceMetadataView, ISequenceRegistry sequenceRegistry, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.sequenceRegistry = sequenceRegistry;
            this.eventAggregator = eventAggregator;

            this.DisplayCommentsCommand = new DelegateCommand<object>(this.DisplayComments);
            this.DisplayAdsCommand = new DelegateCommand<object>(this.DisplayAds);
            this.DisplayMarkersCommand = new DelegateCommand<object>(this.DisplayMarkers);

            this.RegisterSequencePropertyChangedHandlers();

            this.sequenceRegistry.CurrentSequenceChanged += this.CurrentSequenceChanged;

            this.View = sequenceMetadataView;
            this.regionManager = regionManager;
            this.View.SetViewModel(this);
        }

        public ISequenceMetadataView View { get; private set; }

        public ImageSource Thumbnail
        {
            get 
            { 
                if (this.sequenceRegistry.CurrentSequence == null)
                {
                    return null;
                }

                return this.sequenceRegistry.CurrentSequence.Thumbnail;
            }
        }

        public TimeCode Duration 
        { 
            get
            {
                if (this.sequenceRegistry.CurrentSequence == null)
                {
                    return TimeCode.FromSeconds(0d, SmpteFrameRate.Smpte2997NonDrop);
                }

                return this.sequenceRegistry.CurrentSequenceModel.Duration;
            }
        }

        public ICommand DisplayCommentsCommand { get; private set; }

        public ICommand DisplayAdsCommand { get; private set; }

        public ICommand DisplayMarkersCommand { get; private set; }

        private void CurrentSequencePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Thumbnail")
            {
                this.OnPropertyChanged("Thumbnail");
            }
        }

        private void CurrentSequenceModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Duration")
            {
                this.OnPropertyChanged("Duration");
            }
        }

        private void CurrentSequenceChanged(object sender, Infrastructure.DataEventArgs<ISequenceModel> e)
        {
            this.RegisterSequencePropertyChangedHandlers();
            this.OnPropertyChanged("Duration");
            this.OnPropertyChanged("Thumbnail");
        }

        private void DisplayComments(object obj)
        {
            if (!this.IsMarkersBrowserRegionLoaded())
            {
                this.eventAggregator.GetEvent<RequestMarkersBrowserRegionEvent>().Publish(null);
            }

            this.eventAggregator.GetEvent<DisplayMarkerBrowserWindowEvent>().Publish(SelectedMarkersBrowserTab.Comments);
        }

        private void RegisterSequencePropertyChangedHandlers()
        {
            if (this.sequenceRegistry.CurrentSequence != null)
            {
                this.sequenceRegistry.CurrentSequence.PropertyChanged += this.CurrentSequencePropertyChanged;
            }

            if (this.sequenceRegistry.CurrentSequenceModel != null)
            {
                this.sequenceRegistry.CurrentSequenceModel.PropertyChanged += this.CurrentSequenceModelPropertyChanged;
            }
        }

        private void DisplayAds(object obj)
        {
            if (!this.IsMarkersBrowserRegionLoaded())
            {
                this.eventAggregator.GetEvent<RequestMarkersBrowserRegionEvent>().Publish(null);
            }

            this.eventAggregator.GetEvent<DisplayMarkerBrowserWindowEvent>().Publish(SelectedMarkersBrowserTab.Ads);
        }

        private void DisplayMarkers(object obj)
        {
            if (!this.IsMarkersBrowserRegionLoaded())
            {
                this.eventAggregator.GetEvent<RequestMarkersBrowserRegionEvent>().Publish(null);
            }

            this.eventAggregator.GetEvent<DisplayMarkerBrowserWindowEvent>().Publish(SelectedMarkersBrowserTab.Markers);
        }

        private bool IsMarkersBrowserRegionLoaded()
        {
            return this.regionManager.Regions.ContainsRegionWithName(RegionNames.MarkerBrowserRegion);
        }
    }
}
