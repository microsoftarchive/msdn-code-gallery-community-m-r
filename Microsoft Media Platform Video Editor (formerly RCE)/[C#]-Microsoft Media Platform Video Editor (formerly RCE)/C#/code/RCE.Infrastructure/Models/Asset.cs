// <copyright file="Asset.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Asset.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using RCE.Services.Contracts;

    /// <summary>
    /// A class that represents an asset.
    /// </summary>
    public abstract class Asset : Audit
    {
        private string title;
        
        /// <summary>
        /// Gets or sets the Id of the asset.
        /// </summary>
        /// <value>The id of the asset.</value>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets or sets the CMS Id of the asset.
        /// </summary>
        /// <value>The CMS Id of the asset.</value>
        public string CMSId { get; set; }

        /// <summary>
        /// Gets or sets the Azure Id of the asset.
        /// </summary>
        /// <value>The Azure Id of the asset.</value>
        public string AzureId { get; set; }

        /// <summary>
        /// Gets or sets the ParentFolderId of the asset.
        /// </summary>
        /// <value>The parent folder id of the asset.</value>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets or sets the Source of the asset.
        /// </summary>
        /// <value>The source of the asset.</value>
        public Uri Source { get; set; }

        /// <summary>
        /// Gets or sets the ProviderUri that identifies the asset on the server.
        /// </summary>
        /// <value>The uri that identifies the asset on the server side.</value>
        public Uri ProviderUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the resouce type of the asset.
        /// </summary>
        /// <value>The type of the resource.</value>
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the metadata of the asset.
        /// </summary>
        /// <value>The list of metadata fields.</value>
        public List<MetadataField> Metadata { get; set; }

        public virtual string Summary
        { 
            get
            {
                return this.Title;
            } 
        }

        /// <summary>
        /// Gets a value indicating whether the asset is an adaptive or not.
        /// </summary>
        /// <value>A true if the asset is adaptive;otherwise false.</value>
        public bool IsAdaptiveAsset
        {
            get { return this.ResourceType == ResourceType.LiveSmoothStream || this.ResourceType == ResourceType.SmoothStream; }
        }
    }
}
