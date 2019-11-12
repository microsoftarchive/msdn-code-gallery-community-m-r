// <copyright file="ConfigurationServiceExtensionsFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ConfigurationServiceExtensionsFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Services;

    [TestClass]
    public class ConfigurationServiceExtensionsFixture
    {
        /// <summary>
        /// Tests if returns user name.
        /// </summary>
        [TestMethod]
        public void ShouldReturnUserName()
        {
            var settings = new Dictionary<string, string> { { "UserName", "test" } };

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual("test", configurationService.GetUserName());
        }

        /// <summary>
        /// Tests if returns max no. of items.
        /// </summary>
        [TestMethod]
        public void ShouldReturnMaxNumberOfItems()
        {
            var settings = new Dictionary<string, string> { { "MaxNumberOfItems", "100" } };

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual(100, configurationService.GetMaxNumberOfItems());
        }

        /// <summary>
        /// Tests if returns project id.
        /// </summary>
        [TestMethod]
        public void ShouldReturnProjectId()
        {
            var settings = new Dictionary<string, string> { { "ProjectId", "http://test/" } };

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual("http://test/", configurationService.GetProjectId().ToString());
        }

        /// <summary>
        /// Tests if returns metadata fields.
        /// </summary>
        [TestMethod]
        public void ShouldReturnMetadataFields()
        {
            var settings = new Dictionary<string, string> { { "MetadataFields", "test;another test" } };

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual(2, configurationService.GetMetadataFields().Count);
        }

        /// <summary>
        /// Tests if returns list of comment types.
        /// </summary>
        [TestMethod]
        public void ShouldReturnCommentTypes()
        {
            var settings = new Dictionary<string, string> { { "CommentTypes", "Ink;Global" } };

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual(2, configurationService.GetCommentTypes().Count);
        }

        /// <summary>
        /// Tests if returns undo level.
        /// </summary>
        [TestMethod]
        public void ShouldReturnUndoLevel()
        {
            var settings = new Dictionary<string, string> { { "UndoLevel", "100" } };

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual(100, configurationService.GetUndoLevel());
        }

        /// <summary>
        /// Tests if returns title templates.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTitleTemplates()
        {
            var spinnerUri = new Uri("http://test/spinner.xaml");

            var settings = new Dictionary<string, string> { { "TitleTemplates", "Spinner|" + spinnerUri } };

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual(spinnerUri, configurationService.GetTitleTemplate("Spinner"));
        }

        /// <summary>
        /// Tests if returns null if the title template doesn't exist.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenTitleTemplateKeyDoesNotExist()
        {
            var settings = new Dictionary<string, string>();

            var configurationService = new ConfigurationService(settings);

            var result = configurationService.GetTitleTemplate("Spinner");

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests if returns comment duration.
        /// </summary>
        [TestMethod]
        public void ShouldReturnCommentDuration()
        {
            var settings = new Dictionary<string, string> { { "CommentDurationInSeconds", "10" } };

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual(10, configurationService.GetCommentDuration());
        }

        /// <summary>
        /// Tests if returns null if comment duration doesn't exist.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullWhenCommentDurationDoesNotExist()
        {
            var settings = new Dictionary<string, string>();

            var configurationService = new ConfigurationService(settings);

            Assert.AreEqual(null, configurationService.GetCommentDuration());
        }

        [TestMethod]
        public void ShouldReturnDateTimeParameterValueIfParameterExists()
        {
            var value = new DateTime(2009, 1, 1);
            var settings = new Dictionary<string, string> { { "test", value.ToString() } };
            var configurationService = new ConfigurationService(settings);

            DateTime? result = configurationService.GetParameterValueAsDateTime("test");

            Assert.AreEqual(value, result.Value);
        }

        [TestMethod]
        public void ShouldReturnNullableDateTimeIfParameterDoesNotExists()
        {
            var settings = new Dictionary<string, string>();
            var configurationService = new ConfigurationService(settings);

            DateTime? result = configurationService.GetParameterValueAsDateTime("test");

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void ShouldReturnDoubleParameterValueIfParameterExists()
        {
            double value = 1;
            var settings = new Dictionary<string, string> { { "test", value.ToString() } };
            var configurationService = new ConfigurationService(settings);

            double? result = configurationService.GetParameterValueAsDouble("test");

            Assert.AreEqual(value, result.Value);
        }

        [TestMethod]
        public void ShouldReturnNullableDoubleIfParameterDoesNotExists()
        {
            var settings = new Dictionary<string, string>();
            var configurationService = new ConfigurationService(settings);

            double? result = configurationService.GetParameterValueAsDouble("test");

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void ShouldReturnIntParameterValueIfParameterExists()
        {
            int value = 1;
            var settings = new Dictionary<string, string> { { "test", value.ToString() } };
            var configurationService = new ConfigurationService(settings);

            int? result = configurationService.GetParameterValueAsInt("test");

            Assert.AreEqual(value, result.Value);
        }

        [TestMethod]
        public void ShouldReturnNullableIntIfParameterDoesNotExists()
        {
            var settings = new Dictionary<string, string>();
            var configurationService = new ConfigurationService(settings);

            int? result = configurationService.GetParameterValueAsInt("test");

            Assert.IsFalse(result.HasValue);
        }
    }
}
