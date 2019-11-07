// <copyright file="ConfigurationServiceFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ConfigurationServiceFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Services;

    /// <summary>
    /// Test class for <see cref="ConfigurationService"/>.
    /// </summary>
    [TestClass]
    public class ConfigurationServiceFixture
    {
        /// <summary>
        /// Tests that exception is thrown if the constructor parameter is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowIfSettingsAreNull()
        {
            new ConfigurationService(null);
        }

        [TestMethod]
        public void ShouldReturnParameterValueIfParameterExists()
        {
            var settings = new Dictionary<string, string> { { "test", "value" } };

            var configurationService = new ConfigurationService(settings);

            var result = configurationService.GetParameterValue("test");

            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void ShouldReturnNullIfParameterDoesNotExists()
        {
            var settings = new Dictionary<string, string>();
            var configurationService = new ConfigurationService(settings);

            var result = configurationService.GetParameterValue("test");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldRaiseConfigurationChangedEventIfConfigurationChanges()
        {
            bool eventRaised = false;
            var settings = new Dictionary<string, string> { { "test", "value" } };

            var configurationService = new ConfigurationService(settings);
            configurationService.ConfigurationChanged += (s, e) => eventRaised = true;

            configurationService.UpdateParameters(new Dictionary<string, string> { { "test", "value1" } });

            Assert.IsTrue(eventRaised);
            Assert.AreEqual("value1", configurationService.GetParameterValue("test"));
        }

        [TestMethod]
        public void ShouldNotRaiseConfigurationChangedEventIfConfigurationDoesNotChange()
        {
            bool eventRaised = false;
            var settings = new Dictionary<string, string> { { "test", "value" } };

            var configurationService = new ConfigurationService(settings);
            configurationService.ConfigurationChanged += (s, e) => eventRaised = true;

            configurationService.UpdateParameters(new Dictionary<string, string> { { "test", "value" } });

            Assert.IsFalse(eventRaised);
            Assert.AreEqual("value", configurationService.GetParameterValue("test"));
        }
    }
}
