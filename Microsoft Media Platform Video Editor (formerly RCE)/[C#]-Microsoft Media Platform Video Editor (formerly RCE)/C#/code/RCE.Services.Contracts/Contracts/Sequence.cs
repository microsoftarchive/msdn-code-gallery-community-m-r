// <copyright file="Sequence.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Sequence.cs                     
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
    using System.Runtime.Serialization;

    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class Sequence
    {
        public Sequence()
        {
            this.Tracks = new TrackCollection();
            this.AdOpportunities = new AdOpportunityCollection();
            this.MarkerCollection = new MarkerCollection();
            this.CommentsCollection = new CommentCollection();
        }

        [DataMember]
        public TrackCollection Tracks { get; set; }

        [DataMember]
        public AdOpportunityCollection AdOpportunities { get; set; }

        [DataMember]
        public MarkerCollection MarkerCollection { get; set; }

        [DataMember]
        public CommentCollection CommentsCollection { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string SequenceThumbnail { get; set; }
    }
}
