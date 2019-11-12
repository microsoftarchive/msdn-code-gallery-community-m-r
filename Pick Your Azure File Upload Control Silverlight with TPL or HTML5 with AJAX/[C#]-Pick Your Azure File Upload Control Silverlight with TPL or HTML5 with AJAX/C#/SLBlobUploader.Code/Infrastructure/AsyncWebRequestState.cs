//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncWebRequestState.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace SLBlobUploader.Code.Infrastructure
{
    using System.Net;

    /// <summary>
    /// State to be propagated between web requests
    /// </summary>
    internal class AsyncWebRequestState
    {
        /// <summary>
        /// Gets or sets the state of the web request.
        /// </summary>
        /// <value>The state of the web request.</value>
        internal WebRequest WebRequestState
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the request payload.
        /// </summary>
        /// <value>The request payload.</value>
        internal DataPacket RequestPayload
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file to upload.
        /// </summary>
        /// <value>The file to upload.</value>
        internal UserFile FileToUpload
        {
            get;
            set;
        }
    }
}
