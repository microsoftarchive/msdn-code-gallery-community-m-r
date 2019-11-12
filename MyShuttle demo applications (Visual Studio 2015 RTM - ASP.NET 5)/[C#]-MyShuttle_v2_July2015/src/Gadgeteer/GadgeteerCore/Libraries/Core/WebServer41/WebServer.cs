////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Networking
{
    /// <summary>
    /// Represents a Web Server used in a Microsoft .NET Gadgeteer application.
    /// </summary>
    public class WebServer
    {
        ///<summary>
        /// Enum that represents supported http methods.
        ///</summary>
        public enum HttpMethod
        {
            /// <summary>
            /// Http POST method.
            /// </summary>
            POST,
            /// <summary>
            /// Http GET method.
            /// </summary>
            GET,
            /// <summary>
            /// Http DELETE method.
            /// </summary>
            DELETE,
            /// <summary>
            /// Http PUT method.
            /// </summary>
            PUT
        }

        private static string _ipAddress = string.Empty;
        /// <summary>
        /// Standard port that the server runs on if the user does not specify another one. 
        /// </summary>
        private static int _port = 80;

        ///<summary>
        /// Starts the web server and configures the network for the specified port.  
        ///</summary>
        ///<param name="ipAddress">Ip address of the server</param>
        ///<param name="port">An <see cref="T:System.int"/> that specifies a port on which the web server runs.</param>
        ///<remarks>
        /// The server starts automatically when a resource is shared and the network interface is 
        /// up. If this does not happen, stop the server before sharing data. 
        /// </remarks>
        public static void StartLocalServer(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            WebServerManager.GetInstance().StartServer(_ipAddress, _port);
        }

        ///<summary>
        /// Stops the server and removes all added resources.  
        ///</summary>
        public static void StopLocalServer()
        {
            if (WebServerManager.GetInstance().ServerIsRunning())
            {
                WebServerManager.GetInstance().StopAll();
            }
        }

        /// <summary>
        /// Creates a path on the server so that an http GET request can be processed.
        /// Specifies a path of the form: http://{IP}:{port}/{path}.
        /// </summary>
        /// <param name="path">The path used to identify a resource.</param>
        /// <returns>A <see cref="T:GTM.NetworkModule.WebEvent"/> object that is used to update data and respond to incoming requests.
        /// </returns>
        public static WebEvent SetupWebEvent(string path)
        {
            WebEvent webEvent = WebServerManager.GetInstance().GetWebEventById(path);

            if (webEvent == null)
            {
                webEvent = new WebEvent(path);
            }
            return webEvent;
        }

        /// <summary>
        /// Creates a path to a resource on the server so that a http GET requests can be processed.
        /// Specifies the path of the form: http://{IP}:{port}/{path}.
        /// </summary>
        /// <param name="path">The path used to identify a resource.</param>
        /// <param name="refreshAfter">Specifies the refresh interval in seconds.</param>
        /// <returns>A <see cref="WebEvent"/> object that is used to update data and respond to incoming requests.
        /// </returns>
        public static WebEvent SetupWebEvent(string path, uint refreshAfter)
        {
            WebEvent webEvent = WebServerManager.GetInstance().GetWebEventById(path);

            if (webEvent == null)
            {
                webEvent = new WebEvent(path);
            }
            webEvent.refreshAfter = refreshAfter;
            return webEvent;
        }

        ///<summary>
        /// Stops and removes a single resource. If the resource is requesterd again, the server will return the default page. 
        ///</summary>
        ///<param name="webEvent">The <see cref="WebEvent"/> to be stopped</param>
        ///<returns>The result of the operation, <b>true</b> if the resource is found and removed, otherwise <b>false</b>.</returns>
        public static bool DisableWebEvent(WebEvent webEvent)
        {
            return WebServerManager.GetInstance().Stop(webEvent);
        }
    }
}

