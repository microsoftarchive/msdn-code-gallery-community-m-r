// <copyright file="AssetsDataServiceFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetsDataServiceFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Tests
{
    using System;
    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    [TestClass]
    public class AssetsDataServiceFixture
    {
        /// <summary>
        /// Mock for <see cref="IAssetsDataProvider"/>.
        /// </summary>
        private MockAssetsDataProvider dataProvider;

        /// <summary>
        /// Mock for ILoggerService.
        /// </summary>
        private MockLoggerService loggerService;

        /// <summary>
        /// Initilize the data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.dataProvider = new MockAssetsDataProvider();
            this.loggerService = new MockLoggerService();
        }

        /// <summary>
        /// Should call to LoadLibrary with max number of items.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadLibraryWithMaxNumberOfItems()
        {
            var dataService = this.CreateAssetsDataService();

            Assert.IsFalse(this.dataProvider.LoadLibraryCalled);

            dataService.LoadLibrary(20);

            Assert.IsTrue(this.dataProvider.LoadLibraryCalled);
        }

        /// <summary>
        /// Should call to LoadLibrary with given filter.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadLibraryFilter()
        {
            var dataService = this.CreateAssetsDataService();

            Assert.IsFalse(this.dataProvider.LoadLibraryFilterCalled);

            dataService.LoadLibrary("Test", 0);

            Assert.IsTrue(this.dataProvider.LoadLibraryFilterCalled);
        }

        /// <summary>
        /// Should call to LoadLibrary when filter is null.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadLibraryWhenFilterIsNull()
        {
            var dataService = this.CreateAssetsDataService();

            Assert.IsFalse(this.dataProvider.LoadLibraryCalled);

            dataService.LoadLibrary(null, 0);

            Assert.IsTrue(this.dataProvider.LoadLibraryCalled);
        }

        /// <summary>
        /// Should call to LoadLibrary when filter is empty.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadLibraryWhenFilterIsEmpty()
        {
            var dataService = this.CreateAssetsDataService();

            Assert.IsFalse(this.dataProvider.LoadLibraryCalled);

            dataService.LoadLibrary(string.Empty, 0);

            Assert.IsTrue(this.dataProvider.LoadLibraryCalled);
        }

        /// <summary>
        /// Should call to LoadLibraryById.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadLibraryById()
        {
            var dataService = this.CreateAssetsDataService();

            Assert.IsFalse(this.dataProvider.LoadLibraryByIdCalled);

            dataService.LoadLibraryById(new Uri("http://test"), 0);

            Assert.IsTrue(this.dataProvider.LoadLibraryByIdCalled);
        }

        /// <summary>
        /// Shoulds the return data provider result when loading filtered A library.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenLoadingFilteredALibrary()
        {
            var dataService = this.CreateAssetsDataService();

            this.dataProvider.LoadLibraryResult = null;

            var library = dataService.LoadLibrary("test", 0);

            Assert.IsNull(library);

            this.dataProvider.LoadLibraryResult = new Container();

            library = dataService.LoadLibrary("test", 0);

            Assert.IsNotNull(library);
            Assert.AreEqual(this.dataProvider.LoadLibraryResult, library);
        }

        /// <summary>
        /// Shoulds the return data provider result when loading a library by id.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenLoadingLibraryById()
        {
            var dataService = this.CreateAssetsDataService();

            this.dataProvider.LoadLibraryResult = null;

            var library = dataService.LoadLibraryById(new Uri("http://test"), 0);

            Assert.IsNull(library);

            this.dataProvider.LoadLibraryByIdResult = new Container();

            library = dataService.LoadLibraryById(new Uri("http://test"), 0);

            Assert.IsNotNull(library);
            Assert.AreEqual(this.dataProvider.LoadLibraryByIdResult, library);
        }

        /// <summary>
        /// Should thrown an exception happens when loading library with given filter.
        /// </summary>
        [TestMethod]
        public void ShouldThrowAnExceptionIfAnErrorOcurredWhenLoadingFilteredLibrary()
        {
            bool exceptionThrown = false;

            var dataService = this.CreateAssetsDataService();

            this.dataProvider.ThrowException = true;
            
            try
            {
                var library = dataService.LoadLibrary("test", 0);
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        /// <summary>
        /// Should log exception if an exception happens when loading library
        /// with given filter.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenLoadingFilteredLibrary()
        {
            var dataService = this.CreateAssetsDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.LoadLibrary("test", 0);
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Should log exception if an exception happens when loading library
        /// with given filter.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenLoadingLibraryById()
        {
            var dataService = this.CreateAssetsDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.LoadLibraryById(new Uri("http://test"), 0);
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Should return data provider result when loading library with max items limit.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenLoadingLibraryWithMaxItemsLimit()
        {
            var dataService = this.CreateAssetsDataService();

            this.dataProvider.LoadLibraryResult = null;

            var library = dataService.LoadLibrary(20);

            Assert.IsNull(library);

            this.dataProvider.LoadLibraryResult = new Container();

            library = dataService.LoadLibrary(20);

            Assert.IsNotNull(library);
            Assert.AreEqual(this.dataProvider.LoadLibraryResult, library);
        }

        /// <summary>
        /// Should thrown an exception if an error ocurred when loading library with max items limit.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullIfAnExceptionHappensWhenLoadingLibraryWithMaxItemsLimit()
        {
            bool exceptionThrown = false;

            var dataService = this.CreateAssetsDataService();

            this.dataProvider.ThrowException = true;

            try
            {
                var library = dataService.LoadLibrary(20);
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        /// <summary>
        /// Should log exception if an exception happens when loading library with max items limit.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenLoadingLibraryWithMaxItemsLimit()
        {
            var dataService = this.CreateAssetsDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.LoadLibrary(20);                
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Creates the assets data service.
        /// </summary>
        /// <returns>The <see cref="IDataService"/>.</returns>
        private IAssetsDataService CreateAssetsDataService()
        {
            return new AssetsDataService(this.dataProvider, this.loggerService);
        }
    }
}
