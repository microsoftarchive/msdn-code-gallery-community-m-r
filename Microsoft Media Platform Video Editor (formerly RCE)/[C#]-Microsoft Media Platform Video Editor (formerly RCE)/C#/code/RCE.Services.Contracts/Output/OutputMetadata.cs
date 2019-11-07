// <copyright file="OutputMetadata.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OutputMetadata.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts.Output
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the xEncoder metadata.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    [KnownType(typeof(OutputSettings))]
    [KnownType(typeof(WindowsMediaHeaderProperties))]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class OutputMetadata
    {
        /// <summary>
        /// Gets or sets the xEncoder settings.
        /// </summary>
        /// <value>The xEncoder settings used for output generation.</value>
        [DataMember]
        public OutputSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the header properties.
        /// </summary>
        /// <value>The header properties used for output generation.</value>
        [DataMember]
        public WindowsMediaHeaderProperties WindowsMediaHeaderProperties { get; set; }
    }
}
