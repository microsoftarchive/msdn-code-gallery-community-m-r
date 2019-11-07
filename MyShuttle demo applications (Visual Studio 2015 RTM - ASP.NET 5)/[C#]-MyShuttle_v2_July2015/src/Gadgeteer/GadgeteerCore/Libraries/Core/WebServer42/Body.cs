////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;
using System.IO;
using System.Text;

namespace Gadgeteer.Networking
{
    /// <summary>
    /// Specifies the body of the Http request as a byte array.
    /// </summary>
    public class Body
    {
        /// <summary>
        /// Gets the posted data as a byte[] array.
        /// </summary>
        public byte[] RawContent { get; internal set; }

        /// <summary>
        /// Gets the content type of the incoming data. 
        /// </summary>
        public string ContentType { get; internal set; }

        internal Body(byte[] RawContent, string ContentType)
        {
            this.RawContent = RawContent;
            this.ContentType = ContentType;
        }

        /// <summary>
        /// Gets the incoming posted data as text, or returns null.
        /// </summary>
        public string Text
        {
            get
            {
                try
                {
                    if (RawContent != null && RawContent.Length > 0)
                    {
                        return new string(Encoding.UTF8.GetChars(RawContent));
                    }
                }
                catch
                {
                    Debug.Print("Could not decode the requested data and create text");
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the incoming posted data as a picture, or returns null.
        /// </summary>
        public Gadgeteer.Picture Picture
        {
            get
            {
                Picture p = null;

                try
                {
                    if (RawContent == null || RawContent.Length <= 0)
                    {
                        return null;
                    }



                    if (ContentType.ToLower() == "image/jpeg")
                    {
                        p = new Picture(RawContent, Picture.PictureEncoding.JPEG);
                    }
                    else if (ContentType.ToLower() == "image/gif")
                    {
                        p = new Picture(RawContent, Picture.PictureEncoding.GIF);
                    }
                    else if (ContentType.ToLower() == "image/bmp")
                    {
                        p = new Picture(RawContent, Picture.PictureEncoding.BMP);
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
        /// Gets the posted data as a stream, or returns null.
        /// </summary>
        public Stream Stream
        {
            get
            {
                if (RawContent == null || RawContent.Length <= 0)
                {
                    return null;
                }

                MemoryStream stream = new MemoryStream();

                stream.Write(RawContent, 0, RawContent.Length);
                stream.Position = 0;
                return stream;
            }
        }
    }

}
