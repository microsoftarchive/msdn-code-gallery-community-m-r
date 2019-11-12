// Copyright 2010 Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License"); 
// You may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, 
// INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
// CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, 
// MERCHANTABLITY OR NON-INFRINGEMENT. 

// See the Apache 2 License for the specific language governing 
// permissions and limitations under the License.

using System;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace Microsoft.Synchronization.ClientServices
{
    /// <summary>
    /// Class used for synchronizing an offline cache with a remote sync service.
    /// </summary>
    public class CacheController
    {
        OfflineSyncProvider _localProvider;
        Uri _serviceUri;
        CacheControllerBehavior _controllerBehavior;

        AsyncWorkRequest _refreshRequestWorker;
        AsyncWorkerManager _asyncWorkManager;
        CacheRequestHandler _cacheRequestHandler;

        Guid changeSetId;
        CacheRefreshStatistics refreshStats;
        object _lockObject = new object(); // Object used for locking access to the cancelled flag
        bool _cancelled = false;
        bool _beginSessionComplete = false;

        bool Cancelled
        {
            get
            {
                lock (_lockObject)
                {
                    return _cancelled;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _cancelled = value;
                }
            }
        }

        /// <summary>
        /// Constructor for CacheController
        /// </summary>
        /// <param name="serviceUri">Remote sync service Uri with a trailing "/" parameter.</param>
        /// <param name="scopeName">The scope name being synchronized</param>
        /// <param name="localProvider">The OfflineSyncProvider instance for the local store.</param>
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
        /// Bool property indicating if the CacheController is in middle of a Sync operation
        /// </summary>
        public bool IsBusy
        {
            get
            {
                lock (_lockObject)
                {
                    return this._asyncWorkManager != null;
                }
            }
        }

        /// <summary>
        /// Method that refreshes the Cache by uploading all modified changes and then downloading the
        /// server changes.
        /// </summary>
        public void RefreshAsync()
        {
            if (this.IsBusy)
            {
                throw CacheControllerException.CreateCacheBusyException();
            }

            this.refreshStats = new CacheRefreshStatistics();

            this.StartAsyncWorkerManager();

            // Enqueue an async operation
            this._asyncWorkManager.AddWorkRequest(new AsyncWorkRequest(RefreshWorker, RefreshWorkerCompleted, null));
        }

        /// <summary>
        /// Cancels an async operation if the CacheController is in middle of synchronization. This method does nothing if
        /// there is no active Refresh in progress.
        /// </summary>
        public void CancelAsync()
        {
            // Set the local cancelled flag
            this.Cancelled = true;

            if (this._asyncWorkManager != null)
            {
                // Tell the async worker manager to cancel all pending workers
                this._asyncWorkManager.CancelPendingWorkers();
            }
        }

        /// <summary>
        /// EventHandler to register Refresh completed callbacks 
        /// </summary>
        public event EventHandler<RefreshCompletedEventArgs> RefreshCompleted;

        /// <summary>
        /// Instantiates a new AsyncWorkerManager object that will be used for the current sync session
        /// </summary>
        void StartAsyncWorkerManager()
        {
            if (this._asyncWorkManager == null)
            {
                lock (this._lockObject)
                {
                    if (this._asyncWorkManager == null)
                    {
                        this._asyncWorkManager = new AsyncWorkerManager(OnRefreshCancelled);
                        this._controllerBehavior.Locked = true;
                        // Reset Cancelled flag
                        this.Cancelled = false;
                    }
                    else
                    {
                        // This means two different threads raced and called RefreshAsync which wasnt caught by the IsBusy check
                        throw CacheControllerException.CreateCacheBusyException();
                    }
                }
            }
        }

        // Reset the state of the objects
        void ResetAsyncWorkerManager()
        {
            lock (this._lockObject)
            {
                this._asyncWorkManager = null;
                this._refreshRequestWorker = null;
                this._cacheRequestHandler = null;
                this._controllerBehavior.Locked = false;
                this._beginSessionComplete = false;
            }
        }

        /// <summary>
        /// Called when a new RefreshAsync is invoked by the user
        /// </summary>
        /// <param name="asyncWorker"></param>
        /// <param name="inputParams">An array of input params that can be passed to this method. Null in this case</param>
        void RefreshWorker(AsyncWorkRequest asyncWorker, object[] inputParams)
        {
            this._refreshRequestWorker = asyncWorker;

            refreshStats.StartTime = DateTime.Now;
            try
            {
                // First create the CacheRequestHandler
                this._cacheRequestHandler = CacheRequestHandler.CreateRequestHandler(this._serviceUri, this._controllerBehavior, this._asyncWorkManager);

                // Register for the ProcessRequestAsync callback of the CacheRequestHandler
                this._cacheRequestHandler.ProcessCacheRequestCompleted += new EventHandler<ProcessCacheRequestCompletedEventArgs>(ProcessCacheRequestCompleted);

                // Then fire the BeginSession call on the local provider.
                this._localProvider.BeginSession();

                // Set the flag to indicate BeginSession was successful
                this._beginSessionComplete = true;

                // Dont enqueue another request if its been cancelled
                if (this.Cancelled)
                {
                    this._asyncWorkManager.CheckAndSendCancellationNotice();
                    return;
                }

                // Do uploads first
                this.EnqueueUploadRequest();
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }
                CompleteAsyncWithException(e);
            }
        }

        /// <summary>
        /// Called whenever the CacheRequestHandler proceeses an upload/download request. It is also responsible for
        /// issuing another request if it wasnt the last batch. In case of receiving an Upload response it calls the
        /// underlying provider with the status of the upload. In case of Download it notifies the local provider of the
        /// changes that it needs to save.
        /// </summary>
        /// <param name="sender">Object invoking this method. Usually its the CacheRequestHandler</param>
        /// <param name="e">The result of processing the CacheRequest</param> 
        void ProcessCacheRequestCompleted(object sender, ProcessCacheRequestCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    // Check to see if it was a UploadRequest in which case we will have to call OnChangeSetUploaded
                    // with error to reset the dirty bits.
                    if (e.ChangeSetResponse != null)
                    {
                        // its an response to a upload
                        this._localProvider.OnChangeSetUploaded(e.Id, e.ChangeSetResponse);
                    }

                    // Finally complete Refresh with error.
                    CompleteAsyncWithException(e.Error);
                }
                else if (e.ChangeSetResponse != null)
                {
                    // its an response to a upload
                    this._localProvider.OnChangeSetUploaded(e.Id, e.ChangeSetResponse);

                    if (e.ChangeSetResponse.Error != null)
                    {
                        CompleteAsyncWithException(e.ChangeSetResponse.Error);
                        return;
                    }

                    // Increment the ChangeSets uploaded count
                    refreshStats.TotalChangeSetsUploaded++;
                    refreshStats.TotalUploads += (uint)e.BatchUploadCount;

                    // Update refresh stats
                    e.ChangeSetResponse.ConflictsInternal.ForEach((e1) =>
                    {
                        if (e1 is SyncConflict)
                        {
                            this.refreshStats.TotalSyncConflicts++;
                        }
                        else
                        {
                            this.refreshStats.TotalSyncErrors++;
                        }
                    });

                    // Dont enqueue another request if its been cancelled
                    if (!this.Cancelled)
                    {
                        if (!((bool)e.State))
                        {
                            // Check to see if this was the last batch or else enqueue another pending Upload request
                            this.EnqueueUploadRequest();
                        }
                        else
                        {
                            // That was the last batch. Issue an Download request
                            this.EnqueueDownloadRequest();
                        }
                    }
                    else
                    {
                        // This will process the queued Cancellation request
                        this._asyncWorkManager.CheckAndSendCancellationNotice();
                    }

                }
                else // It means its an Download response
                {
                    Debug.Assert(e.ChangeSet != null, "Completion is not for a download request.");

                    // Increment the refresh stats
                    this.refreshStats.TotalChangeSetsDownloaded++;
                    this.refreshStats.TotalDownloads += (uint)e.ChangeSet.Data.Count;

                    this.LocalProvider.SaveChangeSet(e.ChangeSet);

                    // Dont enqueue another request if its been cancelled
                    if (!this.Cancelled)
                    {
                        if (!e.ChangeSet.IsLastBatch)
                        {
                            // Enqueue the next download
                            this.EnqueueDownloadRequest();
                        }
                        else
                        {
                            // Uploads and downloads are done. Mark the session as complete
                            this._asyncWorkManager.EndChainedAsyncSession();
                            this._asyncWorkManager.CompleteWorkRequest(this._refreshRequestWorker, null);
                        }
                    }
                    else
                    {
                        // This will process the queued Cancellation request
                        this._asyncWorkManager.CheckAndSendCancellationNotice();
                    }
                }
            }
            catch (Exception exp)
            {
                if (ExceptionUtility.IsFatal(exp))
                {
                    throw;
                }
                // Calling in to user code here (OnChangeSetUploaded and SaveChangeSet). Catch exceptions and fail
                CompleteAsyncWithException(exp);
            }
        }

        /// <summary>
        /// Called when the Refresh operation is completed. This is guaranteed to be called in the right user SynchronizationContext
        /// and thread context
        /// </summary>
        /// <param name="error">Denotes whether or not we completed with an exception.</param>
        void RefreshWorkerCompleted(object error)
        {
            try
            {
                refreshStats.EndTime = DateTime.Now;

                if (this._beginSessionComplete)
                {
                    // Call EndSession only if BeginSession was successful.
                    this.LocalProvider.EndSession();
                }
            }
            finally
            {
                // Reset the state
                this.ResetAsyncWorkerManager();

                FireRefreshCompletedEvent(error as Exception, false /*wasCancelled*/);
            }
        }

        private void FireRefreshCompletedEvent(Exception error, bool wasCancelled)
        {
            // RefreshCompleted session is calling user code which can throw. So guard it
            if (this.RefreshCompleted != null)
            {
                // Fire the event
                this.RefreshCompleted(this, new RefreshCompletedEventArgs(refreshStats, error, wasCancelled));
            }
        }

        /// <summary>
        /// Called when the AsyncWorkerManager successfully completes a Cancel WebRequest. This will also be called 
        /// withing the correct SynchronizationContext and threading context.
        /// </summary>
        void OnRefreshCancelled(object state)
        {
            try
            {
                try
                {
                    refreshStats.EndTime = DateTime.Now;

                    // Call EndSession.
                    this.LocalProvider.EndSession();
                }
                finally
                {
                    // The state object (a bool) indicates whether or not the cancellation was successful
                    // If it was successful then fire the RefreshCompletedEvent with Cancelled set to true.
                    // If its false it means cancellation failed i.e the sync request completed and hence no 
                    // need to fire the RefreshCompletedEvent as it must have already been fired.
                    if ((bool)state)
                    {
                        FireRefreshCompletedEvent(null /*error*/, true/*wasCancelled*/);
                    }
                }
            }
            finally
            {
                // Reset the state
                this.ResetAsyncWorkerManager();
            }
        }

        /// <summary>
        /// Method that performs an upload. It gets the ChangeSet from the local provider and then creates an
        /// CacheRequest object for that ChangeSet and then passed the processing asynchronously to the underlying
        /// CacheRequestHandler.
        /// </summary>
        void EnqueueUploadRequest()
        {
            this.changeSetId = Guid.NewGuid();

            try
            {
                ChangeSet changeSet = this._localProvider.GetChangeSet(this.changeSetId);

                if (changeSet == null || changeSet.Data == null || changeSet.Data.Count == 0)
                {
                    // No data to upload. Skip upload phase.
                    this.EnqueueDownloadRequest();
                }
                else
                {
                    // Create a SyncRequest out of this.
                    CacheRequest request = new CacheRequest()
                    {
                        RequestId = this.changeSetId,
                        Format = this.ControllerBehavior.SerializationFormat,
                        RequestType = CacheRequestType.UploadChanges,
                        Changes = changeSet.Data,
                        KnowledgeBlob = changeSet.ServerBlob,
                        IsLastBatch = changeSet.IsLastBatch
                    };

                    this._cacheRequestHandler.ProcessCacheRequestAsync(request, changeSet.IsLastBatch /*state is the IsLastBatch flag*/);
                }
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }
                // Error. EndSession refresh and post callback
                CompleteAsyncWithException(e);
            }
        }

        /// <summary>
        /// Method that performs a download. It gets the server blob anchor from the local provider and then creates an 
        /// CacheRequest object for that download request. It then passes the processing asynchronously to the underlying
        /// CacheRequestHandler.
        /// </summary>
        void EnqueueDownloadRequest()
        {
            try
            {
                // Create a SyncRequest for download.
                CacheRequest request = new CacheRequest()
                {
                    Format = this.ControllerBehavior.SerializationFormat,
                    RequestType = CacheRequestType.DownloadChanges,
                    KnowledgeBlob = this.LocalProvider.GetServerBlob()
                };

                this._cacheRequestHandler.ProcessCacheRequestAsync(request, null /*state is null*/);
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }
                // Error. EndSession refresh and post callback
                CompleteAsyncWithException(e);
            }
        }

        /// <summary>
        /// Complete the uber RefreshAsync request with an optional error if passed.
        /// </summary>
        /// <param name="e">Exception object to report to the user.</param>
        void CompleteAsyncWithException(Exception e)
        {
            this._asyncWorkManager.EndChainedAsyncSession();
            this._asyncWorkManager.CompleteWorkRequest(this._refreshRequestWorker, e);
        }
    }
}
