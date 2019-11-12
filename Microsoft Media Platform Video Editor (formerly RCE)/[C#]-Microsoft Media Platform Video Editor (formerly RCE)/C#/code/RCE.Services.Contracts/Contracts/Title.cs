// <copyright file="Title.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Title.cs                     
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

    /// <summary>
    /// A class that represents a Title.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class Title : RceObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Title"/> class.
        /// </summary>
        public Title()
        {
            this.TextBlockCollection = new TextBlockCollection();
        }

        /// <summary>
        /// Gets or sets the anchor mark in/out.
        /// </summary>
        /// <value>The mark in/out anchor of the title.</value>
        [DataMember]
        public Anchor TrackAnchor { get; set; }

        /// <summary>
        /// Gets or sets the title template.
        /// </summary>
        /// <value>The title template of the title.</value>
        [DataMember]
        public TitleTemplate TitleTemplate { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="TextBlock"/>s that are associated to the title.
        /// </summary>
        /// <value>The collection of TextBlock of the title.</value>
        [DataMember]
        public TextBlockCollection TextBlockCollection { get; set; }

        [DataMember]
        public Guid SequenceId { get; set; }
    }
}
