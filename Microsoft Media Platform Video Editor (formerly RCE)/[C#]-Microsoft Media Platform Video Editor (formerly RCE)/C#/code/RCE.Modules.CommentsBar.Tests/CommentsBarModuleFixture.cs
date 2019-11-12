// <copyright file="CommentsBarModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentsBarModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar.Tests
{
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure;

    /// <summary>
    /// Test class for <see cref="CommentsBarModule"/>.
    /// </summary>
    [TestClass]
    public class CommentsBarModuleFixture
    {
        /// <summary>
        /// Tests if the views and models are registered.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableCommentsBarModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(1, container.Types.Count);
            Assert.AreEqual(2, container.NamedTypes[typeof(ICommentsBarPresenter)].Count);
            Assert.AreEqual(typeof(CommentsBarView), container.Types[typeof(ICommentsBarView)]);
            Assert.AreEqual(typeof(TimelineCommentsBarPresenter), container.NamedTypes[typeof(ICommentsBarPresenter)][1]);
            Assert.AreEqual(typeof(SubClipCommentsBarPresenter), container.NamedTypes[typeof(ICommentsBarPresenter)][0]);
        }

        /// <summary>
        /// Tests if the <see cref="CommentsBarView"/> is added into the CommentBarRegion.
        /// </summary>
        [TestMethod]
        public void ShouldAddCommentsBarViewToCommentsBarRegion()
        {
            var regionViewRegistry = new MockRegionViewRegistry();
            var container = new MockUnityResolver();

            var presenter = new MockCommentsBarPresenter();
            container.Bag.Add(typeof(ICommentsBarPresenter), presenter);

            var module = new CommentsBarModule(container, regionViewRegistry);

            Assert.AreEqual(0, regionViewRegistry.ViewsByRegion.Count);

            module.Initialize();

            Assert.AreSame(presenter.View, regionViewRegistry.ViewsByRegion[RegionNames.CommentsBarRegion]);
        }

        /// <summary>
        /// Tests if the <see cref="CommentsBarView"/> is added into the CommentBarRegion.
        /// </summary>
        [TestMethod]
        public void ShouldAddSubClipCommentsBarViewToSubClipCommentsBarRegion()
        {
            var regionViewRegistry = new MockRegionViewRegistry();
            var container = new MockUnityResolver();

            var presenter = new MockCommentsBarPresenter();
            container.Bag.Add(typeof(ICommentsBarPresenter), presenter);

            var module = new CommentsBarModule(container, regionViewRegistry);

            Assert.AreEqual(0, regionViewRegistry.ViewsByRegion.Count);

            module.Initialize();

            Assert.AreSame(presenter.View, regionViewRegistry.ViewsByRegion[RegionNames.SubClipCommentsBarRegion]);
        }

        /// <summary>
        /// Testable class for <see cref="CommentsBarModule"/>.
        /// </summary>
        internal class TestableCommentsBarModule : CommentsBarModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableCommentsBarModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableCommentsBarModule(IUnityContainer container)
                : base(container, new MockRegionViewRegistry())
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