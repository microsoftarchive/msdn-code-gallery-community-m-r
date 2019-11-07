//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="CloudBlobClient.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace SLBlobUploader.Code.StorageClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Browser;
    using System.Text;
    using System.Xml;
    using PortableTPL;
    using SLBlobUploader.Code.Abstract;
    using SLBlobUploader.Code.Infrastructure;

    /// <summary>
    /// Cloud client to carry out upload operations.
    /// </summary>
    public class CloudBlobClient : IFileUploader
    {
        /// <summary>
        /// Version header tag of request.
        /// </summary>
        private const string MsVersionHeader = "x-ms-version";

        /// <summary>
        /// Request helper to generate request URIs
        /// </summary>
        private RequestGenerator request;

        /// <summary>
        /// Packets of data to be transmitted.
        /// </summary>
        private List<DataPacket> packets;

        /// <summary>
        /// File to be uploaded.
        /// </summary>
        private UserFile file;

        /// <summary>
        /// Array of tasks containing payload data.
        /// </summary>
        private Task[] allTasks;

        /// <summary>
        /// Reference of DataPacket to split data into packets.
        /// </summary>
        private DataPacket packager;

        /// <summary>
        /// Locking object to make locks.
        /// </summary>
        private object lockingObject;

        /// <summary>
        /// Tokens to cancel asynchronous tasks
        /// </summary>
        private CancellationTokenSource cancellationTokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudBlobClient"/> class.
        /// </summary>
        /// <param name="uploadUrl">The upload URL.</param>
        /// <param name="file">The file to upload.</param>
        public CloudBlobClient(Uri uploadUrl, UserFile file)
        {
            if (file != null)
            {
                this.request = new RequestGenerator(uploadUrl, file.FileName);
                this.packager = new DataPacket();
                this.lockingObject = new object();
                this.cancellationTokens = new CancellationTokenSource();
                this.file = file;
                this.packets = this.packager.TransformStreamToPackets(file.FileStream).ToList<DataPacket>();
            }
        }

        /// <summary>
        /// Occurs when the file has been uploaded or when upload is cancelled.
        /// </summary>
        public event EventHandler<UploadCompletedEventArgs> UploadFinished;

        /// <summary>
        /// Cancels the upload.
        /// </summary>
        public void CancelUpload()
        {
            if (this.cancellationTokens != null)
            {
                this.cancellationTokens.Cancel();
                this.NotifyClient(UploadCompleteReason.UserCanceled);
            }
        }

        /// <summary>
        /// Starts the upload.
        /// </summary>
        /// <param name="initParameters">The init parameters.</param>
        public void StartUpload(string initParameters)
        {
            if (this.packets.Count > 1)
            {
                int concurrencyLevel = 0;
                ////TO DO: Modify the code to work on a fixed concurrency level.
                this.allTasks = new Task[this.packets.Count];
                while (new Func<bool>(() => { return packets.Count(packet => packet.IsTransported == false) > 0 ? true : false; })())
                {
                    if (concurrencyLevel < this.packets.Count)
                    {
                        var uploadBlock = (from uploadPacket in this.packets
                                           where uploadPacket.IsTransported == false
                                           select uploadPacket).FirstOrDefault();
                        uploadBlock.IsTransported = null;
                        this.allTasks[concurrencyLevel] = Task.Factory.StartNew(
                            () => this.UploadFileChunk(uploadBlock, this.file, this.request.GetBlockBlobUri(uploadBlock.SerialNumber)),
                            this.cancellationTokens.Token);
                        concurrencyLevel++;
                    }
                    else
                    {
                        try
                        {
                            ////TO DO: This statement never hits currently. This statement halts execution of all threads due to some reason.
                            Task.WaitAll(this.allTasks);
                            concurrencyLevel = 0;
                        }
                        catch (AggregateException ex)
                        {
                            this.NotifyClient(UploadCompleteReason.ErrorOccurred, ex.Message);
                        }
                    }
                }
            }
            else
            {
                Task.Factory.StartNew(() => this.UploadFileChunk(this.packets.FirstOrDefault(), this.file), this.cancellationTokens.Token);
            }
        }

        /// <summary>
        /// Notifies the client of upload halt.
        /// </summary>
        /// <param name="haltReason">The halt reason.</param>
        /// <param name="errorMessage">The error message.</param>
        private void NotifyClient(UploadCompleteReason haltReason, string errorMessage = "")
        {
            EventHandler<UploadCompletedEventArgs> intermediateHandler = this.UploadFinished;
            if (intermediateHandler != null)
            {
                var uploadCompletedEventArgs = new UploadCompletedEventArgs()
                {
                    ErrorMessage = errorMessage,
                    Reason = haltReason,
                    UploadedFile = this.file
                };
                intermediateHandler(this, uploadCompletedEventArgs);
            }
        }

        /// <summary>
        /// Uploads the file chunk.
        /// </summary>
        /// <param name="requestPayload">The request payload.</param>
        /// <param name="fileToUpload">The file to upload.</param>
        /// <param name="blockUri">The block URI.</param>
        private void UploadFileChunk(DataPacket requestPayload, UserFile fileToUpload, Uri blockUri = null)
        {
            if (!this.cancellationTokens.IsCancellationRequested)
            {
                if (requestPayload != null && fileToUpload != null)
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(blockUri == null ? this.request.SASUrl : blockUri);
                    webRequest.Method = RequestType.Put.ToString();
                    var webRequestState = new AsyncWebRequestState()
                        {
                            WebRequestState = webRequest,
                            RequestPayload = requestPayload,
                            FileToUpload = fileToUpload
                        };
                    webRequest.BeginGetRequestStream(new AsyncCallback(this.WriteToStreamCallback), webRequestState);
                }
            }
        }

        /// <summary>
        /// Writes to stream callback.
        /// </summary>
        /// <param name="asynchronousResult">The asynchronous result.</param>
        private void WriteToStreamCallback(IAsyncResult asynchronousResult)
        {
            if (!this.cancellationTokens.IsCancellationRequested)
            {
                AsyncWebRequestState requestState = (AsyncWebRequestState)asynchronousResult.AsyncState;
                HttpWebRequest webRequest = (HttpWebRequest)requestState.WebRequestState;
                Stream requestStream = webRequest.EndGetRequestStream(asynchronousResult);
                requestStream.Write(requestState.RequestPayload.GetPayload(), 0, requestState.RequestPayload.GetPayload().Length);
                requestStream.Close();
                var webRequestState = new AsyncWebRequestState()
                    {
                        WebRequestState = webRequest,
                        RequestPayload = requestState.RequestPayload,
                        FileToUpload = requestState.FileToUpload
                    };
                webRequest.BeginGetResponse(new AsyncCallback(this.ReadHttpResponseCallback), webRequestState);
            }
        }

        /// <summary>
        /// Reads the HTTP response callback.
        /// </summary>
        /// <param name="asynchronousResult">The asynchronous result.</param>
        private void ReadHttpResponseCallback(IAsyncResult asynchronousResult)
        {
            if (!this.cancellationTokens.IsCancellationRequested)
            {
                AsyncWebRequestState requestState = (AsyncWebRequestState)asynchronousResult.AsyncState;
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)requestState.WebRequestState;
                    if (webRequest.HaveResponse && asynchronousResult.IsCompleted)
                    {
                        requestState.RequestPayload.IsTransported = true;
                    }
                }
                catch (Exception ex)
                {
                    if (requestState != null)
                    {
                        if (requestState.RequestPayload.RetryCount <= Constants.RetryLimit)
                        {
                            requestState.RequestPayload.RetryCount++;
                            this.UploadFileChunk(requestState.RequestPayload, requestState.FileToUpload);
                        }
                        else
                        {
                            this.cancellationTokens.Cancel();
                            this.NotifyClient(UploadCompleteReason.ErrorOccurred, ex.Message);
                        }
                    }
                }

                lock (this.lockingObject)
                {
                    if (this.packets.Count > 1)
                    {
                        if (this.IsUploadFinished())
                        {
                            this.PutBlockList(requestState);
                        }
                    }
                    else
                    {
                        this.NotifyClient(UploadCompleteReason.UploadCommitted);
                    }
                }
            }
        }

        /// <summary>
        /// Puts the block list.
        /// </summary>
        /// <param name="requestState">State of the request.</param>
        private void PutBlockList(AsyncWebRequestState requestState)
        {
            if (!this.cancellationTokens.IsCancellationRequested)
            {
                var webRequest = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(this.request.PutBlockBlobListUri());
                webRequest.Method = RequestType.Put.ToString();
                webRequest.Headers[MsVersionHeader] = Constants.HeaderVersion;
                requestState.WebRequestState = webRequest;
                webRequest.BeginGetRequestStream(new AsyncCallback(this.BlockListWriteToStreamCallback), requestState);
            }
        }

        /// <summary>
        /// Blocks the list write to stream callback.
        /// </summary>
        /// <param name="asynchronousResult">The asynchronous result.</param>
        private void BlockListWriteToStreamCallback(IAsyncResult asynchronousResult)
        {
            if (!this.cancellationTokens.IsCancellationRequested)
            {
                AsyncWebRequestState requestState = (AsyncWebRequestState)asynchronousResult.AsyncState;
                HttpWebRequest webRequest = (HttpWebRequest)requestState.WebRequestState;
                Stream requestStream = webRequest.EndGetRequestStream(asynchronousResult);
                var writer = XmlWriter.Create(requestStream, new XmlWriterSettings() { Encoding = Encoding.UTF8 });
                RequestGenerator.UncommittedBlockBlobList(this.packets.Count()).Save(writer);
                writer.Flush();
                requestStream.Close();
                requestState.WebRequestState = webRequest;
                webRequest.BeginGetResponse(new AsyncCallback(this.BlockListReadHttpResponseCallback), requestState);
            }
        }

        /// <summary>
        /// Blocks the list read HTTP response callback.
        /// </summary>
        /// <param name="asynchronousResult">The asynchronous result.</param>
        private void BlockListReadHttpResponseCallback(IAsyncResult asynchronousResult)
        {
            if (!this.cancellationTokens.IsCancellationRequested)
            {
                AsyncWebRequestState requestState = (AsyncWebRequestState)asynchronousResult.AsyncState;
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)requestState.WebRequestState;
                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                    StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                    reader.ReadToEnd();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    this.NotifyClient(UploadCompleteReason.ErrorOccurred, ex.Message);
                }

                this.NotifyClient(UploadCompleteReason.UploadCommitted);
            }
        }

        /// <summary>
        /// Determines whether upload has finished.
        /// </summary>
        /// <returns>
        /// 	true if upload is finished otherwise, false
        /// </returns>
        private bool IsUploadFinished()
        {
            return !(this.packets.Count(packet => packet.IsTransported == false || packet.IsTransported == null) > 0);
        }
    }
}
