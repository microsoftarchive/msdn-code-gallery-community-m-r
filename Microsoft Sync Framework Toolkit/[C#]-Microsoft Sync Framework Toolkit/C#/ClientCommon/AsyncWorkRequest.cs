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

namespace Microsoft.Synchronization.ClientServices
{
    /// <summary>
    /// Utility class to denote an async work that is to be done
    /// </summary>
    class AsyncWorkRequest
    {
        Action<AsyncWorkRequest, object[]> _workerCallback;
        Action<object> _completedCallback;
        AsyncOperation _asyncOp;
        Guid _id;
        object[] inputParams;

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="workCallback">The call back to call which will perform the work</param>
        /// <param name="completionCallback">Callback to call when the work is done.</param>
        /// <param name="inputParams">Input parameters to be passed to the worker method.</param>
        public AsyncWorkRequest(Action<AsyncWorkRequest, object[]> workCallback, Action<object> completionCallback, params object[] inputParams)
        {
            _id = Guid.NewGuid();
            this._workerCallback = workCallback;
            this._completedCallback = completionCallback;
            this.inputParams = inputParams;
        }

        /// <summary>
        /// Worker callback
        /// </summary>
        public Action<AsyncWorkRequest, object[]> WorkerCallback
        {
            get
            {
                return this._workerCallback;
            }
        }

        /// <summary>
        /// Work completion callback
        /// </summary>
        public Action<object> CompletionCallback
        {
            get
            {
                return this._completedCallback;
            }
        }

        /// <summary>
        /// Worker method's input parameters
        /// </summary>
        public object[] InputParameters
        {
            get
            {
                return this.inputParams;
            }
        }

        /// <summary>
        /// Unique id for this work token
        /// </summary>
        public Guid Id
        {
            get
            {
                return this._id;
            }
        }

        /// <summary>
        /// The AsyncOperation that is associated for this work request
        /// </summary>
        public AsyncOperation AsyncOperation
        {
            get
            {
                return this._asyncOp;
            }
        }

        /// <summary>
        /// Called by the AsyncWorkManager when it finally manages to assign an AsyncOperation to this
        /// work request
        /// </summary>
        /// <param name="asyncOp"></param>
        internal void SetAsyncOperation(AsyncOperation asyncOp)
        {
            this._asyncOp = asyncOp;
        }
    }
}
