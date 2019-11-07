// <copyright file="AssetMetadata.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetMetadata.cs                     
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
    /// <summary>
    /// A class that represents an asset's metadata.
    /// </summary>
    public class AssetMetadata
    {
        /// <summary>
        /// Initializes a new instance of the AssetMetadata class.
        /// </summary>
        public AssetMetadata()
        {
        }

        /// <summary>
        /// Initializes a new instance of the AssetMetadata class. Overloaded constructor.
        /// Set the MetadataTagName and MetadataTagValue, when passes as parameters to this constructor.
        /// </summary>
        /// <param name="tagName">Metadata tag name as configured in configurations.</param>
        /// <param name="tagValue">Value for the metadata tag.</param>
        public AssetMetadata(string tagName, string tagValue)
        {
            this.MetadataTagName = tagName;
            this.MetadataTagValue = tagValue;
        }

        /// <summary>
        /// Gets or sets the value of the [MetadataTag] of the asset.
        /// </summary>
        /// <value>The tag name used to get the value.</value>
        public string MetadataTagName { get; set; }

        /// <summary>
        /// Gets or sets the value for the [MetadataTagValue] of the asset.
        /// </summary>
        /// <value>The value of the specified metadata tag.</value>
        public string MetadataTagValue { get; set; }
    }
}