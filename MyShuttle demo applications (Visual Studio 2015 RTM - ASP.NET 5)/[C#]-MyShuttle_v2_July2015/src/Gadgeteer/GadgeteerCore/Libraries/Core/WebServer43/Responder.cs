////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;
using System.Collections;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace Gadgeteer.Networking
{

    /// <summary>
    /// Class used to respond to web requests. 
    /// </summary>
    public class Responder
    {
        internal WebEvent webEvent;

        internal Responder()
        {
            Responded = false;
        }

        private object _lock = new Object();

        /*
         * Responder stuff 
         */

        /// <summary>
        /// Updates the data with which the web event responds and sets the correct MIME type. 
        /// </summary>
        /// <param name="picture">Picture to be published. </param>
        public void Respond(Picture picture)
        {
            if (CheckResponded()) return;
            if (picture != null)
            {
                string tempMimeType = "application/binary";

                if (picture.Encoding == Picture.PictureEncoding.BMP)
                {
                    tempMimeType = "image/bmp";
                }
                else if (picture.Encoding == Picture.PictureEncoding.GIF)
                {
                    tempMimeType = "image/gif";
                }
                else if (picture.Encoding == Picture.PictureEncoding.JPEG)
                {
                    tempMimeType = "image/jpeg";
                }

                webEvent.ContentType = tempMimeType;
                webEvent.ResponseData = picture.PictureData;
            }
            SendResponse();
        }

        /// <summary>
        /// Updates the data with which the web event responds and sets the correct MIME type. 
        /// </summary>
        /// <param name="audioStream">An audio/mp3 stream to be published. </param>
        public void Respond(Stream audioStream)
        {
            if (CheckResponded()) return;

            if (audioStream != null)
            {
                MemoryStream mstream = new MemoryStream();
                byte[] buffer = new byte[1024];
                int bytes = 0;

                audioStream.Position = 0;

                while ((bytes = audioStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    mstream.Write(buffer, 0, bytes);
                }

                mstream.Position = 0;
                audioStream.Position = 0;

                webEvent.ContentType = "audio/mp3";
                byte[] temp = mstream.ToArray();
                webEvent.ResponseData = temp;
            }
            SendResponse();
        }

        /// <summary>
        /// Updates the data with which the web event responds and sets the correct MIME type.  
        /// </summary>
        /// <param name="text">The plain/text to be published.</param>
        public void Respond(string text)
        {
            if (CheckResponded()) return;

            if (text != null)
            {
                webEvent.ContentType = "text/plain;charset=utf-8";
                webEvent.ResponseData = Encoding.UTF8.GetBytes(text);
            }
            SendResponse();
        }

        /// <summary>
        ///  Updates the data with which the WebEvent responds. The MIME type can be set manually.
        /// </summary>
        /// <param name="data">The data to be streamed. </param>
        /// <param name="contentType">The MIME type of the outgoing data.</param>
        public void Respond(byte[] data, string contentType)
        {
            if (CheckResponded()) return;

            if (data != null)
            {
                webEvent.ContentType = contentType;
                webEvent.ResponseData = data;
            }
            SendResponse();
        }

        /// <summary>
        /// Whether or not this web responder has been responded to.
        /// </summary>
        public bool Responded { get { return _responded; } protected set { _responded = value; } }
        private bool _responded;

        private bool CheckResponded()
        {
            lock (_lock)
            {
                if (_responded)
                {
                    Debug.Print("Response already sent for web event");
                    return true;
                }
                else
                {
                    _responded = true;
                    return false;
                }
            }
        }


        internal void SendResponse()
        {
            try
            {
                BinaryResponseTemplate template = this.webEvent.ComputeResponse();

                if (template.Header != null && template.Header.Length > 0)
                {
                    this.ClientSocket.Send(template.Header, 0, template.Header.Length, SocketFlags.None);

                    if (template.Content != null && template.Content.Length > 0)
                    {
                        this.ClientSocket.Send(template.Content, 0, template.Content.Length, SocketFlags.None);
                    }
                }
            }
            catch
            {
                Debug.Print("Exception sending web event response (connection was probably terminated) ");
            }
            finally
            {
                this.ClientSocket.Close();
            }
        }

        /*
         * Properties 
         */

        /// <summary>
        /// Gets the Http method as specified by the <see cref="HttpMethod"/> enumeration.
        /// </summary>
        public WebServer.HttpMethod HttpMethod { get { return _httpMethod; } }
        private WebServer.HttpMethod _httpMethod;

        /// <summary>
        /// The header content of the message.
        /// </summary>
        public byte[] HeaderData { get { return _headerData; } }
        private byte[] _headerData;

        /// <summary>
        /// The path of the addressed web event. 
        /// </summary>
        public string Path { get { return _path; } }
        private string _path;

        /// <summary>
        /// The http version.
        /// </summary>
        public string HTTPVersion { get { return _httpVersion; } }
        private string _httpVersion;

        /// <summary>
        /// Parameters appended to the URL. 
        /// </summary>
        public IDictionary UrlParameters;

        /// <summary>
        /// Gets the value of the specified url parameter or null.
        /// </summary>
        /// <param name="name">A string that identifies the name of the parameter.</param>
        /// <returns>The value of the parameter.</returns>
        public string GetParameterValueFromURL(string name)
        {
            return (string)UrlParameters[name];
        }

        /// <summary>
        /// The client's IP address.
        /// </summary>
        public string ClientEndpoint { get; internal set; }

        /// <summary>
        /// The content type of the incoming request. 
        /// </summary>
        internal string ContentType;

        /// <summary>
        /// Internal Body element to cache the object instead of creating it each time. 
        /// </summary>
        private Body _Body;

        /// <summary>
        /// Wraps the content for PUT and POST requests.
        /// </summary>
        public Body Body
        {
            get
            {
                if (_httpMethod == WebServer.HttpMethod.POST || _httpMethod == WebServer.HttpMethod.PUT)
                {
                    if (_Body == null)
                    {
                        _Body = new Body(BodyContent, ContentType);
                        return _Body;
                    }
                    else
                    {
                        return _Body;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the value of the specified header field or null.
        /// </summary>
        /// <param name="name">A string that identifies the name of the header field.</param>
        /// <returns>The value of the specified header field.</returns>
        public string GetHeaderField(string name)
        {
            return (string)headerFields[name];
        }

        /*
         * required for the request parsing 
         */

        /// <summary>
        /// The content of the message.
        /// </summary>
        internal byte[] BodyContent;

        /// <summary>
        /// Internal paramater which keeps track of the http method.
        /// </summary>
        protected string MethodAsString;

        /// <summary>
        /// Tracks all header fields sent by the client. This can be accessed with the GetHeaderField method.
        /// </summary>
        protected Hashtable headerFields = new Hashtable();
        /// <summary>
        /// Stores the incoming data. 
        /// </summary>
        protected ArrayList receivedList = new ArrayList();
        /// <summary>
        /// Intenral parameter to store the cotnent length of the incoming data. 
        /// </summary>
        protected int contentLength = -1;
        /// <summary>
        /// Indicates whether the header section was found in an incoming request. 
        /// </summary>
        protected bool headerProcessed = false;

        /// <summary>
        /// Parses the request and fills in the request properties. 
        /// </summary>
        /// <param name="buffer">Binary data to Parse.</param>
        /// <returns>A Boolean value that indicates whether the request was parsed successfully.</returns>
        internal bool Parse(byte[] buffer)
        {
            // create one global Buffer 
            // calculate neccessary Buffer Length 
            if (buffer != null && buffer.Length > 0) receivedList.Add(buffer);

            int processedLength = 0;
            for (int i = 0; i < receivedList.Count; i++)
            {
                processedLength += ((byte[])receivedList[i]).Length;
            }

            if (!headerProcessed)
            {
                // create data array 
                byte[] rawArray = ReceivedListToByteArray(receivedList, processedLength);

                // find header section 
                if (FindHeaderSection(rawArray))
                {
                    // Debug.Print("Header found");
                    ProcessHeader();
                    // Debug.Print("Header processed");
                }
            }
            else
            {
                // put everything in received list until content-length is reached 
                // // Debug.Print("contentLength " + contentLength + " processed length " + processedLength);
                if (contentLength > processedLength)
                {
                    return false;
                }
                else
                {
                    // create final body data array 
                    // Debug.Print("processed all data " + processedLength);
                    BodyContent = ReceivedListToByteArray(receivedList, processedLength);
                    receivedList.Clear();
                    this.ContentType = GetHeaderField("Content-Type");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Extracts data from the header and fills in corresponding properties.
        /// </summary>
        protected void ProcessHeader()
        {
            // Process Header 
            string request = new string(Encoding.UTF8.GetChars(_headerData));
            char[] delimiters = new char[] { '\r', '\n' };
            string[] splittedRequest = request.Split(delimiters);

            for (int i = 0; i < splittedRequest.Length; i++)
            {
                string line = splittedRequest[i];
                line.Trim();

                if (line.Length > 0 && !line.Equals(" "))
                {
                    // used for identifying body part 
                    // *******************************************************
                    // prepare request values 
                    // *******************************************************
                    // differ between different cases
                    // generally, first parameter is GET, PUT, POST, DELETE, followed by the URL and HTTP Version
                    // seperated by 
                    if (i == 0)
                    {
                        string[] httpFirstLineParameters = line.Split(' ');
                        MethodAsString = httpFirstLineParameters[0];

                        if (MethodAsString.Equals("GET"))
                        {
                            contentLength = 0;
                            _httpMethod = WebServer.HttpMethod.GET;
                        }
                        else if (MethodAsString.Equals("POST"))
                        {
                            _httpMethod = WebServer.HttpMethod.POST;
                        }
                        else if (MethodAsString.Equals("PUT"))
                        {
                            _httpMethod = WebServer.HttpMethod.PUT;
                        }
                        else if (MethodAsString.Equals("DELETE"))
                        {
                            contentLength = 0;
                            _httpMethod = WebServer.HttpMethod.DELETE;
                        }

                        // Debug.Print("HttpMethod " + HttpMethod);

                        string path = httpFirstLineParameters[1];

                        // get the path and check for parameters 
                        path = path.Substring(1, path.Length - 1);

                        if (_httpMethod == WebServer.HttpMethod.DELETE || HttpMethod == WebServer.HttpMethod.GET)
                        {
                            string[] array = path.Split('?');
                            if (array.Length == 2)
                            {
                                _path = array[0];
                                string[] parameterTuples = array[1].Split('&');
                                UrlParameters = new Hashtable();

                                for (int t = 0; t < parameterTuples.Length; t++)
                                {
                                    string[] parameterData = ((string)parameterTuples[t]).Split('=');

                                    if (parameterData.Length == 2)
                                    {
                                        UrlParameters.Add(parameterData[0], parameterData[1]);
                                    }
                                    else
                                    {
                                        throw new Exception("Malformed URL request");
                                    }
                                }
                            }
                            else
                            {
                                _path = path;
                            }
                        }
                        else
                        {
                            _path = path;
                        }

                        _httpVersion = httpFirstLineParameters[2];
                    }
                    else
                    {
                        // put all other valuse into a hashtable for further computations, if necessary
                        string[] nextLine = line.Split(':');
                        headerFields.Add(nextLine[0], nextLine[1]);

                        if (nextLine[0].ToLower().Equals("content-length"))
                        {
                            contentLength = int.Parse(nextLine[1]);
                            // Debug.Print("content-Length found " + contentLength);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Indicates whether the header end was reached. 
        /// </summary>
        /// <param name="rawArray">Binary data received so far. </param>
        /// <returns>A Boolean value that indicates whether the end of the header was found or not. </returns>
        protected bool FindHeaderSection(byte[] rawArray)
        {
            // header section is identified by the first occurence of \r\n\r\n
            // Debug.Print("Find Header");

            for (int i = 0; i < rawArray.Length; i++)
            {
                // get the next 7 chars after i
                if ((i + 3) < rawArray.Length)
                {
                    // create the array 
                    byte[] temp = new byte[4];
                    for (int j = 0; j < temp.Length; j++)
                    {
                        temp[j] = rawArray[i + j];
                    }

                    // get chars for the current part
                    char[] headerChars = Encoding.UTF8.GetChars(temp);

                    // check if char contains \r\n\r\n sequence 
                    if (headerChars[0] == '\r'
                        && headerChars[1] == '\n'
                        && headerChars[2] == '\r'
                        && headerChars[3] == '\n')
                    {
                        // create header and body array, break 
                        // create another array containing only data from the header section, the rest of the data is body content and
                        // shall be processed by the developer 
                        headerProcessed = true;

                        _headerData = new byte[i + 4];
                        byte[] tempContent = new byte[rawArray.Length - (i + 4)];

                        // from now on, received list will contain body content only

                        int index = 0;
                        for (int j = 0; j < _headerData.Length; j++)
                        {
                            _headerData[j] = rawArray[j];
                            index = j;
                        }
                        // process the rest of the array as byte data 
                        for (int j = 0; j < tempContent.Length; j++)
                        {
                            tempContent[j] = rawArray[j + index + 1];
                        }

                        // add body data to receive list which contains now on only body content 
                        receivedList.Clear();
                        receivedList.Add(tempContent);

                        // clear raw content data 
                        rawArray = null;

                        // header processed, can stop
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Computes a byte[] array based on the first parameter. 
        /// </summary>
        /// <param name="receivedList">An <see cref="T:System.Collections.ArrayList"/> object containing received data in a byte[] format.</param>
        /// <param name="size">The number of bytes received.</param>
        /// <returns>A byte[] array that contains all data from the receivedList parameter. </returns>
        protected byte[] ReceivedListToByteArray(ArrayList receivedList, int size)
        {
            byte[] result = new byte[size];

            int lastIndex = 0;

            for (int i = 0; i < receivedList.Count; i++)
            {
                byte[] data = (byte[])receivedList[i];

                int j = 0;

                while (j < data.Length)
                {
                    result[lastIndex + i] = data[j];
                    j++;

                }
                lastIndex = lastIndex + j;
            }

            // Debug.Print("raw array created " + result.Length + "-------------------------------------------------------------");
            return result;
        }

        internal System.Net.Sockets.Socket ClientSocket;
    }

}
