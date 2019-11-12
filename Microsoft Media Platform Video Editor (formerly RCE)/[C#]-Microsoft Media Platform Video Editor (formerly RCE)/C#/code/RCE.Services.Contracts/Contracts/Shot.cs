// <copyright file="Shot.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Shot.cs                     
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
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A class that defines a shot.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class Shot : Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shot"/> class.
        /// </summary>
        public Shot()
        {
            this.Comments = new CommentCollection();   
            this.VolumeNodeCollection = new List<VolumeLevelNode>();
            this.InTransition = new TransitionItem();
            this.OutTransition = new TransitionItem();
        }

        /// <summary>
        /// Gets or sets the volume associated to the shot.
        /// </summary>
        /// <value>The volume of the shot.</value>
        [DataMember]
        public decimal Volume { get; set; }

        [DataMember]
        public string SequenceAudioStream { get; set; }

        [DataMember]
        public string SequenceVideoStream { get; set; }

        /// <summary>
        /// Gets or sets the source of the item if there is any.
        /// </summary>
        /// <value>The source of the item associated with the shot.</value>
        [DataMember]
        public Item Source { get; set; }

        /// <summary>
        /// Gets or sets the source mark in/out.
        /// </summary>
        /// <value>The mark in/out source of the shot.</value>
        [DataMember]
        public Anchor SourceAnchor { get; set; }

        /// <summary>
        /// Gets or sets the anchor mark in/out.
        /// </summary>
        /// <value>The mark in/out anchor of the shot.</value>
        [DataMember]
        public Anchor TrackAnchor { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Comment"/>s that are associated to the shot.
        /// </summary>
        /// <value>The comments collection of the shot.</value>
        [DataMember]
        public CommentCollection Comments { get; set; }

        [DataMember]
        public List<VolumeLevelNode> VolumeNodeCollection { get; set; }

        [DataMember]
        public double Balance { get; set; }

        [DataMember]
        public TransitionItem InTransition { get; set; }

        [DataMember]
        public TransitionItem OutTransition { get; set; }
    }
}
