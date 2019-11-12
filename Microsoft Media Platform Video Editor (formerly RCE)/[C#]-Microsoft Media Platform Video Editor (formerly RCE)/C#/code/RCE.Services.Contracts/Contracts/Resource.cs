// <copyright file="Resource.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Resource.cs                     
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
    using System.Runtime.Serialization;

    /// <summary>
    /// A class that defines what resources are attached to a <see cref="MediaItem"/>.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class Resource : RceObject
    {
        /// <summary>
        /// Gets or sets the mime type associated to the resource.
        /// </summary>
        /// <value>The mime type associated to the resource.</value>
        [DataMember]
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the addressable URL of the <see cref="MediaItem"/>.
        /// </summary>
        /// <value>The URL of the resource.</value>
        [DataMember]
        public string Ref { get; set; }

        /// <summary>
        /// Gets or sets the resource type associated to the resource.
        /// </summary>
        /// <value>The resource type of the resource.</value>
        [DataMember]
        public string ResourceType { get; set; }
    }
}