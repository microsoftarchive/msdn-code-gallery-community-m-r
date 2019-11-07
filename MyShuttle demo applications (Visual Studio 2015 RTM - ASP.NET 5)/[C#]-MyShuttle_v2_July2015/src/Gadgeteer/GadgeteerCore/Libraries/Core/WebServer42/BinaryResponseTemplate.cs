////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;
using System.Text;

namespace Gadgeteer.Networking
{
    /// <summary>
    /// Internal class used as a template for http responses. 
    /// </summary>
    internal class BinaryResponseTemplate
    {
        /// <summary>
        /// the body content to be returned. 
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// The header content to be returned. 
        /// </summary>
        public byte[] Header { get; set; }

        /// <summary>
        /// The content type of the body content. 
        /// </summary>
        protected string ContentType;

        /// <summary>
        /// The refresh interval.
        /// </summary>
        protected uint refreshAfter;

        /// <summary>
        /// Constructor that prepares the response.
        /// </summary>
        /// <param name="ContentType">The content type to be published.</param>
        /// <param name="refreshAfter">Specifies the refresh interval of the web page.</param>
        /// <param name="Content">The binary response data.</param>
        public BinaryResponseTemplate(string ContentType, byte[] Content, uint refreshAfter)
        {
            this.Content = Content;
            this.ContentType = ContentType;
            this.refreshAfter = refreshAfter;
            string header = "";

            if (Content == null && refreshAfter <= 0)
            {
                header = "HTTP/1.0 200 OK\r\nCache-Control: no-cache\r\nConnection: Close\r\nContent-Length: 0\r\nContent-Type: " + ContentType.Trim() + "\r\n\r\n";
            }
            else if (Content != null && refreshAfter <= 0)
            {
                header = "HTTP/1.0 200 OK\r\nCache-Control: no-cache\r\nConnection: Close\r\nContent-Length: " + Content.Length + "\r\nContent-Type: " + ContentType.Trim() + "\r\n\r\n";
            }
            else if (Content == null && refreshAfter > 0)
            {
                header = "HTTP/1.0 200 OK\r\nCache-Control: no-cache\r\nConnection: Close\r\nRefresh: " + refreshAfter + "\r\nContent-Length: 0\r\nContent-Type: " + ContentType.Trim() + "\r\n\r\n";
            }
            else if (Content != null && refreshAfter > 0)
            {
                header = "HTTP/1.0 200 OK\r\nCache-Control: no-cache\r\nConnection: Close\r\nRefresh: " + refreshAfter + "\r\nContent-Length: " + Content.Length + "\r\nContent-Type: " + ContentType.Trim() + "\r\n\r\n";
            }

            Header = Encoding.UTF8.GetBytes(header);
        }
    }

}
