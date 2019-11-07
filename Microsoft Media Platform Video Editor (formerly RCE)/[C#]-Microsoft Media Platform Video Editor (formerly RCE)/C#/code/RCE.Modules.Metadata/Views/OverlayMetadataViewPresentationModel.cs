// <copyright file="OverlayMetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlayMetadataViewPresentationModel.cs                     
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
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Composite.Regions;

    public class OverlayMetadataViewPresentationModel : BaseModel, IOverlayMetadataViewPresentationModel
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IRegionManager regionManager;

        private OverlayAsset overlay;

        private bool showMetadataInformation;

        private bool previewChecked;

        /// <summary>
        /// Initializes a new instance of the ClipMetadataViewPresentationModel class.
        /// </summary>
        /// <param name="view">The instance of IClipMetadataView interface.</param>
        /// <param name="regionManager">The instance of IRegionManager interface.</param>
        /// <param name="eventAggregator">The instance of IEventAggregator interface.</param>
        public OverlayMetadataViewPresentationModel(IOverlayMetadataView view, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<ShowMetadataEvent>().Subscribe(this.ShowMetadata, ThreadOption.PublisherThread, true, this.FilterViewMetadata);
            this.eventAggregator.GetEvent<HideMetadataEvent>().Subscribe(this.HideMetadata, true);
            this.eventAggregator.GetEvent<PlayClickedEvent>().Subscribe(this.HandlePlayClicked, true);

            this.PreviewOverlayCommand = new DelegateCommand<object>(this.RaiseShowPreviewEvent);

            this.View = view;
            this.View.Model = this;
            this.ShowMetadataInformation = false;
            this.regionManager = regionManager;
        }

        public IOverlayMetadataView View { get; set; }

        public ICommand PreviewOverlayCommand { get; private set; }

        public bool ShowMetadataInformation
        {
            get
            {
                return this.showMetadataInformation;
            }

            set
            {
                this.showMetadataInformation = value;

                this.OnPropertyChanged("ShowMetadataInformation");
            }
        }

        public OverlayAsset Overlay
        {
            get
            {
                return this.overlay;
            }

            set 
            { 
                this.overlay = value;
                this.OnPropertyChanged("Overlay");
                this.SetOnPropertyChangedHandler(this.overlay);
            }
        }

        public bool PreviewChecked
        {
            get
            {
                return this.previewChecked;
            }

            set
            {
                this.previewChecked = value;
                this.OnPropertyChanged("PreviewChecked");
            }
        }

        private void RaiseShowPreviewEvent(object parameter)
        {
            if (this.PreviewChecked)
            {
                this.eventAggregator.GetEvent<ShowPreviewOverlayEvent>().Publish(new PreviewOverlayPayload(this.Overlay));
            }
            else
            {
                this.eventAggregator.GetEvent<HidePreviewOverlayEvent>().Publish(null);
            }
        }

        private void SetOnPropertyChangedHandler(OverlayAsset overlayAsset)
        {
            if (overlayAsset != null)
            {
                overlayAsset.PropertyChanged += this.OnOverlayAssetPropertyChanged;
                overlayAsset.Metadata.ForEach(m => m.PropertyChanged -= this.OnOverlayAssetPropertyChanged);
                overlayAsset.Metadata.ForEach(m => m.PropertyChanged += this.OnOverlayAssetPropertyChanged);
            }
        }

        private void OnOverlayAssetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.eventAggregator.GetEvent<OperationExecutedInTimelineEvent>().Publish(null);
        }

        private bool FilterViewMetadata(TimelineElement payload)
        {
            return (payload != null) && (payload.Asset is OverlayAsset);
        }

        /// <summary>
        /// This method is called to show the metadata for an 
        /// asset in the metadata region.
        /// </summary>
        /// <param name="asset">Asset for the metadata.</param>
        private void ShowMetadata(TimelineElement timelineElement)
        {
            OverlayAsset overlayAsset = timelineElement != null ? timelineElement.Asset as OverlayAsset : null;

            if (overlayAsset != null)
            {
                this.Overlay = overlayAsset;

                this.ShowMetadataInformation = true;
                this.regionManager.Regions["ClipMetadataRegion"].Activate(this.View);
            }
        }

        /// <summary>
        /// This method is called to hide the metadata for an 
        /// asset in the metadata region.
        /// </summary>
        /// <param name="asset">Asset for the metadata.</param>
        private void HideMetadata(object asset)
        {
            this.ShowMetadataInformation = false;
        }

        private void HandlePlayClicked(object notUsed)
        {
            this.PreviewChecked = false;
        }
    }
}