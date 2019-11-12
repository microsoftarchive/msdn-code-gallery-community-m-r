// <copyright file="DeleteAllPreviewsPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DeleteAllPreviewsPayload.cs                     
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
    using System.Collections.Generic;

    using RCE.Infrastructure.Models;
    using RCE.Services.Contracts;

    public class DeleteAllPreviewsPayload
    {
        public DeleteAllPreviewsPayload(CommentMode source)
        {
            this.Source = source;
        }

        public CommentMode Source { get; set; }

        public IEnumerable<PlayByPlay> ItemsToErase { get; set; }
    }
}
