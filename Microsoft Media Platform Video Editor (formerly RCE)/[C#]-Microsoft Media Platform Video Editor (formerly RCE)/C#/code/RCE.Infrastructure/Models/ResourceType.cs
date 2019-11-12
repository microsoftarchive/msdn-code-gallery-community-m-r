// <copyright file="ResourceType.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ResourceType.cs                     
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
    /// Defines the range of resource types that are available.
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// The resource is a Master.
        /// </summary>
        Master,

        /// <summary>
        /// The resource is a Proxy.
        /// </summary>
        Proxy,

        /// <summary>
        /// The resource is a Mezzanine.
        /// </summary>
        Mezzanine,

        /// <summary>
        /// The resource is a SmoothStream.
        /// </summary>
        SmoothStream,

        /// <summary>
        /// The resource is a LiveSmoothStream.
        /// </summary>
        LiveSmoothStream
    }
}
