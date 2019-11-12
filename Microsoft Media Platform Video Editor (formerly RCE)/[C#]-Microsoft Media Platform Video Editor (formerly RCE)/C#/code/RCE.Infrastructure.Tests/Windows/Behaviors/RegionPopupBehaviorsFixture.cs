// <copyright file="RegionPopupBehaviorsFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: RegionPopupBehaviorsFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Windows.Behaviors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;

    using Microsoft.Practices.Composite.Presentation.Regions;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Tests.Mocks;
    using RCE.Infrastructure.Windows.Behaviors;

    [TestClass]
    public class RegionPopupBehaviorsFixture
    {
        [TestMethod]
        public void ShouldCreateRegion()
        {
            try
            {
                var regionManager = new MockRegionManager();
                var windowManager = new MockWindowManager();
                var dialogActivationBehavior = new DialogActivationBehavior(windowManager);

                ServiceLocator.SetLocatorProvider(
                    () =>
                    new MockServiceLocator(
                        new Tuple<Type, Func<object>>(typeof(IRegionManager), () => regionManager),
                        new Tuple<Type, Func<object>>(typeof(DialogActivationBehavior), () => dialogActivationBehavior)));

                FrameworkElement hostControl = new MockFrameworkElement();
                RegionWindowBehaviors.RegisterNewWindowRegion(hostControl, "MyPopupRegion");

                Assert.IsTrue(regionManager.MockRegions.Regions.ContainsKey("MyPopupRegion"));
                Assert.IsNotNull(regionManager.MockRegions.Regions["MyPopupRegion"]);
                Assert.IsInstanceOfType(regionManager.MockRegions.Regions["MyPopupRegion"], typeof(AllActiveRegion));
                Assert.IsTrue(regionManager.MockRegions.Regions["MyPopupRegion"].Behaviors.ContainsKey(DialogActivationBehavior.BehaviorKey));
                Assert.IsInstanceOfType(regionManager.MockRegions.Regions["MyPopupRegion"].Behaviors[DialogActivationBehavior.BehaviorKey], typeof(DialogActivationBehavior));
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(() => null);
            }
        }

        internal class MockFrameworkElement : FrameworkElement
        {
        }

        internal class MockRegionManager : IRegionManager
        {
            public MockRegionManager()
            {
                this.MockRegions = new MockRegions();
            }

            public MockRegions MockRegions { get; private set; }

            public IRegionCollection Regions
            {
                get { return MockRegions; }
            }

            public IRegionManager CreateRegionManager()
            {
                throw new System.NotImplementedException();
            }
        }

        internal class MockRegions : IRegionCollection
        {
            public MockRegions()
            {
                this.Regions = new Dictionary<string, IRegion>();
            }

            public Dictionary<string, IRegion> Regions { get; private set; }

            public IRegion this[string regionName]
            {
                get { return this.Regions[regionName]; }
            }

            public void Add(IRegion region)
            {
                this.Regions.Add(region.Name, region);
            }

            public IEnumerator<IRegion> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            public bool Remove(string regionName)
            {
                throw new NotImplementedException();
            }

            public bool ContainsRegionWithName(string regionName)
            {
                throw new NotImplementedException();
            }
        }

        internal class MockServiceLocator : ServiceLocatorImplBase
        {
            private readonly Dictionary<Type, Func<object>> factoriesForType;

            public MockServiceLocator(params Tuple<Type, Func<object>>[] methodsForType)
            {
                this.factoriesForType = new Dictionary<Type, Func<object>>();
                
                foreach (Tuple<Type, Func<object>> tuple in methodsForType)
                {
                    this.factoriesForType[tuple.Item1] = tuple.Item2;
                }
            }

            protected override object DoGetInstance(Type serviceType, string key)
            {
                return this.factoriesForType[serviceType].Invoke();
            }

            protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
            {
                throw new NotImplementedException();
            }
        }
    }
}
