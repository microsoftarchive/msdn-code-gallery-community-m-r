// <copyright file="MockRegionCollection.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockRegionCollection.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Tests.Mocks
{
    using System.Collections;
    using System.Collections.Generic;

    using Microsoft.Practices.Composite.Regions;

    public class MockRegionCollection : IRegionCollection
    {
        private readonly Dictionary<string, IRegion> regions = new Dictionary<string, IRegion>();

        public IRegion this[string regionName]
        {
            get { return this.regions[regionName]; }
        }

        public IEnumerator<IRegion> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(IRegion region)
        {
            this.regions[region.Name] = region;
        }

        public bool Remove(string regionName)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsRegionWithName(string regionName)
        {
            return true;
        }
    }
}