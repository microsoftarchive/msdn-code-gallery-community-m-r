// <copyright file="AddPreviewPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AddPreviewPayload.cs                     
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

    public class AddPreviewPayload
    {
        public AddPreviewPayload(string registryKey, object value, CommentMode source)
        {
            this.RegistryKey = registryKey;
            this.Value = value;
            this.Source = source;
        }

        public string RegistryKey { get; set; }

        public object Value { get; private set; }

        public CommentMode Source { get; set; }
    }
}