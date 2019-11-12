////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using System.Threading;

using Gadgeteer;

namespace Gadgeteer.Networking
{
    /// <summary>
    /// Encapsulates the data returned by the server.
    /// </summary>
    public class HttpResponse
    {
        internal HttpResponse(string statusCode, byte[] rawContentBytes, string url, WebHeaderCollection headerFields, string contentType)
        {
            this.StatusCode = statusCode;
            this.RawContentBytes = rawContentBytes;
            this.URL = url;
            this.headerFields = headerFields;
            this.ContentType = contentType;
        }

        /// <summary>
        /// Private collection that contains all the header fields sent by the server. Users can access this data via 
        /// GetHeaderField(); 
        /// </summary>
        private WebHeaderCollection headerFields;

        /// <summary>
        /// The status code that the server returns.
        /// </summary>
        /// 
        public string StatusCode { get; internal set; }

        /// <summary>
        /// The mime-type of the encapsulated data.
        /// </summary>
        public string ContentType { get; internal set; }

        /// <summary>
        /// Gets the requested stream or an error message.
        /// </summary>
        public string Text
        {
            get
            {
                if (RawContentBytes != null && RawContentBytes.Length > 0)
                {
                    try
                    {
                        return new string(Encoding.UTF8.GetChars(RawContentBytes));
                    }
                    catch
                    {
                        Debug.Print("Could not decode the requested data and create text");
                        return null;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the requested data as a <see cref="T:System.IO.Stream"/> object.
        /// </summary>
        public Stream Stream
        {
            get
            {
                if (RawContentBytes == null || RawContentBytes.Length <= 0)
                {
                    return null;
                }

                MemoryStream stream = new MemoryStream();

                stream.Write(RawContentBytes, 0, RawContentBytes.Length);
                stream.Position = 0;
                return stream;
            }
        }

        /// <summary>
        /// Return the requested data as a picture or an error message.
        /// </summary>
        public Picture Picture
        {
            get
            {
                Picture p = null;
                try
                {
                    if (RawContentBytes == null || RawContentBytes.Length <= 0)
                    {
                        return null;
                    }

                    if (ContentType.ToLower() == "image/jpeg")
                    {
                        p = new Picture(RawContentBytes, Picture.PictureEncoding.JPEG);
                    }
                    else if (ContentType.ToLower() == "image/gif")
                    {
                        p = new Picture(RawContentBytes, Picture.PictureEncoding.GIF);
                    }
                    else if (ContentType.ToLower() == "image/bmp")
                    {
                        p = new Picture(RawContentBytes, Picture.PictureEncoding.BMP);
                    }
                }
                catch
                {
                    Debug.Print("Could not decode the requested data and create picture");
                }

                return p;
            }
        }

        /// <summary>
        /// Gets the raw binary data of the requested data.
        /// </summary>
        public byte[] RawContentBytes { get; internal set; }

        /// <summary>
        /// The URL requested. 
        /// </summary>
        public string URL { get; internal set; }

        /// <summary>
        /// Gets the value of the specified header field or null.
        /// </summary>
        /// <param name="name">a string that identifies the name of the header field.</param>
        /// <returns>The value of the specified header field.</returns>
        public string GetHeaderField(string name)
        {
            return headerFields[name];
        }

        /// <summary>
        /// Get the header fields that are returned by the server
        /// </summary>
        /// <returns>Header fields that are returned by the server</returns>
        public WebHeaderCollection GetWebHeaderCollection()
        {
            return headerFields;
        }
    }
}
    
