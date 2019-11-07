// <copyright file="TimelineDropInfo.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineDropInfo.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.DragDrop
{
    using System;
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Defines the types that can be dragged into the visual drop zone.
    /// </summary>
    public class TimelineDropInfo : IDropInfo
    {
        /// <summary>
        /// Gets the allowed types that can be dragged into the visual zone.
        /// </summary>
        /// <value>The list of allowed types.</value>
        public IList<Type> AllowedDragTypes
        {
            get
            {
                return new List<Type>
                           {
                               typeof(SmoothStreamingVideoAsset),
                               typeof(VideoAsset),
                               typeof(VideoAssetInOut),
                               typeof(ImageAsset),
                               typeof(TitleAsset),
                               typeof(TitleTemplate)
                           };
            }
        }
    }
}