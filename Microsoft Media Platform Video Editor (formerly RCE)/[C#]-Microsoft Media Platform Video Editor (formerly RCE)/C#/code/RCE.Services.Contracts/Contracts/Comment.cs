// <copyright file="Comment.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Comment.cs                     
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
    /// A class the defines a comment in the RCE.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    [KnownType(typeof(InkComment))]
    public class Comment : Anchor
    {
        /// <summary>
        /// Gets or sets the text of the comment.
        /// </summary>
        /// <value>The text of the comment.</value>
        [DataMember]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the type of the comment.
        /// </summary>
        /// <value>The type of the comment.</value>
        [DataMember]
        public string Type { get; set; }
    }
}