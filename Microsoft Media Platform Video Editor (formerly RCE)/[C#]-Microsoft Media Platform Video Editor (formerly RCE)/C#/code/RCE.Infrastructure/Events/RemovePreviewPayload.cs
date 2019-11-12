// <copyright file="RemovePreviewPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: RemovePreviewPayload.cs                     
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
    using RCE.Infrastructure.Models;

    public class RemovePreviewPayload
    {
        public RemovePreviewPayload(string id, CommentMode source)
        {
            this.EventId = id;
            this.Source = source;
        }

        public string EventId { get; set; }

        public CommentMode Source { get; set; }
    }
}
