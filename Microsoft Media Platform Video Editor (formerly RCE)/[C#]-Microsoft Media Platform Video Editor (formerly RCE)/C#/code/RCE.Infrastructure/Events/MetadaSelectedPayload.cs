// <copyright file="MetadaSelectedPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadaSelectedPayload.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Events
{
    using Models;

    public class MetadaSelectedPayload
    {
        public MetadaSelectedPayload(EventData eventData, CommentMode commentMode)
        {
            this.EventData = eventData;
            this.CommentMode = commentMode;
        }

        public CommentMode CommentMode
        {
            get;
            set;
        }

        public EventData EventData
        {
            get;
            set;
        }
    }
}