// <copyright file="UnityBehaviorExtensionElementFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UnityBehaviorExtensionElementFixture.cs                     
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
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="UnityBehaviorExtensionElement"/>.
    /// </summary>
    [TestClass]
    public class UnityBehaviorExtensionElementFixture
    {
        /// <summary>
        /// Should loading container service behaviour from configuration.
        /// </summary>
        [TestMethod]
        public void ShouldCreateServiceBehaviorLoadingContainerFromConfiguration()
        {
            var unityBehaviorExtensionElement = new TestableUnityBehaviorExtensionElement();

            var behavior = unityBehaviorExtensionElement.BaseCreateBehavior();

            Assert.IsNotNull(behavior);
            Assert.IsInstanceOfType(behavior, typeof(UnityServiceBehavior));
        }

        /// <summary>
        /// Should loading container service behaviour from configuration based on the container name.
        /// </summary>
        [TestMethod]
        public void ShouldCreateServiceBehaviorLoadingContainerFromConfigurationBasedOnContainerName()
        {
            var unityBehaviorExtensionElement = new TestableUnityBehaviorExtensionElement
                                                    {
                                                        ContainerName = "testContainer",
                                                        UnityConfigurationSectionPath = "anotherUnity"
                                                    };

            var behavior = unityBehaviorExtensionElement.BaseCreateBehavior();

            Assert.IsNotNull(behavior);
            Assert.IsInstanceOfType(behavior, typeof(UnityServiceBehavior));
        }

        /// <summary>
        /// Should be the unity service behavior.
        /// </summary>
        [TestMethod]
        public void ShouldBeUnityServiceBehavior()
        {
            var unityBehaviorExtensionElement = new TestableUnityBehaviorExtensionElement();

            var behavior = unityBehaviorExtensionElement.BehaviorType;

            Assert.IsNotNull(behavior);
            Assert.AreEqual(behavior, typeof(UnityServiceBehavior));
        }

        /// <summary>
        /// Should thow exception if configuration section does not exist.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThownExceptionIfConfigurationSectionDoesNotExist()
        {
            var unityBehaviorExtensionElement = new TestableUnityBehaviorExtensionElement
                                                    {
                                                        UnityConfigurationSectionPath = "notExist"
                                                    };

            unityBehaviorExtensionElement.BaseCreateBehavior();
        }

        /// <summary>
        /// Testable class for <see cref="UnityBehaviorExtensionElement"/>.
        /// </summary>
        private class TestableUnityBehaviorExtensionElement : UnityBehaviorExtensionElement
        {
            /// <summary>
            /// Behaviour class.
            /// </summary>
            /// <returns>Base create behaviour.</returns>
            public object BaseCreateBehavior()
            {
                return this.CreateBehavior();
            }
        }
    }
}