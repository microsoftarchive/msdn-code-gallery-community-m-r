// <copyright file="MediaBin.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MediaBin.cs                     
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
    using System.Collections.ObjectModel;

    /// <summary>
    /// Specifies the properties of the media bin.
    /// </summary>
    public class MediaBin : Audit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaBin"/> class.
        /// </summary>
        public MediaBin()
        {
            this.Id = Guid.NewGuid();
            this.Assets = new ObservableCollection<Asset>();
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The unique identifier for the media bin.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the provider URI.
        /// </summary>
        /// <value>The provider URI.</value>
        public Uri ProviderUri { get; set; }

        /// <summary>
        /// Gets the assets of the media bin.
        /// </summary>
        /// <value>The assets.</value>
        public ObservableCollection<Asset> Assets { get; private set; }

        /// <summary>
        /// Adds the assets.
        /// </summary>
        /// <param name="assets">The assets.</param>
        public void AddAssets(ObservableCollection<Asset> assets)
        {
            this.Assets = assets;
        }
    }
}