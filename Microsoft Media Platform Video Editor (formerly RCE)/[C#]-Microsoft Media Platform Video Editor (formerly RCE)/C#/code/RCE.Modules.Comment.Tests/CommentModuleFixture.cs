// <copyright file="CommentModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Tests
{
    using Comment;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Services;

    /// <summary>
    /// Test class for <see cref="CommentModule"/>.
    /// </summary>
    [TestClass]
    public class CommentModuleFixture
    {
        /// <summary>
        /// Should register views.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableCommentModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(5, container.Types.Count);
            Assert.AreEqual(typeof(CommentView), container.Types[typeof(ICommentView)]);
            Assert.AreEqual(typeof(CommentViewPresentationModel), container.Types[typeof(ICommentViewPresentationModel)]);
            Assert.AreEqual(typeof(CommentViewPreview), container.Types[typeof(ICommentViewPreview)]);
            Assert.AreEqual(typeof(CommentEditBox), container.Types[typeof(ICommentEditBox)]);
            Assert.AreEqual(typeof(CommentEditBoxPresentationModel), container.Types[typeof(ICommentEditBoxPresentationModel)]);
        }

        /// <summary>
        /// Should register the comment timeline bar element to the timeline bar registry.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterCommentTimelineBarElementToTimelineBarRegistry()
        {
            var timelineBarRegistry = new MockTimelineBarRegistry();
            var regionManager = new MockRegionManager();
            regionManager.Regions.Add("MainRegion", new MockRegion());
            var container = new MockUnityResolver();

            container.Bag.Add(typeof(ICommentViewPresentationModel), new MockCommentViewPresentationModel());
            container.Bag.Add(typeof(ITimelineBarRegistry), timelineBarRegistry);

            var module = new CommentModule(container, new MockRegionViewRegistry());

            Assert.IsFalse(timelineBarRegistry.RegisterTimelineBarElementCalled);

            module.Initialize();

            Assert.IsTrue(timelineBarRegistry.RegisterTimelineBarElementCalled);
            Assert.AreEqual("Comment", timelineBarRegistry.RegisterTimelineBarElementKeyArgument);
            Assert.IsNotNull(timelineBarRegistry.RegisterTimelineBarElementValueArgument);
        }

        /// <summary>
        /// Testable class for <see cref="CommentModule"/>.
        /// </summary>
        internal class TestableCommentModule : CommentModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableCommentModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableCommentModule(IUnityContainer container)
                : base(container, new MockRegionViewRegistry())
            {
            }

            /// <summary>
            /// Invokes the register views and services.
            /// </summary>
            public void InvokeRegisterViewsAndServices()
            {
                this.RegisterViewsAndServices();
            }
        }
    }
}
