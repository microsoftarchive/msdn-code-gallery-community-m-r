////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;
using System.Collections;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Gadgeteer.Networking
{
    /// <summary>
    /// Internal server manager.
    /// </summary>
    internal class WebServerManager
    {
        /// <summary>
        /// The internal server object.
        /// </summary>
        protected Server server;

        // Defines server as singleton, so that only one Webserver can be started and managed by this class.
        /// <summary>
        /// Manager is handled as a singleton so that only one server is created and managed by one manager. 
        /// </summary>
        protected static WebServerManager serverManager;

        ///<summary>
        /// Hashtable to store and manage web events.
        /// </summary>
        protected Hashtable WebEvents = new Hashtable();

        ///<summary>
        ///a default template which is displayed if a requested site does not exists 
        ///</summary>
        internal WebEvent DefaultEvent { get; set; }

        /// <summary>
        /// An internal constructor that is hidden to ensure that the GetInstance method is used 
        /// and to realize the singleton pattern.
        /// </summary>
        protected WebServerManager() { }

        /// <summary>
        /// The threshold in milliseconds when a timeout shall be sent or received. Default is 60 seconds.
        /// </summary>
        public static int Timeout { get; set; }

        /// <summary>
        /// The address of the server.
        /// </summary>
        public string ServerAddress { get; internal set; }

        /// <summary>
        /// Gets the WebServerManager instance, which is handled as a singleton. 
        /// </summary>
        /// <returns>The <see cref="T:GTM.NetworkModule.WebServerManager"/> instance</returns>
        public static WebServerManager GetInstance()
        {
            if (serverManager == null)
            {
                serverManager = new WebServerManager();
                serverManager.DefaultEvent = new WebEvent();
                serverManager.DefaultEvent.ResponseData = Encoding.UTF8.GetBytes("<html><head></head><body><h1>Hey, it works:-)</h1><p>Your own .NET Gadgeteer Web Server is up and running!</p></body></html>");
                serverManager.DefaultEvent.ContentType = "text/html;charset=utf-8";

                Timeout = 60000;

                return serverManager;
            }
            else
            {
                return serverManager;
            }
        }

        /// <summary>
        /// Gets the server port. 
        /// </summary>
        /// <returns>The server port. </returns>
        public int GetPort()
        {
            return server.Port;
        }

        /// <summary>
        /// Starts the server. 
        /// </summary>
        /// <param name="ipAddress">The IP address the server will run on.</param>
        /// <param name="Port">The port the server will run on.</param>
        /// <returns>A string that contains the server IP address.</returns>
        public string StartServer(string ipAddress, int Port)
        {
            server = new Server(this);
            ServerAddress = server.StartLocal(ipAddress, Port);
            return ServerAddress;
        }

        /// <summary>
        /// Stops the server and all services. 
        /// </summary>
        public void StopAll()
        {
            server.Stop();
        }

        /// <summary>
        /// Adds a new web event to the server.
        /// </summary>
        /// <param name="webEvent">The web event and corresponding data that the server will handle.</param>
        /// <returns>A Boolean value that indicates whether the <see cref="WebEvent"/> could be added, <b>true</b> if added, 
        /// <b>false</b>, if webEvent already exists. </returns>
        internal bool AddWebEvent(WebEvent webEvent)
        {

            if (!WebEvents.Contains(webEvent.GetWebEventId()))
            {
                WebEvents.Add(webEvent.GetWebEventId(), webEvent);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Stops and removes the given <see cref="WebEvent"/>.
        /// </summary>
        /// <param name="webEvent">The <see cref="WebEvent"/> to stop.</param>
        /// <returns>Returns <b>true</b> if the <see cref="WebEvent"/> exists and can be removed, <b>false</b> if <see cref="WebEvent"/>
        /// does not exist or cannot be stopped.</returns>
        public bool Stop(WebEvent webEvent)
        {
            if (WebEvents.Contains(webEvent.GetWebEventId()))
            {
                WebEvents.Remove(webEvent.GetWebEventId());
                return true;
            }

            // Debug.Print("Service not found");
            return false;
        }
        /// <summary>
        /// Indicates whether the server is running.
        /// </summary>
        /// <returns>Returns <b>true</b> if the server is running, otherwise <b>false</b>.</returns>
        public bool ServerIsRunning()
        {
            if (server == null)
            {
                return false;
            }
            return server.IsRunning;
        }

        /// <summary>
        /// Gets a <see cref="WebEvent"/> by its id.
        /// </summary>
        /// <param name="id">the <see cref="WebEvent"/> id.</param>
        /// <returns>The <see cref="WebEvent"/> for the specified id.</returns>
        internal WebEvent GetWebEventById(string id)
        {
            return (WebEvent)WebEvents[id];
        }

        /// <summary>
        /// Internal subclass that encapsulates the server implementation.
        /// </summary>
        protected class Server
        {
            /// <summary>
            /// The port of the locally running server.
            /// </summary>
            internal int Port = -1;

            /// <summary>
            /// Indicates whether the server is running or not.
            /// </summary>
            internal bool IsRunning = false;

            /// <summary>
            /// The socket of the locally running server.
            /// </summary>
            internal System.Net.Sockets.Socket LocalServer;

            /// <summary>
            /// The IP addres of the server. 
            /// </summary>
            internal IPAddress Host;

            /// <summary>
            /// The number of pending connections the server supports.
            /// </summary>
            internal readonly int BacklogNumber = 255;

            /// <summary>
            /// The <see cref="WebServerManager"/> object that manages the server.
            /// </summary>
            internal WebServerManager WebServerManager;

            /// <summary>
            /// Creates the <see cref="Server"/> object. 
            /// </summary>
            /// <param name="WebServerManager">The WebServerManager object that manages the server.</param>
            public Server(WebServerManager WebServerManager)
            {
                this.WebServerManager = WebServerManager;
            }

            /// <summary>
            /// Starts the server.
            /// </summary>
            /// <param name="ipAddress">the IP address the server will run on.</param>
            /// <param name="Port">The port the server will use.</param>
            /// <returns>A string that contains the server address.</returns>
            public string StartLocal(string ipAddress, int Port)
            {
                IsRunning = true;
                this.Port = Port;

                LocalServer = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Host = IPAddress.Parse(ipAddress);

                IPEndPoint endpoint = new IPEndPoint(Host, Port);

                LocalServer.Bind(endpoint);
                LocalServer.Listen(BacklogNumber);

                Thread t = new Thread(new ThreadStart(ProcessRequest));

                t.Start();
                string temp = "http://" + ipAddress + ":" + Port + "/";
                Debug.Print("Web server started at " + temp);
                return temp;
            }

            private static TimeSpan MaxProcessingTime = new TimeSpan(0, 0, 30);

            /// <summary>
            /// Processes incoming requests 
            /// </summary>
            protected void ProcessRequest()
            {
                // accept an incoming connection request and once we have one, spawn a new thread to accept more
                bool newThread = false;
                System.Net.Sockets.Socket clientSocket = null;
                Responder responder = null;
                while (IsRunning && !newThread)
                {
                    try
                    {
                        // process incoming request
                        clientSocket = LocalServer.Accept();
                        clientSocket.ReceiveTimeout = Timeout;
                        clientSocket.SendTimeout = Timeout;

                        // parse message an create an object containing parsed data 
                        responder = new Responder();
                        responder.ClientEndpoint = clientSocket.RemoteEndPoint.ToString();
                        responder.ClientSocket = clientSocket;

                        Thread t = new Thread(new ThreadStart(ProcessRequest));
                        t.Start();
                        newThread = true;
                    }
                    catch
                    {
                        if (clientSocket != null)
                        {
                            try
                            {
                                clientSocket.Close();
                            }
                            catch { }
                        }
                    }
                }

                // now process the request
                try
                {
                    bool finishedParsing = false;

                    TimeSpan parseStart = Timer.GetMachineTime();

                    while (!finishedParsing)
                    {
                        if (Timer.GetMachineTime() - parseStart > MaxProcessingTime) return;

                        // receiving data, add to an array to process later 
                        byte[] buffer = new byte[clientSocket.Available];
                        clientSocket.Receive(buffer);

                        finishedParsing = responder.parse(buffer);
                    }

                    // trigger event to get response
                    WebEvent webevent = null;

                    if (responder.Path != null)
                    {
                        webevent = serverManager.GetWebEventById(responder.Path);
                    }

                    if (webevent == null)
                    {
                        webevent = serverManager.DefaultEvent;
                    }

                    responder.webEvent = webevent;

                    webevent.OnWebEventReceived(responder.Path, responder.HttpMethod, responder);
                }
                catch
                {

                }
            }

            /// <summary>
            /// Stops the server. 
            /// </summary>
            public void Stop()
            {
                IsRunning = false;
            }
        }
    }
}
