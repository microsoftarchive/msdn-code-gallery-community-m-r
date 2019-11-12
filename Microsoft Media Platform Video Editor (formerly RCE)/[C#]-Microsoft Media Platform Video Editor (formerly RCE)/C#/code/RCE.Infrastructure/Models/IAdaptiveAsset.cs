// <copyright file="IAdaptiveAsset.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IAdaptiveAsset.cs                     
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

    public interface IAdaptiveAsset
    {
        /// <summary>
        /// Gets or sets the start position of the Video.
        /// </summary>
        /// <value>The start position of the video.</value>
        double StartPosition { get; set; }

        /// <summary>
        /// Gets or sets the Source of the asset.
        /// </summary>
        /// <value>The source of the asset.</value>
        Uri Source { get; set; }

        double DurationInSeconds { get; }

        bool IsStereo { get; set; }
    }
}