// <copyright file="MetadataLocatorFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataLocatorFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Tests.Metadata
{
    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using RCE.Services.Metadata;

    [TestClass]
    public class MetadataLocatorFixture
    {
        private IMetadataStrategy[] metadataStrategies;

        private MockMetadataStrategy metadataStrategy1;

        private MockMetadataStrategy metadataStrategy2;

        [TestInitialize]
        public void SetUp()
        {
            this.metadataStrategy1 = new MockMetadataStrategy();
            this.metadataStrategy2 = new MockMetadataStrategy();

            this.metadataStrategies = new IMetadataStrategy[] { this.metadataStrategy1, this.metadataStrategy2 };
        }

        [TestMethod]
        public void ShouldCallToCanRetrieveMetadataOnStrategiesWhenTryingToGetFileMetadata()
        {
            string file = "file.wmv";

            var metadataLocator = this.CreateMetadataLocator();

            Assert.IsFalse(this.metadataStrategy1.CanRetrieveMetadataCalled);
            Assert.IsFalse(this.metadataStrategy2.CanRetrieveMetadataCalled);
            Assert.IsNull(this.metadataStrategy1.CanRetrieveMetadataArgument);
            Assert.IsNull(this.metadataStrategy2.CanRetrieveMetadataArgument);

            metadataLocator.GetMetadata(file);

            Assert.IsTrue(this.metadataStrategy1.CanRetrieveMetadataCalled);
            Assert.IsTrue(this.metadataStrategy2.CanRetrieveMetadataCalled);
            Assert.AreEqual(file, this.metadataStrategy1.CanRetrieveMetadataArgument);
            Assert.AreEqual(file, this.metadataStrategy2.CanRetrieveMetadataArgument);
        }

        [TestMethod]
        public void ShouldNotCallToGetMetadataOnStrategiesWhenTryingToGetFileMetadataIfStrategiesCantRetrieveFileMetadata()
        {
            string file = "file.wmv";

            var metadataLocator = this.CreateMetadataLocator();

            this.metadataStrategy1.CanRetrieveMetadataFunction = (object target) => false;
            this.metadataStrategy2.CanRetrieveMetadataFunction = (object target) => false;

            Assert.IsFalse(this.metadataStrategy1.GetMetadataCalled);
            Assert.IsFalse(this.metadataStrategy2.GetMetadataCalled);

            metadataLocator.GetMetadata(file);

            Assert.IsFalse(this.metadataStrategy1.GetMetadataCalled);
            Assert.IsFalse(this.metadataStrategy2.GetMetadataCalled);
        }

        [TestMethod]
        public void ShouldCallToGetMetadataOnStrategyWhenTryingToGetFileMetadataIfStrategiesCanRetrieveFileMetadata()
        {
            string file = "file.wmv";

            var expectedMetadata = new MockMetadata();

            var metadataLocator = this.CreateMetadataLocator();

            this.metadataStrategy1.CanRetrieveMetadataFunction = (object target) => false;
            this.metadataStrategy2.CanRetrieveMetadataFunction = (object target) => true;
            this.metadataStrategy2.GetMetadataFunction = (object target) => expectedMetadata;

            Assert.IsFalse(this.metadataStrategy1.GetMetadataCalled);
            Assert.IsFalse(this.metadataStrategy2.GetMetadataCalled);

            var metadata = metadataLocator.GetMetadata(file);

            Assert.IsFalse(this.metadataStrategy1.GetMetadataCalled);
            Assert.IsTrue(this.metadataStrategy2.GetMetadataCalled);
            Assert.AreEqual(file, this.metadataStrategy2.CanRetrieveMetadataArgument);
            Assert.AreEqual(expectedMetadata, metadata);
        }

        [TestMethod]
        public void ShouldNotCallToCanRetrieveMetadataOnStrategy2WhenTryingToGetFileMetadataAndStrategy1CanRetrieveTheMetadata()
        {
            string file = "file.wmv";

            var metadataLocator = this.CreateMetadataLocator();

            this.metadataStrategy1.CanRetrieveMetadataFunction = (object target) => true;

            Assert.IsFalse(this.metadataStrategy1.CanRetrieveMetadataCalled);
            Assert.IsFalse(this.metadataStrategy2.CanRetrieveMetadataCalled);

            metadataLocator.GetMetadata(file);

            Assert.IsTrue(this.metadataStrategy1.CanRetrieveMetadataCalled);
            Assert.IsFalse(this.metadataStrategy2.CanRetrieveMetadataCalled);
        }

        private IMetadataLocator CreateMetadataLocator()
        {
            return new MetadataLocator(this.metadataStrategies);
        }
    }
}
