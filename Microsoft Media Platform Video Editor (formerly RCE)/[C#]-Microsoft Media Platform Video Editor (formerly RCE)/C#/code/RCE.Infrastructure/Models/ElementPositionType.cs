// <copyright file="ElementPositionType.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ElementPositionType.cs                     
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
    /// Specifies the change in <see cref="TimelineElement"/>.
    /// </summary>
    public enum ElementPositionType
    {
        /// <summary>
        /// If no change.
        /// </summary>
        None = 0,

        /// <summary>
        /// When user trims the in position.
        /// </summary>
        InPosition = 1,
        
        /// <summary>
        /// If element is moved by the user.
        /// </summary>
        Position = 2,
        
        /// <summary>
        /// When the user trims the out position.
        /// </summary>
        OutPosition = 3
    }
}