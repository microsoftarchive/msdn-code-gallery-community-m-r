// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.Samples.Synchronization.ClientServices.Formatters;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    /// <summary>
    /// A Http transport implementation for processing a CachedRequest.
    /// </summary>
    class HttpCacheRequestHandler : CacheRequestHandler
    {
        ICredentials _credentials;
        SyncReader _syncReader;
        SyncWriter _syncWriter;
        Action<HttpWebRequest> _beforeRequestHandler;
        Action<HttpWebResponse> _afterResponseHandler;

        Type[] _knownTypes;
        Dictionary<string, string> _scopeParameters;

        public Dictionary<string, IOfflineEntity> TempIdToEntityMapping;

        public HttpCacheRequestHandler(Uri serviceUri, CacheControllerBehavior behaviors)
            : base(serviceUri, behaviors.ScopeName)
        {
            this._credentials = behaviors.Credentials;
            this._beforeRequestHandler = behaviors.BeforeSendingRequest;
            this._afterResponseHandler = behaviors.AfterReceivingResponse;
            this._knownTypes = new Type[behaviors.KnownTypes.Count];
            behaviors.KnownTypes.CopyTo(this._knownTypes, 0);
            this._scopeParameters = new Dictionary<string, string>(behaviors.ScopeParametersInternal);
        }

        /// <summary>
        /// Called by the CacheController when it wants this CacheRequest to be processed.
        /// </summary>
        /// <param name="request">CacheRequest to be processed</param>
        /// <param name="state">User state object</param>
        public override object ProcessCacheRequest(CacheRequest request)
        {
            StringBuilder requestUri = new StringBuilder();
            requestUri.AppendFormat("{0}{1}{2}/{3}",
                    base.BaseUri,
                    (base.BaseUri.ToString().EndsWith("/")) ? string.Empty : "/",
                    Uri.EscapeUriString(base.ScopeName),
                    request.RequestType.ToString());


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

            webRequest = (HttpWebRequest)WebRequest.Create(requestUri.ToString());
            if (this._credentials != null)
            {
                webRequest.Credentials = this._credentials;
            }

            // Set the method type
            webRequest.Method = "POST";
            webRequest.Accept = "application/atom+xml";
            webRequest.ContentType = "application/atom+xml";

            switch (request.RequestType)
            {
                case CacheRequestType.UploadChanges:
                    return ProcessUploadRequest(webRequest, request);
                case CacheRequestType.DownloadChanges:
                default:
                    return ProcessDownloadRequest(webRequest, request);
            }
        }

        private object ProcessUploadRequest(HttpWebRequest webRequest, CacheRequest request)
        {
            using (Stream memoryStream = new MemoryStream())
            {
                // Create a SyncWriter to write the contents
                this._syncWriter = new ODataAtomWriter(base.BaseUri);

                this._syncWriter.StartFeed(true, request.KnowledgeBlob ?? new byte[0]);

                foreach (IOfflineEntity entity in request.Changes)
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
                        if (TempIdToEntityMapping == null)
                        {
                            TempIdToEntityMapping = new Dictionary<string, IOfflineEntity>();
                        }
                        tempId = Guid.NewGuid().ToString();
                        TempIdToEntityMapping.Add(tempId, entity);
                    }

                    this._syncWriter.AddItem(entity, tempId);
                }

                this._syncWriter.WriteFeed(XmlWriter.Create(memoryStream));

                memoryStream.Flush();
                // Set the content length
                webRequest.ContentLength = memoryStream.Position;

                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    CopyStreamContent(memoryStream, requestStream);

                    // Close the request stream
                    requestStream.Flush();
                    requestStream.Close();
                }
            }

            // Fire the Before request handler
            this.FirePreRequestHandler(webRequest);

            // Get the response
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                ChangeSetResponse changeSetResponse = new ChangeSetResponse();

                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    // Create the SyncReader
                    this._syncReader = new ODataAtomReader(responseStream, this._knownTypes);

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
                                    CheckEntityServiceMetadataAndTempIds(TempIdToEntityMapping, entity, tempId, changeSetResponse);
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
                                        CheckEntityServiceMetadataAndTempIds(TempIdToEntityMapping, conflictEntity, tempId, changeSetResponse);
                                    }

                                    // Add conflict                                    
                                    changeSetResponse.AddConflict(conflict);
                                
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
                                    changeSetResponse.AddUpdatedItem(ackedEntity);
                                }                                

                                break;
                            case ReaderItemType.SyncBlob:
                                changeSetResponse.ServerBlob = this._syncReader.GetServerBlob();
                                break;
                        }
                    }
                }

                if (TempIdToEntityMapping != null && TempIdToEntityMapping.Count != 0)
                {
                    // The client sent some inserts which werent ack'd by the service. Throw.
                    StringBuilder builder = new StringBuilder("Server did not acknowledge with a permanant Id for the following tempId's: ");
                    builder.Append(string.Join(",", TempIdToEntityMapping.Keys.ToArray()));
                    throw new CacheControllerException(builder.ToString());
                }

                this.FirePostResponseHandler(webResponse);

                webResponse.Close();

                return changeSetResponse;
            }
            else
            {
                throw new CacheControllerException(
                                    string.Format("Remote service returned error status. Status: {0}, Description: {1}",
                                    webResponse.StatusCode,
                                    webResponse.StatusDescription));
            }
        }

        private void CheckEntityServiceMetadataAndTempIds(Dictionary<string, IOfflineEntity> tempIdToEntityMapping, 
            IOfflineEntity entity, string tempId, ChangeSetResponse response)
        {
            // Check service ID 
            if (string.IsNullOrEmpty(entity.ServiceMetadata.Id))
            {
                throw new CacheControllerException(string.Format("Service did not return a permanent Id for tempId '{0}'", tempId));
            }

            // If an entity has a temp id then it should not be a tombstone                
            if (entity.ServiceMetadata.IsTombstone)
            {
                throw new CacheControllerException(string.Format("Service returned a tempId '{0}' in tombstoned entity.", tempId));
            }

            // Check that the tempId was sent by client
            if (!tempIdToEntityMapping.ContainsKey(tempId))
            {
                throw new CacheControllerException("Service returned a response for a tempId which was not uploaded by the client. TempId: " + tempId);
            }

            // Add the entity to the Updated list.
            response.AddUpdatedItem(entity);

            // Once received, remove the tempId from the mapping list.
            tempIdToEntityMapping.Remove(tempId);
        }

        private object ProcessDownloadRequest(HttpWebRequest webRequest, CacheRequest request)
        {
            using (Stream memoryStream = new MemoryStream())
            {

                // Create a SyncWriter to write the contents
                this._syncWriter = new ODataAtomWriter(base.BaseUri);

                this._syncWriter.StartFeed(true, request.KnowledgeBlob ?? new byte[0]);

                this._syncWriter.WriteFeed(XmlWriter.Create(memoryStream));
                memoryStream.Flush();

                webRequest.ContentLength = memoryStream.Position;
                Stream requestStream = webRequest.GetRequestStream();
                CopyStreamContent(memoryStream, requestStream);

                requestStream.Flush();
                requestStream.Close();

                // Fire the Before request handler
                this.FirePreRequestHandler(webRequest);
            }

            // Get the response
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                ChangeSet changeSet = new ChangeSet();

                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    // Create the SyncReader
                    this._syncReader = new ODataAtomReader(responseStream, this._knownTypes);

                    // Read the response
                    while (this._syncReader.Next())
                    {
                        switch (this._syncReader.ItemType)
                        {
                            case ReaderItemType.Entry:
                                changeSet.AddItem(this._syncReader.GetItem());
                                break;
                            case ReaderItemType.SyncBlob:
                                changeSet.ServerBlob = this._syncReader.GetServerBlob();
                                break;
                            case ReaderItemType.HasMoreChanges:
                                changeSet.IsLastBatch = !this._syncReader.GetHasMoreChangesValue();
                                break;
                        }
                    }

                    this.FirePostResponseHandler(webResponse);
                }

                webResponse.Close();

                return changeSet;
            }
            else
            {
                throw new CacheControllerException(
                                    string.Format("Remote service returned error status. Status: {0}, Description: {1}",
                                    webResponse.StatusCode,
                                    webResponse.StatusDescription));
            }
        }

        private void CopyStreamContent(Stream src, Stream dst)
        {
            src.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[2048];
            int len = 0;
            while ((len = src.Read(buffer, 0, buffer.Length)) > 0)
            {
                dst.Write(buffer, 0, len);
            }
        }                

        /// <summary>
        /// Invokes the user BeforeSendingRequest callback. 
        /// </summary>
        /// <param name="state">Async user state object. Ignored.</param>
        void FirePreRequestHandler(HttpWebRequest request)
        {
            if (this._beforeRequestHandler != null)
            {
                // Invoke the user code.
                this._beforeRequestHandler(request);
            }
        }

        /// <summary>
        /// Invokes the user's AfterReceivingResponse callback.
        /// </summary>
        /// <param name="state">AsyncArgsWrapper object</param>
        void FirePostResponseHandler(HttpWebResponse response)
        {
            if (this._afterResponseHandler != null)
            {
                // Invoke the user code.
                this._afterResponseHandler(response);
            }
        }        
    }
}
