// <copyright file="MockRegion.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockRegion.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Microsoft.Practices.Composite.Regions;

    public class MockRegion : IRegion
    {
        private readonly List<object> addedViews = new List<object>();
        private readonly IViewsCollection views = new MockViewsCollection();

        public event PropertyChangedEventHandler PropertyChanged;

        public List<object> AddedViews
        {
            get { return this.addedViews; }
        }

        public IViewsCollection Views
        {
            get { return this.views; }
        }

        public string Name { get; set; }

        public IRegionManager RegionManager
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IRegionBehaviorCollection Behaviors
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object SelectedItem { get; set; }

        public IViewsCollection ActiveViews { get; set; }

        public object Context
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IRegionManager Add(object view)
        {
            this.AddedViews.Add(view);
            return null;
        }

        public void Remove(object view)
        {
            this.AddedViews.Remove(view);
        }

        public void Activate(object view)
        {
            this.SelectedItem = view;
        }

        public void Deactivate(object view)
        {
            throw new NotImplementedException();
        }

        public IRegionManager Add(object view, string viewName)
        {
            this.Add(view);
            return null;
        }

        public object GetView(string viewName)
        {
            return null;
        }

        public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
        {
            throw new NotImplementedException();
        }
    }
}