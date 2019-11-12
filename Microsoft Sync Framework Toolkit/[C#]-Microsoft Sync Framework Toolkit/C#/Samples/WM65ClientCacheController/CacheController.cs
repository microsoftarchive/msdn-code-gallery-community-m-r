// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    /// <summary>
    /// Class used for synchronizing an offline cache with a remote sync service.
    /// </summary>
    public class CacheController
    {
        OfflineSyncProvider _localProvider;
        Uri _serviceUri;
        CacheControllerBehavior _controllerBehavior;

        CacheRequestHandler _cacheRequestHandler;

        object _lockObject = new object(); // Object used for locking access to the cancelled flag

        public CacheController(Uri serviceUri, string scopeName, OfflineSyncProvider localProvider)
        {
            if (serviceUri == null)
            {
                throw new ArgumentNullException("serviceUri");
            }

            if (string.IsNullOrEmpty(scopeName))
            {
                throw new ArgumentNullException("scopeName");
            }

            if (!serviceUri.Scheme.Equals("http", StringComparison.InvariantCultureIgnoreCase) &&
                !serviceUri.Scheme.Equals("https", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("Uri must be http or https schema", "serviceUri");
            }

            if (localProvider == null)
            {
                throw new ArgumentNullException("localProvider");
            }

            this._serviceUri = serviceUri;
            this._localProvider = localProvider;

            this._controllerBehavior = new CacheControllerBehavior();
            this._controllerBehavior.ScopeName = scopeName;
        }

        /// <summary>
        /// Returns the local provider used by the CacheController.
        /// </summary>
        public OfflineSyncProvider LocalProvider
        {
            get { return this._localProvider; }
        }

        /// <summary>
        /// Returns the Uri of the remote Sync Service.
        /// </summary>
        public Uri ServiceUri
        {
            get { return this._serviceUri; }
        }

        /// <summary>
        /// Returns the reference to the CacheControllerBehavior object that can be used to 
        /// customize the CacheController's settings.
        /// </summary>
        public CacheControllerBehavior ControllerBehavior
        {
            get { return this._controllerBehavior; }
        }

        /// <summary>
        /// Method that refreshes the Cache by uploading all modified changes and then downloading the
        /// server changes.
        /// </summary>
        /// <returns>A CacheRefreshStatistics object denoting the statistics from the Refresh call</returns>
        public CacheRefreshStatistics Refresh()
        {
            this._controllerBehavior.Locked = true;
            try
            {
                // First create the CacheRequestHandler
                this._cacheRequestHandler = CacheRequestHandler.CreateRequestHandler(this._serviceUri, this._controllerBehavior);

                CacheRefreshStatistics refreshStats = new CacheRefreshStatistics();
                refreshStats.StartTime = DateTime.Now;

                bool uploadComplete = false;
                bool downloadComplete = false;

                // Start sync by executin an Upload request
                while (!uploadComplete || !downloadComplete)
                {
                    if (!uploadComplete)
                    {
                        Guid changeSetId = Guid.NewGuid();

                        ChangeSet changeSet = this._localProvider.GetChangeSet(changeSetId);

                        if (changeSet.Data == null || changeSet.Data.Count == 0)
                        {
                            // No data to upload. Skip upload phase.
                            uploadComplete = true;
                        }
                        else
                        {
                            // Create a SyncRequest out of this.
                            CacheRequest request = new CacheRequest()
                            {
                                RequestId = changeSetId,
                                RequestType = CacheRequestType.UploadChanges,
                                Changes = changeSet.Data,
                                KnowledgeBlob = changeSet.ServerBlob,
                                IsLastBatch = changeSet.IsLastBatch
                            };

                            // Increment the stats
                            refreshStats.TotalChangeSetsUploaded++;

                            ChangeSetResponse response = (ChangeSetResponse)this._cacheRequestHandler.ProcessCacheRequest(request);

                            // Increment the stats
                            refreshStats.TotalUploads += (uint)request.Changes.Count;
                            response.ConflictsInternal.ForEach((e1) =>
                            {
                                if (e1 is SyncConflict)
                                {
                                    refreshStats.TotalSyncConflicts++;
                                }
                                else
                                {
                                    refreshStats.TotalSyncErrors++;
                                }
                            });

                            // Send the response to the local provider
                            this._localProvider.OnChangeSetUploaded(changeSetId, response);

                            uploadComplete = request.IsLastBatch;
                        }
                    }
                    else if (!downloadComplete)
                    {
                        // Create a SyncRequest for download.
                        CacheRequest request = new CacheRequest()
                        {
                            RequestType = CacheRequestType.DownloadChanges,
                            KnowledgeBlob = this.LocalProvider.GetServerBlob()
                        };

                        ChangeSet changeSet = (ChangeSet)this._cacheRequestHandler.ProcessCacheRequest(request);

                        // Increment the refresh stats
                        refreshStats.TotalChangeSetsDownloaded++;
                        refreshStats.TotalDownloads += (uint)changeSet.Data.Count;

                        // Call the SaveChangeSet method on local provider.
                        this.LocalProvider.SaveChangeSet(changeSet);

                        downloadComplete = changeSet.IsLastBatch;
                    }
                }

                refreshStats.EndTime = DateTime.Now;
                // Finally return the statistics object
                return refreshStats;

            }
            finally
            {
                // Unlock the ControllerBehavior object
                this._controllerBehavior.Locked = false;
            }
        }
    }
}

