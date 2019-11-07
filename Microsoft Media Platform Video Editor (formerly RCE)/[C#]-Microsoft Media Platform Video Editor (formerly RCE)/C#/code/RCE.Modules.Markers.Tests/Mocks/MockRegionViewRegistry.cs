// <copyright file="MockRegionViewRegistry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockRegionViewRegistry.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>
namespace RCE.Modules.Markers.Tests.Mocks
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Composite.Regions;

    public class MockRegionViewRegistry : IRegionViewRegistry
    {
        public event EventHandler<ViewRegisteredEventArgs> ContentRegistered;

        public IEnumerable<object> GetContents(string regionName)
        {
            throw new NotImplementedException();
        }

        public void RegisterViewWithRegion(string regionName, Type viewType)
        {
            throw new NotImplementedException();
        }

        public void RegisterViewWithRegion(string regionName, Func<object> getContentDelegate)
        {
        }
    }
}
