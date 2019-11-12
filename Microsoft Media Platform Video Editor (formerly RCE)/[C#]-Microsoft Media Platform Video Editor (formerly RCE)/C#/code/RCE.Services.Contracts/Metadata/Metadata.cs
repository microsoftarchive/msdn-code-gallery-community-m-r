// <copyright file="Metadata.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Metadata.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.Collections.ObjectModel;
    using SMPTETimecode;

    public abstract class Metadata
    {
        public ReadOnlyCollection<MetadataField> MetadataFields { get; protected set; }

        public abstract string Title { get; }

        public abstract TimeSpan Duration { get; }

        public abstract SmpteFrameRate FrameRate { get; }

        public abstract int? Width { get; }

        public abstract int? Height { get; }
    }
}
