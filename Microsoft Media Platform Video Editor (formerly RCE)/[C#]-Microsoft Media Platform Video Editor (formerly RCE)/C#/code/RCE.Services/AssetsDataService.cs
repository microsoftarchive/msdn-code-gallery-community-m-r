// <copyright file="AssetsDataService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetsDataService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services
{
    using System;
    using System.ServiceModel.Activation;
    using LAgger;
    using RCE.Services.Contracts;

    /// <summary>
    /// Provides the implementation for <see cref="IAssetsDataService"/> that will connect to the registered data provider to load the data.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AssetsDataService : IAssetsDataService
    {
        /// <summary>
        /// The <see cref="ILoggerService"/>.
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsDataService"/> class with the specified data provider.
        /// </summary>
        /// <param name="provider">The assets data provider.</param>
        /// <param name="loggerService">The logger.</param>
        public AssetsDataService(IAssetsDataProvider provider, ILoggerService loggerService)
        {
            this.Provider = provider;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Gets the <see cref="IAssetsDataProvider"/> that is used for the implementation.
        /// </summary>
        /// <value>The data provider to retrieve data.</value>
        public IAssetsDataProvider Provider { get; private set; }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibrary(int maxNumberOfItems)
        {
            return this.LoadLibrary(null, maxNumberOfItems);
        }

        /// <summary>
        /// Returns back all of the items that are contained in the library filtering them using the filter provided.
        /// </summary>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public virtual Container LoadLibrary(string filter, int maxNumberOfItems)
        {
            try
            {
                if (string.IsNullOrEmpty(filter))
                {
                    return this.Provider.LoadLibrary(maxNumberOfItems);
                }

                return this.Provider.LoadLibrary(filter, maxNumberOfItems);
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public virtual Container LoadLibraryById(Uri libraryId, int maxNumberOfItems)
        {
            try
            {
                return this.Provider.LoadLibraryById(libraryId, maxNumberOfItems);
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public virtual Container LoadLibraryById(Uri libraryId, string filter, int maxNumberOfItems)
        {
            try
            {
                return this.Provider.LoadLibraryById(libraryId, filter, maxNumberOfItems);
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="ex">The Exception.</param>
        private void Log(Exception ex)
        {
            string message = UtilityHelper.FormatExceptionMessage(ex);

            LogEntry entry = new LogEntry(message, "Assets Data Service", 1, 0, TraceEventType.Error, "Error in Assets Data Service");

            this.loggerService.LogEntries(new[] { entry });
        }
    }
}
