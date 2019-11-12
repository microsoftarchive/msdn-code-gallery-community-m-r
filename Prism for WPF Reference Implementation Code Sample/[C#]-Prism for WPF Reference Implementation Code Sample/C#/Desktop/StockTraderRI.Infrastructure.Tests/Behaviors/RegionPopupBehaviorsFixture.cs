// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Behaviors;

namespace StockTraderRI.Infrastructure.Tests.Behaviors
{
    [TestClass]
    public class RegionPopupBehaviorsFixture
    {
        [TestMethod]
        public void ShouldCreateRegion()
        {
            try
            {
                var regionManager = new MockRegionManager();
                ServiceLocator.SetLocatorProvider(() => new MockServiceLocator(() => regionManager));

                FrameworkElement hostControl = new MockFrameworkElement();
                RegionPopupBehaviors.RegisterNewPopupRegion(hostControl, "MyPopupRegion");
                RegionPopupBehaviors.SetCreatePopupRegionWithName(hostControl, "MyPopupRegion");
                Assert.AreEqual("MyPopupRegion", RegionPopupBehaviors.GetCreatePopupRegionWithName(hostControl));

                var style = new Style();
                RegionPopupBehaviors.SetContainerWindowStyle(hostControl, style);
                Assert.AreEqual(style, RegionPopupBehaviors.GetContainerWindowStyle(hostControl));

                Assert.IsTrue(regionManager.MockRegions.Regions.ContainsKey("MyPopupRegion"));
                Assert.IsNotNull(regionManager.MockRegions.Regions["MyPopupRegion"]);
                Assert.IsInstanceOfType(regionManager.MockRegions.Regions["MyPopupRegion"], typeof(SingleActiveRegion));
                Assert.IsTrue(regionManager.MockRegions.Regions["MyPopupRegion"].Behaviors.ContainsKey(DialogActivationBehavior.BehaviorKey));
                Assert.IsInstanceOfType(regionManager.MockRegions.Regions["MyPopupRegion"].Behaviors[DialogActivationBehavior.BehaviorKey], typeof(WindowDialogActivationBehavior));
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(() => null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetContainerWindowStyleShouldThrowExceptionWhenOwnerIsNull()
        {
            RegionPopupBehaviors.GetContainerWindowStyle(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetContainerWindowStyleShouldThrowExceptionWhenOwnerIsNull()
        {
            RegionPopupBehaviors.SetContainerWindowStyle(null, new Style());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCreatePopupRegionWithNameShouldThrowExceptionWhenOwnerIsNull()
        {
            RegionPopupBehaviors.GetCreatePopupRegionWithName(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetCreatePopupRegionWithNameShouldThrowExceptionWhenOwnerIsNull()
        {
            RegionPopupBehaviors.SetCreatePopupRegionWithName(null, "My Region");
        }

        internal class MockFrameworkElement : FrameworkElement
        {
        }

        internal class MockRegionManager : IRegionManager
        {
            public MockRegions MockRegions = new MockRegions();

            public IRegionCollection Regions { get { return MockRegions; } }

            #region Not implemented members

            public IRegionManager CreateRegionManager()
            {
                throw new System.NotImplementedException();
            }

            #endregion

            public bool Navigate(Uri source)
            {
                throw new NotImplementedException();
            }
        }

        internal class MockRegions : IRegionCollection
        {
            public Dictionary<string, IRegion> Regions = new Dictionary<string, IRegion>();

            public IRegion this[string regionName]
            {
                get { return this.Regions[regionName]; }
            }

            public void Add(IRegion region)
            {
                this.Regions.Add(region.Name, region);
            }

            #region Not implemented members

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

            #endregion

            public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;
        }

        internal class MockServiceLocator : ServiceLocatorImplBase
        {
            public Func<object> ResolveMethod;

            public MockServiceLocator(Func<object> resolveMethod)
            {
                this.ResolveMethod = resolveMethod;
            }

            protected override object DoGetInstance(Type serviceType, string key)
            {
                return this.ResolveMethod();
            }

            #region Not implemented members

            protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }
}
