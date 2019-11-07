// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Behaviors;

namespace StockTraderRI.Infrastructure.Tests.Behaviors
{
    [TestClass]
    public class DialogActivationBehaviorFixture
    {
        [TestMethod]
        public void ShouldCreateWindowOnViewActivation()
        {
            var parentWindow = new MockDependencyObject();
            var region = new MockRegion();
            var view = new UserControl();
            var behavior = new TestableDialogActivationBehavior();
            behavior.HostControl = parentWindow;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            Assert.IsNotNull(behavior.CreatedWindow);
            Assert.IsTrue(behavior.CreatedWindow.ShowCalled);
            Assert.AreSame(view, behavior.CreatedWindow.Content);
            Assert.AreSame(parentWindow, behavior.CreatedWindow.Owner);
        }

        [TestMethod]
        public void ShouldCloseWindowOnViewDeactivation()
        {
            var region = new MockRegion();
            var view = new UserControl();
            var behavior = new TestableDialogActivationBehavior();
            behavior.HostControl = new MockDependencyObject();
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);
            Assert.IsNotNull(behavior.CreatedWindow);

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Remove, view);

            Assert.IsTrue(behavior.CreatedWindow.CloseCalled);
        }

        [TestMethod]
        public void ShouldDeactivateViewWhenClosed()
        {
            var view = new UserControl();
            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior();
            behavior.HostControl = new MockDependencyObject();
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            behavior.CreatedWindow.InvokeClosed();

            Assert.IsTrue(region.DeactivateCalled);
        }

        [TestMethod]
        public void ShouldAlwaysShowOnlyLastActiveView()
        {
            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior();
            behavior.HostControl = new MockDependencyObject();
            behavior.Region = region;
            behavior.Attach();

            var view1 = new UserControl();
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view1);
            Assert.AreSame(view1, behavior.CreatedWindow.Content);

            var view2 = new UserControl();
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view2);
            Assert.AreSame(view2, behavior.CreatedWindow.Content);
        }

        [TestMethod]
        public void ShouldSetStyleToRegionWindow()
        {
            var parentWindow = new MockDependencyObject();
            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior();
            behavior.HostControl = parentWindow;
            behavior.Region = region;

            var regionStyle = new Style();
            parentWindow.SetValue(RegionPopupBehaviors.ContainerWindowStyleProperty, regionStyle);

            behavior.Attach();
            var view = new UserControl();
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            Assert.AreEqual(regionStyle, behavior.CreatedWindow.Style);
        }

        private class TestableDialogActivationBehavior : DialogActivationBehavior
        {
            public MockWindow CreatedWindow;

            protected override IWindow CreateWindow()
            {
                if (CreatedWindow == null)
                {
                    CreatedWindow = new MockWindow();
                }

                return CreatedWindow;
            }
        }

        private class MockWindow : IWindow
        {
            public bool ShowCalled;

            public bool CloseCalled;

            public event EventHandler Closed;

            public object Content { get; set; }

            public object Owner { get; set; }

            public Style Style { get; set; }

            public void Show()
            {
                ShowCalled = true;
            }

            public void Close()
            {
                CloseCalled = true;
            }

            public void InvokeClosed()
            {
                EventHandler closedHandler = Closed;
                if (closedHandler != null) closedHandler(this, null);
            }
        }

        public class MockDependencyObject : DependencyObject
        {
        }

        internal class MockRegion : IRegion
        {
            public MockViewsCollection MockActiveViews;
            public bool DeactivateCalled;

            public IViewsCollection ActiveViews
            {
                get
                {
                    return MockActiveViews;
                }
            }

            public MockRegion()
            {
                this.MockActiveViews = new MockViewsCollection();
            }

            public void Deactivate(object view)
            {
                DeactivateCalled = true;
            }

            #region Not Implemented members

            public event PropertyChangedEventHandler PropertyChanged;

            public IViewsCollection Views
            {
                get { throw new System.NotImplementedException(); }
            }

            public object Context
            {
                get { throw new System.NotImplementedException(); }
                set { throw new System.NotImplementedException(); }
            }

            public NavigationParameters NavigationParameters
            {
                get { throw new System.NotImplementedException(); }
                set { throw new System.NotImplementedException(); }
            }

            public string Name
            {
                get { throw new System.NotImplementedException(); }
                set { throw new System.NotImplementedException(); }
            }

            public IRegionManager Add(object view)
            {
                throw new System.NotImplementedException();
            }

            public IRegionManager Add(object view, string viewName)
            {
                throw new System.NotImplementedException();
            }

            public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
            {
                throw new System.NotImplementedException();
            }

            public void Remove(object view)
            {
                throw new System.NotImplementedException();
            }

            public void Activate(object view)
            {
                throw new System.NotImplementedException();
            }

            public object GetView(string viewName)
            {
                throw new System.NotImplementedException();
            }

            public IRegionManager RegionManager
            {
                get { throw new System.NotImplementedException(); }
                set { throw new System.NotImplementedException(); }
            }

            public IRegionBehaviorCollection Behaviors
            {
                get { throw new System.NotImplementedException(); }
            }

            public void MoveFrom(IRegion sourceRegion, object view)
            {
                throw new System.NotImplementedException();
            }

            public void MoveFrom(IRegion sourceRegion, object view, string viewName)
            {
                throw new System.NotImplementedException();
            }
   
            #endregion

            public void RequestNavigate(Uri source, Action<NavigationResult> navigationCallback)
            {
                throw new NotImplementedException();
            }

            public void RequestNavigate(Uri source, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
            {
                throw new NotImplementedException();
            }

            public IRegionNavigationService NavigationService
            {
                get { throw new NotImplementedException(); }
                set { throw new System.NotImplementedException(); }
            }


            public Comparison<object> SortComparison
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
        }

        internal class MockViewsCollection : IViewsCollection
        {
            public event NotifyCollectionChangedEventHandler CollectionChanged;

            public void TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction action, object view)
            {
                NotifyCollectionChangedEventHandler handler = CollectionChanged;
                if (handler != null)
                {
                    NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(action, view, 0);
                    handler(this, args);
                }
            }

            #region Not Implemented members

            public IEnumerator<object> GetEnumerator()
            {
                throw new System.NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public bool Contains(object value)
            {
                throw new System.NotImplementedException();
            }

            #endregion
        }
    }
}
