// <copyright file="MockCacheManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCacheManager.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Mocks
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class MockCacheManager : ICacheManager
    {
        public event EventHandler<DataEventArgs<Tuple<double, double, Asset>>> CacheUpdated;

        public event EventHandler<DataEventArgs<Tuple<IDictionary<double, double>, Asset>>> CacheRebuilt;

        public IDictionary<double, double> RetrieveAssetCache(IAdaptiveAsset adaptiveAsset)
        {
            return null;
        }
    }
}
