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

using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Threading;

namespace Microsoft.Synchronization.ClientServices
{
    /// <summary>
    /// This is a utility class that lets us manage a queue of chained Async workers. This lets us easily cancel a chain of 
    /// async events. Since we need better control over the type and number of arguments passed for our async work we are
    /// using this instead of using BackgroundWorker. This works off by managing a pseudo session around the chained async events.
    /// One async work might not be complete till a unknown number of chained and related async events complete. 
    /// 
    /// Users of this class deal with AsyncWorkRequest which is an agnostic way of denoting the work that needs to be executed asynchronously and 
    /// callbacks to call when that work is complete. Has provision for providing multiple input parameters.
    /// </summary>
    class AsyncWorkerManager
    {
        // Object used for locking access to the cancelled flag
        object _lockObject = new object();

        volatile bool _cancelRequested = false; // Bool that represents a cancel request
        volatile bool _cancelled = false; // Bool to denote whether to a pending cancel request has been processed or not.
        bool _workersCompleted = false; // Bool to denote that the worker queue manager has completed all jobs.

        bool _sessionEnabled = false; // bool to denote whether or not session is enabled
        bool _sessionCompleted = false; // bool to denote if session was enabled whether it is completed or not.

        Queue<AsyncWorkRequest> _workersQueue;

        Action<object> _cancellationCallback;
        AsyncOperation _cancellationAsyncOp;

        /// <summary>
        /// Flag denoting whether all pending workers are done. Usually set to true when the last AsyncWorkerRequest has been
        /// completed and the session has ended.
        /// </summary>
        bool AllWorkersCompleted
        {
            get
            {
                lock (this._lockObject)
                {
                    return _workersCompleted;
                }
            }

            set
            {
                lock (this._lockObject)
                {
                    _workersCompleted = value;
                }
            }
        }

        /// <summary>
        /// Flag to enqueue a Cancel request
        /// </summary>
        bool CancelRequested
        {
            get
            {
                return _cancelRequested;
            }
            set
            {
                _cancelRequested = value;
            }
        }

        /// <summary>
        /// Flag to denote whether the pending cancel request has been processed or not.
        /// </summary>
        bool Cancelled
        {
            get
            {
                return _cancelled;
            }
            set
            {
                _cancelled = value;
            }
        }

        /// <summary>
        /// Constructor to call when a Queuemanager capable of cancellation is to be instantiated.
        /// </summary>        
        /// <param name="cancellationCallback">The callback to call when the queue manager successfully cancels some pending workers.</param>
        public AsyncWorkerManager(Action<object> cancellationCallback)
        {
            _workersQueue = new Queue<AsyncWorkRequest>();
            this._cancellationCallback = cancellationCallback;
            this.StartChainedAsyncSession();
        }

        /// <summary>
        /// Start the session for the chained set of async work
        /// </summary>
        public void StartChainedAsyncSession()
        {
            if (this._sessionEnabled)
            {
                throw new AsyncWorkManagerException("A chained async session is already in progress.");
            }
            this._sessionEnabled = true;
        }

        /// <summary>
        /// End the session
        /// </summary>
        public void EndChainedAsyncSession()
        {
            if (!this._sessionEnabled)
            {
                throw new AsyncWorkManagerException("No chained async session was found.");
            }
            this._sessionCompleted = true;
        }

        /// <summary>
        /// Adds an AsyncWorkRequest to the queue. 
        /// </summary>
        /// <param name="workRequest"></param>
        public void AddWorkRequest(AsyncWorkRequest workRequest)
        {
            this._workersQueue.Enqueue(workRequest);
            this.ProcessNextWorkRequest();
        }

        /// <summary>
        /// Useful to post intermediate callbacks from an async work
        /// </summary>
        /// <param name="request">The async work for which this intermediate call is being made.</param>
        /// <param name="callback">The user callback to be invoked</param>
        /// <param name="state">User passed state param to the callback</param>
        public void PostProgress(AsyncWorkRequest request, SendOrPostCallback callback, object state)
        {
            if (request.AsyncOperation == null)
            {
                throw new AsyncWorkManagerException("Cannot post progress for a worker WebRequest that hasnt been started yet.");
            }

            request.AsyncOperation.Post(callback, state);
        }

        /// <summary>
        /// Called when the associated AsyncWorkRequest is to be completed.
        /// </summary>
        /// <param name="workRequest">The AsyncWorkRequest that is to be completed.</param>
        /// <param name="completionArguments">Completion arguments</param>
        public void CompleteWorkRequest(AsyncWorkRequest workRequest, object completionArguments)
        {
            if (workRequest.AsyncOperation == null)
            {
                throw new AsyncWorkManagerException("Cannot complete a worker WebRequest that hasnt been started yet.");
            }

            // Post the completion callback
            workRequest.AsyncOperation.PostOperationCompleted(new SendOrPostCallback(workRequest.CompletionCallback), completionArguments);

            // Process the next request
            this.ProcessNextWorkRequest();
        }

        /// <summary>
        /// Cancel the async queue after the current execuing async work completes.
        /// </summary>
        public void CancelPendingWorkers()
        {
            this.CancelRequested = true;

            // Check to see if user wants to be notified of cancellation. If yes then queue an event.
            if (this._cancellationCallback != null)
            {
                lock (this._lockObject)
                {
                    if (this._cancellationCallback != null)
                    {
                        //Capture the AsyncOperation in the current users SynchronizationContext which can be used to fire later.
                        _cancellationAsyncOp = AsyncOperationManager.CreateOperation(null);
                    }
                }
            }
        }

        /// <summary>
        /// Called to process the next queued work item.
        /// </summary>
        public void ProcessNextWorkRequest()
        {
            if (this.AllWorkersCompleted)
            {
                throw new AsyncWorkManagerException("Cannot restart an already completed queue manager.");
            }

            if (this.CancelRequested)
            {
                // Check to see that the cancelled request has not been processed
                if (!this.Cancelled)
                {
                    lock (this._lockObject)
                    {
                        if (!this.Cancelled)
                        {
                            CheckAndSendCancellationNotice();
                        }
                    }
                }
            }
            else if (this._workersQueue.Count > 0)
            {
                AsyncWorkRequest workRequest = this._workersQueue.Dequeue();

                AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(workRequest.Id);
                workRequest.SetAsyncOperation(asyncOp);

                ThreadPool.QueueUserWorkItem(new WaitCallback(
                    o =>
                    {
                        asyncOp.SynchronizationContext.Post(new SendOrPostCallback(LaunchWorkerCallback), workRequest);
                    }));
            }
            else
            {
                // No workers were found. End the session if no session is established or if a session has been
                // established and has completed.
                if (!this._sessionEnabled || (this._sessionEnabled && this._sessionCompleted))
                {
                    this.EndSession();
                }
            }
        }

        /// <summary>
        /// This one checks to see if a Cancellation notice has been already sent or not and if it hasnt been sent 
        /// then the fires the cancellation event.
        /// </summary>
        internal void CheckAndSendCancellationNotice()
        {
            if (this._cancellationAsyncOp != null)
            {
                this.Cancelled = true;
                _cancellationAsyncOp.PostOperationCompleted(new SendOrPostCallback(this._cancellationCallback), true);
            }

            this.EndSession();
        }

        /// <summary>
        /// This method will be called on a background thread and will be called within the correct synchronization context.
        /// Now invoke the Asyc workers work callback on this thread.
        /// </summary>
        /// <param name="state"></param>
        void LaunchWorkerCallback(object state)
        {
            AsyncWorkRequest workRequest = state as AsyncWorkRequest;
            workRequest.WorkerCallback(workRequest, workRequest.InputParameters);
        }

        /// <summary>
        /// Calls to end the session
        /// </summary>
        void EndSession()
        {
            this.AllWorkersCompleted = true;
            this._sessionEnabled = false;
            this._cancellationAsyncOp = null;
        }
    }
}
