// <copyright file="CacheAsyncResult.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CacheAsyncResult.cs                     
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
    using System.Threading;

    internal class CacheAsyncResult : IAsyncResult
    {
        /// <summary>
        /// Callback function when GetChunk is completed. Used in asynchronous mode only.
        /// Should be null for synchronous mode.
        /// </summary>
        private AsyncCallback callback;

        /// <summary>
        /// Event is used to signal the completion of the operation
        /// </summary>
        private ManualResetEvent completeEvent = new ManualResetEvent(false);

        public string FragmentUrl { get; set; }

        public object AsyncState { get; private set; }

        public WaitHandle AsyncWaitHandle
        {
            get { return this.completeEvent; }
        }

        public bool CompletedSynchronously { get; private set; }

        public bool IsCompleted { get; private set; }

        // Contains all the output result of the GetChunk API
        public object Result { get; internal set; }

        internal TimeSpan Timestamp { get; private set; }

        /// <summary>
        /// Called when the operation is completed
        /// </summary>
        public void Complete(object result, bool completedSynchronously)
        {
            this.Result = result;
            this.CompletedSynchronously = completedSynchronously;

            this.IsCompleted = true;
            this.completeEvent.Set();

            if (null != this.callback)
            {
            }
        }
    }
}
