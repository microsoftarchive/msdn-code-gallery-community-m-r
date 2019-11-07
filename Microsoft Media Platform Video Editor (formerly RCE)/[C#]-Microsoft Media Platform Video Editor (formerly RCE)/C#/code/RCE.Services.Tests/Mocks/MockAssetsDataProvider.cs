// <copyright file="MockAssetsDataProvider.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockDataProvider.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Tests.Mocks
{
    using System;
    using Contracts;

    public class MockAssetsDataProvider : IAssetsDataProvider
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not an exception should be thrown;
        /// </summary>
        public bool ThrowException { get; set; }

        public Container LoadLibraryResult { get; set; }

        public Container LoadLibraryByIdResult { get; set; }

        public bool LoadLibraryCalled { get; set; }

        public bool LoadLibraryFilterCalled { get; set; }

        public bool LoadLibraryByIdCalled { get; set; }

        public Container LoadLibrary(int maxNumberOfItems)
        {
            this.LoadLibraryCalled = true;

            if (this.ThrowException)
            {
                throw new ApplicationException("I was told to throw an exception, so I am.");
            }

            return this.LoadLibraryResult;
        }

        public Container LoadLibrary(string filter, int maxNumberOfItems)
        {
            this.LoadLibraryFilterCalled = true;

            if (this.ThrowException)
            {
                throw new ApplicationException("I was told to throw an exception, so I am.");
            }

            return this.LoadLibraryResult;
        }

        public Container LoadLibraryById(Uri libraryId, int maxNumberOfItems)
        {
            this.LoadLibraryByIdCalled = true;

            if (this.ThrowException)
            {
                throw new ApplicationException("I was told to throw an exception, so I am.");
            }

            return this.LoadLibraryByIdResult;
        }

        public Container LoadLibraryById(Uri libraryId, string filter, int maxNumberOfItems)
        {
            throw new NotImplementedException();
        }
    }
}
