// <copyright file="SmoothStreamingMetadata.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SmoothStreamingMetadata.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Metadata.Strategies.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Services.Contracts;
    using SMPTETimecode;

    public class SmoothStreamingMetadata : Metadata
    {
        private readonly IList<MetadataField> metadataFields;

        public SmoothStreamingMetadata()
        {
            this.metadataFields = new List<MetadataField>();
            this.MetadataFields = new ReadOnlyCollection<MetadataField>(this.metadataFields);
        }

        public override string Title
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override TimeSpan Duration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override SmpteFrameRate FrameRate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int? Width
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int? Height
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void AddMetadataField(MetadataField metadataField)
        {
            this.metadataFields.Add(metadataField);
        }
    }
}
