// <copyright file="Item.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Item.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// An abstract class that defines an item for RCE.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    [KnownType(typeof(MediaItem))]
    [KnownType(typeof(VideoItem))]
    [KnownType(typeof(ImageItem))]
    [KnownType(typeof(AudioItem))]
    [KnownType(typeof(SubClipItem))]
    [KnownType(typeof(OverlayItem))]
    public abstract class Item : RceObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        protected Item()
        {
            this.Resources = new ResourceCollection();
        }

        /// <summary>
        /// Gets or sets the CMS Id of the item.
        /// </summary>
        /// <value>The CMS Id of the item.</value>
        [DataMember]
        public string CMSId { get; set; }

        /// <summary>
        /// Gets or sets the Azure Id of the item.
        /// </summary>
        /// <value>The Azure Id of the item.</value>
        [DataMember]
        public string AzureId { get; set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        /// <value>The description of the item.</value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the title of the item.
        /// </summary>
        /// <value>The title of the item.</value>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ResourceCollection"/> that identifies the resources that are contained in the <see cref="Item"/>.
        /// </summary>
        /// <value>The collection of resources of the item.</value>
        [DataMember]
        public ResourceCollection Resources { get; set; }

        /// <summary>
        /// Gets or sets the metadata of the item.
        /// </summary>
        /// <value>The metadata of the item.</value>
        [DataMember]
        public List<MetadataField> Metadata { get; set; }
    }
}
