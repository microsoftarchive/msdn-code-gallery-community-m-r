// <copyright file="UnityInstanceProviderFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UnityInstanceProviderFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Infrastructure.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// A class for testing the <see cref="UnityInstanceProvider"/>.
    /// </summary>
    [TestClass]
    public class UnityInstanceProviderFixture
    {
        /// <summary>
        /// The mocked UnityContainer.
        /// </summary>
        private MockUnityContainer container;

        /// <summary>
        /// Initializes resources need it by the tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.container = new MockUnityContainer();
        }

        /// <summary>
        /// Tests that the Resolve method should be called when calling to GetInstance without any message.
        /// </summary>
        [TestMethod]
        public void ShouldCallToResolveWhenCallingToGetInstanceWithoutMessage()
        {
            var unityInstanceProvider = new UnityInstanceProvider(this.container, typeof(object));

            Assert.IsFalse(this.container.ResolveCalled);

            unityInstanceProvider.GetInstance(null);

            Assert.IsTrue(this.container.ResolveCalled);
        }

        /// <summary>
        /// Tests that the Resolve method should be called when calling to GetInstance.
        /// </summary>
        [TestMethod]
        public void ShouldCallToResolveWhenCallingToGetInstance()
        {
            var unityInstanceProvider = new UnityInstanceProvider(this.container, typeof(object));

            Assert.IsFalse(this.container.ResolveCalled);

            unityInstanceProvider.GetInstance(null, null);

            Assert.IsTrue(this.container.ResolveCalled);
        }

        /// <summary>
        /// Tests that the Teardown method should be called when calling to ReleaseInstance without any message.
        /// </summary>
        [TestMethod]
        public void ShouldCallToTeardownWhenCallingToReleaseInstance()
        {
            var unityInstanceProvider = new UnityInstanceProvider(this.container, typeof(object));

            var instance = new object();

            Assert.IsFalse(this.container.TeardownCalled);

            unityInstanceProvider.ReleaseInstance(null, instance);

            Assert.IsTrue(this.container.TeardownCalled);
            Assert.AreEqual(instance, this.container.TeardownArgument);
        }
    }
}