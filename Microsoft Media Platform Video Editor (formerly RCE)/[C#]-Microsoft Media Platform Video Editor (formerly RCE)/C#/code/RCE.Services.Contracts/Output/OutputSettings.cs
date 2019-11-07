// <copyright file="OutputSettings.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OutputSettings.cs                     
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
    /// Defines the available attributes as settings for a xEncoder.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class OutputSettings
    {
        /// <summary>
        /// Gets or sets the height of the output.
        /// </summary>
        /// <value>The height of the output.</value>
        [DataMember]
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the output.
        /// </summary>
        /// <value>The width of the output.</value>
        [DataMember]
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the frame rate of the output.
        /// </summary>
        /// <value>The frame rate of the output.</value>
        [DataMember]
        public double? FrameRate { get; set; }

        /// <summary>
        /// Gets or sets the resize mode of the output.
        /// </summary>
        /// <value>The resize mode of the output.</value>
        [DataMember]
        public string ResizeMode { get; set; }

        /// <summary>
        /// Gets or sets the aspect ratio of the output.
        /// </summary>
        /// <value>The aspect ratio of the output.</value>
        [DataMember]
        public string AspectRatio { get; set; }
    }
}