// <copyright file="MockViewsCollection.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockViewsCollection.cs                     
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
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Microsoft.Practices.Composite.Regions;

    public class MockViewsCollection : List<object>, IViewsCollection
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}