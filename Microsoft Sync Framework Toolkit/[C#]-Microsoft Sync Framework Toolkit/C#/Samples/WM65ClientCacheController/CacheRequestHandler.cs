// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Net;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    /// <summary>
    /// Base class that will handle the processing of a CacheRequest
    /// </summary>
    abstract class CacheRequestHandler
    {
        Uri _baseUri;
        string _scopeName;

        protected string ScopeName
        {
            get { return _scopeName; }
        }

        protected Uri BaseUri
        {
            get { return _baseUri; }
        }

        protected CacheRequestHandler(Uri baseUri, string scopeName)
        {
            this._baseUri = baseUri;
            this._scopeName = scopeName;
        }

        /// <summary>
        /// Method that will contain the actual implementation of the cache request processing.
        /// </summary>
        /// <param name="request">CacheRequest object</param>
        /// <returns>ChangeSet for a dowload request or ChangeSetResponse for an upload request</returns>
        public abstract object ProcessCacheRequest(CacheRequest request);

        /// <summary>
        /// Factory method for creating a cache handler. For labs only Http based implementation is provided.
        /// </summary>
        /// <param name="serviceUri">Base Uri to connect to</param>
        /// <param name="behaviors">The CacheControllerBehavior object</param>
        /// <returns>A CacheRequestHandler object</returns>
        public static CacheRequestHandler CreateRequestHandler(Uri serviceUri, CacheControllerBehavior behaviors)
        {
            // For labs its always Http
            return new HttpCacheRequestHandler(serviceUri, behaviors);
        }
    }
}
