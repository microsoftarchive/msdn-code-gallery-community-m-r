////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;

namespace Gadgeteer.Networking
{
    ///<summary>
    /// Provides functionality to update a resource for path and to respond to incoming requests. 
    ///</summary>
    public class WebEvent
    {
        /// <summary>
        /// The path the WebEvent listens on. 
        /// </summary>
        internal string Path;
        /// <summary>
        /// An internal manager object that ensures that only one server per interface can be used. 
        /// </summary>
        internal WebServerManager manager;

        ///<summary>
        /// Represents the binary data that will be sent to the client when the web event was requested. Set by user via the Respond methods. 
        ///</summary>
        internal byte[] ResponseData { set; get; }

        /// <summary>
        /// Internal paramater that specifies the auto refresh interval on which a client will request the data agent.
        /// </summary>
        internal uint refreshAfter = 0;

        /// <summary>
        /// The content type of the data.
        /// </summary>
        internal string ContentType { get; set; }

        /// <summary>
        /// Gets the full URL of the service. 
        /// </summary>
        internal string URL
        {
            get
            {
                return manager.ServerAddress + Path;
            }
        }

        ///<summary>
        /// Creates a WebEvent with the given path.
        ///</summary>
        ///<param name="path">A string that identifies the event when an http request is received.</param>
        internal WebEvent(string path) : this()
        {
            Path = path;
            manager.AddWebEvent(this);
        }

        /// <summary>
        /// Creates a default WebEvent.
        /// </summary>
        internal WebEvent()
        {
            noHandlerSet = true;
            this.manager = WebServerManager.GetInstance();

            ContentType = "text/plain;charset=utf-8";
        }

        // Computes the response to be sent to the client. 
        internal BinaryResponseTemplate ComputeResponse()
        {
            if (ResponseData == null || ResponseData.Length == 0)
            {
                return new BinaryResponseTemplate(ContentType, null, refreshAfter);
            }
            else
            {
                return new BinaryResponseTemplate(ContentType, ResponseData, refreshAfter);
            }
        }

        /// <summary>
        /// Gets the internal id of the <see cref="WebEvent"/> resource. This id is based on the resource path http method. 
        /// The Id is unique and identifies a <see cref="WebEvent"/>.  
        /// </summary>
        /// <returns>A string that contains the id.</returns>
        internal string GetWebEventId()
        {
            return Path;
        }

        // the actual event
        private event ReceivedWebEventHandler _WebEventReceived;

        ///<summary>
        /// Delegate method that is called to handle the event when a web resource is requested.
        /// </summary>
        /// <param name="responder">Contains request data sent by the the client and functionality to respond to the request.</param>
        /// <param name="method">The incoming http method. </param>
        /// <param name="path">The path of the requested resource.</param>
        public delegate void ReceivedWebEventHandler(string path, WebServer.HttpMethod method, Responder responder);

        ///<summary>
        /// The event that is raised when a custom handler is requested.
        ///</summary>
        public event ReceivedWebEventHandler WebEventReceived
        {
            add
            {
                // add new handler
                _WebEventReceived += value;
                noHandlerSet = false;
            }
            remove
            {
                _WebEventReceived -= value;
            }
        }

        private bool noHandlerSet = true;

        private ReceivedWebEventHandler onWebEventReceived;

        /// <summary>
        /// Raises the <see cref="WebEventReceived"/> event.
        /// </summary>
        /// <param name="responder">Contains request data sent by the the client and functionality to respond to the request.</param>
        /// <param name="method">The incoming http method. </param>
        /// <param name="path">The path of the requested resource.</param>
        internal virtual void OnWebEventReceived(string path, WebServer.HttpMethod method, Responder responder)
        {
            if (onWebEventReceived == null) onWebEventReceived = new ReceivedWebEventHandler(OnWebEventReceived);

            if (_WebEventReceived == null || noHandlerSet)
            {
                responder.SendResponse();
            }
            else if (Program.CheckAndInvoke(_WebEventReceived, onWebEventReceived, path, method, responder))
            {
                _WebEventReceived(path, method, responder);
            }
        }
    }

}
