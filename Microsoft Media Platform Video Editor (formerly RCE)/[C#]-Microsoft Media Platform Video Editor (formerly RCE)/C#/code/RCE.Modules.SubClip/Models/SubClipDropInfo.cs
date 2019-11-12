// <copyright file="SubClipDropInfo.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClipDropInfo.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip.Models
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure.DragDrop;
    using RCE.Infrastructure.Models;

    public class SubClipDropInfo : IDropInfo
    {
        public IList<Type> AllowedDragTypes
        {
            get
            {
                return new List<Type> { typeof(AudioAsset), typeof(VideoAsset), typeof(SmoothStreamingVideoAsset), typeof(VideoAssetInOut) };
            }
        }
    }
}
