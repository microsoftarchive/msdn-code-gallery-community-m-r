////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;
using System.Collections;
using System.Text;

namespace Gadgeteer.Networking
{
    ///<summary>
    /// Abstract class used to handle different types for http content.
    ///</summary>
    public abstract class HttpContent
    {
        internal HttpContent() { }

        ///<summary>
        /// Helper method to create a byte array containing string data.
        ///</summary>
        internal static byte[] StringToByte(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
    }

    ///<summary>
    /// Encapsulates the data and functionality required for Http POST requests.
    ///</summary>
    public class POSTContent : HttpContent
    {
        internal Byte[] ByteContent;

        ///<summary>
        /// Create a <see cref="POSTContent"/> object, that is typically used for html forms. 
        ///</summary>
        ///<param name="keyValueParamaters">A <see cref="T:System.Collection.IDictionary"/> object containing key/value pairs for the http body.</param>
        ///<returns>A <see cref="POSTContent"/> object encapsulating the key/value pairs as a byte array, formatted for POST requests.</returns>
        public static POSTContent CreateWebFormData(IDictionary keyValueParamaters)
        {
            POSTContent c = new POSTContent();
            string lcontent = "";

            int counter = 0;

            foreach (DictionaryEntry entry in keyValueParamaters)
            {
                counter++;
                lcontent += entry.Key + "=" + entry.Value;

                if (counter != keyValueParamaters.Count)
                {
                    lcontent += "&";
                }
            }
            c.ByteContent = StringToByte(lcontent);
            return c;
        }

        /// <summary>
        /// Create a <see cref="POSTContent"/> object, that includes text content, typically used for JSON, XML, or plan text.
        /// </summary>
        /// <param name="content">String data that will be sent to the server.</param>
        /// <returns>A <see cref="POSTContent"/> object encapsulating the string data, formatted for POST requests.</returns>
        public static POSTContent CreateTextBasedContent(string content)
        {
            POSTContent post = new POSTContent();
            post.ByteContent = StringToByte(content);
            return post;
        }

        ///<summary>
        /// Create a <see cref="POSTContent"/> object, that includes raw byte content.
        ///</summary>
        ///<param name="data">A byte[] array containing data to be sent to the server.</param>
        ///<returns>A <see cref="POSTContent"/> object encapsulating the byte array data, formatted for POST requests.</returns>
        public static POSTContent CreateBinaryBasedContent(byte[] data)
        {
            POSTContent post = new POSTContent();
            post.ByteContent = data;
            return post;
        }
    }

    ///<summary>
    /// Encapsulates data and functionality required for Http GET request.
    ///</summary>
    public class GETContent : HttpContent
    {
        internal string stringContent;

        ///<summary>
        /// Adds passed arguments to the Url so that they can be processed by the targeted server. 
        ///</summary>
        ///<param name="keyValueParameters">A <see cref="T:System.Collection.IDictionary"/> object containing key/value pairs that will be 
        ///added to the request Url.</param>
        ///<returns>A <see cref="GETContent"/> object encapsulating key/value pairs formatted for a GET request. </returns>
        public static GETContent CreateGETParameterList(IDictionary keyValueParameters)
        {
            GETContent c = new GETContent();

            if (keyValueParameters == null || keyValueParameters.Count == 0)
            {
                return c;
            }

            string lcontent = "?";
            int counter = 0;

            foreach (DictionaryEntry entry in keyValueParameters)
            {
                counter++;
                lcontent += entry.Key + "=" + entry.Value;

                if (counter != keyValueParameters.Count)
                {
                    lcontent += "&";
                }
            }

            c.stringContent = lcontent;
            return c;
        }
    }

    ///<summary>
    /// Encapsulates data and functionality required for an Http PUT request.
    ///</summary>
    public class PUTContent : POSTContent
    {
        ///<summary>
        /// Create a <see cref="PUTContent"/> object, that is typically used for html forms. 
        ///</summary>
        ///<param name="keyValuePairs">A <see cref="T:System.Collection.IDictionary"/> object containing key/value pairs for the http body.</param>
        ///<returns>A <see cref="PUTContent"/> object encapsulating the key/value pairs as a byte array, formatted for PUT requests.</returns>
        new public static PUTContent CreateWebFormData(IDictionary keyValuePairs)
        {
            POSTContent post = POSTContent.CreateWebFormData(keyValuePairs);
            PUTContent put = new PUTContent();
            put.ByteContent = post.ByteContent;
            return put;
        }

        ///<summary>
        /// Create a <see cref="PUTContent"/> object, that includes text content, typically used for JSON, XML, or plan text.
        ///</summary>
        ///<param name="content">String data that will be sent to the server.</param>
        ///<returns>A <see cref="PUTContent"/> object encapsulating the string data, formatted for PUT requests.</returns>
        new public static PUTContent CreateTextBasedContent(string content)
        {
            POSTContent post = POSTContent.CreateTextBasedContent(content);
            PUTContent put = new PUTContent();
            put.ByteContent = post.ByteContent;
            return put;
        }

        ///<summary>
        /// Create a <see cref="PUTContent"/> object, that includes raw byte content.
        ///</summary>
        ///<param name="data">A byte[] array containing data to be sent to the server.</param>
        ///<returns>A <see cref="PUTContent"/> object encapsulating the byte array data, formatted for PUT requests.</returns>
        new public static PUTContent CreateBinaryBasedContent(byte[] data)
        {
            POSTContent post = POSTContent.CreateBinaryBasedContent(data);
            PUTContent put = new PUTContent();
            put.ByteContent = post.ByteContent;
            return put;
        }
    }

    ///<summary>
    /// Encapsulates data and functionality required for an Http DELETE request.
    ///</summary>
    public class DELETEContent : GETContent
    {
        ///<summary>
        /// Adds arguments to the URL so that these can be processed by the targeted server. 
        ///</summary>
        ///<param name="keyValueParameters">>A <see cref="T:System.Collection.IDictionary"/> object containing key/value pairs that will be 
        ///added to the request Url.</param>
        ///<returns>A <see cref="DELETEContent"/> object encapsulating key/value pairs formatted for a DELETE request. </returns>
        public static DELETEContent CreateDELETEParameterList(IDictionary keyValueParameters)
        {
            GETContent content = GETContent.CreateGETParameterList(keyValueParameters);
            DELETEContent del = new DELETEContent();
            del.stringContent = content.stringContent;
            return del;
        }
    }

}
