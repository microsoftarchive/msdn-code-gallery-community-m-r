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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Browser;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Text;
using System.Xml;
using System.Linq;
using Microsoft.Synchronization.Services.Formatters;

namespace Microsoft.Synchronization.ClientServices
{
    /// <summary>
    /// A Http transport implementation for processing a CachedRequest.
    /// </summary>
    class HttpCacheRequestHandler : CacheRequestHandler
    {
        ICredentials _credentials;
        SyncReader _syncReader;
        SyncWriter _syncWriter;
        Action<HttpWebRequest, Action<HttpWebRequest>> _beforeRequestHandler;
        Action<HttpWebResponse> _afterResponseHandler;
        AsyncWorkerManager _workerManager;
        Type[] _knownTypes;
        Dictionary<string, string> _scopeParameters;
        Dictionary<int, AsyncArgsWrapper> _requestToArgsMapper;

        public HttpCacheRequestHandler(Uri serviceUri, CacheControllerBehavior behaviors, AsyncWorkerManager manager)
            : base(serviceUri, behaviors.SerializationFormat, behaviors.ScopeName)
        {
            this._credentials = behaviors.Credentials;
            this._beforeRequestHandler = behaviors.BeforeSendingRequest;
            this._afterResponseHandler = behaviors.AfterReceivingResponse;
            this._workerManager = manager;
            this._knownTypes = new Type[behaviors.KnownTypes.Count];
            behaviors.KnownTypes.CopyTo(this._knownTypes, 0);
            this._scopeParameters = new Dictionary<string, string>(behaviors.ScopeParametersInternal);
            this._requestToArgsMapper = new Dictionary<int, AsyncArgsWrapper>();
        }

        /// <summary>
        /// Called by the CacheController when it wants this CacheRequest to be processed asynchronously.
        /// </summary>
        /// <param name="request">CacheRequest to be processed</param>
        /// <param name="state">User state object</param>
        public override void ProcessCacheRequestAsync(CacheRequest request, object state)
        {
            this._workerManager.AddWorkRequest(new AsyncWorkRequest(ProcessCacheRequestWorker, CacheRequestCompleted, request, state));
        }

        /// <summary>
        /// Actual worker performing the work
        /// </summary>
        /// <param name="worker">AsyncWorkRequest object</param>
        /// <param name="inputParams">input parameters</param>
        void ProcessCacheRequestWorker(AsyncWorkRequest worker, object[] inputParams)
        {
            Debug.Assert(inputParams.Length == 2);

            CacheRequest request = inputParams[0] as CacheRequest;
            object state = inputParams[1];

            AsyncArgsWrapper wrapper = new AsyncArgsWrapper()
            {
                UserPassedState = state,
                WorkerRequest = worker,
                CacheRequest = request
            };

            ProcessRequest(wrapper);
        }

        /// <summary>
        /// Callback invoked when the cache request has been processed.
        /// </summary>
        /// <param name="state">AsyncArgsWrapper object</param>
        void CacheRequestCompleted(object state)
        {
            // Fire the ProcessCacheRequestCompleted handler
            AsyncArgsWrapper wrapper = state as AsyncArgsWrapper;
            if (wrapper.CacheRequest.RequestType == CacheRequestType.UploadChanges)
            {
                base.OnProcessCacheRequestCompleted(
                    new ProcessCacheRequestCompletedEventArgs(
                        wrapper.CacheRequest.RequestId,
                        wrapper.UploadResponse,
                        wrapper.CacheRequest.Changes.Count,
                        wrapper.Error,
                        wrapper.UserPassedState)
                        );
            }
            else
            {
                base.OnProcessCacheRequestCompleted(
                    new ProcessCacheRequestCompletedEventArgs(
                        wrapper.CacheRequest.RequestId,
                        wrapper.DownloadResponse,
                        wrapper.Error,
                        wrapper.UserPassedState)
                        );
            }
        }

        /// <summary>
        /// Method that does the actual processing. 
        /// 1. It first creates an HttpWebRequest
        /// 2. Fills in the required method type and parameters.
        /// 3. Attaches the user specified ICredentials.
        /// 4. Serializes the input params (Server blob for downloads and input feed for uploads)
        /// 5. If user has specified an BeforeSendingRequest callback then invokes it
        /// 6. Else proceeds to issue the request
        /// </summary>
        /// <param name="wrapper">AsyncArgsWrapper object</param>
        void ProcessRequest(AsyncArgsWrapper wrapper)
        {
            try
            {
                StringBuilder requestUri = new StringBuilder();
                requestUri.AppendFormat("{0}{1}{2}/{3}",
                    base.BaseUri,
                    (base.BaseUri.ToString().EndsWith("/")) ? string.Empty : "/",
                    Uri.EscapeUriString(base.ScopeName),
                    wrapper.CacheRequest.RequestType.ToString());

                string prefix = "?";
                // Add the scope params if any
                foreach (KeyValuePair<string, string> kvp in this._scopeParameters)
                {
                    requestUri.AppendFormat("{0}{1}={2}", prefix, Uri.EscapeUriString(kvp.Key), Uri.EscapeUriString(kvp.Value));
                    if (prefix.Equals("?"))
                    {
                        prefix = "&";
                    }
                }

                // Create the WebRequest
                HttpWebRequest webRequest = null;

                if (this._credentials != null)
                {
                    // Create the Client Http request
                    webRequest = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(new Uri(requestUri.ToString()));
                    // Add credentials
                    webRequest.Credentials = this._credentials;
                }
                else
                {
                    // Use WebRequest.Create the request. This uses any user defined prefix preferences for certain paths
                    webRequest = (HttpWebRequest)WebRequest.Create(requestUri.ToString());
                }

                // Set the method type
                webRequest.Method = "POST";
                webRequest.Accept = (base.SerializationFormat == SerializationFormat.ODataAtom) ? "application/atom+xml" : "application/json";
                webRequest.ContentType = (base.SerializationFormat == SerializationFormat.ODataAtom) ? "application/atom+xml" : "application/json";

                wrapper.WebRequest = webRequest;

                // Get the request stream
                if (wrapper.CacheRequest.RequestType == CacheRequestType.UploadChanges)
                {
                    webRequest.BeginGetRequestStream(OnUploadGetRequestStreamCompleted, wrapper);
                }
                else
                {
                    webRequest.BeginGetRequestStream(OnDownloadGetRequestStreamCompleted, wrapper);
                }
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }
                wrapper.Error = e;
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
        }

        /// <summary>
        /// Callback for the Upload HttpWebRequest.beginGetRequestStream
        /// </summary>
        /// <param name="asyncResult">IAsyncResult object</param>
        void OnUploadGetRequestStreamCompleted(IAsyncResult asyncResult)
        {
            AsyncArgsWrapper wrapper = asyncResult.AsyncState as AsyncArgsWrapper;
            try
            {
                Stream requestStream = wrapper.WebRequest.EndGetRequestStream(asyncResult);

                // Create a SyncWriter to write the contents
                this._syncWriter = (base.SerializationFormat == SerializationFormat.ODataAtom)
                    ? (SyncWriter)new ODataAtomWriter(base.BaseUri)
                    : (SyncWriter)new ODataJsonWriter(base.BaseUri);

                this._syncWriter.StartFeed(wrapper.CacheRequest.IsLastBatch, wrapper.CacheRequest.KnowledgeBlob ?? new byte[0]);

                foreach (IOfflineEntity entity in wrapper.CacheRequest.Changes)
                {
                    // Skip tombstones that dont have a ID element.
                    if (entity.ServiceMetadata.IsTombstone && string.IsNullOrEmpty(entity.ServiceMetadata.Id))
                    {
                        continue;
                    }

                    string tempId = null;

                    // Check to see if this is an insert. i.e ServiceMetadata.Id is null or empty
                    if (string.IsNullOrEmpty(entity.ServiceMetadata.Id))
                    {
                        if (wrapper.TempIdToEntityMapping == null)
                        {
                            wrapper.TempIdToEntityMapping = new Dictionary<string, IOfflineEntity>();
                        }
                        tempId = Guid.NewGuid().ToString();
                        wrapper.TempIdToEntityMapping.Add(tempId, entity);
                    }

                    this._syncWriter.AddItem(entity, tempId);
                }

                if (base.SerializationFormat == SerializationFormat.ODataAtom)
                {
                    this._syncWriter.WriteFeed(XmlWriter.Create(requestStream));
                }
                else
                {
                    this._syncWriter.WriteFeed(JsonReaderWriterFactory.CreateJsonWriter(requestStream));
                }

                requestStream.Flush();
                requestStream.Close();

                if (this._beforeRequestHandler != null)
                {
                    // Invoke user code and wait for them to call back us when they are done with the input request
                    this._workerManager.PostProgress(wrapper.WorkerRequest, this.FirePreRequestHandler, wrapper);
                }
                else
                {
                    this.GetWebResponse(wrapper);
                }
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }
                wrapper.Error = e;
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
        }

        /// <summary>
        /// Callback for the Download HttpWebRequest.beginGetRequestStream
        /// </summary>
        /// <param name="asyncResult">IAsyncResult object</param>
        void OnDownloadGetRequestStreamCompleted(IAsyncResult asyncResult)
        {
            AsyncArgsWrapper wrapper = asyncResult.AsyncState as AsyncArgsWrapper;
            try
            {
                Stream requestStream = wrapper.WebRequest.EndGetRequestStream(asyncResult);

                // Create a SyncWriter to write the contents
                this._syncWriter = (base.SerializationFormat == SerializationFormat.ODataAtom)
                    ? (SyncWriter)new ODataAtomWriter(base.BaseUri)
                    : (SyncWriter)new ODataJsonWriter(base.BaseUri);

                this._syncWriter.StartFeed(wrapper.CacheRequest.IsLastBatch, wrapper.CacheRequest.KnowledgeBlob ?? new byte[0]);

                if (base.SerializationFormat == SerializationFormat.ODataAtom)
                {
                    this._syncWriter.WriteFeed(XmlWriter.Create(requestStream));
                }
                else
                {
                    this._syncWriter.WriteFeed(JsonReaderWriterFactory.CreateJsonWriter(requestStream));
                }

                requestStream.Flush();
                requestStream.Close();

                if (this._beforeRequestHandler != null)
                {
                    // Invoke user code and wait for them to call back us when they are done with the input request
                    this._workerManager.PostProgress(wrapper.WorkerRequest, this.FirePreRequestHandler, wrapper);
                }
                else
                {
                    this.GetWebResponse(wrapper);
                }
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }
                wrapper.Error = e;
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
        }

        /// <summary>
        /// Invokes the user BeforeSendingRequest callback. It also passes the resumption handler for the user
        /// to call when they are done with the customizations on the request.
        /// </summary>
        /// <param name="state">Async user state object. Ignored.</param>
        void FirePreRequestHandler(object state)
        {
            AsyncArgsWrapper wrapper = state as AsyncArgsWrapper;
            try
            {
                // Add this to the requestToArgs mapper so we can look up the args when the user calls ResumeRequestProcessing
                this._requestToArgsMapper[wrapper.WebRequest.GetHashCode()] = wrapper;

                // Invoke the user code.
                this._beforeRequestHandler(wrapper.WebRequest, ResumeRequestProcessing);
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }
                wrapper.Error = e;
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
        }

        /// <summary>
        /// Invokes the user's AfterReceivingResponse callback.
        /// </summary>
        /// <param name="wrapper">AsyncArgsWrapper object</param>
        void FirePostResponseHandler(AsyncArgsWrapper wrapper)
        {
            if (this._afterResponseHandler != null)
            {
                // Invoke the user code.
                this._afterResponseHandler(wrapper.WebResponse);
            }
        }

        /// <summary>
        /// Handler that the user will call when they want the request to resume processing.
        /// It will check to ensure that the correct WebRequest is passed back to this resumption point.
        /// Else an error will be thrown.
        /// </summary>
        /// <param name="request">HttpWebRequest for which the processing has to resume.</param>
        void ResumeRequestProcessing(HttpWebRequest request)
        {
            AsyncArgsWrapper wrapper = null;

            this._requestToArgsMapper.TryGetValue(request.GetHashCode(), out wrapper);
            if (wrapper == null)
            {
                // It means they called Resume with another WebRequest. Fail sync.
                throw new CacheControllerException("Incorrect HttpWebRequest object passed to ResumeRequestProcessing callback.");
            }

            try
            {
                this._requestToArgsMapper.Remove(request.GetHashCode());

                this.GetWebResponse(wrapper);
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }

                wrapper.Error = e;
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
        }

        /// <summary>
        /// Issues the BeginGetResponse call for the HttpWebRequest
        /// </summary>
        /// <param name="wrapper">AsyncArgsWrapper object</param>
        private void GetWebResponse(AsyncArgsWrapper wrapper)
        {
            // Send the request and wait for the response.
            if (wrapper.CacheRequest.RequestType == CacheRequestType.UploadChanges)
            {
                wrapper.WebRequest.BeginGetResponse(OnUploadGetResponseCompleted, wrapper);
            }
            else
            {
                wrapper.WebRequest.BeginGetResponse(OnDownloadGetResponseCompleted, wrapper);
            }
        }

        /// <summary>
        /// Callback for the Upload HttpWebRequest.BeginGetResponse call
        /// </summary>
        /// <param name="asyncResult">IAsyncResult object</param>
        void OnUploadGetResponseCompleted(IAsyncResult asyncResult)
        {
            AsyncArgsWrapper wrapper = asyncResult.AsyncState as AsyncArgsWrapper;

            wrapper.UploadResponse = new ChangeSetResponse();

            HttpWebResponse response = null;
            try
            {
                try
                {
                    response = wrapper.WebRequest.EndGetResponse(asyncResult) as HttpWebResponse;
                }
                catch (WebException we)
                {
                    wrapper.UploadResponse.Error = we;
                    // If we get here then it means we completed the request. Return to the original caller
                    this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
                    return;
                }
                catch (SecurityException se)
                {
                    wrapper.UploadResponse.Error = se;
                    // If we get here then it means we completed the request. Return to the original caller
                    this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
                    return;
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();

                    // Create the SyncReader
                    this._syncReader = (base.SerializationFormat == ClientServices.SerializationFormat.ODataAtom)
                        ? (SyncReader)new ODataAtomReader(responseStream, this._knownTypes)
                        : (SyncReader)new ODataJsonReader(responseStream, this._knownTypes);

                    // Read the response
                    while (this._syncReader.Next())
                    {
                        switch (this._syncReader.ItemType)
                        {
                            case ReaderItemType.Entry:                                
                                IOfflineEntity entity = this._syncReader.GetItem();
                                IOfflineEntity ackedEntity = entity;                                
                                string tempId = null;
                                
                                // If conflict only one temp ID should be set
                                if (this._syncReader.HasTempId() && this._syncReader.HasConflictTempId()) 
                                {
                                    throw new CacheControllerException(string.Format("Service returned a TempId '{0}' in both live and conflicting entities.", 
                                                                       this._syncReader.GetTempId()));
                                }

                                // Validate the live temp ID if any, before adding anything to the offline context
                                if (this._syncReader.HasTempId()) 
                                {
                                    tempId = this._syncReader.GetTempId();
                                    CheckEntityServiceMetadataAndTempIds(wrapper, entity, tempId);

                                }
                                           
                                //  If conflict 
                                if (this._syncReader.HasConflict()) 
                                {                                    
                                    Conflict conflict = this._syncReader.GetConflict();
                                    IOfflineEntity conflictEntity = (conflict is SyncConflict) ? 
                                                                    ((SyncConflict)conflict).LosingEntity : ((SyncError)conflict).ErrorEntity;

                                    // Validate conflict temp ID if any
                                    if (this._syncReader.HasConflictTempId())
                                    {
                                        tempId = this._syncReader.GetConflictTempId();
                                        CheckEntityServiceMetadataAndTempIds(wrapper, conflictEntity, tempId);
                                    }

                                    // Add conflict                                    
                                    wrapper.UploadResponse.AddConflict(conflict);
                                
                                    //
                                    // If there is a conflict and the tempId is set in the conflict entity then the client version lost the 
                                    // conflict and the live entity is the server version (ServerWins)
                                    //
                                    if (this._syncReader.HasConflictTempId() && entity.ServiceMetadata.IsTombstone)
                                    {
                                        //
                                        // This is a ServerWins conflict, or conflict error. The winning version is a tombstone without temp Id
                                        // so there is no way to map the winning entity with a temp Id. The temp Id is in the conflict so we are
                                        // using the conflict entity, which has the PK, to build a tombstone entity used to update the offline context
                                        //
                                        // In theory, we should copy the service metadata but it is the same end result as the service fills in
                                        // all the properties in the conflict entity
                                        //

                                        // Add the conflict entity                                              
                                        conflictEntity.ServiceMetadata.IsTombstone = true;
                                        ackedEntity = conflictEntity;
                                    }
                                }
                        
                                // Add ackedEntity to storage. If ackedEntity is still equal to entity then add non-conflict entity. 
                                if (!String.IsNullOrEmpty(tempId)) {
                                    wrapper.UploadResponse.AddUpdatedItem(ackedEntity);
                                }                                
                                break;

                            case ReaderItemType.SyncBlob:
                                wrapper.UploadResponse.ServerBlob = this._syncReader.GetServerBlob();
                                break;
                        }
                    }

                    if (wrapper.TempIdToEntityMapping != null && wrapper.TempIdToEntityMapping.Count != 0)
                    {
                        // The client sent some inserts which werent ack'd by the service. Throw.
                        StringBuilder builder = new StringBuilder("Server did not acknowledge with a permanent Id for the following tempId's: ");
                        builder.Append(string.Join(",", wrapper.TempIdToEntityMapping.Keys.ToArray()));
                        throw new CacheControllerException(builder.ToString());
                    }

                    wrapper.WebResponse = response;
                    // Invoke user code on the correct synchronization context.
                    this.FirePostResponseHandler(wrapper);
                }
                else
                {
                    wrapper.UploadResponse.Error = new CacheControllerException(
                        string.Format("Remote service returned error status. Status: {0}, Description: {1}",
                        response.StatusCode,
                        response.StatusDescription));
                }

                // If we get here then it means we completed the request. Return to the original caller
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }

                wrapper.Error = e;
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
        }

        private void CheckEntityServiceMetadataAndTempIds(AsyncArgsWrapper wrapper, IOfflineEntity entity, string tempId)
        {
            // Check service ID 
            if (string.IsNullOrEmpty(entity.ServiceMetadata.Id))
            {
                throw new CacheControllerException(string.Format("Service did not return a permanent Id for tempId '{0}'", tempId));
            }

            // If an entity has a temp id then it should not be a tombstone                
            if (entity.ServiceMetadata.IsTombstone) {
                throw new CacheControllerException(string.Format("Service returned a tempId '{0}' in tombstoned entity.", tempId));
            }

            // Check that the tempId was sent by client
            if (!wrapper.TempIdToEntityMapping.ContainsKey(tempId))
            {
                throw new CacheControllerException("Service returned a response for a tempId which was not uploaded by the client. TempId: " + tempId);
            }

            // Once received, remove the tempId from the mapping list.
            wrapper.TempIdToEntityMapping.Remove(tempId);            
        }

        /// <summary>
        /// Callback for the Download HttpWebRequest.beginGetRequestStream. Deserializes the response feed to
        /// retrieve the list of IOfflineEntity objects and constructs an ChangeSet for that.
        /// </summary>
        /// <param name="asyncResult">IAsyncResult object</param>
        void OnDownloadGetResponseCompleted(IAsyncResult asyncResult)
        {
            AsyncArgsWrapper wrapper = asyncResult.AsyncState as AsyncArgsWrapper;

            wrapper.DownloadResponse = new ChangeSet();

            HttpWebResponse response = null;
            try
            {
                try
                {
                    response = wrapper.WebRequest.EndGetResponse(asyncResult) as HttpWebResponse;
                }
                catch (WebException we)
                {
                    wrapper.Error = we;
                    // If we get here then it means we completed the request. Return to the original caller
                    this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
                    return;
                }
                catch (SecurityException se)
                {
                    wrapper.Error = se;
                    // If we get here then it means we completed the request. Return to the original caller
                    this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
                    return;
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();

                    // Create the SyncReader
                    this._syncReader = (base.SerializationFormat == ClientServices.SerializationFormat.ODataAtom)
                        ? (SyncReader)new ODataAtomReader(responseStream, this._knownTypes)
                        : (SyncReader)new ODataJsonReader(responseStream, this._knownTypes);

                    // Read the response
                    while (this._syncReader.Next())
                    {
                        switch (this._syncReader.ItemType)
                        {
                            case ReaderItemType.Entry:
                                wrapper.DownloadResponse.AddItem(this._syncReader.GetItem());
                                break;
                            case ReaderItemType.SyncBlob:
                                wrapper.DownloadResponse.ServerBlob = this._syncReader.GetServerBlob();
                                break;
                            case ReaderItemType.HasMoreChanges:
                                wrapper.DownloadResponse.IsLastBatch = !this._syncReader.GetHasMoreChangesValue();
                                break;
                        }
                    }

                    wrapper.WebResponse = response;
                    // Invoke user code on the correct synchronization context.
                    this.FirePostResponseHandler(wrapper);
                }
                else
                {
                    wrapper.Error = new CacheControllerException(
                        string.Format("Remote service returned error status. Status: {0}, Description: {1}",
                        response.StatusCode,
                        response.StatusDescription));
                }

                // If we get here then it means we completed the request. Return to the original caller
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
            catch (Exception e)
            {
                if (ExceptionUtility.IsFatal(e))
                {
                    throw;
                }

                wrapper.Error = e;
                this._workerManager.CompleteWorkRequest(wrapper.WorkerRequest, wrapper);
            }
        }

        /// <summary>
        /// Wrapper class that holds multiple related arguments that is passed around from
        /// async call to its completion
        /// </summary>
        class AsyncArgsWrapper
        {
            public CacheRequest CacheRequest;
            public AsyncWorkRequest WorkerRequest;
            public object UserPassedState;
            public ChangeSetResponse UploadResponse;
            public ChangeSet DownloadResponse;
            public Exception Error;
            public HttpWebRequest WebRequest;
            public HttpWebResponse WebResponse;
            public Dictionary<string, IOfflineEntity> TempIdToEntityMapping;
        }
    }
}
