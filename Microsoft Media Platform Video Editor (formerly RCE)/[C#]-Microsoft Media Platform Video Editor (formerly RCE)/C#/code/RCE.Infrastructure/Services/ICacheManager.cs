// <copyright file="ICacheManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ICacheManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure.Models;

    public interface ICacheManager
    {
        event EventHandler<DataEventArgs<Tuple<double, double, Asset>>> CacheUpdated;

        event EventHandler<DataEventArgs<Tuple<IDictionary<double, double>, Asset>>> CacheRebuilt;

        IDictionary<double, double> RetrieveAssetCache(IAdaptiveAsset adaptiveAsset);
    }
}
