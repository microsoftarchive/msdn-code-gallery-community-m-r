// <copyright file="ProjectsModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectsModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects.Tests
{
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Menu;
    using RCE.Modules.EncoderOutput.Tests.Mocks;

    /// <summary>
    /// A class for testing the <see cref="ProjectsModule"/>.
    /// </summary>
    [TestClass]
    public class ProjectsModuleFixture
    {
        /// <summary>
        /// Tests that the views are being registered in the container.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableProjectsModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(2, container.Types.Count);
            Assert.AreEqual(typeof(ProjectView), container.Types[typeof(IProjectView)]);
            Assert.AreEqual(typeof(ProjectViewPresenter), container.Types[typeof(IProjectViewPresenter)]);
        }

        /// <summary>
        /// Tests that the <see cref="ProjectView"/> is being added to the <seealso cref="IRegion">Tools Region</seealso>.
        /// </summary>
        [TestMethod]
        public void ShouldAddViewsToRegions()
        {
            var mainRegion = new MockRegion();
            var menuRegion = new MockRegion();
            var regionManager = new MockRegionManager();
            var container = new MockUnityResolver();
            var windowManager = new MockWindowManager();

            container.Bag.Add(typeof(IProjectViewPresenter), new MockProjectViewPresenter());
            container.Bag.Add(typeof(IMenuButtonViewModel), new MockProjectMenuButtonViewModel());

            regionManager.Regions.Add("MainRegion", mainRegion);
            regionManager.Regions.Add("MenuRegion", menuRegion);

            var module = new ProjectsModule(container, regionManager, windowManager);

            Assert.AreEqual(0, mainRegion.AddedViews.Count);
            Assert.AreEqual(0, menuRegion.AddedViews.Count);

            module.Initialize();

            Assert.AreEqual(1, mainRegion.AddedViews.Count);
            Assert.AreEqual(1, menuRegion.AddedViews.Count);
            Assert.IsInstanceOfType(mainRegion.AddedViews[0], typeof(IProjectView));
            Assert.IsInstanceOfType(menuRegion.AddedViews[0], typeof(IMenuButtonView));
        }

        /// <summary>
        /// Defines a testable <see cref="ProjectsModule"/>.
        /// </summary>
        internal class TestableProjectsModule : ProjectsModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableProjectsModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableProjectsModule(IUnityContainer container)
                : base(container, new MockRegionManager(), new MockWindowManager())
            {
            }

            /// <summary>
            /// Invokes the RegisterViewsAndServices.
            /// </summary>
            public void InvokeRegisterViewsAndServices()
            {
                this.RegisterViewsAndServices();
            }
        }
    }
}