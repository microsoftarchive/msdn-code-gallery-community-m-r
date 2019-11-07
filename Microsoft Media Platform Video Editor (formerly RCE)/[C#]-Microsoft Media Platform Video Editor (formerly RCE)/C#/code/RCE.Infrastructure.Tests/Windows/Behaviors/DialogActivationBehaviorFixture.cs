// <copyright file="DialogActivationBehaviorFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DialogActivationBehaviorFixture.cs                     
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
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using Microsoft.Practices.Composite.Regions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Tests.Mocks;
    using RCE.Infrastructure.Windows;
    using RCE.Infrastructure.Windows.Behaviors;

    [TestClass]
    public class DialogActivationBehaviorFixture
    {
        private IWindowManager windowManager;

        [TestInitialize]
        public void Initialize()
        {
            this.windowManager = new MockWindowManager();
        }

        [TestMethod]
        public void ShouldCreateWindowOnViewActivation()
        {
            var parentCanvas = new Canvas();
            var region = new MockRegion();
            var view = new UserControl();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            Assert.IsNotNull(behavior.ViewMappings.First().Value);
            Assert.IsTrue(((MockWindow)behavior.ViewMappings.First().Value).ShowCalled);
            Assert.AreSame(view, behavior.ViewMappings.First().Value.Content);
        }

        [TestMethod]
        public void ShouldCloseWindowOnViewDeactivation()
        {
            var region = new MockRegion();
            var view = new UserControl();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            behavior.HostControl = new Canvas();
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);
            Assert.IsNotNull(behavior.ViewMappings.First().Value);

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Remove, view);

            Assert.IsTrue(((MockWindow)behavior.ViewMappings.First().Value).CloseCalled);
        }

        [TestMethod]
        public void ShouldRemoveViewWhenClosed()
        {
            var view = new UserControl();
            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            behavior.HostControl = new Canvas();
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            ((MockWindow)behavior.ViewMappings.First().Value).InvokeClosed();

            Assert.IsTrue(region.RemoveCalled);
        }

        [TestMethod]
        public void ShouldSetStyleToRegionWindow()
        {
            var parentControl = new Canvas();
            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            behavior.HostControl = parentControl;
            behavior.Region = region;

            var regionStyle = new Style();
            parentControl.SetValue(RegionWindowBehaviors.ContainerWindowStyleProperty, regionStyle);

            behavior.Attach();
            var view = new UserControl();
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            Assert.AreEqual(regionStyle, behavior.ViewMappings.First().Value.Style);
        }

        [TestMethod]
        public void ShouldCreateDifferentWindowsForEachView()
        {
            var view = new UserControl();
            var view2 = new UserControl();

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas();

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view2);

            Assert.AreEqual(2, behavior.ViewMappings.Count);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;
            var windowForView2 = behavior.ViewMappings.ElementAt(1).Value;

            Assert.AreNotSame(windowForView, windowForView2);

            Assert.IsTrue(((MockWindow)windowForView).ShowCalled);
            Assert.IsTrue(((MockWindow)windowForView2).ShowCalled);
        }

        [TestMethod]
        public void ShouldCloseCorrectWindowWhenRemovingViewFromRegion()
        {
            var view = new UserControl();
            var view2 = new UserControl();

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas();

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view2);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;
            var windowForView2 = behavior.ViewMappings.ElementAt(1).Value;

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Remove, view2);

            Assert.IsFalse(((MockWindow)windowForView).CloseCalled);
            Assert.IsTrue(((MockWindow)windowForView2).CloseCalled);
        }

        [TestMethod]
        public void ShouldSetLeftPositionToZeroWhenValueProvidedByWindowInfoProviderInViewDataContextIsLeft()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            view.DataContext = viewModel;
            
            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas { Width = 300 };

            viewModel.Horizontal = HorizontalWindowPosition.Left;

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual(0, windowForView.Left);
        }

        [TestMethod]
        public void ShouldSetLeftPositionToOneThirdOfWidthWhenValueProvidedByWindowInfoProviderInViewDataContextIsCenter()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas { Width = 300 };

            viewModel.Horizontal = HorizontalWindowPosition.Center;

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual(100, windowForView.Left);
        }

        [TestMethod]
        public void ShouldSetLeftPositionToPointFiftyFiveOfWidthWhenValueProvidedByWindowInfoProviderInViewDataContextIsRight()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas { Width = 300 };

            viewModel.Horizontal = HorizontalWindowPosition.Right;

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual(165, windowForView.Left);
        }

        [TestMethod]
        public void ShouldSetTopPositionToZeroWhenValueProvidedByWindowInfoProviderInViewDataContextIsTop()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas { Height = 300 };

            viewModel.Vertical = VerticalWindowPosition.Top;

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual(0, windowForView.Top);
        }

        [TestMethod]
        public void ShouldSetTopPositionToApproximatelyOneThirdOfHeightWhenValueProvidedByWindowInfoProviderInViewDataContextIsCenter()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas { Height = 300 };

            viewModel.Vertical = VerticalWindowPosition.Center;

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual(300 / 2.75, windowForView.Top);
        }

        [TestMethod]
        public void ShouldSetTopPositionToHalfOfHeightWhenValueProvidedByWindowInfoProviderInViewDataContextIsBottom()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas();

            parentCanvas.Height = 300;

            viewModel.Vertical = VerticalWindowPosition.Bottom;

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual(150, windowForView.Top);
        }

        [TestMethod]
        public void ShouldSetTitleBasedOnValueProvidedByWindowInfoProviderInViewDataContext()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            viewModel.TitleForWindow = "Window Title";

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas();

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual("Window Title", windowForView.Title);
        }

        [TestMethod]
        public void ShouldSetResizeModeToBothIfWindowInfoProviderResizeDirectionsIsBoth()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas();

            viewModel.Resize = ResizeDirection.Both;

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual(ResizeDirection.Both, windowForView.ResizeDirection);
        }

        [TestMethod]
        public void ShouldSetWindowSizeBasedOnWindowInfoProvider()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas();

            viewModel.SizeForWindow = new Size(10, 20);

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual(20, windowForView.Size.Height);
            Assert.AreEqual(10, windowForView.Size.Width);
        }

        [TestMethod]
        public void ShouldUpdateTitleWhenTitlePropertyChanges()
        {
            var view = new UserControl();

            var viewModel = new MockWindowMetadataProvider();

            viewModel.TitleForWindow = "Window Title";

            view.DataContext = viewModel;

            var region = new MockRegion();
            var behavior = new TestableDialogActivationBehavior(this.windowManager);
            var parentCanvas = new Canvas();

            behavior.HostControl = parentCanvas;
            behavior.Region = region;
            behavior.Attach();

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view);

            var windowForView = behavior.ViewMappings.ElementAt(0).Value;

            Assert.AreEqual("Window Title", windowForView.Title);

            viewModel.TitleForWindow = "Different Window Title";

            viewModel.InvokeTitleUpdated(view);

            Assert.AreEqual("Different Window Title", windowForView.Title);
        }

        public class MockDependencyObject : DependencyObject
        {
        }

        internal class MockRegion : IRegion
        {
            public MockRegion()
            {
                this.MockActiveViews = new MockViewsCollection();
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public MockViewsCollection MockActiveViews { get; set; }

            public bool DeactivateCalled { get; set; }

            public bool RemoveCalled { get; set; }

            public IViewsCollection ActiveViews
            {
                get
                {
                    return this.MockActiveViews;
                }
            }

            public IViewsCollection Views
            {
                get
                {
                    return this.MockActiveViews;
                }
            }

            public object Context
            {
                get { throw new NotImplementedException(); }
                set { throw new NotImplementedException(); }
            }

            public string Name
            {
                get { throw new NotImplementedException(); }
                set { throw new NotImplementedException(); }
            }

            public IRegionManager RegionManager
            {
                get { throw new NotImplementedException(); }
                set { throw new NotImplementedException(); }
            }

            public IRegionBehaviorCollection Behaviors
            {
                get { throw new NotImplementedException(); }
            }

            public void Deactivate(object view)
            {
                this.DeactivateCalled = true;
            }

            public IRegionManager Add(object view)
            {
                throw new NotImplementedException();
            }

            public IRegionManager Add(object view, string viewName)
            {
                throw new NotImplementedException();
            }

            public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
            {
                throw new NotImplementedException();
            }

            public void Remove(object view)
            {
                this.RemoveCalled = true;
            }

            public void Activate(object view)
            {
                throw new NotImplementedException();
            }

            public object GetView(string viewName)
            {
                throw new NotImplementedException();
            }
        }

        internal class MockViewsCollection : IViewsCollection
        {
            public event NotifyCollectionChangedEventHandler CollectionChanged;

            public void TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction action, object view)
            {
                NotifyCollectionChangedEventHandler handler = this.CollectionChanged;
                if (handler != null)
                {
                    NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(action, view, 0);
                    handler(this, args);
                }
            }

            public IEnumerator<object> GetEnumerator()
            {
                throw new System.NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public bool Contains(object value)
            {
                return true;
            }
        }

        private class MockWindowMetadataProvider : IWindowMetadataProvider
        {
            public event EventHandler<DataEventArgs<object>> TitleUpdated;

            public event EventHandler<DataEventArgs<object>> ResetPositionRaised;

            public VerticalWindowPosition Vertical { get; set; }

            public HorizontalWindowPosition Horizontal { get; set; }

            public string TitleForWindow { get; set; }

            public ResizeDirection Resize { get; set; }

            public Size SizeForWindow { get; set; }

            public VerticalWindowPosition VerticalPosition
            {
                get
                {
                    return this.Vertical;
                }
            }

            public HorizontalWindowPosition HorizontalPosition
            {
                get
                {
                    return this.Horizontal;
                }
            }

            public object Title
            {
                get
                {
                    return this.TitleForWindow;
                }
            }

            public ResizeDirection ResizeDirection
            {
                get
                {
                    return this.Resize;
                }
            }

            public Size Size
            {
                get
                {
                    return this.SizeForWindow;
                }
            }

            public void InvokeTitleUpdated(object view)
            {
                EventHandler<DataEventArgs<object>> handler = this.TitleUpdated;
                if (handler != null)
                {
                    handler(this, new DataEventArgs<object>(view));
                }
            }
        }

        private class TestableDialogActivationBehavior : DialogActivationBehavior
        {
            public TestableDialogActivationBehavior(IWindowManager windowManager)
                : base(windowManager)
            {
            }

            public Dictionary<object, IWindow> ViewMappings
            {
                get
                {
                    return this.ViewWindowMappings;
                }
            }

            protected override IWindow CreateWindow()
            {
                return new MockWindow();
            }
        }

        private class MockWindow : IWindow
        {
            public event EventHandler Closed;

            public event EventHandler Closing;

            public bool ShowCalled { get; set; }

            public bool CloseCalled { get; set; }

            public bool IsModal
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasFocus
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public object Title { get; set; }

            public ResizeDirection ResizeDirection { get; set; }

            public Size Size { get; set; }

            public object Content { get; set; }

            public Style Style { get; set; }

            public double Left { get; set; }

            public double Top { get; set; }

            public bool IsOpen
            {
                get
                {
                    return true;
                }
            }

            public void Show(Panel root)
            {
                this.ShowCalled = true;
            }

            public void ShowDialog(Panel root)
            {
                this.ShowCalled = true;
            }

            public void Close()
            {
                this.CloseCalled = true;
            }

            public void SetWindowPosition(double heigth, double width)
            {
                throw new NotImplementedException();
            }

            public void InvokeClosed()
            {
                EventHandler closedHandler = this.Closed;
                if (closedHandler != null)
                {
                    closedHandler(this, null);
                }
            }
        }
    }
}