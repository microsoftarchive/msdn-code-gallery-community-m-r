// <copyright file="MetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata
{
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.Practices.Composite.Events;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    using Services.Contracts;

    /// <summary>
    /// Implements IMetadataViewPresentationModel interface.
    /// </summary>
    public class MetadataViewPresentationModel : BaseModel, IMetadataViewPresentationModel
    {
        /// <summary>
        /// The <seealso cref="IList"/> instance used to store 
        /// metadata fields for the asset.
        /// </summary>
        private readonly IList<string> metadataFields;

        /// <summary>
        /// The <seealso cref="IEventAggregator"/> instance used to 
        /// publish and subscribe to events.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// The list of <seealso cref="AssetMetadata"/> instance used 
        /// to store name/value pair for the asset metadata.
        /// </summary>
        private IList<AssetMetadata> assetMetadataDetails;

        /// <summary>
        /// The private boolean variable used to indicate whether the 
        /// metadata region should eb visible or not.
        /// </summary>
        private bool showMetadataInformation;

        /// <summary>
        /// Initializes a new instance of the MetadataViewPresentationModel class.
        /// </summary>
        /// <param name="view">The instance of IMetadataView interface.</param>
        /// <param name="configurationService">The instance of IConfigurationService interface.</param>
        /// <param name="eventAggregator">The instance of IEventAggregator interface.</param>
        public MetadataViewPresentationModel(IMetadataView view, IConfigurationService configurationService, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<ShowMetadataEvent>().Subscribe(this.ShowMetadata, true);
            this.eventAggregator.GetEvent<HideMetadataEvent>().Subscribe(this.HideMetadata, true);

            this.metadataFields = configurationService.GetMetadataFields();

            this.View = view;
            this.View.Model = this;
            this.ShowMetadataInformation = false;
        }

        /// <summary>
        /// Gets or sets the value of the the view to be set as datacontext 
        /// for the metadata information.
        /// </summary>
        /// <value>The metadata view.</value>
        public IMetadataView View { get; set; }

        /// <summary>
        /// Gets the list of <see cref="AssetMetadata"/> for the given asset.
        /// </summary>
        /// <value>The metadata details for an asset.</value>
        public IList<AssetMetadata> AssetMetadataDetails
        {
            get
            {
                return this.assetMetadataDetails;
            }

            private set
            {
                this.assetMetadataDetails = value;
                this.OnPropertyChanged("AssetMetadataDetails");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the metadata region will be visible 
        /// to show up the asset metadata.
        /// </summary>
        /// <value>A <c>true</c> if the metadata region is visible;otherwise false.</value>
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

        /// <summary>
        /// This method reads the value for any passed property name for the asset.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="asset">Asset to show up the metadata.</param>
        /// <returns>Property value as <see cref="string"/>.</returns>
        private static string GetPropertyValue(string propertyName, Asset asset)
        {
            string result = "Not Available";

            if (asset.GetType().GetProperty(propertyName.Replace(" ", string.Empty)) != null)
            {
                object propertyValue = asset.GetType().GetProperty(propertyName.Replace(" ", string.Empty)).GetValue(asset, null);

                if (propertyValue != null)
                {
                    result = propertyValue.ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// This method is called to show the metadata for an 
        /// asset in the metadata region.
        /// </summary>
        /// <param name="asset">Asset for the metadata.</param>
        private void ShowMetadata(Asset asset)
        {
            IList<AssetMetadata> metadataDetails = new List<AssetMetadata>();

            if (asset != null)
            {
                if (asset.Metadata == null)
                {
                    foreach (string metadataField in this.metadataFields)
                    {
                        metadataDetails.Add(new AssetMetadata
                        {
                            MetadataTagName = metadataField + ": ",
                            MetadataTagValue = GetPropertyValue(metadataField, asset)
                        });
                    }
                }
                else
                {
                    foreach (MetadataField field in asset.Metadata)
                    {
                        metadataDetails.Add(new AssetMetadata
                                                {
                                                    MetadataTagName = field.Name + ": ",
                                                    MetadataTagValue = (field.Value != null) ? field.Value.ToString() : string.Empty
                                                });
                    }
                }

                this.AssetMetadataDetails = metadataDetails;
                this.ShowMetadataInformation = true;
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
    }
}
