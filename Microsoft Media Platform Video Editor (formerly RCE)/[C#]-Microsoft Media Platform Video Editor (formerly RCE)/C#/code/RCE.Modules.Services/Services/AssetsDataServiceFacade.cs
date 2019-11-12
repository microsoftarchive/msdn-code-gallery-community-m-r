// <copyright file="AssetsDataServiceFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetsDataServiceFacade.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Browser;
    using Infrastructure.Translators;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Modules.Services.AssetsDataService;

    /// <summary>
    /// Interacts with server to get the assets data.
    /// </summary>
    public class AssetsDataServiceFacade : IAssetsDataServiceFacade
    {
        /// <summary>
        /// The <see cref="ILogger"/> to log the exceptions.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The service address.
        /// </summary>
        private readonly Uri serviceAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsDataServiceFacade"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationService">The configuration service.</param>
        public AssetsDataServiceFacade(ILogger logger, IConfigurationService configurationService)
        {
            this.logger = logger;

            string serviceUriString = configurationService.GetParameterValue("AssetsDataServiceUrl");

            this.serviceAddress = new Uri(serviceUriString, UriKind.Absolute);
        }

        /// <summary>
        /// Occurs when [load assets completed].
        /// </summary>
        public event EventHandler<DataEventArgs<List<Asset>>> LoadAssetsCompleted;

        public event EventHandler<DataEventArgs<List<Asset>>> LoadAssetsByLibraryIdCompleted;

        /// <summary>
        /// Loads the assets asynchronously.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="maxNumberOfItems">The max number of items.</param>
        public void LoadAssetsAsync(string filter, int maxNumberOfItems)
        {
            AssetsDataServiceClient client = this.CreateAssetsDataServiceClient();

            client.LoadLibraryFilterCompleted += (sender, args) =>
            {
                if (args.Error != null)
                {
                    client.Abort();
                    this.logger.Log(this.GetType().Name, args.Error);

                    if (args.Error.GetType() == typeof(Exception))
                    {
                        throw args.Error;
                    }

                    return;
                }

                try
                {
                    List<Asset> assets = DataServiceTranslator.ConvertToAssets(args.Result);
                    this.OnLoadAssetsCompleted(new DataEventArgs<List<Asset>>(assets));
                }
                catch (Exception e)
                {
                    client.Abort();
                    this.logger.Log(this.GetType().Name, e);
                    throw;
                }
            };

            string projectId = null;
            if (HtmlPage.Document.QueryString.TryGetValue("projectId", out projectId))
            {
                var highlightId = Guid.Parse(projectId);

                filter = string.Format("{0}HighlightId={1}", filter, highlightId);
            }
            else
            {
                maxNumberOfItems = 0;
            }

            client.LoadLibraryFilterAsync(filter, maxNumberOfItems);
        }

        /// <summary>
        /// Loads the assets asynchronously.
        /// </summary>
        /// <param name="maxNumberOfItems">The max number of items in the result.</param>
        public void LoadAssetsAsync(int maxNumberOfItems)
        {
            AssetsDataServiceClient client = this.CreateAssetsDataServiceClient();

            client.LoadLibraryWithLimitedItemsCompleted += (sender, args) =>
            {
                if (args.Error != null)
                {
                    client.Abort();
                    this.logger.Log(this.GetType().Name, args.Error);

                    if (args.Error.GetType() == typeof(Exception))
                    {
                        throw args.Error;
                    }

                    return;
                }

                try
                {
                    List<Asset> assets = DataServiceTranslator.ConvertToAssets(args.Result);
                    this.OnLoadAssetsCompleted(new DataEventArgs<List<Asset>>(assets));
                }
                catch (Exception e)
                {
                    client.Abort();
                    this.logger.Log(this.GetType().Name, e);
                    throw;
                }
            };

            client.LoadLibraryWithLimitedItemsAsync(maxNumberOfItems);
        }

        /// <summary>
        /// Loads the assets of a library asynchronously.
        /// </summary>
        /// <param name="libraryId">The library Id being loaded.</param>
        /// <param name="maxNumberOfItems">
        /// The max number of items in the result.
        /// </param>
        public void LoadAssetsByLibraryIdAsync(Uri libraryId, int maxNumberOfItems)
        {
            AssetsDataServiceClient client = this.CreateAssetsDataServiceClient();

            client.LoadLibraryByIdCompleted += (sender, args) =>
            {
                if (args.Error != null)
                {
                    client.Abort();
                    this.logger.Log(this.GetType().Name, args.Error);

                    if (args.Error.GetType() == typeof(Exception))
                    {
                        throw args.Error;
                    }

                    return;
                }

                try
                {
                    List<Asset> assets = DataServiceTranslator.ConvertToAssets(args.Result);
                    this.OnLoadAssetsByLibraryIdCompleted(new DataEventArgs<List<Asset>>(assets));
                }
                catch (Exception e)
                {
                    client.Abort();
                    this.logger.Log(this.GetType().Name, e);
                    throw;
                }
            };

            client.LoadLibraryByIdAsync(libraryId, maxNumberOfItems);
        }

        /// <summary>
        /// Raises the <see cref="LoadAssetsCompleted"/> event.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        private void OnLoadAssetsCompleted(DataEventArgs<List<Asset>> e)
        {
            EventHandler<DataEventArgs<List<Asset>>> loadAssetsCompletedHandler = this.LoadAssetsCompleted;
            if (loadAssetsCompletedHandler != null)
            {
                loadAssetsCompletedHandler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="LoadAssetsByLibraryIdCompleted"/> event.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        private void OnLoadAssetsByLibraryIdCompleted(DataEventArgs<List<Asset>> e)
        {
            EventHandler<DataEventArgs<List<Asset>>> loadAssetsByLibraryIdCompletedHandler = this.LoadAssetsByLibraryIdCompleted;
            if (loadAssetsByLibraryIdCompletedHandler != null)
            {
                loadAssetsByLibraryIdCompletedHandler(this, e);
            }
        }

        /// <summary>
        /// Creates the dataservice client.
        /// </summary>
        /// <returns>The data service client.</returns>
        private AssetsDataServiceClient CreateAssetsDataServiceClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
            {
                Name = "AssetsDataServiceBinding",
                MaxReceivedMessageSize = 2147483647,
                MaxBufferSize = 2147483647,
            };

            EndpointAddress endpointAddress = new EndpointAddress(this.serviceAddress);

            return new AssetsDataServiceClient(binding, endpointAddress);
        }
    }
}
