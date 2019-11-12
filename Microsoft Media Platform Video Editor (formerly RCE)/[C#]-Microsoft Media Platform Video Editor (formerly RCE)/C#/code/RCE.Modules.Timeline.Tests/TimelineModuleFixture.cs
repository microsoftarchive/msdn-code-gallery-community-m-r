// <copyright file="TimelineModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests
{
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Menu;
    using RCE.Modules.Timeline.Locking;
    using RCE.Modules.Timeline.Views;

    using Timeline.Commands;

    /// <summary>
    /// A class for testing the <see cref="TimelineModule"/>.
    /// </summary>
    [TestClass]
    public class TimelineModuleFixture
    {
        private MockWindowManager windowManager;

        [TestInitialize]
        public void SetUp()
        {
            this.windowManager = new MockWindowManager();
        }

        /// <summary>
        /// Tests that the views should be registered in the container.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableTimelineModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(6, container.Types.Count);
            Assert.AreEqual(typeof(SequencesView), container.Types[typeof(ISequencesView)]);
            Assert.AreEqual(typeof(SequencesViewModel), container.Types[typeof(ISequencesViewModel)]);
            Assert.AreEqual(typeof(TimelineView), container.Types[typeof(ITimelineView)]);
            Assert.AreEqual(typeof(TimelinePresenter), container.Types[typeof(ITimelinePresenter)]);
            Assert.AreEqual(typeof(Caretaker), container.Types[typeof(ICaretaker)]);
            Assert.AreEqual(typeof(LockGroupManager), container.Types[typeof(ILockGroupManager)]);
        }

        /// <summary>
        /// Tests that the TimelineView should be added to the Timeline region.
        /// </summary>
        [TestMethod]
        public void ShouldAddTimelineViewToMainRegion()
        {
            var regionViewRegistry = new MockRegionViewRegistry();
            var regionManager = new MockRegionManager();
            var container = new MockUnityResolver();
            var menuButtonView = new MockMenuButtonView();

            MockTimelinePresenter timelinePresenter = new MockTimelinePresenter();

            container.Bag[typeof(ITimelinePresenter)] = timelinePresenter;

            MockTimelineMenuButtonViewModel menuViewModel = new MockTimelineMenuButtonViewModel();
            menuViewModel.View = menuButtonView;

            container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;
            container.Bag.Add(typeof(ISequencesViewModel), new MockSequencesViewModel());

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            regionManager.Regions.Add("MainRegion", mainRegion);
            regionManager.Regions.Add("MenuRegion", menuRegion);

            var module = new TimelineModule(container, regionManager, regionViewRegistry, this.windowManager);

            Assert.AreEqual(0, mainRegion.AddedViews.Count);

            module.Initialize();

            Assert.AreEqual(1, mainRegion.AddedViews.Count);
            Assert.IsInstanceOfType(mainRegion.AddedViews[0], typeof(ITimelineView));
        }

        [TestMethod]
        public void ShouldAddSequenceMenuButtonViewToMenuRegion()
        {
            var container = new MockUnityResolver();
            var regionManager = new MockRegionManager();
            var regionViewRegistry = new MockRegionViewRegistry();
            var module = new TimelineModule(container, regionManager, regionViewRegistry, this.windowManager);
            var menuButtonView = new MockMenuButtonView();

            MockTimelinePresenter timelinePresenter = new MockTimelinePresenter();

            container.Bag[typeof(ITimelinePresenter)] = timelinePresenter;

            MockTimelineMenuButtonViewModel menuViewModel = new MockTimelineMenuButtonViewModel();
            menuViewModel.View = menuButtonView;

            container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;
            container.Bag.Add(typeof(ISequencesViewModel), new MockSequencesViewModel());

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            regionManager.Regions.Add(mainRegion);
            regionManager.Regions.Add(menuRegion);

            Assert.AreEqual(0, menuRegion.AddedViews.Count);

            module.Initialize();

            Assert.AreSame(timelinePresenter.View, menuViewModel.ViewToDisplay);
            Assert.IsTrue(menuViewModel.IsViewActive);
            Assert.AreEqual(1, menuRegion.AddedViews.Count);
            Assert.IsNotNull(menuViewModel.View);
            Assert.AreSame(menuViewModel.View, menuRegion.AddedViews[0]);
            Assert.AreEqual("Sequence", menuViewModel.Text);
        }

        /// <summary>
        /// Should add Media bin view to tools region.
        /// </summary>
        [TestMethod]
        public void ShouldAddSequencesViewToProjectBrowserRegion()
        {
            var regionManager = new MockRegionManager();
            var regionViewRegistry = new MockRegionViewRegistry();
            var container = new MockUnityResolver();
            var menuButtonView = new MockMenuButtonView();

            MockTimelinePresenter timelinePresenter = new MockTimelinePresenter();
            MockSequencesViewModel sequenceViewModel = new MockSequencesViewModel();

            container.Bag[typeof(ITimelinePresenter)] = timelinePresenter;
            container.Bag[typeof(ISequencesViewModel)] = sequenceViewModel;

            MockTimelineMenuButtonViewModel menuViewModel = new MockTimelineMenuButtonViewModel();
            menuViewModel.View = menuButtonView;

            container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            regionManager.Regions.Add(mainRegion);
            regionManager.Regions.Add(menuRegion);

            var module = new TimelineModule(container, regionManager, regionViewRegistry, this.windowManager);

            module.Initialize();

            Assert.IsInstanceOfType(regionViewRegistry.ViewsByRegion[RegionNames.ProjectBrowserRegion], typeof(ISequencesView));
        }

        /// <summary>
        /// Testable Timeline Module.
        /// </summary>
        internal class TestableTimelineModule : TimelineModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableTimelineModule"/> class.
            /// </summary>
            /// <param name="container">The container used to register views an services.</param>
            public TestableTimelineModule(IUnityContainer container)
                : base(container, new MockRegionManager(), new MockRegionViewRegistry(), new MockWindowManager())
            {
            }

            /// <summary>
            /// Invokes the RegisterViewAndServices method.
            /// </summary>
            public void InvokeRegisterViewsAndServices()
            {
                this.RegisterViewsAndServices();
            }
        }
    }
}